using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace TaiSan.DeXuat
{
    public partial class frmEdit : XtraForm
    {
        public int _MaDX = -1;
        public tnNhanVien objnhanvien;
        public int _kyduyet = 0;
        public byte? MaTN;

        private MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void lookNhanVien_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void slookLoaiTS_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }
    }
}