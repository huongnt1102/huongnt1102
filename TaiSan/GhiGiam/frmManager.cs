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

namespace TaiSan.GhiGiam
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
                        gcGhiGiam.DataSource = db.tsGhiGiams
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayCT.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayCT.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayCT)
                            .Select(p => new
                            {
                                p.ID,
                                p.SoCT,
                                p.NgayCT,
                                p.tsGhiGiamLyDo.TenLD,
                                p.tnNhanVien.HoTenNV,
                                HoTenNV2 = p.tnNhanVien1.HoTenNV,
                                p.NgayTao,
                                p.NgayCN
                            }).ToList();
                    }
                    else
                    {
                        gcGhiGiam.DataSource = db.tsGhiGiams
                           .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayCT.Value) >= 0 &
                                   SqlMethods.DateDiffDay(p.NgayCT.Value, denNgay) >= 0)
                           .OrderByDescending(p => p.NgayCT)
                           .Select(p => new
                           {
                               p.ID,
                               p.SoCT,
                               p.NgayCT,
                               p.tsGhiGiamLyDo.TenLD,
                               p.tnNhanVien.HoTenNV,
                               HoTenNV2 = p.tnNhanVien1.HoTenNV,
                               p.NgayTao,
                               p.NgayCN
                           }).ToList();
                    }
                }
                else
                {
                    gcGhiGiam.DataSource = null;
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
            if (gvGhiGiam.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn tài sản để sữa ghi giảm. Xin cảm ơn!");
                return;
            }
            frmEdit frm = new frmEdit();
            frm.MaGG = (int)gvGhiGiam.GetFocusedRowCellValue("ID");
            frm.objNhanVien = objnhanvien;
            frm.ShowDialog();
        }

        private void DeleteData()
        {
            if (gvGhiGiam.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn ghi giảm để xóa. Xin cảm ơn!");
                return;
            }
            int MaDG = (int)gvGhiGiam.GetFocusedRowCellValue("ID");
            var objDG = db.tsGhiGiams.Where(p => p.ID == MaDG).SingleOrDefault();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa ghi giảm này!", "Xác nhận thông tin trước khi xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                db.tsGhiGiams.DeleteOnSubmit(objDG);

                db.SubmitChanges();
                LoadData();
            }
        }

        private void LoadGGCT(int MaGG)
        {
            var obj = db.tsGhiGiamChiTiets.Where(p => p.MaGG == MaGG)
                .Select(q => new
                {
                    q.MaGG,
                    q.tsTaiSan.TenTS,
                    q.TKXuKyGTCL,
                    q.TKHaoMon,
                    q.GiaTriConLai,
                    q.GiaTriKhauHao,
                    q.HaoMonLuyKe,
                    q.IsCPKoHopLy
                });
            gcGhiGiamCT.DataSource = obj;
        }

        private void LoadCT(int MaGG)
        {
            gcChungTu.DataSource = db.tsggChungTuThamChieus.Where(p => p.MaGG == MaGG)
                 .Select(p => new
                 {
                     p.SoCT,
                     p.MaCT,
                     p.tsLoaiChungTu.TenLCT
                 });
        }

        private void LoadDetail()
        {
            if (gvGhiGiam.FocusedRowHandle < 0) return;
            try
            {
                int MaGG = (int)gvGhiGiam.GetFocusedRowCellValue("ID");
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        LoadGGCT(MaGG);
                        break;
                    case 1:
                        LoadCT(MaGG);
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

        private void btnInDSGhiGiam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcGhiGiam);
        }

       }
}