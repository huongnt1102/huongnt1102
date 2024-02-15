using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace Building.Asset.PhanCong
{
    public partial class frmBanGiaoCa_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmBanGiaoCa_Manager()
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
                gc.DataSource = null;
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    _db = new MasterDataContext();

                    gc.DataSource = _db.tbl_PhanCong_BanGiaoCas
                        .Where(_ => _.MaTN == ((byte?) beiToaNha.EditValue ?? Common.User.MaTN) &
                                    SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, _.NgayTao) >= 0 &
                                    SqlMethods.DateDiffDay(_.NgayTao, (DateTime) beiDenNgay.EditValue) >= 0).Select(_ =>
                            new
                            {
                                CaTruc = _.tbl_PhanCong_PhanLoaiCa.KyHieu,
                                Loai = _.tbl_PhanCong_BanGiaoCa_LoaiNhan.Name, _.MaSoPhieu, _.NguoiNhan, _.NguoiGiao,
                                _.NgayNhan, _.NgayGiao, _.NgayTao, _.NguoiTao, _.NgaySua, _.NguoiSua,_.ID,_.IDLoaiCa,_.IDLoaiNhan
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
            beiKBC.EditValue = objKbc.Source[0];
            SetDate(0);
            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            LoadData();

            gv.BestFitColumns();
        }

        private void LoadDetail()
        {
            _db = new MasterDataContext();
            try
            {
                var id = (int?)gv.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {

                    case "tabLichSu":
                        gcLichSu.DataSource = (from p in _db.tbl_PhanCong_BanGiaoCa_ThietBis
                            join tb in _db.tbl_PhanCong_ThietBis on p.IDThietBi equals tb.ID
                            join tt in _db.tbl_TinhTrangTaiSans on p.IDTinhTrangTaiSanNhan equals tt.ID
                            where p.IDBanGiaoCa == id 
                            select new
                            {
                                tb.Name,
                                p.SoLuongNhan,
                                tt.TenTinhTrang
                            }).ToList();
                        break;
                    case "tabDsCheckList":
                        gcDs.DataSource = (from p in _db.tbl_PhanCong_BanGiaoCa_ChiTiets
                            where p.IDBanGiaoCa == id & p.NameTable == "tbl_DSCheckList"
                            select new
                            {
                                p.KyHieu,
                                p.NameValue
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
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                var loaiCa = (int?) gv.GetFocusedRowCellValue("IDLoaiNhan");
                switch (loaiCa)
                {
                    case 1:
                        using (var frm = new frmNhanCa_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (int?)gv.GetFocusedRowCellValue("ID") })
                        {
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.OK) LoadData();
                        }

                        break;
                    case 2:
                        using (var frm = new frmGiaoCa_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 1, Id = (int?)gv.GetFocusedRowCellValue("ID") })
                        {
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.OK) LoadData();
                        }

                        break;
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
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_PhanCong_BanGiaoCas.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        foreach (var s in o.tbl_PhanCong_BanGiaoCa_ThietBis)
                        {
                            var tb = _db.tbl_PhanCong_ThietBis.FirstOrDefault(_ =>
                                _.ID == s.IDThietBi & _.IDTinhTrangTaiSan == s.IDTinhTrangTaiSanNhan);
                            if (tb != null)
                            {
                                if (o.IDLoaiNhan == 1)
                                {
                                    tb.SoLuong = tb.SoLuong + s.SoLuongNhan;
                                }
                                else
                                {
                                    tb.SoLuong = tb.SoLuong - s.SoLuongNhan;
                                }
                            }

                            _db.SubmitChanges();
                        }
                        _db.tbl_PhanCong_BanGiaoCa_ThietBis.DeleteAllOnSubmit(o.tbl_PhanCong_BanGiaoCa_ThietBis);
                        _db.tbl_PhanCong_BanGiaoCas.DeleteOnSubmit(o);
                    }

                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng thiết bị này nên không xóa được");
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
            //try
            //{
            //    using (var frm = new Import.frmThietBi_Import())
            //    {
            //        frm.MaTn = (byte)beiToaNha.EditValue;
            //        frm.ShowDialog();
            //        if (frm.IsSave)
            //            LoadData();
            //    }
            //}
            //catch
            //{
            //    //
            //}
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (int)gv.GetFocusedRowCellValue("ID");
            }

            using (var frm = new frmNhanCa_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemGiaoCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (int)gv.GetFocusedRowCellValue("ID");
            }

            using (var frm = new frmGiaoCa_Edit { MaTn = (byte)beiToaNha.EditValue, IsSua = 0, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

    }
}