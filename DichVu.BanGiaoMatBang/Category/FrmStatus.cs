using System;
using System.Linq;
using System.Windows.Forms;
using Library;

namespace DichVu.BanGiaoMatBang.Category
{
    public partial class FrmStatus : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();

        public FrmStatus()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        void LoadData()
        {
            gc.DataSource = _db.ho_Status;
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.AddNewRow();
        }

        private void grvTrangThai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                gv.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lt = _db.tnycLichSuCapNhats.Where(p => p.MaTT == short.Parse(gv.GetFocusedRowCellValue("MaTT").ToString())).ToList();
                if (lt.Count > 0)
                {
                    DialogBox.Error("Trạng thái này đang được sử dụng. Vui lòng kiểm tra lại");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                gv.DeleteSelectedRows();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}