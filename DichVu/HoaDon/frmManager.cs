using System;
using System.Collections.Generic;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using System.Net.Mail;
using System.IO;
using DevExpress.XtraReports.UI;


namespace DichVu.HoaDon
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        string MM = "";
        bool first = true;
        int Month = 0, Year = 2000;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void BindData()
        {
            var wait = DialogBox.WaitingForm("Đang tổng hợp và lấy dữ liệu công nợ. Vui lòng chờ....");  

            #region Old code
            //if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            //{
            //    var now = db.GetSystemDate();

            //    var tuNgay = (DateTime)itemTuNgay.EditValue;
            //    var denNgay = (DateTime)itemDenNgay.EditValue;

            //    var PQLDaThu = db.PhiQuanLies.Where(p => p.ThangThanhToan.Value.Month == now.Month & p.ThangThanhToan.Value.Year == now.Year).Select(p => p.MaMB).ToList();

            //    var khTM = db.dvtmThanhToanThangMays
            //            .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(tuNgay, p.ThangThanhToan.Value) >= 0 &
            //                        SqlMethods.DateDiffDay(p.ThangThanhToan.Value, denNgay) >= 0)
            //            .Select(p => p.dvtmTheThangMay.mbMatBang.MaMB).ToList();
            //    var khHD = db.thueCongNos
            //        .Where(p => p.ConNo > 0 & SqlMethods.DateDiffDay(tuNgay, p.ChuKyMin.Value) >= 0 & SqlMethods.DateDiffDay(p.ChuKyMin.Value, denNgay) >= 0)
            //        .Select(p => p.thueHopDong.mbMatBang.MaMB).ToList();
            //    var khDVK = db.dvkDichVuThanhToans
            //        .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(tuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, denNgay) >= 0)
            //        .Select(p => p.dvkDichVu.mbMatBang.MaMB).ToList();
            //    var khTX = db.dvgxTheXeThanhToans
            //        .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(tuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, denNgay) >= 0)
            //        .Select(p => p.dvgxTheXe.mbMatBang.MaMB).ToList();
            //    var khNuoc = db.dvdnNuocs
            //        .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(tuNgay, p.NgayNhap.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayNhap.Value, denNgay) >= 0)
            //        .Select(p => p.mbMatBang.MaMB).ToList();
            //    var khDien = db.dvdnDiens
            //        .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(tuNgay, p.NgayNhap.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayNhap.Value, denNgay) >= 0)
            //        .Select(p => p.mbMatBang.MaMB).ToList();
            //    var khPQL = db.mbMatBangs
            //        .Where(p => !PQLDaThu.Contains(p.MaMB))
            //        .Select(p => p.MaMB).ToList();


            //    var newlist = khTM.Concat(khHD).Concat(khDVK).Concat(khTX).Concat(khNuoc).Concat(khDien).Concat(khPQL);
            //    newlist = newlist.Distinct();

            //    //Bind data to master 
            //    var lstMatBang = (from mb in db.mbMatBangs
            //                      where mb.MaKH != null
            //                      & (newlist.Contains(mb.MaMB))
            //                      select new
            //                      {
            //                          MaMB = mb.MaMB,
            //                          MaSoMB = mb.MaSoMB,
            //                          LoaiMB = mb.mbLoaiMatBang.TenLMB,
            //                          ThuocToaNha = mb.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
            //                          ThuocKhoiNha = mb.mbTangLau.mbKhoiNha.TenKN,
            //                          ThuocTangLau = mb.mbTangLau.TenTL,
            //                          DienTich = mb.DienTich,
            //                          ThuocKhachHang = mb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", mb.tnKhachHang.HoKH, mb.tnKhachHang.TenKH) : mb.tnKhachHang.CtyTen,
            //                          DiaChi = mb.tnKhachHang.IsCaNhan.Value ? mb.tnKhachHang.DCLL : mb.tnKhachHang.CtyDiaChi,
            //                          DienThoai = mb.tnKhachHang.IsCaNhan.Value ? mb.tnKhachHang.DienThoaiKH : mb.tnKhachHang.CtyDienThoai,
            //                          Email = mb.tnKhachHang.IsCaNhan.Value ? mb.tnKhachHang.EmailKH : mb.tnKhachHang.CtyFax,
            //                          TongTien = 0//TinhTongTien(mb.MaMB)
            //                      });
            //    gcGiayBao.DataSource = lstMatBang;
            //}
            #endregion
            try
            {
                var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                Year = Convert.ToInt32(itemYear.EditValue.ToString());
                Month = GetMonth();

                //load data
                gcGiayBao.DataSource = db.cnLichSu_HoaDonGiayBao(Month, Year, maTN);
            }
            catch { gcGiayBao.DataSource = null; }
            finally
            {
                wait.Close();
                wait.Dispose();
                tabPhiQuanLy.SelectedTabPageIndex = 0;
            }
        }

        private void BindDetailDataDien(int MaMB)
        {
            ctlDichVuDien.MaMB = MaMB;
            ctlDichVuDien.MaLDV = 1;
            ctlDichVuDien.Month = Month;
            ctlDichVuDien.Year = Year;
            ctlDichVuDien.LoadData();
        }

        private void BindDetailDataNuoc(int MaMB)
        {
            ctlDichVuNuoc.MaMB = MaMB;
            ctlDichVuNuoc.MaLDV = 2;
            ctlDichVuNuoc.Month = Month;
            ctlDichVuNuoc.Year = Year;
            ctlDichVuNuoc.LoadData();
        }

        private void BindDetailDataThueHopDong(int MaMB)
        {
            ctlDichVuHDThue.MaMB = MaMB;
            ctlDichVuHDThue.MaLDV = 5;
            ctlDichVuHDThue.Month = Month;
            ctlDichVuHDThue.Year = Year;
            ctlDichVuHDThue.LoadData();
        }

        private void BindDetailDataDichVuKhac(int MaMB)
        {
            ctlDichVuHDThue.MaMB = MaMB;
            ctlDichVuHDThue.MaLDV = 3;
            ctlDichVuHDThue.Month = Month;
            ctlDichVuHDThue.Year = Year;
            ctlDichVuHDThue.LoadData();
        }

        private void BindDetailDataThangMay(int MaMB)
        {
            ctlDichVuThangMay.MaMB = MaMB;
            ctlDichVuThangMay.MaLDV = 7;
            ctlDichVuThangMay.Month = Month;
            ctlDichVuThangMay.Year = Year;
            ctlDichVuThangMay.LoadData();
        }

        private void BindDetailDataTheXe(int MaMB)
        {
            ctlDichVuTheXe.MaMB = MaMB;
            ctlDichVuTheXe.MaLDV = 6;
            ctlDichVuTheXe.Month = Month;
            ctlDichVuTheXe.Year = Year;
            ctlDichVuTheXe.LoadData();
        }
        
        private void btnGiayBaoThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.ReportTemplate.rptPhieuThanhToan(list);
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {            
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            db = new MasterDataContext();
            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            itemYear.EditValue = db.GetSystemDate().Year;
            SetMonth();

            BindData();
            first = false;
        }

        int GetMonth()
        {
            if (itemMonth.EditValue == null)
                return 0;
            else
            {
                if (MM != itemMonth.EditValue.ToString())
                    MM = itemMonth.EditValue.ToString();
                switch (MM)
                {
                    case "Tháng 1":
                        return 1;
                    case "Tháng 2":
                        return 2;
                    case "Tháng 3":
                        return 3;
                    case "Tháng 4":
                        return 4;
                    case "Tháng 5":
                        return 5;
                    case "Tháng 6":
                        return 6;
                    case "Tháng 7":
                        return 7;
                    case "Tháng 8":
                        return 8;
                    case "Tháng 9":
                        return 9;
                    case "Tháng 10":
                        return 10;
                    case "Tháng 11":
                        return 11;
                    case "Tháng 12":
                        return 12;
                    default:
                        return 0;
                }
            }
        }

        void SetMonth()
        {
            int month = DateTime.Now.Month;

            MM = string.Format("Tháng {0}", month);

            itemMonth.EditValue = MM;
        }

        void SetDate(int index)
        {
            //KyBaoCao objKBC = new KyBaoCao();
            //objKBC.Index = index;
            //objKBC.SetToDate();

            //itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            //itemTuNgay.EditValue = objKBC.DateFrom;
            //itemDenNgay.EditValue = objKBC.DateTo;
            //itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiayBaoMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng cần thanh toán");  
                return;
            }
            //using (frmThanhToanAll frm = new frmThanhToanAll())
            //{
            //    frm.MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  
            //    frm.objnhanvien = objnhanvien;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        BindData();
            //    }
            //}

            //using (frmThanhToanMulti frm = new frmThanhToanMulti())
            //{
            //    frm.MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  
            //    frm.objnhanvien = objnhanvien;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        BindData();
            //    }
            //}

            using (var frm = new frmPaidMultiV3())
            {
                int month = GetMonth();
                int year = Convert.ToInt32(itemYear.EditValue.ToString());
                DateTime DateSystem = db.GetSystemDate();
                if (year == DateSystem.Year & month == DateSystem.Month)
                {
                    frm.Year = year;
                    frm.Month = month;
                }
                else
                {
                    if (year < DateSystem.Year)
                    {
                        if (month == 12)
                        {
                            frm.Month = 1;
                            frm.Year = year + 1;
                        }
                        else
                        {
                            frm.Month = month + 1;
                            frm.Year = year;
                        }
                    }
                    else
                    {
                        frm.Year = year;
                        frm.Month = month;
                    }
                }

                frm.MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  
                frm.MaKH = (int?)grvGiayBaoMatBang.GetFocusedRowCellValue("MaKH");  
                frm.MaSoMB = grvGiayBaoMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
                frm.objnhanvien = objnhanvien;
                var now = db.GetSystemDate();
                frm.Year = now.Year;
                frm.Month = now.Month;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    BindData();
                }
            }
        }

        private void btnEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.HeThongCls.SendMailCls sm = new Library.HeThongCls.SendMailCls();
            var lstThueCongNo = db.thueCongNos.Where(p => p.ConNo > 0 & p.ChuKyMin <= db.GetSystemDate()).Select(p=>p.thueHopDong.MaKH);
            var lstDien = db.dvdnDiens.Where(p => p.DaTT == false).Select(p=>p.MaKH);
            var lstNuoc = db.dvdnNuocs.Where(p => p.DaTT == false).Select(p=>p.MaKH);
            var lstdvk = 0;
            var lstThangMay = db.dvtmThanhToanThangMays.Where(p => p.DaTT == false).Select(p => p.dvtmTheThangMay.MaKH);

            var lstKhachHang = db.tnKhachHangs.Where(p => lstDien.Contains(p.MaKH) | lstThueCongNo.Contains(p.MaKH) | lstNuoc.Contains(p.MaKH) |
                                                        lstThangMay.Contains(p.MaKH))
                                                        .Select(p => p.EmailKH);

            foreach (var item in lstKhachHang)
            {
                var wait = DialogBox.WaitingForm();

                wait.SetCaption(item);

                wait.Close();
                wait.Dispose();
                //namespace DichVu.SendMail
                //TODO: MAIL nhac no
            }
        }

        private void grvGiayBaoMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        void Clicks()
        {
            try
            {
                int MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  

                switch (tabPhiQuanLy.SelectedTabPageIndex)
                {
                    case 0:
                        BindDetailDataDien(MaMB);
                        break;
                    case 1:
                        BindDetailDataNuoc(MaMB);
                        break;
                    case 2:
                        BindDetailDataThueHopDong(MaMB);
                        break;
                    case 3:
                        BindDetailDataDichVuKhac(MaMB);
                        break;
                    case 4:
                        BindDetailDataThangMay(MaMB);
                        break;
                    case 5:
                        BindDetailDataTheXe(MaMB);
                        break;
                    case 6:
                        BindDetailDataPQL(MaMB);
                        break;
                    case 7:
                        BindDetailDataGas(MaMB);
                        break;
                    case 8:
                        BindDetailDataPVS(MaMB);
                        break;
                    case 9:
                        BindDetailDataHoBoi(MaMB);
                        break;
                }
            }
            catch { }
        }

        private void BindDetailDataPVS(int MaMB)
        {
            ctlPhiVeSinh1.MaMB = MaMB;
            ctlPhiVeSinh1.MaLDV = 13;
            ctlPhiVeSinh1.Month = Month;
            ctlPhiVeSinh1.Year = Year;
            ctlPhiVeSinh1.LoadData();
        }

        private void BindDetailDataGas(int MaMB)
        {
            ctlDichVuGas.MaMB = MaMB;
            ctlDichVuGas.MaLDV = 11;
            ctlDichVuGas.Month = Month;
            ctlDichVuGas.Year = Year;
            ctlDichVuGas.LoadData();
        }

        private void BindDetailDataPQL(int MaMB)
        {
            ctlPhiQuanLy1.MaMB = MaMB;
            ctlPhiQuanLy1.MaLDV = 12;
            ctlPhiQuanLy1.Month = Month;
            ctlPhiQuanLy1.Year = Year;
            ctlPhiQuanLy1.LoadData();
        }

        private void BindDetailDataHoBoi(int MaMB)
        {
            ctlPhatSinhHoBoi.MaMB = MaMB;
            ctlPhatSinhHoBoi.MaLDV = 15;
            ctlPhatSinhHoBoi.Month = Month;
            ctlPhatSinhHoBoi.Year = Year;
            ctlPhatSinhHoBoi.LoadData();
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void tabPhiQuanLy_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 1))
                //{
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BindData();
        }

        private void itemCreateFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmCreateFee() { objnhanvien = objnhanvien };
            f.ShowDialog();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) BindData();
        }

        private void itemViewMau2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 2))
                //{
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemViewMau3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 3))
                //{
                //    frm.NguoiLap = objnhanvien.HoTenNV;
                //    frm.MM = MM;
                //    frm.yyyy = yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemInMau2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.Charm.rptGiaoBao(list, MM, yyyy);
                    //rpt.MM = MM;
                    //rpt.yyyy = yyyy;
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose();
            //f.Dispose();
            }
        }

        private void itemInMau3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.Sacomreal.rptGiayBaoTongHop(list[0].MaMB, objnhanvien.HoTenNV, MM, yyyy);
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose();  }
        }

        private void itemViewMau4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 4))
                //{
                //    frm.NguoiLap = objnhanvien.HoTenNV;
                //    frm.MM = MM;
                //    frm.yyyy = yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemInMau4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;
            
            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.Sacomreal.rptSanThuongMai(list[0].MaMB, objnhanvien.HoTenNV, MM, yyyy);
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemViewMau5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 5))
                //{
                //    frm.NguoiLap = objnhanvien.HoTenNV;
                //    frm.MM = MM;
                //    frm.yyyy = yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemViewMau6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 6))
                //{
                //    frm.NguoiLap = objnhanvien.HoTenNV;
                //    frm.MM = MM;
                //    frm.yyyy = yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemInMau5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.Sacomreal.rptGiayBao2(list[0].MaMB, objnhanvien.HoTenNV, MM, yyyy);
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose();  }
        }

        private void itemInMau6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.Sacomreal.rptGiayBaoTongHop(list[0].MaMB, objnhanvien.HoTenNV, MM, yyyy);
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose();  }
        }

        private void itemInMau7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //var rpt = new ReportMisc.DichVu.HoaDon.Himlam.rptGiayBao(list[0].MaMB, MM, yyyy);
                    //rpt.Print();

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemViewMau7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 7))
                //{
                //    frm.MM = f.MM;
                //    frm.yyyy = f.yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemPrintMau8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvGiayBaoMatBang.GetRowCellValue(item, "MaMB"));
                    var list = new List<mbMatBang>();
                    list.Add(objMB);

                    //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
                    //using (var rpt = new ReportMisc.DichVu.HoaDon.Himlam.rptHoaDonGiayBao(list[0].MaMB, MM, yyyy, barCodeString))
                    //{
                    //    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                    //    rpt.Print();
                    //    SavePrint(barCodeString, list[0].MaMB, MM, yyyy);
                    //}

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        void SavePrint(string barCode, int maMB, int month, int year)
        {
            var obj = new dvthLichSu();
            obj.DienGiai = "In phiếu Gas. Mã vạch [" + barCode + "]";
            obj.MaNV = objnhanvien.MaNV;
            obj.NgayTH = db.GetSystemDate();
            obj.Thang = month;
            obj.Nam = year;
            obj.MaMB = maMB;
            obj.MaLDV = 100;
            obj.Barcode = barCode;
            db.dvthLichSus.InsertOnSubmit(obj);

            db.SubmitChanges();
        }

        private void itemViewMau8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            else
            {
                if (rows.Length > 1)
                {
                    DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
                    return;
                }
            }

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();
                SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

                var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 9))
                //{
                //    frm.MM = f.MM;
                //    frm.yyyy = f.yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng [Dự án], xin cảm ơn.");  
                return;
            }

            var f = new KhoaSo.frmEdit();
            f.objnhanvien = objnhanvien;
            f.MaTN = Convert.ToByte(itemToaNha.EditValue);
            f.ShowDialog();
        }

        private void cmbMonth_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemViewMau9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            //if (rows.Length <= 0)
            //{
            //    DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
            //    return;
            //}
            //else
            //{
            //    if (rows.Length > 1)
            //    {
            //        DialogBox.Error("Vui lòng chọn 1 [Mặt bằng] cần in đề nghị thanh toán.");  
            //        return;
            //    }
            //}

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            //#region Bind data to report
            //try
            //{
            //    db = new MasterDataContext();
            //    List<int> SelectedMaHD = new List<int>();
            //    SelectedMaHD.Add((int)grvGiayBaoMatBang.GetFocusedRowCellValue(colMaMB));

            //    var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB)).ToList();
                
            //    if (lstmb.Count <= 0) return;
            //    using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, lstmb[0].IsCanHoCaNhan.GetValueOrDefault() ? 10 : 11))
            //    {
            //        frm.MM = f.MM;
            //        frm.yyyy = f.yyyy;
            //        frm.ShowDialog();
            //    }
            //}
            //catch { }
            //#endregion

            PrintMulti();
        }

        private void itemPrintMau9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                List<int> SelectedMaHD = new List<int>();

                foreach (var item in rows)
                {
                    var list = new List<int>();
                    list.Add(Convert.ToInt32(grvGiayBaoMatBang.GetRowCellValue(item, "MaMB")));

                    //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
                    var objMB = db.mbMatBangs.SingleOrDefault(p => p.MaMB == Convert.ToInt32(grvGiayBaoMatBang.GetRowCellValue(item, "MaMB")));
                    if (objMB.IsCanHoCaNhan.GetValueOrDefault())
                    {
                        //using (var rpt = new ReportMisc.DichVu.Khac.rptGiayBaoTongV2(list, yyyy, MM))
                        //{
                        //    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                        //    rpt.Print();
                        //    //SavePrint(barCodeString, list[0].MaMB, MM, yyyy);
                        //}
                    }
                    else
                    {
                        //using (var rpt = new ReportMisc.DichVu.Khac.rptGiayBaoTongV3(list, yyyy, MM))
                        //{
                        //    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                        //    rpt.Print();
                        //    //SavePrint(barCodeString, list[0].MaMB, MM, yyyy);
                        //}
                    }

                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void PrintMulti()
        {
            //List<Item> listItem = new List<Item>();
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán.");  
                return;
            }
            for (int i = 0; i < rows.Length; i++)
            {
                if ((decimal)grvGiayBaoMatBang.GetRowCellValue(rows[i], "TongTien") > 0)
                {
                    //var item = new Item();
                    //item.STT = i;
                    //item.IDHD = (int)grvGiayBaoMatBang.GetRowCellValue(rows[i], "MaMB");  
                    //listItem.Add(item);
                }
            }

            //if (listItem.Count <= 0) return;
            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            #region Bind data to report
            try
            {
                db = new MasterDataContext();
                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControlV2 frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControlV2(listItem, f.MM, f.yyyy))
                //{
                //    frm.MM = f.MM;
                //    frm.yyyy = f.yyyy;
                //    frm.MaMB = listItem[0].IDHD;
                //    frm.Stt = 0;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
        }

        private void itemMauTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintMulti();
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");  
                return;
            }

            var listMail = db.SendMailAccounts.ToList();
            if (listMail.Count <= 0) return;

            //var f = new ReportMisc.frmChooseMonthYear();
            //f.ShowDialog();
            //if (f.DialogResult != System.Windows.Forms.DialogResult.OK) return;

            //int MM = f.MM;
            //int yyyy = f.yyyy;

            var wait = DialogBox.WaitingForm();
            try
            {
                int count = 0;
                foreach (var i in rows)
                {
                    if (grvGiayBaoMatBang.GetRowCellValue(i, "Email").ToString() != "")
                    {
                        Random ran = new Random();
                        var index = ran.Next(0, listMail.Count - 1);
                        var item = listMail[index];
                        //send mail
                        MailProviderCls objMail = new MailProviderCls();
                        var objMailForm = new MailAddress(item.DiaChi, "Công Ty Cổ Phần Kim Cương Xanh");  
                        objMail.MailAddressFrom = objMailForm;
                        var objMailTo = new MailAddress(grvGiayBaoMatBang.GetRowCellValue(i, "Email").ToString(), grvGiayBaoMatBang.GetRowCellValue(i, "ThuocKhachHang").ToString());
                        objMail.MailAddressTo = objMailTo;
                        objMail.SmtpServer = item.Server;
                        objMail.EnableSsl = true;
                        objMail.PassWord = Commoncls.DecodeString(item.Password);
                        //objMail.Subject = string.Format("V/v Giấy báo thanh toán {0} tháng {1}/{2}", grvGiayBaoMatBang.GetRowCellValue(i, "MaSoMB"), MM, yyyy);
                        string str = "";
                        str += string.Format("Dear {0}!", grvGiayBaoMatBang.GetRowCellValue(i, "ThuocKhachHang"));
                        str += "<br/><br/>Kim Cương Xanh xin gửi đến Quý khách";
                        //str += string.Format(" <b>Thông tin giấy báo mặt bằng {0} tháng {1}/{2}</b><br/>", grvGiayBaoMatBang.GetRowCellValue(i, "MaSoMB"), MM, yyyy);
                        str += "Vui lòng xem file đính kèm. Xin chân thành cảm ơn.";

                        objMail.Content = str;
                        try
                        {
                            MemoryStream msPDF = new MemoryStream();
                            //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
                            var objMB = db.mbMatBangs.SingleOrDefault(p => p.MaMB == Convert.ToInt32(grvGiayBaoMatBang.GetRowCellValue(i, "MaMB")));
                            var list = new List<int>();
                            list.Add(Convert.ToInt32(grvGiayBaoMatBang.GetRowCellValue(i, "MaMB")));
                            if (objMB.IsCanHoCaNhan.GetValueOrDefault())
                            {
                                //var rpt = new ReportMisc.DichVu.Khac.rptGiayBaoTongV2(list, yyyy, MM);
                                //rpt.ExportToPdf(msPDF);
                            }
                            else
                            {
                                //var rpt = new ReportMisc.DichVu.Khac.rptGiayBaoTongV3(list, yyyy, MM);
                                //rpt.ExportToPdf(msPDF);
                            }

                            objMail.SendMailAttachFileV2(msPDF, string.Format("Hoa-don-giay-bao-{0}.pdf", grvGiayBaoMatBang.GetRowCellValue(i, "MaSoMB")));

                            //SaveSendMail(barCodeString, Convert.ToInt32(grvGiayBaoMatBang.GetRowCellValue(i, "MaMB")), MM, yyyy);
                        }
                        catch { }
                    }
                    count++;

                    wait.SetCaption(string.Format("Đã gửi {0}/{1} mặt bằng", count, listMail.Count));
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void SaveSendMail(string barCode, int maMB, int month, int year)
        {
            try
            {
                var obj = new dvthLichSu();
                obj.DienGiai = "Gửi mail. Mã vạch [" + barCode + "]";
                obj.MaNV = objnhanvien.MaNV;
                obj.NgayTH = db.GetSystemDate();
                obj.Thang = month;
                obj.Nam = year;
                obj.MaMB = maMB;
                obj.MaLDV = 100;
                obj.Barcode = barCode;
                db.dvthLichSus.InsertOnSubmit(obj);

                db.SubmitChanges();
            }
            catch { }
        }

        private void itemprintGBCTM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiayBaoMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mục cần xem thông báo!");  
                return;
            }
            int MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  
            Year = Convert.ToInt32(itemYear.EditValue.ToString());
            Month = GetMonth();
            //using (var rpt = new ReportMisc.DichVu.HoaDon.CTM.rptThongBaoPhiV2(MaMB, Month, Year))
            //{
            //    rpt.PrintDialog();
            //}
        }

        private void itemViewGBMatBang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var frm = new ReportMisc.BaoCaoCTM.ThongBao.ctlThongThueMB() { objnhanvien = objnhanvien })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void viewGBCTM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiayBaoMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mục cần xem thông báo!");  
                return;
            }
            int MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  
            Year = Convert.ToInt32(itemYear.EditValue.ToString());
            Month = GetMonth();
            //using (var rpt = new ReportMisc.DichVu.HoaDon.CTM.rptThongBaoPhiV2(MaMB, Month, Year))
            //{
            //    rpt.ShowPreviewDialog();
            //}
        }
    }
}