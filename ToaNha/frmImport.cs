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

namespace ToaNha
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
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

                System.Collections.Generic.List<QuocGiaItem> list = Library.Import.ExcelAuto.ConvertDataTable<QuocGiaItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

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
                var quocTich = db.QuocTiches;

                var ltSource = (List<QuocGiaItem>)gcImport.DataSource;
                var ltError = new List<QuocGiaItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.TenVT == null)
                        {
                            i.Error = "Vui lòng nhập tên viết tắt";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.TenQuocTich == null)
                        {
                            i.Error = "Vui lòng nhập tên quốc tịch";
                            ltError.Add(i);
                            continue;
                        }
                        #endregion
                        
                        var objQT = new QuocTich();
                        objQT.TenQuocTich = i.TenQuocTich;
                        objQT.TenVT = i.TenVT;
                        
                        try
                        {
                            if (i.TenVT != "")
                            {
                                var lx = quocTich.FirstOrDefault(_ => _.TenVT.ToLower() == i.TenVT.ToLower());
                                if (lx != null)
                                {
                                    i.Error = "Quốc gia này đã tồn tại trong hệ thống";
                                    ltError.Add(i);
                                    continue;
                                }
                            }
                        }
                        catch { }

                        db.QuocTiches.InsertOnSubmit(objQT);
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

        public class QuocGiaItem
        {
            public string TenQuocTich { get; set; }
            public string TenVT { get; set; }
            public string Error { get; set; }
        }
    }
}