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
    public partial class frmThanhLy : DevExpress.XtraEditors.XtraForm
    {

        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public frmThanhLy()
        {
            InitializeComponent();
        }
        private void SetDate( int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            beiTuNgay.EditValue = objKBC.DateFrom;
            beiDenNgay.EditValue = objKBC.DateTo;
        }
        private void LoadData()
        {
            try
            {
                gcThanhLy.DataSource = null;
                gcThanhLy.DataSource = linqInstantFeedbackSource1;
            }
            catch
            {

            }
        }
        private void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
            gvThanhLy.BestFitColumns();
            gvPhieuChi.BestFitColumns();
            gvPhieuThu.BestFitColumns();
        }
        private void Edit()
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
                var maHopDong = gvThanhLy.GetFocusedRowCellValue("MaHopDong");
                if(id==null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu thanh lý cần sửa. Xin cảm ơn!");
                    return;
                }
                if(maHopDong ==null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu thanh lý cần sửa. Xin cảm ơn!");
                    return;
                }
                using (frmThanhLy_Edit frm = new frmThanhLy_Edit()
                {
                    MaTN = (byte?)beiToaNha.EditValue,
                    ID = id,
                    MaHopDong=maHopDong.ToString()
                })
                {
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {

            }
        }
        private void Delete()
        {
            // xoa phieu thanh ly, cap nhat lai trang thai cua hop dong
            // chua xu ly xoa phieu thu chi
            int[] indexs = gvThanhLy.GetSelectedRows();
            if(indexs.Length<=0)
            {
                DialogBox.Error("Vui lòng chọn phiếu thanh lý cần xóa.");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach(int i in indexs)
                {
                    var objTl = db.hdctnThanhLies.Single(p => p.MaHopDong == gvThanhLy.GetRowCellValue(i, "MaHopDong").ToString());
                    var objHd = db.hdctnDanhSachHopDongThueNgoais.Single(p => p.SoHopDong == gvThanhLy.GetRowCellValue(i, "MaHopDong").ToString());
                    foreach (var congNo in objTl.hdctnCongNoNhaCungCaps)
                    {
                        if (congNo.PhieuChiId != null | congNo.PhieuThuId != null)
                        {
                            DialogBox.Error("Kiểm tra thấy phiếu thanh lý này đã có phiếu chi. Vui lòng xóa phiếu chi trước!");
                            return;
                        }
                    }

                    db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(objTl.hdctnCongNoNhaCungCaps);
                    db.hdctnThanhLies.DeleteOnSubmit(objTl);
                    
                    objHd.TrangThai = 1;
                    
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (System.Exception ex)
                    {
                        DialogBox.Error("Không xóa được, lỗi: " + ex.Message);
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                DialogBox.Error("Không xóa được, lỗi: " + ex.Message);
                return;
            }
            finally
            {
                db.Dispose();
                this.RefreshData();
            }
        }
        private void Detail()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
                if(id==null)
                {
                    switch(tabMain.SelectedTabPageIndex)
                    {
                        case 0:
                            gcPhieuChi.DataSource = null;
                            gcPhieuThu.DataSource = null;
                            break;
                        case 1:
                            break;
                    }
                }
                gcPhieuChi.DataSource = (from p in db.hdctnThanhLies
                                         join ct in db.pcChiTiets on p.RowID equals ct.LinkID
                                         join pc in db.pcPhieuChis on ct.MaPC equals pc.ID
                                         join ncc in db.tnKhachHangs on p.NhaCungCap equals ncc.MaKH.ToString()
                                         join lt in db.LoaiTiens on p.MaLoaiTien equals lt.ID.ToString()
                                         join hd in db.hdctnDanhSachHopDongThueNgoais on p.MaHopDong equals hd.SoHopDong
                                         where pc.TableName == "hdctnThanhLy"
                                         & pc.MaTN == (byte?)beiToaNha.EditValue
                                         & p.RowID == id
                                         select new
                                         {
                                             SoHopDong = p.MaHopDong,
                                             NhaCungCap = ncc.HoKH + " "+ ncc.TenKH,
                                             LoaiTien=lt.KyHieuLT,
                                             TyGia = p.TyGia,
                                             TongTienThanhLy = p.TienThanhLyQuyDoi,
                                             NgayChi=pc.NgayChi,
                                             NgayKy=hd.NgayKy,
                                             NgayHetHan=hd.NgayHetHan,
                                             NgayBanGiao=p.NgayThanhLy,
                                             TienThanhLy = ct.SoTien,
                                             pc.ID
                                         }).ToList();

                gcPhieuThu.DataSource = (from p in db.hdctnThanhLies
                                         join ct in db.ptChiTietPhieuThus on p.RowID equals ct.LinkID
                                         join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                         join ncc in db.tnKhachHangs on p.NhaCungCap equals ncc.MaKH.ToString()
                                         join lt in db.LoaiTiens on p.MaLoaiTien equals lt.ID.ToString()
                                         where ct.TableName == "hdctnThanhLy"
                                         & pt.MaTN == (byte?)beiToaNha.EditValue
                                         & p.RowID == id
                                         select new
                                         {
                                             SoPhieuThu = pt.SoPT,
                                             SoHopDong = p.MaHopDong,
                                             NhaCungCap=ncc.HoKH +" "+ncc.TenKH,
                                             LoaiTien= lt.KyHieuLT,
                                             TyGia=p.TyGia,
                                             NgayThu=pt.NgayThu,
                                             TienDaThu = ct.SoTien,
                                             pt.ID

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
        private void TraTien()
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
                if(id==null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu thanh lý cần trả tiền. Xin cảm ơn!");
                    return;
                }

                var phaiTra = (decimal)(gvThanhLy.GetFocusedRowCellValue("PhaiTra") ?? 0);
                if(phaiTra<=0)
                {
                    DialogBox.Error("Số còn lại phải trả phải lớn hơn 0");
                    return;
                }

                var hdtl = (from p in _db.hdctnThanhLies
                    where p.RowID == id
                    select p).FirstOrDefault();
                if (hdtl == null) return;

                Library.hdctnCongNoNhaCungCap congNo = new Library.hdctnCongNoNhaCungCap() { SoTien = 0, BuildingId = (byte?)beiToaNha.EditValue, ThanhLyId = hdtl.RowID, IsPhieuChi = true, DateCreate = System.DateTime.UtcNow.AddHours(7), UserCreate = Library.Common.User.MaNV, UserName = Library.Common.User.HoTenNV, HopDongId = hdtl.HopDongId };
                _db.hdctnCongNoNhaCungCaps.InsertOnSubmit(congNo);

                var objPhieuChi = new LandSoftBuilding.Fund.Output.ChiTietPhieuChiItem();
                objPhieuChi.LinkID = id;
                objPhieuChi.SoTien = phaiTra;
                objPhieuChi.DienGiai = string.Format("Chi tiền thanh lý thuê ngoài số:{0}", gvThanhLy.GetFocusedRowCellValue("SoThanhLy"));

                using (var frm = new LandSoftBuilding.Fund.Output.frmEdit())
                {
                    frm.MaTN = (byte?)beiToaNha.EditValue;
                    frm.MaKH = int.Parse(hdtl.NhaCungCap.ToString());
                    frm.TableName = "hdctnThanhLy";
                    frm.ChiTiets = new List<LandSoftBuilding.Fund.Output.ChiTietPhieuChiItem>();
                    frm.ChiTiets.Add(objPhieuChi);
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        
                        congNo.SoTien = frm.SoTien;
                        congNo.KhachHangId = frm.MaKH;
                        congNo.PhieuChiId = frm.ID;
                        congNo.SoTienThuChi = frm.SoTien;
                        congNo.DienGiai = frm.DienGiai;
                    }
                }

                _db.SubmitChanges();
                CapNhatTien(hdtl);
            }
            catch
            {

            }
        }
        private void ThuTien()
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
                if(id==null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu thanh lý cần xóa. Xin cảm ơn!");
                    return;
                }
                var _phaiThu = (decimal)(gvThanhLy.GetFocusedRowCellValue("PhaiThu") ?? 0);
                if(_phaiThu<=0)
                {
                    DialogBox.Error("Số tiền còn lại phải thu phải lớn hơn 0");
                    return;
                }

                _db = new MasterDataContext();

                var hdtl = (from p in _db.hdctnThanhLies
                    where p.RowID == id
                    select p).FirstOrDefault();
                if (hdtl == null) return;

                Library.hdctnCongNoNhaCungCap congNo = new Library.hdctnCongNoNhaCungCap() { SoTien = 0, BuildingId = (byte?)beiToaNha.EditValue, ThanhLyId = hdtl.RowID, IsPhieuChi = true, DateCreate = System.DateTime.UtcNow.AddHours(7), UserCreate = Library.Common.User.MaNV, UserName = Library.Common.User.HoTenNV, HopDongId = hdtl.HopDongId };
                _db.hdctnCongNoNhaCungCaps.InsertOnSubmit(congNo);

                var objPhieuThuChiTiet = new LandSoftBuilding.Fund.Input.ChiTietPhieuThuItem();
                objPhieuThuChiTiet.LinkID = id;
                objPhieuThuChiTiet.SoTien = _phaiThu;
                objPhieuThuChiTiet.DienGiai = string.Format("Thu tiền thanh lý hợp đồng thuê ngoài số: {0}", gvThanhLy.GetFocusedRowCellValue("SoThanhLy"));
                objPhieuThuChiTiet.TableName = "hdctnThanhLy";

                using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
                {
                    frm.MaTN = (byte?)beiToaNha.EditValue;
                    frm.MaKH = int.Parse(hdtl.NhaCungCap.ToString());
                    //frm.TableName = "hdctnThanhLy";
                    frm.ChiTiets = new List<LandSoftBuilding.Fund.Input.ChiTietPhieuThuItem>();
                    frm.ChiTiets.Add(objPhieuThuChiTiet);
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        
                        congNo.SoTien = frm.SoTien;
                        congNo.KhachHangId = frm.MaKH;
                        congNo.PhieuChiId = frm.MaPT;
                        congNo.SoTienThuChi = -frm.SoTien;
                        congNo.DienGiai = frm.DienGiai;
                    }
                }

                CapNhatTien(hdtl);

                _db.SubmitChanges();
            }
            catch
            {

            }
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetHopDongById(int? id)
        {
            return _db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == id);
        }

        private Library.hdctnThanhLy GetThanhLyById(int? id)
        {
            return _db.hdctnThanhLies.FirstOrDefault(_ => _.RowID == id);
        }

        private void CapNhatTien(Library.hdctnThanhLy thanhLy)
        {
            var hopDong = GetHopDongById(thanhLy.HopDongId);
            if (hopDong == null) return;

            var tongTienThu = _db.hdctnCongNoNhaCungCaps.Where(_ => _.IsPhieuChi == false & _.ThanhLyId == thanhLy.RowID).Sum(_=>_.SoTien).GetValueOrDefault();
            var tongTienChi = _db.hdctnCongNoNhaCungCaps.Where(_=>_.IsPhieuChi == true & _.ThanhLyId == thanhLy.RowID).Sum(_=>_.SoTien).GetValueOrDefault();

            thanhLy.DaTra = (tongTienThu == 0 & tongTienChi == 0) ? 0 : tongTienChi;
            thanhLy.DaThu = (tongTienThu == 0 & tongTienChi == 0) ? 0 : ((tongTienThu == 0) ? 0 : tongTienThu);
            thanhLy.PhaiTra = (tongTienThu == 0 & tongTienChi == 0) ? thanhLy.TienThanhLyQuyDoi : ((tongTienThu == 0) ? ((tongTienChi <= thanhLy.TienThanhLyQuyDoi) ? (thanhLy.TienThanhLyQuyDoi - tongTienChi) : 0) : ((thanhLy.DaTra - thanhLy.TienThanhLyQuyDoi) >= tongTienThu ? 0 : (tongTienThu - (thanhLy.DaTra - thanhLy.TienThanhLyQuyDoi))));
            thanhLy.PhaiThu = (tongTienThu == 0 & tongTienChi == 0) ? 0 : ((tongTienThu == 0) ? ((tongTienChi <= thanhLy.TienThanhLyQuyDoi) ? 0 : (tongTienChi - thanhLy.TienThanhLyQuyDoi)) : ((thanhLy.DaTra - thanhLy.TienThanhLyQuyDoi) >= tongTienThu ? (thanhLy.DaTra - thanhLy.TienThanhLyQuyDoi - tongTienThu) : 0));

            _db.SubmitChanges();

            hopDong = CapNhatHopDong(hopDong, tongTienChi, tongTienThu);

            this.RefreshData();
        }

        private Library.hdctnDanhSachHopDongThueNgoai CapNhatHopDong(Library.hdctnDanhSachHopDongThueNgoai hopDong, decimal? daTra, decimal? daThu)
        {
            // đã trả 100k, đã thu 150k, như vậy đã trả: phải làm sao thể hiện được là đang nợ -50k
            hopDong.DaTra = daTra;
            hopDong.DaThu = daThu;

            return hopDong;
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                var tuNgay = (DateTime)beiTuNgay.EditValue;
                var denNgay = (DateTime)beiDenNgay.EditValue;
                var db = new MasterDataContext();
                e.QueryableSource = from p in db.hdctnThanhLies
                                    join hd in db.hdctnDanhSachHopDongThueNgoais on p.MaHopDong equals hd.SoHopDong
                                    join lt in db.LoaiTiens on p.MaLoaiTien equals lt.ID.ToString()
                                    join ncc in db.tnKhachHangs on p.NhaCungCap equals ncc.MaKH.ToString() into nccs
                                    from ncc in nccs.DefaultIfEmpty()
                                    join cv in db.hdctnNhomCongViecs on p.CongViec equals cv.MaNhomCongViec
                                    join nvn in db.tnNhanViens on p.NhanVienNhap equals nvn.MaNV.ToString() into nvns
                                    from nvn in nvns.DefaultIfEmpty()
                                    join pbn in db.tnPhongBans on p.BoPhanNhap equals pbn.MaPB.ToString() into pbns
                                    from pbn in pbns.DefaultIfEmpty()
                                    join nvs in db.tnNhanViens on p.NhanVienSua equals nvs.MaNV.ToString() into nvss
                                    from nvs in nvss.DefaultIfEmpty()
                                    join pbs in db.tnPhongBans on p.BoPhanSua equals pbs.MaPB.ToString() into pbss
                                    from pbs in pbss.DefaultIfEmpty()
                                    where p.MaToaNha == (byte?)beiToaNha.EditValue
                                    & SqlMethods.DateDiffDay(tuNgay, p.NgayThanhLy) >= 0
                                    & SqlMethods.DateDiffDay(p.NgayThanhLy, denNgay) >= 0
                                    orderby p.NgayThanhLy descending
                                    select new
                                    {
                                        TrangThaiThu = (p.PhaiThu > 0) ? "PhaiThu" : "KhongPhaiThu",
                                        TrangThaiTra = (p.PhaiTra > 0) ? "PhaiTra" : "KhongPhaiTra",
                                        p.RowID,
                                        p.MaToaNha,
                                        p.MaHopDong,
                                        p.SoThanhLy,
                                        p.TienThanhLy,
                                        MaLoaiTien = lt.KyHieuLT,
                                        p.TyGia,
                                        p.TienThanhLyQuyDoi,
                                        p.DaThu,
                                        p.DaTra,
                                        p.PhaiThu,
                                        p.PhaiTra,
                                        p.LyDo,
                                        NhanVienNhap = nvn.HoTenNV,
                                        p.NgayNhap,
                                        BoPhanNhap = pbn.TenPB,
                                        NhanVienSua = nvs.HoTenNV,
                                        p.NgaySua,
                                        BoPhanSua = pbs.TenPB,
                                        NhaCungCap = ncc.HoKH + " " + ncc.TenKH,
                                        CongViec = cv.TenNhomCongViec
                                    };
                e.Tag = db;
            }
            catch{}
        }

        private void frmThanhLy_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

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
            this.LoadData();
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
            this.LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit();
        }

        private void btnTraTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.TraTien();
        }

        private void gvThanhLy_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }

        private void gvThanhLy_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Detail();
        }

        private void btnThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ThuTien();
        }

        private void cbxKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex);
        }

        private void btnCapNhatTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
                if (id == null)
                {
                    DialogBox.Alert("Vui lòng chọn phiếu thanh lý cần cập nhật.");
                    return;
                }

                var thanhLy = GetThanhLyById(id);
                if (thanhLy == null) return;
                CapNhatTien(thanhLy);
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void gvThanhLy_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if(e.Column.FieldName=="PhaiThu")
            {
                string value = gv.GetRowCellDisplayText(e.RowHandle, gv.Columns["TrangThaiThu"]);
                if(value=="PhaiThu")
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.BackColor2 = Color.LightCyan;
                    e.Appearance.ForeColor = Color.Blue;
                }
            }
            if(e.Column.FieldName=="PhaiTra")
            {
                string value = gv.GetRowCellDisplayText(e.RowHandle, gv.Columns["TrangThaiTra"]);
                if (value == "PhaiTra")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void ItemSuaPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var thanhLyId = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
            if (thanhLyId == null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thanh lý cần cập nhật.");
                return;
            }

            var thanhLy = GetThanhLyById(thanhLyId);
            if (thanhLy == null)
            {
                Library.DialogBox.Alert("Không tìm thấy phiếu thanh lý");
                return;
            }

            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu thu cần chỉnh sửa");
                return;
            }

            using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
            {
                frm.MaPT = id;
                frm.MaTN = (byte)beiToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }

            CapNhatTien(thanhLy);

            Detail();
        }

        private void ItemXoaPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu thu cần xóa");
                return;
            }

            var thanhLyId = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
            if (thanhLyId == null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thanh lý cần cập nhật.");
                return;
            }

            var thanhLy = GetThanhLyById(thanhLyId);
            if (thanhLy == null) return;

            if (Library.DialogBox.QuestionDelete() == DialogResult.No) return;

            LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu(id);

            CapNhatTien(thanhLy);
            Detail();
        }

        private void ItemSuaPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var thanhLyId = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
            if (thanhLyId == null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thanh lý cần cập nhật.");
                return;
            }

            var thanhLy = GetThanhLyById(thanhLyId);
            if (thanhLy == null)
            {
                Library.DialogBox.Alert("Không tìm thấy phiếu thanh lý");
                return;
            }

            var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu chi cần chỉnh sửa");
                return;
            }

            using (var frm = new LandSoftBuilding.Fund.Output.frmEdit())
            {
                frm.ID = id;
                frm.MaTN = (byte)beiToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    RefreshData();
            }

            CapNhatTien(thanhLy);

            Detail();
        }

        private void ItemXoaPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu chi cần xóa");
                return;
            }

            var thanhLyId = (int?)gvThanhLy.GetFocusedRowCellValue("RowID");
            if (thanhLyId == null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thanh lý cần cập nhật.");
                return;
            }

            var thanhLy = GetThanhLyById(thanhLyId);
            if (thanhLy == null)
            {
                Library.DialogBox.Alert("Không tìm thấy phiếu thanh lý");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            LandSoftBuilding.Fund.Class.PhieuChi.DeletePhieuChi(id);

            CapNhatTien(thanhLy);

            Detail();
        }

        private void itemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
    }
}