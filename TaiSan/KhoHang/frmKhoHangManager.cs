using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace TaiSan.KhoHang
{
    public partial class frmKhoHangManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmKhoHangManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmKhoHangManager_Load(object sender, EventArgs e)
        {
            lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, p.HoTenNV });
            gcKhoHang.DataSource = db.KhoHangs;
            lookMatBang.DataSource = db.mbMatBangs.Where(p => p.MaKH == null);
        }

        string getNewMaKho()
        {
            string MaNK = "";
            db.tmThangMay_getNewMaTM(ref MaNK);
            return db.DinhDang(24, int.Parse(MaNK));
        }

        private void grvKhoHang_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvKhoHang.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhoHangManager_Load(null, null);
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                grvKhoHang.DeleteSelectedRows();
                db.SubmitChanges();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvKhoHang.AddNewRow();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvKhoHang.UpdateCurrentRow();
            db.SubmitChanges();
            DialogBox.Alert("Đã lưu dữ liệu");
        }

        private void btnThietLapTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKhoHang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Chọn kho hàng muốn thiết lập lại trạng thái mặt bằng");
                return;
            }

            var obj = db.KhoHangs.Single(p => p.ID == (int)grvKhoHang.GetFocusedRowCellValue("ID"));
            if (obj.MaMB == null)
            {
                DialogBox.Alert("Kho hàng này chưa gán mặt bằng!!!");
                return;
            }

            frmThietLapTrangThaiMatBang frmthietlap = new frmThietLapTrangThaiMatBang();
            while (frmthietlap.objTrangThai == null)
            {
                DialogBox.Alert("Vui lòng chọn trạng thái");
                frmthietlap.ShowDialog();
            }
            obj.mbMatBang.MaTT = frmthietlap.objTrangThai.MaTT;

            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Thiết lập trạng thái mặt bằng xong");
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void grvKhoHang_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName == "MaMB")
            {
                var t = (Library.KhoHang)grvKhoHang.GetFocusedRow();
                t.Clear();
            }
        }

    }
}