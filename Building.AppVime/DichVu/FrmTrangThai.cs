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

namespace Building.AppVime.DichVu
{
    public partial class FrmTrangThai : DevExpress.XtraEditors.XtraForm
    {
        Library.MasterDataContext db = new Library.MasterDataContext();
        public FrmTrangThai()
        {
            InitializeComponent();
        }

        private void FrmTrangThai_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = db.app_BookingServiceExtensionStatus;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                Library.DialogBox.Success();
            }
            catch (Exception ex) {
                XtraMessageBox.Show("Mã lỗi: " + ex.GetType().FullName + "\n" + "Chi tiết: " + ex.Message + "\n" + "Tác nhân gây lỗi: " + "Lưu", ex.GetType().FullName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}