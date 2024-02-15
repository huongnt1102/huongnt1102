using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace TaiSan.MuaHang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public msMuaHang objMH;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        string getNewMaMH()
        {
            string MaMH = "";
            db.msMuaHang_getNewMaMH(ref MaMH);
            return db.DinhDang(19,int.Parse(MaMH));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            slookLoaiTS.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS, p.KyHieu, NhomTS = p.tsLoaiTaiSan_Type.TypeNam, p.tsLoaiTaiSan_DVT.TenDVT, p.DacTinh }).OrderBy(p=>p.NhomTS); 
            slookNhaCungCap.DataSource = db.tnNhaCungCaps;
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
            }
          //  colMaLTS.ColumnEdit = new ripLoaiTaiSan(objnhanvien);

            if (this.objMH != null)
            {
                lookDatHang.Properties.DataSource = db.ddhDatHangs
                  .Select(p => new { p.MaDH, p.MaSoDH, p.DienGiai, p.tnNhanVien.HoTenNV, p.ddhTrangThai.TenTT, p.NgayDH });
                objMH = db.msMuaHangs.Single(p => p.MaMH == objMH.MaMH);
                txtMaSoMH.Text = objMH.MaSoMH;
                dateNgayMH.EditValue = objMH.NgayMH;
                lookNhanVien.EditValue = objMH.MaNV;
                txtDienGiai.Text = objMH.DienGiai;
                ckbDaTT.Checked = (bool)objMH.DaTT;
                lookDatHang.EditValue = objMH.MaDH;
                lookDatHang.Properties.ReadOnly = true;
                grvTaiSan.OptionsBehavior.Editable = false;
            }
            else
            {
                lookDatHang.Properties.DataSource = db.ddhDatHangs
                   .Where(p => p.MaTT == 2) //Da duyet
                   .Select(p => new { p.MaDH, p.MaSoDH, p.DienGiai, p.tnNhanVien.HoTenNV, p.ddhTrangThai.TenTT, p.NgayDH });
                objMH = new msMuaHang();
                db.msMuaHangs.InsertOnSubmit(objMH);

                txtMaSoMH.Text = getNewMaMH();
                dateNgayMH.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookDatHang.EditValue = objMH.MaDH;
                lookDatHang.Properties.ReadOnly = false;
                grvTaiSan.OptionsBehavior.Editable = true;
            }

            gcTaiSan.DataSource = objMH.msTaiSans;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            objMH.MaSoMH = txtMaSoMH.Text;
            objMH.NgayMH = dateNgayMH.DateTime;
            objMH.DienGiai = txtDienGiai.Text;
            objMH.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objMH.DaTT = ckbDaTT.Checked;
            objMH.MaTN = objnhanvien.MaTN;
            objMH.MaDH = (int)lookDatHang.EditValue;
            objMH.DaNhapKho = false;
            decimal? TongTien = 0;
            if (grvTaiSan.RowCount > 0)
            {
                for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                    TongTien += (grvTaiSan.GetRowCellValue(i, "ThanhTien") == null ? 0 : (decimal?)grvTaiSan.GetRowCellValue(i, "ThanhTien"));
            }
            objMH.TongTien = TongTien;

            int retry = 0;
            Save:
            try
            {
                if (lookDatHang.EditValue !=null)
                {
                    ddhDatHang objdt = db.ddhDatHangs.Single(p => String.Compare(p.MaDH.ToString(), lookDatHang.EditValue.ToString(), false) == 0);
                    if (objdt != null)
                    {
                        objdt.MaTT = 3; //Da nhan hang
                    }
                }
                db.SubmitChanges();
            }
            catch
            {
                retry++;
                if (retry <= 5)
                {
                    objMH.MaSoMH = getNewMaMH();
                    for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                        grvTaiSan.SetRowCellValue(i, "MaMH", objMH.MaMH);
                    goto Save;
                }
                DialogBox.Error("Không lưu dữ liệu được, có thể đường truyền có vấn đề. Vui lòng thử lại sau");
            }

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
            
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                msTaiSan objTS = (msTaiSan)grvTaiSan.GetRow(e.RowHandle);
                objTS.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "SoLuong":
                case "DonGia":

                    if (grvTaiSan.GetFocusedRowCellValue("SoLuong") != null &
                        grvTaiSan.GetFocusedRowCellValue("DonGia") != null)
                    {
                        decimal ThanhTien = (decimal)grvTaiSan.GetFocusedRowCellValue("DonGia") * (int)grvTaiSan.GetFocusedRowCellValue("SoLuong");
                        grvTaiSan.SetFocusedRowCellValue("ThanhTien", ThanhTien);
                    }
                    break;
                default:
                    break;
            }
        }

        private void lookDatHang_EditValueChanged(object sender, EventArgs e)
        {
            if (lookDatHang.EditValue == null) return;
            grvTaiSan.SelectAll();
            grvTaiSan.DeleteSelectedRows();
            var objddh = db.ddhDatHangs.SingleOrDefault(p => p.MaDH.ToString() == lookDatHang.EditValue.ToString());
            var ts = db.ddhTaiSans.Where(p => p.MaDH.ToString() == lookDatHang.EditValue.ToString());
            foreach (ddhTaiSan t in ts)
            {
                grvTaiSan.AddNewRow();
                grvTaiSan.SetFocusedRowCellValue("MaLTS", t.MaLTS);
                grvTaiSan.SetFocusedRowCellValue("SoLuong", t.SoLuong);
                grvTaiSan.SetFocusedRowCellValue("DonGia", t.DonGia);
                grvTaiSan.SetFocusedRowCellValue("DienGiai", t.DienGiai);
                grvTaiSan.SetFocusedRowCellValue("MaNCC", t.MaNCC);
            }
            grvTaiSan.UpdateCurrentRow();
        }

    }
}