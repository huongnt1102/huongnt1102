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
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
namespace TaiSan.XuatKho
{
    public partial class frmCongNoKhachHang : XtraForm
    {
       

        public frmCongNoKhachHang()
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

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbxKBC.Items.Add(str);
            }
            beiKBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();

            grvBanVatTu.BestFitColumns();
        }

       
      
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmBanVatTu_BanHang frm = new frmBanVatTu_BanHang { })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    RefreshData();
                }
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBanVatTu.FocusedRowHandle >= 0)
            {
                using (frmBanVatTu_BanHang frm = new frmBanVatTu_BanHang { })
                {
                    
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        RefreshData();
                    }
                } 
            }
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

        private void btnThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            using (frmBanVatTu_ThanhToan frm = new frmBanVatTu_ThanhToan { })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    this.RefreshData();
                    
                    ctabBanHang.TabPages[2].Select();
                }
            }
        }
        private void gvPhieuXuatKho_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gvLichThuTien_Click(object sender, EventArgs e)
        {
            
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
        

        private void LoadDetail()
        {
           
        }

        private void RefreshData()
        {

            
        }
        private void Xoa()
        {
            
        }



        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void grvBanVatTu_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {

        }

        private void grvBanVatTu_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        bool cal(Int32 _width, DevExpress.XtraGrid.Views.Grid.GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
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

        private void gvChiTiet_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvChiTiet); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvChiTiet); }));
            }
        }

        private void gvLichThuTien_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvLichThuTien); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvLichThuTien); }));
            }
        }

        private void grvBanVatTu_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView v = sender as GridView;
            if(e.RowHandle>=0)
            {
                string tt = v.GetRowCellDisplayText(e.RowHandle, v.Columns["TrangThai"]);
                if(tt== "Phải Thu")
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
                if (tt == "Phải thu")
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
                if (tt == "Cần xuất hàng")
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
        }

        private void bbiThemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            using (frmBanVatTu_ThanhToan frm = new frmBanVatTu_ThanhToan
            {
                
            })
            {
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    this.RefreshData();
                    
                    ctabBanHang.TabPages[2].Select();
                }
            }
        }

        private void bbiSuaThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            using (frmBanVatTu_ThanhToan frm = new frmBanVatTu_ThanhToan
            {
                
            })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    this.RefreshData();
                    
                    ctabBanHang.TabPages[2].Select();
                }
            }
        }

        private void bbiXoaThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            try
            {
                
            }
            catch { }
        }
    }
}