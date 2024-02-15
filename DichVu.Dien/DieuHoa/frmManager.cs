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

namespace DichVu.Dien.DieuHoa
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            gcDien.DataSource = null;
            gcDien.DataSource = linqInstantFeedbackSource1;
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
                if (gvDien.FocusedRowHandle < 0)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from nc in db.dvDienDH_ChiTiets
                                        join dm in db.dvDienDH_DinhMucs on nc.MaDM equals dm.ID
                                        where nc.MaDien == (int)gvDien.GetFocusedRowCellValue("ID")
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
                f.MaTN = _MaTN;
                f.Thang = _Thang;
                f.Nam = _Nam;
                f.ShowDialog();
                if (f.IsSave)
                    RefreshData();
            }
        }

        void EditRecord()
        {
            if (gvDien.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn bản ghi, xin cảm ơn.");
                return;
            }

            using (var f = new frmEdit())
            {
                f.ID = (int)gvDien.GetFocusedRowCellValue("ID");
                f.MaTN = (byte)itemToaNha.EditValue;
                f.ShowDialog();
                if (f.IsSave)
                {
                    RefreshData();
                    var db = new MasterDataContext();
                    var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == (int)gvDien.GetFocusedRowCellValue("ID") & p.MaLDV == 11);
                    var Nuoc = db.dvDienDHs.FirstOrDefault(p => p.ID == (int)gvDien.GetFocusedRowCellValue("ID"));
                    if (KTIDHD != null)
                    {
                        if (DialogBox.Question("Bạn muốn cập nhật hoá đơn điện căn này sau khi được chỉnh sủa không") ==
                            DialogResult.No)
                        {
                            // return;
                        }
                        else
                        {
                            KTIDHD.PhiDV = Nuoc.TienTT;
                            KTIDHD.TienTT = Nuoc.TienTT;
                            KTIDHD.PhaiThu = Nuoc.TienTT;
                            KTIDHD.ConNo = Nuoc.TienTT - ((from ct in db.ptChiTietPhieuThus
                                                           join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                           where
                                                               ct.TableName == "dvHoaDon" & ct.LinkID == KTIDHD.ID //&
                                                           //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0
                                                           select ct.SoTien).Sum().GetValueOrDefault());
                            KTIDHD.MaNVS = Common.User.MaNV;
                            KTIDHD.NgaySua = DateTime.Now;
                            db.SubmitChanges();

                        }

                    }

                }
            }
        }

        void DeleteRecord()
        {
            int[] indexs = gvDien.GetSelectedRows();
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
                    var obj = db.dvDienDHs.Single(p => p.ID == (int)gvDien.GetRowCellValue(q, "ID"));
                    var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == (int)gvDien.GetRowCellValue(q, "ID") & p.MaLDV == 11);
                    if (KTIDHD == null)
                    {
                        db.dvDienDHs.DeleteOnSubmit(obj);
                    }
                    else
                    {
                        DialogBox.Error("Phí điện căn này đã có hoá đơn phát sinh vui lòng kiểm tra lại !");
                        var frm = new LandSoftBuilding.Receivables.frmManagerKiemTra();
                        frm.LinkID = (int)gvDien.GetRowCellValue(q, "ID");
                        frm.MaKH = (int)KTIDHD.MaKH;
                        frm.MaLDV = 11;
                        // return;
                        frm.ShowDialog();
                    }
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
                var ltData = from n in db.dvDienDHs
                             join b in db.mbMatBangs on n.MaMB equals b.MaMB
                             join dh in db.dvDienDH_DongHos on n.MaDH equals dh.ID into tblDongHo
                             from dh in tblDongHo.DefaultIfEmpty()
                             where n.MaTN == _MaTN & SqlMethods.DateDiffMonth(n.NgayTB, _Ngay) == 0
                             orderby b.MaSoMB
                             select new
                             {
                                 Thang =  n.NgayTB.Value.Month == 12 ? 1: (n.NgayTB.Value.Month + 1),
                                 Nam = n.NgayTB.Value.Month == 12 ? n.NgayTB.Value.Year + 1 : n.NgayTB.Value.Year,
                                 b.MaSoMB,
                                 dh.SoDH,
                                 NgayTT = n.DenNgay.Value.AddMonths(1),
                                 TuNgay = n.DenNgay.Value.AddDays(1),
                                 DenNgay = n.DenNgay.Value.AddMonths(1),
                                 ChiSoCu = n.ChiSoMoi,
                                 ChiSoMoi = 0,
                                 n.HeSo,
                                 SoTieuThu = 0,
                                 n.TyLeVAT,
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

            gvDien.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

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
            e.QueryableSource = from n in db.dvDienDHs
                                join b in db.mbMatBangs on n.MaMB equals b.MaMB
                                join t in db.mbTangLaus on b.MaTL equals t.MaTL
                                join kn in db.mbKhoiNhas on t.MaKN equals kn.MaKN
                                join k in db.tnKhachHangs on n.MaKH equals k.MaKH
                                join dh in db.dvDienDH_DongHos on n.MaDH equals dh.ID into tblDongHo
                                from dh in tblDongHo.DefaultIfEmpty()
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
                                    dh.SoDH,
                                    n.ChiSoCu,
                                    n.ChiSoMoi,
                                    n.HeSo,
                                    n.SoTieuThu,
                                    //ChenhLech = db.funcChenhLechDienDH(n.MaMB, n.SoTieuThu, _Ngay),
                                    n.ThanhTien,
                                    n.TyLeVAT,
                                    n.TienVAT,
                                    n.TienTT,
                                    n.DienGiai,
                                    t.TenTL,
                                    n.NgayTT,
                                    n.TuNgay,
                                    n.DenNgay,
                                    n.NgayNhap,
                                    NguoiNhap = nvn.HoTenNV,
                                    n.NgaySua,
                                    NguoiSua = nvs.HoTenNV
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

        private void gvDien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DetailRecord();
        }

        private void gvDien_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
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

        private void gvDien_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //try
            //{
            //    if (e.Column.FieldName == "STT")
            //    {
            //        if ((decimal)gvDien.GetRowCellValue(e.RowHandle, "ChenhLech") > 0)
            //        {
            //            e.Appearance.BackColor = Color.Red;
            //        }
            //    }
            //}
            //catch { }
        }
    }
}