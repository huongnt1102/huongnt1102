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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Data.Linq;
namespace TaiSan.XuatKho
{
    public partial class frmManager_DaXuatKho : XtraForm
    {

        public frmManager_DaXuatKho()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbxKBC.Items.Add(str);
            }
            beiKBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();

            gvPhieuXuatKho.BestFitColumns();
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch
            { }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, GetQueryableEventArgs e)
        {

        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them(-1);
        }

        private void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();
            beiTuNgay.EditValue = objKBC.DateFrom;
            beiDenNgay.EditValue = objKBC.DateTo;
        }

        private void LoadData()
        {

        }

        private void RefreshData()
        {

        }
        private void DanhSachChiTiet()
        {
            
        }

        private void Them(int maXK)
        {
            try
            {

                using (frmXuatKho frm = new frmXuatKho {  })
                {
                    
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        RefreshData();
                    }
                }

            }
            catch (Exception ex)
            { }
        }


        private void gvPhieuXuatKho_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvPhieuXuatKho.IsGroupRow(e.RowHandle))
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
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvPhieuXuatKho); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvPhieuXuatKho); }));
            }

        }
        bool cal(Int32 _width, GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void grvTaiSan_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
                var _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSan); }));
            }
        }

        private void gvPhieuXuatKho_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

        }

        private void gvPhieuXuatKho_FocusedRowLoaded(object sender, RowEventArgs e)
        {

        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(gvPhieuXuatKho.GetFocusedRowCellValue("MaXK").ToString()))
                Them(int.Parse(gvPhieuXuatKho.GetFocusedRowCellValue("MaXK").ToString()));
        }

        private void bbiSuaXuatKhoChiTiet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var maXk = gvPhieuXuatKho.GetFocusedRowCellValue("MaXK");
                if (maXk == null)
                {
                    DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                    return;
                }
                Them((int)maXk);
            }
            catch { }
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnInPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}