using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.SqlClient;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class rptBaoCaoKhNoPhiDichVuToanCongTy : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime _TuNgay;
        private DateTime _DenNgay;
        private string strTN;
        public rptBaoCaoKhNoPhiDichVuToanCongTy(DateTime TuNgay,DateTime DenNgay,string str)
        {
            InitializeComponent();
            _TuNgay = TuNgay;
            _DenNgay = DenNgay;
            strTN = str;
     
           
            lbThang.Text = String.Format("Tháng {0} - {1} ",_DenNgay.Month,DenNgay.Year );
            lbTuyBien.Text = String.Format("CÒN PHẢI THU (ĐẾN {0:MM/yyyy})(KHÁCH NỢ)",_TuNgay.AddDays(-1));
            GroupHeader1.GroupFields.Add(new GroupField("TenTN", XRColumnSortOrder.Ascending));
            cTenCongTy.DataBindings.Add("Text", null, "TenTN");
            cSTT.DataBindings.Add("Text", null, "STT", "{0:#,0.##}");
            cKH.DataBindings.Add("Text", null, "TenKH");
            cDauKy.DataBindings.Add("Text", null, "SoTienDK", "{0:#,0.##}");
            cTienDien.DataBindings.Add("Text", null, "Dien", "{0:#,0.##}");
            cTienNuoc.DataBindings.Add("Text", null, "Nuoc", "{0:#,0.##}");
            cTienXe.DataBindings.Add("Text", null, "Xe", "{0:#,0.##}");
            cNgoaiGio.DataBindings.Add("Text", null, "NgoaiGio", "{0:#,0.##}");
            cPhaiThu.DataBindings.Add("Text", null, "SoTienPhaiThu", "{0:#,0.##}");
            cDaThu.DataBindings.Add("Text", null, "SoTienDaThu", "{0:#,0.##}");
            cConPhaiThu.DataBindings.Add("Text", null, "PhaiThuConLai", "{0:#,0.##}");

            cSumCongTyConPhaiThu.DataBindings.Add("Text", null, "PhaiThuConLai", "{0:#,0.##}");
            cSumCongTyConPhaiThu.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyDaThu.DataBindings.Add("Text", null, "SoTienDaThu", "{0:#,0.##}");
            cSumCongTyDaThu.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyDauKy.DataBindings.Add("Text", null, "SoTienDK", "{0:#,0.##}");
            cSumCongTyDauKy.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyTienDien.DataBindings.Add("Text", null, "Dien", "{0:#,0.##}");
            cSumCongTyTienDien.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyTienNgoaiGio.DataBindings.Add("Text", null, "NgoaiGio", "{0:#,0.##}");
            cSumCongTyTienNgoaiGio.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyTienNuoc.DataBindings.Add("Text", null, "Nuoc", "{0:#,0.##}");
            cSumCongTyTienNuoc.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyTienXe.DataBindings.Add("Text", null, "Xe", "{0:#,0.##}");
            cSumCongTyTienXe.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCongTyTongPhaiThu.DataBindings.Add("Text", null, "SoTienPhaiThu", "{0:#,0.##}");
            cSumCongTyTongPhaiThu.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");
           
            //------------------------------------------------------------
            
            cSumDaThu.DataBindings.Add("Text", null, "SoTienDaThu", "{0:#,0.##}");
            cSumDaThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumDK.DataBindings.Add("Text", null, "SoTienDK", "{0:#,0.##}");
            cSumDK.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumDien.DataBindings.Add("Text", null, "Dien", "{0:#,0.##}");
            cSumDien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumNgoaiGio.DataBindings.Add("Text", null, "NgoaiGio", "{0:#,0.##}");
            cSumNgoaiGio.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumNuoc.DataBindings.Add("Text", null, "Nuoc", "{0:#,0.##}");
            cSumNuoc.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumXe.DataBindings.Add("Text", null, "Xe", "{0:#,0.##}");
            cSumXe.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumConPhaiThu.DataBindings.Add("Text", null, "PhaiThuConLai", "{0:#,0.##}");
            cSumConPhaiThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumPhaiThu.DataBindings.Add("Text", null, "SoTienPhaiThu", "{0:#,0.##}");
            cSumPhaiThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltToaNha = this.GetToaNha();

                #region//Số dư đầu kỳ

                var DK = (from hd in db.dvHoaDons
                             where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true
                             
                               & (hd.MaLDV == 35 | hd.MaLDV == (int)MaLDVs.Nuoc | hd.MaLDV == (int)MaLDVs.PGX  | hd.MaLDV == (int)MaLDVs.Dien )
                             group hd by new { hd.MaTN, hd.MaKH, hd.MaMB } into gr
                             select new
                             {

                                 gr.Key.MaTN,
                                 gr.Key.MaKH,
                                 gr.Key.MaMB,
                                
                                 Dien = (decimal?)0,
                                 Nuoc = (decimal?)0,
                                 Xe = (decimal?)0,
                                 NgoaiGio = (decimal?)0,
                                 SoTienDK = (decimal?)gr.Sum(p => p.PhaiThu- (from ct in db.ptChiTietPhieuThus
                                                                               join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                                               where ct.TableName == "dvHoaDon" & ct.LinkID == p.ID & SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0
                                                                               select ct.SoTien).Sum().GetValueOrDefault()).GetValueOrDefault(),


                                 SoTienPhaiThu = (decimal?)0,
                                 SoTienDaThu = (decimal?)0
                             }).ToList();
                #endregion
                #region//Phát sinh trong kỳ
                var PhaiThu = (from hd in db.dvHoaDons
                                       where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.IsDuyet == true
                                              & (hd.MaLDV == 35 | hd.MaLDV == (int)MaLDVs.Nuoc | hd.MaLDV == (int)MaLDVs.PGX | hd.MaLDV == (int)MaLDVs.Dien)
                      
                               group hd by new { hd.MaTN, hd.MaKH, hd.MaMB } into gr
                                       select new
                                       {

                                           gr.Key.MaTN,
                                           gr.Key.MaKH,
                                           gr.Key.MaMB,

                                           Dien = (decimal?)gr.Sum(p => p.MaLDV == 8 & SqlMethods.DateDiffMonth(_TuNgay, p.NgayTT) == 0 ? p.PhaiThu : 0),
                                           Nuoc = (decimal?)gr.Sum(p => p.MaLDV == 9 & SqlMethods.DateDiffMonth(_TuNgay, p.NgayTT) == 0 ? p.PhaiThu : 0),
                                           Xe = (decimal?)gr.Sum(p => p.MaLDV == 6 & SqlMethods.DateDiffMonth(_TuNgay, p.NgayTT) == 0 ? p.PhaiThu : 0),
                                           NgoaiGio = gr.Sum(p => p.MaLDV == 35 & SqlMethods.DateDiffMonth(_TuNgay, p.NgayTT) == 0 ? p.PhaiThu : 0),
                                           SoTienDK = (decimal?)0,
                                           SoTienPhaiThu = (decimal?)gr.Sum(p => p.PhaiThu).GetValueOrDefault(),
                                           SoTienDaThu = (decimal?)0,
                                       }).ToList();
                #endregion
                #region//Đã thu trong kỳ
                var DaThu = (from ct in db.ptChiTietPhieuThus
                             join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                             join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                             where ltToaNha.Contains(pt.MaTN) & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                    & (hd.MaLDV == 35 | hd.MaLDV == (int)MaLDVs.Nuoc | hd.MaLDV == (int)MaLDVs.PGX | hd.MaLDV == (int)MaLDVs.Dien)
                      
                             group ct by new { pt.MaTN, pt.MaKH, hd.MaMB, } into gr
                             select new
                             {

                                 gr.Key.MaTN,
                                 gr.Key.MaKH,
                                 gr.Key.MaMB,

                                 Dien = (decimal?)0,
                                 Nuoc = (decimal?)0,
                                 Xe = (decimal?)0,
                                 NgoaiGio = (decimal?)0,
                                 SoTienDK = (decimal?)0,
                                 SoTienPhaiThu = (decimal?)0,
                                 SoTienDaThu = (decimal?)gr.Sum(p => p.SoTien).GetValueOrDefault()
                             }).ToList();
                #endregion
     

                
                var ltData = DK.Concat(PhaiThu).Concat(DaThu);

               
                #region //Nap vào pivot
                this.DataSource = (from hd in ltData
                                   join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                              
                                   join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                 
                                  
                                   group new { hd,tn} by new { hd.MaTN, hd.MaKH, hd.MaMB,tn.TenTN,kh.HoKH,kh.TenKH,kh.IsCaNhan,kh.CtyTen } into gr
                                   select new
                                   {
                                    
                                       gr.Key.TenTN,
                                
                                       TenKH = gr.Key.IsCaNhan == true ? (gr.Key.HoKH + " " + gr.Key.TenKH) : gr.Key.CtyTen,
                                     
                                       gr.Key.MaKH,
                                       SoTienDK = gr.Sum(p => p.hd.SoTienDK) == 0 ? null : gr.Sum(p => p.hd.SoTienDK),
                                       SoTienDaThu = gr.Sum(p => p.hd.SoTienDaThu) == 0 ? null : gr.Sum(p => p.hd.SoTienDaThu),
                                       SoTienPhaiThu = (gr.Sum(p => p.hd.SoTienPhaiThu) + gr.Sum(p => p.hd.SoTienDK)) == 0 ? null : gr.Sum(p => p.hd.SoTienPhaiThu) + gr.Sum(p => p.hd.SoTienDK),
                                       Dien = gr.Sum(p => p.hd.Dien) == 0 ? null : gr.Sum(p => p.hd.Dien),
                                       Nuoc = gr.Sum(p => p.hd.Nuoc) == 0 ? null : gr.Sum(p => p.hd.Nuoc),
                                       Xe = gr.Sum(p => p.hd.Xe) == 0 ? null : gr.Sum(p => p.hd.Xe),
                                       NgoaiGio = gr.Sum(p => p.hd.NgoaiGio) == 0 ? null : gr.Sum(p => p.hd.NgoaiGio),
                                       PhaiThuConLai = gr.Sum(p => p.hd.SoTienPhaiThu) + gr.Sum(p => p.hd.SoTienDK) - gr.Sum(p => p.hd.SoTienDaThu),
                                   }).Where(p => p.SoTienPhaiThu!=null).Select(p => new { p.TenTN, p.TenKH, p.SoTienDK, p.SoTienDaThu, p.SoTienPhaiThu, p.Dien, p.Nuoc, p.Xe, p.NgoaiGio, PhaiThuConLai = p.PhaiThuConLai == 0 ? "-" : (string.Format("{0:#,0.##}", p.PhaiThuConLai)) }).ToList();
                 

                #endregion
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }
        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            //var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            var arrMaTN = strTN.Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s.Trim()));
            return ltToaNha;
        }

       

   


  

    }
}
