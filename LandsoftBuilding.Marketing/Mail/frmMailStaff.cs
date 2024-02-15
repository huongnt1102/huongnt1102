using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandSoftBuilding.Marketing.Mail
{
    public partial class frmMailStaff : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmMailStaff()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                lkNhanVien.DataSource = db.tnNhanViens.Where(p => p.MaTN == (byte?)itemToaNha.EditValue).Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV, p.Email }).ToList();
                gcControl.DataSource = db.mailStaffs.Where(p => p.MaTN == (byte?)itemToaNha.EditValue);
            }
            catch
            {
            }
        }

        private void frmMailStaff_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;            
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvView.AddNewRow();
        }

        private void itemAddAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvView.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            try
            {
                foreach (var i in rows)
                {
                    var obj = db.mailStaffs.Single(p => p.ID == (int)grvView.GetRowCellValue(i, "ID"));
                    db.mailStaffs.DeleteOnSubmit(obj);
                }
                db.SubmitChanges();
                grvView.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvView.RefreshData();
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void grvView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvView.SetFocusedRowCellValue("MaTN", (byte?)itemToaNha.EditValue);
        }
    }
}