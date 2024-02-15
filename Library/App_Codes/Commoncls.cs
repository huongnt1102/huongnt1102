using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Export;

namespace Library
{
    public class Commoncls
    {
        public static string EncodeString(string inputString)
        {
            CspParameters parameters = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(0x400, parameters);
            CspParameters parameters2 = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider2 = new RSACryptoServiceProvider();
            provider2.FromXmlString("<RSAKeyValue><Modulus>rxZwQi8PwO9vGKVxGFTzuehApb0MpO92N/HOAMe0Ib7VkS6++gDtrFiotHWPzUjUklKa2hJjmG+6Sh74c+iwJpU7dQGRxvoXYuF+m9r4lyGzXTrRP4Wt16SmbF8Pm6jaw9JPu1Xy+8sVBxYq8B5jyI5aaZ7aKvSBuJGLMtv/wcE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");  
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            int length = bytes.Length;
            int num2 = length / 0x56;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <= num2; i++)
            {
                byte[] dst = new byte[((length - (0x56 * i)) > 0x56) ? 0x56 : (length - (0x56 * i))];
                Buffer.BlockCopy(bytes, 0x56 * i, dst, 0, dst.Length);
                byte[] array = provider2.Encrypt(dst, true);
                Array.Reverse(array);
                builder.Append(Convert.ToBase64String(array));
            }
            return builder.ToString();
        }

        public static string DecodeString(string inputString)
        {
            CspParameters parameters = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString("<RSAKeyValue><Modulus>rxZwQi8PwO9vGKVxGFTzuehApb0MpO92N/HOAMe0Ib7VkS6++gDtrFiotHWPzUjUklKa2hJjmG+6Sh74c+iwJpU7dQGRxvoXYuF+m9r4lyGzXTrRP4Wt16SmbF8Pm6jaw9JPu1Xy+8sVBxYq8B5jyI5aaZ7aKvSBuJGLMtv/wcE=</Modulus><Exponent>AQAB</Exponent><P>5nR8EplxlG0uPVGorn8OkMXZ9TF7BPa5wZs1vL4JPsxZv8D+UjufUsGrHOQmZRxvFe4J/1/iZI/6m+nHOcFk1w==</P><Q>wn7R12szMYoIMFN8UEXcEmamO7PSELqhV+qe9a/7N6G1pKG1xU3AZpkfW0E/GJZGl7pA9UQNQZTxS/LSv0AjJw==</Q><DP>inrSl4aXBp6422X3W6vDv+D0AO+Twb7Ujm9K0jjLa232PFCnQhjLuznfLcQ3Aikc42ufnFIsw0r1R70p1x3MDw==</DP><DQ>lYaKLOLtaJiF0yFb4RrUJhFkm2GTjejtQXnO23N/3zUjQH5SEG3GDRqLUMzIhU6C1wMKDYVT66dmGs2D2CSm4Q==</DQ><InverseQ>eXW6RmvwuAoo52IAnv9dBq+ixrZqhDKyFRYusjuUpFggPw7A4OknUNwJtCHeQecOCmKNTo0T+AmGfq530XnDqg==</InverseQ><D>RTclocRhAfClhqTAlNHgl/nMtLiLqxhPL8aTnZNVDpIWc5J7RPHhA2T5LH3dH1ZPUpj9RoBGhxiEGJEtvwSZvb76txmEXaUlou0ZZveeJe7O+crWT70dn06Qz+Ua7F6uwpVCQr7VmTEY4qXFowvrdH8Haz/2uHM+FFpv/1idD9E=</D></RSAKeyValue>");  
            int num = inputString.Length / 0xac;
            ArrayList list = new ArrayList();
            for (int i = 0; i < num; i++)
            {
                byte[] array = Convert.FromBase64String(inputString.Substring(0xac * i, 0xac));
                Array.Reverse(array);
                list.AddRange(provider.Decrypt(array, true));
            }
            return Encoding.UTF32.GetString(list.ToArray(Type.GetType("System.Byte")) as byte[]);
        }

        public static string HashPassword(string OriginalString)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(OriginalString);
            encodedBytes = md5.ComputeHash(originalBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encodedBytes.Length; i++)
            {
                sb.Append(encodedBytes[i].ToString("X2"));
            }

