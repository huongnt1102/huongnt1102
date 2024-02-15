using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.NhapKho
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {

        public frmManager()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            var objKbc = new KyBaoCao();
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            foreach (var str in objKbc.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKbc.Source[7];
            SetDate(7);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
            LoadDetail();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void grvPNK_DoubleClick(object sender, EventArgs e)
        {
            try
            {


                using (var frm = new frmEdit() {})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    LoadData();
                    LoadDetail();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
               
                using (var frm = new frmEdit() {})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    LoadData();
                    LoadDetail();
                    gcPNK.RefreshDataSource();
                    gcTaiSan.RefreshDataSource();
                    gcDonHangThieu.RefreshDataSource();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                
                using (var frm = new frmEdit { })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    LoadData();
                    LoadDetail();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void btnInHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        }

       
        private void grvPNK_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void grvDonHangThieu_DoubleClick(object sender, EventArgs e)
        {
            
            using (var frm = new frmEdit() {  })
            {
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;
                LoadData();
                LoadDetail();
            }
        }

       
        void LoadData()
        {
            
        }

        void LoadDetail()
        {
            
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();
            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        

        private void grvTaiSan_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!grvTaiSan.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSan); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSan); }));
            }
        }
        bool cal(Int32 _width, GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void grvPNK_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!grvPNK.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, grvPNK); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvPNK); }));
            }
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
            LoadDetail();
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
    }
}