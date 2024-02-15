using System.Linq;

namespace BuildingDesignTemplate.Class
{
    public static class HopDongThueNgoai
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();
        private const string GroupSubChung = "CHUNG";
        private const string GroupChiTiet = "CNHD";

        public static string PrintHopDong(int? hopDongId, int? formTemplateId, int? groupId, string groupSub)
        {
            try
            {
                var hopDong = GetHopDongById(hopDongId);
                return hopDongId == null ? "" : GetHopDongTemplate(formTemplateId, hopDong.MaToaNha, hopDong.SoHopDong, groupId, groupSub);
            }
            catch (System.Exception ex)
            { 
                Library.DialogBox.Error("Lỗi in hợp đồng: " + ex.Message);
                return "";
            }
        }

        private static string GetHopDongTemplate(int? formTemplateId, string buildingId, string soHopDong, int? groupId, string groupSub)
        {
            var formTemplate = BuildingDesignTemplate.Class.Form.GetFormTemplateById(formTemplateId);
            if (formTemplate == null) return "";

            var toaNha = BuildingDesignTemplate.Class.Common.GetToaNhaById(buildingId);
            if (toaNha == null) return "";

            var lhdtn = GetListHopDongThueNgoai(soHopDong, toaNha.DiaChi, toaNha.CongTyQuanLy, toaNha.NguoiDaiDien, toaNha.TenTN);

            return BuildingDesignTemplate.Class.Common.GetContents(formTemplate.Content, groupId, Library.SqlCommon.LINQToDataTable(lhdtn).Rows[0], groupSub);
        }

        private static System.Collections.Generic.List<HopDongThueNgoaiClass> GetListHopDongThueNgoai(string soHopDong, string diaChi, string congTyQuanLy, string nguoiDaiDien, string tenToaNha)
        {
            return new System.Collections.Generic.List<HopDongThueNgoaiClass>
            {
                new HopDongThueNgoaiClass
                {
                    SoHopDong = soHopDong, Ngay = System.DateTime.Now.Day.ToString(),
                    Thang = System.DateTime.Now.Month.ToString(), Nam = System.DateTime.Now.Year.ToString(),
                    Tai = diaChi, BenA = congTyQuanLy, DaiDienBenA = nguoiDaiDien,
                    TenToaNha = tenToaNha
                }
            }.ToList();
        }

