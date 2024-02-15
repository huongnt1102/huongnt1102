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

namespace DichVu.HopTac
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public hdhtHopDong objHD;
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            try
            {
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    lookNhanVien.Properties.DataSource = db.tnNhanViens
                        .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                    lookTyGia.Properties.DataSource = db.tnTyGias;
                    lookKhachHang.Properties.DataSource = db.tnKhachHangs
                        .Select(p => new
                        {
                            p.MaKH,
                            TenKH = (bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                            MaSoKH = (bool)p.IsCaNhan ? p.CMND : p.CtySoDKKD,
                            DiaChiKH = p.DCLL
                        });
                }
                else
                {
                    lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN)
                        .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                    lookTyGia.Properties.DataSource = db.tnTyGias.Where(p => p.MaTN == objnhanvien.MaTN);
                    lookKhachHang.Properties.DataSource = db.tnKhachHangs.Where(p => p.MaTN == objnhanvien.MaTN)
                        .Select(p => new
                        {
                            p.MaKH,
                            TenKH = (bool)p.IsCaNhan ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                            MaSoKH = (bool)p.IsCaNhan ? p.CMND : p.CtySoDKKD,
                            DiaChiKH = p.DCLL
                        });
                }
                lookTrangThai.Properties.DataSource = db.hdhtTrangThais;
                if (objHD != null)
                {
                    objHD = db.hdhtHopDongs.Single(p => p.MaHD == objHD.MaHD);
                    txtTenHD.Text = objHD.TenHD;
                    txtSoHD.Text = objHD.SoHD;
                    dateNgayKy.EditValue = objHD.NgayKy;
                    dateNgayBD.EditValue = objHD.NgayBD;
                    dateNgayKT.EditValue = objHD.NgayKT;
                    spinGiaTri.EditValue = objHD.GiaTri;
                    lookTyGia.EditValue = objHD.MaTG;
                    lookTrangThai.EditValue = objHD.MaTT;
                    lookNhanVien.EditValue = objHD.MaNV;
                    lookKhachHang.EditValue = objHD.MaKH;
                    txtDienGiai.Text = objHD.DienGiai;
                    spinChuKyThanhToan.EditValue = objHD.ChuKyThanhToan;
                }
                else
                {
                    objHD = new hdhtHopDong();
                    string MaHDNew = string.Empty;
                    db.hdht_getNewMaHD(ref MaHDNew);
                    txtSoHD.Text = db.DinhDang(12, int.Parse(MaHDNew));
                    objHD.MaTN = objnhanvien.MaTN;
                    db.hdhtHopDongs.InsertOnSubmit(objHD);

                    dateNgayKy.DateTime = DateTime.Now;
                    dateNgayBD.DateTime = DateTime.Now;
                    lookNhanVien.EditValue = objnhanvien.MaNV;
                    lookTyGia.ItemIndex = 0;
                    lookTrangThai.ItemIndex = 0;
                }
            }
            catch { }
            

            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void lookDoiTac_EditValueChanged(object sender, EventArgs e)
        {
            txtDiaChiKH.Text = lookKhachHang.GetColumnValue("DiaChiKH").ToString();
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
                DialogBox.Error("Vui long nhập số hợp đồng");  
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
                DialogBox.Error("Vui lòng nập ngaày kết thúc");  
                dateNgayKT.Focus();
                return;
            }
            if (lookKhachHang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");  
                lookKhachHang.Focus();
                return;
            }
            if (spinChuKyThanhToan.Value < 0)
            {
                DialogBox.Error("Vui lòng chọn lịch thanh toán");  
                spinChuKyThanhToan.Focus();
                return;
            }

            objHD.TenHD = txtTenHD.Text;
            objHD.NgayKy = dateNgayKy.DateTime;
            objHD.NgayBD = dateNgayBD.DateTime;
            objHD.NgayKT = dateNgayKT.DateTime;
            objHD.GiaTri = spinGiaTri.Value;
            objHD.tnTyGia = db.tnTyGias.Single(p => p.MaTG == (int)lookTyGia.EditValue);
            objHD.hdhtTrangThai = db.hdhtTrangThais.Single(p => p.MaTT == (int)lookTrangThai.EditValue);
            objHD.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objHD.tnKhachHang = db.tnKhachHangs.Single(p => p.MaKH == (int)lookKhachHang.EditValue);
            objHD.DienGiai = txtDienGiai.Text;
            objHD.ChuKyThanhToan = (int)spinChuKyThanhToan.Value;

            save:
            try
            {
                objHD.SoHD = txtSoHD.Text;
                db.SubmitChanges();
                #region Tao lịch thanh toán
                if (db.hthtCongNos.Where(p => p.MaHD == objHD.MaHD).Count() == 0)
                {
                    List<hthtCongNo> lsthdtn = new List<hthtCongNo>();
                    int khoangthoigianhopdong = (dateNgayKT.DateTime - dateNgayBD.DateTime).Days / 30;
                    int solantt = (khoangthoigianhopdong / (int)spinChuKyThanhToan.Value) <= 1 ? 1 : ((khoangthoigianhopdong % (int)spinChuKyThanhToan.Value) > 0 ? (khoangthoigianhopdong / (int)spinChuKyThanhToan.Value) + 1 : (khoangthoigianhopdong / (int)spinChuKyThanhToan.Value));
                    for (int i = 1; i <= solantt; i++)
                    {
                        hthtCongNo objcn = new hthtCongNo();
                        objcn.SoTien = 0;
                        objcn.MaHD = objHD.MaHD;
                        objcn.SoHD = objHD.SoHD;
                        if (i == solantt)
                        {
                            var tt = (spinGiaTri.Value / solantt) * (solantt - 1);
                            objcn.ConLai = objHD.GiaTri - tt;
                        }
                        else
                            objcn.ConLai = spinGiaTri.Value / solantt;
                        objcn.NgayThanhToan = objHD.NgayBD.Value.AddMonths((int)spinChuKyThanhToan.Value * i);

                        lsthdtn.Add(objcn);
                    }


                    db.hthtCongNos.InsertAllOnSubmit(lsthdtn);
                    db.SubmitChanges();
                }
                #endregion
            }
            catch
            {
                string MaHDNew = string.Empty;
                db.hdht_getNewMaHD(ref MaHDNew);
                txtSoHD.Text = db.DinhDang(12, int.Parse(MaHDNew));
                goto save;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}