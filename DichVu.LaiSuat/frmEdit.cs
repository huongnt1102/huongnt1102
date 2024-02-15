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

namespace DichVu.LaiSuat
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        dvLaiSuat objDM;
        public int? KeyID;
        public byte MaTN;
        public frmEdit()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            if (KeyID == null)
            {
                objDM = new  dvLaiSuat();
                db.dvLaiSuats.InsertOnSubmit(objDM);
                objDM.MaTN = MaTN;
                dateTuNgay.EditValue = db.GetSystemDate();
                dateDenNgay.EditValue = db.GetSystemDate().AddMonths(12);
                rdgIsByMonth.EditValue = true;
            }
            else
            {
                try
                {
                    //objDM = db.dvLaiSuats.Single(p => p.ID == KeyID);
                    //spinLaiSuat.EditValue = objDM.LaiSuat ?? 0M;
                    //txtTenDM.EditValue = objDM.TenDM;
                    //if (objDM.DenNgay != null)
                    //    dateDenNgay.DateTime = objDM.DenNgay.Value;
                    //if (objDM.TuNgay != null)
                    //    dateTuNgay.DateTime = objDM.TuNgay.Value;
                    //rdgIsByMonth.EditValue = objDM.IsByMonth.GetValueOrDefault();
                }
                catch { }
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (dateTuNgay.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Từ ngày], xin cảm ơn.");
                dateTuNgay.Focus();
                return;
            }

            if (dateDenNgay.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Đến ngày], xin cảm ơn.");
                dateDenNgay.Focus();
                return;
            }

            if (dateTuNgay.DateTime.CompareTo(dateDenNgay.DateTime) > 0)
            {
                DialogBox.Error("[Từ ngày] phải trước [Đến ngày]");
                dateDenNgay.Focus();
                return;
            }

            if (txtTenDM.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên định mức], xin cảm ơn.");
                txtTenDM.Focus();
                return;
            }

            try
            {
                if (KeyID == null)
                {
                    //objDM.NgayTao = db.GetSystemDate();
                    //objDM.MaNV = objNV.MaNV;
                }
                else
                {
                    //objDM.NgayCN = db.GetSystemDate();
                    //objDM.MaNVCN = objNV.MaNV;
                }
                //objDM.TenDM = txtTenDM.Text;
                //objDM.TuNgay = dateTuNgay.DateTime;
                //objDM.DenNgay = dateDenNgay.DateTime;
                //objDM.LaiSuat = spinLaiSuat.Value;
                //objDM.IsByMonth = (bool)rdgIsByMonth.EditValue;

                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
            }
            catch
            {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }
    }
}