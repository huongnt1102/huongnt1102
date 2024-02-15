using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using Dapper;
//using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using Library;

namespace Building.AppVime.CongNo
{
    public partial class FrmBangKeCongNoAll : DevExpress.XtraEditors.XtraForm
    {
        private DataTable _data;
        private bool _anKhachHang;
        private int i;
        List<int> lLoaiDichVu = new List<int>();

        #region Khai báo list

        List<KhachHang> khachHangs = new List<KhachHang>(); //noDauKySq_Ldv
        List<LoaiDichVu> noDauKyHd_Ldv = new List<LoaiDichVu>();
        List<CongNoItem> noDauKyHd = new List<CongNoItem>();
        List<CongNoItem> noDauKySq = new List<CongNoItem>();
        List<LoaiDichVu> noDauKySq_Ldv = new List<LoaiDichVu>();
        List<LoaiDichVu> phatSinh_Ldv = new List<LoaiDichVu>();
        List<LoaiDichVu> phatSinh = new List<LoaiDichVu>();
        List<LoaiDichVu> daThu_Ldv = new List<LoaiDichVu>();
        List<LoaiDichVu> daThu = new List<LoaiDichVu>();
        //List<LoaiDichVu> khauTru = new List<LoaiDichVu>();
        //List<LoaiDichVu> khauTru_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> thuTruoc = new List<LoaiDichVu>();
        //List<LoaiDichVu> thuTruoc_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> thuTruocTrongKy = new List<LoaiDichVu>();
        //List<LoaiDichVu> objThuTruocTrongKy_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> objListLdvDk = new List<LoaiDichVu>();
        List<LoaiDichVu> lPhatSinh_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> lDaThu_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> lKhauTru_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> lThuTruoc_Ldv = new List<LoaiDichVu>();
        //List<LoaiDichVu> objListLdvTttk = new List<LoaiDichVu>();
        List<LoaiDichVu> lDauKy_Ldv_1 = new List<LoaiDichVu>();
        List<LoaiDichVu> lDauKy_Ldv_2 = new List<LoaiDichVu>();

        #endregion

        public FrmBangKeCongNoAll()
        {
            InitializeComponent();
        }

        private void FrmBcCongNo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);
            _anKhachHang = true;
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private DataTable DoCotCoDinh(DataTable data)
        {
            // data
            data.Columns.Add("TenTn");
            data.Columns.Add("MaKH");
            data.Columns.Add("TenKH");
            data.Columns.Add("DienTich", typeof(decimal));

            return data;
        }

        private GridBand CreateBand(string name, string caption)
        {
            var band = new GridBand();
            band.Name = name;
            band.Caption = caption;
            band.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            band.AppearanceHeader.Options.UseFont = true;
            band.Visible = true;
            gv.Bands.Add(band);

            return band;
        }

        private BandedGridColumn CreateColumn(string field, string name, string caption, string fieldName, int index, GridBand band, string formatTemplate, System.Drawing.FontStyle fontStyle, bool isAllowEdit, bool isReadOnly, bool isSum)
        {
            gv.Columns.AddField(field);
            var col = new BandedGridColumn();
            col.Name = name;
            col.Caption = caption;
            col.FieldName = fieldName;

            if (formatTemplate != "")
            {
                col.DisplayFormat.FormatString = formatTemplate;
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            }

            col.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, fontStyle);
            col.AppearanceCell.Options.UseFont = true;
            col.AppearanceCell.Options.UseTextOptions = true;

            col.OptionsColumn.AllowEdit = isAllowEdit;
            col.OptionsColumn.ReadOnly = isReadOnly;

            col.Visible = true;
            col.VisibleIndex = index;
            col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            col.OwnerBand = band;

