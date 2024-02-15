using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
namespace TaiSan.MuaHang
{
    public partial class frmCongNoMuaHang : XtraForm
    {
        
        public frmCongNoMuaHang()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }


        private void frmBanVatTu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var str in objKbc.Source)
            {
                cbxKBC.Items.Add(str);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            LoadData();

            grvBanVatTu.BestFitColumns();
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void itemNap_ItemClick(object sender, ItemClickEventArgs e)
        {
            RefreshData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = sender as ComboBoxEdit;
            if (comboBoxEdit != null) SetDate(comboBoxEdit.SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void gvPhieuXuatKho_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle < 0) return;
                LoadDetail();
                grvTaiSan.BestFitColumns();
                gvChiTiet.BestFitColumns();
                gvLichThuTien.BestFitColumns();
            }
            catch
            {
                // ignored
            }
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                gcBanVatTu.DataSource = null;
                if (beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                }
                
            }
            catch
            {
                // ignored
            }

            grvBanVatTu.BestFitColumns();
        }

        private void LoadDetail()
        {

        }

        private void RefreshData()
        {
            LoadData();
            LoadDetail();
            grvBanVatTu.BestFitColumns();
            
        }

        private void grvBanVatTu_FocusedRowLoaded(object sender, RowEventArgs e)
        {
            RefreshData();
            grvTaiSan.BestFitColumns();
            gvChiTiet.BestFitColumns();
            gvLichThuTien.BestFitColumns();
        }

        private void grvBanVatTu_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!grvBanVatTu.IsGroupRow(e.RowHandle))
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
                    var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    var width = Convert.ToInt32(size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(width, grvBanVatTu); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, grvBanVatTu); }));
            }

        }
        bool Cal(Int32 width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
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
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, grvTaiSan); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(_width, grvTaiSan); }));
            }
        }

        private void gvChiTiet_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvChiTiet.IsGroupRow(e.RowHandle))
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
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvChiTiet); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvChiTiet); }));
            }
        }

        private void gvLichThuTien_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvLichThuTien.IsGroupRow(e.RowHandle))
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
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvLichThuTien); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvLichThuTien); }));
            }
        }

        private void grvBanVatTu_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView v = sender as GridView;
            if(e.RowHandle>=0)
            {
                string tt = v.GetRowCellDisplayText(e.RowHandle, v.Columns["TrangThai"]);
                if(tt== "Phải Trả")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void grvTaiSan_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView v = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string tt = v.GetRowCellDisplayText(e.RowHandle, v.Columns["TrangThai"]);
                if (tt == "Phải trả")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void gvChiTiet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView v = sender as GridView;
            if(e.Column.FieldName=="TrangThai")
            {
                string tt = v.GetRowCellDisplayText(e.RowHandle, v.Columns["TrangThai"]);
                if (tt == "Cần mua hàng")
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
        }

        private void bbiThemThuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                frmThanhToan frm = new frmThanhToan();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    RefreshData();
                }
            }
            catch 
            {

            }
        }

        private void bbiSuaThuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {


                frmThanhToan frm = new frmThanhToan();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    RefreshData();
                }
            }
            catch
            {
                // ignored
            }
        }

        private void bbiXoaThuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }
    }
}