using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Library;
using LinqToExcel;

namespace SAP.Import
{
    public partial class frmCompanyCode : DevExpress.XtraEditors.XtraForm
    {
        public frmCompanyCode()
        {
            InitializeComponent();
        }

        public bool isSave { get; set; }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
        }

        private void itemChoice_ItemClick(object sender, ItemClickEventArgs e)
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

                System.Collections.Generic.List<CompanyCodeImportItem> list = Library.Import.ExcelAuto.ConvertDataTable<CompanyCodeImportItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gcImport.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltSource = (List<CompanyCodeImportItem>)gcImport.DataSource;
                var ltError = new List<CompanyCodeImportItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.KyHieu == null)
                        {
                            i.Error = "Vui lòng nhập mã công ty";
                            ltError.Add(i);
                            continue;
                        }
                        #endregion

                        var objQuocGia = db.QuocTiches.FirstOrDefault(_ => _.TenVT == i.QuocGia);
                        if (objQuocGia == null)
                        {
                            i.Error = "Quốc gia không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objTinh = db.Tinhs.FirstOrDefault(_ => _.Code == i.Tinh);
                        if(objTinh == null)
                        {
                            i.Error = "Tỉnh không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objLoaiTien = db.LoaiTiens.FirstOrDefault(_ => _.KyHieuLT == i.LoaiTien);
                        if (objLoaiTien == null)
                        {
                            i.Error = "Loại tiền không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        try
                        {
                            var obj = db.CompanyCodes.FirstOrDefault(_ => _.KyHieu.ToLower() == i.KyHieu.ToLower());
                            if (obj == null)
                            {
                                obj = new CompanyCode();
                                obj.KyHieu = i.KyHieu;
                                db.CompanyCodes.InsertOnSubmit(obj);
                            }

                            obj.TenVT = i.TenVT;
                            obj.Ten = i.Ten;
                            obj.MaSoThue = i.MaSoThue;
                            obj.DiaChi = i.DiaChi;
                            obj.MaQuocGia = objQuocGia.ID;
                            obj.MaTinh = objTinh.MaTinh;
                            obj.MaLoaiTien = objLoaiTien.ID;
                        }
                        catch { }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcImport.DataSource = ltError;
                }
                else
                {
                    gcImport.DataSource = null;
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

        private void itemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcImport);
        }

        public class CompanyCodeImportItem
        {
            public string KyHieu { get; set; }
            public string TenVT { get; set; }
            public string Ten { get; set; }
            public string MaSoThue { get; set; }
            public string DiaChi { get; set; }
            public string QuocGia { get; set; }
            public string Tinh { get; set; }
            public string LoaiTien { get; set; }
            public string Error { get; set; }
        }
    }
}