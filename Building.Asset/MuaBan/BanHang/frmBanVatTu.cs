using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using TaiSan.XuatKho.Report;

namespace TaiSan.XuatKho
{
    public partial class frmBanVatTu : XtraForm
    {
        public frmBanVatTu()
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
            beiKBC.EditValue = objKbc.Source[3];
            SetDate(3);

            LoadData();

            grvBanVatTu.BestFitColumns();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmBanVatTu_BanHang frm = new frmBanVatTu_BanHang {})
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    this.RefreshData();
                }
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


                using (var frm = new frmBanVatTu_BanHang {})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }


        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            using (var frm = new frmBanVatTu_ThanhToan
            {
                
            })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    RefreshData();
                    
                    xtraTabDetail.TabPages[2].Select();
                }
            }
        }

        private void gvPhieuXuatKho_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    LoadDetail();
                    grvTaiSan.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void gvPhieuXuatKho_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "TienCon" && Convert.ToInt32(e.CellValue) > 0)
                {
                    //e.Appearance.BackColor = Color.FromArgb(240, 105, 57);
                    e.Appearance.ForeColor = Color.FromArgb(241, 43, 25);
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void gvLichThuTien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        private void gvLichThuTien_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs) e;
            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point p = new Point(me.X,me.Y);
                
            }
        }
        
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            
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


        
        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
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
                    System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, grvBanVatTu); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvBanVatTu); }));
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
                    System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSan); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSan); }));
            }
        }

        private void grvBanVatTu_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            LoadDetail();
            grvTaiSan.BestFitColumns();
        }

        private void btnInPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbiInPhieuBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbiDeNghiBoSung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemXoaLichSu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }
    }
}