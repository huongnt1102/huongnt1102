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

namespace TaiSan.DieuChuyen
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
            lookDVSDC.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.ID, p.TenDV });
            lookDVSDN.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.ID, p.TenDV });
            lookMatBangN.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB });
            lookMatBangC.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB });
               
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcDieuChuyen.DataSource = db.tsDieuChuyens
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDC.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDC.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDC)
                            .Select(p => new
                            {
                                p.ID,
                                p.SoDC,
                                p.NgayDC,
                                p.NguoiBanGiao,
                                p.NguoiDieuChuyen,
                                p.NguoiTiepNhan,
                                p.LyDo,
                                p.tnNhanVien.HoTenNV,
                                HoTenNV2=p.tnNhanVien1.HoTenNV,
                                p.NgayTao,
                                p.NgayCN
                            }).ToList();
                    }
                    else
                    {
                        gcDieuChuyen.DataSource = db.tsDieuChuyens
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDC.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDC.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayDC)
                            .Select(p => new
                            {
                                p.ID,
                                p.SoDC,
                                p.NgayDC,
                                p.NguoiBanGiao,
                                p.NguoiDieuChuyen,
                                p.NguoiTiepNhan,
                                p.LyDo,
                                p.tnNhanVien.HoTenNV,
                                HoTenNV2 = p.tnNhanVien1.HoTenNV,
                                p.NgayTao,
                                p.NgayCN
                            }).ToList();
                    }
                }
                else
                {
                    gcDieuChuyen.DataSource = null;
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
            if (gvDieuChuyen.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn mã điều chuyển để chỉnh Sửa. Xin cảm ơn!");
                return;
            }
            frmEdit frm = new frmEdit();
            frm.MaDC = (int)gvDieuChuyen.GetFocusedRowCellValue("ID");
            frm.objNhanVien = objnhanvien;
            frm.ShowDialog();
        }

        private void DeleteData()
        {
            if (gvDieuChuyen.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn điều chuyển để xóa. Xin cảm ơn!");
                return;
            }
            if (gvDieuChuyen.FocusedRowHandle > 0)
            {
                DialogBox.Warning("Bạn chỉ có thể xóa điều chuyền và khôi phục lại trạng thái tài sản của lần điều chuyển gần nhất. Xin cảm ơn!");
                gvDieuChuyen.FocusedRowHandle = 0;
                return;
            }
            int MaDG = (int)gvDieuChuyen.GetFocusedRowCellValue("ID");
            var objDG = db.tsDieuChuyens.Where(p => p.ID == MaDG).SingleOrDefault();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa điều chuyển này!", "Xác nhận thông tin trước khi xóa", MessageBoxButtons.YesNo);
            var objDCCT = objDG.tsDieuChinhChiTiets.ToList();
            List<tsTaiSan> ListTS = new List<tsTaiSan>();
            if (dialogResult == DialogResult.Yes)
            {
                foreach (var p in objDCCT)
                {
                    var objTS = db.tsTaiSans.SingleOrDefault(t => t.ID == p.MaTS);
                    ListTS.Add(objTS);
                }
                for(int i=0; i < ListTS.Count;i++) 
                {
                    ListTS[i].MaMB = objDCCT[i].MaMBC;
                    ListTS[i].IsNoiBo = objDCCT[i].IsNoiBoN;
                    ListTS[i].MaDVSD = objDCCT[i].MaDVSDN;
                }
                db.tsDieuChuyens.DeleteOnSubmit(objDG);
                db.SubmitChanges();
                LoadData();
            }
        }

        private void LoadDCCT(int MaDC)
        {
            var obj = db.tsDieuChinhChiTiets.Where(p => p.MaDC == MaDC)
                .Select(q => new
                {
                    q.MaDC,
                    q.tsTaiSan.TenTS,
                    TenDV = q.tsDonViSuDung.TenDV,
                    TenDV1 = q.tsDonViSuDung1.TenDV,
                    q.IsNoiBoC,
                    q.IsNoiBoN,
                    MaSoMB1 = q.mbMatBang.MaSoMB,
                    MaSoMB2 = q.mbMatBang1.MaSoMB
                });
            gcDieuChuyenCT.DataSource = obj;
        }

        private void LoadCT(int MaDC)
        {
            gcChungTu.DataSource = db.tsdcChungTuThamChieus.Where(p => p.MaDC == MaDC)
                 .Select(p => new
                 {
                     p.SoCT,
                     p.MaCT,
                     p.tsLoaiChungTu.TenLCT
                 });
        }

        private void LoadDetail()
        {
            if (gvDieuChuyen.FocusedRowHandle < 0) return;
            try
            {
                int MaDC = (int)gvDieuChuyen.GetFocusedRowCellValue("ID");
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        LoadDCCT(MaDC);
                        break;
                    case 1:
                        LoadCT(MaDC);
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

        private void btnInDSDieuChuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDieuChuyen);
        }

       }
}