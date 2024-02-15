using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace SAP
{
    public partial class frmManHinhTrungGianHoaDon : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Class.ZIAR008List> Data { get; set; }

        public frmManHinhTrungGianHoaDon()
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

                var model = new { MaTN = _MaTN, TuNgay = _TuNgay, DenNgay = _DenNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var obj = Library.Class.Connect.QueryConnect.Query<Class.ZIAR008List>("sapin_ZIAR008List", param);
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

        /// <summary>
        /// Duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            try
            {
                Duyet(true);
                
            }
            catch(System.Exception ex) { }
        }

        public void Duyet(bool isDuyet)
        {
            handle = ShowProgressPanel();

            var indexs = gv.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn công nợ. Xin cám ơn!");
                return;
            }

            foreach (var i in indexs)
            {
                try
                {
                    var id_ = gv.GetRowCellValue(i, "SOCONGNO");
                    if (id_ == null) continue;

                    Library.Class.Connect.QueryConnect.QueryData<bool>("sapin_ZIAR008_Duyet",
                    new
                    {
                        ID = id_,
                        IsDuyet = isDuyet
                    });
                }
                catch(System.Exception ex) { }
                gv.SetRowCellValue(i, "IsDuyet", isDuyet);
            }

            CloseProgressPanel(handle);
        }

        /// <summary>
        /// Không duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Duyet(false);

            }
            catch (System.Exception ex) { }
        }

        /// <summary>
        /// Đồng bộ dòng chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            handle = ShowProgressPanel();
            try
            {
                int[] indexs = gv.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn dòng công nợ.");
                    return;
                }

                foreach (int i in indexs)
                {
                    //SAP.Funct.SyncCus.DongBo_1_MB((int)gv.GetRowCellValue(i, "MaMB"));

                    var item = Data.FirstOrDefault(_=>_.SOCONGNO == Convert.ToString( gv.GetRowCellValue(i, "SOCONGNO")));
                    if(item != null)
                    {
                        if (item.IsDuyet.Value == false) continue;
                        SAP.Funct.SyncHoaDon.DongBo(item);

                        gv.SetRowCellValue(i, "SAP_HD", Convert.ToString(SAP.Funct.SyncHoaDon.SAP_HD));
                        gv.SetRowCellValue(i, "SAP_MSG", Convert.ToString(SAP.Funct.SyncHoaDon.SAP_MSG));
                    }
                    
                }

            }
            catch (System.Exception ex) { }
            CloseProgressPanel(handle);
        }

        /// <summary>
        /// Đồng bộ tất cả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            handle = ShowProgressPanel();
            try
            {
                foreach (var i in Data)
                {
                    if (i.IsDuyet.Value == false) continue;
                    SAP.Funct.SyncHoaDon.DongBo(i);
                }
            }
            catch (System.Exception ex) { }
            CloseProgressPanel(handle);
            LoadData();
        }

    }
}