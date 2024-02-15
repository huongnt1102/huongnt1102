using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraSplashScreen;

namespace DichVu.Khac
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmManager()
        {
            InitializeComponent();
            
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        public class Data { public System.Int32 ID { get; set; } public System.String MaPhu { get; set; } public System.String SoCT { get; set; } public System.DateTime? NgayCT { get; set; } public System.String KyHieu { get; set; } public System.String TenKH { get; set; } public System.String MaSoMB { get; set; } public System.Decimal? SoLuong { get; set; } public System.String TenDVT { get; set; } public System.Decimal? DonGia { get; set; } public System.Decimal? ThanhTien { get; set; } public System.DateTime? NgayTT { get; set; } public System.Int32? KyTT { get; set; } public System.Boolean? IsLapLai { get; set; } public System.Decimal? TienTT { get; set; } public System.Decimal? ThueGTGT { get; set; } public System.Decimal? TienThueGTGT { get; set; } public System.String KyHieuLT { get; set; } public System.Decimal? TyGia { get; set; } public System.Decimal? TienTTQD { get; set; } public System.DateTime? TuNgay { get; set; } public System.DateTime? DenNgay { get; set; } public System.Boolean? IsNgungSuDung { get; set; } public System.String DienGiai { get; set; } public System.String NguoiNhap { get; set; } public System.DateTime? NgayNhap { get; set; } public System.String NguoiSua { get; set; } public System.DateTime? NgaySua { get; set; } public System.Decimal? TienTruocThue { get; set; } }

        private System.Collections.Generic.List<Data> GetDatas(byte? matn, int? _MaLDV, System.DateTime? tuNgay, System.DateTime? denNgay)
        {
            using(Library.MasterDataContext db = new MasterDataContext())
            {
                return (from dvk in db.dvDichVuKhacs
                        join lt in db.LoaiTiens on dvk.MaLT equals lt.ID
                        join dvt in db.DonViTinhs on dvk.MaDVT equals dvt.ID
                        join kh in db.tnKhachHangs on dvk.MaKH equals kh.MaKH
                        join mb in db.mbMatBangs on dvk.MaMB equals mb.MaMB into tblMatBang
                        from mb in tblMatBang.DefaultIfEmpty()
                        join nvn in db.tnNhanViens on dvk.MaNVN equals nvn.MaNV
                        join nvs in db.tnNhanViens on dvk.MaNVS equals nvs.MaNV into tblNguoiSua
                        from nvs in tblNguoiSua.DefaultIfEmpty()
                        where dvk.MaTN == matn & dvk.MaLDV == _MaLDV & SqlMethods.DateDiffDay(tuNgay, dvk.NgayTT) >= 0 & SqlMethods.DateDiffDay(dvk.NgayTT, denNgay) >= 0
                        orderby dvk.NgayCT descending
                        select new Data
                        {
                            ID = dvk.ID,
                            MaPhu = kh.MaPhu,
                            SoCT = dvk.SoCT,
                            NgayCT = dvk.NgayCT,
                            KyHieu = kh.KyHieu,
                            TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                            MaSoMB = mb.MaSoMB,
                            SoLuong = dvk.SoLuong,
                            TenDVT = dvt.TenDVT,
                            DonGia = dvk.DonGia,
                            ThanhTien = dvk.ThanhTien,
                            NgayTT = dvk.NgayTT,
                            KyTT = dvk.KyTT,
                            IsLapLai = dvk.IsLapLai,
                            TienTT = dvk.TienTT,
                            ThueGTGT = dvk.ThueGTGT,
                            TienThueGTGT = dvk.TienThueGTGT,
                            KyHieuLT = lt.KyHieuLT,
                            TyGia = dvk.TyGia,
                            TienTTQD = dvk.TienTTQD,
                            TuNgay = dvk.TuNgay,
                            DenNgay = dvk.DenNgay,
                            IsNgungSuDung = dvk.IsNgungSuDung,
                            DienGiai = dvk.DienGiai,
                            NguoiNhap = nvn.HoTenNV,
                            NgayNhap = dvk.NgayNhap,
                            NguoiSua = nvs.HoTenNV,
                            NgaySua = dvk.NgaySua,
                            TienTruocThue = dvk.TienTruocThue,
                        }).ToList();
            }
        }
        async void LoadData()
        {
            //gcDichVuKhac.DataSource = null;
           // gcDichVuKhac.DataSource = linqInstantFeedbackSource1;
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;
            var _MaLDV = (int?)itemLoaiDichVu.EditValue;

            var db = new MasterDataContext();

            System.Collections.Generic.List<Data> datas = new List<Data>();
            await System.Threading.Tasks.Task.Run(() => { datas = GetDatas(matn, _MaLDV, tuNgay, denNgay); });

            gcDichVuKhac.DataSource = datas;

            gvDichVuKhac.FocusedRowHandle = -1;
        }

        private string GetClass<T>(List<T> iList)
        {
            var className = "DataDanhSachMatBang";
            var text = "public class " + className + " { ";

            System.ComponentModel.PropertyDescriptorCollection propertyDescriptorCollection = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) type = Nullable.GetUnderlyingType(type);

                text = text + " public " + type + " " + propertyDescriptor.Name + " {get; set;}";
            }

            text = text + " }";
            return text;
        }

        void RefreshData()
        {
           // linqInstantFeedbackSource1.Refresh();
            LoadData();
        }

        void AddRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");  
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.MaTN = _MaTN;
                frm.MaLDV = (int?)itemLoaiDichVu.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvDichVuKhac.GetFocusedRowCellValue("ID");  
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");  
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.ID = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void DeleteRecord()
        {
            var indexs = gvDichVuKhac.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");  
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var dvk = db.dvDichVuKhacs.Single(p => p.ID == (int)gvDichVuKhac.GetRowCellValue(i, "ID"));
                db.dvDichVuKhacs.DeleteOnSubmit(dvk);
            }

            try
            {
                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void ImportRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");  
                return;
            }

            using (var frm = new frmImport())
            {
                frm.MaTN = _MaTN.Value;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            gvDichVuKhac.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            lkNV.DataSource = db.tnNhanViens;
            var ltLoaiDichVu = (from l in db.dvLoaiDichVus where l.ParentID == 12 select new { l.ID, TenLDV = l.TenHienThi }).ToList();
            lkLoaiDichVu.DataSource = ltLoaiDichVu;

            if (ltLoaiDichVu.Count > 0)
            {
                itemLoaiDichVu.EditValue = ltLoaiDichVu[0].ID;
            }

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;
            var _MaLDV = (int?)itemLoaiDichVu.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from dvk in db.dvDichVuKhacs
                                join lt in db.LoaiTiens on dvk.MaLT equals lt.ID
                                join dvt in db.DonViTinhs on dvk.MaDVT equals dvt.ID
                                join kh in db.tnKhachHangs on dvk.MaKH equals kh.MaKH
                                join mb in db.mbMatBangs on dvk.MaMB equals mb.MaMB into tblMatBang
                                from mb in tblMatBang.DefaultIfEmpty()
                                join nvn in db.tnNhanViens on dvk.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on dvk.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where dvk.MaTN == matn & dvk.MaLDV == _MaLDV & SqlMethods.DateDiffDay(tuNgay, dvk.NgayTT) >= 0 & SqlMethods.DateDiffDay(dvk.NgayTT, denNgay) >= 0
                                orderby dvk.NgayCT descending
                                select new
                                {
                                    dvk.ID,
                                    dvk.SoCT,
                                    dvk.NgayCT,
                                    kh.KyHieu,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    mb.MaSoMB,
                                    dvk.SoLuong,
                                    dvt.TenDVT,
                                    dvk.DonGia,
                                    dvk.ThanhTien,
                                    dvk.NgayTT,
                                    dvk.KyTT,
                                    dvk.IsLapLai,
                                    dvk.TienTT,
                                    dvk.ThueGTGT,
                                    dvk.TienThueGTGT,
                                    lt.KyHieuLT,
                                    dvk.TyGia,
                                    dvk.TienTTQD,
                                    dvk.TuNgay,
                                    dvk.DenNgay,
                                    dvk.IsNgungSuDung,
                                    dvk.DienGiai,
                                    NguoiNhap = nvn.HoTenNV,
                                    dvk.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    dvk.NgaySua,
                                    dvk.TienTruocThue,
                                };
            e.Tag = db;
            gvDichVuKhac.FocusedRowHandle = -1;
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDichVuKhac);
        }

        private void gvDichVuKhac_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var id = (int?)gvDichVuKhac.GetFocusedRowCellValue("ID");          
            if (id!=null)
            {
                gcLS.DataSource = db.LichSuThayDoiDG_DVCBs.Where(p => p.MaDVCB == id);
            }
        }

        private async void itemTaoLaiCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No)
                return;

            await System.Threading.Tasks.Task.Run(() => { TaoLaiCongNo(); });

            Library.DialogBox.Success();
            LoadData();
        }

        private void TaoLaiCongNo()
        {
            var indexs = gvDichVuKhac.GetSelectedRows();
            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    try
                    {
                        var _ID = (int?)gvDichVuKhac.GetRowCellValue(i, "ID");

                        var tx = db.dvDichVuKhacs.FirstOrDefault(o => o.ID == _ID);
                        if (tx == null) continue;
                        db.dvHoaDon_InsertAll_LoaiDichVu(tx.MaTN, tx.NgayTT.Value.Month, tx.NgayTT.Value.Year, Library.Common.User.MaNV, tx.ID, 13);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void CapNhatKhachHang(System.DateTime tuNgay, System.DateTime denNgay, byte matn, int? _MaLDV)
        {
            using(var db = new Library.MasterDataContext())
            {
                var dichVus = (from dvk in db.dvDichVuKhacs
                               join mb in db.mbMatBangs on dvk.MaMB equals mb.MaMB
                               where dvk.MaTN == matn & dvk.MaLDV == _MaLDV & SqlMethods.DateDiffDay(tuNgay, dvk.NgayTT) >= 0 & SqlMethods.DateDiffDay(dvk.NgayTT, denNgay) >= 0 & dvk.MaKH != mb.MaKH
                               orderby dvk.NgayCT descending
                               select new
                               {
                                   dvk.ID,
                                   mb.MaKH,
                                   MaKhachHang = dvk.MaKH
                               }).ToList();
                foreach (var item in dichVus)
                {
                    if (item.MaKH != item.MaKhachHang)
                    {
                        var dvk = db.dvDichVuKhacs.FirstOrDefault(_ => _.ID == item.ID);
                        if (dvk != null & dvk.MaKH != item.MaKH)
                        {
                            dvk.MaKH = item.MaKH;
                        }
                    }

                }

                db.SubmitChanges();
            }
        }

        private async void itemCapNhatKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;
            var _MaLDV = (int?)itemLoaiDichVu.EditValue;

            await System.Threading.Tasks.Task.Run(() => { CapNhatKhachHang(tuNgay, denNgay, matn, _MaLDV); });

            Library.DialogBox.Success();
            LoadData();
        }

        private void itemCapNhatTyGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var frm = new frmTyGia())
            {
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        private void itemCapNhatDonGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvDichVuKhac.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn dữ liệu cần cập nhật đơn giá");
                return;
            }

            using (var frm = new frmDonGia())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    using (var db = new MasterDataContext())
                    {
                        handle = ShowProgressPanel();

                        foreach (var i in indexs)
                        {
                            try
                            {
                                var _ID = (int?)gvDichVuKhac.GetRowCellValue(i, "ID");

                                var tx = db.dvDichVuKhacs.FirstOrDefault(o => o.ID == _ID);
                                if (tx == null) continue;
                                Library.Class.Connect.QueryConnect.QueryData<bool>("dvDichVuKhac_CapNhatDonGia",
                                    new
                                    {
                                        Id = _ID,
                                        DonGia = frm.DonGia,
                                        MaNV = Common.User.MaNV
                                    });
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        CloseProgressPanel(handle);
                    }
            }

            this.RefreshData();
        }

        #region overlay
        IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }

        void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        IOverlaySplashScreenHandle handle = null;
        #endregion
    }
}