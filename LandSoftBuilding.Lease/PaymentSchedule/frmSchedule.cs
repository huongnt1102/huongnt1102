using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace LandSoftBuilding.Lease.PaymentSchedule
{
    public partial class frmSchedule : DevExpress.XtraEditors.XtraForm
    {
        public frmSchedule()
        {
            InitializeComponent();
        }

        string GetThongBaoTyGia(decimal? TyGiaTT, decimal? TyGiaHD, decimal? TyGiaAD, decimal? MucDCTG)
        {
            if (TyGiaTT != TyGiaAD)
            {
                var TyLeTD = ((TyGiaTT - TyGiaHD) / TyGiaTT).GetValueOrDefault();
                MucDCTG = MucDCTG.GetValueOrDefault();
                if (TyLeTD > MucDCTG)
                {
                    return "Lên giá";
                }
                else if (TyLeTD < -MucDCTG)
                {
                    return "Giảm giá";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        string GetThongBaoTyGiaTheoLTT(decimal? TyGiaHD, decimal? TyGiaAD)
        {
            if (TyGiaHD != TyGiaAD)
            {
                if (TyGiaAD > TyGiaHD)
                {
                    return "Lên giá";
                }
                else 
                {
                    return "Giảm giá";
                }

            }
            else
            {
                return "";
            }
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        void LoadData()
        {
            var _MaTN = (byte)itemToaNha.EditValue;
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            var db = new MasterDataContext();
            gcLTT.DataSource = null;
            gcLTT.DataSource = (from l in db.ctLichThanhToans
                                    //join ct in db.ctChiTiets on new { l.MaHD, l.MaMB } equals new { MaHD = ct.MaHDCT, ct.MaMB }
                                join mb in db.mbMatBangs on l.MaMB equals mb.MaMB into matBang
                                from mb in matBang.DefaultIfEmpty()
                                join p in db.ctHopDongs on l.MaHD equals p.ID into hopDong
                                from p in hopDong.DefaultIfEmpty()
                                join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                                join lt in db.LoaiTiens on p.MaLT equals lt.ID into loaiTien
                                from lt in loaiTien.DefaultIfEmpty()
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                where p.MaTN == (byte)itemToaNha.EditValue //& l.MaLDV == 2
                                    & SqlMethods.DateDiffDay(_TuNgay, l.TuNgay) >= 0 & SqlMethods.DateDiffDay(l.TuNgay, _DenNgay) >= 0
                                    & p.NgungSuDung.GetValueOrDefault() == false
                                //& ct.NgungSuDung == false
                                orderby mb.MaSoMB, l.DotTT
                                select new DataItem
                                {
                                    ID = l.ID,
                                    MaHD = l.MaHD,
                                    MaMB = l.MaMB,
                                    MaSoMB = mb != null ? mb.MaSoMB : "",
                                    TenTL = tl.TenTL,
                                    TenKN = kn.TenKN,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    DotTT = l.DotTT,
                                    TuNgay = l.TuNgay,
                                    DenNgay = l.DenNgay,
                                    SoThang = l.SoThang,
                                    SoTien = l.SoTien,
                                    IsGui = false,
                                    TenLT = lt.KyHieuLT,
                                    TyGia = l.TyGia,
                                    SoTienQD = l.SoTienQD,
                                    DienGiai = l.DienGiai,
                                    SoHD = p.SoHDCT,
                                    TyGiaHD = p.TyGiaHD,
                                    TyGiaTT = l.TyGia,
                                    TyLeTDTG = l.MaLDV == 2 ? (l.TyGia - p.TyGiaHD) / l.TyGia : 0,
                                    MucDCTG = p.MucDCTG,
                                    TBTDTG = l.MaLDV == 2 ? (p.TyGiaHD != l.TyGia ? (l.TyGia > p.TyGiaHD ? "Lên giá": "Giảm giá") : ""): "",
                                    ////TBTDTG = GetThongBaoTyGia(l.TyGia, p.TyGiaHD, l.TyGia, p.MucDCTG)
                                    //,
                                    IsCoHoaDon = db.dvHoaDons.FirstOrDefault(_ => _.LinkID == l.ID & _.TableName == "ctLichThanhToan") != null ? true : false,
                                    IdHoaDon = db.dvHoaDons.FirstOrDefault(_ => _.LinkID == l.ID & _.TableName == "ctLichThanhToan") != null
                                                ? db.dvHoaDons.FirstOrDefault(_ => _.LinkID == l.ID & _.TableName == "ctLichThanhToan").ID
                                                : 0
                                }).ToList();
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            this.LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemDieuChinhTyGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvLTT.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;

            }

            int? hopDongId = 0;

            var db = new MasterDataContext();
            try
            {
                frmLSDCGEdit frm = new frmLSDCGEdit();
                //frm.MaHD = id;
                frm.ShowDialog();
                if (frm.IsSave)
                {

                    foreach (var i in indexs)
                    {
                        var id = (int?)gvLTT.GetRowCellValue(i, "ID");

                        var ltt = db.ctLichThanhToans.FirstOrDefault(_ => _.ID == id);
                        if (ltt != null)
                        {
                            // Set giá trị cho id hợp đồng
                            hopDongId = ltt.MaHD;

                            var hoaDon = db.dvHoaDons.FirstOrDefault(_ => _.LinkID == ltt.ID & _.MaLDV == 2);
                            if (hoaDon == null)
                            {
                                var hopDong = db.ctHopDongs.FirstOrDefault(_ => _.ID == ltt.MaHD);
                                var chiTiet = db.ctChiTiets.FirstOrDefault(_ => _.MaHDCT == ltt.MaHD & _.MaMB == ltt.MaMB);
                                if (chiTiet != null)
                                {
                                    //Them vao lich su
                                    var ct = new ctLichSuDieuChinhGia();
                                    ct.MaCT = chiTiet.ID;
                                    ct.MaHD = ltt.MaHD;
                                    ct.NgayDC = (DateTime?)frm.NgayDieuChinh;
                                    ct.MaLDC = frm.PhanLoai;
                                    ct.GiaTriCu = ltt.TyGia;
                                    ct.TyLeDC = (ltt.TyGia - hopDong.TyGiaHD) / ltt.TyGia;
                                    ct.GiaTriMoi = frm.GiaTriMoi;
                                    ct.DienGiai = frm.DienGiai;
                                    ct.NgayNhap = db.GetSystemDate();
                                    ct.MaNVN = Common.User.MaNV;
                                    db.ctLichSuDieuChinhGias.InsertOnSubmit(ct);
                                }
                                

                                ltt.TyGia = frm.GiaTriMoi;
                                //ltt.SoTienQD = ltt.SoTien * frm.GiaTriMoi;

                                db.SubmitChanges();
                            }
                            
                        }
                    }
                    db.SubmitChanges();

                    #region Tính lại tiền lịch thanh toán theo công thức
                    Library.Class.Connect.QueryConnect.QueryData<bool>("ctLichThanhToanUpdateValue", new { MaHD = hopDongId });
                    #endregion

                    DialogBox.Success("Dữ liệu được cập nhật"); 
                }

                LoadData();
            }
            catch(System.Exception ex) { }
            finally
            {
                db.Dispose();
            }
        }
        private void ceCheck_EditValueChanged(object sender, EventArgs e)
        {
            var check = (bool)gvLTT.GetFocusedRowCellValue("IsGui");
            gvLTT.SetFocusedRowCellValue("IsGui", !check);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string ketqua = "";
            List<int?> ListID = new List<int?>();
            for (int i = 0; i < gvLTT.RowCount; i++)
            {
                if ((bool?)gvLTT.GetRowCellValue(i, "IsGui") == true)
                {
                    ListID.Add((int)gvLTT.GetRowCellValue(i, "ID"));
                }
            }
            if (ListID.Count() == 0) return;
            using (var dbo = new MasterDataContext())
            {

                try
                {
                    var Tungay = (DateTime)itemTuNgay.EditValue;
                    foreach (var item in ListID)
                    {
                        var KT = dbo.dvHoaDons.Where(p => p.LinkID == item & p.TableName == "ctLichThanhToan"
                                & p.NgayTT.Value.Month == Tungay.Month & p.NgayTT.Value.Year == Tungay.Year
                                & p.MaTN == Convert.ToInt32(itemToaNha.EditValue)).FirstOrDefault();
                        if (KT == null)
                        {
                            Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.dvHoaDon_InsertAllLTT1", new
                            {
                                ID  = item,
                                MaTN = (byte?)itemToaNha.EditValue,
                                Thang = Tungay.Month,
                                @Nam = Tungay.Year,
                                MaNV = Common.User.MaNV
                            });
                        }
                        else
                        {
                            Library.Class.Connect.QueryConnect.QueryData<bool>("dvHoaDonUpdateValue", new
                            {
                                Id = item,
                                PhaiThu = Convert.ToDecimal(gvLTT.GetFocusedRowCellValue("SoTienQD"))
                            });
                        }
                    }
                    
                    DialogBox.Alert("Dữ liệu đã được cập nhật");
                }
                catch (Exception ex)
                {
                    
                    DialogBox.Error("Đã xảy ra lỗi vui long kiểm tra lại");
                }
               
            }
        }
        private void itemCheckAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvLTT.GetSelectedRows();
                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn lịch thanh toán");
                    return;
                }
                foreach (var i in indexs)
                {
                    var check = (bool?)gvLTT.GetRowCellValue(i, "IsGui");
                    gvLTT.SetRowCellValue(i, "IsGui", !check.GetValueOrDefault());
                }
            }
            catch (Exception EX)
            {
                DialogBox.Alert(EX.Message);
            }
        }
        #region class
        public class DataItem
        {
            public int ID { set; get; }
            public int? MaHD { set; get; }
            public int? MaMB { set; get; }
            public string MaSoMB { set; get; }
            public string TenTL { set; get; }
            public string TenKN { set; get; }
            public string TenKH { set; get; }
            public string TenLT { set; get; }
            public string DienGiai { set; get; }
            public string SoHD { set; get; }
            public string TBTDTG { set; get; }
            public int? DotTT { set; get; }
            public DateTime? TuNgay { set; get; }
            public DateTime? DenNgay { set; get; }
            public decimal? SoThang { set; get; }
            public decimal? SoTien { set; get; }
            public decimal? TyGia { set; get; }
            public decimal? SoTienQD { set; get; }
            public decimal? TyGiaHD { set; get; }
            public decimal? TyGiaTT { set; get; }
            public decimal? TyLeTDTG { set; get; }
            public decimal? MucDCTG { set; get; }
            public bool IsCoHoaDon { set; get; }
            public bool IsGui { set; get; }
            public long IdHoaDon { set; get; }

        }
        #endregion

        
    }
}