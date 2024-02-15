using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;
using LinqToExcel;

namespace DichVu.Dien.DieuHoa
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
                System.Collections.Generic.List<DienDHItem> list = Library.Import.ExcelAuto.ConvertDataTable<DienDHItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                gc.DataSource = list;

                //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new DienDHItem
                //{
                //    Thang = p["Tháng"].Cast<int>(),
                //    Nam = p["Năm"].Cast<int>(),
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    SoDH = p["Đồng hồ"].ToString().Trim(),
                //    NgayTT = p["Ngày TT"].Cast<DateTime>(),
                //    TuNgay = p["Từ ngày"].Cast<DateTime>(),
                //    DenNgay = p["Đến ngày"].Cast<DateTime>(),
                //    ChiSoCu = p["Chỉ số cũ"].Cast<int>(),
                //    ChiSoMoi = p["Chỉ số mới"].Cast<int>(),
                //    HeSo = p["Hệ số"].Cast<int>(),
                //    SoTieuThu = p["Số tiêu thụ"].Cast<decimal>(),
                //    TyLeVAT = p["Tỷ lệ VAT"].Cast<decimal>(),
                //    SoTien = p["Số tiền"].Cast<decimal>(),
                //    DienGiai = p["Diễn giải"].ToString().Trim()
                //}).ToList();

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
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
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
                                 select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower(), mb.MaKH }).ToList();

                var ltDongHo = (from dh in db.dvDienDH_DongHos
                                where dh.MaTN == this.MaTN
                                orderby dh.SoDH
                                select new { dh.ID, SoDH = dh.SoDH.ToLower() }).ToList();


                var ltDien = (List<DienDHItem>)gc.DataSource;
                var ltError = new List<DienDHItem>();

                foreach(var n in ltDien)
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

                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB == n.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            n.Error = "Mặt bằng không tồn tại trong hệ thống";
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

                        var count = db.dvDienDHs.Where(p =>p.MaMB == objMB.MaMB & p.MaKH == objMB.MaKH & p.MaDH == _MaDH & SqlMethods.DateDiffMonth(n.DenNgay, p.DenNgay) == 0).Count();
                        if (count > 0)
                        {
                            n.Error = string.Format("Mặt bằng này đã tồn tại chỉ số điện của tháng {0:MM/yyyy}", n.DenNgay);
                            ltError.Add(n);
                            continue;
                        }
                        if (SqlMethods.DateDiffDay(n.TuNgay, n.NgayTT) <= 0)
                        {
                            n.Error = "Khoảng thời gian không hợp lệ";
                            ltError.Add(n);
                            continue;
                        }

                        var objDien = new dvDienDH();
                        objDien.MaTN = this.MaTN;
                        objDien.MaMB = objMB.MaMB;
                        objDien.MaKH = objMB.MaKH;
                        objDien.MaDH = _MaDH;
                        objDien.NgayTT = n.NgayTT;
                        objDien.TuNgay = n.TuNgay;
                        objDien.DenNgay = n.DenNgay;
                        objDien.ChiSoCu = n.ChiSoCu;
                        objDien.ChiSoMoi = n.ChiSoMoi;
                        objDien.HeSo = n.HeSo <= 0 ? 1 : n.HeSo;
                        objDien.SoTieuThu = (objDien.ChiSoMoi - objDien.ChiSoCu) * objDien.HeSo;
                        objDien.SoTieuThu = n.SoTieuThu;

                        #region Tinh dien cho import data moi
                        objCachTinh.MaMB = objDien.MaMB.Value;
                        objCachTinh.LoadDinhMuc();

                        objCachTinh.SoTieuThu = objDien.SoTieuThu;
                        objCachTinh.XuLy();

                        foreach (var ct in objCachTinh.ltChiTiet)
                        {
                            if (ct.SoLuong.Value > 0)
                            {
                                var objCT = new dvDienDH_ChiTiet();
                                objCT.MaDM = ct.MaDM;
                                objCT.SoLuong = ct.SoLuong;
                                objCT.DonGia = ct.DonGia;
                                objCT.ThanhTien = ct.ThanhTien;
                                objCT.DienGiai = ct.DienGiai;
                                objDien.dvDienDH_ChiTiets.Add(objCT);
                            }
                        }

                        objDien.ThanhTien = objCachTinh.GetThanhTien();

                        if (n.TienTruocThue > 0) objDien.ThanhTien = n.TienTruocThue;

                        objDien.TyLeVAT = n.TyLeVAT;
                        if (n.TienVAT > 0) objDien.TienVAT = n.TienVAT;
                        else objDien.TienVAT = objDien.ThanhTien * objDien.TyLeVAT;
                        if (n.SoTien > 0)
                            objDien.TienTT = n.SoTien;
                        else
                            objDien.TienTT = objDien.ThanhTien + objDien.TienVAT;
                        #endregion

                        objDien.DienGiai = n.DienGiai;
                        objDien.NgayNhap = db.GetSystemDate();
                        objDien.MaNVN = Common.User.MaNV;
                        objDien.NgayTB = new DateTime(n.Nam, n.Thang, 1);
                        db.dvDienDHs.InsertOnSubmit(objDien);
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
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
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
            Library.Commoncls.ExportExcel(gc);
        }
    }

    public class DienDHItem
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
        public decimal? SoTieuThu { get; set; }
        public decimal? TyLeVAT { get; set; }
        public decimal? TienVAT { get; set; }
        public decimal? TienTruocThue { get; set; }
        public decimal? SoTien { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}