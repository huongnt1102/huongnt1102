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

namespace KyThuat.SuaChua
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public sckhSuaChua objSC;
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        btDauMucCongViec objDMCV;
        public long? MaDMCV { get; set; }

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        string getNewMaSC()
        {
            string MaSC = "";
            db.sckhSuaChua_getNewMaSC(ref MaSC);
            return db.DinhDang(21, int.Parse(MaSC));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
           // txtMaSoSC.Properties.PasswordChar = Convert.ToChar("*");
            long? ID;
            //Nhan vien
            lookNhanSu.DataSource = db.tnNhanViens
                .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
            //Mat bang
            slookMatBang.Properties.DataSource = db.mbMatBangs
                .Select(p => new { p.MaMB,TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen, p.MaSoMB, p.mbTangLau.TenTL, p.mbTangLau.mbKhoiNha.TenKN, p.mbTangLau.mbKhoiNha.tnToaNha.TenTN, p.DienGiai, p.MaKH });
            //Khach hang
            //lookKhachHang.Properties.DataSource = db.tnKhachHangs
            //    .Select(p => new { p.MaKH, TenKH = (bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen, p.MaSoThue, p.DienThoaiKH, p.CtyDiaChi });
            slookKhachHang.Properties.DataSource = db.tnKhachHangs
                .Select(p => new { p.MaKH, TenKH = (bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen, p.MaSoThue, p.DienThoaiKH, CtyDiaChi=p.DCLL });
            colMaTB.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objnhanvien);

            //khi load cong viec tu dau muc cong viec


            if (this.objSC != null)
            {
                objSC = db.sckhSuaChuas.Single(p => p.MaSC == objSC.MaSC);
                txtMaSoSC.Text = objSC.MaSoSC;
                dateNgaySC.DateTime = (DateTime)objSC.NgaySC;
                slookMatBang.EditValue = objSC.MaMB;
               // lookKhachHang.EditValue = objSC.MaKH;
                slookKhachHang.EditValue = objSC.MaKH;
                spinPhiSC.EditValue = objSC.PhiSC;
                spinDaThanhToan.EditValue = objSC.DaTT;
                txtDienGiai.Text = objSC.DienGiai;
                objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == objSC.MaDMCV);
                txtMaSoDMCV.Text = objDMCV.MaSoCV;
                ID = objSC.MaDMCV;
            }
            else
            {
                objSC = new sckhSuaChua();
                objSC.MaSoSC = getNewMaSC();
                objSC.MaTN = objnhanvien.MaTN;
                db.sckhSuaChuas.InsertOnSubmit(objSC);

                txtMaSoSC.Text = getNewMaSC();
                dateNgaySC.DateTime = db.GetSystemDate();
                if (MaDMCV != null)
                {
                    objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaDMCV);
                    txtMaSoDMCV.Text = objDMCV.MaSoCV;
                    slookMatBang.EditValue = objDMCV.MaMB;
                    slookKhachHang.EditValue = objDMCV.mbMatBang.MaKH;
                   // lookKhachHang.EditValue = objDMCV.mbMatBang.MaKH;
                    txtDienGiai.Text = objDMCV.MoTa;
                    dateNgaySC.EditValue = objDMCV.ThoiGianTH;
                    ID = MaDMCV;
                }
            }

            gcThietBi.DataSource = objDMCV.btDauMucCongViec_ThietBis;
            gcNhanSu.DataSource = objDMCV.btDauMucCongViec_NhanViens;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal TongTien = 0;
            try
            {
                objSC.MaSoSC = txtMaSoSC.Text;
                objSC.NgaySC = dateNgaySC.DateTime;
                objSC.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == objnhanvien.MaNV);
                objSC.tnKhachHang = db.tnKhachHangs.Single(p => p.MaKH == (int)slookKhachHang.EditValue);

                objSC.mbMatBang = db.mbMatBangs.Single(p => p.MaMB == (int)slookMatBang.EditValue & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN);
                objSC.PhiSC = spinPhiSC.Value;
                objSC.DaTT = (decimal?)spinDaThanhToan.EditValue;
                objSC.DienGiai = txtDienGiai.Text;
                objSC.MaTN = objnhanvien.MaTN;
                objSC.MaDMCV = objDMCV.ID;
                for (int i = 0; i < grvThietBi.RowCount; i++)
                {
                    TongTien += (grvThietBi.GetRowCellValue(i, "ThanhTien") == null ? 0 : (decimal)grvThietBi.GetRowCellValue(i, "ThanhTien"));
                }
                TongTien += (decimal)spinPhiSC.EditValue;
                objDMCV.ChiPhi = Convert.ToDouble(TongTien);
            }
            catch
            {
                DialogBox.Alert("Vui lòng kiểm tra lại các giá trị nhập vào");
            }

            Save:
            try
            {
                db.SubmitChanges();
            }
            catch
            {
                objSC.MaSoSC = getNewMaSC();
                for (int i = 0; i < grvThietBi.RowCount - 1; i++)
                    grvThietBi.SetRowCellValue(i, "MaSC", objSC.MaSC);
                for (int i = 0; i < grvNhanSu.RowCount - 1; i++)
                    grvNhanSu.SetRowCellValue(i, "MaSC", objSC.MaSC);
                goto Save;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grvThietBi_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvThietBi.SetFocusedRowCellValue("MaSC", objSC.MaSC);
        }

        private void grvNhanSu_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvNhanSu.SetFocusedRowCellValue("MaSC", objSC.MaSC);
        }

        private void grvThietBi_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                sckhThietBi objTB = (sckhThietBi)grvThietBi.GetRow(e.RowHandle);
                objTB.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvNhanSu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaNV")
            {
                sckhNhanSu objNV = (sckhNhanSu)grvNhanSu.GetRow(e.RowHandle);
                objNV.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)e.Value);
            }
        }

        private void grvThietBi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvThietBi.DeleteSelectedRows();
        }

        private void grvNhanSu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvNhanSu.DeleteSelectedRows();
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

        private void slookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            if (slookKhachHang.EditValue != null)
                slookKhachHang.EditValue = db.mbMatBangs.SingleOrDefault(p => p.MaMB == (int?)slookMatBang.EditValue).MaKH;
        }
    }
}