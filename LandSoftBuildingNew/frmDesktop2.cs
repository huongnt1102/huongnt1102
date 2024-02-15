using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace LandSoftBuildingMain
{
    public partial class frmDesktop2 : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien { get; set; }
        MasterDataContext db;
        public frmDesktop2()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmDesktop2_Load(object sender, EventArgs e)
        {
            
        }
        private void itemNhanYeuCau_ItemClick(object sender, EventArgs e)
        {
            using (DichVu.YeuCau.frmEdit frm = new DichVu.YeuCau.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
            }
        }
    }
}