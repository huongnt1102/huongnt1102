using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Library;
using DevExpress.XtraPrinting.BarCode;
using QRCoder;
using System.Linq;
using BarcodeLib;
using System.Drawing.Imaging;
using System.Data;

namespace Building.Asset.BaoCao
{
    public partial class rptQRTaiSan : DevExpress.XtraReports.UI.XtraReport
    {
       
        public rptQRTaiSan(List<long> TS)
        {
            InitializeComponent();
            lbQR.DataBindings.Add("Text", null, "TenChiTietTaiSan");
            QRCode1.DataBindings.Add("Text", null, "ID");
            MasterDataContext db = new MasterDataContext();
                var QRCode = (from tx in db.tbl_ChiTietTaiSans
                                 where TS.Contains(tx.ID)
                                 select new
                                 {
                                     tx.ID,
                                     tx.TenChiTietTaiSan
                                 }).ToList();
            if (QRCode.Count <= 0)
            {
                this.DataSource = null;
            }
            else
            {
                this.DataSource = QRCode;
            }
        }
       
    }
}
