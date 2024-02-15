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

namespace LandSoftBuilding.Receivables
{
    public partial class frmAddMulti : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        public bool IsSave { get; set; }

        MasterDataContext db = new MasterDataContext();
        List<HoaDon> HoaDons = new List<HoaDon>();
        
        public frmAddMulti()
        {
            InitializeComponent();
            lookLichThanhToan.Enabled = false;
        }

        void LoadLichThanhToan()
        {
            try
            {
                if (lkLoaiDichVu.EditValue == null) return;
                var _MaLDV = (int)lkLoaiDichVu.EditValue;
                if (_MaLDV == 6) //Giu xe
                {
                    var _MaMB = (int?)glkMatBang.EditValue;
                    var _MaKH = (int?)glkKhachHang.EditValue;
                    var _data = (from gx in db.dvgxGiuXes
                                 where gx.MaKH == _MaKH & gx.MaMB == _MaMB & gx.NgungSuDung == false
                                 select new
                                 {
                                     gx.ID,
                                     gx.NgayTT,
                                     gx.TienTT,
                                     DienGiai = gx.DienGiai != "" ? gx.DienGiai : gx.SoDK,
                                     IsGiuXe = true,
                                     MaLX = (int?)null
                                 }).Union(from gx in db.dvgxTheXes
                                          where gx.MaKH == _MaKH & gx.MaMB == _MaMB & gx.MaGX == null & gx.NgungSuDung == false
                                          select new
                                          {
                                              gx.ID,
                                              gx.NgayTT,
                                              gx.TienTT,
                                              DienGiai = gx.DienGiai != "" ? gx.DienGiai : gx.SoThe,
                                              IsGiuXe = false,
                                              gx.MaLX
                                          });
                    lookLichThanhToan.Properties.DataSource = _data.ToList();
                    lookLichThanhToan.EditValue = null;
                    lookLichThanhToan.Enabled = true;
                }
                else
                {
                    lookLichThanhToan.Enabled = false;
                }
                this.SetChietKhau();
            }
            catch 
            {
                lookLichThanhToan.Properties.DataSource = null;
                lookLichThanhToan.EditValue = null;
            }
        }

