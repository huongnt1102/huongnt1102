using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Utils;
using System.Threading.Tasks;
using System.Collections;
using DevExpress.Data;

namespace Building.Survey.Questions
{
    public partial class frmQuestionManager : DevExpress.XtraEditors.XtraForm
    {
        public frmQuestionManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

        }

        void LoadData()
        {
            using (var db = new MasterDataContext())
                gcQuestion.DataSource = db.SV_QUESTIONs.Select(o => new { o.SV_QUESTION_TYPE.question_type_name, o.question_name }).ToList();
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvQuestion.GetFocusedRowCellValue("question_id");

            if(id == null)
            {
                DialogBox.Error("Vui lòng chọn câu hỏi");
                return;
            }

            using (var frm = new frmEditQuestion())
            {
                frm.question_id = id;
                frm.ShowDialog();
            }
            LoadData();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var list = gvQuestion.GetSelectedRows();

            if (list.Count() == 0)
            {
                DialogBox.Error("Vui lòng chọn câu hỏi");
                return;
            }

            using (var db = new MasterDataContext())
            {
                foreach (var i in list)
                {
                    try
                    {
                        var obj = db.SV_QUESTIONs.FirstOrDefault(o => o.question_id == (int?)gvQuestion.GetRowCellValue(i, "question_id"));
                        db.SV_QUESTIONs.DeleteOnSubmit(obj);
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Error("Lỗi: " + ex.Message);
                        return;
                    }
                }

                db.SubmitChanges();
                DialogBox.Success("Dữ liệu đã được cập nhật!!!");
                LoadData();
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmEditQuestion())
                frm.ShowDialog();

            LoadData();
        }

    }
    
}