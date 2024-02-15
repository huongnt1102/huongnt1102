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
    public partial class frmTangLau : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmTangLau()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            try
            {
                if (itemKhoiNha.EditValue != null)
                    gcTangLau.DataSource = db.mbTangLaus.Where(p => p.MaKN == (int)itemKhoiNha.EditValue);
                else
                    gcTangLau.DataSource = null;
            }
            catch (System.Exception ex) { }
            
        }

        private void frmTangLau_Load(object sender, EventArgs e)
        {
            lookUpEditTaoNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void grvTangLau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvTangLau.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            grvTangLau.DeleteSelectedRows();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTangLau.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvTangLau_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTangLau.SetFocusedRowCellValue("MaKN", itemKhoiNha.EditValue);
        }

        private void itemKhoiNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var maTN = itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue);
            lookKhoiNha.DataSource = db.mbKhoiNhas.Where(p => p.MaTN == maTN);
            LoadData();
        }
    }
}