using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;


namespace DichVu.YeuCau.BieuDo.NhieuToaNha
{
    public partial class frmDanhGiaSaoMuiltiTN : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhGiaSaoMuiltiTN()
        {
            InitializeComponent();
        }
        private class SaoClass
        {
            public decimal SoSao { set; get; }
            public string TenSao { set; get; }
        }
        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var strCongTy = (itemToaNha.EditValue ?? "").ToString().Replace(" ", "");
                var arrCongTy = strCongTy.Split(',');
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;

               // chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var objYeuCau = (from p in db.tnycYeuCaus
                                     join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                                     where arrCongTy.Contains(p.MaTN.Value.ToString()) == true
                                          && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                          && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                     group new { p, tn } by new { p.Rating, tn.MaTN, tn.TenTN } into NCV
                                     select new
                                     {
                                         SoSao = NCV.Key.Rating,
                                         NCV.Key.MaTN,
                                         NCV.Key.TenTN,
                                         SoLuong = NCV.Count()
                                     }).ToList();
                    var objSao = new List<SaoClass>();
                    for (int i = 0; i <= 5; i++)
                    {
                        var item = new SaoClass();
                        item.SoSao = i;
                        item.TenSao = i.ToString() + "*";
                        objSao.Add(item);
                    }
                    var objData = (from ncv in objSao
                                   join yc in objYeuCau on ncv.SoSao equals yc.SoSao into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new { 
                                        SoSao=ncv.SoSao,
                                        TenTN=yc==null?"":yc.TenTN,
                                        SoLuong = yc == null ? 0 : yc.SoLuong
                                   });
                    pivotGridControl1.DataSource = objData.Where(p=>p.TenTN!="").ToList();
                    chartControl1.Legend.Visible = true;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
        private void itemRefresh_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void frmBieuDoGas_Load(object sender, EventArgs e)
        {
            rpsToaNha.DataSource = Common.TowerList;
           // itemToaNha.EditValue = Common.User.MaTN;
            itemTuNgay.EditValue = DateTime.Now;
            itemDenNgay.EditValue = DateTime.Now;
           // BaoCao_Nap();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chartControl1.ShowPrintPreview();
        }

    }
}