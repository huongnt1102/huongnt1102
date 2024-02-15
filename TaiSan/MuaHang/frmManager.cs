using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace TaiSan.MuaHang
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
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcMuaHang.DataSource = db.msMuaHangs
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayMH.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayMH.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayMH).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaMH,
                                p.NgayMH,
                                p.MaSoMH,
                                TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                                p.DienGiai,
                                p.DaTT,
                                p.tnNhanVien.HoTenNV,
                                p.DaNhapKho,
                                p.TongTien,
                                SoTienDaTT = (p.msLichSuTTs != null ? p.msLichSuTTs.Sum(q => q.SoTien) : 0),
                                ConLai = p.TongTien - (p.msLichSuTTs != null ? p.msLichSuTTs.Sum(q => q.SoTien) : 0)
                            }).ToList();
                    }
                    else
                    {
                        gcMuaHang.DataSource = db.msMuaHangs
                            .Where(p => p.MaTN == objnhanvien.MaTN &
                                    SqlMethods.DateDiffDay(tuNgay, p.NgayMH.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayMH.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayMH).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaMH,
                                p.NgayMH,
                                p.MaSoMH,
                                TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                                p.DienGiai,
                                p.DaTT,
                                p.tnNhanVien.HoTenNV,
                                SoTienDaTT = (p.msLichSuTTs != null ? p.msLichSuTTs.Sum(q => q.SoTien) : 0),
                                ConLai = p.TongTien - (p.msLichSuTTs != null ? p.msLichSuTTs.Sum(q => q.SoTien) : 0)
                            }).ToList();
                    }
                }
                else
                {
                    gcMuaHang.DataSource = null;
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
            if (grvMuaHang.FocusedRowHandle >= 0)
            {
                var MaMH = (int)grvMuaHang.GetFocusedRowCellValue("MaMH");
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:

                        gcTaiSan.DataSource = db.msTaiSans.Where(p => p.MaMH == MaMH).AsEnumerable()

                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.tsLoaiTaiSan.TenLTS,
                                p.SoLuong,
                                p.DonGia,
                                p.DienGiai,
                                TenNCC = p.MaNCC != null ? p.tnNhaCungCap.TenNCC : "",
                                p.ThanhTien
                            }).ToList();
                        break;
                    case 1:
                        ctlTaiLieu1.FormID = 101;
                        ctlTaiLieu1.LinkID = MaMH;
                        ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                        ctlTaiLieu1.objNV = objnhanvien;
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                    case 2:
                        gcLichThanhToan.DataSource = db.msLichSuTTs.Where(p => p.MaMH == MaMH)
                            .Select(p => new { 
                                p.tnNhanVien.HoTenNV,
                                p.ID,
                                p.NgayTT,
                                p.SoTien,
                                p.GhiChu,
                                p.NguoiTT
                            });
                        break;
                }

            }
            else
            {
                gcTaiSan.DataSource = null;
                gcLichThanhToan.DataSource = null;
                //ctlTaiLieu1.
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

            var MaMHs = "|";
            int[] indexs = grvMuaHang.GetSelectedRows();

            foreach (int i in indexs)
                MaMHs += grvMuaHang.GetRowCellValue(i, "MaMH") + "|";

            db.msTaiSans.DeleteAllOnSubmit(db.msTaiSans.Where(p => SqlMethods.Like(MaMHs, "%" + p.MaMH + "%")));
            db.msMuaHangs.DeleteAllOnSubmit(db.msMuaHangs.Where(p => SqlMethods.Like(MaMHs, "%" + p.MaMH + "%")));
            db.SubmitChanges();

            grvMuaHang.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaHang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }

            int MaMH = (int)grvMuaHang.GetFocusedRowCellValue("MaMH");
            msMuaHang objMH = db.msMuaHangs.Single(p => p.MaMH == MaMH);

            if (objMH.DaTT.Value)
            {
                DialogBox.Error("Đơn này đã thanh toán nên không thể chỉnh sửa được nữa");
                return;
            }
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objMH = objMH})
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

        private void grvMuaHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }
        
        private void grvMuaHang_DoubleClick(object sender, EventArgs e)
        {
            if (itemSua.Enabled == false) return;
            itemSua_ItemClick(null, null);
        }

        private void itemThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemThemTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMuaHang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn đơn hàng để thanh toán!");
                return;
            }
            var MaMH = (int?)grvMuaHang.GetFocusedRowCellValue("MaMH");
            frmThanhToan frm = new frmThanhToan();
            frm.objnhanvien = objnhanvien;
            frm.MaMH = MaMH;
            frm.ShowDialog();
            LoadDetail();
        }

        private void itemXoaTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvLichThanhToan.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thanh toán để xóa!");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            var maLTT = (int?)gvLichThanhToan.GetFocusedRowCellValue("ID");
            try
            {
                db.msLichSuTTs.DeleteOnSubmit(db.msLichSuTTs.SingleOrDefault(p => p.ID == maLTT));
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã xóa!");
                LoadDetail();
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể xóa. VUi lòng kiểm tra lại!");
            }
        }

        private void itemSuaTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvLichThanhToan.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thanh toán để chỉnh sửa!");
                return;
            }
            var maLTT = (int?)gvLichThanhToan.GetFocusedRowCellValue("ID");
            frmThanhToan frm = new frmThanhToan();
            frm.objnhanvien = objnhanvien;
            frm.objLTT = db.msLichSuTTs.SingleOrDefault(p => p.ID == maLTT);
            frm.ShowDialog();
            LoadDetail();
        }
    }
}