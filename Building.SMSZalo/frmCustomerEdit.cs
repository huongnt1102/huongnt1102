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

namespace Building.SMSZalo
{
    public partial class frmCustomerEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public string smsid;
        public string smsname;
        public byte? MaTN;
        public bool? isUpDate;
        public frmCustomerEdit()
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
            var data = (from kh in db.tnKhachHangs
                        join mb in db.mbMatBangs on kh.MaKH equals mb.MaKHF1 into mbr
                        from mb in mbr.DefaultIfEmpty()
                        select new
                        {
                            kh.MaTN,
                            kh.MaKH,
                            kh.KyHieu,
                            kh.DienThoaiKH,
                            mb.MaSoMB,
                            TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen
                        }).ToList();

            glkKhachHang.Properties.DataSource = data;
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                glkKhachHang.Focus();
                return;
            }

            try
            {
                var MaKH = (int?)glkKhachHang.EditValue;
                var objkh = db.tnKhachHangs.FirstOrDefault(o => o.MaKH == MaKH);
                if(objkh!= null)
                {
                    objkh.smsZalo = smsid;
                    objkh.nameZalo = smsname;
                    objkh.issmsZalo = true;
                }
                db.SubmitChanges();
                isUpDate = true;
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                //DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }
    }
}