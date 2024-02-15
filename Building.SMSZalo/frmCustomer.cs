using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using Library;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZaloDotNetSDK;

namespace Building.SMSZalo
{
    public partial class frmCustomer : Form
    {
        public MasterDataContext db;
        public frmCustomer()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }
        public class ZaloUserInfo
        {
            public string avatar { set; get; }
            public string user_gender { set; get; }
            public long user_id { set; get; }
            public long user_id_by_app { set; get; }
            public string display_name { set; get; }

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
            public string phone { get; set; }
            public shared_infos shared_info { get; set; }

        }
        void LoadData()
        {
            var MaTN = Convert.ToInt32(itemBieuMauToaNha.EditValue);
            var db = new MasterDataContext();
            var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
            if (objConfig == null)
            {
                DialogBox.Error("Dự án này chưa được cấu hình gửi sms zalo !");
                return;
            }
            var acc = objConfig.LinkToken;
            var wait = DialogBox.WaitingForm();
            ZaloClient client = new ZaloClient(acc);
            JObject result = client.getListFollower(0, 10);
            JObject jObject = JObject.Parse(result.ToString());
            JToken jUser = jObject["data"];
            JToken total = jUser["total"];
            int vt = 0;
            int dau = 0;
            var sl = Convert.ToInt32(total.ToString());
            int lan = 0;
            List<web_ZaloUser> lt = new List<web_ZaloUser>();
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
                    JToken dt = jOb["data"];
                    JToken foll = dt["followers"];
                    var ltUser = foll.ToObject<List<ltUser>>();
                    foreach (var user in ltUser)
                    {
                        try
                        {
                            string dthoai = "";
                            JObject resultx = client.getProfileOfFollower(user.user_id);
                            JObject jObjectImages = JObject.Parse(resultx.ToString());
                            JToken data = jObjectImages["data"];
                            var tt = data.ToObject<ltimg>();
                            if (tt.shared_info != null)
                                dthoai = "0" + tt.shared_info.phone.ToString().Substring(2, 9);
                            lt.Add(new web_ZaloUser
                            {
                                avatar = tt.avatar,
                                user_gender = tt.user_gender,
                                user_id = tt.user_id,
                                display_name = tt.display_name,
                                birth_date = tt.birth_date,
                                user_id_by_app = tt.user_id_by_app,
                                phone = dthoai,
                                MaTN = MaTN,
                                Customerid = 0,
                            });
                        }
                        catch(Exception ex)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                vt = vt + 1;
                lan = lan + 50;
                #endregion
            }
            var ltdata = lt.AsEnumerable()
                .Select((p, index) => new 
                {
                    STT = index + 1,
                    p.Id,
                    p.MaTN,
                    p.user_id,
                    p.avatar,
                    user_gender = p.user_gender == "1" ? "Nam" : "Nữ",
                    p.display_name,
                    p.phone,
                    MaKH = p.Customerid,
                }).ToList();
            BindingList <PictureObject> list = new BindingList<PictureObject>();
            foreach (var item in ltdata)
            {
                var tnkhachang = db.tnKhachHangs.FirstOrDefault(o => o.smsZalo == item.user_id);
                if (tnkhachang != null)
                    continue;

                list.Add(new PictureObject(item.STT,item.MaKH, item.Id, item.user_id, item.user_gender, item.display_name, item.avatar, "Quan tâm", item.phone));
            }
            grvDanhSach.OptionsView.RowAutoHeight = true;
            grvDanhSach.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            gcDanhSach.DataSource = list;
            list.ListChanged += (s, args) =>
            {
                if (args.PropertyDescriptor.Name == "avatar")
                    grvDanhSach.LayoutChanged();
            };
            
            var ltKhachHang = (from kh in db.tnKhachHangs
                                        join mb in db.mbMatBangs on kh.MaKH equals mb.MaKHF1 into mbr
                                        from mb in mbr.DefaultIfEmpty()
                                        select new
                                        {
                                            kh.MaTN,
                                            kh.MaKH,
                                            kh.KyHieu,
                                            kh.DienThoaiKH,
                                            mb.MaSoMB,
                                            HoTenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen
                                        }).ToList();
            itemKhachHang.DataSource = ltKhachHang;
            wait.Close();
        }

