using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.YeuCau
{
    public partial class frmTrangThai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTrangThai()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        void LoadData()
        {
            gcTrangThai.DataSource = db.tnycTrangThais;
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTrangThai.AddNewRow();
        }

        private void grvTrangThai_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            
        }

        private void grvTrangThai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvTrangThai.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lt = db.tnycLichSuCapNhats.Where(p => p.MaTT == short.Parse(grvTrangThai.GetFocusedRowCellValue("MaTT").ToString())).ToList();
                if (lt.Count > 0)
                {
                    DialogBox.Error("Trạng thái này đang được sử dụng. Vui lòng kiểm tra lại");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvTrangThai.DeleteSelectedRows();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}