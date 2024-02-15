using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using LinqToExcel;

namespace DichVu.Nuoc
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();

                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<NuocItem> list = Library.Import.ExcelAuto.ConvertDataTable<NuocItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvNuoc, itemSheet));

                gcNuoc.DataSource = list;

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            grvNuoc.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcNuoc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objCachTinh = new CachTinhCls();
                objCachTinh.MaTN = this.MaTN;

                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 orderby mb.MaSoMB
                                 select new { mb.MaMB, mb.MaSoMB, mb.MaKH }).ToList();

                var ltDongHo = (from dh in db.dvNuocDongHos
                                where dh.MaTN == this.MaTN
                                orderby dh.SoDH
                                select new { dh.ID, SoDH = dh.SoDH.ToLower() }).ToList();
                var lKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == MaTN select new { kh.MaKH, KyHieu = kh.KyHieu.ToLower() }).ToList();

                var ltNuoc = (List<NuocItem>)gcNuoc.DataSource;
                var ltError = new List<NuocItem>();

                foreach (var n in ltNuoc)
                {
                    db = new MasterDataContext();
                    try
                    {
                        if (SqlMethods.DateDiffDay(n.TuNgay, n.DenNgay) <= 0)
                        {
                            n.Error = "Khoảng thời gian không hợp lệ";
                            ltError.Add(n);
                            continue;
                        }

                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB.ToLower() == n.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            n.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objKH = lKhachHang.FirstOrDefault(_ => _.KyHieu == n.MaKhachHang.ToLower());
                        if(objKH == null)
                        {
                            n.Error = "Khách hàng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        int? _MaDH = null;
                        if (!string.IsNullOrEmpty(n.SoDH))
                        {
                            var objDH = ltDongHo.FirstOrDefault(p => p.SoDH == n.SoDH.ToLower());
                            if (objDH == null)
                            {
                                n.Error = "Số đồng hồ không tồn tại trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }

                            _MaDH = objDH.ID;
                        }

                        var count = db.dvNuocs.Where(p => p.MaMB == objMB.MaMB & p.MaKH == objMB.MaKH & p.MaDH == _MaDH & SqlMethods.DateDiffMonth(n.DenNgay, p.DenNgay) == 0).Count();
                        if (count > 0)
                        {
                            n.Error = string.Format("Mặt bằng này đã tồn tại chỉ số nước của tháng {0:MM/yyyy}", n.DenNgay);
                            ltError.Add(n);
                            continue;
                        }

                        var objNuoc = new dvNuoc();
                        objNuoc.MaTN = this.MaTN;
                        objNuoc.MaMB = objMB.MaMB;
                        objNuoc.MaKH = objKH.MaKH;
                        objNuoc.MaDH = _MaDH;
                        objNuoc.TuNgay = n.TuNgay;
                        objNuoc.DenNgay = n.DenNgay;
                        objNuoc.ChiSoCu = n.ChiSoCu;
                        objNuoc.ChiSoMoi = n.ChiSoMoi;
                        objNuoc.HeSo = n.HeSo <= 0 ? 1 : n.HeSo;
                        objNuoc.SoTieuThuDHCu = n.SoTieuThuDHCu;
                        objNuoc.SoTieuThu = (objNuoc.ChiSoMoi - objNuoc.ChiSoCu) * objNuoc.HeSo;
                        objNuoc.SoTieuThuDHCu = n.SoTieuThuDHCu.GetValueOrDefault();

                        objNuoc.NgayTT = n.NgayTT;
                        //if (n.DenNgay.Value.Day >= 15) objNuoc.NgayTB = new DateTime(n.DenNgay.Value.Year, n.DenNgay.Value.Month, 1);
                        //else objNuoc.NgayTB = new DateTime(n.TuNgay.Value.Year, n.TuNgay.Value.Month, 1);
                        objNuoc.NgayTB = new DateTime(n.Nam, n.Thang, 1);

                        #region Tinh tien
                        objCachTinh.MaMB = objNuoc.MaMB.Value;
                        objCachTinh.LoadDinhMuc();
                        if (n.SoTieuThuDHCu.GetValueOrDefault() == 0)
                        {
                            objCachTinh.SoTieuThu = objNuoc.SoTieuThu.Value;
                        }
                        else
                        {
                            objCachTinh.SoTieuThu = objNuoc.SoTieuThu.Value + objNuoc.SoTieuThuDHCu.Value;
                        }
                        objCachTinh.SoUuDai = n.SoM3UD ?? 0;
                        objCachTinh.XuLy();

                        foreach (var ct in objCachTinh.ltChiTiet)
                        {
                            if (ct.SoLuong.Value > 0)
                            {
                                var objCT = new dvNuocChiTiet();
                                objCT.MaDM = ct.MaDM;
                                objCT.SoLuong = ct.SoLuong;
                                objCT.DonGia = ct.DonGia;
                                objCT.ThanhTien = ct.ThanhTien;
                                objCT.DienGiai = ct.DienGiai;
                                objNuoc.dvNuocChiTiets.Add(objCT);
                            }
                        }

                        objNuoc.ThanhTien = objCachTinh.GetThanhTien();

                        if (n.TienTruocThue > 0) objNuoc.ThanhTien = n.TienTruocThue;

                        objNuoc.TyLeVAT = n.TyLeVAT;
                        if (n.TienVAT > 0) objNuoc.TienVAT = n.TienVAT;
                        else objNuoc.TienVAT = Math.Round((decimal)(objNuoc.ThanhTien * objNuoc.TyLeVAT), 0, MidpointRounding.AwayFromZero);
                        objNuoc.TyLeBVMT = n.TyLeBVMT;
                        if (n.TienBVMT > 0) objNuoc.TienBVMT = n.TienBVMT;
                        else
                            objNuoc.TienBVMT = Math.Round((decimal)(objNuoc.ThanhTien * objNuoc.TyLeBVMT), 0, MidpointRounding.AwayFromZero);//objNuoc.TyLeBVMT * objNuoc.ThanhTien;
                        if (n.SoTien > 0)
                            objNuoc.TienTT = n.SoTien;
                        else
                            objNuoc.TienTT = objNuoc.ThanhTien + objNuoc.TienVAT + objNuoc.TienBVMT;

                        #endregion

                        objNuoc.SoNguoiUD = n.SoNguoiUD;
                        objNuoc.SoM3UD1Nguoi = n.SoM3UD1Nguoi;
                        objNuoc.SoM3UD = n.SoM3UD;

                        objNuoc.DienGiai = n.DienGiai;
                        objNuoc.NgayNhap = db.GetSystemDate();
                        objNuoc.MaNVN = Common.User.MaNV;

                        db.dvNuocs.InsertOnSubmit(objNuoc);
                        db.SubmitChanges();


                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcNuoc.DataSource = ltError;
                }
                else
                {
                    gcNuoc.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
                db.Dispose();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcNuoc);
        }

    }

    public class NuocItem
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaSoMB { get; set; }
        public string SoDH { get; set; }
        public DateTime? NgayTT { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? ChiSoCu { get; set; }
        public int? ChiSoMoi { get; set; }
        public int? HeSo { get; set; }
        public int? SoTieuThu { get; set; }
        public decimal? TienTruocThue { get; set; }
        public decimal? TyLeVAT { get; set; }
        public decimal? TienVAT { get; set; }
        public decimal? TyLeBVMT { get; set; }
        public decimal? TienBVMT { get; set; }
        public decimal? SoTien { get; set; }
        public int? SoTieuThuDHCu { get; set; }
        public int? SoNguoiUD { get; set; }
        public int? SoM3UD1Nguoi { get; set; }
        public int? SoM3UD { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
        public string MaKhachHang { get; set; }
    }
}