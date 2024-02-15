using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraPrinting;

namespace KyThuat.BaoTri
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                gcBaoTri.DataSource = null;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcBaoTri.DataSource = db.btBaoTris
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayBT.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayBT.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayBT).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaBT,
                            p.NgayBT,
                            p.MaSoBT,
                            p.tsTaiSanSuDung.KyHieu,
                            p.tsTaiSanSuDung.tsLoaiTaiSan.TenLTS,
                            p.tsTrangThai.TenTT,
                            p.tsTrangThai.MauNen,
                            p.btHinhThuc.TenHT,
                            TenNCC = p.MaDT != null ? p.tnNhaCungCap.TenNCC : "",
                            p.PhiBT,
                            p.DaTT,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.tsTaiSanSuDung.MaLTS,
                            p.MaTS
                        }).ToList();
                }
                else
                {
                    gcBaoTri.DataSource = db.btBaoTris
                        .Where(p => p.MaTN == objnhanvien.MaTN &
                                SqlMethods.DateDiffDay(tuNgay, p.NgayBT.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayBT.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayBT).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaBT,
                            p.NgayBT,
                            p.MaSoBT,
                            p.tsTaiSanSuDung.KyHieu,
                            p.tsTaiSanSuDung.tsLoaiTaiSan.TenLTS,
                            p.tsTrangThai.TenTT,
                            p.tsTrangThai.MauNen,
                            p.btHinhThuc.TenHT,
                            TenNCC = p.MaDT != null ? p.tnNhaCungCap.TenNCC : "",
                            p.PhiBT,
                            p.DaTT,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.tsTaiSanSuDung.MaLTS,
                            p.MaTS
                        }).ToList();
                }
            }
            else
            {
                gcBaoTri.DataSource = null;
            }
        }

        void LoadDataByPage()
        {
            if (grvBaoTri.FocusedRowHandle < 0)
            {
                gcThietBi.DataSource = null;
                gcNhanSu.DataSource = null;
                gcTSCT.DataSource = null;
                return;
            }

            var MaBT = (int)grvBaoTri.GetFocusedRowCellValue("MaBT");

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcThietBi.DataSource = db.btThietBis.Where(p => p.MaBT == MaBT)
                        .Select(p => new { p.tsLoaiTaiSan.TenLTS, p.SoLuong, p.DienGiai });
                    break;
                case 1:
                    gcNhanSu.DataSource = db.btNhanViens.Where(p => p.MaBT == MaBT)
                        .Select(p => new { p.tnNhanVien.MaSoNV, p.tnNhanVien.HoTenNV, p.DienGiai });
                    break;
                case 2:
                    int mats = (int)grvBaoTri.GetFocusedRowCellValue("MaLTS");
                    gcTSCT.DataSource = db.ChiTietTaiSans.Where(p => p.MaTS == mats)
                        .Select(p => new
                        {
                            p.TenChiTiet,
                            p.KyHieu,
                            p.tnNhaCungCap.TenNCC,
                            p.tsHangSanXuat.TenHSX,
                            p.tsXuatXu.TenXX,
                            p.HSD,
                            p.NgayBDSD,
                            p.NgaySX
                        });
                    break;
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
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var MaBTs = "|";
            int[] indexs = grvBaoTri.GetSelectedRows();

            foreach (int i in indexs)
                MaBTs += grvBaoTri.GetRowCellValue(i, "MaBT") + "|";

            db.btThietBis.DeleteAllOnSubmit(db.btThietBis.Where(p => SqlMethods.Like(MaBTs, "%"+p.MaBT+"%" )));
            db.btNhanViens.DeleteAllOnSubmit(db.btNhanViens.Where(p => SqlMethods.Like(MaBTs, "%"+p.MaBT+"%" )));

            db.btBaoTris.DeleteAllOnSubmit(db.btBaoTris.Where(p => SqlMethods.Like(MaBTs, "%" + p.MaBT + "%")));

            db.SubmitChanges();

            grvBaoTri.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBaoTri.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }

            bool datt = (bool)grvBaoTri.GetFocusedRowCellValue(colDaTT);

            if (datt)
            {
                DialogBox.Error("Không thể sửa mục đã thanh toán");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                //frm.Ma = grvBaoTri.GetFocusedRowCellValue("MaBT").ToString();
                var MaBT = (int)grvBaoTri.GetFocusedRowCellValue("MaBT");
                frm.objBT = db.btBaoTris.Single(p => p.MaBT == MaBT);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void grvBaoTri_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void grvBaoTri_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb((int)grvBaoTri.GetRowCellValue(e.RowHandle, "MauNen"));
            }
            catch { }
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBaoTri.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn bảo trì cần in");
                return;
            }

            report.frmPrintControl frm = new report.frmPrintControl((int)grvBaoTri.GetFocusedRowCellValue("MaBT"), 1);
            frm.ShowDialog();
        }

        private void btnBaoCaoBaoTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBaoTriThietBiChiTiet frm = new frmBaoTriThietBiChiTiet();
            frm.objtssd = db.tsTaiSanSuDungs.Single(p => p.MaTS == (int)grvBaoTri.GetFocusedRowCellValue("MaTS"));
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                LoadDataByPage();
            }
        }

        private void btnInMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBaoTri.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn bảo trì cần in");
                return;
            }

            report.frmPrintControl frm = new report.frmPrintControl((int)grvBaoTri.GetFocusedRowCellValue("MaBT"), 2);
            frm.ShowDialog();
        }

        private void btnXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBaoTri.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn kế hoạch bảo trì để xác nhận. Xin cảm ơn!");
                return;
            }
            var obj = db.khbtKeHoaches.SingleOrDefault(p => p.MaKH == (int?)grvBaoTri.GetFocusedRowCellValue("MaKH"));
            obj.MaTT = 2;
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Kế hoạch bảo trì này đã được duyệt. Xim cảm ơn!");
            }
            catch
            {
                DialogBox.Error("Không thể duyệt kế hoạch này. Vui lòng kiểm tra lại!");
            }
        }


    }
}