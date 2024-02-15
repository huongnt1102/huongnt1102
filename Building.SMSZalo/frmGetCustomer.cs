using Library;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ZaloDotNetSDK;

namespace Building.SMSZalo
{
    public partial class frmGetCustomer : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmGetCustomer()
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
            var lt = db.tnToaNhas.Select(p => new { p.MaTN, p.TenVT, p.TenTN}).ToList();
            glkDuAn.Properties.DataSource = lt;
            if(lt.Count() > 0)
            {
                glkDuAn.EditValue = lt.FirstOrDefault().MaTN;
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (glkDuAn.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                glkDuAn.Focus();
                return;
            }
            btnCancel.Enabled = false;
            try
            {
                var MaTN = Convert.ToInt32(glkDuAn.EditValue);
                var db = new MasterDataContext();
                var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
                if (objConfig == null)
                {
                    btnCancel.Enabled = true;
                    DialogBox.Error("Dự án này chưa được cấu hình gửi sms zalo !");
                    return;
                }
                var acc = objConfig.LinkToken;
                ZaloClient client = new ZaloClient(acc);
                var vt = 0;
                var dau = 0;
                var tc = 0;
                var tb = 0;
                var ht = 0;
                JObject result = client.getListFollower(0, 10);
                JObject jObject = JObject.Parse(result.ToString());
                JToken jUser = jObject["data"];
                JToken total = jUser["total"];
                lblTong.Text = "Tổng: " + total;
                var sl = Convert.ToInt32(total.ToString());
                int lan = 0;
                while (lan < sl)
                {
                    #region Lưu
                    try
                    {
                        if (vt == 0)
                            dau = 0;
                        else
                            dau = dau + 50;

                        JObject get = client.getListFollower(dau, 50);
                        JObject jOb = JObject.Parse(get.ToString());
                        JToken dt = jObject["data"];
                        JToken foll = dt["followers"];

                        var ltUser = foll.ToObject<List<ltUser>>();
                        foreach (var user in ltUser)
                        {
                            try
                            {
                                JObject resultx = client.getProfileOfFollower(user.user_id);
                                JObject jObjectImages = JObject.Parse(resultx.ToString());
                                JToken data = jObjectImages["data"];
                                var tt = data.ToObject<ltimg>();
                                var objUser = db.web_ZaloUsers.FirstOrDefault(o => o.user_id == tt.user_id);
                                if (objUser != null)
                                {
                                    objUser.avatar = tt.avatar;
                                    objUser.user_gender = tt.user_gender;
                                    objUser.user_id = tt.user_id;
                                    objUser.display_name = tt.display_name;
                                    objUser.birth_date = tt.birth_date;
                                    objUser.user_id_by_app = tt.user_id_by_app;
                                    objUser.MaTN = MaTN;
                                    db.SubmitChanges();
                                    tc = tc + 1;
                                    lblThanhCong.Text = "Thành công: " + tc;
                                }
                                else
                                {
                                    web_ZaloUser us = new web_ZaloUser();
                                    us.avatar = tt.avatar;
                                    us.user_gender = tt.user_gender;
                                    us.user_id = tt.user_id;
                                    us.display_name = tt.display_name;
                                    us.birth_date = tt.birth_date;
                                    us.user_id_by_app = tt.user_id_by_app;
                                    us.MaTN = MaTN;
                                    db.web_ZaloUsers.InsertOnSubmit(us);
                                    db.SubmitChanges();
                                    tc = tc + 1;
                                    lblThanhCong.Text = lblThanhCong.Text + " " + tc;
                                }
                            }
                            catch
                            {
                                tb = tb + 1;
                                lblThatBai.Text = "Thất bại: " + tb;
                            }
                        }
                        ht = tc + tb;
                        var i = (int)(((decimal)ht / (decimal)sl) * 100);
                        txtProgess.Position = i;
                    }
                    catch (Exception ex)
                    {

                    }
                    vt = vt + 1;
                    lan = lan + 50;
                    #endregion

                }
                btnCancel.Enabled = true;
            }
            catch
            {
                btnCancel.Enabled = true;
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }
        public class tong
        {
            //public string followers { get; set; }
            public string total { get; set; }
        }
        public class follower
        {
            //public string followers { get; set; }
            public List<ltUser> followers { get; set; }
        }
        public class ltUser
        {
            public string user_id { get; set; }
        }

        public class ltimg
        {
            public string avatar { get; set; }
            public string user_gender { get; set; }
            public string user_id { get; set; }
            public string user_id_by_app { get; set; }
            public string display_name { get; set; }
            public string birth_date { get; set; }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}