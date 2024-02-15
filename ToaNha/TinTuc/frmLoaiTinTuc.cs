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
    public partial class frmLoaiTinTuc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmLoaiTinTuc()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
          
            gcLoaiTT.DataSource = db.ttLoaiTinTucs;
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            LoadData();
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
                    var del = db.ttLoaiTinTucs.Single(p => p.MaLoaiTinTuc == (int)grvLoaiTT.GetFocusedRowCellValue("MaLoaiTinTuc"));
                    db.ttLoaiTinTucs.DeleteOnSubmit(del);
                    db.SubmitChanges();
                    grvLoaiTT.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                    this.Close();
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.ttLoaiTinTucs.Single(p => p.MaLoaiTinTuc == (int)grvLoaiTT.GetFocusedRowCellValue("MaLoaiTinTuc"));
                db.ttLoaiTinTucs.DeleteOnSubmit(del);
                db.SubmitChanges();
                grvLoaiTT.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLoaiTT.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvLoaiMB_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
           
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void grvLoaiTT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var KT = grvLoaiTT.GetFocusedRowCellValue("TenLoaiTinTuc");
            if (KT == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Tên loại tin tức]");
                return;
            }
        }
    }
}