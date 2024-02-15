using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.IO;

namespace Library
{
    public partial class frmKyBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        List<int> GetListNV = new List<int>();
        public frmKyBaoCao()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmKyBaoCao_Load(object sender, EventArgs e)
        {
            LoadKBC();
            var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
            if (GetNhomOfNV.Count > 0)
            {
                GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();
            }
        }

        void LoadKBC()
        {
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKBC.Properties.Items.Add(str);
            }
            cbbKBC.EditValue = objKBC.Source[7];
            SetDate(7);
        }

        private void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            DateTuNgay.EditValueChanged -= new EventHandler(DateTuNgay_EditValueChanged);
            DateTuNgay.EditValue = objKBC.DateFrom;
            DateDenNgay.EditValue = objKBC.DateTo;
            DateDenNgay.EditValueChanged += new EventHandler(DateDenNgay_EditValueChanged);
        }

        private void DateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void DateDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cbbKBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (DateDenNgay.EditValue !=null & DateTuNgay.EditValue!=null)
            {
                DateTime tungay = DateTuNgay.DateTime;
                DateTime denngay = DateDenNgay.DateTime;

                //khoi tao cac doi tuong Com Excel de lam viec
                //var xlApp = new Excel.ApplicationClass();
                Excel.Application xlApp = new Excel.Application();
                Excel.Worksheet xlSheet;
                Excel.Workbook xlBook;
                //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
                object missValue = System.Reflection.Missing.Value;
                //khoi tao doi tuong Com Excel moi
                xlBook = xlApp.Workbooks.Add(missValue);
                xlApp.Visible = false;
                int socot;
                int sohang;
                Excel.Range caption;
                Excel.Range header;
                DataTable dt;
                xlBook.Worksheets.Add(missValue, missValue, missValue, missValue);
                xlBook.Worksheets.Add(missValue, missValue, missValue, missValue);
                xlBook.Worksheets.Add(missValue, missValue, missValue, missValue);
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "Excel file (*.xls)|*.xls";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    var wait = DialogBox.WaitingForm();

                    #region Thue Hop Dong
                    dt = new DataTable();

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        var ts = db.thueCongNos
                            .Where(p => SqlMethods.DateDiffDay(tungay, p.ChuKyMax.Value) >= 0
                                & SqlMethods.DateDiffDay(p.ChuKyMax.Value, denngay) >= 0)
                            .OrderBy(p => p.thueHopDong.mbMatBang.MaSoMB)
                            .Select(p => new
                            {
                                p.MaCN,
                                p.thueHopDong.SoHD,
                                NgayHD = p.thueHopDong.NgayHD.Value.ToShortDateString(),
                                NgayBG = p.thueHopDong.NgayBG.Value.ToShortDateString(),
                                KhachHang = p.thueHopDong.MaKH.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.Value ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen) : "") : "",
                                MatBang = p.thueHopDong.mbMatBang.MaSoMB,
                                p.thueHopDong.ChuKyThanhToan,
                                ChuKy = string.Format("{0}-{1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                                p.ConNo,
                                p.DaThanhToan
                            });

                        dt = SqlCommon.LINQToDataTable(ts);
                    }
                    else
                    {
                        if (GetListNV.Count > 0)
                        {
                            var ts = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay(tungay, p.ChuKyMax.Value) >= 0
                                    & SqlMethods.DateDiffDay(p.ChuKyMax.Value, denngay) >= 0 &
                                    GetListNV.Contains(p.thueHopDong.tnNhanVien.MaNV))
                                .OrderBy(p => p.thueHopDong.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.MaCN,
                                    p.thueHopDong.SoHD,
                                    NgayHD = p.thueHopDong.NgayHD.Value.ToShortDateString(),
                                    NgayBG = p.thueHopDong.NgayBG.Value.ToShortDateString(),
                                    KhachHang = p.thueHopDong.MaKH.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.Value ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen) : "") : "",
                                    MatBang = p.thueHopDong.mbMatBang.MaSoMB,
                                    p.thueHopDong.ChuKyThanhToan,
                                    ChuKy = string.Format("{0}-{1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                                    p.ConNo,
                                    p.DaThanhToan
                                });

                            dt = SqlCommon.LINQToDataTable(ts);
                        }
                        else
                        {
                            var ts = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay(tungay, p.ChuKyMax.Value) >= 0
                                    & SqlMethods.DateDiffDay(p.ChuKyMax.Value, denngay) >= 0 &
                                    p.thueHopDong.MaNV == objnhanvien.MaNV)
                                .OrderBy(p => p.thueHopDong.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.MaCN,
                                    p.thueHopDong.SoHD,
                                    NgayHD = p.thueHopDong.NgayHD.Value.ToShortDateString(),
                                    NgayBG = p.thueHopDong.NgayBG.Value.ToShortDateString(),
                                    KhachHang = p.thueHopDong.MaKH.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.Value ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen) : "") : "",
                                    MatBang = p.thueHopDong.mbMatBang.MaSoMB,
                                    p.thueHopDong.ChuKyThanhToan,
                                    ChuKy = string.Format("{0}-{1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                                    p.ConNo,
                                    p.DaThanhToan
                                });

                            dt = SqlCommon.LINQToDataTable(ts);
                        }
                    }
                    socot = dt.Columns.Count;
                    sohang = dt.Rows.Count;

                    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
                    xlSheet.Name = "Thuê hợp đồng";
                    xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                    caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                    caption.Select();
                    caption.FormulaR1C1 = "Thuê hợp đồng";
                    caption.HorizontalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Bold = true;
                    caption.VerticalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Size = 15;
                    caption.Interior.ColorIndex = 20;
                    caption.RowHeight = 30;
                    header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  
                    header.Select();

                    header.HorizontalAlignment = Excel.Constants.xlCenter;
                    header.Font.Bold = true;
                    header.Font.Size = 10;
                    //điền tiêu đề cho các cột trong file excel
                    for (int i = 0; i < socot; i++)
                        xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                    //dien cot stt
                    xlSheet.Cells[2, 1] = "STT";
                    for (int i = 0; i < sohang; i++)
                        xlSheet.Cells[i + 3, 1] = i + 1;
                    //dien du lieu vao sheet
                    for (int i = 0; i < sohang; i++)
                        for (int j = 0; j < socot; j++)
                        {
                            xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                        }
                    //autofit độ rộng cho các cột 
                    for (int i = 0; i < sohang; i++)
                        ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();
                    #endregion

                    #region Dien
                    dt = new DataTable();
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        var ts2 = db.dvdnDiens
                                .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayNhap) >= 0
                                    & SqlMethods.DateDiffDay(p.NgayNhap, denngay) >= 0)
                                .OrderBy(p => p.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.mbMatBang.MaSoMB,
                                    KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                                    p.ChiSoCu,
                                    p.ChiSoMoi,
                                    p.SoTieuThu,
                                    p.TienDien,
                                    p.ConNo,
                                    p.DaThanhToan,
                                    Thang = string.Format("{0} / {1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year),
                                    ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                    TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                });

                        dt = SqlCommon.LINQToDataTable(ts2);
                    }
                    else
                    {
                        if (GetListNV.Count > 0)
                        {
                            var ts2 = db.dvdnDiens
                                    .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayNhap) >= 0
                                        & SqlMethods.DateDiffDay(p.NgayNhap, denngay) >= 0 &
                                        GetListNV.Contains(p.tnNhanVien.MaNV))
                                    .OrderBy(p => p.mbMatBang.MaSoMB)
                                    .Select(p => new
                                    {
                                        p.mbMatBang.MaSoMB,
                                        KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                                        p.ChiSoCu,
                                        p.ChiSoMoi,
                                        p.SoTieuThu,
                                        p.TienDien,
                                        p.ConNo,
                                        p.DaThanhToan,
                                        Thang = string.Format("{0} / {1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year),
                                        ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                        TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                    });

                            dt = SqlCommon.LINQToDataTable(ts2);
                        }
                        else
                        {
                            var ts2 = db.dvdnDiens
                                    .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayNhap) >= 0
                                        & SqlMethods.DateDiffDay(p.NgayNhap, denngay) >= 0 &
                                        p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                                    .OrderBy(p => p.mbMatBang.MaSoMB)
                                    .Select(p => new
                                    {
                                        p.mbMatBang.MaSoMB,
                                        KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                                        p.ChiSoCu,
                                        p.ChiSoMoi,
                                        p.SoTieuThu,
                                        p.TienDien,
                                        p.ConNo,
                                        p.DaThanhToan,
                                        Thang = string.Format("{0} / {1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year),
                                        ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                        TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                    });

                            dt = SqlCommon.LINQToDataTable(ts2);
                        }
                    }
                    socot = dt.Columns.Count;
                    sohang = dt.Rows.Count;


                    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(2);
                    xlSheet.Name = "Điện";
                    xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                    caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                    //caption.Select();
                    caption.FormulaR1C1 = "Điện";
                    caption.HorizontalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Bold = true;
                    caption.VerticalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Size = 15;
                    caption.Interior.ColorIndex = 20;
                    caption.RowHeight = 30;
                    header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  
                    //header.Select();

                    header.HorizontalAlignment = Excel.Constants.xlCenter;
                    header.Font.Bold = true;
                    header.Font.Size = 10;
                    //điền tiêu đề cho các cột trong file excel
                    for (int i = 0; i < socot; i++)
                        xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                    //dien cot stt
                    xlSheet.Cells[2, 1] = "STT";
                    for (int i = 0; i < sohang; i++)
                        xlSheet.Cells[i + 3, 1] = i + 1;
                    //dien du lieu vao sheet
                    for (int i = 0; i < sohang; i++)
                        for (int j = 0; j < socot; j++)
                        {
                            xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                        }
                    //autofit độ rộng cho các cột 
                    for (int i = 0; i < sohang; i++)
                        ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();
                    #endregion

                    #region Nuoc
                    dt = new DataTable();
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        var nuoc = db.dvdnNuocs
                                .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayNhap) >= 0
                                    & SqlMethods.DateDiffDay(p.NgayNhap, denngay) >= 0)
                                .OrderBy(p => p.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.mbMatBang.MaSoMB,
                                    KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                                    p.ChiSoCu,
                                    p.ChiSoMoi,
                                    p.SoTieuThu,
                                    p.TienNuoc,
                                    p.ConNo,
                                    p.DaThanhToan,
                                    Thang = string.Format("{0} / {1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year),
                                    ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                    TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                });

                        dt = SqlCommon.LINQToDataTable(nuoc);
                    }
                    else
                    {
                        if (GetListNV.Count > 0)
                        {
                            var nuoc = db.dvdnNuocs
                                    .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayNhap) >= 0
                                        & SqlMethods.DateDiffDay(p.NgayNhap, denngay) >= 0 &
                                        GetListNV.Contains(p.tnNhanVien.MaNV))
                                    .OrderBy(p => p.mbMatBang.MaSoMB)
                                    .Select(p => new
                                    {
                                        p.mbMatBang.MaSoMB,
                                        KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                                        p.ChiSoCu,
                                        p.ChiSoMoi,
                                        p.SoTieuThu,
                                        p.TienNuoc,
                                        p.ConNo,
                                        p.DaThanhToan,
                                        Thang = string.Format("{0} / {1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year),
                                        ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                        TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                    });

                            dt = SqlCommon.LINQToDataTable(nuoc);
                        }
                        else
                        {
                            var nuoc = db.dvdnNuocs
                                    .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayNhap) >= 0
                                        & SqlMethods.DateDiffDay(p.NgayNhap, denngay) >= 0 &
                                        p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                                    .OrderBy(p => p.mbMatBang.MaSoMB)
                                    .Select(p => new
                                    {
                                        p.mbMatBang.MaSoMB,
                                        KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                                        p.ChiSoCu,
                                        p.ChiSoMoi,
                                        p.SoTieuThu,
                                        p.TienNuoc,
                                        p.ConNo,
                                        p.DaThanhToan,
                                        Thang = string.Format("{0} / {1}", p.NgayNhap.Value.Month, p.NgayNhap.Value.Year),
                                        ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                        TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                    });

                            dt = SqlCommon.LINQToDataTable(nuoc);
                        }
                    }
                    socot = dt.Columns.Count;
                    sohang = dt.Rows.Count;


                    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(3);
                    xlSheet.Name = "Nước";
                    xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                    caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                    caption.FormulaR1C1 = "Nước";
                    caption.HorizontalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Bold = true;
                    caption.VerticalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Size = 15;
                    caption.Interior.ColorIndex = 20;
                    caption.RowHeight = 30;
                    header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  

                    header.HorizontalAlignment = Excel.Constants.xlCenter;
                    header.Font.Bold = true;
                    header.Font.Size = 10;
                    //điền tiêu đề cho các cột trong file excel
                    for (int i = 0; i < socot; i++)
                        xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                    //dien cot stt
                    xlSheet.Cells[2, 1] = "STT";
                    for (int i = 0; i < sohang; i++)
                        xlSheet.Cells[i + 3, 1] = i + 1;
                    //dien du lieu vao sheet
                    for (int i = 0; i < sohang; i++)
                        for (int j = 0; j < socot; j++)
                        {
                            xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                        }
                    //autofit độ rộng cho các cột 
                    for (int i = 0; i < sohang; i++)
                        ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();
                    #endregion

                    #region The xe
                    dt = new DataTable();
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                    }
                    else
                    {
                        if (GetListNV.Count > 0)
                        {
                            //var tx = db.dvgxTheXeThanhToans
                            //        .Where(p => SqlMethods.DateDiffDay(tungay, p.ThangThanhToan) >= 0
                            //            & SqlMethods.DateDiffDay(p.ThangThanhToan, denngay) >= 0 &
                            //            GetListNV.Contains(p.dvgxTheXe.tnNhanVien.MaNV))
                            //        .OrderBy(p => p.dvgxTheXe.mbMatBang.MaSoMB)
                            //        .Select(p => new
                            //        {
                            //            p.dvgxTheXe.mbMatBang.MaSoMB,
                            //            KhachHang = p.dvgxTheXe.tnNhanKhau.HoTenNK,
                            //            p.dvgxTheXe.ChuThe,
                            //            p.dvgxTheXe.BienSo,
                            //            p.dvgxTheXe.dvgxLoaiXe.TenLX,
                            //            p.dvgxTheXe.dvgxLoaiXe.PhiGiuXe,
                            //            p.dvgxTheXe.dvgxLoaiXe.PhiLamThe,
                            //            Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                            //            ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                            //            TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                            //        });

                            //dt = SqlCommon.LINQToDataTable(tx);
                        }
                        else
                        {
                        }
                    }
                    socot = dt.Columns.Count;
                    sohang = dt.Rows.Count;

                    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(4);
                    xlSheet.Name = "Thẻ xe";
                    xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                    caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                    caption.FormulaR1C1 = "Thẻ xe";
                    caption.HorizontalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Bold = true;
                    caption.VerticalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Size = 15;
                    caption.Interior.ColorIndex = 20;
                    caption.RowHeight = 30;
                    header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  

                    header.HorizontalAlignment = Excel.Constants.xlCenter;
                    header.Font.Bold = true;
                    header.Font.Size = 10;
                    //điền tiêu đề cho các cột trong file excel
                    for (int i = 0; i < socot; i++)
                        xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                    //dien cot stt
                    xlSheet.Cells[2, 1] = "STT";
                    for (int i = 0; i < sohang; i++)
                        xlSheet.Cells[i + 3, 1] = i + 1;
                    //dien du lieu vao sheet
                    for (int i = 0; i < sohang; i++)
                        for (int j = 0; j < socot; j++)
                        {
                            xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                        }
                    //autofit độ rộng cho các cột 
                    for (int i = 0; i < sohang; i++)
                        ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();
                    #endregion

                    #region Thang may
                    dt = new DataTable();
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        var tm = db.dvtmThanhToanThangMays
                                .Where(p => SqlMethods.DateDiffDay(tungay, p.ThangThanhToan) >= 0
                                    & SqlMethods.DateDiffDay(p.ThangThanhToan, denngay) >= 0)
                                .OrderBy(p => p.dvtmTheThangMay.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.dvtmTheThangMay.mbMatBang.MaSoMB,
                                    KhachHang = p.dvtmTheThangMay.MaKH.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? p.dvtmTheThangMay.tnKhachHang.HoKH + " " + p.dvtmTheThangMay.tnKhachHang.TenKH : p.dvtmTheThangMay.tnKhachHang.CtyTen) : "") : "",
                                    p.dvtmTheThangMay.ChuThe,
                                    p.dvtmTheThangMay.SoThe,
                                    p.dvtmTheThangMay.PhiLamThe,
                                    Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                                    ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                    TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                });

                        dt = SqlCommon.LINQToDataTable(tm);
                    }
                    else
                    {
                        if (GetListNV.Count > 0)
                        {
                            var tm = db.dvtmThanhToanThangMays
                                       .Where(p => SqlMethods.DateDiffDay(tungay, p.ThangThanhToan) >= 0
                                           & SqlMethods.DateDiffDay(p.ThangThanhToan, denngay) >= 0 &
                                           GetListNV.Contains(p.dvtmTheThangMay.tnNhanVien.MaNV))
                                       .OrderBy(p => p.dvtmTheThangMay.mbMatBang.MaSoMB)
                                       .Select(p => new
                                       {
                                           p.dvtmTheThangMay.mbMatBang.MaSoMB,
                                           KhachHang = p.dvtmTheThangMay.MaKH.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? p.dvtmTheThangMay.tnKhachHang.HoKH + " " + p.dvtmTheThangMay.tnKhachHang.TenKH : p.dvtmTheThangMay.tnKhachHang.CtyTen) : "") : "",
                                           p.dvtmTheThangMay.ChuThe,
                                           p.dvtmTheThangMay.SoThe,
                                           p.dvtmTheThangMay.PhiLamThe,
                                           Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                                           ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                           TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                       });

                            dt = SqlCommon.LINQToDataTable(tm);
                        }
                        else
                        {
                            var tm = db.dvtmThanhToanThangMays
                                    .Where(p => SqlMethods.DateDiffDay(tungay, p.ThangThanhToan) >= 0
                                        & SqlMethods.DateDiffDay(p.ThangThanhToan, denngay) >= 0 &
                                        p.dvtmTheThangMay.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                                    .OrderBy(p => p.dvtmTheThangMay.mbMatBang.MaSoMB)
                                    .Select(p => new
                                    {
                                        p.dvtmTheThangMay.mbMatBang.MaSoMB,
                                        KhachHang = p.dvtmTheThangMay.MaKH.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? p.dvtmTheThangMay.tnKhachHang.HoKH + " " + p.dvtmTheThangMay.tnKhachHang.TenKH : p.dvtmTheThangMay.tnKhachHang.CtyTen) : "") : "",
                                        p.dvtmTheThangMay.ChuThe,
                                        p.dvtmTheThangMay.SoThe,
                                        p.dvtmTheThangMay.PhiLamThe,
                                        Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                                        ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                        TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                    });

                            dt = SqlCommon.LINQToDataTable(tm);
                        }
                    }
                    socot = dt.Columns.Count;
                    sohang = dt.Rows.Count;

                    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(5);
                    xlSheet.Name = "Thang máy";
                    xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                    caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                    caption.FormulaR1C1 = "Thang máy";
                    caption.HorizontalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Bold = true;
                    caption.VerticalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Size = 15;
                    caption.Interior.ColorIndex = 20;
                    caption.RowHeight = 30;
                    header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  

                    header.HorizontalAlignment = Excel.Constants.xlCenter;
                    header.Font.Bold = true;
                    header.Font.Size = 10;
                    //điền tiêu đề cho các cột trong file excel
                    for (int i = 0; i < socot; i++)
                        xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                    //dien cot stt
                    xlSheet.Cells[2, 1] = "STT";
                    for (int i = 0; i < sohang; i++)
                        xlSheet.Cells[i + 3, 1] = i + 1;
                    //dien du lieu vao sheet
                    for (int i = 0; i < sohang; i++)
                        for (int j = 0; j < socot; j++)
                        {
                            xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                        }
                    //autofit độ rộng cho các cột 
                    for (int i = 0; i < sohang; i++)
                        ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();
                    #endregion

                    #region dvk
                    dt = new DataTable();
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        var dvkk = db.dvkDichVuThanhToans
                                .Where(p => SqlMethods.DateDiffDay(tungay, p.ThangThanhToan) >= 0
                                    & SqlMethods.DateDiffDay(p.ThangThanhToan, denngay) >= 0)
                                .Select(p => new
                                {
                                    Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                                    ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                    TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                });

                        dt = SqlCommon.LINQToDataTable(dvkk);
                    }
                    else
                    {
                        if (GetListNV.Count > 0)
                        {

                        }
                        else
                        {
                        }
                    }
                    socot = dt.Columns.Count;
                    sohang = dt.Rows.Count;

                    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(6);
                    xlSheet.Name = "Dịch vụ khác";
                    xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                    caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                    caption.FormulaR1C1 = "Dịch vụ khác";
                    caption.HorizontalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Bold = true;
                    caption.VerticalAlignment = Excel.Constants.xlCenter;
                    caption.Font.Size = 15;
                    caption.Interior.ColorIndex = 20;
                    caption.RowHeight = 30;
                    header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  

                    header.HorizontalAlignment = Excel.Constants.xlCenter;
                    header.Font.Bold = true;
                    header.Font.Size = 10;
                    //điền tiêu đề cho các cột trong file excel
                    for (int i = 0; i < socot; i++)
                        xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                    //dien cot stt
                    xlSheet.Cells[2, 1] = "STT";
                    for (int i = 0; i < sohang; i++)
                        xlSheet.Cells[i + 3, 1] = i + 1;
                    //dien du lieu vao sheet
                    for (int i = 0; i < sohang; i++)
                        for (int j = 0; j < socot; j++)
                        {
                            xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                        }
                    //autofit độ rộng cho các cột 
                    for (int i = 0; i < sohang; i++)
                        ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();
                    #endregion

                    #region save
                    //save file
                    xlBook.SaveAs(f.FileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                    xlBook.Close(true, missValue, missValue);
                    xlApp.Quit();

                    // release cac doi tuong COM
                    ExportToExcel.releaseObject(xlSheet);
                    ExportToExcel.releaseObject(xlBook);
                    ExportToExcel.releaseObject(xlApp);
                    #endregion

                    wait.Close();
                    wait.Dispose();
                    if (f.FileName.Trim().Length != 0)
                    {
                        DialogBox.Alert("Export thành công");  
                        if (DialogBox.Question("Bạn có muốn mở file này không?") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(f.FileName);
                        }
                    }
                    
                    this.Close();
                }

            }
        }
    }
}