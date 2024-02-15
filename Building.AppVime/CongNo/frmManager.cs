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

namespace Building.AppVime.CongNo
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
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

        void RefreshData()
        {
            LoadData();
        }

        void Load_PhieuThu()
        {
            var db = new MasterDataContext();
            try
            { 
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                var model = new { linkid = id, isdvapp = true };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gcChiTiet.DataSource = Library.Class.Connect.QueryConnect.Query<ChiTietPhieuThu>("dbo.soquy_thuchi_get_pt", param);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        public class ChiTietPhieuThu
        {
            public DateTime? NgayThu { get; set; }
            public string SoPT { get; set; }
            public string DienGiai { get; set; }
            public decimal? SoTien { get; set; }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            gvChiTiet.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            try
            {
                foreach (var i in rows)
                {
                    if ((bool?)gvHoaDon.GetRowCellValue(i, "IsDuyet") == true) continue;
                    if ((decimal?)gvHoaDon.GetRowCellValue(i, "DaThu") > 0) continue;

                    var model = new { id = (long)gvHoaDon.GetRowCellValue(i, "Id") };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);
                    var result = Library.Class.Connect.QueryConnect.Query<bool>("dbo.ad_hoadon_xoa_1", param);
                    LoadData();
                }

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        public class ad_hoadon_get_list
        {
            public long? Id { get; set; }
            public DateTime? NgayTT { get; set; }
            public int? MaKH { get; set; }
            public string KyHieu { get; set; }
            public string MaPhu { get; set; }
            public string TenKH { get; set; }
            public string TenLDV { get; set; }
            public string DienGiai { get; set; }
            public decimal? PhiDV { get; set; }
            public decimal? TienTT { get; set; }
            public decimal? PhaiThu { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? ConNo { get; set; }
            public DateTime? NgayNhap { get; set; }
            public DateTime? NgaySua { get; set; }
            public bool? IsDuyet { get; set; }
            public string MaSoMB { get; set; }
            public string TenTL { get; set; }
            public string TenKN { get; set; }
            public string TenLMB { get; set; }
            public string Loai { get; set; }
            public string NguoiNhap { get; set; }
            public string NguoiSua { get; set; }
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            try
            {
                var model = new { matn = matn, tungay = tuNgay, denngay = denNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var kq = Library.Class.Connect.QueryConnect.Query<ad_hoadon_get_list>("dbo.ad_HoaDon_get_list", param);
                gcHoaDon.DataSource = kq;

            }
            catch
            {

            }
            
        }
        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            var obj = from hd in db.dvHoaDons
                      join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                      join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                      join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                      from mb in tblMatBang.DefaultIfEmpty()
                      join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                      from tl in tblTangLau.DefaultIfEmpty()
                      join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                      from kn in tblKhoiNha.DefaultIfEmpty()
                      join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                      from lmb in tblLoaiMatBang.DefaultIfEmpty()
                      where hd.MaTN == matn & SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0 & hd.MaKH == 5773
                      orderby kn.TenKN, tl.TenTL, mb.MaMB ascending //, hd.NgayTT descending
                      select new
                      {
                          hd.ID,
                          hd.NgayTT,
                          hd.MaKH,
                          kh.KyHieu,
                          kh.MaPhu,
                          TenKH = kh.IsCaNhan == true ? (kh.TenKH) : kh.CtyTen,
                          TenLDV = l.TenHienThi,
                          hd.DienGiai,
                          hd.PhiDV,
                          hd.KyTT,
                          hd.TienTT,
                          hd.TyLeCK,
                          hd.TienCK,
                          hd.PhaiThu,
                          hd.DaThu,
                          hd.ConNo,
                          hd.IsDuyet,
                          mb.MaSoMB,
                          tl.TenTL,
                          kn.TenKN,
                          lmb.TenLMB
                      };
            e.QueryableSource = obj;
            e.Tag = db;
        }

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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

        }

        void Duyet(bool isDuyet)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            try
            {
                foreach (var i in rows)
                {
                    if ((bool?)gvHoaDon.GetRowCellValue(i, "IsDuyet") == isDuyet) continue;

                    var model = new { id = (long)gvHoaDon.GetRowCellValue(i, "Id"), isduyet = isDuyet };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);
                    var result = Library.Class.Connect.QueryConnect.Query<bool>("dbo.ad_hoadon_duyet_1", param);
                    LoadData();
                }

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void DuyetAll(bool isDuyet)
        {
            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var matn = (byte)itemToaNha.EditValue;

                var model = new { matn = matn, tungay = tuNgay, denngay = denNgay, isduyet = isDuyet };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var result = Library.Class.Connect.QueryConnect.Query<bool>("dbo.ad_HoaDon_duyet_all", param);
                LoadData();

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(true);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(false);
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuyetAll(true);
        }

        private void itemKhongDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuyetAll(false);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcHoaDon);
        }

        private void itemAddMulti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gvHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Load_PhieuThu();
            Load_PhieuKhauTru();
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
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        private void gvHoaDon_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Load_PhieuThu();
            Load_PhieuKhauTru();
        }

        private void itemCapNhatConNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var db = new MasterDataContext();
            //try
            //{
            //    var indexs = gvHoaDon.GetSelectedRows();

            //    if (indexs.Length == 0)
            //    {
            //        DialogBox.Alert("Vui lòng chọn hóa đơn");
            //        return;
            //    }

            //    foreach (var i in indexs)
            //    {
            //        var phieuThu = (from ct in db.ptChiTietPhieuThus
            //                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
            //                        where ct.TableName == "dvHoaDon" & ct.LinkID == (long)gvHoaDon.GetRowCellValue(i, "ID")
            //                        select new { pt.NgayThu, pt.SoPT, ct.DienGiai, SoTien = pt.IsKhauTru == false ? ct.SoTien : (ct.SoTien.GetValueOrDefault() + ct.KhauTru.GetValueOrDefault()) }).ToList();
            //        var hoaDon = db.dvHoaDons.FirstOrDefault(_ => _.ID == (long)gvHoaDon.GetRowCellValue(i, "ID"));
            //        if (hoaDon != null)
            //        {
            //            hoaDon.DaThu = phieuThu.Sum(_ => _.SoTien);
            //            hoaDon.ConNo = hoaDon.PhaiThu - hoaDon.DaThu;
            //            db.SubmitChanges();
            //        }
            //    }
                

            //}
            //catch { }
            //finally
            //{
            //    db.Dispose();
            //}

        }
    }
}