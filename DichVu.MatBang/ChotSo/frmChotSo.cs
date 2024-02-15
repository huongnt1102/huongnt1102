using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace DichVu.MatBang.ChotSo
{
    public partial class frmChotSo : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        public frmChotSo()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            spinThang.EditValue = System.DateTime.Now.Month;
            spinNam.EditValue = System.DateTime.Now.Year;
            spinTuan.EditValue = DateTime.Now.GetWeekInMonth() > 4 ? 4 : DateTime.Now.GetWeekInMonth();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MaTN == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn Dự án");
            }

            Library.Class.Connect.QueryConnect.QueryData<bool>("mbMatBang_ChotSo_Save", new
            {
                MaTN = MaTN,
                Tuan = spinTuan.EditValue,
                Thang = spinThang.EditValue,
                Nam = spinNam.EditValue,
                CreatedBy = Common.User.MaNV
            });

            Library.DialogBox.Success("Lưu dữ liệu thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbxPeriod_EditValueChanged(object sender, EventArgs e)
        {
        }
    }
}