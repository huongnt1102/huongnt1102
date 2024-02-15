using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;

namespace HopDongThueNgoai
{
    public partial class frmDaHetHan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext Data = new MasterDataContext();
        public frmDaHetHan()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                gcDanhSach.DataSource = null;
                gcDanhSach.DataSource = linqInstantFeedbackSource1;
            }
            catch
            {

            }
            gvDanhSach.BestFitColumns();
            gvChiTiet.BestFitColumns();
        }
        private void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
            gvDanhSach.BestFitColumns();
            gvChiTiet.BestFitColumns();
        }
        private void DanhSachChiTietHopDong()
        {
            var db = new MasterDataContext();
            try
            {
                var maHopDong = gvDanhSach.GetFocusedRowCellValue("SoHopDong");
                if(maHopDong==null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                gcChiTiet.DataSource = (from p in db.hdctnChiTietHopDongThueNgoais
                                        join cv in db.hdctnCongViecs on p.MaCongViec equals cv.RowID into cvs
                                        from cv in cvs.DefaultIfEmpty()
                                        where p.MaHopDong == maHopDong.ToString()
                                        orderby p.RowID
                                        select new
                                        {
                                            p.RowID,
                                            CongViec = cv.TenCongViec,
                                            p.SoCongViec,
                                            p.DonGia,
                                            p.SoTien,
                                            p.MaHopDong,
                                            p.TaiKhoanNo
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
        private void beiNgayHetHan_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void spneTimeHetHan_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            var denNgay = DateTime.Now;
            var tuNgay = DateTime.Now.AddDays(-Convert.ToInt32(beiNgayHetHan.EditValue));

            var sql = from p in db.hdctnDanhSachHopDongThueNgoais
                      join c in db.tnKhachHangs on p.NhaCungCap equals c.MaKH.ToString() into nccs
                      from c in nccs.DefaultIfEmpty()
                      join lt in db.LoaiTiens on p.LoaiTien equals lt.ID.ToString()
                      join ncv in db.hdctnNhomCongViecs on p.MaCongViec equals ncv.MaNhomCongViec
                      join nvn in db.tnNhanViens on p.NhanVienNhap equals nvn.MaNV.ToString() into nvns
                      from nvn in nvns.DefaultIfEmpty()
                      join pbn in db.tnPhongBans on nvn.MaPB equals pbn.MaPB into pbns
                      from pbn in pbns.DefaultIfEmpty()
                      join nvs in db.tnNhanViens on p.NhanVienSua equals nvs.MaNV.ToString() into nvss
                      from nvs in nvss.DefaultIfEmpty()
                      join pbs in db.tnPhongBans on nvs.MaPB equals pbs.MaPB into pbss
                      from pbs in pbss.DefaultIfEmpty()
                      where p.MaToaNha == beiToaNha.EditValue.ToString()
                      & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, p.NgayHetHan) >= 0
                      & SqlMethods.DateDiffDay(p.NgayHetHan, denNgay) >= 0

                      orderby p.TrangThai descending
                      select new
                      {
                          p.RowID,
                          NhaCungCap = c.HoKH + " " + c.TenKH,
                          p.SoHopDong,
                          p.MaToaNha,
                          p.SoChungTu,
                          LoaiTien = lt.KyHieuLT,
                          p.TyGia,
                          p.TaiKhoanCo,
                          p.DiaChi,
                          MaCongViec = ncv.TenNhomCongViec,
                          p.TienChuaThue,
                          p.VAT,
                          p.NgayKy,
                          p.NgayHieuLuc,
                          p.NgayHetHan,
                          p.KyThanhToan,
                          TrangThai = (p.TrangThai == 0) ? "Chưa Thanh Lý" : "Đã Thanh Lý",
                          NhanVienNhap = nvn.HoTenNV,
                          BoPhan_NVN = pbn.TenPB,
                          p.NgayNhap,
                          NhanVienSua = nvs.HoTenNV,
                          BoPhan_NVS = pbs.TenPB,
                          p.NgaySua,
                          p.TienThue,
                          p.TienSauThue
                      };
            e.QueryableSource = sql;
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch
            {

            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void frmDaHetHan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            LoadData();
            gvDanhSach.BestFitColumns();
            gvChiTiet.BestFitColumns();
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void gvDanhSach_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if(!gvDanhSach.IsGroupRow(e.RowHandle))
            {
                if(e.Info.IsRowIndicator)
                {
                    if(e.RowHandle <0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvDanhSach); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvDanhSach); }));
            }
        }
        bool cal(Int32 _width, GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void gvChiTiet_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDanhSach.IsGroupRow(e.RowHandle))
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
                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvChiTiet); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvChiTiet); }));
            }
        }

        private void gvDanhSach_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DanhSachChiTietHopDong();
            gvDanhSach.BestFitColumns();
            gvChiTiet.BestFitColumns();
        }

        private void gvDanhSach_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.FieldName == "TrangThai")
            {
                string tt = gv.GetRowCellDisplayText(e.RowHandle, gv.Columns["TrangThai"]);
                if (tt == "Đã Thanh Lý")
                {
                    e.Appearance.BackColor = Color.Green;
                    e.Appearance.BackColor2 = Color.White;
                }

            }
        }

        private void gvDanhSach_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.DanhSachChiTietHopDong();
            gvDanhSach.BestFitColumns();
            gvChiTiet.BestFitColumns();
        }
    }
}