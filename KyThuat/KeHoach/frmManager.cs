using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraBars.Alerter;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.XtraReports.UI;

namespace KyThuat.KeHoach
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        DateTime? NgayTaoViec;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
            now = db.GetSystemDate();
        }

        void LoadData()
        {
            db = new MasterDataContext();
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcKeHoach.DataSource = db.khbtKeHoaches
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayKH.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayKH.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayKH)
                        .Select(p => new
                        {
                            LoaiKH=p.LoaiLichBT==1?"Lịch BT hằng ngày":(p.LoaiLichBT==2?"Lịch BT hằng tuần":(p.LoaiLichBT==3?"Lịch BT hằng tháng":"Lịch BT hằng năm")),
                            p.MaKH,
                            p.NgayKH,
                            p.MaSoKH,
                            p.khbtTrangThai.TenTT,
                            p.khbtTrangThai.MauNen,
                            p.ChiPhi,
                            p.NoiBo,
                            p.ThueNgoai,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.TuNgay,
                            p.DenNgay,
                            p.IsLoop, 
                            KieuBT = p.IsBTTaiSan.GetValueOrDefault() == true? "Bảo trì tài sản" : "Bảo trì hệ thống"
                        });
                }
                else
                {
                    gcKeHoach.DataSource = db.khbtKeHoaches
                        .Where(p => p.MaTN == objnhanvien.MaTN &
                                SqlMethods.DateDiffDay(tuNgay, p.NgayKH.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayKH.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayKH)
                        .Select(p => new
                        {
                            LoaiKH=p.LoaiLichBT==1?"Lịch BT hằng ngày":(p.LoaiLichBT==2?"Lịch BT hằng tuần":(p.LoaiLichBT==3?"Lịch BT hằng tháng":"Lịch BT hằng năm")),
                            p.MaKH,
                            p.NgayKH,
                            p.MaSoKH,
                            p.khbtTrangThai.TenTT,
                            p.khbtTrangThai.MauNen,
                            p.ChiPhi,
                            p.NoiBo,
                            p.ThueNgoai,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.TuNgay,
                            p.DenNgay,
                            p.IsLoop,
                            KieuBT = p.IsBTTaiSan.GetValueOrDefault() == true ? "Bảo trì tài sản" : "Bảo trì hệ thống"
                        });
                }
            }
            else
            {
                gcKeHoach.DataSource = null;
            }
        }

        void LoadDataByPage()
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                gcTaiSan.DataSource = null;
                gcThietBi.DataSource = null;
                gcDoiTac.DataSource = null;
                gcNhanSu.DataSource = null;
                return;
            }

            var maKH = grvKeHoach.GetFocusedRowCellValue("MaKH").ToString();

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcTaiSan.DataSource = db.khbtTaiSans.Where(p => String.Compare(p.MaKH.ToString(), maKH, false) == 0)
                            .Select(p => new
                            {
                                p.ID,
                                MaTS = p.MaTS == null ? null : p.tsTaiSan.MaTS,
                                TenTS = p.MaTS == null ? null : p.tsTaiSan.TenTS,
                                // p.tsTrangThai.TenTT,
                                p.DienGiai,
                                TenHT = p.tsTaiSan.MaHT == null ? "" : p.tsTaiSan.tsHeThong.TenHT,
                                MaSoMB = p.tsTaiSan.MaMB == null ? "" : p.tsTaiSan.mbMatBang.MaSoMB
                            });
                    break;
                case 1:
                    gcThietBi.DataSource = db.khbtThietBis.Where(p => String.Compare(p.MaKH.ToString(), maKH, false) == 0)
                        .Select(p => new { p.tsLoaiTaiSan.TenLTS, p.SoLuong, p.DienGiai });
                    break;
                case 2:
                    gcDoiTac.DataSource = db.khbtDoiTacs.Where(p => String.Compare(p.MaKH.ToString(), maKH, false) == 0)
                        .Select(p => new { p.tnNhaCungCap.TenNCC, p.DienGiai });
                    break;
                case 3:
                    gcNhanSu.DataSource = db.khbtNhanViens.Where(p => String.Compare(p.MaKH.ToString(), maKH, false) == 0)
                        .Select(p => new { p.tnNhanVien.MaSoNV, p.tnNhanVien.HoTenNV, p.DienGiai });
                    break;
                default:
                    break;
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        string getNewMaBT()
        {
            string MaBT = "";
            db.btDauMucCongViec_getNewMaBT(ref MaBT);
            return db.DinhDang(32, int.Parse(MaBT));
        }

        public int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public string GetDayofWeek(int day)
        {
            string Sday = "";
            switch (day)
            {
                case 1:
                    //DateTime.Now.DayOfWeek();
                    break;
            }
            return Sday;
        }

        void CreateWork_LBT1(khbtKeHoach KHBT)
        {
            btDauMucCongViec objDMCV = new btDauMucCongViec();
            btDauMucCongViec_NhanVien objCVNV;
            btDauMucCongViec_TaiSan objCVTS;
            btDauMucCongViec_ThietBi objCVTB;
            btDauMucCongViec_DoiTac objCVDT;
            try
            {
                btKeyDauMuc objKey;
                objKey = db.btKeyDauMucs.FirstOrDefault(p => p.CurrentYear == NgayTaoViec.Value.Year);
                if (objKey == null)//chua phat sinh cv trong nam nay
                {
                    //delete het du lieu cu trong nam truoc
                    db.btKeyDauMucDelete();
                }
                objKey = new btKeyDauMuc();
                objKey.Code = string.Format("{0:dd/MM/yyyy}-1-{1}", NgayTaoViec.Value,KHBT.MaKH);
                objKey.MaNV = objnhanvien.MaNV;
                objKey.CurrentYear = NgayTaoViec.Value.Year;
                objKey.MaLoai = 1;
                db.btKeyDauMucs.InsertOnSubmit(objKey);
                db.SubmitChanges();
                objDMCV = new btDauMucCongViec();
                db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                objDMCV.NguonCV = 2;
                objDMCV.KeyCode = objKey.Code;
                objDMCV.MaNguonCV = KHBT.MaKH;
                objDMCV.MaSoCV = getNewMaBT();
                objDMCV.MoTa = KHBT.DienGiai;
                objDMCV.MaTN = KHBT.MaTN;
                objDMCV.MaKN = KHBT.MaKN;
                objDMCV.MaTL = KHBT.MaTL;
                objDMCV.MaMB = KHBT.MaMB;
                objDMCV.TrangThaiCV = 1;
                objDMCV.ThoiGianTheoLich = NgayTaoViec.Value;
                objDMCV.ThoiGianGhiNhan = DateTime.Now;
                KHBT.MaTT = 3;
                //
                foreach (var p in KHBT.khbtNhanViens.ToList())
                {
                    objCVNV = new btDauMucCongViec_NhanVien();
                    objCVNV.MaNVTH = p.MaNV;
                    objDMCV.btDauMucCongViec_NhanViens.Add(objCVNV);

                }
                foreach (var p in KHBT.khbtTaiSans.ToList())
                {
                    objCVTS = new btDauMucCongViec_TaiSan();
                    objCVTS.DienGiai = p.DienGiai;
                    objCVTS.MaTS = p.MaTS;
                    objDMCV.btDauMucCongViec_TaiSans.Add(objCVTS);
                }
                foreach (var p in KHBT.khbtDoiTacs.ToList())
                {
                    objCVDT = new btDauMucCongViec_DoiTac();
                    objCVDT.DienGiai = p.DienGiai;
                    objCVDT.MaDT = p.MaDT;
                    objDMCV.btDauMucCongViec_DoiTacs.Add(objCVDT);
                }
                foreach (var p in KHBT.khbtThietBis.ToList())
                {
                    objCVTB = new btDauMucCongViec_ThietBi();
                    objCVTB.DienGiai = p.DienGiai;
                    objCVTB.MaLTS = p.MaTB;
                    objDMCV.btDauMucCongViec_ThietBis.Add(objCVTB);
                }
                KHBT.MaTT = 3;
                db.SubmitChanges();
            }
            catch { }
        }

        void CreateWork_LBT2(khbtKeHoach KHBT)
        {
            btDauMucCongViec objDMCV = new btDauMucCongViec();
            btDauMucCongViec_NhanVien objCVNV;
            btDauMucCongViec_TaiSan objCVTS;
            btDauMucCongViec_ThietBi objCVTB;
            btDauMucCongViec_DoiTac objCVDT;
            try
            {
                CultureInfo ci = new CultureInfo("en-US");
                string s=NgayTaoViec.Value.ToString("dddd",ci);
                if (KHBT.DayOfWeeks.Contains(NgayTaoViec.Value.ToString("dddd",ci))==true)
                {
                    btKeyDauMuc objKey;
                    objKey = db.btKeyDauMucs.FirstOrDefault(p => p.CurrentYear == NgayTaoViec.Value.Year);
                    if (objKey == null)//chua phat sinh cv trong nam nay
                    {
                        //delete het du lieu cu trong nam truoc
                        db.btKeyDauMucDelete();
                    }
                    objKey = new btKeyDauMuc();
                    db.btKeyDauMucs.InsertOnSubmit(objKey);
                    objKey.Code = string.Format("{0}/{1}/{2}-2-{3}", NgayTaoViec.Value.Day, GetWeekNumber(NgayTaoViec.Value), NgayTaoViec.Value.Year,KHBT.MaKH);
                    objKey.MaNV = objnhanvien.MaNV;
                    objKey.CurrentYear = NgayTaoViec.Value.Year;
                    objKey.MaLoai = 2;
                    db.SubmitChanges();
                    objDMCV = new btDauMucCongViec();
                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                    objDMCV.NguonCV = 2;
                    objDMCV.KeyCode = objKey.Code;
                    objDMCV.MaNguonCV = KHBT.MaKH;
                    objDMCV.MaSoCV = getNewMaBT();
                    objDMCV.MoTa = KHBT.DienGiai;
                    objDMCV.MaTN = KHBT.MaTN;
                    objDMCV.MaKN = KHBT.MaKN;
                    objDMCV.MaTL = KHBT.MaTL;
                    objDMCV.MaMB = KHBT.MaMB;
                    objDMCV.TrangThaiCV = 1;
                    objDMCV.ThoiGianTheoLich = NgayTaoViec.Value;
                    objDMCV.ThoiGianGhiNhan = DateTime.Now;
                    //
                    foreach (var p in KHBT.khbtNhanViens.ToList())
                    {
                        objCVNV = new btDauMucCongViec_NhanVien();
                        objCVNV.MaNVTH = p.MaNV;
                        objDMCV.btDauMucCongViec_NhanViens.Add(objCVNV);

                    }
                    foreach (var p in KHBT.khbtTaiSans.ToList())
                    {
                        objCVTS = new btDauMucCongViec_TaiSan();
                        objCVTS.DienGiai = p.DienGiai;
                        objCVTS.MaTS = p.MaTS;
                        objDMCV.btDauMucCongViec_TaiSans.Add(objCVTS);
                    }
                    foreach (var p in KHBT.khbtDoiTacs.ToList())
                    {
                        objCVDT = new btDauMucCongViec_DoiTac();
                        objCVDT.DienGiai = p.DienGiai;
                        objCVDT.MaDT = p.MaDT;
                        objDMCV.btDauMucCongViec_DoiTacs.Add(objCVDT);
                    }
                    foreach (var p in KHBT.khbtThietBis.ToList())
                    {
                        objCVTB = new btDauMucCongViec_ThietBi();
                        objCVTB.DienGiai = p.DienGiai;
                        objCVTB.MaLTS = p.MaTB;
                        objDMCV.btDauMucCongViec_ThietBis.Add(objCVTB);
                    }
                    KHBT.MaTT = 3;
                    db.SubmitChanges();
                }
            }
            catch { }
        }

        void CreateWork_LBT3(khbtKeHoach KHBT)
        {
            btDauMucCongViec objDMCV = new btDauMucCongViec();
            btDauMucCongViec_NhanVien objCVNV;
            btDauMucCongViec_TaiSan objCVTS;
            btDauMucCongViec_ThietBi objCVTB;
            btDauMucCongViec_DoiTac objCVDT;
            try
            {
                if (KHBT.DayOfMonth.Contains(NgayTaoViec.Value.Day.ToString()) == true)
                {
                    btKeyDauMuc objKey;
                    objKey = db.btKeyDauMucs.FirstOrDefault(p => p.CurrentYear == NgayTaoViec.Value.Year);
                    if (objKey == null)//chua phat sinh cv trong nam nay
                    {
                        //delete het du lieu cu trong nam truoc
                        db.btKeyDauMucDelete();
                    }
                    objKey = new btKeyDauMuc();
                    db.btKeyDauMucs.InsertOnSubmit(objKey);
                    objKey.Code = string.Format("{0:dd/MM/yyyy}-3-{1}", NgayTaoViec.Value, KHBT.MaKH);
                    objKey.MaNV = objnhanvien.MaNV;
                    objKey.CurrentYear = NgayTaoViec.Value.Year;
                    objKey.MaLoai = 3;
                    db.SubmitChanges();
                    objDMCV = new btDauMucCongViec();
                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                    objDMCV.NguonCV = 2;
                    objDMCV.KeyCode = objKey.Code;
                    objDMCV.MaNguonCV = KHBT.MaKH;
                    objDMCV.MaSoCV = getNewMaBT();
                    objDMCV.MoTa = KHBT.DienGiai;
                    objDMCV.MaTN = KHBT.MaTN;
                    objDMCV.MaKN = KHBT.MaKN;
                    objDMCV.MaTL = KHBT.MaTL;
                    objDMCV.MaMB = KHBT.MaMB;
                    objDMCV.TrangThaiCV = 1;
                    objDMCV.ThoiGianTheoLich = NgayTaoViec.Value;
                    objDMCV.ThoiGianGhiNhan = DateTime.Now;
                    //
                    foreach (var p in KHBT.khbtNhanViens)
                    {
                        objCVNV = new btDauMucCongViec_NhanVien();
                        objCVNV.MaNVTH = p.MaNV;
                        objDMCV.btDauMucCongViec_NhanViens.Add(objCVNV);

                    }
                    foreach (var p in KHBT.khbtTaiSans.ToList())
                    {
                        objCVTS = new btDauMucCongViec_TaiSan();
                        objCVTS.DienGiai = p.DienGiai;
                        objCVTS.MaTS = p.MaTS;
                        objDMCV.btDauMucCongViec_TaiSans.Add(objCVTS);
                    }
                    foreach (var p in KHBT.khbtDoiTacs.ToList())
                    {
                        objCVDT = new btDauMucCongViec_DoiTac();
                        objCVDT.DienGiai = p.DienGiai;
                        objCVDT.MaDT = p.MaDT;
                        objDMCV.btDauMucCongViec_DoiTacs.Add(objCVDT);
                    }
                    foreach (var p in KHBT.khbtThietBis.ToList())
                    {
                        objCVTB = new btDauMucCongViec_ThietBi();
                        objCVTB.DienGiai = p.DienGiai;
                        objCVTB.MaLTS = p.MaTB;
                        objDMCV.btDauMucCongViec_ThietBis.Add(objCVTB);
                    }
                    KHBT.MaTT = 3;
                    db.SubmitChanges();
                }
            }
            catch { }
        }

        void CreateWork_LBT4(khbtKeHoach KHBT)
        {
            btDauMucCongViec objDMCV = new btDauMucCongViec();
            btDauMucCongViec_NhanVien objCVNV;
            btDauMucCongViec_TaiSan objCVTS;
            btDauMucCongViec_ThietBi objCVTB;
            btDauMucCongViec_DoiTac objCVDT;
            try
            {
                if (KHBT.MonthOfYears.Contains(NgayTaoViec.Value.Month.ToString()) == true)
                {
                    btKeyDauMuc objKey;
                    objKey = db.btKeyDauMucs.FirstOrDefault(p => p.CurrentYear == NgayTaoViec.Value.Year);
                    if (objKey == null)//chua phat sinh cv trong nam nay
                    {
                        //delete het du lieu cu trong nam truoc
                        db.btKeyDauMucDelete();
                    }
                    objKey = new btKeyDauMuc();
                    db.btKeyDauMucs.InsertOnSubmit(objKey);
                    objKey.Code = string.Format("{0:MM/yyyy}-4-{1}", NgayTaoViec.Value, KHBT.MaKH);
                    objKey.MaNV = objnhanvien.MaNV;
                    objKey.CurrentYear = NgayTaoViec.Value.Year;
                    objKey.MaLoai = 4;
                    db.SubmitChanges();
                    objDMCV = new btDauMucCongViec();
                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                    objDMCV.NguonCV = 2;
                    objDMCV.KeyCode = objKey.Code;
                    objDMCV.MaNguonCV = (int?)KHBT.MaKH;
                    objDMCV.MaSoCV = getNewMaBT();
                    objDMCV.MoTa = KHBT.DienGiai;
                    objDMCV.MaTN = KHBT.MaTN;
                    objDMCV.MaKN = KHBT.MaKN;
                    objDMCV.MaTL = KHBT.MaTL;
                    objDMCV.MaMB = KHBT.MaMB;
                    objDMCV.TrangThaiCV = 1;
                    objDMCV.ThoiGianTheoLich = NgayTaoViec.Value;
                    objDMCV.ThoiGianGhiNhan = DateTime.Now;
                    //
                    foreach (var p in KHBT.khbtNhanViens.ToList())
                    {
                        objCVNV = new btDauMucCongViec_NhanVien();
                        objCVNV.MaNVTH = p.MaNV;
                        objDMCV.btDauMucCongViec_NhanViens.Add(objCVNV);

                    }
                    foreach (var p in KHBT.khbtTaiSans.ToList())
                    {
                        objCVTS = new btDauMucCongViec_TaiSan();
                        objCVTS.DienGiai = p.DienGiai;
                        objCVTS.MaTS = p.MaTS;
                        objDMCV.btDauMucCongViec_TaiSans.Add(objCVTS);
                    }
                    foreach (var p in KHBT.khbtDoiTacs.ToList())
                    {
                        objCVDT = new btDauMucCongViec_DoiTac();
                        objCVDT.DienGiai = p.DienGiai;
                        objCVDT.MaDT = p.MaDT;
                        objDMCV.btDauMucCongViec_DoiTacs.Add(objCVDT);
                    }
                    foreach (var p in KHBT.khbtThietBis.ToList())
                    {
                        objCVTB = new btDauMucCongViec_ThietBi();
                        objCVTB.DienGiai = p.DienGiai;
                        objCVTB.MaLTS = p.MaTB;
                        objDMCV.btDauMucCongViec_ThietBis.Add(objCVTB);
                    }
                    KHBT.MaTT = 3;
                    db.SubmitChanges();
                }
            }
            catch { }
        }

        void CreateWork()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var KHBT = db.khbtKeHoaches.Where(p => p.MaTT > 1 && SqlMethods.DateDiffDay(p.TuNgay, NgayTaoViec) >= 0 
                    && SqlMethods.DateDiffDay(NgayTaoViec, p.DenNgay) >= 0 && p.IsLoop.GetValueOrDefault() == true).ToList();
                for (int i = 0; i < KHBT.Count; i++)
                {
                    db = new MasterDataContext();
                    switch (KHBT[i].LoaiLichBT)
                    {
                            
                        case 1:// CV hằng ngày
                            CreateWork_LBT1(KHBT[i]);
                            break;
                        case 2: //cong viec hang tuan
                            CreateWork_LBT2(KHBT[i]);
                            break;
                        case 3: //công việc hằng tháng
                            CreateWork_LBT3(KHBT[i]);
                            break;
                        case 4: //công việc hằng năm
                            CreateWork_LBT4(KHBT[i]);
                            break;
                    }

                }
                DialogBox.Alert("Dữ liệu đã được cập nhật!");
            }
            catch 
            {
                DialogBox.Alert("Dữ liệu không thể cập nhật!");
            }
            finally
            {
                wait.Close();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
            timer1.Start();
            timer1_Tick(null, null);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var maKHs = "|";
            int[] indexs = grvKeHoach.GetSelectedRows();
            foreach (var p in indexs)
            {
                var objKH = db.khbtKeHoaches.SingleOrDefault(q => q.MaKH == (int?)grvKeHoach.GetRowCellValue(p,"MaKH"));
                if ((int)objKH.MaTT > 1)
                {
                    DialogBox.Alert("Trong danh sách xóa có kế hoạch đã được phê duyệt. Vui lòng kiểm tra lại!");
                    return;
                }
            }

            foreach (int i in indexs)
                maKHs += grvKeHoach.GetRowCellValue(i, "MaKH") + "|";

            //db.khbtTaiSans.DeleteAllOnSubmit(db.khbtTaiSans.Where(p => SqlMethods.Like(maKHs, "%" + p.MaKH + "%")));
            //db.khbtThietBis.DeleteAllOnSubmit(db.khbtThietBis.Where(p => SqlMethods.Like(maKHs, "%" + p.MaKH + "%")));
            //db.khbtDoiTacs.DeleteAllOnSubmit(db.khbtDoiTacs.Where(p => SqlMethods.Like(maKHs, "%" + p.MaKH + "%")));
            //db.khbtNhanViens.DeleteAllOnSubmit(db.khbtNhanViens.Where(p => SqlMethods.Like(maKHs, "%" + p.MaKH + "%")));
            db.khbtKeHoaches.DeleteAllOnSubmit(db.khbtKeHoaches.Where(p => SqlMethods.Like(maKHs, "%" + p.MaKH + "%")));

            db.SubmitChanges();

            grvKeHoach.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng mục cần sửa");
                return;
            }
            var objKH = db.khbtKeHoaches.SingleOrDefault(p => p.MaKH == (int?)grvKeHoach.GetFocusedRowCellValue("MaKH"));
            //if ((int)objKH.MaTT > 1)
            //{
            //    DialogBox.Alert("Kế hoạch này đã được phê duyệt vì vậy không thể sữa. Xin cảm ơn!");
            //    return;
            //}
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.objKH = db.khbtKeHoaches.Single(p => String.Compare(p.MaKH.ToString(), grvKeHoach.GetFocusedRowCellValue("MaKH").ToString(), false) == 0);
                if ((int)objKH.MaTT > 1)
                    frm.IsEdit = false;
                frm.ShowDialog();
                LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                LoadData();
            }
            
        }

        private void grvKeHoach_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn kế hoạch");
                return;
            }
            int MaKH = (int)grvKeHoach.GetFocusedRowCellValue("MaKH");
            report.rptKeHoachThietBi rpt = new report.rptKeHoachThietBi(MaKH);
            rpt.ShowPreviewDialog();
        }

        private void btnDuyetKeHoach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn kế hoạch");
                return;
            }

            if (DialogBox.Question("Duyệt kế hoạch này?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    int MaKH = (int)grvKeHoach.GetFocusedRowCellValue("MaKH");
                    var SLobjkehoach = db.khbtKeHoaches.Single(p => p.MaKH == MaKH);
                    if (SLobjkehoach.MaTT >= 2)
                    {
                        DialogBox.Alert("Kế hoạch này đã duyệt rồi");
                        return;
                    }
                    else
                    {
                        if (SLobjkehoach.IsLoop.GetValueOrDefault() == false)
                        {
                            var objDMCV = new btDauMucCongViec();
                            db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                            objDMCV.NguonCV = 2;
                            // objDMCV.KeyCode = objKey.Code;
                            objDMCV.MaNguonCV = SLobjkehoach.MaKH;
                            objDMCV.MaSoCV = getNewMaBT();
                            objDMCV.MoTa = SLobjkehoach.DienGiai;
                            objDMCV.MaTN = SLobjkehoach.MaTN;
                            objDMCV.MaKN = SLobjkehoach.MaKN;
                            objDMCV.MaTL = SLobjkehoach.MaTL;
                            objDMCV.MaMB = SLobjkehoach.MaMB;
                            objDMCV.TrangThaiCV = 1;
                            objDMCV.ThoiGianTheoLich = SLobjkehoach.TuNgay;
                            objDMCV.ThoiGianGhiNhan = DateTime.Now;
                            objDMCV.ThoiGianHetHan = SLobjkehoach.DenNgay;

                            SLobjkehoach.MaTT = 3;
                            //
                            foreach (var p in SLobjkehoach.khbtNhanViens.ToList())
                            {
                                var objCVNV = new btDauMucCongViec_NhanVien();
                                objCVNV.MaNVTH = p.MaNV;
                                objDMCV.btDauMucCongViec_NhanViens.Add(objCVNV);

                            }
                            foreach (var p in SLobjkehoach.khbtTaiSans.ToList())
                            {
                                var objCVTS = new btDauMucCongViec_TaiSan();
                                objCVTS.DienGiai = p.DienGiai;
                                objCVTS.MaTS = p.MaTS;
                                objDMCV.btDauMucCongViec_TaiSans.Add(objCVTS);
                            }
                            foreach (var p in SLobjkehoach.khbtDoiTacs.ToList())
                            {
                                var objCVDT = new btDauMucCongViec_DoiTac();
                                objCVDT.DienGiai = p.DienGiai;
                                objCVDT.MaDT = p.MaDT;
                                objDMCV.btDauMucCongViec_DoiTacs.Add(objCVDT);
                            }
                            foreach (var p in SLobjkehoach.khbtThietBis.ToList())
                            {
                                var objCVTB = new btDauMucCongViec_ThietBi();
                                objCVTB.DienGiai = p.DienGiai;
                                objCVTB.MaLTS = p.MaTB;
                                objDMCV.btDauMucCongViec_ThietBis.Add(objCVTB);
                            }

                        }
                        else
                        {
                            SLobjkehoach.MaTT = 2;
                        }
                        db.SubmitChanges();
                        DialogBox.Alert("Duyệt thành công");
                        LoadData();
                    }
                }
                catch
                {
                    DialogBox.Alert("Duyệt không thành công. Vui lòng thử lại.");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = 0;
            if (objnhanvien.IsSuperAdmin.Value)
            {
                count = db.khbtKeHoaches
                    .Where(p => SqlMethods.DateDiffDay(now, p.NgayKH.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.NgayKH.Value, now) >= 0 & p.MaTT == 2).ToList().Count;
            }
            else
            {
                count = db.khbtKeHoaches
                    .Where(p => p.MaTN == objnhanvien.MaTN &
                            SqlMethods.DateDiffDay(now, p.NgayKH.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.NgayKH.Value, now) >= 0 & p.MaTT == 2).ToList().Count;
            }
            if (count > 0)
            {
                string noidung = string.Format("Hôm nay ({0}) có {1} kế hoạch bảo trì phải thực hiện",now.ToShortDateString(),count);
                AlertInfo info = new AlertInfo("<b>KẾ HOẠCH BẢO TRÌ - BẢO DƯỠNG</b>", noidung);
                alertControlNhacKeHoach.Show(this, info);
            }
        }

        private void itemCreateWork_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTimeSelect frm = new frmTimeSelect();
            frm.ShowDialog();
            NgayTaoViec = frm.NgayTao;
            if (NgayTaoViec != null)
                CreateWork();
        }

        private void grvKeHoach_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (e.Column == colTrangThai)
                {
                    e.Appearance.BackColor = Color.FromArgb((int)grvKeHoach.GetRowCellValue(e.RowHandle, "MauNen"));
                }
            }
        }
    }
}