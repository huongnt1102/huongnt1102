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

namespace Library.Other
{
    public partial class ctlTinh : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public ctlTinh()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            ////Translate.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                gcTinh.DataSource = db.Tinhs;//.OrderBy(p => p.TenTinh).Select(p => new { p.MaTinh, p.TenTinh }).ToList();
            }
            catch (Exception)
            {
                gcTinh.DataSource = null;
            }
            finally
            {
                wait.Close();
            }
        }

        private void ctlTinh_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTinh.FocusedRowHandle = -1;
            db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTinh = (int?)gvTinh.GetFocusedRowCellValue("MaTinh");
            if (maTinh == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần sửa");
                return;
            }

            var frm = new frmTinh();
            frm.MaTinh = maTinh;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTinh = (int?)gvTinh.GetFocusedRowCellValue("MaTinh");
            if (maTinh == null)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;

            try
            {
                var obj = db.Tinhs.Single(p => p.MaTinh == maTinh);
                db.Tinhs.DeleteOnSubmit(obj);
                db.SubmitChanges();
                gvTinh.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Error("Tên tỉnh đã được sử dụng. Không thể xóa!");
            }
        }

        private void gvTinh_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void itemDongBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.Xas.DeleteAllOnSubmit(db.Xas);
            db.SubmitChanges();
            db.Huyens.DeleteAllOnSubmit(db.Huyens);
            db.SubmitChanges();
            db.Tinhs.DeleteAllOnSubmit(db.Tinhs);
            db.SubmitChanges();

            var ltTinh = API.GetTinh().Select(o => new Tinh()
            {
                MaTinh = o.ID,
                TenTinh = o.Title,
                TenHienThi = o.Title
            }).ToList();

            db.Tinhs.InsertAllOnSubmit(ltTinh);
            db.SubmitChanges();

            foreach(var t in ltTinh)
            {
                var ltHuyen = API.GetHuyen(t.MaTinh).Select(o => new Huyen()
                {
                    MaHuyen = o.ID,
                    MaTinh = t.MaTinh,
                    TenHuyen = o.Title,
                    TenHienThi = o.Title
                }).ToList();

                db.Huyens.InsertAllOnSubmit(ltHuyen);
                db.SubmitChanges();

                foreach (var h in ltHuyen)
                {
                    var ltXa = API.GetXa(h.MaHuyen).Select(o => new Xa()
                    {
                        MaHuyen = h.MaHuyen,
                        MaXa = o.ID,
                        TenXa = o.Title,
                        TenHienThi = o.Title
                    }).ToList();

                    db.Xas.InsertAllOnSubmit(ltXa);
                    db.SubmitChanges();
                }
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcTinh);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImportTinh())
            {
                frm.ShowDialog();
            }
            
        }

        private void itemAdd_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTinh.AddNewRow();
        }
    }
}
