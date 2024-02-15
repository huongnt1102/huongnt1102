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

namespace ToaNha
{
    public partial class frmKhuVuc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmKhuVuc()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmKhuVuc_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            gcKhuVuc.DataSource = db.tnKhuVucs;
        }

        private void grvKhuVuc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvKhuVuc.SetFocusedRowCellValue("MaTN", objnhanvien.MaTN);
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvKhuVuc.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvKhuVuc.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                DialogBox.Error("Không lưu được, khu vực không thể xóa vì ràng buộc dữ liệu hoặc do điều kiện mạng. Vui lòng thử lại sau");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}