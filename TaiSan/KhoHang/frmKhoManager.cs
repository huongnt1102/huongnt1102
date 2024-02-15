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

namespace TaiSan.KhoHang
{
    public partial class frmKhoManage : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        public frmKhoManage()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmKhoManage_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                gcKho.DataSource = db.Khos
                    .Where(p => p.SoLuong > 0)
                    .Select(p => new
                    {
                        p.ID,
                        p.tsLoaiTaiSan.TenLTS,
                        p.tsTrangThai.TenTT,
                        p.tsTrangThai.MauNen,
                        p.NgayNhap,
                        p.SoLuong,
                        p.DonGia,
                        p.HanSuDung,
                        p.KhoHang.TenKho,
                        TenNCC = p.MaNCC != null ? p.tnNhaCungCap.TenNCC : "",
                        NhomTS = p.tsLoaiTaiSan.TypeID == null ? "" : p.tsLoaiTaiSan.tsLoaiTaiSan_Type.TypeNam
                    }).OrderByDescending(p=>p.NgayNhap);

            }
            catch { gcKho.DataSource = null; }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (NhapKho.frmEdit frmnhapkho = new NhapKho.frmEdit() { objnhanvien = objnhanvien })
            {
                frmnhapkho.ShowDialog();
                if (frmnhapkho.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (XuatKho.frmEdit frmxuatkho = new XuatKho.frmEdit() { objnhanvien = objnhanvien })
            {
                frmxuatkho.ShowDialog();
                if (frmxuatkho.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnThangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKho.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn tài sản");
                return;
            }
            using (frmEditTrangThai frm = new frmEditTrangThai())
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    frm.objkho = db.Khos.Single(p => p.ID == (int)grvKho.GetFocusedRowCellValue("ID"));
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void grvKho_DoubleClick(object sender, EventArgs e)
        {
            if (btnThangThai.Enabled == false) return;
            btnThangThai_ItemClick(null, null);
        }

        private void grvKho_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb((int)grvKho.GetRowCellValue(e.RowHandle, "MauNen"));
            }
            catch { }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnChuyenDenTaiSanSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKho.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn loại tài sản");
                return;
            }
            int ID = (int)grvKho.GetFocusedRowCellValue("ID");
            var objkho = db.Khos.Single(p=>p.ID == ID);
            if (objkho.SoLuong <=0)
            {
                DialogBox.Error("Loại tài sản này không còn trong kho (Số lượng = 0)");
                return;
            }
            var f = new TaiSan.GhiTang.frmEdit();
            f.MaLTS = objkho.MaTS;
            f.ShowDialog();
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKho.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn tài sản");
                return;
            }
            int MaTS = (int)grvKho.GetFocusedRowCellValue("ID");
            var objkho = db.Khos.Single(p => p.ID == MaTS);
            db.Khos.DeleteOnSubmit(objkho);
            db.SubmitChanges();
            LoadData();
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.FileName.Trim().Length != 0)
                {
                    grvKho.OptionsPrint.AutoWidth = false;
                    grvKho.BestFitColumns();

                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                    options.SheetName = "Tai_San_Kho";

                    grvKho.ExportToXls(f.FileName, options);
                    DialogBox.Alert("Export thành công");
                    if (DialogBox.Question("Bạn có muốn mở file này không?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(f.FileName);
                    }
                }
            }
        }

        private void itemChuyenNoiBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKho.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mục cần chuyển. Xin cảm ơn!");
                grvKho.Focus();
                return;
            }
            frmChuyenKhoNB frm = new frmChuyenKhoNB();
            frm.ID = (int?)grvKho.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            LoadData();

        }

    }
}