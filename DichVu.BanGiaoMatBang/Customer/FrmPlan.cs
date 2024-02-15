using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmPlan : XtraForm
    {
        public FrmPlan()
        {
            InitializeComponent();
        }

        private void FrmPlan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            glkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var maTn = (byte)itemToaNha.EditValue;
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;

                var db = new MasterDataContext();

                gc.DataSource = db.ho_Plans.Where(_ =>
                        _.BuildingId == maTn & SqlMethods.DateDiffDay(tuNgay, _.DateHandOverFrom) >= 0 &
                        SqlMethods.DateDiffDay(_.DateHandOverFrom, denNgay) >= 0 & _.IsLocal == false)
                    .Select(_ => new {_.IsAllow,_.Name,_.DateHandOverFrom,_.DateHandOverTo,_.UserCreateName,_.DateCreate,_.DateUpdate,_.UserUpdateName,_.UserAllowName,_.Id,_.ContentAllow,_.No}).ToList();
                LoadDetail();
            }
            catch { }
        }

        private void LoadDetail()
        {
            try
            {
                var db = new MasterDataContext();

                var id = (int?)gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabSchedule":
                        gcSchedule.DataSource = db.ho_Schedules.Where(_ => _.PlanId == id);
                        break;
                    case "tabScheduleApartment":
                        //gcScheduleApartment.DataSource = db.ho_ScheduleApartments.Where(_ => _.PlanId == id & _.IsChoose ==true);
                        gcScheduleApartment.DataSource = (from p in db.ho_ScheduleApartments
                            join st in db.ho_Status on p.StatusId equals st.Id into status
                            from st in status.DefaultIfEmpty()
                            where p.PlanId==id & p.IsChoose == true
                            select new { p.StatusName, p.ScheduleName, p.ApartmentName, p.BuildingChecklistName, p.DateHandoverFrom, p.DateHandoverTo, p.CustomerName, p.DutyName, p.UserName, p.DateNumberNotification, st.Color }).ToList();
                        break;
                    case "tabHandoverHistory":
                        gcHandoverHistory.DataSource = db.ho_PlanHistories.Where(_ => _.PlanId == id);
                        break;
                }
            }
            catch{}
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Local.FrmPlanEdit { BuildingId = (byte)itemToaNha.EditValue, IsLocal = false })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                if (gv.GetFocusedRowCellValue("IsAllow") != null)
                {
                    if ((bool?)gv.GetFocusedRowCellValue("IsAllow") == true)
                    {
                        DialogBox.Error("Kế hoạch đã duyệt rồi, không sửa được.");
                        return;
                    }
                }

                using (var frm = new Local.FrmPlanEdit { BuildingId = (byte)itemToaNha.EditValue, Id = (int?)gv.GetFocusedRowCellValue("Id"), IsLocal = false })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }

        private bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void ItemAddSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn kế hoạch bàn giao, xin cảm ơn.");
                    return;
                }

                if (gv.GetFocusedRowCellValue("IsAllow") != null)
                {
                    if ((bool?)gv.GetFocusedRowCellValue("IsAllow") == true)
                    {
                        DialogBox.Error("Kế hoạch đã duyệt rồi, vui lòng thêm kế hoạch khác.");
                        return;
                    }
                }

                using (var frm = new FrmScheduleEdit { PlanId = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue, PlanName = gv.GetFocusedRowCellValue("Name").ToString() })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                        return;
                    }
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var handover = db.ho_Plans.FirstOrDefault(_ =>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (handover != null)
                        {
                            var apartment = db.ho_ScheduleApartments.Where(_ => _.PlanId == handover.Id).ToList();
                            foreach (var item in apartment)
                            {
                                if (item.ScheduleApartmentLocalId == null) continue;
                                var itemParent = db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == item.ScheduleApartmentLocalId);
                                if (itemParent == null) continue;
                                itemParent.StatusId = 4;
                                itemParent.StatusName = "Chờ bàn giao khách hàng";
                            }

                            db.ho_ScheduleHistories.DeleteAllOnSubmit(
                                db.ho_ScheduleHistories.Where(_ => _.PlanId == handover.Id));
                            db.ho_ScheduleApartmentCheckLists.DeleteAllOnSubmit(
                                db.ho_ScheduleApartmentCheckLists.Where(_ => _.PlanId == handover.Id));
                            db.ho_ScheduleApartmentAssets.DeleteAllOnSubmit(
                                db.ho_ScheduleApartmentAssets.Where(_ => _.PlanId == handover.Id));
                            db.ho_ScheduleApartments.DeleteAllOnSubmit(apartment);
                            db.ho_Schedules.DeleteAllOnSubmit(handover.ho_Schedules);
                            db.ho_PlanHistories.DeleteAllOnSubmit(handover.ho_PlanHistories);
                            db.ho_Plans.DeleteOnSubmit(handover);
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error("Có nơi khác đang dùng kế hoạch bàn giao này nên không xóa được");
                return;
            }
        }

        private void itemAllow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần xác nhận, xin cảm ơn.");
                    return;
                }

                if (gv.GetFocusedRowCellValue("IsAllow") != null)
                {
                    if ((bool?) gv.GetFocusedRowCellValue("IsAllow") == true)
                    {
                        DialogBox.Error("Vui lòng xóa xác nhận trước, xin cảm ơn");
                        return;
                    }
                }

                using (var frm = new FrmPlanAllow {HandoverId = (int?) gv.GetFocusedRowCellValue("Id")})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gv_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void ItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var matn = (byte?)itemToaNha.EditValue;
            frmImportKeHoachBanGiaoKH frm = new frmImportKeHoachBanGiaoKH();
            frm.MaTN = matn;
            frm.ShowDialog();
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void CbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemEditSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvSchedule.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn lịch bàn giao, xin cảm ơn.");
                    return;
                }

                if (gv.GetFocusedRowCellValue("IsAllow") != null)
                {
                    if ((bool?)gv.GetFocusedRowCellValue("IsAllow") == true)
                    {
                        DialogBox.Error("Kế hoạch đã xác nhận nên không sửa được");
                        return;
                    }
                }

                using (var frm = new FrmScheduleEdit { PlanId = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue, Id = (int?)gvSchedule.GetFocusedRowCellValue("Id"), PlanName = gv.GetFocusedRowCellValue("Name").ToString() })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemAllowNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // xóa xác nhận duyệt là xóa hết danh sách checklist auto ra, còn mặt bằng vẫn giữ nguyên, đổi trạng thái thôiS
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                        return;
                    }
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var param = new Dapper.DynamicParameters();
                        param.Add("@PlanId", int.Parse(gv.GetRowCellValue(r, "Id").ToString()), System.Data.DbType.Int32, null, null);
                        param.Add("@UserCreate", Common.User.MaNV, System.Data.DbType.Int32, null, null);
                        param.Add("@UserCreateName", Common.User.HoTenNV, System.Data.DbType.String, null, null);
                        param.Add("@DateCreate", DateTime.UtcNow.AddHours(7), System.Data.DbType.DateTime, null, null);
                        param.Add("@StatusId", 5, System.Data.DbType.Int32, null, null);
                        param.Add("@StatusName", "Chờ xác nhận", System.Data.DbType.String, null, null);
                        param.Add("@IsLocal", false, System.Data.DbType.Boolean, null, null);
                        var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.ho_XoaDuyet", param).ToList();

                        //var plan = db.ho_Plans.FirstOrDefault(_ =>
                        //    _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        //if (plan != null)
                        //{
                        //    db.ho_ScheduleApartmentCheckLists.DeleteAllOnSubmit(
                        //        db.ho_ScheduleApartmentCheckLists.Where(_ => _.PlanId == plan.Id));
                        //    plan.ho_Schedules.ToList().ForEach(_ => _.IsPlanAllow = null);
                        //    db.ho_ScheduleApartments.Where(_ => _.PlanId == plan.Id).ToList().ForEach(_ =>
                        //    {
                        //        _.StatusId = 5;
                        //        _.StatusName = "Chờ xác nhận";
                        //    });

                        //    plan.IsAllow = null;
                        //    plan.UserAllowId = null;
                        //    plan.UserAllowName = null;
                        //    plan.ContentAllow = null;

                        //    #region lưu lịch sử

                        //    var history = new ho_PlanHistory();
                        //    history.BuildingId = plan.BuildingId;
                        //    history.Content = "Xóa xác nhận";
                        //    history.DateCreate = DateTime.UtcNow.AddHours(7);
                        //    history.DateHandoverFrom = plan.DateHandOverFrom;
                        //    history.DateHandoverTo = plan.DateHandOverTo;
                        //    history.IsLocal = plan.IsLocal;
                        //    history.PlanId = plan.Id;
                        //    history.PlanName = plan.Name;
                        //    history.UserCreate = Common.User.MaNV;
                        //    history.UserCreateName = Common.User.HoTenNV;
                        //    db.ho_PlanHistories.InsertOnSubmit(history);

                        //    #endregion
                        //}

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng kế hoạch bàn giao này nên không xóa được");
                return;
            }
        }

        private void gvScheduleApartment_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "StatusName")
                {
                    if (gvScheduleApartment.GetRowCellValue(e.RowHandle, "Color") == null) return;
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)gvScheduleApartment.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            catch { }
        }

        private void itemCapNhatMauTrangThaiMatBang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new Library.MasterDataContext();
            var listMbTrangThai = db.mbTrangThais;
            foreach (var mbTrangThai in listMbTrangThai)
            {
                var idStatus = GethoStatusIdFromMbTrangThai(mbTrangThai.MaTT);
                Library.ho_Status hoStatus = GetHoStatus(idStatus);
                if (hoStatus != null) mbTrangThai.MauNen = hoStatus.Color;
            }

            db.SubmitChanges();
        }

        #region Check for admin
        private int GethoStatusIdFromMbTrangThai(int? mbTrangThaiId)
        {
            int statusId = 0;
            switch (mbTrangThaiId)
            {
                case 1: statusId = 2;
                    break;
                case 2: statusId = 3;
                    break;
                case 3: statusId = 4;
                    break;
                case 4: statusId = 4;
                    break;
                case 53: statusId = 6;
                    break;
                case 54: statusId = 7;
                    break;
                case 55: statusId = 8;
                    break;
                case 57: statusId = 8;
                    break;
                case 58: statusId = 8;
                    break;
                case 59: statusId = 10;
                    break;
                case 60: statusId = 9;
                    break;
            }

            return statusId;
        }

        private int GetMbTrangThaiIdFromHoStatus(int? statusId, string statusName)
        {
            int trangThai = 0;
            switch (statusId)
            {
                case 1: trangThai = 1;
                    break;
                case 2: trangThai = 1;
                    break;
                case 3 : trangThai = 2;
                    break;
                case 4:
                    if (statusName == "Bàn giao nội bộ thành công")
                        trangThai = 3;
                    else if (statusName == "Chờ bàn giao khách hàng")
                        trangThai = 3;
                    else trangThai = 4;
                    break;
                case 5: trangThai = 53;
                    break;
                case 6:
                    trangThai = 53;
                    break;
                case 7:
                    trangThai = 54;
                    break;
                case 8:
                    trangThai = statusName == "Đã bàn giao thành công" ? 55 : 58;
                    break;
                case 9:
                    trangThai = 60;
                    break;
                case 10:
                    trangThai = 59;
                    break;
            }

            return trangThai;
        }

        private Library.ho_Status GetHoStatus(int statusId)
        {
            var db = new Library.MasterDataContext();
            return db.ho_Status.FirstOrDefault(_ => _.Id == statusId);
        }

        private Library.mbMatBang GetMatBang(int? matBangId)
        {
            var db = new Library.MasterDataContext();
            return db.mbMatBangs.FirstOrDefault(_ => _.MaMB == matBangId);
        }

        #endregion

        private void itemDongBoTrangThaiMatBang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new Library.MasterDataContext();
            var listScheduleApartment = db.ho_ScheduleApartments;
            foreach (var item in listScheduleApartment)
            {
                Library.mbMatBang matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaMB == item.ApartmentId);
                if (matBang != null)
                {
                    var status = GetMbTrangThaiIdFromHoStatus(item.StatusId, item.StatusName);
                    matBang.MaTT = status;
                    
                }
            }

            db.SubmitChanges();
        }
        
    }
}