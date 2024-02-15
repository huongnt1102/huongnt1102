using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraEditors;

namespace SAP
{
    public partial class frmDoanhThu : DevExpress.XtraEditors.XtraForm
    {
        public frmDoanhThu()
        {
            InitializeComponent();
        }

        #region Class
        public class sap_list_load
        {
            public int GROUP_KEY { get; set; }

            public int AMOUNT_DOC { get; set; }

            public decimal? AMOUNT_LOC { get; set; }

            public string DOCUMENT_TYPE { get; set; }

            public string CURRENCY { get; set; }

            public string POSTING_KEY { get; set; }

            public string SGLIND { get; set; }

            public string DocumentDate { get; set; }

            public string PostingDate { get; set; }

            public string COMPANYCODE { get; set; }

            public string REFERENCE { get; set; }

            public string DOC_HEADER_TEXT { get; set; }

            public string ACCOUNT { get; set; }

            public string ASSIGNMENT { get; set; }

            public string TEXT { get; set; }

            public string BaselineDate { get; set; }

            public string Referencekey3 { get; set; }

            public string Value_date { get; set; }

            public string ReferenceKey1 { get; set; }

            public string CostCenter { get; set; }

            public string ProfitCenter { get; set; }
        }


        #endregion

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

        void LoadData()
        {
            gc.DataSource = null;
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                var model = new { MaTN = _MaTN, TuNgay = _TuNgay, DenNgay = _DenNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gc.DataSource = Library.Class.Connect.QueryConnect.Query<sap_list_load>("sap_list_load", param);
            }
            catch { }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            this.LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }


    }
}