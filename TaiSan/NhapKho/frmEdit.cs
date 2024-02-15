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

namespace TaiSan.NhapKho
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public nkNhapKho objNK;
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        bool EditMode = false;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
            slookTaiSan.EditValueChanged += new EventHandler(slookTaiSan_EditValueChanged);
        }

        void slookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var LookTS = (SearchLookUpEdit)sender;
            if (LookTS.EditValue != null)
            {
                var obj = db.Khos.SingleOrDefault(p => p.ID == (int?)LookTS.EditValue);
                grvTaiSan.SetFocusedRowCellValue("MaNCC", obj.MaNCC);
                grvTaiSan.SetFocusedRowCellValue("SoLuong", obj.SoLuong);
                grvTaiSan.SetFocusedRowCellValue("DonGia", obj.DonGia);
            }
        }

        string getNewMaNK()
        {
            string MaNK = "";
            db.nkNhapKho_getNewMaNK(ref MaNK);
            return db.DinhDang(22,int.Parse(MaNK));
        }

        void Save()
        {
            if (lookKhoHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn kho hàng");
                return;
            }

            grvTaiSan.UpdateCurrentRow();
            objNK.MaSoNK = txtMaSoNK.Text;
            objNK.NgayNK = dateNgayNK.DateTime;
            objNK.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objNK.DienGiai = txtDienGiai.Text;
            objNK.MaTN = objnhanvien.MaTN;
            objNK.MaMH = (int)lookMuaHang.EditValue;

            if (lookKhoHang.EditValue != null) objNK.NhapVaoKho = (int)lookKhoHang.EditValue;
            if (lookMuaHang.EditValue != null)
            {
                var objmh = db.msMuaHangs.Single(p => p.MaMH == (int)lookMuaHang.EditValue);
                if (objmh != null) objmh.DaNhapKho = true;
            }

        Save:
            try
            {
                db.SubmitChanges();

                if (!EditMode)
                {
                    List<Kho> lstKho = new List<Kho>();
                    DateTime now = db.GetSystemDate();
                    for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                    {
                        Kho objkhothem = new Kho()
                        {
                            MaTS = (int)grvTaiSan.GetRowCellValue(i, "MaLTS"),
                            SoLuong = (int)grvTaiSan.GetRowCellValue(i, "SoLuong"),
                            NgayNhap = now,
                            DonGia = (decimal)grvTaiSan.GetRowCellValue(i, "DonGia"),
                            MaNK = objNK.MaNK,
                            MaNV = objnhanvien.MaNV,
                            MaKhoHang = (int)lookKhoHang.EditValue,
                            HanSuDung = (decimal?)grvTaiSan.GetRowCellValue(i, "HanSuDung"),
                            MaNCC = (int?)grvTaiSan.GetRowCellValue(i,"MaNCC")
                        };
                        lstKho.Add(objkhothem);
                        db.Khos.InsertAllOnSubmit(lstKho);
                    }
                    db.SubmitChanges();
                }
            }
            catch
            {
                objNK.MaSoNK = getNewMaNK();
                goto Save;
            }
            DialogBox.Alert("Đã nhập vào kho");
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            slookTaiSan.DataSource = db.Khos.Select(p => new
            {
                p.DonGia,
                p.HanSuDung,
                p.ID,
                p.KhoHang.TenKho,
                p.tsLoaiTaiSan.TenLTS,
                NhaCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                p.NgayNhap,
                TenTT = p.MaTT != null ? p.tsTrangThai.TenTT : "",
                p.SoLuong
            });
            slookNCC.DataSource = db.tnNhaCungCaps;
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
            }
            
            
            if (this.objNK != null)
            {
                EditMode = true;
                objNK = db.nkNhapKhos.Single(p => p.MaNK == objNK.MaNK);
                txtMaSoNK.Text = objNK.MaSoNK;
                dateNgayNK.EditValue = objNK.NgayNK;
                lookNhanVien.EditValue = objNK.MaNV;
                txtDienGiai.Text = objNK.DienGiai;
                lookKhoHang.EditValue = objNK.NhapVaoKho;

                lookMuaHang.Properties.DataSource = db.msMuaHangs
                    .Select(p => new { 
                        p.MaMH,
                        p.DienGiai,
                        p.MaSoMH,
                        p.NgayMH,
                        NhapKho=p.DaNhapKho.GetValueOrDefault()
                    });
            }
            else
            {
                EditMode = false;
                objNK = new nkNhapKho();
                db.nkNhapKhos.InsertOnSubmit(objNK);
                txtMaSoNK.Text = getNewMaNK();
                dateNgayNK.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookMuaHang.Properties.DataSource = db.msMuaHangs.Where(p=> !(p.DaNhapKho ?? false))
                     .Select(p => new
                     {
                         p.MaMH,
                         p.DienGiai,
                         p.MaSoMH,
                         p.NgayMH,
                         NhapKho = p.DaNhapKho.GetValueOrDefault()
                     });
            }

            lookKhoHang.Properties.DataSource = db.KhoHangs;
            gcTaiSan.DataSource = objNK.nkTaiSans;
          // colMaLTS.ColumnEdit = new ripLoaiTaiSan(objnhanvien);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grvTaiSan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvTaiSan.SetFocusedRowCellValue("MaNK", objNK.MaNK);
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                nkTaiSan objTS = (nkTaiSan)grvTaiSan.GetRow(e.RowHandle);
                objTS.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            Save();
            if (DialogBox.Question("Bạn có muốn in phiếu nhập kho này không?") == DialogResult.Yes)
            {
                using (var frm = new Phieu.frmPrintControl(Phieu.EnumPhieu.PhieuNhapKho, objNK.MaNK))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void lookMuaHang_EditValueChanged(object sender, EventArgs e)
        {
            var objmh = db.msMuaHangs.Single(p => p.MaMH == (int)lookMuaHang.EditValue);
            grvTaiSan.SelectAll();
            grvTaiSan.DeleteSelectedRows();
            foreach (var item in objmh.msTaiSans)
            {
                grvTaiSan.AddNewRow();
                grvTaiSan.SetFocusedRowCellValue(colMaLTS, item.MaLTS);
                grvTaiSan.SetFocusedRowCellValue(colSoLuong, item.SoLuong);
                grvTaiSan.SetFocusedRowCellValue(colDonGia, item.DonGia);
                grvTaiSan.SetFocusedRowCellValue(colDienGiai, item.DienGiai);
                grvTaiSan.SetFocusedRowCellValue("MaNCC", item.MaNCC);
            }
        }
        
    }
}