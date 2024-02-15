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

namespace TaiSan.DanhGiaLai
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
                        gcDanhGiaLai.DataSource = db.tsDanhGiaLais
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDGL.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDGL.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDGL)
                            .Select(p => new
                            {
                                p.ID,
                                p.SoDGL,
                                p.NgayDGL,
                                p.SoQuyetDinh,
                                p.NgayKyQD,
                                p.tsDanhGiaLaiLyDo.TenLD,
                                p.tnNhanVien.HoTenNV,
                                HoTenNV2=p.tnNhanVien1.HoTenNV,
                                p.NgayTao,
                                p.NgayCN
                            }).ToList();
                    }
                    else
                    {
                        gcDanhGiaLai.DataSource = db.tsDanhGiaLais
                            .Where(p =>SqlMethods.DateDiffDay(tuNgay, p.NgayDGL.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDGL.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDGL)
                            .Select(p => new
                            {
                                p.ID,
                                p.SoDGL,
                                p.NgayDGL,
                                p.SoQuyetDinh,
                                p.NgayKyQD,
                                p.tsDanhGiaLaiLyDo.TenLD,
                                p.tnNhanVien.HoTenNV,
                                HoTenNV2 = p.tnNhanVien1.HoTenNV,
                                p.NgayTao,
                                p.NgayCN
                            }).ToList();
                    }
                }
                else
                {
                    gcDanhGiaLai.DataSource = null;
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

        private void EditData()
        {
            if (gvDanhGiaLai.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn đánh giá để chỉnh Sửa. Xin cảm ơn!");
                return;
            }
            frmEdit frm = new frmEdit();
            frm.MaDGL = (int)gvDanhGiaLai.GetFocusedRowCellValue("ID");
            frm.objNhanVien = objnhanvien;
            frm.ShowDialog();
        }

        private void DeleteData()
        {
            if (gvDanhGiaLai.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn đánh giá để xóa. Xin cảm ơn!");
                return;
            }
            int MaDG = (int)gvDanhGiaLai.GetFocusedRowCellValue("ID");
            var objDG = db.tsDanhGiaLais.Where(p => p.ID == MaDG).SingleOrDefault();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa đánh giá này!", "Xác nhận thông tin trước khi xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                db.tsDanhGiaLais.DeleteOnSubmit(objDG);

                db.SubmitChanges();
                LoadData();
            }
        }

        private void LoadDGCT(int MaDGL)
        {
            //int MaDGL = (int)gvDanhGiaLai.GetFocusedRowCellValue("ID");
            gcDanhGiaCT.DataSource = db.tsDanhGiaLaiChiTiets.Where(p => p.MaDGL == MaDGL)
                .Select(q => new
                {
                    q.MaDGL,
                    q.tsTaiSan.TenTS,
                    q.GiaTriKHConLai,
                    q.GiaTriKHSauDC,
                    q.GiaTriKHThangSauDC,
                    q.ChenhLech,
                    q.ThoiGianSD,
                    q.ThoiGianSDSauDC,
                    SoTKNo = q.TaiKhoan.TenTK,
                    SoTKCo = q.TaiKhoan2.TenTK
                });
        }

        private void LoadNhanVienTG(int MaDGL)
        {
            // int MaDGL = (int)gvDanhGiaLai.GetFocusedRowCellValue("ID");
            gcNhanVien.DataSource = db.tsDanhGiaLaiThanhViens.Where(p => p.MaDGL == MaDGL)
                .Select(p => new
                {
                    p.tnNhanVien.HoTenNV,
                    p.DaiDien,
                    p.ChucVu
                });

        }

        private void LoadChungTu(int MaDGL)
        {
            gcChungTu.DataSource = db.tsdglChungTuThamChieus.Where(p => p.MaDGL == MaDGL)
                .Select(q => new
                {
                    q.SoCT,
                    q.MaCT,
                    q.tsLoaiChungTu.TenLCT
                });
        }

        private void LoadDetail()
        {
            if (gvDanhGiaLai.FocusedRowHandle < 0) return;
            try
            {
                int MaDGL = (int)gvDanhGiaLai.GetFocusedRowCellValue("ID");
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        LoadDGCT(MaDGL);
                        break;
                    case 1:
                        LoadNhanVienTG(MaDGL);
                        break;
                    case 2:
                        LoadChungTu(MaDGL);
                        break; 
                }

            }
            catch { }
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
            DeleteData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() {objNhanVien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void gvDanhGiaLai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void btnInDanhSachDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDanhGiaLai);
        }

       }
}