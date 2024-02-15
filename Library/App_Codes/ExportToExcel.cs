using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using Microsoft.CSharp;

namespace Library
{
    public class ExportToExcel
    {
        static public bool exportDataToExcel(string tieude, DataTable dt)
        {
            var wait = DialogBox.WaitingForm();
            bool result = false;
            //khoi tao cac doi tuong Com Excel de lam viec
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");  
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet;
            Excel.Workbook xlBook;
            //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
            object missValue = System.Reflection.Missing.Value;
            //khoi tao doi tuong Com Excel moi
            xlBook = xlApp.Workbooks.Add(missValue);
            //su dung Sheet dau tien de thao tac
            xlSheet = (Excel.Worksheet)xlBook.Worksheets.Item[1];
            //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
            xlApp.Visible = false;
            int socot=dt.Columns.Count;
            int sohang=dt.Rows.Count;
            int i,j;

            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //set thuoc tinh cho tieu de
                xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
                Excel.Range caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                caption.Select();
                caption.FormulaR1C1 = tieude;
                //căn lề cho tiêu đề
                caption.HorizontalAlignment = Excel.Constants.xlCenter;
                caption.Font.Bold = true;
                caption.VerticalAlignment = Excel.Constants.xlCenter;
                caption.Font.Size = 15;
                //màu nền cho tiêu đề
                caption.Interior.ColorIndex = 20;
                caption.RowHeight = 30;
                //set thuoc tinh cho cac header
                Excel.Range header = xlSheet.get_Range("A2", Convert.ToChar(socot + 65) + "2");  
                header.Select();

                header.HorizontalAlignment = Excel.Constants.xlCenter;
                header.Font.Bold = true;
                header.Font.Size = 10;
                //điền tiêu đề cho các cột trong file excel
                for (i = 0; i < socot; i++)
                    xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                //dien cot stt
                xlSheet.Cells[2, 1] = "STT";
                for (i = 0; i < sohang; i++)
                    xlSheet.Cells[i + 3, 1] = i + 1;
                //dien du lieu vao sheet
                for (i = 0; i < sohang; i++)
                    for (j = 0; j < socot; j++)
                    {
                        xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];
                    }
                //autofit độ rộng cho các cột 
                for (i = 0; i < sohang; i++)
                    ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();

                //save file
                xlBook.SaveAs(f.FileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlBook.Close(true, missValue, missValue);
                xlApp.Quit();
                
                // release cac doi tuong COM
                releaseObject(xlSheet);
                releaseObject(xlBook);
                releaseObject(xlApp);
                result = true;
            }
            wait.Close();
            wait.Dispose();

            if (f.FileName.Trim().Length != 0)
            {
                DialogBox.Alert("Export thành công");  
                if (DialogBox.Question("Bạn có muốn mở file này không?") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(f.FileName);
                }
            }
            return result;
        }

        static public bool exportDataToExcel(DataTable dt)
        {
            var wait = DialogBox.WaitingForm();
            bool result = false;
            //khoi tao cac doi tuong Com Excel de lam viec
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");  
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet;
            Excel.Workbook xlBook;
            //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
            object missValue = System.Reflection.Missing.Value;
            //khoi tao doi tuong Com Excel moi
            xlBook = xlApp.Workbooks.Add(missValue);
            //su dung Sheet dau tien de thao tac
            xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
            //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
            xlApp.Visible = false;
            int socot = dt.Columns.Count;
            int sohang = dt.Rows.Count;
            int i, j;

            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //set thuoc tinh cho cac header
                Excel.Range header = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");  
                header.Select();

                header.HorizontalAlignment = Excel.Constants.xlCenter;
                header.Font.Bold = true;
                //điền tiêu đề cho các cột trong file excel
                for (i = 0; i < socot; i++)
                    xlSheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;
                //dien cot stt
                xlSheet.Cells[1, 1] = "STT";
                for (i = 0; i < sohang; i++)
                    xlSheet.Cells[i + 2, 1] = i + 1;
                //dien du lieu vao sheet
                for (i = 0; i < sohang; i++)
                    for (j = 0; j < socot; j++)
                    {
                        xlSheet.Cells[i + 2, j + 2] = dt.Rows[i][j];

                    }
                //autofit độ rộng cho các cột 
                for (i = 0; i < sohang; i++)
                    ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();

                //save file
                xlBook.SaveAs(f.FileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlBook.Close(true, missValue, missValue);
                xlApp.Quit();

                // release cac doi tuong COM
                releaseObject(xlSheet);
                releaseObject(xlBook);
                releaseObject(xlApp);
                result = true;
            }
            wait.Close();
            wait.Dispose();

            if (f.FileName.Trim().Length != 0)
            {
                DialogBox.Alert("Export thành công");  
                if (DialogBox.Question("Bạn có muốn mở file này không?") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(f.FileName);
                }
            }
            return result;
        }

        static  public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
               throw new  Exception("Exception Occured while releasing object " + ex);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
