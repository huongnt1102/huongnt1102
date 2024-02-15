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
using System.Data.Linq.SqlClient;

namespace DichVu.ThueNgoai
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public hdtnHopDong objHD;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens
                    .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                lookDoiTac.Properties.DataSource = db.tnNhaCungCaps
                    .Select(p => new { p.MaNCC, p.TenNCC, p.DiaChi });
                lookToaNha.Properties.DataSource = db.tnToaNhas;
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                lookDoiTac.Properties.DataSource = db.tnNhaCungCaps
                    .Select(p => new { p.MaNCC, p.TenNCC, p.DiaChi });
                lookToaNha.Properties.DataSource = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN);
            }
            lookTyGia.Properties.DataSource = db.tnTyGias;
            lookTrangThai.Properties.DataSource = db.hdtnTrangThais;
            lookLoaiDV.Properties.DataSource = db.hdtnLoaiHDs;
            if (this.objHD != null)
            {
                objHD = db.hdtnHopDongs.Single(p => p.MaHD == objHD.MaHD);
                txtTenHD.Text = objHD.TenHD;
                txtSoHD.Text = objHD.SoHD;
                dateNgayKy.EditValue = objHD.NgayKy;
                spinThoiHan.EditValue = objHD.ThoiHan ?? 1;
                dateNgayBD.EditValue = objHD.NgayBD;
                dateNgayKT.EditValue = objHD.NgayKT;
                spinGiaTri.EditValue = objHD.GiaTri;
                spinTienCoc.EditValue = objHD.TienCoc ?? 0;
                lookTyGia.EditValue = objHD.MaTG;
                lookTrangThai.EditValue = objHD.MaTT;
                lookNhanVien.EditValue = objHD.MaNV;
                lookDoiTac.EditValue = objHD.MaNCC;
                lookLoaiDV.EditValue = objHD.MaLDV;
                txtDienGiai.Text = objHD.DienGiai;
                spinDaThanhToan.EditValue = db.hdtnCongNos.Where(p => p.MaHD == objHD.MaHD).Sum(p => p.DaThanhToan) ?? 0;
                spinConLai.EditValue = objHD.GiaTri - Convert.ToDecimal(spinDaThanhToan.EditValue);
                lookToaNha.EditValue = objHD.MaTN;
             //   cbbHinhThucThanhToan.SelectedIndex = objHD.MaTG.Value;
                objHD.MaNVCN = objnhanvien.MaNV;
                objHD.NgayCN = db.GetSystemDate();
            }
            else
            {
                objHD = new hdtnHopDong();
                string MaHDNew = string.Empty;
                db.hdtn_getNewMaHD(ref MaHDNew);
                txtSoHD.Text = db.DinhDang(11, int.Parse(MaHDNew));
                objHD.MaTN = objnhanvien.MaTN;
                objHD.MaNV = objnhanvien.MaNV;
                objHD.NgayTao = db.GetSystemDate();
                db.hdtnHopDongs.InsertOnSubmit(objHD);

                dateNgayKy.DateTime = DateTime.Now;
                dateNgayBD.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookTyGia.ItemIndex = 0;
                lookTrangThai.ItemIndex = 0;
                spinDaThanhToan.EditValue = 0;
                spinConLai.EditValue = 0;
            }
        }

        //string[] HinhThucThanhToan = new string[]
        //{
        //    "Theo tuần",
        //    "Theo tháng",
        //    "Theo quý",
        //    "Theo năm"
        //};
        decimal SoTienTTCK;
        int solantt;
        int thoigian;

        private void lookDoiTac_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDiaChiKH.Text = lookDoiTac.GetColumnValue("DiaChi").ToString();
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTenHD.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tên hợp đồng");  
                txtTenHD.Focus();
                return;
            }
            if (txtSoHD.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập số hợp đồng");  
                txtSoHD.Focus();
                return;
            }
            if (dateNgayKy.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày ký");  
                dateNgayKy.Focus();
                return;
            }
            if (dateNgayBD.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày bắt đầu");  
                dateNgayBD.Focus();
                return;
            }
            if (dateNgayKT.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày kết thúc");  
                dateNgayKT.Focus();
                return;
            }
            if (lookDoiTac.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn nhà cung cấp");  
                lookDoiTac.Focus();
                return;
            }
            if (lookToaNha.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn Dự án");  
                lookDoiTac.Focus();
                return;
            }

            //if (db.hdtnCongNos.Where(p => p.MaHD == objHD.MaHD).Count() == 0 & cbbHinhThucThanhToan.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng chọn hình thức thanh toán");  
            //    cbbHinhThucThanhToan.Focus();
            //    return;
            //}

            objHD.ThoiHan = Convert.ToInt32(spinThoiHan.EditValue);
            objHD.TenHD = txtTenHD.Text;
            objHD.NgayKy = dateNgayKy.DateTime;
            objHD.NgayBD = dateNgayBD.DateTime;
            objHD.NgayKT = dateNgayKT.DateTime;
            objHD.MaLDV = (int?)lookLoaiDV.EditValue;
            objHD.GiaTri = spinGiaTri.Value;
            objHD.TienCoc = spinTienCoc.Value;
            objHD.MaTG = (int?)lookTyGia.EditValue;
            objHD.MaTT = (int?)lookTrangThai.EditValue;
            objHD.MaNV = (int?)lookNhanVien.EditValue;
            objHD.MaNCC = (int?)lookDoiTac.EditValue;
            objHD.DienGiai = txtDienGiai.Text;
            objHD.MaTN = Convert.ToByte(lookToaNha.EditValue);

            save:
            try
            {
                objHD.SoHD = txtSoHD.Text;

                if (objHD.MaHD != 0)
                {
                    var objLS = new hdtnLichSu();
                    objLS.ContractID = objHD.MaHD;
                    objLS.DateCreate = db.GetSystemDate();
                    objLS.Description = "Cập nhật hợp đồng";
                    objLS.StaffID = objnhanvien.MaNV;
                    objLS.StatusID = objHD.MaTT;
                    db.hdtnLichSus.InsertOnSubmit(objLS);
                }

                db.SubmitChanges();

                #region Tao lịch thanh toán
                if (db.hdtnCongNos.Where(p => p.MaHD == objHD.MaHD).FirstOrDefault() == null)
                {
                    List<hdtnCongNo> lsthdtn = new List<hdtnCongNo>();
                    for (int i = 1; i <= solantt; i++)
                    {
                        hdtnCongNo objcn = new hdtnCongNo();
                        objcn.DaThanhToan = 0;
                        objcn.MaHD = objHD.MaHD;
                        objcn.SoHD = objHD.SoHD;
                        if (i == solantt)
                        {
                            var tt = (decimal)spinSoTien.EditValue * (solantt - 1);
                            objcn.ConLai = objcn.SoTien = objHD.GiaTri - tt;
                        }
                        else
                            objcn.ConLai = objcn.SoTien = (decimal)spinSoTien.EditValue;
                        objcn.NgayThanhToan = objHD.NgayBD.Value.AddDays(thoigian * i);

                        lsthdtn.Add(objcn);
                    }


                    db.hdtnCongNos.InsertAllOnSubmit(lsthdtn);
                    db.SubmitChanges();
                }
                #endregion

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch 
            {
                string MaHDNew = string.Empty;
                db.hdtn_getNewMaHD(ref MaHDNew);
                txtSoHD.Text = db.DinhDang(11, int.Parse(MaHDNew));
                goto save;
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbbHinhThucThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMoney();
        }

        void SetMoney()
        {
            int SoThang = SqlMethods.DateDiffMonth((DateTime)dateNgayBD.EditValue, (DateTime)dateNgayKT.EditValue);
            int KyTT = Convert.ToInt32(spinChuKyThanhToan.EditValue);
            solantt = (SoThang == 0 ? 1 : (SoThang % KyTT == 0 ? SoThang / KyTT : SoThang / KyTT + 1));  SoTienTTCK = Math.Round((decimal)spinGiaTri.EditValue / solantt, 0, MidpointRounding.AwayFromZero);
            spinSoTien.EditValue = SoTienTTCK;
            try
            {
                spinDaThanhToan.EditValue = db.hdtnCongNos.Where(p => p.MaHD == objHD.MaHD).Sum(p => p.DaThanhToan) ?? 0;
                spinConLai.EditValue = spinGiaTri.Value - Convert.ToDecimal(spinDaThanhToan.EditValue);
            }
            catch
            {
                spinDaThanhToan.EditValue = db.hdtnCongNos.Where(p => p.MaHD == objHD.MaHD).Sum(p => p.DaThanhToan) ?? 0;
                spinConLai.EditValue = spinGiaTri.Value - Convert.ToDecimal(spinDaThanhToan.EditValue);
            }
        }

        private void spinGiaTri_EditValueChanged(object sender, EventArgs e)
        {
            SetMoney();
        }

        private void dateNgayKT_EditValueChanged(object sender, EventArgs e)
        {
            SetMoney();
        }

        private void spinThoiHan_EditValueChanged(object sender, EventArgs e)
        {
            if (dateNgayBD.EditValue != null & spinThoiHan.EditValue != null)
                dateNgayKT.EditValue = ((DateTime)dateNgayBD.EditValue).AddMonths((int)spinThoiHan.Value);
        }
    }
}