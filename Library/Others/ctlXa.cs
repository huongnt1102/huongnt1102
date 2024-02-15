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
    public partial class ctlXa : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public ctlXa()
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
                gcXa.DataSource = db.Xas;
            }
            catch
            {
                gcXa.DataSource = null;
            }
            finally
            {
                wait.Close();
            }
        }

        private void ctlXa_Load(object sender, EventArgs e)
        {
            glkHuyen.DataSource = db.Huyens;//.OrderBy(p => p.TenHuyen).Select(p => new { p.MaHuyen, p.TenHuyen }).ToList();
            lkTinh.DataSource = db.Tinhs.Select(p => new { p.MaTinh, p.TenTinh }).ToList();            
            LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maXa = (int?)gvXa.GetFocusedRowCellValue("MaXa");
            if (maXa == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần sửa");
                return;
            }

            using (var frm = new frmXa())
            {
                frm.MaTinh = (int?)itemTinh.EditValue;
                frm.MaHuyen = (int?)gvXa.GetFocusedRowCellValue("MaHuyen");
                frm.MaXa = maXa;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");
            LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maXa = (int?)gvXa.GetFocusedRowCellValue("MaXa");
            if (maXa == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;

            try
            {
                var obj = db.Xas.Single(p => p.MaXa == maXa);
                db.Xas.DeleteOnSubmit(obj);
                db.SubmitChanges();
                gvXa.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Error("Tên xã đã sử dụng, không thể xóa!");
            }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvXa_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void itemTinh_EditValueChanged(object sender, EventArgs e)
        {
            var maTinh = (int?)itemTinh.EditValue;
            glkHuyen.DataSource = db.Huyens.Where(p => p.MaTinh == maTinh);
            gcXa.DataSource = db.Xas.Where(o => o.Huyen.MaTinh == maTinh);
           
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcXa);
        }

        private void itemAdd_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvXa.AddNewRow();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImportXa())
            {
                frm.ShowDialog();
            }
        }
    }
}
