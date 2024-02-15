using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;

namespace Building.SMSZalo.Mau
{
    public partial class frmTemlate : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmTemlate()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            var MaTN = (byte?)itemBieuMauToaNha.EditValue;
            var obj = (from bm in db.web_ZaloTemplates
                       join tn in db.tnToaNhas on bm.MaTN equals tn.MaTN
                       where bm.MaTN == (int)MaTN
                       select new
                       {
                           Id = bm.Id,
                           bm.Name,
                           bm.Content,
                       }).ToList();
            gcDinhMuc.DataSource = obj;
            wait.Close();
            wait.Dispose();
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            var lt = db.tnToaNhas.Select(p => new { p.MaTN,p.TenVT, p.TenTN }).ToList();
            itemDuAn.DataSource = lt;
            if (lt.Count() > 0)
                itemBieuMauToaNha.EditValue = lt.FirstOrDefault().MaTN;
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void DeleteSelected()
        {
            int[] indexs = grvDinhMuc.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Dòng], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {

                foreach (int i in indexs)
                {
                    var Id = (int)grvDinhMuc.GetRowCellValue(i, "Id");
                    var obj = db.web_ZaloTemplates.FirstOrDefault(o => o.Id == Id);
                    db.web_ZaloTemplates.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                }

                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                LoadData();
            }
            catch
            {
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSelected();
        }

        private void grvDinhMuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelected();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaTN = (byte?)itemBieuMauToaNha.EditValue;
            if (MaTN == null)
            {
                DialogBox.Error("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            if (grvDinhMuc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Dòng], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() {MaTN = MaTN, Id = (int)grvDinhMuc.GetFocusedRowCellValue("Id") })
            {
                frm.ShowDialog();
                if (frm.IsLoad == true)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaTN = (byte?)itemBieuMauToaNha.EditValue;
            if (MaTN == null)
            {
                DialogBox.Error("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            using (frmEdit frm = new frmEdit())
            {
                frm.MaTN = MaTN;
                frm.ShowDialog();
                if (frm.IsLoad == true)
                    LoadData();
            }
        }

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}