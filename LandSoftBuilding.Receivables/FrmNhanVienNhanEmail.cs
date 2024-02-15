using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace LandSoftBuilding.Receivables
{
    public partial class FrmNhanVienNhanEmail : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();
        public FrmNhanVienNhanEmail()
        {
            InitializeComponent();
        }

        private void FrmNhanVienNhanEmail_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            lkRptGroup.DataSource = db.rptGroups.Select(_ => new { _.ID, _.Name }).ToList();
            glkNhanVien.DataSource = db.tnNhanViens.Select(_ => new { _.MaSoNV, _.MaNV, _.HoTenNV }).ToList();

            LoadData(); 

        }

        public void LoadData()
        {
            try
            {
                var toaNha = (byte) itemToaNha.EditValue;
                gc.DataSource = db.mail_SetupSendMailToEmployeeDebits.Where(_=>_.BuildingId == toaNha);
            }
            catch
            {
            }
        }

        private void Gv_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("BuildingId", (byte)itemToaNha.EditValue);
            gv.SetFocusedRowCellValue("IsSendMail", true);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu");
                LoadData();
            }
            catch { }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = db.mail_SetupSendMailToEmployeeDebits.FirstOrDefault(_ => _.Id == (int)gv.GetFocusedRowCellValue("Id"));
                if (obj != null)
                {
                    db.mail_SetupSendMailToEmployeeDebits.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }
        private bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }
    }
}