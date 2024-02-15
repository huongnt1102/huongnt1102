using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraReports.UI;
//using ReportMisc.DichVu;

namespace DichVu.ThueNgoai
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public hdtnCongNo objhd;
        public tnNhanVien objnhanvien;
        string sSoPhieu;
        public frmThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            LoadData();
            
            if (objhd!=null)
            {
                txtSoPhieu.Text = "PTTN-" + sSoPhieu;
                spinSoTienCanThanhToan.EditValue = objhd.ConLai;
                txtsotien.EditValue = objhd.ConLai;
                txtSoHD.Text = objhd.SoHD;
                txtNhaCungCap.Text = objhd.hdtnHopDong.tnNhaCungCap.TenNCC;
                txtThangThanhToan.Text = objhd.NgayThanhToan.Value.ToShortDateString();
                txtHoVaTen.Text = objnhanvien.HoTenNV;
                txtDiaChi.Text = objnhanvien.DiaChi;
                txtSoDienThoai.Text = objnhanvien.DienThoai;
                txtEmail.Text = objnhanvien.Email;
                txtGhiChu.Focus();
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;
            
            PhieuChi objphieuchi = new PhieuChi()
            {
                //DichVu = (int)EnumLoaiDichVu.DichVuThueNgoai,
                DienGiai = txtGhiChu.Text.Trim(),
                DotThanhToan = objhd.NgayThanhToan,
                MaHopDong = objhd.SoHD,
                MaNV = objnhanvien.MaNV,
                NgayChi = db.GetSystemDate(),
                SoTienThanhToan = txtsotien.Value,
                SoPhieu = sSoPhieu
            };

            db.PhieuChis.InsertOnSubmit(objphieuchi);

            #region công nợ
            objhd = db.hdtnCongNos.Single(p => p.MaCongNo == objhd.MaCongNo);
            if (objhd != null)
            {
                objhd.DaThanhToan = objhd.DaThanhToan + txtsotien.Value;
                objhd.ConLai = objhd.ConLai - txtsotien.Value;
            }
            #endregion
            try
            {
                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu chi không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptPhieuChi rpt = new ReportMisc.DichVu.Quy.rptPhieuChi(objphieuchi);
                    //rpt.ShowPreviewDialog();
                }
            }
            catch
            {
                DialogBox.Error("Vui lòng kiểm tra các giá trị nhập vào");  
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadData()
        {
            DateTime now = db.GetSystemDate();
            db.btPhieuChi_getNewMaPhieuChi(ref sSoPhieu);
            sSoPhieu = db.DinhDang(9, int.Parse(sSoPhieu));
        }

    }
}