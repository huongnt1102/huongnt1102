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
using DevExpress.XtraGrid.Views.Base;
using System.Data.Linq.SqlClient;

namespace DichVu.GiuXe
{
    public partial class frmCapThe : DevExpress.XtraEditors.XtraForm
    {
        public frmCapThe()
        {
            InitializeComponent();
        }

        public long? ID { get; set; }
        public byte? MaTN { get; set; }
        public int? MaThe { get; set; }

        int? MaDMGX;
        KhoThe_DSCapThe objGX; 
        MasterDataContext db;
        CachTinhCls objCT;

        string getNewMaTX()
        {
            string MaNK = "";
            db.txTheXe_getNewMaTX(ref MaNK);
            return db.DinhDang(6, int.Parse(MaNK));
        }

        void SetTienTT()
        {
            decimal _TienTT = 0;
            for (int i = 0; i < gvTheXe.RowCount; i++)
            {
                _TienTT += Convert.ToDecimal(gvTheXe.GetRowCellValue(i, "GiaThang") ?? 0);
            }

            //spTienTT.EditValue = spKyTT.Value * _TienTT;
        }


        decimal SetDonGia(int MaLX)
        {
            try
            {
                var _MaLX = MaLX;
                var _MaMB = (int?)glkMatBang.EditValue;

                var objCT = new CachTinhCls();
                objCT.MaTN = this.MaTN.Value;
                objCT.MaMB = _MaMB;
                objCT.MaLX = _MaLX;
                objCT.LoadDinhMuc();

                int _SoLuong = (from tx in db.dvgxTheXes where tx.MaTN == this.MaTN & tx.MaMB == _MaMB & tx.MaLX == _MaLX select tx).Count();

                if (this.ID == null)
                {
                    _SoLuong++;
                }

                var objGia = (from bg in objCT.ltBangGia
                              where bg.SoLuong <= _SoLuong
                              orderby bg.SoLuong descending
                              select bg).First();

                return objGia.DonGiaThang.GetValueOrDefault();
            }
            catch { return 0; }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            tabbedControlGroup1.SelectedTabPageIndex = 0;

            objCT = new CachTinhCls();

            gvTheXe.InvalidRowException += Common.InvalidRowException;
            gcTheXe.KeyUp += Common.GridViewKeyUp;

            db = new MasterDataContext();

            lkSoThe.DataSource = (from tx in db.dvgxTheXes
                                  where tx.MaTN == this.MaTN & !tx.IsSaoLuu.GetValueOrDefault()
                                  select new
                                  {
                                      tx.ID,
                                      tx.SoThe,
                                      TenLoaiThe =  tx.IsTheOto.GetValueOrDefault() ? "Thẻ ô tô"
                                                    : (tx.IsTheXe.GetValueOrDefault() && tx.IsThangMay.GetValueOrDefault()) ? "Thẻ tích hợp" : tx.IsTheXe.GetValueOrDefault() ? "Thẻ xe" : "Thẻ thang máy",
                                      LoaiThe =     tx.IsTheOto.GetValueOrDefault() ? 5
                                                   : (tx.IsTheXe.GetValueOrDefault() && tx.IsThangMay.GetValueOrDefault()) ? 3 : tx.IsTheXe.GetValueOrDefault() ? 2 : 1,
                                      TrangThai = tx.IsHongThe.GetValueOrDefault() ? "Thẻ Hỏng/Mất"
                                                                : tx.NgungSuDung.GetValueOrDefault() | tx.NgungSuDung == null ? "Ngưng SD" : "Đang SD",
                                  }).ToList();

             lkLoaiThe.DataSource = db.Kho_LoaiThes.ToList();

             lkLoaiXeGird.DataSource = (from l in db.dvgxLoaiXes
                                                                        where l.MaTN == this.MaTN
                                                                        orderby l.STT
                                                                        select new { l.MaLX, l.TenLX })
                                                                        .ToList();
            lkLoaiDK.DataSource = db.LoaiDangKies;
            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                where kn.MaTN == this.MaTN
                                                orderby mb.MaSoMB descending
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    kh.MaKH,
                                                    kh.KyHieu,
                                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                }).ToList();
            
            if (this.ID != null)
            {
                objGX = db.KhoThe_DSCapThes.Single(p => p.ID == this.ID);
                glkMatBang.EditValue = objGX.MaCH;
                lkNhanKhau.EditValue = objGX.MaNhanKhau;
                txtGhiChu.EditValue = objGX.GhiChu;
                txtEmail.EditValue = objGX.Email;
                txtSoPhieu.Text = objGX.SoCT;
                txtSDT.Text = objGX.SDT;
            }
            else
            {
                objGX = new KhoThe_DSCapThe();
                txtSoPhieu.Text = string.Format("{0}", db.CreateSoChungTu(36, MaTN));
                txtEmail.EditValue ="";
            }

            gcTheXe.DataSource = objGX.CapThe_ChiTiets;

