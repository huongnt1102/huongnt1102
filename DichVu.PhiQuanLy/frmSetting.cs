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

namespace DichVu.PhiQuanLy
{
    public partial class frmSetting : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        bool first = true;
        public frmSetting()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                int maTN = 0;
                maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                gcOption.DataSource = db.pqlOption_getBy(maTN);
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            var f = new frmEditSetting() { objnhanvien = objnhanvien };
            f.MaTN = maTN;
            f.ID = (int)gvOption.GetFocusedRowCellValue("ID");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            var f = new frmEditSetting() { objnhanvien = objnhanvien };
            f.MaTN = maTN;
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            db = new MasterDataContext();
            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.DataSource = list;
                if (list.Count > 0)
                    itemToaNha.EditValue = list[0].MaTN;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.DataSource = list2;
                if (list2.Count > 0)
                    itemToaNha.EditValue = list2[0].MaTN;
            }

            LoadData();
            first = false;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvOption.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Cài đặt], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == System.Windows.Forms.DialogResult.No) return;

            try
            {
                var obj = db.pqlOptions.Single(p => p.ID == (int)gvOption.GetFocusedRowCellValue("ID"));
                db.pqlOptions.DeleteOnSubmit(obj);
                db.SubmitChanges();

                gvOption.DeleteRow(gvOption.FocusedRowHandle);
            }
            catch { }
        }
    }
}