        void AddGiuXe(int _MaKH, int _MaMB, int _MaLDV, int _KyTT, DateTime _TuNgay, DateTime _DenNgay, decimal _PhiDV, decimal _TyLeCK,
            string _MaSoKH, string _TenKH, string _MaSoMB, string _TenLDV, bool _IsGiuXe, int _ID)
        {
            //var ltData = _IsGiuXe ?
            //              (from gx in db.dvgxGiuXes
            //               where gx.ID == _ID
            //               select new
            //               {
            //                   gx.ID,
            //                   gx.NgayTT,
            //                   gx.KyTT,
            //                   gx.TienTT,
            //                   gx.DienGiai,
            //                   TableName = "dvgxGiuXe",
                               
            //               }).ToList() :
            //              (from gx in db.dvgxTheXes
            //               where gx.ID == _ID
            //               select new
            //               {
            //                   gx.ID,
            //                   gx.NgayTT,
            //                   gx.KyTT,
            //                   gx.TienTT,
            //                   gx.DienGiai,
            //                   TableName = "dvgxTheXe",
            //                   gx.ThueGTGT,
            //                   gx.TienThueGTGT,
            //                   gx.TienTruocThue
            //               }).ToList();

            var ltData = (from gx in db.dvgxTheXes
                          where gx.ID == _ID
                          select new
                          {
                              gx.ID,
                              gx.NgayTT,
                              gx.KyTT,
                              gx.TienTT,
                              gx.DienGiai,
                              TableName = "dvgxTheXe",
                              gx.ThueGTGT,
                              gx.TienThueGTGT,
                              gx.TienTruocThue,
                              gx.MaLX,
                              gx.MaTN
                          }).ToList();

            foreach (var i in ltData)
            {
                var _NgayTT = i.NgayTT.Value.CompareTo(_TuNgay) < 0 ? _TuNgay : i.NgayTT.Value;

                while (_NgayTT.CompareTo(_DenNgay) <= 0)
                {
                    if (this.HoaDons.Where(p => p.MaKH == _MaKH & p.MaMB == _MaMB & p.MaLDV == _MaLDV & p.LinkID == i.ID & p.NgayTT.Value.Month == _NgayTT.Month & p.NgayTT.Value.Year == _NgayTT.Year).Count() <= 0)
                    {
                        var objHD = new HoaDon();
                        objHD.LinkID = i.ID;
                        objHD.TableName = i.TableName;
                        objHD.MaKH = _MaKH;
                        objHD.MaMB = _MaMB;
                        objHD.MaLDV = _MaLDV;
                        objHD.NgayTT = _NgayTT;
                        objHD.KyTT = Convert.ToInt32(i.KyTT);
                        objHD.PhiDV = i.TienTruocThue;
                        objHD.ThueGTGT = i.ThueGTGT;
                        objHD.TienThueGTGT = i.TienThueGTGT;
                        objHD.TienTruocThue = i.TienTruocThue;
                        objHD.TienTT = i.TienTT;
                        objHD.TyLeCK = _TyLeCK;
                        objHD.TienCK = objHD.TienTT * objHD.TyLeCK;
                        objHD.PhaiThu = objHD.TienTT - objHD.TienCK;
                        

                        objHD.MaSoMB = _MaSoMB;
                        objHD.MaSoKH = _MaSoKH;
                        objHD.TenKH = _TenKH;
                        objHD.TenLDV = _TenLDV;
                        objHD.MaLX = i.MaLX;

                        #region Đối với tòa Vinh 1 và Vinh 2, cài đặt từ ngày và đến ngày bằng tháng sau, còn lại cài bằng ngày trong tháng
                        if (i.MaTN == 4 | i.MaTN == 14)
                        {
                            objHD.TuNgay = GetFirstDayOfMonth(objHD.NgayTT.Value.AddMonths(1));
                            objHD.DenNgay = GetLastDayOfMonth(objHD.NgayTT.Value.AddMonths(1));
                        }
                        else
                        {
                            objHD.TuNgay = GetFirstDayOfMonth(objHD.NgayTT.Value);
                            objHD.DenNgay = GetLastDayOfMonth(objHD.NgayTT.Value);
                        }
                        objHD.DienGiai = string.Format("{0} tháng {1:MM/yyyy}", (string.IsNullOrEmpty(i.DienGiai) ? _TenLDV : i.DienGiai), objHD.TuNgay);
                        #endregion

                        this.HoaDons.Add(objHD);

                    }

                    _NgayTT = _NgayTT.AddMonths(Convert.ToInt32(i.KyTT.Value));
                }
            }

            gvHoaDon.RefreshData();
        }

        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        void AddDichVuCoBan(int _MaKH, int _MaMB, int _MaLDV, int _KyTT, DateTime _TuNgay, DateTime _DenNgay, decimal _PhiDV, decimal _TyLeCK,
            string _MaSoKH, string _TenKH, string _MaSoMB, string _TenLDV)
        {
            var ltData = (from dv in db.dvDichVuKhacs
                          where dv.MaTN == this.MaTN & dv.MaKH == _MaKH & dv.MaMB == _MaMB & dv.MaLDV == _MaLDV & dv.IsLapLai == true & dv.IsNgungSuDung.GetValueOrDefault() == false
                          select new
                          {
                              dv.ID,
                              dv.NgayTT,
                              PhiDV = dv.ThanhTien * dv.TyGia,
                              dv.KyTT,
                              dv.TienTTQD,
                              dv.DienGiai,
                              dv.ThueGTGT,
                              dv.TienThueGTGT,
                              dv.TienTruocThue,
                              dv.SoLuong
                          }).ToList();
            //

            foreach (var i in ltData)
            {
                var _NgayTT = i.NgayTT.Value.CompareTo(_TuNgay) < 0 ? _TuNgay : i.NgayTT.Value;

                while (_NgayTT.CompareTo(_DenNgay) <= 0)
                {
                    if (this.HoaDons.Where(p => p.MaKH == _MaKH & p.MaMB == _MaMB & p.MaLDV == _MaLDV & p.LinkID == i.ID & p.NgayTT.Value.Month == _NgayTT.Month & p.NgayTT.Value.Year == _NgayTT.Year).Count() <= 0)
                    {
                        var objHD = new HoaDon();
                        objHD.LinkID = i.ID;
                        objHD.TableName = "dvDichVuKhac";
                        objHD.MaKH = _MaKH;
                        objHD.MaMB = _MaMB;
                        objHD.MaLDV = _MaLDV;
                        objHD.NgayTT = _NgayTT;
                        objHD.KyTT = Convert.ToInt32(i.KyTT);
                        objHD.PhiDV = i.PhiDV;
                        objHD.TienTT = i.TienTTQD;
                        objHD.ThueGTGT = i.ThueGTGT;
                        objHD.TienThueGTGT = i.TienThueGTGT;
                        objHD.TienTruocThue = i.TienTruocThue;
                        objHD.TyLeCK = _TyLeCK;
                        objHD.TienCK = objHD.TienTT * objHD.TyLeCK;
                        objHD.PhaiThu = objHD.TienTT - objHD.TienCK;
                        objHD.DienGiai = string.Format("{0} tháng {1:MM/yyyy}-{2:#,0.##} m2", ((ltData.Count > 1 & !string.IsNullOrEmpty(i.DienGiai)) ? i.DienGiai : _TenLDV), _NgayTT, i.SoLuong);

                        objHD.MaSoMB = _MaSoMB;
                        objHD.MaSoKH = _MaSoKH;
                        objHD.TenKH = _TenKH;
                        objHD.TenLDV = _TenLDV;

                        this.HoaDons.Add(objHD);
                    }

                    _NgayTT = _NgayTT.AddMonths(Convert.ToInt32(i.KyTT.Value));
                }
            }

            gvHoaDon.RefreshData();
        }

