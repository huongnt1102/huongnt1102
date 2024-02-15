using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichVu.KhoaSo.Class
{
    /// <summary>
    /// Kiểm tra bút toán khóa sổ
    /// </summary>
    public class CheckEntryModel
    {
        /// <summary>
        /// Tòa nhà cần kiểm tra
        /// </summary>
        public byte? TowerId { get; set; }
        /// <summary>
        /// Bút toán nào? Hóa đơn, phiếu thu, điện, nước
        /// Từ table bcService
        /// </summary>
        public int? ServiceId { get; set; }
        /// <summary>
        /// Ngày của bút toán
        /// </summary>
        public DateTime? Date { get; set; }
    }
}
