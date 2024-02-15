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

namespace TaiSan.DeXuat
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public dxmsDeXuat objDX;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        string getNewMaDX()
        {
            string MaDX = "";
            db.dxmsDeXuat_getNewMaDX(ref MaDX);
            return db.DinhDang(2,int.Parse(MaDX));
        }

        void Save()
        {
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái");
                return;
            }
            if (lookNguoiNhan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người nhận đề xuất");
                return;
            }
            for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
            {
                if (grvTaiSan.GetRowCellValue(i, "MaLTS") == null
                    | grvTaiSan.GetRowCellValue(i, "SoLuong") == null)
                {
                    DialogBox.Alert("Các trường dữ liệu [Loại tài sản] và [Số lượng] không được để trống");
                    return;
                }
            }

            int retry = 0;
        Save:
            try
            {
                if (retry >= 10)
                {
                    DialogBox.Alert("Lưu không thành công, Vui lòng thử lại");
                    return;
                }
                objDX.MaSoDX = txtMaSoDX.Text;
                objDX.NgayDX = dateNgayDX.DateTime;
                objDX.DienGiai = txtDienGiai.Text;
                objDX.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
                objDX.dxmsTrangThai = db.dxmsTrangThais.Single(p => p.MaTT == (int)lookTrangThai.EditValue);
                objDX.MaTN = objnhanvien.MaTN;
                objDX.tnNhanVien1 = db.tnNhanViens.Single(p => p.MaNV == (int)lookNguoiNhan.EditValue);
                db.SubmitChanges();
            }
            catch
            {
                retry++;
                objDX.MaSoDX = getNewMaDX();
                for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                    grvTaiSan.SetRowCellValue(i, "MaDX", objDX.MaDX);
                goto Save;
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            slookLoaiTS.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS, p.KyHieu, NhomTS = p.tsLoaiTaiSan_Type.TypeNam, p.tsLoaiTaiSan_DVT.TenDVT, p.DacTinh }).OrderBy(p => p.NhomTS); 
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNguoiNhan.Properties.DataSource =  lookNhanVien.Properties.DataSource = db.tnNhanViens;
                lookChucVuNV.Properties.DataSource =  lookChucVu.Properties.DataSource = db.tnChucVus;
                lookPhongBan.Properties.DataSource = lookPhongBanNV.Properties.DataSource = db.tnPhongBans;
            }
            else
            {
                lookNguoiNhan.Properties.DataSource = lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
                lookChucVuNV.Properties.DataSource = lookChucVu.Properties.DataSource = db.tnChucVus.Where(p => p.MaTN == objnhanvien.MaTN);
                lookPhongBan.Properties.DataSource = lookPhongBanNV.Properties.DataSource = db.tnPhongBans.Where(p => p.MaTN == objnhanvien.MaTN);
            }
            //lookTrangThai.Properties.DataSource = db.dxmsTrangThais;
            //lookLTS.DataSource = db.tsLoaiTaiSans;
           // colMaLTS.ColumnEdit = new ripLoaiTaiSan(objnhanvien);

            if (objDX != null)
            {
                objDX = db.dxmsDeXuats.Single(p => p.MaDX == objDX.MaDX);
                txtMaSoDX.Text = objDX.MaSoDX;
                dateNgayDX.DateTime = (DateTime)objDX.NgayDX;
                lookTrangThai.EditValue = objDX.MaTT;
                lookNhanVien.EditValue = objDX.MaNV;
                txtDienGiai.Text = objDX.DienGiai;
                lookNguoiNhan.EditValue = objDX.MaNvNhan;
                lookPhongBanNV.EditValue = objDX.tnNhanVien.MaPB;
                lookChucVuNV.EditValue = objDX.tnNhanVien.MaCV;
                if (objDX.MaNvNhan != null)
                {
                    lookChucVu.EditValue = objDX.tnNhanVien1.MaCV;
                    lookPhongBan.EditValue = objDX.tnNhanVien1.MaPB;
                }
                lookTrangThai.Properties.ReadOnly = true;
            }
            else
            {
                objDX = new dxmsDeXuat();
                db.dxmsDeXuats.InsertOnSubmit(objDX);

                txtMaSoDX.Text = getNewMaDX();
                dateNgayDX.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookPhongBanNV.EditValue = objnhanvien.MaPB;
                lookChucVuNV.EditValue = objnhanvien.MaCV;
                lookTrangThai.EditValue = 1;
                lookTrangThai.Properties.ReadOnly = true;
            }

            gcTaiSan.DataSource = objDX.dxmsTaiSans;
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
                dxmsTaiSan objTS = (dxmsTaiSan)grvTaiSan.GetRow(e.RowHandle);
                objTS.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        private void lookNguoiNhan_EditValueChanged(object sender, EventArgs e)
        {
            var nguoinhan = db.tnNhanViens.Single(p => p.MaNV == (int)lookNguoiNhan.EditValue);
            lookChucVu.EditValue = nguoinhan.MaCV;
            lookPhongBan.EditValue = nguoinhan.MaPB;
        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MaLTS")
            {
                var DacTinh = db.tsLoaiTaiSans.SingleOrDefault(p => p.MaLTS == (int)grvTaiSan.GetFocusedRowCellValue("MaLTS")).DacTinh;
                grvTaiSan.SetFocusedRowCellValue("DacTinh", DacTinh);
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            Save();
            if (DialogBox.Question("Bạn có muốn in phiếu đề xuất này không?") == DialogResult.Yes)
            {
                Report.frmPrintControl frm = new Report.frmPrintControl(objDX.MaDX.ToString());
                frm.ShowDialog();
            }

        }
    }
}