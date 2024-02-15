using System;
using System.Linq;
using Library;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;

namespace DichVu.HoaDon
{
    public partial class frmThanhToanMulti : DevExpress.XtraEditors.XtraForm
    {
        readonly MasterDataContext db;
        public int MaMB;
        decimal TongTien = 0;
        string sSoPhieu;
        public tnNhanVien objnhanvien;
        TienTeCls ttcls = new TienTeCls();
        DateTime now;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmThanhToanMulti()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThanhToanAll_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            try
            {
                LoadData();
                LoadChuKyThanhToanHopDong();
                LoadChuKyThanhToanDien();
                LoadChuKyThanhToanGas();
                LoadChuKyThanhToanNuoc();
                LoadChuKyThanhToanGiuXe();
                LoadChuKyThanhToanThangMay();
                LoadChuKyThanhToanPhiQuanLy();
                LoadChuKyThanhToanPhiVeSinh();
            }
            catch { }
            itemHuongDan.Click += ItemHuongDan_Click;
        }
        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void LoadChuKyThanhToanPhiVeSinh()
        {
            int SoChuKy = 60;
            DateTime NgayHetHanHD = db.GetSystemDate();

            var objmatbang = db.mbMatBangs.Single(p => p.MaMB == MaMB);
            var objhopdong = db.thueHopDongs.Where(p => p.MaMB == MaMB).FirstOrDefault();
            var DaTT = db.PhiVeSinhs.Where(p => p.MaMB == MaMB).ToList();

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();

            if (objhopdong != null)
            {
                SoChuKy = objhopdong.ThoiHan == null ? 60 : objhopdong.ThoiHan.Value + SqlMethods.DateDiffYear((objhopdong.NgayBG ?? objhopdong.NgayHD.Value), now);
                
                if (objhopdong.ThoiHan == null)
                {
                    for (int i = 0; i < SoChuKy; i++)
                    {
                        if (DaTT.Where(p => p.ThangThanhToan.Value.Month == (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).Month & p.ThangThanhToan.Value.Year == now.Year).Count() <= 0)
                        {
                            ChuKyItem item = new ChuKyItem();
                            item.Display = string.Format("Tháng: {0} Thành tiền: {1}", (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year).ToShortDateString().Substring(3, 7), (0).ToString("C"));
                            item.Value = objhopdong.mbMatBang.MaMB;
                            item.ThangThanhToanPQL = (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year);
                            ListChuKy.Add(item);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < SoChuKy; i++)
                    {
                        if (DaTT.Where(p => p.ThangThanhToan.Value.Month == (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).Month & p.ThangThanhToan.Value.Year == now.Year).Count() <= 0)
                        {
                            ChuKyItem item = new ChuKyItem();
                            item.Display = string.Format("Tháng: {0} Thành tiền: {1}", (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year).ToShortDateString().Substring(3, 7), ( 0).ToString("C"));
                            item.Value = objhopdong.mbMatBang.MaMB;
                            item.ThangThanhToanPQL = (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year);
                            ListChuKy.Add(item);
                        }
                    }
                }
            }

            else
            {
                for (int i = 0; i < SoChuKy; i++)
                {
                    if (DaTT.Where(p => p.ThangThanhToan.Value.Month == ( now).AddMonths(i).Month & p.ThangThanhToan.Value.Year == ( now).Year).Count() <= 0)
                    {
                        ChuKyItem item = new ChuKyItem();
                        item.Display = string.Format("Tháng: {0} Thành tiền: {1}", ( now).AddMonths(i).AddYears(now.Year - (now).Year).ToShortDateString().Substring(3, 7), ( 0).ToString("C"));
                        item.Value = objmatbang.MaMB;
                        item.ThangThanhToanPQL = ( now).AddMonths(i).AddYears(now.Year - (now).Year);
                        ListChuKy.Add(item);
                    }
                }
            }
            checkedListBoxPhiVeSinh.DataSource = ListChuKy;
            checkedListBoxPhiVeSinh.UnCheckAll();
        }

        private void LoadChuKyThanhToanGas()
        {
          
        }

        private void LoadChuKyThanhToanPhiQuanLy()
        {
            int SoChuKy = 60;
            DateTime NgayHetHanHD = db.GetSystemDate();

            var objmatbang = db.mbMatBangs.Single(p => p.MaMB == MaMB);
            var objhopdong = db.thueHopDongs.Where(p => p.MaMB == MaMB).FirstOrDefault();
            var DaTT = db.PhiQuanLies.Where(p => p.MaMB == MaMB).ToList();

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();

            if (objhopdong != null)
            {
                SoChuKy = objhopdong.ThoiHan == null ? 60 : objhopdong.ThoiHan.Value + SqlMethods.DateDiffYear((objhopdong.NgayBG ?? objhopdong.NgayHD.Value), now);
                //NgayHetHanHD = (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(objhopdong.ThoiHan ?? 12);

                if (objhopdong.ThoiHan == null)
                {
                    for (int i = 0; i < SoChuKy; i++)
                    {
                        if (DaTT.Where(p => p.ThangThanhToan.Value.Month == (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).Month & p.ThangThanhToan.Value.Year == now.Year).Count() <= 0)
                        {
                            ChuKyItem item = new ChuKyItem();
                            item.Display = string.Format("Tháng: {0} Thành tiền: {1}", (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year).ToShortDateString().Substring(3, 7), ( 0).ToString("C"));
                            item.Value = objhopdong.mbMatBang.MaMB;
                            item.ThangThanhToanPQL = (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year);
                            ListChuKy.Add(item);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < SoChuKy; i++)
                    {
                        if (DaTT.Where(p => p.ThangThanhToan.Value.Month == (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).Month & p.ThangThanhToan.Value.Year == now.Year).Count() <= 0)
                        {
                            ChuKyItem item = new ChuKyItem();
                            item.Display = string.Format("Tháng: {0} Thành tiền: {1}", (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year).ToShortDateString().Substring(3, 7), (0).ToString("C"));
                            item.Value = objhopdong.mbMatBang.MaMB;
                            item.ThangThanhToanPQL = (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).AddMonths(i).AddYears(now.Year - (objhopdong.NgayBG ?? objhopdong.NgayHD.Value).Year);
                            ListChuKy.Add(item);
                        }
                    }
                }
            }

            else
            {
                for (int i = 0; i < SoChuKy; i++)
                {
                    if (DaTT.Where(p => p.ThangThanhToan.Value.Month == (now).AddMonths(i).Month & p.ThangThanhToan.Value.Year == ( now).Year).Count() <= 0)
                    {
                        ChuKyItem item = new ChuKyItem();
                        item.Display = string.Format("Tháng: {0} Thành tiền: {1}", (now).AddMonths(i).AddYears(now.Year - (now).Year).ToShortDateString().Substring(3, 7), (0).ToString("C"));
                        item.Value = objmatbang.MaMB;
                        item.ThangThanhToanPQL = (now).AddMonths(i).AddYears(now.Year - ( now).Year);
                        ListChuKy.Add(item);
                    }
                }
            }
            checkedListBoxPhiQuanLy.DataSource = ListChuKy;
            checkedListBoxPhiQuanLy.UnCheckAll();
            #region dua vao hop dong

            /*
            var objmatbang = db.mbMatBangs.Single(p => p.MaMB == MaMB);
            var objhopdong = db.thueHopDongs.Where(p => p.MaMB == MaMB).FirstOrDefault();
            if (objhopdong != null)
            {
                var DaTT = db.PhiQuanLies.Where(p => p.MaMB == MaMB).ToList();
                var NgayHetHanHD = objhopdong.NgayBG.Value.AddMonths(objhopdong.ThoiHan ?? 12);
                var SoChuKy = objhopdong.ThoiHan ?? 12;

                var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();

                if (objhopdong.ThoiHan == null)
                {
                    for (int i = 0; i < SoChuKy; i++)
                    {
                        if(DaTT.Where(p=>p.NgayThanhToan.Value.Month == objhopdong.NgayBG.Value.AddMonths(i).Month & p.NgayThanhToan.Value.Year == now.Year).Count() <= 0)
                        {
                            ChuKyItem item = new ChuKyItem();
                            item.Display = string.Format("Tháng: {0}/{1} Thành tiền: {2}", objhopdong.NgayBG.Value.AddMonths(i).Month, now.Year, (objhopdong.mbMatBang.PhiQuanLy ?? 0).ToString("C"));
                            item.Value = objhopdong.MaHD;
                            item.ThangThanhToanPQL = objhopdong.NgayBG.Value.AddMonths(i);
                            ListChuKy.Add(item);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < SoChuKy; i++)
                    {
                        if (DaTT.Where(p => p.NgayThanhToan.Value.Month == objhopdong.NgayBG.Value.AddMonths(i).Month & p.NgayThanhToan.Value.Year == now.Year).Count() <= 0)
                        {
                            ChuKyItem item = new ChuKyItem();
                            item.Display = string.Format("Tháng: {0}/{1} Thành tiền: {2}", objhopdong.NgayBG.Value.AddMonths(i).Month, objhopdong.NgayBG.Value.AddMonths(i).Year, (objhopdong.mbMatBang.PhiQuanLy ?? 0).ToString("C"));
                            item.Value = objhopdong.MaHD;
                            item.ThangThanhToanPQL = objhopdong.NgayBG.Value.AddMonths(i);
                            ListChuKy.Add(item);
                        }
                    }
                }
             
                
                
            }
            */
            #endregion

        }

        private void LoadData()
        {
            var objmb = db.mbMatBangs.Single(p => p.MaMB == MaMB);
            txtMatBang.Text = objmb.MaSoMB;
            DateTime now = db.GetSystemDate();
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            lookChietKhau.Properties.DataSource = db.PhiQuanLy_ChietKhaus;
            //Dien ten mac dinh
            try
            {
                txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                txtDienGiai.Text = string.Format("Thanh toán phí dịch vụ tổng hợp cho mặt bằng {0}", objmb.MaSoMB);
            }
            catch { }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            DateTime now = db.GetSystemDate();
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);

            try
            {
                #region Lưu phiếu
                PhieuThu objphieuthu = new PhieuThu()
                {
                    DiaChi = txtDiaChi.Text.Trim(),
                    NguoiNop = txtNguoiNop.Text.Trim(),
                    DichVu = 100,
                    DienGiai = txtDienGiai.Text.Trim(),
                    DotThanhToan = db.GetSystemDate(),
                    MaHopDong = MaMB.ToString(),
                    MaNV = objnhanvien.MaNV,
                    NgayThu = now,
                    SoTienThanhToan = spinTongTien.Value,
                    SoPhieu = "PTTH-" + sSoPhieu,
                    KeToanDaDuyet = false,
                    MaMB = MaMB,
                    SoThangThuPhiQuanLy = checkedListBoxPhiQuanLy.CheckedItems.Count,
                    SoTienChietKhauPhiQL = spinSoTienDuocCK.Value
                };

                db.PhieuThus.InsertOnSubmit(objphieuthu);
                #endregion

                #region Luu du lieu
                #region dien
                if (checkedListDien.CheckedItems.Count > 0)
                {
                    foreach (ChuKyItem item in checkedListDien.CheckedItems)
                    {
                        var objdien = db.dvdnDiens.Single(p => p.ID == item.Value);
                        objdien.NguoiNop = txtNguoiNop.Text.Trim();
                        objdien.DiaChi = txtDiaChi.Text.Trim();
                        objdien.DaTT = true;
                        objdien.NgayThanhToan = now;
                        objdien.ChuyenKhoan = ckChuyenKhoan.Checked;
                        objdien.SoPhieuThu = "PTTH-" + sSoPhieu;
                        objdien.ConNo = 0;
                        objdien.DaThanhToan = objdien.TongTien;
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region nuoc
                if (checkedListNuoc.CheckedItems.Count > 0)
                {
                    foreach (ChuKyItem item in checkedListNuoc.CheckedItems)
                    {
                        var objnuoc = db.dvdnNuocs.Single(p => p.ID == item.Value);
                        objnuoc.NguoiNop = txtNguoiNop.Text.Trim();
                        objnuoc.DiaChi = txtDiaChi.Text.Trim();
                        objnuoc.DaTT = true;
                        objnuoc.NgayThanhToan = now;
                        objnuoc.ChuyenKhoan = ckChuyenKhoan.Checked;
                        objnuoc.SoPhieuThu = "PTTH-" + sSoPhieu;
                        objnuoc.ConNo = 0;
                        objnuoc.DaThanhToan = objnuoc.TongTien;
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region thang may
                if (checkedListThangMay.CheckedItems.Count > 0)
                {
                    foreach (ChuKyItem item in checkedListThangMay.CheckedItems)
                    {
                        var objtm = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == item.Value);
                        objtm.NguoiNop = txtNguoiNop.Text.Trim();
                        objtm.DiaChi = txtDiaChi.Text.Trim();
                        objtm.DaTT = true;
                        objtm.NgayThanhToan = now;
                        objtm.ChuyenKhoan = ckChuyenKhoan.Checked;
                        objtm.SoPhieuThu = "PTTH-" + sSoPhieu;
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region the xe
                if (checkedListGiuXe.CheckedItems.Count > 0)
                {
                }
                #endregion
                #region dvk
                if (checkedListDichVuKhac.CheckedItems.Count > 0)
                {
                    foreach (ChuKyItem item in checkedListDichVuKhac.CheckedItems)
                    {
                        var objdvk = db.dvkDichVuThanhToans.Single(p => p.ThanhToanID == item.Value);
                        objdvk.NguoiNop = txtNguoiNop.Text.Trim();
                        objdvk.DiaChi = txtDiaChi.Text.Trim();
                        objdvk.DaTT = true;
                        objdvk.NgayThanhToan = now;
                        objdvk.ChuyenKhoan = ckChuyenKhoan.Checked;
                        objdvk.SoPhieuThu = "PTTH-" + sSoPhieu;
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region hop dong
                if (checkListChuKy.CheckedItems.Count > 0)
                {
                    foreach (ChuKyItem item in checkListChuKy.CheckedItems)
                    {
                        thueCongNo objthue = db.thueCongNos.Single(p => p.MaCN == item.Value);
                        objthue.DaThanhToan = objthue.ConNo;
                        objthue.ConNo = 0;
                        objthue.NgayThanhToan = now;
                        objthue.ChuyenKhoan = ckChuyenKhoan.Checked;
                        objthue.SoPhieuThu = "PTTH-" + sSoPhieu;
                        db.SubmitChanges();
                    }
                }
                #endregion
                #region pql
                if (checkedListBoxPhiQuanLy.CheckedItems.Count > 0)
                {
                    foreach (ChuKyItem item in checkedListBoxPhiQuanLy.CheckedItems)
                    {
                        var objhd = db.mbMatBangs.Single(p => p.MaMB == item.Value);
                        Library.PhiQuanLy objpql = new Library.PhiQuanLy();
                        objpql.ConNo = 0;
                        objpql.MaMB = MaMB;
                        objpql.MaNV = objnhanvien.MaNV;
                        objpql.ChuyenKhoan = ckChuyenKhoan.Checked;
                        objpql.SoPhieuThu = "PTTH-" + sSoPhieu;
                        objpql.NgayThanhToan = now;
                        objpql.ThangThanhToan = item.ThangThanhToanPQL;
                        db.PhiQuanLies.InsertOnSubmit(objpql);
                        db.SubmitChanges();
                    }
                }
                #endregion
                #endregion

                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu(objphieuthu.MaPhieu, "no");  
                    //rpt.ShowPreviewDialog();
                }
                if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objphieuthu);
                    //rpt.ShowPreviewDialog();
                }
            }
            catch
            {
                DialogBox.Error("Lưu dữ liệu thất bại, vui lòng thử lại");  
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void LoadChuKyThanhToanHopDong()
        {
            var chuky = db.thueCongNos.Where(p => p.thueHopDong.MaMB == this.MaMB & p.ConNo > 0)
                    .Select(p => new
                    {
                        KeyValue = p.MaCN,
                        TuNgay = p.ChuKyMin,
                        DenNgay = p.ChuKyMax,
                        ChuKy = string.Format("{0} - {1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                        p.thueHopDong.ThoiHan,
                        p.thueHopDong.SoHD
                    });
            try
            {
                var objhd = db.thueHopDongs.FirstOrDefault(p => p.MaMB == MaMB);
                txtSoHD.Text = objhd.SoHD;
                txtThoiHan.Text = (objhd.ThoiHan ?? 0).ToString();
            }
            catch
            {
                txtSoHD.Text = txtThoiHan.Text = "Không có hợp đồng";
            }

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();
            if (chuky.Count() <= 0)
            {
                checkListChuKy.DataSource = null;
                return;
            }
            foreach (var item in chuky)
            {
                ChuKyItem it = new ChuKyItem() { Display = item.ChuKy, Value = item.KeyValue };
                ListChuKy.Add(it);
            }
            checkListChuKy.UnCheckAll();
            checkListChuKy.DataSource = ListChuKy;
        }

        void LoadChuKyThanhToanDien()
        {
            var chuky = db.dvdnDiens.Where(p => p.MaMB == this.MaMB & !p.DaTT.Value)
                    .Select(p => new
                    {
                        KeyValue = p.ID,
                        p.NgayNhap,
                        p.SoTieuThu,
                        p.ConNo
                    });

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();
            if (chuky.Count() <= 0)
            {
                checkedListDien.DataSource = null;
                return;
            }
            foreach (var item in chuky)
            {
                ChuKyItem it = new ChuKyItem()
                {
                    Display = string.Format("Tháng: {0} Tiêu thụ: {1} Thành tiền: {2}", item.NgayNhap.Value.ToString("MM/yyyy"), item.SoTieuThu, (item.ConNo ?? 0).ToString("C")),
                    Value = item.KeyValue
                };
                ListChuKy.Add(it);
            }
            checkedListDien.UnCheckAll();
            checkedListDien.DataSource = ListChuKy;
        }

        void LoadChuKyThanhToanNuoc()
        {
            var chuky = db.dvdnNuocs.Where(p => p.MaMB == this.MaMB & !p.DaTT.Value)
                    .Select(p => new
                    {
                        KeyValue = p.ID,
                        p.NgayNhap,
                        p.SoTieuThu,
                        p.ConNo
                    });

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();
            if (chuky.Count() <= 0)
            {
                checkedListNuoc.DataSource = null;
                return;
            }
            foreach (var item in chuky)
            {
                ChuKyItem it = new ChuKyItem()
                {
                    Display = string.Format("Tháng: {0} Tiêu thụ: {1} Thành tiền: {2}", item.NgayNhap.Value.ToString("MM/yyyy"), item.SoTieuThu, (item.ConNo ?? 0).ToString("C")),
                    Value = item.KeyValue
                };
                ListChuKy.Add(it);
            }
            checkedListNuoc.UnCheckAll();
            checkedListNuoc.DataSource = ListChuKy;
        }

        void LoadChuKyThanhToanGiuXe()
        {
        }

        void LoadChuKyThanhToanThangMay()
        {
            var chuky = db.dvtmThanhToanThangMays.Where(p => p.dvtmTheThangMay.MaMB == this.MaMB & !p.DaTT.Value)
                    .Select(p => new
                    {
                        KeyValue = p.ThanhToanID,
                        p.ThangThanhToan,
                        p.dvtmTheThangMay.PhiLamThe
                    });

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();
            if (chuky.Count() <= 0)
            {
                checkedListThangMay.DataSource = null;
                return;
            }
            foreach (var item in chuky)
            {
                ChuKyItem it = new ChuKyItem()
                {
                    Display = string.Format("Tháng: {0} Thành tiền: {1}", item.ThangThanhToan.Value.ToString("MM/yyyy"), (item.PhiLamThe ?? 0).ToString("C")),
                    Value = item.KeyValue
                };
                ListChuKy.Add(it);
            }
            checkedListThangMay.UnCheckAll();
            checkedListThangMay.DataSource = ListChuKy;
        }

        void LoadChuKyThanhToanDVK()
        {
            var chuky = db.dvkDichVuThanhToans.Where(p => !p.DaTT.Value)
                    .Select(p => new
                    {
                        KeyValue = p.ThanhToanID,
                        p.ThangThanhToan
                    });

            var ListChuKy = new System.Collections.Generic.List<ChuKyItem>();
            foreach (var item in chuky)
            {
                ChuKyItem it = new ChuKyItem()
                {
                    Display = string.Format("Tháng: {0} Thành tiền: {1}", item.ThangThanhToan.Value.ToString("MM/yyyy"), (0).ToString("C")),
                    Value = item.KeyValue
                };
                ListChuKy.Add(it);
            }
            checkedListDichVuKhac.UnCheckAll();
            checkedListDichVuKhac.DataSource = ListChuKy;
        }

        decimal tienhd = 0;
        private void checkListChuKy_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            TongTien = TongTien - tienhd;
            try
            {
                List<int> ListMaCN = new List<int>();
                foreach (ChuKyItem item in checkListChuKy.CheckedItems)
                {
                    ListMaCN.Add(item.Value);
                }

                tienhd = db.thueCongNos.Where(p => ListMaCN.Contains(p.MaCN)).ToList().Sum(p => p.ConNo) ?? 0;

                TongTien += tienhd;

                spinTongTien.EditValue = TongTien;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
            }
            catch { }
        }

        decimal tiendien = 0;
        private void checkedListDien_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            TongTien = TongTien - tiendien;
            try
            {
                List<int> ListMaCN = new List<int>();
                foreach (ChuKyItem item in checkedListDien.CheckedItems)
                {
                    ListMaCN.Add(item.Value);
                }

                tiendien = db.dvdnDiens.Where(p => ListMaCN.Contains(p.ID)).ToList().Sum(p => p.ConNo) ?? 0;

                TongTien += tiendien;

                spinTongTien.EditValue = TongTien;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
            }
            catch { }
        }

        decimal tienuoc = 0;
        private void checkedListNuoc_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            TongTien = TongTien - tienuoc;
            try
            {
                List<int> ListMaCN = new List<int>();
                foreach (ChuKyItem item in checkedListNuoc.CheckedItems)
                {
                    ListMaCN.Add(item.Value);
                }

                tienuoc = db.dvdnNuocs.Where(p => ListMaCN.Contains(p.ID)).ToList().Sum(p => p.ConNo) ?? 0;

                TongTien += tienuoc;

                spinTongTien.EditValue = TongTien;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
            }
            catch { }
        }

        decimal tiengiuxe = 0;
        private void checkedListGiuXe_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
        }

        decimal tienthangmay;
        private void checkedListThangMay_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            TongTien = TongTien - tienthangmay;
            try
            {
                List<int> ListMaCN = new List<int>();
                foreach (ChuKyItem item in checkedListThangMay.CheckedItems)
                {
                    ListMaCN.Add(item.Value);
                }

                tienthangmay = db.dvtmThanhToanThangMays.Where(p => ListMaCN.Contains(p.ThanhToanID)).ToList().Sum(p => p.dvtmTheThangMay.PhiLamThe) ?? 0;

                TongTien += tienthangmay;

                spinTongTien.EditValue = TongTien;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
            }
            catch { }
        }

        decimal tiendvk = 0;
        private void checkedListDichVuKhac_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            TongTien = TongTien - tiendvk;
            try
            {
                List<int> ListMaCN = new List<int>();
                foreach (ChuKyItem item in checkedListDichVuKhac.CheckedItems)
                {
                    ListMaCN.Add(item.Value);
                }

                tiendvk =  0;

                TongTien += tiendvk;

                spinTongTien.EditValue = TongTien;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
            }
            catch { }
        }

        decimal tienpql = 0;
        decimal tienck = 0;
        private void checkedListBoxPhiQuanLy_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {            
            TongTien = TongTien - tienpql;
            try
            {
                List<int> ListMaMB = new List<int>();
                foreach (ChuKyItem item in checkedListBoxPhiQuanLy.CheckedItems)
                {
                    ListMaMB.Add(item.Value);
                }
                tienpql =  0;
                tienpql = tienpql * checkedListBoxPhiQuanLy.CheckedItems.Count;
                TongTien += tienpql;
                                
                lookChietKhau_EditValueChanged(null, null);

                //TongTien = TongTien - tienck;

                spinTongTien.EditValue = TongTien - tienck;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien - tienck, "VNĐ");  
            }
            catch { }
        }

        decimal tiengas = 0;
        private void checkedListGas_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            
        }

