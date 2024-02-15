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

namespace Building.Survey
{
    public partial class frmSurveyManager : DevExpress.XtraEditors.XtraForm
    {
        public frmSurveyManager()
        {
            InitializeComponent();
            gvQuestion.FocusedRowLoaded += gvQuestion_FocusedRowLoaded;
            gvQuestion.FocusedRowChanged += gvQuestion_FocusedRowChanged;
        }

        void gvQuestion_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadAnswer();
        }

        void gvQuestion_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            loadAnswer();
        }

        void loadAnswer()
        {
            var id = (int?)gvQuestion.GetFocusedRowCellValue("ID");
            if (id == null)
                gcAnswer.DataSource = null;

            using (var db = new MasterDataContext())
            {
                gcAnswer.DataSource = db.svAnswers.Where(o => o.idQuestion == id).Select(o => new { answer_name = o.answer_name }).ToList();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            //gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
            LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        #region

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        #endregion

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmEdit())
            {
                frm.tower_id = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
            }
            LoadData();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvSurvey.GetFocusedRowCellValue("ID");

            using (var frm = new frmEdit())
            {
                frm.tower_id = (byte?)itemToaNha.EditValue;
                frm.idSV = id;
                frm.ShowDialog();
            }
            LoadData();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var obj = gvSurvey.GetSelectedRows();

            if (obj.Count() == 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu khảo sát");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            using (var db = new MasterDataContext())
            {
                foreach (var i in obj)
                {
                    var sv = db.svSurveys.Single(o => o.ID == (int?)gvSurvey.GetRowCellValue(i, "ID"));
                    db.svSurveys.DeleteOnSubmit(sv);

                }

                try
                {
                    db.SubmitChanges();
                    DialogBox.Success("Dữ liệu đã được cập nhật");

                }
                catch (Exception ex)
                {
                    DialogBox.Error("Lỗi: " + ex.Message);
                }
            }

            LoadData();
        }

        void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var towerID = (byte?)itemToaNha.EditValue;
                var dateFr = (DateTime?)itemTuNgay.EditValue;
                var dateTo = (DateTime?)itemDenNgay.EditValue;

                gcSurvey.DataSource = (from s in db.svSurveys
                                       join nvn in db.tnNhanViens on s.usercreate equals nvn.MaNV
                                       join nvs in db.tnNhanViens on s.userchange equals nvs.MaNV into dsnvs
                                       from nvs in dsnvs.DefaultIfEmpty()
                                       where s.towerID == towerID
                                       & SqlMethods.DateDiffDay(dateFr, s.datecreate) >= 0
                                       & SqlMethods.DateDiffDay(s.datecreate, dateTo) >= 0
                                       select new
                                       {
                                           s.ID,
                                           s.surveycode,
                                           s.surveyname,
                                           s.isactive,
                                           s.usercreate,
                                           s.datecreate,
                                           createby = nvn.HoTenNV,
                                           updateby = nvs.HoTenNV,
                                       }).ToList();
            }
        }

        void LoadDetail()
        {
            var id = (int?)gvSurvey.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                //DialogBox.Error("Vui lòng chọn phiếu khảo sát");
                return;
            }
            using (var db = new MasterDataContext())
                gcQuestion.DataSource = (from s in db.svQuestions
                                         join t in db.svAnswerTypes on s.answer_type equals t.ID
                                         where s.idSurvey == id
                                         select new
                                         {
                                             s.ID,
                                             s.question_name,
                                             t.answer_type_name,
                                         }).ToList();
        }

        private void gvSurvey_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void gvSurvey_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            LoadDetail();
        }

        private void itemDeactive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvSurvey.GetFocusedRowCellValue("survey_id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu khảo sát");
                return;
            }
            using (var db = new MasterDataContext())
            {
                var obj = db.svSurveys.Single(o => o.ID == id);
                obj.isactive = false;
                db.SubmitChanges();
                DialogBox.Success("Dữ liệu đã được cập nhật !!!");
            }

            LoadData();
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
    
}