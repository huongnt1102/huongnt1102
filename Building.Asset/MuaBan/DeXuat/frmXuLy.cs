using System;
using System.Windows.Forms;
using Library;
using DevExpress.XtraEditors;

namespace TaiSan.DeXuat
{
    public partial class frmXuLy : XtraForm
    {
        public int? Id { get; set; }
        public int? LoaiPhongKhongDuyet { get; set; }

        private MasterDataContext _db;

        public frmXuLy()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            _db = new MasterDataContext();
            txtLyDo.Text = "";
            
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
    }
}