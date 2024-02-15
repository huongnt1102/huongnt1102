using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace Building.Asset.VatTu
{
    public partial class frmDeXuat_Manager : XtraForm
    {
        /// <summary>
        /// TrangThaiID: 1- Chưa duyệt, 2 - Đã duyệt, 3- Đang mua hàng, 4-Đã mua hàng
        /// </summary>
 
        private MasterDataContext _db;

        public frmDeXuat_Manager()
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
            _db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();

            var l = new List<TrangThaiDeXuat>
            {
                new TrangThaiDeXuat {ID = 1, Ten = "Chưa Duyệt"},
                new TrangThaiDeXuat {ID = 2, Ten = "Đã duyệt"},
                new TrangThaiDeXuat {ID = 3, Ten = "Đang mua hàng"},
                new TrangThaiDeXuat {ID = 4, Ten = "Đã mua hàng"}
            };
            glkTrangThai.DataSource = l;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    _db = new MasterDataContext();

                    gc.DataSource = _db.tbl_VatTu_DeXuats.Where(_ =>
                        SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, _.NgayPhieu) >= 0 &
                        SqlMethods.DateDiffDay(_.NgayPhieu, (DateTime)beiDenNgay.EditValue) >= 0 & _.MaTN == (byte)beiToaNha.EditValue);
                }
            }
            catch
            {
                // ignored
            }
            LoadDetail();
        }

        public class TrangThaiDeXuat
        {
            public int? ID { get; set; }
            public string Ten { get; set; }
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

        private void LoadDetail()
        {
            _db = new MasterDataContext();
            try
            {
                var id = (long?)gv.GetFocusedRowCellValue("ID");
                var trangThaiId = (int?)gv.GetFocusedRowCellValue("TrangThaiID");
                if (id == null)
                {
                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabLichSu":
                        gcChiTiet.DataSource = (from p in _db.tbl_VatTu_DeXuat_ChiTiets
                            where p.DeXuatID == id
                            select new
                            {
                                p.tbl_VatTu.Ten, p.tbl_VatTu.KyHieu, p.tbl_VatTu.tbl_VatTu_DVT.TenDVT, p.SoLuongDeXuat,
                                SoLuongDuyet=trangThaiId==1?0:p.SoLuongDuyet, p.SoLuongNhapKho,p.SoLuongMuaHang
                            }).ToList();
                        break;
                    case "tabPhieuDeXuatSuaChua":
                        gcPhieuDeXuatSuaChua.DataSource = (from p in _db.tbl_DeXuatSuaChuas
                            join ta in _db.tbl_VatTu_DeXuat_PhieuSuaChuas on p.ID equals ta.PhieuDeXuatSuaChuaID
                            join tt in _db.tbl_DeXuatSuaChua_TrangThais on p.TrangThaiID equals tt.Id
                            join nv in _db.tnNhanViens on p.NguoiYeuCau equals nv.MaNV
                            where ta.DeXuatID == id
                            orderby p.SoPhieu descending
                            select new
                            {
                                p.SoPhieu,
                                p.MaTN,
                                p.ID,
                                p.Ngay,
                                p.LyDo,
                                NguoiYeuCau=nv.HoTenNV,
                                SoPhieuDeXuat = p.tbl_PhieuVanHanh.SoPhieu,
                                TrangThaiPhieu = tt.Name,
                                p.TrangThaiID,
                                p.BuocDaDuyet,
                                p.TongBuocDuyet,
                                MaPhieuVanHanh = p.tbl_PhieuVanHanh.SoPhieu
                            }).ToList();
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                _db.Dispose();
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bbiSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                var kt = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ =>
                    _.ID == (long?) gv.GetFocusedRowCellValue("ID") & _.TrangThaiID == 1);
                if (kt == null)
                {
                    DialogBox.Error("Phiếu này đã duyệt hoặc đang đặt hàng nên không sửa được nữa");
                    return;
                }

                using (var frm = new frmDeXuat_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (long?)gv.GetFocusedRowCellValue("ID") })
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

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                var indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()) 
                        //& (_.TrangThaiID==1||_.TrangThaiID==3)
                        );
                    if (o != null)
                    {
                        _db.tbl_VatTu_DeXuat_ChiTiets.DeleteAllOnSubmit(o.tbl_VatTu_DeXuat_ChiTiets);
                        _db.tbl_VatTu_DeXuat_PhieuSuaChuas.DeleteAllOnSubmit(o.tbl_VatTu_DeXuat_PhieuSuaChuas);
                        _db.tbl_VatTu_DeXuats.DeleteOnSubmit(o);
                    }

                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng phiếu này nên không xóa được");
                return;
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

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmDeXuat_Import())
                {
                    frm.MaTn = (byte)beiToaNha.EditValue;
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

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gv.GetFocusedRowCellValue("ID");
            }

            using (var frm = new frmDeXuat_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemDuyetDeXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                var kt = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ =>
                    _.ID == (long?)gv.GetFocusedRowCellValue("ID") & _.TrangThaiID !=3);
                if (kt == null)
                {
                    DialogBox.Error("Phiếu này đã duyệt hoặc đang đặt hàng nên không sửa được nữa");
                    return;
                }

                using (var frm = new frmDeXuat_Duyet { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (long?)gv.GetFocusedRowCellValue("ID") })
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

        private void itemBoDuyetDeXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần bỏ duyệt");
                    return;
                }
                if (DialogBox.Question("Bạn muốn bỏ duyệt?") == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()) & _.TrangThaiID != 3);
                    if (o != null)
                    {
                        foreach (var i in o.tbl_VatTu_DeXuat_ChiTiets)
                        {
                            i.SoLuongDuyet = null;
                        }

                        o.NguoiDuyet = new int?();
                        o.TrangThaiID = 1;
                        o.NgayDuyet = new DateTime?();
                    }

                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng phiếu này nên không thực hiện được");
                return;
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}