using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace TaiSan.DeXuat
{
    public partial class frmManager : XtraForm
    {

        private int? _formId = 29;

        public frmManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            var objKbc = new KyBaoCao();
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;
            foreach (var str in objKbc.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKbc.Source[3];
            SetDate(3);
        }

        private void itemTruongBPDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemBPVTDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemBGĐuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnInPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDeXuat.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn đề xuất!");
                return;
            }
            var madx = (int)gvDeXuat.GetFocusedRowCellValue("MaDX");
            var db=new MasterDataContext();

            var objDx = (from p in db.dxmsDeXuats where p.MaDX == madx select p).FirstOrDefault();
            if (objDx != null)
            {

                using (var frm = new frmEdit { objnhanvien = Common.User, _MaDX = objDx.MaDX, MaTN = (byte)beiToaNha.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        RefeshData();
                    }
                }
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (beiToaNha.EditValue != null)
                {
                    using (frmEdit frm = new frmEdit() {objnhanvien = Common.User, MaTN = (byte) beiToaNha.EditValue})
                    {
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            RefeshData();
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void btnChoDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void grvDeXuat_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void grvDeXuat_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void gcDeXuat_DockChanged(object sender, EventArgs e)
        {

        }

        private void grvDeXuat_DoubleClick(object sender, EventArgs e)
        {

        }

        private void RefeshData()
        {

        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao();
            objKbc.Index = index;
            objKbc.SetToDate();

            itemTuNgay.EditValueChanged -= itemTuNgay_EditValueChanged;
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
            itemTuNgay.EditValueChanged += itemTuNgay_EditValueChanged;
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {

        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bsiTruongBoPhanKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void bsiBoPhanVatTuKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void bsiGiamDocKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

    }
}