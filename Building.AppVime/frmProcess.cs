using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        public byte? TypeId;
        public Guid? Id;
        public decimal? ResidentId { get; set; }
        app_Resident objRe;
        MasterDataContext db;

        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var list = new Building.AppVime.ResidentStatusBindingModel().GetData();
            lookNoteType.Properties.DataSource = list;

            lookNoteType.EditValue = TypeId;

            objRe = db.app_Residents.FirstOrDefault(p => p.Id == ResidentId);
            if (objRe == null)
            {
                DialogBox.Alert("[Khách hàng] này không có trong hệ thống. \r\nVui lòng kiểm tra lại, xin cảm ơn.");
                this.Close();
            }
            else
            {
                chkLock.Checked = objRe.IsLock.GetValueOrDefault();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var obj = db.app_ResidentTowers.FirstOrDefault(p => p.Id == Id);
            if (obj != null)
            {
                obj.DateOfProcess = db.GetSystemDate();
                obj.DescriptionProcess = txtContents.Text;
                obj.EmployeeIdUpdate = Common.User.MaNV;

                objRe.IsLock = chkLock.Checked;

                try
                {
                    db.SubmitChanges();
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch
                {
                    DialogBox.Error("Đã xảy ra lỗi. Vui lòng kiểm tra lại, xin cảm ơn.");
                }
            }
        }
    }
}