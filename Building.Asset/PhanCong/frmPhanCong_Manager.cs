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
using System.Diagnostics;
namespace Building.Asset.PhanCong
{
    public partial class frmPhanCong_Manager : XtraForm
    {
        public frmPhanCong_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }
        #region Code
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }
        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                gc.DataSource = linqInstantFeedbackSource1;
            }
            catch
            {
                // ignored
            }
            gv.BestFitColumns();
            LoadDetail();
        }
        private void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
            gv.BestFitColumns();
        }
        #endregion

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            var db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);
            lkNhanVien.DataSource = db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            LoadData();

            gv.BestFitColumns();
        }

        private void LoadDetail()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gv.GetFocusedRowCellValue("ID");
                if (id == null)
                {

                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabChiTiet":

                        break;
                }
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                db.Dispose();
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    var db = new MasterDataContext();
                    var sql = from p in db.tbl_PhieuVanHanhs
                        join nts in db.tbl_NhomTaiSans on p.NhomTaiSanID equals nts.ID into nhomTaiSan
                        from nts in nhomTaiSan.DefaultIfEmpty()
                        where p.MaTN == (byte?) beiToaNha.EditValue
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.NgayPhieu) >= 0
                              & SqlMethods.DateDiffDay(p.NgayPhieu, (DateTime) beiDenNgay.EditValue) >= 0
                        orderby p.SoPhieu descending
                        select new
                        {
                            p.NgayPhieu,p.KeHoachVanHanhID,p.SoPhieu,p.NguoiThucHien,p.NguoiDuyet,p.MaTN,nts.TenNhomTaiSan
                        };
                    e.QueryableSource = sql;
                    e.Tag = db;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                ((MasterDataContext)e.Tag).Dispose();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        bool cal(Int32 width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }

        private void bbiThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = 0;
                if (gv.GetFocusedRowCellValue("ID") != null)
                {
                    id = (int)gv.GetFocusedRowCellValue("ID");
                }
                using (var frm = new frmLichTruc_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 0, Id = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) RefreshData();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void bbiSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }
                //using (var frm = new frmThietLapBaoTriDinhKy_Edit
                //{
                //    MaTn = (byte?)beiToaNha.EditValue,
                //    Id = (int)gv.GetFocusedRowCellValue("ID"),
                //    IsSua = 1
                //})
                //{
                //    frm.ShowDialog();
                //    if (frm.DialogResult == DialogResult.OK)
                //    {
                //        RefreshData();
                //    }
                //}
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    //var o =
                    //    db.tssdBaoHanh_DanhMuc_ThietLapDinhKy_ps.Single(
                    //        p => p.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    //if (o != null)
                    //{
                    //    // kiem tra xem co cai nao da duoc dung trong bang KhoTaiSan chua
                    //    var kt = (db.tssdNhapKho_KhoTaiSans.Where(p => p.MaDanhMucBaoHanhNCC == o.ID || p.MaDanhMucBaoHanhCty == o.ID))
                    //            .FirstOrDefault();
                    //    if (kt == null)
                    //    {
                    //        // neu k co cai nao thi moi xoa
                    //        // kiem tra xem co ct k
                    //        var ct =
                    //            (from p in db.tssdBaoHanh_DanhMuc_ThietLapDinhKy_cts where p.MaP == o.ID select p)
                    //                .ToList();
                    //        foreach (var row in ct)
                    //        {
                    //            db.tssdBaoHanh_DanhMuc_ThietLapDinhKy_cts.DeleteOnSubmit(row);
                    //        }

                    //        db.tssdBaoHanh_DanhMuc_ThietLapDinhKy_ps.DeleteOnSubmit(o);
                    //    }
                    //}
                }
                db.SubmitChanges();
                RefreshData();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void gvDanhSachYeuCau_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();

        }

        private void gvDanhSachYeuCau_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.Column.FieldName == "LoaiBaoHanh")
            {
                if (view != null)
                {
                    var category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["LoaiBaoHanh"]);
                    if (category == "Theo chu kỳ")
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void gvDanhSachYeuCau_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gv.IsGroupRow(e.RowHandle))
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
                    Int32 _width = Convert.ToInt32(size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gv); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle + 1));
                System.Drawing.SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gv); }));
            }

        }
    }
}