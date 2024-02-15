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
    public partial class frmDeXuatDoiCa_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmDeXuatDoiCa_Manager()
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

                    gc.DataSource = (from _ in _db.tbl_PhanCong_DoiCas
                                     join tt in _db.tbl_PhanCong_DoiCa_TrangThais on _.IdTrangThai equals tt.Id into _ttrang
                                     from tt in _ttrang.DefaultIfEmpty()
                                     where SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, _.Ngay) >= 0 &
                                     SqlMethods.DateDiffDay(_.Ngay, (DateTime)beiDenNgay.EditValue) >= 0 &
                                     _.IsThayCa.GetValueOrDefault()==false & _.MaTN==Convert.ToInt32(beiToaNha.EditValue)
                                     select new
                                     {
                                         _.ID,
                                         _.IdDoiCa1,
                                         _.IdDoiCa2,
                                         _.IdPhanLoaiCaCu1,
                                         _.IdPhanLoaiCaCu2,
                                         _.IdPhanLoaiCaMoi1,
                                         _.IdPhanLoaiCaMoi2,
                                         _.MaNV1,
                                         _.MaNV2,
                                         _.Ngay,
                                         _.NgayDuyet,
                                         _.NgayLap,
                                         _.NgaySua,
                                         _.NguoiDuyet,
                                         _.NguoiLap,
                                         _.NguoiSua,
                                         _.IdTrangThai,
                                         TenTrangThai = tt.Name
                                     }).ToList();
                }
            }
            catch
            {
                // ignored
            }
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
            beiKBC.EditValue = objKbc.Source[3];
            SetDate(3);
            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            repPhanLoaiCa.DataSource = _db.tbl_PhanCong_PhanLoaiCas;
            LoadData();

            gv.BestFitColumns();
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

                var duyet = (bool?)gv.GetFocusedRowCellValue("IdDuyet");
                if (duyet == true)
                {
                    DialogBox.Error("Phiếu đã duyệt nên không được sửa.");
                    return;
                }

                using (var frm = new frmDoiCa
                {
                    MaTn = (byte?)beiToaNha.EditValue,
                    Ngay = (DateTime)beiTuNgay.EditValue,
                    IsSua = 1,
                    Id = (int?)gv.GetFocusedRowCellValue("ID")
                })
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
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_PhanCong_DoiCas.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        if (o.IdTrangThai != (byte?)20)
                            _db.tbl_PhanCong_DoiCas.DeleteOnSubmit(o);
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

            using (var frm = new frmDoiCa
            {
                MaTn = (byte?)beiToaNha.EditValue,
                Ngay = (DateTime)beiTuNgay.EditValue,
                IsSua = 0,
                Id = 0
            })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemDuyetDC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần duyệt");
                    return;
                }
                if (DialogBox.Question("Bạn muốn duyệt đồng ý đổi ca những phiếu này chứ?") == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_PhanCong_DoiCas.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        var dc1 = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ => _.ID == o.IdDoiCa1);
                        if (dc1 != null)
                        {
                            var dc2 = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ => _.ID == o.IdDoiCa2);
                            if (dc2 != null)
                            {
                                dc1.IDPhanLoaiCa = o.IdPhanLoaiCaMoi1;
                                dc1.NgaySua = DateTime.Now;
                                dc1.NguoiSua = Common.User.MaNV;

                                dc2.IDPhanLoaiCa = o.IdPhanLoaiCaMoi2;
                                dc2.NguoiSua = Common.User.MaNV;
                                dc2.NgaySua = DateTime.Now;

                                o.IdTrangThai = 20;//Đã duyệt
                                o.NgayDuyet = DateTime.Now;
                                o.NguoiDuyet = Common.User.MaNV;
                            }
                        }
                    }

                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                DialogBox.Error("Bị lỗi nên không xóa được");
                return;
            }
        }

        private void itemBoDuyetDC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                if (DialogBox.Question("Bỏ duyệt sẽ đổi lại ca cũ của nhân viên, bạn vẫn muốn tiếp tục?") == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_PhanCong_DoiCas.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        var dc1 = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ => _.ID == o.IdDoiCa1);
                        if (dc1 != null)
                        {
                            var dc2 = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ => _.ID == o.IdDoiCa2);
                            if (dc2 != null)
                            {
                                dc1.IDPhanLoaiCa = o.IdPhanLoaiCaCu1;
                                dc1.NgaySua = DateTime.Now;
                                dc1.NguoiSua = Common.User.MaNV;

                                dc2.IDPhanLoaiCa = o.IdPhanLoaiCaCu2;
                                dc2.NguoiSua = Common.User.MaNV;
                                dc2.NgaySua = DateTime.Now;

                                o.IdTrangThai = 30;//Không duyệt
                                o.NgayDuyet = new DateTime?();
                                o.NguoiDuyet = new int?();
                            }
                        }
                    }

                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                DialogBox.Error("Bị lỗi nên không xóa được");
                return;
            }
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}