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

namespace LandSoftBuilding.Receivables.Reports
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
        List<LoaiDichVu> noDauKyHd = new List<LoaiDichVu>();
        List<LoaiDichVu> noDauKySq = new List<LoaiDichVu>();
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
            data.Columns.Add("TenKN");
            data.Columns.Add("TenLMB");
            data.Columns.Add("MaSoMB");
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
            if(name == "colMaKh") col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Tổng cộng", null,"Tổng cộng") });
            return col;
        }

        private void DoCotCoDinh()
        {
            _data = DoCotCoDinh(_data);

            i = 0;
            // band
            var band = CreateBand("", "");
            CreateColumn("Tòa nhà", "colToaNha", @"TÒA NHÀ", "TenTn", i++, band, "", System.Drawing.FontStyle.Regular, false, true, false);
            CreateColumn("Khối nhà", "colKhoiNha", @"KHỐI NHÀ", "TenKN", i++, band, "", System.Drawing.FontStyle.Regular, false, true, false);
            CreateColumn("Loại mặt bằng", "colLoaiMatBang", @"LOẠI MẶT BẰNG", "TenLMB", i++, band, "", System.Drawing.FontStyle.Regular, false, true, false);
            CreateColumn("Mã mặt bằng", "colMaMb", @"Mã mặt bằng", "MaSoMB", i++, band, "", System.Drawing.FontStyle.Regular, true, false, false);
            CreateColumn("Mã khách hàng", "colMaKh", @"Mã khách hàng", "MaKH", i++, band, "", System.Drawing.FontStyle.Regular, true, false, false);
            CreateColumn("Tên khách hàng", "colTenKh", @"CHỦ HỘ", "TenKH", i++, band, "", System.Drawing.FontStyle.Regular, true, false, false);
            CreateColumn("Diện tích", "colDienTich", @"DIỆN TÍCH", "DienTich", i++, band, "{0:#,0.##; (0.##);-}", System.Drawing.FontStyle.Regular, false, true, true);

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

            if (_anKhachHang == true)
            {
                return Library.Class.Connect.QueryConnect.Query<KhachHang>("dbo.bc_cna_Khach_Hang_An", param).ToList();
            }
            else
            {
                return Library.Class.Connect.QueryConnect.Query<KhachHang>("dbo.bc_cna_Khach_Hang", param).ToList();
            }
        }
        #endregion

        private void LoadData()
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
                    khachHangs = GetKhachHang(db, toaNha, denNgay).ToList() ;
                    

                    #region Nợ đầu kỳ
                    var noDauKyHd1 = (from hd in db.dvHoaDons
                                      where SqlMethods.DateDiffDay(hd.NgayTT, tuNgay) > 0 & hd.IsDuyet == true & hd.MaTN == toaNha //& hd.MaKH == 2634
                                      select new { hd.ID, hd.MaKH, hd.PhaiThu, hd.MaLDV, hd.MaMB }).ToList();

                    

                        noDauKyHd_Ldv = (from hd in noDauKyHd1
                                         group hd by new { hd.MaKH, hd.MaLDV, hd.MaMB }
                        into ndk
                                         select new LoaiDichVu
                                         { MaKH = ndk.Key.MaKH.Value, MaMB = ndk.Key.MaMB.Value, SoTien = ndk.Sum(_ => _.PhaiThu), MaLDV = ndk.Key.MaLDV }).ToList();
                    

                     noDauKyHd = (from hd in noDauKyHd_Ldv
                                     group hd by new { hd.MaKH, hd.MaMB }
                        into ndk
                                     select new LoaiDichVu
                                     {
                                         MaKH = ndk.Key.MaKH,
                                         MaMB = ndk.Key.MaMB,
                                         SoTien = ndk.Sum(s => s.SoTien)
                                     }).ToList();
                     var noDauKySq1 = (from sq in db.SoQuy_ThuChis
                                       join hd in db.dvHoaDons on new { sq.TableName, sq.LinkID } equals new
                                       { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                       from hd in hoaDon.DefaultIfEmpty()
                                       where SqlMethods.DateDiffDay(sq.NgayPhieu, tuNgay) > 0 & sq.MaTN == toaNha &
                                                sq.IsPhieuThu == true & sq.MaLoaiPhieu != 24 & sq.LinkID != null &
                                                sq.TableName == "dvHoaDon" //& sq.MaKH == 2634
                                          select new
                                          {
                                              sq.MaKH,
                                              sq.LinkID,
                                              hd.MaMB,
                                              SoTien = sq.DaThu.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault() - sq.ThuThua.GetValueOrDefault()
                                          }).ToList();

                        noDauKySq_Ldv = (from sq in noDauKySq1
                                         join hd in noDauKyHd1 on sq.LinkID equals hd.ID
                                         group new { sq, hd } by new { sq.MaKH, hd.MaLDV, sq.MaMB }
                            into ndk
                                         select new LoaiDichVu
                                         { MaKH = ndk.Key.MaKH.Value, MaMB = ndk.Key.MaMB.Value, SoTien = ndk.Sum(_ => _.sq.SoTien), MaLDV = ndk.Key.MaLDV })
                        .ToList();
                    
                        noDauKySq = (from sq in noDauKySq_Ldv
                                     group new { sq } by new { sq.MaKH, sq.MaMB }
                            into ndk
                                     select new LoaiDichVu
                                     { MaKH = ndk.Key.MaKH, MaMB = ndk.Key.MaMB, SoTien = ndk.Sum(_ => _.sq.SoTien), MaLDV = 0 })
                        .ToList();
                    

                    #endregion

                    #region Phát sinh
                    
                        phatSinh_Ldv = (from hd in db.dvHoaDons
                                        where //SqlMethods.DateDiffMonth(hd.NgayTT, tuNgay) == 0 
                                            SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0
                                            & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                                            & hd.IsDuyet == true && hd.MaTN == toaNha
                                        //& hd.MaKH == 2634
                                        group hd by new { hd.MaKH, hd.MaLDV, hd.MaMB }
                        into ps
                                        select new LoaiDichVu
                                        {
                                            MaKH = ps.Key.MaKH.Value,
                                            MaMB = ps.Key.MaMB.Value,
                                            SoTien = ps.Sum(s => s.PhaiThu).GetValueOrDefault(),
                                            MaLDV = ps.Key.MaLDV
                                        }).ToList();
                    
                        phatSinh = (from hd in phatSinh_Ldv

                                    group hd by new { hd.MaKH, hd.MaMB }
                        into ps
                                    select new LoaiDichVu
                                    {
                                        MaKH = ps.Key.MaKH,
                                        MaMB = ps.Key.MaMB,
                                        SoTien = ps.Sum(s => s.SoTien),
                                    }).ToList();
                    

                    #endregion

                    #region Đã thu

                    //
                    // List đã thu
                    //

                    var lDaThu = (from ct in db.SoQuy_ThuChis
                                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new
                                  { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                  from hd in hoaDon.DefaultIfEmpty()
                                  where //SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                      SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0
                                      & SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0
                                      && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 /*& ct.MaKH == 3806 &*/
                                      & ct.MaTN == toaNha & ct.LinkID != null & ct.IsDvApp == false //& ct.IsKhauTru == false 
                                  select new
                                  {
                                      MaKH = ct.MaKH,
                                      MaMB = hd == null ? 0 : hd.MaMB,
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
                                         group ct by new { ct.MaKH, ct.MaLDV, ct.MaMB }
                        into dt
                                         select new LoaiDichVu
                                         {
                                             MaKH = dt.Key.MaKH.Value,
                                             MaMB = dt.Key.MaMB.Value,
                                             SoTien = dt.Sum(s => s.SoTien),
                                             MaLDV = dt.Key.MaLDV
                                         }).ToList();
                    // 
                    // đã thu nợ cũ
                    //
                    var daThuNoCu = (from ct in daThuNoCu_Ldv
                                     group ct by new { ct.MaKH, ct.MaMB }
                        into dt
                                     select new LoaiDichVu
                                     {
                                         MaKH = dt.Key.MaKH,
                                         MaMB = dt.Key.MaMB,
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
                                            group ct by new { ct.MaKH, ct.MaLDV, ct.MaMB }
                        into dt
                                            select new LoaiDichVu
                                            {
                                                MaMB = dt.Key.MaMB.Value,
                                                MaKH = dt.Key.MaKH.Value,
                                                SoTien = dt.Sum(s => s.SoTien),
                                                MaLDV = dt.Key.MaLDV
                                            }).ToList();
                    //
                    // Đã thu trong kỳ
                    //
                    var daThuTrongKy = (from ct in daThuTrongKy_Ldv
                                        group ct by new { ct.MaKH, ct.MaMB }
                        into dt
                                        select new LoaiDichVu
                                        {
                                            MaMB = dt.Key.MaMB,
                                            MaKH = dt.Key.MaKH,
                                            SoTien = dt.Sum(s => s.SoTien)
                                        }).ToList();
                    //
                    // Đã thu - Ldv
                    //
                    //daThu_Ldv = (from ct in lDaThu
                    //    group ct by new { ct.MaKH, ct.MaLDV }
                    //    into dt
                    //    select new LoaiDichVu
                    //    {
                    //        MaKH = dt.Key.MaKH,
                    //        SoTien = dt.Sum(s => s.SoTien),
                    //        MaLDV = dt.Key.MaLDV
                    //    }).ToList();
                    
                        daThu = (from ct in lDaThu
                                 group ct by new {ct.MaKH, ct.MaMB}
                        into dt
                                 select new LoaiDichVu
                                 {
                                     MaMB = dt.Key.MaMB.Value,
                                     MaKH = dt.Key.MaKH.Value,
                                     SoTien = dt.Sum(s => s.SoTien)
                                 }).ToList();
                   

                    #endregion

                    #region Khấu trừ

                    //khauTru_Ldv = (from ct in db.SoQuy_ThuChis
                    //               join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                    //               from hd in hoaDon.DefaultIfEmpty()
                    //               where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                    //                     && ct.MaTN == toaNha && ct.IsPhieuThu == true & ct.IsKhauTru == true & ct.LinkID != null & ct.TableName == "dvHoaDon"
                    //               group ct by new { ct.MaKH, MaLDV = hd == null ? 0 : hd.MaLDV }
                    //                   into kt
                    //                   select new LoaiDichVu
                    //                   {
                    //                       MaKH = kt.Key.MaKH,
                    //                       SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault(),
                    //                       MaLDV = kt.Key.MaLDV
                    //                   }).ToList();
                    //khauTru = (from ct in khauTru_Ldv
                    //           group ct by ct.MaKH
                    //               into kt
                    //               select new LoaiDichVu
                    //               {
                    //                   MaKH = kt.Key,
                    //                   SoTien = kt.Sum(s => s.SoTien)
                    //               }).ToList();
                    //var khauTru = (from ct in db.SoQuy_ThuChis
                    //    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                    //    from hd in hoaDon.DefaultIfEmpty()
                    //    where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                    //          && ct.MaTN == toaNha && ct.IsPhieuThu == true & ct.IsKhauTru == true & ct.LinkID != null & ct.TableName == "dvHoaDon"
                    //    group ct by new { ct.MaKH}
                    //    into kt
                    //    select new LoaiDichVu
                    //    {
                    //        MaKH = kt.Key.MaKH,
                    //        SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault()
                    //    }).ToList();

                    #endregion

                    #region Thu Trước

                    //thuTruoc_Ldv = (from sq in db.SoQuy_ThuChis
                    //    join hd in db.dvHoaDons on new { sq.TableName, sq.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                    //    from hd in hoaDon.DefaultIfEmpty()
                    //    where SqlMethods.DateDiffDay(sq.NgayPhieu, DateTime.Now) >= 0 && sq.MaTN == toaNha
                    //                                                                  && sq.IsPhieuThu == true &&
                    //                                                                  sq.MaLoaiPhieu !=
                    //                                                                  24 
                    //    group sq by new { sq.MaKH, MaLDV = hd == null ? 0 : hd.MaLDV }
                    //    into tt
                    //    select new LoaiDichVu
                    //    {
                    //        MaKH = tt.Key.MaKH,
                    //        SoTien = tt.Sum(s => s.ThuThua - s.KhauTru).GetValueOrDefault(),
                    //        MaLDV = tt.Key.MaLDV
                    //    }).ToList();
                    //thuTruoc = (from sq in thuTruoc_Ldv

                    //    group sq by sq.MaKH
                    //    into tt
                    //    select new LoaiDichVu
                    //    {
                    //        MaKH = tt.Key,
                    //        SoTien = tt.Sum(s => s.SoTien)
                    //    }).ToList();

                    //var thuTruocTrongKy = (from ct in db.SoQuy_ThuChis
                    //                   join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                    //                   from hd in hoaDon.DefaultIfEmpty()
                    //                   where //SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                    //                       SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0
                    //                       & SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0
                    //                       && ct.MaTN == toaNha && ct.IsPhieuThu == true & ct.IsKhauTru == true & ct.LinkID != null
                    //                   group ct by new { ct.MaKH}
                    //                       into kt
                    //                       select new LoaiDichVu
                    //                       {
                    //                           MaKH = kt.Key.MaKH,
                    //                           SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault()
                    //                       }).ToList();

                    #endregion

                    #region List loại dịch vụ

                    //
                    // Đầu kỳ
                    //
                    
                        lDauKy_Ldv_1 = (from kh in khachHangs
                                        join ndk in noDauKyHd_Ldv on kh.MaKH equals ndk.MaKH into nodk
                                        from ndk in nodk.DefaultIfEmpty()
                                        select new LoaiDichVu
                                        {
                                            MaKH = kh.MaKH,
                                            MaMB = ndk == null ? 0 : ndk.MaMB,
                                            SoTien = (ndk == null ? 0 : ndk.SoTien.GetValueOrDefault()),
                                            MaLDV = ndk == null ? 0 : ndk.MaLDV
                                        }).ToList();
                    
                        lDauKy_Ldv_2 = (from kh in khachHangs
                                        join sqdk in noDauKySq_Ldv on kh.MaKH equals sqdk.MaKH into soquydk
                                        from sqdk in soquydk.DefaultIfEmpty()
                                        select new LoaiDichVu
                                        {
                                            MaKH = kh.MaKH,
                                            MaMB = sqdk == null ? 0 : sqdk.MaMB,
                                            SoTien = -(sqdk == null ? 0 : sqdk.SoTien),
                                            MaLDV = sqdk == null ? 0 : sqdk.MaLDV
                                        }).ToList();
                    
                    var lDauKy_Ldv = lDauKy_Ldv_1.Concat(lDauKy_Ldv_2).ToList();
                    //
                    
                        lPhatSinh_Ldv = (from kh in khachHangs
                                         join ps in phatSinh_Ldv on kh.MaKH equals ps.MaKH into psinh
                                         from ps in psinh.DefaultIfEmpty()
                                         select new LoaiDichVu
                                         {
                                             MaKH = kh.MaKH,
                                             MaMB = ps == null ? 0 : ps.MaMB,
                                             SoTien = ps == null ? 0 : ps.SoTien.GetValueOrDefault(),
                                             MaLDV = ps == null ? 0 : ps.MaLDV
                                         }).ToList();
                    
                    //
                    // Đã thu nợ cũ
                    //
                    var lDaThuNoCu_Ldv = (from kh in khachHangs
                                          join nc in daThuNoCu_Ldv on kh.MaKH equals nc.MaKH into nCu
                                          from nc in nCu.DefaultIfEmpty()
                                          select new LoaiDichVu
                                          {
                                              MaKH = kh.MaKH,
                                              MaMB = nc == null ? 0 : nc.MaMB,
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
                                                 MaMB = ps == null ? 0 : ps.MaMB,
                                                 SoTien = ps == null ? 0 : ps.SoTien.GetValueOrDefault(),
                                                 MaLDV = ps == null ? 0 : ps.MaLDV
                                             }).ToList();
                    //
                    // Khấu trừ
                    //
                    //lKhauTru_Ldv = (from p in khachHangs
                    //        join k in khauTru_Ldv on p.MaKH equals k.MaKH into kt
                    //        from k in kt.DefaultIfEmpty()
                    //        select new LoaiDichVu
                    //        {
                    //            MaKH = p.MaKH, SoTien = k == null ? 0 : k.SoTien.GetValueOrDefault(),
                    //            MaLDV = k == null ? 0 : k.MaLDV
                    //        }).ToList();
                    //
                    // Thu trước
                    //
                    //lThuTruoc_Ldv = (from p in khachHangs
                    //                join k in thuTruoc_Ldv on p.MaKH equals k.MaKH into tt
                    //                from k in tt.DefaultIfEmpty()
                    //                select new LoaiDichVu
                    //                {
                    //                    MaKH= p.MaKH,
                    //                    SoTien = k == null ? 0 : k.SoTien.GetValueOrDefault(),
                    //                    MaLDV = k == null ? 0 : k.MaLDV
                    //                }).ToList();
                    // Còn nợ

                    #endregion

                    #region list dịch vụ

                    var objList = (from kh in khachHangs
                                   join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH into matbang
                                   from mb in matbang.DefaultIfEmpty()
                                   join lmb in db.mbLoaiMatBangs on (mb != null ? mb.MaLMB : 0) equals lmb.MaLMB into loaiMatBang
                                   from lmb in loaiMatBang.DefaultIfEmpty()
                                   join tl in db.mbTangLaus on (mb != null ? mb.MaTL : 0) equals tl.MaTL into tangLau
                                   from tl in tangLau.DefaultIfEmpty()
                                   join kn in db.mbKhoiNhas on (tl != null ? tl.MaKN : 0) equals kn.MaKN into khoiNha
                                   from kn in khoiNha.DefaultIfEmpty()
                                   join ndk in noDauKyHd on kh.MaKH equals ndk.MaKH into nodk
                                   from ndk in nodk.Where(x => (mb != null ? x.MaMB == mb.MaMB : x.MaKH == kh.MaKH)).DefaultIfEmpty()
                                   join sqdk in noDauKySq on kh.MaKH equals sqdk.MaKH into soquydk
                                   from sqdk in soquydk.Where(x =>(mb != null ? x.MaMB == mb.MaMB : x.MaKH == kh.MaKH)).DefaultIfEmpty()
                                   join ps in phatSinh on kh.MaKH equals ps.MaKH into psinh
                                   from ps in psinh.Where(x => (mb != null ? x.MaMB == mb.MaMB : x.MaKH == kh.MaKH)).DefaultIfEmpty()
                                   join dt in daThu on kh.MaKH equals dt.MaKH into dthu
                                   from dt in dthu.Where(x => (mb != null ? x.MaMB == mb.MaMB : x.MaKH == kh.MaKH)).DefaultIfEmpty()
                                       //join kt in khauTru on kh.MaKH equals kt.MaKH into ktru
                                       //from kt in ktru.DefaultIfEmpty()
                                       //join tt in thuTruoc on kh.MaKH equals tt.MaKH into ttruoc
                                       //from tt in ttruoc.DefaultIfEmpty()
                                       //join tttk in thuTruocTrongKy on kh.MaKH equals tttk.MaKH into tttrongky
                                       //from tttk in tttrongky.DefaultIfEmpty()
                                   join dtnc in daThuNoCu on kh.MaKH equals dtnc.MaKH into dtNoCu
                                   from dtnc in dtNoCu.Where(x => (mb != null ? x.MaMB == mb.MaMB : x.MaKH == kh.MaKH)).DefaultIfEmpty()
                                   join dttk in daThuTrongKy on kh.MaKH equals dttk.MaKH into dtTrongKy
                                   from dttk in dtTrongKy.Where(x => (mb != null ? x.MaMB == mb.MaMB : x.MaKH == kh.MaKH)).DefaultIfEmpty()
                                   select new
                                   {
                                       kh.MaKH,
                                       KyHieu = kh.KyHieu,
                                       TenLMB = lmb != null ? lmb.TenLMB : null,
                                       TenKN = kn != null ? kn.TenKN : null,
                                       DienTich = mb != null ? mb.DienTich : 0,
                                       //kh.MaPhu,
                                       TenKH = kh.TenKH,
                                       //DienThoai = kh.DienThoai,
                                       //kh.EmailKH,
                                       //DiaChi = kh.DiaChi,
                                       MaSoMB = mb != null ? mb.MaSoMB : null,
                                       MaMB = mb != null ? (int?)mb.MaMB : null,
                                       NoDauKy = (ndk == null ? 0 : ndk.SoTien.GetValueOrDefault()) -
                                                 (sqdk == null ? 0 : sqdk.SoTien.GetValueOrDefault()),
                                       PhatSinh = ps == null ? 0 : ps.SoTien,
                                       DaThu = dt == null ? 0 : dt.SoTien,
                                       //KhauTru = kt == null ? 0 : kt.SoTien,
                                       //ThuTruoc = tt == null ? 0 : tt.SoTien,
                                       //ThuTruocTK = tttk == null ? 0 : tttk.SoTien,
                                       DaThuNoCu = dtnc == null ? 0 : dtnc.SoTien,
                                       DaThuTrongKy = dttk == null ? 0 : dttk.SoTien,
                                   }).Select(p => new
                                   {
                                       //ThuTruoc = p.ThuTruoc,
                                       //NoDauKy = p.NoDauKy < 0 ? 0 : p.NoDauKy,
                                       TenLMB = p.TenLMB,
                                       TenKN = p.TenKN,
                                       DienTich = p.DienTich,
                                       NoDauKy = p.NoDauKy,
                                       PhatSinh = p.PhatSinh,
                                       //KhauTru = p.KhauTru,
                                       DaThu = p.DaThu,
                                       //ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) <
                                       //        0
                                       //    ? 0
                                       //    : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)),
                                       //ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu)) <
                                       //        0
                                       //    ? 0
                                       //    : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu)),
                                       ConNo = ((p.NoDauKy) + p.PhatSinh - (p.DaThu)),
                                       //ConNo=((p.NoDauKy<0?0:p.NoDauKy)+p.PhatSinh-(p.DaThu))
                                       MaKH = p.MaKH,
                                       KyHieu = p.KyHieu,
                                       //MaPhu = p.MaPhu,
                                       TenKH = p.TenKH,
                                       //DienThoai = p.DienThoai,
                                       //EmailKH = p.EmailKH,
                                       //DiaChi = p.DiaChi,
                                       MaMB = p.MaMB,
                                       MaSoMB = p.MaSoMB,

                                       //NoCuoi =
                                       //    (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0
                                       //        ? 0
                                       //        : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh -
                                       //           (p.DaThu + p.KhauTru - p.ThuTruocTK))) - p.ThuTruoc,
                                       DaThuNoCu = p.DaThuNoCu,
                                       p.DaThuTrongKy,
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
                        r["TenLMB"] = item.TenLMB;
                        r["TenKN"] = item.TenKN;
                        r["MaKH"] = item.KyHieu;
                        r["MaSoMB"] = item.MaSoMB;
                        r["TenKH"] = item.TenKH;
                        r["DienTich"] = item.DienTich.GetValueOrDefault();


                        foreach (var it in lDauKy_TenDv)
                        {
                            if (it.SoTien != 0)
                            {
                                decimal soTien = lDauKy_Ldv
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH && _.MaMB == item.MaMB).Sum(_ => _.SoTien)
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
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH && _.MaMB == item.MaMB).Sum(_ => _.SoTien)
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
                                    .Where(_ => _.MaLDV == it.MaLDV & _.MaKH == item.MaKH && _.MaMB == item.MaMB).Sum(_ => _.SoTien)
                                    .GetValueOrDefault();
                            }
                        }

                        r["DaThuNoCuTong"] = item.DaThuNoCu;

                        foreach (var it in lDaThuTrongKy_TenDv)
                        {
                            if (it.SoTien != 0)
                            {
                                r["DaThuTrongKy" + it.MaLDV] = lDaThuTrongKy_Ldv
                                    .Where(_ => _.MaLDV == it.MaLDV & _.MaKH == item.MaKH && _.MaMB == item.MaMB).Sum(_ => _.SoTien)
                                    .GetValueOrDefault();
                            }
                        }

                        r["DaThuTrongKyTong"] = item.DaThuTrongKy;

                        //foreach (var it in objDt)
                        //{
                        //    if (it.SoTien != 0)
                        //    {
                        //        //string.Format("{0:#,0.##; (0.##);-}", soTien);
                        //        var soTien = lDaThu_Ldv
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien).GetValueOrDefault();
                        //        r["DaThu" + it.MaLDV] = soTien;
                        //    }
                        //}

                        r["DaThuTong"] = item.DaThu;

                        //foreach (var it in objKt)
                        //{
                        //    if (it.SoTien != 0)
                        //    {
                        //        //string.Format("{0:#,0.##; (0.##);-}", soTien);
                        //        var soTien = lKhauTru_Ldv
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien).GetValueOrDefault();
                        //        r["KhauTru" + it.MaLDV] = soTien;
                        //    }
                        //}

                        //r["KhauTruTong"] = item.KhauTru;

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
                            //r["ConNo" + it.MaLDV] = ((dk < 0 ? 0 : dk) + ps - (dt1 + dt2)) <
                            //                        0
                            //    ? 0
                            //    : ((dk < 0 ? 0 : dk) + ps - (dt1 + dt2));
                            r["ConNo" + it.MaLDV] = ((dk) + ps - (dt1 + dt2));
                        }

                        r["ConNoTong"] = item.ConNo;

                        //foreach (var it in objTt)
                        //{
                        //    if (it.SoTien != 0)
                        //    {
                        //        //string.Format("{0:#,0.##; (0.##);-}", soTien);
                        //        var soTien = lThuTruoc_Ldv
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien).GetValueOrDefault();
                        //        r["ThuTruoc" + it.MaLDV] = soTien;
                        //    }
                        //}

                        //r["ThuTruocTong"] = item.ThuTruoc;

                        // NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK))) - p.ThuTruoc,
                        //foreach (var it in objNc)
                        //{
                        //    if (it.SoTien != 0)
                        //    {
                        //        //string.Format("{0:#,0.##; (0.##);-}", soTien);
                        //        var dk = objListLdvDk
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                        //        var ps = objListLdvPs
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                        //        var dt = objListLdvDt
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                        //        var kt = objListLdvKt
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                        //        var tt = objListLdvTt
                        //            .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                        //        var ttk = objListLdvTttk.Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH)
                        //            .Sum(_ => _.SoTien);
                        //        var soTien = (((dk < 0 ? 0 : dk) + ps - (dt + kt - ttk)) < 0
                        //                         ? 0
                        //                         : ((dk < 0 ? 0 : dk) + ps - (dt + kt - ttk))) - tt;
                        //        r["NoCuoi" + it.MaLDV] = string.Format("{0:#,0.##; (0.##);-}", soTien);
                        //    }
                        //}

                        //r["NoCuoiTong"] = item.NoCuoi;

                        _data.Rows.Add(r);
                    }

                    //cTotalDauKy.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "DauKyTong", "{0:n0}")
                    //});

                    //cPhatSinhTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "PhatSinhTong", "{0:n0}")
                    //});
                    //cDaThuNoCuTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "DaThuNoCuTong", "{0:n0}")
                    //});
                    //cDaThuTrongKyTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "DaThuTrongKyTong", "{0:n0}")
                    //});
                    //cDaThuTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "DaThuTong", "{0:n0}")
                    //});
                    ////cKhauTruTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    ////{
                    ////    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    ////        "KhauTruTong", "{0:n0}")
                    ////});
                    //cConNoTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "ConNoTong", "{0:n0}")
                    //});
                    //cThuTruocTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                    //        "ThuTruocTong", "{0:n0}")
                    //});
                    //cNoCuoiTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    //{
                    //    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NoCuoiTong",
                    //        "{0:n0}")
                    //});

                    #endregion
                }

                gc.DataSource = _data;
                gv.BestFitColumns();
            }
            catch (Exception ex)
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

        public class LoaiDichVu
        {
            public int MaKH { get; set; }
            public int? MaLDV { get; set; }
            public decimal? SoTien { get; set; }
            public int MaMB { set; get; }
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

        /// <summary>
        /// Cập nhật sổ quỹ, cho ngày thanh toán trong sổ quỹ phải luôn luôn lớn hơn ngày hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemCapNhatCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new Library.MasterDataContext())
            {
                var soQuys = (from sq in db.SoQuy_ThuChis join hd in db.dvHoaDons on sq.LinkID equals hd.ID where sq.TableName == "dvHoaDon" & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(sq.NgayPhieu, hd.NgayTT) >= 0 select new { sq.ID, sq.LinkID, sq.NgayPhieu, hd.NgayTT }).ToList();
                foreach (var item in soQuys)
                {
                    var soQuy = db.SoQuy_ThuChis.FirstOrDefault(_ => _.ID == item.ID);
                    if (soQuy != null)
                    {
                        soQuy.Nam = item.NgayTT.Value.Year;
                        soQuy.Thang = item.NgayTT.Value.Month;
                        soQuy.NgayPhieu = item.NgayTT;
                    }
                }
                db.SubmitChanges();
                Library.DialogBox.Success();
            }
        }
    }
}