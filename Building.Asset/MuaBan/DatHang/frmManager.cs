using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
namespace TaiSan.DatHang
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        private int? FormID = 5;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKBC = new KyBaoCao();
            foreach (var str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
        }

        private void btnInDanhSachDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var rpt = new Report.rptDanhSachDonDatHang(tuNgay, denNgay);
                rpt.ShowPreviewDialog();
            }
        }

        private void btnInDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var frm = new Report.frmPrintControl(grvDonHang.GetFocusedRowCellValue("MaDH").ToString());
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void btnSSBG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmSoSanhBaoGia();
            frm.ShowDialog();
                
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var frm = new frmEdit() { })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (beiToaNha.EditValue == null) return;
                using (var frm = new frmEdit() { })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                        LoadDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void btnGiamDocKyDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var frm = new frmLyDoKhongDuyet() {  })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void btnChoDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmLyDoKhongDuyet frm = new frmLyDoKhongDuyet() {  })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    db.SubmitChanges();
                    LoadData();
                    LoadDetail();
                    grvDonHang.BestFitColumns();
                }
            }
            
        }

        private void grvDatHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
            
        }

        private void grvDonHang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvDonHang.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch
            {
                // ignored
            }
        }

        private void grvDonHang_DoubleClick(object sender, EventArgs e)
        {
            if (itemSua.Enabled == false) return;
            itemSua_ItemClick(null, null);
        }

        void LoadData()
        {
            
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= itemTuNgay_EditValueChanged;
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += itemTuNgay_EditValueChanged;
        }

        void LoadDetail()
        {

        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void bbiKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmLyDoKhongDuyet frm = new frmLyDoKhongDuyet() {  })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
            
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void grvTaiSan_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!grvTaiSan.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    var width = Convert.ToInt32(size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(width, grvTaiSan); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, grvTaiSan); }));
            }
        }
        bool Cal(Int32 width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void bbiTruongBoPhanVatTuDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmLyDoKhongDuyet frm = new frmLyDoKhongDuyet() { })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
            
        }

        private void bbiTruongBoPhanVatTuKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmLyDoKhongDuyet() {})
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {

                    LoadData();
                    LoadDetail();
                }
            }
            
        }
    }
}