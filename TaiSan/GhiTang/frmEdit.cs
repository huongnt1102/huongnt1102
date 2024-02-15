using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq;

namespace TaiSan.GhiTang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public tsTaiSan objTaiSan;
        public int? MaLTS { get; set; }
        public int IDTS { get; set; }
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadPage1()
        {
            lookHeThong.Properties.DataSource = db.tsHeThongs;
            lookLoaiTS.Properties.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS });
            lookDonViSD.Properties.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.TenDV, p.ID });
            lookNhaCungCap.Properties.DataSource = db.tnNhaCungCaps.Select(p => new { p.MaNCC, p.TenNCC });
            lookChatLuong.Properties.DataSource = db.tsChatLuongs.Select(p => new { p.MaCL, p.TenCL });
            LookTinhTrang.Properties.DataSource = db.tsTinhTrangs.Select(p => new { p.MaTT, p.TenTT });
            LookMatBangSD.Properties.DataSource=db.mbMatBangs.Select(p=>new {p.MaMB, p.MaSoMB});
            if (IDTS != 0)
            {
                LookMatBangSD.EditValue = (int?)objTaiSan.MaMB;
                txtMaTS.Text = objTaiSan.MaTS;
                txtTenTS.Text = objTaiSan.TenTS;
                lookLoaiTS.EditValue = objTaiSan.MaLTS;
                lookDonViSD.EditValue = objTaiSan.MaDVSD;
                lookNhaCungCap.EditValue = objTaiSan.MaNCC;
                LookTinhTrang.EditValue = objTaiSan.MaTT;
                lookChatLuong.EditValue = objTaiSan.MaCL;
                txtBienBanGNS.Text = objTaiSan.BBGiaoNhan;
                txtNhaSX.Text = objTaiSan.NhaSanXuat;
                txtQuocGia.Text = objTaiSan.NuocSX;
                txtThoiGianBH.Text = objTaiSan.ThoiHanBH;
                txtDieuKienBH.Text = objTaiSan.DieuKienBH;
                lookHeThong.EditValue = objTaiSan.MaHT;
                spinNamSX.EditValue = (int)objTaiSan.NamSX;
                txtSoHieu.Text = objTaiSan.SoHieu;
                dateNgay.EditValue = (DateTime?)objTaiSan.NgayGT;
                if (objTaiSan.IsTinhKH == true)
                    chkKhongTinhKH.Checked = false;
                else
                    chkKhongTinhKH.Checked = true;
                if (objTaiSan.IsNoiBo != true)
                    chkNoiBo.Checked = false;
                else
                    chkNoiBo.Checked = true;

            }
            else
            {
                if (MaLTS != null)
                    lookLoaiTS.EditValue = MaLTS;
            }

        }
        void LoadPage2()
        {
            lookTKBaoHanh.Properties.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
            lookTKKhauHao.Properties.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
            lookTKNguyenGia.Properties.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
            lookTKQLDN.Properties.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
            lookTKSanXuat.Properties.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
            if (IDTS != 0)
            {
                txtSoCT.Text = objTaiSan.SoGT;
                dateNgayGhiTang.EditValue = (DateTime?)objTaiSan.NgayGT;
                DateNgaySD.EditValue = (DateTime?)objTaiSan.NgayBDSD;
                spinTLChiPhiBH.EditValue = objTaiSan.TyLePhanBoBanHang;
                spinTLChiPhiQLDN.EditValue = objTaiSan.TyLePhanBoCPQLDN;
                spinTLChiPhiSX.EditValue = objTaiSan.TyLePhanBoCPSX;
                lookTKBaoHanh.EditValue = objTaiSan.TKCPBH;
                lookTKKhauHao.EditValue = objTaiSan.TKKhauHao;
                lookTKNguyenGia.EditValue = objTaiSan.TKNguyenGia;
                lookTKQLDN.EditValue = objTaiSan.TLCPQLDN;
                lookTKSanXuat.EditValue = objTaiSan.TKSPSX;
                SpinNguyenGia.EditValue = objTaiSan.NguyenGia;
                spinGTConLai.EditValue = objTaiSan.GiaTriConLai;
                spinGTKhauHanoThang.EditValue = objTaiSan.GiaTriKHThang;
                spinGTKhauHao.EditValue = objTaiSan.GiaTriTinhKH;
                spinGTKhauHaoNam.EditValue = objTaiSan.GiaTriKHNam;
                spinGTKhauHaoTNDN.EditValue = objTaiSan.GiaTriTinhKHThueTNDN;
                spinHaoMon.EditValue = objTaiSan.HaoMonLuyKe;
                spinTLKhauHaoNam.EditValue = objTaiSan.TyLeKHNam;
                spinTLKhauHaoThang.EditValue = objTaiSan.TyLeKHThang;
                spinThoiGianSD.EditValue = objTaiSan.ThoiGianSD;
                if ((bool)objTaiSan.DVTTGSD)
                    cbmNamThang.SelectedIndex = 0;
                else
                    cbmNamThang.SelectedIndex = 1;
            }

        }
        void LoadPage3()
        {
            gcMoTa.DataSource = objTaiSan.tsMoTaChiTiets;
        }
        void LoadPage4()
        {
            gcPhuTung.DataSource = objTaiSan.tsDungCu_PhuTungs;
        }
        void LoadPage5()
        {
            lookNguonGoc.Properties.DataSource = db.tsNguonGocHinhThanhs.Select(p => new { p.MaNGHT, p.TenNGHT });
            if (IDTS != 0)
            {
                lookNguonGoc.EditValue = objTaiSan.MaNGHT;
            }
        }
        void LoadTabPage()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (IDTS != 0)
                {
                    objTaiSan = db.tsTaiSans.Single(p => p.ID == IDTS);
                }
                else
                {
                    objTaiSan = new tsTaiSan();
                    db.tsTaiSans.InsertOnSubmit(objTaiSan);
                }
                LoadPage1();
                LoadPage2();
                LoadPage3();
                LoadPage4();
                LoadPage5();
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void SaveData()
        {
            #region ràng buộc dữ liệu
            if (txtMaTS.Text == "")
            {
                DialogBox.Alert("Bạn cần nhập mã tài sản. Xin cảm ơn!");
                txtMaTS.Focus();
                return;
            }
            if (txtTenTS.Text == "")
            {
                DialogBox.Alert("Bạn cần nhập tên tài sản. Xin cảm ơn!");
                txtTenTS.Focus();
                return;
            }
            if (lookLoaiTS.EditValue == null)
            {
                DialogBox.Alert("Bạn cần xét loại tài sản.Xin cảm ơn!");
                lookLoaiTS.Focus();
                return;
            }
            if (lookDonViSD.EditValue == "")
            {
                DialogBox.Alert("Bạn cần xét đơn vị sử dụng. Xin cảm ơn!");
                lookDonViSD.Focus();
                return;
            }
            if (dateNgayGhiTang.EditValue == null)
            {
                DialogBox.Alert("Bạn cần chọn ngày ghi tăng. Xin cảm ơn!");
                dateNgayGhiTang.Focus();
                return;
            }
            if (DateNgaySD.EditValue == null)
            {
                DialogBox.Alert("Bạn cần chọn ngày bắt đầu sử dụng. Xin cảm ơn!");
                DateNgaySD.Focus();
                return;
            }
           

            #endregion
            var wait = DialogBox.WaitingForm();
            try
            {

                if (IDTS == 0)
                {
                    objTaiSan.MaNV = objnhanvien.MaNV;
                    objTaiSan.NgayTao = DateTime.Now;
                }
                else
                {
                    objTaiSan = db.tsTaiSans.Single(p => p.ID == IDTS);
                    objTaiSan.MaNVCN = objnhanvien.MaNV;
                    objTaiSan.NgayCN = DateTime.Now;
                    tsTaiSan_LSCapNhat objls = new tsTaiSan_LSCapNhat();
                    objls.MaNV = objnhanvien.MaNV;
                    objls.NgayTH = DateTime.Now;
                    objTaiSan.tsTaiSan_LSCapNhats.Add(objls);
                }
                #region Page1
                objTaiSan.MaTS = txtMaTS.Text.Trim();
                objTaiSan.TenTS = txtTenTS.Text.Trim();
                objTaiSan.MaLTS = (int?)lookLoaiTS.EditValue;
                objTaiSan.MaDVSD = (int?)lookDonViSD.EditValue;
                objTaiSan.MaNCC = (int?)lookNhaCungCap.EditValue;
                objTaiSan.BBGiaoNhan = txtBienBanGNS.Text.Trim();
                objTaiSan.NhaSanXuat = txtNhaSX.Text.Trim();
                objTaiSan.NamSX = Convert.ToInt32(spinNamSX.EditValue);
                objTaiSan.SoHieu = txtSoHieu.Text.Trim();
                objTaiSan.NuocSX = txtQuocGia.Text.Trim();
                objTaiSan.ThoiHanBH = txtThoiGianBH.Text.Trim();
                objTaiSan.MaHT = (int?)lookHeThong.EditValue;
                if (chkKhongTinhKH.Checked)
                    objTaiSan.IsTinhKH = false;
                else
                    objTaiSan.IsTinhKH = true;
                objTaiSan.MaCL = (short?)lookChatLuong.EditValue;
                objTaiSan.NgayLapBB = (DateTime?)dateNgay.EditValue;
                objTaiSan.MaTT = (short?)LookTinhTrang.EditValue;
                objTaiSan.DieuKienBH = txtDieuKienBH.Text.Trim();
                objTaiSan.MaMB = (int?)LookMatBangSD.EditValue;
                if (chkNoiBo.Checked)
                    objTaiSan.IsNoiBo = true;
                else
                    objTaiSan.IsNoiBo = false;
                #endregion
                #region page2
                objTaiSan.SoGT = txtSoCT.Text.Trim();
                objTaiSan.NgayGT = (DateTime?)dateNgayGhiTang.EditValue;
                objTaiSan.NgayBDSD = (DateTime?)DateNgaySD.EditValue;
                objTaiSan.TyLePhanBoCPSX = (decimal?)spinTLChiPhiSX.EditValue;
                objTaiSan.TyLePhanBoCPQLDN = (decimal?)spinTLChiPhiQLDN.EditValue;
                objTaiSan.TyLePhanBoBanHang = (decimal?)spinTLChiPhiBH.EditValue;

                objTaiSan.TKKhauHao = lookTKKhauHao.EditValue == null ? "" : lookTKKhauHao.EditValue.ToString();
                objTaiSan.TKNguyenGia = lookTKNguyenGia.EditValue == null ? "" : lookTKKhauHao.EditValue.ToString();
                objTaiSan.TKSPSX = lookTKSanXuat.EditValue == null ? "" : lookTKKhauHao.EditValue.ToString();
                objTaiSan.TKCPBH = lookTKBaoHanh.EditValue == null ? "" : lookTKKhauHao.EditValue.ToString();
                objTaiSan.TLCPQLDN = lookTKQLDN.EditValue == null ? "" : lookTKKhauHao.EditValue.ToString();
                objTaiSan.NguyenGia = (decimal?)SpinNguyenGia.EditValue;
                objTaiSan.GiaTriTinhKH = (decimal?)spinGTKhauHao.EditValue;
                objTaiSan.GiaTriTinhKHThueTNDN = (decimal?)spinGTKhauHaoTNDN.EditValue;
                objTaiSan.ThoiGianSD = (decimal?)spinThoiGianSD.EditValue;
                if (cbmNamThang.SelectedIndex == 0)
                    objTaiSan.DVTTGSD = true;
                else
                    objTaiSan.DVTTGSD = false;
                objTaiSan.TyLeKHNam = (decimal?)spinTLKhauHaoNam.EditValue;
                objTaiSan.TyLeKHThang = (decimal?)spinTLKhauHaoThang.EditValue;
                objTaiSan.GiaTriKHNam = (decimal?)spinGTKhauHaoNam.EditValue;
                objTaiSan.GiaTriKHThang = (decimal?)spinGTKhauHanoThang.EditValue;
                objTaiSan.HaoMonLuyKe = (decimal?)spinHaoMon.EditValue;
                objTaiSan.GiaTriConLai = (decimal?)spinGTConLai.EditValue;
                #endregion
                #region Page3
                gvMoTa.RefreshData();
                #endregion
                #region Page4
                gvPhuTung.RefreshData();
                #endregion
                #region Page5
                objTaiSan.MaNGHT = (short?)lookNguonGoc.EditValue;
                #endregion
                db.SubmitChanges();
            }
            catch { }
            finally
            {
                wait.Close();
                this.Close();
            }
            

        }

        void LoadGTCK()
        {
            if (spinGTKhauHao.Value == 0)
                return;
            if (cbmNamThang.EditValue == null)
                return;
            if (spinThoiGianSD.Value == 0)
                return;
            if (spinTLKhauHaoNam.Value != 0)
            {
                spinGTKhauHaoNam.EditValue = (spinGTKhauHao.Value) * (spinTLKhauHaoNam.Value / 100);

            }
            if (spinTLKhauHaoThang.Value != 0)
            {
                spinGTKhauHanoThang.EditValue = (spinGTKhauHao.Value) * (spinTLKhauHaoThang.Value / 100);
            }
            spinGTConLai.EditValue = (decimal?)spinGTKhauHao.Value - (decimal?)spinHaoMon.Value;

        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadTabPage();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvMoTa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                gvMoTa.DeleteSelectedRows();
            }
        }

        private void gvPhuTung_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                gvPhuTung.DeleteSelectedRows();
            }
        }

        private void spinGTKhauHao_EditValueChanged(object sender, EventArgs e)
        {
            if ((decimal?)SpinNguyenGia.Value < (decimal?)spinGTKhauHao.Value)
            {
                DialogBox.Alert("Giá trị khấu hao phải nhỏ hơn hoặc bằng nguyên giá. Vui lòng kiểm tra lại!");
                return;
            }
            LoadGTCK();
        }

        private void spinThoiGianSD_EditValueChanged(object sender, EventArgs e)
        {
            if (spinThoiGianSD.Value == 0)
            {
                DialogBox.Alert("Thời gian sử dụng không thể bằng [0].Vui lòng kiểm tra lại!");
                return;
            }
            if (cbmNamThang.SelectedIndex == 0)
            {
                spinTLKhauHaoNam.EditValue = 100 / (decimal?)spinThoiGianSD.Value;
                spinTLKhauHaoThang.EditValue = 100 / ((decimal?)spinThoiGianSD.Value * 12);
                // spinGTKhauHaoNam.EditValue = 0;

            }
            else
            {
                spinTLKhauHaoThang.EditValue = 100 / (decimal?)spinThoiGianSD.Value;
                spinTLKhauHaoNam.EditValue = 100 * 12 / (decimal?)spinThoiGianSD.Value;
                // spinGTKhauHanoThang.EditValue = 0;
            }
            LoadGTCK();
        }

        private void spinTLKhauHaoNam_EditValueChanged(object sender, EventArgs e)
        {
            LoadGTCK();
        }

        private void spinTLKhauHaoThang_EditValueChanged(object sender, EventArgs e)
        {
            LoadGTCK();
        }

        private void cbmNamThang_EditValueChanged(object sender, EventArgs e)
        {
            if (spinThoiGianSD.Value == 0)
            {
                DialogBox.Alert("Thời gian sử dụng không thể bằng [0].Vui lòng kiểm tra lại!");
                return;
            }
            if (cbmNamThang.SelectedIndex == 0)
            {
                spinTLKhauHaoNam.Properties.ReadOnly = false;
                spinTLKhauHaoThang.Properties.ReadOnly = true;
                spinTLKhauHaoNam.EditValue = 100 / (decimal?)spinThoiGianSD.Value;
                spinTLKhauHaoThang.EditValue = 100 / ((decimal?)spinThoiGianSD.Value * 12);
               // spinGTKhauHaoNam.EditValue = 0;

            }
            else
            {
                spinTLKhauHaoNam.Properties.ReadOnly = true;
                spinTLKhauHaoThang.Properties.ReadOnly = false;
                spinTLKhauHaoThang.EditValue = 100 / (decimal?)spinThoiGianSD.Value;
                spinTLKhauHaoNam.EditValue = 100 * 12 / (decimal?)spinThoiGianSD.Value;
               // spinGTKhauHanoThang.EditValue = 0;
            }
        }

        private void SpinNguyenGia_EditValueChanged(object sender, EventArgs e)
        {
            if ((decimal?)SpinNguyenGia.Value < (decimal?)spinGTKhauHao.Value)
            {
                DialogBox.Alert("Giá trị khấu hao phải nhỏ hơn hoặc bằng nguyên giá. VUi lòng kiểm tra lại!");
                return;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }



    }
}