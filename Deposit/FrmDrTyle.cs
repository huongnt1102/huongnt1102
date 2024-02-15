using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Deposit
{
    public partial class FrmDrTyle : XtraForm
    {
        private MasterDataContext _db;
        public FrmDrTyle()
        {
            InitializeComponent();
        }

        private void FrmDrTyle_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gc.DataSource = _db.Dep_Tyles;
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");
            LoadData();
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.Dep_Tyles.FirstOrDefault(_ => _.Id == (int) gv.GetFocusedRowCellValue("Id"));
                if (obj != null) _db.Dep_Tyles.DeleteOnSubmit(obj);
                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, Phiếu thu đặt cọc đang dùng loại đặt cọc này.");
            }
        }

        private void Gv_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }
        private bool Cal(int width, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

    }
}