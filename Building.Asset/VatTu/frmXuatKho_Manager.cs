using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace Building.Asset.VatTu
{
    public partial class frmXuatKho_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmXuatKho_Manager()
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

                    glkKho.DataSource = _db.tbl_VatTu_Khos;

                    glkLoaiXuat.DataSource = _db.tbl_VatTu_XuatKho_LoaiXuats;

                    gc.DataSource = _db.tbl_VatTu_XuatKhos.Where(_ =>
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
                        gcChiTiet.DataSource = (from p in _db.tbl_VatTu_XuatKho_ChiTiets
                            where p.XuatKhoID == id
                            select new
                            {
                                p.tbl_VatTu.Ten, p.tbl_VatTu.KyHieu, p.tbl_VatTu.tbl_VatTu_DVT.TenDVT, p.SoLuong,
                                p.DonGia,p.ThanhTien
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

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmXuatKho_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (long?)gv.GetFocusedRowCellValue("ID") })
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_VatTu_XuatKhos.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        _db.tbl_VatTu_SoKhos.DeleteAllOnSubmit(_db.tbl_VatTu_SoKhos.Where(_ => _.IDPhieu == o.ID));
                        _db.tbl_VatTu_XuatKho_ChiTiets.DeleteAllOnSubmit(o.tbl_VatTu_XuatKho_ChiTiets);
                        _db.tbl_VatTu_XuatKhos.DeleteOnSubmit(o);
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

        private void gv_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gv.GetFocusedRowCellValue("ID");
            }
            using (var frm = new frmXuatKho_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmXuatKho_Import())
                {
                    frm.MaTn = (byte)beiToaNha.EditValue;
                    frm.ShowDialog();
                    frm.LoaiXuat = 0;
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                // 
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}