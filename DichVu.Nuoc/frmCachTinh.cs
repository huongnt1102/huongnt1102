using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace DichVu.Nuoc
{
    public partial class frmCachTinh : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();

        public frmCachTinh()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _MaLMB = (int?)itemLoaiMatBang.EditValue;
                var _MaMB = (int?)itemMatBang.EditValue;

                if (_MaMB != null)
                    gcCachTinh.DataSource = db.dvNuocCachTinhs.Where(p => p.MaMB == _MaMB);
                else if (_MaLMB != null)
                    gcCachTinh.DataSource = db.dvNuocCachTinhs.Where(p => p.MaLMB == _MaLMB & p.MaMB == null);
                else
                    gcCachTinh.DataSource = db.dvNuocCachTinhs.Where(p => p.MaTN == _MaTN & p.MaLMB == null & p.MaMB == null);
            }
            catch
            {
                gcCachTinh.DataSource = null;
            }
        }

        private void frmCachTinh_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            
            gvCachTinh.InvalidRowException += Library.Common.InvalidRowException;
            gcCachTinh.KeyUp += Library.Common.GridViewKeyUp;

            #region Load cach tinh
            var ltCachTinh = new List<CachTinhItem>();
            ltCachTinh.Add(new CachTinhItem() { ID = 1, TenCT = "Lũy tiến", DienGiai = "Phương pháp lũy tiến" });
            ltCachTinh.Add(new CachTinhItem() { ID = 2, TenCT = "Ưu đãi", DienGiai = "Tính theo phương pháp lũy tiến nhưng mức 1 = số m3 ưu đãi, Mức 2 = 1/2 số m3 ưu đãi" });
            lkCachTinh.DataSource = ltCachTinh;
            #endregion
            
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                lkLoaiMatBang.DataSource = (from l in db.mbLoaiMatBangs
                                              where l.MaTN == _MaTN
                                              select new { l.MaLMB, l.TenLMB })
                                            .ToList();
                itemLoaiMatBang.EditValue = null;

                glkMatBang.DataSource = (from mb in db.mbMatBangs
                                         join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                         join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                         where mb.MaTN == _MaTN
                                         select new { mb.MaMB, mb.MaSoMB, tl.TenTL, kn.TenKN })
                                         .ToList();
                itemMatBang.EditValue = null;
            }
            catch { }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvCachTinh.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                gvCachTinh.DeleteSelectedRows();
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvCachTinh.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvCachTinhNuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var _MaTN = gvCachTinh.GetRowCellValue(e.RowHandle, "MaTN");
            if (_MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án";
                return;
            }

            var _MaCT = gvCachTinh.GetRowCellValue(e.RowHandle, "MaCT");
            if (_MaCT == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn cách tính";
                return;
            }
        }

        private void gvCachTinhNuoc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvCachTinh.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvCachTinh.SetFocusedRowCellValue("MaLMB", itemLoaiMatBang.EditValue);  
            gvCachTinh.SetFocusedRowCellValue("MaMB", itemMatBang.EditValue);
        }

        private void lkLoaiMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                itemLoaiMatBang.EditValue = null;
            }
        }

        private void glkMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                itemMatBang.EditValue = null;
            }
        }

        private void lkCachTinh_EditValueChanged(object sender, EventArgs e)
        {
            gvCachTinh.SetFocusedRowCellValue("DienGiai", (sender as LookUpEdit).GetColumnValue("DienGiai"));
        }
    }

    public class CachTinhItem
    {
        public int ID { get; set; }
        public string TenCT { get; set; }
        public string DienGiai { get; set; }
    }
}