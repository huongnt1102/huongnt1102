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
    public partial class frmImportTDA : DevExpress.XtraEditors.XtraForm
    {
        public frmImportTDA()
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

                System.Collections.Generic.List<TieuDuAnItem> list = Library.Import.ExcelAuto.ConvertDataTable<TieuDuAnItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

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
                var tieuDuAn = db.tnTieuDuAns;

                var ltSource = (List<TieuDuAnItem>)gcImport.DataSource;
                var ltError = new List<TieuDuAnItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.ZZTIEUDUAN == null)
                        {
                            i.Error = "Vui lòng nhập mã tiểu dự án";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.TENTDA == null)
                        {
                            i.Error = "Vui lòng nhập tên tiểu dự án";
                            ltError.Add(i);
                            continue;
                        }
                        #endregion
                        
                        var objTDA = new tnTieuDuAn();
                        objTDA.ZZTIEUDUAN = i.ZZTIEUDUAN;
                        objTDA.TNTIEUDA = i.TNTIEUDA;
                        objTDA.TENTDA = i.TENTDA;
                        objTDA.MATN = i.MATN;
                        
                        try
                        {
                            if (i.TNTIEUDA != "")
                            {
                                var lx = tieuDuAn.FirstOrDefault(_ => _.TNTIEUDA.ToLower() == i.TNTIEUDA.ToLower());
                                if (lx != null)
                                {
                                    i.Error = "Tiểu dự án này đã tồn tại trong hệ thống";
                                    ltError.Add(i);
                                    continue;
                                }
                            }
                        }
                        catch { }

                        db.tnTieuDuAns.InsertOnSubmit(objTDA);
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

        public class TieuDuAnItem
        {
            public string ZZTIEUDUAN { get; set; }
            public string TNTIEUDA { get; set; }
            public string TENTDA { get; set; }
            public byte MATN { get; set; }
            public string Error { get; set; }
        }
    }
}