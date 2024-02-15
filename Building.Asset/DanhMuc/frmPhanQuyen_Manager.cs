using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;
using System.Windows.Forms;

namespace Building.Asset.DanhMuc
{
    public partial class frmPhanQuyen_Manager : XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();

        public frmPhanQuyen_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }

            repNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV,o.MaSoNV }).ToList();

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                    gc.DataSource = _db.tbl_PhanQuyens.Where(_ => _.MaTN == (byte)beiToaNha.EditValue);
            }
            catch
            {
                // ignored
            }
            LoadDetail();
        }

        private void LoadDetail()
        {
            try
            {
                var id = (int?)gv.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabLichSu":
                        gcChiTiet.DataSource = _db.tbl_PhanQuyen_NhanViens.Where(_ => _.PhanQuyenId == id);
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvDanhSachYeuCau_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();
                DialogBox.Success();
            }
            catch
            {

            }
        }

        private void gv_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gv.SetFocusedRowCellValue("MaTN", (byte)beiToaNha.EditValue);
        }

        private void gvChiTiet_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("ID") == null)
            {
                return;
            }

            gvChiTiet.SetFocusedRowCellValue("PhanQuyenId", (int)gv.GetFocusedRowCellValue("ID"));
        }

        private void gvChiTiet_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gvChiTiet.GetSelectedRows().ToString()))
                {
                    gvChiTiet.DeleteSelectedRows();
                }
            }
        }

        private void itemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // tạo tất cả dữ liệu cho tất cả tòa nhà, chỉ tpl_PhanQuyen thôi

            if (gv.GetFocusedRowCellValue("ID") == null)
            {
                return;
            }

            var obj = _db.tbl_PhanQuyens.FirstOrDefault(_ => _.ID == (int) gv.GetFocusedRowCellValue("ID"));
            if (obj != null)
            {
                foreach (var i in _db.tnToaNhas)
                {
                    var kt = _db.tbl_PhanQuyens.FirstOrDefault(_ => _.MaTN == i.MaTN & _.MaSo == obj.MaSo);
                    if (kt == null)
                    {
                        var o = new tbl_PhanQuyen();
                        o.MaSo = obj.MaSo;
                        o.MaTN = i.MaTN;
                        o.FormName = obj.FormName;
                        _db.tbl_PhanQuyens.InsertOnSubmit(o);
                    }
                }
            }

            _db.SubmitChanges();
            DialogBox.Success();
        }
    }
}