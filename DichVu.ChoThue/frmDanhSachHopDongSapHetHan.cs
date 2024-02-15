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
using System.Data.Linq.SqlClient;


namespace DichVu.ChoThue
{
    public partial class frmDanhSachHopDongSapHetHan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime sysdt;
        public frmDanhSachHopDongSapHetHan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmDanhSachHopDongSapHetHan_Load(object sender, EventArgs e)
        {
            sysdt = db.GetSystemDate();
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
        }
        void LoadData()
        {
            db = new MasterDataContext();
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var sysdt = db.GetSystemDate();
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcHopDong.DataSource = db.thueHopDongs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0 &
                                SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) <= Library.Properties.Settings.Default.TimeRemain) 
                        .OrderByDescending(p => p.NgayHD)
                        .Select(p => new
                        {
                            p.MaHD,
                            p.NgayHD,
                            p.NgayBG,
                            p.SoHD,
                            p.ThoiHan,
                            p.thueTrangThai.TenTT,
                            p.thueTrangThai.MauNen,
                            p.mbMatBang.MaSoMB,
                            p.DienTich,
                            p.DonGia,
                            p.ThanhTien,
                            p.PhiQL,
                            p.TienCoc,
                            TenTG = p.tnTyGia.TenVT,
                            TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            DienThoaiKH = p.tnKhachHang.DienThoaiKH,
                            DiaChiKH = p.tnKhachHang.DCLL,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.ChuKyThanhToan,
                            p.tnTyGia.MaTG,
                            p.MaMB,
                            p.MaKH,
                            TimeRemain = SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0))
                        });
                }
                else
                {
                    gcHopDong.DataSource = db.thueHopDongs
                        .Where(p => p.MaTN == objnhanvien.MaTN &
                                SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0 &
                                SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) <= Library.Properties.Settings.Default.TimeRemain) 
                        .OrderByDescending(p => p.NgayHD)
                        .Select(p => new
                        {
                            p.MaHD,
                            p.NgayHD,
                            p.NgayBG,
                            p.SoHD,
                            p.ThoiHan,
                            p.thueTrangThai.TenTT,
                            p.thueTrangThai.MauNen,
                            p.mbMatBang.MaSoMB,
                            p.DienTich,
                            p.DonGia,
                            p.ThanhTien,
                            p.PhiQL,
                            p.TienCoc,
                            TenTG = p.tnTyGia.TenVT,
                            TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            DienThoaiKH = p.tnKhachHang.DienThoaiKH,
                            DiaChiKH = p.tnKhachHang.DCLL,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.ChuKyThanhToan,
                            p.MaMB,
                            p.MaKH,
                            TimeRemain = SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0))
                        });
                }
            }
            else
            {
                gcHopDong.DataSource = null;
            }
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThahToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            using (var frm = new frmThanhToan())
            {
                frm.objhd = db.thueHopDongs.Single(hd => hd.MaHD == MaHD);
                frm.ShowDialog();
            }
        }

        private void btn2Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    DataTable dt = new DataTable();

                    var ts = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.ChuKyMax.Value) >= 0
                                    & SqlMethods.DateDiffDay(p.ChuKyMax.Value, denNgay) >= 0 &
                                    SqlMethods.DateDiffDay(sysdt, p.thueHopDong.NgayBG.Value.AddMonths(p.thueHopDong.ThoiHan ?? 0)) <= Library.Properties.Settings.Default.TimeRemain) 
                                .OrderBy(p => p.thueHopDong.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.MaCN,
                                    p.thueHopDong.SoHD,
                                    KhachHang = p.thueHopDong.MaKH.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.Value ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen) : "") : "",
                                    MatBang = p.thueHopDong.mbMatBang.MaSoMB,
                                    p.thueHopDong.ChuKyThanhToan,
                                    ChuKy = string.Format("{0}-{1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                                    p.ConNo,
                                    p.DaThanhToan
                                });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách công nợ hợp đồng thuê", dt);
                }
            }
        }
        int MaHD;
        private void grvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0) return;
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
        }

        private void btn2hdt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    DataTable dt = new DataTable();

                    var ts = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.ChuKyMax.Value) >= 0
                                    & SqlMethods.DateDiffDay(p.ChuKyMax.Value, denNgay) >= 0 &
                                    SqlMethods.DateDiffDay(sysdt, p.thueHopDong.NgayBG.Value.AddMonths(p.thueHopDong.ThoiHan ?? 0)) <= Library.Properties.Settings.Default.TimeRemain) 
                                .OrderBy(p => p.thueHopDong.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.MaCN,
                                    p.thueHopDong.SoHD,
                                    KhachHang = p.thueHopDong.MaKH.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.Value ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen) : "") : "",
                                    MatBang = p.thueHopDong.mbMatBang.MaSoMB,
                                    p.thueHopDong.ChuKyThanhToan,
                                    ChuKy = string.Format("{0}-{1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                                    p.ConNo,
                                    p.DaThanhToan
                                });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách công nợ hợp đồng thuê", dt);
                }
            }
        }
        
        private void btnInCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }
            List<int> ListMHD = new List<int>();
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            ListMHD.Add(MaHD);
            //using (var frm = new frmPrintControl(ListMHD, 2, ""))
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}