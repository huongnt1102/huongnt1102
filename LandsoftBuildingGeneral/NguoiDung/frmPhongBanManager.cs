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

namespace LandsoftBuildingGeneral.NguoiDung
{
    public partial class frmPhongBanManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmPhongBanManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmPhongBanManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcPhongBan.DataSource = db.tnPhongBans.OrderBy(p=>p.STT);
                looktoanha.DataSource = db.tnToaNhas;
            }
            else
            {
                gcPhongBan.DataSource = db.tnPhongBans.Where(p => p.MaTN == objnhanvien.MaTN).OrderBy(p => p.STT);
                looktoanha.DataSource = db.tnToaNhas.Where(p=>p.MaTN == objnhanvien.MaTN);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvPhongBan.AddNewRow();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //Save:
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch(System.Exception ex)
            {
                //goto Save;
            }
            
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            grvPhongBan.DeleteSelectedRows();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvPhongBan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvPhongBan.DeleteSelectedRows();
            }
        }

        private void grvPhongBan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

    }
}