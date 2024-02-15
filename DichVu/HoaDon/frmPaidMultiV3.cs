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
using DevExpress.XtraReports.UI;

namespace DichVu.HoaDon
{
    public partial class frmPaidMultiV3 : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaMB = 0, MaPhieu = 0, Month = 0, Year = 0;
        public int? MaKH;
        public string MaSoMB = "";
        string sSoPhieu;
        public tnNhanVien objnhanvien;
        TienTeCls ttcls = new TienTeCls();
        DateTime now;
        decimal PhiQL = 0, TongPQL = 0;
        byte MaTN = 0;

        PhieuThu objPT;
        ptChiTiet objPTCT;
        List<ptChiTiet> listPTCT;
        bool first = true;
        public frmPaidMultiV3()
        {
            InitializeComponent();

            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this);
        }

        public string CountCharater(string s)
        {
            string str = "";
            switch (s.Length)
            {
                case 1:
                    str = "0000" + s;
                    break;
                case 2:
                    str = "000" + s;
                    break;
                case 3:
                    str = "00" + s;
                    break;
                case 4:
                    str = "0" + s;
                    break;
                case 5:
                    str = s;
                    break;
            }
            return str;
        }

        public string CreateKeyword(DateTime date)
        {
            string SoPhieu = "";
            string cSoPhieu = "";
            try
            {
                var obj = db.PTDemSoLuongs.FirstOrDefault(p => p.ThangDem.Value.Month == date.Month & p.ThangDem.Value.Year == date.Year);
                if (obj == null)
                {
                    var objdem = new PTDemSoLuong();
                    objdem.ThangDem = date;
                    objdem.SoLuongPhieu = 1;
                    SoPhieu = string.Format("PT{0:yyyy}/{0:MM}/00001", date);
                    db.PTDemSoLuongs.InsertOnSubmit(objdem);
                }
                else
                {
                    cSoPhieu = (obj.SoLuongPhieu + 1).ToString();
                    obj.SoLuongPhieu++;
                    SoPhieu = string.Format("PT{0:yyyy}/{0:MM}/{1}", date, CountCharater(cSoPhieu));
                }
            }
            catch { }
            return SoPhieu;
        }

        private void frmPaidMulti_Load(object sender, EventArgs e)
        {
            if (MaPhieu == 0)
            {
                LoadData();
                var list = db.PaidMultiV2(MaMB, Month, Year).ToList();
                gcDichVu.DataSource = list;

                spinTongTien.EditValue = list.Where(p => p.IsCheck.GetValueOrDefault()).Sum(p => p.PhaiThu);

                dateNgayThu.DateTime = now;
                txtSoPhieu.Text = CreateKeyword(now);
            }
            else
            {
                objPT = db.PhieuThus.Single(p => p.MaPhieu == MaPhieu);
                txtSoPhieu.Text = objPT.SoPhieu;
                txtDiaChi.Text = objPT.DiaChi;
                txtDienGiai.Text = objPT.DienGiai;
                txtMatBang.Text = MaSoMB;
                txtNguoiNop.Text = objPT.NguoiNop;
                MaKH = objPT.CusID;
                MaMB = objPT.MaMB.Value;
                MaTN = objPT.mbMatBang.mbTangLau.mbKhoiNha.MaTN.Value;
                PhiQL = objPT.PhiQuanLy ?? 0;
                if (objPT.NgayThu != null)
                    dateNgayThu.DateTime = objPT.NgayThu.Value;
                spinTongTien.EditValue = objPT.SoTienThanhToan ?? 0;
                ckChuyenKhoan.Checked = objPT.ChuyenKhoan.GetValueOrDefault();
                gcDichVu.DataSource = db.PaidMulti_edit(MaPhieu);
                chkAuto.Checked = objPT.IsAuto.GetValueOrDefault();

                colIsCheck.Visible = false;

                btnCalculator.Enabled = false;
                chkLaiNgay.Enabled = false;
                chkLaiThang.Enabled = false;
                spinLaiNgay.Enabled = false;
                spinLaiThang.Enabled = false;
                chkLaiNgay.Checked = objPT.IsLaiNgay.GetValueOrDefault();
                chkLaiThang.Checked = objPT.IsLaiThang.GetValueOrDefault();
                spinLaiNgay.EditValue = objPT.LaiNgay ?? 0;
                spinLaiThang.Value = objPT.LaiThang ?? 0;
            }
            gvDichVu.ExpandAllGroups();
            colIsDebt.Visible = false;
            first = false;
        }

        private void LoadData()
        {
            var objmb = db.mbMatBangs.Single(p => p.MaMB == MaMB);
            MaTN = objmb.mbTangLau.mbKhoiNha.MaTN.Value;
            txtMatBang.Text = objmb.MaSoMB;
            PhiQL =  0;
            DateTime now = db.GetSystemDate();
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            //Dien ten mac dinh
            try
            {
                txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                txtDienGiai.Text = string.Format("Thanh toán phí dịch vụ tổng hợp cho mặt bằng {0}", objmb.MaSoMB);
            }
            catch { }
        }

        decimal ChietKhau(int soThang)
        {
            decimal ck = 0;
            var obj = db.PhiQuanLy_ChietKhaus.Where(p => p.SoThangThanhToan <= soThang).OrderByDescending(p => p.SoThangThanhToan).FirstOrDefault();
            if (obj == null)
                ck = 0;
            else
            {
                ck = PhiQL * soThang * (obj.TiLeChietKhau ?? 0);
            }

            TongPQL = PhiQL * soThang;
            return ck;
        }

        private void spinTongTien_EditValueChanged(object sender, EventArgs e)
        {
            ResetData();
            gvDichVu.RefreshData();
        }

        void ResetData()
        {
            if (!chkAuto.Checked) return;

            decimal Total = spinTongTien.Value;
            if (Total < TongPQL) return;

            decimal left = Total - TongPQL;

            gvDichVu.FocusedColumn = colMaMB;
            for (int i = 0; i < gvDichVu.RowCount; i++)
            {
                try
                {
                    if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                    {
                        if (i == 0)
                        {
                            if (Convert.ToInt32(gvDichVu.GetRowCellValue(i, "SoThang")) == 0)
                            {
                                gvDichVu.SetRowCellValue(i, "SoTien", (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu"));
                                gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                                left -= (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                            }
                            else
                                gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                        }
                        else
                        {
                            if (left > (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu"))
                            {
                                gvDichVu.SetRowCellValue(i, "SoTien", (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu"));
                                gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                                left -= (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                            }
                            else
                            {
                                gvDichVu.SetRowCellValue(i, "SoTien", left);
                                gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                                left = 0;
                            }
                        }
                    }
                    else
                        gvDichVu.SetRowCellValue(i, "SoTien", (decimal)0);
                }
                catch { }
            }
            gvDichVu.FocusedColumn = colDichVu;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số phiếu thu], xin cảm ơn.");  
                txtSoPhieu.Focus();
                return;
            }

            if (dateNgayThu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Ngày thu], xin cảm ơn.");  
                dateNgayThu.Focus();
                return;
            }

            if (MaPhieu == 0)
            {
                objPT = new PhieuThu();
                objPT.NgayNhap = now;
                #region Detail
                //listPTCT = new List<ptChiTiet>();
                for (int i = 0; i < gvDichVu.RowCount; i++)
                {
                    try
                    {
                        if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                        {
                            var objPTCT = new ptChiTiet();
                            objPTCT.ChietKhau = (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau");  
                            objPTCT.DienGiai = gvDichVu.GetRowCellValue(i, "DienGiai").ToString();
                            objPTCT.MaLDV = (byte)gvDichVu.GetRowCellValue(i, "MaLDV");  
                            objPTCT.MaMB = MaMB;
                            var date = (DateTime?)gvDichVu.GetRowCellValue(i, "NgayThu");  
                            if (date != null)
                                objPTCT.NgayThu = date;
                            else
                            {
                                if (dateNgayThu.DateTime.Year != 1)
                                    objPTCT.NgayThu = dateNgayThu.DateTime;
                            }
                            objPTCT.PhaiThu = (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                            objPTCT.TongCong = (decimal)gvDichVu.GetRowCellValue(i, "SoTien");  
                            objPTCT.IsDebt = (bool)gvDichVu.GetRowCellValue(i, "IsDebt");  
                            objPT.ptChiTiets.Add(objPTCT);
                        }
                    }
                    catch { }
                }
                #endregion

                #region Interest
                if ((spinLaiNgay.Value + spinLaiThang.Value) > 0)
                {
                    var o = new ptChiTiet();
                    o.ChietKhau = 0;
                    o.DienGiai = "Tiền lãi";
                    o.MaLDV = 99;
                    o.MaMB = MaMB;
                    o.NgayThu = dateNgayThu.DateTime;

                    o.PhaiThu = o.TongCong = spinLaiNgay.Value + spinLaiThang.Value;
                    o.IsDebt = false;
                    objPT.ptChiTiets.Add(o);
                }
                #endregion

                objPT.MaNV = objnhanvien.MaNV;
            }
            else
            {
                objPT.MaNVCN = objnhanvien.MaNV;
                objPT.NgayCN = now;

                #region Detail
                for (int i = 0; i < gvDichVu.RowCount; i++)
                {
                    try
                    {
                        if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                        {
                            var objPTCT = objPT.ptChiTiets.SingleOrDefault(p => p.ID == (int?)gvDichVu.GetRowCellValue(i, "ID"));
                            objPTCT.ChietKhau = (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau");  
                            objPTCT.DienGiai = gvDichVu.GetRowCellValue(i, "DienGiai").ToString();
                            objPTCT.MaLDV = (byte)gvDichVu.GetRowCellValue(i, "MaLDV");  
                            objPTCT.MaMB = MaMB;
                            var date = (DateTime?)gvDichVu.GetRowCellValue(i, "NgayThu");  
                            if (date != null)
                                objPTCT.NgayThu = date;
                            else
                            {
                                if (dateNgayThu.DateTime.Year != 1)
                                    objPTCT.NgayThu = dateNgayThu.DateTime;
                            }
                            objPTCT.PhaiThu = (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                            objPTCT.TongCong = (decimal)gvDichVu.GetRowCellValue(i, "SoTien");  
                            objPTCT.IsDebt = (bool)gvDichVu.GetRowCellValue(i, "IsDebt");  
                        }
                    }
                    catch { }
                }
                #endregion

                db.ptLichSu_add(objPT.MaPhieu, objnhanvien.MaNV, "Cập nhật thông tin");  
            }

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 100;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            objPT.MaHopDong = MaMB.ToString();
            if (dateNgayThu.DateTime.Year != 1)
            {
                objPT.NgayThu = dateNgayThu.DateTime;
                objPT.DotThanhToan = dateNgayThu.DateTime;
            }
            try
            {
                objPT.SoThangThuPhiQuanLy = Convert.ToInt32(gvDichVu.GetRowCellValue(0, "SoThang").ToString());
            }
            catch { objPT.SoThangThuPhiQuanLy = 1; }
            objPT.SoTienChietKhauPhiQL = (decimal)gvDichVu.GetRowCellValue(0, "ChietKhau");  
            objPT.PhaiThu = spinTongTien.Value;
            objPT.PhiQuanLy = PhiQL;
            objPT.KeToanDaDuyet = false;
            objPT.MaMB = MaMB;
            objPT.CusID = MaKH;
            objPT.IsAuto = chkAuto.Checked;
            objPT.IsLaiNgay = chkLaiNgay.Checked;
            objPT.IsLaiThang = chkLaiThang.Checked;
            objPT.LaiNgay = spinLaiNgay.Value;
            objPT.LaiThang = spinLaiThang.Value;
            objPT.SoTienThanhToan = spinTongTien.Value;
            
            objPT.ChuyenKhoan = ckChuyenKhoan.Checked;

            try
            {
                if (MaPhieu == 0)
                    db.PhieuThus.InsertOnSubmit(objPT);

                db.SubmitChanges();

                if (DialogBox.Question("Bạn có muốn in [Phiếu thu] không?") == DialogResult.Yes)
                {
                    //Barcode
                    //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now); 
                    //var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV2(objPT.MaPhieu);
                    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                    //    SavePrint(barCodeString, objPT.MaPhieu);
                    //var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV3(objPT.MaPhieu);
                    //rpt.ShowPreviewDialog();
                }
                //if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                //{
                //    ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objPT);
                //    rpt.ShowPreviewDialog();
                //}
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu, vui lòng thử lại sau");  
            }
        }

        void SavePrint(string barCode, int maPhieu)
        {
            var obj = new ptLichSu();
            obj.DienGiai = "In phiếu thu. Mã vạch [" + barCode + "]";
            obj.MaNV = objnhanvien.MaNV;
            obj.MaPT = maPhieu;
            obj.NgayTH = db.GetSystemDate();
            db.ptLichSus.InsertOnSubmit(obj);

            var objPT = db.PhieuThus.Single(p => p.MaPhieu == maPhieu);
            objPT.Barcode = barCode;
            db.SubmitChanges();
        }

        private void gvDichVu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void spinSoThang_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _new = (SpinEdit)sender;
            
            if ((byte)gvDichVu.GetFocusedRowCellValue("MaLDV") == 12)
            {
                decimal ck = ChietKhau(Convert.ToInt32(_new.EditValue));
                gvDichVu.SetFocusedRowCellValue("SoThang", Convert.ToInt32(_new.EditValue));
                gvDichVu.SetFocusedRowCellValue("ChietKhau", ck);
                gvDichVu.SetFocusedRowCellValue("SoTien", TongPQL - ck);
                gvDichVu.SetFocusedRowCellValue("TongCong", TongPQL);
            }
            ResetData();
            gvDichVu.RefreshData();
        }

        private void spinSoTien_EditValueChanged(object sender, EventArgs e)
        {
            if ((byte)gvDichVu.GetFocusedRowCellValue("MaLDV") != 12)
            {
                SpinEdit _new = (SpinEdit)sender;
                gvDichVu.SetFocusedRowCellValue("SoTien", Convert.ToDecimal(_new.EditValue));
                gvDichVu.SetFocusedRowCellValue("TongCong", Convert.ToDecimal(_new.EditValue));
                ResetData();
                gvDichVu.RefreshData();
            }
            {
                try
                {
                    if ((int)gvDichVu.GetFocusedRowCellValue("SoThang") == 0)
                    {
                        SpinEdit _new = (SpinEdit)sender;
                        //TongPQL = Convert.ToDecimal(_new.EditValue);
                        gvDichVu.SetFocusedRowCellValue("SoTien", Convert.ToDecimal(_new.EditValue));
                        gvDichVu.SetFocusedRowCellValue("TongCong", Convert.ToDecimal(_new.EditValue));
                    }
                    else
                    {
                        decimal ck = ChietKhau((int)gvDichVu.GetFocusedRowCellValue("SoThang"));
                        gvDichVu.SetFocusedRowCellValue("ChietKhau", ck);
                        gvDichVu.SetFocusedRowCellValue("SoTien", TongPQL - ck);
                        gvDichVu.SetFocusedRowCellValue("TongCong", TongPQL);
                    }
                }
                catch { }
                ResetData();
                gvDichVu.RefreshData();
            }
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            ResetData();
            gvDichVu.RefreshData();
        }

        private void chkCheck_EditValueChanged(object sender, EventArgs e)
        {
            gvDichVu.FocusedColumn = colMaMB;
            spinTongTien.EditValue = TotalPaid();
        }

        decimal? TotalPaid()
        {
            gvDichVu.RefreshData();
            decimal? result = 0;
            for (int i = 0; i < gvDichVu.RowCount; i++)
            {
                try
                {
                    if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck") & !(bool)gvDichVu.GetRowCellValue(i, "IsRate"))
                    {
                        result += (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                    }
                    else
                        gvDichVu.SetRowCellValue(i, "SoTien", (decimal)0);
                }
                catch { }
            }

            return result;
        }

        private void chkLaiNgay_EditValueChanged(object sender, EventArgs e)
        {
            CalculatorInterest();
        }

        decimal TotalPaidRate()
        {
            gvDichVu.RefreshData();
            decimal result = 0;
            for (int i = 0; i < gvDichVu.RowCount; i++)
            {
                try
                {
                    if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                    {
                        result += (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                    }
                }
                catch { }
            }

            return result;
        }

        void CalculatorInterest()
        {
            if (first) return;
            if (chkLaiNgay.Checked)
            {
                int day = dateNgayThu.DateTime.Day - 25;
                if (day > 0)
                {
                    decimal soTien = TotalPaidRate();
                    decimal tienLai = DichVu.LaiSuat.MangerCls.CalculatorInterestDay(dateNgayThu.DateTime, TotalPaidRate(), day, MaTN);
                    spinLaiNgay.EditValue = tienLai;
                }
                else
                    spinLaiNgay.EditValue = 0;
            }
            else
                spinLaiNgay.EditValue = 0;

            spinTongTien.EditValue = spinLaiNgay.Value + spinLaiThang.Value + TotalPaid();
        }

        private void chkLaiThang_EditValueChanged(object sender, EventArgs e)
        {
            CalculatorInterestMonth();

            if(chkLaiNgay.Checked)
                CalculatorInterest();
        }

        decimal TotalPaidRateMonth()
        {
            gvDichVu.RefreshData();
            decimal result = 0;
            for (int i = 0; i < gvDichVu.RowCount; i++)
            {
                try
                {
                    if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck") & (bool)gvDichVu.GetRowCellValue(i, "IsDebt") & !(bool)gvDichVu.GetRowCellValue(i, "IsRate"))
                    {
                        result += (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                    }
                }
                catch { }
            }

            return result;
        }

        void CalculatorInterestMonth()
        {
            if (first) return;
            decimal laiTruoc = 0;
            if (chkLaiThang.Checked)
            {
                int year, month;
                if (Month == 12)
                {
                    month = 1;
                    year = Year - 1;
                }
                else
                {
                    month = Month - 1;
                    year = Year;
                }

                laiTruoc = db.funcGetInterst(MaMB, month, year) ?? 0;

                decimal soTien = TotalPaidRateMonth() + laiTruoc;
                decimal tienLai = DichVu.LaiSuat.MangerCls.CalculatorInterest(dateNgayThu.DateTime, soTien, MaTN);
                spinLaiThang.EditValue = tienLai;
            }
            else
                spinLaiThang.EditValue = 0;

            spinTongTien.EditValue = spinLaiNgay.Value + spinLaiThang.Value + TotalPaid() + laiTruoc;
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            CalculatorInterestMonth();
            CalculatorInterest();

            spinTongTien.EditValue = spinLaiNgay.Value + spinLaiThang.Value + TotalPaid();
        }
    }
}