using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace Deposit
{
    public partial class FrmDepositEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTn { get; set; }
        public int? MaPt { get; set; }
        public int? MaKh { get; set; }
        public List<ChiTietPhieuThuItem> ChiTiets { get; set; }
        public bool? IsDepositFather { get; set; }
        public int? DepositFatherId { get; set; }
        public int? HopDongDatCocId {get;set;}

        private MasterDataContext _db = new MasterDataContext();
        private ptPhieuThu _pt;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmDepositEdit()
        {
            InitializeComponent();
        }

        private void FrmDepositEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            TranslateLanguage.TranslateControl(this);

            lkTaiKhoanNganHang.Properties.DataSource = (from tk in _db.nhTaiKhoans
                join nh in _db.nhNganHangs on tk.MaNH equals nh.ID
                where tk.MaTN == MaTn
                select new {tk.ID, tk.SoTK, tk.ChuTK, nh.TenNH, nh.DiaChi}).ToList();
            lkNhanVien.Properties.DataSource = _db.tnNhanViens.Where(_ => _.MaTN == MaTn)
                .Select(_ => new {_.MaNV, _.MaSoNV, _.HoTenNV}).ToList();
            lkNhanVien.EditValue = Common.User.MaNV;
            
            glKhachHang.Properties.DataSource = (from kh in _db.tnKhachHangs join mb in _db.mbMatBangs on kh.MaKH equals mb.MaKH into matBang from mb in matBang.DefaultIfEmpty() where kh.MaTN == MaTn select new { MaKH = kh.MaKH, KyHieu = kh.KyHieu, TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen, DiaChi = kh.DCLL, MatBang = mb.MaSoMB }).OrderByDescending(_ => _.KyHieu).ToList();

            lkPhanLoai.Properties.DataSource = _db.ptPhanLoais.Select(_ => new {_.ID, _.TenPL}).ToList();
            lkDepTyle.Properties.DataSource = _db.Dep_Tyles;

            lkPhanLoai.EditValue = 24;
            cmbPTTT.SelectedIndex = 0;
            dateNgayThu.DateTime = DateTime.UtcNow.AddHours(7);
            glKhachHang.EditValue = MaKh;

            if (IsDepositFather == true)
            {
                gv.Columns["KhauTru"].Visible = false;
                gv.Columns["SoTien"].Visible = true;
            }
            else
            {
                gv.Columns["KhauTru"].Visible = true;
                gv.Columns["SoTien"].Visible = false;
            }

            if (MaPt != null)
            {
                _pt = _db.ptPhieuThus.Single(_ => _.ID == MaPt);
                if (_pt.NgayThu != null) dateNgayThu.DateTime = (DateTime) _pt.NgayThu;
                if (_pt.SoTien != null) spinTongTien.Value = (decimal) _pt.SoTien;
                lkPhanLoai.EditValue = _pt.MaPL;
                glKhachHang.EditValue = _pt.MaKH;
                glkMatBang.EditValue = _pt.MaMB;
                txtNguoiNop.Text = _pt.NguoiNop;
                txtDiaChiNN.Text = _pt.DiaChiNN;
                lkTaiKhoanNganHang.EditValue = _pt.MaTKNH;
                txtDienGiai.Text = _pt.LyDo;
                cmbPTTT.SelectedIndex = _pt.MaTKNH == null ? 0 : 1;
                txtChungTuGoc.Text = _pt.ChungTuGoc;
                lkNhanVien.EditValue = _pt.MaNV;
                _pt.MaNVS = Common.User.MaNV;
                _pt.NgaySua = DateTime.UtcNow.AddHours(7);
                txtSoPT.EditValue = _pt.SoPT;
                if (_pt.DepositTyleId != null) lkDepTyle.EditValue = _pt.DepositTyleId;

                HopDongDatCocId = _pt.HopDongDatCocId;
            }
            else
            {
                _pt = new ptPhieuThu();
                _pt.MaTN = MaTn;
                _pt.MaNVN = Common.User.MaNV;
                _pt.NgayNhap = DateTime.UtcNow.AddHours(7);
                _db.ptPhieuThus.InsertOnSubmit(_pt);

                if (KiemTraHopDongDatCoc()) return;

                TaoSoPt();
                if (ChiTiets != null)
                {
                    foreach (var i in ChiTiets)
                    {
                        var ct = new ptChiTietPhieuThu();
                        ct.DienGiai = i.DienGiai;
                        ct.SoTien =IsDepositFather==true? i.SoTien:0;
                        ct.KhauTru = IsDepositFather == true ?0: i.KhauTru;
                        _pt.ptChiTietPhieuThus.Add(ct);
                    }
                    if (IsDepositFather == true)
                        TinhSoTien();
                    else TinhKhauTru();
                }
            }

            gc.DataSource = _pt.ptChiTietPhieuThus;

            itemHuongDan.Click += ItemHuongDan_Click;
            itemClearText.Click += ItemClearText_Click;
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private bool KiemTraHopDongDatCoc()
        {
            return HopDongDatCocId == null;
        }

        private void TaoSoPt()
        {
            if (MaTn == null)
            {
                txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, 0, 0, false);
                return;
            }
            if (glkMatBang.EditValue == null)
            {
                txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, 0, MaTn.Value, false);
                return;
            }
            var temp = 0;
            if (!int.TryParse(glkMatBang.EditValue.ToString(), out temp))
            { 
                txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, 0, MaTn.Value, false);
                return;
            }
            var objMb = _db.mbMatBangs.FirstOrDefault(p => p.MaMB == Convert.ToInt32(glkMatBang.EditValue));
            if (objMb == null) txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month,
                    dateNgayThu.DateTime.Year, 0, MaTn.Value, false);
            if (objMb.mbTangLau.MaKN != null)
                txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month,
                    dateNgayThu.DateTime.Year, objMb.mbTangLau.MaKN.Value, MaTn.Value, false);
        }

        private void TinhSoTien()
        {
            try
            {
                spinTongTien.EditValue = IsDepositFather == true ? _pt.ptChiTietPhieuThus.Sum(o => o.SoTien) : _pt.ptChiTietPhieuThus.Sum(o => o.KhauTru);
                

                var strDienGiai = "";
                foreach (var i in _pt.ptChiTietPhieuThus)
                {
                    strDienGiai += "; " + i.DienGiai + string.Format(" ({0:#,0.##}đ)", i.SoTien);
                }

                strDienGiai = strDienGiai.Trim().Trim(';');
                txtDienGiai.Text = strDienGiai;
            }
            catch { }
        }
        private void TinhKhauTru()
        {
            try
            {
                spinTongTien.EditValue = _pt.ptChiTietPhieuThus.Sum(_ => _.KhauTru);

                var strDienGiai = "";
                foreach (var i in _pt.ptChiTietPhieuThus)
                    strDienGiai += "; " + i.DienGiai + string.Format(" ({0:#,0.##}đ)", i.SoTien);
                strDienGiai = strDienGiai.Trim().Trim(';');
                txtDienGiai.Text = strDienGiai;
            }
            catch { }
        }

        private void GlKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNop.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChiNN.Text = (from mb in _db.mbMatBangs
                                        join tn in _db.tnToaNhas on mb.MaTN equals tn.MaTN
                                        where mb.MaKH == (int)glKhachHang.EditValue
                                        select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();
                    glkMatBang.Properties.DataSource = (from mb in _db.mbMatBangs
                                                        join tl in _db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                        join kn in _db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                        join kh in _db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                        where mb.MaTN == MaTn & mb.MaKH == (int)glKhachHang.EditValue
                                                        orderby mb.MaSoMB descending
                                                        select new
                                                        {
                                                            mb.MaMB,
                                                            mb.MaSoMB,
                                                            tl.TenTL,
                                                            kn.TenKN,
                                                            kh.MaKH,
                                                            TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen
                                                        }).ToList();
                    glkMatBang.EditValue = glkMatBang.Properties.GetKeyValue(0);
                }
            }
            catch { }
        }

        private void ItemCancel_Click(object sender, EventArgs e)
        {
            _db.Dispose();
            Close();
        }

        private void CmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            lkTaiKhoanNganHang.Enabled = cmbPTTT.SelectedIndex == 1;
            try
            {
                TaoSoPt();
            }
            catch { }
        }

        private void LkTaiKhoanNganHang_EditValueChanged(object sender, EventArgs e)
        {
            if (lkTaiKhoanNganHang.EditValue != null)
                txtTenNH.EditValue = _db.nhTaiKhoans.Single(k => k.ID == (int)lkTaiKhoanNganHang.EditValue).nhNganHang.TenNH;
            else
                txtTenNH.EditValue = null;
        }

        private void Gv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if ((gv.GetRowCellValue(e.RowHandle, "DienGiai") ?? "").ToString() == "")
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập diễn giải!";
                return;
            }
            var value = (decimal?)gv.GetFocusedRowCellValue("SoTien") ?? 0;
            //if (value <= 0)
            //{
            //    e.Valid = false;
            //    e.ErrorText = "Vui lòng nhập số tiền!";
            //    return;
            //}
        }

        private void Gv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    gv.DeleteSelectedRows();
                    this.TinhSoTien();
                }
            }
        }

        private void FrmDepositEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_db != null)
                _db.Dispose();
        }

        private void gv_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                TinhSoTien();
            }
            catch { }
        }

        private void itemSave_Click(object sender, EventArgs e)
        {
            SavePhieuThu(false, 0);
        }
        private void SavePhieuThu(bool _IsPrint, int _Lien)
        {
            gv.UpdateCurrentRow();

            #region Rang buoc
            if (cmbPTTT.SelectedIndex < 0)
            {
                DialogBox.Error("Vui lòng chọn phương thức thanh toán");
                cmbPTTT.Focus();
                return;
            }
            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                return;
            }

            if (cmbPTTT.SelectedIndex == 1 && lkTaiKhoanNganHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                return;
            }

            if (txtSoPT.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPT.Focus();
                return;
            }

            if (lkDepTyle.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại đặt cọc");
                return;
            }
            else
            {
                //Trường hợp thêm
                if (MaPt == null)
                {
                    var objKtpt = _db.ptPhieuThus.FirstOrDefault(p => p.MaTN == MaTn & p.SoPT.Equals(txtSoPT.Text.Trim()));
                    if (objKtpt != null)
                    {
                        TaoSoPt();
                    }
                }
                else
                {
                    //Trường hợp sửa
                    var count = _db.ptPhieuThus.Count(p => p.MaTN == _pt.MaTN & p.SoPT == txtSoPT.Text & p.ID != MaPt);
                    if (count > 0)
                    {
                        TaoSoPt();
                    }

                }

            }

            if (dateNgayThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thu");
                dateNgayThu.Focus();
                return;
            }
            if (MaPt == null)
            {
                if (spinTongTien.Value <= 0)
                {
                    DialogBox.Error("Vui lòng nhập số tiền");
                    spinTongTien.Focus();
                    return;
                }
            }

            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }

            if (lkPhanLoai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phân loại phiếu thu");
                lkPhanLoai.Focus();
                return;
            }
            #endregion
            if (MaPt != null)
            {
                _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == MaPt && p.IsPhieuThu == true));
            }
            #region
            //thông tin chứng từ
            _pt.SoPT = txtSoPT.Text;
            _pt.NgayThu = (DateTime)dateNgayThu.EditValue;
            _pt.SoTien = (decimal)spinTongTien.EditValue;
            _pt.MaNV = (int)lkNhanVien.EditValue;
            _pt.MaPL = (int?)lkPhanLoai.EditValue;
            //thong tin chung
            _pt.MaKH = (int)glKhachHang.EditValue;
            _pt.MaMB = (int?)glkMatBang.EditValue;
            _pt.NguoiNop = txtNguoiNop.EditValue + "";
            _pt.DiaChiNN = txtDiaChiNN.EditValue + "";
            if (lkTaiKhoanNganHang.EditValue != null)
                _pt.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
            else
                _pt.MaTKNH = null;
            _pt.ChungTuGoc = txtChungTuGoc.Text;
            _pt.LyDo = txtDienGiai.EditValue + "";
            _pt.NgayNhap = DateTime.UtcNow.AddHours(7);
            _pt.IsKhauTru = false;
            _pt.DepositTyleId = (int?)lkDepTyle.EditValue;
            _pt.DepositTyleName = lkDepTyle.Properties.GetDisplayText(lkDepTyle.EditValue);
            _pt.IsDepositFather = IsDepositFather; // nếu phiếu này là phiếu tổng thì  = true
            _pt.DepositFatherId = DepositFatherId; // nếu phiếu này là phiếu con, thì mới thêm, còn phiếu tổng thì = null
            //_pt.TotalReceipts; tổng số tiền khấu trừ cho các khoản abc..
            //_pt.TotalPay; tổng số tiền trả lại cho khách hàng, bao nhiêu %...
            _db.SubmitChanges();
            decimal sumTotalChild = 0;
            foreach (var hd in _pt.ptChiTietPhieuThus)
            {
                sumTotalChild = sumTotalChild + (IsDepositFather == true? hd.SoTien.GetValueOrDefault():hd.KhauTru.GetValueOrDefault());
            }
            #endregion

            #region Cập nhật phiếu chi và phiếu thu tổng nếu đây là phiếu thu con

            decimal totalReceipts = 0;

            if (IsDepositFather == false)
            {
                _pt.SoTien = 0;
                _pt.IsKhauTru = true;
                _pt.TotalReceipts = (decimal) spinTongTien.EditValue;

                var pc = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(_ => _.PtPhatId == _pt.ID);
                if (pc != null)
                {
                    pc.SoTienPhat = sumTotalChild;
                    var ptFather = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == pc.MaPT);
                    if (ptFather != null)
                    {
                        var listPc = _db.pcPhieuChi_TraLaiKhachHangs.Where(_ => _.ID != pc.ID & _.MaPT == ptFather.ID).ToList();
                        ptFather.TotalReceipts = sumTotalChild +  (listPc.Count>0? listPc.Sum(_ => _.SoTienPhat.GetValueOrDefault()):0);
                        totalReceipts = listPc.Sum(_ => _.SoTienPhat.GetValueOrDefault());
                    }
                    _db.SubmitChanges();
                }
            }
            
            #endregion

            foreach (var hd in _pt.ptChiTietPhieuThus)
            {
                int? iPL = (int?)lkPhanLoai.EditValue;
                Common.SoQuy_Insert(_db, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, this.MaTn,
                    (int)glKhachHang.EditValue, (int?)_pt.MaMB, hd.MaPT, hd.ID, dateNgayThu.DateTime, txtSoPT.Text,
                    cmbPTTT.SelectedIndex, (int?)lkPhanLoai.EditValue, true, hd.PhaiThu.GetValueOrDefault(),
                    hd.SoTien.GetValueOrDefault(),
                    iPL == 2 ? hd.SoTien.GetValueOrDefault() : hd.ThuThua.GetValueOrDefault(),
                    hd.KhauTru.GetValueOrDefault(), null, "ptChiTietPhieuThu", hd.DienGiai, Common.User.MaNV,
                    _pt.IsKhauTru.GetValueOrDefault(),false);
            }

            #region cập nhật hợp đồng

            var hopDong = GetHopDong();
            if (hopDong != null)
            {
                _pt.HopDongDatCocId = hopDong.Id;
                _db.SubmitChanges();
                hopDong = UpdateHopDong(hopDong);
                _db.SubmitChanges();
            }

            #endregion

            try
            {
                if (_IsPrint)
                {
                    if (_Lien == 1)
                    {
                        var rpt = new LandSoftBuilding.Fund.Input.rptPhieuThu(_pt.ID, MaTn.Value, 1);
                        for (int i = 1; i <= 3; i++)
                        {
                            var rpt1 = new LandSoftBuilding.Fund.Input.rptPhieuThu(_pt.ID, MaTn.Value, i);
                            rpt1.CreateDocument();
                            rpt.Pages.AddRange(rpt1.Pages);
                        }
                        rpt.PrintingSystem.ContinuousPageNumbering = true;
                        rpt.ShowPreviewDialog();
                    }
                    else
                    {
                        var rpt = new LandSoftBuilding.Fund.Input.rptDetail(_pt.ID, MaTn.Value);
                        rpt.ShowPreviewDialog();
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private Library.Dep_HopDong GetHopDong()
        {
            return _db.Dep_HopDongs.FirstOrDefault(_ => _.Id == HopDongDatCocId);
        }

        private Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong)
        {
            var listPhieuChi = _db.ptPhieuThus.Where(p => p.HopDongDatCocId == hopDong.Id & p.IsDepositFather == true).ToList();
            hopDong.ThuPhat = listPhieuChi.Sum(_ => _.TotalReceipts).GetValueOrDefault();
            hopDong.TienTra = listPhieuChi.Sum(_ => _.TotalPay).GetValueOrDefault();
            hopDong.TongTien = listPhieuChi.Sum(_ => _.SoTien).GetValueOrDefault();

            return hopDong;
        }

        //private Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong, decimal totalReceipts)
        //{
        //    //hopDong.TongTien = (hopDong.TongTien ?? 0) + (decimal) spinTongTien.EditValue;
        //    //hopDong.ThuPhat = (hopDong.ThuPhat ?? 0) + (decimal) totalReceipts;

        //    hopDong = Deposit.Class.HopDong.UpdateHopDong(hopDong, Deposit.Class.Enum.TienDatCoc.TONG_TIEN, (decimal)spinTongTien.EditValue);
        //    hopDong = Deposit.Class.HopDong.UpdateHopDong(hopDong, Deposit.Class.Enum.TienDatCoc.DA_THU, totalReceipts);
        //    return hopDong;
        //}

        private void itemLuu_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SavePhieuThu(true, 1);
        }

        private void itemSave_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SavePhieuThu(true, 2);
        }

        private void itemSaveAndPrint_Click(object sender, EventArgs e)
        {
            SavePhieuThu(true, 1);
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            TaoSoPt();
        }
    }

    public class ChiTietPhieuThuItem
    {
        public string TableName { get; set; }
        public long? LinkId { get; set; }
        public string DienGiai { get; set; }
        public decimal? SoTien { get; set; }
        public decimal? KhauTru { get; set; }
        public bool IsThuThua { get; set; }
    }
}