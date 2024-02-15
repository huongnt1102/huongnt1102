using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MSDN.Html.Editor;
using System.Linq;
using Library;
using System.IO;

namespace Building.AppVime.Tower
{
    public partial class frmSettingService : DevExpress.XtraEditors.XtraForm
    {
        public frmSettingService()
        {
            InitializeComponent();
        }
        
        public int Id { get; set; }
        public int TowerId { get; set; }
        public int ServiceId { get; set; }

        MasterDataContext db;
        app_SettingService objSS;
        app_TowerSettingService objService;

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var listDep = (from p in db.tnPhongBans
                           join ep in db.app_EmployeeDepartments on p.MaPB equals ep.DepartmentId
                           where ep.TowerId == this.TowerId
                           select new
                           {
                               Id = p.MaPB,
                               Name = p.TenPB
                           });

            lookUpDepartment.Properties.DataSource = listDep.GroupBy(p => p.Id).Select(p => new {
                Id = p.Key,
                Name = p.FirstOrDefault().Name
            });

            lookUpUnit.Properties.DataSource = db.DonViTinhs.Select(p => new { Id = p.ID, Name = p.TenDVT });
            lookUpUnit.ItemIndex = 0;

            var listSS = db.app_TowerSettingServices.Where(p => p.TowerId == this.TowerId).Select(p => p.ServiceId).ToList();
            lookUpService.Properties.DataSource = (from p in db.app_SettingServices
                                                   join dv in db.dvLoaiDichVus on p.Id equals dv.ID
                                                   where !listSS.Contains(p.Id) | p.Id == ServiceId
                                                   select new
                                                   {
                                                       Name = dv.TenHienThi,
                                                       Id = p.Id
                                                   });

            try
            {
                if (Id != 0)
                {
                    objService = db.app_TowerSettingServices.FirstOrDefault(p => p.Id == this.Id);
                    if (objService != null)
                    {
                        ServiceId = objService.ServiceId ?? 0;
                        lookUpService.EditValue = ServiceId;
                        lookUpService.Enabled = false;
                        spinAmount.EditValue = objService.Amount ?? 0;
                        spinDeposit.EditValue = objService.Deposit ?? 0;
                        spinNumberIndex.EditValue = objService.NumberIndex ?? 0;
                        spinPrice.EditValue = objService.Price ?? 0;
                        chkExtension.Checked = (objService.TypeId ?? 0) == 0 ? false : true;
                        spinPreregistraionTime.EditValue = objService.PreregistrationTime ?? 1;
                        lookUpDepartment.EditValue = objService.DepartmentId;
                        spinMaximumPeople.EditValue = objService.MaximumAmountPeople ?? 1;
                        txtNoiDung.InnerHtml = objService.Description;
                        lookUpUnit.EditValue = objService.UnitId;
                    }
                }else
                {
                    chkExtension.Checked = false;
                    groupExtension.Enabled = false;
                }
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(lookUpService.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                lookUpService.Focus();
                return;
            }

            if (lookUpDepartment.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Phong tiếp nhận], xin cảm ơn.");
                lookUpDepartment.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();

            if (Id == 0)
            {
                objService = new app_TowerSettingService();
                objService.ServiceId = (int)lookUpService.EditValue;
                objService.TowerId = this.TowerId;
                db.app_TowerSettingServices.InsertOnSubmit(objService);
            }

            objService.DepartmentId = (int)lookUpDepartment.EditValue;
            objService.Amount = (int)spinAmount.Value;
            objService.Deposit = spinDeposit.Value;

            objService.MaximumBookInMonth = 1;
            objService.NumberIndex = (int)spinNumberIndex.Value;
            objService.PreregistrationTime = (int)spinPreregistraionTime.Value;
            objService.Price = spinPrice.Value;
            objService.TypeId = (byte)(chkExtension.Checked ? 1 : 0);
            objService.UnitId = (int)lookUpUnit.EditValue;
            objService.DateModify = db.GetSystemDate();
            objService.EmployeeIdModify = Common.User.MaNV;
            objService.MaximumAmountPeople = (byte)spinMaximumPeople.Value;
            objService.Description = txtNoiDung.InnerHtml;

            try
            {
                db.SubmitChanges();
            }
            catch
            {
                wait.Close();
                wait.Dispose();
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFields_Click(object sender, EventArgs e)
        {

        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {

        }

        private void chkExtension_EditValueChanged(object sender, EventArgs e)
        {
            groupExtension.Enabled = chkExtension.Checked;
        }

        private void lookUpService_EditValueChanged(object sender, EventArgs e)
        {
            if(Id == 0)
            {
                objSS = db.app_SettingServices.FirstOrDefault(p => p.Id == (int)lookUpService.EditValue);
                if(objSS != null)
                {
                    spinAmount.EditValue = objSS.Amount ?? 0;
                    spinDeposit.EditValue = objSS.Deposit ?? 0;
                    spinNumberIndex.EditValue = objSS.NumberIndex ?? 0;
                    spinPrice.EditValue = objSS.Price ?? 0;
                    chkExtension.Checked = (objSS.TypeId ?? 0) == 0 ? false : true;
                    spinPreregistraionTime.EditValue = objSS.PreregistrationTime ?? 1;
                    txtNoiDung.InnerHtml = objSS.Description;
                }
            }
        }
    }
}