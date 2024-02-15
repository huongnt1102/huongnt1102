using System.Linq;

namespace LandSoftBuilding.Fund.Class
{
    public static class PhieuThu
    {
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        public static void DeletePhieuThu(int? phieuThuId)
        {
            var pt = _db.ptPhieuThus.Single(p => p.ID == phieuThuId);
            if (pt.MaPCT != null)
            {
                Library.DialogBox.Alert("Đây là phiếu thu bên chuyển tiền. Vui lòng qua nghiệp vụ chuyển tiền để xóa");
                return;
            }
            #region Luu lai phieu thu bi xoa

            if (pt != null)
            {
                var PTDX = new Library.ptPhieuThuDaXoa();
                PTDX.LyDo = pt.LyDo;
                PTDX.MaKH = pt.MaKH;
                PTDX.MaNV = pt.MaNV;
                PTDX.MaNVN = Library.Common.User.MaNV;
                // PTDX.MaNVS = pt.MaNVS;
                PTDX.MaPL = pt.MaPL;
                PTDX.MaTKNH = pt.MaTKNH;
                PTDX.MaTN = pt.MaTN;
                PTDX.NguoiNop = pt.NguoiNop;
                PTDX.NgayNhap = System.DateTime.Now;

                PTDX.NgayThu = pt.NgayThu;
                //PTDX.NgaySua = pt.NgaySua;
                PTDX.SoPT = pt.SoPT;
                PTDX.SoTien = pt.SoTien;
                PTDX.ChungTuGoc = pt.ChungTuGoc;
                PTDX.DiaChiNN = pt.DiaChiNN;
                _db.ptPhieuThuDaXoas.InsertOnSubmit(PTDX);
                var queryChiTietPT = _db.ptChiTietPhieuThus.Where(p => p.MaPT == pt.ID).ToList();
                if (queryChiTietPT.Count() > 0)
                {
                    foreach (var qe in queryChiTietPT)
                    {
                        var PTDXChiTiet = new Library.ptChiTietPhieuThuDaXoa();
                        PTDXChiTiet.LinkID = qe.LinkID;
                        PTDXChiTiet.MaPT = pt.SoPT;
                        PTDXChiTiet.SoTien = qe.SoTien;
                        PTDXChiTiet.TableName = qe.TableName;
                        PTDXChiTiet.DienGiai = qe.DienGiai;
                        _db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(PTDXChiTiet);
                    }

                }

                #region // kiểm tra phiếu thu xem có thu thừa k? hoặc có phải là phiếu thu thừa của phiếu khác k
                var thuThua = _db.ptPhieuThus.FirstOrDefault(_ => _.ThuThuaId == phieuThuId);
                if (thuThua!=null)
                {
                    if (Library.DialogBox.Question("Đây là phiếu thu thừa của phiếu thu: " + pt.SoPT + " .Đồng ý xóa cả 2 phiếu chứ? ") == System.Windows.Forms.DialogResult.No) return;
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == thuThua.ID));
                    _db.ptPhieuThus.DeleteOnSubmit(thuThua);
                }
                if (pt.ThuThuaId!=null)
                {
                    if (Library.DialogBox.Question("Đây là phiếu thu thừa của phiếu thu: " + pt.SoPT + " .Đồng ý xóa cả 2 phiếu chứ? ") == System.Windows.Forms.DialogResult.No) return;
                    _db.ptPhieuThus.DeleteAllOnSubmit(_db.ptPhieuThus.Where(_ => _.ID == pt.ThuThuaId));
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == pt.ThuThuaId));
                }
                
                #endregion
            }

            #endregion
            

            _db.ptPhieuThus.DeleteOnSubmit(pt);
            //Xóa Sổ quỹ thu chi
            _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == phieuThuId && p.IsPhieuThu == true));
            _db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(_db.hdctnCongNoNhaCungCaps.Where(_=>_.IsPhieuChi == false & _.PhieuThuId == phieuThuId));

            try
            {
                _db.SubmitChanges();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }
    }
}
