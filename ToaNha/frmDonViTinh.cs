using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmDonViTinh : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmDonViTinh()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            db = new MasterDataContext();
            gcDVT.DataSource = db.DonViTinhs;
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvDVT.DeleteSelectedRows();
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            gvDVT.InvalidRowException += Library.Common.InvalidRowException;

            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDVT.AddNewRow();
        }

        private void gcDVT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu");
                
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
        
        private void gvDVT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var _TenDVT = (gvDVT.GetRowCellValue(e.RowHandle, "TenDVT") ?? "").ToString();
            if (_TenDVT.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập tên đơn vị tính!";
                return;
            }
            else if (Common.Duplication(gvDVT, e.RowHandle, "TenDVT", _TenDVT))
            {
                e.Valid = false;
                e.ErrorText = "Tên đơn vị tính đã tồn tại, vui lòng nhập lại!";
                return;
            }
        }

        /// <summary>
        /// Import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đơn vị tính", "Import");
            using (var f = new Import.frmDonViTinh())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }
    }
}