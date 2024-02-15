using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;

namespace DichVu.HoBoi
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
            this.itemToaNha.EditValueChanged += new EventHandler(itemToaNha_EditValueChanged);
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            gcTheHoBoi.DataSource = null;
            gcTheHoBoi.DataSource = linqInstantFeedbackSource1;
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            this.itemToaNha.EditValueChanged -= new EventHandler(itemToaNha_EditValueChanged);

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            if (objnhanvien.IsSuperAdmin.Value)
                itemToaNha.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            else
                itemToaNha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            // Specify the key property of the PurchaseOrderHeader
            linqInstantFeedbackSource1.KeyExpression = "ID";
            // Handle the GetQueryable event, to create a DataContext and assign a queryable source
            linqInstantFeedbackSource1.GetQueryable += linqInstantFeedbackSource1_GetQueryable;
            // Handle the DismissQueryable event, to dispose of the DataContext
            // Assign the created data source to an XtraGrid
            gcTheHoBoi.DataSource = linqInstantFeedbackSource1;

            this.itemToaNha.EditValueChanged += new EventHandler(itemToaNha_EditValueChanged);

        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void DeleteSelected()
        {
            int[] indexs = grvTheHoBoi.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Thẻ hồ bơi], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                List<dvhbHoBoi> lst = new List<dvhbHoBoi>();
                foreach (int i in indexs)
                {
                    dvhbHoBoi objNK = db.dvhbHoBois.Single(p => p.ID == (int)grvTheHoBoi.GetRowCellValue(i, "ID"));
                    lst.Add(objNK);
                }
                db.dvhbHoBois.DeleteAllOnSubmit(lst);
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

        private void grvTheHoBoi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelected();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTheHoBoi.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Thẻ hồ bơi], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objHB = db.dvhbHoBois.Single(p => p.ID == (int)grvTheHoBoi.GetFocusedRowCellValue("ID")) })
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

        private void grvTheHoBoi_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                DateTime NgayHH = (DateTime)(grvTheHoBoi.GetRowCellValue(e.RowHandle, colNgayHH) ?? now);
                if (NgayHH < now)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }
            catch { }
        }

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTheHoBoi.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Thẻ hồ bơi], xin cảm ơn.");
                return;
            }

            var selectedNK = new List<int>();
            foreach (int row in grvTheHoBoi.GetSelectedRows())
            {
                selectedNK.Add((int)grvTheHoBoi.GetRowCellValue(row, colID));
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();

                var ts = db.dvhbHoBois
                    .Where(p => selectedNK.Contains(p.ID))
                   .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            p.ID,
                            p.SoThe,
                            p.NgayDangKy,
                            p.NgayHetHan,
                            p.dvhbLoaiThe.TenLT,
                            p.MucPhi,
                            p.mbMatBang.MaSoMB,
                            HoTenNK = p.tnNhanKhau != null ? p.tnNhanKhau.HoTenNK : "",
                            p.ChuThe,
                            p.tnNhanVien.HoTenNV,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN,
                            IsSuDung = p.IsSuDung.GetValueOrDefault(),
                            IsTinhDuThang = p.IsTinhDuThang.GetValueOrDefault(),
                            p.DienGiai
                        });
                dt = SqlCommon.LINQToDataTable(ts);
                ExportToExcel.exportDataToExcel("Danh sách Thẻ hồ bơi", dt);
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

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            db = new MasterDataContext();

            if (objnhanvien.IsSuperAdmin.Value)
            {
                var matn = (byte?)itemToaNha.EditValue ?? 0;
                e.QueryableSource = db.dvhbHoBois
                      .Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == matn)
                    .OrderByDescending(p => p.NgayTao)
                    .Select(p => new
                    {
                        p.ID,
                        p.SoThe,
                        p.NgayDangKy,
                        p.NgayHetHan,
                        p.dvhbLoaiThe.TenLT,
                        p.MucPhi,
                        p.mbMatBang.MaSoMB,
                        HoTenNK = p.tnNhanKhau != null ? p.tnNhanKhau.HoTenNK : "",
                        p.ChuThe,
                        p.tnNhanVien.HoTenNV,
                        p.NgayTao,
                        HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                        p.NgayCN,
                        IsSuDung = p.IsSuDung.GetValueOrDefault(),
                        p.DienGiai,
                        IsTinhDuThang = p.IsTinhDuThang.GetValueOrDefault()
                    }).AsQueryable();
            }
            else
            {
                var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                if (GetNhomOfNV.Count > 0)
                {
                    var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                    e.QueryableSource = db.dvhbHoBois
                    .Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN &
                            GetListNV.Contains(p.MaNV.Value))
                    .OrderByDescending(p => p.NgayTao)
                   .Select(p => new
                   {
                       p.ID,
                       p.SoThe,
                       p.NgayDangKy,
                       p.NgayHetHan,
                       p.dvhbLoaiThe.TenLT,
                       p.MucPhi,
                       p.mbMatBang.MaSoMB,
                       HoTenNK = p.tnNhanKhau != null ? p.tnNhanKhau.HoTenNK : "",
                       p.ChuThe,
                       p.tnNhanVien.HoTenNV,
                       p.NgayTao,
                       HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                       p.NgayCN,
                       IsSuDung = p.IsSuDung.GetValueOrDefault(),
                       p.DienGiai,
                       IsTinhDuThang = p.IsTinhDuThang.GetValueOrDefault()
                   }).AsQueryable();
                }
                else
                {
                    e.QueryableSource = db.dvhbHoBois
                      .Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN &
                              p.MaNV == objnhanvien.MaNV)
                      .OrderByDescending(p => p.NgayTao)
                     .Select(p => new
                     {
                         p.ID,
                         p.SoThe,
                         p.NgayDangKy,
                         p.NgayHetHan,
                         p.dvhbLoaiThe.TenLT,
                         p.MucPhi,
                         p.mbMatBang.MaSoMB,
                         HoTenNK = p.tnNhanKhau != null ? p.tnNhanKhau.HoTenNK : "",
                         p.ChuThe,
                         p.tnNhanVien.HoTenNV,
                         p.NgayTao,
                         HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                         p.NgayCN,
                         IsSuDung = p.IsSuDung.GetValueOrDefault(),
                         p.DienGiai,
                         IsTinhDuThang = p.IsTinhDuThang.GetValueOrDefault()
                     }).AsQueryable();
                }
            }

            wait.Close();
            wait.Dispose();
        }

        private void grvTheHoBoi_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                { e.Info.DisplayText = (e.RowHandle + 1).ToString(); }
            }
            catch { }
        }

        private void itemTaoCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmCreateFee();
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemExportMauNhapLieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTheHoBoi.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Thẻ hồ bơi], xin cảm ơn.");
                return;
            }

            var selectedNK = new List<int>();
            foreach (int row in grvTheHoBoi.GetSelectedRows())
            {
                selectedNK.Add((int)grvTheHoBoi.GetRowCellValue(row, colID));
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();

                var ts = db.dvhbHoBois
                    .Where(p => selectedNK.Contains(p.ID))
                   .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            p.SoThe,
                            p.NgayDangKy,
                            p.NgayHetHan,
                            p.dvhbLoaiThe.TenLT,
                            p.MucPhi,
                            p.mbMatBang.MaSoMB,
                            HoTenNK = p.tnNhanKhau != null ? p.tnNhanKhau.HoTenNK : "",
                            p.ChuThe,
                            IsSuDung = p.IsSuDung.GetValueOrDefault(),
                            p.DienGiai
                        });
                dt = SqlCommon.LINQToDataTable(ts);
                ExportToExcel.exportDataToExcel("Danh sách Thẻ hồ bơi", dt);
            }
        }
    }
}