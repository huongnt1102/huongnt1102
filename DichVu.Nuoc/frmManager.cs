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

using DevExpress.XtraGrid;
using System.Net;

namespace DichVu.Nuoc
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
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

                
                switch (tcChiTietNuoc.SelectedTabPageIndex)
                {
                    case 0:
                        gcChiTiet.DataSource = (from nc in db.dvNuocChiTiets
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
                        break;
                    case 1:
                        if (grvSoNuoc.GetFocusedRowCellValue("LinkUrl") == null) return;
                        LoadImageUpLoad(grvSoNuoc.GetFocusedRowCellValue("LinkUrl").ToString());
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        void LoadImageUpLoad(string url)
        {
            pic.Image = null;
            try
            {
                var db = new MasterDataContext();
                if (url != null)
                {
                    var ftp = db.tblConfigs.FirstOrDefault();
                    if (!url.Contains("http"))
                    {
                        url = ftp.WebUrl + url;
                    }

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var request = WebRequest.Create(url);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            pic.Image = Bitmap.FromStream(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

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
                {
                    RefreshData();
                    var db = new MasterDataContext();
                    var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == (int)grvSoNuoc.GetFocusedRowCellValue("ID") & p.MaLDV == 9);
                    var Nuoc = db.dvNuocs.FirstOrDefault(p => p.ID == (int)grvSoNuoc.GetFocusedRowCellValue("ID"));
                    if (KTIDHD != null)
                    {
                        if (DialogBox.Question("Bạn muốn cập nhật hoá đơn nước căn này sau khi được chỉnh sủa không") ==
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
                    var obj = db.dvNuocs.Single(p => p.ID == (int)grvSoNuoc.GetRowCellValue(q, "ID"));
                    var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == obj.ID & p.MaLDV == 9);
                    if (KTIDHD == null)
                    {
                        db.dvNuocs.DeleteOnSubmit(obj);
                    }
                    else
                    {
                        DialogBox.Error("Phí nước căn này đã có hoá đơn phát sinh vui lòng kiểm tra lại !");
                        var frm = new LandSoftBuilding.Receivables.frmManagerKiemTra();
                        frm.LinkID = (int)grvSoNuoc.GetRowCellValue(q, "ID");
                        frm.MaKH = (int)KTIDHD.MaKH;
                        frm.MaLDV = 9;
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
            Library.Commoncls.ExportExcel(gcSoNuoc);
            //var db = new MasterDataContext();
            //try
            //{
            //    var _MaTN = (byte)itemToaNha.EditValue;
            //    var _Ngay = new DateTime(Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemMonth.EditValue), 1);
            //    var ltData = from n in db.dvNuocs
            //                 join b in db.mbMatBangs on n.MaMB equals b.MaMB
            //                 join dh in db.dvNuocDongHos on n.MaDH equals dh.ID into tblDongHo
            //                 from dh in tblDongHo.DefaultIfEmpty()
            //                 where n.MaTN == _MaTN & SqlMethods.DateDiffMonth(n.NgayTB, _Ngay) == 0
            //                 orderby b.MaSoMB
            //                 select new
            //                 {
            //                     Thang = n.NgayTB.Value.Month + 1,
            //                     Nam = n.NgayTB.Value.Month == 12 ? n.NgayTB.Value.Year + 1 : n.NgayTB.Value.Year,
            //                     b.MaSoMB,
            //                     dh.SoDH,
            //                     TuNgay = n.DenNgay.Value.AddDays(1),
            //                     DenNgay = n.DenNgay.Value.AddMonths(1),
            //                     NgayTT = new DateTime(n.DenNgay.Value.AddMonths(1).Year, n.DenNgay.Value.AddMonths(1).Month,1),
            //                     ChiSoCu = n.ChiSoMoi,
            //                     ChiSoMoi = 0,
            //                     n.HeSo,
            //                     SoTieuThu = 0,
            //                     n.TyLeVAT,
            //                     n.TyLeBVMT,
            //                     SoTien = 0,
            //                     n.SoNguoiUD,
            //                     n.SoM3UD1Nguoi,
            //                     n.SoM3UD,
            //                     DienGiai = ""
            //                 };

            //    var tblData = SqlCommon.LINQToDataTable(ltData);
            //    ExportToExcel.exportDataToExcel(tblData);
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
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

            FormatCondition();
        }

        private void FormatCondition()
        {
            var db = new MasterDataContext();
            var chiSo = db.dvColors.FirstOrDefault(_ => _.TableName.ToLower() == "DVNUOC");
            if (chiSo == null || chiSo.ChiSoMin == null || chiSo.ChiSoEqual==null) return;

            #region Chỉ số mới

            var gridFormatRule2 = new GridFormatRule();
            var ruleIconSet1 = new FormatConditionRuleIconSet();
            var iconSet1 = new FormatConditionIconSet();
            var icon1 = new FormatConditionIconSetIcon();
            var icon2 = new FormatConditionIconSetIcon();
            var icon3 = new FormatConditionIconSetIcon();

            gridFormatRule2.Column = colChenhLech;
            //gridFormatRule2.ColumnApplyTo = colChenhLech;
            //gridFormatRule2.Name = "FormatChiSoMoi";
            iconSet1.CategoryName = "Positive/Negative";
            icon1.PredefinedName = "Triangles3_3.png";
            icon1.Value = new decimal(new int[] {
                -1,
                -1,
                -1,
                -2147483648});
            icon2.Value = new decimal(new int[] { (int)chiSo.ChiSoMin, 0, 0, 0 });
            icon3.Value = new decimal(new int[] { (int)chiSo.ChiSoEqual, 0, 0, 0 });
            icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
            icon2.PredefinedName = "Triangles3_2.png";
            icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
            icon3.PredefinedName = "Triangles3_1.png";
            iconSet1.Icons.Add(icon1);
            iconSet1.Icons.Add(icon2);
            iconSet1.Icons.Add(icon3);
            iconSet1.Name = "PositiveNegativeTriangles";
            iconSet1.ValueType = FormatConditionValueType.Number;
            ruleIconSet1.IconSet = iconSet1;
            gridFormatRule2.Rule = ruleIconSet1;

            grvSoNuoc.FormatRules.Add(gridFormatRule2);
            #endregion
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Ngay = new DateTime(Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemMonth.EditValue), 1);
            var db = new MasterDataContext();

            e.QueryableSource = from n in db.dvNuocs
                join b in db.mbMatBangs on n.MaMB equals b.MaMB
                join t in db.mbTangLaus on b.MaTL equals t.MaTL
                join kn in db.mbKhoiNhas on t.MaKN equals kn.MaKN
                join k in db.tnKhachHangs on n.MaKH equals k.MaKH
                join lmb in db.mbLoaiMatBangs on b.MaLMB equals lmb.MaLMB
                join dh in db.dvNuocDongHos on n.MaDH equals dh.ID into tblDongHo
                from dh in tblDongHo.DefaultIfEmpty()
                join nvn in db.tnNhanViens on n.MaNVN equals nvn.MaNV
                from nvs in db.tnNhanViens.Where(nvs => nvs.MaNV == n.MaNVS).DefaultIfEmpty()
                where n.MaTN == _MaTN & SqlMethods.DateDiffMonth(n.NgayTB, _Ngay) == 0
                orderby b.MaSoMB
                select new
                {
                    n.ID,
                    kn.TenKN,
                    n.MaMB, k.MaPhu,
                    IsConfirm = 0,
                    b.MaSoMB,
                    KhachHang = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                    dh.SoDH,
                    n.ChiSoCu,
                    n.ChiSoMoi,
                    n.HeSo,
                    SoTieuThu = n.SoTieuThu.GetValueOrDefault() + n.SoTieuThuDHCu.GetValueOrDefault(),
                    n.ThanhTien,
                    n.TyLeVAT,
                    n.TienVAT,
                    n.TyLeBVMT,
                    n.TienBVMT,
                    n.TienTT,
                    n.SoNguoiUD,
                    SoM3UDNguoi = n.SoM3UD1Nguoi,
                    n.SoM3UD,
                    n.DienGiai,
                    t.TenTL,
                    n.TuNgay,
                    n.DenNgay,
                    n.NgayNhap,
                    NguoiNhap = nvn.HoTenNV,
                    n.NgaySua,
                    NguoiSua = nvs.HoTenNV,
                    ChenhLech = n.SoTieuThu -
                                (db.dvNuocs.Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, _Ngay) > 0)
                                     .OrderByDescending(_ => _.DenNgay).First() != null
                                    ? (int)db.dvNuocs
                                        .Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, _Ngay) > 0)
                                        .OrderByDescending(_ => _.DenNgay).First().SoTieuThu
                                    : 0),
                    SoTieuThuDHCu =
                        (db.dvNuocs.Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, _Ngay) > 0)
                             .OrderByDescending(_ => _.DenNgay).First() != null
                            ? (int)db.dvNuocs
                                .Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, _Ngay) > 0)
                                .OrderByDescending(_ => _.DenNgay).First().SoTieuThu
                            : 0),
                    //n.SoTieuThuDHCu,
                    n.NgayTT,
                    n.LinkUrl,
                    lmb.TenLMB
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

            FormatCondition();
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
                    if ((decimal)(grvSoNuoc.GetRowCellValue(e.RowHandle, "ChenhLech") ?? 0) > 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }

            }
            catch
            { }
        }

        private async void itemTaoLaiCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No)
                return;

            await System.Threading.Tasks.Task.Run(() => { TaoLaiCongNo(); });

            Library.DialogBox.Success();
        }

        private void TaoLaiCongNo()
        {
            var indexs = grvSoNuoc.GetSelectedRows();
            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    try
                    {
                        var _ID = (int?)grvSoNuoc.GetRowCellValue(i, "ID");

                        var tx = db.dvNuocs.FirstOrDefault(o => o.ID == _ID);
                        if (tx == null) continue;
                        db.dvHoaDon_InsertAll_LoaiDichVu(tx.MaTN, tx.NgayTT.Value.Month, tx.NgayTT.Value.Year, Library.Common.User.MaNV, tx.ID, 9);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void tcChiTietNuoc_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            DetailRecord();
        }

        //private void itemInPhieuThuNuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    using (var f = new LandSoftBuilding.Report.Nuoc.frmOption())
        //    {
        //        f.CateID = 1;
        //        f.ShowDialog();

        //    }




        //    //objNV = Common.User;
        //    //var rpt = new LandSoftBuilding.Report.Nuoc.rptPhieuThu(Convert.ToInt32(itemMonth.EditValue), Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemToaNha.EditValue), objNV.HoTenNV);
        //    //rpt.ShowPreviewDialog();
        //}

        //private void itemInChiTietKH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    objNV = Common.User;
        //    var rpt = new LandSoftBuilding.Report.Nuoc.rptChiTiet(Convert.ToInt32(itemMonth.EditValue), Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemToaNha.EditValue), objNV.HoTenNV);
        //    rpt.ShowPreviewDialog();
        //}

        //private void itemCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    var rpt = new LandSoftBuilding.Report.Nuoc.rptCongNo(Convert.ToInt32(itemMonth.EditValue), Convert.ToInt32(itemYear.EditValue), Convert.ToInt32(itemToaNha.EditValue));
        //    rpt.ShowPreviewDialog();
        //}

        //private void itemPhieuThuNuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    using (var f = new LandSoftBuilding.Report.Nuoc.frmOption())
        //    {
        //        f.CateID = 1;
        //        f.ShowDialog();
        //    }
        //}

        //private void itemChiTietKHNuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    using (var f = new LandSoftBuilding.Report.Nuoc.frmOption())
        //    {
        //        f.CateID = 2;
        //        f.ShowDialog();
        //    }
        //}

        //private void itemCongNoNuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    using (var f = new LandSoftBuilding.Report.Nuoc.frmOption())
        //    {
        //        f.CateID = 3;
        //        f.ShowDialog();
        //    }
        //}
    }
}