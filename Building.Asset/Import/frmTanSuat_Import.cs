using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmTanSuat_Import : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmTanSuat_Import()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());
                    System.Collections.Generic.List<TanSuat> list = Library.Import.ExcelAuto.ConvertDataTable<TanSuat>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new TanSuat
                    //{
                    //    TenTanSuat = _["Tên Tần Suất"].ToString().Trim(),
                    //    SoNgay = _["Số Ngày"].Cast<int>(),
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
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
                var objCaTruc = (List<TanSuat>)gc.DataSource;
                var ltError = new List<TanSuat>();
                foreach (var n in objCaTruc)
                {
                    try
                    {
                        db = new MasterDataContext();
                        var kt = db.tbl_TanSuats.FirstOrDefault(_ => _.TenTanSuat == n.TenTanSuat.ToLower());
                        if (kt != null)
                        {
                            kt.TenTanSuat = n.TenTanSuat;
                            kt.SoNgay = n.SoNgay;
                        }
                        else
                        {
                            var objCt = new tbl_TanSuat();
                            objCt.TenTanSuat = n.TenTanSuat;
                            objCt.SoNgay = n.SoNgay;
                            db.tbl_TanSuats.InsertOnSubmit(objCt);
                        }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
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
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
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

        private void frmCaTruc_Import_Load(object sender, EventArgs e)
        {

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class TanSuat
    {
        public string TenTanSuat { get; set; }
        public int? SoNgay { get; set; }
        public string Error { get; set; }
    }
}