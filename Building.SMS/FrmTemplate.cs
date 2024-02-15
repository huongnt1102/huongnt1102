using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.SMS
{
    public partial class FrmTemplate : DevExpress.XtraEditors.XtraForm
    {
        public FrmTemplate()
        {
            InitializeComponent();
        }

        private void FrmSmsTemplate_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var db = new MasterDataContext();
                gc.DataSource = db.SmsTemplateDvs;
            }
            catch
            {

            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FrmEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn mẫu cần sửa");
                return;
            }
            var frm = new FrmEdit
            {
                //MaTn = (byte)barEditItemToaNha.EditValue,
                //IsSua = 1,
                FormId = (int)id
            };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var db = new MasterDataContext();

                var obj = db.SmsTemplateDvs.FirstOrDefault(_ => _.Id == (int)gv.GetFocusedRowCellValue("Id"));
                if (obj != null)
                {
                    db.SmsTemplateDvs.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }
    }
}