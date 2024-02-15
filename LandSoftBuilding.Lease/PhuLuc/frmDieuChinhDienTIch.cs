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
using DevExpress.XtraGrid.Views.Grid;
using LandSoftBuilding.Lease.Class;

namespace LandSoftBuilding.Lease
{
    public partial class frmDieuChinhDienTIch : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPL { get; set; }
        public int? MaHD { get; set; }
        public bool? IsDieuChinh { get; set; }

        int DotTT = 0;

        ctHopDong objHD;
        ctPhuLuc objPL;
        

        List<ctLichThanhToan> ctBackup = new List<ctLichThanhToan>();

        /// <summary>
        /// 1: phụ lục điều chỉnh giá
        /// 2: phụ lục điều chỉnh diện tích
        /// 5: phụ lục điều chỉnh tiền cọc
        /// 11: phụ lục điều chỉnh phí quản lý
        /// 12: phụ lục điều chỉnh phí điều hòa chiếu sáng
        /// </summary>
        int[] LoaiPhuLuc_Edit = { 1 , 2, 3, 4, 5, 6, 7, 8, 9,10, 11, 12, 13 };

        MasterDataContext db = new MasterDataContext();
        class LoaiPhuLuc
        {
            public int? ID { set; get; }
            public string TenLoai { set; get; }
        }
        public frmDieuChinhDienTIch()
        {
            InitializeComponent();
            ckb.EditValueChanged += ckb_EditValueChanged;
        }

        void ckb_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = sender as CheckEdit;

