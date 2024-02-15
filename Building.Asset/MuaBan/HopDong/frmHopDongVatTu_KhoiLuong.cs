using System;

using System.Windows.Forms;
using Library;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace TaiSan.XuatKho
{
    public partial class frmHopDongVatTu_KhoiLuong : XtraForm
    {

        public frmHopDongVatTu_KhoiLuong(){
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void LoadData()
        {
            
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void gvSoLan_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                    System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvChiTiet); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvChiTiet); }));
            }

        }
        bool cal(Int32 _width, DevExpress.XtraGrid.Views.Grid.GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void gvSoLan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gvChiTiet.GetSelectedRows().ToString()))
                {
                    var indexs = gvChiTiet.GetSelectedRows();
                    using (var db = new MasterDataContext())
                    {

                        foreach (var i in indexs)
                        {
                            if (!string.IsNullOrEmpty(gvChiTiet.GetRowCellDisplayText(i, "ID")))
                            {

                            }
                        }
                    }
                    gvChiTiet.DeleteSelectedRows();
                }
            }
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(gvChiTiet.GetFocusedDataSourceRowIndex().ToString()))
                {
                    var indexs = gvChiTiet.GetSelectedRows();
                    using (var db = new MasterDataContext())
                    {
                        
                        foreach (var i in indexs)
                        {
                            if (!string.IsNullOrEmpty(gvChiTiet.GetRowCellDisplayText(i, "ID")))
                            {

                            }
                        }
                    }
                    gvChiTiet.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void repCongViec_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item == null) return;
                
            }
            catch { }
        }

        private void repTuongChiTiet_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch(e.Button.Index)
            {
                case 1:
                    var frm = new frmHopDongVatTu_KhoiLuong_TuongChiTiet();
                    frm.ShowDialog();
                    if(frm.DialogResult == DialogResult.OK)
                    {

                    }
                    break;
            }
        }

        private void glkHopDong_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            gcChiTiet.DataSource = null;

            
        }

        private void repTuongChiTiet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item == null) return;

            }
            catch { }
        }

        private void spinDai_EditValueChanged(object sender, EventArgs e)
        {
            var dai = (SpinEdit)sender;
            gvChiTiet.SetFocusedRowCellValue("Dai", dai.Value);

        }

        private void spinCao_EditValueChanged(object sender, EventArgs e)
        {
            var cao = (SpinEdit)sender;
            gvChiTiet.SetFocusedRowCellValue("Cao", cao.Value);

        }

        private void spinCua_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}