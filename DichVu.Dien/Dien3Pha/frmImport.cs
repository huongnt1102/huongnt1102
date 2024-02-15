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

namespace DichVu.Dien.Dien3Pha
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
                System.Collections.Generic.List<Dien3faItem> list = Library.Import.ExcelAuto.ConvertDataTable<Dien3faItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                gc.DataSource = list;

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
                                 select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower(), mb.MaKH, mb.MaLMB }).ToList();

                var ltDinhMuc = (from dm in db.dvDien3PhaDinhMucs
                                 where dm.MaTN == this.MaTN
                                 select new { dm.ID, TenDM = dm.TenDM.ToLower(), dm.MaLMB, dm.MaMB, dm.MaKH }).ToList();
                var lKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == MaTN select new { kh.MaKH, KyHieu = kh.KyHieu.ToLower() }).ToList();
                var lDongHo = (from dh in db.dvDien3PhaDongHos where dh.MaTN == MaTN select new
                {
                    dh.ID, SoDH = dh.SoDH.ToLower(),
                    dh.HeSo
                }).ToList();

                var ltDien = (List<Dien3faItem>)gc.DataSource;
                var ltError = new List<Dien3faItem>();

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

                        var objKH = lKhachHang.FirstOrDefault(_ => _.KyHieu == n.MaKhachHang.ToLower());
                        if(objKH == null)
                        {
                            n.Error = "Khách hàng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objDH = lDongHo.FirstOrDefault(_ => _.SoDH == n.MaDongHo.ToLower());
                        if(objDH == null)
                        {
                            n.Error = "Số đồng hồ không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        if (SqlMethods.DateDiffDay(n.TuNgay, n.NgayTT) <= 0)
                        {
                            n.Error = "Khoảng thời gian không hợp lệ";
                            ltError.Add(n);
                            continue;
                        }

                        int? _MaDM = null;
                        if (!string.IsNullOrEmpty(n.TenDM))
                        {
                            var objDM = ltDinhMuc.FirstOrDefault(p => p.TenDM == n.TenDM.ToLower() & p.MaMB == objMB.MaMB);
                            if (objDM != null)
                                _MaDM = objDM.ID;
                            else
                            {
                                objDM = ltDinhMuc.FirstOrDefault(p => p.TenDM == n.TenDM.ToLower() & p.MaKH == objMB.MaKH);
                                if (objDM != null)
                                    _MaDM = objDM.ID;
                                else
                                {
                                    objDM = ltDinhMuc.FirstOrDefault(p => p.TenDM == n.TenDM.ToLower() & p.MaLMB == objMB.MaLMB);
                                    if (objDM != null)
                                        _MaDM = objDM.ID;
                                    else
                                    {
                                        objDM = ltDinhMuc.FirstOrDefault(p => p.TenDM == n.TenDM.ToLower());
                                        if (objDM != null)
                                            _MaDM = objDM.ID;
                                    }
                                }
                            }
                        }

                        if (_MaDM == null)
                        {
                            n.Error = "Định mức không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objDien = new dvDien3Pha();                        

                        var count = db.dvDien3Phas.Where(p => p.MaMB == objMB.MaMB & p.MaKH == objMB.MaKH & SqlMethods.DateDiffMonth(n.DenNgay, p.DenNgay) == 0 & p.MaDH == objDH.ID).Select(p=>p.ID).ToList();
                        if (count.Count > 0)
                        {
                            objDien = db.dvDien3Phas.Single(p => p.ID == count[0]);
                        }
                        else
                        {
                            objDien.MaTN = this.MaTN;
                            objDien.MaMB = objMB.MaMB;
                            objDien.MaKH = objKH.MaKH;
                            objDien.TuNgay = n.TuNgay;
                            objDien.DenNgay = n.DenNgay;
                            objDien.NgayTT = n.NgayTT;
                            objDien.TyLeVAT = n.TyLeVAT;
                            objDien.MaDH = objDH.ID;
                            objDien.HeSo = n.HeSo;
                            objDien.MaDH = objDH.ID;
                           
                            objDien.DienGiai = n.DienGiai;
                            objDien.NgayNhap = db.GetSystemDate();
                            objDien.MaNVN = Common.User.MaNV;
                            db.dvDien3Phas.InsertOnSubmit(objDien);
                        }

                        
                        objDien.SoTieuThu = objDien.SoTieuThu.GetValueOrDefault() + n.SoTieuThu;
                        //objDien.ThanhTien = objDien.TienTT = objDien.ThanhTien.GetValueOrDefault() + n.ThanhTien;
                   

                        var objDienCT = new dvDien3PhaChiTiet();
                        objDienCT.MaDM = _MaDM;
                        objDienCT.ChiSoCu = n.ChiSoCu;
                        objDienCT.ChiSoMoi = n.ChiSoMoi;
                        objDienCT.SoLuong = n.SoTieuThu;
                        objDienCT.DonGia = n.DonGia;

                        int? heSo = 1;
                        try
                        {
                            heSo = objDH.HeSo.GetValueOrDefault();
                        }
                        catch
                        {

                        }

                        objDienCT.ThanhTien = n.SoTieuThu * n.DonGia * heSo;
                        //if (n.ThanhTien.GetValueOrDefault() == 0)
                        //{
                        //    objDienCT.ThanhTien = n.SoTieuThu * n.DonGia;
                        //}
                        //else
                        //{
                        //    objDienCT.ThanhTien = n.ThanhTien;
                        //}
                        
                        objDien.dvDien3PhaChiTiets.Add(objDienCT);
                        objDien.NgayTB = new DateTime(n.Nam, n.Thang, 1);

                        var TongTien = objDien.dvDien3PhaChiTiets.Sum(o => o.ThanhTien);
                        //if (n.TienTruocThue > 0) TongTien = n.TienTruocThue;
                        //if (n.TienVAT > 0) objDien.TienVAT = n.TienVAT;
                        //else
                            objDien.TienVAT = Math.Round((decimal)(TongTien * n.TyLeVAT), 0, MidpointRounding.AwayFromZero);
                        objDien.ThanhTien = TongTien;
                        objDien.TienTT = TongTien + objDien.TienVAT;

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

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }
    }

    public class Dien3faItem
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaSoMB { get; set; }
        public DateTime? NgayTT { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string TenDM { get; set; }
        public int? ChiSoCu { get; set; }
        public decimal? TyLeVAT { get; set; }
        public decimal? TienVAT { get; set; }
        public int? ChiSoMoi { get; set; }
        public int? SoTieuThu { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? TienTruocThue { get; set; }
        public decimal? ThanhTien { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
        public string MaKhachHang { get; set; }
        public string MaDongHo { get; set; }
        public int? HeSo { get; set; }
    }
}