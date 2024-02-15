using Library;
using System.Collections.Generic;
using System.Linq;

namespace LandSoftBuilding.Fund.Class
{
    /// <summary>
    /// Xử lý phiếu thu
    /// </summary>
    public static class PhieuThu
    {
        /// <summary>
        /// data
        /// </summary>
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        /// <summary>
        /// Phiếu thu
        /// </summary>
        private static Library.ptPhieuThu pt;

        /// <summary>
        /// Xóa phiếu thu by Id
        /// </summary>
        /// <param name="phieuThuId">Id phiếu thu </param>
        public static void DeletePhieuThu(int? phieuThuId)
        {
            pt = GetPhieuThuById(phieuThuId);
            var check = true;
            if (pt == null) return;
            if (pt.MaPCT != null)
            {
                Library.DialogBox.Alert("Đây là phiếu thu bên chuyển tiền. Vui lòng qua nghiệp vụ chuyển tiền để xóa");
                return;
            }
            if (pt.IDHopDongTL > 0)
            {
                Library.DialogBox.Alert("Đây là phiếu thu bên thanh lý hợp đồng. Vui lòng qua nghiệp vụ khấu trừ để xóa");
                return;
            }
            #region Luu lai phieu thu bi xoa

            if (pt != null)
            {
                LuuPhieuThuDaXoa();

                #region // kiểm tra phiếu thu xem có thu thừa k? hoặc có phải là phiếu thu thừa của phiếu khác k
                // đang tìm phiếu thu thừa, nếu tìm ra phiếu thu thừa
                //var thuThua = _db.ptPhieuThus.FirstOrDefault(_ => _.ThuThuaId == phieuThuId);

                XoaPhieuThuThuaById(phieuThuId);

                //// thu thừa id != null là phiếu thu chính 
                if( XoaThuThuaCuaPhieuChinh()) return;
                // Xóa phiếu thanh lý
                check = XoaPhieuThuThanhLyByHopDongId(pt.IDHopDongTL);
                // hợp đồng chuyển cọc
                if (pt.IdHopDongChuyenCoc > 0)
                {
                    decimal tienChuyenCoc = 0;
                    decimal objSoTien = 0;
                    if (pt.MaPL == 52)
                    {
                        tienChuyenCoc = (pt.SoTien.Value * -1);
                    }
                    using (var db = new MasterDataContext())
                    {
                        var objSoTien1 = db.SoQuy_ThuChis.Where(p => p.MaKH == (int?)pt.MaKH).Select(_ => new { ThuThua = _.ThuThua, KhauTru = _.KhauTru }).ToList();
                        objSoTien = objSoTien1.Sum(s => s.ThuThua.GetValueOrDefault() - s.KhauTru.GetValueOrDefault());

                    }
                    if (objSoTien >= tienChuyenCoc)
                    {
                        check = XoaPhieuThuChuyenTienCocHopDong(pt.IdHopDongChuyenCoc, pt.LanChuyen);
                    }else
                    {
                        Library.DialogBox.Alert("Số tiền chuyển cọc sang thu trước đã được sử dụng, Vui lòng xóa khấu trừ để xóa phiếu thu");
                        return;
                    }
                }
                #endregion
            }

            #endregion

            if (check )
            {
                _db.ptPhieuThus.DeleteOnSubmit(pt);
                //Xóa Sổ quỹ thu chi
                _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == phieuThuId && p.IsPhieuThu == true));
                _db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(_db.hdctnCongNoNhaCungCaps.Where(_ => _.IsPhieuChi == false & _.PhieuThuId == phieuThuId));
            }

            try
            {
                _db.SubmitChanges();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// Lưu lại phiếu thu đã xóa
        /// </summary>
        private static void LuuPhieuThuDaXoa()
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
            _db.SubmitChanges();
            if (queryChiTietPT.Count() > 0)
            {
                foreach (var qe in queryChiTietPT)
                {
                    //var PTDXChiTiet = new Library.ptChiTietPhieuThuDaXoa();
                    //PTDXChiTiet.LinkID = qe.LinkID;
                    //PTDXChiTiet.MaPT = pt.SoPT;
                    //PTDXChiTiet.SoTien = qe.SoTien;
                    //PTDXChiTiet.TableName = qe.TableName;
                    //PTDXChiTiet.DienGiai = qe.DienGiai;
                    //_db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(PTDXChiTiet);
                    try
                    {
                        Library.Class.Connect.QueryConnect.QueryData<bool>("ptChiTietPhieuThuDaXoa_Insert", new { Id = qe.ID });
                    }
                    catch { }
                    
                }

            }
           // _db.SubmitChanges();
        }

