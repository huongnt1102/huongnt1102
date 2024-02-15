using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace ToaNha.TinTuc
{
    public partial class frmTinTuc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmTinTuc()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue; 
            gcTinTuc.DataSource = db.ttTinTucs.Where(o => o.MaTN == _MaTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            grlkLoaiTinTuc.DataSource = db.ttLoaiTinTucs;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
        }

        private void grvLoaiMB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.ttTinTucs.Single(p => p.id == (int)grvTinTuc.GetFocusedRowCellValue("id"));
                    db.ttTinTucs.DeleteOnSubmit(del);
                    db.SubmitChanges();
                    grvTinTuc.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                    return;
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.ttTinTucs.Single(p => p.id == (int)grvTinTuc.GetFocusedRowCellValue("id"));
                db.ttTinTucs.DeleteOnSubmit(del);
                db.SubmitChanges();
                grvTinTuc.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                return;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTinTuc.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvLoaiMB_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTinTuc.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void grvTinTuc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var _TieuDe = grvTinTuc.GetFocusedRowCellValue("TieuDe");
            if (_TieuDe == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Tiêu đề]");
                return;
            }

            var _NoiDung = grvTinTuc.GetFocusedRowCellValue("NoiDung");
            if (_NoiDung == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Nội dung]");
                return;
            }
            var _LoaiTT = grvTinTuc.GetFocusedRowCellValue("MaLoaiTinTuc");
            if (_LoaiTT == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng chọn [Loại tin tức]");
                return;
            }
        }

        private void grlkLoaiTinTuc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn [loại tin tức]");
                    return;
                }
                var TenLoaiTT = item.Properties.View.GetFocusedRowCellValue("TenLoaiTinTuc").ToString();
                //var TenLoaiTT = db.ttLoaiTinTucs.Single(O => O.MaLoaiTinTuc == (int)item.EditValue).TenLoaiTinTuc;
                grvTinTuc.SetFocusedRowCellValue("TenLoaiTinTuc", TenLoaiTT);
            }
            catch { }
        }

      
    }
}