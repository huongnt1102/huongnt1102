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

namespace KhachHang
{
    public partial class frmLoaiKH : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmLoaiKH()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            gcKhachHang.DataSource = db.khLoaiKhachHangs.Where(p => p.MaTN == (byte)itemToaNha.EditValue);
        }

        private void frmNhomKH_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm loại khách hàng", "Lưu");
            db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void grvKhachHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvKhachHang.DeleteSelectedRows();
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xóa loại khách hàng", "Xóa");
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

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvKhachHang_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvKhachHang.SetFocusedRowCellValue("MaTN", (byte)itemToaNha.EditValue);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}