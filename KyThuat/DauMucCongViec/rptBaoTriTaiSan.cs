using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace KyThuat.DauMucCongViec
{
    public partial class rptBaoTriTaiSan : DevExpress.XtraReports.UI.XtraReport
    {
        //MasterDataContext db;
        public rptBaoTriTaiSan(DateTime? TuNgay, DateTime? DenNgay, byte MaTN, int MaNCV)
        {
            InitializeComponent();


            cSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            cLevel.DataBindings.Add(new XRBinding("Text", DataSource, "ViTri"));
            cDescription.DataBindings.Add(new XRBinding("Text", DataSource, "TaiSan"));
            cStart1.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiGianTheoLich", "{0:m}"));
            cFinish1.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiGianHetHan", "{0:m}"));
            cStart2.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiGianTH", "{0:m}"));
            cFinish2.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiGianHT", "{0:m}"));
            cReMark.DataBindings.Add(new XRBinding("Text", DataSource, "MoTa"));
          //  int[] ListMaHT = MaHT.Split(',').Select(p=>Convert.ToInt32(p)).ToArray();


            using (var db = new MasterDataContext())
            {
                if (MaTN != 0)
                {
                    cProject.Text = db.tnToaNhas.SingleOrDefault(p=>p.MaTN == MaTN).TenTN;
                }
             //   cTitle.Text = string.Format("{0} in {1} MAINTENANCE REPORT", tenHT, db.tnToaNhas.SingleOrDefault(p => p.MaTN == MaTN).TenTN);
              //  if(MaHT)
                
                cTuNgay.Text = string.Format("Từ ngày: {0:dd/MM/yyyy}", TuNgay);
                cDenNgay.Text = string.Format("Đến ngày: {0:dd/MM/yyyy}", DenNgay);
                var obj = db.btDauMucCongViecs.Where(p => SqlMethods.DateDiffDay(TuNgay, p.ThoiGianTheoLich) >= 0
                    & SqlMethods.DateDiffDay(p.ThoiGianTheoLich, DenNgay) >= 0 & (p.MaTN == MaTN || p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == MaTN|| MaTN ==0)
                    & (p.MaNguonCV == MaNCV || MaNCV == -1)).OrderByDescending(p => p.ThoiGianTheoLich)//.AsEnumerable()
                    //.Select((p, index) => new
                    .Select(p=>new 
                    {
                       // STT = index + 1,
                        ViTri = (p.MaTN == null ? "" : p.tnToaNha.TenTN) + "- " + (p.MaKN == null ? "" : p.mbKhoiNha.TenKN) + "- " + (p.MaTL == null ? "" : p.mbTangLau.TenTL) + "- " + (p.MaMB == null ? "" : p.mbMatBang.MaSoMB),
                        TaiSan = p.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.TenTS,
                        p.ThoiGianTheoLich,
                        p.ThoiGianHetHan,
                        p.ThoiGianTH,
                        p.ThoiGianHT,
                        p.MoTa
                    }).ToList();
                this.DataSource = obj;
            }

        }

        int stt = 0;
        private void cSTT_EvaluateBinding(object sender, BindingEventArgs e)
        {
            stt++;

            e.Value = stt.ToString();
        }
    }
}
