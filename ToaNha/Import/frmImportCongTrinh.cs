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
    public partial class frmImportCongTrinh : DevExpress.XtraEditors.XtraForm
    {
        public frmImportCongTrinh()
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

                System.Collections.Generic.List<CongTrinhItem> list = Library.Import.ExcelAuto.ConvertDataTable<CongTrinhItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

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
                var ltSource = (List<CongTrinhItem>)gcImport.DataSource;
                var ltError = new List<CongTrinhItem>();

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

                        if (i.ZZCONGTRIN == null)
                        {
                            i.Error = "Vui lòng nhập mã công trình";
                            ltError.Add(i);
                            continue;
                        }
                        #endregion

                        var objTieuDuAn = db.tnTieuDuAns.FirstOrDefault(_ => _.ZZTIEUDUAN == i.ZZTIEUDUAN);
                        if (objTieuDuAn == null)
                        {
                            i.Error = "Tiểu dự án không tồn tại";
                            ltError.Add(i);
                            continue;
                        }
                        
                        var obj = new tnCongTrinh();
                        obj.MaTieuDuAn = objTieuDuAn.ID;
                        obj.ZZCONGTRIN = i.ZZCONGTRIN;
                        obj.TNCONGTRIN = i.TNCONGTRIN;
                        obj.TENCTRINH = i.TENCTRINH;
                        
                        try
                        {
                            if (i.ZZCONGTRIN!= "")
                            {
                                var lx = db.tnCongTrinhs.FirstOrDefault(_ => _.ZZCONGTRIN.ToLower() == i.ZZCONGTRIN.ToLower());
                                if (lx != null)
                                {
                                    i.Error = "Công trình này đã tồn tại";
                                    ltError.Add(i);
                                    continue;
                                }
                            }
                        }
                        catch { }

                        db.tnCongTrinhs.InsertOnSubmit(obj);
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

        public class CongTrinhItem
        {
            public string ZZCONGTRIN { get; set; }
            public string TNCONGTRIN { get; set; }
            public string TENCTRINH { get; set; }
            public string ZZTIEUDUAN { get; set; }
            public string Error { get; set; }
        }
    }
}