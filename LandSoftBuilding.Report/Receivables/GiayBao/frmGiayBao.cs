using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BuildingDesignTemplate;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using Library;
using DevExpress.Export;
using FTP;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class frmGiayBao : DevExpress.XtraEditors.XtraForm
    {
        public frmGiayBao()
        {
            InitializeComponent();
        }
        public List<Library.CongNoCls.DataCongNo> Depts { get; set; }
        public byte MaTN { get; set; }
        public List<int> MaKHs { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public List<long> HoaDonIds { set; get; }
        public List<int> MaMBs { get; set; }
        List<int> GetMaLDV()
        {
            var ltMaLDV = new List<int>();
            var arrMaLDV = ckbLoaiDichVu.EditValue.ToString().Split(',');
            foreach (var s in arrMaLDV)
            {
                if (s != "")
                {
                    ltMaLDV.Add(int.Parse(s));
                }
            }

            return ltMaLDV;
        }
        DevExpress.XtraReports.UI.XtraReport GetReportNew(int _MaMB, List<int> _MaLDVs)
        {
            var _ReportID = (int)lkMauIn.EditValue;
            switch (_ReportID)
            {

                default:
                    return null;
                //case 55:
                //return new LandSoftBuilding.Receivables.rptGiayBaoPhoYen(this.Thang, this.Nam, _MaMB, (int)comboBoxEdit1.SelectedIndex, _MaLDVs);
            }
        }
        private void frmGiayBao_Load(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                ckbLoaiDichVu.Properties.DataSource = (from l in db.dvLoaiDichVus select new { l.ID, TenLDV = l.TenHienThi }).ToList();
                lkTaiKhoan.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                    join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                    where tk.MaTN == this.MaTN
                                                    select new { tk.ID, tk.SoTK, tk.ChuTK, nh.TenNH })
                                                    .ToList();
                lkTaiKhoan.ItemIndex = 0;
                lkMauIn.Properties.DataSource = (from rp in db.rptReports
                                                 join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                                 where tn.MaTN == this.MaTN & rp.GroupID == 1
                                                 orderby rp.Rank
                                                 select new { rp.ID, rp.Name }).ToList();
                lkMauIn.ItemIndex = 0;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        DevExpress.XtraReports.UI.XtraReport GetReport(int _MaKH,int _MaMB, List<int> _MaLDVs)
        {
            var _MaTK = (int)lkTaiKhoan.EditValue;
            var _ReportID = (int)lkMauIn.EditValue;
            
            #region
            switch (_ReportID)
            {
                //case 2:
                //    return new rptGiayBao(this.MaTN, this.Thang, this.Nam, _MaKH, _MaMB, _MaLDVs, _MaTK);
                //case 87:
                //    return new rptGiayBaoImperiaLan2(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 89:
                //    return new rptGiayBaoTTC(this.MaTN, this.Thang, this.Nam, _MaKH, _MaMB, _MaLDVs, _MaTK);
                //case 100:
                //    return new rptGiayBaoTTC_XemLai(this.MaTN, this.Thang, this.Nam, _MaKH, _MaMB, _MaLDVs, _MaTK);
                //case 90:
                //    return new rptThongBaoNhacNo2(this.MaTN, this.Thang, this.Nam, _MaKH, _MaMB, _MaLDVs, _MaTK);
                //case 91:
                //    return new rptThongBaoNhacNo1(this.MaTN, this.Thang, this.Nam, _MaKH, _MaMB, _MaLDVs, _MaTK);
                //case 92:
                //    return new rptGiayBaoImperiaNhacNo(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 93:
                //    return new rptGiayBaoImperiaPQL(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 94:
                //    return new rptGiayBaoImperiaNuoc(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 95:
                //    return new rptGiayBaoImperiaXe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 96:
                //    return new rptGiayBaoImperia(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 98:
                //    return new RptThongBaoThuPhiQuanLyVanHanh04(MaTN, _MaKH, Thang, Nam, _MaTK);
                //case 99:
                //    return new RptThongBaoThuPhiQuanLyVanHanh05(MaTN, _MaKH, Thang, Nam, _MaTK);
                //case 18:
                //    return new LandSoftBuilding.Receivables.rptGiayBao(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 37:
                //    return new LandSoftBuilding.Receivables.rptGiayBao3(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 58:
                //    return new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoMIK(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 78:
                //    return new LandSoftBuilding.Report.rptThongBaoThanhToanVeViecThueMatBangL1(_MaKH, this.MaTN, this.Thang, this.Nam, _MaTK);
                //case 79:
                //    LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt79 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
                //    rpt79.CreateDocument();
                //    if (rpt79.Pages.Count < 2)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //        rpttemp1.CreateDocument();
                //        rpt79.Pages.AddRange(rpttemp1.Pages);
                //    }
                //    this.MaKHs.RemoveAt(0);
                //    foreach (var _MaKH1 in this.MaKHs)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, _MaKH, this.GetMaLDV(), _MaTK);
                //        rpt2.CreateDocument();
                //        if (rpt2.Pages.Count < 2)
                //        {
                //            LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //            rpttemp2.CreateDocument();
                //            rpt2.Pages.AddRange(rpttemp2.Pages);
                //        }
                //        rpt79.Pages.AddRange(rpt2.Pages);
                //    }
                //    // Reset all page numbers in the resulting document.
                //    rpt79.PrintingSystem.ContinuousPageNumbering = true;
                //    // Show the Print Preview form.
                //    rpt79.ShowPreviewDialog();
                //    return new
                //       LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);

                //case 86:
                //    LandSoftBuilding.Receivables.GiayBao.rptGiayBaoHoaPhat rpt86 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoHoaPhat(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
                //    rpt86.CreateDocument();
                //    if (rpt86.Pages.Count < 2)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //        rpttemp1.CreateDocument();
                //        rpt86.Pages.AddRange(rpttemp1.Pages);
                //    }
                //    this.MaKHs.RemoveAt(0);
                //    foreach (var _MaKH1 in this.MaKHs)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoHoaPhat rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoHoaPhat(this.MaTN, this.Thang, this.Nam, _MaKH, this.GetMaLDV(), _MaTK);
                //        rpt2.CreateDocument();
                //        if (rpt2.Pages.Count < 2)
                //        {
                //            LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //            rpttemp2.CreateDocument();
                //            rpt2.Pages.AddRange(rpttemp2.Pages);
                //        }
                //        rpt86.Pages.AddRange(rpt2.Pages);
                //    }
                //    // Reset all page numbers in the resulting document.
                //    rpt86.PrintingSystem.ContinuousPageNumbering = true;
                //    // Show the Print Preview form.
                //    rpt86.ShowPreviewDialog();
                //    return new
                //       LandSoftBuilding.Receivables.GiayBao.rptGiayBaoHoaPhat(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 59:
                //    //return new
                //    var rpt1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //    //  LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
                //    rpt1.CreateDocument();
                //    if (rpt1.Pages.Count < 2)
                //    {
                //        // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //        // rpttemp1.CreateDocument();
                //        // rpt1.Pages.AddRange(rpttemp1.Pages);
                //    }
                //    this.MaKHs.RemoveAt(0);
                //    foreach (var _MaKH1 in this.MaKHs)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienKhoiDe rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH1, _MaLDVs, _MaTK);
                //        rpt2.CreateDocument();
                //        if (rpt2.Pages.Count < 2)
                //        {
                //            // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //            //  rpttemp2.CreateDocument();
                //            // rpt2.Pages.AddRange(rpttemp2.Pages);
                //        }
                //        rpt1.Pages.AddRange(rpt2.Pages);
                //    }
                //    // Reset all page numbers in the resulting document.
                //    rpt1.PrintingSystem.ContinuousPageNumbering = true;
                //    // Show the Print Preview form.
                //    rpt1.ShowPreviewDialog();
                //    return new
                //           LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 60:
                //    var rpt60 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienDieuHoaKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //    //  LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
                //    rpt60.CreateDocument();
                //    if (rpt60.Pages.Count < 2)
                //    {
                //        // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //        // rpttemp1.CreateDocument();
                //        // rpt1.Pages.AddRange(rpttemp1.Pages);
                //    }
                //    this.MaKHs.RemoveAt(0);
                //    foreach (var _MaKH1 in this.MaKHs)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienDieuHoaKhoiDe rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienDieuHoaKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH1, _MaLDVs, _MaTK);
                //        rpt2.CreateDocument();
                //        if (rpt2.Pages.Count < 2)
                //        {
                //            // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //            //  rpttemp2.CreateDocument();
                //            // rpt2.Pages.AddRange(rpttemp2.Pages);
                //        }
                //        rpt60.Pages.AddRange(rpt2.Pages);
                //    }
                //    // Reset all page numbers in the resulting document.
                //    rpt60.PrintingSystem.ContinuousPageNumbering = true;
                //    // Show the Print Preview form.
                //    rpt60.ShowPreviewDialog();
                //    return new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiDienDieuHoaKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 61:
                //    var rpt61 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiNuocKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //    //  LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
                //    rpt61.CreateDocument();
                //    if (rpt61.Pages.Count < 2)
                //    {
                //        // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //        // rpttemp1.CreateDocument();
                //        // rpt1.Pages.AddRange(rpttemp1.Pages);
                //    }
                //    this.MaKHs.RemoveAt(0);
                //    foreach (var _MaKH1 in this.MaKHs)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiNuocKhoiDe rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiNuocKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH1, _MaLDVs, _MaTK);
                //        rpt2.CreateDocument();
                //        if (rpt2.Pages.Count < 2)
                //        {
                //            // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //            //  rpttemp2.CreateDocument();
                //            // rpt2.Pages.AddRange(rpttemp2.Pages);
                //        }
                //        rpt61.Pages.AddRange(rpt2.Pages);
                //    }
                //    // Reset all page numbers in the resulting document.
                //    rpt61.PrintingSystem.ContinuousPageNumbering = true;
                //    // Show the Print Preview form.
                //    rpt61.ShowPreviewDialog();
                //    return new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiNuocKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 62:
                //    var rpt62 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiGiuXeKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //    //  LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
                //    rpt62.CreateDocument();
                //    if (rpt62.Pages.Count < 2)
                //    {
                //        // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //        // rpttemp1.CreateDocument();
                //        // rpt1.Pages.AddRange(rpttemp1.Pages);
                //    }
                //    this.MaKHs.RemoveAt(0);
                //    foreach (var _MaKH1 in this.MaKHs)
                //    {
                //        LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiGiuXeKhoiDe rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiGiuXeKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH1, _MaLDVs, _MaTK);
                //        rpt2.CreateDocument();
                //        if (rpt2.Pages.Count < 2)
                //        {
                //            // LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank rpttemp2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLongBlank();
                //            //  rpttemp2.CreateDocument();
                //            // rpt2.Pages.AddRange(rpttemp2.Pages);
                //        }
                //        rpt62.Pages.AddRange(rpt2.Pages);
                //    }
                //    // Reset all page numbers in the resulting document.
                //    rpt62.PrintingSystem.ContinuousPageNumbering = true;
                //    // Show the Print Preview form.
                //    rpt62.ShowPreviewDialog();
                //    return new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoPhiGiuXeKhoiDe(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                //case 63:
                //    return new LandSoftBuilding.Receivables.rptGiayBaoVStar(this.MaTN, this.Thang, this.Nam, _MaKH, _MaLDVs, _MaTK);
                default:
                    return null;
            }
            #endregion
            
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lkTaiKhoan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tài khoản ngân hàng");
                    return;
                }

                DevExpress.XtraReports.UI.XtraReport rpt = new DevExpress.XtraReports.UI.XtraReport();
                int sttKh = 0;
                var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl();
                if(Depts.Count() > 0)
                {
                    //var itemFirst = Depts[0];
                    var db = new MasterDataContext();
                    var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)lkMauIn.EditValue);
                    if (objForm != null)
                    {
                        //var rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, itemFirst.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, itemFirst, true, itemFirst.MaMB.GetValueOrDefault());
                        //ctlRtf.RtfText = rtfText;
                        bool danhDau = false;
                        foreach (var item in Depts)
                        {
                            if (objForm.IsUseApartment.GetValueOrDefault() == true)
                            {
                                var ApartmentList = db.mbMatBangs.Where(_ => _.MaKH == item.MaKH.GetValueOrDefault()  ); //&_.MaMB != itemFirst.MaMB.GetValueOrDefault()
                                foreach (var am in ApartmentList)
                                {
                                    var ctlRtf1 = new DevExpress.XtraRichEdit.RichEditControl();

                                    var rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, am.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, item, true, am.MaMB);
                                    //if (danhDau == false)
                                    //{
                                    //    ctlRtf.RtfText = rtfText;
                                    //}
                                    //else
                                    //{
                                        ctlRtf1.RtfText = rtfText;
                                        DocumentRange range = ctlRtf1.Document.Range;

                                        

                                        DocumentPosition docPos = ctlRtf.Document.CreatePosition(ctlRtf.Document.Text.Length);

                                        ctlRtf.Document.InsertRtfText(docPos, ctlRtf1.Document.GetRtfText(range));
                                    //}
                                    //danhDau = true;

                                    //if (sttKh < MaKHs.Count() - 1)
                                    //{
                                    InsertPageBreakCommand command = new InsertPageBreakCommand(ctlRtf);
                                    command.Execute();
                                    //}
                                }
                            }
                            else
                            {
                                var ctlRtf1 = new DevExpress.XtraRichEdit.RichEditControl();

                                //if (item.MaKH.GetValueOrDefault() == itemFirst.MaKH.GetValueOrDefault()) continue;

                                var rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, item.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, item, false, 0);
                                ctlRtf1.RtfText = rtfText;
                                DocumentRange range = ctlRtf1.Document.Range;

                                DocumentPosition docPos = ctlRtf.Document.CreatePosition(ctlRtf.Document.Text.Length);
                                ctlRtf.Document.InsertRtfText(docPos, ctlRtf1.Document.GetRtfText(range));

                                //if (sttKh < MaKHs.Count() - 1)
                                //{
                                    InsertPageBreakCommand command = new InsertPageBreakCommand(ctlRtf);
                                    command.Execute();
                                //}
                            }

                            sttKh++;
                        }
                        

                        // item first
                        //var rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, itemFirst.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, itemFirst, true, itemFirst.MaMB.GetValueOrDefault());
                        //ctlRtf.RtfText = rtfText;

                        //var itemSecond = Depts.Where(_ => _.MaKH != itemFirst.MaKH);
                        //if (objForm.IsUseApartment.GetValueOrDefault() == true)
                        //{
                        //    var ApartmentList = db.mbMatBangs.Where(_ => _.MaKH == itemFirst.MaKH.GetValueOrDefault() & _.MaMB != itemFirst.MaMB.GetValueOrDefault());

                        //    foreach (var am in ApartmentList)
                        //    {
                        //        var ctlRtf1 = new DevExpress.XtraRichEdit.RichEditControl();

                        //        rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, am.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, itemFirst, true, am.MaMB);
                        //        ctlRtf1.RtfText = rtfText;
                        //        DocumentRange range = ctlRtf1.Document.Range;

                        //        DocumentPosition docPos = ctlRtf.Document.CreatePosition(ctlRtf.Document.Text.Length);
                        //        ctlRtf.Document.InsertRtfText(docPos, ctlRtf1.Document.GetRtfText(range));


                        //        InsertPageBreakCommand command = new InsertPageBreakCommand(ctlRtf);
                        //        command.Execute();
                        //    }
                        //}


                        //foreach (var item in itemSecond)
                        //{
                        //    if (objForm.IsUseApartment.GetValueOrDefault() == true)
                        //    {
                        //        var ApartmentList = db.mbMatBangs.Where(_ => _.MaKH == item.MaKH.GetValueOrDefault());
                        //        foreach (var am in ApartmentList)
                        //        {
                        //            var ctlRtf1 = new DevExpress.XtraRichEdit.RichEditControl();

                        //            rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, item.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, item, true, item.MaMB.GetValueOrDefault());
                        //            ctlRtf1.RtfText = rtfText;
                        //            DocumentRange range = ctlRtf1.Document.Range;

                        //            DocumentPosition docPos = ctlRtf.Document.CreatePosition(ctlRtf.Document.Text.Length);
                        //            ctlRtf.Document.InsertRtfText(docPos, ctlRtf1.Document.GetRtfText(range));

                        //            if (sttKh < MaKHs.Count() - 1)
                        //            {
                        //                InsertPageBreakCommand command = new InsertPageBreakCommand(ctlRtf);
                        //                command.Execute();
                        //            }


                        //        }


                        //    }
                        //    else
                        //    {
                        //        var ApartmentList = db.mbMatBangs.Where(_ => _.MaKH == item.MaKH.GetValueOrDefault());
                        //        foreach (var am in ApartmentList)
                        //        {
                        //            var ctlRtf1 = new DevExpress.XtraRichEdit.RichEditControl();

                        //            rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, item.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, item, false, 0);
                        //            ctlRtf1.RtfText = rtfText;
                        //            DocumentRange range = ctlRtf1.Document.Range;

                        //            DocumentPosition docPos = ctlRtf.Document.CreatePosition(ctlRtf.Document.Text.Length);
                        //            ctlRtf.Document.InsertRtfText(docPos, ctlRtf1.Document.GetRtfText(range));

                        //            if (sttKh < MaKHs.Count() - 1)
                        //            {
                        //                InsertPageBreakCommand command = new InsertPageBreakCommand(ctlRtf);
                        //                command.Execute();
                        //            }


                        //        }
                        //    }

                        //    sttKh++;


                        //}
                    }
                        
                }

                

                var frm = new FrmDesign { RtfText = ctlRtf.RtfText };
                frm.ShowDialog();

            }
            catch(System.Exception ex)
            {
                string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                DevExpress.XtraEditors.XtraMessageBoxArgs args = new DevExpress.XtraEditors.XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 1000;
                args.Caption = ex.GetType().FullName;
                args.Text = mes;
                args.Buttons = new System.Windows.Forms.DialogResult[] { System.Windows.Forms.DialogResult.OK, System.Windows.Forms.DialogResult.Cancel };
                DevExpress.XtraEditors.XtraMessageBox.Show(args).ToString();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (lkTaiKhoan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tài khoản ngân hàng");
                    return;
                }
                if ((int)lkMauIn.EditValue == 54 | (int)lkMauIn.EditValue == 55)
                {
                    foreach (var _MaMB in this.MaMBs)
                    {
                        this.GetReportNew(_MaMB, this.GetMaLDV()).Print();

                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else
                {
                    var ltMaLDV = this.GetMaLDV();
                    int _sttKH = 0;
                    foreach (var item in Depts)
                    {
                        string linkAdress = "";

                        var db = new MasterDataContext();
                        var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)lkMauIn.EditValue);
                        if (objForm != null)
                        {
                            if (objForm.IsUseApartment.GetValueOrDefault() == true)
                            {
                                var ApartmentList = db.mbMatBangs.Where(_ => _.MaKH == item.MaKH.GetValueOrDefault());
                                foreach (var am in ApartmentList)
                                {
                                    var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl();
                                    ctlRtf.RtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, am.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, item, true, am.MaMB);
                                    ctlRtf.Print();
                                }
                                    
                            }
                            else
                            {
                                var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl();
                                ctlRtf.RtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Thang, Nam, item.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, item, false, 0);
                                ctlRtf.Print();
                            }


                            // Thông tin in lên server

                            string debitNumber = LuuSoThongBaoPhiIn(item.MaKH, Thang, Nam);
                            //if (debitNumber != "")
                            //{
                            //ExportSettings.DefaultExportType = ExportType.WYSIWYG;
                            //SaveFileDialog frm = new SaveFileDialog();
                            //frm.Filter = "PDF (*.pdf)|*.pdf";
                            ////frm.InitialDirectory = @"E:/";
                            //frm.InitialDirectory = Application.StartupPath;
                            ////linkFile = "E:\\"
                            //linkAdress = Application.StartupPath + "\\" + MaTN + "_" + Nam + "_" + Thang + "_" + _MaKH + "_" + debitNumber + ".pdf";
                            //frm.FileName = linkAdress;
                            //if (frm.FileName != "")
                            //{
                            //    ctlRtf.ExportToPdf(frm.FileName);
                            //}

                            //var frmUpload = new FTP.frmUploadFile();
                            //frmUpload.Folder = "ThongBaoPhi";
                            //frmUpload.ClientPath = linkAdress;
                            //frmUpload.ShowDialog();

                            //// Lưu đường link vào lịch sử in
                            //string filenameAdress = frmUpload.FileNameAddress;

                            //Library.Class.Connect.QueryConnect.QueryData<bool>("dvHoaDonUpdateLinkUpload",
                            //    new
                            //    {
                            //        TowerId = MaTN,
                            //        CustomerId = _MaKH,
                            //        Year = Nam,
                            //        Month = Thang,
                            //        DebitNumber = debitNumber,
                            //        UploadAddress = filenameAdress,
                            //        CreatedBy = Library.Common.User.MaNV
                            //    });

                            //System.IO.File.Delete(linkAdress);
                            //}

                            
                        }
                        _sttKH++;
                        System.Threading.Thread.Sleep(1000);
                    }
                }

            }
            catch (Exception ex) {  }
            //catch(System.Exception ex) { Library.DialogBox.Error(ex.Message); }
        }

        private string LuuSoThongBaoPhiIn(int? MaKH, int Thang, int Nam)
        {
            string SoTBP = "";

            var result_mb = Library.Class.Connect.QueryConnect.QueryData<int>("dv_hoadon_loadhoadon_matbang", 
                                                                            new
                                                                            {
                                                                                MaKH = MaKH,
                                                                                MaTN = MaTN
                                                                            });

            foreach (var mb in result_mb)
            {
                SoTBP = Library.Class.Connect.QueryConnect.QueryData<string>("tbp_save_so_lan_in",
                    new
                    {
                        MaTN = MaTN,
                        MaKH = MaKH,
                        MaMB = mb,
                        NgayIn = System.DateTime.Now,
                        Thang = Thang,
                        Nam = Nam
                    }).FirstOrDefault();
            }

            return SoTBP;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lkMauIn_EditValueChanged(object sender, EventArgs e)
        {
            if ((int)lkMauIn.EditValue == 55)
            {
                layoutControlItem7.Visibility = LayoutVisibility.Always;
                emptySpaceItem1.Visibility = LayoutVisibility.Never;
                comboBoxEdit1.SelectedIndex = 0;
            }
            else
            {
                layoutControlItem7.Visibility = LayoutVisibility.Never;
                emptySpaceItem1.Visibility = LayoutVisibility.Always;
            }

        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (lkTaiKhoan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tài khoản ngân hàng");
                return;
            }
            var ltMaLDV = this.GetMaLDV();
            var _MaTK = (int)lkTaiKhoan.EditValue;
            var _ReportID = (int)lkMauIn.EditValue;
            LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt1 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, this.MaKHs[0], this.GetMaLDV(), _MaTK);
            rpt1.CreateDocument();
            this.MaKHs.RemoveAt(0);
            foreach (var _MaKH in this.MaKHs)
            {
                LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong rpt2 = new LandSoftBuilding.Receivables.GiayBao.rptGiayBaoThangLong(this.MaTN, this.Thang, this.Nam, _MaKH, this.GetMaLDV(), _MaTK);
                rpt2.CreateDocument();
                rpt1.Pages.AddRange(rpt2.Pages);
            }
            // Reset all page numbers in the resulting document.
            rpt1.PrintingSystem.ContinuousPageNumbering = true;
            // Show the Print Preview form.
            rpt1.ShowPreviewDialog();
            //}
            //catch { }
        }
    }
}