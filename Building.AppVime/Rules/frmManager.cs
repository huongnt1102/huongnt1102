using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.Rules
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }
        MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.TowerId = this.TowerId;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvRule.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phòng ban], xin cảm ơn.");
                return;
            }

            var f = new frmEdit();
            f.TowerId = this.TowerId;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvRule.FocusedRowHandle >= 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

                var count = db.app_TowerSettingRules.FirstOrDefault(p => p.RuleId == (int)gvRule.GetFocusedRowCellValue("Id"));
                if (count == null)
                {
                    var obj = db.app_TowerRules.FirstOrDefault(p => p.Id == (int)gvRule.GetFocusedRowCellValue("Id"));
                    try
                    {
                        db.app_TowerRules.DeleteOnSubmit(obj);
                        db.SubmitChanges();

                        gvRule.DeleteSelectedRows();

                        LoadData();
                    }
                    catch { }
                }
                else
                {
                    DialogBox.Alert("Dữ liệu đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dữ liệu.");
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            LoadData();
        }

        void LoadData()
        {
            db = new MasterDataContext();

            gcRule.DataSource = db.app_TowerRules;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
            }catch { }
        }
    }
}
