using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;
using EmailAmazon.API;

namespace EmailAmazon
{
  public static class MailCommon
  {
    public static int MaHD { get; set; }

    public static string MatKhau { get; set; }

    public static string TenNV { get; set; }

    public static APISoapClient cmd { get; set; }
  }
}
