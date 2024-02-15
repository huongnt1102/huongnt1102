using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Asset
{
    public partial class FrmAssetCategory : XtraForm
    {
        public FrmAssetCategory()
        {
            InitializeComponent();
        }

        private void FrmAssetCategory_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte?) itemBuilding.EditValue;
                var db = new MasterDataContext();
                gc.DataSource = db.ho_AssetCategories.Where(_=>_.BuildingId == buildingId);
            }
            catch{}
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmAssetCategoryEdit())
            {
                frm.BuildingId = (byte?) itemBuilding.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                
                using (var frm = new FrmAssetCategoryEdit { BuildingId = (byte)itemBuilding.EditValue, AssetCategoryId = (int?)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những tài sản cần xóa");
                        return;
                    }

                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var asset = db.ho_AssetCategories.FirstOrDefault(_ =>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (asset == null) continue;

                        db.ho_AssetCategories.DeleteOnSubmit(asset);
                    }

                    db.SubmitChanges();
                    gv.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Không xóa được, danh mục tài sản đã được chọn trong bàn giao mặt bằng");
            }
        }

        private void ItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var matn = (byte?)itemBuilding.EditValue;
            frmImportDanhMucTaiSan frm = new frmImportDanhMucTaiSan();
            frm.MaTN = matn;
            frm.ShowDialog();
            LoadData();
        }


        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }
    }
}