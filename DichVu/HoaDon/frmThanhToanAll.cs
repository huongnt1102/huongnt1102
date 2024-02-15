using System;
using System.Linq;
using Library;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using System.Collections.Generic;

namespace DichVu.HoaDon
{
    public partial class frmThanhToanAll : DevExpress.XtraEditors.XtraForm
    {
        readonly MasterDataContext db;
        public int MaMB;
        decimal TongTien = 0;
        string sSoPhieu;
        public tnNhanVien objnhanvien;
        TienTeCls ttcls = new TienTeCls();

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmThanhToanAll()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThanhToanAll_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            
            //Dien ten mac dinh
            try
            {
                var objmb = db.mbMatBangs.FirstOrDefault(p => p.MaMB == MaMB);
                txtMatBang.Text = objmb.MaSoMB;
                DateTime now = db.GetSystemDate();
                db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);

                #region load data len lookupedit

                #region dien
                var diends = db.dvdnDiens.Where(p => p.DaTT == false & p.MaMB == MaMB).
                    Select(p => new
                    {
                        ID = p.ID,
                        ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year)
                    }).ToList();
                if (diends.Count > 0)
                {
                    lookDienThangThanhToan.Properties.DataSource = diends;
                }
                else
                {
                    lookDienThangThanhToan.Properties.DataSource = null;
                    lookDienThangThanhToan.Properties.NullText = "Không có công nợ nào";
                    gcDien.Enabled = false;
                }
                #endregion

