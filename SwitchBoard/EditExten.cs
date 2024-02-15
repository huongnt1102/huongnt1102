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


namespace DIP.SwitchBoard
{
    public partial class EditExten : DevExpress.XtraEditors.XtraForm
    {
        public EditExten()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }

        MasterDataContext db;
        pbxExtension objExten;
        
        private void EditExten_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            lkStaff.Properties.DataSource = (from n in db.tnNhanViens orderby n.HoTenNV select new { n.MaNV, n.HoTenNV }).ToList();

            if (this.ID != null)
            {
                objExten = db.pbxExtensions.Single(p => p.ID == this.ID);
                txtDisplay.Text = objExten.Display;
                txtExtenName.Text = objExten.ExtenName;
                txtPass.Text = it.EncDec.Decrypt(objExten.Password);
                spPort.EditValue = objExten.Port;
                lkStaff.EditValue = objExten.StaffID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtExtenName.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tên máy nhánh");
                txtExtenName.Focus();
                return;
            }

            if (txtPass.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập mật khẩu");
                txtPass.Focus();
                return;
            }

            if (spPort.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập cổng kết nối");
                spPort.Focus();
                return;
            }

            if (txtDisplay.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tên hiển thị");
                txtDisplay.Focus();
                return;
            }

            if (lkStaff.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên");
                lkStaff.Focus();
                return;
            }

            var count = (from et in db.pbxExtensions where et.ID != this.ID & et.StaffID == (int)lkStaff.EditValue select et).Count();
            if (count > 0)
            {
                DialogBox.Error("Trùng nhân viên");
                lkStaff.Focus();
                return;
            }

            if (this.ID == null)
            {
                objExten = new pbxExtension();
                db.pbxExtensions.InsertOnSubmit(objExten);
            }

            objExten.Display = txtDisplay.Text;
            objExten.ExtenName = txtExtenName.Text;
            objExten.Password = it.EncDec.Encrypt(txtPass.Text);
            objExten.Port = Convert.ToInt32(spPort.Value);
            objExten.StaffID = (int)lkStaff.EditValue;
            
            db.SubmitChanges();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}