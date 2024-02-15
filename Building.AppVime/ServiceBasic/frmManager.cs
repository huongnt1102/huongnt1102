using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;
using DevExpress.XtraEditors;

namespace Building.AppVime.ServiceBasic
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        bool IsXacNhanCoc = false;
        bool IsXacNhanHoanThanh = false;

        private string GetUID(string value)
        {
            string countryCode = "84";
            string phoneNumber = new Regex(@"\D").Replace(value, string.Empty);
            string uid = countryCode + (phoneNumber.StartsWith("0") ? phoneNumber.Substring(1) : phoneNumber);

            return uid;
        }

        private string FormatPhone(string value)
        {
            string phoneNumber = new Regex(@"\D").Replace(value, string.Empty);

            return phoneNumber;
        }

        void LoadDictionary()
        {
            using (var db = new MasterDataContext())
            {
                lookUpEditZone.DataSource = db.app_Zones.Where(p => p.TowerId == (byte)itemToaNha.EditValue);

                lookUpEditService.DataSource = (from dv in db.dvLoaiDichVus
                                                join s in db.app_TowerSettingServices on dv.ID equals s.ServiceId
                                                where s.TowerId == (byte)itemToaNha.EditValue
                                                select new
                                                {
                                                    Name = dv.TenHienThi,
                                                    Id = s.ServiceId
                                                });
                lookUpEditDepartment.DataSource = db.tnPhongBans.Where(p => p.MaTN == (byte)itemToaNha.EditValue).Select(p => new
                {
                    Id = p.MaPB,
                    Name = p.TenPB
                });

                lookUpEditZone.DataSource = db.app_Zones.Where(p => p.TowerId == Convert.ToInt32(itemToaNha.EditValue));
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = CommonVime.TowerId;// Common.User.MaTN;

            using (var db = new MasterDataContext())
            {
                var listStatus = db.app_BookingStatus;
                lookUpEditStatus.DataSource = listStatus;
                lookUpEditStatus2.DataSource = listStatus;

                LoadDictionary();
            }

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBaoCao.Items.Add(str);
            itemKyBaoCao.EditValue = objKBC.Source[4];
            SetDate(4);

            IsXacNhanCoc = itemXacNhanCoc.Enabled;
            IsXacNhanHoanThanh = itemXacNhanHoanThanh.Enabled;

            LoadData();

            SetEnableControl();
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

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            LoadDictionary();

            gcService.DataSource = null;
            gcService.DataSource = linqInstantFeedbackSource1;
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

            bool isAdmin = false;
            int departmentId = 0;
            var objDepartment = db.app_EmployeeDepartments.FirstOrDefault(p => p.EmployeeId == Common.User.MaNV);
            if(objDepartment != null)
            {
                isAdmin = objDepartment.IsAdmin.GetValueOrDefault();
                departmentId = objDepartment.DepartmentId ?? 0;
            }

            if (Common.User.IsSuperAdmin.GetValueOrDefault())
            {
                e.QueryableSource = from p in db.app_BookingServices
                                    join re in db.app_Residents on p.ResidentId equals re.Id into res from re in res.DefaultIfEmpty()
                                    join mb in db.mbMatBangs on p.ApartmentId equals mb.MaMB into matBang from mb in matBang.DefaultIfEmpty()
                                    join nv in db.tnNhanViens on p.EmployeeId equals nv.MaNV into tempEmployee
                                    from nv in tempEmployee.DefaultIfEmpty()
                                    where p.TowerId == maTN
                                        & p.DateCreate >= tuNgay
                                        & p.DateCreate <= denNgay
                                    orderby p.DateCreate descending
                                    select new
                                    {
                                        p.Id,
                                        re.FullName,
                                        StatusId = p.StatusId,
                                        re.Phone,
                                        EmployeerProcess = p.EmployeeId != null ? nv.HoTenNV : "",
                                        p.ApartmentId,
                                        p.DateOfProcess,
                                        p.DateCreate,
                                        p.Description,
                                        p.EmployeeId,
                                        p.LastComment,
                                        p.ServiceId,
                                        p.TowerId,
                                        ApartmentNo = mb.MaSoMB,
                                        p.DepartmentId,
                                        p.DateBook,
                                        p.AmountDeposit,
                                        p.AmountPeople,
                                        p.ZoneId
                                    };
            }
            else
            {
                if (isAdmin)
                {
                    e.QueryableSource = from p in db.app_BookingServiceExtensions
                                        join re in db.app_Residents on p.ResidentId equals re.Id
                                        join mb in db.mbMatBangs on p.ApartmentId equals mb.MaMB
                                        join nv in db.tnNhanViens on p.EmployeeId equals nv.MaNV into tempEmployee
                                        from nv in tempEmployee.DefaultIfEmpty()
                                        where p.TowerId == maTN
                                            & p.DateCreate >= tuNgay
                                            & p.DateCreate <= denNgay
                                            & p.DepartmentId == departmentId
                                        orderby p.DateCreate descending
                                        select new
                                        {
                                            p.Id,
                                            re.FullName,
                                            StatusId = p.StatusId,
                                            re.Phone,
                                            EmployeerProcess = p.EmployeeId != null ? nv.HoTenNV : "",
                                            p.ApartmentId,
                                            p.DateOfProcess,
                                            p.DateCreate,
                                            p.Description,
                                            p.EmployeeId,
                                            p.LastComment,
                                            p.Price,
                                            p.Rating,
                                            p.RatingComment,
                                            p.ServiceId,
                                            p.TowerId,
                                            ApartmentNo = mb.MaSoMB,
                                            p.DepartmentId,
                                            p.DateBook
                                        };
                }
                else
                {
                    e.QueryableSource = from p in db.app_BookingServices
                                        join re in db.app_Residents on p.ResidentId equals re.Id
                                        join mb in db.mbMatBangs on p.ApartmentId equals mb.MaMB
                                        join nv in db.tnNhanViens on p.EmployeeId equals nv.MaNV into tempEmployee
                                        from nv in tempEmployee.DefaultIfEmpty()
                                        where p.TowerId == maTN
                                            & p.DateCreate >= tuNgay
                                            & p.DateCreate <= denNgay
                                            & p.EmployeeId == Common.User.MaNV
                                        orderby p.DateCreate descending
                                        select new
                                        {
                                            p.Id,
                                            re.FullName,
                                            StatusId = p.StatusId,
                                            re.Phone,
                                            EmployeerProcess = p.EmployeeId != null ? nv.HoTenNV : "",
                                            p.ApartmentId,
                                            p.DateOfProcess,
                                            p.DateCreate,
                                            p.Description,
                                            p.EmployeeId,
                                            p.LastComment,
                                            p.ServiceId,
                                            p.TowerId,
                                            ApartmentNo = mb.MaSoMB,
                                            p.DepartmentId,
                                            p.DateBook,
                                            p.AmountDeposit,
                                            p.AmountPeople,
                                            p.ZoneId
                                        };
                }
            }
            e.Tag = db;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookUpEditService.DataSource = (from dv in db.dvLoaiDichVus
                                                join s in db.app_TowerSettingServices on dv.ID equals s.ServiceId
                                                where s.TowerId == (byte)itemToaNha.EditValue
                                                select new
                                                {
                                                    Name = dv.TenHienThi,
                                                    Id = s.ServiceId
                                                });
                lookUpEditDepartment.DataSource = db.tnPhongBans.Where(p => p.MaTN == (byte)itemToaNha.EditValue).Select(p => new {
                    Id = p.MaPB,
                    Name = p.TenPB
                });
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcService);
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.IsTower = true;
            frm.TowerId = (byte)itemToaNha.EditValue;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        /// <summary>
        /// 2: Xác nhận cọc; 3: Xác nhận hoàn thành; 4: Từ chối
        /// </summary>
        /// <param name="typeId"></param>
        void Process(byte typeId)
        {
            var id = (long?)gvService.GetFocusedRowCellValue("Id");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Tiện ích], xin cảm ơn.");
                return; 
            }

            var db = new MasterDataContext();
            var obj = db.app_BookingServices.FirstOrDefault(P => P.Id == id);
            if (obj != null)
            {     
                var frm = new Building.AppVime.ServiceBasic.frmProcess();
                frm.Id = id;
                frm.TowerId = Convert.ToInt32(itemToaNha.EditValue);
                frm.StatusId = typeId;
                frm.ServiceName = lookUpEditService.GetDisplayText(gvService.GetFocusedRowCellValue("ServiceId"));
                frm.TowerName = lkToaNha.GetDisplayText(itemToaNha.EditValue);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }else
            {
                DialogBox.Error("[Tiện ích] này không có trong hệ thống. Vui lòng thử lại, xin cảm ơn.");
                LoadData();
            }
        }

        private void gvService_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        void Clicks()
        {
            if (gvService.FocusedRowHandle < 0)
            {
                gcHistory.DataSource = null;
                return;
            }

            try
            {                
                var db = new MasterDataContext();
                gcHistory.DataSource = (from p in db.app_BookingHistories
                                        join nv in db.tnNhanViens on p.EmployeeId equals nv.MaNV into tempEmployee
                                        from nv in tempEmployee.DefaultIfEmpty()
                                        where p.BookingId == Convert.ToInt32(gvService.GetFocusedRowCellValue("Id"))
                                        orderby p.DateCreate descending
                                        select new
                                        {
                                            p.DateCreate,
                                            p.Description,
                                            p.IsCustomer,
                                            p.StatusId,
                                            EmployeerName = nv.HoTenNV
                                        });

                db.Dispose();
            }catch { }

            SetEnableControl();
        }

        private void itemTuChoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(4);
        }

        private void itemXacNhanCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(2);
        }

        private void itemXacNhanHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(3);
        }

        private void gvService_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            SetEnableControl();
        }

        void SetEnableControl()
        {
            try
            {
                var deposit = Convert.ToDecimal(gvService.GetFocusedRowCellValue("AmountDeposit"));
                var statusId = Convert.ToByte(gvService.GetFocusedRowCellValue("StatusId"));

                switch (statusId)
                {
                    case 1:
                        if (deposit <= 0)
                        {
                            itemXacNhanCoc.Enabled = false;
                            itemXacNhanHoanThanh.Enabled = IsXacNhanHoanThanh & true;
                        }
                        else
                        {
                            itemXacNhanCoc.Enabled = IsXacNhanCoc & true;
                            itemXacNhanHoanThanh.Enabled = false;
                        }
                        break;

                    case 2:
                        itemXacNhanCoc.Enabled = false;
                        itemXacNhanHoanThanh.Enabled = IsXacNhanHoanThanh & true;
                        break;

                    default:
                        itemXacNhanCoc.Enabled = false;
                        itemXacNhanHoanThanh.Enabled = false;
                        break;
                }
            }
            catch { }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (long?)gvService.GetFocusedRowCellValue("Id");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Tiện ích], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

            var db = new MasterDataContext();
            var objS = db.app_BookingServices.FirstOrDefault(p => p.Id == id);
            if (objS != null)
            {
                try
                {
                    var listDetail = db.app_BookingDetails.Where(p => p.BookingId == id);
                    db.app_BookingDetails.DeleteAllOnSubmit(listDetail);

                    var listHis = db.app_BookingHistories.Where(p => p.BookingId == id);
                    db.app_BookingHistories.DeleteAllOnSubmit(listHis);

                    db.app_BookingServices.DeleteOnSubmit(objS);
                    db.SubmitChanges();

                    LoadData();
                }
                catch
                {
                    DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau, xin cảm ơn.");
                }
            }
        }
    }
}
