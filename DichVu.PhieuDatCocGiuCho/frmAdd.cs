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

namespace DichVu.PhieuDatCocGiuCho
{
    public partial class frmAdd : Form
    {
        private MasterDataContext db;
        public tnKhachHang objKhachhang;
        public frmAdd()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void FrmAdd_Load(object sender, EventArgs e)
        {
            var objKh = db.tnKhachHangs.Select(row => new
            {
                TenKh = row.TenKH,
                MaKh = row.MaKH
            });
            if (objKh.Any())
            {
                lookUpCustomer.Properties.DataSource = objKh;
            }
           
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
