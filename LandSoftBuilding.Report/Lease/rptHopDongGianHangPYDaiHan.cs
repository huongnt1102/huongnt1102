using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using Library;

namespace LandSoftBuilding.Lease
{
    public partial class rptHopDongGianHangPYDaiHan : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHopDongGianHangPYDaiHan()
        {
            InitializeComponent();
        }
        public string SoHD { set; get; }
        public string NgayHT { set; get; }
        public string ThangHT { set; get; }
        public string NamHT { set; get; }
        public string TenKH { set; get; }
        public string DiaChiKH { set; get; }
        public string SoCMND { set; get; }
        public string NgayCapCMND { set; get; }
        public string NoiCapCMND { set; get; }
        public string DienThoaiKH { set; get; }
        public string MaSoThueKH { set; get; }
        public string DienTich { set; get; }
        public string NganhHang { set; get; }
        public string KhoiNha { set; get; }
        public string DiaChiDiemKD { set; get; }
        public string NgayBG { set; get; }
        public string ThangBG { set; get; }
        public string NamBG { set; get; }
        public string DateBG { set; get; }
        public string NgayThue { set; get; }
        public string ThangThue { set; get; }
        public string NamThue { set; get; }
        public string ThoiHan { set; get; }
        public string NgayHL { set; get; }
        public string NgayHH { set; get; }
        public string GiaChuaThue { set; get; }
        public string GiaChuaThueChu { set; get; }
        public string ThueVAT { set; get; }
        public string ThueVATChu { set; get; }
        public string GiaTriHĐ { set; get; }
        public string GiaTriHĐChu { set; get; }
        public byte? MaTN { set; get; }
        public void LoadData()
        {
            /*var db = new MasterDataContext();
            try
            {
                var objTN = (from tn in db.tnToaNhas
                    where tn.MaTN == this.MaTN
                    select new {tn.TenTN, tn.CongTyQuanLy, tn.Logo, tn.DiaChi})
                    .FirstOrDefault();
                picLogo.ImageUrl = objTN.Logo;
                xrRichText1.Text = xrRichText1.Text
                    .Replace("[SoHD]", SoHD)
                    .Replace("[NgayHT]", NgayHT)
                    .Replace("[ThangHT]", ThangHT)
                    .Replace("[NamHT]", NamHT)
                    .Replace("[TenKH]", TenKH)
                    .Replace("[DiaChiKH]", DiaChiKH)
                    .Replace("[SoCMND]", SoCMND)
                    .Replace("[NgayCapCMND]", NgayCapCMND)
                    .Replace("[NoiCapCMND]", NoiCapCMND)
                    .Replace("[DienThoaiKH]", DienThoaiKH)
                    .Replace("[MaSoThueKH]", MaSoThueKH)
                    .Replace("[DienTich]", DienTich)
                    .Replace("[NganhHang]", NganhHang)
                    .Replace("[KhoiNha]", KhoiNha)
                    .Replace("[DiaChiDiemKD]", DiaChiDiemKD)
                    .Replace("[NgayBG]", NgayBG)
                    .Replace("[ThangBG]", ThangBG)
                    .Replace("[NamBG]", NamBG)
                    .Replace("[DateBG]", DateBG)
                    .Replace("[NgayThue]", NgayThue)
                    .Replace("[ThangThue]", ThangThue)
                    .Replace("[NamThue]", NamThue)
                    .Replace("[ThoiHan]", ThoiHan)
                    .Replace("[NgayHL]", NgayHL)
                    .Replace("[NgayHH]", NgayHH)
                    .Replace("[GiaChuaThue]", GiaChuaThue)
                    .Replace("[GiaChuaThueChu]", GiaChuaThueChu)
                    .Replace("[ThueVAT]", ThueVAT)
                    .Replace("[ThueVATChu]", ThueVATChu)
                    .Replace("[Gia]", GiaTriHĐ)
                    .Replace("[GiaChu]", GiaTriHĐChu);
            }
            catch
            {
                
            }*/
        }

        private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {var db = new MasterDataContext();
            try
            {
                var objTN = (from tn in db.tnToaNhas
                    where tn.MaTN == this.MaTN
                    select new {tn.TenTN, tn.CongTyQuanLy, tn.Logo, tn.DiaChi})
                    .FirstOrDefault();
                picLogo.ImageUrl = objTN.Logo;
                XRRichText richText = (XRRichText) sender;
                //richText.LoadFile("Hợp đồng gian hàng PY dài hạn.docx");
                using (RichEditDocumentServer docServer = new RichEditDocumentServer())
                {

                    docServer.RtfText = richText.Rtf;
                    docServer.Document.HtmlText = docServer.Document.HtmlText
                        .Replace("[SoHD]", SoHD)
                    .Replace("[NgayHT]", NgayHT)
                    .Replace("[ThangHT]", ThangHT)
                    .Replace("[NamHT]", NamHT)
                    .Replace("[TenKH]", TenKH)
                    .Replace("[DiaChiKH]", DiaChiKH)
                    .Replace("[SoCMND]", SoCMND)
                    .Replace("[NgayCapCMND]", NgayCapCMND)
                    .Replace("[NoiCapCMND]", NoiCapCMND)
                    .Replace("[DienThoaiKH]", DienThoaiKH)
                    .Replace("[MaSoThueKH]", MaSoThueKH)
                    .Replace("[DienTich]", DienTich)
                    .Replace("[NganhHang]", NganhHang)
                    .Replace("[KhoiNha]", KhoiNha)
                    .Replace("[DiaChiDiemKD]", DiaChiDiemKD)
                    .Replace("[NgayBG]", NgayBG)
                    .Replace("[ThangBG]", ThangBG)
                    .Replace("[NamBG]", NamBG)
                    .Replace("[DateBG]", DateBG)
                    .Replace("[NgayThue]", NgayThue)
                    .Replace("[ThangThue]", ThangThue)
                    .Replace("[NamThue]", NamThue)
                    .Replace("[ThoiHan]", ThoiHan)
                    .Replace("[NgayHL]", NgayHL)
                    .Replace("[NgayHH]", NgayHH)
                    .Replace("[GiaChuaThue]", GiaChuaThue)
                    .Replace("[GiaChuaThueChu]", GiaChuaThueChu)
                    .Replace("[ThueVAT]", ThueVAT)
                    .Replace("[ThueVATChu]", ThueVATChu)
                    ;
                    docServer.Document.HtmlText = docServer.Document.HtmlText
                        .Replace("[Gia]", GiaTriHĐ)
                        .Replace("[GiaChu]", GiaTriHĐChu);
                    docServer.Document.DefaultCharacterProperties.FontName = "Times New Roman";
                    docServer.Document.DefaultCharacterProperties.FontSize = 12;
                    richText.Rtf = docServer.RtfText;
                }
            }
            catch
            {

            }
        }
        

    }
}
