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
using System.Data.Linq.SqlClient;

namespace TaiSan
{
    public partial class frmSelectChungTu : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public int? MaLCT { get; set; }
        public bool IsSave { get; set; }
        public int? MaCT { get; set; }
        public string SoCT { get; set; }
        MasterDataContext db = new MasterDataContext();
        public frmSelectChungTu()
        {
            InitializeComponent();
           // TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            if (MaLCT == null)
                return;
            var wait = DialogBox.WaitingForm();
            try
            {
                switch (MaLCT)
                {
                    case 1:
                        gcChungTu.DataSource = db.PhieuThus.Where(q => SqlMethods.DateDiffDay(tuNgay, q.NgayThu) >= 0 & SqlMethods.DateDiffDay(q.NgayThu, denNgay) >= 0)
                            .Select(p => new { p.MaPhieu, p.SoPhieu, NgayTH = p.NgayThu, NgayTP = p.NgayNhap, p.DienGiai });
                        break;
                    case 2:
                        gcChungTu.DataSource = db.PhieuChis.Where(q => SqlMethods.DateDiffDay(tuNgay, q.NgayChi) >= 0 & SqlMethods.DateDiffDay(q.NgayChi, denNgay) >= 0)
                            .Select(p => new { p.MaPhieu, p.SoPhieu, NgayTH = p.NgayChi, NgayTP = p.NgayTao, p.DienGiai });
                        break;
                    case 3:
                        gcChungTu.DataSource = db.dxmsDeXuats.Where(q => SqlMethods.DateDiffDay(tuNgay, q.NgayDX) >= 0 & SqlMethods.DateDiffDay(q.NgayDX, denNgay) >= 0)
                            .Select(p => new { MaPhieu = p.MaDX, SoPhieu = p.MaSoDX, NgayTH = p.NgayDX, p.DienGiai });
                        break;
                    case 4:
                        gcChungTu.DataSource = db.msMuaHangs.Where(q => SqlMethods.DateDiffDay(tuNgay, q.NgayMH) >= 0 & SqlMethods.DateDiffDay(q.NgayMH, denNgay) >= 0)
                            .Select(p => new { MaPhieu = p.MaMH, SoPhieu = p.MaSoMH, NgayTH = p.NgayMH, p.DienGiai });
                        break;
                    case 5:
                        gcChungTu.DataSource = db.nkNhapKhos.Where(q => SqlMethods.DateDiffDay(tuNgay, q.NgayNK) >= 0 & SqlMethods.DateDiffDay(q.NgayNK, denNgay) >= 0)
                            .Select(p => new { MaPhieu = p.MaNK, SoPhieu = p.MaSoNK, NgayTH = p.NgayNK, p.DienGiai });
                        break;
                    case 6:
                        gcChungTu.DataSource = db.xkXuatKhos.Where(q => SqlMethods.DateDiffDay(tuNgay, q.NgayXK) >= 0 & SqlMethods.DateDiffDay(q.NgayXK, denNgay) >= 0)
                            .Select(p => new { MaPhieu = p.MaXK, SoPhieu = p.MaSoXK, NgayTH = p.NgayXK, p.DienGiai });
                        break;
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
          //  Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
             LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             LoadData();
        }

        private void itemXemCT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (gvChungTu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn chứng từ để xem chi tiết. Xin cảm ơn!");
                return;
            }
            switch (MaLCT)
            {
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    DeXuat.frmEdit frm3 = new DeXuat.frmEdit();
                    frm3.objnhanvien = objnhanvien;
                    frm3.objDX = db.dxmsDeXuats.SingleOrDefault(p => p.MaDX == (int)gvChungTu.GetFocusedRowCellValue("MaPhieu"));
                    frm3.ShowDialog();
                    break;
                case 4:
                    MuaHang.frmEdit frm4 = new MuaHang.frmEdit();
                    frm4.objnhanvien = objnhanvien;
                    frm4.objMH = db.msMuaHangs.SingleOrDefault(p => p.MaMH == (int)gvChungTu.GetFocusedRowCellValue("MaPhieu"));
                    frm4.ShowDialog();
                    break;
                case 5:
                    NhapKho.frmEdit frm5 = new NhapKho.frmEdit();
                    frm5.objnhanvien = objnhanvien;
                    frm5.objNK = db.nkNhapKhos.SingleOrDefault(p=>p.MaNK==(int)gvChungTu.GetFocusedRowCellValue("MaPhieu"));
                    frm5.ShowDialog();
                    break;
                case 6:
                    XuatKho.frmEdit frm6 = new XuatKho.frmEdit();
                    frm6.objnhanvien = objnhanvien;
                    frm6.objXK = db.xkXuatKhos.SingleOrDefault(p=>p.MaXK==(int)gvChungTu.GetFocusedRowCellValue("MaPhieu"));
                    frm6.ShowDialog();
                    break;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvChungTu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn phiếu chứng từ để lấy dữ liệu. Xin cảm ơn!");
                return;
            }
            MaCT = (int?)gvChungTu.GetFocusedRowCellValue("MaPhieu");
            SoCT = gvChungTu.GetFocusedRowCellValue("SoPhieu").ToString();
            this.Close();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsSave = false;
            MaCT = null;
            SoCT = null;
            this.Close();
        }

    }
}