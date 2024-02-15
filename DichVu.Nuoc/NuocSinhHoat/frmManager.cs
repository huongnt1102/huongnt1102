using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Threading;

namespace DichVu.Nuoc.NuocSinhHoat
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            gcSoNuoc.DataSource = null;
            gcSoNuoc.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void DetailRecord()
        {
            var db = new MasterDataContext();
            try
            {
                if (grvSoNuoc.FocusedRowHandle < 0)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from nc in db.dvNuocSinhHoatChiTiets
                                        join dm in db.dvNuocDinhMucs on nc.MaDM equals dm.ID
                                        where nc.MaNuoc == (int)grvSoNuoc.GetFocusedRowCellValue("ID")
                                        orderby dm.STT
                                        select new
                                        {
                                            dm.TenDM,
                                            nc.SoLuong,
                                            nc.DonGia,
                                            nc.ThanhTien,
                                            nc.DienGiai
                                        }).ToList();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        void AddRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemMonth.EditValue);
            var _Nam = Convert.ToInt32(itemYear.EditValue);
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new frmEdit())
            {
                f.MaTN = _MaTN.Value;
                f.Thang = _Thang;
                f.Nam = _Nam;
                f.ShowDialog();
                if (f.IsSave)
                    RefreshData();
            }
        }

        void EditRecord()
        {
            if (grvSoNuoc.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn bản ghi, xin cảm ơn.");
                return;
            }

            using (var f = new frmEdit())
            {
                f.ID = (int)grvSoNuoc.GetFocusedRowCellValue("ID");
                f.MaTN = (byte)itemToaNha.EditValue;
                f.ShowDialog();
                if (f.IsSave)
                    RefreshData();
            }
        }

        void DeleteRecord()
        {
            int[] indexs = grvSoNuoc.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (int q in indexs)
                {
                    var obj = db.dvNuocSinhHoats.Single(p => p.ID == (int)grvSoNuoc.GetRowCellValue(q, "ID"));
                    db.dvNuocSinhHoats.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
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

            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        void ExportRecord()
        {
            var db = new MasterDataContext();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _Ngay = new DateTime(Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemMonth.EditValue), 1);
                var ltData = from n in db.dvNuocSinhHoats
                             join b in db.mbMatBangs on n.MaMB equals b.MaMB
                             where n.MaTN == _MaTN & SqlMethods.DateDiffMonth(n.NgayTB, _Ngay) == 0
                             orderby b.MaSoMB
                             select new
                             {
                                 Thang = n.NgayTB.Value.Month == 12 ?1: (n.NgayTB.Value.Month + 1),
                                 Nam = n.NgayTB.Value.Month == 12 ? (n.NgayTB.Value.Year + 1) : n.NgayTB.Value.Year,
                                 b.MaSoMB,
                                 TuNgay = n.DenNgay.Value.AddDays(1),
                                 DenNgay = n.DenNgay.Value.AddMonths(1),
                                 NgayTT = n.DenNgay.Value.AddMonths(1),
                                 ChiSoCu = n.ChiSoMoi,
                                 ChiSoMoi = 0,
                                 SoTieuThuNL = 0,
                                 DauCap_Cu = n.DauCap_Moi,
                                 DauCap_Moi = 0,
                                 DauHoi_Cu = n.DauHoi_Moi,
                                 DauHoi_Moi = 0,
                                 SoTieuThuNN = 0,
                                 SoTieuThu = 0,
                                 n.TyLeVAT,
                                 n.TyLeBVMT,
                                 SoTien = 0,
                                 n.DienGiai
                             };

                var tblData = SqlCommon.LINQToDataTable(ltData);
                ExportToExcel.exportDataToExcel(tblData);
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

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;

            grvSoNuoc.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Library.Common.User.MaTN;
            itemYear.EditValue = DateTime.Now.Year;
            itemMonth.EditValue = DateTime.Now.Month;

            LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Ngay = new DateTime(Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemMonth.EditValue), 1);
            var db = new MasterDataContext();
            e.QueryableSource = from n in db.dvNuocSinhHoats
                                join b in db.mbMatBangs on n.MaMB equals b.MaMB
                                join t in db.mbTangLaus on b.MaTL equals t.MaTL
                                join kn in db.mbKhoiNhas on t.MaKN equals kn.MaKN
                                join k in db.tnKhachHangs on n.MaKH equals k.MaKH
                                join nvn in db.tnNhanViens on n.MaNVN equals nvn.MaNV
                                from nvs in db.tnNhanViens.Where(nvs => nvs.MaNV == n.MaNVS).DefaultIfEmpty()
                                where n.MaTN == _MaTN & SqlMethods.DateDiffMonth(n.NgayTB, _Ngay) == 0
                                orderby b.MaSoMB
                                select new
                                {
                                    n.ID,
                                    kn.TenKN,
                                    n.MaMB,
                                    IsConfirm = 0,
                                    b.MaSoMB,
                                    KhachHang = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                                    n.ChiSoCu,
                                    n.ChiSoMoi,
                                    SoTieuThuNL=n.SoTieuThuNL.GetValueOrDefault(),
                                    n.DauCap_Cu,
                                    n.DauCap_Moi,
                                    n.DauHoi_Cu,
                                    n.DauHoi_Moi,
                                    n.SoTieuThuNN,
                                    SoTieuThu=n.SoTieuThu.GetValueOrDefault(),
                                    n.ThanhTien,
                                    n.TyLeVAT,
                                    n.TienVAT,
                                    n.TyLeBVMT,
                                    n.TienBVMT,
                                    n.TienTT,
                                    n.DienGiai,
                                    t.TenTL,
                                    n.TuNgay,
                                    n.DenNgay,
                                    n.NgayNhap,
                                    NguoiNhap = nvn.HoTenNV,
                                    n.NgaySua,
                                    NguoiSua = nvs.HoTenNV,
                                    ChenhLech = db.funcChenhLechNuocSinhHoat(n.MaMB,n.SoTieuThu,_Ngay),
                                    n.NgayTT
                                };
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void grvSoNuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DetailRecord();
        }

        private void grvSoNuoc_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.DetailRecord();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExportRecord();
        }

        private void grvSoNuoc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "STT")
                {
                    if (Convert.ToDecimal(grvSoNuoc.GetRowCellValue(e.RowHandle, "ChenhLech")) > 0)
                    {
                        e.Appearance.BackColor = Color.Red;

                    }
                }

            }
            catch
            { }
        }
    }
}