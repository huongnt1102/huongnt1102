using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.YeuCau
{
    public partial class frmCatDat : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmCatDat()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        void LoadData(byte? maTN, int? maPB)
        {
            try
            {
                gcCaiDat.DataSource = db.tnycCaiDats.Where(p => p.MaTN == maTN & p.MaPB == maPB);
            }
            catch
            {
                gcCaiDat.DataSource = null;
            }
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValueChanged -= new EventHandler(itemToaNha_EditValueChanged);
            itemToaNha.EditValue = Common.User.MaTN;
            itemToaNha.EditValueChanged += new EventHandler(itemToaNha_EditValueChanged);

            var ltPB = db.tnPhongBans.ToList();
            if(ltPB.Count > 0)
            {
                lookPhongBan.DataSource = ltPB;
                itemPhongBan.EditValue = ltPB.FirstOrDefault().MaPB;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvCaiDat.AddNewRow();
        }

        private void grvTrangThai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvCaiDat.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            grvCaiDat.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvCaiDat.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData((byte?)itemToaNha.EditValue, (int?)itemPhongBan.EditValue);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lookNhanVien.DataSource = db.tnNhanViens.Where(p => p.MaTN == (byte?)itemToaNha.EditValue & p.MaPB == (int?)itemPhongBan.EditValue).Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });

                LoadData((byte?)itemToaNha.EditValue, (int?)itemPhongBan.EditValue);
            }
            catch { }
        }

        private void itemPhongBan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lookNhanVien.DataSource = db.tnNhanViens.Where(p => p.MaTN == (byte?)itemToaNha.EditValue & p.MaPB == (int?)itemPhongBan.EditValue).Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });

                LoadData((byte?)itemToaNha.EditValue, (int?)itemPhongBan.EditValue);
            }
            catch { }
        }

        private void grvCaiDat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvCaiDat.SetFocusedRowCellValue("MaTN", (byte?)itemToaNha.EditValue);
            grvCaiDat.SetFocusedRowCellValue("MaPB", (int?)itemPhongBan.EditValue);
            grvCaiDat.SetFocusedRowCellValue("IsRemind", true);
            grvCaiDat.SetFocusedRowCellValue("IsMail", true);
            grvCaiDat.SetFocusedRowCellValue("IsSMS", true);
        }
    }
}