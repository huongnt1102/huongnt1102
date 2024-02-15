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

namespace Building.AppVime.ServiceExtension
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

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

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = CommonVime.TowerId;// Common.User.MaTN;

            using (var db = new MasterDataContext())
            {
                var listStatus = db.app_BookingServiceExtensionStatus;
                lookUpEditStatus.DataSource = listStatus;
                lookUpEditStatus2.DataSource = listStatus;

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

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
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
                e.QueryableSource = from p in db.app_BookingServiceExtensions
                                    join re in db.app_Residents on p.ResidentId equals re.Id
                                    join mb in db.mbMatBangs on p.ApartmentId equals mb.MaMB
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
                    e.QueryableSource = from p in db.app_BookingServiceExtensions
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
        /// 1: Tiếp nhận; 2: Đổi trạng thái; 3: Đổi nhân viên; 4: Giao việc
        /// </summary>
        /// <param name="typeId"></param>
        void Process(byte typeId)
        {
            var id = (long?)gvService.GetFocusedRowCellValue("Id");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                return;
            }

            var frm = new Building.AppVime.ServiceExtension.frmProcess();
            frm.Id = id;
            frm.TypeId = typeId;
            frm.TowerId = Convert.ToInt32(itemToaNha.EditValue);
            frm.StatusId = Convert.ToByte(gvService.GetFocusedRowCellValue("StatusId"));
            frm.ServiceName = lookUpEditService.GetDisplayText(gvService.GetFocusedRowCellValue("ServiceId"));
            frm.TowerName = lkToaNha.GetDisplayText(itemToaNha.EditValue);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemTiepNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(1);
        }

        private void itemGiaoViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(4);
        }

        private void itemDoiTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(2);
        }

        private void itemDoiNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(3);
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
                gcHistory.DataSource = (from p in db.app_BookingServiceExtensionHistories
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
        }

        private void itemHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(5);
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (long?)gvService.GetFocusedRowCellValue("Id");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

            var db = new MasterDataContext();
            var objS = db.app_BookingServiceExtensions.FirstOrDefault(p => p.Id == id);
            if (objS != null)
            {
                try
                {
                    var listHis = db.app_BookingServiceExtensionHistories.Where(p => p.BookingId == id);
                    db.app_BookingServiceExtensionHistories.DeleteAllOnSubmit(listHis);

                    db.app_BookingServiceExtensions.DeleteOnSubmit(objS);
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
