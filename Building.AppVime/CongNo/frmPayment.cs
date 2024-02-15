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

namespace Building.AppVime.CongNo
{
    public partial class frmPayment : DevExpress.XtraEditors.XtraForm
    {
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmPayment()
        {
            InitializeComponent();
        }

        public int? MaKH { get; set; }
        public byte? MaTN { get; set; }

        List<PaymentItem> ltData;

        private decimal?  _daThuBanDau, _conLai, _thuThua;
        private int? _stt;

        #region Get dữ liệu

        #region Diễn giải

        string GetDienGiai()
        {
            string strDienGiai = "";
            try
            {
                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             orderby l.SoTTDV
                             select new { l.MaLDV, l.TenLDV, l.PhaiThu }).Distinct().ToList();
                foreach (var i in ltLDV)
                {
                    var ltDV = (from l in ltData
                                where l.IsChon == true & l.MaLDV == i.MaLDV
                                group l by new { l.NgayTT.Value.Month, l.NgayTT.Value.Year } into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";
                    decimal TienXe = 0;
                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {
                            //TienXe += i.PhaiThu.GetValueOrDefault();
                            if (_Start != j)
                            {
                                if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                    strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                else
                                    strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                            }
                            else
                            {
                                strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                            }

                            _Start = j + 1;
                        }

                        j++;
                    }

                    strTime = strTime.TrimEnd(',');
                    strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }
        string GetDienGiaiVStar()
        {
            string strDienGiai = "";
            try
            {
                //var ltLDV = (from l in ltData
                //             where l.IsChon == true
                //             group l by new {l.MaLDV, l.TenLDV, l.PhaiThu} into gr
                //             select new { gr.Key.MaLDV, gr.Key.TenLDV, gr.Key.PhaiThu }).Distinct().ToList();
                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             orderby l.SoTTDV
                             //group l by new { l.MaLDV, l.TenLDV, l.PhaiThu } into gr
                             select new { l.MaLDV, l.TenLDV }).Distinct().ToList();
                foreach (var i in ltLDV)
                {
                    var ltLDVSum = (from l in ltData
                                    where l.IsChon == true & l.MaLDV == i.MaLDV
                                    group l by new { l.NgayTT.Value.Year, l.MaLDV } into gr
                                    select new { gr.Key.MaLDV, gr.Key.Year, PhaiThu = gr.Sum(p => p.ThucThu) }).Distinct().ToList();
                    var ltDV = (from l in ltData
                                where l.IsChon == true & l.MaLDV == i.MaLDV
                                group l by new { l.NgayTT.Value.Month, l.NgayTT.Value.Year, PhaiThu = l.ThucThu } into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";

                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {

                            decimal TienXeC = 0;
                            decimal TienXeD = 0;
                            if (_Start != j)
                            {
                                if (i.MaLDV == 8 | i.MaLDV == 9 | i.MaLDV == 10)
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1), ltDV[j].Value.AddMonths(-1));
                                }
                                else
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                }

                            }
                            else
                            {
                                if (i.MaLDV == 8 | i.MaLDV == 9)
                                    strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                else
                                {
                                    strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                                }
                            }

                            _Start = j + 1;
                        }

                        j++;
                    }

                    strTime = strTime.TrimEnd(',');

