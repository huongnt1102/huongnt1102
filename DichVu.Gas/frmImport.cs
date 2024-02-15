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

namespace DichVu.Gas
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        List<ChiTietGasItem> ltChiTiet = null;

        void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltChiTiet = (from dm in db.dvGasDinhMucs
                             where dm.MaTN == this.MaTN
                             orderby dm.STT
                             select new ChiTietGasItem()
                             {
                                 MaDM = dm.ID,
                                 TenDM = dm.TenDM,
                                 DinhMuc = dm.DinhMuc,
                                 DonGia=dm.DonGia,
                                 DienGiai = dm.DienGiai
                             }).ToList();
            }
            catch {
                ltChiTiet = null;
            }
            finally
            {
                db.Dispose();
            }
        }

        void TinhTien(int _MaMB, decimal? _SoTieuThu, bool? _IsCanHo)
        {
            for (var i = 0; i < ltChiTiet.Count; i++)
            {
                var objCT = ltChiTiet[i];

                if (_SoTieuThu > objCT.DinhMuc && i < ltChiTiet.Count - 1)
                {
                    objCT.SoLuong = objCT.DinhMuc;
                    _SoTieuThu -= objCT.DinhMuc.Value;
                }
                else
                {
                    objCT.SoLuong = _SoTieuThu;
                    _SoTieuThu = 0;
                }

                objCT.DonGia = objCT.DonGia;
                objCT.ThanhTien = objCT.DonGia * objCT.SoLuong;
                objCT.DienGiai = objCT.DienGiai;
            }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            this.LoadDinhMuc();
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
                System.Collections.Generic.List<GasItem> list = Library.Import.ExcelAuto.ConvertDataTable<GasItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvNuoc, itemSheet));

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
            if (ltChiTiet == null)
            {
                DialogBox.Error("Định mức nước chưa được thiết lập");
                return;
            }

            if (gcNuoc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 orderby mb.MaSoMB
                                 select new { mb.MaMB, mb.MaSoMB, mb.MaKH, mb.IsCanHoCaNhan }).ToList();
                var lKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == MaTN orderby kh.KyHieu select new { kh.MaKH, KyHieu = kh.KyHieu.ToLower() }).ToList();
                var lDongHo = (from dh in db.dvGasDongHos where dh.MaTN == MaTN orderby dh.SoDH select new { dh.ID, SoDH = dh.SoDH.ToLower() }).ToList();

                var ltDVT = (from d in db.dvgDonViTinhs select new { d.ID, KyHieu = d.KHDVT.ToLower() }).ToList();

                var ltGas = (List<GasItem>)gcNuoc.DataSource;
                var ltError = new List<GasItem>();

                foreach(var n in ltGas)
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

                        var objDongHo = lDongHo.FirstOrDefault(_ => _.SoDH == n.SoDongHo.ToLower());
                        if(objDongHo == null)
                        {
                            n.Error = "Đồng hồ không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var _MaDVT = ltDVT.Where(p => p.KyHieu == n.TenDVT.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                        if (_MaDVT == null)
                        {
                            n.Error = "Đơn vị tính không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        
                        var objGas = new dvGa();
                        objGas.MaTN = this.MaTN;
                        objGas.MaMB = objMB.MaMB;
                        objGas.MaKH = objKH.MaKH;
                        objGas.MaDH = objDongHo.ID;
                        objGas.TuNgay = n.TuNgay;
                        objGas.DenNgay = n.DenNgay;
                        objGas.ChiSoCu = n.ChiSoCu;
                        objGas.ChiSoMoi = n.ChiSoMoi;
                        objGas.SoTieuThu = n.ChiSoMoi - n.ChiSoCu;
                        objGas.TyLe = n.TyLe;
                        objGas.MaDVT = _MaDVT;
                        objGas.SoTieuThuQD =  objGas.SoTieuThu * objGas.TyLe;
                        objGas.NgayTT = n.NgayTT;
                        //if (n.DenNgay.Value.Day >= 15) objGas.NgayTB = new DateTime(n.DenNgay.Value.Year, n.DenNgay.Value.Month, 1);
                        //else objGas.NgayTB = new DateTime(n.TuNgay.Value.Year, n.TuNgay.Value.Month, 1);
                        objGas.NgayTB = new DateTime(n.Nam, n.Thang, 1);

                        this.TinhTien(objGas.MaMB.Value, objGas.SoTieuThuQD.Value, objMB.IsCanHoCaNhan ?? true);
                        foreach (var ct in ltChiTiet)
                        {
                            if (ct.SoLuong.Value > 0)
                            {
                                var objCT = new dvGasChiTiet();
                                objCT.MaDM = ct.MaDM;
                                objCT.SoLuong = ct.SoLuong;
                                objCT.DonGia = ct.DonGia;
                                objCT.ThanhTien = ct.ThanhTien;
                                objCT.DienGiai = ct.DienGiai;
                                objGas.dvGasChiTiets.Add(objCT);
                            }
                        }

                        objGas.ThanhTien = ltChiTiet.Sum(p => p.ThanhTien).GetValueOrDefault();
                        objGas.TyLeVAT = n.TyLeVAT;
                        objGas.TienVAT = Math.Round((decimal)(objGas.ThanhTien * objGas.TyLeVAT),0,MidpointRounding.AwayFromZero);
                        if (n.SoTien > 0)
                            objGas.TienTT = n.SoTien;
                        else
                            objGas.TienTT = objGas.ThanhTien + objGas.TienVAT;

                        objGas.DienGiai = n.DienGiai;
                        objGas.NgayNhap = db.GetSystemDate();
                        objGas.MaNVN = Common.User.MaNV;

                        db.dvGas.InsertOnSubmit(objGas);
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

        private void frmImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            ltChiTiet = null;
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcNuoc);
        }
    }

    public class GasItem
    {
        public string MaSoMB { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public decimal? ChiSoCu { get; set; }
        public decimal? ChiSoMoi { get; set; }
        public decimal? SoTieuThu { get; set; }
        public string TenDVT { get; set; }
        public decimal? TyLe { get; set; }
        public decimal? TyLeVAT { get; set; }
        public decimal? SoTien { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
        public DateTime? NgayTT { get; set; }
        //public DateTime? NgayTB { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaKhachHang { get; set; }
        public string SoDongHo { get; set; }
        public decimal? TienTruocVAT { get; set; }
    }
}