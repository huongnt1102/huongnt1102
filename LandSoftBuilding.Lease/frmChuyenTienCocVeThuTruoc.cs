using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Lease
{
    public partial class frmChuyenTienCocVeThuTruoc : DevExpress.XtraEditors.XtraForm
    {
        public int? MaHD { set; get; }
        public byte? MaTN { set; get; }
        ctHopDong hopDong;
        tnToaNha toaNha;
        MasterDataContext db = new MasterDataContext();
        public frmChuyenTienCocVeThuTruoc()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (spTongTienCoc.Value <= 0)
            {
                DialogBox.Error("Tổng số tiền cọc phải lớn hơn 0 !!");
                return;
            }
            if (spTongTienCoc.Value < spGiaTriChuyenVeThuTruoc.Value)
            {
                DialogBox.Error("Tổng số tiền cọc phải lớn hơn số tiền cần chuyển sang thu trước !!");
                return;
            }
            if ((int?)glkCompanyCode.EditValue <= 0 || (int?)glkCompanyCode.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập company code !!");
                return;
            }
            // phiếu thu âm
            var khachHang = db.tnKhachHangs.Where(x => x.MaKH == hopDong.MaKH).FirstOrDefault();
            var matBang = db.mbMatBangs.Where(x => x.MaKH == khachHang.MaKH).FirstOrDefault();
            var ngayPhieu = deNgayPhieu.DateTime;
            #region phiếu thu âm
            int soLan = 1;
            var soQuy = db.ptPhieuThus.Where(x => x.IdHopDongChuyenCoc == hopDong.ID).Max(x => x.LanChuyen);
            if (soQuy != null)
                soLan = soQuy.Value + 1;
            var soPhieuThuAm = Common.GetPayNumber(0, MaTN, null);
            ptPhieuThu _objPtam = new ptPhieuThu();
            _objPtam.MaTN = this.MaTN;
            _objPtam.SoPT = soPhieuThuAm;
            _objPtam.NgayThu = ngayPhieu;
            _objPtam.SoTien = -spGiaTriChuyenVeThuTruoc.Value;
            _objPtam.MaNV = Common.User.MaNV;
            _objPtam.MaPL = 52;
            _objPtam.IsThanhLy = false;
            _objPtam.MaNVN = Library.Common.User.MaNV;
            _objPtam.NgayNhap = DateTime.UtcNow.AddHours(7);
            _objPtam.NguoiNop = khachHang.TenKH;
            _objPtam.MaKH = khachHang.MaKH;
            _objPtam.MaMB = matBang != null ? (int?)matBang.MaMB : null;
            _objPtam.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
            _objPtam.MaTKNH = null;
            _objPtam.MaHTHT = 1;
            _objPtam.LanChuyen = soLan;
            _objPtam.IdHopDongChuyenCoc = hopDong.ID;
            _objPtam.HinhThucThanhToanId = 1;
            _objPtam.HinhThucThanhToanName = "Tiền mặt";
            _objPtam.LyDo = "Thu âm cho khách hàng tiền cọc chuyển thu trước";
            _objPtam.IsKhauTru = false;
            db.ptPhieuThus.InsertOnSubmit(_objPtam);
            var ctpt = new ptChiTietPhieuThu();
            ctpt.LinkID = null;
            ctpt.DienGiai = "Thu âm cho khách hàng tiền cọc chuyển thu trước";
            ctpt.SoTien = -spGiaTriChuyenVeThuTruoc.Value;
            ctpt.ThuThua = 0;
            ctpt.KhauTru = 0;
            ctpt.PhaiThu = 0;
            ctpt.CompanyCode = (int?)glkCompanyCode.EditValue;
            _objPtam.ptChiTietPhieuThus.Add(ctpt);
            db.SubmitChanges();
            Common.SoQuy_InsertCompanyCode(db, ngayPhieu.Month, ngayPhieu.Year, this.MaTN, hopDong.MaKH, null, _objPtam.ID, ctpt.ID, ngayPhieu, soPhieuThuAm, 0,52, true, 0, -spGiaTriChuyenVeThuTruoc.Value, 0, 0, null, null, "Thu âm cho khách hàng tiền cọc chuyển thu trước", Common.User.MaNV, false, false, (int?)glkCompanyCode.EditValue);
            #endregion kết thúc phiếu thu âm
            // phiếu thu trước
            #region phiếu thu trước
            var soPhieuThuTruoc = Common.GetPayNumber(0, MaTN, null);
            ptPhieuThu _objPTTruoc = new ptPhieuThu();
            _objPTTruoc.MaTN = this.MaTN;
            _objPTTruoc.SoPT = soPhieuThuTruoc;
            _objPTTruoc.NgayThu = ngayPhieu;
            _objPTTruoc.SoTien = spGiaTriChuyenVeThuTruoc.Value;
            _objPTTruoc.MaNV = Common.User.MaNV;
            _objPTTruoc.MaPL = 2;
            _objPTTruoc.IsThanhLy = false;
            _objPTTruoc.MaNVN = Library.Common.User.MaNV;
            _objPTTruoc.NgayNhap = DateTime.UtcNow.AddHours(7);
            _objPTTruoc.NguoiNop = khachHang.TenKH;
            _objPTTruoc.DiaChiNN = khachHang.KyHieu + " - " + toaNha.TenTN;
            _objPTTruoc.MaTKNH = null;
            _objPTTruoc.MaHTHT = 1;
            _objPTTruoc.IdHopDongChuyenCoc = hopDong.ID;
            _objPTTruoc.IsKhauTruTuDong = false;
            _objPTTruoc.MaKH = khachHang.MaKH;
            _objPTTruoc.LanChuyen = soLan;
            _objPTTruoc.MaMB = matBang != null ? (int?)matBang.MaMB : null;
            _objPTTruoc.HinhThucThanhToanId = 1;
            _objPTTruoc.HinhThucThanhToanName = "Tiền mặt";
            _objPTTruoc.LyDo = "Thu trước cho khách hàng chuyển tiền cọc";
            _objPTTruoc.IsKhauTru = false;
            db.ptPhieuThus.InsertOnSubmit(_objPTTruoc);
            var ctptt = new ptChiTietPhieuThu();
            ctptt.LinkID = null;
            ctptt.DienGiai = "Thu trước cho khách hàng chuyển tiền cọc";
            ctptt.SoTien = spGiaTriChuyenVeThuTruoc.Value;
            ctptt.ThuThua = spGiaTriChuyenVeThuTruoc.Value;
            ctptt.KhauTru = 0;
            ctptt.PhaiThu = 0;
            ctptt.CompanyCode = (int?)glkCompanyCode.EditValue;
            _objPTTruoc.ptChiTietPhieuThus.Add(ctptt);
            db.SubmitChanges();
            Common.SoQuy_InsertCompanyCode(db, ngayPhieu.Month, ngayPhieu.Year, this.MaTN, hopDong.MaKH, null, _objPTTruoc.ID, ctptt.ID, ngayPhieu, soPhieuThuAm, 0, 2, true, 0, 0, spGiaTriChuyenVeThuTruoc.Value, 0, null, null, "Thu trước kèm phiếu thu: " + soPhieuThuAm + " cho khách hàng chuyển tiền cọc", Common.User.MaNV, false, false, (int?)glkCompanyCode.EditValue);
            #endregion kết thúc phiếu thu trước
            DialogBox.Success("Chuyển tiền cọc về thu trước thành công!!");
            this.Close();
        }

        private void frmChuyenTienCocVeThuTruoc_Load(object sender, EventArgs e)
        {
            hopDong = db.ctHopDongs.Where(x => x.ID == this.MaHD).FirstOrDefault();
            toaNha = db.tnToaNhas.Where(x => x.MaTN == this.MaTN).FirstOrDefault();
            var listMB = new List<int>();
            deNgayPhieu.DateTime = DateTime.Now;
            
            foreach (var item in hopDong.ctChiTiets.GroupBy(x => x.MaMB).Select(x => x.Key.Value).ToList())
            {
                listMB.Add(item);
            }
            glkCompanyCode.Properties.DataSource = db.CompanyCodes;
            var tongTienCocDaThu = (from pt in db.SoQuy_ThuChis
                                    join hd in db.dvHoaDons on pt.LinkID equals (long?)hd.ID into hoadon
                                    from hd in hoadon.DefaultIfEmpty()
                                    where pt.MaKH == hopDong.MaKH && listMB.Contains(pt.MaMB.Value) && (pt.MaLoaiPhieu == 49 || (pt.MaLoaiPhieu == 1 && pt.TableName == "dvHoaDon" && hd.MaLDV == 3 && hd.TableName == "ctLichThanhToan"))
                                    select new
                                    {
                                        pt.DaThu
                                    }).Sum(x => x.DaThu);
            var daThu = tongTienCocDaThu >= 0 ? tongTienCocDaThu : 0;
            var tongTienCocDaChuyenThu = (from pt in db.SoQuy_ThuChis
                                          join ptt in db.ptPhieuThus on pt.IDPhieu equals ptt.ID into phieuthu
                                          from ptt in phieuthu.DefaultIfEmpty()
                                          where pt.MaKH == hopDong.MaKH && ptt.IdHopDongChuyenCoc == hopDong.ID && (pt.MaLoaiPhieu == 52)
                                          select new
                                          {
                                              DaThu = -pt.DaThu,
                                          }).Sum(x => x.DaThu);
            var daTru = tongTienCocDaChuyenThu >= 0 ? tongTienCocDaChuyenThu : 0;
            spTongTienCoc.EditValue = daThu - daTru;
            spGiaTriChuyenVeThuTruoc.EditValue = daThu - daTru;
        }
    }
}