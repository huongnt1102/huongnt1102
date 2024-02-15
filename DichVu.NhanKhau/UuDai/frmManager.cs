using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;

namespace DichVu.NhanKhau.UuDai
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();

                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var maTN = (byte?)itemToaNha.EditValue??0;
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcUuDai.DataSource = db.tnnkDangKyUuDais
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTao.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayTao.Value, denNgay) >= 0 & p.tnNhanKhau.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN)
                        .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.tnNhanKhau.HoTenNK,
                            GioiTinh = (bool)p.tnNhanKhau.GioiTinh ? "Nam" : "Nữ",
                            p.tnNhanKhau.NgaySinh,
                            p.tnNhanKhau.CMND,
                            p.tnNhanKhau.DienThoai,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.tnNhanKhau.mbMatBang.MaSoMB,
                            TenKH = p.tnNhanKhau.tnKhachHang == null ? "" : ((bool)p.tnNhanKhau.tnKhachHang.IsCaNhan ? p.tnNhanKhau.tnKhachHang.HoKH + " " + p.tnNhanKhau.tnKhachHang.TenKH : p.tnNhanKhau.tnKhachHang.CtyTen),
                            p.TuNgay,
                            p.DenNgay,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1!=null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN,
                            IsDuyet = p.IsDuyet.GetValueOrDefault()
                        }).ToList();
                }
                else
                {
                    var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                    if (GetNhomOfNV.Count > 0)
                    {
                        var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                        gcUuDai.DataSource = db.tnnkDangKyUuDais
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTao.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayTao.Value, denNgay) >= 0 &
                                p.tnNhanKhau.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN &
                                GetListNV.Contains(p.MaNV.Value))
                        .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.tnNhanKhau.HoTenNK,
                            GioiTinh = (bool)p.tnNhanKhau.GioiTinh ? "Nam" : "Nữ",
                            p.tnNhanKhau.NgaySinh,
                            p.tnNhanKhau.CMND,
                            p.tnNhanKhau.DienThoai,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.tnNhanKhau.mbMatBang.MaSoMB,
                            TenKH = p.tnNhanKhau.tnKhachHang == null ? "" : ((bool)p.tnNhanKhau.tnKhachHang.IsCaNhan ? p.tnNhanKhau.tnKhachHang.HoKH + " " + p.tnNhanKhau.tnKhachHang.TenKH : p.tnNhanKhau.tnKhachHang.CtyTen),
                            p.TuNgay,
                            p.DenNgay,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN,
                            IsDuyet = p.IsDuyet.GetValueOrDefault()
                        }).ToList();
                    }
                    else
                    {
                        gcUuDai.DataSource = db.tnnkDangKyUuDais
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTao.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayTao.Value, denNgay) >= 0 &
                                p.tnNhanKhau.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN &
                                p.MaNV == objnhanvien.MaNV)
                        .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.tnNhanKhau.HoTenNK,
                            GioiTinh = (bool)p.tnNhanKhau.GioiTinh ? "Nam" : "Nữ",
                            p.tnNhanKhau.NgaySinh,
                            p.tnNhanKhau.CMND,
                            p.tnNhanKhau.DienThoai,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.tnNhanKhau.mbMatBang.MaSoMB,
                            TenKH = p.tnNhanKhau.tnKhachHang == null ? "" : ((bool)p.tnNhanKhau.tnKhachHang.IsCaNhan ? p.tnNhanKhau.tnKhachHang.HoKH + " " + p.tnNhanKhau.tnKhachHang.TenKH : p.tnNhanKhau.tnKhachHang.CtyTen),
                            p.TuNgay,
                            p.DenNgay,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN,
                            IsDuyet = p.IsDuyet.GetValueOrDefault()
                        }).ToList();
                    }
                }

                wait.Close();
                wait.Dispose();
            }
            else
            {
                gcUuDai.DataSource = null;
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
            db = new MasterDataContext();
            lookToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN }).ToList();
            try {
                if (!objnhanvien.IsSuperAdmin.Value)
                {
                    itemToaNha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    itemToaNha.EditValue = objnhanvien.MaTN;
                }
            }
            catch { }
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
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

        void DeleteSelected()
        {
            int[] indexs = grvUuDai.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Đăng ký], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                List<tnnkDangKyUuDai> lst = new List<tnnkDangKyUuDai>();
                foreach (int i in indexs)
                {
                    tnnkDangKyUuDai objNK = db.tnnkDangKyUuDais.Single(p => p.ID == (int)grvUuDai.GetRowCellValue(i, "ID"));
                    lst.Add(objNK);
                }
                db.tnnkDangKyUuDais.DeleteAllOnSubmit(lst);
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                LoadData();
            }
            catch
            {
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSelected();
        }

        private void grvNhanKhau_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Delete)
                DeleteSelected();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvUuDai.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Đăng ký], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objDK = db.tnnkDangKyUuDais.Single(p => p.ID == (int)grvUuDai.GetFocusedRowCellValue("ID")) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvUuDai.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Đăng ký], xin cảm ơn.");
                return;
            }

            var selectedNK = new List<int>();
            foreach (int row in grvUuDai.GetSelectedRows())
            {
                selectedNK.Add((int)grvUuDai.GetRowCellValue(row, colID));
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();

                var ts = db.tnnkDangKyUuDais
                    .Where(p => selectedNK.Contains(p.ID))
                   .Select(p => new
                   {
                       p.tnNhanKhau.HoTenNK,
                       GioiTinh = (bool)p.tnNhanKhau.GioiTinh ? "Nam" : "Nữ",
                       p.tnNhanKhau.NgaySinh,
                       p.tnNhanKhau.CMND,
                       p.tnNhanKhau.DienThoai,
                       p.tnNhanKhau.mbMatBang.MaSoMB,
                       TenKH = p.tnNhanKhau.tnKhachHang == null ? "" : ((bool)p.tnNhanKhau.tnKhachHang.IsCaNhan ? p.tnNhanKhau.tnKhachHang.HoKH + " " + p.tnNhanKhau.tnKhachHang.TenKH : p.tnNhanKhau.tnKhachHang.CtyTen),
                       p.TuNgay,
                       p.DenNgay,
                       p.DienGiai,
                       p.tnNhanVien.HoTenNV,
                       p.NgayTao,
                       HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                       p.NgayCN
                   }).ToList();
                dt = SqlCommon.LINQToDataTable(ts);
                ExportToExcel.exportDataToExcel("Danh sách đăng ký ưu đãi", dt);
            }
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}