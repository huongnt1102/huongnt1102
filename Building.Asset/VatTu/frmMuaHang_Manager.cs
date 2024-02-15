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
    public partial class frmMuaHang_Manager : XtraForm
    {
        /// <summary>
        /// TrangThaiTraTien: 1- Chưa thanh toán, 2- Đang thanh toán, 3- Đã trả hết
        /// TrangThaiNhapKho: 1- Chưa nhập kho, 2- Đang nhập kho, 3- Đã nhập kho hết
        /// </summary>
 
        private MasterDataContext _db;

        public frmMuaHang_Manager()
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

            var l1 = new List<TrangThaiTraTien>
            {
                new TrangThaiTraTien {ID = 1, Ten = "Chưa thanh toán"},
                new TrangThaiTraTien {ID = 2, Ten = "Đang thanh toán"},
                new TrangThaiTraTien {ID = 3, Ten = "Đã trả hết"}
            };
            glkTrangThaiTraTien.DataSource = l1;

            var l2 = new List<TrangThaiNhapKho>
            {
                new TrangThaiNhapKho {ID = 1, Ten = "Chưa nhập kho"},
                new TrangThaiNhapKho {ID = 2, Ten = "Đang nhập kho"},
                new TrangThaiNhapKho {ID = 3, Ten = "Đã nhập kho hết"}
            };
            glkTrangThaiNhapKho.DataSource = l2;

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

                    glkNhaCungCap.DataSource = _db.tbl_NhaCungCapTaiSans;
                    glkPhieuDeXuat.DataSource = _db.tbl_VatTu_DeXuats.Where(_ => _.MaTN == (byte) beiToaNha.EditValue);

                    gc.DataSource = _db.tbl_VatTu_MuaHangs.Where(_ =>
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

        public class TrangThaiTraTien
        {
            public int? ID { get; set; }
            public string Ten { get; set; }
        }

        public class TrangThaiNhapKho
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
                if (id == null)
                {
                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabLichSu":
                        gcChiTiet.DataSource = (from p in _db.tbl_VatTu_MuaHang_ChiTiets
                            where p.MuaHangID == id
                            select new
                            {
                                p.tbl_VatTu.Ten, p.tbl_VatTu.KyHieu, p.tbl_VatTu.tbl_VatTu_DVT.TenDVT, p.SoLuongDeXuat,
                                p.SoLuong, p.DonGia,p.ThanhTien,p.SoLuongNhapKho
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

                var kt = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ =>
                    _.ID == (long?) gv.GetFocusedRowCellValue("ID") & _.TrangThaiNhapKhoID == 1);
                if (kt == null)
                {
                    DialogBox.Error("Phiếu này đã duyệt hoặc đang đặt hàng nên không sửa được nữa");
                    return;
                }

                if (gv.GetFocusedRowCellValue("DeXuatID") == null)
                {
                    using (var frm = new frmMuaHang_KhongDeXuat { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (long?)gv.GetFocusedRowCellValue("ID") })
                    {
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK) LoadData();
                    }
                }
                else
                {
                    using (var frm = new frmMuaHang_CoDeXuat { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (long?)gv.GetFocusedRowCellValue("ID") })
                    {
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK) LoadData();
                    }
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
                    //var o = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ =>
                    //    _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()) & _.TrangThaiNhapKhoID == 1 & _.TongTienDaTra <= 0);
                    var o = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()) & _.TongTienDaTra <= 0);
                    if (o != null)
                    {
                        if (o.DeXuatID != null)
                        {
                            var deXuat = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ => _.ID == o.DeXuatID);
                            if (deXuat != null)
                            {
                                var l = deXuat.tbl_VatTu_DeXuat_ChiTiets;
                                foreach (var i in o.tbl_VatTu_MuaHang_ChiTiets)
                                {
                                    var l1 = l.FirstOrDefault(_ => _.ID == i.DeXuatChiTietID);
                                    if (l1 != null)
                                    {
                                        l1.SoLuongMuaHang = l1.SoLuongMuaHang - i.SoLuong;
                                    }
                                }

                                var kt2 = l.Where(_ => _.SoLuongMuaHang> 0).ToList();
                                if (kt2.Count > 0)
                                {
                                    var kt = l.Where(_ => (_.SoLuongDuyet - _.SoLuongMuaHang) > 0).ToList();
                                    deXuat.TrangThaiID = kt.Count > 0 ? 3 : 4;
                                }
                                else
                                    deXuat.TrangThaiID = 2;
                            }
                        }

                        _db.tbl_VatTu_MuaHang_ThanhToans.DeleteAllOnSubmit(o.tbl_VatTu_MuaHang_ThanhToans);
                        _db.tbl_VatTu_MuaHang_ChiTiets.DeleteAllOnSubmit(o.tbl_VatTu_MuaHang_ChiTiets);
                        _db.tbl_VatTu_MuaHangs.DeleteOnSubmit(o);
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
                using (var frm = new Import.frmMuaHang_Import())
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

        private void itemThemTuDeXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gv.GetFocusedRowCellValue("ID");
            }

            using (var frm = new frmMuaHang_CoDeXuat { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemThemPhieuMuaHangKhongDeXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = 0;
            if(gv.GetFocusedRowCellValue("ID")!=null)
            {
                id = (long) gv.GetFocusedRowCellValue("ID");
            }
            using (var frm = new frmMuaHang_KhongDeXuat { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((int?)gv.GetFocusedRowCellValue("TrangThaiNhapKhoID") == 3)
            {
                DialogBox.Error("Phiếu này đã thanh toán xong, không cần thanh toán nữa");
                return;
            }

            long id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gv.GetFocusedRowCellValue("ID");
            }

            using (var frm = new frmMuaHang_ThanhToan { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = 0,IdMuaHang=id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}