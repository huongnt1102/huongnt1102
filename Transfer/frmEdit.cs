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

namespace LandSoftBuilding.Fund.Transfer
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }
        public int? MaCK { get; set; }
        public byte? MaTN { get; set; }
        public bool IsSave { get; set; }
        MasterDataContext db = new MasterDataContext();
        PhieuChuyenTien objPCT;
        List<PhieuThuSelectClss> ltData;
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            var objKH = (from kh in db.tnKhachHangs
                         where kh.MaTN == this.MaTN
                         orderby kh.KyHieu descending
                         select new
                         {
                             kh.MaKH,
                             kh.KyHieu,
                             TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                             DiaChi = kh.DCLL
                         }).ToList();
            glkKhachHangChuyen.Properties.DataSource = objKH;
            glkKhachHangNhanTien.Properties.DataSource = objKH;
            dtNgayChuyen.EditValue = DateTime.UtcNow.AddHours(7);
            if (MaCK == null)
            {
                objPCT = new PhieuChuyenTien();
                txtSoChungTu.EditValue = Common.CreatePhieuChuyenTien(dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, this.MaTN.Value);
            }
            else
            {
                objPCT = db.PhieuChuyenTiens.FirstOrDefault(p => p.ID == this.MaCK);
                dtNgayChuyen.EditValue = objPCT.NgayCT;
                txtSoChungTu.EditValue = objPCT.SoCT;
                glkKhachHangChuyen.EditValue = objPCT.MaKHCT;
                glkKhachHangNhanTien.EditValue = objPCT.MaKHNhanTien;
                spinSoTienNhan.EditValue = objPCT.SoTienChuyen;
                txtDienGiai.EditValue = objPCT.DienGiai;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSoChungTu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập số chứng từ chuyển tiền!");
                return;
            }
            if (glkKhachHangChuyen.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng cần chuyển tiền!");
                return;
            }
            if (glkKhachHangNhanTien.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng cần nhận tiền!");
                return;
            }
            if (spinSoTienNhan.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập số tiền cần chuyển!");
                return;
            }
            if (Convert.ToDecimal(spinSoTienNhan.EditValue) <= 0)
            {
                DialogBox.Alert("Vui lòng nhập số tiền cần chuyển!");
                return;
            }
            if (Convert.ToDecimal(spinSoTienTon.EditValue) < Convert.ToDecimal(spinSoTienNhan.EditValue))
            {
                DialogBox.Alert("Số tiền chuyển không thể lớn hơn số tiền của khách hàng!");
                return;
            }

            if (MaCK == null)
            {
                var objKTCT = db.PhieuChuyenTiens.FirstOrDefault(p => p.SoCT.Equals(txtSoChungTu.Text.Trim()));
                if (objKTCT != null)
                {
                    txtSoChungTu.EditValue = Common.CreatePhieuChuyenTien(dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, this.MaTN.Value);
                }
            }
            else
            {
                var objKTCT = db.PhieuChuyenTiens.FirstOrDefault(p => p.SoCT.Equals(txtSoChungTu.Text.Trim()) && p.ID != MaCK);
                if (objKTCT != null)
                {
                    txtSoChungTu.EditValue = Common.CreatePhieuChuyenTien(dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, this.MaTN.Value);
                }
            }
            objPCT.NgayCT = (DateTime?)dtNgayChuyen.EditValue;
            objPCT.SoCT = txtSoChungTu.Text;
            objPCT.MaKHCT = (int?)glkKhachHangChuyen.EditValue;
            objPCT.MaKHNhanTien = (int?)glkKhachHangNhanTien.EditValue;
            objPCT.SoTienChuyen = (decimal?)spinSoTienNhan.EditValue;
            objPCT.DienGiai = txtDienGiai.Text;
            objPCT.MaTN = this.MaTN;
            if (MaCK == null)
            {
                objPCT.NgayNhap = DateTime.UtcNow.AddHours(7);
                objPCT.NguoiNhap = Common.User.MaNV;
                db.PhieuChuyenTiens.InsertOnSubmit(objPCT);
            }
            else
            {
                objPCT.NgaySua = DateTime.UtcNow.AddHours(7);
                objPCT.NguoiSua = Common.User.MaNV;
                //Xóa phiếu 
                foreach (var item in db.ptPhieuThus.Where(pt => pt.MaPCT == MaCK))
                {
                    db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == item.ID));
                }
                db.ptPhieuThus.DeleteAllOnSubmit(db.ptPhieuThus.Where(pt => pt.MaPCT == MaCK));
            }
            foreach (var item in ltData)
            {
                if (item.Check)
                {
                    var pt = new PhieuChuyenTien_PhieuThu();
                    pt.MaPT = item.ID;
                    objPCT.PhieuChuyenTien_PhieuThus.Add(pt);
                }
            }
            db.SubmitChanges();
            #region Phiếu thu khấu trừ
            //Tạo phiếu thu khấu trừ cho trường hợp chuyển tiền này
            var objPT = new ptPhieuThu();
            objPT.SoPT = Common.CreatePhieuThu(0, dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, 0, this.MaTN.Value, true);
            objPT.NgayThu = (DateTime)dtNgayChuyen.EditValue;
            objPT.SoTien = (decimal?)spinSoTienNhan.EditValue;
            objPT.MaNV = Common.User.MaNV;
            objPT.MaPL = (int?)1;
            objPT.MaTN = this.MaTN;
            //thong tin chung
            objPT.MaKH = (int?)glkKhachHangChuyen.EditValue;
            objPT.MaMB = (int?)null;
            objPT.NguoiNop = glkKhachHangChuyen.Text;
            objPT.DiaChiNN = "";
            objPT.MaPCT = objPCT.ID;
            //if (lkTaiKhoanNganHang.EditValue != null)
            //    objPT.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
            //else
            objPT.MaTKNH = null;
            objPT.ChungTuGoc = "";
            objPT.LyDo = "Chuyển tiền";
            objPT.NgayNhap = DateTime.UtcNow.AddHours(7);
            objPT.MaNVN = Common.User.MaNV;
            objPT.IsKhauTru = true;
            db.ptPhieuThus.InsertOnSubmit(objPT);
            //Thêm chi tiết phiếu thu
            var objCTPT = new ptChiTietPhieuThu();
            objCTPT.DienGiai = "Khấu trừ cho phiếu chuyển tiền";
            objCTPT.KhauTru = (decimal?)spinSoTienNhan.EditValue;
            objCTPT.PhaiThu = 0;
            objCTPT.SoTien = 0;
            objCTPT.ThuThua = 0;
            objPT.ptChiTietPhieuThus.Add(objCTPT);
            db.SubmitChanges();
            foreach (var hd in objPT.ptChiTietPhieuThus)
            {
                int? iPL = (int?)objPT.MaPL;
                Common.SoQuy_Insert(db, dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, this.MaTN, (int)glkKhachHangChuyen.EditValue, (int?)null, hd.MaPT, hd.ID, dtNgayChuyen.DateTime, objPT.SoPT, 0, (int?)objPT.MaPL, true, hd.PhaiThu.GetValueOrDefault(), hd.SoTien.GetValueOrDefault(), hd.ThuThua.GetValueOrDefault(), hd.KhauTru.GetValueOrDefault(), hd.LinkID, hd.TableName, hd.DienGiai, Common.User.MaNV, true);
            }
            #endregion
            #region Phiếu thu trước cho khách hàng chuyển tới
            //Tạo phiếu thu trước cho trường hợp chuyển tiền này
            var objPTTruoc = new ptPhieuThu();
            objPTTruoc.SoPT = Common.CreatePhieuThu(0, dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, 0, this.MaTN.Value, true);
            objPTTruoc.NgayThu = (DateTime)dtNgayChuyen.EditValue;
            objPTTruoc.SoTien = (decimal?)spinSoTienNhan.EditValue;
            objPTTruoc.MaNV = Common.User.MaNV;
            objPTTruoc.MaPL = (int?)2;
            objPTTruoc.MaTN = this.MaTN;
            //thong tin chung
            objPTTruoc.MaKH = (int?)glkKhachHangNhanTien.EditValue;
            objPTTruoc.MaMB = (int?)null;
            objPTTruoc.NguoiNop = glkKhachHangNhanTien.Text;
            objPTTruoc.DiaChiNN = "";
            objPTTruoc.MaPCT = objPCT.ID;
            //if (lkTaiKhoanNganHang.EditValue != null)
            //    objPT.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
            //else
            objPTTruoc.MaTKNH = null;
            objPTTruoc.ChungTuGoc = "";
            objPTTruoc.LyDo = "Nhận chuyển tiền";
            objPTTruoc.NgayNhap = DateTime.UtcNow.AddHours(7);
            objPTTruoc.MaNVN = Common.User.MaNV;
            objPTTruoc.IsKhauTru = false;
            db.ptPhieuThus.InsertOnSubmit(objPTTruoc);
            //Thêm chi tiết phiếu thu
            var objCTPTTruoc = new ptChiTietPhieuThu();
            objCTPTTruoc.DienGiai = "Thu trước cho phiếu chuyển tiền";
            objCTPTTruoc.KhauTru = 0;
            objCTPTTruoc.PhaiThu = 0;
            objCTPTTruoc.SoTien = (decimal?)spinSoTienNhan.EditValue;
            objCTPTTruoc.ThuThua = (decimal?)spinSoTienNhan.EditValue;
            objPTTruoc.ptChiTietPhieuThus.Add(objCTPTTruoc);
            db.SubmitChanges();
            foreach (var hd in objPTTruoc.ptChiTietPhieuThus)
            {
                int? iPL = (int?)2;
                Common.SoQuy_Insert(db, dtNgayChuyen.DateTime.Month, dtNgayChuyen.DateTime.Year, this.MaTN, (int)glkKhachHangNhanTien.EditValue, (int?)null, hd.MaPT, hd.ID, dtNgayChuyen.DateTime, objPTTruoc.SoPT, 0, (int?)2, true, hd.PhaiThu.GetValueOrDefault(), hd.SoTien.GetValueOrDefault(), iPL == 2 ? hd.SoTien.GetValueOrDefault() : hd.ThuThua.GetValueOrDefault(), hd.KhauTru.GetValueOrDefault(), hd.LinkID, hd.TableName, hd.DienGiai, Common.User.MaNV, false);
            }
            #endregion

            this.IsSave = true;
            this.Close();
        }

        private void glkKhachHangChuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (glkKhachHangChuyen.EditValue != null)
            {
                var obj = db.SoQuy_ThuChis.Where(p => p.MaTN == this.MaTN && p.MaKH == (int?)glkKhachHangChuyen.EditValue && p.IsPhieuThu == true).Sum(s => s.ThuThua - s.KhauTru).GetValueOrDefault();
                if (MaCK == null)
                {
                    spinSoTienTon.EditValue = obj;
                }
                else
                {
                    var objP = db.PhieuChuyenTiens.FirstOrDefault(p => p.ID == MaCK);
                    if (objP != null)
                    {
                        spinSoTienTon.EditValue = obj + objP.SoTienChuyen.GetValueOrDefault();
                    }
                }
                ltData = (from pt in db.ptPhieuThus
                                   where !(from pct in db.PhieuChuyenTiens
                                           join ptct in db.PhieuChuyenTien_PhieuThus on pct.ID equals ptct.PhieuChuyenTienID
                                           where pct.MaKHCT == (int?)glkKhachHangChuyen.EditValue
                                           select ptct.MaPT).Contains(pt.ID) && pt.MaKH ==(int?) glkKhachHangChuyen.EditValue && pt.MaPL==2
                                   select new PhieuThuSelectClss
                                   { 
                                       NgayThu= pt.NgayThu,
                                       ID= pt.ID, 
                                       SoPhieu = pt.SoPT, 
                                       SoTien=pt.SoTien, 
                                       Check = false 
                                   }).ToList();
                gcPhieuThu.DataSource = ltData;
            }
        }

        private void grvPhieuThu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grvPhieuThu.PostEditor();
            grvPhieuThu.UpdateCurrentRow();
            grvPhieuThu.RefreshData();
            TinhSoTien();
        }
        void TinhSoTien()
        {
            spinSoTienNhan.EditValue = ltData.Where(p => p.Check == true).Sum(p => p.SoTien);
        }

    }
    public class PhieuThuSelectClss
    {
        public DateTime? NgayThu { set; get; }
        public int? ID { set; get; }
        public string SoPhieu { set; get; }
        public decimal? SoTien { set; get; }
        public bool Check { set; get; }
    }
}