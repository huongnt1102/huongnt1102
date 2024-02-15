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

namespace DichVu.MatBang.PhanNhom
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public int MaLMB { get; set; }
        public int MaLDV { get; set; }
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
                gcImport.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new ImporItem
                {
                    MaSoMB = p["Mã mặt bằng"].ToString().Trim()
                }).ToList();

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
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 orderby mb.MaSoMB
                                 select new { mb.MaMB, mb.MaSoMB }).ToList();
                var ltPhanNhom = (from pl in db.mbPhanNhoms
                                  join mb in db.mbMatBangs on pl.MaMB equals mb.MaMB
                                  where pl.MaLMB == this.MaLMB & pl.MaLDV == this.MaLDV
                                  orderby mb.MaSoMB
                                  select mb.MaSoMB.ToLower())
                                 .ToList();

                var ltSource = (List<ImporItem>)gcImport.DataSource;
                var ltError = new List<ImporItem>();

                foreach(var i in ltSource)
                {
                    try
                    {
                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB.ToLower() == i.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            i.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }

                        if (ltPhanNhom.IndexOf(i.MaSoMB.ToLower()) >= 0)
                        {
                            i.Error = "Mặt bằng đã tồn tại trong nhóm";
                            ltError.Add(i);
                            continue;
                        }

                        var objPN = new mbPhanNhom();
                        objPN.MaLMB = this.MaLMB;
                        objPN.MaLDV = this.MaLDV;
                        objPN.MaMB = objMB.MaMB;
                        db.mbPhanNhoms.InsertOnSubmit(objPN);

                        ltPhanNhom.Add(i.MaSoMB.ToLower());
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                db.SubmitChanges();

                this.isSave = true;

                if (ltError.Count > 0)
                {
                    DialogBox.Alert(string.Format("Có {0:n0} dòng xảy ra lỗi", ltError.Count));
                     gcImport.DataSource = ltError;
                }
                else
                {
                    DialogBox.Success();
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

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcImport);
        }
    }

    public class ImporItem
    {
        public string MaSoMB { get; set; }
        public string Error { get; set; }
    }
}