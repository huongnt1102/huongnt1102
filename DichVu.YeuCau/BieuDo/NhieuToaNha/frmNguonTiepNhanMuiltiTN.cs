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
    public partial class frmNguonTiepNhanMuiltiTN : DevExpress.XtraEditors.XtraForm
    {
        public frmNguonTiepNhanMuiltiTN()
        {
            InitializeComponent();
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
                                     group new { p, tn } by new { p.MaNguonDen, tn.MaTN, tn.TenTN } into NCV
                                     select new
                                     {
                                         IDNguonDen = NCV.Key.MaNguonDen,
                                         NCV.Key.MaTN,
                                         NCV.Key.TenTN,
                                         SoLuong = NCV.Count()
                                     }).ToList();
                    var objNguonDen = db.tnycNguonDens.ToList();
                    var objData = (from ncv in objNguonDen
                                   join yc in objYeuCau on ncv.ID equals yc.IDNguonDen 
                                   select new { 
                                        TenNguonDen=ncv.TenNguonDen,
                                        yc.TenTN,
                                        SoLuong=yc.SoLuong
                                   });
                    pivotGridControl1.DataSource = objData.ToList();
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