using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmBoPhanLienHe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmBoPhanLienHe()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmBoPhanLienHe_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
               gcBoPhanLienHe.DataSource = db.tnKhachHang_NguoiLienHe_BoPhans;
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }
    }
}