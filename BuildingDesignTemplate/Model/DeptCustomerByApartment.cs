using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingDesignTemplate.Model
{
    public class DeptCustomerByApartment
    {
        public decimal? DauKy { get; set; }
        public decimal? PhatSinh { get; set; }
        public decimal? DaThu { get; set; }
        public decimal? ThuTruoc { get; set; }
        public decimal? ConNo { get; set; }
        public decimal? ConNoCuoi { get; set; }
        public decimal? KhauTru { get; set; }
        public decimal? DauKyEMC { get; set; }
        public decimal? PhatSinhEMC { get; set; }
        public decimal? DaThuEMC { get; set; }
        public decimal? ThuTruocEMC { get; set; }
        public decimal? ConNoEMC { get; set; }
        public decimal? ConNoCuoiEMC { get; set; }
        public decimal? KhauTruEMC { get; set; }
    }

    public class DeptCustomerEp: DeptCustomerByApartment
    {
        public decimal? SoTienLai { get; set; }
        public decimal? TienCocChuaThu { get; set; }
    }
}
