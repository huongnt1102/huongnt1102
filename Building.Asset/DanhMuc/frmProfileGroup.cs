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

namespace Building.Asset.DanhMuc
{
    public partial class frmProfileGroup : XtraForm
    {
        private MasterDataContext _db;

        public frmProfileGroup()
        {
            InitializeComponent();
        }

        private void frmNhomCongViec_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            _db = new MasterDataContext();

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            gridControl1.DataSource = _db.tbl_Profile_Nhoms.OrderBy(p => p.TenNhomProfile);
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
            gridView1.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
        }

        private void barButtonItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                    gridView1.DeleteSelectedRows();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?)gridView1.GetFocusedRowCellValue("ID");
            if (id == null | id == 0) return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gridView1.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gridView1.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }
    }
}