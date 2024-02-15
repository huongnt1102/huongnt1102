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

namespace DichVu.KhachHang.PopupKhachHang
{
    public partial class frmKiemTraDongBoSAP : DevExpress.XtraEditors.XtraForm
    {
        public string IdNumber { get; set; }
        public int? Type { get; set; }
        public string TenTN { get; set; } // Tên viết tắt tòa nhà

        public class LoaiGiayTo
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }
        public frmKiemTraDongBoSAP()
        {
            InitializeComponent();
        }

        private void frmKiemTraDongBoSAP_Load(object sender, EventArgs e)
        {
            var objLoaiGiayTo = new List<LoaiGiayTo>() {
                new LoaiGiayTo { ID = "Z00001", Name = "CCCD/CMND/Passport" },
                new LoaiGiayTo { ID = "Z00004", Name = "Mã số thuế"}
            };

            glkLoaiGiayTo.Properties.DataSource = objLoaiGiayTo;

            glkLoaiGiayTo.EditValue = "Z00001";
        }

        /// <summary>
        /// Kiểm tra dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (glkLoaiGiayTo.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn Loại giấy tờ, xin cảm ơn.");
                    return;
                }

                SAP.Funct.SyncCus.KiemTraDongBoSAPByIdnumber(Convert.ToString(glkLoaiGiayTo.EditValue), txtIdnumber.Text, TenTN);
            }
            catch { }
        }

    }
}