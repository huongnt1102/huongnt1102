using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Globalization;

namespace TaiSan
{
    public partial class frmTaiSanSuDung : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmTaiSanSuDung()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gctaisan.DataSource = db.tsTaiSanSuDungs;
                lookNCCts.DataSource = db.tnNhaCungCaps
                    .Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
                lookTrangThaits.DataSource = db.tsTrangThais;
                lookHSXts.DataSource = db.tsHangSanXuats;
                lookxxts.DataSource = db.tsXuatXus;
                lookMatBang.DataSource = db.mbMatBangs.Select(p=> new 
                {
                    p.MaMB,
                    p.MaSoMB,
                    p.mbLoaiMatBang.TenLMB,
                    p.mbTangLau.mbKhoiNha.tnToaNha.TenTN
                });
            }
            else
            {
                gctaisan.DataSource = db.tsTaiSanSuDungs.Where(p => p.MaTN == objnhanvien.MaTN);
                lookNCCts.DataSource = db.tnNhaCungCaps
                    //.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
                lookTrangThaits.DataSource = db.tsTrangThais.Where(p => p.MaTN == objnhanvien.MaTN);
                lookHSXts.DataSource = db.tsHangSanXuats.Where(p => p.MaTN == objnhanvien.MaTN);
                lookxxts.DataSource = db.tsXuatXus.Where(p => p.MaTN == objnhanvien.MaTN);
                lookMatBang.DataSource = db.mbMatBangs.Where(p => p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaSoMB,
                        p.mbLoaiMatBang.TenLMB
                    });
            }
            colLTS.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objnhanvien);
        }

        private void frmTaiSanSuDung_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvtaisan.UpdateCurrentRow();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
                return;
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvtaisan.AddNewRow();
        }


        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvtaisan.DeleteSelectedRows();
        }

        private void grvtaisan_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }
        string KyHieu;
        private void grvtaisan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            db.tsDangDung_getNewMaTSDD(ref KyHieu);
            grvtaisan.SetRowCellValue(e.RowHandle, colMaTN, objnhanvien.MaTN);
            grvtaisan.SetRowCellValue(e.RowHandle, colKyHieu, db.DinhDang(16,int.Parse(KyHieu)));
        }

        private void frmTaiSanSuDung_SizeChanged(object sender, EventArgs e)
        {
            
        }

        int TotalMonths(DateTime start, DateTime end)
        {
            return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
        }

        private void grvtaisan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (grvtaisan.FocusedRowHandle < 0) return;

                int MaTS = (int)grvtaisan.GetFocusedRowCellValue("MaTS");
                var objts = db.tsTaiSanSuDungs.Single(p => p.MaTS == MaTS);

                var TiLeKhauHao = objts.tsLoaiTaiSan.TiLeKhauHao ?? 0;
                decimal GiaTriTaiSan = objts.GiaTriTaiSan ?? 0;
                if (objts.NgaySD != null & objts.NgaySD.Value <= db.GetSystemDate())
                {
                    int datediff = TotalMonths(db.GetSystemDate(), objts.NgaySD.Value);
                    for (int i = 0; i < datediff; i++)
                    {
                        GiaTriTaiSan -= GiaTriTaiSan * TiLeKhauHao;
                    }
                }
               
                htmlEditorControl1.InnerHtml = "Ghi chú: " + objts.DienGiai
                    + "<br>" + "Tỉ lệ khấu hao: " + TiLeKhauHao.ToString("F", CultureInfo.InvariantCulture)
                    + "<br>Giá trị nguyên thủy: " + (objts.GiaTriTaiSan ?? 0).ToString("C")
                    + "<br>Giá trị còn lại: " + GiaTriTaiSan.ToString("C");
                LoadLSSD(objts);
                if (grvtaisan.GetFocusedRowCellValue("MaMB") != null) 
                    LoadMBDSD((int)grvtaisan.GetFocusedRowCellValue("MaMB")); 
                else 
                    vgcmb.DataSource = null;

                LoadLichSuBaoTri(MaTS);
            }
            catch
            {
            }
        }

        private void LoadLichSuBaoTri(int MaTS)
        {
            gclsbt.DataSource = db.tsLichSuBaoTriSuaChuas.Where(p => p.MaTSSD == MaTS)
                .Select(p => new
                {
                    p.tnNhanVien.HoTenNV,
                    p.tsTrangThai.TenTT,
                    p.NgayBaoTriSuaChua,
                    p.DienGiai
                });
        }

        private void LoadMBDSD(int MaMB)
        {
            vgcmb.DataSource = db.mbMatBangs.Where(p => p.MaMB == MaMB)
                .Select(p => new
                {
                    p.MaSoMB,
                    p.mbTangLau.TenTL,
                    p.mbTangLau.mbKhoiNha.TenKN,
                    p.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                    p.mbLoaiMatBang.TenLMB,
                    p.mbTrangThai.TenTT,
                    KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                    p.DienGiai
                });
        }

        private void LoadLSSD(tsTaiSanSuDung ts)
        {
            gcLSSD.DataSource = db.tsLichSuSDs
                .Where(p => p.MaTS == ts.MaTS)
                .OrderBy(p=>p.NgayBatDauSD)
                .Select(p => new
                {
                    p.MaMB,
                    p.mbMatBang.MaSoMB,
                    p.tsTrangThai.TenTT,
                    p.NgayBatDauSD,
                    p.NgayKetThucSD,
                    p.DienGiai
                });
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvtaisan.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn tài sản");
                return;
            }
            LichSuSD.frmChuyenTaiSan frm = new LichSuSD.frmChuyenTaiSan();
            if (grvtaisan.GetFocusedRowCellValue("MaMB") == null)
            {
                frm.objts = db.tsTaiSanSuDungs.Single(p => p.MaTS == (int)grvtaisan.GetFocusedRowCellValue("MaTS"));
                frm.objnhanvien = objnhanvien;
            }
            else
            {
                frm.objts = db.tsTaiSanSuDungs.Single(p => p.MaTS == (int)grvtaisan.GetFocusedRowCellValue("MaTS"));
                frm.objnhanvien = objnhanvien;
                frm.objmbSource = db.mbMatBangs.Single(p => p.MaMB == (int)grvtaisan.GetFocusedRowCellValue("MaMB"));
            }
            frm.ShowDialog();
            if (frm.DialogResult==DialogResult.OK)
            {
                LoadData();
                grvtaisan_FocusedRowChanged(null, null);
            }
        }

        private void itemThuHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(grvtaisan.GetFocusedRowCellValue("MaMB") == null)
            {
                DialogBox.Alert("Tài sản này chưa được sử dụng nên không cần thu hồi");
                return;
            }
            if (DialogBox.QuestionThuHoi() == DialogResult.No) return;

            try
            {
                var maxls = db.tsLichSuSDs.Where(p => p.MaTS == (int)grvtaisan.GetFocusedRowCellValue("MaTS")).Max(p => p.NgayBatDauSD);
                var lssdsource = db.tsLichSuSDs.Single(p => p.MaTS == (int)grvtaisan.GetFocusedRowCellValue("MaTS")
                    & p.NgayBatDauSD == maxls & p.MaMB == (int)grvtaisan.GetFocusedRowCellValue("MaMB"));

                lssdsource.NgayKetThucSD = db.GetSystemDate();
            }
            catch { }
            var objts = db.tsTaiSanSuDungs.Single(p => p.MaTS == (int)grvtaisan.GetFocusedRowCellValue("MaTS"));
            objts.MaMB = null;

            try
            {
                db.SubmitChanges();
                grvtaisan_FocusedRowChanged(null, null);
                DialogBox.Alert("Thu hồi tài sản thành công");
            }
            catch
            {
                DialogBox.Alert("Lưu không thành công");
            }
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                var wait = DialogBox.WaitingForm();
                
                var listImport = book.Worksheet(0).Select(p => new
                {
                    MaTS = p[0].ToString().Trim(),
                    LoaiTS = SearchLTS(p[1].ToString().Trim()),
                    MatBang = SearchMatBang(p[2].ToString().Trim()),
                    TrangThai = SearchTrangThai(p[3].ToString().Trim()),
                    NhaCungCap = SearchNhaCungCap(p[4].ToString().Trim()),
                    HangSX = SearchHSX(p[5].ToString().Trim()),
                    XuatXu = SearchXuatXu(p[6].ToString().Trim()),
                    NgaySX = MyConvert(p[7]),
                    HanSD = MyConvert(p[8]),
                    NgayBDSD = MyConvert(p[9]),
                    ThoiHan = p[10],
                    DienGiai = p[11]
                }).ToList();

                for (int i = 0; i < listImport.Count(); i++)
                {
                    grvtaisan.AddNewRow();
                    grvtaisan.SetFocusedRowCellValue(colMatBang, listImport[i].MatBang);
                    grvtaisan.SetFocusedRowCellValue(colKyHieu, listImport[i].MaTS);
                    grvtaisan.SetFocusedRowCellValue(colLTS, listImport[i].LoaiTS);
                    grvtaisan.SetFocusedRowCellValue(colTrangThai, listImport[i].TrangThai);
                    grvtaisan.SetFocusedRowCellValue(colNCC, listImport[i].NhaCungCap);
                    grvtaisan.SetFocusedRowCellValue(colHSX, listImport[i].HangSX);
                    grvtaisan.SetFocusedRowCellValue(colXuatXu, listImport[i].XuatXu);
                    grvtaisan.SetFocusedRowCellValue(colNgaySX, listImport[i].NgaySX);
                    grvtaisan.SetFocusedRowCellValue(colHSD, listImport[i].HanSD);
                    grvtaisan.SetFocusedRowCellValue(colNBDSD, listImport[i].NgayBDSD);
                    grvtaisan.SetFocusedRowCellValue(colThoiHan, listImport[i].ThoiHan.Cast<int>());
                    grvtaisan.SetFocusedRowCellValue(colDienGiaiTS, listImport[i].DienGiai.Cast<string>());

                }
                grvtaisan.UpdateCurrentRow();
                wait.Close();
                wait.Dispose();
            }
        }

        private int? SearchMatBang(string chuoi)
        {
            try
            {
                return db.mbMatBangs.FirstOrDefault(p => SqlMethods.Like(p.MaSoMB, "%" + chuoi + "%")).MaMB;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchTrangThai(string chuoi)
        {
            try
            {
                return db.tsTrangThais.FirstOrDefault(p => SqlMethods.Like(p.TenTT, "%" + chuoi + "%")).MaTT;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchNhaCungCap(string chuoi)
        {
            try
            {
                return db.tnNhaCungCaps.FirstOrDefault(p => SqlMethods.Like(p.TenNCC, "%" + chuoi + "%")).MaNCC;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchXuatXu(string chuoi)
        {
            try
            {
                return db.tsXuatXus.FirstOrDefault(p => SqlMethods.Like(p.TenXX, "%" + chuoi + "%")).MaXX;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchLTS(string chuoi)
        {
            try
            {
                return db.tsLoaiTaiSans.FirstOrDefault(p => SqlMethods.Like(p.TenLTS, "%" + chuoi + "%")).MaLTS;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchHSX(string chuoi)
        {
            try
            {
                return db.tsHangSanXuats.FirstOrDefault(p => SqlMethods.Like(p.TenHSX, "%" + chuoi + "%")).MaHSX;
            }
            catch
            {
                return null;
            }
        }

        private DateTime? MyConvert(LinqToExcel.Cell value)
        {
            try
            {
                return value.Cast<DateTime>();
            }
            catch
            {
                return null;
            }
        }
    }
}
