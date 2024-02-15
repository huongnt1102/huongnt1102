using Library;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ZaloDotNetSDK;
using static Building.SMSZalo.frmCustomer;

namespace Building.SMSZalo
{
    public partial class frmName : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public byte? MaTN;
        public bool? isUpDate;
        public List<object> ltdata;
        public frmName()
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
            this.ControlBox = false;
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            int tong = 0; int tc = 0; int tb = 0; int ht = 0; int pr = 0;
            try
            {
                btnCancel.Enabled = false;
                tong = ltdata.Count();
                lblTong.Text = "Tổng: " + tong;
                foreach (var item in ltdata)
                {
                    var i = item as PictureObject;
                    var ZaloUser = i.display_name;
                    var objKH = db.tnKhachHangs.FirstOrDefault(o => o.nameZalo.ToLower() == ZaloUser.ToLower());
                    if (objKH == null)
                    {
                        tb = tb + 1;
                        lblThatBai.Text = "Thất bại: " + tb;
                        ht = tc + tb;
                        pr = (int)(((decimal)ht / (decimal)tong) * 100);
                        txtProgess.Position = pr;
                        isUpDate = true;
                        continue;
                    }
                    //Cập nhật khách hàng
                    objKH.issmsZalo = true;
                    objKH.smsZalo = i.user_id;
                    objKH.nameZalo = i.display_name;
                    try
                    {
                        db.SubmitChanges();
                        tc = tc + 1;
                        lblThanhCong.Text = "Thành công: " + tc;
                    }
                    catch
                    {
                        tb = tb + 1;
                        lblThatBai.Text = "Thất bại: " + tb;
                    }
                    ht = tc + tb;
                    pr = (int)(((decimal)ht / (decimal)tong) * 100);
                    txtProgess.Position = pr;
                }
                btnCancel.Enabled = true;
                btnSave.Enabled = false;
                isUpDate = true;
            }
            catch
            {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                return;
            }
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}