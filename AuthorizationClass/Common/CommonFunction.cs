using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AuthorizationClass.Common
{
    public class CommonFunction
    {
        private static string VALID_CHARACTERS_EMAIL = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";

        /// <summary>        
        /// Check inputed string is right email format
        /// </summary>
        /// <param name="strEmailAddress">Email Address</param>
        /// <returns>true/false</returns>
        public static bool CheckEmailAddress(string strEmailAddress)
        {
            Match emailAddressMatch = Regex.Match(strEmailAddress, VALID_CHARACTERS_EMAIL);

            if (emailAddressMatch.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get datetime on server
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime GetServerDateTime()
        {
            dbAccess db = new dbAccess();
            db.CreateNewSqlCommand();
            DateTime dtServer = Convert.ToDateTime(db.ExecuteScalar("spGetSystemDate"));
            return dtServer;
        }
        /// <summary>
        /// Get Form name
        /// </summary>
        /// <param name="form">Form</param>
        /// <returns>Form name</returns>
        public string GetFormName(System.Windows.Forms.Form form)
        {
            return form.Name;
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
    }
}
