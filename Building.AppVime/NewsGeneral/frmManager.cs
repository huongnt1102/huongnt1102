using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;

namespace Building.AppVime.NewsGeneral
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
            var f = new frmEdit();
            f.KeyId = (long?)gvNews.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void frmBuilding_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = db.app_TowerSettingPages.Select(p => new ToaNhaItem
            {
                TenTN = p.DisplayName,
                MaTN = p.Id
            }).ToList();

            lookUpEditCategory.DataSource = db.app_NewsCategories;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBaoCao.Items.Add(str);
            itemKyBaoCao.EditValue = objKBC.Source[4];

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
            DateTime dateNow = DateTime.Now;
            var tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : dateNow;
            tuNgay = new DateTime(tuNgay.Year, tuNgay.Month, tuNgay.Day, 0, 0, 0);

            var denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : dateNow;
            denNgay = new DateTime(denNgay.Year, denNgay.Month, denNgay.Day, 23, 59, 0);

            var db = new MasterDataContext();

            e.QueryableSource = from n in db.app_NewsGenerals
                                join nvn in db.tnNhanViens on n.EmployeeId equals nvn.MaNV
                                join nvs in db.tnNhanViens on n.EmployeeIdModify equals nvs.MaNV into temp
                                from nvs in temp.DefaultIfEmpty()
                                where n.DateCreate >= tuNgay
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
                                    IsLink = string.IsNullOrEmpty(n.Link) ? false : true,
                                    n.ShortDescription,
                                    n.Title,
                                    Employeer = nvn.HoTenNV,
                                    EmployeerProcess = n.EmployeeIdModify != null ? nvs.HoTenNV : "",
                                    n.CategoryId
                                };
            e.Tag = db;
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                RefreshData();
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

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            try
            {
                var objNews = db.app_NewsGenerals.FirstOrDefault(p => p.Id == (long?)gvNews.GetFocusedRowCellValue("Id"));
                db.app_NewsGenerals.DeleteOnSubmit(objNews);

                var objDetail = db.app_NewsGeneralDetails.FirstOrDefault(p => p.Id == (long?)gvNews.GetFocusedRowCellValue("Id"));
                db.app_NewsGeneralDetails.DeleteOnSubmit(objDetail);

                var objNewTower = db.app_News.Where(p => p.NewsGeneralId == (long?)gvNews.GetFocusedRowCellValue("Id"));
                db.app_News.DeleteAllOnSubmit(objNewTower);

                db.SubmitChanges();
                gvNews.DeleteSelectedRows();
                //RefreshData();
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
                        var objDetail = db.app_NewsGeneralDetails.FirstOrDefault(p => p.Id == (long?)gvNews.GetFocusedRowCellValue("Id"));
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

            var id = (long?)gvNews.GetFocusedRowCellValue("Id");
            if(id == null)
            {
                DialogBox.Alert("Vui lòng chọn [Tin tức], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn đăng tin không?") == DialogResult.No) return;

            var wait = DialogBox.WaitingForm();
            wait.SetCaption("Đang gửi thông báo. Vui lòng chờ...");

            try
            {
                var db = new MasterDataContext();
                var objNews = db.app_NewsGenerals.FirstOrDefault(p => p.Id == id);
                if (objNews != null)
                {
                    var listTower = db.app_TowerSettingPages;
                    foreach (var item in listTower)
                    {
                        db = new MasterDataContext();
                        try
                        {
                            Guid? IdNews = null;
                            app_New obj;

                            obj = db.app_News.Where(p => p.TowerId == item.Id & p.NewsGeneralId == id).FirstOrDefault();
                            if(obj == null)
                            {
                                obj = new app_New();
                                obj.DateCreate = db.GetSystemDate();
                                obj.EmployeeId = Common.User.MaNV;
                                obj.Id = Guid.NewGuid();

                                IdNews = obj.Id;

                                obj.TowerId = item.Id;
                                obj.NewsGeneralId = objNews.Id;
                                db.app_News.InsertOnSubmit(obj);

                                obj.FileUrl = objNews.FileUrl;
                                obj.IsDraff = false;
                                obj.IsImportant = obj.IsImportant;
                                obj.IsResidents = false;
                                obj.ShortDescription = objNews.ShortDescription;
                                obj.Title = objNews.Title;
                                obj.Link = objNews.Link;
                                obj.CategoryId = objNews.CategoryId;
                                obj.ImageUrl = objNews.ImageUrl;

                                var objDetail = new app_NewDetail();
                                objDetail.Description = objNews.app_NewsGeneralDetail.Description;
                                objDetail.Id = obj.Id;
                                db.app_NewDetails.InsertOnSubmit(objDetail);

                                db.SubmitChanges();
                            }
                            else
                            {
                                IdNews = obj.Id;
                            }

                            if (IdNews != null)
                            {
                                if (!obj.IsPosted.GetValueOrDefault())
                                {
                                    CommonVime.GetConfig();

                                    var toa_nha = db.app_TowerSettingPages.FirstOrDefault(_=>_.Id == item.Id);
                                    if (toa_nha == null)
                                    {
                                        continue;
                                    }

                                    string building_code = toa_nha.DisplayName;
                                    int building_matn = toa_nha.Id;

                                    Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                                    var param = new Dapper.DynamicParameters();
                                    param.AddDynamicParams(model_param);
                                    //param.Add("EmployeeId", employeeId);
                                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                                    var model = new
                                    {
                                        Id = IdNews,
                                        ApiKey = CommonVime.ApiKey,
                                        SecretKey = CommonVime.SecretKey,
                                        IdNew = a.FirstOrDefault(),
                                        isPersonal = VimeService.isPersonal
                                    };

                                    var ret = VimeService.Post(model, "/News/PostNoId");
                                    var result = ret.Replace("\"", "");

                                    obj.IsPosted = result.Equals("OK");

                                    db.SubmitChanges();
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    objNews.IsPosted = true;
                    objNews.IsDraff = false;
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcNews);
        }

        private void itemSetupGroupNews_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new News.frmCategory();
            f.ShowDialog();
        }
    }
}