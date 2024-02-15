using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraBars.Alerter;
using Library.CongNoCls;
using System.Data.Linq.SqlClient;

namespace DichVu.ChoThue
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public thueHopDong objHD;
        readonly MasterDataContext db;
        bool DaDuyetHD = false;
        public tnNhanVien objnhanvien;
        public int? MaTN { get; set; }
        public int? MaMB { get; set; }
        public bool IsEdit = false;
        public int? HDTT { get; set; }
        bool first = true;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            radioHinhThucThue.SelectedIndex = 0;
            radioHinhThucThue_SelectedIndexChanged(null, null);
            TranslateLanguage.TranslateControl(this);
        }
        
        private void frmEdit_Load(object sender, EventArgs e)
        {
            //if (HDTT >= 2)
            //    btnSave.Enabled = false;
            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookToaNha.Properties.DataSource = list;
            if (MaTN != null)
                lookToaNha.EditValue = (byte)MaTN;

            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens
                .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                lookNhanVien.Properties.ReadOnly = false;
                lookKhachHang.Properties.DataSource = db.tnKhachHangs
                    .Select(p => new
                    {
                        p.MaKH,
                        TenKH = ((bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen).Trim(),
                        MaSoKH = p.KyHieu,
                        DiaChiKH = p.DCLL ,
                        
                    }).ToList();                
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN)
                .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });

                lookNhanVien.Properties.ReadOnly = true;
                lookKhachHang.Properties.DataSource = db.tnKhachHangs.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new
                    {
                        p.MaKH,
                        TenKH = ((bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen).Trim(),
                        MaSoKH = p.KyHieu,
                        DiaChiKH = p.DCLL
                    });
            }

            LoadMatBang();

            lookTyGia.Properties.DataSource = db.tnTyGias;
            lookTrangThai.Properties.DataSource = db.thueTrangThais;
            if(objnhanvien !=null) lookNhanVien.EditValue = objnhanvien.MaNV;
            if (objHD != null)
            {
                objHD = db.thueHopDongs.SingleOrDefault(p => p.MaHD == objHD.MaHD);
                txtSoHD.Text = objHD.SoHD;
                dateNgayHD.EditValue = objHD.NgayHD;
                dateNgayBG.EditValue = objHD.NgayBG;
                dateNgayBGMB.EditValue = objHD.NgayBGMB;
                dateNgayTT.EditValue = objHD.NgayTinhTien;
                spinThoiHan.EditValue = objHD.ThoiHan;
                lookTyGia.EditValue = objHD.MaTG;
                spinTyGia.EditValue = objHD.TyGia;
                lookTrangThai.EditValue = objHD.MaTT;
                searchLookMatBang.EditValue = objHD.MaMB;
                spinDienTich.EditValue = objHD.DienTich;
                spinDonGia.EditValue = objHD.DonGia;
                spinThanhTien.EditValue = objHD.ThanhTien;
                spinDonGiaUSD.EditValue = objHD.DonGiaUSD;
                spinThanhTienUSD.EditValue = objHD.ThanhTienUSD;
                spinPhiQL.EditValue = objHD.PhiQL;
                spinTienCoc.EditValue = objHD.TienCoc;
                spinTienCocUSD.EditValue = objHD.TienCocUSD;
                lookKhachHang.EditValue = objHD.MaKH;
                lookNhanVien.EditValue = objHD.MaNV;
                txtDienGiai.Text = objHD.DienGiai;
                spinChuKyThanhToan.EditValue = objHD.ChuKyThanhToan;
                spinThanhTienKTT.EditValue = objHD.ThanhTienKTT;
                spinChuKyThanhToanUSD.EditValue = objHD.ChuKyThanhToanUSD;
                spinThanhTienKTTUSD.EditValue = objHD.ThanhTienKTTUSD;
                spinGiaThueNgoai.EditValue = objHD.GiaThueNgoai ?? 0;
                spinDieuHoaNG.EditValue = objHD.GiaDieuHoaNgoai ?? 0;
                chkTHueNMB.Checked = objHD.IsNhieuMB ?? false;
                MaTN = objHD.MaTN;
                MaMB = objHD.MaMB;
                lookToaNha.EditValue = (byte)MaTN;
                dateNgayHH.EditValue = objHD.NgayHH;
                searchLookMatBang.EditValue = MaMB;
                if (objHD.IsThueLo == null | objHD.IsThueLo == false)
                {
                    lookLo.Properties.ReadOnly = true;
                }
                else
                {
                    lookLo.Properties.ReadOnly = false;
                    lookLo.EditValue = objHD.MaLo;
                }

                objHD = db.thueHopDongs.Single(p => p.MaHD == objHD.MaHD);

                try
                {
                    if (objHD.thueTrangThai.MaTT != 1) //Đang chờ duyệt
                        DaDuyetHD = true;
                }
                catch
                {
                    DaDuyetHD = false;
                }

                spinPhiBaoDuong.EditValue = objHD.PhiBaoDuong ?? 0;
                lookTrangThai.Enabled = false;
                if (objHD.MaTT == 1)
                {
                    lookKhachHang.Enabled = true;
                    spinThoiHan.Enabled = true;
                }
                else
                {
                    lookKhachHang.Enabled = false;
                    spinThoiHan.Enabled = true;
                }
            }
            else
            {
                objHD = new thueHopDong();
                db.thueHopDongs.InsertOnSubmit(objHD);
                string MaHDNew = string.Empty;
                db.btHopDong_getNewMaHD(ref MaHDNew);
                dateNgayHD.DateTime = DateTime.Now;
                dateNgayBG.DateTime = DateTime.Now;
                dateNgayBGMB.DateTime = DateTime.Now;
                dateNgayTT.DateTime = DateTime.Now;
                dateNgayHH.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookTyGia.ItemIndex = 0;
                lookTrangThai.ItemIndex = 0;
                //txtSoHD.Text = string.Format("HD-{0}", MaHDNew);
                txtSoHD.Text = db.DinhDang(1, int.Parse(MaHDNew));
                spinPhiBaoDuong.EditValue = 0;
                searchLookMatBang.EditValue = MaMB;
            }
            radioHinhThucThue_SelectedIndexChanged(null, null);
        //    gcMatBang.DataSource = objHD.thueMatBangs; để điều chỉnh nếu cần
            first = false;
        }

        void LoadMatBang()
        {
            MaTN = Convert.ToInt32(lookToaNha.EditValue.ToString());

            if (objHD == null)
                slookMatBang.DataSource = searchLookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => (p.MaTT == 3 | p.MaTT == 8) & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == MaTN)
                       .Select(p => new
                       {
                           p.MaMB,
                           p.MaSoMB,
                           p.DienGiai,
                           p.mbTrangThai.TenTT,
                           p.mbTangLau.TenTL,
                           p.mbTangLau.mbKhoiNha.TenKN,
                           p.mbTangLau.mbKhoiNha.tnToaNha.TenTN
                       }).ToList();
            else
                slookMatBang.DataSource = searchLookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == MaTN)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaSoMB,
                        p.DienGiai,
                        p.mbTrangThai.TenTT,
                        p.mbTangLau.TenTL,
                        p.mbTangLau.mbKhoiNha.TenKN,
                        p.mbTangLau.mbKhoiNha.tnToaNha.TenTN
                    }).ToList();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!objnhanvien.IsSuperAdmin.Value)
            {
                if (DaDuyetHD)
                {
                    DialogBox.Error("Không thể thay đổi hợp đồng đã duyệt hoặc đã bàn giao mặt bằng.");
                    return;
                }
            }
            
            if (txtSoHD.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập [Số hợp đồng], xin cảm ơn.");
                txtSoHD.Focus();
                return;
            }

            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Trạng thái], xin cảm ơn.");
                return;
            }

            if (searchLookMatBang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            if (lookKhachHang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                return;
            }

            if (spinThoiHan.Value <= 0)
            {
                DialogBox.Error("Vui lòng chọn [hời hạn thuê]], xin cảm ơn.");
                return;
            }

            if (spinThoiHan.Value < spinChuKyThanhToan.Value)
            {
                DialogBox.Error("Thời hạn cho thuê phải lớn hơn chu kỳ thanh toán.");
                return;
            }

            if (spinPhiQL.Value < 0)
            {
                DialogBox.Error("Phí quản lý không hợp lệ.");
                return;
            }

            if (radioHinhThucThue.SelectedIndex == 1 & lookLo.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn lô.");
                return;
            }

            try
            {
                if (spinDienTich.Value > db.mbMatBangs.Single(p => p.MaMB == (int)searchLookMatBang.EditValue).DienTich)
                {
                    DialogBox.Error("Diện tích thuê không được lớn hơn diện tích mặt bằng.");
                    return;
                }
            }
            catch { }

            if (objHD == null)
            {
                objHD.ThoiHanF1 = Convert.ToInt32(spinThoiHan.EditValue);
            }
            objHD.SoHD = txtSoHD.Text;
            objHD.NgayHD = dateNgayHD.DateTime;
            objHD.NgayBG = dateNgayBG.DateTime;
            objHD.NgayBGMB = dateNgayBGMB.DateTime;
            objHD.NgayTinhTien = dateNgayTT.DateTime;
            objHD.MaTG =  (int?)lookTyGia.EditValue;
            objHD.TyGia = spinTyGia.Value;
            objHD.thueTrangThai = db.thueTrangThais.Single(p => p.MaTT == (int)lookTrangThai.EditValue);
            objHD.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objHD.tnKhachHang = db.tnKhachHangs.Single(p => p.MaKH == (int)lookKhachHang.EditValue);
            objHD.DienTich = spinDienTich.Value;
            objHD.DonGia = spinDonGia.Value;
            objHD.ThanhTien = spinThanhTien.Value;
            objHD.DonGiaUSD = spinDonGiaUSD.Value;
            objHD.ThanhTienUSD = spinThanhTienUSD.Value;
            objHD.PhiQL = spinPhiQL.Value;
            objHD.TienCoc = spinTienCoc.Value;
            objHD.DienGiai = txtDienGiai.Text;
            objHD.ChuKyThanhToan = (int?)spinChuKyThanhToan.Value;
            objHD.ThanhTienKTT = (decimal?)spinThanhTienKTT.EditValue;
            objHD.ThanhTienKTTUSD = (decimal?)spinThanhTienKTTUSD.EditValue;
            objHD.ChuKyThanhToanUSD = (int?)spinChuKyThanhToanUSD.Value;
            objHD.MaTN = (byte)MaTN;
            objHD.PhiBaoDuong = spinPhiBaoDuong.Value;
            objHD.DaHuy = false;
            objHD.ThoiHan = Convert.ToInt32(spinThoiHan.EditValue);
            objHD.TienCocUSD = (decimal?)spinTienCocUSD.EditValue;
            objHD.NgayHH = (DateTime?)dateNgayHH.EditValue;
            objHD.GiaDieuHoaNgoai = (decimal?)spinDieuHoaNG.EditValue;
            objHD.GiaThueNgoai = (decimal?)spinGiaThueNgoai.EditValue;
            objHD.IsNhieuMB = chkTHueNMB.Checked;

            objHD.mbMatBang = db.mbMatBangs.Single(p => p.MaMB == (int)searchLookMatBang.EditValue);
            objHD.MaTTMB = objHD.mbMatBang.MaTT;

            if (radioHinhThucThue.SelectedIndex == 1)
            {
                objHD.IsThueLo = true;
                objHD.MaLo = (int)lookLo.EditValue;
            }
            else objHD.IsThueLo = false;

            var wait = DialogBox.WaitingForm();
            try
            {
                var congnochange = db.thueCongNos.Where(p=>p.thueHopDong == objHD & p.DaThanhToan <= 0);
                foreach (var item in congnochange)
                {
                    item.ConNo = spinThanhTien.Value;
                }

                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                
                string text = string.Format("Hợp đồng <b>{0}</b> đã chuyển trạng thái sang <b>{1}</b>", objHD.SoHD, objHD.thueTrangThai.TenTT);
                AlertInfo info = new AlertInfo("Thông báo", text);
                alertControl1.Show(this, info);

            }
            catch(Exception ex)
            {
                DialogBox.Alert(ex.Message);
            }
            
            #region Luu lịch sử giao dịch
            var objLS = new thueLichSu();
            objLS.MaNV = objnhanvien.MaNV;
            objLS.MaTT = objHD.MaTT;
            objLS.MaHD = objHD.MaHD;
            objLS.NgayTao = db.GetSystemDate();
            if(IsEdit)
                objLS.DienGiai = "Cập nhật hợp đồng";
            else
                objLS.DienGiai = "Thêm mới hợp đồng";
            db.thueLichSus.InsertOnSubmit(objLS);

            try
            {
                db.SubmitChanges();
            }
            catch
            {
                DialogBox.Alert("Không lưu được. Vui lòng thử lại!");
            }
            #endregion
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            mbMatBang objMB = db.mbMatBangs.Single(p => p.MaMB == (int)searchLookMatBang.EditValue);
            
            if (objMB !=null)
            {
                spinDienTich.EditValue = objMB.DienTich;
                //spinDonGia.EditValue = objMB.ThanhTien;
                //spinThanhTien.EditValue = objMB.ThanhTien;
                //spinPhiQL.EditValue = objMB.PhiQuanLy;
                //spinPhiBaoDuong.EditValue = objMB.PhiBaoDuong;

                if (objHD != null)
                    lookLo.Properties.DataSource = db.mbMatBang_ChiaLos.Where(p => p.MaMB == objMB.MaMB & p.MaKH == null);
                else
                    lookLo.Properties.DataSource = db.mbMatBang_ChiaLos.Where(p => p.MaMB == objMB.MaMB & p.mbTrangThai.ChoThue.Value & p.MaKH == null);
            }            
        }

        private void lookKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            txtDiaChiKH.Text = lookKhachHang.GetColumnValue("DiaChiKH").ToString();
        }

        private void lookKhachHang_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                //case 1: //Them
                //    using (KyThuat.KhachHang.frmEdit frm = new KyThuat.KhachHang.frmEdit() { objnv = objnhanvien })
                //    {
                //        frm.ShowDialog();
                //        if (frm.DialogResult == DialogResult.OK)
                //        {
                //            lookKhachHang.Properties.DataSource = db.tnKhachHangs.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new { p.MaKH, TenKH = (bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen, MaSoKH = (bool)p.IsCaNhan ? p.CMND : p.CtySoDKKD, DiaChiKH = p.DCLL});
                //        }
                //    }
                    
                //    break;
                //case 2: //Sua
                //    if (lookKhachHang.EditValue == null)
                //    {
                //        DialogBox.Error("Vui lòng chọn khách hàng");
                //        return;
                //    }
                //    var objKH = db.tnKhachHangs.Single(p => p.MaKH == (int)lookKhachHang.EditValue);
                //    using (KyThuat.KhachHang.frmEdit frm = new KyThuat.KhachHang.frmEdit() { objnv=objnhanvien, objKH = objKH, IsCaNhan = objKH.IsCaNhan.Value })
                //    {
                //        frm.ShowDialog();
                //        if (frm.DialogResult == DialogResult.OK)
                //        {
                //            lookKhachHang.Properties.DataSource = db.tnKhachHangs.Select(p => new { p.MaKH, TenKH = (bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen, MaSoKH = (bool)p.IsCaNhan ? p.CMND : p.CtySoDKKD, DiaChiKH = p.DCLL});
                //        }
                //    }
                //    break;
                //default:
                //    break;
            }
        }

        private void radioHinhThucThue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioHinhThucThue.SelectedIndex)
            {
                case 0:// thue nguyen mat bang
                    lookLo.Properties.ReadOnly = true;
                    break;

                case 1:// thue lo nho trong mat bang
                    lookLo.Properties.ReadOnly = false;
                    break;
                
                default:
                    break;
            }
        }

        private void lookLo_EditValueChanged(object sender, EventArgs e)
        {
            mbMatBang_ChiaLo objMB_ChiaLo = (mbMatBang_ChiaLo)lookLo.GetSelectedDataRow();

            if (objMB_ChiaLo != null)
            {
                spinDienTich.EditValue = objMB_ChiaLo.DienTich;
                //spinDonGia.EditValue = objMB_ChiaLo.DonGia;
                spinThanhTien.EditValue = objMB_ChiaLo.GiaThue;
                //spinPhiQL.EditValue = objMB_ChiaLo.PhiQuanLy;
            }
        }

        private void lookToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadMatBang();
            if (lookToaNha.EditValue != null)
                lookTaiKhoan.Properties
                    .DataSource = db.tnTaiKhoanNHs.Where(p => p.MaTN == Convert.ToByte(lookToaNha.EditValue));
        }

        void LoadTienTyGia()
        {
            if (lookTyGia.EditValue == null)
                return;
            if (lookTyGia.Text == "VND")
            {
               // spinTienCocUSD.EditValue = spinTienCoc.Value / spinTyGia.Value;
                spinTyGia.Enabled = false;
                spinDonGia.Enabled = true;
                spinDonGiaUSD.Enabled = false;
                spinThanhTien.EditValue = (decimal?)spinDonGia.EditValue * (decimal?)spinDienTich.EditValue;
            }
            else if (lookTyGia.Text == "USD")
            {
               // spinTienCoc.EditValue = spinTienCocUSD.Value * spinTyGia.Value;
                spinTyGia.Enabled = true;
                spinDonGiaUSD.Enabled = true;
                spinDonGia.Enabled = false;
                spinDonGia.EditValue = (decimal?)spinDonGiaUSD.EditValue * (decimal?)spinTyGia.EditValue;
                spinThanhTienUSD.EditValue = (decimal?)spinDonGiaUSD.EditValue * (decimal?)spinDienTich.EditValue;
                spinThanhTien.EditValue = (decimal?)spinThanhTienUSD.EditValue * (decimal?)spinTyGia.EditValue;
            }

        }

        private void lookTyGia_EditValueChanged(object sender, EventArgs e)
        {
            if (lookTyGia.EditValue == null)
                return;
           
            LoadTienTyGia();
            //spinThanhTienKTT.EditValue = spinThanhTien.Value * spinChuKyThanhToan.Value;
            //spinThanhTienKTTUSD.EditValue = spinThanhTienUSD.Value * spinChuKyThanhToan.Value;
        }

        private void spinChuKyThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            if (spinThanhTien.EditValue != null)
                spinThanhTienKTT.EditValue = (decimal?)spinThanhTien.EditValue * (decimal?)spinChuKyThanhToan.Value;
            if (spinThanhTienUSD.EditValue != null)
                spinThanhTienKTTUSD.EditValue = spinThanhTienUSD.Value * spinChuKyThanhToanUSD.Value;
        }

        private void dateNgayBG_EditValueChanged(object sender, EventArgs e)
        {
            if (dateNgayBG.EditValue != null && spinThoiHan.Value != 0)
                dateNgayHH.EditValue = dateNgayBG.DateTime.AddMonths(Convert.ToInt32(spinThoiHan.Value));
        }

        private void chkTHueNMB_CheckedChanged(object sender, EventArgs e)
        {
          //  xtraTabPage2.PageVisible = chkTHueNMB.Checked; để điều chỉnh nêu cần
        }

    }
}