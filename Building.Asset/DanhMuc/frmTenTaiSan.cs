using System;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Windows.Forms;

namespace Building.Asset.DanhMuc
{
    public partial class frmTenTaiSan : XtraForm
    {
        private MasterDataContext _db;

        public frmTenTaiSan()
        {
            InitializeComponent();
        }

        private void frmTenTaiSan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            _db = new MasterDataContext();

            repositoryItemLookUpEditToaNha.DataSource = Common.TowerList;
            barEditItemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            var obj=(from ts in _db.tbl_TenTaiSans
              join ncc in _db.tbl_NhaCungCapTaiSans on ts.NhaCungCapID equals ncc.ID into nccr
              from ncc in nccr.DefaultIfEmpty()
              join ttts in _db.tbl_TinhTrangTaiSans on ts.TinhTrangTaiSanID equals ttts.ID into tttsr
              from ttts in tttsr.DefaultIfEmpty()
              join kn in _db.mbKhoiNhas on ts.BlockID equals kn.MaKN into knr
              from kn in knr.DefaultIfEmpty()
              join nv in _db.tnNhanViens on ts.NguoiNhap equals nv.MaNV into nvr
              from nv in nvr.DefaultIfEmpty()
              where ts.MaTN == ((byte?)barEditItemToaNha.EditValue ?? Common.User.MaTN)
              && ts.LoaiTaiSanID ==((int?) barEditItemLoaiTaiSan.EditValue ?? 0)
              select new
              {
                  ts.ID,
                  ncc.TenNhaCungCap,
                  ttts.TenTinhTrang,
                  kn.TenKN,
                  ts.TenTaiSan,
                  ts.TenVietTat,
                  ts.DienGiai,
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
              }).ToList();
            gridControl1.DataSource = obj;
        }

        private void barEditItemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEditNhomTaiSan.DataSource = _db.tbl_NhomTaiSans.Where(_ => _.MaTN == ((byte?) barEditItemToaNha.EditValue ?? Common.User.MaTN)&_.IsSuDung==true).ToList();
            barEditItemNhomTaiSan.EditValue = repositoryItemGridLookUpEditNhomTaiSan.GetKeyValue(0);
        }

        private void barEditItemNhomTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEditLoaiTaiSan.DataSource = _db.tbl_LoaiTaiSans.Where(_ => _.NhomTaiSanID == (int?) barEditItemNhomTaiSan.EditValue).ToList();
            barEditItemLoaiTaiSan.EditValue = repositoryItemGridLookUpEditLoaiTaiSan.GetKeyValue(0);
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }


        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.tbl_TenTaiSans.FirstOrDefault(_ => _.ID == (int)gridView1.GetFocusedRowCellValue("ID"));
                if (obj != null)
                {
                    _db.tbl_TenTaiSans.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gridView1.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, Tên tài sản đã được sử dụng");
            }
        }


        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmTenTaiSan_Import())
                {
                    frm.MaTn = (byte)barEditItemToaNha.EditValue;
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("ID") != null)
            {
                gc.DataSource = (from ts in _db.tbl_ChiTietTaiSans
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
              where ts.MaTN == ((byte?)barEditItemToaNha.EditValue ?? Common.User.MaTN)
              & ts.TenTaiSanID ==Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"))
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
              }).ToList();
            }
            else
            {
                gc.DataSource = null;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaTN = (byte?)barEditItemToaNha.EditValue;
            frmTenTaiSanEdit frm = new frmTenTaiSanEdit();
            frm.MaTn = _MaTN;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("ID") != null)
            {
                var _MaTN = (byte?)barEditItemToaNha.EditValue;
                frmTenTaiSanEdit frm = new frmTenTaiSanEdit();
                frm.Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                frm.MaTn = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn tên tài sản muốn sửa");
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}