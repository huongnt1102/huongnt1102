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
    public partial class frmNhomLienHe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmNhomLienHe()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmNhomLienHe_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
               gc.DataSource = db.tnKhachHang_NguoiLienHe_NhomLienHes;
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.DeleteSelectedRows();
            }
            catch (System.Exception ex) { }
        }
    }
}