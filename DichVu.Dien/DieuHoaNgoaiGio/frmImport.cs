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

namespace DichVu.Dien.DieuHoaNgoaiGio
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
                System.Collections.Generic.List<DienDieuHoaItem> list = Library.Import.ExcelAuto.ConvertDataTable<DienDieuHoaItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

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
                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 orderby mb.MaSoMB
                                 select new
                                 {
                                     mb.MaMB,
                                     MaSoMB = mb.MaSoMB.ToLower(),
                                     mb.MaKH,
                                     mb.DienTich
                                 }).ToList();

                var ltLoaiTien = (from lt in db.LoaiTiens
                                 select new
                                 {
                                     lt.ID,
                                     KyHieu = lt.KyHieuLT.ToLower(),
                                     lt.TyGia
                                 }).ToList();

                var ltDien = (List<DienDieuHoaItem>)gc.DataSource;
                var ltError = new List<DienDieuHoaItem>();

                foreach(var item in ltDien)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB == item.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            item.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(item);
                            continue;
                        }

                        var objLT = ltLoaiTien.FirstOrDefault(p => p.KyHieu == item.LoaiTien.ToLower());
                        if (objLT == null)
                        {
                            item.Error = "Loại tiền không tồn tại trong hệ thống";
                            ltError.Add(item);
                            continue;
                        }

                        if (item.NgayCT == null)
                        {
                            item.Error = "Ngày chứng từ trống hoặc không đúng định dạng";
                            ltError.Add(item);
                            continue;
                        }

                        var dienTich = objMB.DienTich;
                        if (item.SoFCU.GetValueOrDefault() > 0) dienTich = item.SoFCU;

                        var objDien = new dvDienDieuHoa();
                        objDien.MaTN = this.MaTN;
                        objDien.MaMB = objMB.MaMB;
                        objDien.MaKH = objMB.MaKH;
                        objDien.NgayCT = item.NgayCT;
                        objDien.SoGio = item.SoGio;
                        objDien.SoFCU = dienTich;
                        objDien.HeSo = item.HeSo;
                        objDien.DonGia = item.DonGia;
                        
                        if(item.ThanhTien.GetValueOrDefault() > 0)
                        {
                            objDien.ThanhTien = item.ThanhTien;
                        }
                        else
                        {
                            objDien.ThanhTien = dienTich * item.SoGio * item.DonGia * item.HeSo;
                        }

                        objDien.MaLT = objLT.ID;
                        objDien.TyGia = objLT.TyGia;
                        objDien.ThanhTienQD = Math.Round(objDien.ThanhTien.GetValueOrDefault() * objDien.TyGia.GetValueOrDefault(), 0);
                        objDien.DienGiai = item.DienGiai;
                        objDien.NgayNhap = db.GetSystemDate();
                        objDien.MaNVN = Common.User.MaNV;

                        db.dvDienDieuHoas.InsertOnSubmit(objDien);
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        item.Error = ex.Message;
                        ltError.Add(item);
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

    public class DienDieuHoaItem
    {
        public string MaSoMB { get; set; }
        public DateTime? NgayCT { get; set; }
        public decimal? SoFCU { get; set; }
        public decimal? SoGio { get; set; }
        public decimal? HeSo { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public string LoaiTien { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}