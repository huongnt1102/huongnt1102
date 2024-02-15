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

namespace DichVu.Nuoc.NuocNong
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

                System.Collections.Generic.List<Library.Import.ExcelAuto.GridviewInfo> lGridViewInfo = Library.Import.ExcelAuto.GetGridviewInfo(grvNuoc);

                var lExcel = excel.Worksheet(itemSheet.EditValue.ToString()).ToList();

                System.Data.DataTable dt = new System.Data.DataTable();
                foreach (var column in lGridViewInfo)
                {
                    dt.Columns.Add(column.FieldName);
                }

                foreach (var row in lExcel)
                {
                    var r = dt.NewRow();
                    foreach (var column in lGridViewInfo)
                    {
                        r[column.FieldName] = row[column.Caption];
                    }
                    dt.Rows.Add(r);
                }

                System.Collections.Generic.List<NuocItem> list = Library.Import.ExcelAuto.ConvertDataTable<NuocItem>(dt);

                gcNuoc.DataSource = list;

                //gcNuoc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new NuocItem
                //{
                //    Thang = p["Tháng"].Cast<int>(),
                //    Nam = p["Năm"].Cast<int>(),
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    SoDH = p["Đồng hồ"].ToString().Trim(),
                //    NgayTT = p["Ngày TT"].Cast<DateTime>(),
                //    TuNgay = p["Từ ngày"].Cast<DateTime>(),
                //    DenNgay = p["Đến ngày"].Cast<DateTime>(),
                //    //DauCapCu = p["DauCapCu"].Cast<int>(),
                //    //DauCapMoi = p["DauCapMoi"].Cast<int>(),
                //   // DauHoiCu = p["DauHoiCu"].Cast<int>(),
                //    //DauHoiMoi = p["DauHoiMoi"].Cast<int>(),
                //    HeSo = p["Hệ số"].Cast<int>(),
                //    SoTieuThu = p["Số tiêu thụ"].Cast<int>(),
                //    TyLeVAT = p["Tỷ lệ VAT"].Cast<decimal>(),
                //    TyLeBVMT = p["Tỷ lệ BVMT"].Cast<decimal>(),
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
                                 select new { mb.MaMB, mb.MaSoMB, mb.MaKH, mb.IsCanHoCaNhan }).ToList();

                var ltDongHo = (from dh in db.dvNuocNongDongHos
                                where dh.MaTN == this.MaTN
                                orderby dh.SoDH
                                select new { dh.ID, SoDH = dh.SoDH.ToLower() }).ToList();

                var ltNuoc = (List<NuocItem>)gcNuoc.DataSource;
                var ltError = new List<NuocItem>();

                foreach(var n in ltNuoc)
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

                        var count = db.dvNuocNongs.Where(p => p.MaMB == objMB.MaMB & p.MaKH == objMB.MaKH & p.MaDH == _MaDH & SqlMethods.DateDiffMonth(n.DenNgay, p.DenNgay) == 0).Count();
                        if (count > 0)
                        {
                            n.Error = string.Format("Mặt bằng này đã tồn tại chỉ số nước của tháng {0:MM/yyyy}", n.DenNgay);
                            ltError.Add(n);
                            continue;
                        }

                        var objNuoc = new dvNuocNong();
                        objNuoc.MaTN = this.MaTN;
                        objNuoc.MaMB = objMB.MaMB;
                        objNuoc.MaKH = objMB.MaKH;
                        objNuoc.MaDH = _MaDH;
                        objNuoc.TuNgay = n.TuNgay;
                        objNuoc.DenNgay = n.DenNgay;
                        objNuoc.NgayTT = n.NgayTT;
                        objNuoc.DauCap_Cu = n.DauCapCu;
                        objNuoc.DauCap_Moi = n.DauCapMoi;
                        objNuoc.DauCap = objNuoc.DauCap_Moi - objNuoc.DauCap_Cu;
                        objNuoc.DauHoi_Cu = n.DauHoiCu;
                        objNuoc.DauHoi_Moi = n.DauHoiMoi;
                        objNuoc.DauHoi = objNuoc.DauHoi_Moi - objNuoc.DauHoi_Cu;
                        objNuoc.HeSo = n.HeSo <= 0 ? 1 : n.HeSo;
                        objNuoc.SoTieuThu = (objNuoc.DauCap - objNuoc.DauHoi) * objNuoc.HeSo;
                        //if (n.DenNgay.Value.Day >= 15) objNuoc.NgayTB = new DateTime(n.DenNgay.Value.Year, n.DenNgay.Value.Month, 1);
                        //else objNuoc.NgayTB = new DateTime(n.TuNgay.Value.Year, n.TuNgay.Value.Month, 1);
                        objNuoc.NgayTB = new DateTime(n.Nam, n.Thang, 1);

                        #region Tinh chi tiet
                        objCachTinh.MaMB = objNuoc.MaMB.Value;
                        objCachTinh.LoadDinhMuc();

                        objCachTinh.SoTieuThu = objNuoc.SoTieuThu.Value;
                        objCachTinh.XuLy();

                        foreach (var ct in objCachTinh.ltChiTiet)
                        {
                            if (ct.SoLuong.Value > 0)
                            {
                                var objCT = new dvNuocNongChiTiet();
                                objCT.MaDM = ct.MaDM;
                                objCT.SoLuong = ct.SoLuong;
                                objCT.DonGia = ct.DonGia;
                                objCT.ThanhTien = ct.ThanhTien;
                                objCT.DienGiai = ct.DienGiai;
                                objNuoc.dvNuocNongChiTiets.Add(objCT);
                            }
                        }

                        objNuoc.ThanhTien = objCachTinh.GetThanhTien();

                        if (n.TienTruocThue > 0) objNuoc.ThanhTien = n.TienTruocThue;

                        objNuoc.TyLeVAT = n.TyLeVAT;
                        if (n.TienVAT > 0) objNuoc.TienVAT = n.TienVAT;
                        else
                        objNuoc.TienVAT = objNuoc.ThanhTien * objNuoc.TyLeVAT;
                        objNuoc.TyLeBVMT = n.TyLeBVMT;
                        if (n.TienBVMT > 0) objNuoc.TienBVMT = n.TienBVMT;
                        else
                        objNuoc.TienBVMT = objNuoc.TyLeBVMT * objNuoc.ThanhTien;
                        if (n.SoTien > 0)
                            objNuoc.TienTT = n.SoTien;
                        else
                            objNuoc.TienTT = objNuoc.ThanhTien + objNuoc.TienVAT + objNuoc.TienBVMT;
                        #endregion

                        objNuoc.DienGiai = n.DienGiai;
                        objNuoc.NgayNhap = db.GetSystemDate();
                        objNuoc.MaNVN = Common.User.MaNV;

                        db.dvNuocNongs.InsertOnSubmit(objNuoc);
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
        public DateTime? NgayTB { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? DauCapCu { get; set; }
        public int? DauCapMoi { get; set; }
        public int? DauHoiCu { get; set; }
        public int? DauHoiMoi { get; set; }
        public int? HeSo { get; set; }
        public int? SoTieuThu { get; set; }
        public decimal? TienTruocThue { get; set; }
        public decimal? TyLeVAT { get; set; }
        public decimal? TienVAT { get; set; }
        public decimal? TyLeBVMT { get; set; }
        public decimal? TienBVMT { get; set; }
        public decimal? SoTien { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}