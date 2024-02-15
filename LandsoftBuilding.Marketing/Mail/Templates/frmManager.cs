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

namespace LandSoftBuilding.Marketing.Mail.Templates
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public bool IsCate = true;
        public string Content;

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
                gcTemplates.DataSource = (from p in db.mailTemplates
                                          join nv in db.tnNhanViens on p.StaffID equals nv.MaNV
                                          join nvs in db.tnNhanViens on p.StaffModify equals nvs.MaNV into nvsua
                                          from nvs in nvsua.DefaultIfEmpty()
                                          //where nv.MaTN == (byte)itemToaNha.EditValue
                                          select new
                                          {
                                              p.TempID,
                                              p.mailCategory.CateName,
                                              p.TempName,
                                              p.Contents,
                                              nv.HoTenNV,
                                              p.DateCreate,                                              
                                              HoTenNVS = nvs.HoTenNV,
                                              p.DateModify
                                          }).ToList();
                gvTemplates.FocusedRowHandle = -1;
            }
            catch
            {
                gcTemplates.DataSource = null;
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
                htmlContent.InnerHtml = gvTemplates.GetFocusedRowCellValue("Contents").ToString();
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
            if (gvTemplates.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần sửa. Xin cám ơn!");
                return;
            }

            var frm = new frmEdit();
            frm.TempID = (int?)gvTemplates.GetFocusedRowCellValue("TempID");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvTemplates.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    var obj = db.mailTemplates.Single(p => p.TempID == (int?)gvTemplates.GetRowCellValue(i, "TempID"));
                    db.mailTemplates.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                gvTemplates.DeleteSelectedRows();
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