using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace HopDongThueNgoai.DanhSachCongViec
{
    public partial class FrmDanhSachCongViec : XtraForm
    {
        public FrmDanhSachCongViec()
        {
            InitializeComponent();
        }

        #region Ham xu ly
        private void LoadData()
        {
            try
            {
                gcNhomCongViec.DataSource = null;
                gcNhomCongViec.DataSource = linqInstantFeedbackSource1;
            }
            catch (Exception ex)
            {
                DialogBox.Error("Không load được dữ liệu do: " + ex.Message);
            }
        }
        private void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }
        private void ThemNhomCongViec()
        {
            try
            {
                using (frmThemNhom frm = new frmThemNhom())
                {
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                        RefreshData();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error("Không thêm được nhóm công việc do : " + ex.Message);
            }
        }
        private void DanhSachCongViecCon()
        {
            var db = new MasterDataContext();
            try
            {
                var id = gvNhomCongViec.GetFocusedRowCellValue("RowID");
                if (id == null)
                {
                    gcCongViecCon.DataSource = null;
                    return;
                }
                gcCongViecCon.DataSource = db.hdctnCongViecs.Where(_ => _.NhomCongViecId == (int)id).ToList();
            }
            catch
            {

            }
        }

        private void Delete()
        {
            int[] indexs = gvNhomCongViec.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn Nhóm công việc cần xóa. Xin cảm ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            var db = new MasterDataContext();
            try
            {
                foreach (int i in indexs)
                {
                    var hd = db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_=>_.NhomCongViecId == (int)gvNhomCongViec.GetRowCellValue(i,"RowID"));
                    if (hd!=null)
                    {
                        DialogBox.Error("Nhóm công việc này đã được dùng trong hợp đồng. Vui lòng xóa hợp đồng trước!");
                        return;
                    }
                    db.hdctnCongViecs.DeleteAllOnSubmit(db.hdctnCongViecs.Where(_=>_.NhomCongViecId == (int?)gvNhomCongViec.GetRowCellValue(i,"RowID")));
                    db.hdctnNhomCongViecs.DeleteAllOnSubmit(db.hdctnNhomCongViecs.Where(_=>_.RowID == (int?)gvNhomCongViec.GetRowCellValue(i,"RowID")));
                }

                db.SubmitChanges();


            }
            catch
            {

            }

            this.RefreshData();
        }

        #endregion
        // code
        private void frmDanhSachCongViec_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            gvNhomCongViec.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            var sql = db.hdctnNhomCongViecs;
            e.QueryableSource = sql;
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Không load được dữ liệu do: " + ex.Message);
            }
        }

        private void bbiThemNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ThemNhomCongViec();
        }

        private void bbiNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void gvNhomCongViec_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DanhSachCongViecCon();
        }

        private void gvNhomCongViec_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(width, gvNhomCongViec); }));
            }
        }
        bool cal(Int32 _width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _width ? _width : _View.IndicatorWidth;
            return true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void gvNhomCongViec_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.DanhSachCongViecCon();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmThemNhom frm = new frmThemNhom(){ NhomCongViecId = (int?)gvNhomCongViec.GetFocusedRowCellValue("RowID")})
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    RefreshData();
            }
        }
    }
}