        decimal phivs = 0;
        private void checkedListBoxPhiVeSinh_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            TongTien = TongTien - phivs;
            try
            {
                List<int> ListMaMB = new List<int>();
                foreach (ChuKyItem item in checkedListBoxPhiVeSinh.CheckedItems)
                {
                    ListMaMB.Add(item.Value);
                }
                phivs = 0;
                phivs = phivs * checkedListBoxPhiVeSinh.CheckedItems.Count;
                TongTien += phivs;

                spinTongTien.EditValue = TongTien;
                txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
            }
            catch { }
        }

        private void lookChietKhau_EditValueChanged(object sender, EventArgs e)
        {
            tienck = 0;
            #region tinh chiet khau
            try
            {
                int SoThangThanhToan = checkedListBoxPhiQuanLy.CheckedItems.Count;
                var obj = db.PhiQuanLy_ChietKhaus.Where(p => p.SoThangThanhToan <= SoThangThanhToan).OrderByDescending(p => p.SoThangThanhToan).FirstOrDefault();
                if (obj == null)
                {
                    lookChietKhau.EditValue = null;
                    spinSoTienDuocCK.Value = 0;
                }
                else
                {
                    lookChietKhau.EditValue = obj.ID;
                    spinSoTienDuocCK.Value = tienpql * (db.PhiQuanLy_ChietKhaus.Single(p => p.ID == (int)lookChietKhau.EditValue).TiLeChietKhau ?? 0);
                }
                
                tienck = spinSoTienDuocCK.Value;
            }
            catch { }
            #endregion
        }
    }
}