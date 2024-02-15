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
using DevExpress.XtraGrid.Views.Base;
using System.Data.Linq.SqlClient;

namespace DichVu.GiuXe
{
    public partial class frmTheXeEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmTheXeEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }
        public string BlockCode { get; set; }
        MasterDataContext db;
        dvgxTheXe objTheXe;
        private List<APITheXe.TheChuaTaoVeThang> _l;

        public int? idTheXeCuDan { get; set; }
        public dvgxTheXeCuDan objTheCuDan { get; set; }
        public bool? IsTheCuDan { get; set; }

        public int? IDTX { get; set; }

        public int? MaKH { get; set; }

        void TinhThanhTien()
        {
            spTienVAT.Value = spThanhTien.Value * spVAT.Value;
            if (spKyTT.Value == 0)
                spTienTT.EditValue = spThanhTien.Value * (1 + spVAT.Value);
            else
            {
                spTienTT.EditValue = (spThanhTien.Value * (1 + spVAT.Value));
            }
        }

        void SetDonGia()
        {
            try
            {
                var _MaLX = (int?)lkLoaiXe.EditValue;
                var _MaMB = (int?)glkMatBang.EditValue;

                var objCT = new CachTinhCls();
                objCT.MaTN = this.MaTN.Value;
                objCT.MaMB = _MaMB;
                objCT.MaLX = _MaLX;
                objCT.LoadDinhMuc();

                int _SoLuong = (from tx in db.dvgxTheXes where tx.MaTN == this.MaTN & tx.MaMB == _MaMB & tx.MaLX == _MaLX select tx).Count();

                //if (this.ID == null)
                //{
                    _SoLuong++;
                //}

                var objGia = (from bg in objCT.ltBangGia
                              where bg.SoLuong <= _SoLuong
                              orderby bg.SoLuong descending
                              select bg).FirstOrDefault();
                if(objGia != null)
                {
                    objTheXe.MaDM = objGia.MaDM;
                    spGiaThang.EditValue = objGia.DonGiaThang;
                }
                else
                {
                    objTheXe.MaDM = null;
                    spGiaThang.EditValue = 0;
                }
            }
            catch { }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            db = new MasterDataContext();
            var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == this.MaTN);
            lkLoaiXe.Properties.DataSource = (from l in db.dvgxLoaiXes
                                              where l.MaTN == this.MaTN
                                              orderby l.STT
                                              select new { l.MaLX, l.TenLX })
                                              .ToList();
            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT into trangthai from tt in trangthai.DefaultIfEmpty()
                                                where kn.MaTN == this.MaTN
                                                orderby mb.MaSoMB descending
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    kh.MaKH,
                                                    kh.KyHieu,
                                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                    tt.TenTT,
                                                }).ToList();
            
            if (this.ID != null)
            {
                objTheXe = db.dvgxTheXes.Single(p => p.ID == this.ID);
                glkMatBang.EditValue = objTheXe.MaMB;
                txtSoThe.Text = objTheXe.SoThe;
                dateNgayDK.EditValue = objTheXe.NgayDK;
                lkLoaiXe.EditValue = objTheXe.MaLX;
                spGiaThang.EditValue = objTheXe.GiaThang;
                lkChuThe.EditValue = objTheXe.MaNK;
                txtTenChuThe.EditValue = objTheXe.ChuThe;
                txtBienSo.EditValue = objTheXe.BienSo;
                txtMauXe.EditValue = objTheXe.MauXe;
                txtDoiXe.EditValue = objTheXe.DoiXe;
                dateNgayTT.EditValue = objTheXe.NgayTT;
                spKyTT.EditValue = objTheXe.KyTT;
                spTienTT.EditValue = objTheXe.TienTT;
                txtDienGiai.EditValue = objTheXe.DienGiai;
                ckbNgungSuDung.EditValue = objTheXe.NgungSuDung;
                dateNgungSD.EditValue = objTheXe.NgayNgungSD;
                dateNgungSD.Enabled = ckbNgungSuDung.Checked;
                spThanhTien.EditValue = objTheXe.TienTruocThue;
                spVAT.EditValue = objTheXe.ThueGTGT;
                spTienVAT.EditValue = objTheXe.TienThueGTGT;
                txtSoTheChip.Text = objTheXe.MaTheChip;
                dateTuNgay.EditValue = objTheXe.TuNgay;
                dateDenNgay.EditValue = objTheXe.DenNgay;
                spinSoTienCoc.EditValue = objTheXe.SoTienCoc;
            }
            else
            {
                objTheXe = new dvgxTheXe();
                objTheXe.IsTheXe = true;
                dateNgayDK.EditValue =Common.GetDateTimeSystem();
            }
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }
        string GetBlockCode(int? MaMB)
        {
            string sblockcode = "";
            var obj = (from mb in db.mbMatBangs
                       join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                       join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                       where mb.MaMB == MaMB
                       select new { kn.BlockCode }).FirstOrDefault();
            if (obj != null)
                sblockcode = obj.BlockCode;
            return sblockcode;
        }
        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    var _MaMB = (int)glkMatBang.EditValue;

                    objTheXe.MaMB = (int?)glkMatBang.EditValue;
                    objTheXe.MaKH = (int?)type.GetProperty("MaKH").GetValue(r, null);
                    txtTrangThaiMB.EditValue = type.GetProperty("TenTT").GetValue(r, null);
                    #region Load cu dan
                    lkChuThe.Properties.DataSource = from nk in db.tnNhanKhaus
                                                     where nk.MaKH == objTheXe.MaKH
                                                     select new { ID = nk.ID, TenChuThe = nk.HoTenNK };
                    lkChuThe.EditValue = null;
                    #endregion
                    BlockCode = GetBlockCode((int?)glkMatBang.EditValue);
                    this.SetDonGia();
                }
            }
            catch { }
        }

        private void lkNhanKhau_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtTenChuThe.EditValue = (sender as LookUpEdit).GetColumnValue("TenChuThe").ToString();
            }
            catch { }
        }

        private void lkLoaiXe_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.ID == null)
            //{
                this.SetDonGia();
            //}
        }

        private void spGiaThang_EditValueChanged(object sender, EventArgs e)
        {
            spThanhTien.EditValue = spGiaThang.Value * spKyTT.Value;
            TinhThanhTien();
        }

        private void spKyTT_EditValueChanged(object sender, EventArgs e)
        {
            spThanhTien.EditValue = spGiaThang.Value * spKyTT.Value;
            TinhThanhTien();
        }

        #region Nut xu ly luu, huy
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                glkMatBang.Focus();
                return;
            }

            if (lkLoaiXe.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại xe");
                lkLoaiXe.Focus();
                return;
            }

            if (txtSoThe.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số thẻ");
                txtSoThe.Focus();
                return;
            }

            if (dateNgayDK.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày đăng ký");
                dateNgayDK.Focus();
                return;
            }
            if (dateNgayTT.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thanh toán");
                dateNgayTT.Focus();
                return;
            }
            if (txtBienSo.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập biển số xe");
                txtBienSo.Focus();
                return;
            }
            if (txtSoTheChip.Text.Length > 0 & (dateTuNgay.EditValue == null | dateDenNgay.EditValue == null))
            {
                DialogBox.Error("Vui lòng nhập Ngày hiệu lực - Ngày hết hạn");
                return;
            }

            #endregion


            try
            {
                if (objTheXe.ID != 0)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        var ls = new dvgxTheXe_Backup();

                        ls.MaTN = this.MaTN;
                        ls.NgayNhap = db.GetSystemDate();
                        ls.MaNVN = Common.User.MaNV;
                        ls.IsTheXe = true;
                        ls.MaKH = objTheXe.MaKH;
                        ls.MaMB = objTheXe.MaMB;
                        ls.MaThe = objTheXe.ID;
                        ls.SoThe = objTheXe.SoThe;
                        ls.NgayDK = objTheXe.NgayDK;
                        ls.MaLX = objTheXe.MaLX;
                        ls.GiaThang = objTheXe.GiaThang;
                        ls.MaNK = objTheXe.MaNK;
                        ls.ChuThe = objTheXe.ChuThe;
                        ls.BienSo = objTheXe.BienSo;
                        ls.MauXe = objTheXe.MauXe;
                        ls.DoiXe = objTheXe.DoiXe;
                        ls.NgayTT = objTheXe.NgayTT;
                        ls.KyTT = objTheXe.KyTT;
                        ls.TienTT = objTheXe.TienTT;
                        ls.DienGiai = objTheXe.DienGiai;
                        ls.NgungSuDung = objTheXe.NgungSuDung;
                        ls.NgayNgungSD = objTheXe.NgayNgungSD;

                        dbo.dvgxTheXe_Backups.InsertOnSubmit(ls);
                        dbo.SubmitChanges();
                    }
                    
                }
                if (objTheXe.ID == 0)
                {
                    objTheXe.MaTN = this.MaTN;
                    objTheXe.NgayNhap = db.GetSystemDate();
                    objTheXe.MaNVN = Common.User.MaNV;
                    objTheXe.IsTheXe = true;
                    db.dvgxTheXes.InsertOnSubmit(objTheXe);
                }
                else
                {
                    objTheXe.NgaySua = db.GetSystemDate();
                    objTheXe.MaNVS = Common.User.MaNV;

                }


                bool InsertAPI = false;
                bool UpdateAPI = false;
                string MaTheCu = objTheXe.MaTheChip;

                // Thẻ chưa đc gán Mã Thẻ Chip lần nào
                if(!objTheXe.NgungSuDung.GetValueOrDefault())
                {
                    if (objTheXe.MaTheChip == null)
                         InsertAPI = txtSoTheChip.Tag != null;
                    else
                        UpdateAPI = true;
                }


                objTheXe.SoThe = txtSoThe.Text;
                objTheXe.NgayDK = dateNgayDK.DateTime;
                objTheXe.MaLX = (int?)lkLoaiXe.EditValue;
                objTheXe.GiaThang = spGiaThang.Value;
                objTheXe.MaNK = (int?)lkChuThe.EditValue;
                objTheXe.ChuThe = txtTenChuThe.Text;
                objTheXe.BienSo = txtBienSo.Text;
                objTheXe.MauXe = txtMauXe.Text;
                objTheXe.DoiXe = txtDoiXe.Text;
                objTheXe.NgayTT = (DateTime?)dateNgayTT.EditValue;
                objTheXe.KyTT = spKyTT.Value;
                objTheXe.TienTT = spTienTT.Value;
                objTheXe.DienGiai = txtDienGiai.Text;
                objTheXe.NgungSuDung = ckbNgungSuDung.Checked;
                objTheXe.NgayNgungSD = (DateTime?)dateNgungSD.EditValue;
                objTheXe.TienTruocThue = spThanhTien.Value;
                objTheXe.ThueGTGT = spVAT.Value;
                objTheXe.TienThueGTGT = spTienVAT.Value;
                objTheXe.TuNgay = (DateTime?)dateTuNgay.EditValue;
                objTheXe.DenNgay = (DateTime?)dateDenNgay.EditValue;
                objTheXe.SoTienCoc = spinSoTienCoc.Value;
                db.SubmitChanges();

                var kh = db.tnKhachHangs.Single(o => o.MaKH == objTheXe.MaKH);

                // Tạo vé tháng cho thẻ xe
                if (InsertAPI)
                {
                    var objTicket = (APITheXe.TheChuaTaoVeThang)txtSoTheChip.Tag;

                    var objTicketMonth = new APITheXe.TaoVeThangMoi();
                    objTicketMonth.blockCode =BlockCode; // Mã tòa nhà
                    objTicketMonth.IdAccountCreate = 1; // Mã người tạo mặc định là 1
                    objTicketMonth.Stt = objTicket.Identify; // STT của thẻ
                    objTicketMonth.ID = objTicket.ID; // ID thẻ Chip
                    objTicketMonth.Digit = objTheXe.BienSo; // Biển số
                    objTicketMonth.TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen;
                    objTicketMonth.CMND = kh.CMND;
                    objTicketMonth.Company = kh.CtyTen;
                    objTicketMonth.Email = kh.EmailKH;
                    objTicketMonth.Address = kh.DCLL;
                    objTicketMonth.CarKind = objTheXe.DoiXe;
                    objTicketMonth.IDPart =Convert.ToInt32(APITheXe.SearchLoaiXe(MaTN.Value,BlockCode,objTheXe.dvgxLoaiXe.TenLoaiXe_CPK).id);
                    objTicketMonth.fromDate = objTheXe.TuNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                    objTicketMonth.toDate = objTheXe.DenNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                    objTicketMonth.Note = objTheXe.DienGiai;
                    objTicketMonth.Amount = (int)objTheXe.TienTT;

                    var result = APITheXe.DangKyVeThang(objTicketMonth, this.MaTN.Value, BlockCode).FirstOrDefault();

                    if (result != null)
                    {
                        objTheXe.MaTheChip = result.id;
                        objTheXe.RowID = result.rowid;
                        objTheXe.NgayKichHoatThe = result.DayUnLimit;
                        db.SubmitChanges();
                    }
                }
                else
                {
                    if (UpdateAPI)
                    {
                        var objTicket = (APITheXe.TheChuaTaoVeThang)txtSoTheChip.Tag;
                        if (objTicket != null)
                        {
                            var objTicketMonth = new APITheXe.TaoVeThangMoi();
                            objTicketMonth.blockCode = BlockCode; // Mã tòa nhà
                            objTicketMonth.IdAccountCreate = 1; // Mã người tạo mặc định là 1
                            objTicketMonth.Stt = objTicket.Identify; // STT của thẻ
                            objTicketMonth.ID = objTicket.ID; // ID thẻ Chip
                            objTicketMonth.Digit = objTheXe.BienSo; // Biển số
                            objTicketMonth.TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen;
                            objTicketMonth.CMND = kh.CMND;
                            objTicketMonth.Company = kh.CtyTen;
                            objTicketMonth.Email = kh.EmailKH;
                            objTicketMonth.Address = kh.DCLL;
                            objTicketMonth.CarKind = objTheXe.DoiXe;
                            objTicketMonth.IDPart = Convert.ToInt32(APITheXe.SearchLoaiXe(MaTN.Value, BlockCode, objTheXe.dvgxLoaiXe.TenLoaiXe_CPK).id);
                            objTicketMonth.fromDate = objTheXe.TuNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                            objTicketMonth.toDate = objTheXe.DenNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                            objTicketMonth.Note = objTheXe.DienGiai;
                            objTicketMonth.Amount = (int)objTheXe.TienTT;

                            APITheXe.XoaTheXe(MaTheCu, this.MaTN.Value, BlockCode, objTheXe.RowID);
                            // Kiểm tra số thẻ => Xác định đổi thẻ hay update thẻ
                            var result = APITheXe.DangKyVeThang(objTicketMonth, this.MaTN.Value, BlockCode).FirstOrDefault();

                            if (result != null)
                            {
                                objTheXe.MaTheChip = result.id;
                                objTheXe.RowID = result.rowid;
                                objTheXe.NgayKichHoatThe = result.DayUnLimit;
                                db.SubmitChanges();
                            }
                        }
                            //Chỉ cập nhật thông tin ko thay đổi mã thẻ chip
                        else
                        {
                            var objTicketMonth = new APITheXe.CapNhatTheXe();
                            objTicketMonth.blockCode = BlockCode; // Mã tòa nhà
                            objTicketMonth.IdAccountEdit = 1; // Mã người tạo mặc định là 1
                            objTicketMonth.Stt = string.IsNullOrEmpty(objTheXe.SoThe) ? (int?)null : Convert.ToInt32(objTheXe.SoThe); // STT của thẻ
                            objTicketMonth.rowID = objTheXe.RowID;
                            objTicketMonth.Digit = objTheXe.BienSo; // Biển số
                            objTicketMonth.TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen;
                            objTicketMonth.CMND = kh.CMND;
                            objTicketMonth.Company = kh.CtyTen;
                            objTicketMonth.Email = kh.EmailKH;
                            objTicketMonth.Address = kh.DCLL;
                            objTicketMonth.CarKind = objTheXe.DoiXe;
                            objTicketMonth.IDPart = Convert.ToInt32(APITheXe.SearchLoaiXe(MaTN.Value, BlockCode, objTheXe.dvgxLoaiXe.TenLoaiXe_CPK).id);
                            objTicketMonth.fromDate = objTheXe.TuNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                            objTicketMonth.toDate = objTheXe.DenNgay.Value.ToString("yyyy-MM-dd 00:00:00");
                            objTicketMonth.Note = objTheXe.DienGiai;
                            objTicketMonth.Amount = (int)objTheXe.TienTT;
                            var result = APITheXe.CapNhatVeThang(objTicketMonth, this.MaTN.Value, BlockCode);

                            if (result != null)
                            {
                                objTheXe.MaTheChip = result.ID;
                                objTheXe.RowID = result.RowID;
                                objTheXe.NgayKichHoatThe = result.DayUnLimit;
                                db.SubmitChanges();
                            }
                        }
                        
                    }
                }

                if (IsTheCuDan == true)
                {
                    MaKH = objTheXe.MaKH;
                }
                this.IDTX = objTheXe.ID;

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void txtBienSo_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBienSo.Text.Trim()))
            {
                txtDienGiai.Text = string.Format("Phí giữ xe biển số {0}", txtBienSo.Text);
            }
            else
            {
                txtDienGiai.Text = "";
            }
        }

        private void ckbNgungSuDung_CheckedChanged(object sender, EventArgs e)
        {
            dateNgungSD.Enabled = ckbNgungSuDung.Checked;
            if (ckbNgungSuDung.Checked)
                dateNgungSD.EditValue = DateTime.Now;
            else
                dateNgungSD.EditValue = null;
        }

        private void spVAT_EditValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void glkTheAPI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (objTheXe.NgungSuDung.GetValueOrDefault())
            {
                DialogBox.Error("Thẻ đã ngưng sử dụng. Không thể chọn thẻ Chip");
                return;
            }
            try
            {
                using (var frm = new frmChonTheChip())
                {
                    frm.MaTN = this.MaTN.Value;
                    frm.BlockCode = GetBlockCode((int?)glkMatBang.EditValue);
                    if (frm.BlockCode == "")
                    {
                        DialogBox.Alert("Vui lòng chọn mặt bằng trước khi chọn thẻ chip");
                        return;
                    }
                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {
                        txtSoTheChip.Tag = frm.objTicket;
                        txtSoTheChip.Text = frm.objTicket.ID;
                        txtSoThe.Text = frm.objTicket.Identify.GetValueOrDefault().ToString();
                    }
                }
            }
            catch
            {
                DialogBox.Alert("Chưa cấu hình kết nối API cho thẻ xe. Vui lòng liên hệ Admin");
            }
        }
    }
}