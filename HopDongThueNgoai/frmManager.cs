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
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;

namespace HopDongThueNgoai
{
    public partial class FrmManager : DevExpress.XtraEditors.XtraForm
    {
        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();

        private Library.MasterDataContext _db = new Library.MasterDataContext();

        private const int GroupHdtnId = 12;

        /// <summary>
        ///  get form id from table pqForm: SELECT * FROM pqForm AS pf WHERE pf.FormName LIKE '%HopDongThueNgoai.FrmManager%'
        /// </summary>
        private const int FormId = 83;
        
        private const string GroupSubHd = "HD";

        public FrmManager()
        {
            InitializeComponent();
            //gcChiTiet.DataSource = Data.hdctnChiTietHopDongThueNgoais;
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

        private void LoadData()
        {
            try
            {
                //gcDanhSach.DataSource = null;
                //gcDanhSach.DataSource = linqInstantFeedbackSource1;
                try
                {
                    var tuNgay = (DateTime)beiTuNgay.EditValue;
                    var denNgay = (DateTime)beiDenNgay.EditValue;

                    var db = new MasterDataContext();
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
                              join tt in db.hdctnTrangThais on p.TrangThai equals tt.MaTrangThai into trangThai
                              from tt in trangThai.DefaultIfEmpty()
                              where p.MaToaNha == itemBuilding.EditValue.ToString()
                                    & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, p.NgayKy) >= 0
                                    & SqlMethods.DateDiffDay(p.NgayKy, denNgay) >= 0
                              //& p.IsPhuLuc == false
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
                                  TrangThai = p.TrangThai == 0 ? "Chưa thanh lý" : tt.TenTrangThai,
                                  NhanVienNhap = nvn.HoTenNV,
                                  BoPhan_NVN = pbn.TenPB,
                                  p.NgayNhap,
                                  NhanVienSua = nvs.HoTenNV,
                                  BoPhan_NVS = pbs.TenPB,
                                  p.NgaySua,
                                  p.TienThue,
                                  p.TienSauThue,
                                  PhuLuc = p.IsPhuLuc == true ? "Phụ lục" : "Hợp đồng",
                                  tt.MaTrangThai
                              };
                    gcDanhSach.DataSource = sql;
                    //e.QueryableSource = sql;
                    //e.Tag = db;
                }
                catch
                {

                }
            }
            catch (System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
            // group summary
            GroupSummary();
        }

        private void GroupSummary()
        {
            gvDanhSach.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem()
            {
                FieldName = "Loai",
                SummaryType = DevExpress.Data.SummaryItemType.Count,
                DisplayFormat = @"(Tổng số: {0:n0})"
            });
        }

