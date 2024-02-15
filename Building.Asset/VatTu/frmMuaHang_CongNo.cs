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
    public partial class frmMuaHang_CongNo : XtraForm
    {
        /// <summary>
        /// TrangThaiTraTien: 1- Chưa thanh toán, 2- Đang thanh toán, 3- Đã trả hết
        /// TrangThaiNhapKho: 1- Chưa nhập kho, 2- Đang nhập kho, 3- Đã nhập kho hết
        /// </summary>
 
        private MasterDataContext _db;

        public frmMuaHang_CongNo()
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

            var l1 = new List<TrangThaiTraTien>();
            l1.Add(new TrangThaiTraTien { ID = 1, Ten = "Chưa thanh toán" });
            l1.Add(new TrangThaiTraTien { ID = 2, Ten = "Đang thanh toán" });
            l1.Add(new TrangThaiTraTien { ID = 3, Ten = "Đã trả hết" });
            glkTrangThaiTraTien.DataSource = l1;

            var l2 = new List<TrangThaiNhapKho>();
            l2.Add(new TrangThaiNhapKho { ID = 1, Ten = "Chưa nhập kho" });
            l2.Add(new TrangThaiNhapKho { ID = 2, Ten = "Đang nhập kho" });
            l2.Add(new TrangThaiNhapKho { ID = 3, Ten = "Đã nhập kho hết" });
            glkTrangThaiNhapKho.DataSource = l2;

            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();

            LoadData();

            gv.BestFitColumns();
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

                    gc.DataSource = (from p in _db.tbl_VatTu_MuaHangs
                                     where p.MaTN == (byte)beiToaNha.EditValue & SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, p.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(p.NgayPhieu, (DateTime)beiDenNgay.EditValue) >= 0
                        group new {p} by new {p.MaNCC}
                        into g
                        select new
                        {
                            g.Key.MaNCC, TongTienPhieu = g.Sum(_ => _.p.TongTienPhieu).GetValueOrDefault(),
                            TongTienDaTra = g.Sum(_ => _.p.TongTienDaTra).GetValueOrDefault(),
                            TongTienConLai = g.Sum(_ => _.p.TongTienConLai).GetValueOrDefault()
                        }).ToList();
                }
            }
            catch
            {
                // ignored
            }
            gv.BestFitColumns();
            LoadDetail();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
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

            glkNhaCungCap.DataSource = _db.tbl_NhaCungCapTaiSans;
            glkPhieuDeXuat.DataSource = _db.tbl_VatTu_DeXuats.Where(_ => _.MaTN == (byte)beiToaNha.EditValue);

            try
            {
                var id = (int?)gv.GetFocusedRowCellValue("MaNCC");
                if (id == null)
                {
                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabPhieuMuaHang":
                        gcPhieuMuaHang.DataSource = _db.tbl_VatTu_MuaHangs.Where(_ => _.MaNCC == id).OrderBy(_=>_.TrangThaiTraTienID);
                        break;
                    case "tabLichSu":
                        gcLichSu.DataSource = (from p in _db.tbl_VatTu_MuaHang_ThanhToans
                            join nv in _db.tnNhanViens on p.NguoiThanhToan equals nv.MaNV
                            where p.tbl_VatTu_MuaHang.MaNCC == id
                            select new
                            {
                                nv.HoTenNV, p.NgayThanhToan, p.SoTien, p.GhiChu,p.ID,p.MuaHangID,PhieuMuaHang=p.tbl_VatTu_MuaHang.MaPhieu
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

        private void gvDanhSachYeuCau_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemThemPhieuThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((int?)gvPhieuMuaHang.GetFocusedRowCellValue("TrangThaiNhapKhoID") == 3)
            {
                DialogBox.Error("Phiếu này đã thanh toán xong, không cần thanh toán nữa");
                return;
            }

            long id = 0;
            if (gvPhieuMuaHang.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gvPhieuMuaHang.GetFocusedRowCellValue("ID");
            }

            using (var frm = new frmMuaHang_ThanhToan { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = 0, IdMuaHang = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemXoaPhieuThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                int[] indexs = gvLichSu.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.Question("Bạn muốn xóa phiếu thanh toán?") == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_VatTu_MuaHang_ThanhToans.FirstOrDefault(_ =>
                        _.ID == int.Parse(gvLichSu.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        var muaHang = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ => _.ID == o.MuaHangID);
                        if (muaHang != null)
                        {
                            muaHang.TongTienDaTra = muaHang.TongTienDaTra - o.SoTien;
                            muaHang.TongTienConLai = muaHang.TongTienPhieu - muaHang.TongTienDaTra;

                            muaHang.TrangThaiTraTienID = 2;
                            if (muaHang.TongTienConLai == 0) muaHang.TrangThaiTraTienID = 3;
                        }

                        _db.tbl_VatTu_MuaHang_ThanhToans.DeleteOnSubmit(o);
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

        private void itemSuaPhieuThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = 0;
            if (gvLichSu.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gvLichSu.GetFocusedRowCellValue("ID");
            }

            long muaHangId = 0;
            if(gvLichSu.GetFocusedRowCellValue("MuaHangID")!=null)
            {
                muaHangId = (long) gvLichSu.GetFocusedRowCellValue("MuaHangID");
            }

            using (var frm = new frmMuaHang_ThanhToan { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = id, IdMuaHang = muaHangId })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }
    }
}