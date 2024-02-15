using Building.AppVime;
using System;
using System.Linq;

namespace DichVu.BanGiaoMatBang.Category
{
    public partial class FrmNotifycationEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? NotifycationId { get; set; }
        public bool IsLocal { get; set; }
        public byte BuildingId { get; set; }
        public int? CustomerId { get; set; }
        public int? ScheduleId { get; set; }
        public int? ScheduleApartmentId { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentName { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.ho_Notifycation _notifycation;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmNotifycationEdit()
        {
            InitializeComponent();
        }

        private void FrmNotifycationEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            _notifycation = GetNotifycation();
            GetTemplate();
            glkCustomer.Properties.DataSource = _db.tnKhachHangs.Where(_=>_.MaTN == BuildingId).Select(_=>new{_.MaKH,_.KyHieu,_.MaPhu,CustomerName = _.IsCaNhan == true?_.HoKH+" "+_.TenKH:_.CtyTen});
            if (CustomerId != null) glkCustomer.EditValue = CustomerId;
            txtTitle.Text = "";
            txtContent.Text = "";

            itemClearText.ItemClick += ItemClearText_ItemClick;
            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private Library.ho_Notifycation GetNotifycation()
        {
            return NotifycationId!=null?_db.ho_Notifycations.First(_=>_.Id == NotifycationId):new Library.ho_Notifycation();
        }

        private void GetTemplate()
        {
            var db = new Library.MasterDataContext();
            glkTitleTemplate.Properties.DataSource = db.ho_NotifycationTitles;
            glkContentTemplate.Properties.DataSource = db.ho_NotifycationContents;
        }

        private void GlkTitleTemplate_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
            if (item == null) return;

            try
            {
                if (item.EditValue != null) txtTitle.Text = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch{}
        }

        private void GlkContentTemplate_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
            if (item == null) return;

            try
            {
                if (item.EditValue != null) txtContent.Text = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch { }
        }

        private void ItemSaveTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTitle.Text == "" | txtContent.Text == "")
            {
                Library.DialogBox.Alert("Tiêu đề hoặc nội dung không được để trống.");
                return;
            }

            try
            {
                var db = new Library.MasterDataContext();

                var title = new Library.ho_NotifycationTitle {Name = txtTitle.Text};
                var content = new Library.ho_NotifycationContent {Name = txtContent.Text}; 

                db.ho_NotifycationTitles.InsertOnSubmit(title);
                db.ho_NotifycationContents.InsertOnSubmit(content);

                db.SubmitChanges();

                GetTemplate();
                glkTitleTemplate.EditValue = title.Id;
                glkContentTemplate.EditValue = content.Id;

                Library.DialogBox.Success();
            }
            catch{}
        }

        private void ItemSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (glkCustomer.EditValue == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn khách hàng.");
                    return;
                }

                _notifycation = DichVu.BanGiaoMatBang.Class.Notifycation.GetNotifycation(_notifycation, BuildingId, txtContent.Text, IsLocal, txtTitle.Text, (int)glkCustomer.EditValue, ScheduleId, ScheduleApartmentId, ApartmentId, ApartmentName);
                _db.ho_Notifycations.InsertOnSubmit(_notifycation);
                _db.SubmitChanges();

                // gửi notify thông báo đến khách hàng (lưu vào cái bảng notify nhỉ
                //Gửi notify
                var notify_model = new { tieude = txtTitle.Text, noidung = txtContent.Text, makh = (int)glkCustomer.EditValue, scheduleid = _notifycation.Id, tenmatbang = ApartmentName };

                var ret = VimeService.PostH(notify_model, "/HandOverNotify/Send");
                var result = ret.Replace("\"", "");
                if (result.Equals("1"))
                {
                    var param = new Dapper.DynamicParameters();
                    param.Add("@scheduleid", ScheduleId, System.Data.DbType.Int32, null, null);
                    param.Add("@scheduleapartmentid", ScheduleApartmentId, System.Data.DbType.Int32, null, null);
                    param.Add("@notifyid", _notifycation.Id, System.Data.DbType.Int32, null, null);
                    var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.ho_schedule_da_gui_lich", param);

                    Library.DialogBox.Success("Thiết lập lịch bàn giao thành công.");
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    Library.DialogBox.Error("Gửi notify thất bại.");
                }
            }
            catch (Exception ex)
            {
                Library.DialogBox.Error("Đã xảy ra lỗi, vui lòng kiểm tra lại.");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}