        void AddDichVuKhac(int _MaKH, int _MaMB, int _MaLDV, int _KyTT, DateTime _TuNgay, DateTime _DenNgay, decimal _PhiDV, decimal _TyLeCK,
            string _MaSoKH, string _TenKH, string _MaSoMB, string _TenLDV)
        {
            var _NgayTT = _TuNgay;

            while (_NgayTT.CompareTo(_DenNgay) <= 0)
            {
                if (this.HoaDons.Where(p => p.MaKH == _MaKH & p.MaMB == _MaMB & p.MaLDV == _MaLDV & p.NgayTT.Value.Month == _NgayTT.Month & p.NgayTT.Value.Year == _NgayTT.Year).Count() <= 0)
                {
                    var objHD = new HoaDon();
                    objHD.MaKH = _MaKH;
                    objHD.MaMB = _MaMB;
                    objHD.MaLDV = _MaLDV;
                    objHD.NgayTT = _NgayTT;
                    objHD.KyTT = _KyTT;
                    objHD.PhiDV = _PhiDV;
                    objHD.TienTT = objHD.PhiDV * objHD.KyTT;
                    objHD.TyLeCK = _TyLeCK;
                    objHD.TienCK = objHD.TienTT * objHD.TyLeCK;
                    objHD.PhaiThu = objHD.TienTT - objHD.TienCK;
                    objHD.DienGiai = string.Format("{0} tháng {1:MM/yyyy}", _TenLDV, _NgayTT);

                    objHD.MaSoMB = _MaSoMB;
                    objHD.MaSoKH = _MaSoKH;
                    objHD.TenKH = _TenKH;
                    objHD.TenLDV = _TenLDV;

                    this.HoaDons.Add(objHD);
                }

                _NgayTT = _NgayTT.AddMonths(_KyTT);
            }

            gvHoaDon.RefreshData();
        }

