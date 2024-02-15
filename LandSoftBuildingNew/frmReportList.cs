using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System.Linq;
using Library;
using Building.PrintControls;

namespace LandSoftBuildingMain
{
    public partial class frmReportList : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public frmReportList()
        {
            InitializeComponent();

            reportList1.PrintviewClick += new PrintviewEventHandler(reportList1_PrintviewClick);
            reportList1.EditClick += new EditButtonClickEventHandler(reportList1_EditClick);
            reportList1.SavingTemplate += new SavingTemplateEventHandler(reportList1_SavingTemplate);
        }

        void reportList1_SavingTemplate(object sender, SavingTemplateEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    rptReports_ToaNha objBC;
                    objBC = db.rptReports_ToaNhas.SingleOrDefault(p => p.ReportID == e.ReportID & p.MaTN == reportList1.MaTN);
                    if (objBC == null)
                    {
                        objBC = new rptReports_ToaNha();
                        objBC.ReportID = e.ReportID;
                        objBC.MaTN = reportList1.MaTN;
                        db.rptReports_ToaNhas.InsertOnSubmit(objBC);
                    }

                    objBC.Layout = e.Template;

                    db.SubmitChanges();
                }
                e.Saved = true;
            }
            catch { }
        }

        void reportList1_EditClick(object sender, EditButtonClickEventArgs e)
        {
            try
            {
                var dateNow = DateTime.Now;
                List<int> ltMaLDV = new List<int>(); List<int> ltMaLMB = new List<int>();
                List<byte> ltToaNha = new List<byte>();
                XtraReport rpt = null;
                switch (e.ReportID)
                {
                    #region Hoa don - Giay bao
                    case 2: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBao(reportList1.MaTN, dateNow.Month, dateNow.Year, 0,0, ltMaLDV, 0); break;
                    case 3: rpt = new LandSoftBuilding.Fund.Input.rptPhieuThu(0, reportList1.MaTN,1); break;
                    case 4: rpt = new LandSoftBuilding.Fund.Output.rptPhieuChi(0, reportList1.MaTN); break;
                    case 5: rpt = new LandSoftBuilding.Receivables.GiayBao.rptDichVuCoBanImperia(reportList1.MaTN, 0, dateNow.Month, dateNow.Year, 0); break;
                    case 6: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienDien(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 7: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienDien3Pha(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 8: rpt = new LandSoftBuilding.Receivables.GiayBao.rptDieuHoaNgoaiGio(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 9: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienNuocImperia(reportList1.MaTN, 0, dateNow.Month, dateNow.Year,"");   break;
                    case 10: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienNuocNong(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 11: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienNuocSinhHoat(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 12: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienGas(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 13: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTheXeImperia(reportList1.MaTN, 0, dateNow.Month, dateNow.Year,"");   break;
                    case 15: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienThue(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 16: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienDatCoc(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 17: rpt = new LandSoftBuilding.Receivables.GiayBao.rptTienSuaChua(reportList1.MaTN, 0, dateNow.Month, dateNow.Year); break;
                    case 18: rpt = new LandSoftBuilding.Receivables.rptGiayBao(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 20: rpt = new LandSoftBuilding.Receivables.Reports.rptCongNo(reportList1.MaTN); break;
                    case 21: rpt = new LandSoftBuilding.Lease.Reports.rptCongNoDatCoc(reportList1.MaTN); break;
                    case 22: rpt = new DichVu.Reports.rptPhieuThu(reportList1.MaTN); break;
                    case 23: rpt = new DichVu.Reports.rptKhauTru(reportList1.MaTN); break;
                    case 29: rpt = new DichVu.Reports.rptTheXe(reportList1.MaTN); break;
                    case 30: rpt = new LandSoftBuilding.Receivables.GiayBao.rptPhieuThu(0, 0, 0, reportList1.MaTN); break;
                    case 37: rpt = new LandSoftBuilding.Receivables.rptGiayBao3(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                   
                    case 87: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoImperiaLan2(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break; 
                    case 89: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoTTC(reportList1.MaTN, dateNow.Month, dateNow.Year, 0,0, ltMaLDV, 0); break;
                    case 90: rpt = new LandSoftBuilding.Receivables.GiayBao.rptThongBaoNhacNo2(reportList1.MaTN, dateNow.Month, dateNow.Year, 0,0, ltMaLDV, 0); break;
                    case 91: rpt = new LandSoftBuilding.Receivables.GiayBao.rptThongBaoNhacNo1(reportList1.MaTN, dateNow.Month, dateNow.Year, 0,0, ltMaLDV, 0); break;
                    case 92: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoImperiaNhacNo(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 93: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoImperiaPQL(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 94: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoImperiaNuoc(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 95: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoImperiaXe(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 96: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoImperia(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 98: rpt = new LandSoftBuilding.Receivables.GiayBao.RptThongBaoThuPhiQuanLyVanHanh04(reportList1.MaTN,0, dateNow.Month, dateNow.Year, 0); break;
                    case 99: rpt = new LandSoftBuilding.Receivables.GiayBao.RptThongBaoThuPhiQuanLyVanHanh05(reportList1.MaTN,0, dateNow.Month, dateNow.Year, 0); break;
                   
                    case 31:
                        rpt = new LandSoftBuilding.Report.rptDoanhThuTrongNgay(dateNow,(byte)reportList1.MaTN);
                        break;
                    case 32:
                        rpt = new LandSoftBuilding.Report.rptBangTongHopPhaiThu(0,0, (byte)reportList1.MaTN);
                        break;
                    case 34: rpt = new LandSoftBuilding.Report.rptTongHopChuaThu(0, 0, (byte)reportList1.MaTN);
                        break;
                    case 36: rpt = new LandSoftBuilding.Report.rptTongHopThuTheoThang(0, 0, (byte)reportList1.MaTN);
                        break;
                    case 42: rpt = new LandSoftBuilding.Fund.Input.rptPhieuThuMau3(0, reportList1.MaTN); break;
                    case 19: rpt = new LandSoftBuilding.Fund.Input.rptDetail(0, reportList1.MaTN); break;
                    case 58: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 57: rpt = new LandSoftBuilding.Fund.Input.rptDetail(0, reportList1.MaTN); break;
                    #endregion
                    #region Khối đế
                    case 59: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienKhoiDe(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 60: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienDieuHoaKhoiDe(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 61: rpt = new  LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiNuocKhoiDe(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 62: rpt = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiGiuXeKhoiDe(reportList1.MaTN, dateNow.Month, dateNow.Year, 0, ltMaLDV, 0); break;
                    case 76: rpt = new LandSoftBuilding.Lease.rptThongBaoThanhToan(0,reportList1.MaTN); break;
                    case 77: rpt = new LandSoftBuilding.Lease.rptThongBaoThanhToanMau2(0, reportList1.MaTN); break;
                    #endregion
                }

                if (rpt == null) return;

                e.Report = rpt;
            }
            catch { }
        }

        void reportList1_PrintviewClick(object sender, PrintviewEventArgs e)
        {
            var frmPrint = new Building.PrintControls.PrintForm();
            frmPrint.Text = e.ReportName;
            switch (e.ReportID)
            {
                case 31:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmPickDateTower() { MaTN = reportList1.MaTN, ReportID = e.ReportID.Value };
                    break;
                case 32:
                case 34:
                case 36:
               
                case 33:
                case 38:
                case 39:
                case 48:
                case 49:
                case 50:
                case 51:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 35:
                case 40:
                case 41:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmOption() { MaTN = reportList1.MaTN, ReportID = e.ReportID.Value };
                    break;
              
                case 56:
                case 80:
                case 82:
                case 83:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoToaNha() { IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                case 63:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoToaNha() { IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                case 64:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoToaNha() { IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                case 65:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoToaNha { IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                case 66:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBCTienThu() {IDTN=reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                 case 73:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoToaNha() { IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                 case 74:
                    frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoToaNha() { IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
                case 81:
                   frmPrint.PrintControl.FilterForm = new LandSoftBuilding.Report.frmKyBaoCaoYeuCauKhieuNaiCuaKH{ IDTN = reportList1.MaTN, CateID = e.ReportID.Value };
                    break;
            }

            var frm = (frmMain)this.Parent.FindForm();
            frm.LoadForm(frmPrint);
        }

        private void reportList1_ToaNhaEditValueChanged(object sender, ToaNhaEditValueChangedEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    e.DataSource = (from r in db.rptReports
                                    join tnrp in db.rptReports_ToaNhas on r.ID equals tnrp.ReportID
                                    join gr in db.rptGroups on r.GroupID equals gr.ID
                                    where tnrp.MaTN == reportList1.MaTN
                                    select new Building.PrintControls.SourceItem()
                                    {
                                        ID = r.ID,
                                        Name = r.Name,
                                        GroupID = gr.ID,
                                       // GroupName = gr.Name
                                    }).ToList();
                }
            }
            catch { }
        }
    }
}
