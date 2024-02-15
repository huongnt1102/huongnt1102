using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmNhaCungCap : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnv;

        public frmNhaCungCap()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            if (objnv != null & (bool)objnv.IsSuperAdmin)
            {
                gcNhaCungCap.DataSource = db.tnNhaCungCaps;
                lookTN.DataSource = db.tnToaNhas;
            }

            else
            {
                gcNhaCungCap.DataSource = db.tnNhaCungCaps.Where(p => p.MaTN == objnv.MaTN);
                lookTN.DataSource = db.tnToaNhas.Where(p => p.MaTN == objnv.MaTN);
            }
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvNhaCungCap.AddNewRow();
        }

        private void grvNhaCungCap_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvNhaCungCap.SetFocusedRowCellValue("MaTN", UserInfo.MaTN);
        }

        private void grvNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var deleteobj = db.tnNhaCungCaps.Single(p => p.MaNCC == (int)grvNhaCungCap.GetFocusedRowCellValue("MaNCC"));
                    db.tnNhaCungCaps.DeleteOnSubmit(deleteobj);
                    db.SubmitChanges();
                    grvNhaCungCap.DeleteSelectedRows();
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
                var deleteobj = db.tnNhaCungCaps.Single(p => p.MaNCC == (int)grvNhaCungCap.GetFocusedRowCellValue("MaNCC"));
                db.tnNhaCungCaps.DeleteOnSubmit(deleteobj);
                db.SubmitChanges();
                grvNhaCungCap.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}