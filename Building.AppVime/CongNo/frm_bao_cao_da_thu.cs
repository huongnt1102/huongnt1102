using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Data.PivotGrid;
using Dapper;

namespace Building.AppVime.CongNo
{
    public partial class frm_bao_cao_da_thu : DevExpress.XtraEditors.XtraForm
    {
        public frm_bao_cao_da_thu()
        {
            InitializeComponent();
            
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltToaNha = this.GetToaNha();
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var arrMaTN = (itemToaNha.EditValue ?? "").ToString();

                var param = new DynamicParameters();
                param.Add("@TowerId", arrMaTN, DbType.String, null, null);
                param.Add("@TuNgay", _TuNgay, DbType.DateTime, null, null);
                param.Add("@DenNgay", _DenNgay, DbType.DateTime, null, null);
                var lPhatSinh_Ldv = Library.Class.Connect.QueryConnect.Query<BaoCaoDaThuModel>("dbo.ad_hoadon_bao_cao_da_thu", param).ToList();

                pvData.DataSource = lPhatSinh_Ldv;
            }
            catch(Exception ex)
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        public class BaoCaoDaThuModel
        {
            public decimal? SoTien { get; set; }

            public System.DateTime? NgayThu { get; set; }

            public System.DateTime? NgayTT { get; set; }

            public string TenLMB { get; set; }

            public string TenTN { get; set; }

            public string TenKN { get; set; }

            public string TenTL { get; set; }

            public string MaSoMB { get; set; }

            public string KyHieu { get; set; }

            public string MaPhu { get; set; }

            public string TenKH { get; set; }

            public string TenLDV { get; set; }

            public string SoPT { get; set; }

            public string DienGiai { get; set; }

            public string HoTenNV { get; set; }

            public string TenHT { get; set; }

        }

        void Print()
        {
            //var _MaTN = Common.User.MaTN.Value;
            //var rpt = new rptPhieuThu(_MaTN);
            //var stream = new System.IO.MemoryStream();
            //var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            //var _TuNgay = (DateTime)itemTuNgay.EditValue;
            //var _DenNgay = (DateTime)itemDenNgay.EditValue;            

            //pvData.OptionsView.ShowColumnHeaders = false;
            //pvData.OptionsView.ShowDataHeaders = false;
            //pvData.OptionsView.ShowFilterHeaders = false;
            //pvData.SavePivotGridToStream(stream);
            //pvData.OptionsView.ShowColumnHeaders = true;
            //pvData.OptionsView.ShowDataHeaders = true;
            //pvData.OptionsView.ShowFilterHeaders = true;

            //rpt.LoadData( _KyBC, _TuNgay, _DenNgay, stream);
            //rpt.CreateDocument();
            //rpt.PrintingSystem.Document.AutoFitToPagesWidth = 1;       
            //rpt.ShowPreviewDialog();
        }

        private void frmPhieuThu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            itemKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(pvData);
        }

        private void pvData_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");  
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
        private bool DK = true;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pvData.OptionsView.ShowRowTotals = !DK;
            DK = pvData.OptionsView.ShowRowTotals;
        }
    }
}