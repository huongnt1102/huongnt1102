using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MSDN.Html.Editor;
using System.Linq;
using Library;
using EmailAmazon.API;

namespace EmailAmazon.Templates
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public bool IsCate = true;
        public string Content;
        public bool IsSelect { get; set; }


        MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                this.gcTemp.DataSource = (object)MailCommon.cmd.GetMauMail(MailCommon.MaHD, MailCommon.MatKhau);
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        private void LoadDetai()
        {
            try
            {
                int id = (int)this.grvTemp.GetFocusedRowCellValue("ID");
                MauMail mauMail = MailCommon.cmd.DetailMauMail(MailCommon.MaHD, MailCommon.MatKhau, id);
                htmlContent.InnerHtml = mauMail.NoiDung;
            }
            catch
            {
                htmlContent.InnerHtml = null;
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            if (IsSelect)
            {
                itemSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                itemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            this.LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? nullable = (int?)this.grvTemp.GetFocusedRowCellValue("ID");
            if (!nullable.HasValue)
            {
                DialogBox.Error("Vui lòng chọn mẫu");
            }
            else
            {
                frmEdit frmTemplates = new frmEdit();
                frmTemplates.TempID = nullable.Value;
                int num = (int)frmTemplates.ShowDialog();
                if (frmTemplates.DialogResult != DialogResult.OK)
                    return;
                this.LoadData();
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? nullable = (int?)this.grvTemp.GetFocusedRowCellValue("ID");
            if (!nullable.HasValue)
            {
                DialogBox.Error("Vui lòng chọn mẫu");
            }
            else
            {
                if (MailCommon.cmd.DeleteMauMail(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value) != Result.ThanhCong)
                    return;
                this.LoadData();
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Content = htmlContent.InnerHtml;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvTemplates_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetai();
        }
    }
}