        public class ZaloUserID
        {
            public long user_id { set; get; }
        }
        public class DataZalo
        {
            public int total { set; get; }
            public List<ZaloUserID> followers { set; get; }
        }
        List<ZaloUserInfo> listUserInfo = new List<ZaloUserInfo>();
        private void Form1_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            var lt = db.tnToaNhas.Select(p => new { p.MaTN, p.TenVT, p.TenTN }).ToList();
            itemDuAn.DataSource = lt;
            if (lt.Count() > 0)
                itemBieuMauToaNha.EditValue = lt.FirstOrDefault().MaTN;
           
        }
    
        public class PictureObject : INotifyPropertyChanged
        {
            public int? Id { set; get; }
            public int? MaKH { set; get; }
            public int? STT { set; get; }
            public string user_id { set; get; }
            public string display_name { get; set; }
            public string user_gender { set; get; }
            public Image avatar { get; set; }
            public string phone { set; get; }
            public string Status { set; get; }
            public event PropertyChangedEventHandler PropertyChanged;
            public PictureObject(int? _STT,int? _MaKH,int? _Id,string _user_id, string _user_gender, string _display_name, string _avatar, string _status, string _phone)
            {
                Id = _Id;
                MaKH = _MaKH;
                STT = _STT;
                display_name = _display_name;
                user_id = _user_id;
                user_gender = _user_gender;
                Status = _status;
                phone = _phone;
                avatar = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.loading.gif", typeof(BackgroundImageLoader).Assembly);
                BackgroundImageLoader bg = new BackgroundImageLoader();
                bg.Load(_avatar);
                
                bg.Loaded += (s, e) =>
                {
                    avatar = bg.Result;
                    if (!(avatar is Image)) avatar = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", typeof(BackgroundImageLoader).Assembly);
                    PropertyChanged(this, new PropertyChangedEventArgs("avatar"));
                    bg.Dispose();
                };
            }
        }
        public class ltUpdate
        {
            public int? MaKH { set; get; }
            public int? STT { set; get; }
            public string user_id { set; get; }
            public string display_name { get; set; }
            public string user_gender { set; get; }
        }
        public class shared_infos
        {
            public string address { set; get; }
            public string city { get; set; }
            public string phone { set; get; }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemBieuMauToaNha_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcDanhSach);
        }

        private void itemCapNhatKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDanhSach.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Dòng], xin cảm ơn.");
                return;
            }
            var smsid = (string)grvDanhSach.GetFocusedRowCellValue("user_id");
            var smsname = (string)grvDanhSach.GetFocusedRowCellValue("display_name");
            using ( var frm = new frmCustomerEdit())
            {
                var MaTN = (byte?)itemBieuMauToaNha.EditValue;
                frm.smsid = smsid;
                frm.smsname = smsname;
                frm.MaTN = MaTN;
                frm.ShowDialog();
                if (frm.isUpDate == true)
                    LoadData();
            }
        }

        private void itemLocZaloName_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmName())
            {
                var MaTN = (byte?)itemBieuMauToaNha.EditValue;
                frm.MaTN = MaTN;
                frm.ShowDialog();
                if (frm.isUpDate == true)
                    LoadData();
            }
           
        }

        private void itemKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            //DialogBox.Question();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lt = new ltUpdate();
                List<Object> ltdata = ((IEnumerable)gcDanhSach.DataSource).OfType<object>().ToList();
                foreach (var item in ltdata)
                {
                    var i = item as PictureObject;
                    if (i.MaKH != 0)
                    {
                        try
                        {
                            var objkh = db.tnKhachHangs.FirstOrDefault(o => o.MaKH == i.MaKH);
                            objkh.smsZalo = i.user_id;
                            objkh.nameZalo = i.display_name;
                            objkh.issmsZalo = true;
                            db.SubmitChanges();
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }                 
            LoadData();
        }

        private void itemGuiSmS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lt = new ltUpdate();
                List<Object> ltdata = ((IEnumerable)gcDanhSach.DataSource).OfType<object>().ToList();
                var MaTN = Convert.ToInt32(itemBieuMauToaNha.EditValue);
                var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
                foreach (var item in ltdata)
                {
                    var i = item as PictureObject;
                    if (i.phone == "")
                    {
                        var smsid = i.user_id;
                        ZaloClient client = new ZaloClient(objConfig.LinkToken);
                        JObject result = client.sendRequestUserProfileToUserId(smsid, 
                            "BQLTN Yêu cầu cung cấp thông tin cá nhân.", 
                            "Để tiện cho việc gửi tin nhắn thông báo đến khách hàng, nay ban quản lý yêu cầu cư dân cung cấp thông tin số điện thoại. Vui lòng 'Click' vào đây để điền các thông tin. Việc cung cấp này sẽ được bảo mật, xin cảm ơn!",
                            "https://dip.vn/UPLOAD/Buildings/Demo2019/user.png");
                    }
                }
                DialogBox.Success("Cập nhật thành công!");
            }
            catch (Exception ex)
            {

            }
            LoadData();
        }

        public class result
        {
            public string error {get; set;}
            public string message { get; set; }
        }

        private void itemLuuKhachHangSoDienThoai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lt = new ltUpdate();
                List<Object> ltdata = ((IEnumerable)gcDanhSach.DataSource).OfType<object>().ToList();
                var MaTN = Convert.ToInt32(itemBieuMauToaNha.EditValue);
                var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
                foreach (var item in ltdata)
                {
                    var i = item as PictureObject;
                    if (i.phone != "")
                    {
                        var objkh = db.tnKhachHangs.FirstOrDefault(o => o.DienThoaiKH == i.phone);
                        if(objkh != null)
                        {
                            objkh.issmsZalo = true;
                            objkh.nameZalo = i.display_name;
                            objkh.smsZalo = i.user_id;
                            db.SubmitChanges();
                        }
                    }
                }
                DialogBox.Success("Cập nhật thành công!");
            }
            catch (Exception ex)
            {

            }
            LoadData();
        }
    }
}
