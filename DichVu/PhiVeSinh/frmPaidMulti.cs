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
using System.Threading;

namespace DichVu.PhiVeSinh
{
    public partial class frmPaidMulti : DevExpress.XtraEditors.XtraForm
    {
        public List<ItemData> listData;
        MasterDataContext db;
        public tnNhanVien objNV;
        public frmPaidMulti()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            int count = 1;

            var now = db.GetSystemDate();
            for (int i = 0; i < gvCongNo.RowCount; i++)
            {
                try
                {
                    var objPT = new PhieuThu();
                    objPT.NgayNhap = now;
                    objPT.NgayThu = DateTime.Parse(gvCongNo.GetRowCellValue(i, "NgayThu").ToString());
                    #region Detail
                    var objPTCT = new ptChiTiet();
                    objPT.ptChiTiets.Add(objPTCT);
                    objPTCT.ChietKhau = 0;
                    objPTCT.DienGiai = gvCongNo.GetRowCellValue(i, "DienGiai").ToString();
                    objPTCT.MaLDV = 13;
                    objPTCT.MaMB = Convert.ToInt32(gvCongNo.GetRowCellValue(i, "MaMB"));
                    objPTCT.NgayThu = DateTime.Parse(gvCongNo.GetRowCellValue(i, "DotThu").ToString());
                    objPTCT.PhaiThu = Convert.ToDecimal(gvCongNo.GetRowCellValue(i, "SoTien"));
                    objPTCT.TongCong = Convert.ToDecimal(gvCongNo.GetRowCellValue(i, "SoTien"));
                    #endregion

                    objPT.MaNV = objNV.MaNV;
                    string soPhieu = "";
                    db.btPhieuThu_getNewMaPhieuThu(ref soPhieu);
                    objPT.SoPhieu = "PTPVS/" + soPhieu;
                    objPT.DiaChi = gvCongNo.GetRowCellValue(i, "DiaChi").ToString();
                    objPT.NguoiNop = gvCongNo.GetRowCellValue(i, "NguoiNop").ToString();
                    objPT.DichVu = 13;
                    objPT.DienGiai = gvCongNo.GetRowCellValue(i, "DienGiai").ToString();
                    objPT.DotThanhToan = DateTime.Parse(gvCongNo.GetRowCellValue(i, "DotThu").ToString());
                    objPT.MaHopDong = gvCongNo.GetRowCellValue(i, "MaMB").ToString();
                    objPT.MaNV = objNV.MaNV;
                    objPT.SoTienThanhToan = Convert.ToDecimal(gvCongNo.GetRowCellValue(i, "SoTien"));
                    objPT.SoThangThuPhiQuanLy = 1;
                    objPT.SoTienChietKhauPhiQL = 0;
                    objPT.PhaiThu = Convert.ToDecimal(gvCongNo.GetRowCellValue(i, "SoTien"));
                    objPT.PhiQuanLy = Convert.ToDecimal(gvCongNo.GetRowCellValue(i, "SoTien"));
                    objPT.KeToanDaDuyet = false;
                    objPT.MaMB = Convert.ToInt32(gvCongNo.GetRowCellValue(i, "MaMB"));
                    objPT.CusID = Convert.ToInt32(gvCongNo.GetRowCellValue(i, "MaKH"));
                    objPT.ChuyenKhoan = Convert.ToBoolean(gvCongNo.GetRowCellValue(i, "ChuyenKhoan"));
                    db.PhieuThus.InsertOnSubmit(objPT);

                    db.SubmitChanges();
                }
                catch { }
                //
                wait.SetCaption(string.Format("Đã thanh toán {0}/{1} mặt bằng", count, gvCongNo.RowCount));

                count++;
                Thread.Sleep(30);
            }
            wait.SetCaption("Hoàn tất.");  

            wait.Close();
            wait.Dispose();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmPaidMulti_Load(object sender, EventArgs e)
        {
            gcCongNo.DataSource = listData;
        }
    }
}