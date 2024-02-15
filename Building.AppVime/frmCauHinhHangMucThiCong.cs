using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.AppVime
{
    public partial class frmCauHinhHangMucThiCong : XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public DangKyThiCong_HangMuc  objnhanvien;
        public frmCauHinhHangMucThiCong()
        {
            InitializeComponent();
        }

        private void frmCauHinhHangMucThiCong_Load(object sender, System.EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }
        void LoadData()
        {
            gcKhachHang.DataSource = db.DangKyThiCong_HangMucs;
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

        private void grvKhachHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvKhachHang.DeleteSelectedRows();

            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            grvKhachHang.DeleteSelectedRows();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvKhachHang.AddNewRow();
        }
    }
}
