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
    public partial class frmTinh : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTinh()
        {
            InitializeComponent();
            
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            gc.DataSource = db.Tinhs; 
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            lkQuocGia.DataSource = db.QuocTiches;
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
                var del = db.Tinhs.Single(p => p.MaTinh == (int)grv.GetFocusedRowCellValue("MaTinh"));
                db.Tinhs.DeleteOnSubmit(del);
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

        /// <summary>
        /// Import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import Tỉnh", "Import");
            using (var f = new Import.frmTinh())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }
    }
}