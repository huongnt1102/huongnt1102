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

namespace DichVu.PhiQuanLy.DangKy
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        pqlDangKy objDK, objDKTemp;
        public tnNhanVien objNV;
        public int? ID, MaKH, MaMB;
        public string KhachHang = "", MaSoMB = "";
        DateTime dateFrom, dateFromMax, dateTo;
        public decimal? SoTien = 0;
        string sSoPhieu;
        public frmEdit()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void spinChuKy_EditValueChanged(object sender, EventArgs e)
        {
            SetValues();
            
            #region tinh chiet khau
            try
            {
                spinTongThu.EditValue = Convert.ToInt32(spinChuKy.EditValue) * spinSoTien.Value;
                //int SoThangThanhToan = Convert.ToInt32(spinChuKy.EditValue.ToString());
                var obj = db.PhiQuanLy_ChietKhaus.Where(p => p.ID == (int?)lookChietKhau.EditValue).OrderByDescending(p => p.SoThangThanhToan).FirstOrDefault();
                if (obj == null)
                {
                    lookChietKhau.EditValue = null;
                    spinChietKhau.Value = 0;
                }
                else
                {
                    lookChietKhau.EditValue = obj.ID;
                    spinChietKhau.Value = Math.Round(spinTongThu.Value * (obj.TiLeChietKhau ?? 0), 0, MidpointRounding.AwayFromZero);
                }

                spinPhaiThu.EditValue = spinTongThu.Value - spinChietKhau.Value;
            }
            catch { }
            #endregion
        }

        int GetDayInMonth(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                case 3:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                default:
                    return 29;
            }            
        }

        void SetValues()
        {
            try
            {
                dateFrom = new DateTime(Convert.ToInt32(spinTuNam.EditValue), Convert.ToInt32(spinTuThang.EditValue), 1);
                int day = DateTime.DaysInMonth(Convert.ToInt32(spinTuNam.EditValue), Convert.ToInt32(spinTuThang.EditValue));

                string str = string.Format("{2}/{1}/{0}", Convert.ToInt32(spinTuNam.EditValue), Convert.ToInt32(spinTuThang.EditValue), day);
                dateFromMax = GetDate(str).Value;
                dateTo = dateFromMax.AddMonths(Convert.ToInt32(spinChuKy.EditValue) - 1);

                spinDenNam.EditValue = dateTo.Year;
                spinDenThang.EditValue = dateTo.Month;
            }
            catch { }
        }

        DateTime? GetDate(string dateText)
        {
            if (dateText == "") return null;
            string[] ns = dateText.Split('/');
            if (ns.Length == 3)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else if (ns.Length == 2)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[1]), int.Parse(ns[0]));
            }

            return null;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookChietKhau.Properties.DataSource = db.PhiQuanLy_ChietKhaus;
            if (ID != null)
            {
                spinTuThang.Properties.ReadOnly = true;
                spinTuNam.Properties.ReadOnly = true;
                try
                {
                    objDK = objDKTemp = db.pqlDangKies.Single(p => p.ID == ID);
                    txtKhachHang.Text = objDK.tnKhachHang.FullName;
                    txtMatBang.Text = objDK.mbMatBang.MaSoMB;
                    txtSoDK.Text = objDK.SoDK;
                    spinChuKy.EditValue = objDK.ChuKyID;
                    spinTuNam.EditValue = objDK.YearFrom;
                    spinTuThang.EditValue = objDK.MonthFrom;
                    spinDenNam.EditValue = objDK.YearTo;
                    spinDenThang.EditValue = objDK.MonthTo;
                    dateFrom = objDK.NgayDK.Value;
                    dateTo = objDK.NgayKT.Value;
                    txtDienGiai.Text = objDK.DienGiai;
                    spinChietKhau.EditValue = objDK.ChietKhau ?? 0;
                    spinSoTien.EditValue = objDK.SoTien;
                    spinTongThu.EditValue = objDK.ChuKyID * objDK.SoTien;
                    spinPhaiThu.EditValue = objDK.PhaiThu ?? 0;
                    lookChietKhau.EditValue = objDK.ChietKhauID;
                }
                catch { }
            }
            else
            {
                db.pqlDangKy_TaoKyHieu(ref sSoPhieu);
                txtSoDK.Text = sSoPhieu;

                var date = db.GetSystemDate();
                spinTuNam.EditValue = date.Year;
                spinTuThang.EditValue = date.Month;
                txtKhachHang.Text = KhachHang;
                txtMatBang.Text = MaSoMB;
                try
                {
                    var objMB = db.mbMatBangs.Single(p => p.MaMB == MaMB);
                    //spinSoTien.EditValue = objMB.PhiQuanLy ?? 0;
                    spinChuKy.EditValue = 3;
                    spinTongThu.EditValue = 3 * spinSoTien.Value;
                }
                catch { }
            }
        }

        private void spinTuThang_EditValueChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void spinTuNam_EditValueChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (txtSoDK.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số đăng ký], xin cảm ơn.");
                txtSoDK.Focus();
                return;
            }

            objDK.ChuKyID = Convert.ToInt32(spinChuKy.EditValue);
            objDK.MonthFrom = Convert.ToInt32(spinTuThang.EditValue);
            objDK.MonthTo = Convert.ToInt32(spinDenThang.EditValue);
            objDK.SoDK = txtSoDK.Text;
            objDK.YearFrom = Convert.ToInt32(spinTuNam.EditValue);
            objDK.YearTo = Convert.ToInt32(spinDenNam.EditValue);
            objDK.DienGiai = txtDienGiai.Text;
            objDK.SoTien = spinSoTien.Value;
            objDK.ChietKhau = spinChietKhau.Value;
            objDK.PhaiThu = spinPhaiThu.Value;
            if (lookChietKhau.EditValue != null)
                objDK.ChietKhauID = Convert.ToInt32(lookChietKhau.EditValue);
            else
                objDK.ChietKhauID = null;

            objDK.NgayDK = dateFrom;
            objDK.NgayKT = dateTo;

            var ListCN = new List<cnLichSu>();
            DateTime KyDau = new DateTime((int)objDK.YearFrom, (int)objDK.MonthFrom, 1);
            DateTime KyCUoi = new DateTime((int)objDK.YearTo, (int)objDK.MonthTo, 1);
            decimal? SoTien = objDK.PhaiThu / objDK.ChuKyID;
            if (ID == null)
            {
                //Check
                var count = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, dateFrom) >= 0 & SqlMethods.DateDiffDay(dateFrom, p.NgayKT) >= 0 & p.MaMB == MaMB).ToList().Count;
                if (count > 0)
                {
                    DialogBox.Alert("[Mặt bằng] này đã đăng ký phí quản lý. Vui lòng kiểm tra lại.");
                    return;
                }
                objDK = new pqlDangKy();
                objDK.MaNV = objNV.MaNV;
                objDK.MaKH = MaKH;
                objDK.MaMB = MaMB;
                objDK.NgayTao = db.GetSystemDate();
                db.pqlDangKies.InsertOnSubmit(objDK);

                for (int i = 0; i < objDK.ChuKyID; i++)
                {
                    var obj = new cnLichSu();
                    obj.NgayNhap = KyDau.AddMonths(i);
                    obj.MaMB = objDK.MaMB;
                    obj.MaKH = objDK.MaKH;
                    obj.MaLDV = 12;
                    obj.SoTien = obj.Payment = obj.DaThu = SoTien;
                    ListCN.Add(obj);
                }
            }
            else
            {
                objDK.MaNVCN = objNV.MaNV;
                objDK.NgayCN = db.GetSystemDate();
                var ListOBJdelete = db.cnLichSus.Where(p => p.MaMB == objDKTemp.MaMB & SqlMethods.DateDiffMonth(KyDau, p.NgayNhap) >= 0 & SqlMethods.DateDiffMonth(p.NgayNhap, KyCUoi) >= 0);
                db.cnLichSus.DeleteAllOnSubmit(ListOBJdelete);
            }
            try
            {
                db.cnLichSus.InsertAllOnSubmit(ListCN);
                db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
            }
            catch { }
            this.Close();
        }

        private void lookChietKhau_EditValueChanged(object sender, EventArgs e)
        {
            #region tinh chiet khau
            try
            {
                spinTongThu.EditValue = Convert.ToInt32(spinChuKy.EditValue) * SoTien;
                //int SoThangThanhToan = Convert.ToInt32(spinChuKy.EditValue.ToString());
                var obj = db.PhiQuanLy_ChietKhaus.Where(p => p.ID == (int?)lookChietKhau.EditValue).OrderByDescending(p => p.SoThangThanhToan).FirstOrDefault();
                if (obj == null)
                {
                    lookChietKhau.EditValue = null;
                    spinChietKhau.Value = 0;
                }
                else
                {
                    lookChietKhau.EditValue = obj.ID;
                    spinChietKhau.Value = Math.Round(spinTongThu.Value * (obj.TiLeChietKhau ?? 0), 0, MidpointRounding.AwayFromZero);
                }

                spinPhaiThu.EditValue = spinTongThu.Value - spinChietKhau.Value;
            }
            catch { }
            #endregion
        }
    }
}