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
using System.Data.Linq.SqlClient;
using System.Net;
using DevExpress.XtraGrid.Views.Grid;
using FTP;
using Building.AppVime;
using System.IO;
using DevExpress.XtraReports.UI;

namespace DichVu.YeuCau
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public int? MaYC { get; set; }

        MasterDataContext db;
        bool first = true;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        public void ClickYc()
        {
            //
            pic.Image = null;
            picUpload.Image = null;
            int? maYc = xtraTabMain.SelectedTabPageIndex == 0 ? (int?)grvYeuCau.GetFocusedRowCellValue("ID") : (int?)gvKyThuat.GetFocusedRowCellValue("ID");
            if (maYc == null)
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        txtYeuCau.Text = "";
                        break;
                    case 1:
                        gcNKXL.DataSource = null;
                        break;
                    case 2:
                        gcGiaoViec.DataSource = null;
                        gcGiaoViecDetail.DataSource = null;
                        break;
                    case 3:
                        gcHinhAnh.DataSource = null;
                        break;
                    case 4:
                        gcUpload.DataSource = null;
                        break;
                }
                return;
            }




            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    txtYeuCau.Text = grvYeuCau.GetFocusedRowCellValue("NoiDung").ToString();
                    break;
                case 1:
                    #region Load Nhật ký xử lý
                    gcNKXL.DataSource = (from ct in db.tnycLichSuCapNhats
                                         join nv in db.tnNhanViens on ct.MaNV equals nv.MaNV into vns
                                         from nv in vns.DefaultIfEmpty()
                                         join kh in db.tnKhachHangs on ct.MaKH equals kh.MaKH into khs
                                         from kh in khs.DefaultIfEmpty()
                                         orderby ct.NgayCN descending
                                         where ct.MaYC == maYc
                                         select new
                                         {
                                             ct.ID,
                                             ct.tnycTrangThai.TenTT,
                                             ct.NgayCN,
                                             ct.NoiDung,
                                             HoTenNV = ct.MaKH != null ? kh.HoKH + " " + kh.TenKH : nv.HoTenNV,
                                             isKH = ct.MaKH != null ? "KH" : "NV",
                                             ct.tnycTrangThai.MauNen
                                         }).ToList();
                    #endregion
                    break;
                case 2:
                    LoadDetail();
                    break;
                case 3:
                    gcHinhAnh.DataSource = (from p in db.tnycHinhAnhs where p.MaYC == maYc select new { p.ID, p.URL }).ToList();
                    LoadImage();
                    break;
                case 4:
                    gcUpload.DataSource = (from p in db.tnycImgNhanVienUploads where p.MaYC == maYc select new { p.ID, p.URL }).ToList();
                    LoadImageUpLoad();
                    break;
            }

        }
        DateTime? ThoiGianXuLy(DateTime? ThoiGian, int? MaDoUuTienID, byte? MaTN, bool IsQuaHan)
        {
            DateTime? result = (DateTime?)null;
            var objResult = db.tnycDoUuTien_ThoiGianXuLies.FirstOrDefault(p => p.MaTN == MaTN && p.MaDoUuTienID == MaDoUuTienID);
            if (objResult != null)
            {
                if (IsQuaHan)
                {
                    if (ThoiGian == null || objResult.ThoiGianCanhBaoToiHan == null)
                    {
                        return (DateTime?)null;
                    }
                    return ThoiGian.Value.AddHours((double)objResult.ThoiGianCanhBaoToiHan.Value);
                }
                else
                {
                    if (ThoiGian == null || objResult.ThoiGianXuLy == null)
                    {
                        return (DateTime?)null;
                    }
                    return ThoiGian.Value.AddHours((double)objResult.ThoiGianXuLy.Value);
                }
            }

            return result;
        }
        int? GetColorDoUuTien(int? MaDoUuTienID, byte? MaTN, bool IsQuaHan)
        {
            int? result = (int?)null;
            var objResult = db.tnycDoUuTien_ThoiGianXuLies.FirstOrDefault(p => p.MaTN == MaTN && p.MaDoUuTienID == MaDoUuTienID);
            if (objResult != null)
            {
                if (IsQuaHan)
                {
                    if (objResult.MauCanhBaoQuaHan == null)
                    {
                        return (int?)null;
                    }
                    return (int)objResult.MauCanhBaoQuaHan;
                }
                else
                {
                    if (objResult.MauCanhBaoToiHan == null)
                    {
                        return (int?)null;
                    }
                    return (int)objResult.MauCanhBaoToiHan;
                }
            }

            return result;
        }
        void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                db = new MasterDataContext();
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var strToaNha = (itemListToaNha.EditValue ?? "").ToString().Replace(" ", "");
                var arrToaNha = strToaNha.Split(',');
                #region Tab Yêu cầu cư dân
                var objYC = (from p in db.tnycYeuCaus
                             join ncv in db.app_GroupProcesses on p.GroupProcessId equals ncv.Id into ncviec
                             from ncv in ncviec.DefaultIfEmpty()
                             join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                             from nv in nvs.DefaultIfEmpty()
                             join ntn in db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                             from ntn in ntns.DefaultIfEmpty()
                             join es in db.tnycTrangThaiGiaHans on p.ExtendStatusId equals es.Id into extendStatus
                             from es in extendStatus.DefaultIfEmpty()
                             where arrToaNha.Contains(p.MaTN.ToString()) == true
                             & ((MaYC != null & p.ID == MaYC) | (MaYC == null & SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0))
                             orderby p.NgayYC descending
                             select new
                             {
                                 p.MaTN,
                                 p.tnToaNha.TenTN,
                                 p.ID,
                                 p.MaYC,
                                 p.NgayYC,
                                 p.TieuDe,
                                 p.NoiDung,
                                 p.Rating,
                                 p.RatingComment,
                                 p.tnycTrangThai.TenTT,
                                 p.tnPhongBan.TenPB,
                                 TenKH = p.NguoiGui,
                                 HoTenNTN = ntn.HoTenNV,
                                 HoTenNV = nv.HoTenNV,
                                 p.tnycTrangThai.MauNen,
                                 p.tnycDoUuTien.TenDoUuTien,
                                 MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB,
                                 p.tnycNguonDen.TenNguonDen,
                                 TenNhomCongViec = ncv.Name,
                                 p.NgayHetHanBH,
                                 NgayGanHetHan = p.NgayHetHanBH != null ? SqlMethods.DateDiffDay(p.NgayHetHanBH.Value.AddDays(-1), System.DateTime.UtcNow.AddHours(7)) >= 0 : false,
                                 p.MaMB,
                                 ExtendStatusName = es != null ? es.Name : "",
                                 p.NewDeadline,
                                 p.ExtendReason,
                                 p.ApprovedReason,
                                 ThoiGianXuLy = ThoiGianXuLy(p.NgayYC, p.MaDoUuTien, p.MaTN, false),
                                 ThoiGianCanhBaoToiHan = ThoiGianXuLy(p.NgayYC, p.MaDoUuTien, p.MaTN, true),
                                 MauCanhBaoToiHan = GetColorDoUuTien(p.MaDoUuTien, p.MaTN, false),
                                 MauCanhBaoQuaHan = GetColorDoUuTien(p.MaDoUuTien, p.MaTN, true),
                                 p.MaTT

                             }).ToList();
                var objLSYC = (from p in db.tnycYeuCaus
                               join ct in db.tnycLichSuCapNhats on p.ID equals ct.MaYC
                               where SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                               & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                               & arrToaNha.Contains(p.MaTN.ToString()) == true
                               orderby ct.NgayCN descending
                               select new
                               {
                                   ct.ID,
                                   ct.MaYC,
                                   ct.NgayCN,
                                   ct.MaTT,
                                   ct.NoiDung
                               }).ToList();
                switch (xtraTabMain.SelectedTabPageIndex)
                {
                    case 0:
                        var objData = (from yc in objYC
                                       where yc.MaMB != null
                                       select new
                                       {
                                           yc.MaTN,
                                           yc.TenTN,
                                           yc.ID,
                                           yc.MaYC,
                                           yc.NgayYC,
                                           yc.TieuDe,
                                           yc.NoiDung,
                                           yc.Rating,
                                           yc.RatingComment,
                                           yc.TenTT,
                                           yc.TenPB,
                                           yc.TenKH,
                                           yc.HoTenNTN,
                                           yc.HoTenNV,
                                           yc.MauNen,
                                           yc.TenDoUuTien,
                                           yc.MaSoMB,
                                           yc.TenNguonDen,
                                           yc.TenNhomCongViec,
                                           yc.NgayHetHanBH,
                                           yc.NgayGanHetHan,
                                           ThoiGian = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID) == null ? 0 : SqlMethods.DateDiffHour(yc.NgayYC, objLSYC.FirstOrDefault(p => p.MaYC == yc.ID).NgayCN),
                                           NgayXL = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3) == null ? (DateTime?)null : objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3).NgayCN,
                                           NgayDong = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 5) == null ? (DateTime?)null : objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 5).NgayCN,
                                           NoiDungXuLy = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3) == null ? "" : objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3).NoiDung,
                                           yc.ExtendStatusName,
                                           yc.NewDeadline,
                                           yc.ApprovedReason,
                                           yc.ExtendReason,
                                           yc.ThoiGianXuLy,
                                           yc.MauCanhBaoQuaHan,
                                           yc.ThoiGianCanhBaoToiHan,
                                           yc.MauCanhBaoToiHan,
                                           yc.MaTT
                                       });
                        gcYeuCau.DataSource = objData;
                        break;
                    case 1:
                        var objDataKT = (from yc in objYC
                                         where yc.MaMB == null
                                         select new
                                         {
                                             yc.MaTN,
                                             yc.TenTN,
                                             yc.ID,
                                             yc.MaYC,
                                             yc.NgayYC,
                                             yc.TieuDe,
                                             yc.NoiDung,
                                             yc.Rating,
                                             yc.RatingComment,
                                             yc.TenTT,
                                             yc.TenPB,
                                             yc.TenKH,
                                             yc.HoTenNTN,
                                             yc.HoTenNV,
                                             yc.MauNen,
                                             yc.TenDoUuTien,
                                             yc.MaSoMB,
                                             yc.TenNguonDen,
                                             yc.TenNhomCongViec,
                                             yc.NgayGanHetHan,
                                             yc.NgayHetHanBH,
                                             ThoiGian = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID) == null ? 0 : SqlMethods.DateDiffHour(yc.NgayYC, objLSYC.FirstOrDefault(p => p.MaYC == yc.ID).NgayCN),
                                             NgayXL = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3) == null ? (DateTime?)null : objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3).NgayCN,
                                             NgayDong = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 5) == null ? (DateTime?)null : objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 5).NgayCN,
                                             NoiDungXuLy = objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3) == null ? "" : objLSYC.FirstOrDefault(p => p.MaYC == yc.ID && p.MaTT == 3).NoiDung,
                                             yc.ExtendStatusName,
                                             yc.NewDeadline,
                                             yc.ApprovedReason,
                                             yc.ExtendReason,
                                             yc.ThoiGianXuLy,
                                             yc.MauCanhBaoQuaHan,
                                             yc.ThoiGianCanhBaoToiHan,
                                             yc.MauCanhBaoToiHan,
                                             yc.MaTT
                                         });
                        gcKyThuat.DataSource = objDataKT;
                        break;
                    default:
                        gcKyThuat.DataSource = null;
                        gcYeuCau.DataSource = null;
                        break;
                }
                #endregion
                #region Tab yêu cầu kỹ thuật
                //gcKyThuat.DataSource = (from p in db.tnycYeuCaus
                //                       join ncv in db.app_GroupProcesses on p.GroupProcessId equals ncv.Id into ncviec
                //                       from ncv in ncviec.DefaultIfEmpty()
                //                       join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                //                       from nv in nvs.DefaultIfEmpty()
                //                       join ntn in db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                //                       from ntn in ntns.DefaultIfEmpty()
                //                       where SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                //                       & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                //                       & arrToaNha.Contains(p.MaTN.ToString()) == true & p.MaMB ==null
                //                       orderby p.NgayYC descending
                //                       select new
                //                       {
                //                           p.MaTN,
                //                           p.tnToaNha.TenTN,
                //                           p.ID,
                //                           p.MaYC,
                //                           p.NgayYC,
                //                           p.TieuDe,
                //                           p.NoiDung,
                //                           p.Rating,
                //                           p.RatingComment,
                //                           p.tnycTrangThai.TenTT,
                //                           p.tnPhongBan.TenPB,
                //                           TenKH = p.NguoiGui,
                //                           HoTenNTN = ntn.HoTenNV,
                //                           HoTenNV = nv.HoTenNV,
                //                           p.tnycTrangThai.MauNen,
                //                           p.tnycDoUuTien.TenDoUuTien,
                //                           //p.mbMatBang.MaSoMB,
                //                           p.tnycNguonDen.TenNguonDen,
                //                           TenNhomCongViec = ncv.Name,
                //                           ThoiGian =
                //                           (from ct in db.tnycLichSuCapNhats

                //                            orderby ct.NgayCN descending
                //                            where ct.MaYC == p.ID
                //                            select new
                //                            {

                //                                ct.NgayCN,

                //                            }).FirstOrDefault().NgayCN == null ? 0 : SqlMethods.DateDiffHour(p.NgayYC, (from ct in db.tnycLichSuCapNhats

                //                                                                                                        orderby ct.NgayCN descending
                //                                                                                                        where ct.MaYC == p.ID
                //                                                                                                        select new
                //                                                                                                        {

                //                                                                                                            ct.NgayCN,

                //                                                                                                        }).FirstOrDefault().NgayCN),
                //                           NgayXL = (from ct in db.tnycLichSuCapNhats

                //                                     orderby ct.NgayCN descending
                //                                     where ct.MaYC == p.ID & ct.MaTT == 3
                //                                     select new
                //                                     {

                //                                         ct.NgayCN,

                //                                     }).FirstOrDefault().NgayCN,
                //                           NgayDong = (from ct in db.tnycLichSuCapNhats

                //                                       orderby ct.NgayCN descending
                //                                       where ct.MaYC == p.ID & ct.MaTT == 5
                //                                       select new
                //                                       {

                //                                           ct.NgayCN,

                //                                       }).FirstOrDefault().NgayCN,
                //                           NoiDungXuLy = (from ct in db.tnycLichSuCapNhats
                //                                          orderby ct.ID descending
                //                                          where ct.MaYC == p.ID & ct.MaTT == 3
                //                                          select new
                //                                          {

                //                                              ct.NoiDung,

                //                                          }).FirstOrDefault().NoiDung,
                //                       }).ToList();
                #endregion
            }
            else
            {
                gcYeuCau.DataSource = null;
            }

            //grvYeuCau.BestFitColumns();
            //grvNKXL.BestFitColumns();
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }
        private void GiaoViec()
        {
            try
            {
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    if (grvYeuCau.GetFocusedRowCellValue("HoTenNTN") != null)
                    {
                        DialogBox.Error("Công việc này đã có nhân viên thực hiện");
                        return;
                    }
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    if (gvKyThuat.GetFocusedRowCellValue("HoTenNTN") != null)
                    {
                        DialogBox.Error("Công việc này đã có nhân viên thực hiện");
                        return;
                    }
                }
                XuLyGiaoViec(false);
            }
            catch
            { }
        }

        private void XuLyGiaoViec(bool isHide)
        {
            using (frmGiaoViec frm = new frmGiaoViec())
            {
                int MaYC = 0;
                int MaTN = 0;
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    MaYC = int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString());
                    MaTN = int.Parse(grvYeuCau.GetFocusedRowCellValue("MaTN").ToString());
                }
                else
                {
                    MaYC = int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString());
                    MaTN = int.Parse(gvKyThuat.GetFocusedRowCellValue("MaTN").ToString());
                }
                frm.MaYC = MaYC;
                frm.MaTN = MaTN;
                frm.TrangThai = 1;
                frm.isHide = isHide;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void DoiNhanVien()
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }
                using (frmGiaoViec frm = new frmGiaoViec())
                {
                    frm.MaYC = int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString());
                    frm.MaTN = int.Parse(grvYeuCau.GetFocusedRowCellValue("MaTN").ToString());
                    frm.TrangThai = 2;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }
                using (frmGiaoViec frm = new frmGiaoViec())
                {
                    frm.MaYC = int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString());
                    frm.MaTN = int.Parse(gvKyThuat.GetFocusedRowCellValue("MaTN").ToString());
                    frm.TrangThai = 2;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }
        private void HoanThanh()
        {
            try
            {
                int MaYC = 0;
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    MaYC = int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString());
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    MaYC = int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString());
                }
                var Data = new MasterDataContext();
                var yc = Data.tnycYeuCaus.Single(p => p.ID == MaYC);
                if (yc != null)
                {
                    yc.MaTT = 3;

                    tnycLichSuCapNhat ls = new tnycLichSuCapNhat();
                    ls.MaNV = Common.User.MaNV;
                    ls.MaTT = 3;
                    ls.MaYC = MaYC;
                    ls.NgayCN = Common.GetDateTimeSystem();
                    var frm = new frmNoiDung();
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        ls.NoiDung = frm.NoiDung;
                        Data.tnycLichSuCapNhats.InsertOnSubmit(ls);
                        Data.SubmitChanges();
                        DialogBox.Success("Đã lưu");
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;

                        LoadData();
                    }

                }
            }
            catch
            { }
        }
        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            chkListToaNha.DataSource = Common.TowerList;
            itemListToaNha.EditValue = Common.User.MaTN;
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
            first = false;
            grvYeuCau.BestFitColumns();
            grvNKXL.BestFitColumns();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    int[] indexs = grvYeuCau.GetSelectedRows();

                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu cần xóa!");
                        return;
                    }

                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    List<tnycYeuCau> ListXoa = new List<tnycYeuCau>();
                    foreach (int i in indexs)
                    {
                        var objyc = db.tnycYeuCaus.Single(p => p.ID == (int)grvYeuCau.GetRowCellValue(i, "ID"));
                        if (objyc.MaTT > 1)
                        {
                            DialogBox.Alert("Bạn chỉ có thể xóa các yêu cầu trong tình trạng [Yêu cầu mới]. Vui lòng kiểm tra lại!");
                            return;
                        }
                        ListXoa.Add(objyc);
                        db.tnycLichSuCapNhats.DeleteAllOnSubmit(objyc.tnycLichSuCapNhats);
                        //
                    }
                    db.tnycYeuCaus.DeleteAllOnSubmit(ListXoa);

                    db.SubmitChanges();
                    grvYeuCau.DeleteSelectedRows();
                }
                else
                {
                    int[] indexs = gvKyThuat.GetSelectedRows();

                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu cần xóa!");
                        return;
                    }

                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    List<tnycYeuCau> ListXoa = new List<tnycYeuCau>();
                    foreach (int i in indexs)
                    {
                        var objyc = db.tnycYeuCaus.Single(p => p.ID == (int)gvKyThuat.GetRowCellValue(i, "ID"));
                        if (objyc.MaTT > 1)
                        {
                            DialogBox.Alert("Bạn chỉ có thể xóa các yêu cầu trong tình trạng [Yêu cầu mới]. Vui lòng kiểm tra lại!");
                            return;
                        }
                        ListXoa.Add(objyc);
                        db.tnycLichSuCapNhats.DeleteAllOnSubmit(objyc.tnycLichSuCapNhats);
                    }
                    db.tnycYeuCaus.DeleteAllOnSubmit(ListXoa);
                    db.SubmitChanges();

                    gvKyThuat.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                //DialogBox.Error("Đã xảy ra lỗi trong quá trình xóa: "+ex);
                LoadData();
            }

        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var strBuildingId = (itemListToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var ltBuildingId = strBuildingId.Split(',');
            if (strBuildingId == "") { return; }

            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                using (frmEdit frm = new frmEdit())
                {
                    frm.ID = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                    frm.BuildingIds = ltBuildingId;
                    frm.MaTN = (byte?)grvYeuCau.GetFocusedRowCellValue("MaTN") ?? Common.User.MaTN;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                using (frmEdit frm = new frmEdit())
                {
                    frm.ID = (int?)gvKyThuat.GetFocusedRowCellValue("ID");
                    frm.BuildingIds = ltBuildingId;
                    frm.MaTN = (byte?)gvKyThuat.GetFocusedRowCellValue("MaTN") ?? Common.User.MaTN;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var strBuildingId = (itemListToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var ltBuildingId = strBuildingId.Split(',');
            if (strBuildingId == "") { return; }

            using (frmEdit frm = new frmEdit())
            {
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    frm.MaTN = (byte?)grvYeuCau.GetFocusedRowCellValue("MaTN") ?? Common.User.MaTN;
                    frm.BuildingIds = ltBuildingId;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
                else
                {
                    frm.MaTN = (byte?)gvKyThuat.GetFocusedRowCellValue("MaTN") ?? Common.User.MaTN;
                    frm.BuildingIds = ltBuildingId;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
        }

        private void btnPhanHoiYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                using (frmXuLyYC frm = new frmXuLyYC())
                {
                    int? maTN = (int?)grvYeuCau.GetFocusedRowCellValue("MaTN");
                    frm.MaYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                    frm.MaTN = maTN;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                using (frmXuLyYC frm = new frmXuLyYC())
                {
                    int? maTN = (int?)gvKyThuat.GetFocusedRowCellValue("MaTN");
                    frm.MaYC = (int?)gvKyThuat.GetFocusedRowCellValue("ID");
                    frm.MaTN = maTN;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemXuLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region
            //if (grvYeuCau.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn yêu cầu");
            //    return;
            //}
            //var frm = new ToaNha.NhacViec_ThongBao.frmNhacViecEdit();
            //frm.objnhanvien = objnhanvien;
            //frm.MaYC = (int)grvYeuCau.GetFocusedRowCellValue("ID");
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //     ClickYC();
            #endregion
        }

        private void grvYeuCau_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClickYc();
        }

        void LoadDetail()
        {
            var id = (int?)grvGiaoViec.GetFocusedRowCellValue("MaNhacViec");
            if (id == null) gcGiaoViecDetail.DataSource = null;
            gcGiaoViecDetail.DataSource =
                db.tnNhacViec_Details.Select(p => new { p.tnNhanVien.HoTenNV, p.DaDoc }).ToList();
        }

        private void grvGiaoViec_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        string getNewMaBT()
        {
            string MaBT = "";
            db.btDauMucCongViec_getNewMaBT(ref MaBT);
            return db.DinhDang(32, int.Parse(MaBT));
        }

        private void btnGuiYC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn yêu cầu để gửi cho bộ phận kĩ thuật. Xin cảm ơn!");
                    return;
                }
                var objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == (int)grvYeuCau.GetFocusedRowCellValue("ID"));
                if (objYC.MaTT > 1)
                {
                    DialogBox.Alert("Yêu cầu này đã gửi hoàn thành gửi cho kỹ thuật. Vui lòng chọn yêu cầu khác!");
                    return;
                }
                var wait = DialogBox.WaitingForm();
                try
                {
                    var objDMCV = new btDauMucCongViec();
                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);

                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                    objDMCV.MaSoCV = getNewMaBT();
                    objDMCV.MoTa = objYC.NoiDung;
                    objDMCV.MaTN = objYC.MaTN;
                    objDMCV.MaKN = objYC.mbMatBang.mbTangLau.MaKN;
                    objDMCV.MaTL = objYC.mbMatBang.MaTL;
                    objDMCV.MaMB = objYC.MaMB;
                    objDMCV.NguonCV = 0;
                    objDMCV.MaNguonCV = objYC.ID;
                    objDMCV.TrangThaiCV = 1;
                    objDMCV.ThoiGianGhiNhan = db.GetSystemDate();
                    objYC.MaTT = 2;

                    var objLS = new tnycLichSuCapNhat();
                    objYC.tnycLichSuCapNhats.Add(objLS);
                    objLS.MaNV = Common.User.MaNV;
                    objLS.NgayCN = db.GetSystemDate();
                    objLS.MaTT = 2;
                    db.SubmitChanges();
                    DialogBox.Alert("Yêu cầu đã được gửi cho bộ phận kĩ thuật. Xin cảm ơn!");
                    LoadData();
                    LoadDetail();
                }
                catch
                {
                    DialogBox.Alert("Có lỗi phát sinh. Không thể gửi yêu cầu này cho kĩ thuật!");
                }
                finally
                {
                    wait.Close();
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn yêu cầu để gửi cho bộ phận kĩ thuật. Xin cảm ơn!");
                    return;
                }
                var objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == (int)gvKyThuat.GetFocusedRowCellValue("ID"));
                if (objYC.MaTT > 1)
                {
                    DialogBox.Alert("Yêu cầu này đã gửi hoàn thành gửi cho kỹ thuật. Vui lòng chọn yêu cầu khác!");
                    return;
                }
                var wait = DialogBox.WaitingForm();
                try
                {
                    var objDMCV = new btDauMucCongViec();
                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);

                    db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                    objDMCV.MaSoCV = getNewMaBT();
                    objDMCV.MoTa = objYC.NoiDung;
                    objDMCV.MaTN = objYC.MaTN;
                    objDMCV.MaKN = objYC.mbMatBang.mbTangLau.MaKN;
                    objDMCV.MaTL = objYC.mbMatBang.MaTL;
                    objDMCV.MaMB = objYC.MaMB;
                    objDMCV.NguonCV = 0;
                    objDMCV.MaNguonCV = objYC.ID;
                    objDMCV.TrangThaiCV = 1;
                    objDMCV.ThoiGianGhiNhan = db.GetSystemDate();
                    objYC.MaTT = 2;

                    var objLS = new tnycLichSuCapNhat();
                    objYC.tnycLichSuCapNhats.Add(objLS);
                    objLS.MaNV = Common.User.MaNV;
                    objLS.NgayCN = db.GetSystemDate();
                    objLS.MaTT = 2;
                    db.SubmitChanges();
                    DialogBox.Alert("Yêu cầu đã được gửi cho bộ phận kĩ thuật. Xin cảm ơn!");
                    LoadData();
                    LoadDetail();
                }
                catch
                {
                    DialogBox.Alert("Có lỗi phát sinh. Không thể gửi yêu cầu này cho kĩ thuật!");
                }
                finally
                {
                    wait.Close();
                }
            }

        }

        private void btnXacNhanChoKT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn yêu cầu để phản hồi cho bộ phận kĩ thuật. Xin cảm ơn!");
                    return;
                }
                try
                {
                    frmTimeConfirm frm = new frmTimeConfirm();
                    frm.objNV = Common.User;
                    frm.MaNguonCV = (int)grvYeuCau.GetFocusedRowCellValue("ID");
                    frm.ShowDialog();
                    LoadData();
                    LoadDetail();
                }
                catch
                {
                    //  DialogBox.Alert("Có lỗi phát sinh. Không thể gửi yêu cầu này cho kĩ thuật!");
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn yêu cầu để phản hồi cho bộ phận kĩ thuật. Xin cảm ơn!");
                    return;
                }
                try
                {
                    frmTimeConfirm frm = new frmTimeConfirm();
                    frm.objNV = Common.User;
                    frm.MaNguonCV = (int)gvKyThuat.GetFocusedRowCellValue("ID");
                    frm.ShowDialog();
                    LoadData();
                    LoadDetail();
                }
                catch
                {
                    //  DialogBox.Alert("Có lỗi phát sinh. Không thể gửi yêu cầu này cho kĩ thuật!");
                }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            ClickYc();
        }

        private void btnInPhieuXN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn [Yêu cầu] để in phiếu xin xác nhận. Xin cảm ơn!");
                    return;
                }
                int? MaYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                var obj = db.tnycYeuCaus.SingleOrDefault(p => p.ID == MaYC);
                DichVu.YeuCau.rptPhieuYeuCauXN rpt = new rptPhieuYeuCauXN(MaYC);
                rpt.ShowPreviewDialog();
                var objLS = new tnycLichSuCapNhat();
                objLS.MaNV = Common.User.MaNV;
                objLS.NgayCN = db.GetSystemDate();
                objLS.MaYC = MaYC;
                objLS.MaTT = obj.MaTT;
                obj.tnycLichSuCapNhats.Add(objLS);
                try
                {
                    db.SubmitChanges();
                }
                catch
                {
                    DialogBox.Alert("Phiếu yêu cầu xác nhận của khách hàng không thể tao!");
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn [Yêu cầu] để in phiếu xin xác nhận. Xin cảm ơn!");
                    return;
                }
                int? MaYC = (int?)gvKyThuat.GetFocusedRowCellValue("ID");
                var obj = db.tnycYeuCaus.SingleOrDefault(p => p.ID == MaYC);
                DichVu.YeuCau.rptPhieuYeuCauXN rpt = new rptPhieuYeuCauXN(MaYC);
                rpt.ShowPreviewDialog();
                var objLS = new tnycLichSuCapNhat();
                objLS.MaNV = Common.User.MaNV;
                objLS.NgayCN = db.GetSystemDate();
                objLS.MaYC = MaYC;
                objLS.MaTT = obj.MaTT;
                obj.tnycLichSuCapNhats.Add(objLS);
                try
                {
                    db.SubmitChanges();
                }
                catch
                {
                    DialogBox.Alert("Phiếu yêu cầu xác nhận của khách hàng không thể tao!");
                }
            }
        }

        private void btnXacNhanYCKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmXacNhanCVKT frm = new frmXacNhanCVKT();
            frm.objNV = Common.User;
            frm.ShowDialog();
        }

        private void grvYeuCau_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //    {
            //        if (e.Column.FieldName == "TenTT" & grvYeuCau.GetRowCellValue(e.RowHandle, "MauNen") != null)
            //        {
            //            e.Appearance.BackColor = Color.FromArgb((int)grvYeuCau.GetRowCellValue(e.RowHandle, "MauNen"));
            //        }
            //    }
            //}
            //catch { }
            //GridView view = sender as GridView;
            //if (e.Column.FieldName == "TenTT")
            //{
            //    string tentt = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TenTT"]);
            //    if (tentt == "Yêu cầu mới") // 1
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightSalmon;
            //    }
            //    if (tentt == "Đã tiếp nhận thông tin") // 2
            //    {
            //        //e.Appearance.BackColor = Color.DeepSkyBlue;
            //        //e.Appearance.BackColor2 = Color.LightCyan;
            //        e.Appearance.BackColor = Color.DeepPink;
            //        e.Appearance.BackColor2 = Color.LightPink;
            //    }
            //    if (tentt == "Đã xử lý xong") //3
            //    {
            //        e.Appearance.BackColor = Color.DeepSkyBlue;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //    if (tentt == "Báo cáo hoàn thành") //5
            //    {
            //        e.Appearance.BackColor = Color.LimeGreen;
            //        e.Appearance.BackColor2 = Color.LightGreen;
            //    }
            //    if (tentt == "Hủy phiếu") //6
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.Appearance.BackColor2 = Color.GhostWhite;
            //    }
            //    if (tentt == "Đã gửi nhà thầu") //7
            //    {
            //        e.Appearance.BackColor = Color.Aquamarine;
            //        e.Appearance.BackColor2 = Color.Azure;
            //    }
            //    if (tentt == "Xử lý lại") //8
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //        e.Appearance.BackColor2 = Color.LightYellow;
            //    }
            //    if (tentt == "Giao nhân viên")//9
            //    {
            //        e.Appearance.BackColor = Color.Orange;
            //        e.Appearance.BackColor2 = Color.LightGoldenrodYellow;
            //    }
            //    if (tentt == "Không hoàn thành")//10
            //    {
            //        e.Appearance.BackColor = Color.DarkMagenta;
            //        e.Appearance.BackColor2 = Color.Plum;
            //    }
            //}
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                Commoncls.ExportExcel(gcYeuCau);
            }
            else
            {
                Commoncls.ExportExcel(gcKyThuat);
            }
        }

        private void grvNKXL_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string isKH = view.GetRowCellDisplayText(e.RowHandle, view.Columns["isKH"]);
                if (isKH == "KH")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void grvYeuCau_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            grvYeuCau.BestFitColumns();
            grvNKXL.BestFitColumns();
        }

        private void bbiGiaoViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.GiaoViec();
        }

        private void bbiDoiNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DoiNhanVien();
        }

        private void bbiDoiTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                if (grvYeuCau.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                using (frmXuLyYC frm = new frmXuLyYC())
                {
                    int? maTN = int.Parse(grvYeuCau.GetFocusedRowCellValue("MaTN").ToString());
                    frm.MaTN = maTN;

                    frm.MaYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                    frm.TrangThai = 2;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                if (gvKyThuat.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                using (frmXuLyYC frm = new frmXuLyYC())
                {
                    int? maTN = (int?)gvKyThuat.GetFocusedRowCellValue("MaTN");
                    frm.MaTN = maTN;

                    frm.MaYC = (int?)gvKyThuat.GetFocusedRowCellValue("ID");
                    frm.TrangThai = 2;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void btnHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.HoanThanh();
        }

        private void grvNKXL_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    if (e.Column.FieldName == "TenTT")
                    {
                        if (grvNKXL.GetRowCellValue(e.RowHandle, "MauNen") != null)
                            e.Appearance.BackColor = Color.FromArgb((int)grvNKXL.GetRowCellValue(e.RowHandle, "MauNen"));
                    }
                }
            }
            catch { }
            //GridView view = sender as GridView;
            //if (e.Column.FieldName == "TenTT")
            //{
            //    string tentt = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TenTT"]);
            //    if (tentt == "Đã tiếp nhận thông tin") // 2
            //    {
            //        //e.Appearance.BackColor = Color.DeepSkyBlue;
            //        //e.Appearance.BackColor2 = Color.LightCyan;
            //        e.Appearance.BackColor = Color.DeepPink;
            //        e.Appearance.BackColor2 = Color.LightPink;
            //    }
            //    if (tentt == "Đã xử lý xong") //3
            //    {
            //        e.Appearance.BackColor = Color.DeepSkyBlue;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //    if (tentt == "Báo cáo hoàn thành") //5
            //    {
            //        e.Appearance.BackColor = Color.LimeGreen;
            //        e.Appearance.BackColor2 = Color.LightGreen;
            //    }
            //    if (tentt == "Hủy phiếu") //6
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.Appearance.BackColor2 = Color.GhostWhite;
            //    }
            //    if (tentt == "Đã gửi nhà thầu") //7
            //    {
            //        e.Appearance.BackColor = Color.Aquamarine;
            //        e.Appearance.BackColor2 = Color.Azure;
            //    }
            //    if (tentt == "Xử lý lại") //8
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //        e.Appearance.BackColor2 = Color.LightYellow;
            //    }
            //    if (tentt == "Giao nhân viên")//9
            //    {
            //        e.Appearance.BackColor = Color.Orange;
            //        e.Appearance.BackColor2 = Color.LightGoldenrodYellow;
            //    }
            //    if (tentt == "Không hoàn thành")//10
            //    {
            //        e.Appearance.BackColor = Color.DarkMagenta;
            //        e.Appearance.BackColor2 = Color.Plum;
            //    }
            //}
        }

        private void bbiAdminXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // thêm phần này để xóa cho nhanh - HuongNT
            int[] indexs = grvYeuCau.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu cần xóa!");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            List<tnycYeuCau> ListXoa = new List<tnycYeuCau>();
            foreach (int i in indexs)
            {
                var objyc = db.tnycYeuCaus.Single(p => p.ID == (int)grvYeuCau.GetRowCellValue(i, "ID"));
                if (objyc != null)
                {
                    var listLs = (from p in db.tnycLichSuCapNhats
                                  where p.MaYC == objyc.ID
                                  select p).ToList();
                    foreach (var rLs in listLs)
                    {
                        db.tnycLichSuCapNhats.DeleteOnSubmit(rLs);
                        db.SubmitChanges();
                    }
                }
                ListXoa.Add(objyc);
            }
            db.tnycYeuCaus.DeleteAllOnSubmit(ListXoa);
            db.SubmitChanges();

            grvYeuCau.DeleteSelectedRows();
        }

        void LoadImage()
        {
            try
            {
                var hinhAnh = (from p in db.tnycHinhAnhs
                               where p.ID == (int?)gvHinhAnh.GetFocusedRowCellValue("ID")
                               select new
                               {
                                   HinhAnh = p.URL
                               }).FirstOrDefault();
                //var ftp = db.tblConfigs.FirstOrDefault();
                if (hinhAnh != null)
                {
                    //    var url = ftp.WebUrl + hinhAnh.HinhAnh;
                    var request = WebRequest.Create(hinhAnh.HinhAnh);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            pic.Image = Bitmap.FromStream(stream);
                            //pic.Width = 200;
                            //pic.Height = 161;
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void gvHinhAnh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadImage();
        }

        private void gvHinhAnh_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvHinhAnh.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvHinhAnh); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvHinhAnh); }));
            }

        }
        bool cal(Int32 _width, GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void btnUpload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UploadImageToFtp();
            //UploadImageToLinkWeb();
        }

        private void UploadImageToFtp()
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                var id = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                if (id == null) return;

                using (var frm = new FTP.frmUploadFile())
                {
                    if (frm.SelectFile(true))
                    {
                        frm.Folder = "/Upload";
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var img = new tnycImgNhanVienUpload();
                            img.MaYC = id;
                            img.URL = frm.FileName;
                            db.tnycImgNhanVienUploads.InsertOnSubmit(img);
                            db.SubmitChanges();
                            ClickYc();
                        }
                    }
                }
            }
            else
            {
                var id = (int?)gvKyThuat.GetFocusedRowCellValue("ID");
                if (id == null) return;

                using (var frm = new FTP.frmUploadFile())
                {
                    if (frm.SelectFile(true))
                    {
                        frm.Folder = "/Upload";
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var img = new tnycImgNhanVienUpload();
                            img.MaYC = id;
                            img.URL = frm.FileName;
                            db.tnycImgNhanVienUploads.InsertOnSubmit(img);
                            db.SubmitChanges();
                            ClickYc();
                        }
                    }
                }
            }
        }

        private void UploadImageToLinkWeb()
        {
            if (xtraTabMain.SelectedTabPageIndex == 0)
            {
                var id = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                if (id == null) return;

                var frm = new FTP.frmUploadFile();
                if (frm.SelectFile(true))
                {
                    var wait = DialogBox.WaitingForm();
                    wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                    var mineType = CommonVime.GetMimeType(frm.ClientPath);

                    byte[] img = File.ReadAllBytes(frm.ClientPath);

                    CommonVime.GetConfig();

                    var model = new
                    {
                        Bytes = img,
                        ApiKey = CommonVime.ApiKey,
                        SecretKey = CommonVime.SecretKey,
                        MineType = mineType
                    };

                    var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecret");
                    var link = ret.Replace("\"", "");

                    wait.Close();
                    wait.Dispose();

                    try
                    {
                        var imgUp = new tnycImgNhanVienUpload();
                        imgUp.MaYC = id;
                        imgUp.URL = link.Replace("/thumb/", "/");
                        imgUp.thumbnail = link;
                        db.tnycImgNhanVienUploads.InsertOnSubmit(imgUp);
                        db.SubmitChanges();
                    }
                    catch { }
                    ClickYc();
                }
                frm.Dispose();
            }
            else
            {
                var id = (int?)gvKyThuat.GetFocusedRowCellValue("ID");
                if (id == null) return;

                var frm = new FTP.frmUploadFile();
                if (frm.SelectFile(true))
                {
                    var wait = DialogBox.WaitingForm();
                    wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                    var mineType = CommonVime.GetMimeType(frm.ClientPath);

                    byte[] img = File.ReadAllBytes(frm.ClientPath);

                    CommonVime.GetConfig();

                    var model = new
                    {
                        Bytes = img,
                        ApiKey = CommonVime.ApiKey,
                        SecretKey = CommonVime.SecretKey,
                        MineType = mineType
                    };

                    var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecret");
                    var link = ret.Replace("\"", "");

                    wait.Close();
                    wait.Dispose();

                    try
                    {
                        var imgUp = new tnycImgNhanVienUpload();
                        imgUp.MaYC = id;
                        imgUp.URL = link.Replace("/thumb/", "/");
                        imgUp.thumbnail = link;
                        db.tnycImgNhanVienUploads.InsertOnSubmit(imgUp);
                        db.SubmitChanges();
                    }
                    catch { }
                    ClickYc();
                }
                frm.Dispose();
            }
        }

        void LoadImageUpLoad()
        {
            var wait = DialogBox.WaitingForm();
            wait.SetCaption("Đang tải hình. Vui lòng chờ...");

            picUpload.Image = null;
            pic.Image = null;
            try
            {
                var hinhAnh = (from p in db.tnycImgNhanVienUploads
                               where p.ID == (int?)gvUpload.GetFocusedRowCellValue("ID")
                               select new
                               {
                                   HinhAnh = p.URL
                               }).FirstOrDefault();
                var ftp = db.tblConfigs.FirstOrDefault();
                if (hinhAnh != null)
                {
                    var url = "";
                    if (!hinhAnh.HinhAnh.Contains("http"))
                    {
                        url = ftp.WebUrl + hinhAnh.HinhAnh;
                    }
                    else
                    {
                        url = hinhAnh.HinhAnh;
                    }

                    var request = WebRequest.Create(url);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            picUpload.Image = Bitmap.FromStream(stream);
                        }
                    }
                }

                wait.Close();
                wait.Dispose();
            }
            catch (Exception)
            {
                wait.Close();
                wait.Dispose();
            }
            finally
            {
                if (!wait.IsDisposed)
                {
                    wait.Close();
                    wait.Dispose();
                }
            }
        }

        void LoadImageUpLoadFtp()
        {
            picUpload.Image = null;
            pic.Image = null;
            try
            {
                var hinhAnh = (from p in db.tnycImgNhanVienUploads
                               where p.ID == (int?)gvUpload.GetFocusedRowCellValue("ID")
                               select new
                               {
                                   HinhAnh = p.URL
                               }).FirstOrDefault();
                var ftp = db.tblConfigs.FirstOrDefault();
                if (hinhAnh != null & ftp != null)
                {
                    var url = ftp.WebUrl + hinhAnh.HinhAnh;
                    var request = WebRequest.Create(url);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            picUpload.Image = Bitmap.FromStream(stream);
                            //pic.Width = 200;
                            //pic.Height = 161;
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvUpload.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn Hình ảnh");
                    return;
                }
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    foreach (var i in indexs)
                    {
                        var obj = db.tnycImgNhanVienUploads.Single(p => p.ID == (int)gvUpload.GetRowCellValue(i, "ID"));
                        db.tnycImgNhanVienUploads.DeleteOnSubmit(obj);

                        try
                        {
                            FtpClient ftp = new FTP.FtpClient();
                            ftp.Url = obj.URL;
                            ftp.DeleteFile();
                        }
                        catch { }
                    }
                    db.SubmitChanges();
                    ClickYc();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void gvUpload_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadImageUpLoadFtp();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FrmImport frm = new FrmImport())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    var Data = new MasterDataContext();
                    var yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString()));
                    if (yc != null)
                    {
                        yc.MaTT = 2;

                        tnycLichSuCapNhat ls = new tnycLichSuCapNhat();
                        ls.MaNV = Common.User.MaNV;
                        ls.MaTT = 2;
                        ls.MaYC = int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString());
                        ls.NgayCN = db.GetSystemDate();
                        var frm = new frmNoiDung();
                        frm.ShowDialog();
                        if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            ls.NoiDung = frm.NoiDung;
                            ls.isNew = true;
                            Data.tnycLichSuCapNhats.InsertOnSubmit(ls);
                            Data.SubmitChanges();
                            DialogBox.Success("Đã lưu");
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;

                            LoadData();
                        }

                    }
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    var Data = new MasterDataContext();
                    var yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString()));
                    if (yc != null)
                    {
                        yc.MaTT = 2;

                        tnycLichSuCapNhat ls = new tnycLichSuCapNhat();
                        ls.MaNV = Common.User.MaNV;
                        ls.MaTT = 2;
                        ls.MaYC = int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString());
                        ls.NgayCN = db.GetSystemDate();
                        var frm = new frmNoiDung();
                        frm.ShowDialog();
                        if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            ls.NoiDung = frm.NoiDung;
                            ls.isNew = true;
                            Data.tnycLichSuCapNhats.InsertOnSubmit(ls);
                            Data.SubmitChanges();
                            DialogBox.Success("Đã lưu");
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;

                            LoadData();
                        }

                    }
                }
            }
            catch
            { }
        }

        private void gvKyThuat_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "TenTT")
            {
                string tentt = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TenTT"]);
                if (tentt == "Yêu cầu mới") // 1
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.LightSalmon;
                }
                if (tentt == "Đã tiếp nhận thông tin") // 2
                {
                    //e.Appearance.BackColor = Color.DeepSkyBlue;
                    //e.Appearance.BackColor2 = Color.LightCyan;
                    e.Appearance.BackColor = Color.DeepPink;
                    e.Appearance.BackColor2 = Color.LightPink;
                }
                if (tentt == "Đã xử lý xong") //3
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
                if (tentt == "Báo cáo hoàn thành") //5
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.BackColor2 = Color.LightGreen;
                }
                if (tentt == "Hủy phiếu") //6
                {
                    e.Appearance.BackColor = Color.Gainsboro;
                    e.Appearance.BackColor2 = Color.GhostWhite;
                }
                if (tentt == "Đã gửi nhà thầu") //7
                {
                    e.Appearance.BackColor = Color.Aquamarine;
                    e.Appearance.BackColor2 = Color.Azure;
                }
                if (tentt == "Xử lý lại") //8
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.BackColor2 = Color.LightYellow;
                }
                if (tentt == "Giao nhân viên")//9
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.Appearance.BackColor2 = Color.LightGoldenrodYellow;
                }
                if (tentt == "Không hoàn thành")//10
                {
                    e.Appearance.BackColor = Color.DarkMagenta;
                    e.Appearance.BackColor2 = Color.Plum;
                }
            }
        }

        private void gvKyThuat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClickYc();
        }

        private void itemImportPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FrmImport frm = new FrmImport())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void importHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DichVu.YeuCau.Import.FrmLichSuYeuCau())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadDetail();
            }
        }

        private void itemGiaoViecChoPhongBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    if (grvYeuCau.GetFocusedRowCellValue("HoTenNTN") != null)
                    {
                        DialogBox.Error("Công việc này đã có nhân viên thực hiện");
                        return;
                    }
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    if (gvKyThuat.GetFocusedRowCellValue("HoTenNTN") != null)
                    {
                        DialogBox.Error("Công việc này đã có nhân viên thực hiện");
                        return;
                    }
                }
                XuLyGiaoViec(true);
            }
            catch
            { }
        }

        private void grvYeuCau_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                #region Code cũ
                //bool? isExpired = (bool?) view.GetRowCellValue(e.RowHandle, view.Columns["NgayGanHetHan"]);
                //if (isExpired.GetValueOrDefault())
                //{
                //    e.Appearance.BackColor = Color.Salmon;
                //    //e.Appearance.BackColor2 = Color.SeaShell;
                //}
                #endregion
                #region Code theo độ ưu tiên
                int? MaTT = (int?)view.GetRowCellValue(e.RowHandle, view.Columns["MaTT"]);
                if (MaTT.GetValueOrDefault() == 1 || MaTT.GetValueOrDefault() == 2)
                {
                    //So sánh thời gian hiện tại với thời  gian cảnh báo tới hạn
                    DateTime? ThoiGianCanhBaoToiHan = (DateTime?)view.GetRowCellValue(e.RowHandle, view.Columns["ThoiGianCanhBaoToiHan"]);
                    DateTime? ThoiGianXuLy = (DateTime?)view.GetRowCellValue(e.RowHandle, view.Columns["ThoiGianXuLy"]);
                    int? MauCanhBaoToiHan = (int?)view.GetRowCellValue(e.RowHandle, view.Columns["MauCanhBaoToiHan"]);
                    int? MauCanhBaoQuaHan = (int?)view.GetRowCellValue(e.RowHandle, view.Columns["MauCanhBaoQuaHan"]);
                    if (ThoiGianCanhBaoToiHan != null && ThoiGianCanhBaoToiHan <= DateTime.Now && DateTime.Now < ThoiGianXuLy)
                    {
                        e.Appearance.BackColor = Color.FromArgb(MauCanhBaoToiHan.Value);
                    }
                    if (ThoiGianXuLy != null && ThoiGianXuLy <= DateTime.Now)
                    {
                        e.Appearance.BackColor = Color.FromArgb(MauCanhBaoQuaHan.Value);
                    }
                }
                #endregion
            }
        }

        private void gvKyThuat_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                //bool? isExpired = (bool?)view.GetRowCellValue(e.RowHandle, view.Columns["NgayGanHetHan"]);
                //if (isExpired.GetValueOrDefault())
                //{
                //    e.Appearance.BackColor = Color.Salmon;
                //    //e.Appearance.BackColor2 = Color.SeaShell;
                //}
            }
        }

        public void Extend(MasterDataContext data, tnycYeuCau objRequest, int statusExtendId, bool isAgree, string reason)
        {
            objRequest.ExtendStatusId = statusExtendId;
            objRequest.NgayCN = DateTime.UtcNow.AddHours(7);

            tnycLichSuGiaHan objExtend = new tnycLichSuGiaHan()
            {
                ApprovedStatusId = statusExtendId,
                CreatedBy = Common.User.MaNV,
                CreatedDate = DateTime.UtcNow.AddHours(7),
                IsApprove = isAgree,
                NewDeadline = objRequest.NewDeadline,
                OldDeadline = objRequest.OldDeadline,
                Reason = reason,
                RequestSatusId = objRequest.MaTT
            };
            objRequest.tnycLichSuGiaHans.Add(objExtend);

            tnycLichSuCapNhat objLog = new tnycLichSuCapNhat()
            {
                MaNV = Common.User.MaNV,
                NgayCN = DateTime.UtcNow.AddHours(7),
                MaTT = objRequest.MaTT,
                NoiDung = reason,
                NgayHetHanDauTien = objRequest.NgayHetHanDauTien,
                NgayHetHanCuoiCung = objRequest.NgayHetHanCuoiCung,
                NgayGiaHan = DateTime.UtcNow.AddHours(7),
                LyDoGiaHan = reason
            };
            objRequest.tnycLichSuCapNhats.Add(objLog);

            data.SubmitChanges();

            DialogBox.Success("Đã lưu");
        }

        private void itemCreatedExtend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var Data = new MasterDataContext();
                tnycYeuCau yc = new tnycYeuCau();
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }

                    yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString()));
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString()));
                }

                if (yc == null)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                yc.ExtendDate = System.DateTime.UtcNow.AddHours(7);
                yc.ExtendBy = Common.User.MaNV;
                yc.OldDeadline = yc.NgayHetHanBH;
                yc.NewDeadline = yc.NgayHetHanBH;

                if (yc.NgayHetHanDauTien == null)
                {
                    yc.NgayHetHanDauTien = yc.NgayHetHanBH;
                }

                var frm = new Extend.frmExtend();
                {
                    frm.objRequest = yc;
                    frm.formName = "THÊM GIA HẠN";
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        frm.objRequest.ExtendReason = frm.reason;
                        Extend(Data, frm.objRequest, 1, false, frm.reason);

                        this.DialogResult = System.Windows.Forms.DialogResult.OK;

                        LoadData();
                    }
                }
            }
            catch
            { }
        }

        private void itemAgreeExtend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var Data = new MasterDataContext();
                tnycYeuCau yc = new tnycYeuCau();
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }

                    yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString()));
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString()));
                }

                if (yc == null)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                if (yc.ExtendStatusId.GetValueOrDefault() == 0)
                {
                    DialogBox.Error("Chưa tạo gia hạn");
                    return;
                }

                yc.ApprovedDate = System.DateTime.UtcNow.AddHours(7);
                yc.ApprovedBy = Common.User.MaNV;
                yc.OldDeadline = yc.NgayHetHanBH;

                var frm = new Extend.frmExtend();
                {
                    frm.objRequest = yc;
                    frm.formName = "DUYỆT GIA HẠN";
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        frm.objRequest.ApprovedReason = frm.reason;
                        frm.objRequest.NgayHetHanBH = frm.objRequest.NewDeadline;
                        frm.objRequest.NgayHetHanCuoiCung = frm.objRequest.NewDeadline;
                        frm.objRequest.SoLanGiaHan = frm.objRequest.SoLanGiaHan.GetValueOrDefault() + 1;
                        Extend(Data, frm.objRequest, 2, true, frm.reason);

                        this.DialogResult = System.Windows.Forms.DialogResult.OK;

                        LoadData();
                    }
                }
            }
            catch
            { }
        }

        private void itemDisagreeExtend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var Data = new MasterDataContext();
                tnycYeuCau yc = new tnycYeuCau();
                if (xtraTabMain.SelectedTabPageIndex == 0)
                {
                    if (grvYeuCau.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }

                    yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString()));
                }
                else
                {
                    if (gvKyThuat.FocusedRowHandle < 0)
                    {
                        DialogBox.Error("Vui lòng chọn yêu cầu");
                        return;
                    }
                    yc = Data.tnycYeuCaus.Single(p => p.ID == int.Parse(gvKyThuat.GetFocusedRowCellValue("ID").ToString()));
                }

                if (yc == null)
                {
                    DialogBox.Error("Vui lòng chọn yêu cầu");
                    return;
                }

                if (yc.ExtendStatusId.GetValueOrDefault() == 0)
                {
                    DialogBox.Error("Chưa tạo gia hạn");
                    return;
                }

                yc.ApprovedDate = System.DateTime.UtcNow.AddHours(7);
                yc.ApprovedBy = Common.User.MaNV;

                var frm = new Extend.frmExtend();
                {
                    frm.objRequest = yc;
                    frm.formName = "KHÔNG DUYỆT GIA HẠN";
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        frm.objRequest.ApprovedReason = frm.reason;
                        frm.objRequest.NgayHetHanBH = frm.objRequest.OldDeadline;
                        Extend(Data, frm.objRequest, 3, true, frm.reason);

                        this.DialogResult = System.Windows.Forms.DialogResult.OK;

                        LoadData();
                    }
                }
            }
            catch
            { }
        }
    }
}