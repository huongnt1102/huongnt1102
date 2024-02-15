using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Collections.Generic;

namespace Building.AppVime
{
    public partial class frmConfigDate : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();

        public byte? TowerId { get; set; }

        public frmConfigDate()
        {
            InitializeComponent();
            setRepository();
        }

        void LoadData()
        {
                var _MaTN = (byte)itemToaNha.EditValue;

                db = new MasterDataContext();
            gcData.DataSource = db.app_ConfigDates.Where(c => c.MaTN.Equals(_MaTN));//.Select(c=> new { c.MaLDV, c.TuNgay,c.TuNgayFunc, c.DenNgay, c.DenNgayFunc,c.NgayTT, c.NgayTTFunc, c.MaTN, TyLeVAT = c.TyLeVAT== null ? 0: c.TyLeVAT, BVMT = c.BVMT ==null? 0 : c.BVMT});
                
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            gvData.DeleteSelectedRows(); 
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            //TranslateLanguage.TranslateControl(this, barManager1);
           // Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            gvData.InvalidRowException += Library.Common.InvalidRowException;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = TowerId;
            //itemToaNha.EditValue = Common.User.MaTN;
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                glkLoaiDichVu.DataSource = db.dvLoaiDichVus.Where(c => c.IsConfigDateApp == true);
                var func = new List<FuncItem>();
                func.Add(new FuncItem { MaFunc = "PM", TenFunc = "Tháng trước" });
                func.Add(new FuncItem { MaFunc = "IM", TenFunc = "Tháng này" });
                func.Add(new FuncItem { MaFunc = "NM", TenFunc = "Tháng sau" });
                glkFunction.DataSource = func;
            }
            catch { }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gvData.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvData.UpdateCurrentRow();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }

            LoadData();
        }

        private void gcDinhMucNuoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
        }

        private void gvDinhMucNuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //var maTN = gvData.GetRowCellValue(e.RowHandle, "MaTN");
            var obj = (app_ConfigDate)gvData.GetFocusedRow() ;
            if (obj.MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn [Tòa Nhà] !";
                return;
            }

            if (obj.TuNgay == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [Từ ngày] !";
                return;
            }

            if (obj.TuNgayFunc == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [Tháng từ ngày] !";
                return;
            }

            if (obj.DenNgay == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [Đến ngày] !";
                return;
            }

            if (obj.DenNgayFunc == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [Tháng đến ngày] !";
                return;
            }

            if (obj.NgayTT == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [Ngày thanh toán] !";
                return;
            }

            if (obj.NgayTTFunc == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [Tháng ngày thanh toán] !";
                return;
            }

            //var tenDM = (gvData.GetRowCellValue(e.RowHandle, "TenDM") ?? "").ToString();
            //if (tenDM.Length == 0)
            //{
            //    e.Valid = false;
            //    e.ErrorText = "Vui lòng nhập Tên Định Mức !";
            //    return;
            //}
            //else if (Common.Duplication(gvData, e.RowHandle, "TenDM", tenDM))
            //{
            //    e.Valid = false;
            //    e.ErrorText = "Tên Định Mức trùng, vui lòng nhập lại !";
            //    return;
            //}
        }

        private void gvDinhMucNuoc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvData.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private class FuncItem
        {
            public string MaFunc { get; set; }
            public string TenFunc { get; set; }
        }
       
        private void setRepository()
        {
            gvData.Columns["MaLDV"].ColumnEdit = glkLoaiDichVu;
            gvData.Columns["TuNgayFunc"].ColumnEdit = glkFunction;
            gvData.Columns["DenNgayFunc"].ColumnEdit = glkFunction;
            gvData.Columns["NgayTTFunc"].ColumnEdit = glkFunction;
        }
    }
}