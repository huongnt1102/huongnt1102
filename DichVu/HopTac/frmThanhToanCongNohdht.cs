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
//using ReportMisc.DichVu;
//using ReportMisc.DichVu.HopTac;

namespace DichVu.HopTac
{
    public partial class frmThanhToanCongNohdht : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public hthtCongNo objcn;
        string sSoPhieu;
        DateTime now;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;
        public frmThanhToanCongNohdht()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmCongNohdtn_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            //Dien ten mac dinh
            try
            {
                now = db.GetSystemDate();
                db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
                LoadData();

                txtHoVaTen.Text = objcn.hdhtHopDong.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objcn.hdhtHopDong.tnKhachHang.HoKH, objcn.hdhtHopDong.tnKhachHang.TenKH) : objcn.hdhtHopDong.tnKhachHang.CtyTen;
                txtDiaChi.Text = objcn.hdhtHopDong.tnKhachHang.DCLL;
                txtSoDienThoai.Text = objcn.hdhtHopDong.tnKhachHang.DienThoaiKH;
                txtEmail.Text = objcn.hdhtHopDong.tnKhachHang.EmailKH;
                txtGhiChu.Text = string.Format("Thanh toán tiền hợp đồng hợp tác số {0}", objcn.hdhtHopDong.SoHD);
            }
            catch { }

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

        private void LoadData()
        {
            if (objcn != null)
            {
                txtSoHD.Text = objcn.hdhtHopDong.SoHD;
                txtNhaCungCap.Text = objcn.hdhtHopDong.MaKH.HasValue ? (objcn.hdhtHopDong.tnKhachHang.IsCaNhan.HasValue ? (objcn.hdhtHopDong.tnKhachHang.IsCaNhan.Value ? objcn.hdhtHopDong.tnKhachHang.HoKH + " " + objcn.hdhtHopDong.tnKhachHang.TenKH : objcn.hdhtHopDong.tnKhachHang.CtyTen) : "") : "";
                txtThangThanhToan.Text = objcn.NgayThanhToan.Value.ToShortDateString();

                txtSoPhieu.Text = "PTDV-"+sSoPhieu;
                txtHoVaTen.Text = objnhanvien.HoTenNV;
                txtDiaChi.Text = objnhanvien.DiaChi;
                txtSoDienThoai.Text = objnhanvien.DienThoai;
                txtEmail.Text = objnhanvien.Email;
                txtGhiChu.Focus();

                spinSoTienCanThanhToan.EditValue = objcn.ConLai;
                txtsotien.EditValue = objcn.ConLai;
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if ((decimal)txtsotien.EditValue <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền thanh toán hợp lệ");  
                txtsotien.Focus();
                return;
            }
            try
            {
                objcn = db.hthtCongNos.Single(p => p.MaCongNo == objcn.MaCongNo);
                if (objcn!=null)
                {
                    objcn.SoTien = objcn.SoTien + txtsotien.Value;
                    objcn.ConLai = objcn.ConLai - txtsotien.Value;
                }

                PhieuThu objphieuthu = new PhieuThu()
                {
                    DiaChi = txtDiaChi.Text.Trim(),
                    //DichVu = (int)EnumLoaiDichVu.DichVuHoptac,
                    DienGiai = txtGhiChu.Text.Trim(),
                    DotThanhToan = db.GetSystemDate(),
                    MaHopDong = objcn.MaHD.ToString(),
                    MaNV = objnhanvien.MaNV,
                    NgayThu = now,
                    NguoiNop = txtHoVaTen.Text.Trim(),
                    SoPhieu = txtSoPhieu.Text.Trim(),
                    SoTienThanhToan = txtsotien.Value,
                    KeToanDaDuyet = false
                };
                db.PhieuThus.InsertOnSubmit(objphieuthu);

                db.SubmitChanges();

                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //rptPhieuThanhToan rpt = new rptPhieuThanhToan(objphieuthu);
                    //rpt.ShowPreviewDialog();
                }
            }
            catch
            {
                DialogBox.Error("Không lưu được, có thể đường truyền không ổn định! Vui lòng thử lại sau");  
                this.DialogResult = DialogResult.Cancel;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}