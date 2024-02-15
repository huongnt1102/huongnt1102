using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;

namespace Building.AppVime.News
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void Edit()
        {
            if (gvNews.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tin tức], xin cảm ơn.");
                return;
            }

            var id = (long?)gvNews.GetFocusedRowCellValue("NewsGeneralId");
            if (id != null)
            {
                DialogBox.Alert("Tin này được đăng từ [TTCLand-M] nên không được sửa. Vui lòng chọn kiểm tra lại, xin cảm ơn.");
                return;
            }

            var f = new frmEdit();
            f.KeyId = (Guid?)gvNews.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void frmBuilding_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            var listIds = Common.TowerList.Select(p => p.MaTN);
            lkToaNha.DataSource = db.app_TowerSettingPages.Where(p => listIds.Contains(p.Id)).Select(p => new ToaNhaItem
            {
                TenTN = p.DisplayName,
                MaTN = p.Id
            }).ToList();
            itemToaNha.EditValue = CommonVime.TowerId == 0 ? Common.User.MaTN : CommonVime.TowerId;

            lookUpEditCategory.DataSource = db.app_NewsCategories;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBaoCao.Items.Add(str);
            itemKyBaoCao.EditValue = objKBC.Source[4];
            SetDate(4);

            LoadData();
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!first) LoadData();
        }

        private void grvToaNha_DoubleClick(object sender, EventArgs e)
        {
            if (itemEdit.Enabled)
                Edit();
        }

        private void grvToaNha_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        void LoadData()
        {
            gcNews.DataSource = null;
            gcNews.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var maTN = (byte)itemToaNha.EditValue;

            if (CommonVime.TowerId != maTN)
                CommonVime.TowerId = maTN;

            DateTime dateNow = DateTime.Now;
            var tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : dateNow;
            tuNgay = new DateTime(tuNgay.Year, tuNgay.Month, tuNgay.Day, 0, 0, 0);

            var denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : dateNow;
            denNgay = new DateTime(denNgay.Year, denNgay.Month, denNgay.Day, 23, 59, 0);

            var db = new MasterDataContext();

            e.QueryableSource = from n in db.app_News
                                join nvn in db.tnNhanViens on n.EmployeeId equals nvn.MaNV
                                join nvs in db.tnNhanViens on n.EmployeeIdModify equals nvs.MaNV into temp
                                from nvs in temp.DefaultIfEmpty()
                                where n.TowerId == maTN
                                    & n.DateCreate >= tuNgay
                                    & n.DateCreate <= denNgay
                                orderby n.DateCreate descending
                                select new
                                {
                                    n.Id,
                                    n.DateCreate,
                                    n.DateModify,
                                    n.FileUrl,
                                    n.ImageUrl,
                                    IsDraff = n.IsDraff.GetValueOrDefault(),
                                    IsImportant = n.IsImportant.GetValueOrDefault(),
                                    IsResidents = n.IsResidents.GetValueOrDefault(),
                                    IsLink = string.IsNullOrEmpty(n.Link) ? false : true,
                                    n.ShortDescription,
                                    n.Title,
                                    n.TowerId,
                                    Employeer = nvn.HoTenNV,
                                    EmployeerProcess = n.EmployeeIdModify != null ? nvs.HoTenNV : "",
                                    n.CategoryId,
                                    n.NewsGeneralId
                                };
            e.Tag = db;
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte)itemToaNha.EditValue;
            if (maTN > 0)
            {
                var f = new frmEdit();
                f.TowerId = (byte)itemToaNha.EditValue;
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    RefreshData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void itemXoa_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvNews.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tin tức], xin cảm ơn.");
                return;
            }

            var id = (long?)gvNews.GetFocusedRowCellValue("NewsGeneralId");
            if (id != null)
            {
                DialogBox.Alert("Tin này được đăng từ [TTCLand-M] nên không được xóa. Vui lòng chọn kiểm tra lại, xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            try
            {
                var objNews = db.app_News.FirstOrDefault(p => p.Id == (Guid?)gvNews.GetFocusedRowCellValue("Id"));
                db.app_News.DeleteOnSubmit(objNews);

                var objDetail = db.app_NewDetails.FirstOrDefault(p => p.Id == (Guid?)gvNews.GetFocusedRowCellValue("Id"));
                db.app_NewDetails.DeleteOnSubmit(objDetail);

                db.SubmitChanges();
                gvNews.DeleteSelectedRows();

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không xóa được vì bị ràng buộc dữ liệu");
            }
        }

        private void gvNews_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        void Clicks()
        {
            db = new MasterDataContext();

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    try
                    {
                        var objDetail = db.app_NewDetails.FirstOrDefault(p => p.Id == (Guid?)gvNews.GetFocusedRowCellValue("Id"));
                        var str = "";
                        if (objDetail != null)
                        {
                            str = "<html><head><meta content=\"width=device-width, initial-scale=1\" name=\"viewport\"></head><body>" + objDetail.Description + "</body></html>";
                        }
                        htmlContent.DocumentText = str;
                    }
                    catch
                    {
                        htmlContent.DocumentText = "";
                    }
                    break;

                case 1:
                    gcResident.DataSource = (from nr in db.app_NewsResidents
                                             join r in db.app_Residents on nr.ResidentId equals r.Id
                                             where nr.NewsId == (Guid?)gvNews.GetFocusedRowCellValue("Id")
                                             select new
                                             {
                                                 r.FullName,
                                                 IsRead = nr.IsRead.GetValueOrDefault(),
                                                 nr.DateCreate,
                                                 IsResident = nr.IsResident.GetValueOrDefault()
                                             });
                    break;
            }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void gvNews_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            Clicks();
        }

        private void itemPost_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvNews.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tin tức], xin cảm ơn.");
                return;
            }

            var id = (Guid?)gvNews.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn gửi tin đến cư dân?") == DialogResult.No)
            {
                return;
            }

            var wait = DialogBox.WaitingForm();
            wait.SetCaption("Đang thông báo. Vui lòng chờ...");

            try
            {
                using (var db = new MasterDataContext())
                {
                    var objNews = db.app_News.FirstOrDefault(p => p.Id == id);
                    if (objNews != null)
                    {
                        if (objNews.IsDraff.GetValueOrDefault())
                        {
                            DialogBox.Alert("Tin này đang ở trạng thái [Tin nháp] nên không thể đăng.");
                            return;
                        }

                        if (!objNews.IsPosted.GetValueOrDefault())
                        {
                            List<decimal> listResident;
                            if (objNews.IsResidents.GetValueOrDefault())
                            {
                                listResident = db.app_NewsResidents.Where(p => p.NewsId == objNews.Id).Select(p => p.ResidentId).ToList();

                                List<app_Notification> listNotify = new List<app_Notification>();
                                foreach (var itemId in listResident)
                                {
                                    var obj = new app_Notification();
                                    obj.DateCreate = DateTime.Now;
                                    obj.Description = objNews.Title;
                                    obj.Id = Guid.NewGuid();
                                    obj.IsRead = false;
                                    obj.NewsId = objNews.Id;
                                    obj.ResidentId = itemId;
                                    obj.Title = "vừa đăng tin";
                                    obj.TowerId = objNews.TowerId;
                                    obj.TypeId = 2;//Tin tức
                                    listNotify.Add(obj);
                                }

                                db.app_Notifications.InsertAllOnSubmit(listNotify);
                            }

                            try
                            {
                                db.SubmitChanges();

                                //Gửi notify
                                CommonVime.GetConfig();

                                var toa_nha = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == objNews.TowerId);
                                if (toa_nha !=null)
                                {
                                    string building_code = toa_nha.DisplayName;
                                    int building_matn = toa_nha.Id;

                                    Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                                    var param = new Dapper.DynamicParameters();
                                    param.AddDynamicParams(model_param);
                                    //param.Add("EmployeeId", employeeId);
                                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                                    var model = new
                                    {
                                        newsId = objNews.Id,
                                        ApiKey = CommonVime.ApiKey,
                                        SecretKey = CommonVime.SecretKey,
                                        IdNew = a.FirstOrDefault(),
                                        isPersonal = VimeService.isPersonal
                                    };

                                    var ret = VimeService.Post(model, "/News/PostNoId");
                                    var result = ret.Replace("\"", "");
                                    if (result.Equals("OK"))
                                    {
                                        XacNhanPosted(id);
                                        DialogBox.Alert("Đã gửi thành công.");
                                    }
                                    else
                                    {
                                        DialogBox.Error("Gửi thất bại, vui lòng thử lại.");
                                    }
                                }

                                
                            }
                            catch (Exception ex)
                            {
                                DialogBox.Error("Đăng tin có lỗi. Mã lỗi: " + ex.Message);
                            }
                        }
                        else
                        {
                            DialogBox.Error("Tin này đã được đăng.");
                        }
                    }
                }
            }
            catch
            {
                wait.Close();
                wait.Dispose();
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void XacNhanPosted(Guid? id)
        {
            using (var db = new Library.MasterDataContext())
            {
                var objNews1 = db.app_News.FirstOrDefault(p => p.Id == id);
                if (objNews1 != null)
                {
                    objNews1.IsPosted = true;

                    db.SubmitChanges();
                }
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcNews);
        }

        private void itemSetupGroupNews_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmCategory();
            f.ShowDialog();
        }
    }
}