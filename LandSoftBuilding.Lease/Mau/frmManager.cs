using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using MSDN.Html.Editor;
using System.Linq;
using Library;

namespace LandSoftBuilding.Lease.Mau
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public bool IsCate = true;
        public string Content;
        public bool IsSelect { get; set; }
        public byte? MaTN;

        MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
            this.Load += frmManager_Load;
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                byte? maTN = (byte?)itemToaNha.EditValue;
                gcTemp.DataSource = (from p in db.HDTTemplates
                                     join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                     join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into nvsua
                                     from nvs in nvsua.DefaultIfEmpty()
                                     where p.MaTN == maTN
                                     select new
                                     {
                                         p.ID,
                                         p.TieuDe,
                                         p.NoiDung,
                                         NguoiNhap = nvn.HoTenNV,
                                         p.NgayNhap,
                                         NguoiSua = nvs.HoTenNV,
                                         p.NgaySua,
                                         p.IsCongNo,
                                     });
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

        private void LoadDetail()
        {
            try
            {
                string content = grvTemp.GetFocusedRowCellDisplayText("NoiDung").ToString();
                txtContent.RtfText = content;
            }
            catch
            {
                txtContent.RtfText = null;
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            if (this.MaTN.HasValue)
                itemToaNha.EditValue = this.MaTN;
            else
                itemToaNha.EditValue = Common.User.MaTN;

            itemUpdateField.Enabled = true;

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
            frm.MaTN = (byte?)itemToaNha.EditValue;
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
                LandSoftBuilding.Lease.Mau.Edit.frmEdit frmTemplates = new LandSoftBuilding.Lease.Mau.Edit.frmEdit();
                frmTemplates.ID = nullable.Value;
                int num = (int)frmTemplates.ShowDialog();
                if (frmTemplates.DialogResult != DialogResult.OK)
                    return;
                this.LoadData();
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? ID = (int?)this.grvTemp.GetFocusedRowCellValue("ID");
            if (!ID.HasValue)
            {
                DialogBox.Error("Vui lòng chọn mẫu");
                return;
            }

            if (DialogBox.Question("Bạn có muốn xóa không") == System.Windows.Forms.DialogResult.No) return;

            using (var db = new MasterDataContext())
            {
                var obj = db.HDTTemplates.Where(p => p.ID == ID);
                db.HDTTemplates.DeleteAllOnSubmit(obj);
                db.SubmitChanges();
            }

            this.LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Content = txtContent.HtmlText;

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
            LoadDetail();
        }

        private void itemEditTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (LandSoftBuilding.Lease.Mau.frmDesign frm = new LandSoftBuilding.Lease.Mau.frmDesign())
            {
                int? ID = (int?)this.grvTemp.GetFocusedRowCellValue("ID");
                if (!ID.HasValue)
                {
                    DialogBox.Error("Vui lòng chọn mẫu");
                }
                else
                {
                    using (var db = new MasterDataContext())
                    {
                        HDTTemplate temp = db.HDTTemplates.Single(p => p.ID == ID);
                        frm.RtfText = temp.NoiDung;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            temp.NoiDung = frm.RtfText;
                            db.SubmitChanges();

                            LoadData();
                            LoadDetail();
                        }
                    }
                }
            }
        }

        private void grvTemp_AsyncCompleted(object sender, EventArgs e)
        {
            LoadDetail();
        }
    }
}