using Library;
using System.Linq;

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmBoPhanNhanEmail : DevExpress.XtraEditors.XtraForm
    {
        public int Loai { get; set; }

        public frmBoPhanNhanEmail()
        {
            InitializeComponent();
        }

        private void frmKeHoach_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);
            lkToaNha.DataSource = Library.Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            var objKbc = new Library.KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[1];

            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new Library.KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var db = new Library.MasterDataContext();
                gc.DataSource = db.tnKhachHang_BoPhan_NhanEmails;

            }
            catch { }
        }

        #region

        #endregion



        private void CbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {

        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                using (var frm = new DichVu.KhachHang.NguoiLienHe.frmBoPhanNhanEmail_AddAll { ID = 0 })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void Gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void ItemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                using (var frm = new DichVu.KhachHang.NguoiLienHe.frmBoPhanNhanEmail_AddAll { ID = (int?)gv.GetFocusedRowCellValue("ID") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void XtraTabControl1_Click(object sender, System.EventArgs e)
        {

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Library.Commoncls.ExportExcel(gc);
        }

        private void itemThemKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            
        }

        private void itemSuaKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemXoaKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemGuiEmailKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemAddAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}