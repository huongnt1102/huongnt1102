using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Lease.GanHetHan
{
    public partial class rptThaiNguyenLan1 : DevExpress.XtraReports.UI.XtraReport
    {
        //public string TenCT { set; get; }
        public string TenKH { set; get; }
        public string TenMatBang { set; get; }
        public string SoHD { set; get; }
        public string NgayHT { set; get; }
        public string ThangHT { set; get; }
        public string NamHT { set; get; }
        public string DateHL { set; get; }
        public string DateHH { set; get; }
        public string TenLMB { set; get; }
        public string TenTL { set; get; }
        public string SoThang { set; get; }
        public string SoPhuLuc { set; get; }
        public rptThaiNguyenLan1()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            //xrTableCell1.Text = xrTableCell1.Text.Replace("[CongTyQL]", CongTyQL);
            xrTableCell7.Text = xrTableCell7.Text.Replace("[SoHD]", SoHD);

            xrTableCell5.Text =
                xrTableCell5.Text
                    .Replace("[ngayHT]", NgayHT)
                    .Replace("[thangHT]", ThangHT)
                    .Replace("[namHT]", NamHT);
            //xrTableCell8.Text = xrTableCell8.Text.Replace("[namHH]", NamHH);
            //xrTableCell9.Text = xrTableCell9.Text.Replace("[thangHH]", ThangHH).Replace("[namHH]", NamHH);

            xrTableCell10.Text = xrTableCell10.Text.Replace("[TenKH]", TenKH);
            xrTableCell11.Text = xrTableCell11.Text.Replace("[MatBang]", TenMatBang)
                                                   .Replace("[LoaiMB]", TenLMB);

            xrTableCell12.Text =
                xrTableCell12.Text.Replace("[TenKH]", TenKH)
                    .Replace("[MatBang]", TenMatBang)
                    .Replace("[DateNgayHH]", DateHH)
                    .Replace("[TenTL]", TenTL)
                    .Replace("[LoaiMB]", TenLMB)
                    .Replace("[SoThang]", SoThang)
                    .Replace("[SoPhuLuc]", SoPhuLuc);
        }
    }
}
