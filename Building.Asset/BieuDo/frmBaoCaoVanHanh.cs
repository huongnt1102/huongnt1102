using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace Building.Asset.BaoCao
{
    public partial class frmBaoCaoVanHanh : XtraForm
    {
        private DateTime tuNgay;
        private DateTime denNgay;
        private DataTable _data;

        public frmBaoCaoVanHanh()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            //cmbToaNha.DataSource = Common.TowerList;
            cmbToaNha.DataSource = db.tnToaNhas;

            var objKbc = new KyBaoCao();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            LoadData();
        }

        private void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                gc.DataSource = null;
                if (beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    tuNgay = (DateTime)beiTuNgay.EditValue;
                    denNgay = (DateTime) beiDenNgay.EditValue;
                    var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                    var ltToaNha = strToaNha.Split(',');

                    var listToaNha = (from p in db.tnToaNhas
                        where ltToaNha.Contains(p.MaTN.ToString())
                        orderby p.TenTN
                        select new
                        {
                            p.MaTN, p.TenTN, KyHieu="MaTN"+p.MaTN
                        }).ToList();

                    #region Tạo cột table data
                    _data = new DataTable();
                    _data.Columns.Add("TieuDeGroup");
                    _data.Columns.Add("TieuDeDoc");

                    // tạo column tòa nhà tự động
                    foreach (var i in listToaNha)
                    {
                        _data.Columns.Add(i.KyHieu);
                    }

                    // cột tổng
                    _data.Columns.Add("Tong");
                    _data.Columns.Add("Key");
                    #endregion

                    #region Tạo cột gridview
                    // đổ cột gridview
                    gv.Columns.Clear();

                    // col Tiêu đề group
                    var colGroupContains = new GridColumn
                    {
                        Name = "colTieuDeGroup",
                        Caption = @"colTieuDeGroup",
                        FieldName = "TieuDeGroup",
                        Visible = false
                    };
                    gv.Columns.Add(colGroupContains);

                    var colTieuDeDoc = new GridColumn
                    {
                        Name = "colTieuDeDoc",
                        Caption = @"colTieuDeDoc",
                        FieldName = "TieuDeDoc",
                        Visible = true,
                        VisibleIndex = 0
                    };
                    gv.Columns.Add(colTieuDeDoc);

                    var index = 1;
                    foreach (var i in listToaNha)
                    {
                        var colTn = new GridColumn
                        {
                            Name = i.KyHieu,
                            Caption = i.KyHieu,
                            FieldName = i.KyHieu,
                            Visible = true,
                            VisibleIndex = index
                        };
                        gv.Columns.Add(colTn);
                        index++;
                    }

                    // cột tổng
                    var colTong = new GridColumn
                        {Name = "Tong", Caption = @"Tong", FieldName = "Tong", Visible = true, VisibleIndex = index};
                    gv.Columns.Add(colTong);

                    var colKey = new GridColumn { Name = "Key", Caption = @"Key", FieldName = "Key", Visible = false };
                    gv.Columns.Add(colKey);
                    #endregion

                    #region Đổ dữ liệu

                    // dòng 1: tiêu đề
                    var r1 = _data.NewRow();
                    r1["TieuDeGroup"] = "I. THÔNG TIN MẶT BẰNG:";
                    r1["TieuDeDoc"] = "";
                    // add tòa nhà vào đây
                    foreach (var i in listToaNha)
                    {
                        r1[i.KyHieu] = i.TenTN;
                    }

                    r1["Tong"] = "Tổng";
                    r1["Key"] = "1";
                    _data.Rows.Add(r1);

                    // dòng 2: mặt bằng
                    var mb = (from p in db.mbMatBangs
                        join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                        where ltToaNha.Contains(p.MaTN.ToString()) & SqlMethods.DateDiffDay(
                                                                       (DateTime) beiTuNgay.EditValue, p.NgayNhap) >= 0
                                                                   & SqlMethods.DateDiffDay(p.NgayNhap,
                                                                       (DateTime) beiDenNgay.EditValue) >= 0
                        group new {p} by new {p.MaTN}
                        into g
                        select new
                        {
                            Count = g.Count(), g.Key.MaTN, KyHieu="MaTN"+g.Key.MaTN
                        }).ToList();
                    var r2 = _data.NewRow();
                    r2["TieuDeGroup"] = "I. THÔNG TIN MẶT BẰNG:";
                    r2["TieuDeDoc"] = "Mặt bằng";
                    foreach (var i in mb)
                    {
                        r2[i.KyHieu] = i.Count;
                    }

                    r2["Tong"] = mb.Sum(_ => _.Count);
                    _data.Rows.Add(r2);

                    // dòng 3: thông tin khách hàng, tổng khách hàng
                    var objkh = (from p in db.mbMatBangs
                        join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                        join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                        where ltToaNha.Contains(p.MaTN.ToString())
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.NgayNhap) >= 0
                              & SqlMethods.DateDiffDay(p.NgayNhap, (DateTime) beiDenNgay.EditValue) >= 0
                        group new {p} by new {p.MaTN}
                        into g
                        select new
                        {
                            Count = g.Count(), g.Key.MaTN, KyHieu = "MaTN" + g.Key.MaTN
                        }).ToList();

                    var r3 = _data.NewRow();
                    r3["TieuDeGroup"] = "II. THÔNG TIN KHÁCH HÀNG:";
                    r3["TieuDeDoc"] = "Tổng khách hàng (chủ sở hữu)";
                    foreach (var i in objkh)
                    {
                        r3[i.KyHieu] = i.Count;
                    }

                    r3["Tong"] = objkh.Sum(_ => _.Count);
                    _data.Rows.Add(r3);

                    // dòng 4: tổng cư dân
                    var objCuDan = (from p in db.mbMatBangs
                        join kh in db.tnKhachHangs on p.MaKHF1 equals kh.MaKH
                        join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                        where ltToaNha.Contains(p.MaTN.ToString())
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.NgayNhap) >= 0
                              & SqlMethods.DateDiffDay(p.NgayNhap, (DateTime) beiDenNgay.EditValue) >= 0
                        group new {p} by new {p.MaTN}
                        into g
                        select new
                        {
                            Count = g.Count(), g.Key.MaTN, KyHieu = "MaTN" + g.Key.MaTN
                        }).ToList();
                    var r4 = _data.NewRow();
                    r4["TieuDeGroup"] = "II. THÔNG TIN KHÁCH HÀNG:";
                    r4["TieuDeDoc"] = "Tổng cư dân";
                    foreach (var i in objCuDan)
                    {
                        r4[i.KyHieu] = i.Count;
                    }

                    r4["Tong"] = objCuDan.Sum(_ => _.Count);
                    _data.Rows.Add(r4);

                    // dòng 5: danh sách yêu cầu phản ánh theo trạng thái
                    var objTnyc = (from p in db.tnycYeuCaus
                        join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                        join tt in db.tnycTrangThais on p.MaTT equals tt.MaTT
                        where ltToaNha.Contains(p.MaTN.ToString())
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.NgayYC) >= 0
                              & SqlMethods.DateDiffDay(p.NgayYC, (DateTime) beiDenNgay.EditValue) >= 0
                        select new
                        {
                            p.MaTN, p.MaTT, tt.TenTT
                        }).ToList();

                    var objTenTt = objTnyc.Select(_ => new {_.MaTT, _.TenTT}).Distinct().OrderBy(_ => _.MaTT);

                    //int st = 5;
                    foreach (var i in objTenTt)
                    {
                        var rI = _data.NewRow();
                        rI["TieuDeGroup"] = "III. DANH SÁCH YÊU CẦU/PHẢN ÁNH THEO TRẠNG THÁI:";
                        rI["TieuDeDoc"] = i.TenTT;
                        var objSoLuongTt = (from p in objTnyc
                            where p.MaTT == i.MaTT
                            group new {p} by new {p.MaTN}
                            into g
                            select new
                            {
                                Count = g.Count(), KyHieu = "MaTN" + g.Key.MaTN
                            }).ToList();
                        foreach (var ii in objSoLuongTt)
                        {
                            rI[ii.KyHieu] = ii.Count;
                        }
                        rI["Tong"] = objSoLuongTt.Sum(_ => _.Count);
                        _data.Rows.Add(rI);
                    }

                    // group nguồn đến

                    var objNguonDen = (from p in db.tnycYeuCaus
                        join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                        join nd in db.tnycNguonDens on p.MaNguonDen equals nd.ID into nguonDen
                        from nd in nguonDen.DefaultIfEmpty()
                        where ltToaNha.Contains(p.MaTN.ToString())
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.NgayYC) >= 0
                              & SqlMethods.DateDiffDay(p.NgayYC, (DateTime) beiDenNgay.EditValue) >= 0
                        select new
                        {
                            p.MaTN,p.MaNguonDen,TenNguonDen=p.MaNguonDen!=null?nd.TenNguonDen:"..."
                        }).ToList();

                    var tenNguonDen = objNguonDen.Select (_=> new{_.TenNguonDen,_.MaNguonDen}).Distinct().OrderByDescending(_=>_.TenNguonDen);
                    foreach (var i in tenNguonDen)
                    {
                        var ri = _data.NewRow();
                        ri["TieuDeGroup"] = "Phân loại theo nguồn nhận tin";
                        ri["TieuDeDoc"] = i.TenNguonDen;
                        var objSoLuongNguonDen = (from p in objNguonDen
                            where p.MaNguonDen == i.MaNguonDen
                            group new {p} by new {p.MaTN}
                            into g
                            select new
                            {
                                Count = g.Count(), KyHieu = "MaTN" + g.Key.MaTN
                            }).ToList();
                        foreach (var ii in objSoLuongNguonDen)
                        {
                            ri[ii.KyHieu] = ii.Count;
                        }

                        ri["Tong"] = objSoLuongNguonDen.Sum(_ => _.Count);
                        _data.Rows.Add(ri);
                    }

                    // admin
                    var objNews = (from p in db.app_News
                        join tn in db.tnToaNhas on p.TowerId equals tn.MaTN
                        where ltToaNha.Contains(p.TowerId.ToString())
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.DateCreate) >= 0
                              & SqlMethods.DateDiffDay(p.DateCreate, (DateTime) beiDenNgay.EditValue) >= 0
                        group new {p} by new {p.TowerId}
                        into g
                        select new
                        {
                            Count = g.Count(), KyHieu = "MaTN" + g.Key.TowerId
                        }).ToList();
                    var rNews = _data.NewRow();
                    rNews["TieuDeGroup"] = "IV. ADMIN";
                    rNews["TieuDeDoc"] = "Thông báo/Tin tức đã ban hành";
                    foreach (var i in objNews)
                    {
                        rNews[i.KyHieu] = i.Count;
                    }

                    rNews["Tong"] = objNews.Sum(_ => _.Count);
                    _data.Rows.Add(rNews);

                    // sử dụng app
                    var data = (from news in db.app_Residents
                        join rt in db.app_ResidentTowers on news.Id equals rt.ResidentId
                        join tower in db.tnToaNhas on rt.TowerId equals tower.MaTN
                        where ltToaNha.Contains(tower.MaTN.ToString()) &
                            SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, news.DateOfCreate) >= 0 &
                            SqlMethods.DateDiffDay(news.DateOfCreate, (DateTime)beiDenNgay.EditValue) >= 0 & news.IsResident == true

                        select new
                        {
                            news.DateOfCreate,
                            news.Id,
                            Login = news.IsLogin != null ? 1 : 0,tower.MaTN
                        }).ToList();
                    var soLuongDangKy = (from p in data
                        group new {p} by new {p.MaTN}
                        into g
                        select new
                        {
                            Count = g.Count(), KyHieu = "MaTN" + g.Key.MaTN
                        }).ToList();
                    var rDangKy = _data.NewRow();
                    rDangKy["TieuDeGroup"] = "Sử dụng App";
                    rDangKy["TieuDeDoc"] = "Số lượng đăng ký";
                    foreach (var i in soLuongDangKy)
                    {
                        rDangKy[i.KyHieu] = i.Count;
                    }

                    rDangKy["Tong"] = soLuongDangKy.Sum(_ => _.Count);
                    _data.Rows.Add(rDangKy);

                    var soLuongDangNhap = (from p in data
                        where p.Login == 1
                        group new {p} by new {p.MaTN}
                        into g
                        select new
                        {
                            Count = g.Count(), KyHieu = "MaTN" + g.Key.MaTN
                        }).ToList();
                    var rDangNhap = _data.NewRow();
                    rDangNhap["TieuDeGroup"] = "Sử dụng App";
                    rDangNhap["TieuDeDoc"] = "Số lượng đăng nhập";
                    foreach (var i in soLuongDangNhap)
                    {
                        rDangNhap[i.KyHieu] = i.Count;
                    }

                    rDangNhap["Tong"] = soLuongDangNhap.Sum(_ => _.Count);
                    _data.Rows.Add(rDangNhap);

                    #endregion

                    // group column and gán data cho gridview
                    gv.GroupCount = 1;
                    gv.GroupFooterShowMode = GroupFooterShowMode.Hidden;
                    gv.SortInfo.AddRange(new[] {
                        new GridColumnSortInfo(colGroupContains, DevExpress.Data.ColumnSortOrder.Ascending)});
                    gv.OptionsBehavior.Editable = false;

                    gc.DataSource = _data;

                    // bỏ sort tự động của grid group
                    gv.Columns["TieuDeGroup"].GroupIndex = 0;

                    gv.Columns["TieuDeGroup"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
                    gv.Columns["TieuDeGroup"].OptionsColumn.AllowSort = DefaultBoolean.False;

                    gv.BestFitColumns();
                }
            }
            catch
            {
                // ignored
            }
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            switch (e.Level)
            {
                case 0:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    e.LevelAppearance.ForeColor = Color.Black;
                    e.LevelAppearance.Options.UseTextOptions = true;
                    e.LevelAppearance.Font = new Font(e.LevelAppearance.Font.Name,
                        e.LevelAppearance.Font.Size, FontStyle.Bold);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView gridview = sender as GridView;
            try
            {
                if (e.RowHandle >= 0)
                {
                    string category = gridview.GetRowCellDisplayText(e.RowHandle, gridview.Columns["Key"]);
                    if (category == "1")
                    {
                        e.Appearance.Options.UseTextOptions = true;
                        e.Appearance.Font = new Font(e.Appearance.Font.Name,
                            e.Appearance.Font.Size, FontStyle.Bold);
                    }
                }
            }
            catch{}
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Tong")
                {
                    e.Appearance.Options.UseTextOptions = true;
                    e.Appearance.Font = new Font(e.Appearance.Font.Name,
                        e.Appearance.Font.Size, FontStyle.Bold);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void gv_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            // đặt sort bằng chính database
            if (e.Column == gv.Columns["TieuDeGroup"])
            {
                e.Handled = true;
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}