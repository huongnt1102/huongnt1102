using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace KyThuat.BaoTri
{
    public partial class frmBaoTriThietBiChiTiet : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tsTaiSanSuDung objtssd;
        public frmBaoTriThietBiChiTiet()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmBaoTriThietBiChiTiet_Load(object sender, EventArgs e)
        {
            lookTrangThai.DataSource = db.tsTrangThais;
            lookChiTiet.DataSource = db.ChiTietTaiSans;
            LoadData();
        }

        private void LoadData()
        {
            
            gcChiTiet.DataSource = db.tsTaiSanChiTiets.Where(p => p.tsTaiSanSuDung == objtssd);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Alert("Lưu thành công");
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}