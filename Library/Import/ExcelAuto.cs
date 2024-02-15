
using System.Linq;

namespace Library.Import
{
    public static class ExcelAuto
    {
        public class GridviewInfo
        {
            public string FieldName { get; set; }
            public string Caption { get; set; }
        }

        public static System.Collections.Generic.List<GridviewInfo> GetGridviewInfo(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            try
            {
                System.Collections.Generic.List<GridviewInfo> gridViewInfo = new System.Collections.Generic.List<GridviewInfo>();
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView.Columns)
                {
                    gridViewInfo.Add(new GridviewInfo { Caption = col.Caption, FieldName = col.FieldName });
                }

                return gridViewInfo;
            }
            catch(System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
                return null;
            }
        }

        public static System.Collections.Generic.List<T> ConvertDataTable<T>(System.Data.DataTable dt)
        {
            try
            {
                System.Collections.Generic.List<T> data = new System.Collections.Generic.List<T>();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                return data;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
                return null;
            }
            
        }

        private static T GetItem<T>(System.Data.DataRow dr)
        {
            System.Type temp = typeof(T);
            T obj = System.Activator.CreateInstance<T>();
            try
            {
                

                foreach (System.Data.DataColumn column in dr.Table.Columns)
                {
                    foreach (System.Reflection.PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            //object value = dr[column.ColumnName];
                            //var targetType = IsNullableType(pro.PropertyType) ? System.Nullable.GetUnderlyingType(pro.PropertyType) : pro.PropertyType;

                            //if (targetType == null) continue;
                            //if (targetType.IsEnum)
                            //{
                            //    if (value != null) value = System.Enum.ToObject(targetType, value);
                            //}
                            //else
                            //    value = System.Convert.ChangeType(value, targetType);

                            var propType = System.Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType;
                            var safeValue = (dr[column.ColumnName] == null | dr[column.ColumnName] == "") ? null : System.Convert.ChangeType(dr[column.ColumnName], propType);

                            pro.SetValue(obj, safeValue, null);

                            //pro.SetValue(obj, value, null);
                        }

                        else
                            continue;
                    }
                }
                return obj;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
                return obj;
            }
            
        }

        private static bool IsNullableType(System.Type type)
        {
            try
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
                return false;
            }
            
        }

        public static System.Data.DataTable GetDataExcel(LinqToExcel.ExcelQueryFactory excel, DevExpress.XtraGrid.Views.Grid.GridView gv, DevExpress.XtraBars.BarEditItem sheet)
        {
            try
            {
                System.Collections.Generic.List<Library.Import.ExcelAuto.GridviewInfo> lGridViewInfo = Library.Import.ExcelAuto.GetGridviewInfo(gv);

                var lExcel = excel.Worksheet(sheet.EditValue.ToString()).ToList();

                System.Data.DataTable dt = new System.Data.DataTable();
                foreach (var column in lGridViewInfo)
                {
                    dt.Columns.Add(column.FieldName);
                }

                foreach (var row in lExcel)
                {
                    var r = dt.NewRow();
                    foreach (var column in lGridViewInfo)
                    {
                        r[column.FieldName] = row[column.Caption];
                    }
                    dt.Rows.Add(r);
                }

                return dt;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
                return null;
            }
            
        }

        public static System.Data.DataTable GetDataExcel(LinqToExcel.ExcelQueryFactory excel, DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            try
            {
                System.Collections.Generic.List<Library.Import.ExcelAuto.GridviewInfo> lGridViewInfo = Library.Import.ExcelAuto.GetGridviewInfo(gv);

                var lExcel = excel.Worksheet(0).ToList();

                System.Data.DataTable dt = new System.Data.DataTable();
                foreach (var column in lGridViewInfo)
                {
                    dt.Columns.Add(column.FieldName);
                }

                foreach (var row in lExcel)
                {
                    var r = dt.NewRow();
                    foreach (var column in lGridViewInfo)
                    {
                        r[column.FieldName] = row[column.Caption];
                    }
                    dt.Rows.Add(r);
                }

                return dt;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
                return null;
            }

        }
    }
}
