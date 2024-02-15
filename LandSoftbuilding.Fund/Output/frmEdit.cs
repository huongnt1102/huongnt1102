using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using Library;
using BuildingDesignTemplate;
using DevExpress.XtraRichEdit.API.Native;

namespace LandSoftBuilding.Fund.Output
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }
        public int? Id_PDC { get; set; }
        public bool IsChiCocGiuCho = false;

        public Guid? ThiCongId { set; get; }
        public bool? IsDatCocThiCong { get; set; }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }
        public int? MaKH { get; set; }
        public int? PhanLoaiChi { set; get; }
        public int? MaTL { get; set; }
        public string TableName { get; set; }
        public List<ChiTietPhieuChiItem> ChiTiets { get; set; }
        public decimal? SoTien { get; set; }
        public string DienGiai { get; set; }

        pcPhieuChi _objPc;
        MasterDataContext db = new MasterDataContext();


        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            gvChiTiet.InvalidRowException += Common.InvalidRowException;

            #region " Show  LookupItem"
            lkTaiKhoanNganHang.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                        where tk.MaTN == this.MaTN
                                                        select new
                                                        {
                                                            tk.ID,
                                                            tk.SoTK,
                                                            tk.ChuTK,
                                                            nh.TenNH,
                                                            nh.DiaChi
                                                        }).ToList();
            lkLoaiChi.Properties.DataSource = db.pcPhanLoais;
            lkLoaiChi.EditValue = 8;
            if (PhanLoaiChi == 10) lkLoaiChi.EditValue = PhanLoaiChi;
            lkPLchiTiet.DataSource = db.LoaiChi_ChiTiets;
            lkNVNhan.Properties.DataSource = db.tnNhanViens.Where(p=>p.MaTN==this.MaTN);
            lkNhanVien.Properties.DataSource = (from n in db.tnNhanViens where n.MaTN == this.MaTN select new { n.MaNV, n.MaSoNV, n.HoTenNV }).ToList();
            glDoiTuong.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                     DiaChi = kh.DCLL
                                                 }).ToList();

            // loại phiếu chi
            var lOutputTyle = new List<OutputTyle>();
            lOutputTyle.Add(new OutputTyle {Id = 1, Name = "Chi cho Nhà cung cấp"});
            lOutputTyle.Add(new OutputTyle {Id = 2, Name = "Chi nội bộ"});
            lOutputTyle.Add(new OutputTyle {Id = 3, Name = "Chi cho Khách hàng"});
            lkLoaiPhieuChi.Properties.DataSource = lOutputTyle;
            lkLoaiPhieuChi.EditValue = 1;

            if (IsChiCocGiuCho)
                lkLoaiPhieuChi.EditValue = 3;

            #endregion

            #region "   Show Thong tin"
            if (this.ID != null)
            {
                _objPc = db.pcPhieuChis.Single(pt => pt.ID == ID);
                //thông tin chứng từ
                txtSoPC.EditValue = _objPc.SoPC;
                dateNgayChi.EditValue = _objPc.NgayChi;
                spSoTien.EditValue = _objPc.SoTien;
                lkNhanVien.EditValue = Common.User.MaNV;
                //thong tin chung
                glDoiTuong.EditValue = _objPc.MaNCC;
                txtNguoiNhan.EditValue = _objPc.NguoiNhan;
                txtDiaChiNN.EditValue = _objPc.DiaChiNN;
                lkTaiKhoanNganHang.EditValue = _objPc.MaTKNH;
                txtDienGiai.EditValue = _objPc.LyDo;
                cmbPTTT.SelectedIndex = _objPc.MaTKNH == null ? 0 : 1;
                txtChungTuGoc.Text = _objPc.ChungTuGoc;
                lkNhanVien.EditValue = _objPc.MaNV;
                lkNVNhan.EditValue = _objPc.MaNVNhan;
                lkLoaiChi.EditValue = _objPc.MaPhanLoai;
                _objPc.MaNVS = Library.Common.User.MaNV;
                _objPc.NgaySua = db.GetSystemDate();
                lkLoaiPhieuChi.EditValue = _objPc.OutputTyleId ?? (_objPc.MaPhanLoai == 9 ? 2 : (_objPc.MaPhanLoai == 0 ? 1 : 3));
            }
            else
            {
                _objPc = new pcPhieuChi();
                _objPc.MaTN = this.MaTN;
                _objPc.TableName = this.TableName;
                _objPc.MaNVN = Library.Common.User.MaNV;
                _objPc.NgayNhap = DateTime.UtcNow.AddHours(7);
                db.pcPhieuChis.InsertOnSubmit(_objPc);

                cmbPTTT.SelectedIndex = 0;

                dateNgayChi.EditValue = DateTime.UtcNow.AddHours(7);
                TaoSoCTPhieuChi();
                lkNhanVien.EditValue = Library.Common.User.MaNV;
                glDoiTuong.EditValue = this.MaKH;

                if (this.ChiTiets != null)
                {
                    foreach (var i in this.ChiTiets)
                    {
                        var ct = new pcChiTiet();
                        ct.LinkID = i.LinkID;
                        ct.DienGiai = i.DienGiai;
                        ct.SoTien = i.SoTien;
                        _objPc.pcChiTiets.Add(ct);
                    }

                    this.TinhSoTien();
                }
            }

            gcChiTiet.DataSource = _objPc.pcChiTiets;
            #endregion

            if (IsChiCocGiuCho)
            {
                gvChiTiet.AddNewRow();
                gvChiTiet.SetFocusedRowCellValue("SoTien", SoTien);
                gvChiTiet.SetFocusedRowCellValue("DienGiai", "Phiếu chi đặt cọc giữ chỗ");
                gvChiTiet.FocusedRowHandle = -1;
                lkLoaiPhieuChi.ReadOnly = true;
            }

            if ((bool)IsDatCocThiCong.GetValueOrDefault())
            {
                gvChiTiet.AddNewRow();
                lkLoaiChi.EditValue = 10;
                gvChiTiet.SetFocusedRowCellValue("SoTien", SoTien);
                gvChiTiet.SetFocusedRowCellValue("DienGiai", "Phiếu chi hoàn tiền đặt cọc thi công.");
                gvChiTiet.FocusedRowHandle = -1;
                lkLoaiPhieuChi.ReadOnly = true;
            }

            this.Load_MauPhieuChi();
        }

        void TinhSoTien()
        {
            spSoTien.EditValue = _objPc.pcChiTiets.Sum(o => o.SoTien);
        }

        void SavePhieuChi(bool _IsPrint, int _ReportID)
        {
            gvChiTiet.UpdateCurrentRow();
            #region Rang buoc

            if (lkLoaiPhieuChi.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại phiếu chi");
                lkLoaiPhieuChi.Focus();
                return;
            }
            //if (glDoiTuong.EditValue == null & (int?)lkLoaiPhieuChi.EditValue != 2 && (int?)lkLoaiChi.EditValue != 1)
            //{
            //    DialogBox.Error("Vui lòng chọn khách hàng");
            //    return;
            //}
            if ((int)lkLoaiChi.EditValue == 2 & lkNVNhan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên");
                return;
            }

            if (cmbPTTT.SelectedIndex < 0)
            {
                DialogBox.Error("Vui lòng chọn phương thức thanh toán");
                cmbPTTT.Focus();
                return;
            }
            //if (glDoiTuong.EditValue == null & (int?)lkLoaiChi.EditValue != 9 && (int?)lkLoaiChi.EditValue!=0)
            //{
            //    DialogBox.Error("Vui lòng chọn khách hàng");
            //    return;
            //}
            if ((int)lkLoaiChi.EditValue == 9 & lkNVNhan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên");
                return;
            }
            if (lkLoaiChi.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại chi");
                return;
            }
            if (cmbPTTT.SelectedIndex == 1 && lkTaiKhoanNganHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                return;
            }

            if (txtSoPC.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPC.Focus();
                return;
            }
            else
            {
                var count = db.pcPhieuChis.Where(p => p.MaTN == _objPc.MaTN & p.SoPC == txtSoPC.Text & p.ID != _objPc.ID).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng số phiếu thu. Vui lòng kiểm tra lại");
                    txtSoPC.Focus();
                    return;
                }
            }

            if (dateNgayChi.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thu");
                dateNgayChi.Focus();
                return;
            }

            if (spSoTien.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền");
                spSoTien.Focus();
                return;
            }

            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }
            #endregion
            if (ID != null)
            {
                db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == ID && p.IsPhieuThu == false));
            }

            try
            {
                #region "Set thong tin"
                //thông tin chứng từ
                _objPc.SoPC = txtSoPC.Text;
                _objPc.NgayChi = (DateTime)dateNgayChi.EditValue;
                _objPc.SoTien = (decimal)spSoTien.EditValue;
                _objPc.MaNV = (int)lkNhanVien.EditValue;
                //thong tin chung
                _objPc.MaNCC = (int?)glDoiTuong.EditValue;
                _objPc.NguoiNhan = txtNguoiNhan.EditValue + "";
                _objPc.DiaChiNN = txtDiaChiNN.EditValue + "";
                _objPc.MaNVNhan = (int?)lkNVNhan.EditValue;
                _objPc.MaPhanLoai = (int?)lkLoaiChi.EditValue;
                if (lkTaiKhoanNganHang.EditValue != null)
                    _objPc.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
                else
                    _objPc.MaTKNH = null;
                _objPc.ChungTuGoc = txtChungTuGoc.Text;
                _objPc.LyDo = txtDienGiai.EditValue + "";
                _objPc.OutputTyleId = (int?)lkLoaiPhieuChi.EditValue;
                _objPc.OutputTyleName = lkLoaiPhieuChi.Properties.GetDisplayText(lkLoaiPhieuChi.EditValue);
                if (ThiCongId != null)
                {
                    _objPc.ThiCongId = ThiCongId;
                }
                //Cập nhật thanh lý:
                var objTL = (from tl in db.ctThanhLies
                             where tl.ID == this.MaTL
                             select tl).FirstOrDefault();
                if (objTL != null)
                {
                    objTL.DaTra = spSoTien.Value;
                    objTL.PhaiTra = objTL.PhaiTra.GetValueOrDefault() - (decimal?)spSoTien.Value;
                    db.SubmitChanges();
                }

                foreach (var hd in _objPc.pcChiTiets)
                {
                    bool isM = false;
                    isM = (int?)lkLoaiPhieuChi.EditValue == (int?)3 ? true : false;
                    //Kiểm tra chi cho nhân viên hay cho khách hàng
                    bool IsNV = false;
                    IsNV = (int?)lkLoaiPhieuChi.EditValue == 2 ? true : false;
                    //Common.SoQuy_Insert(db, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year, this.MaTN, isM == true ? (int)glDoiTuong.EditValue : (IsNV == true ? (int?)lkNVNhan.EditValue : 0), (int?)null, hd.MaPC, hd.ID, dateNgayChi.DateTime, txtSoPC.Text, cmbPTTT.SelectedIndex, (int?)lkLoaiChi.EditValue, false, 0, hd.SoTien, 0, 0, hd.LinkID, IsNV == true ? "NV" : "KH", hd.DienGiai, Common.User.MaNV, false);
                    Common.SoQuy_Insert(db, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year, this.MaTN, _objPc.OutputTyleId == 3 ? (int?)glDoiTuong.EditValue : (_objPc.OutputTyleId == 2 ? (int?)lkNVNhan.EditValue : 0), (int?)null, hd.MaPC, hd.ID, dateNgayChi.DateTime, txtSoPC.Text, cmbPTTT.SelectedIndex, (int?)lkLoaiChi.EditValue, false, 0, hd.SoTien, 0, 0, hd.LinkID, _objPc.OutputTyleId == 3 ? "KH" : (_objPc.OutputTyleId == 2 ? "NV" : "NCC"), hd.DienGiai, Common.User.MaNV, false, false);
                }
                #endregion

                #region Cập nhật thi công
                // Cập nhật lại tổng số tiền đặt cọc thi công
                // Cập nhật phiếu thu

                if (ThiCongId != null)
                {
                    Library.Class.Connect.QueryConnect.QueryData<bool>("dkThiCong_Update_PhieuChi", new
                    {
                        ThiCongId = ThiCongId,
                        PhieuChiId = _objPc.ID,
                        PhanLoaiId = 10
                    });
                }

                #endregion

                SoTien = (decimal)spSoTien.EditValue;
                ID = _objPc.ID;
                MaKH = (int?)glDoiTuong.EditValue;
                DienGiai = _objPc.LyDo;

                //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm phiếu thu hóa đơn dịch vụ", "Lưu", "Số phiếu: " + txtSoPC.Text + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == this.MaTN).TenTN);
                if (_IsPrint)
                {
                    DevExpress.XtraReports.UI.XtraReport rpt = null;
                    //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In phiếu chi", "In phiếu", "Số phiếu: " + txtSoPC.Text + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == this.MaTN).TenTN);

                    var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)_ReportID);
                    if (objForm != null)
                    {
                        var rtfText = BuildingDesignTemplate.Class.MergeField.PhieuChi(_objPc.ID, objForm.Content);
                        var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
                        frm.ShowDialog(this);
                    }
                }

                using (var dbo = new MasterDataContext())
                {
                    if (IsChiCocGiuCho)
                    {
                        var objPDC = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Id_PDC);
                        if (objPDC != null)
                        {
                            objPDC.MaPC = ID;
                            objPDC.MaTT = 6; // đã đặt cọc

                            foreach (var ob in objPDC.PhieuDatCoc_GiuCho_ChiTiets)
                            {
                                var objMB = dbo.mbMatBangs.FirstOrDefault(p => p.MaMB == ob.MaMB);
                                if (objMB != null)
                                {
                                    objMB.MaTT = 84; // trống
                                }
                                dbo.SubmitChanges();
                            }
                        }
                    }
                    
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
                return;
            }
            finally
            {
                this.Close();
            }
        }
        void TaoSoCTPhieuChi()
        {
            txtSoPC.Text = Common.CreatePhieuChi(this.MaTN.Value, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year);
        }

        public class OutputTyle
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            db.Dispose();
            this.Close();
        }

        private void cmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            lkTaiKhoanNganHang.Enabled = cmbPTTT.SelectedIndex == 1;
            TaoSoCTPhieuChi();
        }

        private void lkTaiKhoanNganHang_EditValueChanged(object sender, EventArgs e)
        {
            if (lkTaiKhoanNganHang.EditValue != null)
                txtTenNH.EditValue = db.nhTaiKhoans.Single(k => k.ID == (int)lkTaiKhoanNganHang.EditValue).nhNganHang.TenNH;
            else
                txtTenNH.EditValue = null;
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (db != null)
                db.Dispose();
        }

        private void gvChiTiet_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                this.TinhSoTien();

                var strDienGiai = "";
                foreach (var i in _objPc.pcChiTiets)
                {
                    strDienGiai += "; " + i.DienGiai;
                }

                strDienGiai = strDienGiai.Trim().Trim(';');
                txtDienGiai.Text = strDienGiai;
            }
            catch { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.SavePhieuChi(false, 0);
        }

        private void glDoiTuong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glDoiTuong.Properties.GetRowByKeyValue(glDoiTuong.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNhan.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChiNN.Text = type.GetProperty("DiaChi").GetValue(r, null) as string;
                }
            }
            catch { }
        }

        private void dateNgayChi_EditValueChanged(object sender, EventArgs e)
        {
            TaoSoCTPhieuChi();
        }

        private void lkNVNhan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r =lkNVNhan.GetColumnValue("HoTenNV");
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNhan.Text = r.ToString();
                }
            }
            catch { }
        }

        private void LkLoaiPhieuChi_EditValueChanged(object sender, EventArgs e)
        {
            // 1: Chi cho khách hàng
            // 2. Chi cho nội bộ
            // 3. Chi cho nhà cung cấp
            if ((int?)lkLoaiPhieuChi.EditValue == 2) // nhân viên // lkloaichi =9
            {
                laynvn.Visibility = LayoutVisibility.Always;
                laykh.Visibility = LayoutVisibility.Never;
                layoutControlTraM.Visibility = LayoutVisibility.Never;
            }
            else
            {
                if ((int?) lkLoaiPhieuChi.EditValue == 1) // nhà cung cấp // loai chi  = 0
                {
                    laynvn.Visibility = LayoutVisibility.Never;
                    laykh.Visibility = LayoutVisibility.Never;
                    layoutControlTraM.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    laynvn.Visibility = LayoutVisibility.Never;
                    laykh.Visibility = LayoutVisibility.Always;
                    layoutControlTraM.Visibility = LayoutVisibility.Never;
                    lkNVNhan.EditValue = null;
                }
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            this.SavePhieuChi(true, (int)btnLuuIn.Tag);
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SavePhieuChi(true, (int)e.Item.Tag);
        }

        void Load_MauPhieuChi()
        {
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == this.MaTN & rp.GroupID == 6
                                orderby rp.Rank
                                select new { rp.ID, rp.Name, tn.IsDefault }).ToList();

                popupMenu1.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                    popupMenu1.ItemLinks.Add(itemPrint);
                }

                var objRP = ltReport.FirstOrDefault(p => p.IsDefault == true);
                if (objRP == null)
                {
                    objRP = ltReport.First();
                }
                btnLuuIn.Tag = objRP.ID;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }

    public class ChiTietPhieuChiItem
    {
        public long? LinkID { get; set; }
        public string DienGiai { get; set; }
        public decimal? SoTien { get; set; }
        public string TableName { get; set; }
    }
}