                    foreach (var tam in ltLDVSum)
                    {
                        if (!strDienGiai.Contains(string.Format("{0} {1}, ", i.TenLDV, strTime)))
                            strDienGiai += string.Format("{0} {1}, ", i.TenLDV, strTime);
                    }
                }
            }
            catch { }
            //string stempDG=
            //string[] arrListStr = stempDG.Split(',');

            return strDienGiai.Trim().TrimEnd(',');
        }
        string GetDienGiaiVStarNew()
        {
            string strDienGiai = "";
            try
            {

                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             orderby l.SoTTDV
                             select new { l.MaLDV, l.TenLDV, l.ThucThu, l.NgayTT }).Distinct().ToList();
                foreach (var i in ltLDV)
                {

                    var strTime = "";

                    if (i.MaLDV == 8 | i.MaLDV == 9 | i.MaLDV == 10)
                        strTime = string.Format("T{0:MM/yyyy},", i.NgayTT.Value.AddMonths(-1));
                    else
                    {
                        strTime = string.Format("T{0:MM/yyyy},", i.NgayTT.Value);
                    }
                    strTime = strTime.TrimEnd(',');
                    strDienGiai += string.Format("{0} {1} ({2:#,0.##}) , ", i.TenLDV, strTime, Math.Round((decimal)i.ThucThu, 0));



                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        #endregion

        void TinhSoTien()
        {
            var soTien = ltData.Where(p => p.IsChon == true).Sum(p => p.ThucThu).GetValueOrDefault() ;
            spinThuThua.EditValue = ltData.Where(p => p.IsChon == true).Sum(p => p.KhauTru);
            txtDienGiai.Text = this.GetDienGiaiVStar();
            spSoTien.EditValue = soTien;
        }

        private void CapNhatDaThu()
        {
            _daThuBanDau = (decimal?)spinDaThu.EditValue;

            var thanhToan = ltData.Where(p => p.IsChon == true);
            decimal tienThanhToan = 0;
            foreach (var item in thanhToan)
            {
                tienThanhToan += item.ThucThu.GetValueOrDefault() > item.ConNo.GetValueOrDefault() ? item.ConNo.GetValueOrDefault() : item.ThucThu.GetValueOrDefault();
            }

            var thanhToanCoThuThua = thanhToan.Sum(p => p.ThucThu).GetValueOrDefault();
            var conNo = ltData.Where(_ => _.IsChon == true).Sum(_ => _.ConNo).GetValueOrDefault();

            //spinDaThu.EditValue = thanhToanCoThuThua > _daThuBanDau ? thanhToanCoThuThua : _daThuBanDau; //(decimal?)spinDaThu.EditValue
            spinDaThu.EditValue = (thanhToanCoThuThua < _daThuBanDau & thanhToanCoThuThua == (decimal?)spinPhaiThu.EditValue) ? _daThuBanDau : thanhToanCoThuThua;

            // cập nhật thu thừa để tạo phiếu thu thừa
            var thanhToanKhongThuThua = tienThanhToan;
            spinDaThanhToan.EditValue = thanhToanKhongThuThua;
            spinTienThua.EditValue = thanhToanKhongThuThua > (decimal?)spinDaThu.EditValue ? 0 : ((decimal?)spinDaThu.EditValue - thanhToanKhongThuThua);

            // cập nhật lại số tiền ở thông tin thanh toán
            spSoTien.EditValue = spinDaThanhToan.EditValue;
        }

        decimal GetTyleCK(int _MaLDV, decimal _KyTT)
        {
            var db = new MasterDataContext();
            return (from ck in db.dvChietKhaus
                    where ck.MaTN == this.MaTN & ck.MaLDV == _MaLDV & ck.KyTT >= _KyTT
                    orderby ck.KyTT
                    select ck.TyLeCK).FirstOrDefault().GetValueOrDefault();
        }

        void UpdateHoaDon(List<PaymentItem> ltHoaDon)
        {
            var db = new MasterDataContext();
            try
            {
                foreach (var hd in ltHoaDon)
                {
                    if (hd.ID == 0)
                    {
                        //var objHD = new dvHoaDon();
                        //objHD.MaTN = this.MaTN;
                        //objHD.MaKH = (int)glKhachHang.EditValue;
                        //objHD.NgayTT = hd.NgayTT;
                        //objHD.PhiDV = hd.PhiDV;
                        //objHD.MaLDV = hd.MaLDV;
                        //objHD.DienGiai = hd.DienGiai;
                        //objHD.MaNVN = Common.User.MaNV;
                        //objHD.IsDuyet = true;
                        //objHD.KyTT = hd.KyTT;
                        //objHD.TienTT = hd.TienTT;
                        //objHD.TyLeCK = hd.TyLeCK;
                        //objHD.TienCK = hd.TienCK;
                        //objHD.PhaiThu = hd.PhaiThu.GetValueOrDefault();
                        //objHD.ConNo = objHD.PhaiThu - objHD.DaThu.GetValueOrDefault();
                        //db.dvHoaDons.InsertOnSubmit(objHD);

                        //db.SubmitChanges();

                        //hd.ID = objHD.ID;
                    }
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void UpdateLaiPhatSinh()
        {
            var _NgayTT = dateNgayThu.DateTime;
            var db = new MasterDataContext();
            try
            {
                var objHD = ltData.FirstOrDefault(p => p.ID == 0 & p.MaLDV == 23);
                var _TienLai = this.TinhTienLai();
                if (_TienLai > 0)
                {
                    if (objHD == null)
                    {
                        objHD = new PaymentItem();
                        objHD.ID = 0;
                        objHD.MaLDV = 23;
                        objHD.TenLDV = (from ldv in db.dvLoaiDichVus where ldv.ID == objHD.MaLDV select ldv.TenHienThi).FirstOrDefault();
                        objHD.SoTTDV = (from ldv in db.dvLoaiDichVus where ldv.ID == objHD.MaLDV select ldv.STT).FirstOrDefault();
                        objHD.IsChon = true;
                        ltData.Add(objHD);
                    }
                    objHD.NgayTT = _NgayTT;
                    objHD.ThangTT = string.Format("{0:yyyy-MM}", _NgayTT);
                    objHD.KyTT = 1;
                    objHD.PhiDV = _TienLai;
                    objHD.TienTT = _TienLai;
                    objHD.TyLeCK = 0;
                    objHD.TienCK = 0;
                    objHD.PhaiThu = _TienLai;
                    objHD.DaThu = 0;
                    objHD.ConNo = _TienLai;
                    objHD.ThucThu = objHD.IsChon == true ? objHD.ConNo : 0;
                    objHD.DienGiai = string.Format("{0} phát sinh đến ngày {1:dd/MM/yyyy}", objHD.TenLDV, _NgayTT);
                }
                else
                {
                    if (objHD != null)
                    {
                        ltData.Remove(objHD);
                    }
                }
                gvHoaDon.PostEditor();
                this.TinhSoTien();
                gvHoaDon.RefreshData();
                gvHoaDon.ExpandAllGroups();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        decimal TinhTienLai()
        {
            decimal _TienLai = 0;
            var _NgayThu = dateNgayThu.DateTime;
            var db = new MasterDataContext();
            foreach (var i in ltData)
            {
                if (i.IsChon & i.MaLDV != 23)
                {
                    _TienLai += db.GetTienLai(this.MaTN, i.MaLDV, i.NgayTT, i.ThucThu, _NgayThu).GetValueOrDefault();
                }
            }

            return _TienLai;
        }

        void SaveData(bool _IsPrint, int _ReportID)
        {
            gvHoaDon.FocusedRowHandle = -1;
            var db = new MasterDataContext();
            #region Rang buoc
            if (txtSoPT.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPT.Focus();
                return;
            }

            if ((decimal?)spinTienThua.Value > 0)
            {
                if (DialogBox.Question("Phiếu có thu thừa, đồng ý cho hệ thống tự tạo thu trước? ") == DialogResult.No) return;
            }

            var objKTPT = db.ptPhieuThus.FirstOrDefault(p => p.MaTN == MaTN & p.SoPT.Equals(txtSoPT.Text.Trim()));

            // kiểm tra tòa nhà
            var strMatBang = (chkMatBang.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var ltMatBang = strMatBang.Split(',');
            if (strMatBang == "") { Library.DialogBox.Alert("Vui lòng chọn mặt bằng"); return; }

            var objMB = db.mbMatBangs.FirstOrDefault(p => ltMatBang.Contains(p.MaMB.ToString()));

            if(objMB==null)
            {
                Library.DialogBox.Alert("Vui lòng chọn mặt bằng");
                return;
            }

            if (objKTPT != null)
            {
                txtSoPT.Text = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, false);
            }
            if (dateNgayThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thu");
                dateNgayThu.Focus();
                return;
            }
            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }

            if (lkPhanLoai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phân loại phiếu thu");
                lkPhanLoai.Focus();
                return;
            }
            if (spinThuThua.Value > spinSoTienThuTruoc.Value)
            {
                DialogBox.Error("Số tiền khấu trừ lớn hơn số tiền dư của khách hàng. Vui lòng kiểm tra lại!");
                return;
            }

            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (cmbPTTT.SelectedIndex == 1 && lkTaiKhoanNganHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                return;
            }

            
            #endregion

            //Cap nhat lai hoa don
            gvHoaDon.UpdateCurrentRow();
            var ltChiTiet = ltData.Where(p => p.IsChon == true).ToList();

            if (ltChiTiet.Count == 0)
            {
                DialogBox.Error("Vui lòng chọn hạng mục cần thu");
                return;
            }

            // update lại ltChitiết, không cho thu thừa
            foreach(var item in ltChiTiet)
            {
                if(item.ThuThua>0)
                {
                    item.ThucThu = item.ConNo;
                    item.KhachTra = 0;
                    item.ThuThua = 0;
                }
            }
            //
            this.UpdateHoaDon(ltChiTiet);

            #region Code Phiếu thu mới
            var objPT = new ptPhieuThu();
            objPT.MaTN = this.MaTN;
            objPT.SoPT = txtSoPT.Text;
            objPT.NgayThu = dateNgayThu.DateTime;
            objPT.SoTien = spSoTien.Value;
            objPT.MaPL = (int)lkPhanLoai.EditValue;
            objPT.MaKH = (int)glKhachHang.EditValue;
            objPT.NguoiNop = txtNguoiNop.Text;
            objPT.DiaChiNN = txtDiaChi.Text;
            objPT.LyDo = txtDienGiai.Text;
            objPT.MaMB = (int?)objMB.MaMB;
            if (cmbPTTT.SelectedIndex == 1)
            {
                objPT.MaTKNH = (int?)lkTaiKhoanNganHang.EditValue;
                objPT.MaHTHT = 2;
                objPT.HinhThucThanhToanId = 2;
                objPT.HinhThucThanhToanName = "Chuyển khoản";
            }
            else
            {
                objPT.MaTKNH = null;
                objPT.MaHTHT = 1;
                objPT.HinhThucThanhToanId = 1;
                objPT.HinhThucThanhToanName = "Tiền mặt";
            }

            objPT.ChungTuGoc = txtChungTuGoc.Text;
            objPT.MaNV = (int)lkNhanVien.EditValue;
            objPT.MaNVN = Common.User.MaNV;
            objPT.NgayNhap = Common.GetDateTimeSystem();
            objPT.IsKhauTru = itemLoaiPhieu.SelectedIndex == 0 ? false : true;
            db.ptPhieuThus.InsertOnSubmit(objPT);

            foreach (var hd in ltChiTiet)
            {
                // kiểm tra lại khúc này
                var objCT = new ptChiTietPhieuThu();
                objCT.PhaiThu = hd.ConNo;
                objCT.SoTien = itemLoaiPhieu.SelectedIndex == 0 ? hd.ThucThu : 0; ////
                decimal? khauTru = itemLoaiPhieu.SelectedIndex == 0 ? 0 : hd.KhauTru;
                objCT.KhauTru = khauTru;
                //objCT.KhauTru = itemLoaiPhieu.SelectedIndex == 0 ? 0 : hd.ThucThu;////
                objCT.ThuThua = hd.ThuThua;
                //objCT.TableName = "dvHoaDon";
                objCT.TableName = hd.TableName;
                objCT.LinkID = hd.ID;
                objCT.DienGiai = hd.DienGiai;

                objPT.ptChiTietPhieuThus.Add(objCT);

                
                db.SubmitChanges();

                // Kiểm tra ngày thu của cái hóa đơn
                // ngày thu lớn hơn ngày thanh toán, ngày thu là ngày datenaythu, ngày thanh toán là ngày trên cái phiếu hóa đơn
                DateTime ngayThu = dateNgayThu.DateTime;
                if (System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(hd.NgayTT, dateNgayThu.DateTime) >= 0) ngayThu = dateNgayThu.DateTime;
                else ngayThu = (System.DateTime)hd.NgayTT;
                    //Lưu vào sổ quỹ
                    Common.SoQuy_Insert(db, ngayThu.Month, ngayThu.Year, this.MaTN, (int)glKhachHang.EditValue, objPT.MaMB, objCT.MaPT, objCT.ID, ngayThu, txtSoPT.Text, cmbPTTT.SelectedIndex, (int?)lkPhanLoai.EditValue, true, objCT.PhaiThu.GetValueOrDefault(), objCT.SoTien.GetValueOrDefault(), objCT.ThuThua.GetValueOrDefault(), objCT.KhauTru.GetValueOrDefault(), hd.ID, hd.TableName, objCT.DienGiai, Common.User.MaNV, objPT.IsKhauTru.GetValueOrDefault(), true);
            }

            
            //foreach (var hd in objPT.ptChiTietPhieuThus)
            //{
                
            //}
            #endregion

            #region Tạo phiếu thu trước + lưu sổ quỹ

            var thuThuaTong = (decimal?)spinTienThua.EditValue;
            if(thuThuaTong>0)
            {
                const int maPhanLoaiThuTruoc = 2;
                string dienGiaiThuTruoc = "Thu trước kèm phiếu thu: " + objPT.SoPT + " (" + txtDienGiai.Text + ").";

                // phiếu thu
                var phieuThuTruoc = new Library.ptPhieuThu();
                phieuThuTruoc.MaTN = this.MaTN;
                phieuThuTruoc.SoPT = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, false);
                phieuThuTruoc.NgayThu = dateNgayThu.DateTime;
                phieuThuTruoc.SoTien = thuThuaTong;
                phieuThuTruoc.MaPL = maPhanLoaiThuTruoc;
                phieuThuTruoc.MaKH = (int)glKhachHang.EditValue;
                phieuThuTruoc.NguoiNop = txtNguoiNop.Text;
                phieuThuTruoc.DiaChiNN = txtDiaChi.Text;
                phieuThuTruoc.LyDo = dienGiaiThuTruoc;
                phieuThuTruoc.MaMB = (int?)objMB.MaMB;
                phieuThuTruoc.MaTKNH = cmbPTTT.SelectedIndex == 1 ? (int?)lkTaiKhoanNganHang.EditValue : null;
                phieuThuTruoc.ChungTuGoc = txtChungTuGoc.Text;
                phieuThuTruoc.MaNV = (int)lkNhanVien.EditValue;
                phieuThuTruoc.MaNVN = Library.Common.User.MaNV;
                phieuThuTruoc.NgayNhap = Library.Common.GetDateTimeSystem();
                phieuThuTruoc.IsKhauTru = false;
                //phieuThuTruoc.ThuThuaId = objPT.ID;

                // chi tiết phiếu thu
                var chiTietPhieuThuTruoc = new Library.ptChiTietPhieuThu();
                chiTietPhieuThuTruoc.PhaiThu = 0;
                chiTietPhieuThuTruoc.SoTien = thuThuaTong;
                chiTietPhieuThuTruoc.KhauTru = 0;
                chiTietPhieuThuTruoc.ThuThua = thuThuaTong;
                chiTietPhieuThuTruoc.TableName = "ad_HoaDon";
                chiTietPhieuThuTruoc.LinkID = null;
                chiTietPhieuThuTruoc.DienGiai = dienGiaiThuTruoc;
                phieuThuTruoc.ptChiTietPhieuThus.Add(chiTietPhieuThuTruoc);

                db.ptPhieuThus.InsertOnSubmit(phieuThuTruoc);
                db.SubmitChanges();

                // lưu phiếu thu trước vào sổ quỹ
                Library.Common.SoQuy_Insert(db, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, this.MaTN, (int)glKhachHang.EditValue, phieuThuTruoc.MaMB, phieuThuTruoc.ID, chiTietPhieuThuTruoc.ID, dateNgayThu.DateTime, phieuThuTruoc.SoPT, cmbPTTT.SelectedIndex, maPhanLoaiThuTruoc, true, chiTietPhieuThuTruoc.PhaiThu, chiTietPhieuThuTruoc.SoTien, chiTietPhieuThuTruoc.ThuThua, chiTietPhieuThuTruoc.KhauTru, null, chiTietPhieuThuTruoc.TableName, phieuThuTruoc.LyDo, Library.Common.User.MaNV, phieuThuTruoc.IsKhauTru.GetValueOrDefault(), true);

                objPT.ThuThuaId = phieuThuTruoc.ID;
                objPT.TienThuThua = phieuThuTruoc.SoTien;
                objPT.TongTienDaThu = objPT.SoTien + phieuThuTruoc.SoTien;
                db.SubmitChanges();
            }

            

            #endregion

            
            #region In Phiếu 
            if (_IsPrint)
            {
                DevExpress.XtraReports.UI.XtraReport rpt = null;
                

                //var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)_ReportID);
                //if (objForm != null)
                //{
                //    var rtfText = BuildingDesignTemplate.Class.MergeField.PhieuThu(objPT.ID, objForm.Content);
                //    var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
                //    frm.ShowDialog(this);
                //}

                //switch (_ReportID)
                //{
                //    case 3:
                //        rpt = new Fund.Input.rptPhieuThu(objPT.ID, this.MaTN.Value, 1);
                //        for (int i = 1; i <= 3; i++)
                //        {
                //            var rpt1 = new Fund.Input.rptPhieuThu(objPT.ID, this.MaTN.Value, i);
                //            rpt1.CreateDocument();
                //            rpt.Pages.AddRange(rpt1.Pages);
                //        }
                //        rpt.PrintingSystem.ContinuousPageNumbering = true;

                //        break;
                //    case 19:
                //        rpt = new Fund.Input.rptDetail(objPT.ID, this.MaTN.Value);
                //        break;
                //    case 42:
                //        rpt = new Fund.Input.rptPhieuThuMau3(objPT.ID, this.MaTN.Value);
                //        break;
                //    case 84:
                //        rpt = new LandSoftBuilding.Report.rptPhieuThuLienBacHaMau2(objPT.ID, this.MaTN.Value);
                //        break;
                //    case 85:
                //        using (var frm = new Building.PrintControls.PrintForm())
                //        {
                //            frm.PrintControl.MaBC = 3;
                //            frm.PrintControl.MaTN = this.MaTN.Value;
                //            frm.PrintControl.IDPhieuthu = objPT.ID;
                //            frm.PrintControl.Report = new rptPhieuThu257(objPT.ID, this.MaTN.Value, 3);

                //            frm.ShowDialog();
                //        }
                //        break;
                //    case 88:
                //        rpt = new Fund.Input.rptPhieuThuImperia(objPT.ID, this.MaTN.Value);
                //        break;
                //    case 97:
                //        rpt = new Fund.Input.rptPhieuThuImperiaOutside(objPT.ID, this.MaTN.Value);
                //        break;
                //}

                //if (rpt != null)
                //{
                //    rpt.ShowPreviewDialog();
                //}
            }
            #endregion
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void Load_MauPhieuThu()
        {
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == this.MaTN & rp.GroupID == 5
                                orderby rp.Rank
                                select new { rp.ID, rp.Name, tn.IsDefault }).ToList();

                popupMenu1.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                    popupMenu1.ItemLinks.Add(itemPrint);
                }

                var objRP = ltReport.FirstOrDefault(p => p.IsDefault == true);
                if (objRP == null)
                {
                    objRP = ltReport.First();
                }
                btnLuuIn.Tag = objRP.ID;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        #endregion

        private void frmPayment_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            itemClearText.Click += ItemClearText_Click;
            itemHuongDan.Click += ItemHuongDan_Click;

            TranslateLanguage.TranslateControl(this);

            var db = new MasterDataContext();

            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN & kh.MaKH == this.MaKH
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.TenKH.ToString() : kh.CtyTen,
                                                     ThuTruoc = (from pt in db.ptPhieuThus
                                                                 where pt.MaKH == this.MaKH & pt.MaPL == 2
                                                                 select pt.SoTien).Sum().GetValueOrDefault()
                                               - (from pt in db.ktttKhauTruThuTruocs
                                                  where pt.MaKH == this.MaKH
                                                  select pt.SoTien).Sum().GetValueOrDefault()
                                                 }).ToList();
            lkNhanVien.Properties.DataSource = (from nv in db.tnNhanViens where nv.MaTN == this.MaTN select new { nv.MaNV, nv.HoTenNV }).ToList();
            lkTaiKhoanNganHang.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                        where tk.MaTN == this.MaTN
                                                        select new { tk.ID, tk.SoTK, nh.TenNH }).ToList();
            lkPhanLoai.Properties.DataSource = (from pl in db.ptPhanLoais where pl.IsDvApp == true select new { pl.ID, pl.TenPL }).ToList();
            lkPhanLoai.ItemIndex = 0;
                        
            glKhachHang.EditValue = this.MaKH;
            dateNgayThu.EditValue = DateTime.UtcNow.AddHours(7);
            lkNhanVien.EditValue = Common.User.MaNV;
            cmbPTTT.SelectedIndex = 0;

            //
            this.Load_MauPhieuThu();
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                #region check mặt bằng

                LoadCheckMatBangByKhachHangId((int)glKhachHang.EditValue);
                chkMatBang.CheckAll();

                #endregion

                var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNop.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChi.Text = (from mb in db.mbMatBangs
                                        join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                        where mb.MaKH == (int)glKhachHang.EditValue
                                        select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();
                }

                LoadHoaDon();
            }
            catch { }
        }

        private void LoadHoaDon()
        {
            try
            {
                var db = new MasterDataContext();

                // kiểm tra tòa nhà
                var strMatBang = (chkMatBang.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltMatBang = strMatBang.Split(',');
                //if (strMatBang == "") { Library.DialogBox.Alert("Vui lòng chọn mặt bằng"); return; }

                var param = new Dapper.DynamicParameters();
                param.Add("@makh", (int?)glKhachHang.EditValue, DbType.Int32, null, null);
                param.Add("@mamb", strMatBang, DbType.String, null, null);
                ltData = Library.Class.Connect.QueryConnect.Query<PaymentItem>("dbo.ad_HoaDon_get_thanh_toan", param).ToList();

                #region Code mới
                //ltData = (from hd in db.dvHoaDons
                //          join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                //          join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB
                //          join dvtg in db.dvgxDonViThoiGians on hd.MaDVTG equals dvtg.ID into tblDonViThoiGian
                //          from dvtg in tblDonViThoiGian.DefaultIfEmpty()

                //          where hd.MaKH == (int?)glKhachHang.EditValue & hd.ConNo.GetValueOrDefault() != 0 & hd.IsDuyet == true & ltMatBang.Contains(mb.MaMB.ToString()) //& System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(hd.NgayTT, dateNgayThu.DateTime) >= 0
                //          select new PaymentItem()
                //          {
                //              IsChon = false,
                //              ID = hd.ID,
                //              MaLDV = hd.MaLDV,
                //              SoTTDV = l.STT,
                //              MaSoMB = mb.MaSoMB,
                //              TenLDV = l.TenHienThi,
                //              DienGiai = hd.DienGiai,
                //              PhiDV = hd.PhiDV,
                //              NgayTT = hd.NgayTT,
                //              ThangTT = string.Format("{0:yyyy-MM}", hd.NgayTT),
                //              KyTT = hd.KyTT,
                //              DonVi = dvtg.TenDVTG,
                //              TienTT = hd.TienTT,
                //              TyLeCK = hd.TyLeCK,
                //              TienCK = hd.TienCK,
                //              PhaiThu = hd.PhaiThu,
                //              DaThu = db.SoQuy_ThuChis.Where(sq => sq.TableName == "dvHoaDon" && sq.LinkID == hd.ID).Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault(),
                //              ConNo
                //                = hd.PhaiThu - db.SoQuy_ThuChis.Where(sq => sq.TableName == "dvHoaDon" && sq.LinkID == hd.ID).Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault(),
                //              ThucThu = 0,

                //          }).Select(p => new PaymentItem
                //          {
                //              IsChon = p.IsChon,
                //              ID = p.ID,
                //              MaLDV = p.MaLDV,
                //              SoTTDV = p.SoTTDV,
                //              MaSoMB = p.MaSoMB,
                //              TenLDV = p.TenLDV,
                //              DienGiai = p.DienGiai,
                //              PhiDV = p.PhiDV,
                //              NgayTT = p.NgayTT,
                //              ThangTT = p.ThangTT,
                //              KyTT = p.KyTT,
                //              DonVi = p.DonVi,
                //              TienTT = p.TienTT,
                //              TyLeCK = p.TyLeCK,
                //              TienCK = p.TienCK,
                //              PhaiThu = p.PhaiThu.GetValueOrDefault(),
                //              DaThu = p.DaThu.GetValueOrDefault(),
                //              ConNo = p.ConNo.GetValueOrDefault(),
                //              KhauTru = 0,
                //              ThuThua = 0,
                //              KhachTra = p.ConNo - 0
                //          }).ToList();
                #endregion

                gcHoaDon.DataSource = null;
                gcHoaDon.DataSource = ltData.Where(p => p.ConNo != 0);
                gvHoaDon.ExpandAllGroups();

                #region Update tổng tiền phải thanh toán
                spinPhaiThu.EditValue = ltData.Where(p => p.ConNo != 0).Sum(_ => _.ConNo);
                spinConLai.EditValue = spinPhaiThu.EditValue;
                _conLai = (decimal?)spinConLai.EditValue;
                _daThuBanDau = _daThuBanDau == null ? 0 : _daThuBanDau;
                _stt = 0;
                spinDaThanhToan.Value = 0;
                #endregion

                //Load số tiền thu trước của khách hàng
                //p.IsPhieuThu == true && 
                if(glKhachHang.EditValue!=null)
                {
                    var objSoTien1 = db.SoQuy_ThuChis.Where(p => p.MaKH == (int?)glKhachHang.EditValue).Select(_=>new { ThuThua = _.ThuThua, KhauTru = _.KhauTru}).ToList();
                    var objSoTien = objSoTien1.Sum(s => s.ThuThua.GetValueOrDefault() - s.KhauTru.GetValueOrDefault());
                    spinSoTienThuTruoc.EditValue = objSoTien;
                }
                
                this.UpdateLaiPhatSinh();
            }
            catch { }
        }

        private void LoadCheckMatBangByKhachHangId(int? khachHangId)
        {
            var db = new MasterDataContext();
            chkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                where mb.MaTN == this.MaTN & mb.MaKH == (int)khachHangId
                                                orderby mb.MaSoMB descending
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    //tl.TenTL,
                                                    //kn.TenKN,
                                                    //kh.MaKH,
                                                    //TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                }).ToList();
        }

        private void gvHoaDon_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
                var hd = (PaymentItem)gvHoaDon.GetRow(e.RowHandle);
            switch (e.Column.FieldName)
            {
                case "ThucThu":
                    {
                        hd.KhachTra = hd.ConNo.GetValueOrDefault() - (hd.ThucThu.GetValueOrDefault() + hd.KhauTru.GetValueOrDefault());
                        hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                        if (hd.KhachTra >= 0)
                        {
                            if (hd.IsChon == false)
                            {
                                hd.IsChon = true;
                            }
                        }
                        if (hd.KhachTra < 0)
                        {
                            if (hd.IsChon == false)
                            {
                                hd.IsChon = true;
                            }
                        }

                    }


                    break;
                case "KhachTra":
                    // hd.IsChon = hd.ThucThu > 0;     
                    hd.KhachTra = hd.ConNo.GetValueOrDefault() - (hd.ThucThu.GetValueOrDefault() + hd.KhauTru.GetValueOrDefault());
                    hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                    if (hd.ThuThua >= 0)
                    {
                        if (hd.IsChon == false)
                        {
                            hd.IsChon = true;
                        }
                    }
                    break;
                case "IsChon":
                    if (hd.IsChon == true)
                    {
                        if (hd.ConNo != 0)
                        {
                            if (itemLoaiPhieu.SelectedIndex == 0)
                            {
                                hd.ThucThu = hd.ConNo;
                                hd.KhachTra = hd.ConNo.GetValueOrDefault() - hd.ThucThu.GetValueOrDefault();
                            }
                            else
                            {
                                hd.ThucThu = 0;
                                hd.KhauTru = hd.ConNo;
                                hd.KhachTra = hd.ConNo.GetValueOrDefault() - hd.KhauTru.GetValueOrDefault();
                            }
                        }

                        else
                            hd.IsChon = false;
                    }
                    else
                    {
                        if (itemLoaiPhieu.SelectedIndex == 0)
                        {
                            hd.ThucThu = 0;
                            hd.KhachTra = hd.ConNo.GetValueOrDefault() - (hd.ThucThu.GetValueOrDefault() + hd.KhauTru.GetValueOrDefault());
                            hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                        }
                        else
                        {
                            hd.ThucThu = 0;
                            hd.KhauTru = 0;
                            hd.KhachTra = hd.ConNo.GetValueOrDefault() - hd.KhauTru.GetValueOrDefault();
                            hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                        }
                    }
                    break;
                case "KhauTru":
                    hd.KhachTra = hd.ConNo.GetValueOrDefault() - (hd.ThucThu.GetValueOrDefault() + hd.KhauTru.GetValueOrDefault());
                    hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                    if (hd.KhachTra >= 0)
                    {
                        if (hd.IsChon == false)
                        {
                            hd.IsChon = true;
                        }
                    }
                    if (hd.KhachTra < 0)
                    {
                        if (hd.IsChon == false)
                        {
                            hd.IsChon = true;
                        }
                    }
                    break;
                case "KyTT":
                case "TyLeCK":
                case "TienCK":
                    if (hd.KyTT > 0)
                        hd.TienTT = hd.PhiDV * hd.KyTT;
                    else
                        hd.TienTT = hd.PhiDV;

                    if (e.Column.FieldName == "KyTT")
                    {
                        hd.TyLeCK = this.GetTyleCK(hd.MaLDV.Value, hd.KyTT.Value);
                        hd.TienCK = hd.TyLeCK * hd.TienTT;
                    }

                    if (e.Column.FieldName == "TyLeCK") hd.TienCK = hd.TyLeCK * hd.TienTT;

                    hd.PhaiThu = hd.TienTT - hd.TienCK;
                    hd.ConNo = hd.PhaiThu - hd.DaThu;

                    // if (hd.ConNo <= 0) hd.IsChon = false;

                    if (hd.IsChon == true)
                    {
                        hd.ThucThu = hd.ConNo;
                        hd.KhachTra = hd.ConNo.GetValueOrDefault() - (hd.ThucThu.GetValueOrDefault() + hd.KhauTru.GetValueOrDefault());
                        hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                    }
                    else
                    {
                        hd.ThucThu = 0;
                        hd.KhachTra = hd.ConNo.GetValueOrDefault() - (hd.ThucThu.GetValueOrDefault() + hd.KhauTru.GetValueOrDefault());
                        hd.ThuThua = hd.KhachTra < 0 ? -hd.KhachTra : 0;
                    }
                    break;
            }
            gvHoaDon.PostEditor();
            this.UpdateLaiPhatSinh();
            CapNhatDaThu();
        }

        private void cmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            lkTaiKhoanNganHang.Enabled = cmbPTTT.SelectedIndex == 1;
        }

        private void dateNgayThu_EditValueChanged(object sender, EventArgs e)
        {
            //LoadHoaDon();
            this.UpdateLaiPhatSinh();
        }

        private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var hd in ltData)
            {
                hd.IsChon = hd.ConNo != 0 & ckbSelectAll.Checked; //& ckbSelectAll.Checked;
                hd.ThucThu = hd.IsChon ? hd.ConNo : 0;
                //hd.ThucThu = hd.IsChon ? hd.ConNo : hd.ThucThu;
                hd.KhachTra = 0;//(hd.ConNo - hd.ThucThu) > 0 ? (hd.ConNo - hd.ThucThu) : 0;
                hd.ThuThua = 0;//hd.ThucThu > hd.ConNo ? (hd.ThucThu - hd.ConNo) : 0;
            }
            gvHoaDon.PostEditor();
            this.UpdateLaiPhatSinh();

            gvHoaDon.RefreshData();

            CapNhatDaThu();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.SaveData(false, 0);
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveData(true, (int)e.Item.Tag);
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            this.SaveData(true, (int)btnLuuIn.Tag);
        }

        private void glKhachHang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }

        private void lkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var db = new MasterDataContext();

                var strMatBang = (chkMatBang.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltMatBang = strMatBang.Split(',');
                if (strMatBang == "") { return; }

                var objMB = db.mbMatBangs.FirstOrDefault(p => ltMatBang.Contains(p.MaMB.ToString()));
                if ((int?)lkPhanLoai.EditValue == 2)
                    txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, itemLoaiPhieu.SelectedIndex == 0 ? false : true);
                else
                    txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, itemLoaiPhieu.SelectedIndex == 0 ? false : true);
            }
            catch { }
        }

        private void spinDaThu_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as SpinEdit;
            if (item == null) return;
            _conLai = (decimal?)item.EditValue > spinPhaiThu.Value ? 0 : (spinPhaiThu.Value - (decimal?)item.EditValue);
            _thuThua = (decimal?)item.EditValue > spinDaThanhToan.Value ? ((decimal?)item.EditValue - spinDaThanhToan.Value) : 0;

            spinConLai.EditValue = _conLai;
            spinTienThua.EditValue = _thuThua;

            //_daThuBanDau = (decimal?)item.EditValue;
            // 1. chưa chọn checkall, và nhập đã thu  = phải thu
            // 2. đã thu = 0
            // 3. đã thu = phải thu, không có thu thừa
            if ((decimal?)item.EditValue >= spinPhaiThu.Value & ckbSelectAll.Checked == false) { ckbSelectAll.Checked = true; gvHoaDon.SelectAll(); }
            else if ((decimal?)item.EditValue <= 0 & ckbSelectAll.Checked == true) { ckbSelectAll.Checked = false; gvHoaDon.ClearSelection(); }
            else if ((decimal?)item.EditValue == spinPhaiThu.Value) { update_hoa_don(); spinTienThua.EditValue = 0; spSoTien.EditValue = spinPhaiThu.EditValue; }
            //else if ((decimal?)item.EditValue < spinPhaiThu.Value & ckbSelectAll.Checked == true) { ckbSelectAll.Checked = false; gvHoaDon.ClearSelection(); }

            //CapNhatDaThu();
        }

        private void update_hoa_don()
        {
            foreach (var hd in ltData)
            {
                //hd.IsChon = hd.ConNo != 0 & ckbSelectAll.Checked; //& ckbSelectAll.Checked;
                hd.IsChon = true;
                hd.ThucThu = hd.IsChon ? hd.ConNo : 0;
                //hd.ThucThu = hd.IsChon ? hd.ConNo : hd.ThucThu;
                hd.KhachTra = 0;//(hd.ConNo - hd.ThucThu) > 0 ? (hd.ConNo - hd.ThucThu) : 0;
                hd.ThuThua = 0;//hd.ThucThu > hd.ConNo ? (hd.ThucThu - hd.ConNo) : 0;
            }
            gvHoaDon.PostEditor();

            gvHoaDon.RefreshData();
        }

        private int? groupHandle = null;
        private bool isSelectAll = false;
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
                var isChonValue = (bool)view.GetFocusedRowCellValue("IsChon");
                if (checkboxValue != isChonValue) view.SetFocusedRowCellValue("IsChon", checkboxValue);
            }
            return view;
        }

        private DevExpress.XtraGrid.Views.Grid.GridView ChangeGridView(DevExpress.XtraGrid.Views.Grid.GridView view, int handle)
        {
            if (view.GetRowCellValue(handle, "DX$CheckboxSelectorColumn") != null)
            {
                var checkboxValue = (bool) view.GetRowCellValue(handle, "DX$CheckboxSelectorColumn");
                var isChonValue = (bool)view.GetRowCellValue(handle, "IsChon");
                if(checkboxValue!=isChonValue) view.SetRowCellValue(handle, "IsChon", checkboxValue);
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
            if(hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column)
            {
                isSelectAll = true;
                return;
            }
        }

        private void chkMatBang_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            LoadHoaDon();
        }

        private void chkOpenPanel_CheckedChanged(object sender, EventArgs e)
        {
            gvHoaDon.OptionsView.ShowGroupPanel = chkOpenPanel.Checked;
        }

        private void chkOpenFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvHoaDon.OptionsView.ShowAutoFilterRow = chkOpenFilter.Checked;
        }

        private void frmPayment_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmbPTTT_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var strMatBang = (chkMatBang.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltMatBang = strMatBang.Split(',');
                if (strMatBang == "") { return; }

                var objMB = db.mbMatBangs.FirstOrDefault(p => ltMatBang.Contains(p.MaMB.ToString()));
                if (objMB != null)
                {
                    if ((int?)cmbPTTT.SelectedIndex == 1)
                        txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, itemLoaiPhieu.SelectedIndex == 0 ? false : true);
                    else txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, itemLoaiPhieu.SelectedIndex == 0 ? false : true);
                }
            }
            
        }

        private void spTien_EditValueChanged(object sender, EventArgs e)
        {
            
            //hd.KhachTra = hd.ConNo - hd.ThucThu;
        }

        private void itemLoaiPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemLoaiPhieu.SelectedIndex != 0)
            {
                if (spinSoTienThuTruoc.Value <= 0)
                {
                    DialogBox.Error(
                        "Hiện tại khách hàng không có thu trước, nếu vẫn làm khấu trừ thì sẽ không có phiếu thu");
                }
            }
            using (var db = new MasterDataContext())
            {
                var strMatBang = (chkMatBang.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltMatBang = strMatBang.Split(',');
                if (strMatBang == "") { Library.DialogBox.Alert("Vui lòng chọn mặt bằng"); return; }

                var objMB = db.mbMatBangs.FirstOrDefault(p => ltMatBang.Contains(p.MaMB.ToString()));
                txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, itemLoaiPhieu.SelectedIndex == 0 ? false : true);
                if (itemLoaiPhieu.SelectedIndex == 0)
                {
                    gvHoaDon.Columns["KhauTru"].OptionsColumn.ReadOnly = true;
                    gvHoaDon.Columns["KhauTru"].Visible = false;
                    gvHoaDon.Columns["ThucThu"].Visible = true;
                    gvHoaDon.Columns["ThucThu"].VisibleIndex = 5;
                    gvHoaDon.Columns["ThucThu"].OptionsColumn.ReadOnly = false;
                    foreach (var item in ltData)
                    {
                        item.KhauTru = 0;
                        item.KhachTra = item.ConNo;
                        item.ThuThua = 0;
                        item.IsChon = false;
                    }
                }
                else
                {
                    gvHoaDon.Columns["KhauTru"].OptionsColumn.ReadOnly = false;
                    gvHoaDon.Columns["ThucThu"].OptionsColumn.ReadOnly = true;
                    gvHoaDon.Columns["KhauTru"].Visible = true;
                    gvHoaDon.Columns["KhauTru"].VisibleIndex = 6;
                    gvHoaDon.Columns["ThucThu"].Visible = false;
                    foreach (var item in ltData)
                    {
                        item.ThucThu = 0;
                        item.KhachTra = item.ConNo;
                        item.ThuThua = 0;
                        item.IsChon = false;
                    }
                }
                this.UpdateLaiPhatSinh();
            }
        }

    }
    public class ThuTuDienGiai
    {
        public int SoTT { get; set; }
        public string DienGiai { get; set; }
        //1-phí quản lý
        //2-phí bơi
        //3-phí xe
        //4-phí nước
        //5-phí gas
        //6-tiền lãi
    }
    public class PaymentItem
    {
        public bool IsChon { get; set; }
        public long? ID { get; set; }
        public int? MaLDV { get; set; }
        public string TenLDV { get; set; }
        public string MaSoMB { get; set; }
        public string DienGiai { get; set; }
        public decimal? PhiDV { get; set; }
        public DateTime? NgayTT { get; set; }
        public string ThangTT { get; set; }
        public decimal? KyTT { get; set; }
        public decimal? TienTT { get; set; }
        public decimal? TyLeCK { get; set; }
        public decimal? TienCK { get; set; }
        public decimal? PhaiThu { get; set; }
        public decimal? KhachTra { get; set; }
        public decimal? DaThu { get; set; }
        public decimal? ConNo { get; set; }
        public decimal? ThucThu { get; set; }
        public decimal? KhauTru { get; set; }
        public decimal? ThuThua { get; set; }
        public string DonVi { get; set; }
        public string NhomXe { get; set; }
        public int? SoTTDV { get; set; }
        public bool IsThuThua { get; set; }

        public string TableName { get; set; }
    }
}