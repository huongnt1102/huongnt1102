using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace TaiSan.GhiTang
{
    public partial class frmDonViSD : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public tnNhanVien objnhanvien;

        public frmDonViSD()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            LookCapNguon.DataSource = db.tsDonViSuDungs.Select(p => new { p.ID, p.TenDV });
            gcDonViSD.DataSource = db.tsDonViSuDungs;
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvDonViSD.AddNewRow();
        }

        private void grvTrangThai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvDonViSD.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            grvDonViSD.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}