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

namespace DichVu.HopTac
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

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcHopDong.DataSource = db.hdhtHopDongs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayKy.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayKy.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayKT).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaHD,
                            p.TenHD,
                            p.SoHD,
                            p.NgayKy,
                            p.NgayBD,
                            p.NgayKT,
                            p.hdhtTrangThai.TenTT,
                            p.hdhtTrangThai.MauNen,
                            p.GiaTri,
                            TenTG = p.tnTyGia.TenVT,
                            TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.MaKH
                        }).ToList();
                }
                else
                {
                    gcHopDong.DataSource = db.hdhtHopDongs
                        .Where(p => p.MaTN == objnhanvien.MaTN &
                                SqlMethods.DateDiffDay(tuNgay, p.NgayKT.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayKT.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayKT).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaHD,
                            p.TenHD,
                            p.SoHD,
                            p.NgayKy,
                            p.NgayBD,
                            p.NgayKT,
                            p.hdhtTrangThai.TenTT,
                            p.hdhtTrangThai.MauNen,
                            p.GiaTri,
                            TenTG = p.tnTyGia.TenVT,
                            TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.MaKH
                        }).ToList();
                }
            }
            else
            {
                gcHopDong.DataSource = null;
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

        private void LoadLSCN()
        {
            gcLSTT.DataSource = db.hthtCongNos.Where(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")
                        & p.SoTien > 0)
                    .Select(p => new
                    {
                        p.MaCongNo,
                        p.SoHD,
                        p.SoTien,
                        p.ConLai,
                        p.DienGiai,
                        p.NgayThanhToan
                    })
                    .OrderByDescending(p => p.NgayThanhToan);
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

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");  
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objHD = db.hdhtHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void DeleteSelected()
        {
            int[] indexs = grvHopDong.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng cần xóa");  
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                db.hdhtHopDongs.DeleteOnSubmit((hdhtHopDong)grvHopDong.GetRow(i));
            }

            db.SubmitChanges();

            grvHopDong.DeleteSelectedRows();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSelected();
        }

        private void grvHopDong_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Delete)
                DeleteSelected();
        }

        private void grvHopDong_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb((int)grvHopDong.GetRowCellValue(e.RowHandle, "MauNen"));
            }
            catch { }
        }

        private void grvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            gcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == (int)grvHopDong.GetFocusedRowCellValue("MaKH"))
                        .Select(p => new
                        {
                            HoVaTen = (bool)p.IsCaNhan ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                            MaSoKH = (bool)p.IsCaNhan ? p.CMND : p.CtySoDKKD,
                            DiaChiKH = p.DCLL,
                            DienThoai = p.DienThoaiKH
                        });
            LoadLSCN();
        }
    }
}