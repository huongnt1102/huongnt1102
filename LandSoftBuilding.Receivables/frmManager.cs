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
using DevExpress.XtraGrid;
using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Utils;
using System.Threading.Tasks;
using System.Collections;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;

namespace LandSoftBuilding.Receivables
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        int? TotalDebit = 0;
        public frmManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            //gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
            //dt = GetDataFromStartToEnd(0, 0);
            LoadDataTinhLai();
            LoadDataFirst();
            //FormatCondition();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
            //FormatCondition();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
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
            var db = new MasterDataContext();
            var wait = new WaitDialogForm("Ðang xử lý. Vui lòng chờ...", "LandSoft Building");
            btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            e.QueryableSource = Library.Class.Connect.QueryConnect.Query<Bill>("dbo.dv_hoadon_load_hoa_don", param).AsQueryable();
            btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            wait.Close();
            e.Tag = db;
        }

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Payment();
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Duyet(true);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Duyet(false);
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddNew();
        }

        private void itemDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DuyetAll(true);
        }

        private void itemKhongDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DuyetAll(false);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Export excel hóa đơn dịch vụ", "Export", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            Commoncls.ExportExcel(gcHoaDon);
        }

        private void itemAddMulti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddMulti();
        }

        private void gvHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Load_PhieuThu();
            Load_PhieuKhauTru();
            LoadLichSuMienLai();
        }
        
        private void gvHoaDon_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Load_PhieuThu();
            Load_PhieuKhauTru();
        }

        private void itemCapNhatMaKhachHangNull_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Library.MasterDataContext db = new MasterDataContext())
            {
                var hoaDons = db.dvHoaDons.Where(_ => _.MaKH == null);
                foreach (var item in hoaDons)
                {
                    var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaMB == item.MaMB);
                    if (matBang != null)
                    {
                        item.MaKH = matBang.MaKH;
                    }
                }
                db.SubmitChanges();
                Library.DialogBox.Success();
            }
        }

        private void itemCapNhatMatBangNull_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            using (Library.MasterDataContext db = new MasterDataContext())
            {
                var hoaDons = db.dvHoaDons.Where(_ => _.MaMB == null);
                foreach (var item in hoaDons)
                {
                    var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaKH == item.MaKH);
                    if (matBang != null)
                    {
                        item.MaMB = matBang.MaMB;
                    }
                }
                db.SubmitChanges();
                Library.DialogBox.Success();
            }
        }

        private void itemLamTronSoLieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var param = new Dapper.DynamicParameters();
            param.Add("@TowerId", matn, DbType.Byte, null, null);
            param.Add("@DateFrom", tuNgay, DbType.DateTime, null, null);
            param.Add("@DateTo", denNgay, DbType.DateTime, null, null);
            var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.dvHoaDon_LamTron", param).ToList();
            LoadData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                //if (id == null)
                //{
                //    gcChiTiet.DataSource = null;
                //    return;
                //}
                //var phieuThu = (from ct in db.ptChiTietPhieuThus
                //                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                        where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                        select new { pt.NgayThu, pt.SoPT, ct.DienGiai, SoTien = pt.IsKhauTru == false ? ct.SoTien : (ct.SoTien.GetValueOrDefault() + ct.KhauTru.GetValueOrDefault()) }).ToList();
                //;
                //var phieuThu = db.SoQuy_ThuChis.Where(_ => _.TableName == "dvHoaDon" & _.LinkID == id).Select(_ => new { SoTien = _.DaThu + _.KhauTru - _.ThuThua }).ToList();
                //var soquy = db.SoQuy_ThuChis.Where(_ => _.TableName == "dvHoaDon" & _.LinkID == id).ToList();
                //var hoaDon = db.dvHoaDons.FirstOrDefault(_ => _.ID == id);
                //if (hoaDon != null)
                //{
                //    hoaDon.DaThu = phieuThu.Sum(_ => _.SoTien).GetValueOrDefault();
                //    hoaDon.ConNo = hoaDon.PhaiThu - hoaDon.DaThu;
                //    db.SubmitChanges();

                //    //Load_PhieuThu();
                //    gvHoaDon.SetFocusedRowCellValue("DaThu", hoaDon.DaThu);
                //    gvHoaDon.SetFocusedRowCellValue("ConNo", hoaDon.ConNo);
                //}

                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    return;
                }

                foreach (var i in indexs)
                {
                    if (gvHoaDon.GetRowCellValue(i, "ID") == null || (long)gvHoaDon.GetRowCellValue(i, "ID") <= 0) continue;
                    var param = new Dapper.DynamicParameters();
                    param.Add("@id", (long)gvHoaDon.GetRowCellValue(i, "ID"), DbType.Int64, null, null);
                    var kq = Library.Class.Connect.QueryConnect.Query<update_da_thu>("dbo.dv_update_da_thu", param);
                    if (kq.Count() > 0)
                    {
                        var result = kq.First();
                        gvHoaDon.SetRowCellValue(i,"DaThu", result.DaThu);
                        gvHoaDon.SetRowCellValue(i,"ConNo", result.ConNo);
                    }

                }


            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var db = new Library.MasterDataContext())
            //{
            //    var soQuys = (from sq in db.SoQuy_ThuChis join hd in db.dvHoaDons on sq.LinkID equals hd.ID where sq.TableName == "dvHoaDon" & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(sq.NgayPhieu, hd.NgayTT) >= 0 select new { sq.ID, sq.LinkID, sq.NgayPhieu, hd.NgayTT }).ToList();
            //    foreach (var item in soQuys)
            //    {
            //        var soQuy = db.SoQuy_ThuChis.FirstOrDefault(_ => _.ID == item.ID);
            //        if (soQuy != null)
            //        {
            //            soQuy.Nam = item.NgayTT.Value.Year;
            //            soQuy.Thang = item.NgayTT.Value.Month;
            //            soQuy.NgayPhieu = item.NgayTT;
            //        }
            //    }
            //    db.SubmitChanges();
            //    Library.DialogBox.Success();
            //}
            var param = new Dapper.DynamicParameters();
            var result = Library.Class.Connect.QueryConnect.Query<bool>("dbo.soquy_thuchi_getfull", param);

            Library.DialogBox.Success();
        }

        /// <summary>
        /// Cài đặt lãi xuất - dòng đang chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn hóa đơn muốn cài lãi suất.");
                    return;
                }

                var f = new LaiSuat.frmLaiSuat();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    string id_arr = "";
                    foreach (var i in indexs)
                    {
                        try
                        {
                            var id = (long)gvHoaDon.GetRowCellValue(i, "ID");
                            id_arr = id_arr + id + ",";
                        }
                        catch { }
                        
                    }

                    var model = new { id_arr = id_arr, pt_lai_suat = f.pt_lai_xuat };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);

                    Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoa_don_lai_xuat_edit_2", param);


                    this.LoadDataTinhLai();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
        
        /// <summary>
        /// Cài đặt lãi xuất - tất cả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var f = new LaiSuat.frmLaiSuat();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    var matn = (byte)itemToaNha.EditValue;

                    var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay, pt_lai_suat = f.pt_lai_xuat };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);

                    Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoa_don_lai_xuat_edit_all1", param);


                    this.LoadDataTinhLai();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// CÀI ĐẶT HẠN THANH TOÁN - dòng đang chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn hóa đơn muốn cài hạn thanh toán.");
                    return;
                }

                var f = new HanThanhToan.frmHanThanhToan();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    string id_arr = "";
                    foreach (var i in indexs)
                    {
                        try
                        {
                            var id = (long)gvHoaDon.GetRowCellValue(i, "ID");
                            id_arr = id_arr + id + ",";
                        }
                        catch { }
                    }

                    var model = new { id_arr = id_arr, han_thanh_toan = f.han_thanh_toan };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);

                    Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoa_don_han_thanh_toan_edit_2", param);


                    this.LoadDataTinhLai();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// Cài đặt hạn thanh toán - tất cả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var f = new HanThanhToan.frmHanThanhToan();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    var matn = (byte)itemToaNha.EditValue;

                    var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay, han_thanh_toan = f.han_thanh_toan };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);

                    Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoa_don_han_thanh_toan_edit_all1", param);


                    this.LoadDataTinhLai();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update hóa đơn tính lãi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadDataTinhLai();
            this.LoadData();
        }

        /// <summary>
        /// TÍCH XUẤT HD GTGT CHO DÒNG ĐANG CHỌN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn hóa đơn.");
                    return;
                }

                string id_arr = "";
                foreach (var i in indexs)
                {
                    try
                    {
                        var id = (long)gvHoaDon.GetRowCellValue(i, "ID");
                        id_arr = id_arr + id + ",";
                        gvHoaDon.SetRowCellValue(i, "IS_DA_XUAT_HD_GTGT", true);
                    }
                    catch { }
                }

                var model = new { id_arr = id_arr, is_chon = true };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);

                Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoadon_tich_hg_gtgt_1", param);
                //LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// TÍCH XUẤT HD GTGT CHO TẤT CẢ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var matn = (byte)itemToaNha.EditValue;

                var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay, is_chon = true };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);

                Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoadon_tich_hd_gtgt_all", param);


                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
        
        /// <summary>
        /// TÍCH HD GTGT - BỎ TÍCH DÒNG CHỌN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn hóa đơn.");
                    return;
                }

                string id_arr = "";
                foreach (var i in indexs)
                {
                    try
                    {
                        var id = (long)gvHoaDon.GetRowCellValue(i, "ID");
                        id_arr = id_arr + id + ",";
                        gvHoaDon.SetRowCellValue(i, "IS_DA_XUAT_HD_GTGT", false);
                    }
                    catch { }
                }

                var model = new { id_arr = id_arr, is_chon = false };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);

                Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoadon_tich_hg_gtgt_1", param);
                //LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        /// <summary>
        /// TÍCH HD GTGT - BỎ TÍCH TẤT CẢ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var matn = (byte)itemToaNha.EditValue;

                var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay, is_chon = false };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);

                Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoadon_tich_hd_gtgt_all", param);


                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        #region
        private void FormatCondition()
        {
            var db = new MasterDataContext();
            var chiSo = db.dvColors.FirstOrDefault(_ => _.TableName.ToLower() == "DVHOADON");
            if (chiSo == null) return;
            if (chiSo.ChiSoMin == null || chiSo.ChiSoEqual == null) return;

            #region Chỉ số mới

            var gridFormatRule2 = new GridFormatRule();
            var ruleIconSet1 = new FormatConditionRuleIconSet();
            var iconSet1 = new FormatConditionIconSet();
            var icon1 = new FormatConditionIconSetIcon();
            var icon2 = new FormatConditionIconSetIcon();
            var icon3 = new FormatConditionIconSetIcon();

            gridFormatRule2.Column = colChenhLech;
            gridFormatRule2.ColumnApplyTo = colPhaiThu;
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

            gvHoaDon.FormatRules.Add(gridFormatRule2);
            #endregion
        }

        void LoadData()
        {
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;
            try
            {

                //gcHoaDon.DataSource = virtualServerModeSource1;

                var wait = new WaitDialogForm("Ðang xử lý. Vui lòng chờ...", "LandSoft Building");
                btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                //System.Collections.Generic.List<dv_hoadon_load_hoa_don> viPham = new System.Collections.Generic.List<dv_hoadon_load_hoa_don>();

                gcHoaDon.DataSource = Library.Class.Connect.QueryConnect.Query<Bill>("dbo.dv_hoadon_load_hoa_don", param);
                btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                wait.Close();

                //TotalDebit = gvHoaDon.RowCount;

                //gcHoaDon.DataSource = linqInstantFeedbackSource1;
            }
            catch
            {

            }

        }

        void LoadDataFirst()
        {
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;
            try
            {

                //gcHoaDon.DataSource = virtualServerModeSource1;

                var wait = new WaitDialogForm("Ðang xử lý. Vui lòng chờ...", "LandSoft Building");
                btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                //System.Collections.Generic.List<dv_hoadon_load_hoa_don> viPham = new System.Collections.Generic.List<dv_hoadon_load_hoa_don>();

                gcHoaDon.DataSource = Library.Class.Connect.QueryConnect.Query<Bill>("dbo.dv_hoadon_load_hoa_don", param);
                btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                wait.Close();

                TotalDebit = gvHoaDon.RowCount;

                //gcHoaDon.DataSource = linqInstantFeedbackSource1;
            }
            catch
            {

            }

        }

        void LoadDataTinhLai()
        {
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            try
            {
                var wait = DialogBox.WaitingForm();
                btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                var model = new { matn = matn, tu_ngay = tuNgay, den_ngay = denNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gcHoaDon.DataSource = Library.Class.Connect.QueryConnect.Query<Bill>("dbo.dv_hoadon_load_hoa_don_tinh_lai", param);

                wait.Close();
                btnNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;


            }
            catch
            {

            }

        }

        void Load_PhieuKhauTru()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                gcKhauTru.DataSource = (from ct in db.ktttChiTiets
                                        join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                        where ct.LinkID == id
                                        select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();
                ;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
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

        void RefreshData()
        {
            LoadDataTinhLai();
            LoadData();
            //linqInstantFeedbackSource1.Refresh();
        }

        void AddRecord()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ tự động", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmAddAuto())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void AddNew()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmEdit())
            {
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void AddMulti()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhiều hóa đơn dịch vụ", "Thêm nhiều", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmAddMulti())
            {
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void Edit()
        {
            if (gvHoaDon.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn hóa đơn, xin cảm ơn.");
                return;
            }

            if ((bool)gvHoaDon.GetFocusedRowCellValue("IsDuyet") == true)
            {
                DialogBox.Error("Hóa đơn đã duyệt, không thể sửa");
                return;
            }



            #region Kiểm tra khóa hóa đơn
            // Cần trả về là có được phép sửa hay return
            // truyền vào form service, từ ngày đến ngày, tòa nhà

            var db = new MasterDataContext();
            var bill = db.dvHoaDons.FirstOrDefault(_ => _.ID == (long)gvHoaDon.GetFocusedRowCellValue("ID"));
            if (bill == null) return;

            var result = DichVu.KhoaSo.Class.ClosingEntry.Closing(bill.MaTN, bill.NgayTT, DichVu.KhoaSo.Class.Enum.BILL);

            if (result.Count() > 0)
            {
                DialogBox.Error("Hóa đơn đã khóa, không thể sửa");
                return;
            }

            #endregion

            #region Kiểm tra khóa khi đã đồng bộ sap

            if(Convert.ToString(bill.SAP_HD) != "" && Convert.ToString(bill.SAP_HD) != null)
            {
                DialogBox.Error("Hóa đơn đã được đồng bộ SAP, không thể sửa");
                return;
            }

            #endregion

            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Sửa hóa đơn dịch vụ", "Sửa", "Dịch vụ: " + gvHoaDon.GetFocusedRowCellValue("TenLDV").ToString() + " của khách hàng:" + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            var f = new frmEdit();
            f.MaTN = (byte)itemToaNha.EditValue;
            f.ID = (long)gvHoaDon.GetFocusedRowCellValue("ID");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        void DeleteRecord()
        {
            
            var indexs = gvHoaDon.GetSelectedRows();
            var count = Convert.ToInt32(TotalDebit);

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            handle = ShowProgressPanel();

            

            //if (indexs.Count() == count)
            //{
            //    // Gọi đến xóa toàn bộ trong cái danh sách hóa đơn cho lẹ
            //    var tuNgay = (DateTime)itemTuNgay.EditValue;
            //    var denNgay = (DateTime)itemDenNgay.EditValue;
            //    var matn = (byte)itemToaNha.EditValue;

            //    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xóa tất cả hóa đơn dịch vụ từ " + tuNgay.ToLongDateString() + " đến "+ denNgay.ToLongDateString(), "Xóa nhiều hóa đơn dịch vụ", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));

            //    Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_delete_all",
            //            new 
            //            {
            //                matn = matn,
            //                tu_ngay= tuNgay,
            //                den_ngay = denNgay,
            //                ServiceId = DichVu.KhoaSo.Class.Enum.BILL,
            //                UserId = Library.Common.User.MaNV
            //            });

            //}
            //else
            //{
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xóa " + indexs.Count().ToString() + " hóa đơn dịch vụ", "Xóa hóa đơn dịch vụ", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            foreach (var i in indexs)
            {
                try
                {
                    var id_ = gvHoaDon.GetRowCellValue(i, "ID");
                    if (id_ == null) continue;

                    bool IsDuyet = Convert.ToBoolean( gvHoaDon.GetRowCellValue(i, "IsDuyet"));
                    if (IsDuyet == true) continue;

                    try
                    {
                        #region delete trên sap trước khi delete hóa đơn
                        var _MaTN = (byte)itemToaNha.EditValue;
                        var _TuNgay = (DateTime)itemTuNgay.EditValue;
                        var _DenNgay = (DateTime)itemDenNgay.EditValue;

                        var model = new { MaTN = _MaTN, TuNgay = _TuNgay, DenNgay = _DenNgay, ID = id_ };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        var obj = Library.Class.Connect.QueryConnect.Query<SAP.Class.ZIAR008List>("sapin_ZIAR008List_1", param);
                        
                        if (obj.Count() > 0)
                        {
                            var item = obj.First();
                            if (Convert.ToString(item.SAP_HD) != "")
                            {
                                // Nếu đã có mã sap_hd, tiến hành gọi xóa hóa đơn
                                item.TYPE = "D";
                                SAP.Funct.SyncHoaDon.DongBo(item);

                            }
                        }
                        #endregion
                    }
                    catch (System.Exception ex) { DialogBox.Error(ex.Message); return; }

                    Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_delete_one",
                        new
                        {
                            Id = (long)gvHoaDon.GetRowCellValue(i, "ID"),
                            ServiceId = DichVu.KhoaSo.Class.Enum.BILL,
                            UserId = Library.Common.User.MaNV
                        });
                }
                catch { }

            }
            //}

            CloseProgressPanel(handle);

            LoadData();
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

        void Payment()
        {
            var indexs = gvHoaDon.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn hóa đơn, xin cảm ơn.");
                return;
            }

            if (gvHoaDon.GetFocusedRowCellValue("MaKH") != null)
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thu tiền khách hàng", "Thu tiền", "Khách hàng:" + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                using (var frm = new frmPayment())
                {
                    frm.MaKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn hóa đơn trước khi thu tiền");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import hóa đơn dịch vụ", "Import", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        void Duyet(bool isDuyet)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            var count = Convert.ToInt32(TotalDebit);

            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Đồng ý thực hiện duyệt/ Không duyệt chứ?") == System.Windows.Forms.DialogResult.No) return;

            handle = ShowProgressPanel();

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            if (isDuyet)
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Duyệt tất cả hóa đơn dịch vụ", "Duyệt", "Từ ngày: " + tuNgay.ToShortDateString() + " đến ngày" + denNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            }
            else
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bỏ duyệt tất cả hóa đơn dịch vụ", "Bỏ Duyệt", "Từ ngày: " + tuNgay.ToShortDateString() + " đến ngày" + denNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            }

            //if (indexs.Count() == count)
            //{
            // Gọi đến xóa toàn bộ trong cái danh sách hóa đơn cho lẹ


            //Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_duyet_all",
            //       new
            //       {
            //           matn = matn,
            //           tu_ngay = tuNgay,
            //           den_ngay = denNgay,
            //           is_chon = isDuyet,
            //           ServiceId = DichVu.KhoaSo.Class.Enum.BILL
            //       });

            //}
            //else
            //{
            foreach (var i in indexs)
                {
                    try
                    {
                        var id_ = gvHoaDon.GetRowCellValue(i, "ID");
                        if (id_ == null) continue;

                        Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_duyet",
                        new
                        {
                            Id = (long)gvHoaDon.GetRowCellValue(i, "ID"),
                            is_chon = isDuyet,
                            ServiceId = DichVu.KhoaSo.Class.Enum.BILL
                        });
                    }
                    catch { }
                //gvHoaDon.SetRowCellValue(i, "IsDuyet", isDuyet);
                }
            //}

            CloseProgressPanel(handle);

            //LoadData();
        }

        void DuyetAll(bool isDuyet)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var _MaTN = (byte)itemToaNha.EditValue;

                var model = new
                {
                    matn = _MaTN,
                    tu_ngay = _TuNgay,
                    den_ngay = _DenNgay,
                    is_chon = isDuyet,
                    ServiceId = DichVu.KhoaSo.Class.Enum.BILL
                };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);

                Library.Class.Connect.QueryConnect.Query<bool>("dbo.dv_hoadon_duyet_all", param);

                if (isDuyet)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Duyệt tất cả hóa đơn dịch vụ", "Duyệt", "Từ ngày: " + _TuNgay.ToShortDateString() + " đến ngày" + _DenNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                }
                else
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bỏ duyệt tất cả hóa đơn dịch vụ", "Bỏ Duyệt", "Từ ngày: " + _TuNgay.ToShortDateString() + " đến ngày" + _DenNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                }
                LoadData();
            }
            catch (Exception ex)
            {
                //DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void Load_PhieuThu()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                        where ct.TableName == "dvHoaDon" & ct.LinkID == id
                                        select new { pt.NgayThu, pt.SoPT, ct.DienGiai, SoTien = pt.IsKhauTru.GetValueOrDefault() == false ? ct.SoTien : (ct.SoTien.GetValueOrDefault() + ct.KhauTru.GetValueOrDefault()) }).ToList();
                ;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void LoadLichSuMienLai()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                gcMienLai.DataSource = Library.Class.Connect.QueryConnect.QueryData<MienLaiModel>("dbo.dv_hoadon_load_lich_su_mien_lai", new
                {
                    LinkId = id
                });
            }
            catch(System.Exception ex) {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void IsMienLaiFunct(bool isMienLai)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            var count = Convert.ToInt32(TotalDebit);

            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Đồng ý miễn lãi/ tính lãi?") == System.Windows.Forms.DialogResult.No) return;

            handle = ShowProgressPanel();

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            if (isMienLai)
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Miễn lãi hóa đơn", "Miễn lãi");
            }
            else
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tính lãi hóa đơn", "Tính lãi");
            }

            foreach (var i in indexs)
            {
                try
                {
                    var id_ = gvHoaDon.GetRowCellValue(i, "ID");
                    if (id_ == null) continue;

                    var soTienLai = gvHoaDon.GetRowCellValue(i, "SoTienLai");

                    Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_mien_lai",
                    new
                    {
                        Id = (long)gvHoaDon.GetRowCellValue(i, "ID"),
                        is_chon = isMienLai,
                        ServiceId = DichVu.KhoaSo.Class.Enum.BILL,
                        UserId = Common.User.MaNV
                    });

                    gvHoaDon.SetRowCellValue(i, "IsMienLai", isMienLai);
                    var SoTienSauMienLai = isMienLai ? 0 : Convert.ToDecimal(soTienLai);
                    gvHoaDon.SetRowCellValue(i, "SoTienLai", SoTienSauMienLai);
                }
                catch { }

            }
            //}

            CloseProgressPanel(handle);

            //LoadData();
        }

        void IsNoQuaHanFunct(bool value)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            var count = Convert.ToInt32(TotalDebit);

            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Ghi nhận/ hủy ghi nhận nợ quá hạn?") == System.Windows.Forms.DialogResult.No) return;

            handle = ShowProgressPanel();

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            if (value)
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Ghi nhận nợ quá hạn", "Nợ quá hạn");
            }
            else
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hủy ghi nhận nợ quá hạn", "Nợ quá hạn");
            }

            foreach (var i in indexs)
            {
                try
                {
                    var id_ = gvHoaDon.GetRowCellValue(i, "ID");
                    if (id_ == null) continue;

                    Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_no_qua_han",
                    new
                    {
                        Id = (long)gvHoaDon.GetRowCellValue(i, "ID"),
                        is_chon = value,
                        ServiceId = DichVu.KhoaSo.Class.Enum.BILL,
                        UserId = Common.User.MaNV
                    });

                    gvHoaDon.SetRowCellValue(i, "IsNoQuaHan", value);
                }
                catch { }

            }
            //}

            CloseProgressPanel(handle);

            //LoadData();
        }

        void IsNoXauFunct(bool value)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            var count = Convert.ToInt32(TotalDebit);

            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Ghi nhận/ hủy ghi nhận nợ xấu?") == System.Windows.Forms.DialogResult.No) return;

            handle = ShowProgressPanel();

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            if (value)
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Ghi nhận nợ xấu", "Nợ xấu");
            }
            else
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hủy ghi nhận nợ xấu", "Nợ xấu");
            }

            foreach (var i in indexs)
            {
                try
                {
                    var id_ = gvHoaDon.GetRowCellValue(i, "ID");
                    if (id_ == null) continue;

                    Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dv_hoadon_no_xau",
                    new
                    {
                        Id = (long)gvHoaDon.GetRowCellValue(i, "ID"),
                        is_chon = value,
                        ServiceId = DichVu.KhoaSo.Class.Enum.BILL,
                        UserId = Common.User.MaNV
                    });

                    gvHoaDon.SetRowCellValue(i, "IsNoXau", value);
                }
                catch { }

            }
            //}

            CloseProgressPanel(handle);

            //LoadData();
        }

        #endregion

        private void itemCapNhatChietKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn hóa đơn");
                    return;
                }

                string ids = "";

                foreach (var i in indexs)
                {
                    ids = ids + ((long)gvHoaDon.GetRowCellValue(i, "ID")).ToString() + ";";
                }

                if (ids.Length > 3000)
                {
                    DialogBox.Error("Chọn quá nhiều dòng, hệ thống không xử lý được.");
                    return;
                }

                using (var frm = new ChietKhau.FrmCapNhatChietKhau())
                {
                    frm.IdHoaDons = ids;
                    frm.matn = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }

            }
            catch { }
        }

        /// <summary>
        /// Duyệt hóa đơn lãi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemCheckedInterest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckInterests(true);
        }

        /// <summary>
        /// Item duyệt tất cả hóa đơn lãi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemCheckedAllInterests_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckAllInterests(true);
        }

        /// <summary>
        /// Không duyệt hóa đơn lãi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemUncheckedInterest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckInterests(false);
        }

        /// <summary>
        /// Không duyệt tất cả hóa đơn lãi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemUncheckedAllInterests_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckAllInterests(false);
        }

        private void CheckInterests(bool check)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                string id_arr = "";
                foreach (var i in rows)
                {
                    try
                    {
                        if ((bool?)gvHoaDon.GetRowCellValue(i, "IsDuyet") == false) continue;
                            if ((bool?)gvHoaDon.GetRowCellValue(i, "IsCheckInterest") == check) continue;

                        var objHD = db.dvHoaDons.FirstOrDefault(p => p.ID == (long)gvHoaDon.GetRowCellValue(i, "ID"));

                        objHD.IsCheckInterest = check;

                        var id = (long)gvHoaDon.GetRowCellValue(i, "ID");
                        id_arr = id_arr + id + ",";
                        gvHoaDon.SetRowCellValue(i, "IsCheckInterest", check);

                        if (check)
                        {
                            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Duyệt hóa đơn lãi", "Duyệt lãi", "Dịch vụ:" + gvHoaDon.GetRowCellValue(i, "TenLDV").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                        }
                        else
                        {
                            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Không duyệt hóa đơn lãi", "Không duyệt lãi", "Dịch vụ:" + gvHoaDon.GetRowCellValue(i, "TenLDV").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                        }
                    }
                    catch(System.Exception ex) { }


                    //db.SubmitChanges();
                }

                Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dvHoaDonCheckInterests", 
                    new
                    {
                        Bills = id_arr,
                        IsCheck = check
                    });

                //db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 5000;
                args.Caption = ex.GetType().FullName;
                args.Text = ex.Message;
                args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                XtraMessageBox.Show(args).ToString();
            }
            finally
            {
                db.Dispose();
            }

            
        }

        private void CheckAllInterests(bool check)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var _MaTN = (byte)itemToaNha.EditValue;


                Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dvHoaDonCheckAllInterests", 
                    new
                    {
                        TowerId = _MaTN,
                        DateFrom = _TuNgay,
                        DateTo = _DenNgay,
                        IsCheck = check
                    });

                if (check)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Duyệt tất cả hóa đơn lãi", "Duyệt lãi ", "Từ ngày: " + _TuNgay.ToShortDateString() + " đến ngày" + _DenNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                }
                else
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bỏ duyệt tất cả hóa đơn lãi", "Bỏ Duyệt lãi", "Từ ngày: " + _TuNgay.ToShortDateString() + " đến ngày" + _DenNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                }
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

        private void itemMienLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemDuocMienLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsMienLaiFunct(true);
        }

        private void itemHuyMienLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsMienLaiFunct(false);
        }

        /// <summary>
        /// Nợ quá hạn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsNoQuaHanFunct(true);
        }

        /// <summary>
        /// Hủy nợ quá hạn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsNoQuaHanFunct(false);
        }

        /// <summary>
        /// Nợ xấu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsNoXauFunct(true);
        }

        /// <summary>
        /// Hủy nợ xấu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsNoXauFunct(false);
        }

        /// <summary>
        /// Cập nhật Company Code cho những hóa đơn không có company code
        /// Cập nhật toàn bộ theo tòa nhà luôn cho khỏe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Library.Class.Connect.QueryConnect.QueryData<bool>("ccCompanyCode_UpdateByTN",
                    new {
                        MaTN = (byte?)itemToaNha.EditValue
                    });
                LoadData();
            }
            catch { }
        }
    }
    public class update_da_thu
    {
        public decimal? DaThu { get; set; }
        public decimal? ConNo { get; set; }
    }

    public class Bill
    {
        public int? MaKH { get; set; }

        public int? NgayCoTheTreHan { get; set; }

        public int? SoNgayTinhLai { get; set; }

        public int? SoNgayTre { get; set; }

        public System.DateTime? NgayTT { get; set; }

        public System.DateTime? NgayNhap { get; set; }

        public System.DateTime? NgaySua { get; set; }

        public System.DateTime? HAN_THANH_TOAN { get; set; }

        public System.DateTime? NGAY_KET_THUC { get; set; }

        public System.DateTime? NgayCuoiCungThanhToan { get; set; }

        public System.DateTime? NgayBatDauTinhLai { get; set; }

        public bool? IsDuyet { get; set; }

        public bool? IS_CON_DANG_CHAY { get; set; }

        public bool? IS_DANG_CHAY { get; set; }

        public bool? IS_DA_XUAT_HD_GTGT { get; set; }


        /// <summary>
        /// Hóa đơn đã duyệt lãi chưa
        /// </summary>
        public bool? IsCheckInterest { get; set; }



        public decimal? PhiDV { get; set; }

        public decimal? KyTT { get; set; }

        public decimal? ThueGTGT { get; set; }

        public decimal? TienThueGTGT { get; set; }

        public decimal? TienTT { get; set; }

        public decimal? TyLeCK { get; set; }

        public decimal? TienCK { get; set; }

        public decimal? PhaiThu { get; set; }

        public decimal? TienTruocThue { get; set; }

        public decimal? DaThu { get; set; }

        public decimal? ConNo { get; set; }

        public decimal? PT_LAI_XUAT { get; set; }

        public decimal? TONG_NGAY { get; set; }

        public decimal? PhanTramLaiXuat { get; set; }

        public decimal? PhaiThuCu { get; set; }

        public decimal? DaThuCu { get; set; }

        public decimal? SoTienBatDauTinhLai { get; set; }

        public decimal? SoTienLai { get; set; }

        public decimal? TienLai1Ngay { get; set; }

        public long ID { get; set; }

        public long? ID_CHA { get; set; }

        public string KyHieu { get; set; }

        public string TenKH { get; set; }

        public string TenLDV { get; set; }

        public string DienGiai { get; set; }

        public string MaSoMB { get; set; }

        public string TenTL { get; set; }

        public string TenKN { get; set; }

        public string TenLMB { get; set; }

        public string NhanVienNhap { get; set; }

        public string GhiChu { get; set; }

        /// <summary>
        /// Loại xe
        /// </summary>
        public string LoaiXe { get; set; }
        public bool? IsMienLai { get; set; }
        public bool? IsNoQuaHan { get; set; }
        public bool? IsNoXau { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public string CompanyCode { get; set; }

        public string SAP_HD { get; set; }
        public string SAP_MSG { get; set; }
    }

    public class MienLaiModel
    {
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public string User { get; set; }
        public bool? IsMienLai { get; set; }
        public decimal? SoTienTruocKhiMienLai { get; set; }
    }
}