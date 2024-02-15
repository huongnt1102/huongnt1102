using System;
using Library;

namespace TaiSan.DatHang
{
    public partial class frmSoSanhBaoGia : DevExpress.XtraEditors.XtraForm
    {
        public frmSoSanhBaoGia()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var _frm = DialogBox.WaitingForm();
                TaoBangSoSanh();
                _frm.Close();
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void LoadData()
        {

        }


        private void TaoBangSoSanh()
        {
            
            
        }



    }
}