using System.Linq;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmHandover : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }

        private Library.MasterDataContext _db;

        public FrmHandover()
        {
            InitializeComponent();
        }

        private void FrmHandoverLocal_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);
            lkToaNha.DataSource = Library.Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            var objKbc = new Library.KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[1];

            SetDate(1);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new Library.KyBaoCao
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
                var tuNgay = (System.DateTime?)itemTuNgay.EditValue;
                var denNgay = (System.DateTime?)itemDenNgay.EditValue;

                var db = new Library.MasterDataContext();

                gc.DataSource = (from _ in db.ho_ScheduleApartments
                    join cl in db.ho_Status on _.StatusId equals cl.Id into color
                    from cl in color.DefaultIfEmpty()
                    where _.BuildingId == maTn & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, _.DateHandoverTo) >= 0 & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.DateHandoverTo, denNgay) >= 0 & _.IsChoose == true & _.StatusId > 5 & _.ScheduleApartmentLocalId != null & _.ApartmentId != null
                    select new
                    {
                        _.ApartmentName,
                        _.CustomerName,
                        _.BuildingChecklistName,
                        _.DateHandoverFrom,
                        _.DateHandoverTo,
                        _.Id,
                        _.PlanName,
                        _.IsChoose,
                        _.ScheduleName,
                        _.StatusName,
                        _.UserName,
                        _.DateNumberNotification,
                        cl.Color,
                        _.No,_.NgayDuKienHoanThanh,_.NgayMacNhienBanGiao, _.NoteMacNhienBanGiao
                    }).ToList();

                LoadDetail();
            }
            catch { }
        }

        private void LoadDetail()
        {
            try
            {
                var db = new Library.MasterDataContext();

                var id = (int?)gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabChecklist":
                        gcScheduleApartmentChecklist.DataSource =
                            db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleApartmentId == id);
                        break;
                    case "tabAsset":
                        gcScheduleApartmentAsset.DataSource =
                            db.ho_ScheduleApartmentAssets.Where(_ => _.ScheduleApartmentId == id);
                        break;
                    case "tabHistory":
                        gcHistory.DataSource = db.ho_PlanHistories.Where(_ => _.ScheduleApartmentId == id);
                        break;
                }
            }
            catch { }
        }

        private void CbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemUserHandover_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmHandoverUser { BuildingId = (byte)itemToaNha.EditValue })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void ItemHandoverAllow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn phiếu, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmHandoverCheckListAllow { Id = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void Gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void ItemEditChecklist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn phiếu, xin cảm ơn.");
                    return;
                }

                using (var frm = new Local.FrmHandoverCheckList { Id = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void XtraTabControl1_Click(object sender, System.EventArgs e)
        {
            LoadDetail();
        }

        private void ItemAssetAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn mặt bằng, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmScheduleApartmentAssetEdit { ScheduleApartmentId = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void ItemAssetEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn mặt bằng, xin cảm ơn.");
                    return;
                }

                if (gvScheduleApartmentAsset.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn tài sản, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmScheduleApartmentAssetEdit { ScheduleApartmentId = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue, ScheduleApartmentAssetId = (int?)gvScheduleApartmentAsset.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void ItemAssetDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new Library.MasterDataContext())
                {
                    var indexs = gvScheduleApartmentAsset.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        Library.DialogBox.Alert("Vui lòng chọn những tài sản cần xóa");
                        return;
                    }
                    if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var asset = db.ho_ScheduleApartmentAssets.FirstOrDefault(_ => _.Id == int.Parse(gvScheduleApartmentAsset.GetRowCellValue(r, "Id").ToString()));
                        if (asset != null)
                        {
                            db.ho_ScheduleApartmentAssets.DeleteOnSubmit(asset);
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Có nơi khác đang dùng kế hoạch bàn giao này nên không xóa được");
                return;
            }
        }

        private void ItemAssetImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ItemAssetExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcScheduleApartmentAsset);
        }

        private void ItemViewImg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (long?)gvApartmentChecklist.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                Library.DialogBox.Error("Vui lòng chọn [Checklist].");
                return;
            }

            using (var frm = new Category.FrmViewImg())
            {
                frm.Id = id;
                frm.ShowDialog();
            }
        }

        private void gv_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "StatusName")
                {
                    if (gv.GetRowCellValue(e.RowHandle, "Color") == null) return;
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            catch { }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        private void ItemMacNhienBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("Id") == null)
            {
                Library.DialogBox.Error("Vui lòng chọn phiếu bàn giao, xin cảm ơn.");
                return;
            }

            using (var frm = new DichVu.BanGiaoMatBang.Customer.FrmMacNhienBanGiao()
                {HandOverId = (int) gv.GetFocusedRowCellValue("Id")})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        #region Mặc nhiên bàn giao

        private Library.ho_ScheduleApartment GetScheduleApartment(int id)
        {
            return _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == id);
        }

        private Library.ho_ScheduleApartment SaveScheduleApartmentCustomer(Library.ho_ScheduleApartment scheduleApartmentCustomer, int? statusId, string statusName)
        {
            scheduleApartmentCustomer.StatusId = statusId;
            scheduleApartmentCustomer.StatusName =statusName;
            return scheduleApartmentCustomer;
        }

        private Library.ho_ScheduleApartment GetScheduleApartmentLocal(Library.ho_ScheduleApartment scheduleApartmentCustomer)
        {
            if (scheduleApartmentCustomer.ScheduleApartmentLocalId == null) return null;
            var local = GetScheduleApartment((int)scheduleApartmentCustomer.ScheduleApartmentLocalId);
            if (local == null) return null;
            local.StatusId = scheduleApartmentCustomer.StatusId;
            local.StatusName = scheduleApartmentCustomer.StatusName;
            return local;
        }

        private void SavePlanHistory(Library.ho_ScheduleApartment scheduleApartment)
        {
            var history = new Library.ho_PlanHistory();
            history.PlanId = scheduleApartment.PlanId;
            history.PlanName = scheduleApartment.PlanName;
            history.Content = scheduleApartment.StatusName;
            history.DateHandoverFrom = scheduleApartment.DateHandoverFrom;
            history.DateHandoverTo = scheduleApartment.DateHandoverTo;
            history.BuildingId = scheduleApartment.BuildingId;
            history.DateCreate = System.DateTime.UtcNow.AddHours(7);
            history.UserCreate = Library.Common.User.MaNV;
            history.UserCreateName = Library.Common.User.HoTenNV;
            history.IsLocal = scheduleApartment.IsLocal;
            history.ScheduleId = scheduleApartment.ScheduleId;
            history.ScheduleName = scheduleApartment.ScheduleName;
            history.ScheduleApartmentId = scheduleApartment.Id;
            history.ApartmentId = scheduleApartment.ApartmentId;
            history.ApartmentName = scheduleApartment.ApartmentName;
            _db.ho_PlanHistories.InsertOnSubmit(history);
        }

        private void SaveMatBang(Library.ho_ScheduleApartment scheduleApartmentCustomer, bool? daGiaoChiaKhoa, int? trangThai)
        {
            var matBang = _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == scheduleApartmentCustomer.ApartmentId);
            if (matBang != null)
            {
                matBang.NgayBanGiao = scheduleApartmentCustomer.DateHandoverTo;
                matBang.MaKH = scheduleApartmentCustomer.CustomerId;
                matBang.MaKHF1 = scheduleApartmentCustomer.CustomerId;
                matBang.DaGiaoChiaKhoa = daGiaoChiaKhoa;
                matBang.NhanVienBanGiaoNha = Library.Common.User.HoTenNV;
                matBang.MaTT = trangThai;
            }
        }

        #endregion

        private void itemChoBanGiaoKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn phiếu bàn giao, xin cảm ơn.");
                    return;
                }

                _db = new Library.MasterDataContext();
                var scheduleApartmentCustomer = GetScheduleApartment((int)gv.GetFocusedRowCellValue("Id"));
                if (scheduleApartmentCustomer == null)
                {
                    Library.DialogBox.Error("Không tìm thấy phiếu bàn giao");
                    return;
                }

                scheduleApartmentCustomer = SaveScheduleApartmentCustomer(scheduleApartmentCustomer, 6, "Chờ bàn giao khách hàng");
                SavePlanHistory(scheduleApartmentCustomer);

                Library.ho_ScheduleApartment scheduleApartmentLocal = GetScheduleApartmentLocal(scheduleApartmentCustomer);
                if (scheduleApartmentLocal != null) SavePlanHistory(scheduleApartmentLocal);

                SaveMatBang(scheduleApartmentCustomer, false, 53);

                _db.SubmitChanges();
                Library.DialogBox.Success();
                LoadData();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Không lưu được dữ liệu, vui lòng báo kỹ thuật với lỗi như sau: \n" + ex);
            }
        }

        private void itemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CreatePhanQuyen();
        }
    }
}