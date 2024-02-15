using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using Library.Others;

namespace Library.Other
{
    public partial class ctlHuyen : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public ctlHuyen()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                gcHuyen.DataSource = db.Huyens;
            }
            catch
            {
                gcHuyen.DataSource = null;
            }
            finally
            {
                wait.Close();
            }
        }

        void Edit()
        {
            var maHuyen = (int?)gvHuyen.GetFocusedRowCellValue("MaHuyen");
            if (maHuyen == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần sửa");
                return;
            }

            var frm = new frmHuyen();
            frm.MaHuyen = maHuyen;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");
            LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maHuyen = (int?)gvHuyen.GetFocusedRowCellValue("MaHuyen");
            if (maHuyen == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;

            try
            {
                var obj = db.Huyens.Single(p => p.MaHuyen == maHuyen);
                db.Huyens.DeleteOnSubmit(obj);
                db.SubmitChanges();
                gvHuyen.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Error("Tên huyện đã sử dụng, không thể xóa!");
            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvHuyen_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvHuyen_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void ctlHuyen_Load(object sender, EventArgs e)
        {
            glkTinh.DataSource = db.Tinhs;
            LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcHuyen);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvHuyen.AddNewRow();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImportHuyen())
            {
                frm.ShowDialog();
            }
        }
    }
}
