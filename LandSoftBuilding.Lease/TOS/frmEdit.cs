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

namespace LandSoftBuilding.Lease.TOS
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db;
        ctHopDong objHD;
        int MaLDV_Thue = 2, MaLDV_SuaChua = 3, MaLDV_TienCoc = 4;

        void TinhGiaThue()
        {
            spGiaThue.EditValue = objHD.ctChiTiets.Sum(p => p.ThanhTien).GetValueOrDefault();
            if(spTyGiaNgoaiTe.Value > 0) spGiaThueNgoaiTe.EditValue = spGiaThue.Value / spTyGiaNgoaiTe.Value;
        }

        void TinhThanhTien()
        {
            try
            {
                var _DienTich = (gvChiTiet.GetFocusedRowCellValue("DienTich") as decimal?) ?? 0;
                var _DonGia = (gvChiTiet.GetFocusedRowCellValue("DonGia") as decimal?) ?? 0;
                var _PhiDichVu = (gvChiTiet.GetFocusedRowCellValue("PhiDichVu") as decimal?) ?? 0;
                var _TyLeVAT = (gvChiTiet.GetFocusedRowCellValue("TyLeVAT") as decimal?) ?? 0;
                var _TyLeCK = (gvChiTiet.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                var _TienCK = (gvChiTiet.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;
                var _TongGiaThue = (gvChiTiet.GetFocusedRowCellValue("TongGiaThue") as decimal?) ?? 0;

                _TongGiaThue = _DienTich * (_DonGia + _PhiDichVu);

                if (ckbLamTron.Checked)
                {
                    var SoTienLe = _TongGiaThue % 1000;
                    var SoTienChan = _TongGiaThue - SoTienLe;
                    _TongGiaThue = SoTienChan + SoTienLe;
                }

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
                if (ckbLamTron.Checked)
                {
                    var SoTienLe = ct.TongGiaThue % 1000;
                    var SoTienChan = ct.TongGiaThue - SoTienLe;
                    SoTienLe = SoTienLe <= 500 ? 0 : 1000;
                    ct.TongGiaThue = SoTienLe + SoTienChan;
                }
                else
                {
                    ct.TongGiaThue = ct.DonGia * ct.DienTich;
                }


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
                if (ltLich.Where(p => p.DotTT == i.DotTT).Count() == 0)
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
                objLTT.SoThang = l.SoThang;
                objLTT.SoTien = l.SoTien;
                objLTT.MaLoaiTien =(int)l.MaLoaiTien;
                //objLTT.TyGia = l.TyGia;
                objLTT.SoTienQD = l.SoTienQD;
                objLTT.DienGiai = l.DienGiai;
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
                        DienGiai = l.DienGiai
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
                

                if(mb.TuNgay != null & mb.DenNgay != null)
                {
                    int _DotTT = 0;
                    var _TuNgay = (DateTime)mb.TuNgay;
                    while (_TuNgay.CompareTo(mb.DenNgay) < 0)
                    {
                        _DotTT++;
                        decimal _KyTT = (int)spKyTT.Value;
                        var _DenNgay = _TuNgay.AddMonths((int)spKyTT.Value).AddDays(-1);
                        if (_DenNgay.CompareTo(mb.DenNgay) > 0)
                        {
                            _DenNgay = (DateTime)mb.DenNgay;
                            _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                        }

                        if (_KyTT > 0)
                        {
                            var _TongTien = mb.ThanhTien * _KyTT;

                            gvLTT.AddNewRow();
                            gvLTT.SetFocusedRowCellValue("DotTT", _DotTT);
                            gvLTT.SetFocusedRowCellValue("MaMB", mb.MaMB);
                            gvLTT.SetFocusedRowCellValue("MaLDV", 2);
                            gvLTT.SetFocusedRowCellValue("DienGiai", string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay));
                            gvLTT.SetFocusedRowCellValue("TuNgay", _TuNgay);
                            gvLTT.SetFocusedRowCellValue("DenNgay", _DenNgay);
                            gvLTT.SetFocusedRowCellValue("SoThang", _KyTT);
                            gvLTT.SetFocusedRowCellValue("SoTien", _TongTien);
                            gvLTT.SetFocusedRowCellValue("SoTienQD", _TongTien);
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
                            gvLTT.SetFocusedRowCellValue("ThanhTienNgoaiTe", _TongTien / (decimal)spTyGiaNgoaiTe.EditValue);
                            gvLTT.SetFocusedRowCellValue("TyGiaNgoaiTe", spTyGiaNgoaiTe.EditValue);
                            gvLTT.SetFocusedRowCellValue("LoaiTienNgoaiTe", (int?)lkLoaiTienNgoaiTe.EditValue);
                            gvLTT.SetFocusedRowCellValue("PhiDichVu", mb.PhiDichVu);
                            gvLTT.SetFocusedRowCellValue("TyLeVAT", mb.TyLeVAT);
                            gvLTT.SetFocusedRowCellValue("TyLeCK", mb.TyLeCK);

                            var item = (ctLichThanhToan)gvLTT.GetFocusedRow();
                            //SchedulePaymentCls.ctLichThanhToan(item, ckbLamTron.Checked);
                        }

                        _TuNgay = _DenNgay.AddDays(1);
                    }
                }
                
            }

            gvLTT.RefreshData();
            colPhong.GroupIndex = 0;

        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            gvChiTiet.InvalidRowException += Common.InvalidRowException;

            xtraTabControl1.SelectedTabPageIndex = 0;

            db = new MasterDataContext();
            var dbb = new MasterDataContext();

            //Load loai tien
            lkLoaiTien.Properties.DataSource =  (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();
            lkLoaiTien_LTT.DataSource  = lkLoaiTien.Properties.DataSource;
            glkLoaiTienMF.DataSource = glkLoaiTienMG.DataSource=lkLoaiTienNgoaiTe.Properties.DataSource = lkLoaiTien.Properties.DataSource;


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
            glMatBang.DataSource = lkMB.DataSource =  from mb in db.mbMatBangs
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
                                   select new { lg.ID, lg.TenLG, DonGia = lg.DonGia * lt.TyGia };
            SpinSoNgayTreThanhToan.Value = 0;
            spinHanThanhToan.Value = 0;
            

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
                spHanTT.EditValue = objHD.HanTT;
                dateNgayBG.EditValue = objHD.NgayBG;
                lkLoaiTien.EditValue = objHD.MaLT;
                spTyGia.EditValue = objHD.TyGiaHD;
                spGiaThue.EditValue = objHD.GiaThue;
                spTienCoc.EditValue = objHD.TienCoc;
                spTyLeDCGT.EditValue = objHD.TyLeDCGT;
                spSoNamDCGT.EditValue = objHD.SoNamDCGT;
                spMucDCTG.EditValue = objHD.MucDCTG;
                ckbNgungSuDung.EditValue = objHD.NgungSuDung;
                ckbLamTron.Checked = objHD.IsLamTron.GetValueOrDefault();
                //txtSoNgayFree.EditValue = objHD.SoNgayFree;
                if (objHD.IsPhuLuc == true)
                {
                    cbLoaiHopDong.SelectedIndex = 1;
                    lkHopDongLienQuan.EditValue = objHD.ParentID;
                }
                else
                {
                    cbLoaiHopDong.SelectedIndex = 0;
                }

                //bdsTienCoc.DataSource = this.GetLichThanhToans(this.MaLDV_TienCoc);
                //txtDienGiai.Text = objHD.DienGiai;
                lkLoaiTienNgoaiTe.EditValue = objHD.LoaiTienNgoaiTe;
                spTyGiaNgoaiTe.EditValue = objHD.TyGiaNgoaiTe;
                spGiaThueNgoaiTe.EditValue = objHD.GiaThueNgoaiTe;
                //glkNhaThau.EditValue = objHD.MaNhaThau;
                txtNhaThau.Text = objHD.NhaThau;
                txtGhiChu.Text = objHD.GhiChu;
                txtDieuKhoanDacBiet.Text = objHD.DieuKhoanDacBiet;
                txtDieuKhoanBoSungGiamThue.Text = objHD.DieuKhoanBoSungGiamThue;
                txtXemXetLaiTienThue.Text = objHD.XemXetLaiTienThue;
                spinTyGiaBanDau.EditValue = objHD.TyGiaApDungBanDau;
                spinDonGiaTuongDuongUSD.EditValue = objHD.DonGiaTuongDuongUSD;
                if(objHD.ThoiGIanThayDoiTyGIa != null) dateThoiGianThayDoi.DateTime = (DateTime)objHD.ThoiGIanThayDoiTyGIa;
                txtCachThayDoiTyGia.Text = objHD.CachThayDoiTyGia;
                if (objHD.NgayTangGiaTiepTheo != null) dateNgayTangGiaTiepTheo.DateTime = (DateTime)objHD.NgayTangGiaTiepTheo;
                txtTenShop.Text = objHD.TenShop;

                SpinSoNgayTreThanhToan.EditValue = objHD.SoNgayGiaHanThanhToan.GetValueOrDefault();
                spinHanThanhToan.EditValue = objHD.NgayHanThanhToan.GetValueOrDefault();

                //gcThiCong.DataSource = db.ctThiCongs.Where(_ => _.MaHD == ID);
                //gvThiCong.OptionsBehavior.Editable = false;
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

                //bdsTienCoc.DataSource = new List<LichThanhToanItem>();

                //List<ctThiCong> thicong = new List<ctThiCong>();
                //gcThiCong.DataSource = thicong.ConvertToDataTable();
            }
            gcChiTiet.DataSource = objHD.ctChiTiets;
            //gcLTT.DataSource = objHD.ctLichThanhToans;
            
        }

        private void cbLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoaiHopDong.SelectedIndex.Equals(0))
            {
                lkHopDongLienQuan.Properties.ReadOnly = true;
                lkHopDongLienQuan.EditValue = null;
            }
            else // Phu Luc
            {
                lkHopDongLienQuan.Properties.ReadOnly = false;
            }
        }

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spTyGia.EditValue = lkLoaiTien.GetColumnValue("TyGia");
            TaoLichThanhToanTienThue();
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lkHopDongLienQuan.Properties.DataSource = (from hd in db.ctHopDongs
                                                           where hd.IsPhuLuc == false & hd.MaKH == (int)glKhachHang.EditValue
                                                           select new
                                                           {
                                                               hd.ID,
                                                               hd.SoHDCT,
                                                               hd.NgayKy
                                                           }).ToList();
                lkHopDongLienQuan.EditValue = null;
            }
            catch { }
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

            //if (gvChiTiet.GetRowCellValue(e.RowHandle, "MaLG") == null)
            //{
            //    e.ErrorText = "Vui lòng chọn loại giá!";
            //    e.Valid = false;
            //    return;
            //}
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
                                var _DonGia = g.DonGia / (decimal?)lkLoaiTien.GetColumnValue("TyGia");
                                gvChiTiet.SetFocusedRowCellValue("MaMB", _MaMB);
                                gvChiTiet.SetFocusedRowCellValue("MaLG", g.MaLG);
                                gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                                gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                                gvChiTiet.SetFocusedRowCellValue("ThanhTien", g.DienTich * _DonGia);
                                gvChiTiet.UpdateCurrentRow();
                                break;
                            }
                        }

                        //Them du lieu neu chua co
                        foreach (var g in ltGiaThue)
                        {
                            if (objHD.ctChiTiets.Where(p => p.MaMB == _MaMB & p.MaLG == g.MaLG ).Count() == 0)
                            {
                                var objCT = new ctChiTiet();
                                objCT.MaMB = _MaMB;
                                objCT.MaLG = g.MaLG;
                                objCT.DonGia = g.DonGia / (decimal?)lkLoaiTien.GetColumnValue("TyGia");
                                objCT.DienTich = g.DienTich;
                                objCT.ThanhTien = objCT.DonGia * objCT.DienTich;
                                objHD.ctChiTiets.Add(objCT);
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
                gvChiTiet.SetFocusedRowCellValue("DonGia", (decimal)(sender as LookUpEdit).GetColumnValue("DonGia") / spTyGia.Value);
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
            gvChiTiet.UpdateCurrentRow();

            #region Validate
            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng!");
                glKhachHang.Focus();
                return;
            }

            if (String.IsNullOrEmpty(cbLoaiHopDong.Text))
            {
                DialogBox.Error("Vui lòng chọn loại hợp đồng!");
                cbLoaiHopDong.Focus();
                return;
            }

            if (lkHopDongLienQuan.EditValue == null)
            {
                if (cbLoaiHopDong.SelectedIndex.Equals(1)) // phu luc
                {
                    DialogBox.Error("Vui lòng chọn Hợp đồng liên quan!");
                    lkHopDongLienQuan.Focus();
                    return;
                }

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
            //if (objHD.ID == 0)
            //{
            //    bool KT = false;
            //    foreach (var item in objHD.ctChiTiets)
            //    {
            //        var objKTHopDong = (from hd in db.ctHopDongs
            //                            join ct in db.ctChiTiets on hd.ID equals ct.MaHDCT
            //                            join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
            //                            where hd.MaTN == this.MaTN
            //                            & SqlMethods.DateDiffDay(hd.NgayHL, dateNgayHL.DateTime) >= 0
            //                            & SqlMethods.DateDiffDay(dateNgayHL.DateTime, hd.NgayHH) >= 0
            //                            & ct.MaMB == item.MaMB
            //                            & !ct.NgungSuDung.GetValueOrDefault()
            //                            & hd.ctThanhLies.Count() == 0
            //                            select new
            //                            {
            //                                Loai = ct.MaMB == null ? "Ghế" : "Phòng",
            //                                mb.MaSoMB,
            //                                hd.ID,
            //                                hd.SoHDCT,
            //                                hd.NgayHL,
            //                                NgayHH = ct.DenNgay,
            //                            }).FirstOrDefault();

            //        if (objKTHopDong != null)
            //        {
            //            string Error = string.Format("{0} {1} đã được cho thuê bởi hợp đồng {2} trong thời gian {3:dd/MM/yyyy} - {4:dd/MM/yyyy} . Vui lòng chọn thời gian hiệu lực khác!", objKTHopDong.Loai, objKTHopDong.MaSoMB, objKTHopDong.SoHDCT, objKTHopDong.NgayHL, objKTHopDong.NgayHH);
            //            DialogBox.Error(Error);
            //            return;
            //        }
            //    }
            //}
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

            //if (ListMB.Count > 0)
            //{
            //    MasterDataContext dbo = new MasterDataContext();
            //    foreach (var i in ListMB)
            //    {
            //        var query = dbo.ctLichThanhToans.Where(p => p.MaMB == i).ToList();
            //        foreach (var k in query)
            //        {
            //            dbo.ctLichThanhToans.DeleteOnSubmit(k);
            //        }


            //    }
            //    dbo.SubmitChanges();

            //}


            objHD.MaKH = (int?)glKhachHang.EditValue;
            objHD.SoHDCT = txtSoHDCT.Text;
            objHD.NgayKy = (DateTime?)dateNgayKy.EditValue;
            objHD.NgayHL = (DateTime?)dateNgayHL.EditValue;
            objHD.NgayHH = (DateTime?)dateNgayHH.EditValue;
            objHD.ThoiHan = spThoiHan.Value;
            objHD.KyTT = Convert.ToInt32(spKyTT.Value);
            objHD.HanTT = Convert.ToInt32(spHanTT.Value);
            objHD.NgayBG = (DateTime?)dateNgayBG.EditValue;
            objHD.MaLT = (int?)lkLoaiTien.EditValue;
            objHD.TyGiaHD = spTyGia.Value;
            objHD.TyGia = spTyGia.Value;
            objHD.MucDCTG = spMucDCTG.Value;
            objHD.GiaThue = spGiaThue.Value;
            objHD.GiaThueNgoaiTe = spGiaThueNgoaiTe.Value;
            objHD.TienCoc = spTienCoc.Value;
            objHD.TyLeDCGT = spTyLeDCGT.Value;
            objHD.SoNamDCGT = Convert.ToInt32(spSoNamDCGT.Value);
            objHD.NgungSuDung = ckbNgungSuDung.Checked;
            objHD.IsLamTron = ckbLamTron.Checked;
            objHD.TyGiaNgoaiTe = spTyGiaNgoaiTe.Value;
            objHD.LoaiTienNgoaiTe = (int?) lkLoaiTienNgoaiTe.EditValue;
            try
            {
                
                
                objHD.DonGiaTuongDuongUSD = spinDonGiaTuongDuongUSD.Value;
                objHD.TyGiaApDungBanDau = spinTyGiaBanDau.Value;
                objHD.ThoiGIanThayDoiTyGIa = (DateTime?)dateThoiGianThayDoi.EditValue;
                
                objHD.NgayTangGiaTiepTheo = (DateTime?)dateNgayTangGiaTiepTheo.EditValue;
                //objHD.MaNhaThau = (int?)glkNhaThau.EditValue;
            }
            catch { }

            objHD.NhaThau = txtNhaThau.Text;
            objHD.CachThayDoiTyGia = txtCachThayDoiTyGia.Text;
            objHD.DieuKhoanDacBiet = txtDieuKhoanDacBiet.Text;
            objHD.DieuKhoanBoSungGiamThue = txtDieuKhoanBoSungGiamThue.Text;
            objHD.XemXetLaiTienThue = txtXemXetLaiTienThue.Text;
            objHD.GhiChu = txtGhiChu.Text;
            objHD.TenShop = txtTenShop.Text;
            //objHD.DienGiai = txtDienGiai.Text;
            //objHD.SoNgayFree =  Convert.ToInt32(txtSoNgayFree.Value);
            //objHD.NgayTruocGiaHan = (DateTime?)dateNgayHH.EditValue;

            objHD.IsHopDongTOS = true;

            objHD.SoNgayGiaHanThanhToan = (int?)SpinSoNgayTreThanhToan.Value;
            objHD.NgayHanThanhToan = (int?)spinHanThanhToan.Value;

            if (cbLoaiHopDong.SelectedIndex.Equals(1)) // Phu luc
            {
                objHD.IsPhuLuc = true;
                objHD.ParentID = (int)lkHopDongLienQuan.GetColumnValue("ID");
            }
            else
            {
                objHD.IsPhuLuc = false;
                objHD.ParentID = null;
            }

            //this.UpdateLichThanhToan(this.MaLDV_TienCoc);

            if (objHD.ID == 0)
            {
                //this.TaoLichThanhToanTienThue();

                foreach (var ct in objHD.ctChiTiets)
                {
                    ct.NgayBDTaoLTT = ct.TuNgay;
                    ct.HanhDong = "EDIT";
                    ct.LoaiTienNgoaiTe = objHD.LoaiTienNgoaiTe;
                    ct.TyGiaNgoaiTe = objHD.TyGiaNgoaiTe;
                    ct.ThanhTienNgoaiTe = ct.ThanhTien / objHD.TyGiaNgoaiTe;
                }
            }



            try
            {
                db.SubmitChanges();
                SaoLuuHopDongGoc();

                #region Lưu thi công và tạo lịch thanh toán cho thi công
                //for(var i = 0; i < gvThiCong.RowCount; i++)
                //{
                //    // loại thi công, thêm vào bảng thi công và thêm vào lịch thanh toán, chỉ thanh toán 1 lần
                //    //(int?) gvScheduleApartment.GetRowCellValue(i, "ApartmentId");
                //    var mamb = (int?)gvThiCong.GetRowCellValue(i, "MaMB");
                //    if (mamb == null) continue;

                //    var model = new { mahd = objHD.ID, mamb = (int?)gvThiCong.GetRowCellValue(i, "MaMB"), dientich = (decimal?)gvThiCong.GetRowCellValue(i, "DienTich"), phidichvu = (decimal?)gvThiCong.GetRowCellValue(i, "PhiDichVu"), phithicong = (decimal?)gvThiCong.GetRowCellValue(i, "PhiThiCong"), tylevat = (decimal?)gvThiCong.GetRowCellValue(i, "TyLeVAT"), tienvat = (decimal?)gvThiCong.GetRowCellValue(i, "TienVAT"), tyleck = (decimal?)gvThiCong.GetRowCellValue(i, "TyLeCK"), tienck = (decimal?)gvThiCong.GetRowCellValue(i, "TienCK"), loaitien = objHD.MaLT, tygia = objHD.TyGia, thanhtien = (decimal?)gvThiCong.GetRowCellValue(i, "ThanhTien"), loaitienngoaite = objHD.LoaiTienNgoaiTe, tygiangoaite = objHD.TyGiaNgoaiTe, thanhtienngoaite = (decimal?)gvThiCong.GetRowCellValue(i, "ThanhTien") / objHD.TyGiaNgoaiTe, tungay = objHD.NgayHL, denngay = objHD.NgayHH, hanhdong = "EDIT" };
                //    var param = new Dapper.DynamicParameters();
                //    param.AddDynamicParams(model);
                //    Library.Class.Connect.QueryConnect.Query<bool>("lease_frmimport_ctthicong_edit", param);
                //}
                #endregion

                DialogBox.Alert("Dữ liệu đã được lưu!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Lỗi: " + ex.Message);
                return;
            }
            #endregion
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

        #region Bang gia dich vu
        private void lkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            var lkLDV = sender as LookUpEdit;

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
            SpinEdit sp = sender as SpinEdit;
            var ThanhTien = (decimal?)gvLTT.GetFocusedRowCellValue("ThanhTien");
            gvLTT.SetFocusedRowCellValue("TyLeCK", sp.Value / (decimal)ThanhTien.GetValueOrDefault());
            gvLTT.SetFocusedRowCellValue("TienCK", sp.Value);
            gvLTT.SetFocusedRowCellValue("SoTien", ThanhTien - sp.Value);
            gvLTT.SetFocusedRowCellValue("SoTienQD", ThanhTien - sp.Value);
        }


        private void dateNgayHH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                TaoLichThanhToanTienThue();
        }

        private void lkLoaiTienNgoaiTe_EditValueChanged(object sender, EventArgs e)
        {
            spTyGiaNgoaiTe.EditValue = lkLoaiTienNgoaiTe.GetColumnValue("TyGia");
            if (spTyGiaNgoaiTe.Value != 0)
                spGiaThueNgoaiTe.EditValue = spGiaThue.Value * spTyGia.Value / spTyGiaNgoaiTe.Value;
        }

        private void glkMatBangThiCong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkMB = (sender as GridLookUpEdit);
                var _MaMB = (int)lkMB.EditValue;

                var r = lkMB.Properties.GetRowByKeyValue(lkMB.EditValue);
                var type = r.GetType();
                //gvThiCong.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));

                if (r != null)
                {
                    //var phidichvu = (gvThiCong.GetFocusedRowCellValue("PhiDichVu") as decimal?) ?? 0;
                    //var phithicong = (gvThiCong.GetFocusedRowCellValue("PhiThiCong") as decimal?) ?? 0;
                    //var tylevat = (gvThiCong.GetFocusedRowCellValue("TyLeVAT") as decimal?) ?? 0;
                    //var tienvat = (gvThiCong.GetFocusedRowCellValue("TienVAT") as decimal?) ?? 0;
                    //var tyleck = (gvThiCong.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                    //var tienck = (gvThiCong.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;

                    //gvThiCong.SetFocusedRowCellValue("PhiDichVu", phidichvu);
                    //gvThiCong.SetFocusedRowCellValue("PhiThiCong", phithicong);
                    //gvThiCong.SetFocusedRowCellValue("TyLeVAT", tylevat);
                    //gvThiCong.SetFocusedRowCellValue("TienVAT", tienvat);
                    //gvThiCong.SetFocusedRowCellValue("TyLeCK", tyleck);
                    //gvThiCong.SetFocusedRowCellValue("TienCK", tienck);

                    TinhThanhTien();
                }
            }
            catch { }
        }

        private void ckbLamTron_CheckedChanged(object sender, EventArgs e)
        {
            SetLamTron();
        }

        private void dateNgayHH_EditValueChanged(object sender, EventArgs e)
        {
            spThoiHan.EditValue = Math.Round(Common.GetTotalMonth(dateNgayHL.DateTime, dateNgayHH.DateTime), 0, MidpointRounding.AwayFromZero);
        }

        private void glkLoaiTienMG_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkloaitien = (sender as GridLookUpEdit);
                var loaitien_id = (int)lkloaitien.EditValue;

                var r = lkloaitien.Properties.GetRowByKeyValue(lkloaitien.EditValue);
                var type = r.GetType();
                gvChiTiet.SetFocusedRowCellValue("TyGia_MG", (decimal?)type.GetProperty("TyGia").GetValue(r, null));
            }
            catch { }
        }

        private void glkLoaiTienMF_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkloaitien = (sender as GridLookUpEdit);
                var loaitien_id = (int)lkloaitien.EditValue;

                var r = lkloaitien.Properties.GetRowByKeyValue(lkloaitien.EditValue);
                var type = r.GetType();
                gvChiTiet.SetFocusedRowCellValue("TyGia_MF", (decimal?)type.GetProperty("TyGia").GetValue(r, null));
            }
            catch { }
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