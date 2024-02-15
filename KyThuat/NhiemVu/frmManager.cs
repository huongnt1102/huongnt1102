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

namespace KyThuat.NhiemVu
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            itemSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcCongViec.DataSource = db.btDauMucCongViec_NhanViens
                    .Where(q => SqlMethods.DateDiffDay(tuNgay, q.ThoiGianTH.Value) >= 0 &
                            SqlMethods.DateDiffDay(q.ThoiGianTH.Value, denNgay) >= 0)
                    .OrderByDescending(t => t.ThoiGianTH)
                    .Select(p => new
                    {
                        p.ID,
                        p.MaCVBT,
                        p.MaNVTH,
                        p.btDauMucCongViec.MaSoCV,
                        TaiSan = p.btDauMucCongViec.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.TenTS,
                        HeThong = p.btDauMucCongViec.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.tsHeThong.TenHT,   
                        p.tnNhanVien.HoTenNV,
                        HoTenNV1 = p.tnNhanVien1.HoTenNV,
                        p.NoiDungCV,
                        p.TienDo,
                        p.TrangThai,
                        p.ThoiGianTH,
                        GioBD=p.ThoiGianTH,
                        p.ThoiGianHetHan,
                        p.ThoiGianHT,
                        p.NgayGiaoViec,
                        p.DienGiai,
                        HoanThanh=p.HoanThanh.GetValueOrDefault(),
                        p.ViTri,
                        TenTT = p.TrangThai == null ? "" : p.btDauMucCongViec_TrangThaiCVNV.TenTT,
                        p.TieuDe

                    }).ToList();
                }
                else
                {
                    gcCongViec.DataSource = db.btDauMucCongViec_NhanViens
                    .Where(q => SqlMethods.DateDiffDay(tuNgay, q.ThoiGianTH.Value) >= 0 &
                            SqlMethods.DateDiffDay(q.ThoiGianTH.Value, denNgay) >= 0 & q.MaNVTH == objnhanvien.MaNV)
                    .OrderByDescending(t => t.ThoiGianTH)
                    .Select(p => new
                    {
                        p.ID,
                        p.MaCVBT,
                        p.btDauMucCongViec.MaSoCV,
                        p.tnNhanVien.HoTenNV,
                        TaiSan = p.btDauMucCongViec.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.TenTS,
                        HeThong = p.btDauMucCongViec.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.tsHeThong.TenHT,    
                        HoTenNV1 = p.tnNhanVien1.HoTenNV,
                        p.NoiDungCV,
                        p.TienDo,
                        p.TrangThai,
                        p.ThoiGianTH,
                        GioBD = p.ThoiGianTH,
                        p.ThoiGianHetHan,
                        p.ThoiGianHT,
                        p.MaNVTH,
                        p.NgayGiaoViec,
                        p.DienGiai,
                        HoanThanh = p.HoanThanh.GetValueOrDefault(),
                        p.ViTri,
                        TenTT = p.TrangThai == null ? "" : p.btDauMucCongViec_TrangThaiCVNV.TenTT,
                        p.TieuDe
                    }).ToList();
                }
            }
            else
            {
                gcCongViec.DataSource = null;
            }
        }

        void LoadDetail()
        {
           
            if (grvCongViec.FocusedRowHandle < 0)
            {
                gcLichSu.DataSource = null;
                gcThietBi.DataSource = null;
                return;
            }
            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    var maCV = (long?)grvCongViec.GetFocusedRowCellValue("ID");
                    gcLichSu.DataSource = db.btDauMucCongViec_NhanVienLSTHs.Where(p => p.MaCVNV == maCV).OrderByDescending(p => p.NgayTH)
                        .Select(q => new
                        {
                            q.NgayTH,
                            q.TienDo,
                            TenTT = q.TrangThai == null ? "" : q.btDauMucCongViec_TrangThaiCVNV.TenTT,
                            q.DienGiai
                        });
                    break;
                case 1:
                    var MaDMCV = (long?)grvCongViec.GetFocusedRowCellValue("MaCVBT");
                    gcThietBi.DataSource = db.btDauMucCongViec_ThietBis.Where(p => p.MaCVBT == MaDMCV)
                        .Select(p => new { 
                            p.tsLoaiTaiSan.TenLTS,
                            p.SoLuong,
                            p.DonGia,
                            p.ThanhTien,
                            p.DienGiai
                        });
                    break;
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
            lookTrangThai.DataSource = db.btDauMucCongViec_TrangThaiCVNVs;
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để xóa. Xin cảm ơn!");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            btDauMucCongViec_NhanVien obj = db.btDauMucCongViec_NhanViens.Where(p => p.ID == (long?)grvCongViec.GetFocusedRowCellValue("ID")).SingleOrDefault();
            db.btDauMucCongViec_NhanViens.DeleteOnSubmit(obj);
            db.SubmitChanges();

            grvCongViec.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }

        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void grvThamQuan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void btnCapNhatTD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để cập nhật tiến độ. Xin cảm ơn!");
                return;
            }
            frmUpdate frm = new frmUpdate();
            frm.ObjNV = objnhanvien;
            frm.MaDMCV = (long?)grvCongViec.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            LoadData();
            LoadDetail();
        }

        private void btnComplete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để xác nhận hoàn thành. Xin cảm ơn!");
                return;
            }
            frmComplete frm = new frmComplete();
            frm.objNV = objnhanvien;
            frm.MACV = (long?)grvCongViec.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            LoadData();
            LoadDetail();
        }

        private void itemDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để xem chi tiết. Xin cảm ơn!");
                return;
            }
            KyThuat.DauMucCongViec.frmEdit frm = new DauMucCongViec.frmEdit();
            frm.MaCVBT = (long?)grvCongViec.GetFocusedRowCellValue("MaCVBT");
            frm.objnhanvien = objnhanvien;
            frm.EnableStatus();
            frm.ShowDialog();
            LoadData();
            LoadDetail();
        }

        private void btnThietBi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để cập nhật thiết bị sử dụng!");
                return;
            }
            frmUpdateEquipmentcs frm = new frmUpdateEquipmentcs();
            frm.objNV = objnhanvien;
            frm.MaDMCV = (long?)grvCongViec.GetFocusedRowCellValue("MaCVBT");
            frm.MaNVTH = (int?)grvCongViec.GetFocusedRowCellValue("MaNVTH");
            frm.ShowDialog();
            LoadData();
            LoadDetail();
        }

        private void grvCongViec_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                if (e.Column == colTrangThai)
                {
                    switch ((byte?)grvCongViec.GetRowCellValue(e.RowHandle, "TrangThai"))
                    {
                        case 1:
                            e.Appearance.BackColor = Color.Red;
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.Gray;
                            break;
                        case 3:
                            e.Appearance.BackColor = Color.Green;
                            break;
                        case 4:
                            e.Appearance.BackColor = Color.Blue;
                            break;
                    }
                }
            }
            catch { }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemLichCongViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KyThuat.NhiemVu.frmLichLamViecNV frm = new frmLichLamViecNV();
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
        }

        private void btnUpdateStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để cập nhật thời gian bắt đầu!");
                return;
            }
            frmStartTime frm = new frmStartTime();
            frm.ObjNV = objnhanvien;
            frm.objCVNV = db.btDauMucCongViec_NhanViens.SingleOrDefault(p => p.ID == (long?)grvCongViec.GetFocusedRowCellValue("ID"));
            frm.ShowDialog();
            LoadData();
        }
    }
}