            //Convert encoded bytes back to a 'readable' string
            return sb.ToString();
        }

        public static string TiegVietKhongDau(string str)
        {
            string[,] arr = new string[14, 18]; //Tạo mảng có 14 hàng và 18 cột, mỗi hàng chứa các ký tự cùng nhóm
            string chuoi;
            string Thga, Thge, Thgo, Thgu, Thgi, Thgd, Thgy;
            string HoaA, HoaE, HoaO, HoaU, HoaI, HoaD, HoaY;
            chuoi = "aAeEoOuUiIdDyY";
            Thga = "áàạảãâấầậẩẫăắằặẳẵ";
            HoaA = "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ";
            Thge = "éèẹẻẽêếềệểễeeeeee";
            HoaE = "ÉÈẸẺẼÊẾỀỆỂỄEEEEEE";
            Thgo = "óòọỏõôốồộổỗơớờợởỡ";
            HoaO = "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ";
            Thgu = "úùụủũưứừựửữuuuuuu";
            HoaU = "ÚÙỤỦŨƯỨỪỰỬỮUUUUUU";
            Thgi = "íìịỉĩiiiiiiiiiiii";
            HoaI = "ÍÌỊỈĨIIIIIIIIIIII";
            Thgd = "đdddddddddddddddd";
            HoaD = "ĐDDDDDDDDDDDDDDDD";
            Thgy = "ýỳỵỷỹyyyyyyyyyyyy";
            HoaY = "ÝỲỴỶỸYYYYYYYYYYYY";

            //Nạp vào trong Mảng các ký tự
            //Nạp vào từng đầu hàng các ký tự không dấu
            //Nạp vào cột đầu tiên
            for (byte i = 0; i < 14; i++)
                arr[i, 0] = chuoi.Substring(i, 1);

            //Nạp vào từng ô các ký tự có dấu
            for (byte j = 1; j < 18; j++)
                for (byte i = 1; i < 18; i++)
                {
                    arr[0, i] = Thga.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thga vào từng ô trong hàng 0
                    arr[1, i] = HoaA.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaA vào từng ô trong  hàng 1
                    arr[2, i] = Thge.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thge vào từng ô trong  hàng 2
                    arr[3, i] = HoaE.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaE vào từng ô trong  hàng 3
                    arr[4, i] = Thgo.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgo vào từng ô trong  hàng 4
                    arr[5, i] = HoaO.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaO vào từng ô trong  hàng 5
                    arr[6, i] = Thgu.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgu vào từng ô trong  hàng 6
                    arr[7, i] = HoaU.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaU vào từng ô trong  hàng 7
                    arr[8, i] = Thgi.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgi vào từng ô trong  hàng 8
                    arr[9, i] = HoaI.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaI vào từng ô trong  hàng 9
                    arr[10, i] = Thgd.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgd vào từng ô trong  hàng 10
                    arr[11, i] = HoaD.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaD vào từng ô trong  hàng 11
                    arr[12, i] = Thgy.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgy vào từng ô trong  hàng 12
                    arr[13, i] = HoaY.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaY vào từng ô trong  hàng 13
                }

            //Tiến hành thay thế
            for (byte j = 0; j < 14; j++)
                for (byte i = 1; i < 18; i++)
                    str = str.Replace(arr[j, i], arr[j, 0]);

            return str;
        }

        public static string TiegVietKhongDauURL(string str)
        {
            str = TiegVietKhongDau(str);
            string KyTuDacBiet = "~!@#$%^&*()+=|\\'\",.?/:;`";
            for (byte i = 0; i < KyTuDacBiet.Length; i++)
            {
                str = str.Replace(KyTuDacBiet.Substring(i, 1), "");  
            }
            str = str.Replace(" ", "-");  
            str = str.Replace("--", "-");  
            return str.ToLower();
        }

        public static string GetMonth(int month)
        {
            switch (month)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return "";
            }
        }
        
        /// <summary>
        /// GetDateString
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="cate">
        /// 1: Từ ngày 01/{0}/{1} - {2}/{0}/{1} | 
        /// 2: Từ/from 01/{0}/{1} đến/to {2}/{0}/{1}
        /// 3: Từ (From) 24/{0}/{1} đến (to) 24/{0}/{1}
        /// </param>
        /// <returns></returns>
        public static string GetDateString(int month, int year, byte cate)
        {
            switch (cate)
            {
                case 2:
                    return string.Format("Từ/from 01/{0}/{1} đến/to {2}/{0}/{1}", month < 10 ? ("0" + month.ToString()) : month.ToString(), year, GetDayString(month, year));
                case 3:
                    int monthFrom = GetMonthFrom(month);
                    int monthTo = GetMonthTo(month);

                    return string.Format("Từ (From) 24/{0}/{1} đến (to) 24/{2}/{3}", monthFrom < 10 ? ("0" + monthFrom.ToString()) : monthFrom.ToString(), GetYearFrom(month, year), monthTo < 10 ? ("0" + monthTo.ToString()) : monthTo.ToString(), GetYearTo(month, year));
                case 4:
                    return string.Format("Từ (From) 01/{0}/{1} đến (to) {2}/{0}/{1}", month, year, DateTime.DaysInMonth(year, month));
                default:
                    return string.Format("Từ ngày 01/{0}/{1} - {2}/{0}/{1}", month < 10 ? ("0" + month.ToString()) : month.ToString(), year, GetDayString(month, year));
            }
        }

        public static int GetMonthFrom(int month)
        {
            switch (month)
            {
                case 1: return 11;
                case 2: return 12;
                case 3: return 1;
                case 4: return 2;
                case 5: return 3;
                case 6: return 4;
                case 7: return 5;
                case 8: return 6;
                case 9: return 7;
                case 10: return 8;
                case 11: return 9;
                case 12: return 10;
                default: return 0;
            }
        }

        public static int GetYearFrom(int month, int year)
        {
            switch (month)
            {
                case 1: return year - 1;
                case 2: return year - 1;
                default: return year;
            }
        }

        public static int GetMonthTo(int month)
        {
            switch (month)
            {
                case 1: return 12;
                case 2: return 1;
                case 3: return 2;
                case 4: return 3;
                case 5: return 4;
                case 6: return 5;
                case 7: return 6;
                case 8: return 7;
                case 9: return 8;
                case 10: return 9;
                case 11: return 10;
                case 12: return 11;
                default: return 0;
            }
        }

        public static int GetYearTo(int month, int year)
        {
            switch (month)
            {
                case 1: return year - 1;
                case 11: return year - 1;
                case 12: return year - 1;
                default: return year;
            }
        }

        public static string GetDateEndString(int month, int year)
        {
            int monthFrom = month - 2;
            int monthTo = month - 1;
            return string.Format("Từ/from {0}/{1}/{2} đến/to {3}/{4}/{5}", GetDayString(monthFrom, year), monthFrom < 10 ? ("0" + monthFrom.ToString()) : monthFrom.ToString(), monthFrom <= 0 ? year - 1 : year, GetDayString(monthTo, year), monthTo < 10 ? ("0" + monthTo.ToString()) : monthTo.ToString(), year);
        }

        public static string GetDateEndStringV2(int month, int year)
        {
            int monthFrom = month - 2;
            int monthTo = month - 1;
            return string.Format("từ ngày {0}/{1}/{2} - {3}/{4}/{5}", GetDayString(monthFrom, year), monthFrom < 10 ? ("0" + monthFrom.ToString()) : monthFrom.ToString(), monthFrom <= 0 ? year - 1 : year, GetDayString(monthTo, year), monthTo < 10 ? ("0" + monthTo.ToString()) : monthTo.ToString(), year);
        }

        public static string GetDayString(int month, int year)
        {
            int day = DateTime.DaysInMonth(year, month);
            return day < 10 ? ("0" + day.ToString()) : day.ToString();
        }

        public static string GetMonthEnglish(int _Month)
        {
            switch (_Month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                default: return "December";
            }
        }

        public static void ExportExcel(DevExpress.XtraPivotGrid.PivotGridControl pv)
        {
            ExportSettings.DefaultExportType = ExportType.WYSIWYG;
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "Excel|*.xlsx";
            frm.ShowDialog();
            if (frm.FileName != "")
            {
                pv.ExportToXlsx(frm.FileName);
                if (DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == DialogResult.Yes)
                    System.Diagnostics.Process.Start(frm.FileName);
            }
        }

        public static void ExportExcel(DevExpress.XtraGrid.GridControl gc)
        {
            ExportSettings.DefaultExportType = ExportType.WYSIWYG;
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "Excel|*.xlsx";
            frm.ShowDialog();
            if (frm.FileName != "")
            {
                try
                {
                    gc.ExportToXlsx(frm.FileName);
                    if (DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == DialogResult.Yes)
                        System.Diagnostics.Process.Start(frm.FileName);
                }
                catch (Exception ex)
                {

                    DialogBox.Alert(ex.Message);
                }
               
                
            }
        }

        public static void ExportExcel(DevExpress.XtraReports.UI.XtraReport rpt)
        {
            ExportSettings.DefaultExportType = ExportType.WYSIWYG;
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "Excel|*.xlsx";
            frm.ShowDialog();
            if (frm.FileName != "")
            {
                rpt.ExportToXlsx(frm.FileName);
                if (DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == DialogResult.Yes)
                    System.Diagnostics.Process.Start(frm.FileName);
            }
        }

        public static DataTable Table(string str)
        {
            SqlConnection SqlConn = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter Ad;
            DataTable Dt = new DataTable();
            try
            {
                Ad = new SqlDataAdapter(str, SqlConn);
                SqlConn.Open();
                Ad.Fill(Dt);
                SqlConn.Close();
            }
            catch
            {
                SqlConn.Close();
            }
            return Dt;
        }   
    }
}
