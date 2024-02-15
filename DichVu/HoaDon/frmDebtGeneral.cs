using System;
using System.Collections.Generic;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace DichVu.HoaDon
{
    public partial class frmDebtGeneral : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;
        public frmDebtGeneral()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
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

            var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            //if (maTN == 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");  
            //    return;
            //}
            //if (objnhanvien.IsSuperAdmin.Value)
                gcGiayBao.DataSource = db.cnCongNo_getBy(maTN);
            //else
            //{
            //    var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
            //    if (GetNhomOfNV.Count > 0)
            //    {
            //        var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

            //        gcGiayBao.DataSource = db.cnCongNo_getBy(maTN).Where(p => GetListNV.Contains(p.MaNV.Value));
            //    }
            //    else
            //    {
            //        gcGiayBao.DataSource = db.cnCongNo_getBy(maTN).Where(p => p.MaNV == objnhanvien.MaNV);
            //    }
            //}

            wait.Close();
            wait.Dispose();
            tabPhiQuanLy.SelectedTabPageIndex = 0;
        }

        private void BindDetailDataDien(int MaMB)
        {
            ctlDichVuDien.MaMB = MaMB;
            ctlDichVuDien.MaLDV = 1;
            ctlDichVuDien.LoadData();
        }

        private void BindDetailDataNuoc(int MaMB)
        {
            ctlDichVuNuoc.MaMB = MaMB;
            ctlDichVuNuoc.MaLDV = 2;
            ctlDichVuNuoc.LoadData();
        }

        private void BindDetailDataThueHopDong(int MaMB)
        {
            ctlDichVuHDThue.MaMB = MaMB;
            ctlDichVuHDThue.MaLDV = 5;
            ctlDichVuHDThue.LoadData();
        }

        private void BindDetailDataDichVuKhac(int MaMB)
        {
            DateTime now = db.GetSystemDate();
            //Bind data to grid
            var temp = from dvk in db.dvkDichVuThanhToans
                       where dvk.ThangThanhToan <= now & dvk.DaTT==false
                       select dvk.ThangThanhToan;
            if (temp.Count() <= 0)
            {
                gcDichVuKhac.DataSource = null;
                return;
            }
            else
            {
                var lstDVK = from dvk in db.dvkDichVuThanhToans
                             where dvk.DaTT == false & dvk.ThangThanhToan<=now
                             select new
                             {
                                 DaTT = dvk.DaTT,
                                 ThangThanhToan = String.Format("{0}/{1}", dvk.ThangThanhToan.Value.Month, dvk.ThangThanhToan.Value.Year),
                             };
                gcDichVuKhac.DataSource = lstDVK;
            }

        }

        private void BindDetailDataThangMay(int MaMB)
        {
            ctlDichVuThangMay.MaMB = MaMB;
            ctlDichVuThangMay.MaLDV = 7;
            ctlDichVuThangMay.LoadData();
        }

        private void BindDetailDataTheXe(int MaMB)
        {
            ctlDichVuTheXe.MaMB = MaMB;
            ctlDichVuTheXe.MaLDV = 6;
            ctlDichVuTheXe.LoadData();
        }


        private void btnGiayBaoThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                Library.DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");  
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
            
            //if(grvGiayBaoMatBang.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Chọn mặt bằng cần in đề nghị thanh toán");  
            //    return;
            //}
            ////int MaMB = (int)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB");  
            //DateTime now = db.GetSystemDate();

            //#region bind data to report
            //db = new MasterDataContext();
            //List<int> SelectedMaHD = new List<int>();
            //foreach (var item in grvGiayBaoMatBang.GetSelectedRows())
            //{
            //    SelectedMaHD.Add((int)grvGiayBaoMatBang.GetRowCellValue(item, colMaMB));
            //}
            //var lstmb = db.mbMatBangs.Where(p => SelectedMaHD.Contains(p.MaMB) & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN).ToList();

            //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb))
            //{
            //    frm.ShowDialog();
            //}
            //#endregion
        }

        private decimal TinhTongTien(int MaMB)
        {
            decimal tt = 0;
            db = new MasterDataContext();
            DateTime now = db.GetSystemDate();

            tt += db.dvtmThanhToanThangMays.Where(p => p.DaTT == false & p.dvtmTheThangMay.MaMB == MaMB & p.ThangThanhToan <= now).Sum(p => p.dvtmTheThangMay.PhiLamThe) ?? 0;
            tt += db.dvdnNuocs.Where(p => !p.DaTT.Value & p.MaMB == MaMB & p.NgayNhap <= now).Sum(p => p.TienNuoc) ?? 0;
            tt += db.dvdnDiens.Where(p => !p.DaTT.Value & p.MaMB == MaMB & p.NgayNhap <= now).Sum(p => p.TienDien) ?? 0;
            tt +=  0;
            tt += db.thueCongNos.Where(p => p.ConNo > 0 & now >= p.ChuKyMin & p.thueHopDong.MaMB == MaMB & p.ChuKyMax <= now).Sum(p => p.ConNo) ?? 0;

            return tt;
        }

        private void frmManager_Load(object sender, EventArgs e)
        {            
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            db = new MasterDataContext();
            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            BindData();

            //GeneratorThanhToan();
            //Ky bao cao
            //KyBaoCao objKBC = new KyBaoCao();
            //foreach (string str in objKBC.Source)
            //    cbbKyBC.Items.Add(str);
            //itemKyBaoCao.EditValue = objKBC.Source[3];
            //SetDate(3);
            first = false;
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
                }
            }
            catch { }
        }

        private void BindDetailDataPVS(int MaMB)
        {
            ctlPhiVeSinh1.MaMB = MaMB;
            ctlPhiVeSinh1.LoadData();
        }

        private void BindDetailDataGas(int MaMB)
        {
            ctlDichVuGas.MaMB = MaMB;
            ctlDichVuGas.MaLDV = 11;
            ctlDichVuGas.LoadData();
        }

        private void BindDetailDataPQL(int MaMB)
        {
            ctlPhiQuanLy1.MaMB = MaMB;
            ctlPhiQuanLy1.LoadData();
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
            finally { wait.Close(); wait.Dispose(); 
                //f.Dispose();
            }
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
            finally { wait.Close(); wait.Dispose(); 
                //f.Dispose();
            }
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
            finally { wait.Close(); wait.Dispose(); 
                //f.Dispose(); 
            }
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
            finally { wait.Close(); wait.Dispose(); 
                //f.Dispose(); 
            }
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
            finally { wait.Close(); wait.Dispose(); 
                //f.Dispose(); 
            }
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

                    string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
                    //using (var rpt = new ReportMisc.DichVu.HoaDon.Himlam.rptGiayBaoChung(list[0].MaMB, MM, yyyy, barCodeString))
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
            finally { wait.Close(); wait.Dispose(); //f.Dispose(); 
            }
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

                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, 8))
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

        List<ItemData> listMB;

        private void itemCreateFeeSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");  
                return;
            }

            var f = new frmChooseDate();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK & f.PickedDate != null)
            {
                //Check Khoa So
                DichVu.KhoaSo.ManagerCls.IsFirst = true;
                DichVu.KhoaSo.ManagerCls.Month = f.PickedDate.Value.Month;
                DichVu.KhoaSo.ManagerCls.TowerID = Convert.ToByte(itemToaNha.EditValue);
                DichVu.KhoaSo.ManagerCls.TowerName = lookUpToaNha.GetDisplayText(itemToaNha.EditValue);
                DichVu.KhoaSo.ManagerCls.Year = f.PickedDate.Value.Year;
                if (DichVu.KhoaSo.ManagerCls.CheckEditData())
                    return;

                var wait = DialogBox.WaitingForm();
                try
                {
                    int count = 1;
                    foreach (var i in rows)
                    {
                        listMB = new List<ItemData>();
                        //listMB = db.mbMatBangs.Where(p => p.MaMB == (int?)grvGiayBaoMatBang.GetRowCellValue(i, "MaMB")).Select(p => new ItemData() { ID = p.MaMB, CusID = p.MaKH.Value, Fee = p.PhiQuanLy ?? 0, Fee2 = p.PhiVeSinh ?? 0, DateFee = p.BatDauTinhPhi, DateFeeVS = p.NgayTinhPVS, DateEndFee = p.KetThucTinhPhi, IsChuyen = p.IsChuyenThangSau.GetValueOrDefault() }).ToList();

                        PhiQuanLy(f.PickedDate.Value);

                        wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Length));
                        count++;
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }
            }
        }

        void PhiQuanLy(DateTime date)
        {
            foreach (var item in listMB)
            {
                if (SqlMethods.DateDiffDay(item.DateFee, date) >= 0)
                {
                    db.cnLichSu_addPQL(item.ID, item.CusID, date, GetFee(item, date));

                    var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, date) >= 0 & SqlMethods.DateDiffDay(date, p.NgayKT) >= 0 & p.MaMB == item.ID).FirstOrDefault();
                    if (objPDK != null)
                    {
                        if (objPDK.NgayDK.Value.Month != date.Month | objPDK.NgayDK.Value.Year != date.Year)
                            db.cnLichSu_addPQL(item.ID, item.CusID, date, 0);
                    }
                }
                else
                    db.cnLichSu_addPQL(item.ID, item.CusID, date, 0);
            }
        }

        private decimal GetFee(ItemData item, DateTime date)
        {
            decimal Fee = 0;
            try
            {
                //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên
                if (item.DateFee.Value.Month == date.Month & item.DateFee.Value.Year == date.Year)
                {
                    if (item.IsChuyen)
                        Fee = 0;
                    else
                    {
                        //Ngày của tháng
                        int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                        //Ngày ở thực tế
                        int dayReal = day - item.DateFee.Value.Day;

                        Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                    }
                }
                else//Tháng tiếp theo
                {
                    if (item.IsChuyen)
                    {
                        //Chuyen sang thang sau tinh phi thang dau tien
                        if ((item.DateFee.Value.Month + 1) == date.Month & item.DateFee.Value.Year == date.Year)
                        {
                            //Ngày của tháng
                            int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                            //Ngày ở thực tế
                            int dayReal = day - item.DateFee.Value.Day;

                            Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + item.Fee;//+1 tính luôn ngày vào ở
                        }//
                        else
                            Fee = item.Fee;
                    }
                    else
                        Fee = item.Fee;
                }

                var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, date) >= 0 & SqlMethods.DateDiffDay(date, p.NgayKT) >= 0 & p.MaMB == item.ID).FirstOrDefault();
                if (objPDK != null)
                {
                    if (objPDK.NgayDK.Value.Month == date.Month & item.DateFee.Value.Year == date.Year)
                        Fee = objPDK.PhaiThu ?? 0;
                    else
                        Fee = 0;
                }
            }
            catch { Fee = 0; }

            return Fee;
        }

        private void itemViewMau9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                if (lstmb.Count <= 0) return;
                //using (ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.HoaDon.ReportTemplate.frmPrintControl(lstmb, lstmb[0].IsCanHoCaNhan.GetValueOrDefault() ? 10 : 11))
                //{
                //    frm.MM = f.MM;
                //    frm.yyyy = f.yyyy;
                //    frm.ShowDialog();
                //}
            }
            catch { }
            #endregion
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
            finally { wait.Close(); wait.Dispose(); //f.Dispose(); 
            }
        }
    }
}