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
//using ReportMisc.DichVu;

namespace DichVu.Quy
{
    public partial class frmPhieuThuDetail : DevExpress.XtraEditors.XtraForm
    {
        public PhieuThu objphieuthu;
        MasterDataContext db;
        public frmPhieuThuDetail()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmPhieuThuDetail_Load(object sender, EventArgs e)
        {
            if (objphieuthu != null)
            {
                txtSoPhieu.Text = objphieuthu.SoPhieu;
                txtDichVu.Text = LoaiDichVu(objphieuthu.DichVu.Value);
                txtNguoiNop.Text = objphieuthu.NguoiNop;
                txtDiaChi.Text = objphieuthu.DiaChi;
                txtNgayThu.Text = objphieuthu.NgayThu.Value.ToShortDateString();
                txtSoTien.Text = objphieuthu.SoTienThanhToan.Value.ToString("C");
                txtDotThanhToan.Text = objphieuthu.DotThanhToan.ToString();
                txtNguoiThu.Text = objphieuthu.tnNhanVien.HoTenNV;
            }
        }

        private string LoaiDichVu(int LDV)
        {
            string LoaiDV;

            switch (LDV)
            {
                //case (int)EnumLoaiDichVu.DichVuDien:
                //    LoaiDV = "Điện";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuNuoc:
                //    LoaiDV = "Nước";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuKhac:
                //    //LoaiDV = db.dvkDichVus.Single(p => String.Compare(p.MaDV.ToString(), objphieuthu.MaHopDong, false) == 0).dvkLoai.TenLDV;
                //    LoaiDV = "Nước";
                //    break;
                //case (int)EnumLoaiDichVu.HopDongThue:
                //    LoaiDV = "Hợp đồng thuê";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuGiuXe:
                //    LoaiDV = "Thẻ xe";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuThangMay:
                //    LoaiDV = "Thẻ thang máy";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuHoptac:
                //    LoaiDV = "Dịch vụ hợp tác";
                //    break;
                default:
                    LoaiDV = "Tổng hợp";
                    break;
            }
            
            return LoaiDV;
        }
    }
}