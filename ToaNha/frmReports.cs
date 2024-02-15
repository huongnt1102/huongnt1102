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
    public partial class frmReports : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmReports()
        {
            InitializeComponent();
            
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            gcReport.DataSource = db.rptReports_ToaNhas.Where(p => p.MaTN == maTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            lkReport.DataSource = db.rptReports;
            lookUpToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            itemToaNha.EditValue = Common.User.MaTN;            
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvReport.RefreshData();

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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.rptReports_ToaNhas.Single(p => p.ID == (int)gvReport.GetFocusedRowCellValue("ID"));
                db.rptReports_ToaNhas.DeleteOnSubmit(del);
                db.SubmitChanges();
                gvReport.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvReport.AddNewRow();
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
            gvReport.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }
    }
}