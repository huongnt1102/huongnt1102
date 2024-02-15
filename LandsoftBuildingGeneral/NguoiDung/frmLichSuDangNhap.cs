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

namespace LandsoftBuildingGeneral.NguoiDung
{
    public partial class frmLichSuDangNhap : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien { get; set; }

        public frmLichSuDangNhap()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmLichSuDangNhap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            if (objnhanvien != null)
            {
                gcLichSu.DataSource = db.tnNhanVien_LichSuDangNhaps.Where(p => p.MaNV == objnhanvien.MaNV)
                    .Select(p => new
                    {
                        p.tnNhanVien.HoTenNV,
                        p.tnNhanVien.MaSoNV,
                        p.ThoiGian,
                        p.DiaChiIP,
                        p.ComputerName
                    })
                    .OrderByDescending(p => p.ThoiGian);
                this.Text = "Lịch sử đăng nhập: " + objnhanvien.HoTenNV; 
            }
            else
            {
                gcLichSu.DataSource = db.tnNhanVien_LichSuDangNhaps
                    .Select(p => new
                    {
                        p.tnNhanVien.HoTenNV,
                        p.tnNhanVien.MaSoNV,
                        p.ThoiGian,
                        p.DiaChiIP,
                        p.ComputerName
                    })
                    .OrderByDescending(p => p.ThoiGian);
            }
        }
    }
}