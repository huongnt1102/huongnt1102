using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace TaiSan.DatHang
{
    public partial class frmLyDoKhongDuyet : XtraForm
    {

        
        public frmLyDoKhongDuyet()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void frmChuyenTaiSan_Load(object sender, EventArgs e)
        {

        }
    }
}