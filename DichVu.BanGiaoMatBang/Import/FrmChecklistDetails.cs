using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.BanGiaoMatBang.Import
{
    public partial class FrmChecklistDetails : XtraForm
    {
        public bool IsSave { get; set; }

        public FrmChecklistDetails()
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

                    System.Collections.Generic.List<ChecklistDetails> list = Library.Import.ExcelAuto.ConvertDataTable<ChecklistDetails>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new ChecklistDetails
                    //{
                    //    Name = _["Hạng mục kiểm tra"].ToString().Trim(),
                    //    GroupName = _["Nhóm"].ToString().Trim(),
                    //    Description=_["Mô tả"].ToString().Trim(),
                    //    Stt = _["Tầng"].ToString().Trim()
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        #region Excel tự động

        public class GridviewInfo
        {
            public string FieldName { get; set; }
            public string Caption { get; set; }
        }

        public System.Collections.Generic.List<GridviewInfo> GetGridviewInfo(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            System.Collections.Generic.List<GridviewInfo> gridViewInfo = new List<GridviewInfo>();
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView.Columns)
            {
                gridViewInfo.Add(new GridviewInfo { Caption = col.Caption, FieldName = col.FieldName });
            }

            return gridViewInfo;
        }

        private static List<T> ConvertDataTable<T>(System.Data.DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(System.Data.DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (System.Data.DataColumn column in dr.Table.Columns)
            {
                foreach (System.Reflection.PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        } 
        #endregion


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
                var lChecklistDetails = (List<ChecklistDetails>) gc.DataSource;
                var ltError = new List<ChecklistDetails>();
                foreach (var n in lChecklistDetails)
                {
                    try
                    {
                        db = new MasterDataContext();

                        var checklistDetail = new ho_ChecklistDetail();
                        checklistDetail.GroupName = n.GroupName;
                        checklistDetail.Name = n.Name;
                        checklistDetail.IsNotUse = false;
                        checklistDetail.Description = n.Description;
                        checklistDetail.Stt = n.Stt;
                        db.ho_ChecklistDetails.InsertOnSubmit(checklistDetail);

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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        
    }

    public class ChecklistDetails
    {
        // hạng mục chi tiết
        public string Name { get; set; } 

        // Nhóm
        public string GroupName { get; set; }

        // tầng
        public string Stt { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
    }
}