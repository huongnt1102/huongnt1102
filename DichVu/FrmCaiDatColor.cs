using System;
using DevExpress.XtraEditors;
using Library;
using System.Windows.Forms;
using System.Linq;

namespace DichVu
{
    public partial class FrmCaiDatColor : XtraForm
    {
        private MasterDataContext _db;

        public FrmCaiDatColor()
        {
            InitializeComponent();
        }

        private void FrmCaiDatColor_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gc.DataSource = _db.dvColors;
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");  

            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.dvColors.FirstOrDefault(_ => _.Id == (int) gv.GetFocusedRowCellValue("Id"));
                if (obj != null)
                {
                    _db.dvColors.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");  
            }
        }

        private void gv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var obj = _db.dvColors.FirstOrDefault(_ => _.Id == (int)gv.GetFocusedRowCellValue("Id"));
                    if (obj != null)
                    {
                        _db.dvColors.DeleteOnSubmit(obj);
                    }

                    _db.SubmitChanges();
                    gv.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");  
                }
            }
        }
    }
}