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

namespace ToaNha.Import
{
    public partial class frmHuyen : DevExpress.XtraEditors.XtraForm
    {
        public frmHuyen()
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

                System.Collections.Generic.List<HuyenImportItem> list = Library.Import.ExcelAuto.ConvertDataTable<HuyenImportItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

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
                var ltSource = (List<HuyenImportItem>)gcImport.DataSource;
                var ltError = new List<HuyenImportItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.Code == null)
                        {
                            i.Error = "Vui lòng nhập mã";
                            ltError.Add(i);
                            continue;
                        }
                        #endregion

                        var objTinh = db.Tinhs.FirstOrDefault(_ => _.Code == i.Tinh);
                        if (objTinh == null)
                        {
                            i.Error = "Tỉnh không tồn tại";
                            ltError.Add(i);
                            continue;
                        }
                        
                        
                        
                        
                        try
                        {
                            if (i.TenHuyen!= "")
                            {
                                var obj= db.Huyens.FirstOrDefault(_ => _.TenHuyen.ToLower() == i.TenHuyen.ToLower());
                                if (obj != null)
                                {
                                    //i.Error = "Công trình này đã tồn tại";
                                    //ltError.Add(i);
                                    //continue;

                                    
                                }
                                else
                                {
                                    obj = new Huyen();
                                    db.Huyens.InsertOnSubmit(obj);
                                }

                                obj.MaTinh = objTinh.MaTinh;
                                obj.TenHuyen = i.TenHuyen;
                                obj.TenHienThi = i.TenHienThi;
                                obj.Code = i.Code;
                            }
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

        public class HuyenImportItem
        {
            public string TenHuyen { get; set; }
            public string TenHienThi { get; set; }
            public string Tinh { get; set; }
            public string Code { get; set; }
            public string Error { get; set; }
        }
    }
}