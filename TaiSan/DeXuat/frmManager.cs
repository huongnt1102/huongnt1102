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

namespace TaiSan.DeXuat
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcDeXuat.DataSource = db.dxmsDeXuats
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDX.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayDX.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayDX)
                        .Select(p => new
                        {
                            p.MaDX,
                            p.NgayDX,
                            p.MaSoDX,
                            p.dxmsTrangThai.TenTT,
                            p.dxmsTrangThai.MauNen,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            MaNvNhan = p.MaNvNhan.HasValue ? p.tnNhanVien1.HoTenNV : ""
                        });
                    }
                    else
                    {
                        gcDeXuat.DataSource = db.dxmsDeXuats
                            .Where(p => p.MaTN == objnhanvien.MaTN &
                                (p.MaNvNhan == objnhanvien.MaNV | p.MaNV == objnhanvien.MaNV) &
                                SqlMethods.DateDiffDay(tuNgay, p.NgayDX.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayDX.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDX)
                            .Select(p => new
                            {
                                p.MaDX,
                                p.NgayDX,
                                p.MaSoDX,
                                p.dxmsTrangThai.TenTT,
                                p.dxmsTrangThai.MauNen,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                MaNvNhan = p.MaNvNhan.HasValue ? p.tnNhanVien1.HoTenNV : ""
                            });
                    }
                }
                else
                {
                    gcDeXuat.DataSource = null;
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
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

        void LoadDetail()
        {
            if (grvDeXuat.FocusedRowHandle >= 0)
            {
                var MaDX = (int)grvDeXuat.GetFocusedRowCellValue("MaDX");

                gcTaiSan.DataSource = db.dxmsTaiSans.Where(p => p.MaDX == MaDX).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.tsLoaiTaiSan.TenLTS,
                        p.SoLuong,
                        p.DienGiai,
                        p.DacTinh
                    }).ToList();
            }
            else
            {
                gcTaiSan.DataSource = null;
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var MaDXs = "|";
                int[] indexs = grvDeXuat.GetSelectedRows();

                foreach (int i in indexs)
                    MaDXs += grvDeXuat.GetRowCellValue(i, "MaDX") + "|";

                db.dxmsTaiSans.DeleteAllOnSubmit(db.dxmsTaiSans.Where(p => SqlMethods.Like(MaDXs, "%" + p.MaDX + "%")));
                db.dxmsDeXuats.DeleteAllOnSubmit(db.dxmsDeXuats.Where(p => SqlMethods.Like(MaDXs, "%" + p.MaDX + "%")));
                db.SubmitChanges();

                grvDeXuat.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được do ràng buộc dữ liệu");
                return;
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDeXuat.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng đề xuất");
                return;
            }
            int madx = (int)grvDeXuat.GetFocusedRowCellValue("MaDX");
            var objDX = db.dxmsDeXuats.Single(p => p.MaDX == madx);

            if (objDX.MaTT != 1)
            {
                DialogBox.Error("Chỉ sửa được đề xuất ở trạng thái <Chờ duyệt>");
                return;
            }
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objDX = objDX })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void grvDeXuat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void grvDeXuat_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb((int)grvDeXuat.GetRowCellValue(e.RowHandle, "MauNen"));
            }
            catch { }
        }

        private void gcDeXuat_DockChanged(object sender, EventArgs e)
        {
            if (itemSua.Enabled == false) return;
            if (grvDeXuat.FocusedRowHandle < 0)
                return;
            itemSua_ItemClick(null, null);
        }

        private void grvDeXuat_DoubleClick(object sender, EventArgs e)
        {
            itemSua_ItemClick(null, null);
        }

        private void btnInPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDeXuat.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng đề xuất");
                return;
            }
            Report.frmPrintControl frm = new Report.frmPrintControl(grvDeXuat.GetFocusedRowCellValue("MaDX").ToString());
            frm.ShowDialog();
        }

        private void btnKiDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDeXuat.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng đề xuất");
                return;
            }
            
            int MaDX = (int)grvDeXuat.GetFocusedRowCellValue("MaDX");
            var objdx = db.dxmsDeXuats.Single(p => p.MaDX == MaDX);

            if (objdx.MaTT != 1)
            {
                DialogBox.Error("Đơn đề xuất này không cần duyệt nữa");
                return;
            }
            try
            {
                objdx.MaTT = 2;
                db.SubmitChanges();
                DialogBox.Alert("Đã duyệt");
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không duyệt được");
            }
        }
    }
}