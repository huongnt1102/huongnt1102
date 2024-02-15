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

namespace LandSoftBuilding.Lease.Liquidate
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public int? MaHD { get; set; }
        public byte? MaTN { get; set; }
        private bool isSelectAll = false;
        private int? groupHandle = null;
        ctHopDong HopDong;
        tnToaNha toaNha;
        MasterDataContext db = new MasterDataContext();
        ctThanhLy objTL;
        List<HoaDonItem> ltData;
        List<int> ListMB = new List<int>();
        List<string> ListHD = new List<string>();

        private void frmEdit_Load(object sender, EventArgs e)
        {
            //ckbCompanyCode.Properties.DataSource = db.CompanyCodes;
            //glkCompanyCode.Properties.DataSource = db.CompanyCodes;

            HopDong = db.ctHopDongs.Where(x => x.ID == this.MaHD).FirstOrDefault();
            toaNha = db.tnToaNhas.Where(x => x.MaTN == this.MaTN).FirstOrDefault();
            foreach (var item in HopDong.ctChiTiets.GroupBy(x=> x.MaMB).Select(x=> x.Key.Value).ToList())
            {
                ListMB.Add(item);
            }

            var listHopDong = (from hd in db.ctHopDongs
                               where hd.ID != HopDong.ID && hd.MaKH == HopDong.MaKH && hd.MaTN == MaTN && (db.ctThanhLies.Where(x=> x.MaHD ==hd.ID).FirstOrDefault() == null)
                               select new
                               {
                                   hd.ID,
                                   hd.SoHDCT
                               }).ToList();
            ccbxHopDong.Properties.DataSource = listHopDong;
            TranslateLanguage.TranslateControl(this);

            lkLoaiTien.Properties.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();
            var tongTienCocDaThu = (from pt in db.SoQuy_ThuChis
                                    join hd in db.dvHoaDons on pt.LinkID equals (long?)hd.ID into hoadon
                                    from hd in hoadon.DefaultIfEmpty()
                                    where pt.MaKH == HopDong.MaKH && ListMB.Contains(pt.MaMB.Value)&& (pt.MaLoaiPhieu == 49 || (pt.MaLoaiPhieu == 1 && pt.TableName == "dvHoaDon" && hd.MaLDV == 3 && hd.TableName == "ctLichThanhToan"))
                                    select new
                                    {
                                        pt.DaThu
                                    }).Sum(x=> x.DaThu);
            var tongTienCocDaChuyenThu = (from pt in db.SoQuy_ThuChis
                                          join ptt in db.ptPhieuThus on pt.IDPhieu equals ptt.ID into phieuthu
                                          from ptt in phieuthu.DefaultIfEmpty()
                                          where pt.MaKH == HopDong.MaKH && ptt.IdHopDongChuyenCoc == HopDong.ID && (pt.MaLoaiPhieu == 52)
                                          select new
                                          {
                                              DaThu = -pt.DaThu,
                                          }).Sum(x => x.DaThu);
            spTienTL.EditValue = tongTienCocDaThu - (tongTienCocDaChuyenThu >= 0 ? tongTienCocDaChuyenThu : 0);
            if (this.ID != null)
            {
                objTL = db.ctThanhLies.Single(p => p.ID == this.ID);
                txtSoTL.EditValue = objTL.SoTL;
                dateNgayTL.EditValue = objTL.NgayTL;
                spTienTL.EditValue = objTL.TienTL;
                lkLoaiTien.EditValue = objTL.MaLT;
                spTyGia.EditValue = objTL.TyGia;
                spTienTLQD.EditValue = objTL.TienTLQD;
                txtDienGiai.EditValue = objTL.LyDo;
                objTL.MaNVS = Common.User.MaNV;
                objTL.NgaySua = db.GetSystemDate();
            }
            else
            {
                //objTL = new ctThanhLy();
                //objTL.MaTN = this.MaTN;
                //objTL.MaHD = this.MaHD;
                //objTL.DaThu = 0;
                //objTL.DaTra = 0;
                //objTL.NgayNhap = db.GetSystemDate();
                //objTL.MaNVN = Common.User.MaNV;
                //db.ctThanhLies.InsertOnSubmit(objTL);

                txtSoTL.EditValue = db.CreateSoChungTu(40, this.MaTN);
                dateNgayTL.EditValue = db.GetSystemDate();
                lkLoaiTien.ItemIndex = 0;
            }
            LoadData();
        }

        private void ChuyenTTMatBang(ctHopDong hopDong)
        {
            try
            {
                var chiTiet = db.ctChiTiets.Where(p => p.MaHDCT == hopDong.ID).ToList();
                if (chiTiet.Count() > 0)
                {
                    foreach (var item in chiTiet)
                    {
                        var itemMB = db.mbMatBangs.FirstOrDefault(p => p.MaMB == item.MaMB);
                        itemMB.MaTT = 84;
                        itemMB.MaKHSauTL = hopDong.MaKH;
                        itemMB.MaKH = null;
                        itemMB.MaKHF1 = null;
                        db.SubmitChanges();

                    }

                }
                
            }
            catch {}

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (txtSoTL.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập số thanh lý");
                txtSoTL.Focus();
                return;
            }
            if (spTienHoaDon.Value > spTienTLQD.Value)
            {
                DialogBox.Alert("Vui lòng chọn lại hóa đơn, số tiền hóa đơn gạch nợ phải nhỏ hơn số tiền thanh lý");
                spTienTLQD.Focus();
                return;
            }
            if (dateNgayTL.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập ngày thanh lý");
                txtSoTL.Focus();
                return;
            }
            #endregion

            try
            {
                //var strCompany = (ckbCompanyCode.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                //if (strCompany == "") { Library.DialogBox.Alert("Vui lòng chọn mặt bằng"); return; }


                var hoaDonKhauTrus = ltData.Where(x => x.IsChon == true).ToList();
                var tienHoaDOn = (decimal?)spTienHoaDon.EditValue;
                var giaTriThanhLy = (decimal?)spTienTL.EditValue;
                var hopDongKhauTru = new List<HopDongKhauTru>();
                foreach (var item in ListHD)
                {
                    if (this.ID == null)
                    {
                        var hopDong = db.ctHopDongs.Where(x => x.ID.ToString() == item.Trim()).FirstOrDefault();
                        if (hopDong != null)
                        {

                            ChuyenTTMatBang(hopDong);
                            db.SubmitChanges();
                            var matBangs = new List<int>();

                            foreach (var chitiet in hopDong.ctChiTiets.GroupBy(x => x.MaMB).Select(x => x.Key.Value).ToList())
                            {
                                matBangs.Add(chitiet);
                            }
                            var tongTienCocDaThu = (from pt in db.SoQuy_ThuChis
                                                    join hd in db.dvHoaDons on pt.LinkID equals (long?)hd.ID into hoadon
                                                    from hd in hoadon.DefaultIfEmpty()
                                                    where pt.MaKH == hopDong.MaKH && matBangs.Contains(pt.MaMB.Value) && (pt.MaLoaiPhieu == 49 || (pt.MaLoaiPhieu == 1 && pt.TableName == "dvHoaDon" && hd.MaLDV == 3 && hd.TableName == "ctLichThanhToan"))
                                                    select new
                                                    {
                                                        pt.DaThu
                                                    }).Sum(x => x.DaThu);
                            var s = (from pt in db.SoQuy_ThuChis
                                     join ptt in db.ptPhieuThus on pt.IDPhieu equals ptt.ID
                                     where pt.MaKH == hopDong.MaKH && ptt.IdHopDongChuyenCoc == hopDong.ID && (pt.MaLoaiPhieu == 52)
                                     select new
                                     {
                                         DaThu = pt.DaThu,
                                     }).ToList();
                            var tongTienCocDaChuyenThu = s.Count() > 0 ? s.Sum(x => x.DaThu.GetValueOrDefault()) : 0;
                            var gtThanhLy = tongTienCocDaThu + tongTienCocDaChuyenThu;
                            var khachHang = db.tnKhachHangs.Where(x => x.MaKH == hopDong.MaKH).FirstOrDefault();
                            var matBang = db.mbMatBangs.Where(x => x.MaKH == khachHang.MaKH).FirstOrDefault();
                            // tạo các phiếu thu, chi
                            //2023-12-11 Quang
                            // phiếu chi 

                            // phiếu thu âm
                            #region phiếu thu âm
                            var dienGiaiThuAm = "Thu âm cho khách hàng thanh lý hợp đồng: " + hopDong.SoHDCT;
                            var soPhieuThuAm = Common.GetPayNumber(0, MaTN, null);
                            ptPhieuThu _objPtam = new ptPhieuThu();
                            _objPtam.MaTN = this.MaTN;
                            _objPtam.SoPT = soPhieuThuAm;
                            _objPtam.NgayThu = DateTime.Now;
                            _objPtam.SoTien = -gtThanhLy;
                            _objPtam.MaNV = Common.User.MaNV;
                            _objPtam.MaPL = 47;
                            _objPtam.IsThanhLy = true;
                            _objPtam.IDHopDongTL = hopDong.ID;
                            _objPtam.MaNVN = Library.Common.User.MaNV;
                            _objPtam.NgayNhap = DateTime.UtcNow.AddHours(7);
                            _objPtam.NguoiNop = khachHang.TenKH;
                            _objPtam.MaKH = khachHang.MaKH;
                            _objPtam.MaMB = matBang != null ? (int?)matBang.MaMB : null;
                            _objPtam.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
                            _objPtam.MaTKNH = null;
                            _objPtam.MaHTHT = 1;
                            _objPtam.HinhThucThanhToanId = 1;
                            _objPtam.HinhThucThanhToanName = "Tiền mặt";
                            _objPtam.LyDo = dienGiaiThuAm;
                            _objPtam.IsKhauTru = false;
                            db.ptPhieuThus.InsertOnSubmit(_objPtam);
                            var ctpt = new ptChiTietPhieuThu();
                            ctpt.LinkID = null;
                            ctpt.DienGiai = dienGiaiThuAm;
                            ctpt.SoTien = -gtThanhLy;
                            ctpt.ThuThua = 0;
                            ctpt.KhauTru = 0;
                            ctpt.PhaiThu = 0;
                            _objPtam.ptChiTietPhieuThus.Add(ctpt);
                            db.SubmitChanges();
                            Common.SoQuy_Insert(db, DateTime.Now.Month, DateTime.Now.Year, this.MaTN, hopDong.MaKH, null, _objPtam.ID, ctpt.ID, DateTime.Now, soPhieuThuAm, 0, 47, true, 0, -gtThanhLy, 0, 0, null, null, dienGiaiThuAm, Common.User.MaNV, false, false);
                            #endregion kết thúc phiếu thu âm
                            // phiếu thu trước
                            #region phiếu thu trước
                            var dienGiaiThuTruoc = "Thu trước cho khách hàng thanh lý hợp đồng" + hopDong.SoHDCT;
                            var soPhieuThuTruoc = Common.GetPayNumber(0, MaTN, null);
                            ptPhieuThu _objPTTruoc = new ptPhieuThu();
                            _objPTTruoc.MaTN = this.MaTN;
                            _objPTTruoc.SoPT = soPhieuThuTruoc;
                            _objPTTruoc.NgayThu = DateTime.Now;
                            _objPTTruoc.SoTien = gtThanhLy;
                            _objPTTruoc.MaNV = Common.User.MaNV;
                            _objPTTruoc.MaPL = 2;
                            _objPTTruoc.IsThanhLy = true;
                            _objPTTruoc.IDHopDongTL = hopDong.ID;
                            _objPTTruoc.MaNVN = Library.Common.User.MaNV;
                            _objPTTruoc.NgayNhap = DateTime.UtcNow.AddHours(7);
                            _objPTTruoc.NguoiNop = khachHang.TenKH;
                            _objPTTruoc.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
                            _objPTTruoc.MaTKNH = null;
                            _objPTTruoc.MaHTHT = 1;
                            _objPTTruoc.IsKhauTruTuDong = false;
                            _objPTTruoc.MaKH = khachHang.MaKH;
                            _objPTTruoc.MaMB = matBang != null ? (int?)matBang.MaMB : null;
                            _objPTTruoc.HinhThucThanhToanId = 1;
                            _objPTTruoc.HinhThucThanhToanName = "Tiền mặt";
                            _objPTTruoc.LyDo = dienGiaiThuTruoc;
                            _objPTTruoc.IsKhauTru = false;
                            db.ptPhieuThus.InsertOnSubmit(_objPTTruoc);
                            var ctptt = new ptChiTietPhieuThu();
                            ctptt.LinkID = null;
                            ctptt.DienGiai = dienGiaiThuTruoc;
                            ctptt.SoTien = gtThanhLy;
                            ctptt.ThuThua = gtThanhLy;
                            ctptt.KhauTru = 0;
                            ctptt.PhaiThu = 0;
                            _objPTTruoc.ptChiTietPhieuThus.Add(ctptt);
                            db.SubmitChanges();
                            Common.SoQuy_Insert(db, DateTime.Now.Month, DateTime.Now.Year, this.MaTN, hopDong.MaKH, null, _objPTTruoc.ID, ctptt.ID, DateTime.Now, soPhieuThuTruoc, 0, 2, true, 0, 0, gtThanhLy, 0, null, null, "Thu trước kèm phiếu thu: " + soPhieuThuAm + " cho khách hàng thanh lý hợp đồng", Common.User.MaNV, false, false);
                            #endregion kết thúc phiếu thu trước


                            // phiếu khấu trừ
                            #region phiếu khấu trừ
                            int ptktID = 0;
                            decimal? phaiThu = 0;
                            if (hoaDonKhauTrus != null && hoaDonKhauTrus.Where(x => matBangs.Contains(x.MaMB.GetValueOrDefault()) && x.DaThu > 0).Count() > 0)
                            {
                                var soPhieuKhauTru = Common.GetPayNumber(2, MaTN, null);
                                ptPhieuThu _objPTKhauTru = new ptPhieuThu();
                                _objPTKhauTru.MaTN = this.MaTN;
                                _objPTKhauTru.SoPT = soPhieuKhauTru;
                                _objPTKhauTru.NgayThu = DateTime.Now;
                                _objPTKhauTru.SoTien = 0;
                                _objPTKhauTru.MaNV = Common.User.MaNV;
                                _objPTKhauTru.MaPL = 1;
                                _objPTKhauTru.IsThanhLy = true;
                                _objPTKhauTru.IDHopDongTL = hopDong.ID;
                                _objPTKhauTru.MaKH = khachHang.MaKH;
                                _objPTKhauTru.MaMB = matBang != null ? (int?)matBang.MaMB : null;
                                _objPTKhauTru.MaNVN = Library.Common.User.MaNV;
                                _objPTKhauTru.NgayNhap = DateTime.UtcNow.AddHours(7);
                                _objPTKhauTru.NguoiNop = khachHang.TenKH;
                                _objPTKhauTru.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
                                _objPTKhauTru.MaTKNH = null;
                                _objPTKhauTru.MaHTHT = 1;
                                _objPTKhauTru.IsKhauTruTuDong = false;
                                _objPTKhauTru.HinhThucThanhToanId = 1;
                                _objPTKhauTru.HinhThucThanhToanName = "Tiền mặt";
                                _objPTKhauTru.LyDo = "Khấu trừ cho khách hàng thanh lý hợp đồng" + hopDong.SoHDCT;
                                _objPTKhauTru.IsKhauTru = true;
                                db.ptPhieuThus.InsertOnSubmit(_objPTKhauTru);
                                db.SubmitChanges();
                                ptktID = _objPTKhauTru.ID;
                                foreach (var hoaDon in hoaDonKhauTrus.Where(x => matBangs.Contains(x.MaMB.GetValueOrDefault()) && x.DaThu > 0))
                                {
                                    if (gtThanhLy > 0)
                                    {
                                        var soTien = (gtThanhLy - hoaDon.DaThu >= 0) ? hoaDon.DaThu : gtThanhLy;
                                        phaiThu += soTien;
                                        var ctptkt = new ptChiTietPhieuThu();
                                        ctptkt.LinkID = hoaDon.ID;
                                        ctptkt.TableName = "dvHoaDon";
                                        ctptkt.DienGiai = "Khấu trừ cho KH thanh lý HĐ: " + hoaDon.DienGiai;
                                        ctptkt.SoTien = 0;
                                        ctptkt.ThuThua = 0;
                                        ctptkt.PhaiThu = soTien;
                                        ctptkt.KhauTru = soTien;
                                        _objPTKhauTru.ptChiTietPhieuThus.Add(ctptkt);
                                        db.SubmitChanges();
                                        Common.SoQuy_Insert(db, DateTime.Now.Month, DateTime.Now.Year, this.MaTN, hopDong.MaKH, null, _objPTKhauTru.ID, ctptkt.ID, DateTime.Now, soPhieuKhauTru, 0, 1, true, soTien, 0, 0, soTien, hoaDon.ID, "dvHoaDon", "Khấu trừ cho KH thanh lý HĐ: " + hoaDon.DienGiai, Common.User.MaNV, true, false);
                                        hoaDon.IsDaThanhLy = true;
                                        gtThanhLy -= soTien;
                                        hoaDon.DaThu -= soTien;
                                    }
                                }

                            }
                            objTL = new ctThanhLy();
                            objTL.MaTN = this.MaTN;
                            objTL.MaHD = hopDong.ID;
                            objTL.DaThu = 0;
                            objTL.DaTra = 0;
                            objTL.PhaiThu = phaiThu;
                            objTL.PhaiTra = gtThanhLy;
                            objTL.NgayNhap = db.GetSystemDate();
                            objTL.MaNVN = Common.User.MaNV;
                            objTL.SoTL = db.CreateSoChungTu(40, this.MaTN);
                            objTL.NgayTL = dateNgayTL.DateTime;
                            objTL.TienTL = gtThanhLy + phaiThu;
                            objTL.MaLT = (int)lkLoaiTien.EditValue;
                            objTL.TyGia = spTyGia.Value;
                            objTL.LyDo = txtDienGiai.Text;
                            db.ctThanhLies.InsertOnSubmit(objTL);
                            db.SubmitChanges();
                            var hdkt = new HopDongKhauTru();
                            hdkt.SoTienConDu = gtThanhLy;
                            hdkt.HopDongID = hopDong.ID;
                            hdkt.PhieuThuKhauTruId = ptktID;
                            hopDongKhauTru.Add(hdkt);
                            db.SubmitChanges();
                            #endregion kết thúc phiếu khấu trừ

                        }
                    }

                }
                if (hopDongKhauTru.Where(x => x.SoTienConDu > 0).Count() > 0 && hoaDonKhauTrus.Where(x => x.DaThu > 0).Count() > 0)
                {
                    foreach (var item in hopDongKhauTru.Where(x => x.SoTienConDu > 0))
                    {
                        using (var _db = new MasterDataContext())
                        {
                            var hopDong = _db.ctHopDongs.Where(x => x.ID == item.HopDongID).FirstOrDefault();
                            var khachHang = _db.tnKhachHangs.Where(x => x.MaKH == hopDong.MaKH).FirstOrDefault();
                            var matBang = _db.mbMatBangs.Where(x => x.MaKH == khachHang.MaKH).FirstOrDefault();
                            var ptKhauTru = new ptPhieuThu();
                            if (item.PhieuThuKhauTruId > 0)
                            {
                                ptKhauTru = _db.ptPhieuThus.Where(x => x.ID == item.PhieuThuKhauTruId).FirstOrDefault();
                            }
                            else
                            {
                                var soPhieuKhauTru = Common.GetPayNumber(2, MaTN, null);
                                ptKhauTru.MaTN = this.MaTN;
                                ptKhauTru.SoPT = soPhieuKhauTru;
                                ptKhauTru.NgayThu = DateTime.Now;
                                ptKhauTru.SoTien = 0;
                                ptKhauTru.MaNV = Common.User.MaNV;
                                ptKhauTru.MaPL = 1;
                                ptKhauTru.IsThanhLy = true;
                                ptKhauTru.IDHopDongTL = hopDong.ID;
                                ptKhauTru.MaKH = khachHang.MaKH;
                                ptKhauTru.MaMB = matBang != null ? (int?)matBang.MaMB : null;
                                ptKhauTru.MaNVN = Library.Common.User.MaNV;
                                ptKhauTru.NgayNhap = DateTime.UtcNow.AddHours(7);
                                ptKhauTru.NguoiNop = khachHang.TenKH;
                                ptKhauTru.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
                                ptKhauTru.MaTKNH = null;
                                ptKhauTru.MaHTHT = 1;
                                ptKhauTru.IsKhauTruTuDong = false;
                                ptKhauTru.HinhThucThanhToanId = 1;
                                ptKhauTru.HinhThucThanhToanName = "Tiền mặt";
                                ptKhauTru.LyDo = "Khấu trừ cho khách hàng thanh lý hợp đồng";
                                ptKhauTru.IsKhauTru = true;
                                _db.ptPhieuThus.InsertOnSubmit(ptKhauTru);
                                _db.SubmitChanges();
                            }
                            item.PhieuThuKhauTruId = ptKhauTru.ID;
                            foreach (var hoaDon in hoaDonKhauTrus.Where(x => x.DaThu > 0))
                            {
                                if (item.SoTienConDu > 0)
                                {
                                    var soTien = (item.SoTienConDu - hoaDon.DaThu >= 0) ? hoaDon.DaThu : item.SoTienConDu;
                                    var ctptkt = new ptChiTietPhieuThu();
                                    ctptkt.LinkID = hoaDon.ID;
                                    ctptkt.TableName = "dvHoaDon";
                                    ctptkt.DienGiai = "Khấu trừ cho KH thanh lý HĐ: " + hoaDon.DienGiai;
                                    ctptkt.SoTien = 0;
                                    ctptkt.ThuThua = 0;
                                    ctptkt.PhaiThu = soTien;
                                    ctptkt.KhauTru = soTien;
                                    ptKhauTru.ptChiTietPhieuThus.Add(ctptkt);
                                    _db.SubmitChanges();
                                    Common.SoQuy_Insert(_db, DateTime.Now.Month, DateTime.Now.Year, this.MaTN, hopDong.MaKH, null, ptKhauTru.ID, ctptkt.ID, DateTime.Now, ptKhauTru.SoPT, 0, 1, true, soTien, 0, 0, soTien, hoaDon.ID, "dvHoaDon", "Khấu trừ cho KH thanh lý HĐ: " + hoaDon.DienGiai, Common.User.MaNV, true, false);
                                    hoaDon.IsDaThanhLy = true;
                                    item.SoTienConDu -= soTien;
                                    hoaDon.DaThu -= soTien;
                                }
                            }
                            _db.SubmitChanges();
                        }
                    }
                }
                if (hopDongKhauTru.Where(x => x.SoTienConDu > 0).Count() > 0)
                {
                    foreach (var item in hopDongKhauTru.Where(x => x.SoTienConDu > 0))
                    {
                        var hopDong = db.ctHopDongs.Where(x => x.ID == item.HopDongID).FirstOrDefault();
                        var khachHang = db.tnKhachHangs.Where(x => x.MaKH == hopDong.MaKH).FirstOrDefault();
                        var matBang = db.mbMatBangs.Where(x => x.MaKH == khachHang.MaKH).FirstOrDefault();
                        #region phiếu chi
                        pcPhieuChi _objPc = new pcPhieuChi();
                        var dienGiaiPhieuChi = "Chi cho khách hàng thanh lý hợp đồng(" + hopDong.SoHDCT +")";
                        var soPhieu = Common.CreatePhieuChi(this.MaTN.Value, DateTime.Now.Month, DateTime.Now.Year);
                        _objPc.SoPC = soPhieu;
                        _objPc.MaTN = this.MaTN;
                        _objPc.NgayChi = DateTime.Now;
                        _objPc.SoTien = item.SoTienConDu;
                        _objPc.MaNV = Common.User.MaNV;
                        _objPc.MaNCC = hopDong.MaKH;
                        _objPc.IsThanhLy = true;
                        _objPc.IDHopDongTL = hopDong.ID;
                        _objPc.NguoiNhan = khachHang.TenKH;
                        _objPc.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
                        _objPc.MaPhanLoai = 8;
                        _objPc.MaTKNH = null;
                        _objPc.LyDo = dienGiaiPhieuChi;
                        _objPc.OutputTyleId = 3;
                        _objPc.OutputTyleName = "Chi cho Khách hàng";
                        _objPc.NgayNhap = DateTime.UtcNow.AddHours(7);
                        _objPc.MaNVN = Library.Common.User.MaNV;
                        db.pcPhieuChis.InsertOnSubmit(_objPc);
                        var ct = new pcChiTiet();
                        ct.LinkID = null;
                        ct.DienGiai = dienGiaiPhieuChi;
                        ct.SoTien = item.SoTienConDu;
                        _objPc.pcChiTiets.Add(ct);
                        db.SubmitChanges();
                        bool isM = false;
                        isM = true;
                        bool IsNV = false;
                        //Common.SoQuy_Insert(db, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year, this.MaTN, isM == true ? (int)glDoiTuong.EditValue : (IsNV == true ? (int?)lkNVNhan.EditValue : 0), (int?)null, hd.MaPC, hd.ID, dateNgayChi.DateTime, txtSoPC.Text, cmbPTTT.SelectedIndex, (int?)lkLoaiChi.EditValue, false, 0, hd.SoTien, 0, 0, hd.LinkID, IsNV == true ? "NV" : "KH", hd.DienGiai, Common.User.MaNV, false);
                        Common.SoQuy_Insert(db, DateTime.Now.Month, DateTime.Now.Year, this.MaTN, HopDong.MaKH, (int?)null, _objPc.ID, ct.ID, DateTime.Now, soPhieu, 0, 8, false, 0, item.SoTienConDu
                            , 0, 0, null, null, dienGiaiPhieuChi, Common.User.MaNV, false, false);
                        db.SubmitChanges();
                        #endregion kết thúc phiếu chi
                        item.SoTienConDu -= item.SoTienConDu;
                    }
                }
                DialogBox.Success("Dữ liệu đã được lưu!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                db.SubmitChanges();
                //#endregion kết thúc phiếu khấu trừ

            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
            finally
            {
                db.Dispose();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.Dispose();
            }
            catch { }
        }

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spTyGia.EditValue = lkLoaiTien.GetColumnValue("TyGia");
        }

        private void spTienTL_EditValueChanged(object sender, EventArgs e)
        {
            spTienTLQD.EditValue = spTienTL.Value / (spTyGia.Value > 0 ? spTyGia.Value : 1);
            var giaTriThanhLy = spTienTLQD.Value;
            var tienHoaDon = spTienHoaDon.Value;
            spTienTraLai.Value = giaTriThanhLy - tienHoaDon;

        }
        public void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var dsData = new List<HoaDonItem>();
                //var strCompany = (ckbCompanyCode.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                //var listCompany = strCompany.Split(',');
                foreach (var item in ListHD)
                {
                    var hopDong = db.ctHopDongs.Where(x => x.ID.ToString() == item.Trim()).FirstOrDefault();
                    var matBang = hopDong.ctChiTiets.GroupBy(x => x.MaMB).Select(x => x.Key).ToList();
                    dsData.AddRange((from hd in db.dvHoaDons
                              join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH into khachhang
                              from kh in khachhang.DefaultIfEmpty()
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID into loaidichvu
                              from ldv in loaidichvu.DefaultIfEmpty()
                              join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into matbang
                              from mb in matbang.DefaultIfEmpty()
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tanglau
                              from tl in tanglau.DefaultIfEmpty()
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into khoinha
                              from kn in khoinha.DefaultIfEmpty()
                              join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into loaimatbang
                              from lmb in loaimatbang.DefaultIfEmpty()
                              join lx in db.dvgxLoaiXes on hd.MaLX equals lx.MaLX into loaixe
                              from lx in loaixe.DefaultIfEmpty()
                              join nv in db.tnNhanViens on hd.MaNVN equals nv.MaNV into nhanvien
                              from nv in nhanvien.DefaultIfEmpty()
                              where hd.MaTN == this.MaTN && hd.MaKH == hopDong.MaKH && matBang.Contains(hd.MaMB) && hd.ConNo > 0 && hd.IsDuyet == true
                              select new HoaDonItem
                              {
                                  ID = hd.ID,
                                  MaSoMB = mb != null ? mb.MaSoMB : null,
                                  IsChon = false,
                                  IsDaThanhLy = false,
                                  MaMB = hd.MaMB,
                                  NgayTT = hd.NgayTT,
                                  DienGiai = hd.DienGiai,
                                  TenLDV = ldv.TenHienThi,
                                  TienTruocThue = hd.TienTruocThue,
                                  TienThueGTGT = hd.TienThueGTGT,
                                  KyTT = hd.KyTT,
                                  TyLeCK = hd.TyLeCK,
                                  TienCK = hd.TienCK,
                                  PhaiThu = hd.PhaiThu,
                                  DaThu = 0,
                                  ConNo = hd.ConNo
                              }).ToList());
                }
                if (dsData.Count() > 0)
                {
                    ltData = dsData.GroupBy(x => x.ID).Select(x => new HoaDonItem
                    {
                        ID = x.Key,
                        MaSoMB = x.FirstOrDefault().MaSoMB,
                        IsChon = false,
                        IsDaThanhLy = false,
                        NgayTT = x.FirstOrDefault().NgayTT,
                        MaMB = x.FirstOrDefault().MaMB,
                        DienGiai = x.FirstOrDefault().DienGiai,
                        TenLDV = x.FirstOrDefault().TenLDV,
                        TienTruocThue = x.FirstOrDefault().TienTruocThue,
                        TienThueGTGT = x.FirstOrDefault().TienThueGTGT,
                        KyTT = x.FirstOrDefault().KyTT,
                        TyLeCK = x.FirstOrDefault().TyLeCK,
                        TienCK = x.FirstOrDefault().TienCK,
                        PhaiThu = x.FirstOrDefault().PhaiThu,
                        DaThu = 0,
                        ConNo = x.FirstOrDefault().ConNo,
                    }).ToList();

                }
                else
                {
                    ltData = new List<HoaDonItem>();
                }
                gcHoaDon.DataSource = ltData;

            }
        }

        private void gvHoaDon_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                gvHoaDon.BeginUpdate();
                switch (e.Action)
                {
                    case CollectionChangeAction.Refresh:
                        if (isSelectAll) for (int i = 0; i < gvHoaDon.RowCount; i++) gvHoaDon = ChangeGridView(gvHoaDon, i);
                        else
                        {
                            isSelectAll = false;
                            if (view.GetFocusedRowCellValue("DX$CheckboxSelectorColumn") != null) view = ChangeGridView(view);
                            else foreach (int item in view.GetSelectedRows()) view = ChangeGridView(view, item);
                        }
                        break;
                    case CollectionChangeAction.Remove:
                        isSelectAll = false;
                        if (view.GetFocusedRowCellValue("DX$CheckboxSelectorColumn") != null) view = ChangeGridView(view);
                        else if (groupHandle != null) for (int i = 0; i < view.GetChildRowCount((int)groupHandle); i++) view = ChangeGridView(view, view.GetChildRowHandle((int)groupHandle, i));
                        break;
                    default:
                        groupHandle = null;
                        isSelectAll = false;
                        view = ChangeGridView(view);
                        break;
                }

                gvHoaDon.EndUpdate();
            }
            catch { }
        }

        private DevExpress.XtraGrid.Views.Grid.GridView ChangeGridView(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            if (view.GetFocusedRowCellValue("DX$CheckboxSelectorColumn") != null)
            {
                var checkboxValue = (bool)view.GetFocusedRowCellValue("DX$CheckboxSelectorColumn");
                var isChonValue = (bool?)view.GetFocusedRowCellValue("IsChon");
                if (checkboxValue != isChonValue) view.SetFocusedRowCellValue("IsChon", checkboxValue);
            }
            return view;
        }

        private DevExpress.XtraGrid.Views.Grid.GridView ChangeGridView(DevExpress.XtraGrid.Views.Grid.GridView view, int handle)
        {
            if (view.GetRowCellValue(handle, "DX$CheckboxSelectorColumn") != null)
            {
                var checkboxValue = (bool)view.GetRowCellValue(handle, "DX$CheckboxSelectorColumn");
                var isChonValue = (bool?)view.GetRowCellValue(handle, "IsChon");
                if (checkboxValue != isChonValue) view.SetRowCellValue(handle, "IsChon", checkboxValue);
            }
            return view;
        }

        private void gvHoaDon_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gridView.CalcHitInfo(e.Location);

            if (hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowGroupCheckSelector && gridView.IsGroupRow(hitInfo.RowHandle))
            {
                //if (gridView.GetRowExpanded(hitInfo.RowHandle))
                //    gridView.CollapseGroupRow(hitInfo.RowHandle);
                //else
                //    gridView.ExpandGroupRow(hitInfo.RowHandle);
                groupHandle = hitInfo.RowHandle;
                return;
                //((DevExpress.Utils.DXMouseEventArgs)(e)).Handled = true;
            }
            if (hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column)
            {
                isSelectAll = true;
                //groupHandle = hitInfo.RowHandle;
                //gvHoaDon.SelectAll();
                return;
            }
        }

        private void gvHoaDon_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var hd = (HoaDonItem)gvHoaDon.GetRow(e.RowHandle);
            switch (e.Column.FieldName)
            {
                case "IsChon":
                    if (hd.IsChon == true)
                    {
                        if (hd.DaThu == 0)
                        {
                            hd.DaThu = hd.PhaiThu;
                        }
                        spTienHoaDon.Value += hd.DaThu != null ? hd.DaThu.Value : 0;
                        hd.ConNo = hd.PhaiThu - hd.DaThu;
                    }
                    else
                    {
                        spTienHoaDon.Value -= hd.DaThu != null ? hd.DaThu.Value : 0;
                        hd.DaThu = 0;
                        hd.ConNo = hd.PhaiThu - hd.DaThu;
                    }
                    if (spTienHoaDon.Value < 0)
                    {
                        spTienHoaDon.Value = 0;
                    }
                    gvHoaDon.UpdateCurrentRow();
                    break;
                case "DaThu":
                    hd.ConNo = hd.PhaiThu - hd.DaThu;
                    gvHoaDon.UpdateCurrentRow();
                    decimal soTien = 0;
                    for (int i = 0; i < gvHoaDon.RowCount; i++)
                    {
                        var item = (HoaDonItem)gvHoaDon.GetRow(i);
                        if (item.IsChon)
                        {
                            soTien += item.DaThu.GetValueOrDefault();
                        }
                    }
                    spTienHoaDon.Value = soTien;
                    spTienTraLai.Value = spTienTLQD.Value - spTienHoaDon.Value;
                    break;
            }
        }
        public class HopDongKhauTru
        {
            public int HopDongID { set; get; }
            public int PhieuThuKhauTruId { set; get; }
            public decimal? SoTienConDu { set; get; }
        }
        public class HoaDonItem
        {
            public long ID { set; get; }
            public bool IsChon { get; set; }
            public bool IsDaThanhLy { get; set; }
            public DateTime? NgayTT { set; get; }
            public string TenLDV { set; get; }
            public int? MaMB { set; get; }
            public string DienGiai { set; get; }
            public string MaSoMB { set; get; }
            public decimal? TienTruocThue { set; get; }
            public decimal? TienThueGTGT { set; get; }
            public decimal? KyTT { set; get; }
            public decimal? TyLeCK { set; get; }
            public decimal? TienCK { set; get; }
            public decimal? ConNo { set; get; }
            public decimal? PhaiThu { set; get; }
            public decimal? DaThu { set; get; }
        }

        private void spTienHoaDon_EditValueChanged(object sender, EventArgs e)
        {
            var giaTriThanhLy = spTienTLQD.Value;
            var tienHoaDon = spTienHoaDon.Value;
            spTienTraLai.Value = giaTriThanhLy - tienHoaDon;
        }

        private void ccbxHopDong_EditValueChanged(object sender, EventArgs e)
        {
            var maHDs = ccbxHopDong.EditValue.ToString();
            ListHD = new List<string>();
            ListMB = new List<int>();
            if (!string.IsNullOrEmpty(maHDs))
            {
                ListHD = maHDs.Split(',').ToList();
            }
            ListHD.Add(this.MaHD.ToString());
            decimal? tongTienCocDaThu = 0;
            decimal? tongTienCocDaChuyenThu = 0;
            foreach (var item in ListHD)
            {
                var hopDong = db.ctHopDongs.Where(x => x.ID.ToString() == item.Trim()).FirstOrDefault();
                if (hopDong != null)
                {
                    var matBangs = new List<int>();

                    foreach (var chitiet in hopDong.ctChiTiets.GroupBy(x => x.MaMB).Select(x => x.Key.Value).ToList())
                    {
                        matBangs.Add(chitiet);
                    }
                    tongTienCocDaThu += (from pt in db.SoQuy_ThuChis
                                        join hd in db.dvHoaDons on pt.LinkID equals (long?)hd.ID into hoadon
                                        from hd in hoadon.DefaultIfEmpty()
                                        where pt.MaKH == hopDong.MaKH && matBangs.Contains(pt.MaMB.Value) && (pt.MaLoaiPhieu == 49 || (pt.MaLoaiPhieu == 1 && pt.TableName == "dvHoaDon" && hd.MaLDV == 3 && hd.TableName == "ctLichThanhToan"))
                                        select new
                                        {
                                            pt.DaThu
                                        }).Sum(x => x.DaThu);
                    var s = (from pt in db.SoQuy_ThuChis
                             join ptt in db.ptPhieuThus on pt.IDPhieu equals ptt.ID
                             where pt.MaKH == hopDong.MaKH && ptt.IdHopDongChuyenCoc == hopDong.ID && (pt.MaLoaiPhieu == 52)
                             select new
                             {
                                 DaThu = pt.DaThu,
                             }).ToList();
                    tongTienCocDaChuyenThu += s.Sum(x => x.DaThu.GetValueOrDefault());
                }
            }
            LoadData();
            spTienTL.EditValue = tongTienCocDaThu + (tongTienCocDaChuyenThu != null ? tongTienCocDaChuyenThu : 0);
            spTienHoaDon.EditValue = 0;
        }

        private void ckbCompanyCode_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}