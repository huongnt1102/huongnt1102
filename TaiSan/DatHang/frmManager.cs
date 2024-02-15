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
using DevExpress.XtraReports.UI;

namespace TaiSan.DatHang
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
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcDonHang.DataSource = db.ddhDatHangs
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDH.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDH.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDH).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaDH,
                                p.NgayDH,
                                p.NgayGH,
                                p.MaSoDH,
                                TenTT = p.MaTT == null ? "" : p.ddhTrangThai.TenTT,
                                MauNen = p.MaTT == null ? 0 : p.ddhTrangThai.MauNen,
                                TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                                p.DienGiai,
                                HoTenNV = p.MaNV == null ? "" : p.tnNhanVien.HoTenNV,
                                MaSoDX = p.MaDX == null ? "" : p.dxmsDeXuat.MaSoDX
                            }).ToList();
                    }
                    else
                    {
                        gcDonHang.DataSource = db.ddhDatHangs
                            .Where(p => p.MaTN == objnhanvien.MaTN &
                                    SqlMethods.DateDiffDay(tuNgay, p.NgayDH.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDH.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDH).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaDH,
                                p.NgayDH,
                                p.NgayGH,
                                p.MaSoDH,
                                TenTT = p.MaTT == null ? "" : p.ddhTrangThai.TenTT,
                                MauNen = p.MaTT == null ? 0 : p.ddhTrangThai.MauNen,
                                TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                                p.DienGiai,
                                HoTenNV = p.MaNV == null ? "" : p.tnNhanVien.HoTenNV,
                                MaSoDX = p.MaDX == null ? "" : p.dxmsDeXuat.MaSoDX
                            }).ToList();
                    }
                }
                else
                {
                    gcDonHang.DataSource = null;
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

        void LoadDetail()
        {
            if (grvDonHang.FocusedRowHandle >= 0)
            {
                var MaDH = (int)grvDonHang.GetFocusedRowCellValue("MaDH");

                gcTaiSan.DataSource = db.ddhTaiSans.Where(p => p.MaDH == MaDH).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.tsLoaiTaiSan.TenLTS,
                        p.SoLuong,
                        p.DonGia,
                        p.DienGiai,
                        ThanhTien = p.DonGia * p.SoLuong,
                        TenNCC = p.MaNCC != null ? p.tnNhaCungCap.TenNCC : ""
                    }).ToList();
            }
            else
            {
                gcTaiSan.DataSource = null;
            }
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

            var MaDHs = "|";
            int[] indexs = grvDonHang.GetSelectedRows();

            foreach (int i in indexs)
                MaDHs += grvDonHang.GetRowCellValue(i, "MaDH") + "|";

            db.ddhTaiSans.DeleteAllOnSubmit(db.ddhTaiSans.Where(p => SqlMethods.Like(MaDHs, "%" + p.MaDH + "%")));
            db.ddhDatHangs.DeleteAllOnSubmit(db.ddhDatHangs.Where(p => SqlMethods.Like(MaDHs, "%" + p.MaDH + "%")));

            try
            {
                db.SubmitChanges();
                grvDonHang.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được đơn đặt hàng này");
                this.Close();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDonHang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn đơn hàng");
                return;
            }
            int madh = (int)grvDonHang.GetFocusedRowCellValue("MaDH");
            var ttddh = db.ddhDatHangs.Single(p => String.Compare(p.MaDH.ToString(), madh.ToString(), false) == 0).MaTT;
            if (ttddh == 3 | ttddh == 2)
            {
                DialogBox.Error("Đơn hàng này đã duyệt hoặc đã nhận hàng xong");
                return;
            }
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objDH = db.ddhDatHangs.Single(p => p.MaDH == madh) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void grvDatHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void grvDonHang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvDonHang.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvDonHang_DoubleClick(object sender, EventArgs e)
        {
            if (itemSua.Enabled == false) return;
            itemSua_ItemClick(null, null);
        }

        private void btnInDanhSachDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var rpt = new Report.rptDanhSachDonDatHang(tuNgay, denNgay);
                rpt.ShowPreviewDialog();
            }
        }

        private void btnTruongPhongKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnGiamDocKyDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDonHang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn đơn hàng");
                return;
            }
            var MaDH = (int)grvDonHang.GetFocusedRowCellValue("MaDH");
            ddhDatHang del = db.ddhDatHangs.Single(p => p.MaDH == MaDH);
            del.ddhTrangThai = db.ddhTrangThais.Single(p => p.MaTT == 2);
            try
            {
                db.SubmitChanges();
                LoadData();
                DialogBox.Alert("Đã duyệt đơn hàng");
            }
            catch
            {
                DialogBox.Alert("Không duyệt được");
            }
        }
    }
}