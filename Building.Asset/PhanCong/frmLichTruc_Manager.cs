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
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;

namespace Building.Asset.PhanCong
{
    public partial class frmLichTruc_Manager : XtraForm
    {
        private MasterDataContext _db;

        private DataTable _data;

        public frmLichTruc_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }
        #region Code
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

        private void LoadData()
        {
            try
            {
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    // load 1 lần, lần sau chỉ design lại gridview.
                    // chỉ load lại khi thêm sửa xóa
                    _db = new MasterDataContext();
                    var sql = (from p in _db.tbl_PhanCong_NhanVienChiTiets
                                join nv in _db.tnNhanViens on p.MaNV equals nv.MaNV
                                //join plc in _db.tbl_PhanCong_PhanLoaiCas on p.IDPhanLoaiCa equals plc.ID 
                                 //   into phanLoaiCa
                                //from plc in phanLoaiCa.DefaultIfEmpty()
                                where p.MaTN == ((byte?)beiToaNha.EditValue ?? Common.User.MaTN)
                                      & SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, p.Ngay) >= 0
                                      & SqlMethods.DateDiffDay(p.Ngay, (DateTime)beiDenNgay.EditValue) >= 0
                                select new //PhanCongLoad
                                {
                                    HoTenNV = nv.HoTenNV,
                                    MaSoNV = nv.MaSoNV,
                                    Ngay = p.Ngay,
                                    FieldName = string.Format("{0:dd_MM_yyyy}", p.Ngay),
                                    //PhanLoaiCa = plc.KyHieu ?? "-",
                                    PhanLoaiCa = p.tbl_PhanCong_PhanLoaiCa.KyHieu ?? "Chưa phân ca",
                                    //MaPhanLoaiCa = plc.ID,
                                    MaPhanLoaiCa =p.IDPhanLoaiCa!=null? p.tbl_PhanCong_PhanLoaiCa.ID:0,
                                    //TenLoaiCa=plc.Ten,
                                    TenLoaiCa=p.tbl_PhanCong_PhanLoaiCa.Ten??"",
                                    MaNV=nv.MaNV,
                                }).ToList();

                    _data = new DataTable();
                    _data.Columns.Add("HoTenNV"); // cột họ tên nhân viên
                    _data.Columns.Add("MaSoNV"); // cột mã số
                    _data.Columns.Add("MaNV");

                    // cột ngày
                    var fieldName = sql.Select(_ => _.FieldName).Distinct();
                    foreach (var f in fieldName)
                    {
                        _data.Columns.Add(f);
                    }

                    #region đổ gridview
                    gv.Columns.Clear();

                    // cột Mã số nhân viên
                    GridColumn maSo = new GridColumn();
                    maSo.Name = "colmaSo";
                    maSo.Caption = "Mã số NV";
                    maSo.FieldName = "MaSoNV";
                    maSo.Visible = true;
                    maSo.VisibleIndex = 0;
                    gv.Columns.Add(maSo);

                    // đổ cột gridview

                    // cột Họ tên nhân viên
                    DevExpress.XtraGrid.Columns.GridColumn colHoVaTen = new DevExpress.XtraGrid.Columns.GridColumn();
                    colHoVaTen.Name = "colHoVaTen";
                    colHoVaTen.Caption = "Họ và tên";
                    colHoVaTen.FieldName = "HoTenNV";
                    colHoVaTen.Visible = true;
                    colHoVaTen.VisibleIndex = 1;
                    gv.Columns.Add(colHoVaTen);
                    gv.BestFitColumns();

                    //BandedGridView bandedGridView = new BandedGridView();
                    //GridBand gridBandNhanVien = new GridBand();
                    //gridBandNhanVien.Caption = "Nhân viên";
                    //gridBandNhanVien.Name = "gridBand";
                    //gridBandNhanVien.OptionsBand.AllowSize = false;
                    //gridBandNhanVien.OptionsBand.FixedWidth = true;

                    //var bandedColumn1 = new BandedGridColumn();
                    //bandedColumn1 = bandedGridView.Columns.AddField("HoTenNV");
                    //bandedColumn1.Caption = "Họ tên NV";
                    //bandedColumn1.OwnerBand = gridBandNhanVien;
                    //bandedColumn1.Visible = true;
                    //bandedColumn1.Fixed = FixedStyle.Left;
                    //bandedColumn1.BestFit();

                    //var bandedColumn2 = new BandedGridColumn();
                    //bandedColumn2 = bandedGridView.Columns.AddField("MaSoNV");
                    //bandedColumn2.Caption = "Mã số NV";
                    //bandedColumn2.OwnerBand = gridBandNhanVien;
                    //bandedColumn2.Visible = true;
                    //bandedColumn2.Fixed = FixedStyle.Left;

                    // đổ cột ngày tháng tự động
                    var tuNgay = (DateTime)beiTuNgay.EditValue;
                    var denNgay = (DateTime)beiDenNgay.EditValue;

                    int ii = 3;
                    while (tuNgay.AddDays(1).Date <= denNgay.Date)
                    {
                        //GridBand gbMonth = new GridBand();
                        //gbMonth.Caption = string.Format("Tháng {0:MM-yyyy}",tuNgay);
                        //gbMonth.Name = tuNgay.ToString("MM_yyyy");

                        var ngay1 = string.Format("{0:dd_MM_yyyy}", tuNgay);
                        var colToDay = new GridColumn();
                        colToDay.Name = tuNgay.DayOfYear.ToString();
                        colToDay.Caption = tuNgay.ToString("dddd | dd-MM-yyyy");
                        //colToDay.FieldName = "Ngay0";
                        colToDay.FieldName = ngay1;
                        colToDay.Visible = true;
                        colToDay.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        colToDay.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        colToDay.VisibleIndex = 2;
                        gv.Columns.Add(colToDay);
                        

                        //var b = new BandedGridColumn();
                        //b = bandedGridView.Columns.AddField(ngay1);
                        //b.Caption = tuNgay.ToString("dd");
                        //b.OwnerBand = gbMonth;
                        //b.Visible = true;
                        //b.AutoFillDown = true;

                        DateTime dtResult = new DateTime(DateTime.Now.Year,tuNgay.Month,1);
                        dtResult = dtResult.AddMonths(1);
                        dtResult=dtResult.AddDays(-(dtResult.Day));
                        if (denNgay <= dtResult) dtResult = denNgay;

                        while (tuNgay.AddDays(1).Date <= dtResult.Date)
                        {
                            tuNgay = tuNgay.AddDays(1);

                            var cName = string.Format("{0:dd_MM_yyyy}", tuNgay);
                            //data.Columns.Add(cName);

                            DevExpress.XtraGrid.Columns.GridColumn c = new DevExpress.XtraGrid.Columns.GridColumn();
                            c.Name = tuNgay.DayOfYear.ToString();
                            c.Caption = tuNgay.ToString("dddd | dd-MM-yyyy");
                            //c.FieldName = "Ngay"+i; // ??
                            c.FieldName = cName;
                            c.Visible = true;
                            c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                            c.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                            c.VisibleIndex = ii;
                            gv.Columns.Add(c);
                            ii++;

                            //var d = new BandedGridColumn();
                            //d = bandedGridView.Columns.AddField(cName);
                            //d.Caption = tuNgay.ToString("dd");
                            //d.OwnerBand = gbMonth;
                            //d.Visible = true;
                        }

                        tuNgay = tuNgay.AddMonths(1);
                        tuNgay = new DateTime(tuNgay.Year, tuNgay.Month, 1);
                    }

                    
                    #endregion

                    // thêm 1 số cột loại ca
                    //GridBand gbLoaiCa = new GridBand();
                    //gbLoaiCa.Caption = "Tổng cộng";
                    //gbLoaiCa.Name = "gbLoaiCa";

                    var cloaiCa = sql.OrderBy(_=>_.PhanLoaiCa).Select(_ => _.PhanLoaiCa).Distinct();
                    foreach (var f in cloaiCa)
                    {
                        _data.Columns.Add(f);

                        GridColumn c = new GridColumn();
                        c.Name = f;
                        c.Caption = f;
                        c.FieldName = f;
                        c.Visible = true;
                        c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        c.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        c.VisibleIndex = ii;
                        
                        gv.Columns.Add(c);

                        //var b = new BandedGridColumn();
                        //b = bandedGridView.Columns.AddField(f);
                        //b.Caption = f;
                        //b.OwnerBand = gbLoaiCa;
                        //b.Visible = true;
                        //b.AutoFillDown = true;

                        ii++;
                    }

                    // đổ dữ liệu
                    var maSoNv = sql.OrderBy(_=>_.HoTenNV).Select(_ => _.MaSoNV).Distinct();
                    foreach (var nv in maSoNv)
                    {
                        var r = _data.NewRow();
                        r["HoTenNV"] = sql.First(_ => _.MaSoNV == nv).HoTenNV;
                        r["MaSoNV"] = nv;
                        r["MaNV"] = sql.First(_ => _.MaSoNV == nv).MaNV;
                        var caTruc = sql.Where(_ => _.MaSoNV == nv);
                        foreach (var ct in caTruc)
                        {
                            r[ct.FieldName] = sql.First(_ => _.MaSoNV == nv & _.FieldName == ct.FieldName).PhanLoaiCa;
                            r[ct.PhanLoaiCa] =
                                sql.Count(_ => _.MaSoNV == ct.MaSoNV & _.MaPhanLoaiCa == ct.MaPhanLoaiCa);
                        }

                        _data.Rows.Add(r);
                    }

                    // đổ dòng ghi chú
                    var rGhiChu = _data.NewRow();
                    rGhiChu["HoTenNV"] = "Ghi chú";
                    _data.Rows.Add(rGhiChu);

                    //var loaiCa = _db.tbl_PhanCong_PhanLoaiCas.ToList();
                    var loaiCa = sql.OrderBy(_=>_.PhanLoaiCa).Select(_ => _.MaPhanLoaiCa).Distinct();
                    foreach (var lc in loaiCa)
                    {
                        var r = _data.NewRow();
                        r["MaSoNV"] = sql.First(_ => _.MaPhanLoaiCa == lc).PhanLoaiCa;
                        r["HoTenNV"] = sql.First(_ => _.MaPhanLoaiCa == lc).TenLoaiCa;
                        var sl = sql.Where(_ => _.MaPhanLoaiCa == lc);
                        foreach (var s in sl)
                        {
                            r[s.FieldName] = sql.Count(_ => _.MaPhanLoaiCa == lc & _.FieldName == s.FieldName);
                        }

                        _data.Rows.Add(r);
                    }

                    var rTongCongNhanSu = _data.NewRow();
                    rTongCongNhanSu["HoTenNV"] = "Tổng cộng nhân sự";
                    var sl1 = sql;
                    foreach (var s in sl1)
                    {
                        rTongCongNhanSu[s.FieldName] = sql.Count(_ => _.FieldName == s.FieldName);
                    }
                    _data.Rows.Add(rTongCongNhanSu);

                    var rNhanSuThucTe = _data.NewRow();
                    rNhanSuThucTe["HoTenNV"] = "Nhân sự thực tế";
                    var sl2 = sql;
                    foreach (var s in sl2)
                    {
                        rNhanSuThucTe[s.FieldName] = sql.Where(_=>_.PhanLoaiCa!="Off").Count(_ => _.FieldName == s.FieldName);
                    }
                    _data.Rows.Add(rNhanSuThucTe);

                    // tạo band
                    
                    //gridBand.VisibleIndex = 0;
                    //var columnNames = new string[2] { "HoTenNV", "MaSoNV" };
                    //BandedGridColumn[] bandedColumns = new BandedGridColumn[2];
                    //for (int i = 0; i < 2; i++)
                    //{
                    //    bandedColumns[i] = (BandedGridColumn)bandedGridView.Columns.AddField(columnNames[i]);

                    //    bandedColumns[i].OwnerBand = gridBand;
                    //    bandedColumns[i].Visible = true;
                    //}

                    

                    //bandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
                    //    gridBand});
                    //bandedGridView.GridControl = gc;

                    //bandedGridView.Name = "bandedGridView";
                    //gc.ViewCollection.Add(bandedGridView);

                    //gc.MainView = bandedGridView; 

                    gc.DataSource = _data;

                    gv.BestFitColumns();
                    gv.OptionsBehavior.Editable = false;


                }
            }
            catch
            {
                //
            }
        }

        private void RefreshData()
        {
            LoadData();
            gv.BestFitColumns();
        }

        #endregion

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            var db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[1];
            SetDate(1);

            LoadData(); 

            gv.BestFitColumns();
        }


        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                ((MasterDataContext)e.Tag).Dispose();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        bool cal(Int32 width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
            RefreshData();
            gv.BestFitColumns();
        }

        private void bbiThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmLichTruc_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 0,  TuNgay=(DateTime)beiTuNgay.EditValue,DenNgay=(DateTime)beiDenNgay.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void bbiSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (gv.GetFocusedRowCellValue("ID") == null)
                //{
                //    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                //    return;
                //}
                using (var frm = new frmLichTruc_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 1, TuNgay = (DateTime)beiTuNgay.EditValue, DenNgay = (DateTime)beiDenNgay.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_PhanCong_NhanVienChiTiets.Where(_ =>
                        _.MaNV == int.Parse(gv.GetRowCellValue(r, "MaNV").ToString())&SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue,_.Ngay)>=0&SqlMethods.DateDiffDay(_.Ngay,(DateTime)beiDenNgay.EditValue)>=0);
                    foreach (var oo in o)
                    {
                        oo.IDPhanLoaiCa = null;
                    }
                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void gvDanhSachYeuCau_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gv.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gv); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gv); }));
            }

        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            var category = view.GetRowCellDisplayText(e.RowHandle, e.Column);

            switch (category)
            {
                case "H":
                    e.Appearance.ForeColor = Color.DeepSkyBlue;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;
                case "A":
                    e.Appearance.ForeColor = Color.Lime;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;
                case "B":
                    e.Appearance.ForeColor = Color.Blue;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;
                case "C":
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;
                case "Off":
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;

            }
        }

        private void itemDoiCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmDoiCa { MaTn = (byte?)beiToaNha.EditValue, 
                    Ngay = (DateTime)beiTuNgay.EditValue,Id=0,IsSua=0 })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gv_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                e.Allow = false;
                popupPhanCa.ShowPopup(gc.PointToScreen(e.Point));
            }
        }

        private void buttonPhanCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maNv = 0;
            if (gv.GetFocusedRowCellValue("MaNV") != null)
            {
                maNv = int.Parse(gv.GetFocusedRowCellValue("MaNV").ToString());
            }

            using (var frm = new frmLichTruc_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 0, TuNgay = (DateTime)beiTuNgay.EditValue, DenNgay = (DateTime)beiDenNgay.EditValue,MaNv=maNv })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmLichTruc_Import())
                {
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        private void itemDeXuatThayCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmThayCa
                { MaTn = (byte?)beiToaNha.EditValue, 
                    Ngay = (DateTime)beiTuNgay.EditValue,
                    Id = 0,
                    IsSua = 0
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}