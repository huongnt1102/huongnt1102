using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;

namespace Building.SMSZalo.DanhMuc
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmConfig()
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

            var obj = (from cf in db.web_Zalos
                       join tn in db.tnToaNhas on cf.MaTN equals tn.MaTN
                       select new
                       {
                           Id = cf.Id,
                           ToaNha = tn.TenTN,
                           ZaloName = cf.ZaloName,
                           Link = cf.Link,
                           LinkToken = cf.LinkToken,
                       }).ToList();
            gcDinhMuc.DataSource = obj;
            wait.Close();
            wait.Dispose();
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
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
                    var obj = db.web_Zalos.FirstOrDefault(o => o.Id == Id);
                    db.web_Zalos.DeleteOnSubmit(obj);
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
            if (grvDinhMuc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Dòng], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { ID = (int)grvDinhMuc.GetFocusedRowCellValue("Id") })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (frmEdit frm = new frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
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