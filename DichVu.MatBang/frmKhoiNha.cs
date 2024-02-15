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

namespace MatBang
{
    public partial class frmKhoiNha : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;
        public frmKhoiNha()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            gcKhoiNha.DataSource = db.mbKhoiNhas.Where(p => p.tnToaNha.MaTN == maTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            glkCongTrinh.DataSource = db.tnCongTrinhs;

            LoadData();

            first = false;
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

        private void grvKhoiNha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.mbKhoiNhas.Single(p => p.MaKN == (int)grvKhoiNha.GetFocusedRowCellValue("MaKN"));
                    db.SubmitChanges();
                    grvKhoiNha.DeleteSelectedRows();
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
                var del = db.mbKhoiNhas.Single(p => p.MaKN == (int)grvKhoiNha.GetFocusedRowCellValue("MaKN"));
                db.SubmitChanges();
                grvKhoiNha.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvKhoiNha.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvKhoiNha_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvKhoiNha.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void itemKN_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }
    }
}