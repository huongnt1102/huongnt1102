using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace BuildingDesignTemplate.Import
{
    public partial class FrmField : XtraForm
    {
        public bool IsSave { get; set; }

        public FrmField()
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
                    System.Collections.Generic.List<Fields> list = Library.Import.ExcelAuto.ConvertDataTable<Fields>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new Fields
                    //{
                    //    Name = _["Tên trường"].ToString().Trim(),
                    //    Description = _["Mô tả"].ToString().Trim(),
                    //    Field = _["Ký hiệu"].ToString().Trim(),
                    //    Symbol = _["Bảng"].ToString().Trim(),
                    //    GroupId = _["Mã nhóm"].Cast<int>(),
                    //    GroupSub = _["Nhóm con"].ToString().Trim()
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
                var lChecklistDetails = (List<Fields>) gc.DataSource;
                var ltError = new List<Fields>();
                foreach (var n in lChecklistDetails)
                {
                    try
                    {
                        db = new MasterDataContext();

                        var group = db.rptGroups.FirstOrDefault(_ => _.ID == n.GroupId);
                        if (group == null)
                        {
                            n.Error = "Mã nhóm không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var field = db.template_Fields.FirstOrDefault(_ => _.Field.ToLower() == n.Field.ToLower() & _.GroupId == group.ID);
                        if (field == null)
                        {
                            field = new Library.template_Field();
                            db.template_Fields.InsertOnSubmit(field);
                        }

                        field.Description = n.Description;
                        field.Field = n.Field;
                        field.GroupId = group.ID;
                        field.GroupSub = n.GroupSub;
                        field.Name = n.Name;
                        field.Symbol = n.Symbol;

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

                gc.DataSource = ltError.Count > 0 ? ltError : null;
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

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class Fields
    {
        // Mã trường (bắt buộc)
        public string Field { get; set; }

        // Mã table (nếu có)
        public string Symbol { get; set; }

        // Mã nhóm (bắt buộc)
        public int? GroupId { get; set; }

        // Tên nhóm con
        public string GroupSub { get; set; }

        // mô tả
        public string Description { get; set; }

        // Tên trường
        public string Name { get; set; }

        public string Error { get; set; }
    }
}