        private void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        private void Add()
        {
            try
            {
                using (FrmEdit frm = new FrmEdit { MaTn =(byte?)itemBuilding.EditValue})
                {
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch (System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
        }

        private void Edit()
        {
            try
            {
                var id = (int?)gvDanhSach.GetFocusedRowCellValue("RowID");
                var trangThai = gvDanhSach.GetFocusedRowCellValue("MaTrangThai");
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn Hợp Đồng cần chỉnh sửa, xin cảm ơn!");
                    return;
                }
                //if ((int?)trangThai == 3)
                //{
                //    DialogBox.Error("Hợp đồng này đã thanh lý, vui lòng chọn hợp đồng khác. Xin cảm ơn!");
                //    return;
                //}
                using (FrmEdit frm = new FrmEdit()
                {
                    MaTn = (byte?)itemBuilding.EditValue,
                    Id = id
                })
                {
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch(System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
        }

        private void Delete()
        {
            int[] indexs = gvDanhSach.GetSelectedRows();
            if(indexs.Length<=0)
            {
                DialogBox.Error("Vui lòng chọn những hợp đồng cần xóa");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach(int i in indexs)
                {
                    var objHd = db.hdctnDanhSachHopDongThueNgoais.Single(p => p.SoHopDong == gvDanhSach.GetRowCellValue(i, "SoHopDong").ToString());
                    if(objHd.TrangThai==3)
                    {
                        DialogBox.Error("Hợp đồng này đã thanh lý, vui lòng xóa thanh lý trước. Xin cảm ơn!");
                        return;
                    }

                    // delete all đánh giá
                    db.hdctnCongViec_DanhGias.DeleteAllOnSubmit(db.hdctnCongViec_DanhGias.Where(_=>_.HopDongId == objHd.RowID));
                    db.SubmitChanges();

                    var objCt = from p in db.hdctnChiTietHopDongThueNgoais
                                where p.MaHopDong == gvDanhSach.GetRowCellValue(i, "SoHopDong").ToString()
                                select p;
                    foreach(var r in objCt)
                    {
                        db.hdctnChiTietHopDongThueNgoais.DeleteOnSubmit(r);
                    }

                    db.hdctnLichSuTienTrinhs.DeleteAllOnSubmit(objHd.hdctnLichSuTienTrinhs);
                    
                    db.hdctnDanhSachHopDongThueNgoais.DeleteOnSubmit(objHd);
                }
                try
                {
                    db.SubmitChanges();
                }
                catch (System.Exception ex)
                {
                    Library.DialogBox.Error("Xóa không được, "+ex.Message);
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                this.RefreshData();
            }
        }

        private void LoadDetail()
        {
            var db = new MasterDataContext();
            try
            {
                var maHopDong = gvDanhSach.GetFocusedRowCellValue("SoHopDong");
                if (maHopDong == null)
                {
                    gcChiTiet.DataSource = null;

                    return;
                }

                switch (tabMain.SelectedTabPage.Name)
                {
                    case "tabChiTiet":
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
                        break;
                    case "tabPhieuChi":
                        gcPhieuChi.DataSource = (from cn in db.hdctnCongNoNhaCungCaps
                            join pc in db.pcPhieuChis on cn.PhieuChiId equals pc.ID into phieuChi
                            from pc in phieuChi.DefaultIfEmpty()
                            where cn.HopDongId == (int) gvDanhSach.GetFocusedRowCellValue("RowID") &
                                  cn.IsPhieuChi == true
                            select new
                            {
                                cn.DateCreate, SoPhieu = pc.SoPC, DienGiai = cn.DienGiai, SoTien = cn.SoTien
                            }).ToList();

                        break;
                    case "tabPhieuThu":
                        gcPhieuThu.DataSource = (from cn in db.hdctnCongNoNhaCungCaps
                            join pt in db.ptPhieuThus on cn.PhieuChiId equals pt.ID into phieuThus
                            from pt in phieuThus.DefaultIfEmpty()
                            where cn.HopDongId == (int)gvDanhSach.GetFocusedRowCellValue("RowID") &
                                  cn.IsPhieuChi == false
                            select new
                            {
                                cn.DateCreate,
                                SoPhieu = pt.SoPT,
                                DienGiai = cn.DienGiai,
                                SoTien = cn.SoTien
                            }).ToList();
                        break;

                    case "tabUpdateStatus": gcUpdateStatus.DataSource = db.hdctnLichSus.Where(_ => _.HopDongNo.ToLower() == maHopDong.ToString().ToLower() & _.LoaiId == 1); break;
                    case "tabUpdateHopDong": gcUpdateHopDong.DataSource = db.hdctnLichSus.Where(_ => _.HopDongNo.ToLower() == maHopDong.ToString().ToLower() & _.LoaiId == 2); break;
                    case "tabLichThanhToan": gcLichThanhToan.DataSource = db.hdctnLichThanhToans.Where(_=>_.HopDongId ==(int)gvDanhSach.GetFocusedRowCellValue("RowID")); break;
                    case "tabLichSuThucHien":
                        gcLichSuTienTrinh.DataSource = (from tt in db.hdctnLichSuTienTrinhs
                            join dt in db.tnKhachHangs on tt.KhachHangId equals dt.MaKH into khachHang
                            from dt in khachHang.DefaultIfEmpty()
                            where tt.HopDongId == (int) gvDanhSach.GetFocusedRowCellValue("RowID")
                            select new
                            {
                                tt.Id, tt.HopDongId, tt.DateCreate, tt.DienGiai,
                                HoTenKh = dt.IsCaNhan == true ? dt.HoKH + " " + dt.TenKH : dt.CtyTen
                            });
                        break;
                    case "tabTaiLieu": 
                        ctlTaiLieu1.FormID = FormId;
                        ctlTaiLieu1.LinkID = (int)gvDanhSach.GetFocusedRowCellValue("RowID");
                        ctlTaiLieu1.MaNV = Library.Common.User.MaNV;
                        ctlTaiLieu1.objNV = Library.Common.User;
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
        }

        private void ThanhLy()
        {
            try
            {
                var maHopDong = gvDanhSach.GetFocusedRowCellValue("SoHopDong");
                var trangThai = gvDanhSach.GetFocusedRowCellValue("MaTrangThai");
                if(maHopDong==null)
                {
                    DialogBox.Error("Vui lòng chọn Hợp Đồng cần thanh lý, xin cảm ơn!");
                    return;
                }

                if ((int?)trangThai ==3)
                {
                    DialogBox.Error("Hợp đồng này đang thanh lý, vui lòng chọn hợp đồng khác. Xin cảm ơn!");
                    return;
                }
                using (var frm = new frmThanhLy_Edit()
                {
                    MaTN = (byte?)itemBuilding.EditValue,
                    MaHopDong = maHopDong.ToString()
                })
                {
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch(System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                var tuNgay = (DateTime)beiTuNgay.EditValue;
                var denNgay = (DateTime)beiDenNgay.EditValue;

                var db = new MasterDataContext();
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
                    join tt in db.hdctnTrangThais on p.TrangThai equals tt.MaTrangThai into trangThai from tt in trangThai.DefaultIfEmpty()
                    where p.MaToaNha == itemBuilding.EditValue.ToString()
                          & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, p.NgayKy) >= 0
                          & SqlMethods.DateDiffDay(p.NgayKy, denNgay) >= 0
                    //& p.IsPhuLuc == false
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
                        TrangThai =p.TrangThai == 0? "Chưa thanh lý" : tt.TenTrangThai,
                        NhanVienNhap = nvn.HoTenNV,
                        BoPhan_NVN = pbn.TenPB,
                        p.NgayNhap,
                        NhanVienSua = nvs.HoTenNV,
                        BoPhan_NVS = pbs.TenPB,
                        p.NgaySua,
                        p.TienThue,
                        p.TienSauThue,
                        PhuLuc = p.IsPhuLuc == true ? "Phụ lục" : "Hợp đồng",
                        tt.MaTrangThai
                    };
                e.QueryableSource = sql;
                e.Tag = db;
            }
            catch
            {

            }
            
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch(System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
            this.LoadData();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;

            gvDanhSach.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            KyBaoCao objKBC = new KyBaoCao();
            foreach(string str in objKBC.Source)
            {
                cbxKyBaoCao.Items.Add(str);
            }
            beiKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();


        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            //this.LoadData();
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                    join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                    where tn.MaTN == (byte) itemBuilding.EditValue & rp.GroupID == GroupHdtnId
                    orderby rp.Rank
                    select new {rp.ID, rp.Name}).ToList();

                barPrint.ItemLinks.Clear();
                foreach (var i in ltReport)
                {
                    var itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name) {Tag = i.ID};
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                    barPrint.ItemLinks.Add(itemPrint);
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void itemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = (int?)gvDanhSach.GetFocusedRowCellValue("RowID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Hợp đồng] cần xem");
                return;
            }

            var rtfText = BuildingDesignTemplate.Class.HopDongThueNgoai.PrintHopDong(id.Value, (int) e.Item.Tag, GroupHdtnId, GroupSubHd);
            var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
            frm.ShowDialog(this);
        }

        private void gvDanhSach_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gvDanhSach); }));
            }
        }

        bool Cal(Int32 _width,GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void gvChiTiet_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gvChiTiet); }));
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Add();
        }

        private void gvDanhSach_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.LoadDetail();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit();
        }

        private void btnThanhLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ThanhLy();
        }

        private void gvDanhSach_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if(e.Column.FieldName=="TrangThai")
            {
                string tt = gv.GetRowCellDisplayText(e.RowHandle, gv.Columns["TrangThai"]);
                if(tt=="Đã Thanh Lý")
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }

            }
        }

        private void cbxKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex);
        }

        private void gvDanhSach_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.LoadDetail();
        }

        private void gvPhieuChi_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gvPhieuChi); }));
            }
        }

        private void TabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.LoadDetail();
        }

        private void ItemThemLichSuThucHien_ItemClick(object sender, ItemClickEventArgs e)
        {
            ThemLichSuThucHien();
        }

        private void ThemLichSuThucHien()
        {
            var id = (int?)gvDanhSach.GetFocusedRowCellValue("RowID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn hợp đồng");
                return;
            }

            using (var frm = new HopDongThueNgoai.LichSuTienTrinh.FrmLichSuThucHienEdit { HopDongId = id, BuildingId = (byte?)itemBuilding.EditValue })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
            }
        }

        private void ItemPopupThemLichSuTienTrinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            ThemLichSuThucHien();
        }

        private void ItemPopupSuaLichSuTienTrinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = (int?)gvLichSuTienTrinh.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch sử");
                return;
            }

            using (var frm = new HopDongThueNgoai.LichSuTienTrinh.FrmLichSuThucHienEdit() { BuildingId = (byte?)itemBuilding.EditValue, TienTrinhId = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
            }
        }

        private void ItemXoaLichSuTienTrinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            var indexs = gvLichSuTienTrinh.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch sử thực hiện");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var _db = new Library.MasterDataContext();

            foreach (var i in indexs) _db.hdctnLichSuTienTrinhs.DeleteAllOnSubmit(_db.hdctnLichSuTienTrinhs.Where(_ => _.Id == (int)gvLichSuTienTrinh.GetRowCellValue(i, "Id")));

            _db.SubmitChanges();
            LoadDetail();
        }

        private void itemXoaLichThanhToan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var indexs = gvLichThanhToan.GetSelectedRows();
            if (KiemTra(indexs)) return;

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            foreach (var i in indexs)
            {
                DeleteLichThanhToan((int)gvLichThanhToan.GetRowCellValue(i, "Id"));
            }

            _db.SubmitChanges();

            LoadDetail();
        }

        #region Delete lịch thanh toán

        private bool KiemTra(int[] indexs)
        {
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch thanh toán");
                return true;
            }
            return false;
        }

        private void DeleteLichThanhToan(int? lichThanhToanId)
        {
            var congNoNhaCungCaps = _db.hdctnCongNoNhaCungCaps.Where(_ => _.LichThanhToanId == lichThanhToanId);
            foreach (var item in congNoNhaCungCaps)
            {
                if (item.IsPhieuChi == false) LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu(item.PhieuThuId);
                else LandSoftBuilding.Fund.Class.PhieuChi.DeletePhieuChi(item.PhieuChiId);

                var hopDong = GetHopDongById(item.HopDongId);
                if (hopDong == null) continue;
                hopDong.DaTra = item.IsPhieuChi == false ? hopDong.DaTra : hopDong.DaTra.GetValueOrDefault() - item.SoTien;
                hopDong.DaThu = item.IsPhieuChi == false ? hopDong.DaThu.GetValueOrDefault() - item.SoTien : hopDong.DaThu;
            }

            _db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(congNoNhaCungCaps);
            _db.hdctnLichThanhToans.DeleteAllOnSubmit(_db.hdctnLichThanhToans.Where(_ => _.Id == lichThanhToanId));
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetHopDongById(int? hopDongId)
        {
            return _db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == hopDongId);
        }

        #endregion
    }
}