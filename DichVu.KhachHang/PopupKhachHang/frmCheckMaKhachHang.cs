using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.KhachHang.PopupKhachHang
{
    public partial class frmCheckMaKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public string KyHieu { get; set; }

        public int? MaKH { get; set; }
        public bool? IsLuuVaoDuAnMoi { get; set; }
        public byte? MaTN { get; set; }

        public frmCheckMaKhachHang()
        {
            InitializeComponent();
        }

        private void frmCheckMaKhachHang_Load(object sender, EventArgs e)
        {
            IsLuuVaoDuAnMoi = false;

            var db =  new MasterDataContext();

            gcCaNhan.DataSource = from c in db.tnKhachHangs
                                  join d in db.khNhomKhachHangs on c.MaNKH equals d.ID
                                  into tblNhomKH
                                  from d in tblNhomKH.DefaultIfEmpty()
                                  join lkh in db.khLoaiKhachHangs on c.MaLoaiKH equals lkh.ID into loaiKH
                                  from lkh in loaiKH.DefaultIfEmpty()
                                  join tn in db.tnToaNhas on c.MaTN equals tn.MaTN
                                  where c.CMND.ToUpper() == KyHieu.ToUpper() || c.CtyMaSoThue.ToUpper() == KyHieu.ToUpper()
                                  select new
                                  {
                                      c.KyHieu,
                                      c.MaPhu,
                                      c.MaKH,
                                      c.HoKH,
                                      TenKH = c.IsCaNhan == true ? c.TenKH : c.CtyTen,
                                      c.TaiKhoanNganHang,
                                      c.NoiCap,
                                      c.NgayCap,
                                      GioiTinh = c.GioiTinh.Value ? "Nam" : "Nữ",
                                      c.NgaySinh,
                                      c.CMND,
                                      c.DienThoaiKH,
                                      c.EmailKH,
                                      c.DCLL,
                                      c.DCTT,
                                      TenKV = c.MaKV != null ? c.tnKhuVuc.TenKV : "",
                                      c.QuocTich,
                                      MaSoThue = c.CtyMaSoThue,
                                      c.tnNhanVien.HoTenNV,
                                      TenNKH = d.TenNKH,
                                      lkh.TenLoaiKH,
                                      c.NguoiDongSoHuu,
                                      c.smsZalo,
                                      c.issmsZalo,
                                      c.nameZalo,
                                      c.DiaChiNhanThu,
                                      c.Website,
                                      c.NganhNgheDoanhNghiep
                                      ,
                                      c.EmailKhachThue
                                      ,
                                      c.DiaPhan
                                      ,
                                      c.Reference,
                                      TenTN = tn.TenTN,
                                      c.IsCaNhan
                                  };
        }

        /// <summary>
        /// Chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MaKH = Convert.ToInt32(grvCaNhan.GetFocusedRowCellValue("MaKH"));
            Close();
        }

        /// <summary>
        /// Lưu vào dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (grvCaNhan.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn khách hàng");
                    return;
                }

                
                Library.Class.Connect.QueryConnect.QueryData<bool>("tnKhachHang_Copy", new
                {
                    MaTN = MaTN,
                    MaKH = Convert.ToInt32(grvCaNhan.GetFocusedRowCellValue("MaKH"))
                });

                IsLuuVaoDuAnMoi = true;

                DialogBox.Success();
                this.DialogResult = DialogResult.OK;
                
                this.Close();
            }
            catch(System.Exception ex) { }
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsLuuVaoDuAnMoi = false;
            Close();
        }
    }
}