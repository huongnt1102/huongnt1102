using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Data.Linq;

namespace Library.Controls.NhiemVu
{
    public partial class Select_frm : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int MaNVu = 0;
        public string TieuDe = "";
        bool KT = false, KT1 = false;
        public Select_frm()
        {
            InitializeComponent();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                TieuDe = gridView1.GetFocusedRowCellValue(colTieuDe).ToString();
                this.Close();
            }
            else
                DialogBox.Alert("Vui lòng chọn <Nhiệm vụ>, xin cảm ơn.");
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            gridControl1.DataSource = (from nv in db.NhiemVus
                                       join lnv in db.NhiemVu_Loais on nv.MaLNV equals lnv.MaLNV into loai
                                       from lnv in loai.DefaultIfEmpty()
                                       join md in db.NhiemVu_MucDos on nv.MaMD equals md.MaMD into mucdo
                                       from md in mucdo.DefaultIfEmpty()
                                       join tt in db.NhiemVu_TinhTrangs on nv.MaTT equals tt.MaTT into dstt
                                       from tt in dstt.DefaultIfEmpty()
                                       join nvien in db.tnNhanViens on nv.MaNV equals nvien.MaNV into dsnv
                                       from nvien in dsnv.DefaultIfEmpty()
                                       join kh in db.tnKhachHangs on nv.MaKH equals kh.MaKH into dskh
                                       from kh in dskh.DefaultIfEmpty()
                                       join nv1 in db.tnNhanViens on nv.NguoiCapNhat equals nv1.MaNV into dsnv1
                                       from nv1 in dsnv1.DefaultIfEmpty()
                                       where SqlMethods.DateDiffDay(dateTuNgay.DateTime, nv.NgayBD) >= 0 & SqlMethods.DateDiffDay(nv.NgayBD, dateDenNgay.DateTime) >= 0 & nv.MaNV == Common.User.MaNV
                                       & nvien.MaTN == Common.User.MaTN & ((int)lookUpLoaiNVu.EditValue == 0 || nv.MaLNV == (int?)lookUpLoaiNVu.EditValue)
                                       & ((int)lookUpStatus.EditValue == 0 || nv.MaTT == (int?)lookUpStatus.EditValue) & ((int)lookUpMucDo.EditValue == 0 || nv.MaTT == (int?)lookUpMucDo.EditValue)
                                       select new
                                       {
                                           nv.TieuDe,
                                           nv.NgayBD,
                                           nv.NgayHH,
                                           nv.MaNVu,
                                           nv.NgayNhac,
                                           TongSoNgay = SqlMethods.DateDiffDay(DateTime.Now, nv.NgayHH),
                                           lnv.TenLNV,
                                           md.TenMD,
                                           tt.TenTT,
                                           tt.MaTT,
                                           nv.NgayHT,
                                           NguoiCapNhat = nv1.HoTenNV,
                                           NguoiTao = nvien.HoTenNV,
                                           nv.MaNV,
                                           nv.TienDo,
                                           HoTenKH = kh.HoKH + " " + kh.TenKH,
                                       }).AsEnumerable()
                                                   .Select((p, index) => new
                                                   {
                                                       STT = index + 1,
                                                       p.TieuDe,
                                                       p.NgayBD,
                                                       p.NgayHH,
                                                       p.MaNVu,
                                                       p.NgayNhac,
                                                       p.TongSoNgay,
                                                       p.TenLNV,
                                                       p.TenMD,
                                                       p.TenTT,
                                                       p.NgayHT,
                                                       p.NguoiCapNhat,
                                                       p.NguoiTao,
                                                       p.MaNV,
                                                       p.TienDo,
                                                       p.HoTenKH,
                                                   }).ToList();

            try
            {
                wait.Close();
            }
            catch { }
            finally
            {
                //o = null;
                System.GC.Collect();
            }
        }

        int ThangDauCuaQuy(int Thang)
        {
            if (Thang <= 3)
                return 1;
            else if (Thang <= 6)
                return 4;
            else if (Thang <= 9)
                return 7;
            else
                return 10;
        }

        void SetToDate()
        {
            KT = false;
            KT1 = false;
            dateDenNgay.Enabled = false;
            dateTuNgay.Enabled = false;
            DateTime dateHachToan = DateTime.Now.Date;
            switch (cmbKyBC.SelectedIndex)
            {
                case 0: //Ngay nay
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
                case 1: //Tuan nay
                    dateDenNgay.DateTime = dateHachToan.AddDays(7 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 2: //Dau tuan den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 3: //Thang nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 4: //Dau thang den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 5: //Quy nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month) + 2, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 6: //Dau quy den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 7: //Nam nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 8: //Dau nam den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 9: //Thang 1
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 10: //Thang 2
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1);

                    break;
                case 11: //Thang 3
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1);

                    break;
                case 12: //Thang 4
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 13: //Thang 5
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1);

                    break;
                case 14: //Thang 6
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1);

                    break;
                case 15: //Thang 7
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 16: //Thang 8
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1);

                    break;
                case 17: //Thang 9
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1);

                    break;
                case 18: //Thang 10
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 19: //Thang 11
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1);

                    break;
                case 20: //Thang 12
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1);

                    break;
                case 21: //Quy I
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 22: //Quy II
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 23: //Quy III
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 24: //Quy IV
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 25: //Tuan truoc
                    dateDenNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek - 6);

                    break;
                case 26: //Thang truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(-1);

                    break;
                case 27: //Quy truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddMonths(-3);

                    break;
                case 28: //Nam truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year - 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year - 1, 1, 1);

                    break;
                case 29: //Tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(14 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 30: //Bon tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(35 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 31: //Thang sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(2).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1);

                    break;
                case 32: //Quy sau
                    switch (ThangDauCuaQuy(dateHachToan.Month))
                    {
                        case 10:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 4, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);
                            break;

                        case 1:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);
                            break;
                        case 4:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);
                            break;
                        case 7:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);
                            break;
                    }
                    break;

                case 33: //Nam sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);

                    break;
                case 34: //Tu chon
                    dateDenNgay.Enabled = true;
                    dateTuNgay.Enabled = true;
                    KT = true;
                    KT1 = true;
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
            }
        }

        void LoadDictionary()
        {
            Library.NhiemVuCls o = new Library.NhiemVuCls();
            lookUpStatus.Properties.DataSource = o.TinhTrang.SelectAll();
            lookUpStatus.ItemIndex = 0;
            lookUpMucDo.Properties.DataSource = o.MucDo.SelectAll();
            lookUpMucDo.ItemIndex = 0;
            lookUpLoaiNVu.Properties.DataSource = o.LoaiNV.SelectAll();
            lookUpLoaiNVu.ItemIndex = 0;
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToDate();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void Select_frm_Load(object sender, EventArgs e)
        {
            cmbKyBC.SelectedIndex = 3;
            LoadDictionary();
            LoadData();
        }
    }
}