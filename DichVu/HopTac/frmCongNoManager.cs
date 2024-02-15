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
using System.Data.Linq.SqlClient;

namespace DichVu.HopTac
{
    public partial class frmCongNoManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmCongNoManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmCongNoManager_Load(object sender, EventArgs e)
        {
            itemDenNgay.EditValue = db.GetSystemDate();
            LoadData();
        }

        private void LoadData()
        {
            if (itemDenNgay.EditValue == null)
            {
                gcCongNo.DataSource = null;
            }
            else
            {
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcCongNo.DataSource = db.hthtCongNos
                        .Where(p => p.NgayThanhToan <= (DateTime)itemDenNgay.EditValue
                            & p.ConLai > 0
                            & SqlMethods.DateDiffDay(p.hdhtHopDong.NgayBD, (DateTime)itemDenNgay.EditValue) >= 0)
                        .Select(p => new
                        {
                            p.MaCongNo,
                            MaHD = p.MaHD,
                            SoHopDong = p.hdhtHopDong.SoHD,
                            DaThanhToan = p.SoTien,
                            ConNo = p.ConLai,
                            NgayThanhToan = p.NgayThanhToan,
                            PhaiThanhToan = p.ConLai + p.SoTien,
                            NhaCungCap = p.hdhtHopDong.MaKH.HasValue ? (p.hdhtHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.hdhtHopDong.tnKhachHang.IsCaNhan.Value ? p.hdhtHopDong.tnKhachHang.HoKH + " " + p.hdhtHopDong.tnKhachHang.TenKH : p.hdhtHopDong.tnKhachHang.CtyTen) : "") : "",
                        });
                }
                else
                {
                    gcCongNo.DataSource = db.hthtCongNos
                        .Where(p => p.NgayThanhToan <= (DateTime)itemDenNgay.EditValue
                            & p.ConLai > 0
                            & SqlMethods.DateDiffDay(p.hdhtHopDong.NgayBD, (DateTime)itemDenNgay.EditValue) >= 0
                            & p.hdhtHopDong.tnNhanVien.MaTN == objnhanvien.MaTN)
                        .Select(p => new
                        {
                            p.MaCongNo,
                            MaHD = p.MaHD,
                            SoHopDong = p.hdhtHopDong.SoHD,
                            DaThanhToan = p.SoTien,
                            ConNo = p.ConLai,
                            NgayThanhToan = p.NgayThanhToan,
                            PhaiThanhToan = p.ConLai + p.SoTien,
                            NhaCungCap = p.hdhtHopDong.MaKH.HasValue ? (p.hdhtHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.hdhtHopDong.tnKhachHang.IsCaNhan.Value ? p.hdhtHopDong.tnKhachHang.HoKH + " " + p.hdhtHopDong.tnKhachHang.TenKH : p.hdhtHopDong.tnKhachHang.CtyTen) : "") : "",
                        });
                }
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn công nợ cần thanh toán");  
                return;
            }

            using (frmThanhToanCongNohdht frmtt = new frmThanhToanCongNohdht())
            {
                frmtt.objcn = db.hthtCongNos.Single(p => p.MaCongNo == (int)grvCongNo.GetFocusedRowCellValue("MaCongNo"));
                frmtt.objnhanvien = objnhanvien;
                frmtt.ShowDialog();
                if (frmtt.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }
    }
}