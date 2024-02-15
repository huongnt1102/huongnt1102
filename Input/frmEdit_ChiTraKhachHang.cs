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
namespace LandSoftBuilding.Fund.Input
{
    public partial class frmEdit_ChiTraKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPC { set; get; }
        public int? MaPT { set; get; }
        public bool IsSave = false;
        byte MaTN = 0;
        MasterDataContext db = new MasterDataContext();
        pcPhieuChi_TraLaiKhachHang objPC;
        public frmEdit_ChiTraKhachHang()
        {
            InitializeComponent();
        }

        private void frmEdit_ChiTraKhachHang_Load(object sender, EventArgs e)
        {
            dtNgayChi.EditValue = DateTime.UtcNow.AddHours(7);
            spinTienChi.EditValue = 0;
            spinTienPhat.EditValue = 0;
            var objPt = db.ptPhieuThus.FirstOrDefault(p => p.ID == MaPT);
            if (objPt != null)
            {
                spinTienKyQuy.EditValue = objPt.SoTien;
                if (objPt.MaTN != null) MaTN = objPt.MaTN.Value;
                //Hiển thị khách hàng ký quỹ
                var objKh = db.tnKhachHangs.FirstOrDefault(p => p.MaKH == objPt.MaKH);
                if (objKh != null)
                {
                    txtKhacHang.EditValue = objKh.IsCaNhan == false ? objKh.CtyTen : objKh.HoKH + " " + objKh.TenKH;
                }

            }
            if (MaPC == null)
            {
                objPC = new pcPhieuChi_TraLaiKhachHang();
                if (objPt == null) return;
                if (objPt.MaTN != null)
                    txtSoCT.EditValue = Common.CreatePhieuKyQuy(dtNgayChi.DateTime.Month, dtNgayChi.DateTime.Year,
                        objPt.MaTN.Value);
            }
            else
            {
                objPC = db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(p => p.ID == MaPC);
                dtNgayChi.EditValue = objPC.NgayChi;
                txtSoCT.EditValue = objPC.SoPhieuChi;
                spinTienChi.EditValue = objPC.SoTienChi;
                spinTienPhat.EditValue = objPC.SoTienPhat;
                txtGhiChu.EditValue = objPC.GhiChu;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSoCT.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập số chứng từ phiếu chi");
                txtSoCT.Focus();
                return;
            }
            if (Convert.ToDecimal(spinTienChi.EditValue) == 0 && Convert.ToDecimal(spinTienPhat.EditValue) == 0)
            {
                DialogBox.Alert("Vui lòng nhập số tiền chi cho khách hàng");
                spinTienChi.Focus();
                return;
            }
            if (spinTienKyQuy.Value < spinTienChi.Value + spinTienPhat.Value)
            {
                DialogBox.Error("Số tiền chi không thể lớn hơn số tiền ký quỹ");
                spinTienChi.Focus();
                return;
            }
            //Kiểm tra tổng số tiền chi ra so với phiếu thu ký quỹ
            var objKyQuy = db.pcPhieuChi_TraLaiKhachHangs.Where(p => p.MaPT == this.MaPT).ToList();
            decimal sSoTienKyQuy = 0;
            foreach (var item in objKyQuy)
            {
                sSoTienKyQuy = sSoTienKyQuy + item.SoTienPhat.GetValueOrDefault() + item.SoTienChi.GetValueOrDefault();
            }
            if (sSoTienKyQuy + spinTienChi.Value + spinTienPhat.Value > spinTienKyQuy.Value)
            {
                DialogBox.Error("Số tiền chi không thể lớn hơn số tiền ký quỹ");
                spinTienChi.Focus();
                return;
            }
            //Check số chứng từ
            if (MaPC == null)
            {
                var objKt = db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(p => p.SoPhieuChi.Equals(txtSoCT.Text.Trim()));
                if (objKt != null)
                {
                    txtSoCT.EditValue = Common.CreatePhieuKyQuy(dtNgayChi.DateTime.Month, dtNgayChi.DateTime.Year, MaTN);
                }
            }
            else
            {

                var objKt = db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(p => p.SoPhieuChi.Equals(txtSoCT.Text.Trim()) && p.ID!=this.MaPC);
                if (objKt != null)
                {
                    txtSoCT.EditValue = Common.CreatePhieuKyQuy(dtNgayChi.DateTime.Month, dtNgayChi.DateTime.Year, MaTN);
                }
            }
            objPC.GhiChu = txtGhiChu.Text;
            objPC.MaPT = MaPT;
            objPC.NgayChi =(DateTime?) dtNgayChi.EditValue;
            objPC.SoPhieuChi = txtSoCT.Text;
            objPC.SoTienChi = spinTienChi.Value;
            objPC.SoTienPhat = spinTienPhat.Value;
            if (MaPC == null)
            {
                objPC.NgayNhap = DateTime.UtcNow.AddHours(7);
                objPC.NguoiNhap = Common.User.MaNV;
                db.pcPhieuChi_TraLaiKhachHangs.InsertOnSubmit(objPC);
            }
            else
            {
                objPC.NgaySua = DateTime.UtcNow.AddHours(7);
                objPC.NguoiSua = Common.User.MaNV;
            }

            #region Tạo phiếu thu nếu số tiền phạt >0
            // nếu đây là sửa phiếu, kiểm tra số tiền phạt 
            if (spinTienPhat.Value <= 0)
            {

            }
            #endregion

            db.SubmitChanges();
            IsSave = true;
            this.Close();

        }
    }
}