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
    public partial class frmTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTaiKhoan()
        {
            InitializeComponent();
            
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            gcTaiKhoan.DataSource = db.nhTaiKhoans.OrderBy(p => p.STT).Where(p => p.MaTN == maTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            lookBank.DataSource = db.nhNganHangs;
            lookUpToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            itemToaNha.EditValue = Common.User.MaTN;            
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvTaiKhoan.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật thành công!");
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
        }

        private void grvKhoiNha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.mbKhoiNhas.Single(p => p.MaKN == (int)grvTaiKhoan.GetFocusedRowCellValue("MaKN"));
                    db.SubmitChanges();
                    grvTaiKhoan.DeleteSelectedRows();
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
                var del = db.nhTaiKhoans.Single(p => p.ID == (int)grvTaiKhoan.GetFocusedRowCellValue("ID"));
                db.nhTaiKhoans.DeleteOnSubmit(del);
                db.SubmitChanges();
                grvTaiKhoan.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTaiKhoan.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvTaiKhoan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTaiKhoan.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            grvTaiKhoan.SetFocusedRowCellValue("STT", grvTaiKhoan.RowCount);
        }
    }
}