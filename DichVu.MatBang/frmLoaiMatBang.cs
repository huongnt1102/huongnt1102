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

namespace MatBang
{
    public partial class frmLoaiMatBang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmLoaiMatBang()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            var _MaTN = (byte)itemToaNha.EditValue;
            gcLoaiMB.DataSource = db.mbLoaiMatBangs.Where(p => p.MaTN == _MaTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LKNhomMatBang.DataSource = db.mbNhomMatBangs;
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
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
        }

        private void grvLoaiMB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.mbLoaiMatBangs.Single(p => p.MaLMB == (int)grvLoaiMB.GetFocusedRowCellValue("MaLMB"));
                    db.mbLoaiMatBangs.DeleteOnSubmit(del);
                    db.SubmitChanges();
                    grvLoaiMB.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                    this.Close();
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.mbLoaiMatBangs.Single(p => p.MaLMB == (int)grvLoaiMB.GetFocusedRowCellValue("MaLMB"));
                db.mbLoaiMatBangs.DeleteOnSubmit(del);
                db.SubmitChanges();
                grvLoaiMB.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLoaiMB.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvLoaiMB_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvLoaiMB.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void grvLoaiMB_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var TenNMB = (grvLoaiMB.GetRowCellValue(e.RowHandle, "MaNMB") ?? "").ToString();
            if (TenNMB.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui chọn nhóm mặt bằng !";
                return;
            }
        }
    }
}