                #region nuoc
                var nuocds = db.dvdnNuocs.Where(p => p.DaTT == false & p.MaMB == MaMB).
                    Select(p => new
                    {
                        ID = p.ID,
                        ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year)
                    }).ToList();
                if (nuocds.Count > 0)
                {
                    lookNuocThangThanhToan.Properties.DataSource = nuocds;
                }
                else
                {
                    lookNuocThangThanhToan.Properties.DataSource = null;
                    lookNuocThangThanhToan.Properties.NullText = "Không có công nợ nào";
                    gcNuoc.Enabled = false;
                }
                #endregion

                #region  Dich vu khac
                var dichvukhacds = db.dvkDichVuThanhToans.Where(p => p.DaTT == false).
                    Select(p => new
                    {
                        ID = p.ThanhToanID,
                        ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year)
                    }).ToList();
                if (dichvukhacds.Count > 0)
                {
                    lookDVKThangThanhToan.Properties.DataSource = dichvukhacds;
                }
                else
                {
                    lookDVKThangThanhToan.Properties.DataSource = null;
                    lookDVKThangThanhToan.Properties.NullText = "Không có công nợ nào";
                    gcDvk.Enabled = false;
                }
                #endregion

                #region Hop dong thue
                var hopdongthueds = db.thueCongNos.Where(p => p.ConNo > 0 & p.thueHopDong.MaMB == MaMB & p.ChuKyMin <= now).
                    Select(p => new
                    {
                        ID = p.MaCN,
                        ThangThanhToan = string.Format("{0}/{1} - {2}/{3}", p.ChuKyMin.Value.Month, p.ChuKyMin.Value.Year, p.ChuKyMax.Value.Month, p.ChuKyMax.Value.Year)
                    }).ToList();
                if (hopdongthueds.Count > 0)
                {
                    lookHopDongThue.Properties.DataSource = hopdongthueds;
                }
                else
                {
                    lookHopDongThue.Properties.DataSource = null;
                    lookHopDongThue.Properties.NullText = "Không có công nợ nào";
                    gcHopDongThue.Enabled = false;
                }
                #endregion


                #region Thang may
                var thangmayds = db.dvtmThanhToanThangMays.Where(p => p.DaTT == false & p.dvtmTheThangMay.MaMB == MaMB)
                    .Select(p => new
                    {
                        ID = p.ThanhToanID,
                        ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                        p.dvtmTheThangMay.SoThe,
                        p.dvtmTheThangMay.ChuThe
                    }).ToList();
                if (thangmayds.Count > 0)
                {
                    lookThangMay.Properties.DataSource = thangmayds;
                }
                else
                {
                    lookThangMay.Properties.DataSource = null;
                    lookThangMay.Properties.NullText = "Không có công nợ nào";
                    gcThangMay.Enabled = false;
                }
                #endregion

                #endregion

                txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                txtDienGiai.Text = string.Format("Thanh toán phí cho mặt bằng {0}", objmb.MaSoMB);
            }
            catch { }

            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        decimal tiendien = 0;
        private void lookDienThangThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            TongTien = TongTien - tiendien;
            tiendien = db.dvdnDiens.Single(p => p.ID == (int)lookDienThangThanhToan.EditValue).TienDien ?? 0;
            txtDienThangThanhToan.Text = string.Format("{0:#,0.#} VNĐ", tiendien);
            TongTien += tiendien;
            txtTongTien.EditValue = TongTien;
            txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            #region check dieu kien thoa man
            if (ckDienThanhToan.Checked & lookDienThangThanhToan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục điện");  
                return;
            }
            if (ckNuocThanhToan.Checked & lookNuocThangThanhToan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục nước");  
                return;
            }
            if (ckhopdongthue.Checked & lookHopDongThue.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục hợp đồng thuê");  
                return;
            }
            if (ckThangMayTT.Checked & lookThangMay.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục thang máy");  
                return;
            }
            if (ckGiuXeTT.Checked & lookGiuXe.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục thẻ xe");  
                return;
            }
            if (ckDVKThanhToan.Checked & lookDVKThangThanhToan.EditValue ==null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục dịch vụ khác");  
                return;
            }
            if (ckPhiQuanLy.Checked & datePhiQuanLy.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán cho danh mục phí quản lý");  
                return;
            }
            #endregion

            #region Insert to database
            DateTime now = db.GetSystemDate();
            if (ckDienThanhToan.Checked)
            {
                db.btHopDong_getNewMaHD(ref sSoPhieu);
                dvdnDien objdien = db.dvdnDiens.Single(p => p.ID == (int)lookDienThangThanhToan.EditValue);
                objdien.NguoiNop = txtNguoiNop.Text.Trim();
                objdien.DiaChi = txtDiaChi.Text.Trim();
                objdien.DaTT = true;
                objdien.NgayThanhToan = now;
                objdien.ChuyenKhoan = ckChuyenKhoan.Checked;
                objdien.SoPhieuThu = "PTDV-" + sSoPhieu;
            }
            if (ckNuocThanhToan.Checked)
            {
                dvdnNuoc objnuoc = db.dvdnNuocs.Single(p => p.ID == (int)lookNuocThangThanhToan.EditValue);
                objnuoc.NguoiNop = txtNguoiNop.Text.Trim();
                objnuoc.DiaChi = txtDiaChi.Text.Trim();
                objnuoc.DaTT = true;
                objnuoc.NgayThanhToan = now;
                objnuoc.ChuyenKhoan = ckChuyenKhoan.Checked;
                objnuoc.SoPhieuThu = "PTDV-" + sSoPhieu;
            }
            if (ckDVKThanhToan.Checked)
            {
                dvkDichVuThanhToan objdvk = db.dvkDichVuThanhToans.Single(p => p.ThanhToanID == (int)lookDVKThangThanhToan.EditValue);
                objdvk.NguoiNop = txtNguoiNop.Text.Trim();
                objdvk.DaTT = true;
                objdvk.DiaChi = txtDiaChi.Text.Trim();
                objdvk.NgayThanhToan = now;
                objdvk.ChuyenKhoan = ckChuyenKhoan.Checked;
                objdvk.SoPhieuThu = "PTDV-" + sSoPhieu;
            }
            if (ckThangMayTT.Checked)
            {
                dvtmThanhToanThangMay objtm = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == (int)lookThangMay.EditValue);
                objtm.NguoiNop = txtNguoiNop.Text.Trim();
                objtm.DiaChi = txtDiaChi.Text.Trim();
                objtm.DaTT = true;
                objtm.NgayThanhToan = now;
                objtm.ChuyenKhoan = ckChuyenKhoan.Checked;
                objtm.SoPhieuThu = "PTDV-" + sSoPhieu;
            }
            if (ckGiuXeTT.Checked)
            {
            }
            if (ckhopdongthue.Checked)
            {
                thueCongNo objthue = db.thueCongNos.Single(p => p.MaCN == (int)lookHopDongThue.EditValue);
                objthue.DaThanhToan = objthue.ConNo;
                objthue.ConNo = 0;
                objthue.NgayThanhToan = now;
                objthue.ChuyenKhoan = ckChuyenKhoan.Checked;
                objthue.SoPhieuThu = "PTDV-" + sSoPhieu;
            }
            if (ckPhiQuanLy.Checked)
            {
                try
                {
                    decimal phinop = decimal.Parse(txtPhiQuaLy.Text);
                    int i = 0;
                    List<Library.PhiQuanLy> lstthuphi = new List<Library.PhiQuanLy>();
                    while (phinop >= tienql)
                    {
                        Library.PhiQuanLy objpqlx = new Library.PhiQuanLy()
                        {
                            ConNo = 0,
                            DaThanhToan = tienql,
                            MaMB = this.MaMB,
                            MaNV = objnhanvien.MaNV,
                            ThangThanhToan = datePhiQuanLy.DateTime.AddMonths(i),
                            NgayThanhToan = now,
                            ChuyenKhoan = ckChuyenKhoan.Checked,
                            SoPhieuThu = "PTPQL-" + sSoPhieu
                        };
                        lstthuphi.Add(objpqlx);
                        phinop = phinop - tienql;
                        i++;
                    }

                    db.PhiQuanLies.InsertAllOnSubmit(lstthuphi);
                }
                catch
                {
                    DialogBox.Error("Vui lòng nhập đúng định dạng");  
                    return;
                }
            }
            #endregion

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
                NgayThu = db.GetSystemDate(),
                SoTienThanhToan = txtTongTien.Value,
                SoPhieu = "PTDV-" + sSoPhieu,
                KeToanDaDuyet = false,
                MaMB = this.MaMB
            };

            db.PhieuThus.InsertOnSubmit(objphieuthu);
            #endregion

            try
            {
                db.SubmitChanges();
            }
            catch
            {
                DialogBox.Error("Lưu dữ liệu thất bại, vui lòng thử lại");  
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        decimal tiennuoc = 0;
        private void lookNuocThangThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            TongTien = TongTien - tiennuoc;
            tiennuoc = db.dvdnNuocs.Single(p => p.ID == (int)lookNuocThangThanhToan.EditValue).TienNuoc ?? 0;
            txtNuocThanhTien.Text = string.Format("{0:#,0.#} VNĐ", tiennuoc);
            TongTien += tiennuoc;
            txtTongTien.EditValue = TongTien;
            txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
        }
        decimal tiendvk = 0;
        private void lookDVKThangThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            TongTien = TongTien - tiendvk;
            tiendvk = 0;
            txtDVKThanhTien.Text = string.Format("{0:#,0.#} VNĐ", tiendvk);
            TongTien += tiendvk;
            txtTongTien.EditValue = TongTien;
            txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
        }
        decimal tienhd = 0;
        private void lookHopDongThue_EditValueChanged(object sender, EventArgs e)
        {
            TongTien = TongTien - tienhd;
            tienhd = db.thueCongNos.Single(p=>p.MaCN==(int)lookHopDongThue.EditValue).ConNo ?? 0;
            txtHopDongThueTT.Text = string.Format("{0:#,0.#} VNĐ", tienhd);
            TongTien += tienhd;
            txtTongTien.EditValue = TongTien;
            txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
        }
        decimal tiengiuxe = 0;
        private void lookGiuXe_EditValueChanged(object sender, EventArgs e)
        {
        }
        decimal tientm = 0;
        private void lookThangMay_EditValueChanged(object sender, EventArgs e)
        {
            TongTien = TongTien - tientm;
            tientm = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == (int)lookThangMay.EditValue).dvtmTheThangMay.PhiLamThe ?? 0;
            txtThangMayTT.Text = string.Format("{0:#,0.#} VNĐ", tientm);
            TongTien += tientm;
            txtTongTien.EditValue = TongTien;
            txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
        }
        decimal tienql = 0;
        private void datePhiQuanLy_EditValueChanged(object sender, EventArgs e)
        {
            TongTien = TongTien - tienql;
            var objhdql = db.thueHopDongs.Where(p => p.MaMB == MaMB);
            if (objhdql.Count() == 0) tienql = 0;
            else tienql = objhdql.First().PhiQL ?? 0 + 0;
            txtPhiQuaLy.Text = string.Format("{0:#,0.#} VNĐ", tienql);
            TongTien += tienql;
            txtTongTien.EditValue = TongTien;
            txtDocBangChu.Text = ttcls.DocTienBangChu(TongTien, "VNĐ");  
        }

        private void datePhiQuanLy_Properties_Popup(object sender, EventArgs e)
        {
            DateEdit edit = sender as DateEdit;
            PopupDateEditForm form = (edit as IPopupControl).PopupWindow as PopupDateEditForm;
            form.Calendar.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.YearsInfo;
        }
    }
}