            if (this.MaThe != null)
            {
                var tx = db.dvgxTheXes.Single(o => o.ID == this.MaThe);
                if (tx.NgungSuDung == null || tx.NgungSuDung == true)
                {
                    gvTheXe.AddNewRow();
                    gvTheXe.SetFocusedRowCellValue("MaLoaiDK", 1);
                    gvTheXe.SetFocusedRowCellValue("MaThe", this.MaThe);
                    if(tx.IsTheOto == true)
                        gvTheXe.SetFocusedRowCellValue("LoaiThe", 5);

                }
            }
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }


        public static T Clone<T>(T source)
        {
            var clone = (T)Activator.CreateInstance(typeof(T));
            var cols = typeof(T).GetProperties()
                .Select(p => new { Prop = p, Attr = (System.Data.Linq.Mapping.ColumnAttribute)p.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), true).SingleOrDefault() })
                .Where(p => p.Attr != null && !p.Attr.IsDbGenerated);
            foreach (var col in cols)
                col.Prop.SetValue(clone, col.Prop.GetValue(source, null), null);
            return clone;
        }


        int ResetThe(int? MaThe, int LoaiReset)
        {
            int _theID = 0;
            // Nếu hủy toàn bộ cả thẻ xe + thang máy Sao lưu lại 1 bản
            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == MaThe);
            tx.NgungSuDung = true;
            tx.NgayNgungSD = DateTime.Now;
            tx.IsSaoLuu = true; // Là bản sao lưu lại thông tin , Không sử dụng

            if (LoaiReset == 1) // Reset thẻ xe
            {
                // Tạo 1 thẻ rỗng mới
                var the = new dvgxTheXe();
                the.SoThe = tx.SoThe;
                the.MaTN = tx.MaTN;
                the.MaKH = tx.MaKH;
                the.MaMB = tx.MaMB;
                the.MaNVN = tx.MaNVN;
                the.NgayNhap = tx.NgayNhap;
                the.MaNVS = tx.MaNVS;
                the.NgaySua = tx.NgaySua;
                the.IsSaoLuu = false;
                the.IsTheXe = false;
                the.IsTheOto = tx.IsTheOto;
                the.IsThangMay = tx.IsThangMay;
                the.NgungSuDung = false;
                db.dvgxTheXes.InsertOnSubmit(the);
                db.SubmitChanges();

                tx.MaTheMoi = the.ID;

                // copy toàn bộ lịch sử giao dịch của thẻ sao lưu sang thẻ mới
                var ls = db.dvgxTheXe_LichSuCapNhats.Where(o => o.MaThe == tx.ID);
                foreach (var item in ls)
                    the.dvgxTheXe_LichSuCapNhats.Add(item);

                db.SubmitChanges();
                _theID = the.ID;
            }

            if (LoaiReset == 3) // Reset Tất cả
            {

                // Tạo 1 thẻ rỗng mới
                var the = new dvgxTheXe();
                the.SoThe = tx.SoThe;
                the.MaTN = tx.MaTN;
                the.NgayNhap = tx.NgayNhap;
                the.MaNVN = tx.MaNVN;
                the.NgaySua = tx.NgaySua;
                tx.MaNVS = tx.MaNVS;
                the.IsSaoLuu = false;
                the.IsTheOto = tx.IsTheOto;
                db.dvgxTheXes.InsertOnSubmit(the);
                db.SubmitChanges();

                tx.MaTheMoi = the.ID;
                // chuyển toàn bộ lịch sử giao dịch của thẻ sao lưu sang thẻ mới
                var ls = db.dvgxTheXe_LichSuCapNhats.Where(o => o.MaThe == tx.ID);
                foreach (var item in tx.dvgxTheXe_LichSuCapNhats)
                    item.MaThe = the.ID;

                db.SubmitChanges();
                _theID = the.ID;
            }

            return _theID;
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                   var _MaMB = (int)glkMatBang.EditValue;

                //    objCT.MaMB = _MaMB;
                //    objCT.MaTN = (byte)MaTN;

                    objGX.MaCH = (int?)glkMatBang.EditValue;
                    objGX.MaKH = (int?)type.GetProperty("MaKH").GetValue(r, null);

                    lkTheNgungSD.DataSource = (from tx in db.dvgxTheXes
                                               where tx.MaMB == (int?)type.GetProperty("MaMB").GetValue(r, null)
                                               & (tx.IsTheXe.GetValueOrDefault() | tx.IsThangMay.GetValueOrDefault() | !tx.NgungSuDung.GetValueOrDefault())
                                               & !tx.IsHongThe.GetValueOrDefault()
                                               & !tx.IsSaoLuu.GetValueOrDefault()
                                               select new
                                               {
                                                   tx.ID,
                                                   tx.SoThe,
                                                   TenLoaiThe = tx.IsTheOto.GetValueOrDefault() ? "Thẻ ô tô"
                                                    : (tx.IsTheXe.GetValueOrDefault() && tx.IsThangMay.GetValueOrDefault()) ? "Thẻ tích hợp" : tx.IsTheXe.GetValueOrDefault() ? "Thẻ xe" : "Thẻ thang máy",
                                                   LoaiThe = tx.IsTheOto.GetValueOrDefault() ? 5
                                                                : (tx.IsTheXe.GetValueOrDefault() && tx.IsThangMay.GetValueOrDefault()) ? 3 : tx.IsTheXe.GetValueOrDefault() ? 2 : 1,
                                                   TrangThai =  tx.IsHongThe.GetValueOrDefault() ? "Hỏng thẻ"
                                                                : tx.NgungSuDung.GetValueOrDefault() | tx.NgungSuDung == null ? "Ngưng SD" : "Đang SD",
                                               });

                    lkNhanKhau.Properties.DataSource = db.tnNhanKhaus.Where(o=>o.MaMB == _MaMB).ToList();

                        var objMB = (from p in db.mbMatBangs
                                     where p.MaMB == _MaMB
                                     select new
                                     {
                                         TenKH = p.tnKhachHang == null ? "" : ((bool)p.tnKhachHang.IsCaNhan ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen),
                                     }).FirstOrDefault();
                        txtChuHo.Text = objMB.TenKH;
                }
            }
            catch { }
        }

        int? BackupThe(int? MaThe)
        {
            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == MaThe);
            var bak = new dvgxTheXe_Backup();
            bak.MaTN = tx.MaTN;
            bak.MaKH = tx.MaKH;
            bak.MaMB = tx.MaMB;
            bak.MaNK = tx.MaNK;
            bak.MaGX = tx.MaGX;
            bak.SoThe = tx.SoThe;
            bak.NgayDK = tx.NgayDK;
            bak.ChuThe = tx.ChuThe;
            bak.MaLX = tx.MaLX;
            bak.BienSo = tx.BienSo;
            bak.DoiXe = tx.DoiXe;
            bak.MauXe = tx.MauXe;
            bak.PhiLamThe = tx.PhiLamThe;
            bak.GiaNgay = tx.GiaNgay;
            bak.GiaThang = tx.GiaThang;
            bak.NgayTT = tx.NgayTT;
            bak.KyTT = tx.KyTT;
            bak.TienTT = tx.TienTT;
            bak.NgungSuDung = tx.NgungSuDung;
            bak.DienGiai = tx.DienGiai;
            bak.MaDM = tx.MaDM;
            bak.MaNVN = tx.MaNVN;
            bak.NgayNhap = tx.NgayNhap;
            bak.MaNVS = tx.MaNVS;
            bak.NgaySua = tx.NgaySua;
            bak.NgayNgungSD = tx.NgayNgungSD;
            bak.MaKhoThe = tx.MaKhoThe;
            bak.IsTheXe = tx.IsTheXe;
            bak.IsThangMay = tx.IsThangMay;
            bak.GhiChu = tx.GhiChu;
            bak.IsTheOto = tx.IsTheOto;
            bak.IsHongThe = tx.IsHongThe;
            bak.LyDo = tx.LyDo;
            bak.IsSaoLuu = tx.IsSaoLuu;
            db.dvgxTheXe_Backups.InsertOnSubmit(bak);
            db.SubmitChanges();
            return bak.ID;
        }

        dvgxTheXe_LichSuCapNhat SaveHistory(int? _maThe,string _noidung)
        {
            var ls = new dvgxTheXe_LichSuCapNhat();
            ls.KhoThe_DSCapThe = objGX;
            ls.MaThe = _maThe;
            ls.MaMB = (int?)glkMatBang.EditValue;
            ls.MaKH = objGX.MaKH;
            ls.NgaySua = DateTime.Now;
            ls.NoiDung = _noidung;
            ls.NguoiSua = Common.User.MaNV;
            return ls;
        }


        #region Nut xu ly luu, huy
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                glkMatBang.Focus();
                return;
            }

            if (txtEmail.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập Email");
                txtEmail.Focus();
                return;
            }

            gvTheXe.UpdateCurrentRow();
            if (gvTheXe.RowCount < 1)
            {
                DialogBox.Error("Vui lòng nhập thẻ xe");
                return;
            }
            #endregion

            try
            {
                var _maMB = (int?)glkMatBang.EditValue;
                string NoiDung = "";
                db.SubmitChanges();
                if (objGX.ID == 0)
                {
                    objGX.NgayNhap = db.GetSystemDate();
                    objGX.NguoiNhap = Common.User.MaNV;
                    db.KhoThe_DSCapThes.InsertOnSubmit(objGX);

                    //Update the xe
                    foreach (var item in objGX.CapThe_ChiTiets)
                    {
                        //// Sao luu The truoc khi thuc hien
                        if (item.MaTheCu != null)
                            item.TheCuBackupID = BackupThe(item.MaTheCu);

                        if (item.MaThe != null)
                            item.TheMoiBackupID = BackupThe(item.MaThe);

                        // Chức năng tích hợp thẻ
                        if (item.MaLoaiDK == 2)
                        {
                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaTheCu);

                            tx.MaLX = item.MaLX;
                            tx.BienSo = item.BienSo;
                            tx.MauXe = item.MauSac;
                            tx.ChuThe = item.ChuThe;
                            tx.DoiXe = item.HangXe;
                            tx.NgayDK = DateTime.Now;
                            tx.MaKH = objGX.MaKH;
                            tx.MaMB = _maMB;
                            tx.DienGiai = string.Format("Phí giữ xe biển số {0}", tx.BienSo);
                            tx.GiaThang = item.PhiGiuXe;
                            tx.KyTT = item.KyTT;
                            tx.TienTT = item.TienTT;
                            tx.NgayTT = item.NgayTT;
                            tx.IsTheXe = true;
                            tx.IsThangMay = true;
                            tx.NgungSuDung = false;
                            tx.NgayNgungSD = null;

                            // Lưu lại lịch sử thẻ xe
                            NoiDung = "Tích hợp thẻ";
                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(tx.ID, NoiDung));
                        }

                            if (item.MaLoaiDK == 5) // Chức năng đổi thẻ
                            {
                                var thecu = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaTheCu);
                                // Cấp thẻ mới
                                var moi = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaThe);
                                moi.MaLX = item.MaLX;
                                moi.BienSo = item.BienSo;
                                moi.MauXe = item.MauSac;
                                moi.ChuThe = item.ChuThe;
                                moi.DoiXe = item.HangXe;
                                moi.NgayDK = DateTime.Now;
                                moi.MaKH = objGX.MaKH;
                                moi.MaMB = _maMB;
                                moi.DienGiai = string.Format("Phí giữ xe biển số {0}", moi.BienSo);
                                moi.GiaThang = item.PhiGiuXe;
                                moi.KyTT = item.KyTT;
                                moi.TienTT = item.TienTT;
                                moi.NgayTT = item.NgayTT;
                                moi.NgungSuDung = false;
                                moi.NgayNgungSD = null;
                                moi.IsThangMay = thecu.IsThangMay;
                                moi.IsTheXe = thecu.IsTheXe;

                                //1. Ngưng sử dụng thẻ cũ
                                var _maThe = ResetThe(item.MaTheCu,3);

                                // Lưu lại lịch sử thẻ xe
                                var ls = new dvgxTheXe_LichSuCapNhat();
                                ls.KhoThe_DSCapThe = objGX;
                                ls.MaThe = _maThe;
                                ls.MaMB = _maMB;
                                ls.MaKH = objGX.MaKH;
                                ls.NgaySua = DateTime.Now;
                                ls.NoiDung = String.Format("Đổi lại thẻ xe cũ {0} sang thẻ mới {1}", thecu.SoThe, moi.SoThe);
                                ls.NguoiSua = Common.User.MaNV;
                                db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(ls);

                                // Lưu lịch sử thẻ mới
                                var ls2 = Clone(ls);
                                ls.MaThe = moi.ID;
                                db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(ls2);
                            }
                            else // chức năng khác
                            {
                            switch (item.LoaiThe)
                            {
                                case 1: // thẻ thang máy
                                    {
                                        #region chức năng hủy thẻ
                                        if (item.MaLoaiDK == 3)
                                        {

                                            //1. Ngưng sử dụng thẻ
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaTheCu);

                                            var _maThe = tx.ID;
                                            if (!tx.IsTheXe.GetValueOrDefault())
                                                _maThe = ResetThe(tx.ID, 3);
                                            else
                                            {
                                                
                                                tx.IsThangMay = false;
                                                // Sao lưu thẻ thang máy
                                                var tm = new dvgxTheXe();
                                                tm.IsSaoLuu = true;
                                                tm.IsThangMay = true;
                                                tm.MaMB = tx.MaMB;
                                                tm.MaTN = tx.MaTN;
                                                tm.MaKH = tx.MaKH;
                                                tm.NgayNhap = tx.NgayNhap;
                                                tm.MaNVN = tx.MaNVN;
                                                tm.NgaySua = tx.NgaySua;
                                                tm.MaNVS = tm.MaNVS;
                                                tm.NgungSuDung = true;
                                                tm.NgayNgungSD = DateTime.Now;
                                                tm.SoThe = tx.SoThe;
                                                tm.ChuThe = tx.ChuThe;
                                                tm.KhoThe_DSCapThe = objGX;
                                                db.dvgxTheXes.InsertOnSubmit(tm);
                                                db.SubmitChanges();
                                                _maThe = tm.ID;
                                            }

                                            // Lưu lại lịch sử
                                            NoiDung = "Hủy thẻ thang máy";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(_maThe, NoiDung));


                                        }
                                        #endregion

                                        #region Chức năng cấp mới => OK
                                        if (item.MaLoaiDK == 1| item.MaLoaiDK == 4) // cấp mới - bàn giao
                                        {
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaThe);
                                            // Cập nhật thông tin thẻ 
                                            tx.ChuThe = item.ChuThe;
                                            tx.NgayDK = DateTime.Now;
                                            tx.MaKH = objGX.MaKH;
                                            tx.MaMB = _maMB;
                                            tx.IsThangMay = true;
                                            tx.MaNVN = Common.User.MaNV;
                                            tx.NgayNhap = DateTime.Now;
                                            tx.NgungSuDung = false;
                                            tx.NgayNgungSD = null;

                                            // Lưu lại lịch sử
                                            NoiDung = "Cấp mới thẻ thang máy";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(tx.ID,NoiDung));
                                        }
                                        #endregion

                                    }
                                    break;
                                case 2: // thẻ xe
                                    {
                                        #region chức năng hủy thẻ
                                        if (item.MaLoaiDK == 3)
                                        {
                                            //1. Ngưng sử dụng thẻ xe
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaTheCu);
                                            var _maThe = tx.ID;

                                            if (!tx.IsThangMay.GetValueOrDefault()) // Kiểm tra có reset thẻ hoàn toàn
                                                 _maThe = ResetThe(tx.ID,3);
                                            else
                                                _maThe = ResetThe(tx.ID, 1);

                                            // Lưu lại lịch sử
                                            NoiDung = "Hủy thẻ xe";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(_maThe, NoiDung));
                                        }
                                        #endregion

                                        #region Cấp mới
                                        if (item.MaLoaiDK == 1 | item.MaLoaiDK == 4) // cấp mới - bàn giao
                                        {
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaThe);
                                            // Cập nhật thông tin thẻ 
                                            tx.MaLX = item.MaLX;
                                            tx.BienSo = item.BienSo;
                                            tx.MauXe = item.MauSac;
                                            tx.ChuThe = item.ChuThe;
                                            tx.DoiXe = item.HangXe;
                                            tx.NgayDK = DateTime.Now;
                                            tx.MaKH = objGX.MaKH;
                                            tx.MaMB = _maMB;
                                            tx.DienGiai = string.Format("Phí giữ xe biển số {0}", tx.BienSo);
                                            tx.GiaThang = item.PhiGiuXe;
                                            tx.KyTT = item.KyTT;
                                            tx.TienTT = item.TienTT;
                                            tx.NgayTT = item.NgayTT;
                                            tx.IsTheXe = true;
                                            tx.MaNVN = Common.User.MaNV;
                                            tx.NgayNhap = DateTime.Now;
                                            tx.NgungSuDung = false;
                                            tx.NgayNgungSD = null;

                                            // Lưu lại lịch sử
                                            NoiDung = "Cấp mới thẻ xe";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(tx.ID, NoiDung));
                                        }
                                        #endregion
                                    }
                                    break;
                                case 3: // thẻ tích hợp
                                    {
                                        if (item.MaLoaiDK == 3)
                                        {
                                            ResetThe(item.MaTheCu,3);
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaTheCu);

                                            // Lưu lại lịch sử
                                            NoiDung = "Hủy thẻ tich hợp";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(tx.ID, NoiDung));
                                        }



                                        #region Chức năng cấp mới => OK
                                        if (item.MaLoaiDK == 1 | item.MaLoaiDK == 4) // cấp mới - bàn giao
                                        {
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaThe);
                                            // Cập nhật thông tin thẻ 
                                            tx.MaLX = item.MaLX;
                                            tx.BienSo = item.BienSo;
                                            tx.MauXe = item.MauSac;
                                            tx.DoiXe = item.HangXe;
                                            tx.DienGiai = string.Format("Phí giữ xe biển số {0}", tx.BienSo);
                                            tx.GiaThang = item.PhiGiuXe;
                                            tx.KyTT = item.KyTT;
                                            tx.TienTT = item.TienTT;
                                            tx.NgayTT = item.NgayTT;
                                            tx.ChuThe = item.ChuThe;
                                            tx.NgayDK = DateTime.Now;
                                            tx.MaKH = objGX.MaKH;
                                            tx.MaMB = _maMB;
                                            tx.IsThangMay = true;
                                            tx.IsTheXe = true;
                                            tx.NgungSuDung = false;
                                            tx.NgayNgungSD = null;

                                            // Lưu lại lịch sử
                                            NoiDung = "Cấp mới thẻ tích hợp";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(tx.ID, NoiDung));
                                        }
                                        #endregion
                                    }
                                    break;
                                case 5: // Thẻ ô tô
                                    {
                                        #region Cấp mới
                                        if (item.MaLoaiDK == 1 | item.MaLoaiDK == 4) // cấp mới - bàn giao
                                        {
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaThe);
                                            // Cập nhật thông tin thẻ 
                                            tx.MaLX = item.MaLX;
                                            tx.BienSo = item.BienSo;
                                            tx.MauXe = item.MauSac;
                                            tx.ChuThe = item.ChuThe;
                                            tx.DoiXe = item.HangXe;
                                            tx.NgayDK = DateTime.Now;
                                            tx.MaKH = objGX.MaKH;
                                            tx.MaMB = _maMB;
                                            tx.DienGiai = string.Format("Phí giữ xe biển số {0}", tx.BienSo);
                                            tx.GiaThang = item.PhiGiuXe;
                                            tx.KyTT = item.KyTT;
                                            tx.TienTT = item.TienTT;
                                            tx.NgayTT = item.NgayTT;
                                            tx.IsTheXe = true;
                                            tx.MaNVN = Common.User.MaNV;
                                            tx.NgayNhap = DateTime.Now;
                                            tx.NgungSuDung = false;
                                            tx.NgayNgungSD = null;

                                            // Lưu lại lịch sử
                                            NoiDung = "Cấp mới thẻ";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(tx.ID, NoiDung));
                                        }
                                        #endregion

                                        #region chức năng hủy thẻ
                                        if (item.MaLoaiDK == 3)
                                        {
                                            //1. Ngưng sử dụng thẻ
                                            var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == item.MaTheCu);
                                               var _maThe = ResetThe(tx.ID,3);

                                            //// Lưu lại lịch sử
                                            //var ls = new dvgxTheXe_LichSuCapNhat();
                                            //ls.KhoThe_DSCapThe = objGX;
                                            //ls.MaThe = _maThe;
                                            //ls.MaMB = _maMB;
                                            //ls.MaKH = objGX.MaKH;
                                            //ls.NgaySua = DateTime.Now;
                                            //ls.NoiDung = "Hủy thẻ";
                                            //ls.NguoiSua = Common.User.MaNV;
                                            //db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(ls);

                                            NoiDung = "Hủy thẻ";
                                            db.dvgxTheXe_LichSuCapNhats.InsertOnSubmit(SaveHistory(_maThe, NoiDung));
                                        }
                                        #endregion
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    objGX.NgaySua = db.GetSystemDate();
                    objGX.NguoiSua = Common.User.MaNV;
                }

                objGX.MaNhanKhau = (int?)lkNhanKhau.EditValue;
                objGX.Email = txtEmail.Text;
                // Kiểm tra nếu trùng tạo số thẻ mới

                while (db.KhoThe_DSCapThes.Any(o => o.SoCT == txtSoPhieu.Text.Trim() & o.MaTN == this.MaTN & o.ID != objGX.ID))
                    txtSoPhieu.Text = string.Format("{0}", db.CreateSoChungTu(36, MaTN));
                objGX.MaTN = this.MaTN;
                objGX.GhiChu = txtGhiChu.Text;
                objGX.SoCT = txtSoPhieu.Text;
                objGX.MaMB = (int?)glkMatBang.EditValue;
                objGX.SDT = txtSDT.Text;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void lkLoaiXe_EditValueChanged(object sender, EventArgs e)
        {
            //objCT.MaLX = (int)lkLoaiXe.EditValue;
        }

        private void ckbNgungSuDung_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvTheXe.RowCount; i++)
            {
              //  gvTheXe.SetRowCellValue(i, "NgungSuDung", ckbNgungSuDung.Checked);
            }

            gvTheXe.UpdateCurrentRow();
        }

        private void gvTheXe_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvTheXe.SetRowCellValue(e.RowHandle,"KyTT",1);
            gvTheXe.SetRowCellValue(e.RowHandle, "NgayTT", DateTime.Now);
        }

        private void gvTheXe_RowUpdated(object sender, RowObjectEventArgs e)
        {
            this.SetTienTT();
        }

        private void spKyTT_EditValueChanged(object sender, EventArgs e)
        {
            this.SetTienTT();
        }

        void ReadOnlyColumm(bool value)
        {
            colBienSo.OptionsColumn.ReadOnly = value;
            colMauXe.OptionsColumn.ReadOnly = value;
            colLoaiXe.OptionsColumn.ReadOnly = value;
            colHangXe.OptionsColumn.ReadOnly = value;
        }

        private void gvTheXe_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var loaiDK = (int?)gvTheXe.GetFocusedRowCellValue("MaLoaiDK");
            switch (loaiDK)
            {
                case 1: // Thẻ mới
                    colTheMoi.OptionsColumn.ReadOnly = false;
                    colTheCu.OptionsColumn.ReadOnly = true;
                    gvTheXe.SetFocusedRowCellValue("MaTheCu", null);
                    break;
                case 2: // Tích hơp thẻ
                    colTheMoi.OptionsColumn.ReadOnly = true;
                    colTheCu.OptionsColumn.ReadOnly = false;
                    break;
                case 3: // Hủy thẻ
                    colTheMoi.OptionsColumn.ReadOnly = true;
                    colTheCu.OptionsColumn.ReadOnly = false;
                    gvTheXe.SetFocusedRowCellValue("MaThe", null);
                    break;
                case 5: // Đổi thẻ
                    colTheMoi.OptionsColumn.ReadOnly = false;
                    colTheCu.OptionsColumn.ReadOnly = false;
                    break;
            }
        }

        private void gvTheXe_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
                var loaiDK = (int?)gvTheXe.GetFocusedRowCellValue("MaLoaiDK");
                switch (loaiDK)
                {
                    case 1: // Thẻ mới
                        colTheMoi.OptionsColumn.ReadOnly = false;
                        colTheCu.OptionsColumn.ReadOnly = true;
                        gvTheXe.SetFocusedRowCellValue("MaTheCu", null);
                        break;
                    case 2: // Tích hơp thẻ
                        colTheMoi.OptionsColumn.ReadOnly = true;
                        colTheCu.OptionsColumn.ReadOnly = false;
                        break;
                    case 3: // Hủy thẻ
                        colTheMoi.OptionsColumn.ReadOnly = true;
                        colTheCu.OptionsColumn.ReadOnly = false;
                        gvTheXe.SetFocusedRowCellValue("MaThe", null);
                        break;
                    case 5: // Đổi thẻ
                        colTheMoi.OptionsColumn.ReadOnly = false;
                        colTheCu.OptionsColumn.ReadOnly = false;
                        break;
                }
        }

        private void gvTheXe_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvTheXe_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            // Kiểm nhập mới, Thẻ tích hợp , đổi thẻ
            var loaiDK = (int?)gvTheXe.GetFocusedRowCellValue("MaLoaiDK");
            if (loaiDK == null)
            {
                e.ErrorText = "Vui lòng chọn loại đăng ký";
                e.Valid = false;
            }


            if (loaiDK == 5 || loaiDK == 3 || loaiDK == 2) // Hủy thẻ - đổi thẻ - Tích hợp thẻ
            {
                var thecu = (int?)gvTheXe.GetFocusedRowCellValue("MaTheCu");
                if (thecu == null)
                {
                    e.ErrorText = "Vui lòng nhập <Thẻ ngừng sử dụng/Tích hợp>";
                    e.Valid = false;
                    return;
                }
            }

            #region Ràng buộc Đổi thẻ - Thêm mới thẻ - Tích hợp thẻ
            if (loaiDK == 1 | loaiDK == 5 | loaiDK == 2)
            {
                var loaithe = (int?)gvTheXe.GetFocusedRowCellValue("LoaiThe");
                if (loaiDK!= 5 & loaithe == null)
                {
                    e.ErrorText = "Vui lòng chọn loại thẻ";
                    e.Valid = false;
                    return;
                }

                var loaixe = (int?)gvTheXe.GetFocusedRowCellValue("MaLX");
                if (loaithe == 2 || loaithe == 3 || loaithe == 5)
                {
                    if (loaixe == null)
                    {
                        e.ErrorText = "Vui lòng chọn loại xe";
                        e.Valid = false;
                        return;
                    }


                    if (loaithe == 5 & loaixe != 25 & loaixe != 65 & loaixe != 91)
                    {
                        e.ErrorText = "loại thẻ này chỉ dành cho ô tô. Vui lòng chọn lại";
                        e.Valid = false;
                        return;
                    }

                    var bienso = gvTheXe.GetFocusedRowCellValue("BienSo");
                    if (bienso == null || bienso.ToString().Trim() == "")
                    {
                        e.ErrorText = "Vui lòng nhập <Biển số>";
                        e.Valid = false;
                        return;
                    }

                    var phithang = (decimal?)gvTheXe.GetFocusedRowCellValue("PhiGiuXe");
                    if (phithang == null)
                    {
                        e.ErrorText = "Vui lòng nhập phí tháng";
                        e.Valid = false;
                        return;
                    }

                    var NgayTT = (DateTime?)gvTheXe.GetFocusedRowCellValue("NgayTT");
                    if (NgayTT == null)
                    {
                        e.ErrorText = "Vui lòng nhập <Ngày thanh toán>";
                        e.Valid = false;
                        return;
                    }

                }

                var sothe = (int?)gvTheXe.GetFocusedRowCellValue("MaThe");
                if (sothe == null & loaiDK != 2) // Tích hơp thẻ không cần chọn thẻ đăng ký mới
                {
                    e.ErrorText = "Vui lòng chon <Thẻ đăng ký mới>";
                    e.Valid = false;
                    return;
                }
            }
            #endregion
        }

        private void gvTheXe_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void lkTheNgungSD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lk = (LookUpEdit)sender;
            if(lk.GetColumnValue("TrangThai").ToString() == "Ngưng SD")
            {
                DialogBox.Error("Thẻ này đã ngưng sử dụng");
                gvTheXe.SetFocusedRowCellValue("MaTheCu", (int?)gvTheXe.GetFocusedRowCellValue("MaTheCu"));
                return;
            }

            if (lk.GetColumnValue("TrangThai").ToString() == "Thẻ Hỏng/Mất")
            {
                DialogBox.Error("Thẻ này đã hỏng/mất. Không thể chọn");
                gvTheXe.SetFocusedRowCellValue("MaTheCu", (int?)gvTheXe.GetFocusedRowCellValue("MaTheCu"));
            }

                var loaidk = (int?)gvTheXe.GetFocusedRowCellValue("MaLoaiDK");


                var LoaiThe = (int?)lk.GetColumnValue("LoaiThe");

                if (loaidk == 2 & LoaiThe == 3)
                {
                    DialogBox.Error("Thẻ này đã là thẻ tích hợp. Không thể tích hợp");
                    gvTheXe.SetFocusedRowCellValue("MaTheCu", (int?)gvTheXe.GetFocusedRowCellValue("MaTheCu"));
                    return;
                }


                if (loaidk == 2 & LoaiThe == 5) // nếu chọn thẻ ô tô để tích hợp=> báo lỗi
                {
                    DialogBox.Error("Thẻ ô tô không thể tích hợp");
                    gvTheXe.SetFocusedRowCellValue("MaTheCu", (int?)gvTheXe.GetFocusedRowCellValue("MaTheCu"));
                    return;
                }
                            if(loaidk == 2)
                                gvTheXe.SetFocusedRowCellValue("LoaiThe", 3);
                            else
                                gvTheXe.SetFocusedRowCellValue("LoaiThe", LoaiThe);

                            var the = db.dvgxTheXes.FirstOrDefault(o => o.ID == (int?)lk.EditValue);
                            gvTheXe.SetFocusedRowCellValue("BienSo", the.BienSo);
                            gvTheXe.SetFocusedRowCellValue("MauSac", the.MauXe);
                            gvTheXe.SetFocusedRowCellValue("ChuThe", the.ChuThe);
                            gvTheXe.SetFocusedRowCellValue("MaLX", the.MaLX);
                            gvTheXe.SetFocusedRowCellValue("HangXe", the.DoiXe);
                            gvTheXe.SetFocusedRowCellValue("PhiGiuXe", the.GiaThang);
                            gvTheXe.SetFocusedRowCellValue("KyTT", the.KyTT);
                            gvTheXe.SetFocusedRowCellValue("TienTT", the.TienTT);
                            gvTheXe.SetFocusedRowCellValue("NgayTT", the.NgayTT);        

        }

        private void repositoryItemSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit sp = (SpinEdit)sender;
            var phithang = (decimal?)gvTheXe.GetFocusedRowCellValue("PhiGiuXe");
            var kyTT = (int?)sp.Value;
            var TienTT = phithang.GetValueOrDefault() * kyTT.GetValueOrDefault();
            gvTheXe.SetFocusedRowCellValue("TienTT", TienTT);
        }

        private void lkSoThe_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit lk = (GridLookUpEdit)sender;
            var r = lk.Properties.GetRowByKeyValue(lk.EditValue);
            var loaidk = (int?)gvTheXe.GetFocusedRowCellValue("MaLoaiDK");
            if (r != null)
            {
                var type = r.GetType();
                if ((string)type.GetProperty("TrangThai").GetValue(r, null) == "Đang SD" & loaidk != 2)
                {
                    DialogBox.Error("Thẻ này đang sử dụng. Không thể chọn");
                    gvTheXe.SetFocusedRowCellValue("MaThe", (int?)gvTheXe.GetFocusedRowCellValue("MaThe"));
                    return;
                }

                if ((string)type.GetProperty("TrangThai").GetValue(r, null) == "Thẻ Hỏng/Mất")
                {
                    DialogBox.Error("Thẻ này đã hỏng/mất. Không thể chọn");
                    gvTheXe.SetFocusedRowCellValue("MaThe", (int?)gvTheXe.GetFocusedRowCellValue("MaThe"));
                    return;
                }

                var LoaiThe = (int?)type.GetProperty("LoaiThe").GetValue(r, null);

                // Kiểm tra nếu thẻ đã được chọn rồi 
                for (int i = 0; i < gvTheXe.RowCount-1; i++)
                {
                    var thexe = (int?)gvTheXe.GetRowCellValue(i, "MaThe");
                    if (thexe == (int?)lk.EditValue)
                    {
                        DialogBox.Error("Thẻ này đã được chọn vui lòng chọn thẻ khác");
                        gvTheXe.SetFocusedRowCellValue("MaThe", gvTheXe.GetFocusedRowCellValue("MaThe"));
                        return;
                    }
                }

                if (loaidk == 1 & LoaiThe == 5)
                {
                    gvTheXe.SetFocusedRowCellValue("LoaiThe", LoaiThe);
                }

                //var the = db.dvgxTheXes.FirstOrDefault(o => o.ID == (int?)lk.EditValue);
                //gvTheXe.SetFocusedRowCellValue("BienSo", the.BienSo);
                //gvTheXe.SetFocusedRowCellValue("MauSac", the.MauXe);
                //gvTheXe.SetFocusedRowCellValue("ChuThe", the.ChuThe);
                //gvTheXe.SetFocusedRowCellValue("MaLX", the.MaLX);
                //gvTheXe.SetFocusedRowCellValue("HangXe", the.DoiXe);
                //gvTheXe.SetFocusedRowCellValue("PhiGiuXe", the.GiaThang);
                //gvTheXe.SetFocusedRowCellValue("KyTT", the.KyTT?? 1);
                //gvTheXe.SetFocusedRowCellValue("TienTT", the.TienTT);
                //gvTheXe.SetFocusedRowCellValue("NgayTT", the.NgayTT);        
            }
        }

        void TinhThanhTien()
        {
            var kyTT = (int?)gvTheXe.GetFocusedRowCellValue("KyTT");
            var phithang = (decimal?)gvTheXe.GetFocusedRowCellValue("PhiGiuXe");
            var TienTT = phithang.GetValueOrDefault() * kyTT.GetValueOrDefault();
            gvTheXe.SetFocusedRowCellValue("TienTT", TienTT);
        }

        private void spTienTT_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit sp = (SpinEdit)sender;
            var phithang = (decimal?)sp.Value;
            gvTheXe.SetFocusedRowCellValue("PhiGiuXe",phithang);
            TinhThanhTien();
        }

        private void lkLoaiDK_EditValueChanged(object sender, EventArgs e)
        {
            if (gvTheXe.IsNewItemRow(gvTheXe.FocusedRowHandle) && gvTheXe.GetRow(gvTheXe.FocusedRowHandle) == null)
                gvTheXe.AddNewRow();

            // reset lại các cột
            gvTheXe.SetFocusedRowCellValue("LoaiThe", null);
            gvTheXe.SetFocusedRowCellValue("MaTheCu", null);
            gvTheXe.SetFocusedRowCellValue("MaThe", null);
            gvTheXe.SetFocusedRowCellValue("BienSo", null);
            gvTheXe.SetFocusedRowCellValue("MauSac", null);
            gvTheXe.SetFocusedRowCellValue("ChuThe", null);
            gvTheXe.SetFocusedRowCellValue("MaLX", null);
            gvTheXe.SetFocusedRowCellValue("HangXe", null);
            gvTheXe.SetFocusedRowCellValue("PhiGiuXe", null);
            gvTheXe.SetFocusedRowCellValue("TienTT", null);
            gvTheXe.SetFocusedRowCellValue("KyTT", 1);
        }

        private void lkNhanKhau_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                var nk = db.tnNhanKhaus.Single(o => o.ID == (int?)lkNhanKhau.EditValue);
                txtSDT.Text = nk.DienThoai;
                txtEmail.Text = nk.Email;
            }
            catch { }
        }

        private void lkLoaiXeGird_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lk = (LookUpEdit)sender;
            var GiaThang = SetDonGia((int)lk.EditValue);
            gvTheXe.SetFocusedRowCellValue("PhiGiuXe", GiaThang);
            TinhThanhTien();
        }
    }

    public class ChiTietGiuXeItem
    {
        public int? MaDM { get; set; }
        public string TenDM { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGiaThang { get; set; }
        public decimal? DonGiaNgay { get; set; }
    }

}