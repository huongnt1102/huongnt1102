using System;

using System.Windows.Forms;
using Library;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.XuatKho
{
    public partial class frmBanVatTu_BanHang : XtraForm
    {

        public frmBanVatTu_BanHang()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);
            TranslateLanguage.TranslateControl(this, barManager1);
        }


        private void frmBanVatTu_BanHang_Load(object sender, EventArgs e)
        {
            
        }


        private void slookHopDong_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void slookVatTu_EditValueChanged(object sender, EventArgs e)
        {
            
        }


        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void grvThongTinHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void grvThongTinHopDong_RowClick(object sender, RowClickEventArgs e)
        {

        }


        private void grvChonVatTu_RowClick(object sender, RowClickEventArgs e)
        {
            
        }

        private void grvTaiSanBan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
           
        }


        private void grvTaiSanBan_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        private void grvTaiSanBan_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!grvTaiSanBan.IsGroupRow(e.RowHandle))
            {
                if (!e.Info.IsRowIndicator) return;
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
                Int32 _width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSanBan); }));
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, grvTaiSanBan); }));
            }

        }
        bool cal(Int32 _width, GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void chkChon_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}