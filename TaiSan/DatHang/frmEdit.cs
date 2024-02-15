using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace TaiSan.DatHang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public ddhDatHang objDH;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        string getNewMaDH()
        {
            string MaDH = "";
            db.ddhDatHang_getNewMaDH(ref MaDH);
            return db.DinhDang(18, int.Parse(MaDH));
        }

        void Save()
        {

            if (lookDeXuat.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn đề xuất");
                return;
            }


            for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
            {
                if (grvTaiSan.GetRowCellValue(i, "MaLTS") == null
                    | grvTaiSan.GetRowCellValue(i, "SoLuong") == null
                    | grvTaiSan.GetRowCellValue(i, "DonGia") == null)
                {
                    DialogBox.Alert("Các trường dữ liệu [Loại tài sản],[Số lượng] và [Đơn giá] không được để trống");
                    return;
                }
            }

            objDH.MaSoDH = txtMaSoDH.Text;
            objDH.NgayDH = dateNgayDH.DateTime;
            objDH.NgayGH = dateNgayGH.DateTime;
            objDH.DienGiai = txtDienGiai.Text;
            objDH.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objDH.ddhTrangThai = db.ddhTrangThais.Single(p => p.MaTT == (int)lookTrangThai.EditValue);
            objDH.MaDX = (int)lookDeXuat.EditValue;
            objDH.MaTN = objnhanvien.MaTN;

        Save:
            try
            {
                if (lookDeXuat.EditValue != null)
                {
                    dxmsDeXuat objdx = db.dxmsDeXuats.Single(p => String.Compare(p.MaDX.ToString(), lookDeXuat.EditValue.ToString(), false) == 0);
                    if (objdx != null & (int)lookTrangThai.EditValue == 2) //Da duyet don hang
                    {
                        objdx.MaTT = 3; //Dang dat hang
                    }
                }

                db.SubmitChanges();
            }
            catch
            {
                objDH.MaSoDH = getNewMaDH();
                for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                    grvTaiSan.SetRowCellValue(i, "MaDH", objDH.MaDH);
                goto Save;
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            slookLoaiTS.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS, p.KyHieu, NhomTS = p.tsLoaiTaiSan_Type.TypeNam, p.tsLoaiTaiSan_DVT.TenDVT, p.DacTinh }).OrderBy(p => p.NhomTS); 
            //var countdx = db.ddhDatHangs.Select(p => p.MaDX).Distinct().Count();
            slookNhaCungCap.DataSource = db.tnNhaCungCaps;
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
                lookTrangThai.Properties.DataSource = db.ddhTrangThais;
                lookDeXuat.Properties.DataSource = db.dxmsDeXuats
                    .Where(p=>p.MaTT==2)
                    .Select(p => new { p.MaDX, p.MaSoDX, p.NgayDX, p.tnNhanVien.HoTenNV, HoTenNV1 = p.tnNhanVien1.HoTenNV });
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
                lookTrangThai.Properties.DataSource = db.ddhTrangThais.Where(p => p.MaTN == objnhanvien.MaTN);
                lookDeXuat.Properties.DataSource = db.dxmsDeXuats.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Where(p => p.MaTT == 2 & p.MaNvNhan == objnhanvien.MaNV)
                    .Select(p => new { p.MaDX, p.MaSoDX, p.NgayDX, p.tnNhanVien.HoTenNV, HoTenNV1 = p.tnNhanVien1.HoTenNV });
            }

           // colMaLTS.ColumnEdit = new ripLoaiTaiSan(objnhanvien);

            if (this.objDH != null)
            {
                objDH = db.ddhDatHangs.Single(p => p.MaDH == objDH.MaDH);
                txtMaSoDH.Text = objDH.MaSoDH;
                dateNgayDH.EditValue = objDH.NgayDH;
                dateNgayGH.EditValue = objDH.NgayGH;
                lookTrangThai.EditValue = objDH.MaTT;
                lookNhanVien.EditValue = objDH.MaNV;
                txtDienGiai.Text = objDH.DienGiai;
                lookDeXuat.EditValue = objDH.MaDX;
                lookTrangThai.Enabled = false;
                
                lookDeXuat.Properties.ReadOnly = true;
            }
            else
            {
                objDH = new ddhDatHang();
                db.ddhDatHangs.InsertOnSubmit(objDH);
                objDH.MaSoDH = txtMaSoDH.Text = getNewMaDH();
                dateNgayDH.DateTime = DateTime.Now;
                dateNgayGH.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookTrangThai.ItemIndex = 0;
                lookTrangThai.Enabled = false;
                lookTrangThai.EditValue = 1;

                lookDeXuat.Properties.ReadOnly = false;
            }

            gcTaiSan.DataSource = objDH.ddhTaiSans;
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
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                ddhTaiSan objTS = (ddhTaiSan)grvTaiSan.GetRow(e.RowHandle);
                objTS.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        //private void lookDeXuat_EditValueChanged(object sender, EventArgs e)
        //{
        //    grvTaiSan.SelectAll();
        //    grvTaiSan.DeleteSelectedRows();
        //    var ts = db.dxmsTaiSans.Where(p => p.MaDX.ToString() == lookDeXuat.EditValue.ToString());
        //    foreach (dxmsTaiSan t in ts)
        //    {
        //        grvTaiSan.AddNewRow();
        //        grvTaiSan.SetFocusedRowCellValue("MaLTS", t.MaLTS);
        //        grvTaiSan.SetFocusedRowCellValue("SoLuong", t.SoLuong);
        //        grvTaiSan.SetFocusedRowCellValue("DienGiai", t.DienGiai);
        //    }
        //    grvTaiSan.UpdateCurrentRow();
        //}

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void lookDeXuat_EditValueChanged(object sender, EventArgs e)
        {
            grvTaiSan.SelectAll();
            grvTaiSan.DeleteSelectedRows();
            var ts = db.dxmsTaiSans.Where(p => p.MaDX.ToString() == lookDeXuat.EditValue.ToString());
            foreach (dxmsTaiSan t in ts)
            {
                grvTaiSan.AddNewRow();
                grvTaiSan.SetFocusedRowCellValue("MaLTS", t.MaLTS);
                grvTaiSan.SetFocusedRowCellValue("SoLuong", t.SoLuong);
                grvTaiSan.SetFocusedRowCellValue("DienGiai", t.DienGiai);
            }
            grvTaiSan.UpdateCurrentRow();
        }
    }
}