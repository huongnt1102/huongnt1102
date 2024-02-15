using System.Linq;

namespace LandSoftBuilding.Fund.Class
{
    public static class PhieuChi
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        public static void DeletePhieuChi(int? phieuChiId)
        {
            var pt = Db.pcPhieuChis.Single(p => p.ID ==phieuChiId);
            Db.SoQuy_ThuChis.DeleteAllOnSubmit(Db.SoQuy_ThuChis.Where(p => p.IDPhieu == phieuChiId && p.IsPhieuThu == false));
            Db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(Db.hdctnCongNoNhaCungCaps.Where(_=>_.IsPhieuChi == true & _.PhieuChiId == phieuChiId));
            Db.pcPhieuChis.DeleteOnSubmit(pt);

            try
            {
                Db.SubmitChanges();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }
    }
}
