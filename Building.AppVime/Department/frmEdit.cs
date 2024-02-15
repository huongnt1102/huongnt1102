using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Building.AppVime.Tower.Department
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }
        public int KeyId { get; set; }
        MasterDataContext db;
        app_EmployeeDepartment objDep;

        public frmEdit() 
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (glEmployee.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Nhân viên], xin cảm ơn.");
                glEmployee.Focus();
                return;
            }

            try
            {
                var objCheck = db.app_EmployeeDepartments.FirstOrDefault(p => p.TowerId == this.TowerId & p.EmployeeId == (int)glEmployee.EditValue & p.Id != KeyId);
                if (objCheck == null)
                {
                    objDep.EmployeeId = (int)glEmployee.EditValue;
                    objDep.IsAdmin = chkIsAdmin.Checked;
                    objDep.DepartmentId = (int)lookUpDepartment.EditValue;
                    objDep.IsAdminTower = chkBQL.Checked;

                    db.SubmitChanges();

                    this.DialogResult = DialogResult.OK;
                }else
                {
                    DialogBox.Alert("[Nhân viên] này đã có trong phòng. Vui lòng kiểm tra lại, xin cảm ơn.");
                }
            }catch {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng thử lại, xin cảm ơn.");
            }

            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            lookUpDepartment.Properties.DataSource = db.tnPhongBans.Where(p => p.MaTN == this.TowerId).Select(p => new { p.MaPB, p.TenPB });
            glEmployee.Properties.DataSource = (from p in db.tnNhanViens
                                                join re in db.app_Residents on p.MaNV equals re.EmployeeIdRefer
                                                join tn in db.tnToaNhas on p.MaTN equals tn.MaTN into tblToaNha
                                                from tn in tblToaNha.DefaultIfEmpty()
                                            select new
                                            {
                                                Id = p.MaNV,
                                                Name = p.HoTenNV,
                                                Phone = p.DienThoai,
                                                Tower = p.MaTN != null ? tn.TenTN : ""
                                            }); 

            objDep = db.app_EmployeeDepartments.FirstOrDefault(p => p.Id == KeyId);
            if (objDep != null)
            {
                KeyId = objDep.Id;
                glEmployee.EditValue = objDep.EmployeeId;
                lookUpDepartment.EditValue = objDep.DepartmentId;
                chkIsAdmin.Checked = objDep.IsAdmin.GetValueOrDefault();
                chkBQL.Checked = objDep.IsAdminTower.GetValueOrDefault();
            }
            else
            {
                objDep = new app_EmployeeDepartment();
                objDep.TowerId = this.TowerId;
                db.app_EmployeeDepartments.InsertOnSubmit(objDep);
                chkIsAdmin.Checked = false;
                chkBQL.Checked = false;
            }
        }

        private void glEmployee_EditValueChanged(object sender, EventArgs e)
        {
            if(KeyId == 0)
            {
                var objEmployee = db.tnNhanViens.FirstOrDefault(p => p.MaNV == (int)glEmployee.EditValue);
                if(objEmployee != null)
                {
                    lookUpDepartment.EditValue = objEmployee.MaPB;
                }
            }
        }
    }
}
