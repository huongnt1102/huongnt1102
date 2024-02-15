using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace SAP
{
    public partial class frmLichSuDongBoLSToSap : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<SAP.Class.SapInLog> Data { get; set; }

        public frmLichSuDongBoLSToSap()
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

        #region overlay
        IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }

        void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        IOverlaySplashScreenHandle handle = null;
        #endregion

        void LoadData()
        {
            handle = ShowProgressPanel();
            gc.DataSource = null;
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                var model = new { TuNgay = _TuNgay, DenNgay = _DenNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var obj = Library.Class.Connect.QueryConnect.Query<SAP.Class.SapInLog>("sapIn_Log", param);
                if (obj.Count() > 0)
                {
                    Data = obj.ToList();
                    gc.DataSource = Data;
                }
            }
            catch { }

            CloseProgressPanel(handle);
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
            itemKyBC.EditValue = objKBC.Source[0];
            SetDate(0);

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


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        }



        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// View giá trị của column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var valueIndex = gv.GetFocusedDisplayText();
                var view = new frmView();
                view.text = valueIndex;
                view.ShowDialog();
            }
            catch { }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var valueIndex = gv.GetFocusedDisplayText();
                var view = new frmViewJson();
                view.text = valueIndex;
                view.ShowDialog();
            }
            catch { }
        }
    }
}