        void SetChietKhau()
        {
            try
            {
                var _MaLDV = (int)lkLoaiDichVu.EditValue;
                var _TuNgay = dateTuNgay.DateTime;
                var _DenNgay = dateDenNgay.DateTime;
                var _SoThang = (_DenNgay - _TuNgay).TotalDays / 30;

                var _TyLeCK = (from ck in db.dvChietKhaus
                               where ck.MaTN == this.MaTN & ck.MaLDV == _MaLDV & ck.KyTT <= _SoThang
                               orderby ck.KyTT descending
                               select ck.TyLeCK).FirstOrDefault().GetValueOrDefault();

                spinTyLeCK.EditValue = _TyLeCK;
            }
            catch { }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            gcHoaDon.KeyUp += Common.GridViewKeyUp;

            #region Load tu dien
            lkLoaiDichVu.Properties.DataSource = (from l in db.dvLoaiDichVus
                                                  join bg in db.dvBangGiaDichVus on new { MaLDV = (int?)l.ID, this.MaTN } equals new { bg.MaLDV, bg.MaTN } into tblBangGia
                                                  from bg in tblBangGia.DefaultIfEmpty()
                                                  select new
                                                  {
                                                      l.ID,
                                                      TenLDV = l.TenHienThi,
                                                      bg.DonGia,
                                                      bg.MaLT,
                                                      bg.MaDVT,
                                                      l.IsCoBan
                                                  }).ToList();

            glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  where kh.MaTN == this.MaTN
                                                  orderby kh.KyHieu descending
                                                  select new
                                                  {
                                                      kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                      DiaChi = kh.DCLL
                                                  }).ToList();


            #endregion

