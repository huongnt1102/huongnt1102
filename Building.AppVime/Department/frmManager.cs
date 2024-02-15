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
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }

        public frmManager()
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
            if (gvDepartment.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phòng ban], xin cảm ơn.");
                return;
            }

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
            using (var db = new MasterDataContext())
            {
                gcDepartment.DataSource = (from d in db.app_EmployeeDepartments
                                           join pb in db.tnPhongBans on d.DepartmentId equals pb.MaPB
                                           join nv in db.tnNhanViens on d.EmployeeId equals nv.MaNV
                                           where d.TowerId == this.TowerId
                                           select new
                                           {
                                               d.Id,
                                               IsAdmin = d.IsAdmin.GetValueOrDefault(),
                                               EmployeeName = nv.HoTenNV,
                                               Name = pb.TenPB,
                                               Role = nv.IsSuperAdmin.GetValueOrDefault() ? "Supper Admin" : (d.IsAdminTower.GetValueOrDefault() ? "Admin Tower" : (d.IsAdmin.GetValueOrDefault() ? "Leader" : "Staff"))
                                           });
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSetupGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDepartment.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phòng ban], xin cảm ơn.");
                return;
            }

            var f = new frmGroupProcess();
            f.TowerId = this.TowerId;
            f.ShowDialog();
        }
    }
}
