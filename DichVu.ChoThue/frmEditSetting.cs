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

namespace DichVu.ChoThue
{
    public partial class frmEditSetting : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public int MaTN { get; set; }
        public int? ID { get; set; }
        hdtnOption objHDTN;

        public frmEditSetting()
        {
            InitializeComponent();
        }

        private void frmEditSetting_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.Properties.DataSource = list;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.Properties.DataSource = list2;
            }
            
            if (ID != null)
            {
                objHDTN = db.hdtnOptions.Single(p => p.ID == ID);
                txtName.Text = objHDTN.Name;
                spinDateFrom.EditValue = objHDTN.IndexMax;
                spinDateTo.EditValue = objHDTN.IndexMin;
                spinMoneyPerDay.EditValue = objHDTN.MoneyPerDay;
                spinPercentage.EditValue = objHDTN.Percentage;
                dateFrom.EditValue = objHDTN.DateFrom;
                dateTo.EditValue = objHDTN.DateTo;
                lookUpToaNha.EditValue = objHDTN.TowerID;
            }
            else
                lookUpToaNha.EditValue = (byte)MaTN;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Diễn giải], xin cảm ơn.");
                txtName.Focus();
                return;
            }

            if (spinDateFrom.Value < spinDateTo.Value)
            {
                DialogBox.Alert("Điều kiện không đúng. Vui lòng nhập lại, xin cảm ơn.");
                spinDateFrom.Focus();
                return;
            }

            if (dateTo.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Từ ngày], xin cảm ơn.");
                dateTo.Focus();
                return;
            }

            if (dateFrom.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Đến ngày], xin cảm ơn.");
                dateFrom.Focus();
                return;
            }

            if (lookUpToaNha.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                lookUpToaNha.Focus();
                return;
            }

            if (ID == null)
            {
                objHDTN = new hdtnOption();
                objHDTN.DateCreate = db.GetSystemDate();
                objHDTN.StaffID = objnhanvien.MaNV;
            }
            else
            {
                objHDTN.DateModify = db.GetSystemDate();
                objHDTN.StaffIDModify = objnhanvien.MaNV;
            }

            objHDTN.DateFrom = dateFrom.DateTime;
            objHDTN.DateTo = dateTo.DateTime;
            objHDTN.IndexMax = Convert.ToInt32(spinDateFrom.EditValue);
            objHDTN.IndexMin = Convert.ToInt32(spinDateTo.EditValue);
            objHDTN.IsDay = chkDay.Checked;
            objHDTN.MoneyPerDay = Convert.ToDecimal(spinMoneyPerDay.EditValue);
            objHDTN.Name = txtName.Text.Trim();
            objHDTN.Percentage = Convert.ToDecimal(spinPercentage.EditValue);
            objHDTN.TowerID = Convert.ToByte(lookUpToaNha.EditValue);

            try
            {
                if (ID == null)
                    db.hdtnOptions.InsertOnSubmit(objHDTN);

                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
            }
            catch {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }
    }
}