            if(isSum == true) col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, fieldName, "{0:n0}") });

            return col;
        }

        private void DoCotCoDinh()
        {
            _data = DoCotCoDinh(_data);

            i = 0;
            // band
            var band = CreateBand("", "");
            CreateColumn("Tòa nhà", "colToaNha", @"TÒA NHÀ", "TenTn", i++, band, "", System.Drawing.FontStyle.Regular, false, true, false);
            CreateColumn("Mã khách hàng", "colMaKh", @"MÃ CĂN HỘ/ SẢN PHẨM", "MaKH", i++, band, "", System.Drawing.FontStyle.Regular, true, false, false);
            CreateColumn("Tên khách hàng", "colTenKh", @"CHỦ HỘ", "TenKH", i++, band, "", System.Drawing.FontStyle.Regular, true, false, false);
            CreateColumn("Diện tích", "colDienTich", @"DIỆN TÍCH", "DienTich", i++, band, "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, false, true, false);

            //band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

        private void DoCotTuDong(string bandName, string bandCaption, List<LoaiDichVuTuDong> lDauKy_TenDv, string field, string tenLoai, string dinhDang, System.Drawing.FontStyle fontStyle, string fieldData, string fieldTong, string tenLoaiTong, string captionTong, string dinhDangTong, System.Drawing.FontStyle fontStyleTong, bool isTong)
        {
            // Band tổng
            var bandDauKy = CreateBand(bandName, bandCaption);

            // column bên trong
            if (lDauKy_TenDv != null)
                foreach (var item in lDauKy_TenDv)
                {
                    if (isTong == false)
                    {
                        if (item.SoTien != 0)
                        {
                            _data.Columns.Add(tenLoai + item.MaLDV.ToString(), typeof(decimal));
                            CreateColumn(field, tenLoai + item.MaLDV.ToString(), item.TenLDV, tenLoai + item.MaLDV.ToString(), i++, bandDauKy, dinhDang, fontStyle, false, true, true);

                            if (lLoaiDichVu.Any(_ => _ == item.MaLDV) == false)
                            {
                                lLoaiDichVu.Add((int)item.MaLDV);
                            }
                        }

                    }
                    else
                    {
                        _data.Columns.Add(tenLoai + item.MaLDV.ToString(), typeof(decimal));
                        CreateColumn(field, tenLoai + item.MaLDV.ToString(), item.TenLDV, tenLoai + item.MaLDV.ToString(), i++, bandDauKy, dinhDang, fontStyle, false, true, true);

                        if (lLoaiDichVu.Any(_ => _ == item.MaLDV) == false)
                        {
                            lLoaiDichVu.Add((int)item.MaLDV);
                        }
                    }
                }

            // column tổng
            _data.Columns.Add(fieldData, typeof(decimal));

            CreateColumn(fieldTong, tenLoaiTong, captionTong, tenLoaiTong, i++, bandDauKy, dinhDangTong, fontStyleTong, false, true, true);
        }

        private List<LoaiDichVuTuDong> GetLoaiDichVuTuDongs(List<LoaiDichVu> loaiDichVus, System.Data.Linq.Table<dvLoaiDichVu> dvLoaiDichVus)
        {
            if (loaiDichVus != null & dvLoaiDichVus != null)
            {
                return (from p in loaiDichVus
                        join ldv in dvLoaiDichVus on p.MaLDV equals ldv.ID into loaiDichVu
                        from ldv in loaiDichVu.DefaultIfEmpty()
                        group p by new
                        {
                            p.MaLDV,
                            TenLDV = p.MaLDV == 0 ? "Phí khác" : ldv.TenHienThi
                        } into g
                        select new LoaiDichVuTuDong { MaLDV = g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), TenLDV = g.Key.TenLDV }).ToList();
            }
            return null;
        }

        #region Load dữ liệu
        private List<KhachHang> GetKhachHang(Library.MasterDataContext db, byte? toaNha, System.DateTime denNgay)
        {
            var param = new DynamicParameters();
            param.Add("@TowerId", toaNha, DbType.Byte, null, null);
            param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);

            return Library.Class.Connect.QueryConnect.Query<KhachHang>("dbo.ad_hoadon_Khach_Hang", param).ToList();

        }
        #endregion

        public class LoaiDichVu
        {
            public int? MaKH { get; set; }
            public int? MaLDV { get; set; }

            public int? MaMB { get; set; }
            public decimal? SoTien { get; set; }
        }

        public class CongNoItem
        {
            public int? MaKH { get; set; }
            public decimal? SoTien { get; set; }
            public int? MaMB { get; set; }
        }

        private async void LoadData()
        {
            // đổi gridview thành bandgridview
            gv.Bands.Clear();
            gv.Columns.Clear();

            try
            {
                _data = new DataTable();
                gc.DataSource = _data;

                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    var toaNha = (byte)itemToaNha.EditValue;

                    var db = new MasterDataContext();
                    var tenToaNha = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == toaNha);
                    if (tenToaNha == null) return;

                    #region Dữ liệu
                    await System.Threading.Tasks.Task.Run(() => { khachHangs = GetKhachHang(db, toaNha, denNgay); });


                    #region Nợ đầu kỳ
                    var param1 = new DynamicParameters();
                    param1.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param1.Add("@Nam", denNgay.Year, DbType.Int32, null, null);
                    param1.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);

                    
                    await System.Threading.Tasks.Task.Run(() => { noDauKyHd_Ldv = Library.Class.Connect.QueryConnect.Query<LoaiDichVu>("dbo.ad_hoadon_No_Dau_Ky_Hoa_Don_Ldv", param1).ToList(); });

                    await System.Threading.Tasks.Task.Run(() => { noDauKyHd = Library.Class.Connect.QueryConnect.Query<CongNoItem>("dbo.ad_hoadon_No_Dau_Ky_Hoa_Don", param1).ToList(); });

                    await System.Threading.Tasks.Task.Run(() => { noDauKySq_Ldv = Library.Class.Connect.QueryConnect.Query<LoaiDichVu>("dbo.ad_hoadon_No_Dau_Ky_So_Quy_Ldv", param1).ToList(); });
                    await System.Threading.Tasks.Task.Run(() => { noDauKySq = Library.Class.Connect.QueryConnect.Query<CongNoItem>("dbo.ad_hoadon_No_Dau_Ky_So_Quy", param1).ToList(); });

                    #endregion

                    #region Phát sinh
                    var param = new DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    await System.Threading.Tasks.Task.Run(() => { lPhatSinh_Ldv = Library.Class.Connect.QueryConnect.Query<LoaiDichVu>("dbo.ad_hoadon_Phat_Sinh_Ldv", param).ToList(); });
                    await System.Threading.Tasks.Task.Run(() => { phatSinh = Library.Class.Connect.QueryConnect.Query<LoaiDichVu>("dbo.ad_hoadon_Phat_Sinh", param).ToList(); });

                    #endregion

                    #region Đã thu


                    var lDaThu = (from ct in db.SoQuy_ThuChis
                                  join hd in db.ad_HoaDons on ct.LinkID equals hd.Id into hoaDon
                                  from hd in hoaDon.DefaultIfEmpty()
                                  where //SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                      SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0
                                      & SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0
                                      && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 & //ct.MaKH == 2634 &
                                      ct.MaTN == toaNha & ct.LinkID != null & ct.IsDvApp == true //& ct.IsKhauTru == false 
                                  select new
                                  {
                                      MaKH = ct.MaKH,
                                      SoTien = ct.DaThu.GetValueOrDefault() + ct.KhauTru.GetValueOrDefault() - ct.ThuThua.GetValueOrDefault(),
                                      MaLDV = hd == null ? 0 : hd.MaLDV,
                                      hd.NgayTT
                                  }).ToList();
                    //
                    // Đã thu nợ cũ - Ldv
                    //
                    var daThuNoCu_Ldv = (from ct in lDaThu
                                         where SqlMethods.DateDiffDay(ct.NgayTT, tuNgay) > 0
                                         //&& ct.MaKH == 2634
                                         group ct by new { ct.MaKH, ct.MaLDV }
                        into dt
                                         select new LoaiDichVu
                                         {
                                             MaKH = dt.Key.MaKH,
                                             SoTien = dt.Sum(s => s.SoTien),
                                             MaLDV = dt.Key.MaLDV
                                         }).ToList();
                    // 
                    // đã thu nợ cũ
                    //
                    var daThuNoCu = (from ct in daThuNoCu_Ldv
                                     group ct by ct.MaKH
                        into dt
                                     select new LoaiDichVu
                                     {
                                         MaKH = dt.Key,
                                         SoTien = dt.Sum(s => s.SoTien)
                                     }).ToList();
                    //
                    // Đã thu trong kỳ - Ldv
                    //
                    var daThuTrongKy_Ldv = (from ct in lDaThu
                                            where //SqlMethods.DateDiffDay(ct.NgayTT, tuNgay) == 0
                                                SqlMethods.DateDiffDay(tuNgay, ct.NgayTT) >= 0
                                                & SqlMethods.DateDiffDay(ct.NgayTT, denNgay) >= 0
                                            //&& ct.MaKH == 2634
                                            group ct by new { ct.MaKH, ct.MaLDV }
                        into dt
                                            select new LoaiDichVu
                                            {
                                                MaKH = dt.Key.MaKH,
                                                SoTien = dt.Sum(s => s.SoTien),
                                                MaLDV = dt.Key.MaLDV
                                            }).ToList();
                    //
                    // Đã thu trong kỳ
                    //
                    var daThuTrongKy = (from ct in daThuTrongKy_Ldv
                                        group ct by ct.MaKH
                        into dt
                                        select new LoaiDichVu
                                        {
                                            MaKH = dt.Key,
                                            SoTien = dt.Sum(s => s.SoTien)
                                        }).ToList();

                    await System.Threading.Tasks.Task.Run(() =>
                    {
                        daThu = (from ct in lDaThu
                                 group ct by ct.MaKH
                        into dt
                                 select new LoaiDichVu
                                 {
                                     MaKH = dt.Key,
                                     SoTien = dt.Sum(s => s.SoTien)
                                 }).ToList();
                    });

                    #endregion


                    #region List loại dịch vụ

                    //
                    // Đầu kỳ
                    //
                    await System.Threading.Tasks.Task.Run(() =>
                    {
                        lDauKy_Ldv_1 = (from kh in khachHangs
                                        join ndk in noDauKyHd_Ldv on kh.MaKH equals ndk.MaKH into nodk
                                        from ndk in nodk.DefaultIfEmpty()
                                        select new LoaiDichVu
                                        {
                                            MaKH = kh.MaKH,
                                            SoTien = (ndk == null ? 0 : ndk.SoTien.GetValueOrDefault()),
                                            MaLDV = ndk == null ? 0 : ndk.MaLDV
                                        }).ToList();
                    });
                    await System.Threading.Tasks.Task.Run(() =>
                    {
                        lDauKy_Ldv_2 = (from kh in khachHangs
                                        join sqdk in noDauKySq_Ldv on kh.MaKH equals sqdk.MaKH into soquydk
                                        from sqdk in soquydk.DefaultIfEmpty()
                                        select new LoaiDichVu
                                        {
                                            MaKH = kh.MaKH,
                                            SoTien = -(sqdk == null ? 0 : sqdk.SoTien),
                                            MaLDV = sqdk == null ? 0 : sqdk.MaLDV
                                        }).ToList();
                    });
                    var lDauKy_Ldv = lDauKy_Ldv_1.Concat(lDauKy_Ldv_2).ToList();

                    // Đã thu nợ cũ
                    //
                    var lDaThuNoCu_Ldv = (from kh in khachHangs
                                          join nc in daThuNoCu_Ldv on kh.MaKH equals nc.MaKH into nCu
                                          from nc in nCu.DefaultIfEmpty()
                                          select new LoaiDichVu
                                          {
                                              MaKH = kh.MaKH,
                                              SoTien = nc == null ? 0 : nc.SoTien.GetValueOrDefault(),
                                              MaLDV = nc == null ? 0 : nc.MaLDV
                                          }).ToList();
                    //
                    // Đã thu trong kỳ
                    //
                    var lDaThuTrongKy_Ldv = (from kh in khachHangs
                                             join ps in daThuTrongKy_Ldv on kh.MaKH equals ps.MaKH into psinh
                                             from ps in psinh.DefaultIfEmpty()
                                             select new LoaiDichVu
                                             {
                                                 MaKH = kh.MaKH,
                                                 SoTien = ps == null ? 0 : ps.SoTien.GetValueOrDefault(),
                                                 MaLDV = ps == null ? 0 : ps.MaLDV
                                             }).ToList();

                    #endregion

                    #region list dịch vụ

                    var objList = (from kh in khachHangs
                                       //join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH into matBang from mb in matBang.DefaultIfEmpty()
                                   join ndk in noDauKyHd on kh.MaKH equals ndk.MaKH into nodk
                                   from ndk in nodk.DefaultIfEmpty()
                                   join sqdk in noDauKySq on kh.MaKH equals sqdk.MaKH into soquydk
                                   from sqdk in soquydk.DefaultIfEmpty()
                                   join ps in phatSinh on kh.MaKH equals ps.MaKH into psinh
                                   from ps in psinh.DefaultIfEmpty()
                                   join dt in daThu on kh.MaKH equals dt.MaKH into dthu
                                   from dt in dthu.DefaultIfEmpty()
                                   join dtnc in daThuNoCu on kh.MaKH equals dtnc.MaKH into dtNoCu
                                   from dtnc in dtNoCu.DefaultIfEmpty()
                                   join dttk in daThuTrongKy on kh.MaKH equals dttk.MaKH into dtTrongKy
                                   from dttk in dtTrongKy.DefaultIfEmpty()
                                   select new
                                   {
                                       kh.MaKH,
                                       KyHieu = kh.KyHieu,

                                       TenKH = kh.TenKH,

                                       NoDauKy = (ndk == null ? 0 : ndk.SoTien.GetValueOrDefault()) -
                                                 (sqdk == null ? 0 : sqdk.SoTien.GetValueOrDefault()),
                                       PhatSinh = ps == null ? 0 : ps.SoTien,
                                       DaThu = dt == null ? 0 : dt.SoTien,

                                       DaThuNoCu = dtnc == null ? 0 : dtnc.SoTien,
                                       DaThuTrongKy = dttk == null ? 0 : dttk.SoTien,
                                       DienTich = kh.DienTich
                                   }).Select(p => new
                                   {

                                       NoDauKy = p.NoDauKy,
                                       PhatSinh = p.PhatSinh,

                                       DaThu = p.DaThu,

                                       ConNo = ((p.NoDauKy) + p.PhatSinh - (p.DaThu)),

                                       MaKH = p.MaKH,
                                       KyHieu = p.KyHieu,

                                       TenKH = p.TenKH,

                                       DaThuNoCu = p.DaThuNoCu,
                                       p.DaThuTrongKy,
                                       p.DienTich,
                                       TenTn = tenToaNha.TenTN
                                   }).ToList();

                    #endregion

                    var objLoaiDichVu = db.dvLoaiDichVus;
                    lLoaiDichVu = new List<int>();

                    #endregion

                    DoCotCoDinh();

                    #region Đổ cột tự động

                    #region đầu kỳ
                    var lDauKy_TenDv = GetLoaiDichVuTuDongs(lDauKy_Ldv, objLoaiDichVu);

                    DoCotTuDong("DauKy", "SỐ PHẢI THU KỲ TRƯỚC (1)", lDauKy_TenDv, "", "DauKy", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, "DauKyTong", "", "DauKyTong", "CỘNG (1)", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Bold, false);

                    gc.DataSource = _data;
                    #endregion

                    #region Phát sinh
                    var lPhatSinh_TenDv = GetLoaiDichVuTuDongs(lPhatSinh_Ldv, objLoaiDichVu);

                    DoCotTuDong("PhatSinh", "PHẢI THU TRONG KỲ (2)", lPhatSinh_TenDv, "", "PhatSinh", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, "PhatSinhTong", "", "PhatSinhTong", "CỘNG (2)", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Bold, false);


                    #endregion

                    #region Đã thu nợ cũ

                    var lDaThuNoCu_TenDv = GetLoaiDichVuTuDongs(lDaThuNoCu_Ldv, objLoaiDichVu);

                    DoCotTuDong("DaThuNoCu", "ĐÃ THU NỢ CŨ TRONG KỲ (3A)", lDaThuNoCu_TenDv, "", "DaThuNoCu", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, "DaThuNoCuTong", "", "DaThuNoCuTong", "CỘNG (3A)", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Bold, false);


                    #endregion

                    #region Đã thu trong kỳ
                    var lDaThuTrongKy_TenDv = GetLoaiDichVuTuDongs(lDaThuTrongKy_Ldv, objLoaiDichVu);

                    DoCotTuDong("DaThuTrongKy", "ĐÃ THU PHÁT SINH TRONG KỲ (3B)", lDaThuTrongKy_TenDv, "", "DaThuTrongKy", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, "DaThuTrongKyTong", " ", "DaThuTrongKyTong", "CỘNG (3B)", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Bold, false);

                    #endregion

                    #region Đã thu
                    DoCotTuDong("DaThuTong", "ĐÃ THU (3)=(3A)+(3B)", null, "", "DaThuTong", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, "DaThuTong", " ", "DaThuTong", "TỔNG THU TRONG KỲ (3)", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Bold, false);
                    #endregion


                    #region Còn nợ

                    var lConNo_TenDv = (from p in objLoaiDichVu
                                        where lLoaiDichVu.Contains(p.ID)
                                        select new LoaiDichVuTuDong { TenLDV = p.TenLDV, MaLDV = p.ID }).ToList();
                    DoCotTuDong("ConNo", @"CÒN PHẢI THU (4)=(1)+(2)-(3)", lConNo_TenDv, "", "ConNo", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, "ConNoTong", " ", "ConNoTong", "CỘNG (4)", "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Bold, true);

                    #endregion

                    #endregion

                    #region Đổ dữ liệu



                    foreach (var item in objList)
                    {
                        var r = _data.NewRow();
                        r["TenTn"] = item.TenTn;
                        r["MaKH"] = item.KyHieu;
                        r["TenKH"] = item.TenKH;
                        r["DienTich"] = item.DienTich.GetValueOrDefault();


                        foreach (var it in lDauKy_TenDv)
                        {
                            if (it.SoTien != 0)
                            {
                                decimal soTien = lDauKy_Ldv
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien)
                                    .GetValueOrDefault();
                                r["DauKy" + it.MaLDV.ToString()] = soTien;
                            }

                        }

                        r["DauKyTong"] = item.NoDauKy;

                        foreach (var it in lPhatSinh_TenDv)
                        {
                            if (it.SoTien != 0)
                            {
                                //string.Format("{0:#,0.##; (0.##);-}", soTien);
                                decimal soTien = lPhatSinh_Ldv
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien)
                                    .GetValueOrDefault();
                                r["PhatSinh" + it.MaLDV] = soTien;
                            }
                        }

                        r["PhatSinhTong"] = item.PhatSinh;

                        //
                        // Đã thu nợ cũ
                        //
                        foreach (var it in lDaThuNoCu_TenDv)
                        {
                            if (it.SoTien != 0)
                            {
                                r["DaThuNoCu" + it.MaLDV] = lDaThuNoCu_Ldv
                                    .Where(_ => _.MaLDV == it.MaLDV & _.MaKH == item.MaKH).Sum(_ => _.SoTien)
                                    .GetValueOrDefault();
                            }
                        }

                        r["DaThuNoCuTong"] = item.DaThuNoCu;

                        foreach (var it in lDaThuTrongKy_TenDv)
                        {
                            if (it.SoTien != 0)
                            {
                                r["DaThuTrongKy" + it.MaLDV] = lDaThuTrongKy_Ldv
                                    .Where(_ => _.MaLDV == it.MaLDV & _.MaKH == item.MaKH).Sum(_ => _.SoTien)
                                    .GetValueOrDefault();
                            }
                        }

                        r["DaThuTrongKyTong"] = item.DaThuTrongKy;


                        r["DaThuTong"] = item.DaThu;


                        foreach (var it in lConNo_TenDv)
                        {
                            var dk = lDauKy_Ldv
                                .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien)
                                .GetValueOrDefault();
                            var ps = lPhatSinh_Ldv
                                .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien)
                                .GetValueOrDefault();
                            var dt1 = lDaThuNoCu_Ldv.Where(_ => _.MaLDV == it.MaLDV & _.MaKH == item.MaKH)
                                .Sum(_ => _.SoTien).GetValueOrDefault();
                            var dt2 = lDaThuTrongKy_Ldv.Where(_ => _.MaLDV == it.MaLDV & _.MaKH == item.MaKH)
                                .Sum(_ => _.SoTien).GetValueOrDefault();

                            r["ConNo" + it.MaLDV] = ((dk) + ps - (dt1 + dt2));
                        }

                        r["ConNoTong"] = item.ConNo;

                        _data.Rows.Add(r);
                    }

                    #endregion
                }

                gc.DataSource = _data;
                gv.BestFitColumns();
            }
            catch
            {
            }
        }

        public class LoaiDichVuTuDong
        {
            public int? MaLDV { get; set; }
            public decimal? SoTien { get; set; }
            public string TenLDV { get; set; }
        }

        public class KhachHang
        {
            public int MaKH { get; set; }
            public string TenKH { get; set; }
            public string KyHieu { get; set; }
            public decimal? DienTich { get; set; }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

    }
}