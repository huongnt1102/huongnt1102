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

namespace DichVu.PhiBaoTri
{
    public partial class frmEditSetting : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public int MaTN { get; set; }
        public int? ID { get; set; }
        pqlOption objPQL;

        public frmEditSetting()
        {
            InitializeComponent();
        }

        private void frmEditSetting_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.Properties.DataSource = list;
            if (list.Count > 0)
                lookUpToaNha.EditValue = list[0].MaTN;
            
            if (ID != null)
            {
                objPQL = db.pqlOptions.Single(p => p.ID == ID);
                txtName.Text = objPQL.Name;
                spinDateFrom.EditValue = objPQL.IndexMax;
                spinDateTo.EditValue = objPQL.IndexMin;
                spinMoneyPerDay.EditValue = objPQL.MoneyPerDay;
                spinPercentage.EditValue = objPQL.Percentage;
                dateFrom.EditValue = objPQL.DateFrom;
                dateTo.EditValue = objPQL.DateTo;
                lookUpToaNha.EditValue = objPQL.TowerID;
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
                objPQL = new pqlOption();
                objPQL.DateCreate = db.GetSystemDate();
                objPQL.StaffID = objnhanvien.MaNV;
            }
            else
            {
                objPQL.DateModify = db.GetSystemDate();
                objPQL.StaffIDModify = objnhanvien.MaNV;
            }

            objPQL.DateFrom = dateFrom.DateTime;
            objPQL.DateTo = dateTo.DateTime;
            objPQL.IndexMax = Convert.ToInt32(spinDateFrom.EditValue);
            objPQL.IndexMin = Convert.ToInt32(spinDateTo.EditValue);
            objPQL.IsDay = chkDay.Checked;
            objPQL.MoneyPerDay = Convert.ToDecimal(spinMoneyPerDay.EditValue);
            objPQL.Name = txtName.Text.Trim();
            objPQL.Percentage = Convert.ToDecimal(spinPercentage.EditValue);
            objPQL.TowerID = Convert.ToByte(lookUpToaNha.EditValue);

            try
            {
                if (ID == null)
                    db.pqlOptions.InsertOnSubmit(objPQL);

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