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

namespace Building.Asset.DanhMuc
{
    public partial class frmVatTu_Manager : XtraForm
    {
        private MasterDataContext _db;
        public frmVatTu_Manager()
        {
            InitializeComponent();
            _db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmVatTu_Manager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            repositoryItemLookUpEditToaNha.DataSource = Common.TowerList;
            barEditItemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        void LoadData()
        {
            _db = new MasterDataContext();

            repKhoiNha.DataSource = _db.mbKhoiNhas.Where(_ => _.MaTN == (byte?) barEditItemToaNha.EditValue);
            repDonViTinh.DataSource = _db.tbl_VatTu_DVTs.Where(_ => _.NgungSuDung ==false);

            gc.DataSource = _db.tbl_VatTus.Where(_ => _.MaTN == (byte?) barEditItemToaNha.EditValue);

            gv.BestFitColumns();
        }

        private void barButtonItemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmVatTu_Edit
            {
                MaTn = (byte) barEditItemToaNha.EditValue, IsSua = 0, Id = (long?) gv.GetFocusedRowCellValue("ID")
            };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) LoadData();
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var o = _db.tbl_VatTus.FirstOrDefault(p =>p.ID == (long) gv.GetFocusedRowCellValue("ID"));
                if (o != null)
                {
                    _db.tbl_VatTus.DeleteOnSubmit(o);
                }
                _db.SubmitChanges();
                gv.DeleteSelectedRows();
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Không xóa được vì danh mục vật tư này đã được dùng ở nơi khác");
            }
        }

        private void barEditItemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void barButtonItemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn vật tư cần sửa");
                return;
            }
            var frm = new frmVatTu_Edit
            {
                MaTn = (byte)barEditItemToaNha.EditValue,
                IsSua = 1,
                Id = (long?)id
            };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) LoadData();
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmVatTu_Import())
                {
                    frm.MaTn = (byte)barEditItemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}