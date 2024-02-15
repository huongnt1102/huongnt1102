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

namespace DichVu.MatBang.Import
{
    public partial class frmGiaThue : DevExpress.XtraEditors.XtraForm
    {
        public frmGiaThue()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool IsSave { get; set; }

        void UpdateGiaMatBang(int _MaMB)
        {
            var db = new MasterDataContext();
            try
            {
                var objMB = db.mbMatBangs.Single(p => p.MaMB == _MaMB);
                objMB.GiaThue = objMB.mbGiaThues.Sum(p => p.ThanhTien);
                db.SubmitChanges();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
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
                System.Collections.Generic.List<GiaThueItem> list = Library.Import.ExcelAuto.ConvertDataTable<GiaThueItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvGiaThue, itemSheet));

                gcGiaThue.DataSource = list;

                //gcGiaThue.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new GiaThueItem
                //{
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    TenLG = p["Loại giá thuê"].ToString().Trim(),
                //    DienTich = p["Diện tích"].Cast<decimal>(),
                //    DonGia = p["Đơn giá"].Cast<decimal>(),
                //    ThanhTien = p["Thành tiền"].Cast<decimal>(),
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
            gvGiaThue.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcGiaThue.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltMatBang = (from mb in db.mbMatBangs where mb.MaTN == this.MaTN orderby mb.MaSoMB select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower() }).ToList();
                var ltLoaiGia = (from p in db.LoaiGiaThues where p.MaTN == this.MaTN orderby p.TenLG select new { p.ID, TenLG = p.TenLG.ToLower() }).ToList();
                
                var ltSource = (List<GiaThueItem>)gcGiaThue.DataSource;
                var ltError = new List<GiaThueItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        if (i.DienTich <= 0)
                        {
                            i.Error = "Diện tích phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.DonGia <= 0)
                        {
                            i.Error = "Đơn giá phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }
                        
                        if (i.ThanhTien <= 0)
                        {
                            i.Error = "Thành tiền phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaLG = ltLoaiGia.Where(p => p.TenLG == i.TenLG.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                        if (_MaLG == null)
                        {
                            i.Error = "Loại giá không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaMB = ltMatBang.Where(p => p.MaSoMB == i.MaSoMB.ToLower()).Select(p => (int?)p.MaMB).FirstOrDefault();
                        if (_MaMB == null)
                        {
                            i.Error = "Mã mặt bằng không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objGiaThue = db.mbGiaThues.FirstOrDefault(p => p.MaMB == _MaMB & p.MaLG == _MaLG);
                        if (objGiaThue == null)
                        {
                            objGiaThue = new mbGiaThue();
                            objGiaThue.MaMB = _MaMB;
                            objGiaThue.MaLG = _MaLG;
                            db.mbGiaThues.InsertOnSubmit(objGiaThue);
                        }
                        
                        objGiaThue.DienTich = i.DienTich;
                        objGiaThue.DonGia = i.DonGia;
                        objGiaThue.ThanhTien = i.ThanhTien;
                        objGiaThue.DienGiai = i.DienGiai;

                        db.SubmitChanges();

                        this.UpdateGiaMatBang(_MaMB.Value);
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcGiaThue.DataSource = ltError;
                }
                else
                {
                    gcGiaThue.DataSource = null;
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
            Library.Commoncls.ExportExcel(gcGiaThue);
        }
    }

    public class GiaThueItem
    {
        public string MaSoMB { get; set; }
        public string TenLG { get; set; }
        public decimal DienTich { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}