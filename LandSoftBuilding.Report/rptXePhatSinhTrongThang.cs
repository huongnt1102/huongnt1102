﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.Data.PivotGrid;

namespace LandSoftBuilding.Report
{
    public partial class rptXePhatSinhTrongThang : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        MasterDataContext db = new MasterDataContext();

        public rptXePhatSinhTrongThang(DateTime _TuNgay, DateTime _DenNgay, byte _MaTN)
        {
            InitializeComponent();
            cThoiGian.Text = string.Format("Từ ngày {0:dd/MM/yyy} - Đến ngày {1:dd/MM/yyy}", _TuNgay, _DenNgay);
            cTieuDeSoThang.Text = string.Format("Số tháng làm tròn tính đến ngày {0:dd/MM/yyy}", _DenNgay);
            this.MaTN = _MaTN;

            #region DataBindings
            cSTT.DataBindings.Add("Text", null, "STT");
            cMatBang.DataBindings.Add("Text", null, "MaSoMB");
            cLoaiMB.DataBindings.Add("Text", null, "TenLMB");
            cSoThe.DataBindings.Add("Text", null, "SoThe");
            cNgayDangKy.DataBindings.Add("Text", null, "NgayDK","{0:dd/MM/yyyy}");
            cLoaiXe.DataBindings.Add("Text", null, "TenLX");
            cPhiGiuXe.DataBindings.Add("Text", null, "GiaThang", "{0:#,0.##}");
            cBienSo.DataBindings.Add("Text", null, "BienSo");
            cSoThang.DataBindings.Add("Text", null, "SoThang");
            #endregion

            var objTheXe = (from tx in db.dvgxTheXes
                            where SqlMethods.DateDiffDay(_TuNgay, tx.NgayDK) >= 0
                            & SqlMethods.DateDiffDay(tx.NgayDK, _DenNgay) >= 0
                            orderby tx.NgayDK
                            select new
                            {
                                tx.mbMatBang.MaSoMB,
                                db.mbLoaiMatBangs.SingleOrDefault(p1 => p1.MaLMB == tx.mbMatBang.MaLMB).TenLMB,
                                tx.SoThe,
                                tx.NgayDK,
                                tx.dvgxLoaiXe.TenLX,
                                tx.GiaThang,
                                tx.BienSo,
                                SoThang = tx.NgayDK.Value.Day > 15 ?  0.5 : 1,
                            }).ToList()
                              .AsEnumerable()
                              .Select((p, Index) => new
                              {
                                  STT = Index +1,
                                  p.MaSoMB,
                                  p.TenLMB,
                                  p.SoThe,
                                  p.NgayDK,
                                  p.TenLX,
                                  p.GiaThang,
                                  p.BienSo,
                                  p.SoThang,
                              }).ToList();
            this.DataSource = objTheXe;
        }

        double TinhSoThang(DateTime? NgayDK, DateTime _DenNgay)
        {
            double SoThang = (double)SqlMethods.DateDiffMonth(NgayDK, _DenNgay);
            SoThang = SoThang == 0 ? SoThang : SoThang - 1;
            SoThang = NgayDK.Value.Day > 15 ? SoThang + 0.5 : SoThang + 1;
            return SoThang;
        }
    }
}
