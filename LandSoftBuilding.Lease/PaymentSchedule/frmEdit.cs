using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using Library.App_Codes;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Lease.PaymentSchedule
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? MaHD { get; set; }
        public int? MaMB { get; set; }
        public int? MaLDV { get; set; }
        ctHopDong objHD;

        MasterDataContext db = new MasterDataContext();
        decimal? ThanhTien, TyGia;

        bool IsSoNguyen(decimal val)
        {
            try
            {
                Convert.ToInt32(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                objHD = db.ctHopDongs.Single(o => o.ID == MaHD);

                gcLTT.KeyUp += Common.GridViewKeyUp;
                gvLTT.InvalidRowException += Common.InvalidRowException;


                //Load loai tien
                lkLoaiTien.Properties.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();
                lkLoaiTien_LTT.DataSource = lkLoaiTien.Properties.DataSource;

                var objMB = (from mb in db.ctChiTiets
                             join hd in db.ctHopDongs on mb.MaHDCT equals hd.ID
                             where mb.MaHDCT == this.MaHD & mb.MaMB == this.MaMB
                             select new
                             {
                                 mb.ThanhTien,
                                 mb.PhiSuaChua,
                                 hd.TyGia,
                                 hd.KyTT,
                                 hd.NgayHL,
                                 hd.NgayHH
                             }).First();

                this.ThanhTien = this.MaLDV == 2 ? objMB.ThanhTien : objMB.PhiSuaChua;

                this.TyGia = objMB.TyGia;
                spKyTT.EditValue = objMB.KyTT;
                dateTuNgay.EditValue = objMB.NgayHL;
                dateDenNgay.EditValue = objMB.NgayHH;

                gcLTT.DataSource = db.ctLichThanhToans.Where(o => o.MaMB == this.MaMB & o.MaHD == this.MaHD);

                spDienTich_.EditValueChanged += SpDienTich__EditValueChanged;
                spDonGia_.EditValueChanged += SpDonGia__EditValueChanged;
            }
            catch { }
        }

        private void SpDonGia__EditValueChanged(object sender, EventArgs e)
        {
            gvLTT.SetFocusedRowCellValue("DonGia", (sender as SpinEdit).Value);

            var ltt = (ctLichThanhToan)gvLTT.GetFocusedRow();
            SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
        }

        private void SpDienTich__EditValueChanged(object sender, EventArgs e)
        {
            gvLTT.SetFocusedRowCellValue("DienTich", (sender as SpinEdit).Value);
            var ltt = (ctLichThanhToan)gvLTT.GetFocusedRow();
            SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());
        }

        private void btnTaoLichThanhToan_Click(object sender, EventArgs e)
        {
            #region Rang buoc dieu kien
            if (dateTuNgay.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập từ ngày");
                return;
            }

            if (dateDenNgay.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập đến ngày");
                return;
            }

            if (spKyTT.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập kỳ thanh toán");
                return;
            }
            #endregion

            var ltLTT = gvLTT.DataSource as IEnumerable<ctLichThanhToan>;
            var _TuNgay = dateTuNgay.DateTime;
            var _NgayHH = dateDenNgay.DateTime;

            var _Count = ltLTT.Where(p => SqlMethods.DateDiffDay(p.TuNgay, _TuNgay) >= 0 & SqlMethods.DateDiffDay(_TuNgay, p.DenNgay) >= 0).Count();
            if (_Count > 0)
            {
                DialogBox.Error("Khoảng thời gian không hợp lệ");
                return;
            }

            _Count = ltLTT.Where(p => SqlMethods.DateDiffDay(p.TuNgay, _NgayHH) >= 0 & SqlMethods.DateDiffDay(_NgayHH, p.DenNgay) >= 0).Count();
            if (_Count > 0)
            {
                DialogBox.Error("Khoảng thời gian không hợp lệ");
                return;
            }

            int _DotTT = ltLTT.Where(p => SqlMethods.DateDiffDay(p.DenNgay, _TuNgay) > 0).Max(p => p.DotTT).GetValueOrDefault();

            while (_TuNgay.CompareTo(_NgayHH) < 0)
            {
                _DotTT++;
                decimal _KyTT = spKyTT.Value;
                var _DenNgay = _TuNgay.AddMonths(Convert.ToInt32(spKyTT.Value)).AddDays(-1);

                if (_DenNgay.CompareTo(_NgayHH) > 0)
                {
                    _DenNgay = _NgayHH;
                    _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                }
               
                if (_KyTT > 0)
                {
                    gvLTT.AddNewRow();
                    gvLTT.SetFocusedRowCellValue("MaHD", this.MaHD);
                    gvLTT.SetFocusedRowCellValue("DotTT", _DotTT);
                    gvLTT.SetFocusedRowCellValue("MaMB", this.MaMB);
                    gvLTT.SetFocusedRowCellValue("MaLDV", 2);
                    gvLTT.SetFocusedRowCellValue("TuNgay", _TuNgay);
                    gvLTT.SetFocusedRowCellValue("DenNgay", _DenNgay);
                    gvLTT.SetFocusedRowCellValue("SoThang", _KyTT);
                    gvLTT.SetFocusedRowCellValue("DonGia", spDonGia.Value);
                    gvLTT.SetFocusedRowCellValue("DienTich", spDienTich.Value);
                    gvLTT.SetFocusedRowCellValue("MaLoaiTien", (int?)lkLoaiTien.EditValue);
                    gvLTT.SetFocusedRowCellValue("DienGiai", string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay));

                    var ltt = (ctLichThanhToan)gvLTT.GetFocusedRow();

                    SchedulePaymentCls.ctLichThanhToan(ltt, objHD.IsLamTron.GetValueOrDefault());

                    if(lkLoaiTien.EditValue != null && spTyGia.Value > 0)
                        SchedulePaymentCls.UpdateTyGia(ltt, (int)lkLoaiTien.EditValue, spTyGia.Value);
                }

                _TuNgay = _DenNgay.AddDays(1);
            }

            gvLTT.FocusedRowHandle = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                db.SubmitChanges();

                DialogBox.Success();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvLTT_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var ltt = gvLTT.GetRow(e.RowHandle) as LichThanhToanItem;

            switch (e.Column.FieldName)
            {
                case "TuNgay":
                case "SoThang":
                    if (ltt.SoThang != null)
                    {
                        ltt.SoTien = ltt.SoThang * this.ThanhTien;
                        ltt.SoTienQD = ltt.SoTien * this.TyGia;

                        if (ltt.TuNgay != null)
                        {
                            ltt.DenNgay = ltt.TuNgay.Value.AddDays(Convert.ToDouble(ltt.SoThang.Value * 30 - 1));
                        }
                    }
                    break;
                case "SoTien":
                    ltt.SoTienQD = ltt.SoTien * this.TyGia;
                    break;
            }
        }

        private void gvLTT_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            var ltt = e.Row as LichThanhToanItem;
            if (ltt == null) return;
            if (ltt.DotTT == null)
            {
                e.ErrorText = "Vui lòng nhập đợt thanh toán";
                e.Valid = false;
                return;
            }
            else
            {
                if (Common.Duplication(sender as GridView, e.RowHandle, "DotTT", ltt.DotTT.ToString()))
                {
                    e.ErrorText = "Đợt thanh toán đã tồn tại";
                    e.Valid = false;
                    return;
                }
            }

            if (ltt.TuNgay == null)
            {
                e.ErrorText = "Vui lòng nhập [Từ ngày]";
                e.Valid = false;
                return;
            }

            if (ltt.SoThang == null)
            {
                e.ErrorText = "Vui lòng nhập [Số tháng]";
                e.Valid = false;
                return;
            }

            if (ltt.DenNgay == null)
            {
                e.ErrorText = "Vui lòng nhập [Đến ngày]";
                e.Valid = false;
                return;
            }

            if (ltt.SoTien == null)
            {
                e.ErrorText = "Vui lòng nhập [Số tiền]";
                e.Valid = false;
                return;
            }

            if (ltt.SoTienQD == null)
            {
                e.ErrorText = "Vui lòng nhập [Số tiền quy đổi]";
                e.Valid = false;
                return;
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            spDonGia.EditValue = 0;
            spDienTich.EditValue = 0;
            this.ThanhTien = 0;

            try
            {
                var ltt = objHD.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(o.DenNgay, dateDenNgay.DateTime) >= 0 & o.MaMB == MaMB).OrderByDescending(o => o.DenNgay).First();

                spDonGia.EditValue = ltt.DonGia;
                spDienTich.EditValue = ltt.DienTich;
                lkLoaiTien.EditValue = ltt.MaLoaiTien;
                spTyGia.EditValue = ltt.TyGia;

                this.ThanhTien = ltt.DonGia * ltt.DienTich;

                if (objHD.IsLamTron.GetValueOrDefault())
                {
                    var SoTienLe = this.ThanhTien % 1000;
                    var SoTienChan = this.ThanhTien - SoTienLe;
                    SoTienLe = SoTienLe <= 500 ? 0 : 1000;
                    this.ThanhTien = SoTienLe + SoTienChan;
                }
            }
            catch
            {
            }
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

        private void gvLTT_CellValueChanged_1(object sender, CellValueChangedEventArgs e)
        {
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

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spTyGia.EditValue = lkLoaiTien.GetColumnValue("TyGia");
        }

    }
}