using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using BarcodeLib;
using QRCoder;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Building.Asset.BaoCao;

namespace Building.Asset.DanhMuc
{
    public partial class frmListTaiSan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext _db;

        public frmListTaiSan()
        {
            InitializeComponent();
            _db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadDetail()
        {
            long id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gv.GetFocusedRowCellValue("ID");
            }
            else
                return;
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "xtraTabPage1":
                    gcChiTiet.DataSource = //_db.tbl_ChiTietTaiSan_KhoHangs.Where(o => o.ID == id);
                        from ctts in _db.tbl_ChiTietTaiSan_KhoHangs
                        join ts in _db.tbl_ChiTietTaiSans on ctts.MaChiTietTaiSanID equals ts.ID into tsr
                        from ts in tsr.DefaultIfEmpty()
                        where ctts.MaChiTietTaiSanID == id
                        select new
                        {
                            ctts.SoLuong,
                            ts.MaChiTietTaiSan,
                        };
                    break;
                case "tabVatTu":
                    gcVatTu.DataSource = (from p in _db.tbl_VatTu_XuatKho_ChiTiets
                                          where p.ChiTietTaiSanID == id
                                          select new
                                          {
                                              p.tbl_VatTu.Ten,
                                              p.tbl_VatTu.KyHieu,
                                              p.tbl_VatTu.tbl_VatTu_DVT.TenDVT,
                                              p.SoLuong,
                                              p.DonGia,
                                              p.ThanhTien,
                                              p.ChiTietTaiSanID
                                          }).ToList();
                    break;
            }
        }

        void LoadData()
        {
            _db = new MasterDataContext();
            //lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            //repKhoiNha.DataSource =
            //    _db.mbKhoiNhas.Where(_ => _.MaTN == ((byte?) itemToaNha.EditValue ?? Common.User.MaTN)).ToList();
            //repNhaCungCap.DataSource = _db.tbl_NhaCungCapTaiSans;
            gc.DataSource = //_db.tbl_ChiTietTaiSans.Where(_=>_.MaTN==((byte?)itemToaNha.EditValue??Common.User.MaTN)&_.TenTaiSanID==(int?)itemTenTaiSan.EditValue);
                from ts in _db.tbl_ChiTietTaiSans
                join ncc in _db.tbl_NhaCungCapTaiSans on ts.NhaCungCapID equals ncc.ID into nccr
                from ncc in nccr.DefaultIfEmpty()
                join ttts in _db.tbl_TinhTrangTaiSans on ts.TinhTrangTaiSanID equals ttts.ID into tttsr
                from ttts in tttsr.DefaultIfEmpty()
                join tts in _db.tbl_TenTaiSans on ts.TenTaiSanID equals tts.ID into ttsr
                from tts in ttsr.DefaultIfEmpty()
                join kn in _db.mbKhoiNhas on ts.BlockID equals kn.MaKN into knr
                from kn in knr.DefaultIfEmpty()
                join nv in _db.tnNhanViens on ts.NguoiNhap equals nv.MaNV into nvr
                from nv in nvr.DefaultIfEmpty()
                where ts.MaTN == ((byte?)itemToaNha.EditValue ?? Common.User.MaTN)
                & ts.TenTaiSanID == (int?)itemTenTaiSan.EditValue
                select new
                {
                    ts.ID,
                    ncc.TenNhaCungCap,
                    ttts.TenTinhTrang,
                    tts.TenTaiSan,
                    kn.TenKN,
                    ts.TenChiTietTaiSan,
                    ts.MaChiTietTaiSan,
                    ts.MoTa,
                    ts.ThongSoKyThuat,
                    ts.ThoiGianBaoHanh,
                    ts.SoLuong,
                    ts.QuanTrong,
                    ts.NgungSuDung,
                    ts.NgayMua,
                    ts.NgayHetHanBaoHanh,
                    ts.NgayDuaVaoSuDung,
                    ts.ViTri,
                    ts.TonKho,
                    ts.NguyenGia,
                    ts.NgayNhap,
                    NguoiSua = ts.NguoiSua != null ? nv.HoTenNV : null,
                    NguoiNhap = ts.NguoiNhap != null ? nv.HoTenNV : null,
                    ts.NgaySua,
                };
            LoadDetail();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            repToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var _MaTN = (byte?)itemToaNha.EditValue;
                using (var frm = new frmTaiSanEdit { MaTn = _MaTN, IsSua = 0, Id = 0 })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }

            }
            catch (Exception)
            {
            }
        }

        private void grvHangSanXuat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //gv.AddNewRow();
            //gv.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
            //gv.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
            //gv.SetFocusedRowCellValue("MaTN", (byte?) itemToaNha.EditValue ?? Common.User.MaTN);
            //gv.SetFocusedRowCellValue("TenTaiSanID", (int?) itemTenTaiSan.EditValue);
        }

        private void grvHangSanXuat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var delobj = _db.tbl_ChiTietTaiSans.FirstOrDefault(p => p.ID == (int)gv.GetFocusedRowCellValue("ID"));
                    if (delobj != null)
                        _db.tbl_ChiTietTaiSans.DeleteOnSubmit(delobj);
                    _db.SubmitChanges();
                    gv.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                long id = 0;
                if (gv.GetFocusedRowCellValue("ID") != null)
                {
                    id = (long)gv.GetFocusedRowCellValue("ID");
                }
                else
                {
                    DialogBox.Error("Vui lòng chọn dòng cần xóa!");
                    return;
                }
                var delobj = _db.tbl_ChiTietTaiSans.FirstOrDefault(p => p.ID == id);
                if (delobj != null)
                    _db.tbl_ChiTietTaiSans.DeleteOnSubmit(delobj);
                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvHangSanXuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("ID");
            if (id == null | (long?)id == 0)
                return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gv.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gv.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        bool IsDuplication(string fieldName, int index, string value)
        {
            var newValue = value;
            for (int i = 0; i < gv.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gv.GetRowCellValue(i, fieldName) != null)
                {
                    var oldValue = gv.GetRowCellValue(i, fieldName).ToString();
                    if (oldValue == newValue) return true;
                }
                else return false;
            }
            return false;
        }


        private void grvHangSanXuat_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var ncc = gv.GetFocusedRowCellValue("MaChiTietTaiSan");
            if (ncc == null)
            {
                e.ErrorText = "Vui lòng nhập tài sản";
                e.Valid = false;
                return;
            }

            if (IsDuplication("MaChiTietTaiSan", e.RowHandle, ncc.ToString()))
            {
                e.ErrorText = "Tài sản này đã có";
                e.Valid = false;
                return;
            }
        }

        private void grvHangSanXuat_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEditHeThong.DataSource =
                _db.tbl_NhomTaiSans.Where(_ => _.MaTN == ((byte?)itemToaNha.EditValue ?? Common.User.MaTN)).ToList();
            itemHeThong.EditValue = repositoryItemGridLookUpEditHeThong.GetKeyValue(0);
        }

        private void itemHeThong_EditValueChanged(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEditLoaiTaiSan.DataSource =
                _db.tbl_LoaiTaiSans.Where(_ => _.NhomTaiSanID == (int?)itemHeThong.EditValue).ToList();
            itemLoaiTaiSan.EditValue = repositoryItemGridLookUpEditLoaiTaiSan.GetKeyValue(0);
        }

        private void itemLoaiTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEditTenTaiSan.DataSource =
                _db.tbl_TenTaiSans.Where(_ => _.LoaiTaiSanID == (int?)itemLoaiTaiSan.EditValue).ToList();
            itemTenTaiSan.EditValue = repositoryItemGridLookUpEditTenTaiSan.GetKeyValue(0);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmListTaiSan_Import())
                {
                    frm.MaTn = (byte)itemToaNha.EditValue;
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

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                long id = 0;
                if (gv.GetFocusedRowCellValue("ID") != null)
                {
                    id = (long)gv.GetFocusedRowCellValue("ID");
                }
                else
                {
                    DialogBox.Error("Vui lòng chọn dòng cần sửa!");
                    return;
                }
                var _MaTN = (byte?)itemToaNha.EditValue;
                using (var frm = new frmTaiSanEdit { MaTn = _MaTN, IsSua = 1, Id = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }

            }
            catch (Exception)
            {
            }
        }

        private void gv_RowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            gv.FocusedRowHandle = 1;
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemXuatKhoSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (long)gv.GetFocusedRowCellValue("ID");
            }
            else
            {
                DialogBox.Error("Vui lòng chọn chi tiết tài sản!");
                return;
            }

            using (var frm = new VatTu.frmXuatKhoSuDung_Edit { MaTn = (byte)itemToaNha.EditValue, IsSua = 0, Id = 0, ChiTietTaiSanID = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadDetail();
            }
        }

        private void itemSuaXuatKhoSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void btnQR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{
            //    long id = 0;
            //    if (gv.GetFocusedRowCellValue("ID") != null)
            //    {
            //        id = (long)gv.GetFocusedRowCellValue("ID");
            //    }
            //    else
            //    {
            //        DialogBox.Error("Vui lòng chọn dòng!");
            //        return;
            //    }
            //    using (var frm = new frmQRcodeTaiSan { ID = id })
            //    {
            //        frm.ShowDialog();
            //        if (frm.DialogResult == DialogResult.OK)
            //            LoadData();
            //    }

            //}
            //catch (Exception)
            //{
            //}
            var indexs = gv.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn tài sản!");
                return;
              
            }
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = (from i in indexs
                               join ts in db.tbl_ChiTietTaiSans on (long?)gv.GetRowCellValue(i, "ID") equals ts.ID
                               select

                                   ts.ID
                               ).ToList();
                    if (obj != null)
                    {
                        rptQRTaiSan rpt = new rptQRTaiSan(obj);
                        rpt.ShowPreviewDialog();

                    }
                }

            }
            catch (Exception)
            {

            }
        }
    }
}