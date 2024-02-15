using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.Tower.Department
{
    public partial class frmGroupProcess : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }

        MasterDataContext db;

        public frmGroupProcess()
        {
            InitializeComponent();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.TowerId = this.TowerId;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.TowerId = this.TowerId;
            f.KeyId = (int)gvDepartment.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDepartment.FocusedRowHandle >= 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    var obj = db.app_EmployeeDepartments.FirstOrDefault(p => p.Id == (int)gvDepartment.GetFocusedRowCellValue("Id"));
                    try
                    {
                        db.app_EmployeeDepartments.DeleteOnSubmit(obj);
                        db.SubmitChanges();

                        gvDepartment.DeleteSelectedRows();
                    }
                    catch { }
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dữ liệu.");
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            db = new MasterDataContext();

            //gcDepartment.DataSource = (from p in db.app_EmployeeDepartments
            //                           join pb in db.tnPhongBans on p.DepartmentId equals pb.MaPB
            //                           //where p.TowerId == this.TowerId
            //                           select new
            //                           {
            //                               Id = p.DepartmentId,
            //                               Name = pb.TenPB
            //                           }).Distinct().ToList();
            gcGroup.DataSource = db.app_GroupProcesses;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSaveGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
            }
            catch { }
        }

        private void gvDepartment_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(gvDepartment.FocusedRowHandle < 0)
            {
                gcGroup.DataSource = null;
                return;
            }

            db = new MasterDataContext();

            gcGroup.DataSource = db.app_GroupProcesses;//.Where(p => p.DepartmentId == (int)gvDepartment.GetFocusedRowCellValue("Id"));
        }

        private void gvGroup_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //if (gvDepartment.FocusedRowHandle < 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Phòng ban], xin cảm ơn.");
            //    return;
            //}

            //try
            //{
            //    gvGroup.SetRowCellValue(e.RowHandle, colDepartmentId, (int)gvDepartment.GetFocusedRowCellValue("Id"));
            //}
            //catch { }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
            }
            catch { }
        }

        private void itemDelGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvGroup.FocusedRowHandle >= 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    var obj = db.app_GroupProcesses.FirstOrDefault(p => p.Id == (int)gvGroup.GetFocusedRowCellValue("Id"));
                    try
                    {
                        db.app_GroupProcesses.DeleteOnSubmit(obj);
                        db.SubmitChanges();

                        gvDepartment.DeleteSelectedRows();

                        LoadData();
                    }
                    catch { }
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dữ liệu.");
            }
        }
    }
}