            gvLTT.SetFocusedRowCellValue("IsCheck", ckb.Checked);
            GetGiaThue();
        }

        void Load_LichThanhToan()
        {
            var ltLTT = db.ctLichThanhToans.Where(o => o.MaHD == MaHD).GroupBy(o => new { o.DotTT, o.TuNgay, o.DenNgay }).Select(o => new { o.Key.DotTT, o.Key.TuNgay, o.Key.DenNgay });
            glkKyTT_From.Properties.DataSource = ltLTT;
            glkKyTT_To.Properties.DataSource = ltLTT;
        }

        private void itemSubmit_Click(object sender, EventArgs e)
        {
            gvPhuLuc.FocusedRowHandle = -1;
            foreach (var check in objPL.ctPhuLuc_ChiTiets)
            {
                if (objPL.MaPL > 0 && !LoaiPhuLuc_Edit.Contains(check.MaLoaiPL.Value))
                    continue;
                switch (check.MaLoaiPL)
                {
                    case 1: // Điều chỉnh giá
                        if (check.MaMB == null)
                        {
                            DialogBox.Error("<Điều chỉnh giá thuê> chưa chọn <Mặt bằng>");
                            return;
                        }

                        if (check.DonGia == null)
                        {
                            DialogBox.Error("<Điều chỉnh giá thuê> chưa nhập <Giá thuê>");
                            return;
                        }

                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Điều chỉnh Giá thuê> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        if (check.MaLoaiTien == null)
                        {
                            DialogBox.Error("<Điều chỉnh giá thuê> chưa chọn <Loại tiền>");
                            return;
                        }
                        break;
                    case 2: // Điều chỉnh diện tích
                        if (check.MaMB == null)
                        {
                            DialogBox.Error("<Điều chỉnh Diện tích> chưa chọn <Mặt bằng>");
                            return;
                        }

                        if (check.DienTich == null)
                        {
                            DialogBox.Error("<Điều chỉnh diện tích> chưa nhập <Diện tích>");
                            return;
                        }

                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Điều chỉnh Diện tích> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        break;
                    case 3:
                        break;
                    case 4:

                        break;
                    case 5: // Điều chỉnh tiền cọc
                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Điều chỉnh tiền cọc> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        //var checkLTTCoc = (from ltt in db.ctLichThanhToans
                        //                   where ltt.MaLDV == 4 && ltt.MaHD == MaHD
                        //                   select new
                        //                   {
                        //                       ltt.DotTT,
                        //                       ltt.TuNgay,
                        //                       ltt.DenNgay,
                        //                   }).ToList();

                        //foreach (var i in checkLTTCoc)
                        //{
                        //    if (check.TuNgay >= i.TuNgay && check.DenNgay <= i.DenNgay)
                        //    {
                        //        this.DotTT = i.DotTT.Value;
                        //    }
                        //}
                        //if (this.DotTT == 0)
                        //{
                        //    DialogBox.Error("<Điều chỉnh tiền cọc> Không tìm thấy đợt thanh toán <Từ ngày> - <Đến ngày>");
                        //    return;
                        //}



                        break;
                    case 6: // Gia hạn hợp đồng
                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Gia hạn hợp đồng> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        break;
                    case 8:
                        break;
                    case 9: // Điều chỉnh tỷ giá

                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Điều chỉnh Tỷ giá> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }

                        if (check.MaLoaiTien == null)
                        {
                            DialogBox.Error("<Điều chỉnh Tỷ giá> chưa chọn <Loại tiền>");
                            return;
                        }
                        break;
                    case 10:
                        // Điều chỉnh pháp nhân
                        if (check.MaKH == null | check.MaKHMoi == null)
                        {
                            DialogBox.Error("<ĐIỀU CHỈNH PHÁP NHÂN> chưa chọn đủ thông tin <Pháp nhân cũ> và <Pháp nhân mới>");
                            return;
                        }
                        break;

                    case 11: // phụ lục điều chỉnh phí quản lý
                        if (check.MaMB == null)
                        {
                            DialogBox.Error("<Điều chỉnh phí quản lý> chưa chọn <Mặt bằng>");
                            return;
                        }

                        if (check.PQL_DonGia_New == null)
                        {
                            DialogBox.Error("<Điều chỉnh phí quản lý> chưa nhập <Phí quản lý>");
                            return;
                        }

                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Điều chỉnh phí quản lý> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        if (check.MaLoaiTien == null)
                        {
                            DialogBox.Error("Điều chỉnh Phí quản lý> chưa chọn <Loại tiền>");
                            return;
                        }

                        break;

                    case 12: // phụ lục điều chỉnh phí điều hòa chiếu sáng
                        if (check.MaMB == null)
                        {
                            DialogBox.Error("<Điều chỉnh phí điều hòa chiếu sáng> chưa chọn <Mặt bằng>");
                            return;
                        }

                        if (check.PDHCS_DonGia_New == null)
                        {
                            DialogBox.Error("<Điều chỉnh phí điều hòa chiếu sáng> chưa nhập <Phí quản lý>");
                            return;
                        }

                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Điều chỉnh phí điều hòa chiếu sáng> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        if (check.MaLoaiTien == null)
                        {
                            DialogBox.Error("Điều chỉnh phí điều hòa chiếu sáng> chưa chọn <Loại tiền>");
                            return;
                        }

                        break;
                    case 13: // Giảm giá
                        if (check.MaMB == null)
                        {
                            DialogBox.Error("<Phụ lục giảm giá> chưa chọn <Mặt bằng>");
                            return;
                        }
                        if (check.TuNgay == null | check.DenNgay == null)
                        {
                            DialogBox.Error("<Phụ lục giảm giá> chưa nhập đầy đủ <Từ ngày> - <Đến ngày>");
                            return;
                        }
                        if (check.MaLoaiGiamGia == null)
                        {
                            DialogBox.Error("<Phụ lục giảm giá> chưa chọn <Loại giảm giá>");
                            return;
                        }
                        if (check.MaLDV == null)
                        {
                            DialogBox.Error("<Phụ lục giảm giá> chưa chọn <Loại dịch vụ giảm>");
                            return;
                        }
                        if (check.SoTienGiam == null || check.PhanTramGiamGia == null)
                        {
                            DialogBox.Error("<Phụ lục giảm giá> chưa nhập <tiền giảm>");
                            return;
                        }
                        break;
                } 
            }

            objPL.SoPL = txtSoPL.Text;
            objPL.NgayPL = dateNgayDC.DateTime;
            objPL.IsDieuChinh = IsDieuChinh.GetValueOrDefault();

            
            #region backup dữ liệu lịch thanh toán, hợp đồng theo phụ lục

            //BackUp chi tiết hợp đồng trước.
            var cthd = db.ctChiTiets.Where(o => o.MaHDCT == objHD.ID).ToList();
            //lưu để lấy dữ liệu phụ lục.
            db.SubmitChanges();

            if (MaPL != null)
            {
                //Xóa chi tiết hợp đồng cũ
                var ct_bk = db.ctChiTiet_BackUps.Where(o => o.id_pl == objPL.MaPL).ToList();
                if (ct_bk.Count() > 0)
                {
                    db.ctChiTiet_BackUps.DeleteAllOnSubmit(ct_bk);
                    db.SubmitChanges();
                }

                //Xóa chi tiết lịch thanh toán cũ
                var kt_ct = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == objPL.MaPL).ToList();
                if (kt_ct.Count() <= 0)
                {
                    db.ctLichThanhToan_BackUps.DeleteAllOnSubmit(kt_ct);
                    db.SubmitChanges();
                } 



            }

            foreach (var add in cthd)
            {
                ctChiTiet_BackUp ctbk = new ctChiTiet_BackUp();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                ctbk.id_pl = objPL.MaPL;
                db.ctChiTiet_BackUps.InsertOnSubmit(ctbk);
                db.SubmitChanges();
            }
            if (MaPL == null)
            {
                foreach (var add in ctBackup)
                {
                    ctLichThanhToan_BackUp ctbk = new ctLichThanhToan_BackUp();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                    ctbk.id_pl = objPL.MaPL;
                    db.ctLichThanhToan_BackUps.InsertOnSubmit(ctbk);
                    db.SubmitChanges();
                }
            }
            //BackUp chi tiết lịch thanh toán

            #endregion
         
            foreach (var item in objPL.ctPhuLuc_ChiTiets)
            {
                if (objPL.MaPL > 0 && !LoaiPhuLuc_Edit.Contains(item.MaLoaiPL.Value))
                    continue;

                switch (item.MaLoaiPL)
                {
                    case 1: // Điều chỉnh giá
                        //ĐƯỢC SỬA
                        DieuChinhGia(item);
                        break;
                    case 2: // Điều chỉnh diện tích
                        // ĐƯỢC SỬA
                        DieuChinhDienTich(item);
                        break;
                    case 3:// ĐƯỢC SỬA
                        ChuyenPhong(item);
                        break;
                    case 4:
                        NgungSuDung(item);
                        break;
                    case 5:
                        DieuChinhTienCoc(item);
                        break;
                    case 6: // Gia hạn hợp đồng
                        GiaHan(item);
                        break;
                    case 8:
                        break;
                    case 9: // Điều chỉnh tỷ giá
                        // ĐƯỢC SỬA
                        DieuChinhTyGia(item);
                        break;
                    case 10:
                        DieuChinhPhapNhan(item);
                        break;
                    case 11:
                        DieuChinhPhiQuanLy(item);
                        break;
                    case 12:
                        DieuChinhPhiDieuHoaChieuSang(item);
                        break;
                    case 13:
                        DieuChinhPhuLucGiamGia(item);
                        break;
                }
            }

            // Kiểm tra có làm phụ lục bổ sung thêm mặt bằng
            var objChiTiet = (from ct in objHD.ctChiTiets
                              group ct by ct.MaMB into g
                              select new
                              {
                                  MaMB = g.Key.Value
                              }).ToList();
            foreach (var ct in objChiTiet)
            {
                var value = (from t in objHD.ctChiTiets
                             where t.MaMB == ct.MaMB
                             orderby t.DenNgay descending
                             select t).FirstOrDefault();
                if(value != null)
                {
                    var obj = new ctPhuLuc_ChiTiet();
                    obj.TuNgay = DateTime.Now;
                    obj.DenNgay = value.DenNgay;
                    obj.MaMB = ct.MaMB;
                    obj.Them_DonGia = value.DonGia;
                    obj.Them_DienTich = value.DienTich;
                    obj.Them_GiaThue = value.TongGiaThue;
                }
                
            }
            db.SubmitChanges();

            #region Tính lại tiền lịch thanh toán theo công thức
            Library.Class.Connect.QueryConnect.QueryData<bool>("ctLichThanhToanUpdateValue", new { MaHD = objHD.ID });
            #endregion

            this.Close();
        }
        void DieuChinhPhuLucGiamGia(ctPhuLuc_ChiTiet item)
        {
            
            if (MaPL != null)
            {
                if (this.MaHD != null && this.MaPL != null)
                    XoaPhuLuc();
                var ltt_bk = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
                foreach (var add in ltt_bk)
                {
                    ctLichThanhToan_BackUp ctbk = new ctLichThanhToan_BackUp();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                    ctbk.id_pl = objPL.MaPL;
                    db.ctLichThanhToan_BackUps.InsertOnSubmit(ctbk);
                    db.SubmitChanges();
                }
                var ltLTT_u = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                & SqlMethods.DateDiffDay(o.TuNgay, item.DenNgay) >= 0
                                                & o.MaMB == item.MaMB
                                                & !o.IsKhuyenMai.GetValueOrDefault());

                foreach (var ltt in ltLTT_u)
                {
                    if (ltt.TienCK == null)
                        ltt.TienCK = 0;
                    if (item.MaLoaiGiamGia == 1)
                    {
                        if (item.MaLDV == 1)
                        {
                            ltt.TienCK += (ltt.DonGia * item.PhanTramGiamGia);
                            ltt.DonGia = ltt.DonGia - (ltt.DonGia * item.PhanTramGiamGia);
                        }
                        else if (item.MaLDV == 2)
                        {
                            ltt.TienCK += (ltt.PhiDichVu * item.PhanTramGiamGia);
                            ltt.PhiDichVu = ltt.PhiDichVu - (ltt.PhiDichVu * item.PhanTramGiamGia);
                        }
                        else if (item.MaLDV == 3)
                        {
                            ltt.TienCK += (ltt.PhiDieuHoaChieuSang * item.PhanTramGiamGia);
                            ltt.PhiDieuHoaChieuSang = ltt.PhiDieuHoaChieuSang - (ltt.PhiDieuHoaChieuSang * item.PhanTramGiamGia);
                        }
                    }
                    else if (item.MaLoaiGiamGia == 2)
                    {
                        if (item.MaLDV == 1)
                            ltt.DonGia = ltt.DonGia - item.SoTienGiam;
                        else if (item.MaLDV == 2)
                            ltt.PhiDichVu = ltt.PhiDichVu - item.SoTienGiam;
                        else if (item.MaLDV == 3)
                            ltt.PhiDieuHoaChieuSang = ltt.PhiDieuHoaChieuSang - item.SoTienGiam;
                        ltt.TienCK += item.SoTienGiam;
                    }
                    SchedulePaymentCls.ctLichThanhToanCoVAT(ltt, objHD.IsLamTron.GetValueOrDefault());
                }
                db.SubmitChanges();

                return;

            }
            //BackUp chi tiết lịch thanh toán   
            var LTT = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
            db.ctLichThanhToans.DeleteAllOnSubmit(LTT);
            var ctltt = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == item.MaPL).ToList();
            foreach (var add in ctltt)
            {
                ctLichThanhToan ct_ltt = new ctLichThanhToan();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ct_ltt);
                db.ctLichThanhToans.InsertOnSubmit(ct_ltt);
                db.SubmitChanges();
            }
            // Điều chỉnh lịch thanh toán
            var ltLTT = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.TuNgay, item.DenNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & !o.IsKhuyenMai.GetValueOrDefault());

            foreach (var ltt in ltLTT)
            {
                if (ltt.TienCK == null)
                    ltt.TienCK = 0;
                if (item.MaLoaiGiamGia == 1)
                {
                    if (item.MaLDV == 1)
                    {
                        ltt.TienCK += (ltt.DonGia * item.PhanTramGiamGia);
                        ltt.DonGia = ltt.DonGia - (ltt.DonGia * item.PhanTramGiamGia);
                    }
                    else if (item.MaLDV == 2)
                    {
                        ltt.TienCK += (ltt.PhiDichVu * item.PhanTramGiamGia);
                        ltt.PhiDichVu = ltt.PhiDichVu - (ltt.PhiDichVu * item.PhanTramGiamGia);
                    }
                    else if (item.MaLDV == 3)
                    {
                        ltt.TienCK += (ltt.PhiDieuHoaChieuSang * item.PhanTramGiamGia);
                        ltt.PhiDieuHoaChieuSang = ltt.PhiDieuHoaChieuSang - (ltt.PhiDieuHoaChieuSang * item.PhanTramGiamGia);
                    }
                }
                else if (item.MaLoaiGiamGia == 2)
                {
                    if (item.MaLDV == 1)
                        ltt.DonGia = ltt.DonGia - item.SoTienGiam;
                    else if (item.MaLDV == 2)
                        ltt.PhiDichVu = ltt.PhiDichVu - item.SoTienGiam;
                    else if (item.MaLDV == 3)
                        ltt.PhiDieuHoaChieuSang = ltt.PhiDieuHoaChieuSang - item.SoTienGiam;
                    ltt.TienCK += item.SoTienGiam;
                }
                SchedulePaymentCls.ctLichThanhToanCoVAT(ltt, objHD.IsLamTron.GetValueOrDefault());
            }
            db.SubmitChanges();
        }
        void XoaPhuLuc()
        {
            var idhd = this.MaHD;
            var idpl = this.MaPL;

            var objPL = db.ctPhuLucs.Single(o => o.MaPL == idpl);
           
            //if (objPL.ctPhuLuc_ChiTiets.Where(o => o.MaLoaiPL == 6 & o.MaPL == objPL.MaPL).Count() > 0)
            //{
            //    var HD = db.ctHopDongs.SingleOrDefault(o => o.ID == idhd);
            //    HD.NgayHH = HD.NgayTruocGiaHan;
            //    db.SubmitChanges();
            //}

            //Update Lichthanhtoan
            var ctltt = db.ctLichThanhToans.Where(o => o.MaHD == idhd).ToList();
            db.ctLichThanhToans.DeleteAllOnSubmit(ctltt);

            var ctltt_bk = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == idpl).ToList();
            foreach (var add in ctltt_bk)
            {
                ctLichThanhToan ctbk = new ctLichThanhToan();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                db.ctLichThanhToans.InsertOnSubmit(ctbk);
                db.SubmitChanges();
            }
            db.ctLichThanhToan_BackUps.DeleteAllOnSubmit(ctltt_bk);

            //Update Chitiet hợp đồng
            //db.ctChiTiets.DeleteAllOnSubmit(db.ctChiTiets.Where(o => o.MaHDCT == idhd && o.IsXoa.GetValueOrDefault()== false).ToList());
            var ctChiTietHD = db.ctChiTiets.Where(o => o.MaHDCT == idhd).ToList();
            foreach (var item in ctChiTietHD)
            {
                var ct = db.ctChiTiets.Single(o => o.ID == item.ID);
                db.ctChiTiets.DeleteOnSubmit(ct);
            }
            db.SubmitChanges();

            var sumCocDotTT = db.ctLichThanhToans.Where(p => p.MaHD == idhd && p.MaLDV == 4).Select(p => p.SoTien).Sum();
            var objHD_UCoc = db.ctHopDongs.Where(p => p.ID == idhd).Select(p => p).FirstOrDefault();

            objHD_UCoc.TienCoc = sumCocDotTT;
            db.SubmitChanges();

            var cthd_bk = db.ctChiTiet_BackUps.Where(o => o.id_pl == idpl).ToList();
            foreach (var add in cthd_bk)
            {
                ctChiTiet ctbk = new ctChiTiet();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                //ctbk.IsXoa = false;
                db.ctChiTiets.InsertOnSubmit(ctbk);
                db.SubmitChanges();
            }
            db.ctChiTiet_BackUps.DeleteAllOnSubmit(cthd_bk);
            db.SubmitChanges();
        }
        void DieuChinhGia(ctPhuLuc_ChiTiet item)
        {
            if(MaPL!= null)
            {
                var ltt_bk = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
                foreach (var add in ltt_bk)
                {
                    ctLichThanhToan_BackUp ctbk = new ctLichThanhToan_BackUp();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                    ctbk.id_pl = objPL.MaPL;
                    db.ctLichThanhToan_BackUps.InsertOnSubmit(ctbk);
                    db.SubmitChanges();
                }
                var ltLTT_u = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                & o.MaMB == item.MaMB
                                                & !o.IsKhuyenMai.GetValueOrDefault());

                foreach (var ltt in ltLTT_u)
                {
                    ltt.DonGia = item.DonGia;
                    ltt.TyGia = item.TyGia;
                    ltt.MaLoaiTien = item.MaLoaiTien;
                    SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
                }
                db.SubmitChanges();

                return;

            }
            //BackUp chi tiết lịch thanh toán   
            var LTT = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
            db.ctLichThanhToans.DeleteAllOnSubmit(LTT);
            var ctltt = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == item.MaPL).ToList();
            foreach (var add in ctltt)
            {
                ctLichThanhToan ct_ltt = new ctLichThanhToan();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ct_ltt);
                db.ctLichThanhToans.InsertOnSubmit(ct_ltt);
                db.SubmitChanges();
            }
            // Điều chỉnh lịch thanh toán
            var ltLTT = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & !o.IsKhuyenMai.GetValueOrDefault());

            foreach (var ltt in ltLTT)
            {
                ltt.DonGia = item.DonGia;
                ltt.TyGia = item.TyGia;
                ltt.MaLoaiTien = item.MaLoaiTien;     
                SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
            }
            db.SubmitChanges();
        }

        void DieuChinhPhiQuanLy(ctPhuLuc_ChiTiet item)
        {
            if (MaPL != null)
            {
                var ltt_bk = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
                foreach (var add in ltt_bk)
                {
                    ctLichThanhToan_BackUp ctbk = new ctLichThanhToan_BackUp();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                    ctbk.id_pl = objPL.MaPL;
                    db.ctLichThanhToan_BackUps.InsertOnSubmit(ctbk);
                    db.SubmitChanges();
                }
                var ltLTT_u = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                & o.MaMB == item.MaMB
                                                & !o.IsKhuyenMai.GetValueOrDefault());

                foreach (var ltt in ltLTT_u)
                {
                    ltt.PhiDichVu = item.PQL_DonGia_New;
                    ltt.TyGia = item.TyGia;
                    ltt.MaLoaiTien = item.MaLoaiTien;
                    SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
                }
                db.SubmitChanges();

                return;

            }
            //BackUp chi tiết lịch thanh toán   
            var LTT = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
            db.ctLichThanhToans.DeleteAllOnSubmit(LTT);
            var ctltt = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == item.MaPL).ToList();
            foreach (var add in ctltt)
            {
                ctLichThanhToan ct_ltt = new ctLichThanhToan();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ct_ltt);
                db.ctLichThanhToans.InsertOnSubmit(ct_ltt);
                db.SubmitChanges();
            }

            // Điều chỉnh lịch thanh toán
            var ltLTT = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & !o.IsKhuyenMai.GetValueOrDefault());

            foreach (var ltt in ltLTT)
            {
                ltt.PhiDichVu = item.PQL_DonGia_New;
                ltt.TyGia = item.TyGia;
                ltt.MaLoaiTien = item.MaLoaiTien;
                SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
            }

            db.SubmitChanges();

            // Update chi tiết mặt bằng
            Library.Class.Connect.QueryConnect.QueryData<bool>("ct_PhuLuc_PhiDichVu",
                new
                {
                    MaHD = objPL.MaHD,
                    MaMB = item.MaMB,
                    TuNgay = item.TuNgay,
                    DenNgay = item.DenNgay,
                    DonGiaMoi = item.PQL_DonGia_New,
                    Type = 1,
                    MaPLCT = item.ID
                });
        }

        void DieuChinhPhiDieuHoaChieuSang(ctPhuLuc_ChiTiet item)
        {
            if (MaPL != null)
            {
                var ltt_bk = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
                foreach (var add in ltt_bk)
                {
                    ctLichThanhToan_BackUp ctbk = new ctLichThanhToan_BackUp();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                    ctbk.id_pl = objPL.MaPL;
                    db.ctLichThanhToan_BackUps.InsertOnSubmit(ctbk);
                    db.SubmitChanges();
                }
                var ltLTT_u = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                & o.MaMB == item.MaMB
                                                & !o.IsKhuyenMai.GetValueOrDefault());

                foreach (var ltt in ltLTT_u)
                {
                    ltt.PhiDieuHoaChieuSang = item.PDHCS_DonGia_New;
                    ltt.TyGia = item.TyGia;
                    ltt.MaLoaiTien = item.MaLoaiTien;
                    SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
                }
                db.SubmitChanges();

                return;

            }
            //BackUp chi tiết lịch thanh toán   
            var LTT = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
            db.ctLichThanhToans.DeleteAllOnSubmit(LTT);
            var ctltt = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == item.MaPL).ToList();
            foreach (var add in ctltt)
            {
                ctLichThanhToan ct_ltt = new ctLichThanhToan();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ct_ltt);
                db.ctLichThanhToans.InsertOnSubmit(ct_ltt);
                db.SubmitChanges();
            }

            // Điều chỉnh lịch thanh toán
            var ltLTT = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & !o.IsKhuyenMai.GetValueOrDefault());

            foreach (var ltt in ltLTT)
            {
                ltt.PhiDieuHoaChieuSang = item.PDHCS_DonGia_New;
                ltt.TyGia = item.TyGia;
                ltt.MaLoaiTien = item.MaLoaiTien;
                SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
            }

            db.SubmitChanges();

            // Update chi tiết mặt bằng
            Library.Class.Connect.QueryConnect.QueryData<bool>("ct_PhuLuc_PhiDichVu",
                new
                {
                    MaHD = objPL.MaHD,
                    MaMB = item.MaMB,
                    TuNgay = item.TuNgay,
                    DenNgay = item.DenNgay,
                    DonGiaMoi = item.PQL_DonGia_New,
                    Type = 2,
                    MaPLCT = item.ID
                });
        }

        void DieuChinhTyGia(ctPhuLuc_ChiTiet item)
        {
            // Điều chỉnh lịch thanh toán
            var ltLTT = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                            & ( o.MaMB == item.MaMB | item.MaMB == null)
                                                            & !o.IsKhuyenMai.GetValueOrDefault());

            foreach (var ltt in ltLTT)
            {
                ltt.MaLoaiTien = item.MaLoaiTien;
                SchedulePaymentCls.UpdateTyGia(ltt, item.MaLoaiTien.Value ,item.TyGia.GetValueOrDefault());
            }
        }

        void DieuChinhDienTich(ctPhuLuc_ChiTiet item)
        {
            #region Backup chi tiết hợp đồng
            //var ChiTiet_HD = db.ctChiTiets.Where(o => o.MaHDCT == objHD.ID & o.MaMB == item.MaMB).ToList();
            //var ChiTiet_BK = db.ctChiTiet_BackUps.Where(o => o.MaHDCT == objHD.ID & o.MaMB == item.MaMB).ToList();

            //if (ChiTiet_BK.Count() <= 0)
            //{
            //    foreach (var add in ChiTiet_BK)
            //    {
            //        ctChiTiet_BackUp ct_ltt = new ctChiTiet_BackUp();
            //        LandSoftBuilding.Lease.GetData.InsertUpdate(add, ct_ltt);
            //        db.ctChiTiet_BackUps.InsertOnSubmit(ct_ltt);
            //    }
            //    db.SubmitChanges();
            //}

            //db.ctChiTiets.DeleteAllOnSubmit(ChiTiet_HD);
            //foreach (var add in ChiTiet_BK)
            //{
            //    ctChiTiet ct_ltt = new ctChiTiet();
            //    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ct_ltt);
            //    db.ctChiTiets.InsertOnSubmit(ct_ltt);
            //}
            //db.SubmitChanges();

            
            //// Cập nhật diện tích 
            //var HDchitiet = db.ctChiTiets.Where(o => o.MaHDCT == objHD.ID & o.MaMB == item.MaMB).FirstOrDefault();
            //HDchitiet.DienTich = item.DienTich;
            #endregion

            // Điều chỉnh lịch thanh toán
            var ltLTT = db.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & o.MaHD == objHD.ID
                                                            & !o.IsKhuyenMai.GetValueOrDefault());
            foreach (var ltt in ltLTT)
            {
                ltt.DienTich = item.DienTich;
                SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
            }
        }

        void ChuyenPhong(ctPhuLuc_ChiTiet item)
        {
            int? a = this.MaHD;
            int? b = item.MaMB;

            var ct_old = db.ctChiTiets.Where(o => o.MaHDCT == this.MaHD & o.MaMB == item.MaMB).FirstOrDefault();
            ct_old.DenNgay = item.TuNgay;
            ct_old.NgungSuDung = true;

            var ct = new ctChiTiet();
            ct.MaHDCT = this.MaHD;
            ct.MaMB = item.MaMB_new;
            ct.MaLG = item.Chuyen_MaLG;
            ct.DienTich = item.DienTich;
            ct.DonGia = item.Chuyen_DonGia;
            ct.TongGiaThue = item.Chuyen_GiaThue;
            ct.ThanhTien = item.Chuyen_GiaThue;
            ct.TuNgay = item.TuNgay;
            ct.DenNgay = item.DenNgay;
            db.ctChiTiets.InsertOnSubmit(ct);

            if (MaPL != null)
            {
                var ltt_bk = db.ctLichThanhToans.Where(o => o.MaHD == objPL.MaHD).ToList();
                foreach (var add in ltt_bk)
                {
                    ctLichThanhToan_BackUp ctbk = new ctLichThanhToan_BackUp();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                    ctbk.id_pl = objPL.MaPL;
                    db.ctLichThanhToan_BackUps.InsertOnSubmit(ctbk);
                    db.SubmitChanges();
                }
                var ltLTT_u = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                & o.MaMB == item.MaMB
                                                & !o.IsKhuyenMai.GetValueOrDefault());

                foreach (var ltt in ltLTT_u)
                {
                    ltt.MaMB = item.MaMB_new;

                    ltt.DonGia = item.DonGia;
                    ltt.MaLoaiTien = item.MaLoaiTien;
                    SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
                }
                db.SubmitChanges();

                return;

            }
            // Điều chỉnh lịch thanh toán
            var ltLTT = db.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & SqlMethods.DateDiffDay(o.DenNgay, item.DenNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & o.MaHD == objHD.ID
                                                            & !o.IsKhuyenMai.GetValueOrDefault());

            foreach (var ltt in ltLTT)
            {
                ltt.MaMB = item.MaMB_new;
                ltt.DonGia = item.Chuyen_DonGia;
                ltt.DienTich = item.DienTich;
                //ltt.TyLeVAT = 0;
                //ltt.TyLeCK = 0;
                SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
            }
        }

        void NgungSuDung(ctPhuLuc_ChiTiet item)
        {

            var ct = objHD.ctChiTiets.Single(o => o.MaMB == item.MaMB);
            {
                ct.NgungSuDung = true;
                ct.DenNgay = item.DenNgay;
            }

            // Điều chỉnh lịch thanh toán
            var ltLTT = db.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(item.TuNgay, o.TuNgay) >= 0
                                                            & o.MaMB == item.MaMB
                                                            & o.MaLDV == 2
                                                            & o.MaHD == objHD.ID);

            db.ctLichThanhToans.DeleteAllOnSubmit(ltLTT);
            db.SubmitChanges();
        }

        void DieuChinhPhapNhan(ctPhuLuc_ChiTiet item)
        {
            var model = new { MaKHMoi = item.MaKHMoi, MaHD = objHD.ID };
            var pram = new Dapper.DynamicParameters();
            pram.AddDynamicParams(model);
            Library.Class.Connect.QueryConnect.Query<string>("lease_phu_luc_dieu_chinh_phap_nhan", pram);
        }

        void DieuChinhTienCoc(ctPhuLuc_ChiTiet item)
        {
            decimal tyGia = 1;
            int MaLoaiTien = 1;

            if(item.MaLoaiTien != null)
            {
                tyGia = item.TyGia.GetValueOrDefault();
                MaLoaiTien = item.MaLoaiTien.GetValueOrDefault();
            }

            //var obj = db.ctLichThanhToans.Where(p => p.MaHD == MaHD && p.MaLDV==3 && p.DotTT == DotTT).Select(p => p).FirstOrDefault();
            //if(obj == null)
            //{
               var  obj = new ctLichThanhToan();
                obj.MaHD = MaHD;
                obj.MaLDV = 3;
                obj.DotTT = DotTT;
                obj.MaLoaiTien = 1;
                obj.MaMB = item.MaMB;
                obj.TyGia = tyGia;

                db.ctLichThanhToans.InsertOnSubmit(obj);
            //}

            var matBang = db.ctChiTiets.FirstOrDefault(_ => _.MaMB == item.MaMB & _.MaHDCT == MaHD);
            if(matBang == null)
            {
                return;
            }
            obj.MaHD = MaHD;
            obj.MaLDV = 3;
            obj.DotTT = DotTT;
            obj.MaLoaiTien = MaLoaiTien;
            obj.MaMB = item.MaMB;
            obj.TyGia = tyGia;
            // obj.DotTT = 
            obj.MaLDV = 3;
            obj.SoTien =  item.Coc_SoTien;
            obj.SoTienQD = item.Coc_SoTien * tyGia;
           obj.TuNgay = item.TuNgay;
            obj.DenNgay = item.DenNgay;
            obj.SoThang = Math.Round(Common.GetTotalMonth(item.TuNgay.Value, item.DenNgay.Value), 0, MidpointRounding.AwayFromZero);
            obj.NgayHHTT = item.DenNgay;
            obj.IsKhuyenMai = false;
            obj.DienTich = 1;
            obj.DonGia = item.Coc_SoTien;
            obj.GiaThue = item.Coc_SoTien;
            obj.NgungSuDung = false;
            obj.TyGia = tyGia;
            obj.LoaiTienNgoaiTe = 1;
            obj.TyGiaNgoaiTe = 1;
            obj.PhiDichVu = 0;
            obj.DienGiai = "Tiền đặt cọc";
            obj.Loai = "DATCOC";
            obj.NgayTao = System.DateTime.Now;
            obj.HanhDong = "PHULUCDATCOC";
            obj.ThanhTienNgoaiTe = item.Coc_SoTien;
            obj.TyLeVAT = 0;
            obj.TyLeCK = 0;


            db.SubmitChanges();

            var sumCocDotTT = db.ctLichThanhToans.Where(p => p.MaHD == MaHD && p.MaLDV==3).Select(p => p.SoTien).Sum();
            objHD.TienCoc = sumCocDotTT;
            db.SubmitChanges();



            //objHD.ctLichThanhToans.Add(obj);
        }

        void TaoLichThanhToanTienThue(ctPhuLuc_ChiTiet item)
        {
            if (item == null || item.MaLoaiPL == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại phụ lục]");
                return;
            }
            if (item.MaLoaiPL != 6) return;

            if (item.TuNgay == null | item.DenNgay == null)
            {
                DialogBox.Error("Vui lòng nhập [Từ ngày] - [Đến ngày]");
                return;
            }

            if(item.NgayTinhTronKyThanhToan == null)
            {
                item.NgayTinhTronKyThanhToan = item.TuNgay;
            }

            decimal? tyGiaCuoiCung = 0, phiDichVu = 0, KyTT = 0;

            var ltt_ct = db.ctLichThanhToans.Where(o => o.MaHD == objHD.ID).ToList();
            if (ltt_ct.Count() > 0)
            {
                tyGiaCuoiCung = ltt_ct.FirstOrDefault().TyGia;
                phiDichVu = ltt_ct.FirstOrDefault().PhiDichVu;

            }
            db.ctLichThanhToans.DeleteAllOnSubmit(ltt_ct);

            db.ctLichThanhToans.InsertAllOnSubmit(ctBackup);
            
            colPhong.GroupIndex = -1;
            gvLichThanhToan.FocusedRowHandle = -1;
            gvLichThanhToan.SelectAll();
            gvLichThanhToan.DeleteSelectedRows();

            var MaxDotTT = ctBackup.Max(o => o.DotTT).GetValueOrDefault();

            var chiTiet = (from ct in objHD.ctChiTiets
                           where !ct.NgungSuDung.GetValueOrDefault()
                                & ct.MaHDCT == objHD.ID
                           group ct by ct.MaMB into g
                           select new
                           {
                               MaMB = g.Key.Value
                           }).ToList();

            foreach (var mb in chiTiet)
            {
                //var _ltt = objHD.ctLichThanhToans.FirstOrDefault(o => o.DotTT == MaxDotTT && o.MaMB == mb.MaMB );

                var hdChiTiet = objHD.ctChiTiets.Where(ct => 
                                    ct.MaMB == mb.MaMB 
                                    & !ct.NgungSuDung.GetValueOrDefault() 
                                    & ct.MaHDCT == objHD.ID
                                    )
                                    .OrderByDescending(_ => _.DenNgay)
                                    .FirstOrDefault();

                if (hdChiTiet == null) continue;

                var _ltt = ctBackup.FirstOrDefault(o => o.DotTT == MaxDotTT && o.MaMB == mb.MaMB);

                
                int _DotTT = MaxDotTT;

                var _TuNgay = item.TuNgay.Value;

                while (_TuNgay.CompareTo(item.DenNgay.Value) < 0)
                {
                    _DotTT++;
                    decimal _KyTT = objHD.KyTT.Value;

                    var _DenNgay = _TuNgay.AddMonths((int)_KyTT).AddDays(-1);

                    if (_DenNgay.CompareTo(item.DenNgay.Value) > 0)
                    {
                        _DenNgay = item.DenNgay.Value;

                        _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                    }
                    try
                    {
                        // tách kỳ lẻ, ví dụ lần 1, từ ngày = 16/3, đến ngày = 16/6
                        if (item.NgayTinhTronKyThanhToan != null)
                        {
                            if (_TuNgay.Date <= ((DateTime)item.NgayTinhTronKyThanhToan).Date.AddDays(-1))
                                _DenNgay = ((DateTime)item.NgayTinhTronKyThanhToan).AddDays(-1);
                            _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                        }
                    }
                    catch { }
                    


                    if (_KyTT > 0)
                    {
                        decimal _TongTien = 0, _TongTienChuaTron = 0;

                        if(_KyTT != objHD.KyTT.Value)
                        {
                            //if(_TuNgay.Date.Day == 1)
                            //{
                            // Tính tiền kỳ lẻ,
                            // lấy tiền từng tháng cộng lại
                            // làm phần tính tiền trước, cái tháng lẻ, sẽ tách ra từng tháng lẻ
                            DateTime fromDate = _TuNgay;
                            DateTime toDate = _DenNgay;
                            // Kỳ thanh toán con
                            decimal? billingCycle = _KyTT, Ky = 0;
                            // Tiền thanh toán
                            decimal? pay = 0;

                            try
                            {
                                while (fromDate.CompareTo(_DenNgay) < 0)
                                {
                                    // tách ra từng tháng
                                    toDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                                    toDate = toDate.AddMonths(1);
                                    toDate = toDate.AddDays(-(toDate.Day));

                                    if (toDate.CompareTo(_DenNgay) > 0) toDate = _DenNgay;

                                    billingCycle = Common.GetTotalOneMonth(fromDate, toDate);
                                    Ky += billingCycle;

                                    pay += billingCycle * hdChiTiet.ThanhTien.GetValueOrDefault();

                                    fromDate = toDate.AddDays(1);
                                }

                                _TongTien = (decimal)pay;
                                _TongTienChuaTron = (decimal)pay;
                                _KyTT = (decimal)Ky;
                                //}
                                //else
                                //{
                                //    _TongTien = (decimal)mb.ThanhTien * _KyTT;
                                //}

                                
                            }
                            catch { }
                        }
                        else
                        {
                            _TongTien = _KyTT * hdChiTiet.ThanhTien.GetValueOrDefault();
                            _TongTienChuaTron = _TongTien;
                        }

                        _TongTien = Math.Round(((decimal)_TongTien), 2);


                        gvLichThanhToan.AddNewRow();
                        gvLichThanhToan.SetFocusedRowCellValue("MaHD", this.MaHD);
                        gvLichThanhToan.SetFocusedRowCellValue("DotTT", _DotTT);
                        gvLichThanhToan.SetFocusedRowCellValue("MaMB", mb.MaMB);
                        gvLichThanhToan.SetFocusedRowCellValue("MaLDV", 2);
                        gvLichThanhToan.SetFocusedRowCellValue("TuNgay", _TuNgay);
                        gvLichThanhToan.SetFocusedRowCellValue("DenNgay", _DenNgay);
                        gvLichThanhToan.SetFocusedRowCellValue("SoThang", _KyTT);
                        gvLichThanhToan.SetFocusedRowCellValue("DonGia", _ltt.DonGia);
                        gvLichThanhToan.SetFocusedRowCellValue("DienTich", _ltt.DienTich);
                        gvLichThanhToan.SetFocusedRowCellValue("MaLoaiTien", _ltt.MaLoaiTien);
                        gvLichThanhToan.SetFocusedRowCellValue("TyLeVAT", hdChiTiet.TyLeVAT);
                        gvLichThanhToan.SetFocusedRowCellValue("IsKhuyenMai", false);
                        gvLichThanhToan.SetFocusedRowCellValue("PhiDichVu", hdChiTiet.PhiDichVu);
                        gvLichThanhToan.SetFocusedRowCellValue("TyGia", tyGiaCuoiCung);
                        gvLichThanhToan.SetFocusedRowCellValue("TyLeCK", hdChiTiet.TyLeCK);
                        gvLichThanhToan.SetFocusedRowCellValue("DienGiai", string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay));

                        var ltt = (ctLichThanhToan)gvLichThanhToan.GetFocusedRow();
                        SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
                    }

                    _TuNgay = _DenNgay.AddDays(1);
                }
            }
            colPhong.GroupIndex = 0;
            gvLichThanhToan.FocusedRowHandle = -1;
            gvLichThanhToan.RefreshData();
        }

        void GetctLichThanhToan()
        {
            var ct_bk = db.ctLichThanhToan_BackUps.Where(o => o.MaHD == objHD.ID & o.id_pl == objPL.MaPL).ToList();
            if (ct_bk.Count() == 0)
            {
                //BackUp chi tiết lịch thanh toán   
                var ltt = db.ctLichThanhToans.Where(o => o.MaHD == objHD.ID).ToList();
                ctBackup = ltt;
            }
            else
            {
                foreach(var item in ct_bk)
                {
                    var itemLITT = new ctLichThanhToan();
                    LandSoftBuilding.Lease.GetData.InsertUpdate(item, itemLITT);
                    ctBackup.Add(itemLITT);
                }
            }
        }

        void TaoLichThanhToanTienThue_Them()
        {
           
            gvChiTiet.FocusedRowHandle = -1;

            colPhong.GroupIndex = -1;
            gvLTT_BoSung.FocusedRowHandle = -1;
            gvLTT_BoSung.SelectAll();
            gvLTT_BoSung.DeleteSelectedRows();

            foreach (var mb in objHD.ctChiTiets.Where(o=>o.ID == null | o.ID == 0))
            {
                var MaxDotTT = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(o.TuNgay, mb.TuNgay) >= 0 & SqlMethods.DateDiffDay(mb.TuNgay, o.DenNgay) >= 0).Max(o => o.DotTT).GetValueOrDefault();

                //var _ltt = objHD.ctLichThanhToans.Single(o => o.DotTT == MaxDotTT & o.MaMB == mb.MaMB);

                if (mb.TuNgay == null | mb.DenNgay == null)
                {
                    DialogBox.Error("Vui lòng nhập đầy đủ thông tin để tạo lịch");

                    colPhong.GroupIndex = -1;
                    gvLTT_BoSung.FocusedRowHandle = -1;
                    gvLTT_BoSung.SelectAll();
                    gvLTT_BoSung.DeleteSelectedRows();
                    colPhong.GroupIndex = 0;

                    return;
                }

                DateTime? NgayBDTao = mb.NgayBDTaoLTT;

                int _DotTT = MaxDotTT > 0 ?  MaxDotTT - 1: MaxDotTT;

                var _TuNgay = mb.TuNgay.Value;
                while (_TuNgay.CompareTo(mb.DenNgay.Value) < 0)
                {
                    _DotTT++;
                    decimal _KyTT = objHD.KyTT.Value;

                    var _DenNgay = _TuNgay.AddMonths((int)_KyTT).AddDays(-1);
                    if (NgayBDTao != null && SqlMethods.DateDiffDay(_TuNgay, NgayBDTao) > 0)
                    {
                        _KyTT = Common.GetTotalMonth(_TuNgay, NgayBDTao.Value.AddDays(-1));
                        _DenNgay = NgayBDTao.Value.AddDays(-1);
                        NgayBDTao = null;
                    }
                    else
                    {
                        if (_DenNgay.CompareTo(mb.DenNgay.Value) > 0)
                        {
                            _DenNgay = mb.DenNgay.Value;

                            _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                        }
                    }

                    if (_KyTT > 0)
                    {
                        gvLTT_BoSung.AddNewRow();
                        gvLTT_BoSung.SetFocusedRowCellValue("MaHD", this.MaHD);
                        gvLTT_BoSung.SetFocusedRowCellValue("DotTT", _DotTT);
                        gvLTT_BoSung.SetFocusedRowCellValue("MaMB", mb.MaMB);
                        gvLTT_BoSung.SetFocusedRowCellValue("MaLDV", 2);
                        gvLTT_BoSung.SetFocusedRowCellValue("TuNgay", _TuNgay);
                        gvLTT_BoSung.SetFocusedRowCellValue("DenNgay", _DenNgay);
                        gvLTT_BoSung.SetFocusedRowCellValue("SoThang", _KyTT);
                        gvLTT_BoSung.SetFocusedRowCellValue("DonGia", mb.DonGia);
                        gvLTT_BoSung.SetFocusedRowCellValue("DienTich", mb.DienTich);
                        gvLTT_BoSung.SetFocusedRowCellValue("TyLeVAT", mb.TyLeVAT);
                        gvLTT_BoSung.SetFocusedRowCellValue("GiaThue", mb.ThanhTien);
                        gvLTT_BoSung.SetFocusedRowCellValue("IsKhuyenMai", false);
                        gvLTT_BoSung.SetFocusedRowCellValue("TyLeCK", mb.TyLeCK);
                        gvLTT_BoSung.SetFocusedRowCellValue("MaLoaiTien", mb.MaLoaiTien == null ? 1 : mb.MaLoaiTien);
                        gvLTT_BoSung.SetFocusedRowCellValue("DienGiai", string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay));

                        var ltt = (ctLichThanhToan)gvLTT_BoSung.GetFocusedRow();

                        SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
                    }
                    _TuNgay = _DenNgay.AddDays(1);
                }
            }
            colPhong.GroupIndex = 0;
            gvLTT_BoSung.FocusedRowHandle = -1;
            gvLTT_BoSung.RefreshData();
        }

        void GiaHan(ctPhuLuc_ChiTiet item)
        {

            objHD.NgayHH = item.DenNgay;
            objHD.ThoiHan = Math.Round(Common.GetTotalMonth(objHD.NgayHL.Value, objHD.NgayHH.Value), 0, MidpointRounding.AwayFromZero);

            var chiTiet = (from ct in objHD.ctChiTiets
                           group ct by ct.MaMB
                               into g
                               select new
                               {
                                   MaMB = g.Key.Value
                               }).ToList();

            foreach (var ct in chiTiet)
            {
                var hdChiTiet = objHD.ctChiTiets.Where(_ => _.MaMB == ct.MaMB & !_.NgungSuDung.GetValueOrDefault()).OrderByDescending(_ => _.DenNgay).FirstOrDefault();
                if (hdChiTiet != null)
                {
                    hdChiTiet.DenNgay = item.DenNgay;
                }
                //ct.DenNgay = item.DenNgay; 
            }
                         
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLSDCGEdit_Load(object sender, EventArgs e)
        {
            objHD = db.ctHopDongs.Single(o => o.ID == this.MaHD);
            objPL = db.ctPhuLucs.SingleOrDefault(o => o.MaPL == this.MaPL);

            //Load lao gia thue
            lkLoaiGia.DataSource = lkLoaiGia_Them.DataSource = from lg in db.LoaiGiaThues
                                                               join lt in db.LoaiTiens on lg.MaLT equals lt.ID
                                                               where lg.MaTN == objHD.MaTN
                                                               orderby lg.TenLG
                                                               select new { lg.ID, lg.TenLG, DonGia = lg.DonGia * lt.TyGia };

            glkMatBang.DataSource = glMatBang.DataSource = from mb in db.mbMatBangs
                                                           join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                                           join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                                           where k.MaTN == objHD.MaTN
                                                           orderby mb.MaSoMB
                                                           select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich };

            lkMatBang.DataSource = //db.ctChiTiets.Where(o => o.MaHDCT == this.MaHD & !o.NgungSuDung.GetValueOrDefault()).Select(o => new { o.MaMB, o.mbMatBang.MaSoMB });
                                          (from ct in db.ctChiTiets
                                           where ct.MaHDCT == this.MaHD
                                           & !ct.NgungSuDung.GetValueOrDefault()
                                           group ct by new { ct.MaMB, ct.mbMatBang.MaSoMB } into gr
                                           select new
                                           {
                                               MaMB = gr.Key.MaMB,
                                               MaSoMB = gr.Key.MaSoMB,
                                           }).ToList();


            lkLoaiTien.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();
            lkLoaiTien_GiaHan.DataSource = lkLoaiTien_Them.DataSource = lkLoaiTien.DataSource;

            lkLoaiPL.DataSource = db.ctLoaiPhuLucs.Where(o=> o.MaLoai!=7 && o.MaLoai!=8);
            glkPhapNhanHienTai.DataSource = from kh in db.tnKhachHangs where kh.MaKH == objHD.MaKH select new { kh.MaKH, kh.KyHieu, TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen, DiaChi = kh.IsCaNhan == true? kh.DCLL : kh.CtyDiaChi };

            glkPhapNhanMoi.DataSource = from kh in db.tnKhachHangs where kh.MaTN == objHD.MaTN & kh.MaKH != objHD.MaKH select new { kh.MaKH, kh.KyHieu, TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen, DiaChi = kh.IsCaNhan == true ? kh.DCLL : kh.CtyDiaChi };
            var loaiGiamGia = new List<LoaiPhuLuc>()
            {
                new LoaiPhuLuc()
                {
                    ID = 1,
                    TenLoai = "Giảm theo tỉ lệ %",
                },
                new LoaiPhuLuc()
                {
                    ID = 2,
                    TenLoai = "Giảm theo số tiền",
                }
            };
            lkuLoaiGiamGia.DataSource = loaiGiamGia;
            var loaiDichVu = new List<LoaiPhuLuc>()
            {
                new LoaiPhuLuc()
                {
                    ID = 1,
                    TenLoai = "Giá thuê",
                },
                new LoaiPhuLuc()
                {
                    ID = 2,
                    TenLoai = "Phí quản lý",
                },
                new LoaiPhuLuc()
                {
                    ID = 3,
                    TenLoai = "Phí điều hòa chiếu sáng",
                }
            };
            lkLoaiDichVu.DataSource = loaiDichVu;
            if (objPL == null)
            {
                //txtSoPL.EditValue = LeaseScheduleCls.TaoSoCT_PhuLuc(MaHD);
                dateNgayDC.EditValue = DateTime.Now;

                objPL = new ctPhuLuc();
                objPL.LoaiPL = (int)PhuLuc.PhuLucCls.LoaiPhuLuc.DieuChinhDienTich;
                objPL.MaHD = MaHD;
                objPL.MaNVN = Common.User.MaNV;
                objPL.NgayNhap = db.GetSystemDate();
                objPL.NgayPL = db.GetSystemDate();

                db.ctPhuLucs.InsertOnSubmit(objPL);
            }

            gcPhuLuc.DataSource = objPL.ctPhuLuc_ChiTiets;

            gcLTT.DataSource = db.ctLichThanhToans.Where(o=>o.MaHD == -1);

            gcLTT_BoSung.DataSource = db.ctLichThanhToans.Where(o => o.MaHD == -1);

            gcChiTiet.DataSource = objHD.ctChiTiets;

            txtSoPL.Text = objPL.SoPL;
            dateNgayDC.EditValue = objPL.NgayPL;
            GetctLichThanhToan();
        }

        void GetGiaThue()
        {
            List<decimal?> ltGiaThue = new List<decimal?>();

            for (int i = 0; i < gvLTT.RowCount; i++)
            {
                if ((bool?)gvLTT.GetRowCellValue(i, "IsCheck") == true)
                    ltGiaThue.Add((decimal?)gvLTT.GetRowCellValue(i, "DonGia"));
            }

            var giaThue = ltGiaThue.Distinct();

        }

        private void lkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void spTyLeDC_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void gvLTT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle >= 0 & (bool?)view.GetRowCellValue(e.RowHandle, "IsCheck") == true)
            {
                 e.Appearance.BackColor = Color.NavajoWhite;
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void dateDenNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void dateTuNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void cmbMatBang_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void lkLoaiGia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gvPhuLuc.SetFocusedRowCellValue("Chuyen_DonGia", (decimal)(sender as LookUpEdit).GetColumnValue("DonGia"));
                TinhThanhTien();
            }
            catch { }
        }
        private void lkuLoaiGiamGia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkLGG = (sender as LookUpEdit);
                var _MaLGG = (int)lkLGG.EditValue;
                if (_MaLGG == 1)
                {
                    spPhanTramGiamGia.ReadOnly = false;
                    spSoTienGiam.ReadOnly = true;
                    gvPhuLuc.SetFocusedRowCellValue("SoTienGiam", 0);
                    gvPhuLuc.SetFocusedRowCellValue("MaLoaiGiamGia", 1);

                }
                else if (_MaLGG == 2) {
                    spPhanTramGiamGia.ReadOnly = true;
                    spSoTienGiam.ReadOnly = false;
                    gvPhuLuc.SetFocusedRowCellValue("PhanTramGiamGia", 0);
                    gvPhuLuc.SetFocusedRowCellValue("MaLoaiGiamGia", 2);
                }
            }
            catch { }
        }
        private void spSoTienGiam_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var soTien = (sender as SpinEdit);
                var _SoTien = (decimal)soTien.EditValue;
                
            }
            catch { }
        }

        void TinhThanhTien()
        {
            try
            {
                var _DienTich = (gvPhuLuc.GetFocusedRowCellValue("Chuyen_DienTich") as decimal?) ?? 0;
                var _DonGia = (gvPhuLuc.GetFocusedRowCellValue("Chuyen_DonGia") as decimal?) ?? 0;
                var _TongGiaThue = (gvPhuLuc.GetFocusedRowCellValue("Chuyen_GiaThue") as decimal?) ?? 0;

                _TongGiaThue = _DienTich * (_DonGia);

                gvPhuLuc.SetFocusedRowCellValue("Chuyen_GiaThue", _TongGiaThue);
            }
            catch { }
        }

        private void btnTaoLich_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                var item = (ctPhuLuc_ChiTiet)gvPhuLuc.GetFocusedRow();
                TaoLichThanhToanTienThue(item);
            }
        }

        private void spDieuChinhGia_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void itemTaoLTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TaoLichThanhToanTienThue_Them();
        }

        private void lkLoaiGia_Them_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gvChiTiet.SetFocusedRowCellValue("DonGia", (decimal)(sender as LookUpEdit).GetColumnValue("DonGia"));
                TinhThanhTien_Them();
            }
            catch { }
        }
         

        void TinhThanhTien_Them()
        {
            try
            {
                var _DienTich = (gvChiTiet.GetFocusedRowCellValue("DienTich") as decimal?) ?? 0;
                var _DonGia = (gvChiTiet.GetFocusedRowCellValue("DonGia") as decimal?) ?? 0;
                var _PhiDichVu = (gvChiTiet.GetFocusedRowCellValue("PhiDichVu") as decimal?) ?? 0;
                var _TyLeVAT = (gvChiTiet.GetFocusedRowCellValue("TyLeVAT") as decimal?) ?? 0;
                var _TyLeCK = (gvChiTiet.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                var _TienCK = (gvChiTiet.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;
                var _TongGiaThue = (gvChiTiet.GetFocusedRowCellValue("TongGiaThue") as decimal?) ?? 0;

                _TongGiaThue = _DienTich * (_DonGia + _PhiDichVu);

                //if (objHD.IsLamTron.GetValueOrDefault())
                //{
                //    _TongGiaThue = SchedulePaymentCls.LamTron(_TongGiaThue);
                //}

                if (_TongGiaThue > 0)
                {
                    if (_TyLeCK > 0)
                        _TienCK = _TongGiaThue * _TyLeCK;
                    else
                        _TyLeCK = _TienCK / _TongGiaThue;
                }

                var _TienVAT = _TongGiaThue * _TyLeVAT;


                var _ThanhTien = _TongGiaThue + _TienVAT - _TienCK;

                gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                gvChiTiet.SetFocusedRowCellValue("TongGiaThue", _TongGiaThue);
                gvChiTiet.SetFocusedRowCellValue("TienVAT", _TienVAT);
                gvChiTiet.SetFocusedRowCellValue("TyLeCK", _TyLeCK);
                gvChiTiet.SetFocusedRowCellValue("TienCK", _TienCK);
                gvChiTiet.SetFocusedRowCellValue("ThanhTien", _ThanhTien);
            }
            catch { }
        }

        private void spDonGia_EditValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                gvChiTiet.SetFocusedRowCellValue("DonGia", ((SpinEdit)sender).Value);
                TinhThanhTien_Them();
            }
            catch { }
        }

        private void spTyLeCK_LTT_EditValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                SpinEdit sp = sender as SpinEdit;
                var ThanhTien = (decimal?)gvLTT_BoSung.GetFocusedRowCellValue("ThanhTien");
                gvLTT_BoSung.SetFocusedRowCellValue("TienCK", ThanhTien * sp.Value);
                gvLTT_BoSung.SetFocusedRowCellValue("SoTien", ThanhTien * (1 - sp.Value));
                gvLTT_BoSung.SetFocusedRowCellValue("SoTienQD", ThanhTien * (1 - sp.Value));
            }
            catch { }
        }

        private void spTienCK_LTT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SpinEdit sp = sender as SpinEdit;
                var ThanhTien = (decimal?)gvLTT_BoSung.GetFocusedRowCellValue("ThanhTien");
                gvLTT_BoSung.SetFocusedRowCellValue("TyLeCK", sp.Value / (decimal)ThanhTien.GetValueOrDefault());
                gvLTT_BoSung.SetFocusedRowCellValue("TienCK", sp.Value);
                gvLTT_BoSung.SetFocusedRowCellValue("SoTien", ThanhTien - sp.Value);
                gvLTT_BoSung.SetFocusedRowCellValue("SoTienQD", ThanhTien - sp.Value);
            }
            catch { }
            
        }

        private void glMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkMB = (sender as GridLookUpEdit);
                var _MaMB = (int)lkMB.EditValue;

                var r = lkMB.Properties.GetRowByKeyValue(lkMB.EditValue);
                var type = r.GetType();
                gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                gvChiTiet.SetFocusedRowCellValue("DenNgay", objHD.NgayHH);

                if (r != null)
                {
                    var ltGiaThue = (from g in db.mbGiaThues
                                     join mb in db.mbMatBangs on g.MaMB equals mb.MaMB
                                     join lt in db.LoaiTiens on mb.MaLT equals lt.ID
                                     where g.MaMB == _MaMB
                                     select new { g.MaLG, DonGia = g.DonGia * lt.TyGia, g.DienTich }).ToList();
                    if (ltGiaThue.Count() > 0)
                    {
                        //Update du lieu cho dong hien tai
                        foreach (var g in ltGiaThue)
                        {
                            if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG).Count() == 0)
                            {
                                var _DonGia = g.DonGia;
                                gvChiTiet.SetFocusedRowCellValue("MaMB", _MaMB);
                                gvChiTiet.SetFocusedRowCellValue("MaLG", g.MaLG);
                                gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                                gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                                gvChiTiet.SetFocusedRowCellValue("ThanhTien", g.DienTich * _DonGia);
                                gvChiTiet.UpdateCurrentRow();
                                break;
                            }
                        }

                        //Them du lieu neu chua co
                        foreach (var g in ltGiaThue)
                        {
                            if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG).Count() == 0)
                            {
                                var objCT = new ctChiTiet();
                                objCT.MaMB = _MaMB;
                                objCT.MaLG = g.MaLG;
                                objCT.DonGia = g.DonGia;
                                objCT.DienTich = g.DienTich;
                                objCT.ThanhTien = objCT.DonGia * objCT.DienTich;
                                objHD.ctChiTiets.Add(objCT);
                            }
                        }
                    }

                    TinhThanhTien_Them();
                }
            }
            catch { }

        }

        private void spDienTich_EditValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                gvChiTiet.SetFocusedRowCellValue("DienTich", ((SpinEdit)sender).Value);
                TinhThanhTien_Them();
            }
            catch { }
        }

        private void spTienCK_GiaHan_EditValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                SpinEdit sp = sender as SpinEdit;
                var ThanhTien = (decimal?)gvLichThanhToan.GetFocusedRowCellValue("ThanhTien");
                gvLichThanhToan.SetFocusedRowCellValue("TyLeCK", sp.Value / (decimal)ThanhTien.GetValueOrDefault());
                gvLichThanhToan.SetFocusedRowCellValue("TienCK", sp.Value);
                gvLichThanhToan.SetFocusedRowCellValue("SoTien", ThanhTien - sp.Value);
                gvLichThanhToan.SetFocusedRowCellValue("SoTienQD", ThanhTien - sp.Value);
            }
            catch { }
        }

        private void spTyLeCK_GiaHan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SpinEdit sp = sender as SpinEdit;
                var ThanhTien = (decimal?)gvLichThanhToan.GetFocusedRowCellValue("ThanhTien");
                gvLichThanhToan.SetFocusedRowCellValue("TienCK", ThanhTien * sp.Value);
                gvLichThanhToan.SetFocusedRowCellValue("SoTien", ThanhTien * (1 - sp.Value));
                gvLichThanhToan.SetFocusedRowCellValue("SoTienQD", ThanhTien * (1 - sp.Value));
            }
            catch { }
            
        }

        private void ckKhuyenMai_GiaHan_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = sender as CheckEdit;
            try
            {
                if (ckb.Checked)
                {
                    gvLichThanhToan.SetFocusedRowCellValue("TyLeCK", 1);
                    gvLichThanhToan.SetFocusedRowCellValue("TienCK", gvLichThanhToan.GetFocusedRowCellValue("ThanhTien"));
                    gvLichThanhToan.SetFocusedRowCellValue("SoTien", 0);
                    gvLichThanhToan.SetFocusedRowCellValue("SoTienQD", 0);
                }
                else
                {
                    gvLichThanhToan.SetFocusedRowCellValue("TyLeCK", 0);
                    gvLichThanhToan.SetFocusedRowCellValue("TienCK", 0);
                    gvLichThanhToan.SetFocusedRowCellValue("SoTien", gvLichThanhToan.GetFocusedRowCellValue("ThanhTien"));
                    gvLichThanhToan.SetFocusedRowCellValue("SoTienQD", gvLichThanhToan.GetFocusedRowCellValue("ThanhTien"));
                }
            }
            catch
            {
            }
        }

        private void ckKhuyenMai_BoSung_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = sender as CheckEdit;
            try
            {
                if (ckb.Checked)
                {
                    gvLTT_BoSung.SetFocusedRowCellValue("TyLeCK", 1);
                    gvLTT_BoSung.SetFocusedRowCellValue("TienCK", gvLTT_BoSung.GetFocusedRowCellValue("ThanhTien"));
                    gvLTT_BoSung.SetFocusedRowCellValue("SoTien", 0);
                    gvLTT_BoSung.SetFocusedRowCellValue("SoTienQD", 0);
                }
                else
                {
                    gvLTT_BoSung.SetFocusedRowCellValue("TyLeCK", 0);
                    gvLTT_BoSung.SetFocusedRowCellValue("TienCK", 0);
                    gvLTT_BoSung.SetFocusedRowCellValue("SoTien", gvLTT_BoSung.GetFocusedRowCellValue("ThanhTien"));
                    gvLTT_BoSung.SetFocusedRowCellValue("SoTienQD", gvLTT_BoSung.GetFocusedRowCellValue("ThanhTien"));
                }
            }
            catch
            {
            }
        }

        private void glMatBang__PhuLuc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkMB = (sender as GridLookUpEdit);
                var _MaMB = (int)lkMB.EditValue;

                var r = lkMB.Properties.GetRowByKeyValue(lkMB.EditValue);
                var type = r.GetType();
                gvPhuLuc.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                gvPhuLuc.SetFocusedRowCellValue("Chuyen_DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                gvPhuLuc.SetFocusedRowCellValue("DenNgay", objHD.NgayHH);

                if (r != null)
                {
                    var ltGiaThue = (from g in db.mbGiaThues
                                     join mb in db.mbMatBangs on g.MaMB equals mb.MaMB
                                     join lt in db.LoaiTiens on mb.MaLT equals lt.ID
                                     where g.MaMB == _MaMB
                                     select new { g.MaLG, DonGia = g.DonGia * lt.TyGia, g.DienTich }).ToList();

                    if (ltGiaThue.Count() > 0)
                    {
                        //Update du lieu cho dong hien tai
                        var g = ltGiaThue.First();
                        var _DonGia = g.DonGia;
                        var _ThanhTien = g.DienTich * _DonGia;

                        //if (objHD.IsLamTron.GetValueOrDefault())
                        //    _ThanhTien = _ThanhTien.GetValueOrDefault().LamTron();

                        gvPhuLuc.SetFocusedRowCellValue("MaMB_new", _MaMB);
                        gvPhuLuc.SetFocusedRowCellValue("Chuyen_MaLG", g.MaLG);
                        gvPhuLuc.SetFocusedRowCellValue("Chuyen_DonGia", _DonGia);
                        gvPhuLuc.SetFocusedRowCellValue("Chuyen_DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                        gvPhuLuc.SetFocusedRowCellValue("Chuyen_GiaThue", _ThanhTien);
                        gvPhuLuc.UpdateCurrentRow();
                    }
                }
            }
            catch { }

        }

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lk = sender as LookUpEdit;
                gvPhuLuc.SetFocusedRowCellValue("TyGia", lk.GetColumnValue("TyGia"));
            }
            catch { }
            
        }

        private void lkLoaiPL_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as LookUpEdit;
            if (item != null)
            {
                // Phụ lục thay đổi pháp nhân
                // tiến hành khóa hoặc ẩn các cột khác
                var idPhuLuc = (int)item.EditValue;
                if (idPhuLuc == 10)
                {
                    gvPhuLuc.SetFocusedRowCellValue("MaKH", objHD.MaKH);
                }

            }
        }


    }
}