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
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;
//using DichVu.ChinhSach;

namespace DIPCRM.PriceAlert
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaKH { get; set; }
        public byte? MaTN { get; set; }
        public int? MaNC { get; set; }

        MasterDataContext db = new MasterDataContext();

        BaoGia objBG;
        ncNhuCau objNC;

        void TinhThanhTien(GridView gv)
        {
            var sl = (decimal?)gv.GetFocusedRowCellValue("SoLuong") ?? 0;
            var dg = (decimal?)gv.GetFocusedRowCellValue("DonGia") ?? 0;
            var tyleCK = (decimal?)gv.GetFocusedRowCellValue("TyLeCK") ?? 0;
            var thue = (decimal?)gv.GetFocusedRowCellValue("ThueGTGT") ?? 0;
            var tt = sl * dg;
            var tienCK = tt * tyleCK;
            var tienDaCK = tt - tienCK;
            var tienGTGT = tienDaCK * thue;
            var soTien = tienDaCK + tienGTGT;
            gv.SetFocusedRowCellValue("ThanhTien", tt);
            gv.SetFocusedRowCellValue("TienCK", tienCK);
            gv.SetFocusedRowCellValue("TienDaCK", tienDaCK);
            gv.SetFocusedRowCellValue("TienGTGT", tienGTGT);
            gv.SetFocusedRowCellValue("SoTien", soTien);
        }

        void DanhMuc_Load()
        {
            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                 });
            
            glkToaNha.DataSource = db.tnToaNhas;
            //glkMoTaPhong.DataSource = db.MoTaPhongs.Select(p => new { p.Ma_mtp, p.Ten_mtp }).ToList();

            glkToaNha1.LoadData();
            glkLoaiBaoGia1.LoadData();
            glkDonViTinh1.LoadData();
            glkDonViTinh_DatCoc.LoadData();
            ctlLoaiTienEdit1.LoadData();
            ctlNhanVienEdit1.LoadData();
            //ctlMauBaoGiaEdit1.LoadData();
            lookDVT.DataSource = db.DonViTinhs;
        }

        public frmEdit()
        {
            InitializeComponent();
            //ctlMauBaoGiaEdit1.EditValueChanged += new EventHandler(ctlMauBaoGiaEdit1_EditValueChanged);
            hplHistory.Click += new EventHandler(hplHistory_Click);
            spinSoLuong.EditValueChanged += new EventHandler(spinSoLuong_EditValueChanged);
            spinDonGia.EditValueChanged += new EventHandler(spinDonGia_EditValueChanged);
            spinThue.EditValueChanged += new EventHandler(spinThueGTGT_EditValueChanged);
            spinTyLeCK.EditValueChanged += new EventHandler(spinTyLeCK_EditValueChanged);
            spinTienCK.EditValueChanged += new EventHandler(spinTienCK_EditValueChanged);
            glkGhe.ButtonPressed += glkGhe_ButtonPressed;
        }

        void glkGhe_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChonPhongGhe(true);
        }

        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Rang buoc nhap lieu
            if (txtSoBG.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số báo giá");
                txtSoBG.Focus();
                return;
            }

            if (dateNgayBG.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập ngày báo giá");
                dateNgayBG.Focus();
                return;
            }

            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn đối tượng cần báo giá");
                return;
            }

            grvSanPham.RefreshData();
            if (grvSanPham.RowCount <= 1)
            {
                DialogBox.Error("Vui lòng nhập sản phẩm");
                return;
            }
            #endregion

            //bao gia
            objBG.SoBG = txtSoBG.Text;
            objBG.MaNC = (int?)glkCoHoi.EditValue;
            objBG.NgayBG = (DateTime?)dateNgayBG.EditValue;
            objBG.NgayYC = (DateTime?)dateNgayYC.EditValue;
            objBG.MaDA = (byte?)glkToaNha1.EditValue;
            objBG.MaNV = (int?)ctlNhanVienEdit1.EditValue;
            objBG.MaDVT = (int?)glkDonViTinh1.EditValue;
            objBG.SoThangDatCoc = spDatCoc.Value;
            objBG.DonViDatCoc = (int?)glkDonViTinh_DatCoc.EditValue;
            objBG.SoNgayHieuLuc = (int)spHieuLucBG.Value;
            objBG.DKTT = txtDKTT.Text;
            objBG.ThoiHanBG = spThoiHan.Value;
            objBG.MaLT = Convert.ToInt16(ctlLoaiTienEdit1.EditValue);
            objBG.MaLBG = (int?)glkLoaiBaoGia1.EditValue;
            //objBG.TieuDe = txtTieuDe.Text;
            objBG.GhiChu = txtGhiChu.Text;
            //objBG.MaBM = (int?)ctlMauBaoGiaEdit1.EditValue;
            objBG.MaKH = (int?)glKhachHang.EditValue;

           

            db.SubmitChanges();

            if (MaNC != null)
                Library.Utilities.NhuCauCls.TinhTiemNang(MaNC); 

            DialogResult = System.Windows.Forms.DialogResult.OK;
            
        }

        void grvSanPham_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        void grvSanPham_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var maSP = (int?)grvSanPham.GetRowCellValue(e.RowHandle, "MaSP");
            if (maSP == null)
            {
                e.ErrorText = "Vui lòng chọn sản phẩm";
                e.Valid = false;
                return;
            }

            var sl = (decimal?)grvSanPham.GetRowCellValue(e.RowHandle, "SoLuong");
            if (sl.GetValueOrDefault() <= 0)
            {
                e.ErrorText = "Vui lòng nhập số lượng";
                e.Valid = false;
            }

            var dg = (decimal?)grvSanPham.GetRowCellValue(e.RowHandle, "DonGia");
            if (dg.GetValueOrDefault() <= 0)
            {
                e.ErrorText = "Vui lòng nhập đơn giá";
                e.Valid = false;
            }
        }

        void grvSanPham_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc chắn không?") == System.Windows.Forms.DialogResult.No) return;
                grvSanPham.DeleteSelectedRows();
            }
        }

        void spinTienCK_EditValueChanged(object sender, EventArgs e)
        {
            var tienCK = ((SpinEdit)sender).Value;
            decimal tyLeCK = 0;
            if (tienCK > 0)
            {
                var tt = (decimal?)grvSanPham.GetFocusedRowCellValue("ThanhTien") ?? 0;
                tyLeCK = tienCK / tt;
            }
            grvSanPham.SetFocusedRowCellValue("TyLeCK", tyLeCK);
            TinhThanhTien(grvSanPham);
        }

        void spinTyLeCK_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("TyLeCK", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void spinThueGTGT_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("ThueGTGT", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void spinDonGia_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("DonGia", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void spinSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("SoLuong", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void hplHistory_Click(object sender, EventArgs e)
        {
            var maKH = (int?)glKhachHang.EditValue;
            if (maKH == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            using (var frm = new frmHistory())
            {
                frm.MaKH = (int)maKH;
                frm.ShowDialog(this);
            }
        }

        void ctlMauBaoGiaEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //if (ctlMauBaoGiaEdit1.EditValue == null) return;
            //var objBM = db.bgBieuMaus.Single(p => p.ID == (int?)ctlMauBaoGiaEdit1.EditValue);
            //ctlDuAnEdit1.EditValue = objBM.MaDA;
            //ctlNhanVienEdit1.EditValue = objBM.MaNV;
            //txtDKGH.EditValue = objBM.DKGH;
            //txtDKTT.EditValue = objBM.DKTT;
            //spThoiHan.EditValue = objBM.ThoiHanBG;
            //ctlLoaiTienEdit1.EditValue = objBM.MaLT;
            //txtTieuDe.EditValue = objBM.TieuDe;
            //txtGhiChu.EditValue = objBM.GhiChu;
            //
            grvSanPham.SelectAll();
            grvSanPham.DeleteSelectedRows();
            grvSanPham.RefreshData();
        }
     
        void frmEdit_Load(object sender, EventArgs e)
        {
            DanhMuc_Load();

            objBG = db.BaoGias.SingleOrDefault(p => p.ID == this.ID);

            objNC = db.ncNhuCaus.SingleOrDefault(o => o.MaNC == this.MaNC);

            if (objBG == null)
            {
                objBG = new BaoGia();
                objBG.NgayBG = DateTime.Now;
                objBG.NgayYC = DateTime.Now;
                objBG.MaNV = Common.User.MaNV;
                objBG.NgayNhap = DateTime.Now;
                objBG.MaNVN = Common.User.MaNV;
                objBG.MaLT = 1;
                ctlLoaiTienEdit1.EditValue = 1;
                if (objNC != null)
                {
                    this.MaTN = objNC.MaTN;
                    objBG.MaKH = objNC.MaKH;
                }
                objBG.MaTN = this.MaTN;
                db.BaoGias.InsertOnSubmit(objBG);
            }
            else
            {
                ctlLoaiTienEdit1.EditValue = (int)objBG.MaLT;
                objBG.MaNVS = Common.User.MaNV;
                objBG.NgaySua = DateTime.Now;
            }

            txtSoBG.EditValue = objBG.SoBG;
            glKhachHang.EditValue = objBG.MaKH;
            glkCoHoi.EditValue = objBG.MaNC;
            dateNgayBG.EditValue = objBG.NgayBG;
            dateNgayYC.EditValue = objBG.NgayYC;
            glkToaNha1.EditValue = objBG.MaDA;
            ctlNhanVienEdit1.EditValue = objBG.MaNV;
            glkDonViTinh1.EditValue = objBG.MaDVT;
            spDatCoc.EditValue = objBG.SoThangDatCoc;
            glkDonViTinh_DatCoc.EditValue = objBG.DonViDatCoc;
            spHieuLucBG.EditValue = objBG.SoNgayHieuLuc;
            glkLoaiBaoGia1.EditValue = objBG.MaLBG;
            txtDKTT.EditValue = objBG.DKTT;
            spThoiHan.EditValue = objBG.ThoiHanBG;
            
            txtGhiChu.EditValue = objBG.GhiChu;
            //ctlMauBaoGiaEdit1.EditValueChanged -= new EventHandler(ctlMauBaoGiaEdit1_EditValueChanged);
            //ctlMauBaoGiaEdit1.EditValue = objBG.MaBM;
            //ctlMauBaoGiaEdit1.EditValueChanged += new EventHandler(ctlMauBaoGiaEdit1_EditValueChanged);

            if (objNC != null)
                glkCoHoi.EditValue = objNC.MaNC;

            LoadPhong();

            //lkChinhSach_KyHD.DataSource = from p in db.kmChiTiet_CongThucs
            //                              join ct in db.kmChiTiets on p.MaKMCT equals ct.ID
            //                              join km in db.kmKhuyenMais on ct.MaKM equals km.ID
            //                              select new
            //                              {
            //                                  p.ID,
            //                                  TenCS = string.Format("[{0}] {1}", km.TenChinhSach, km.DienGiai),
            //                              };

            lkToaNha.DataSource = db.tnToaNhas.ToList();

            lkChinhSach_ThanhToan.DataSource = lkChinhSach_KyHD.DataSource;

            lkChinhSach_Khac.DataSource = lkChinhSach_KyHD.DataSource;

            lkPhong_MB.DataSource = db.mbMatBangs.Select(o => new { o.MaMB, o.MaSoMB }).ToList();
            lkGhe.DataSource = db.mbMatBangs.Select(o => new { o.MaMB, o.MaSoMB }).ToList();
            gcSanPham.DataSource = objBG.bgSanPhams;
            gcChiTietKM.DataSource = objBG.bgSanPhams;
            
            TaoSoCT();
        }

        void LoadPhong()
        {

            var ltGheDangThue = (from p in db.ctChiTiets
                                 join hd in db.ctHopDongs on p.MaHDCT equals hd.ID
                                 join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                 where SqlMethods.DateDiffDay(DateTime.Now, p.DenNgay) > 0
                                     //& hd.MaTN == this.MaTN
                                 & !p.NgungSuDung.GetValueOrDefault()
                                 select new
                                 {
                                     MaMB = p.MaMB ,
                                     p.MaHDCT,
                                     hd.SoHDCT,
                                     kh.KyHieu,
                                     hd.NgayHL,
                                     hd.NgayHH,
                                     p.ThanhTien,
                                     hd.ThoiHan,
                                 });

            var dsPhongGhes = (from mb in db.mbMatBangs
                               //join sgn in db.SoGheNgois on mb.idSoGheNgoi equals sgn.Ma_sgn into soghengoi
                               //from sgn in soghengoi.DefaultIfEmpty()
                               join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                               //join mtp in db.MoTaPhongs on mb.idMoTaPhong equals mtp.Ma_mtp into motaphong
                               //from mtp in motaphong.DefaultIfEmpty()
                               join thue in ltGheDangThue on mb.MaMB equals thue.MaMB into dangthue
                               from thue in dangthue.DefaultIfEmpty()
                               //where mb.MaTN == this.MaTN
                               //where !mb.IsNgungSuDung.GetValueOrDefault()
                               select new
                               {
                                   //mb.IsGhe,
                                   mb.MaSoMB,
                                   mb.MaMB,
                                   lmb.TenLMB,
                                   //mtp.Ten_mtp,
                                   //sgn.SoLuongGhe,
                                   thue.SoHDCT,
                                   thue.NgayHL,
                                   thue.NgayHH,
                                   thue.ThoiHan,
                               });

            //glkMatBang.DataSource = dsPhongGhes.Where(o => !o.IsGhe.GetValueOrDefault());
            //glkGhe.DataSource = dsPhongGhes.Where(o => o.IsGhe.GetValueOrDefault());
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            var maKH = (int?)glKhachHang.EditValue;

            if (maKH == null)
                return;

            glkCoHoi.Properties.DataSource = (from nc in db.ncNhuCaus
                                              where nc.MaKH == maKH
                                              select new 
                                              { 
                                                  nc.MaNC, 
                                                  nc.SoNC,
                                              }).ToList();
        }

        void LoadChiTietByMaNC(int? maNC)
        {
            if (maNC == null) return;

            objNC = db.ncNhuCaus.Single(p => p.MaNC == maNC);

            var details = objNC.ncSanPhams.Select(p => new bgSanPham
            {
                //MaMB = p.MaMB,
                MaDVT = (int)p.MaDVT.GetValueOrDefault(),
                SoLuong = p.SoLuong,
                DonGia = p.DonGia,
                ThanhTien = p.ThanhTien,
                TyLeCK = p.TyLeCK,
                TienCK = p.TienCK,
                TienDaCK = p.ThanhTien.GetValueOrDefault() - p.TienCK.GetValueOrDefault(),
                ThueGTGT = p.ThueGTGT,
                TienGTGT = p.TienGTGT,
                SoTien = p.SoTien,
            }).ToList();

            //objBG.bgSanPhams.Clear();
            objBG.bgSanPhams.AddRange(details);
            //grvSanPham.RefreshData();
            
        }

        private void ctlCoHoi_EditValueChanged(object sender, EventArgs e)
        {
            //LoadChiTietByMaNC((int?)glkCoHoi.EditValue);
            TaoSoCT();
        }

        void TaoSoCT()
        {
            if (objBG.ID == 0)
            {
                txtSoBG.Text = Library.Utilities.BaoGiaCls.TaoSoCT((int?)glkCoHoi.EditValue, this.MaTN);
            }
        }

        private void glkMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChonPhongGhe(false);
        }

        void ChonPhongGhe(bool isGhe)
        {
                var row = grvSanPham.GetFocusedRow() as bgSanPham;
                var MaMTP = grvSanPham.GetFocusedRowCellValue("idMoTaPhong") as string;

                var MaTN = (byte?)grvSanPham.GetFocusedRowCellValue("MaTN");

                if (MaTN == null || MaMTP == null)
                {
                    DialogBox.Error("Vui lòng chọn Tòa nhà và mô tả phòng");
                    return;
                }

                using (var frm = new frmChonPhongGhe(row.idMoTaPhong, row.MaTN.Value))
                {
                    frm.IsGhe = isGhe;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        grvSanPham.SetFocusedRowCellValue("MaMB", frm.maMB);
                        grvSanPham.SetFocusedRowCellValue("SoLuong", frm.soLuong);

                        SetGia();
                    }
                }
        }

        void SetGia()
        {
            //try
            //{
            //    var maKH = (int?)glKhachHang.EditValue;

            //    if (maKH == null)
            //    {
            //        DialogBox.Error("Vui lòng chọn khách hàng");
            //        return;
            //    }

            //    var MaMB = (int)grvSanPham.GetFocusedRowCellValue("MaMB");

            //    using (var frmGia = new frmGiaThue(dateNgayBG.DateTime, 2))
            //    {
            //        frmGia.MaTN = (byte?)grvSanPham.GetFocusedRowCellValue("MaTN");
            //        frmGia.MaMB = MaMB;
            //        frmGia.MaKH = maKH;
            //        if (frmGia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            grvSanPham.SetFocusedRowCellValue("DonGia", frmGia.objGia.DonGia);
            //            grvSanPham.SetFocusedRowCellValue("idkmGiaThue", frmGia.objGia.ID);
            //            grvSanPham.SetFocusedRowCellValue("MaDVT", frmGia.objGia.MaDVT);
            //            grvSanPham.SetFocusedRowCellValue("idkmKhuyenMai", frmGia.objGia.kmChiTiet.kmKhuyenMai.ID);
            //            grvSanPham.SetFocusedRowCellValue("ThueGTGT", frmGia.objGia.TyLeVAT);
            //            TinhThanhTien(grvSanPham);
            //        }
            //    }
            //}
            //catch
            //{
            //}
        }

        private List<int> ListMB = new List<int>();
        private void grvSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    var MaMB = (int?)grvSanPham.GetFocusedRowCellValue("MaMB");

                    if (MaMB != null)
                    {
                        ListMB.Add((int)MaMB);

                    }
                    grvSanPham.DeleteSelectedRows();
                }

                //SetTienCoc();
            }
        }

        private void lkChinhSach_ThanhToan_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //{
            //    if (e.Button.Index == 1)
            //    {
            //        #region Kiểm tra ràng buộc
            //        if (glKhachHang.EditValue == null)
            //        {
            //            DialogBox.Error("Vui lòng chọn <khách hàng>");
            //            return;
            //        }

            //        var maMB = (int?)gvChiTietKM.GetFocusedRowCellValue("MaMB");

            //        if (maMB == null)
            //        {
            //            maMB = (int?)gvChiTietKM.GetFocusedRowCellValue("MaGhe");
            //        }

            //        #endregion

            //        using (var frm = new frmKhuyenMai(dateNgayBG.DateTime))
            //        {
            //            frm.MaTN = (byte?)gvChiTietKM.GetFocusedRowCellValue("MaTN");
            //            frm.MaMB = maMB;
            //            frm.MaKH = (int?)glKhachHang.EditValue;
            //            frm.MaHinhThuc = 2;
            //            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //            {
            //                gvChiTietKM.SetFocusedRowCellValue("idkmChiTiet_CongThuc_ThanhToan", frm.objGia.ID);
            //                foreach (var item in frm.objGia.kmChiTiet_CongThucChiTiets)
            //                {
            //                    SetKhuyenMai(item);
            //                }

            //            }
            //        }
            //    }
            //}
        }

        private void lkChinhSach_Khac_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            //if (e.Button.Index == 1)
            //{
            //    if (e.Button.Index == 1)
            //    {
            //        #region Kiểm tra ràng buộc
            //        if (glKhachHang.EditValue == null)
            //        {
            //            DialogBox.Error("Vui lòng chọn <khách hàng>");
            //            return;
            //        }

            //        var maMB = (int?)gvChiTietKM.GetFocusedRowCellValue("MaMB");

            //        if (maMB == null)
            //        {
            //            maMB = (int?)gvChiTietKM.GetFocusedRowCellValue("MaGhe");
            //        }

            //        #endregion

            //        using (var frm = new frmKhuyenMai(dateNgayBG.DateTime))
            //        {
            //            frm.MaTN = (byte?)gvChiTietKM.GetFocusedRowCellValue("MaTN");
            //            frm.MaMB = maMB;
            //            frm.MaKH = (int?)glKhachHang.EditValue;
            //            frm.ThoiHanHD = spThoiHan.Value;

            //            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //            {


            //                var idkm_ThanhToan = (int?)gvChiTietKM.GetFocusedRowCellValue("idkmChiTiet_CongThuc_ThanhToan");
            //                var idkm_KyHD = (int?)gvChiTietKM.GetFocusedRowCellValue("idkmChiTiet_CongThuc_KyHD");

            //                if (frm.objGia.ID == idkm_ThanhToan | frm.objGia.ID == idkm_KyHD)
            //                {
            //                    DialogBox.Error("Trùng chương trình khuyến mãi. Vui lòng chọn chương trình khác");
            //                    return;
            //                }

            //                gvChiTietKM.SetFocusedRowCellValue("idkmChiTiet_CongThuc_Khac", frm.objGia.ID);

            //                foreach (var item in frm.objGia.kmChiTiet_CongThucChiTiets)
            //                {
            //                    SetKhuyenMai(item);
            //                }

            //            }
            //        }
            //    }
            //}
        }

        private void lkChinhSach_KyHD_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //{

            //    if (glKhachHang.EditValue == null)
            //    {
            //        DialogBox.Error("Vui lòng chọn <khách hàng>");
            //        return;
            //    }

            //    var maMB = (int?)gvChiTietKM.GetFocusedRowCellValue("MaMB");

            //    if (maMB == null)
            //    {
            //        maMB = (int?)gvChiTietKM.GetFocusedRowCellValue("MaGhe");
            //    }

            //    if (spThoiHan.Value == 0)
            //    {
            //        DialogBox.Error("Vui lòng nhập <Thời hạn>");
            //        return;
            //    }

            //    using (var frm = new frmKhuyenMai(dateNgayBG.DateTime))
            //    {
            //        frm.MaTN = (byte?)gvChiTietKM.GetFocusedRowCellValue("MaTN");
            //        frm.MaMB = maMB;
            //        frm.MaKH = (int?)glKhachHang.EditValue;
            //        frm.ThoiHanHD = spThoiHan.Value;
            //        frm.MaHinhThuc = 1;

            //        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            foreach (var item in frm.objGia.kmChiTiet_CongThucChiTiets)
            //            {
            //                gvChiTietKM.SetFocusedRowCellValue("idkmChiTiet_CongThuc_KyHD", frm.objGia.ID);

            //                SetKhuyenMai(item);
            //            }

            //        }
            //    }
            //}
        }

        //void SetKhuyenMai(kmChiTiet_CongThucChiTiet item)
        //{
        //    switch ((DonViTinhKM)item.MaDVT)
        //    {
        //        case DonViTinhKM.Thang_HopDong:
        //            DateTime TuNgay_KM, DenNgay_KM;
        //            TuNgay_KM = dateNgayBG.DateTime.AddMonths((int)spThoiHan.Value).AddDays(1);

        //            DenNgay_KM = HopDongCls.GetDenNgay(TuNgay_KM, item.SoLuong.GetValueOrDefault());

        //            gvChiTietKM.SetFocusedRowCellValue("TuNgay_KM", TuNgay_KM);
        //            gvChiTietKM.SetFocusedRowCellValue("DenNgay_KM", DenNgay_KM);
        //            gvChiTietKM.SetFocusedRowCellValue("SoThang_KM", item.SoLuong);
        //            break;
        //        case DonViTinhKM.PhanTram:
        //            gvChiTietKM.SetFocusedRowCellValue("TyLeCK_KM", item.SoLuong);
        //            break;
        //    }
        //}

        private void spinDonGia_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SetGia();
        }

        private void grvSanPham_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //grvSanPham.SetFocusedRowCellValue("ThueGTGT", 0.1);
        }
    }
}