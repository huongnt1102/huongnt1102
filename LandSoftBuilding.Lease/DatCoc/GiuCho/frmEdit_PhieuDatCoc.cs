using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using Library.App_Codes;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace LandSoftBuilding.Lease
{
    public partial class frmEdit_PhieuDatCoc : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public byte? MaTN { get; set; }
        public bool? IsView { get; set; }
        private MasterDataContext db = null;
        private PhieuDatCoc_GiuCho objPDC = null;

        public frmEdit_PhieuDatCoc()
        {
            InitializeComponent();
        }
        
        private void frmEdit_PhieuDatCoc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            db = new MasterDataContext();

            // Load trang thai
            lkTrangThai.Properties.DataSource = db.PhieuDatCoc_GiuCho_TrangThais;

            // Load nhan vien sale
            glkNhanVienSale.Properties.DataSource = db.tnNhanViens.Where(p => p.IsLocked == false)
                                                    .Select(p => new
                                                    {
                                                        p.MaNV,
                                                        p.MaSoNV,
                                                        p.HoTenNV,
                                                        TenHienThi = String.Format("{0}-{1}", p.MaSoNV, p.HoTenNV),
                                                    }).OrderBy(p => p.MaSoNV).ToList();

            //Load khach hang
            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                 }).ToList();

            //Load mat bang
            glMatBang.DataSource =  (from mb in db.mbMatBangs
                                    join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                    join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                    where k.MaTN == this.MaTN
                                         & mb.MaTT != 85
                                    orderby mb.MaSoMB
                                    select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich, mb.GiaThue }).ToList();

            lkLoaiTien.Properties.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();

            if (this.ID != null)
            {
                objPDC = db.PhieuDatCoc_GiuChos.FirstOrDefault(o => o.ID == ID);
                if (objPDC != null)
                {
                    txtSoHDCT.Text = objPDC.SoCT;
                    lkTrangThai.EditValue = objPDC.MaTT;
                    glkNhanVienSale.EditValue = objPDC.MaNVSale;
                    
                    dateNgayDC.EditValue = (DateTime?)objPDC.NgayDatCoc;
                    spHanDC.EditValue = objPDC.ThoiHanCoc;
                    spTienDC.EditValue = objPDC.TienDatCoc;
                    dateNgayHH.EditValue = objPDC.NgayHHCoc;

                    glKhachHang.EditValue = objPDC.MaKH;
                    memoLinhVucHoatDong.EditValue = objPDC.LinhVucHoatDong;
                    memoDienGiai.EditValue = objPDC.DienGiai;

                    lkLoaiTien.EditValue = objPDC.MaLT;
                    spTyGia.EditValue = objPDC.TyGiaHD;

                    objPDC.MaNVSua = Common.User.MaNV;
                    objPDC.NgaySua = db.GetSystemDate();
                }
            }
            else
            {
                txtSoHDCT.Text = TaoSoHopDong();
                lkTrangThai.EditValue = 1;
                dateNgayDC.EditValue = DateTime.Now;
                spHanDC.EditValue = 0;
                dateNgayHH.EditValue = DateTime.Now;
                lkLoaiTien.ItemIndex = 0;

                objPDC = new PhieuDatCoc_GiuCho();
                objPDC.NgayNhap = db.GetSystemDate();
                objPDC.MaNVNhap = Common.User.MaNV;
                objPDC.MaTN = this.MaTN;
                db.PhieuDatCoc_GiuChos.InsertOnSubmit(objPDC);
            }

            gcChiTiet.DataSource = objPDC.PhieuDatCoc_GiuCho_ChiTiets;

            if(IsView.GetValueOrDefault()) // == true
            {
                glKhachHang.ReadOnly = true;
                itemsave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemXoaDong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spTyGia.EditValue = lkLoaiTien.GetColumnValue("TyGia");
            spTienDC.EditValue = calTienCoc() / Convert.ToDecimal(spTyGia.EditValue);
        }

        public string TaoSoHopDong()
        {
            var objTR = db.PhieuDatCoc_GiuCho_TrangThais.ToList();

            string SoHD = "";
            var objHD = db.PhieuDatCoc_GiuChos.Where(p => p.NgayDatCoc.Value.Year == DateTime.UtcNow.AddHours(7).Year && p.MaTN == MaTN).Select(p => new { p.SoCT });
            int SoTT = 0;
            foreach (var item in objHD)
            {
                string[] arrayString = item.SoCT.Split('/');
                string so = arrayString[0];
                if (so.Length > 0)
                {
                    if (so.All(char.IsDigit))
                    {
                        if (SoTT < Convert.ToInt32(so))
                        {
                            SoTT = Convert.ToInt32(so);
                        }
                    }
                }
            }
            string CodeTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN).TenVT;
            SoHD = (SoTT + 1).ToString() + "/" + DateTime.UtcNow.Year.ToString() + "/PĐC-" + CodeTN;
            return SoHD;
        }

        private void itemsave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtSoHDCT.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập số [phiếu đặt cọc]");
                txtSoHDCT.Focus();
                return;
            }

            if (lkTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Trạng thái phiếu đặt cọc]");
                lkTrangThai.Focus();
                return;
            }

            if (glkNhanVienSale.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Nhân viên Sale]");
                glkNhanVienSale.Focus();
                return;
            }

            if (dateNgayDC.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Ngày đặt cọc]");
                glkNhanVienSale.Focus();
                return;
            }

            objPDC.SoCT = txtSoHDCT.Text;
            objPDC.MaTT = (int?)lkTrangThai.EditValue;
            objPDC.MaNVSale = Convert.ToInt32(glkNhanVienSale.EditValue);

            objPDC.NgayDatCoc = Convert.ToDateTime(dateNgayDC.EditValue);
            objPDC.ThoiHanCoc = Convert.ToInt32(spHanDC.EditValue);
            objPDC.TienDatCoc = Convert.ToDecimal(spTienDC.EditValue);
            objPDC.NgayHHCoc = Convert.ToDateTime(dateNgayHH.EditValue);

            objPDC.MaKH = Convert.ToInt32(glKhachHang.EditValue);
            objPDC.LinhVucHoatDong = memoLinhVucHoatDong.Text;
            objPDC.DienGiai = memoDienGiai.Text;
            objPDC.MaLT = Convert.ToInt32(lkLoaiTien.EditValue);
            objPDC.TyGiaHD = Convert.ToDecimal(spTyGia.EditValue);

            db.SubmitChanges();
            DialogBox.Success();
            this.Close();
        }

        private void glMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkMB = (sender as GridLookUpEdit);
                var _MaMB = (int)lkMB.EditValue;
                var r = lkMB.Properties.GetRowByKeyValue(lkMB.EditValue);
                var type = r.GetType();
                gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                gvChiTiet.SetFocusedRowCellValue("DonGia", (decimal?)type.GetProperty("GiaThue").GetValue(r, null));
                gvChiTiet.SetFocusedRowCellValue("ThanhTien", (decimal?)type.GetProperty("GiaThue").GetValue(r, null) * (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                gvChiTiet.SetFocusedRowCellValue("TenTangLau", (string)type.GetProperty("TenTL").GetValue(r, null));
                gvChiTiet.SetFocusedRowCellValue("Commission", (decimal?)0);
                //gvChiTiet.SetFocusedRowCellValue("TienCoc", (decimal?)0);

                calThanhTien();
            }
            catch { }
        }

        private void calThanhTien()
        {
            try
            {
                gvChiTiet.FocusedRowHandle = -1;

                for (int i = 0; i < gvChiTiet.RowCount - 1; i++)
                {
                    decimal DienTich = Convert.ToDecimal(gvChiTiet.GetRowCellValue(i, "DienTich"));
                    decimal DonGia = Convert.ToDecimal(gvChiTiet.GetRowCellValue(i, "DonGia"));
                    decimal SoThangThue = Convert.ToDecimal(gvChiTiet.GetRowCellValue(i, "KyTT"));

                    gvChiTiet.SetRowCellValue(i, "ThanhTien", DienTich * DonGia * SoThangThue);
                }
            }
            catch { }
            
        }

        private decimal calTienCoc()
        {
            gvChiTiet.FocusedRowHandle = -1;

            decimal TienCoc = 0;
            for (int i = 0; i < gvChiTiet.RowCount - 1; i++)
            {
                TienCoc += gvChiTiet.GetRowCellValue(i, "TienCoc") != null ? Convert.ToDecimal(gvChiTiet.GetRowCellValue(i, "TienCoc")) : Convert.ToDecimal(0);
            }
            return TienCoc;
        }

        private void spTienCoc_EditValueChanged(object sender, EventArgs e)
        {
            //\
            gvChiTiet.SetFocusedRowCellValue("TienCoc", ((SpinEdit)sender).Value);
            spTienDC.EditValue = calTienCoc() / Convert.ToDecimal(spTyGia.EditValue);
        }
  
        private void spDonGia_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("DonGia", ((SpinEdit)sender).Value);
            calThanhTien();
        }

        private void spDienTich_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("DienTich", ((SpinEdit)sender).Value);
            calThanhTien();
        }

        private void itemclose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void spHanDC_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? NgayDC = (DateTime?)dateNgayDC.EditValue;
            int HanDC = Convert.ToInt32(spHanDC.EditValue);
            dateNgayHH.EditValue = NgayDC.Value.AddHours(HanDC);
        }

        private void itemXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvChiTiet.DeleteSelectedRows();
        }

        private void glKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (IsView.GetValueOrDefault())
                return;

            ButtonEdit editor = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if(editor.Properties.Buttons.IndexOf(e.Button) == 1)
            {
                DichVu.KhachHang.frmEdit frm = new DichVu.KhachHang.frmEdit();
                frm.maTN = (byte)MaTN;
                frm.objnv = Common.User;
                frm.IsNCC = false;

                frm.ShowDialog();

                ReloadCustomer();
                
                try
                {
                    glKhachHang.EditValue = frm.objKH.MaKH;
                }
                catch { }
                
            }
            if (editor.Properties.Buttons.IndexOf(e.Button) == 2)
            {
                if (glKhachHang.EditValue == null)
                {
                    return;
                }

                DichVu.KhachHang.frmEdit frm = new DichVu.KhachHang.frmEdit()
                {
                    maTN = (byte)MaTN,
                    objnv = Common.User,
                    objKH = db.tnKhachHangs.Single(p => p.MaKH == (int)glKhachHang.EditValue)
                };
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    ReloadCustomer();

                    try
                    {
                        glKhachHang.EditValue = frm.objKH.MaKH;
                    }
                    catch { }
                }
            }

        }

        private void ReloadCustomer()
        {
            var db = new Library.MasterDataContext();
            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                 }).ToList();
            db.Dispose();
        }

        private void spSoThangThue_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("KyTT", ((SpinEdit)sender).Value);
            calThanhTien();
        }
    }
}