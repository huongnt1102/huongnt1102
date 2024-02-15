using System.Linq;

namespace Deposit.Class
{
    public static class InHopDong
    {
        private static System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        public static string Print(int? hopDongId, int? formTemplateId, int? groupId, string groupSub, Library.Dep_DoiTac doiTac, string hoTenKhachHang, string matBang)
        {
            try
            {
                var hopDong = Deposit.Class.HopDong.GetHopDongById(hopDongId);
                return hopDongId == null ? "" : GetTemplateHopDong(formTemplateId, hopDong.BuildingId, groupId, groupSub, hopDong.No,
                        hoTenKhachHang, matBang, doiTac.GhiChu);
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi in hợp đồng: " + ex.Message);
                return "";
            }
        }

        private static string GetTemplateHopDong(int? formTemplateId, byte? buildingId, int? groupId, string groupSub, string soHopDong, string hoTenKhachHang, string matBang, string ghiChu)
        {
            var formTempate = BuildingDesignTemplate.Class.Form.GetFormTemplateById(formTemplateId);
            if (formTempate == null) return "";

            var toaNha = BuildingDesignTemplate.Class.Common.GetToaNhaById(buildingId);
            if (toaNha == null) return "";

            var hopDongItem = Deposit.Class.HopDong.SetValueHopDongItem(soHopDong, hoTenKhachHang, matBang,
                toaNha.NguoiDaiDien, toaNha.DiaChiCongTy, ghiChu);

            return BuildingDesignTemplate.Class.Common.GetContents(formTempate.Content, groupId,
                Library.SqlCommon.LINQToDataTable(hopDongItem).Rows[0], groupSub);
        }
    }
}
