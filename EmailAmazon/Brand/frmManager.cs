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

namespace EmailAmazon.Brand
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public bool IsCate = true;
        public string Content;

        MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
            MailCommon.cmd = new APISoapClient();
            MailCommon.cmd.Open();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                this.gcConfig.DataSource = (object)MailCommon.cmd.GetThuongHieu(MailCommon.MaHD, MailCommon.MatKhau);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
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
                htmlContent.InnerHtml = grvConfig.GetFocusedRowCellValue("Contents").ToString();
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

            if (IsCate)
            {
                itemSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            if (frm.DialogResult != DialogResult.OK)
                return;
            this.LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? nullable = (int?)this.grvConfig.GetFocusedRowCellValue("ID");
            if (!nullable.HasValue)
            {
                DialogBox.Error("Vui lòng chọn thương hiệu");
            }
            else
            {
                frmEdit frmConfig = new frmEdit();
                frmConfig.ID = nullable.Value;
                int num = (int)frmConfig.ShowDialog();
                if (frmConfig.DialogResult != DialogResult.OK)
                    return;
                this.LoadData();
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int? nullable = (int?)this.grvConfig.GetFocusedRowCellValue("ID");
                if (!nullable.HasValue)
                {
                    DialogBox.Error("Vui lòng chọn thương hiệu");
                }
                else
                {
                    if (DialogBox.Question("Bạn có muốn xóa không") == DialogResult.No)
                        return;
                    switch (MailCommon.cmd.DeleteThuongHieu(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value))
                    {
                        case Result.DaSuDung:
                            DialogBox.Error("Thương hiệu này đã sử dụng, không thể xóa");
                            break;
                        case Result.ThanhCong:
                            this.grvConfig.DeleteRow(this.grvConfig.FocusedRowHandle);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
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