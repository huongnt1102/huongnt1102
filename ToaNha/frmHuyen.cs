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
    public partial class frmHuyen : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmHuyen()
        {
            InitializeComponent();
            
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            gc.DataSource = db.Huyens; 
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            lookTinh.DataSource = db.Tinhs;
            LoadData();          
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grv.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật thành công!");
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
            }
        }

        private void grvKhoiNha_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    if (DialogBox.QuestionDelete() == DialogResult.No) return;
            //    try
            //    {
            //        var del = db.mbKhoiNhas.Single(p => p.MaKN == (int)grvTaiKhoan.GetFocusedRowCellValue("MaKN"));
            //        db.SubmitChanges();
            //        grvTaiKhoan.DeleteSelectedRows();
            //    }
            //    catch
            //    {
            //        DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            //        this.Close();
            //    }
            //}
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.Huyens.Single(p => p.MaHuyen == (int)grv.GetFocusedRowCellValue("MaHuyen"));
                db.Huyens.DeleteOnSubmit(del);
                db.SubmitChanges();
                grv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grv.AddNewRow();
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
            //grv.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            //grv.SetFocusedRowCellValue("STT", grv.RowCount);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import huyện", "Import");
            using (var f = new Import.frmHuyen())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }
    }
}