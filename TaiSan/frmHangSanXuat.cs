using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace TaiSan
{
    public partial class frmHangSanXuat : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmHangSanXuat()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcHangSanXuat.DataSource = db.tsHangSanXuats;
            }
            else
            {
                gcHangSanXuat.DataSource = db.tsHangSanXuats.Where(p => p.MaTN == objnhanvien.MaTN);
            }
        }

        private void frmHangSanXuat_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvHangSanXuat.AddNewRow();
        }

        private void grvHangSanXuat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvHangSanXuat.SetFocusedRowCellValue("MaTN", objnhanvien.MaTN);
        }

        private void grvHangSanXuat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var delobj = db.tsHangSanXuats.Single(p => p.MaHSX == (int)grvHangSanXuat.GetFocusedRowCellValue("MaHSX"));
                    db.tsHangSanXuats.DeleteOnSubmit(delobj);
                    db.SubmitChanges();
                    grvHangSanXuat.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var delobj = db.tsHangSanXuats.Single(p => p.MaHSX == (int)grvHangSanXuat.GetFocusedRowCellValue("MaHSX"));
                db.tsHangSanXuats.DeleteOnSubmit(delobj);
                db.SubmitChanges();
                grvHangSanXuat.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
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