            gcHoaDon.DataSource = this.HoaDons;
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                    join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                    join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                    join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                    where mb.MaTN == this.MaTN & mb.MaKH == (int)glkKhachHang.EditValue
                                                    orderby mb.MaSoMB descending
                                                    select new
                                                    {
                                                        mb.MaMB,
                                                        mb.MaSoMB,
                                                        tl.TenTL,
                                                        kn.TenKN,
                                                        kh.MaKH,
                                                        TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                    }).ToList();
            }
            catch
            {
                glkMatBang.Properties.DataSource = null;
            }

            glkMatBang.EditValue = null;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap nhieu
            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (glkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                return;
            }

            if (lkLoaiDichVu.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại dịch vụ");
                return;
            }

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
            #endregion

            var _MaMB = (int)glkMatBang.EditValue;
            var _MaKH = (int)glkKhachHang.EditValue;
            var _MaLDV = (int)lkLoaiDichVu.EditValue;
            var _KyTT = Convert.ToInt32(spKyTT.Value);
            var _TuNgay = dateTuNgay.DateTime;
            var _DenNgay = dateDenNgay.DateTime;
            var _PhiDV = spPhiDichVu.Value;
            var _TyLeCK = spinTyLeCK.Value;

            var _MaSoMB = glkMatBang.Text;
            var _MaSoKH = gvKhachHang.GetFocusedRowCellDisplayText("KyHieu");
            var _TenKH = glkKhachHang.Text;
            var _TenLDV = lkLoaiDichVu.Text;
            var _ID = (int?)lookLichThanhToan.EditValue;

            if (_MaLDV == 6) //Giu xe
            {
                if (_ID == null)
                {
                    DialogBox.Error("Vui lòng chọn lịch thanh toán");
                    return;
                }
                var _IsGiuXe = (bool)lookLichThanhToan.GetColumnValue("IsGiuXe");
                this.AddGiuXe(_MaKH, _MaMB, _MaLDV, _KyTT, _TuNgay, _DenNgay, _PhiDV, _TyLeCK, _MaSoKH, _TenKH, _MaSoMB, _TenLDV, _IsGiuXe, _ID.Value);
            }
            else
            {
                var _IsCoBan = (bool?)lkLoaiDichVu.GetColumnValue("IsCoBan");
                if (_IsCoBan.GetValueOrDefault()) //Dich vu co ban
                {
                    this.AddDichVuCoBan(_MaKH, _MaMB, _MaLDV, _KyTT, _TuNgay, _DenNgay, _PhiDV, _TyLeCK, _MaSoKH, _TenKH, _MaSoMB, _TenLDV);
                }
                else //Dich vu khac
                {
                    this.AddDichVuKhac(_MaKH, _MaMB, _MaLDV, _KyTT, _TuNgay, _DenNgay, _PhiDV, _TyLeCK, _MaSoKH, _TenKH, _MaSoMB, _TenLDV);
                }
            }

            gvHoaDon.RefreshData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (gvHoaDon.RowCount <= 0)
            {
                DialogBox.Error("Vui lòng nhập hóa đơn");
                return;
            }

            var _TuNgay = dateTuNgay.DateTime;
            var _DenNgay = dateDenNgay.DateTime;

            foreach (var i in this.HoaDons)
            {
                var objHD = new dvHoaDon();
                objHD.MaTN = this.MaTN;
                objHD.LinkID = i.LinkID;
                objHD.TableName = i.TableName;
                if (i.MaLX != null)
                    objHD.MaLX = i.MaLX;
                objHD.MaMB = i.MaMB;
                objHD.MaKH = i.MaKH;
                objHD.MaLDV = i.MaLDV;
                objHD.NgayTT = i.NgayTT;
                objHD.PhiDV = i.PhiDV;
                objHD.ThueGTGT = i.ThueGTGT;
                objHD.TienThueGTGT = i.TienThueGTGT;
                objHD.TienTruocThue = i.TienTruocThue;
                objHD.KyTT = i.KyTT;
                objHD.TienTT = i.TienTT;
                objHD.TyLeCK = i.TyLeCK;
                objHD.TienCK = i.TienCK;
                objHD.PhaiThu = i.PhaiThu;
                objHD.DaThu = 0;
                objHD.ConNo = objHD.PhaiThu - objHD.DaThu;
                objHD.DienGiai = i.DienGiai;
                if (objHD.MaLDV == 6)
                {
                    //var firstDay = objHD.NgayTT.Value.AddMonths(1);
                    //var LastDay = new DateTime(firstDay.Year, firstDay.Month, 1);
                    //var lastmon = LastDay.AddMonths(1);
                    //lastmon = lastmon.AddDays(-1);
                    //objHD.TuNgay = LastDay;
                    //objHD.DenNgay = lastmon;
                    objHD.TuNgay = i.TuNgay;
                    objHD.DenNgay = i.DenNgay;
                }
                else
                {
                    var first = objHD.NgayTT.Value;
                    var lastmon = first.AddMonths(1);
                    lastmon = lastmon.AddDays(-1);
                    var firstDAy = new DateTime(first.Year, first.Month, 1);
                    var lastDay = new DateTime(lastmon.Year, lastmon.Month, lastmon.Day);
                    objHD.TuNgay = firstDAy;
                    objHD.DenNgay = lastDay;
                }
                objHD.NgayNhap = System.DateTime.UtcNow.AddHours(7);
                objHD.MaNVN = Library.Common.User.MaNV;
                db.dvHoaDons.InsertOnSubmit(objHD);
              
                
            }

            try
            {
                db.SubmitChanges();
                this.IsSave = true;
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hóa đơn dịch vụ", "Thêm hóa đơn thu trước","Dự án: "+db.tnToaNhas.Single(p=>p.MaTN==this.MaTN).TenTN);
                DialogBox.Success("Dữ liệu đã được lưu!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
            finally
            {
                db.Dispose();
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.Dispose();
            }
            catch { }
        }

        private void lkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadLichThanhToan();
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadLichThanhToan();
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            this.SetChietKhau();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            this.SetChietKhau();
        }
    }

    public class HoaDon
    {
        public int? LinkID { get; set; }
        public string TableName { get; set; }

        public int? MaKH { get; set; }
        public int? MaMB { get; set; }
        public int? MaLDV { get; set; }
        public DateTime? NgayTT { get; set; }
        public int? KyTT { get; set; }
        public decimal? PhiDV { get; set; }
        public decimal? TienTT { get; set; }
        public decimal? TyLeCK { get; set; }
        public decimal? TienCK { get; set; }
        public decimal? PhaiThu { get; set; }
        public decimal? ThueGTGT { get; set; }
        public decimal? TienThueGTGT { get; set; }
        public decimal? TienTruocThue { get; set; }
        public string DienGiai { get; set; }

        public string MaSoMB { get; set; }
        public string MaSoKH { get; set; }
        public string TenKH { get; set; }
        public string TenLDV { get; set; }

        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public int? MaLX { get; set; }
    }
}