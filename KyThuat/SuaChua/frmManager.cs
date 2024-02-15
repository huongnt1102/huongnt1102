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

namespace KyThuat.SuaChua
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                gcSuaChua.DataSource = null;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcSuaChua.DataSource = db.sckhSuaChuas
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgaySC.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgaySC.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgaySC).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaSC,
                            p.NgaySC,
                            p.MaSoSC,
                            p.DienGiai,
                            p.PhiSC,
                            p.DaTT,
                            TenKH = p.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            p.mbMatBang.MaSoMB,
                            p.tnNhanVien.HoTenNV,
                            p.MaDMCV
                        }).ToList();
                }
                else
                {
                    gcSuaChua.DataSource = db.sckhSuaChuas
                        .Where(p => p.MaTN == objnhanvien.MaTN &
                                SqlMethods.DateDiffDay(tuNgay, p.NgaySC.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgaySC.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgaySC).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaSC,
                            p.NgaySC,
                            p.MaSoSC,
                            p.DienGiai,
                            p.PhiSC,
                            p.DaTT,
                            TenKH = p.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            p.mbMatBang.MaSoMB,
                            p.tnNhanVien.HoTenNV,
                            p.MaDMCV
                        }).ToList();
                }
            }
            else
            {
                gcSuaChua.DataSource = null;
            }
        }

        void LoadDataByPage()
        {
            if (grvSuaChua.FocusedRowHandle < 0)
            {
                gcThietBi.DataSource = null;
                gcNhanSu.DataSource = null;
                return;
            }

            var MaDM = (long?)grvSuaChua.GetFocusedRowCellValue("MaDMCV");

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcThietBi.DataSource = db.btDauMucCongViec_ThietBis.Where(p => p.MaCVBT == MaDM)
                        .Select(p => new { p.tsLoaiTaiSan.TenLTS, p.SoLuong, p.DonGia, p.ThanhTien, p.DienGiai });
                    break;
                case 1:
                    gcNhanSu.DataSource = db.btDauMucCongViec_NhanViens.Where(p => p.MaCVBT == MaDM)
                        .Select(p => new { p.tnNhanVien.MaSoNV, p.tnNhanVien.HoTenNV, p.DienGiai });
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

            var MaSCs = "|";
            int[] indexs = grvSuaChua.GetSelectedRows();

            foreach (int i in indexs)
                MaSCs += grvSuaChua.GetRowCellValue(i, "MaSC") + "|";

            db.sckhThietBis.DeleteAllOnSubmit(db.sckhThietBis.Where(p => SqlMethods.Like(MaSCs, "%" + p.MaSC + "%")));
            db.sckhNhanSus.DeleteAllOnSubmit(db.sckhNhanSus.Where(p => SqlMethods.Like(MaSCs, "%" + p.MaSC + "%")));
            db.sckhSuaChuas.DeleteAllOnSubmit(db.sckhSuaChuas.Where(p => SqlMethods.Like(MaSCs, "%" + p.MaSC + "%")));

            db.SubmitChanges();

            grvSuaChua.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvSuaChua.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }
            int MaSC = (int)grvSuaChua.GetFocusedRowCellValue("MaSC");
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objSC = db.sckhSuaChuas.Single(p => p.MaSC == MaSC) })
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

        private void barbtnGiayDeNghiThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvSuaChua.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }
            int MaSC = (int)grvSuaChua.GetFocusedRowCellValue("MaSC");
            var itemSC = db.sckhSuaChuas.Single(p => p.MaSC == MaSC);
            if (itemSC.DaTT.Value == itemSC.PhiSC.Value)
            {
                DialogBox.Error("Mục này đã thanh toán rồi");
                return;
            }
            using (var frm = new ReportTemplate.frmPrintControl(itemSC,1)) 
            {
                frm.ShowDialog();
            }
        }

        private void btnPhieuNghiemThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvSuaChua.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần in phiếu nghiệm thu");
                return;
            }
            var itemSC = db.sckhSuaChuas.Single(p => p.MaSC.ToString() == grvSuaChua.GetFocusedRowCellValue("MaSC").ToString());
            using (var frm = new ReportTemplate.frmPrintControl(itemSC, 2))
            {
                frm.ShowDialog();
            }
        }

        private void grvSuaChua_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void gcSuaChua_Click(object sender, EventArgs e)
        {

        }

    }
}