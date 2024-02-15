using System.Collections.Generic;
using System.Linq;

namespace LandSoftBuilding.Fund.Class
{
    public static class PhieuChi
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        public static void DeletePhieuChi(int? phieuChiId)
        {
            var pt = Db.pcPhieuChis.Single(p => p.ID ==phieuChiId);
            var check = XoaPhieuChiThanhLyByHopDongId(pt.IDHopDongTL);
            if (check)
            {
                Db.SoQuy_ThuChis.DeleteAllOnSubmit(Db.SoQuy_ThuChis.Where(p => p.IDPhieu == phieuChiId && p.IsPhieuThu == false));
                Db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(Db.hdctnCongNoNhaCungCaps.Where(_ => _.IsPhieuChi == true & _.PhieuChiId == phieuChiId));
                // Xóa phiếu thanh lý
                Db.pcPhieuChis.DeleteOnSubmit(pt);
            }
            

            try
            {
                Db.SubmitChanges();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }
        private static bool XoaPhieuChiThanhLyByHopDongId(int? id)
        {
            if (id > 0)
            {
                var ChiThanhLy = GetPhieuChiThanhLyByHopDongId(id);
                var ThuThanhLy = GetPhieuThuThanhLyByHopDongId(id);
                var hopDong = Db.ctHopDongs.Where(x => x.ID == id).FirstOrDefault();
                var thanhLy = Db.ctThanhLies.Where(x => x.MaHD == id).FirstOrDefault();
                var matBang = hopDong.ctChiTiets.GroupBy(x => x.mbMatBang).Select(x => x.Key).ToList();
                if (ChiThanhLy != null && ChiThanhLy.Count() > 0)
                {
                    if (matBang.Where(x => x.MaKH != null && x.MaKHF1 != null).Count() == 0)
                    {
                        if (Library.DialogBox.Question("Đây là phiếu Chi thanh lý của hợp đồng: " + hopDong.SoHDCT + " .Đồng ý xóa tất cả phiếu thanh lý chứ? ") == System.Windows.Forms.DialogResult.Yes)
                        {
                            foreach (var item in ThuThanhLy)
                            {
                                Db.SoQuy_ThuChis.DeleteAllOnSubmit(Db.SoQuy_ThuChis.Where(p => p.IDPhieu == item.ID && p.IsPhieuThu == true));
                                Db.ptPhieuThus.DeleteOnSubmit(item);
                            }
                            foreach (var item in ChiThanhLy)
                            {
                                Db.SoQuy_ThuChis.DeleteAllOnSubmit(Db.SoQuy_ThuChis.Where(p => p.IDPhieu == item.ID && p.IsPhieuThu == false));
                                Db.pcPhieuChis.DeleteOnSubmit(item);
                            }
                            if (thanhLy != null)
                            {
                                Db.ctThanhLies.DeleteOnSubmit(thanhLy);
                            }
                            foreach (var item in matBang)
                            {
                                item.MaKH = item.MaKHSauTL;
                                item.MaKHF1 = item.MaKHSauTL;
                            }
                            return true;
                        }else
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
            }else
            {
                return true;
            }

        }
        private static List<Library.ptPhieuThu> GetPhieuThuThanhLyByHopDongId(int? id)
        {
            return Db.ptPhieuThus.Where(_ => _.IDHopDongTL == id && _.IsThanhLy == true).ToList();
        }
        private static List<Library.pcPhieuChi> GetPhieuChiThanhLyByHopDongId(int? id)
        {
            return Db.pcPhieuChis.Where(_ => _.IDHopDongTL == id && _.IsThanhLy == true).ToList();
        }
    }
}
