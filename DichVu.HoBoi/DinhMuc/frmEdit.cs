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

namespace DichVu.HoBoi.DinhMuc
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        dvhbDinhMuc objDM;
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
            var lstLoaiThe = db.dvhbLoaiThes.OrderBy(p => p.STT).Select(p => new { p.ID, p.TenLT }).ToList();
            lookLoaiThe.Properties.DataSource = lstLoaiThe;
            if (KeyID == null)
            {
                objDM = new dvhbDinhMuc();
                db.dvhbDinhMucs.InsertOnSubmit(objDM);
                objDM.MaTN = MaTN;
                dateTuNgay.EditValue = db.GetSystemDate();
                dateDenNgay.EditValue = db.GetSystemDate().AddMonths(12);

                try
                {
                    lookLoaiThe.EditValue = lstLoaiThe[0].ID;
                }
                catch { }
            }
            else
            {
                try
                {
                    objDM = db.dvhbDinhMucs.Single(p => p.ID == KeyID);
                    spinMucPhi.EditValue = objDM.MucPhi ?? 0;
                    txtTenDM.EditValue = objDM.TenDM;
                    if (objDM.DenNgay != null)
                        dateDenNgay.DateTime = objDM.DenNgay.Value;
                    if (objDM.TuNgay != null)
                        dateTuNgay.DateTime = objDM.TuNgay.Value;
                    lookLoaiThe.EditValue = objDM.MaLT;
                }
                catch { }
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (lookLoaiThe.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Loại thẻ], xin cảm ơn.");
                lookLoaiThe.Focus();
                return;
            }

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
                    objDM.NgayTao = db.GetSystemDate();
                    objDM.MaNV = objNV.MaNV;
                }
                else
                {
                    objDM.NgayCN = db.GetSystemDate();
                    objDM.MaNVCN = objNV.MaNV;
                }
                objDM.TenDM = txtTenDM.Text;
                objDM.TuNgay = dateTuNgay.DateTime;
                objDM.DenNgay = dateDenNgay.DateTime;
                objDM.MucPhi = spinMucPhi.Value;
                objDM.MaLT = (short)lookLoaiThe.EditValue;

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