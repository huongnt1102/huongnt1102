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

namespace KyThuat.NhiemVu
{
    public partial class frmUpdateEquipmentcs : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public tnNhanVien objNV;
        public int? MaNVTH { get; set; }
        btDauMucCongViec_NhanVien objCVNV;
        public long? MaDMCV { get; set; }
        public frmUpdateEquipmentcs()
        {
            InitializeComponent();
        }

        private void frmUpdateEquipmentcs_Load(object sender, EventArgs e)
        {

            gcThietBi.DataSource = db.btDauMucCongViec_ThietBis.Where(p=>p.MaCVBT==MaDMCV);
            colMaTB.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objNV);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (grvThietBi.FocusedRowHandle < 0)
                return;
            grvThietBi.DeleteSelectedRows();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = db.btDauMucCongViec_NhanViens.SingleOrDefault(p=>p.MaCVBT==MaDMCV & p.MaNVTH == MaNVTH);
                var objLS = new btDauMucCongViec_NhanVienLSTH();
                objLS.DienGiai = "Xác nhận hoàn thành công việc";
                objLS.MaNV = objNV.MaNV;
                objLS.NgayTH = DateTime.Now;
                objLS.TienDo = obj.TienDo;
                objLS.TrangThai = obj.TrangThai;
                objLS.DienGiai = "Cập nhật vật tư sử dụng.";
                db.btDauMucCongViec_NhanVienLSTHs.InsertOnSubmit(objLS);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã cập nhật thành công!");
            }
            catch 
            {
                DialogBox.Error("Dữ liệu không thể cập nhật!");
            }
            this.Close();

        }

        private void grvThietBi_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                sckhThietBi objTB = (sckhThietBi)grvThietBi.GetRow(e.RowHandle);
                objTB.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvThietBi_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong" | e.Column.FieldName == "DonGia")
            {
                if (grvThietBi.GetFocusedRowCellValue("SoLuong") != null &&
                    grvThietBi.GetFocusedRowCellValue("DonGia") != null)
                {
                    decimal ThanhTien = (int)grvThietBi.GetFocusedRowCellValue("SoLuong") *
                        (decimal)grvThietBi.GetFocusedRowCellValue("DonGia");
                    grvThietBi.SetFocusedRowCellValue("ThanhTien", ThanhTien);
                }
            }
        }

        private void grvThietBi_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvThietBi.SetFocusedRowCellValue("MaCVBT", MaDMCV);
        }
            
    }
}