using System;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using Library;
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.MuaHang
{
    public partial class frmManager : XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            var objKbc = new KyBaoCao();
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            foreach (var str in objKbc.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKbc.Source[7];
            SetDate(7);
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaHang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn đề xuất");
            }
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
            PhieuMH_Xoa();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuMH_Sua();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuMH_Them();
        }

        private void itemThemTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThanhToan_Them();
        }

        private void itemXoaTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThanhToan_Xoa();
        }

        private void itemSuaTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThanhToan_Sua();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThanhToan_Them();
        }

        private void btnTraHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuMH_Tra();
        }

        private void btnTaiLieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        private void grvMuaHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void grvMuaHang_DoubleClick(object sender, EventArgs e)
        {
            if (itemSua.Enabled == false) return;
            PhieuMH_Sua();
        }

        private void grvMuaHang_RowStyle(object sender, RowStyleEventArgs e)
        {
        }

        private void grvMuaHang_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void LoadData()
        {

        }

        private void LoadDetail()
        {

        }

        private void PhieuMH_Them()
        {
            try
            {
                if (beiToaNha.EditValue != null)
                {
                    using (var frm = new frmEdit() {  })
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
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void PhieuMH_Tra()
        {
            try
            {
                if (grvMuaHang.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn đơn hàng cần trả hàng!");
                    return;
                }

                using (var frm = new frmTraHang() {  })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                        LoadDetail();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void PhieuMH_Sua()
        {
            try
            {
                if (grvMuaHang.FocusedRowHandle < 0)
                {
                    DialogBox.Error("Vui lòng chọn đơn hàng cần sửa!");
                    return;
                }

                using (var frm = new frmEdit() {  })
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

        private void PhieuMH_Xoa()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void ThanhToan_Them()
        {
            try
            {

                var frm = new frmThanhToan();
                
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void ThanhToan_Xoa()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void ThanhToan_Sua()
        {
            try
            {

                var frm = new frmThanhToan();
                
                frm.ShowDialog();
                LoadData();
                LoadDetail();
            }
            catch
            {
                // ignored
            }
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
            LoadDetail();
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

    }
}
