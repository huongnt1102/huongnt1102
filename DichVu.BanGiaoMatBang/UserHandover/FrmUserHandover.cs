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

namespace DichVu.BanGiaoMatBang.UserHandover
{
    public partial class FrmUserHandover : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmUserHandover()
        {
            InitializeComponent();
        }

        private void FrmUserHandover_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                glkUser.DataSource = _db.tnNhanViens;
                gc.DataSource = _db.ho_UserHandovers;
            }
            catch { }
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
            try
            {
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void GlkUser_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                gv.SetFocusedRowCellValue("UserId", item.EditValue);
                gv.SetFocusedRowCellValue("UserName", item.Properties.View.GetFocusedRowCellValue("HoTenNV"));
                gv.SetFocusedRowCellValue("UserNo", item.Properties.View.GetFocusedRowCellValue("MaSoNV"));
                gv.UpdateCurrentRow();

            }
            catch { }
        }
    }
}