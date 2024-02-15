using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoTri
{
    public partial class frmDeXuatSuaChua_Manager : XtraForm
    {
        public frmDeXuatSuaChua_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

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
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    var db = new MasterDataContext();
                    gc.DataSource = (from p in db.tbl_DeXuatSuaChuas
                        join tt in db.tbl_DeXuatSuaChua_TrangThais on p.TrangThaiID equals tt.Id
                        where p.MaTN == (byte?) beiToaNha.EditValue
                              & SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, p.Ngay) >= 0
                              & SqlMethods.DateDiffDay(p.Ngay, (DateTime) beiDenNgay.EditValue) >= 0
                        orderby p.SoPhieu descending
                        select new
                        {
                            p.SoPhieu,
                            p.MaTN,
                            p.ID,
                            p.Ngay,
                            p.LyDo,
                            p.NguoiYeuCau,
                            SoPhieuDeXuat = p.tbl_PhieuVanHanh.SoPhieu,
                            TrangThaiPhieu = tt.Name,
                            p.TrangThaiID,
                            p.BuocDaDuyet, p.TongBuocDuyet, MaPhieuVanHanh = p.tbl_PhieuVanHanh.SoPhieu
                        }).ToList();
                }
            }
            catch
            {
                // ignored
            }
            LoadDetail();
        }
        private void RefreshData()
        {
            LoadData();
        }

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
                    //case "tabChiTiet":
                    //    gcChiTietCongViec.DataSource = (from p in db.tbl_PhieuBaoTri_ChiTiet_CongViecs
                    //        join cv in db.tbl_CongViec_TuyChon_Lists on (long)p.CongViecID equals cv.ID
                    //        where p.PhieuBaoTriID == id
                    //        select new
                    //        {
                    //            CongViecID=cv.tbl_CongViec_TuyChon.tbl_CongViec.TenCongViec,p.GhiChu,p.GiaTriChon,p.GiaTriNhap
                    //        }).ToList();
                    //    break;
                    case "xtraTabPage1":
                        gcChiTietTaiSan.DataSource =
                            db.tbl_DeXuatSuaChua_ChiTiets.Where(_ => _.IdDeXuatSuaChua == id);
                        break;
                }
                gvChiTietTaiSan.BestFitColumns();
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

        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        bool cal(Int32 width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
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
                using (var frm = new frmDeXuatSuaChua_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 0, Id = id })
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

                using (var frm = new frmDeXuatSuaChua_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 1, Id = (int)gv.GetFocusedRowCellValue("ID") })
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
                    var o = db.tbl_DeXuatSuaChuas.FirstOrDefault(_ => _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null && o.TrangThaiID!=(byte?)1)
                    {
                        db.tbl_DeXuatSuaChua_ChiTiets.DeleteAllOnSubmit(o.tbl_DeXuatSuaChua_ChiTiets);
                        db.tbl_DeXuatSuaChuas.DeleteOnSubmit(o);
                    }
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

        private void itemXLDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            int[] indexs = gv.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn những phiếu cần duyệt");
                return;
            }
            if (DialogBox.Question("Bạn có muốn duyệt không?") == DialogResult.No) return;

            foreach (var r in indexs)
            {
                var o = db.tbl_DeXuatSuaChuas.FirstOrDefault(_ => _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                if (o != null)
                {
                    o.TrangThaiID =(byte?)1;
                    db.SubmitChanges();
                }
            }
            db.SubmitChanges();
            RefreshData();
        }

        private void itemXLKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            int[] indexs = gv.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn những phiếu cần không duyệt");
                return;
            }
            if (DialogBox.Question("Bạn có muốn không duyệt không?") == DialogResult.No) return;

            foreach (var r in indexs)
            {
                var o = db.tbl_DeXuatSuaChuas.FirstOrDefault(_ => _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                if (o != null)
                {
                    o.TrangThaiID = (byte?)2;
                    db.SubmitChanges();
                }
            }
            db.SubmitChanges();
            RefreshData();
        }
    }
}