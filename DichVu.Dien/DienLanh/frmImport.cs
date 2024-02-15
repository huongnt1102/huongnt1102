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

namespace DichVu.DienLanh
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
                System.Collections.Generic.List<DienItem> list = Library.Import.ExcelAuto.ConvertDataTable<DienItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvNuoc, itemSheet));

                gcDien.DataSource = list;

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
            if (gcDien.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objCachTinh = new DichVu.DienLanh.CachTinhCls();
                objCachTinh.MaTN = this.MaTN;

                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 orderby mb.MaSoMB
                                 select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower(), mb.MaKH }).ToList();

                var ltDongHo = (from dh in db.dvDienLanh_DongHos
                                where dh.MaTN == this.MaTN
                                orderby dh.SoDH
                                select new { dh.ID, SoDH = dh.SoDH.ToLower() }).ToList();
                var lKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == MaTN orderby kh.KyHieu select new { kh.MaKH, KyHieu = kh.KyHieu.ToLower() }).ToList();


                var ltDien = (List<DienItem>)gcDien.DataSource;
                var ltError = new List<DienItem>();

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
                        if (SqlMethods.DateDiffDay(n.TuNgay, n.NgayTT) <= 0)
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

                        var count = db.dvDiens.Where(p =>p.MaMB == objMB.MaMB & p.MaKH == objMB.MaKH & p.MaDH == _MaDH & SqlMethods.DateDiffMonth(n.DenNgay, p.DenNgay) == 0 & p.MaDH == _MaDH).Count();
                        if (count > 0)
                        {
                            n.Error = string.Format("Mặt bằng này đã tồn tại chỉ số điện của tháng {0:MM/yyyy}", n.DenNgay);
                            ltError.Add(n);
                            continue;
                        }

                        var loai_dien = db.dvDienLoaiDiens.FirstOrDefault(_=>_.TenLoaiDien == n.MaLoaiDien.Trim());
                        if(loai_dien == null)
                        {
                            n.Error = "Loại điện không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objDien = new dvDienLanh();
                        objDien.MaTN = this.MaTN;
                        objDien.MaMB = objMB.MaMB;
                        objDien.MaKH = objKH.MaKH;
                        objDien.MaDH = _MaDH;
                        objDien.NgayTT = n.NgayTT;
                        objDien.TuNgay = n.TuNgay;
                        objDien.DenNgay = n.DenNgay;
                        objDien.ChiSoCu = n.ChiSoCu;
                        objDien.ChiSoMoi = n.ChiSoMoi;
                        objDien.HeSo = n.HeSo ;// <= 0 ? 1 : n.HeSo;
                        objDien.MaDH = _MaDH;
                       
                            //objDien.SoTieuThu = (n.ChiSoCu.GetValueOrDefault() == 0 & n.ChiSoMoi.GetValueOrDefault() == 0) ? n.SoTieuThu : (objDien.ChiSoMoi - objDien.ChiSoCu) * objDien.HeSo;
                        objDien.SoTieuThu = n.SoTieuThu;

                        objDien.MaLoaiDien = loai_dien.ID;
                        
                        #region Tinh dien cho import data moi
                        objCachTinh.MaMB = objDien.MaMB.Value;
                        objCachTinh.MaLD = loai_dien.ID;
                        objCachTinh.LoadDinhMuc();
                        if (n.TieuThuDHCu.GetValueOrDefault() == 0)
                        {
                            objCachTinh.SoTieuThu = objDien.SoTieuThu;
                        }
                        else
                        {
                            objCachTinh.SoTieuThu = objDien.SoTieuThu.Value + n.TieuThuDHCu.Value;
                        }
                       
                        objCachTinh.XuLy();

                        foreach (var ct in objCachTinh.ltChiTiet)
                        {
                            if (ct.SoLuong.Value > 0)
                            {
                                var objCT = new dvDienLanhChiTiet();
                                objCT.MaDM = ct.MaDM;
                                objCT.SoLuong = ct.SoLuong;
                                objCT.DonGia = ct.DonGia;
                                objCT.ThanhTien = ct.ThanhTien;
                                objCT.DienGiai = ct.DienGiai;
                                objDien.dvDienLanhChiTiets.Add(objCT);
                            }
                        }

                        objDien.ThanhTien = objCachTinh.GetThanhTien();
                        if (n.TienTruocThue > 0) objDien.ThanhTien = n.TienTruocThue;
                        objDien.TyLeVAT = n.TyLeVAT;
                        if (n.TienVAT > 0) objDien.TienVAT = n.TienVAT;
                        else
                            objDien.TienVAT = Math.Round((decimal)(objDien.ThanhTien * objDien.TyLeVAT), 0, MidpointRounding.AwayFromZero);
                         objDien.TyLeHaoHut = n.HaoHut!=null?n.HaoHut:0;
                         objDien.TienHaoHut = Math.Round((decimal)(objDien.ThanhTien * objDien.TyLeHaoHut), 0, MidpointRounding.AwayFromZero);
                        if (n.SoTien > 0)
                            objDien.TienTT = n.SoTien;
                        else
                            objDien.TienTT = objDien.ThanhTien + objDien.TienVAT + objDien.TienHaoHut;
                        #endregion

                        objDien.DienGiai = n.DienGiai;
                        objDien.NgayNhap = db.GetSystemDate();
                        objDien.MaNVN = Common.User.MaNV;
                        objDien.NgayTB = new DateTime(n.Nam, n.Thang, 1);
                        db.dvDienLanhs.InsertOnSubmit(objDien);
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                        n.Error = mes;
                        ltError.Add(n);
                    }
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcDien.DataSource = ltError;
                }
                else
                {
                    gcDien.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                DevExpress.XtraEditors.XtraMessageBoxArgs args = new DevExpress.XtraEditors.XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 1000;
                args.Caption = ex.GetType().FullName;
                args.Text = mes;
                args.Buttons = new System.Windows.Forms.DialogResult[] { System.Windows.Forms.DialogResult.OK, System.Windows.Forms.DialogResult.Cancel };
                DevExpress.XtraEditors.XtraMessageBox.Show(args).ToString();
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
            Library.Commoncls.ExportExcel(gcDien);
        }

        public class DienItem
        {
            public int Thang { get; set; }
            public int Nam { get; set; }
            public string MaSoMB { get; set; }
            public string SoDH { get; set; }
            public DateTime? NgayTT { get; set; }
            public DateTime? TuNgay { get; set; }
            public DateTime? DenNgay { get; set; }
            public decimal? ChiSoCu { get; set; }
            public decimal? ChiSoMoi { get; set; }
            public decimal? HeSo { get; set; }
            public decimal? SoTieuThu { get; set; }
            public decimal? TieuThuDHCu { get; set; }
            public decimal? TyLeVAT { get; set; }
            public decimal? TienVAT { get; set; }
            public decimal? TienTruocThue { get; set; }
            public decimal? SoTien { get; set; }
            public string DienGiai { get; set; }
            public decimal? HaoHut { get; set; }
            public string Error { get; set; }
            public decimal? ChenhLech { get; set; }
            // 2022/04/14 - thêm mã khách hàng
            public string MaKhachHang { get; set; }
            public string MaLoaiDien { get; set; }
        }
    }

    
}