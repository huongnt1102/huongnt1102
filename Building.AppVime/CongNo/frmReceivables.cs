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
    public partial class frmReceivables : DevExpress.XtraEditors.XtraForm
    {
        public frmReceivables()
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

        public class HoaDonChiTiet
        {
            public bool? IsDuyet { get; set; }
            public DateTime? NgayTT { get; set; }
            public string TenLDV { get; set; }
            public decimal? PhiDV { get; set; }
            public string DienGiai { get; set; }
            public decimal? TienTT { get; set; }
            public decimal? PhaiThu { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? ConNo { get; set; }
            public string MaSoMB { get; set; }
            public int? MaMB { get; set; }
            public string TenTL { get; set; }
            public string TenKN { get; set; }
            public string TenLMB { get; set; }
        }

        void Detail()
        {
            var db = new MasterDataContext();
            try
            {
                var maKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                if (maKH == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        var model = new { makh = maKH, denngay = (DateTime)itemDenNgay.EditValue };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        gcChiTiet.DataSource = Library.Class.Connect.QueryConnect.Query<HoaDonChiTiet>("dbo.ad_HoaDon_get_cong_no_ct", param);

                        break;
                    //case 1:
                    //    //ctlMailHistory1.MaKH = maKH;
                    //    //ctlMailHistory1.MailHistory_Load();
                    //    break;
                    case 1:
                        var model1 = new { makh = maKH, denngay = (DateTime)itemDenNgay.EditValue, tungay = (DateTime)itemTuNgay.EditValue };
                        var param1 = new Dapper.DynamicParameters();
                        param1.AddDynamicParams(model1);
                        gcPhieuThu.DataSource = Library.Class.Connect.QueryConnect.Query<PhieuThuByKhModel>("dbo.ad_hoadon_get_phieu_thu_list", param1);

                        break;
                    
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        public class PhieuThuByKhModel
        {
            public System.DateTime? NgayThu { get; set; }

            public System.DateTime? NgayNhap { get; set; }

            public System.DateTime? NgaySua { get; set; }

            public decimal? SoTienThu { get; set; }

            public decimal? TienPhaiThu { get; set; }

            public decimal? TienKhauTru { get; set; }

            public decimal? TienThuThua { get; set; }

            public string SoPT { get; set; }

            public string KyHieu { get; set; }

            public string TenKH { get; set; }

            public string NguoiThu { get; set; }

            public string NguoiNop { get; set; }

            public string DiaChiNN { get; set; }

            public string LyDo { get; set; }

            public string TenPL { get; set; }

            public string ChungTuGoc { get; set; }

            public string NguoiNhap { get; set; }

            public string PhuongThuc { get; set; }

            public string SoTK { get; set; }

            public string TenNH { get; set; }

            public string NguonThu { get; set; }

        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            
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

        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        public class ad_hoadon_cong_no
        {
            public string KyHieu { get; set; }
            public string MaPhu { get; set; }
            public string TenKH { get; set; }
            public string MaSoMB { get; set; }
            public string DiaChi { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public decimal? PhaiThu { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? ConNo { get; set; }
            public int? MaKH { get; set; }
            public int? MaMB { get; set; }

            public byte? MaTN { get; set; }
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            try
            {
                var model = new { matn = matn, tungay = tuNgay, denngay = denNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var kq = Library.Class.Connect.QueryConnect.Query<ad_hoadon_cong_no>("dbo.ad_HoaDon_get_cong_no", param);
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


        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
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



        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcHoaDon);
        }

        private void gvHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Detail();
        }

        private void gvHoaDon_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            //Detail();
        }

        private void itemCapNhatConNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn hóa đơn");
                    return;
                }

                foreach (var i in indexs)
                {
                    var phieuThu = (from ct in db.ptChiTietPhieuThus
                                    join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                    where ct.TableName == "dvHoaDon" & ct.LinkID == (long)gvHoaDon.GetRowCellValue(i, "ID")
                                    select new { pt.NgayThu, pt.SoPT, ct.DienGiai, SoTien = pt.IsKhauTru == false ? ct.SoTien : (ct.SoTien.GetValueOrDefault() + ct.KhauTru.GetValueOrDefault()) }).ToList();
                    var hoaDon = db.dvHoaDons.FirstOrDefault(_ => _.ID == (long)gvHoaDon.GetRowCellValue(i, "ID"));
                    if (hoaDon != null)
                    {
                        hoaDon.DaThu = phieuThu.Sum(_ => _.SoTien);
                        hoaDon.ConNo = hoaDon.PhaiThu - hoaDon.DaThu;
                        db.SubmitChanges();
                    }
                }
                

            }
            catch { }
            finally
            {
                db.Dispose();
            }

        }

        private void itemThuTien_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmPayment())
            {
                
                frm.MaKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Detail();
        }
    }
}