        private static Library.hdctnDanhSachHopDongThueNgoai GetHopDongById(int? hopDongId)
        {
            return Db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == hopDongId);
        }

        public static string GetContentCongNoNcc(string contents, string buildingId, int? groupId)
        {
            var building = BuildingDesignTemplate.Class.Common.GetToaNhaById(buildingId);
            if (building == null) return contents;

            contents = MergeCongNoTableCnhd(contents, building.MaTN, groupId);
            contents = MergeCongNoChung(contents, building.TenTN, groupId);

            return contents;
        }

        private static string MergeCongNoChung(string contents, string buildingName, int? groupId)
        {
            System.Collections.Generic.List<CongNoChung> congNoChung = CreateCongNoChung(buildingName);
            return BuildingDesignTemplate.Class.Common.GetContents(contents, groupId, Library.SqlCommon.LINQToDataTable(congNoChung).Rows[0],GroupSubChung);
        }

        private static System.Collections.Generic.List<CongNoChung> CreateCongNoChung(string buildingName)
        {
            return new System.Collections.Generic.List<CongNoChung>{ new CongNoChung{ Nam = System.DateTime.Now.Year.ToString(), Thang = System.DateTime.Now.Month.ToString(), TenToaNha = buildingName}};
        }

        private static string MergeCongNoTableCnhd(string contents, byte? buildingId, int? groupId)
        {
            // merge báo cáo công nợ hợp đồng
            var list1 = (from hd in Db.hdctnDanhSachHopDongThueNgoais 
                    join ncc in Db.tnKhachHangs on hd.NhaCungCap equals ncc.MaKH.ToString()
                    //join cn in Db.hdctnCongNoNhaCungCaps on hd.RowID equals cn.HopDongId into congNo from cn in congNo.DefaultIfEmpty()
                    where hd.MaToaNha == buildingId.ToString()
                          //group new {hd, ncc} by new {TenKh = ncc.IsCaNhan == true? ncc.HoKH +" "+ncc.TenKH: ncc.CtyTen} into g
                select new
                {
                    TenKh = ncc.IsCaNhan == true ? ncc.HoKH + " " + ncc.TenKH : ncc.CtyTen,
                    DaThu = Db.hdctnCongNoNhaCungCaps.Where(_=>_.HopDongId == hd.RowID & _.IsPhieuChi == false).Sum(_=>_.SoTien).GetValueOrDefault(),  //g.Sum(_=>_.cn.IsPhieuChi == false? _.cn.SoTien:0),
                    DaTra = Db.hdctnCongNoNhaCungCaps.Where(_ => _.HopDongId == hd.RowID & _.IsPhieuChi == true).Sum(_ => _.SoTien).GetValueOrDefault(),
                    PhaiTra = hd.TienSauThue,
                    ConNo = hd.TienSauThue - Db.hdctnCongNoNhaCungCaps.Where(_ => _.HopDongId == hd.RowID & _.IsPhieuChi == true).Sum(_ => _.SoTien).GetValueOrDefault() + Db.hdctnCongNoNhaCungCaps.Where(_ => _.HopDongId == hd.RowID & _.IsPhieuChi == false).Sum(_ => _.SoTien).GetValueOrDefault()
                    }).ToList();
            var list = (from l in list1
                    group new {l} by new {l.TenKh}
                    into g
                    select new
                    {
                        g.Key.TenKh, DaThu = g.Sum(_ => _.l.DaThu), DaTra = g.Sum(_ => _.l.DaTra),
                        PhaiTra = g.Sum(_ => _.l.PhaiTra), ConNo = g.Sum(_ => _.l.ConNo)
                    })
                .AsEnumerable().Select((_, index) => new CongNoTableCnhd
                {
                    Stt = (index + 1).ToString(),
                    ConNo = string.Format("{0:#,0.##}", _.ConNo),
                    DaTra = string.Format("{0:#,0.##}", _.DaTra),
                    PhaiTra = string.Format("{0:#,0.##}", _.PhaiTra),
                    TenDoiTac = _.TenKh,
                    PhaiTraCn = _.PhaiTra,
                    DaTraCn = _.DaTra,
                    ConNoCn = _.ConNo
                }).ToList();

            if (list.Count == 0) return contents;

            contents = BuildingDesignTemplate.Class.Common.MergeTable(contents, "[CNHD]",Library.SqlCommon.LINQToDataTable(list));

            System.Collections.Generic.List<CongNoTableTong> congNoTong =
                new System.Collections.Generic.List<CongNoTableTong>
                {
                    new CongNoTableTong
                    {
                        TongConNo = string.Format("{0:#,0.##}", list.Sum(_ => _.ConNoCn).GetValueOrDefault()),
                         TongDaTra = string.Format("{0:#,0.##}", list.Sum(_ => _.DaTraCn).GetValueOrDefault()),
                         TongPhaiTra = string.Format("{0:#,0.##}", list.Sum(_ => _.PhaiTraCn).GetValueOrDefault()),
                    }
                };
            contents = BuildingDesignTemplate.Class.Common.GetContents(contents, groupId, Library.SqlCommon.LINQToDataTable(congNoTong).Rows[0],GroupChiTiet);

            return contents;
        }

        private class HopDongThueNgoaiClass
        {
            public string SoHopDong { get; set; }
            public string Ngay { get; set; }
            public string Thang { get; set; }
            public string Nam { get; set; }
            public string Tai { get; set; }
            public string BenA { get; set; }
            public string DaiDienBenA { get; set; }
            public string TenToaNha { get; set; }
        }

        private class CongNoChung
        {
            public string TenToaNha { get; set; }
            public string Thang { get; set; }
            public string Nam { get; set; }
        }

        private class CongNoTableCnhd
        {
            public string Stt { get; set; }
            public string TenDoiTac { get; set; }
            public string PhaiTra { get; set; }
            public string DaTra { get; set; }
            public string ConNo { get; set; }
            public decimal? PhaiTraCn {get;set;}
            public decimal? DaTraCn { get; set; }
            public decimal? ConNoCn { get; set; }
        }

        private class CongNoTableTong
        {
            public string TongPhaiTra { get; set; }
            public string TongDaTra { get; set; }
            public string TongConNo { get; set; }
        }
    }
}
