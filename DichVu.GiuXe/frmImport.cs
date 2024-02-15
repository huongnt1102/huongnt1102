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

namespace DichVu.GiuXe
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        public bool IsGiuXe { get; set; }
        public bool isSave { get; set; }
        CachTinhCls objCT;

        public frmImport()
        {
            InitializeComponent();
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog(); 
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
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
            { file.Dispose(); }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gcTheXe.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(this.Tag.ToString());
                    System.Collections.Generic.List<TheXeItem> list = Library.Import.ExcelAuto.ConvertDataTable<TheXeItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvTheXe, itemSheet));

                    gcTheXe.DataSource = list;

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            grvTheXe.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcTheXe.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var listLX = (from lx in db.dvgxLoaiXes where lx.MaTN == MaTN select new { lx.MaLX, lx.TenLX }).ToList();
                var listMB = (from mb in db.mbMatBangs where mb.MaTN == MaTN select new { mb.MaMB, mb.MaSoMB, mb.MaKH, mb.IsCanHoCaNhan }).ToList();
                
                #region "   Rang buoc du lieu"
                if (listLX.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Loại xe] cho Dự án này trước.");
                    return;
                }

                if (listMB.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Mặt bằng] cho Dự án này trước.");
                    return;
                }

                #endregion

                var _IsLTT_TheXe = itemKieuLTT.EditValue.ToString() == "Tạo lịch thanh toán theo từng thẻ xe" ? true : false;

                var ltTheXe = (List<TheXeItem>)gcTheXe.DataSource;
                var ltError = new List<TheXeItem>();
                foreach (var n in ltTheXe)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region "   Rang buoc du lieu"
                        var objMB = listMB.FirstOrDefault(p => p.MaSoMB.Trim().Replace(" ", "").ToLower() == n.MaSoMB.Trim().Replace(" ", "").ToLower());
                        if (objMB == null)
                        {
                            n.Error = "Mặt bằng này không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objLX = listLX.FirstOrDefault(lx => lx.TenLX.Trim().Replace(" ", "").ToLower() == n.LoaiXe.Trim().Replace(" ", "").ToLower());
                        if (objLX == null)
                        {
                            n.Error = "Loại xe này không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        //The xe
                        var objTheXe = new dvgxTheXe();
                        objTheXe.MaTN = this.MaTN;
                        objTheXe.MaMB = objMB.MaMB;
                        objTheXe.MaKH = objMB.MaKH;
                        objTheXe.NgayDK = n.NgayDK;
                        objTheXe.SoThe = n.SoThe;
                        objTheXe.ChuThe = n.ChuThe;
                        objTheXe.MaLX = objLX.MaLX;
                        objTheXe.BienSo = n.BienSo;
                        objTheXe.MauXe = n.MauXe;
                        objTheXe.DoiXe = n.DoiXe;
                        objTheXe.TuNgay = n.TuNgay;
                        objTheXe.DenNgay = n.DenNgay;
                        objTheXe.DienGiai = n.DienGiai;
                        objTheXe.NgungSuDung = false;
                        objTheXe.NgayNhap = db.GetSystemDate();
                        objTheXe.MaNVN = Common.User.MaNV;

                        //Tinh gia
                        objCT.MaTN = this.MaTN;
                        objCT.MaLX = objLX.MaLX;
                        objCT.MaMB = objMB.MaMB;
                        objCT.LoadDinhMuc();

                        var _SoLuongThe = (from tx in db.dvgxTheXes
                                           
                                           where tx.MaMB == objMB.MaMB & tx.MaKH == objMB.MaKH & tx.MaLX == objLX.MaLX
                                           select tx).Count();
                        _SoLuongThe++;
                        var objGia = (from bg in objCT.ltBangGia
                                      where bg.SoLuong <= _SoLuongThe
                                      orderby bg.SoLuong descending
                                      select bg).First();
                        objTheXe.MaDM = objGia.MaDM;
                        objTheXe.GiaThang = objGia.DonGiaThang;
                        objTheXe.GiaNgay = objGia.DonGiaNgay;

                        if (this.IsGiuXe)
                        {
                            var objGiuXe = db.dvgxGiuXes.FirstOrDefault(p => p.MaTN == this.MaTN & p.SoDK == n.SoDK);
                            if (objGiuXe == null)
                            {
                                objGiuXe = new dvgxGiuXe();
                                objGiuXe.MaTN = this.MaTN;
                                objGiuXe.MaMB = objMB.MaMB;
                                objGiuXe.MaKH = objMB.MaKH;
                                objGiuXe.NgayDK = n.NgayDK;
                                objGiuXe.SoDK = n.SoDK;
                                objGiuXe.NgayTT = n.NgayTT;
                                objGiuXe.KyTT = n.KyTT;
                                objGiuXe.TienTT = objGiuXe.KyTT * objTheXe.GiaThang;
                                objGiuXe.DienGiai = n.DienGiai;
                                objGiuXe.NgayNhap = db.GetSystemDate();
                                objGiuXe.MaNVN = Common.User.MaNV;
                                objGiuXe.NgungSuDung = false;
                                db.dvgxGiuXes.InsertOnSubmit(objGiuXe);
                            }
                            else
                            {
                                objGiuXe.TienTT += objTheXe.GiaThang * objGiuXe.KyTT;
                            }

                            //Add vao giu xe
                            objGiuXe.dvgxTheXes.Add(objTheXe);
                        }
                        else
                        {
                            objTheXe.NgayTT = n.NgayTT;
                            objTheXe.KyTT = n.KyTT;
                            objTheXe.TienTruocThue = objTheXe.GiaThang.GetValueOrDefault() * objTheXe.KyTT;
                            objTheXe.ThueGTGT = n.TyLeVAT;
                            objTheXe.TienThueGTGT = objTheXe.TienTruocThue.GetValueOrDefault() * n.TyLeVAT;
                            objTheXe.TienTT = objTheXe.TienTruocThue + objTheXe.TienThueGTGT;
                            db.dvgxTheXes.InsertOnSubmit(objTheXe);
                        }

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
                    gcTheXe.DataSource = ltError;
                }
                else
                {
                    gcTheXe.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
            finally { wait.Dispose(); db.Dispose(); }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            objCT = new CachTinhCls();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcTheXe);
        }
    }

    public class TheXeItem
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string MaSoMB { get; set; }
        public string SoDK { get; set; }
        public DateTime NgayDK { get; set; }
        public string SoThe { get; set; }
        public string ChuThe { get; set; }
        public string LoaiXe { get; set; }
        public string BienSo { get; set; }
        public string DoiXe { get; set; }
        public string MauXe { get; set; }
        public DateTime NgayTT { get; set; }
        public decimal KyTT { get; set; }
        public decimal TyLeVAT { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}