        /// <summary>
        /// Xóa phiếu thu thừa bởi id của phiếu thu thừa
        /// </summary>
        /// <param name="id">id phiếu thu thừa</param>
        private static void XoaPhieuThuThuaById(int? id)
        {
            var thuThua = GetPhieuThuThuaById(id);
            if (thuThua != null)
            {
                // Đây là phiếu thu thừa, nếu yes mới xóa phiếu chính
                if (Library.DialogBox.Question("Đây là phiếu thu thừa của phiếu thu: " + pt.SoPT + " .Đồng ý xóa cả 2 phiếu chứ? ") == System.Windows.Forms.DialogResult.Yes)
                {
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == thuThua.ID));
                    _db.ptPhieuThus.DeleteOnSubmit(thuThua);
                }
            }
        }
        /// <summary>
        /// Xóa phiếu thu thừa bởi id của phiếu thu thừa
        /// </summary>
        /// <param name="id">id phiếu thu thừa</param>
        private static bool XoaPhieuThuThanhLyByHopDongId(int? id)
        {
            if (id > 0)
            {
                var thuThanhLy = GetPhieuThuThanhLyByHopDongId(id);
                var hopDong = _db.ctHopDongs.Where(x => x.ID == id).FirstOrDefault();
                var thanhLy = _db.ctThanhLies.Where(x => x.MaHD == id).FirstOrDefault();
                var matBangCu = hopDong.ctChiTiets.GroupBy(x => x.mbMatBang).Select(x => x.Key).ToList();
                var matBang = new List<mbMatBang>();
                using (var db = new MasterDataContext())
                {
                    foreach (var item in matBangCu)
                    {
                        matBang.Add(db.mbMatBangs.Where(x => x.MaMB == item.MaMB).FirstOrDefault());
                    }
                }
                
                if (thuThanhLy != null && thuThanhLy.Count() > 0)
                {
                    if (matBang.Where(x => (x.MaKH != null && x.MaKHF1 != null) ).Count() == 0)
                    {
                        if (Library.DialogBox.Question("Đây là phiếu thu thanh lý của hợp đồng: " + hopDong.SoHDCT + " .Đồng ý xóa tất cả phiếu thanh lý chứ? ") == System.Windows.Forms.DialogResult.Yes)
                        {
                            foreach (var item in thuThanhLy)
                            {
                                _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == item.ID && p.IsPhieuThu == true));
                                _db.ptPhieuThus.DeleteOnSubmit(item);
                            }
                            var phieuChi = _db.pcPhieuChis.Where(x => x.IDHopDongTL == id && x.IsThanhLy == true).FirstOrDefault();
                            if (phieuChi != null)
                            {
                                _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == phieuChi.ID && p.IsPhieuThu == false));
                                _db.pcPhieuChis.DeleteOnSubmit(phieuChi);
                            }
                            if (thanhLy != null)
                            {
                                _db.ctThanhLies.DeleteOnSubmit(thanhLy);
                            }
                            foreach (var item in matBang)
                            {
                                item.MaKH = item.MaKHSauTL;
                                item.MaKHF1 = item.MaKHSauTL;
                            }
                            return true;
                        }
                        else
                        {
                            return false;

                        }
                    }
                    else
                    {
                        Library.DialogBox.Alert("Không thể xóa mặt bằng của hợp đồng đã được cho thuê!");
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Xóa phiếu thu chuyển cọc hợp đồng
        /// </summary>
        /// <param name="id">id phiếu hợp đồng</param>
        private static bool XoaPhieuThuChuyenTienCocHopDong(int? id, int? lanChuyen)
        {
            if (id > 0)
            {
                var thuChuyenCoc = GetPhieuThuChuyenTienCocHopDong(id, lanChuyen);
                var hopDong = _db.ctHopDongs.Where(x => x.ID == id).FirstOrDefault();
                if (thuChuyenCoc != null && thuChuyenCoc.Count() > 0)
                {
                    if (Library.DialogBox.Question("Đây là phiếu thu chuyển cọc của hợp đồng: " + hopDong.SoHDCT + " .Đồng ý xóa tất cả phiếu thu liên quan chuyển cọc chứ? ") == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (var item in thuChuyenCoc)
                        {
                            _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == item.ID && p.IsPhieuThu == true));
                            _db.ptPhieuThus.DeleteOnSubmit(item);
                        }
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Xóa hết phiếu thu thừa của phiếu này
        /// </summary>
        /// <returns></returns>
        private static bool XoaThuThuaCuaPhieuChinh()
        {
            if (pt.ThuThuaId != null)
            {
                // đây là phiếu thu chính, xóa hết phiếu thu thừa
                // Kiểm tra phiếu thu thừa
                var getThuThua = GetPhieuThuById(pt.ThuThuaId);
                if (getThuThua != null)
                {
                    if (Library.DialogBox.Question("Đây là phiếu thu có đính kèm phiếu thu thừa: " + getThuThua.SoPT + " .Đồng ý xóa cả 2 phiếu chứ? ") == System.Windows.Forms.DialogResult.No) return true;
                    _db.ptPhieuThus.DeleteAllOnSubmit(_db.ptPhieuThus.Where(_ => _.ID == pt.ThuThuaId));
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == pt.ThuThuaId));
                }
            }
            return false;
        }

        /// <summary>
        /// Get phiếu thu by id
        /// </summary>
        /// <param name="id">id phiếu thu</param>
        /// <returns></returns>
        private static Library.ptPhieuThu GetPhieuThuById(int? id)
        {
            return _db.ptPhieuThus.FirstOrDefault(_ => _.ID == id);
        }

        /// <summary>
        /// Get phiếu thu thừa bởi id thu thừa
        /// </summary>
        /// <param name="id">id thu thừa</param>
        /// <returns></returns>
        private static Library.ptPhieuThu GetPhieuThuThuaById(int? id)
        {
            return _db.ptPhieuThus.FirstOrDefault(_ => _.ThuThuaId == id);
        }
        /// <summary>
        /// Get phiếu thu thanh lý bởi id hợp đồng
        /// </summary>
        /// <param name="id">id hợp đồng</param>
        /// <returns></returns>
        private static List<Library.ptPhieuThu> GetPhieuThuThanhLyByHopDongId(int? id)
        {
            return _db.ptPhieuThus.Where(_ => _.IDHopDongTL == id && _.IsThanhLy == true).ToList();
        }
        private static List<Library.ptPhieuThu> GetPhieuThuChuyenTienCocHopDong(int? id, int? lanChuyen)
        {
            return _db.ptPhieuThus.Where(_ => _.IdHopDongChuyenCoc == id && _.LanChuyen == lanChuyen).ToList();
        }
    }
}
