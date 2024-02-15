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
using DevExpress.XtraGrid.Views.Grid;
namespace LandSoftBuilding.Lease
{
    public partial class frmChuyenPhong : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPL { get; set; }
        public int? MaHD { get; set; }

        ctHopDong objHD;
        ctPhuLuc objPL;

        MasterDataContext db = new MasterDataContext();

        public frmChuyenPhong()
        {
            InitializeComponent();
        }

        void ckb_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = sender as CheckEdit;

            gvChiTiet.SetFocusedRowCellValue("IsCheck", ckb.Checked);
            GetGiaThue();
        }

        void Load_LichThanhToan()
        {
            var ltLTT = db.ctLichThanhToans.Where(o => o.MaHD == MaHD).GroupBy(o => new { o.DotTT, o.TuNgay, o.DenNgay }).Select(o => new { o.Key.DotTT, o.Key.TuNgay, o.Key.DenNgay });
            glkKyTT_From.Properties.DataSource = ltLTT;
            glkKyTT_To.Properties.DataSource = ltLTT;
        }

        private void itemSubmit_Click(object sender, EventArgs e)
        {
            DateTime? tuNgay = (DateTime?)glkKyTT_From.EditValue;
            DateTime? denNgay = (DateTime?)glkKyTT_To.EditValue;

            if (tuNgay == null | denNgay == null)
            {
                DialogBox.Error("Vui lòng chọn Từ ngày - Đến ngày");
                return;
            }

            if (SqlMethods.DateDiffDay(tuNgay, denNgay) < 0)
            {
                DialogBox.Error("Từ ngày - Đến ngày không hợp lệ");
                return;
            }

            objPL.SoPL = txtSoPL.Text;
            objPL.NgayPL = dateNgayDC.DateTime;
            objPL.TuNgay = tuNgay;
            objPL.DenNgay = denNgay;

            // Lấy danh sách mặt bằng áp dụng
            for (int i = 0; i < gvChiTiet.RowCount; i++)
            {
                var item = (ChuyenPhongItem)gvChiTiet.GetRow(i);

                if (item.MaMB != null)
                {

                    // Lưu chi tiết phụ lục
                    var ctpl = new ctPhuLuc_ChiTiet();
                    ctpl.MaMB = item.MaMB_old;
                    ctpl.MaMB_new = item.MaMB;
                    objPL.ctPhuLuc_ChiTiets.Add(ctpl);

                    var ct_old = db.ctChiTiets.Single(o => o.MaHDCT == this.MaHD & o.MaMB == item.MaMB_old);
                    ct_old.DenNgay = tuNgay;
                    ct_old.NgungSuDung = true;

                    var ct = new ctChiTiet();
                    ct.MaHDCT = this.MaHD;
                    ct.MaMB = item.MaMB;
                    ct.MaLG = item.MaLG;
                    ct.DienTich = item.DienTich;
                    ct.DonGia = item.DonGia;
                    ct.TongGiaThue = item.TongGiaThue;
                    ct.TyLeVAT = item.TyLeVAT;
                    ct.TienVAT = item.TienVAT;
                    ct.TyLeCK = item.TyLeCK;
                    ct.TienCK = item.TienCK;
                    ct.ThanhTien = item.ThanhTien;
                    ct.TuNgay = tuNgay;
                    ct.DenNgay = denNgay;
                    db.ctChiTiets.InsertOnSubmit(ct);

                    // Điều chỉnh lịch thanh toán
                    var ltLTT = db.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(tuNgay, o.TuNgay) >= 0
                                                                    & SqlMethods.DateDiffDay(o.DenNgay, denNgay) >= 0
                                                                    & o.MaMB == item.MaMB_old
                                                                    & o.MaHD == objHD.ID
                                                                    & !o.IsKhuyenMai.GetValueOrDefault());

                    //foreach (var ltt in ltLTT)
                    //{
                    //    ltt.MaMB = item.MaMB;
                    //    ltt.DonGia = item.DonGia;
                    //    ltt.DienTich = item.DienTich;
                    //    ltt.TyLeVAT = item.TyLeVAT;
                    //    ltt.TyLeCK = item.TyLeCK;
                    //    SchedulePaymentCls.ctLichThanhToan(ltt);
                    //}
                }

                db.SubmitChanges();
                this.Close();

            }
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLSDCGEdit_Load(object sender, EventArgs e)
        {
            objHD = db.ctHopDongs.Single(o => o.ID == MaHD);
            objPL = db.ctPhuLucs.SingleOrDefault(o => o.MaPL == MaPL);

            //Load mat bang
            glMatBang.DataSource = lkMB.DataSource = from mb in db.mbMatBangs
                                   join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                   join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                   where k.MaTN == objHD.MaTN
                                   orderby mb.MaSoMB
                                   select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich };

            //Load lao gia thue
            lkLoaiGia.DataSource = from lg in db.LoaiGiaThues
                                   join lt in db.LoaiTiens on lg.MaLT equals lt.ID
                                   where lg.MaTN == objHD.MaTN
                                   orderby lg.TenLG
                                   select new { lg.ID, lg.TenLG, DonGia = lg.DonGia * lt.TyGia };

            if (objPL == null)
            {
                //txtSoPL.EditValue = LeaseScheduleCls.TaoSoCT_PhuLuc(MaHD);
                dateNgayDC.EditValue = DateTime.Now;

                objPL = new ctPhuLuc();
                objPL.LoaiPL = (int)PhuLuc.PhuLucCls.LoaiPhuLuc.ChuyenPhong;
                objPL.MaHD = MaHD;
                objPL.MaNVN = Common.User.MaNV;
                objPL.NgayNhap = DateTime.Now;
                db.ctPhuLucs.InsertOnSubmit(objPL);
            }

            gcChiTiet.DataSource = from p in db.ctChiTiets
                               where !p.NgungSuDung.GetValueOrDefault()
                               & p.MaHDCT == this.MaHD
                               select new ChuyenPhongItem
                               {
                                   MaMB_old = p.MaMB
                               };

            Load_LichThanhToan();
        }

        void GetGiaThue()
        {
            List<decimal?> ltGiaThue = new List<decimal?>();

            for (int i = 0; i < gvChiTiet.RowCount; i++)
            {
                if ((bool?)gvChiTiet.GetRowCellValue(i, "IsCheck") == true)
                    ltGiaThue.Add((decimal?)gvChiTiet.GetRowCellValue(i, "DonGia"));
            }

            var giaThue = ltGiaThue.Distinct();

        }

        private void spTyLeDC_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void gvLTT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle >= 0 & (bool?)view.GetRowCellValue(e.RowHandle, "IsCheck") == true)
            {
                 e.Appearance.BackColor = Color.NavajoWhite;
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.RefreshData();
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.RefreshData();
        }

        private void dateDenNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            gvChiTiet.RefreshData();
        }

        private void dateTuNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            gvChiTiet.RefreshData();
        }

        private void cmbMatBang_EditValueChanged(object sender, EventArgs e)
        {
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

        #region Chi tiet mat bang
        private void gvChiTiet_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("DienTich", 0);
            gvChiTiet.SetFocusedRowCellValue("DonGia", 0);
            gvChiTiet.SetFocusedRowCellValue("PhiDichVu", 0);
            gvChiTiet.SetFocusedRowCellValue("PhiSuaChua", 0);
            gvChiTiet.SetFocusedRowCellValue("NgungSuDung", false);
            gvChiTiet.SetFocusedRowCellValue("TyLeVAT", 0.1);
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
                                var _DonGia = g.DonGia;

                                gvChiTiet.SetFocusedRowCellValue("MaMB", _MaMB);
                                gvChiTiet.SetFocusedRowCellValue("MaLG", g.MaLG);
                                gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                                gvChiTiet.SetFocusedRowCellValue("DienTich", (decimal?)type.GetProperty("DienTich").GetValue(r, null));
                                gvChiTiet.SetFocusedRowCellValue("ThanhTien", g.DienTich * _DonGia);
                                gvChiTiet.UpdateCurrentRow();
                                break;
                            }
                        }
                    }
                }
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
            gvChiTiet.SetFocusedRowCellValue("DonGia", 0);
            gvChiTiet.SetFocusedRowCellValue("TongGiaThue", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spTyLeVAT_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("TyLeVAT", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spTyLeCK_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("TienCK", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void spTienCK_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("TyLeCK", 0);
            TinhThanhTien();
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
        #endregion

        private class ChuyenPhongItem : ctChiTiet
        {
            public int? MaMB_old { get; set; }
        }

        private void gvChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void glkKyTT_To_EditValueChanged(object sender, EventArgs e)
        {
            DateTime? tuNgay = (DateTime?)glkKyTT_From.EditValue;
            DateTime? denNgay = (DateTime?)glkKyTT_To.EditValue;

            gcLTT.DataSource = db.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(tuNgay, o.TuNgay) >= 0
                                                                    & SqlMethods.DateDiffDay(o.DenNgay, denNgay) >= 0
                                                                    & o.MaHD == objHD.ID
                                                                    & !o.IsKhuyenMai.GetValueOrDefault());
        }
    }
}