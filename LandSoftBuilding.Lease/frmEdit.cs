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
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? DatCocId { get; set; }

        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db;
        ctHopDong objHD;
        int MaLDV_Thue = 2, MaLDV_SuaChua = 3, MaLDV_TienCoc = 3;

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            gvChiTiet.InvalidRowException += Common.InvalidRowException;
            gvTienCoc.InvalidRowException += Common.InvalidRowException;
            gvBangGia.InvalidRowException += Common.InvalidRowException;

            //gcChiTiet.KeyUp += Common.GridViewKeyUp;
            gcTienCoc.KeyUp += Common.GridViewKeyUp;
            gcBangGia.KeyUp += Common.GridViewKeyUp;

            xtraTabControl1.SelectedTabPageIndex = 0;

            db = new MasterDataContext();

            //Load loai tien
            lkLoaiTien.Properties.DataSource = lkLoaiTienDV.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();
            lkLoaiTien_LTT.DataSource = lkLoaiTien_DatCoc.DataSource = lkLoaiTien.Properties.DataSource;
            lkLoaiTienNgoaiTe.Properties.DataSource = lkLoaiTien.Properties.DataSource;

            //Load don vi tinh
            lkDoiViTinhDV.DataSource = (from dvt in db.DonViTinhs select new { dvt.ID, dvt.TenDVT }).ToList();

            //Load loai dich vu trong gird bang gia
            lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus
                                       join bg in db.dvBangGiaDichVus on new { MaLDV = (int?)l.ID, this.MaTN } equals new { bg.MaLDV, bg.MaTN } into tblBangGia
                                       from bg in tblBangGia.DefaultIfEmpty()
                                       where l.ParentID == 12
                                       select new
                                       {
                                           l.ID,
                                           TenLDV = l.TenHienThi,
                                           bg.DonGia,
                                           bg.MaLT,
                                           bg.MaDVT
                                       }).ToList();

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
            // load danh sách nhà thầu
            //glkNhaThau.Properties.DataSource = db.tnKhachHangs.Where(_ => _.MaTN == MaTN).Select(_ => new { _.MaKH, _.KyHieu, TenKH = _.IsCaNhan == true ? _.TenKH : _.CtyTen }).ToList();

            //Load mat bang
            glMatBang.DataSource = lkMB.DataSource = glkMatBangThiCong.DataSource = from mb in db.mbMatBangs
                                                                                    join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                                                                    join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                                                                    where k.MaTN == this.MaTN
                                                                                    orderby mb.MaSoMB
                                                                                    select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich };
            //Load lao gia thue
            lkLoaiGia.DataSource = from lg in db.LoaiGiaThues
                                   join lt in db.LoaiTiens on lg.MaLT equals lt.ID
                                   where lg.MaTN == this.MaTN
                                   orderby lg.TenLG
                                   select new { lg.ID, lg.TenLG, DonGia = lg.DonGia, LoaiTien = lt.KyHieuLT };
            spinSoNgayTreThanhToan.Value = 0;
            spinHanThanhToan.Value = 0;
            dateNgayBatDauTinhTronKyThanhToan.DateTime = System.DateTime.UtcNow.AddHours(7);

            glkDatCoc.Properties.DataSource = (from p in db.PhieuDatCoc_GiuChos
                                               where p.MaTN == MaTN
                                                //& (p.MaTT == 2 || p.MaTT == 3)
                                               select new
                                               {
                                                   ID = p.ID,
                                                   Name = p.SoCT
                                               }).ToList();
            dateNgayHetHan.DateTime = System.DateTime.Now;
            dateNgayHieuLuc.DateTime = System.DateTime.Now;

            glkTyGiaNganHang.Properties.DataSource = (from tg in db.TyGiaNganHangs
                                                      join lt in db.LoaiTiens on tg.MaLoaiTien equals lt.ID
                                                      where tg.MaTN == MaTN
                                                      select new
                                                      {
                                                          tg.Id,
                                                          tg.NganHang,
                                                          tg.Ngay,
                                                          lt.TenLT,
                                                          tg.TyGia,
                                                          TenDanhMuc = string.Format("Mã: {0}, tại ngân hàng {1}, ngày {2:dd/MM/yyyy}, tỷ giá: {3:#,0.##}", tg.TenDanhMuc, tg.NganHang,
                                                          tg.Ngay.GetValueOrDefault(),
                                                          tg.TyGia)

                                                      }).ToList();

            if (this.ID != null)
            {
                objHD = db.ctHopDongs.Single(p => p.ID == this.ID);
                glKhachHang.EditValue = objHD.MaKH;
                txtSoHDCT.EditValue = objHD.SoHDCT;
                dateNgayKy.EditValue = objHD.NgayKy;
                dateNgayHL.EditValue = objHD.NgayHL;
                dateNgayHH.EditValue = objHD.NgayHH;
                spThoiHan.EditValue = objHD.ThoiHan;
                spKyTT.EditValue = objHD.KyTT;
                //spHanTT.EditValue = objHD.HanTT;
                dateNgayBG.EditValue = objHD.NgayBG;
                lkLoaiTien.EditValue = objHD.MaLT;
                spTyGia.EditValue = objHD.TyGiaHD;
                spGiaThue.EditValue = objHD.GiaThue;
                spTienCoc.EditValue = objHD.TienCoc;
                spTyLeDCGT.EditValue = objHD.TyLeDCGT;
                spSoNamDCGT.EditValue = objHD.SoNamDCGT;
                spMucDCTG.EditValue = objHD.MucDCTG;
                ckbNgungSuDung.EditValue = objHD.NgungSuDung;
                //ckbLamTron.Checked = objHD.IsLamTron.GetValueOrDefault();
                //txtSoNgayFree.EditValue = objHD.SoNgayFree;
                //if (objHD.IsPhuLuc == true)
                //{
                //    cbLoaiHopDong.SelectedIndex = 1;
                //    lkHopDongLienQuan.EditValue = objHD.ParentID;
                //}
                //else
                //{
                //    cbLoaiHopDong.SelectedIndex = 0;
                //}

                bdsTienCoc.DataSource = this.GetLichThanhToans(this.MaLDV_TienCoc);
                txtDienGiai.Text = objHD.GhiChu;
                lkLoaiTienNgoaiTe.EditValue = objHD.LoaiTienNgoaiTe;
                spTyGiaNgoaiTe.EditValue = objHD.TyGiaNgoaiTe;
                spGiaThueNgoaiTe.EditValue = objHD.GiaThueNgoaiTe;
                //glkNhaThau.EditValue = objHD.MaNhaThau;
                txtNhaThau.Text = objHD.NhaThau;
                //txtGhiChu.Text = objHD.GhiChu;
                //txtDieuKhoanDacBiet.Text = objHD.DieuKhoanDacBiet;
                //txtDieuKhoanBoSungGiamThue.Text = objHD.DieuKhoanBoSungGiamThue;
                //txtXemXetLaiTienThue.Text = objHD.XemXetLaiTienThue;
                //spinTyGiaBanDau.EditValue = objHD.TyGiaApDungBanDau;
                spinDonGiaTuongDuongUSD.EditValue = objHD.DonGiaTuongDuongUSD;
                if (objHD.ThoiGIanThayDoiTyGIa != null) //dateThoiGianThayDoi.DateTime = (DateTime)objHD.ThoiGIanThayDoiTyGIa;
                //txtCachThayDoiTyGia.Text = objHD.CachThayDoiTyGia;
                if (objHD.NgayTangGiaTiepTheo != null) dateNgayTangGiaTiepTheo.DateTime = (DateTime)objHD.NgayTangGiaTiepTheo;

                spinSoNgayTreThanhToan.EditValue = objHD.SoNgayGiaHanThanhToan.GetValueOrDefault();
                spinHanThanhToan.EditValue = objHD.NgayHanThanhToan.GetValueOrDefault();

                gcThiCong.DataSource = db.ctThiCongs.Where(_ => _.MaHD == ID);
                gvThiCong.OptionsBehavior.Editable = false;

                dateNgayBatDauTinhTronKyThanhToan.EditValue = objHD.NgayBatDauTinhTronKyThanhToan;

                spinLaiSuatNopCham.EditValue = objHD.LaiSuatNopCham.GetValueOrDefault();
                spinSoLuongXeMienPhi.EditValue = objHD.SoLuongXeMienPhi.GetValueOrDefault();
                spinSoNgayFree.EditValue = objHD.SoNgayFree.GetValueOrDefault();
                spinThoiGianChoPhepNopCham.EditValue = objHD.SoNgayFree.GetValueOrDefault();
                chkIsMienLai.Checked = objHD.IsMienLai.GetValueOrDefault();
                if(objHD.DatCocId.GetValueOrDefault() != 0)
                {
                    glkDatCoc.Properties.DataSource = (from p in db.PhieuDatCoc_GiuChos
                                                       where p.MaTN == MaTN
                                                        //& (p.MaTT == 2 || p.MaTT == 3 || p.ID == objHD.DatCocId)
                                                       select new
                                                       {
                                                           ID = p.ID,
                                                           Name = p.SoCT
                                                       }).ToList();

                    glkDatCoc.EditValue = objHD.DatCocId;
                }
                if (objHD.NgayHetHan != null) dateNgayHetHan.DateTime = objHD.NgayHetHan.GetValueOrDefault();
                if (objHD.NgayHieuLuc != null) dateNgayHieuLuc.DateTime = objHD.NgayHieuLuc.GetValueOrDefault();
                glkTyGiaNganHang.EditValue = objHD.MaTyGiaNganHang;
            }
            else
            {
                objHD = new ctHopDong();
                txtSoHDCT.EditValue = db.CreateSoChungTu(1, this.MaTN);
                dateNgayKy.EditValue = db.GetSystemDate();
                dateNgayBG.EditValue = dateNgayKy.EditValue;
                lkLoaiTien.ItemIndex = 0;
                cbLoaiHopDong.SelectedIndex = 0;

                lkLoaiTienNgoaiTe.ItemIndex = 1;

                bdsTienCoc.DataSource = new List<LichThanhToanItem>();

                List<ctThiCong> thicong = new List<ctThiCong>();
                gcThiCong.DataSource = thicong.ConvertToDataTable();

                if(DatCocId != null)
                {
                    var datCoc = db.PhieuDatCoc_GiuChos.FirstOrDefault(_ => _.ID == DatCocId);
                    if(datCoc != null)
                    {
                        glkDatCoc.EditValue = DatCocId;
                        glKhachHang.EditValue = datCoc.MaKH;
                    }
                    
                }

            }
            gcChiTiet.DataSource = objHD.ctChiTiets;
            gcBangGia.DataSource = objHD.ctBangGiaDichVus;
            gcLTT.DataSource = objHD.ctLichThanhToans;

            if (dateNgayBatDauTinhTronKyThanhToan.DateTime == null)
            {
                dateNgayBatDauTinhTronKyThanhToan.DateTime = System.DateTime.UtcNow.AddHours(7);
            }

        }

        void TinhGiaThue()
        {
            spGiaThue.EditValue = objHD.ctChiTiets.Sum(p => p.ThanhTien).GetValueOrDefault();
             spGiaThueNgoaiTe.EditValue = spTyGiaNgoaiTe.Value > 0 ? spGiaThue.Value * spTyGia.Value / spTyGiaNgoaiTe.Value : 0;
        }

        void TinhTienCoc()
        {
            decimal _TienCoc = 0;
            for (int i = 0; i < gvTienCoc.RowCount; i++)
            {
                _TienCoc += (decimal?)gvTienCoc.GetRowCellValue(i, "SoTien") ?? 0;
            }

            spTienCoc.EditValue = _TienCoc;
        }

        void TinhThanhTien()
        {
            try
            {
                var _DienTich = (gvChiTiet.GetFocusedRowCellValue("DienTich") as decimal?) ?? 0;
                var _DonGia = (gvChiTiet.GetFocusedRowCellValue("DonGia") as decimal?) ?? 0;
                var _PhiDichVu = (gvChiTiet.GetFocusedRowCellValue("PhiDichVu") as decimal?) ?? 0;
                var _PhiDieuHoaChieuSang = (gvChiTiet.GetFocusedRowCellValue("PhiDieuHoaChieuSang") as decimal?) ?? 0;
                var _TyLeVAT = (gvChiTiet.GetFocusedRowCellValue("TyLeVAT") as decimal?) ?? 0;
                var _TyLeCK = (gvChiTiet.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                var _TienCK = (gvChiTiet.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;
                var _TongGiaThue = (gvChiTiet.GetFocusedRowCellValue("TongGiaThue") as decimal?) ?? 0;

                _TongGiaThue = _DienTich * (_DonGia + _PhiDichVu + _PhiDieuHoaChieuSang);

                //if (ckbLamTron.Checked)
                //{
                //    var SoTienLe = _TongGiaThue % 1000;
                //    var SoTienChan = _TongGiaThue - SoTienLe;
                //    _TongGiaThue = SoTienChan + SoTienLe;
                //}

                if (_TongGiaThue > 0)
                {
                    if (_TyLeCK > 0)
                        _TienCK = _TongGiaThue * _TyLeCK;
                    else
                        _TyLeCK = _TienCK / _TongGiaThue;
                }

                var _TienVAT = _TongGiaThue * _TyLeVAT;


                var _ThanhTien = _TongGiaThue + _TienVAT - _TienCK;

                gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                gvChiTiet.SetFocusedRowCellValue("TongGiaThue", _TongGiaThue);
                gvChiTiet.SetFocusedRowCellValue("TienVAT", _TienVAT);
                gvChiTiet.SetFocusedRowCellValue("TyLeCK", _TyLeCK);
                gvChiTiet.SetFocusedRowCellValue("TienCK", _TienCK);
                gvChiTiet.SetFocusedRowCellValue("ThanhTien", _ThanhTien);
            }
            catch { }
        }

        void SetLamTron()
        {
            gvChiTiet.FocusedRowHandle = -1;

            foreach (var ct in objHD.ctChiTiets)
            {
                //if (ckbLamTron.Checked)
                //{
                //    var SoTienLe = ct.TongGiaThue % 1000;
                //    var SoTienChan = ct.TongGiaThue - SoTienLe;
                //    SoTienLe = SoTienLe <= 500 ? 0 : 1000;
                //    ct.TongGiaThue = SoTienLe + SoTienChan;
                //}
                //else
                //{
                //    ct.TongGiaThue = ct.DonGia * ct.DienTich;
                //}


                ct.TienVAT = ct.TongGiaThue * ct.TyLeVAT;
                ct.ThanhTien = ct.TongGiaThue + ct.TienVAT;
            }

            gvChiTiet.FocusedRowHandle = -1;
            TaoLichThanhToanTienThue();
        }

        void UpdateLichThanhToan(int _MaLDV)
        {
            List<LichThanhToanItem> ltLich = bdsTienCoc.DataSource as List<LichThanhToanItem>;

            //Xoa lich thanh toan
            foreach (var i in objHD.ctLichThanhToans.Where(p => p.MaLDV == _MaLDV))
            {
                if (ltLich.Where(p => p.DotTT == i.DotTT 
                                    & p.TuNgay == i.TuNgay 
                                    & p.DenNgay == i.DenNgay
                                    & p.SoThang == i.SoThang
                                    & p.SoTien == i.SoTien
                                 ).Count() == 0)
                {
                    db.ctLichThanhToans.DeleteOnSubmit(i);
                }
            }

            ///Them sua lich thanh toan
            foreach (var l in ltLich)
            {
                ctLichThanhToan objLTT;
                if (l.ID == null)
                {
                    objLTT = new ctLichThanhToan();
                    objLTT.MaLDV = _MaLDV;
                    objHD.ctLichThanhToans.Add(objLTT);
                }
                else
                {
                    objLTT = objHD.ctLichThanhToans.FirstOrDefault(p => p.ID == l.ID);
                }

                objLTT.DotTT = l.DotTT;
                objLTT.TuNgay = l.TuNgay;
                objLTT.DenNgay = l.DenNgay;
                objLTT.SoThang = Common.GetTotalMonth((DateTime)l.TuNgay, (DateTime)l.DenNgay); //l.SoThang;
                objLTT.SoTien = l.SoTien;
                objLTT.MaLoaiTien =(int?)l.MaLoaiTien;
                objLTT.TyGia = l.TyGia;
                
                objLTT.SoTienQD = objLTT.TyGia == 1 ? Math.Round( (decimal)l.SoTien.GetValueOrDefault() ,0) : l.SoTien.GetValueOrDefault() * objLTT.TyGia.GetValueOrDefault();
                objLTT.DienGiai = l.DienGiai;
                objLTT.DonGia = l.DonGia;
                objLTT.DienTich = l.DienTich;
            }
        }

        List<LichThanhToanItem> GetLichThanhToans(int _MaLDV)
        {
            return (from l in db.ctLichThanhToans
                    where l.MaHD == this.ID & l.MaLDV == _MaLDV
                    select new LichThanhToanItem()
                    {
                        ID = l.ID,
                        DotTT = l.DotTT,
                        TuNgay = l.TuNgay,
                        DenNgay = l.DenNgay,
                        SoThang = l.SoThang,
                        SoTien = l.SoTien,
                        SoTienQD = l.SoTienQD,
                        DienGiai = l.DienGiai,
                        TyGia = l.TyGia,
                        MaLoaiTien = l.MaLoaiTien,
                         DienTich = l.DienTich,
                          DonGia = l.DonGia
                    }).ToList();
        }

        void TaoLichThanhToanTienThue()
        {
            if (objHD.ID > 0) return;


            colPhong.GroupIndex = -1;
            gvLTT.FocusedRowHandle = -1;
            gvLTT.SelectAll();
            gvLTT.DeleteSelectedRows();

            foreach (var mb in objHD.ctChiTiets)
            {
                
                // Tách ra kỳ thanh toán lẻ chưa tròn tháng


                if(mb.TuNgay != null & mb.DenNgay != null)
                {
                    int _DotTT = 0;
                    var _TuNgay = (DateTime)mb.TuNgay;
                    while (_TuNgay.CompareTo(mb.DenNgay) < 0)
                    {
                        _DotTT++;
                        decimal _KyTT = (int)spKyTT.Value;
                        var _DenNgay = _TuNgay.AddMonths((int)spKyTT.Value).AddDays(-1);

                        // Tách kỳ lẻ
                        // Nếu kỳ nhỏ hơn 1 năm thì sẽ tách kỳ đầu lẻ, còn ngược lại thì không tách, vẫn chạy như cũ
                        // và từ ngày không phải là đầu tháng
                        if (_KyTT < 12 & _TuNgay != _DenNgay)
                        {
                            // tách kỳ lẻ, ví dụ lần 1, từ ngày = 16/3, đến ngày = 16/6
                            
                            if(_TuNgay.Date <= dateNgayBatDauTinhTronKyThanhToan.DateTime.Date.AddDays(-1))
                                _DenNgay = dateNgayBatDauTinhTronKyThanhToan.DateTime.AddDays(-1);
                            _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                        }

                        if (_DenNgay.CompareTo(mb.DenNgay) > 0)
                        {
                            _DenNgay = (DateTime)mb.DenNgay;
                            _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                        }

                        if (_KyTT > 0)
                        {
                            decimal _TongTien = 0, _TongTienChuaTron = 0;

                            if(_KyTT != (int)spKyTT.Value)
                            {
                                //if(_TuNgay.Date.Day == 1)
                                //{
                                // Tính tiền kỳ lẻ,
                                // lấy tiền từng tháng cộng lại
                                // làm phần tính tiền trước, cái tháng lẻ, sẽ tách ra từng tháng lẻ
                                DateTime fromDate = _TuNgay;
                                DateTime toDate = _DenNgay;
                                // Kỳ thanh toán con
                                decimal? billingCycle = _KyTT, Ky = 0;
                                // Tiền thanh toán
                                decimal? pay = 0;


                                while (fromDate.CompareTo(_DenNgay) < 0)
                                {
                                    // tách ra từng tháng
                                    toDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                                    toDate = toDate.AddMonths(1);
                                    toDate = toDate.AddDays(-(toDate.Day));

                                    if (toDate.CompareTo(_DenNgay) > 0) toDate = _DenNgay;

                                    billingCycle = Common.GetTotalOneMonth(fromDate, toDate);
                                    Ky += billingCycle;

                                    pay += billingCycle * mb.ThanhTien;

                                    fromDate = toDate.AddDays(1);
                                }

                                _TongTien = (decimal)pay;
                                _TongTienChuaTron = (decimal)pay;
                                _KyTT = (decimal)Ky;
                            }
                            else
                            {
                                _TongTien = _KyTT * mb.ThanhTien.GetValueOrDefault();
                                _TongTienChuaTron = _TongTien;
                            }
                            
                            //}
                            //else
                            //{
                            //    _TongTien = (decimal)mb.ThanhTien * _KyTT;
                            //}

                            _TongTien = Math.Round(((decimal)_TongTien), 2);
                            

                            gvLTT.AddNewRow();
                            gvLTT.SetFocusedRowCellValue("DotTT", _DotTT);
                            gvLTT.SetFocusedRowCellValue("MaMB", mb.MaMB);
                            gvLTT.SetFocusedRowCellValue("MaLDV", 2);
                            gvLTT.SetFocusedRowCellValue("DienGiai", string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay));
                            gvLTT.SetFocusedRowCellValue("TuNgay", _TuNgay);
                            gvLTT.SetFocusedRowCellValue("DenNgay", _DenNgay);
                            gvLTT.SetFocusedRowCellValue("SoThang", _KyTT);
                            gvLTT.SetFocusedRowCellValue("SoTien", _TongTien);
                            gvLTT.SetFocusedRowCellValue("SoTienQD", Math.Round( _TongTienChuaTron * (decimal)spTyGia.EditValue,0));
                            gvLTT.SetFocusedRowCellValue("NgayHHTT", _DenNgay);
                            gvLTT.SetFocusedRowCellValue("IsKhuyenMai", false);
                            gvLTT.SetFocusedRowCellValue("DienTich", mb.DienTich);
                            gvLTT.SetFocusedRowCellValue("DonGia", mb.DonGia);
                            gvLTT.SetFocusedRowCellValue("GiaThue", mb.ThanhTien);
                            gvLTT.SetFocusedRowCellValue("MaLoaiTien", (int?)lkLoaiTien.EditValue);
                            gvLTT.SetFocusedRowCellValue("NgungSuDung",false);
                            gvLTT.SetFocusedRowCellValue("TyGia", spTyGia.EditValue);
                            gvLTT.SetFocusedRowCellValue("Loai", "CHOTHUE");
                            gvLTT.SetFocusedRowCellValue("NgayTao", System.DateTime.UtcNow);
                            gvLTT.SetFocusedRowCellValue("HanhDong", "EDIT");
                            //gvLTT.SetFocusedRowCellValue("ThanhTienNgoaiTe", _TongTienChuaTron / (decimal)spTyGiaNgoaiTe.EditValue);
                            gvLTT.SetFocusedRowCellValue("ThanhTienNgoaiTe", _TongTienChuaTron / 1);
                            //gvLTT.SetFocusedRowCellValue("TyGiaNgoaiTe", spTyGiaNgoaiTe.EditValue);
                            gvLTT.SetFocusedRowCellValue("TyGiaNgoaiTe", 1);
                            //gvLTT.SetFocusedRowCellValue("LoaiTienNgoaiTe", (int?)lkLoaiTienNgoaiTe.EditValue);
                            gvLTT.SetFocusedRowCellValue("LoaiTienNgoaiTe", 1);
                            gvLTT.SetFocusedRowCellValue("PhiDichVu", mb.PhiDichVu);
                            gvLTT.SetFocusedRowCellValue("PhiDieuHoaChieuSang", mb.PhiDieuHoaChieuSang);
                            gvLTT.SetFocusedRowCellValue("TyLeVAT", mb.TyLeVAT);
                            gvLTT.SetFocusedRowCellValue("TyLeCK", mb.TyLeCK);

                            var item = (ctLichThanhToan)gvLTT.GetFocusedRow();
                            //SchedulePaymentCls.ctLichThanhToan(item, ckbLamTron.Checked);
                        }

                        _TuNgay = _DenNgay.AddDays(1);
                    }
                }
                
            }
            gvLTT.UpdateCurrentRow();
            gvLTT.RefreshData();
            colPhong.GroupIndex = 0;
            
            gvLTT.FocusedRowHandle = -1;
        }



        private void cbLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbLoaiHopDong.SelectedIndex.Equals(0))
            //{
            //    lkHopDongLienQuan.Properties.ReadOnly = true;
            //    lkHopDongLienQuan.EditValue = null;
            //}
            //else // Phu Luc
            //{
            //    lkHopDongLienQuan.Properties.ReadOnly = false;
            //}
        }

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spTyGia.EditValue = lkLoaiTien.GetColumnValue("TyGia");
            TaoLichThanhToanTienThue();
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    lkHopDongLienQuan.Properties.DataSource = (from hd in db.ctHopDongs
            //                                               where hd.IsPhuLuc == false & hd.MaKH == (int)glKhachHang.EditValue
            //                                               select new
            //                                               {
            //                                                   hd.ID,
            //                                                   hd.SoHDCT,
            //                                                   hd.NgayKy
            //                                               }).ToList();
            //    lkHopDongLienQuan.EditValue = null;
            //}
            //catch { }
        }

        private void glKhachHang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }

        void NgayHL_NgayHH_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spThoiHan.EditValue = Math.Round(Common.GetTotalMonth(dateNgayHL.DateTime, dateNgayHH.DateTime), 0, MidpointRounding.AwayFromZero);

                this.TaoLichThanhToanTienThue();

            }
            catch { }
        }

        #region Chi tiet mat bang
        private void gvChiTiet_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("DienTich", 0);
            gvChiTiet.SetFocusedRowCellValue("DonGia", 0);
            gvChiTiet.SetFocusedRowCellValue("PhiDichVu", 0);
            gvChiTiet.SetFocusedRowCellValue("PhiDieuHoaChieuSang", 0);
            gvChiTiet.SetFocusedRowCellValue("PhiSuaChua", 0);
            gvChiTiet.SetFocusedRowCellValue("NgungSuDung", false);
            gvChiTiet.SetFocusedRowCellValue("TyLeVAT", 0.0);
        }

        private void gvChiTiet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (gvChiTiet.GetFocusedRowCellValue("MaMB") == null)
            {
                e.ErrorText = "Vui lòng chọn mặt bằng!";
                e.Valid = false;
                return;
            }

            if (gvChiTiet.GetRowCellValue(e.RowHandle, "MaLG") == null)
            {
                e.ErrorText = "Vui lòng chọn loại giá!";
                e.Valid = false;
                return;
            }
        }

        private void gvChiTiet_RowUpdated(object sender, RowObjectEventArgs e)
        {
            this.TinhGiaThue();
        }

        private void gvChiTiet_RowCountChanged(object sender, EventArgs e)
        {
            this.TinhGiaThue();
        }

        private void glMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkMB = (sender as GridLookUpEdit);
                var _MaMB = (int)lkMB.EditValue;

                #region // Kiểm tra nếu mặt bằng đã có ở các hợp đồng khác, thì không cho phép thêm vào

                var modelKiemTraTonTaiMatBang = new { MaMB = _MaMB, MaHD = ID };
                var paramKiemTraTonTaiMatBang = new Dapper.DynamicParameters();
                paramKiemTraTonTaiMatBang.AddDynamicParams(modelKiemTraTonTaiMatBang);
                var resultKiemTraTonTaiMatBang = Library.Class.Connect.QueryConnect.Query<string>("ctHopDong_Kiem_Tra_Ton_Tai_Mat_Bang", paramKiemTraTonTaiMatBang);
                var checkTonTaiMatBang = "";
                if(resultKiemTraTonTaiMatBang.Count() > 0) checkTonTaiMatBang = resultKiemTraTonTaiMatBang.FirstOrDefault();

                if (checkTonTaiMatBang == "1")
                {
                    DialogBox.Alert("Mặt bằng này đã được lên hợp đồng.");
                    lkMB.EditValue = 0;
                    gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)0);
                    gvChiTiet.SetFocusedRowCellValue("DonGia", (decimal?)0);
                    gvChiTiet.SetFocusedRowCellValue("MaLG", 0);
                    gvChiTiet.SetFocusedRowCellValue("ThanhTien", 0);
                    return;
                }

                #endregion


                var r = lkMB.Properties.GetRowByKeyValue(lkMB.EditValue);
                var type = r.GetType();
                gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));

                if (r != null)
                {
                    var ltGiaThue = (from g in db.mbGiaThues
                                     join mb in db.mbMatBangs on g.MaMB equals mb.MaMB
                                     join lt in db.LoaiTiens on mb.MaLT equals lt.ID
                                     where g.MaMB == _MaMB
                                     select new { g.MaLG, DonGia = g.DonGia * lt.TyGia, g.DienTich }).ToList();
                    if (ltGiaThue.Count() > 0)
                    {
                        //Update du lieu cho dong hien tai
                        foreach (var g in ltGiaThue)
                        {
                            if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG).Count() == 0)
                            {
                                try
                                {
                                    var _DonGia = g.DonGia / (decimal?)lkLoaiTien.GetColumnValue("TyGia");
                                    gvChiTiet.SetFocusedRowCellValue("MaMB", _MaMB);
                                    gvChiTiet.SetFocusedRowCellValue("MaLG", g.MaLG);
                                    gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                                    gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                                    gvChiTiet.SetFocusedRowCellValue("ThanhTien", g.DienTich * _DonGia);
                                    gvChiTiet.UpdateCurrentRow();
                                    break;
                                }
                                catch (System.Exception ex)
                                {
                                    DialogBox.Error("Vui lòng nhập tỷ giá \n Lỗi: " + ex.Message);
                                }
                                
                            }
                        }

                        //Them du lieu neu chua co
                        foreach (var g in ltGiaThue)
                        {
                            if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG ).Count() == 0)
                            {
                                try
                                {
                                    var objCT = new ctChiTiet();
                                    objCT.MaMB = _MaMB;
                                    objCT.MaLG = g.MaLG;
                                    objCT.DonGia = g.DonGia / (decimal?)lkLoaiTien.GetColumnValue("TyGia");
                                    objCT.DienTich = g.DienTich;
                                    objCT.ThanhTien = objCT.DonGia * objCT.DienTich;
                                    objHD.ctChiTiets.Add(objCT);
                                }
                                catch ( System.Exception ex)
                                {
                                    DialogBox.Error("Vui lòng nhập tỷ giá \n Lỗi: " + ex.Message);
                                }
                                
                            }
                        }
                    }

                    this.TinhGiaThue();
                    TinhThanhTien();
                }
            }
            catch { }

        }

        private void lkLoaiGia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gvChiTiet.SetFocusedRowCellValue("DonGia", (decimal)(sender as LookUpEdit).GetColumnValue("DonGia"));
                TinhThanhTien();
            }
            catch { }
        }

        private void spDienTich_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("DienTich", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spDonGia_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("DonGia", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spPhiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("PhiDichVu", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spThanhTienCT_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void spTyLeVAT_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("TyLeVAT", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spTyLeCK_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("spTyLeCK", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spTienCK_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("TyLeCK", 0);
            TinhThanhTien();
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                gvChiTiet.UpdateCurrentRow();
                gvTienCoc.UpdateCurrentRow();
                gvBangGia.UpdateCurrentRow();

                #region Validate
                if (glKhachHang.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn khách hàng!");
                    glKhachHang.Focus();
                    return;
                }


                if (String.IsNullOrEmpty(txtSoHDCT.Text))
                {
                    if (cbLoaiHopDong.SelectedIndex.Equals(0))
                    {
                        DialogBox.Error("Vui lòng nhập số HDCT!");
                    }
                    else
                    {
                        DialogBox.Error("Vui lòng nhập số phụ lục!");
                    }

                    txtSoHDCT.Focus();
                    return;
                }

                if (dateNgayKy.EditValue == null)
                {
                    DialogBox.Error("Vui lòng nhập ngày ký!");
                    dateNgayKy.Focus();
                    return;
                }

                if (dateNgayHL.EditValue == null)
                {
                    DialogBox.Error("Vui lòng nhập ngày hiệu lực!");
                    dateNgayKy.Focus();
                    return;
                }

                if (dateNgayHH.EditValue == null)
                {
                    DialogBox.Error("Vui lòng nhập ngày hết hạn!");
                    dateNgayKy.Focus();
                    return;
                }

                if (lkLoaiTien.EditValue == null)
                {
                    DialogBox.Error("Vui lòng nhập loại tiền!");
                    lkLoaiTien.Focus();
                    return;
                }

                if (gvChiTiet.RowCount == 1)
                {
                    DialogBox.Error("Vui lòng nhập thông tin mặt bằng!");
                    return;
                }
                gvChiTiet.RefreshData();
                //Kiểm tra mặt bằng có còn thuê hay không?
                //Kiểm tra phòng có còn thuê hay không?
                if (objHD.ID == 0)
                {
                    bool KT = false;
                    foreach (var item in objHD.ctChiTiets)
                    {
                        var objKTHopDong = (from hd in db.ctHopDongs
                                            join ct in db.ctChiTiets on hd.ID equals ct.MaHDCT
                                            join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                            where hd.MaTN == this.MaTN
                                            & SqlMethods.DateDiffDay(hd.NgayHL, dateNgayHL.DateTime) >= 0
                                            & SqlMethods.DateDiffDay(dateNgayHL.DateTime, hd.NgayHH) >= 0
                                            & ct.MaMB == item.MaMB
                                            & !ct.NgungSuDung.GetValueOrDefault()
                                            & hd.ctThanhLies.Count() == 0
                                            select new
                                            {
                                                Loai = ct.MaMB == null ? "Ghế" : "Phòng",
                                                mb.MaSoMB,
                                                hd.ID,
                                                hd.SoHDCT,
                                                hd.NgayHL,
                                                NgayHH = ct.DenNgay,
                                            }).FirstOrDefault();

                        if (objKTHopDong != null)
                        {
                            string Error = string.Format("{0} {1} đã được cho thuê bởi hợp đồng {2} trong thời gian {3:dd/MM/yyyy} - {4:dd/MM/yyyy} . Vui lòng chọn thời gian hiệu lực khác!", objKTHopDong.Loai, objKTHopDong.MaSoMB, objKTHopDong.SoHDCT, objKTHopDong.NgayHL, objKTHopDong.NgayHH);
                            DialogBox.Error(Error);
                            return;
                        }
                    }
                }
                #endregion


                #region Save
                if (objHD.ID == 0)
                {
                    objHD.MaTN = this.MaTN;
                    objHD.MaNVN = Library.Common.User.MaNV;
                    objHD.NgayNhap = db.GetSystemDate();
                    objHD.TyGia = spTyGia.Value;

                    foreach (var ct in objHD.ctChiTiets)
                    {
                        ct.MaLoaiTien = (int?)lkLoaiTien.EditValue;
                        //ct.TyGia = spTyGia.Value;
                    }

                    db.ctHopDongs.InsertOnSubmit(objHD);
                }
                else
                {
                    objHD.MaNVS = Library.Common.User.MaNV;
                }

                if (ListMB.Count > 0)
                {
                    MasterDataContext dbo = new MasterDataContext();
                    foreach (var i in ListMB)
                    {
                        var query = dbo.ctLichThanhToans.Where(p => p.MaMB == i).ToList();
                        foreach (var k in query)
                        {
                            dbo.ctLichThanhToans.DeleteOnSubmit(k);
                        }


                    }
                    dbo.SubmitChanges();

                }


                objHD.MaKH = (int?)glKhachHang.EditValue;
                objHD.SoHDCT = txtSoHDCT.Text;
                objHD.NgayKy = (DateTime?)dateNgayKy.EditValue;
                objHD.NgayHL = (DateTime?)dateNgayHL.EditValue;
                objHD.NgayHH = (DateTime?)dateNgayHH.EditValue;
                objHD.ThoiHan = spThoiHan.Value;
                objHD.KyTT = Convert.ToInt32(spKyTT.Value);
                objHD.HanTT = 0;
                //objHD.HanTT = Convert.ToInt32(spHanTT.Value);
                objHD.NgayBG = (DateTime?)dateNgayBG.EditValue;
                objHD.MaLT = (int?)lkLoaiTien.EditValue;
                objHD.TyGiaHD = spTyGia.Value;
                objHD.MucDCTG = spMucDCTG.Value;
                objHD.GiaThue = spGiaThue.Value;
                objHD.GiaThueNgoaiTe = spGiaThueNgoaiTe.Value;
                objHD.TienCoc = spTienCoc.Value;
                objHD.TyLeDCGT = spTyLeDCGT.Value;
                objHD.SoNamDCGT = Convert.ToInt32(spSoNamDCGT.Value);
                objHD.NgungSuDung = ckbNgungSuDung.Checked;
                //objHD.IsLamTron = ckbLamTron.Checked;
                objHD.TyGiaNgoaiTe = spTyGiaNgoaiTe.Value;
                objHD.LoaiTienNgoaiTe = (int?)lkLoaiTienNgoaiTe.EditValue;
                objHD.NgayBatDauTinhTronKyThanhToan = dateNgayBatDauTinhTronKyThanhToan.DateTime;

                if (glkDatCoc.EditValue != null)
                {
                    objHD.DatCocId = (int?)glkDatCoc.EditValue;

                    var datCoc = db.PhieuDatCoc_GiuChos.FirstOrDefault(_ => _.ID == (int?)glkDatCoc.EditValue
                                                                   );
                    if (datCoc != null)
                    {
                        Library.Class.Connect.QueryConnect.QueryData<bool>("Log_CapNhatTrangThai_PDC_Insert", new
                        {
                            DatCocId = datCoc.ID,
                            MaNV = Common.User.MaNV
                        });

                    }
                }


                try
                {


                    objHD.DonGiaTuongDuongUSD = spinDonGiaTuongDuongUSD.Value;
                    //objHD.TyGiaApDungBanDau = spinTyGiaBanDau.Value;
                    //objHD.ThoiGIanThayDoiTyGIa = (DateTime?)dateThoiGianThayDoi.EditValue;

                    objHD.NgayTangGiaTiepTheo = (DateTime?)dateNgayTangGiaTiepTheo.EditValue;
                    //objHD.MaNhaThau = (int?)glkNhaThau.EditValue;

                    objHD.NgayHieuLuc = dateNgayHieuLuc.DateTime;
                    objHD.NgayHetHan = dateNgayHetHan.DateTime;
                }
                catch { }

                objHD.NhaThau = txtNhaThau.Text;
                //objHD.CachThayDoiTyGia = txtCachThayDoiTyGia.Text;
                //objHD.DieuKhoanDacBiet = txtDieuKhoanDacBiet.Text;
                //objHD.DieuKhoanBoSungGiamThue = txtDieuKhoanBoSungGiamThue.Text;
                //objHD.XemXetLaiTienThue = txtXemXetLaiTienThue.Text;
                //objHD.GhiChu = txtGhiChu.Text;
                objHD.GhiChu = txtDienGiai.Text;
                //objHD.SoNgayFree =  Convert.ToInt32(txtSoNgayFree.Value);
                //objHD.NgayTruocGiaHan = (DateTime?)dateNgayHH.EditValue;

                objHD.SoNgayGiaHanThanhToan = (int?)spinSoNgayTreThanhToan.Value;
                objHD.NgayHanThanhToan = (int?)spinHanThanhToan.Value;

                objHD.LaiSuatNopCham = (decimal?)spinLaiSuatNopCham.Value;
                objHD.SoLuongXeMienPhi = (int?)spinSoLuongXeMienPhi.Value;
                objHD.IsMienLai = chkIsMienLai.Checked;
                objHD.ThoiGianChoPhepNopCham = (int?)spinThoiGianChoPhepNopCham.Value;
                objHD.SoNgayFree = (int?)spinSoNgayFree.Value;

                objHD.MaTyGiaNganHang = Convert.ToInt32(glkTyGiaNganHang.EditValue);

                //if (cbLoaiHopDong.SelectedIndex.Equals(1)) // Phu luc
                //{
                //    objHD.IsPhuLuc = true;
                //    objHD.ParentID = (int)lkHopDongLienQuan.GetColumnValue("ID");
                //}
                //else
                //{
                //    objHD.IsPhuLuc = false;
                //    objHD.ParentID = null;
                //}

                this.UpdateLichThanhToan(this.MaLDV_TienCoc);

                if (objHD.ID == 0)
                {
                    this.TaoLichThanhToanTienThue();

                    foreach (var ct in objHD.ctChiTiets)
                    {
                        ct.NgayBDTaoLTT = ct.TuNgay;
                        ct.HanhDong = "EDIT";
                        ct.LoaiTienNgoaiTe = objHD.LoaiTienNgoaiTe;
                        ct.TyGiaNgoaiTe = objHD.TyGiaNgoaiTe;
                        //ct.ThanhTienNgoaiTe = ct.ThanhTien / objHD.TyGiaNgoaiTe;
                        ct.ThanhTienNgoaiTe = ct.ThanhTien / 1;
                    }
                }



                try
                {
                    db.SubmitChanges();
                    SaoLuuHopDongGoc();

                    #region Lưu thi công và tạo lịch thanh toán cho thi công
                    for (var i = 0; i < gvThiCong.RowCount; i++)
                    {
                        // loại thi công, thêm vào bảng thi công và thêm vào lịch thanh toán, chỉ thanh toán 1 lần
                        //(int?) gvScheduleApartment.GetRowCellValue(i, "ApartmentId");
                        var mamb = (int?)gvThiCong.GetRowCellValue(i, "MaMB");
                        if (mamb == null) continue;

                        var model = new { mahd = objHD.ID, mamb = (int?)gvThiCong.GetRowCellValue(i, "MaMB"), dientich = (decimal?)gvThiCong.GetRowCellValue(i, "DienTich"), phidichvu = (decimal?)gvThiCong.GetRowCellValue(i, "PhiDichVu"), phithicong = (decimal?)gvThiCong.GetRowCellValue(i, "PhiThiCong"), tylevat = (decimal?)gvThiCong.GetRowCellValue(i, "TyLeVAT"), tienvat = (decimal?)gvThiCong.GetRowCellValue(i, "TienVAT"), tyleck = (decimal?)gvThiCong.GetRowCellValue(i, "TyLeCK"), tienck = (decimal?)gvThiCong.GetRowCellValue(i, "TienCK"), loaitien = objHD.MaLT, tygia = objHD.TyGia, thanhtien = (decimal?)gvThiCong.GetRowCellValue(i, "ThanhTien"), loaitienngoaite = objHD.LoaiTienNgoaiTe, tygiangoaite = objHD.TyGiaNgoaiTe, thanhtienngoaite = (decimal?)gvThiCong.GetRowCellValue(i, "ThanhTien") / objHD.TyGiaNgoaiTe, tungay = (DateTime?)gvThiCong.GetRowCellValue(i, "TuNgay"), denngay = (DateTime?)gvThiCong.GetRowCellValue(i, "DenNgay"), hanhdong = "EDIT" };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        Library.Class.Connect.QueryConnect.Query<bool>("lease_frmimport_ctthicong_edit", param);
                    }
                    #endregion

                    #region Tính lại tiền lịch thanh toán theo công thức
                    Library.Class.Connect.QueryConnect.QueryData<bool>("ctLichThanhToanUpdateValue", new { MaHD = objHD.ID });
                    #endregion

                    DialogBox.Alert("Dữ liệu đã được lưu!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    //DialogBox.Alert("Lỗi: " + ex.Message);
                    DialogBox.Error("Thông tin chi tiết hợp đồng không đúng");
                    return;
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                DialogBox.Error("Lỗi khi lưu dữ liệu: "+ ex.Message);
            }

        }

        void SaoLuuHopDongGoc()
        {
            //if (objHD.idctHopDong_Goc != null)
            //{
            //    if (DialogBox.Question("Đã sao lưu hợp đồng gốc. Bạn có muốn sao lưu lại hợp đồng gốc không") == DialogResult.No) return;

            //    var hd = db.ctHopDong_Gocs.Single(o => o.ID == objHD.idctHopDong_Goc);
            //    db.ctHopDong_Gocs.DeleteOnSubmit(hd);
            //}

            //var obj_hdGoc = new ctHopDong_Goc();
            //db.ctHopDong_Gocs.InsertOnSubmit(obj_hdGoc);
            //UpdateData.InsertUpdate(objHD, obj_hdGoc);

            //// Tạo chi tiết cho hợp đồng gốc
            //foreach (var ct in objHD.ctChiTiets)
            //{
            //    var item = new ctChiTiet_Goc();
            //    UpdateData.InsertUpdate(ct, item);
            //    obj_hdGoc.ctChiTiet_Gocs.Add(item);
            //}

            //// Tạo lịch thanh toán
            //foreach(var ltt in objHD.ctLichThanhToans)
            //{
            //    var item = new ctLichThanhToan_Goc();
            //    UpdateData.InsertUpdate(ltt, item);
            //    obj_hdGoc.ctLichThanhToan_Gocs.Add(item);
            //}

            //db.SubmitChanges();
            //objHD.idctHopDong_Goc = obj_hdGoc.ID;
            //db.SubmitChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Tien coc
        private void gvTienCoc_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvTienCoc.SetFocusedRowCellValue("MaLDV", this.MaLDV_TienCoc);
            gvTienCoc.SetFocusedRowCellValue("MaLoaiTien", lkLoaiTien.EditValue);
            gvTienCoc.SetFocusedRowCellValue("TyGia", spTyGia.Value);
        }

        private void gvTienCoc_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            switch (e.Column.FieldName)
            {
                case "SoThang":
                    var _SoThang = (gvTienCoc.GetRowCellValue(e.RowHandle, "SoThang") as decimal?) ?? 0;
                    var _TuNgay = (gvTienCoc.GetRowCellValue(e.RowHandle, "SoThang") as DateTime?) ?? DateTime.Now;
                    var _DenNgay = _TuNgay.AddDays(Convert.ToDouble(_SoThang * 30 - 1));
                    gvTienCoc.SetRowCellValue(e.RowHandle, "DenNgay", _DenNgay);
                    break;
                case "SoTien":
                    SetTienDatCoc(e.RowHandle);
                    break;
                case "TyGia":
                    SetTienDatCoc(e.RowHandle);
                    break;
            }
        }

        void SetTienDatCoc(int RowHandle)
        {
            var _SoTien = (gvTienCoc.GetRowCellValue(RowHandle, "SoTien") as decimal?) ?? 0;
            var _TyGia = (gvTienCoc.GetRowCellValue(RowHandle, "TyGia") as decimal?) ?? 1;
            var _soTienQD = _SoTien * _TyGia;
            gvTienCoc.SetRowCellValue(RowHandle, "SoTienQD", _soTienQD);
        }

        private void gvTienCoc_RowUpdated(object sender, RowObjectEventArgs e)
        {
            this.TinhTienCoc();
        }

        private void gvTienCoc_RowCountChanged(object sender, EventArgs e)
        {
            this.TinhTienCoc();
        }
        #endregion

        #region Bang gia dich vu
        private void lkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            var lkLDV = sender as LookUpEdit;
            gvBangGia.SetFocusedRowCellValue("DonGia", lkLDV.GetColumnValue("DonGia"));
            gvBangGia.SetFocusedRowCellValue("MaLT", lkLDV.GetColumnValue("MaLT"));
            gvBangGia.SetFocusedRowCellValue("MaDVT", lkLDV.GetColumnValue("MaDVT"));
        }
        #endregion

        private void dateNgayKy_EditValueChanged(object sender, EventArgs e)
        {
            dateNgayHL.EditValue = dateNgayKy.EditValue;
        }

        private List<int> ListMB = new List<int>();
        private void gvChiTiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    var MaMB = (int?)gvChiTiet.GetFocusedRowCellValue("MaMB");

                    if (MaMB != null)
                    {
                        ListMB.Add((int)MaMB);

                    }
                    gvChiTiet.DeleteSelectedRows();
                }
            }
        }

        private void ckKM_CheckStateChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = sender as CheckEdit;
            try
            {
                if (ckb.Checked)
                {
                    gvLTT.SetFocusedRowCellValue("TyLeCK", 1);
                    gvLTT.SetFocusedRowCellValue("TienCK", gvLTT.GetFocusedRowCellValue("ThanhTien"));
                    gvLTT.SetFocusedRowCellValue("SoTien", 0);
                    gvLTT.SetFocusedRowCellValue("SoTienQD", 0);
                }
                else
                {
                    gvLTT.SetFocusedRowCellValue("TyLeCK", 0);
                    gvLTT.SetFocusedRowCellValue("TienCK", 0);
                    gvLTT.SetFocusedRowCellValue("SoTien", gvLTT.GetFocusedRowCellValue("ThanhTien"));
                    gvLTT.SetFocusedRowCellValue("SoTienQD", gvLTT.GetFocusedRowCellValue("ThanhTien"));
                }
            }
            catch
            {
            }
        }

        private void gvChiTiet_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DonGia" | e.Column.FieldName == "TuNgay" | e.Column.FieldName == "DenNgay" | e.Column.FieldName == "TyLeVAT")
                TaoLichThanhToanTienThue();
        }

        private void spKyTT_EditValueChanged(object sender, EventArgs e)
        {
            TaoLichThanhToanTienThue();
        }

        private void spTyLeCK_LTT_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit sp = sender as SpinEdit;
            var ThanhTien = (decimal?)gvLTT.GetFocusedRowCellValue("ThanhTien");
            gvLTT.SetFocusedRowCellValue("TienCK", ThanhTien * sp.Value);
            gvLTT.SetFocusedRowCellValue("SoTien", ThanhTien * (1 - sp.Value));
            gvLTT.SetFocusedRowCellValue("SoTienQD", ThanhTien * (1 - sp.Value));
        }

        private void spTienCK_LTT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SpinEdit sp = sender as SpinEdit;
                var ThanhTien = (decimal?)gvLTT.GetFocusedRowCellValue("ThanhTien");
                gvLTT.SetFocusedRowCellValue("TyLeCK", sp.Value / (decimal)ThanhTien.GetValueOrDefault());
                gvLTT.SetFocusedRowCellValue("TienCK", sp.Value);
                gvLTT.SetFocusedRowCellValue("SoTien", ThanhTien - sp.Value);
                gvLTT.SetFocusedRowCellValue("SoTienQD", ThanhTien - sp.Value);
            }
            catch (System.Exception ex)
            {
                DialogBox.Error("Vui lòng nhập các giá trị đơn giá, diện tích để tính ra thành tiền trước  khi nhập chiết khấu \n Lỗi: " + ex.Message);
            }
            
        }

        private void lkLoaiTien_DatCoc_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lk = sender as LookUpEdit;
            gvTienCoc.SetFocusedRowCellValue("TyGia", lk.GetColumnValue("TyGia"));
        }

        private void dateNgayHH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                TaoLichThanhToanTienThue();
        }

        private void lkLoaiTienNgoaiTe_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spTyGiaNgoaiTe.EditValue = lkLoaiTienNgoaiTe.GetColumnValue("TyGia");
                spGiaThueNgoaiTe.EditValue = spGiaThue.Value / spTyGiaNgoaiTe.Value;
            }
            catch (System.Exception ex)
            {
                DialogBox.Error("Vui lòng chọn ngoại tệ khác, ngoại tệ này chưa được cấu hình tỷ giá trong danhn mục ngoại tệ. \n Lỗi: " + ex.Message);
            }
            
        }

        private void glkMatBangThiCong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkMB = (sender as GridLookUpEdit);
                var _MaMB = (int)lkMB.EditValue;

                var r = lkMB.Properties.GetRowByKeyValue(lkMB.EditValue);
                var type = r.GetType();
                gvThiCong.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));

                if (r != null)
                {
                    var phidichvu = (gvThiCong.GetFocusedRowCellValue("PhiDichVu") as decimal?) ?? 0;
                    var phithicong = (gvThiCong.GetFocusedRowCellValue("PhiThiCong") as decimal?) ?? 0;
                    var tylevat = (gvThiCong.GetFocusedRowCellValue("TyLeVAT") as decimal?) ?? 0;
                    var tienvat = (gvThiCong.GetFocusedRowCellValue("TienVAT") as decimal?) ?? 0;
                    var tyleck = (gvThiCong.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                    var tienck = (gvThiCong.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;
                    //var _DonGia = (gvChiTiet.GetFocusedRowCellValue("DonGia") as decimal?) ?? 0;
                    //var _PhiDichVu = (gvChiTiet.GetFocusedRowCellValue("PhiDichVu") as decimal?) ?? 0;
                    //var _TyLeVAT = (gvChiTiet.GetFocusedRowCellValue("TyLeVAT") as decimal?) ?? 0;
                    //var _TyLeCK = (gvChiTiet.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                    //var _TienCK = (gvChiTiet.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;
                    //var _TongGiaThue = (gvChiTiet.GetFocusedRowCellValue("TongGiaThue") as decimal?) ?? 0;

                    gvThiCong.SetFocusedRowCellValue("PhiDichVu", phidichvu);
                    gvThiCong.SetFocusedRowCellValue("PhiThiCong", phithicong);
                    gvThiCong.SetFocusedRowCellValue("TyLeVAT", tylevat);
                    gvThiCong.SetFocusedRowCellValue("TienVAT", tienvat);
                    gvThiCong.SetFocusedRowCellValue("TyLeCK", tyleck);
                    gvThiCong.SetFocusedRowCellValue("TienCK", tienck);

                    //gvChiTiet.SetFocusedRowCellValue("ThanhTien", g.DienTich * _DonGia);

                    //var ltGiaThue = (from g in db.mbGiaThues
                    //                 join mb in db.mbMatBangs on g.MaMB equals mb.MaMB
                    //                 join lt in db.LoaiTiens on mb.MaLT equals lt.ID
                    //                 where g.MaMB == _MaMB
                    //                 select new { g.MaLG, DonGia = g.DonGia * lt.TyGia, g.DienTich }).ToList();
                    //if (ltGiaThue.Count() > 0)
                    //{
                    //    //Update du lieu cho dong hien tai
                    //    foreach (var g in ltGiaThue)
                    //    {
                    //        if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG).Count() == 0)
                    //        {
                    //            var _DonGia = g.DonGia / (decimal?)lkLoaiTien.GetColumnValue("TyGia");
                    //            gvChiTiet.SetFocusedRowCellValue("MaMB", _MaMB);
                    //            gvChiTiet.SetFocusedRowCellValue("MaLG", g.MaLG);
                    //            gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                    //            gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                    //            gvChiTiet.SetFocusedRowCellValue("ThanhTien", g.DienTich * _DonGia);
                    //            gvChiTiet.UpdateCurrentRow();
                    //            break;
                    //        }
                    //    }

                    //    //Them du lieu neu chua co
                    //    foreach (var g in ltGiaThue)
                    //    {
                    //        if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG).Count() == 0)
                    //        {
                    //            var objCT = new ctChiTiet();
                    //            objCT.MaMB = _MaMB;
                    //            objCT.MaLG = g.MaLG;
                    //            objCT.DonGia = g.DonGia / (decimal?)lkLoaiTien.GetColumnValue("TyGia");
                    //            objCT.DienTich = g.DienTich;
                    //            objCT.ThanhTien = objCT.DonGia * objCT.DienTich;
                    //            objHD.ctChiTiets.Add(objCT);
                    //        }
                    //    }
                    //}

                    //this.TinhGiaThue();
                    TinhThanhTien();
                }
            }
            catch { }
        }

        private void lkLoaiGia_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var frm = new ToaNha.frmLoaiGiaThue())
                {
                    frm.ShowDialog();
                }
                var data = new MasterDataContext();
                lkLoaiGia.DataSource = from lg in data.LoaiGiaThues
                                       join lt in data.LoaiTiens on lg.MaLT equals lt.ID
                                       where lg.MaTN == this.MaTN
                                       orderby lg.TenLG
                                       select new { lg.ID, lg.TenLG, DonGia = lg.DonGia, LoaiTien = lt.KyHieuLT };
            }
            catch { }
            
        }

        private void glkDatCoc_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (editor.Properties.Buttons.IndexOf(e.Button) == 1)
            {
                try
                {
                    if (glkDatCoc.EditValue == null)
                    {
                        return;
                    }

                    using (frmEdit_PhieuDatCoc frm = new frmEdit_PhieuDatCoc() { MaTN = (byte?)MaTN, ID = (int?)glkDatCoc.EditValue })
                    {
                        frm.IsView = true;
                        frm.ShowDialog();
                    }
                }
                catch { }

            }
        }

        private void glkDatCoc_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            try
            {
                if (item.EditValue != null)
                {
                    glMatBang.DataSource = lkMB.DataSource = glkMatBangThiCong.DataSource = from mb in db.mbMatBangs
                                                                                            join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                                                                            join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                                                                            join dc in db.PhieuDatCoc_GiuCho_ChiTiets on mb.MaMB equals dc.MaMB
                                                                                            where k.MaTN == this.MaTN
                                                                                                & dc.ID_PhieuDatCoc_GiuCho == Convert.ToInt32( item.EditValue)
                                                                                            orderby mb.MaSoMB
                                                                                            select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich };
                    //glkDatCoc.EditValue = item.EditValue;
                }

            }
            catch (System.Exception ex)
            { }

        }

        private void spDichVuDieuHoaChieuSang_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("PhiDieuHoaChieuSang", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void ckbLamTron_CheckedChanged(object sender, EventArgs e)
        {
            SetLamTron();
        }

        private void dateNgayHH_EditValueChanged(object sender, EventArgs e)
        {
            spThoiHan.EditValue = Math.Round(Common.GetTotalMonth(dateNgayHL.DateTime, dateNgayHH.DateTime), 0, MidpointRounding.AwayFromZero);
        }
    }

    public class LichThanhToanItem
    {
        public int? ID { get; set; }
        public int? DotTT { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? DienTich { get; set; }
        public string DienGiai { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public decimal? SoThang { get; set; }
        public int? MaLoaiTien { get; set; }
        public decimal? TyGia { get; set; }
        public decimal? SoTien { get; set; }
        public decimal? SoTienQD { get; set; }
    }
}