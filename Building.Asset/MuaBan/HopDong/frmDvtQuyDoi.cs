using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
namespace TaiSan
{
    public partial class frmDvtQuyDoi : DevExpress.XtraEditors.XtraForm
    {

        public frmDvtQuyDoi()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {

            
            
        }

        private void frmXuatXu_Load(object sender, EventArgs e)
        {
            lueToaNha.DataSource = Common.TowerList;

            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (beiToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [tòa nhà]");
                return;
            }
            gv.AddNewRow();

            gv.SetFocusedRowCellValue("MaTN", (byte?)beiToaNha.EditValue);
            gv.SetFocusedRowCellValue("TyLe1", 1);
            gv.SetFocusedRowCellValue("TyLe2", 1);
        }

        private void grvXuatXu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                gv.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
            this.DialogResult=DialogResult.OK;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm=new Import.frmImportDonViTinh())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvtype_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gv.IsGroupRow(e.RowHandle))
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
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gv); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gv); }));
            }

        }
        bool cal(Int32 _width, GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void glueDVT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
            }
        }



        private void spinTyLe_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void repTyLe2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void repDVT1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}