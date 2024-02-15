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

namespace TaiSan.GhiTang
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
            gvTaiSan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvTaiSan_FocusedRowChanged);
            xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);
        }

        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        void gvTaiSan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
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
                        gcTaiSan.DataSource = db.tsTaiSans
                             .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayGT.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayGT.Value, denNgay) >= 0 & p.IsGhiGiam.GetValueOrDefault() != true)
                            //.OrderByDescending(p => p.NgayGT)//.AsEnumerable()
                             .Select(/*(p, index)*/p => new
                             {
                                 // STT=index+1,
                                 p.ID,
                                 p.MaTS,
                                 p.TenTS,
                                 p.NgayGT,
                                 p.tsDonViSuDung.TenDV,
                                 IsNoiBo = p.IsNoiBo.GetValueOrDefault(),
                                 p.mbMatBang.MaSoMB,
                                 p.NhaSanXuat,
                                 p.NamSX,
                                 p.SoHieu,
                                 p.NuocSX,
                                 p.ThoiHanBH,
                                 p.DieuKienBH,
                                 p.tnNhaCungCap.TenNCC,
                                 p.BBGiaoNhan,
                                 p.NgayLapBB,
                                 p.NgayBDSD,
                                 IsTinhKH = p.IsTinhKH,
                                 TenHT = p.MaHT == null ? "" : p.tsHeThong.TenHT

                             }).ToList();

                    }
                    else
                    {
                        gcTaiSan.DataSource = db.tsTaiSans
                           .Where(p => p.MaNV == objnhanvien.MaNV & SqlMethods.DateDiffDay(tuNgay, p.NgayGT.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayGT.Value, denNgay) >= 0 & p.IsGhiGiam.GetValueOrDefault() != true)
                            //.OrderByDescending(p => p.NgayGT)//.AsEnumerable()
                           .Select(/*(p, index)*/p => new
                           {
                               // STT=index+1,
                               p.ID,
                               p.MaTS,
                               p.TenTS,
                               p.NgayGT,
                               p.tsDonViSuDung.TenDV,
                               IsNoiBo = p.IsNoiBo.GetValueOrDefault(),
                               p.mbMatBang.MaSoMB,
                               p.NhaSanXuat,
                               p.NamSX,
                               p.SoHieu,
                               p.NuocSX,
                               p.ThoiHanBH,
                               p.DieuKienBH,
                               p.tnNhaCungCap.TenNCC,
                               p.BBGiaoNhan,
                               p.NgayLapBB,
                               p.NgayBDSD,
                               IsTinhKH = p.IsTinhKH,
                               TenHT = p.MaHT == null ? "" : p.tsHeThong.TenHT

                           }).ToList();
                    }
                }
                else
                {
                    //gcTaiSan.DataSource = null;
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void AddNew()
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void DeleteData()
        {
            int[] indexs = gvTaiSan.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            List<tsTaiSan> ListXoa = new List<tsTaiSan>();
            foreach (int i in indexs)
            {
                var objyc = db.tsTaiSans.Single(p => p.ID == (int)gvTaiSan.GetRowCellValue(i, "ID"));
                ListXoa.Add(objyc);
            }
            db.tsTaiSans.DeleteAllOnSubmit(ListXoa);
            db.SubmitChanges();

            gvTaiSan.DeleteSelectedRows();
        }

        void EditData()
        {
            if (gvTaiSan.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Bạn cần chọn tài sản để sữa. Xin cảm ơn!");
                return;
            }
            frmEdit frm = new frmEdit();
            frm.IDTS = (int)gvTaiSan.GetFocusedRowCellValue("ID");
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
        }

        void GhiGiam()
        {
            int[] list = gvTaiSan.GetSelectedRows();
            int[] ListItem = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                ListItem[i] = (int)gvTaiSan.GetRowCellValue(list[i], "ID");
            }
            TaiSan.GhiGiam.frmEdit frm = new GhiGiam.frmEdit();
            frm.objNhanVien = objnhanvien;
            frm.ListTS = ListItem;
            frm.ShowDialog();
        }

        void DieuChuyen()
        {
            int[] list = gvTaiSan.GetSelectedRows();
            int[] ListItem = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                ListItem[i] = (int)gvTaiSan.GetRowCellValue(list[i], "ID");
            }
            TaiSan.DieuChuyen.frmEdit frm = new DieuChuyen.frmEdit();
            frm.objNhanVien = objnhanvien;
            frm.ListTS = ListItem;
            frm.ShowDialog();
        }

        void KhauHao()
        {
            int[] list = gvTaiSan.GetSelectedRows();
            int[] ListItem = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                ListItem[i] = (int)gvTaiSan.GetRowCellValue(list[i], "ID");
            }
            TaiSan.KhauHao.frmEdit frm = new KhauHao.frmEdit();
            frm.objNhanVien = objnhanvien;
            frm.ListTS = ListItem;
            frm.ShowDialog();
        }

        void KiemKe()
        {
            int[] list = gvTaiSan.GetSelectedRows();
            int[] ListItem = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                ListItem[i] = (int)gvTaiSan.GetRowCellValue(list[i], "ID");
            }
            TaiSan.KiemKe.frmEdit frm = new KiemKe.frmEdit();
            frm.objNhanVien = objnhanvien;
            frm.ListTS = ListItem;
            frm.ShowDialog();
        }

        void DanhGiaLai()
        {

            int[] list = gvTaiSan.GetSelectedRows();
            int[] ListItem = new int[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                ListItem[i] = (int)gvTaiSan.GetRowCellValue(list[i], "ID");
            }
            TaiSan.DanhGiaLai.frmEdit frm = new DanhGiaLai.frmEdit();
            frm.objNhanVien = objnhanvien;
            frm.ListTS = ListItem;
            frm.ShowDialog();
        }

        void LoadThongTinKH(int MaTS)
        {
            gcKhauHao.DataSource = db.tsTaiSans.Where(p => p.ID == MaTS)
                .Select(p => new 
                {
                    p.TyLePhanBoBanHang,
                    p.TyLePhanBoCPQLDN,
                    p.TyLePhanBoCPSX,
                    p.TKKhauHao,
                    p.TKCPBH,
                    p.TKNguyenGia,
                    p.TKSPSX,
                    p.TLCPQLDN,
                    p.GiaTriConLai,
                    p.GiaTriKHNam,
                    p.GiaTriKHThang,
                    p.GiaTriTinhKH,
                    p.GiaTriTinhKHThueTNDN,
                    p.TyLeKHNam,
                    p.TyLeKHThang,
                    p.NguyenGia,
                    p.HaoMonLuyKe
                });
        }

        void LoadMoTa(int MaTS)
        {
            gcMoTa.DataSource = db.tsMoTaChiTiets.Where(p => p.MaTS == MaTS);
        }

        void LoadDungCu(int MaTS)
        {
            gcPhuTung.DataSource = db.tsDungCu_PhuTungs.Where(p => p.MaTS == MaTS);
        }

        void LoadDGL(int MaTS)
        {
            gcDanhGiaCT.DataSource = db.tsDanhGiaLaiChiTiets
                .Where(p => p.MaTS == MaTS)
                .OrderByDescending(p => p.tsDanhGiaLai.NgayDGL)
                .Select(p => new { 
                    p.tsDanhGiaLai.SoDGL,
                    p.tsDanhGiaLai.NgayDGL,
                    TenLD=p.tsDanhGiaLai.MaLyDo!=null?p.tsDanhGiaLai.tsDanhGiaLaiLyDo.TenLD:null,
                    p.ThoiGianSD,
                    p.ThoiGianSDSauDC,
                    p.GiaTriKHConLai,
                    p.GiaTriKHSauDC,
                    p.GiaTriKHThangSauDC,
                    SoTKNo = p.TKNo!=null?p.TaiKhoan.MaTK:"",
                    SoTKCo = p.TKCo!=null?p.TaiKhoan2.MaTK:""

                });
        }

        void LoadDieuChuyen(int MaTS)
        {
            gcDieuChuyenCT.DataSource = db.tsDieuChinhChiTiets
                .Where(p => p.MaTS == MaTS)
                .OrderByDescending(p => p.tsDieuChuyen.NgayDC)
                .Select(p => new
                {
                    p.tsDieuChuyen.SoDC,
                    p.tsDieuChuyen.NgayDC,
                    IsNoiBoC=p.IsNoiBoC.GetValueOrDefault(),
                    IsNoiBoN=p.IsNoiBoN.GetValueOrDefault(),
                    TenDVCu = p.MaDVSDC != null ? p.tsDonViSuDung.TenDV : "",
                    TenDVMoi = p.MaDVSDN != null ? p.tsDonViSuDung1.TenDV : "",
                    MBCu = p.MaMBC != null ? p.mbMatBang.MaSoMB : "",
                    MBMoi = p.MaMBN != null ? p.mbMatBang1.MaSoMB : ""
                }); 
            
        }

        void LoadLSHD(int MaTS)
        {
            gcLichSuHD.DataSource = db.btDauMucCongViec_TaiSans.Where(p => p.MaTS == MaTS && p.btDauMucCongViec.HoanThanh==true)
                            .Select(q => new
                            {
                                NguonCV = (q.btDauMucCongViec.NguonCV == 0 ? "Yêu cầu khách hàng" : (q.btDauMucCongViec.NguonCV == 1 ? "Hệ thống vận hành" : "Lịch bảo trì tài sản cố định")),
                                q.btDauMucCongViec.MaSoCV,
                                q.btDauMucCongViec.ThoiGianTH,
                                q.btDauMucCongViec.ThoiGianHT,
                                q.DienGiai
                            });
        }

        void LoadViTri(int MaTS)
        {
            gcViTri.DataSource = db.tsTaiSans.Where(p => p.ID == MaTS)
                           .Select(q => new
                           {
                               TenTN = q.MaMB == null ? "" : q.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                               TenKN = q.MaMB == null ? "" : q.mbMatBang.mbTangLau.mbKhoiNha.TenKN,
                               TenTL = q.MaMB == null ? "" : q.mbMatBang.mbTangLau.TenTL,
                               MaSoMB = q.MaMB == null ? "" : q.mbMatBang.MaSoMB,
                           });
        }

        void LoadMBLQ(int MaTS)
        {
            gcMatBangLQ.DataSource = db.tsTaiSanMatBangs.Where(p => p.MaTS == MaTS).Select(p => new {
                p.ID,
                p.mbMatBang.MaSoMB,
                p.mbMatBang.mbTangLau.TenTL,
                p.mbMatBang.mbTangLau.mbKhoiNha.TenKN,
                p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.TenTN
            });
        }

        void LoadDetail()
        {
            if (gvTaiSan.GetFocusedRowCellValue("ID") != null)
            {
                int MaTS = (int)gvTaiSan.GetFocusedRowCellValue("ID");
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        LoadThongTinKH(MaTS);
                        break;
                    case 1:
                        LoadMoTa(MaTS);
                        break;
                    case 2:
                        LoadDungCu(MaTS);
                        break;
                    case 3:
                        txtNguonGoc.Text = db.tsTaiSans.Single(p => p.ID == MaTS).MaNGHT == null ? "" : db.tsTaiSans.Single(p => p.ID == MaTS).tsNguonGocHinhThanh.TenNGHT;
                        break;
                    case 4:
                        LoadLSHD(MaTS);
                        break;
                    case 5:
                        LoadViTri(MaTS);
                        break;
                    case 6:
                        LoadDGL(MaTS);
                        break;
                    case 7:
                        LoadDieuChuyen(MaTS);
                        break;
                    case 8:
                        LoadMBLQ(MaTS);
                        break;
                }
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
            DeleteData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvTaiSan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng mục cần sửa");
                return;
            }
            TaiSan.GhiTang.frmEdit frm = new frmEdit();
            frm.objnhanvien = objnhanvien;
            frm.IDTS = (int)gvTaiSan.GetFocusedRowCellValue( "ID");
            frm.ShowDialog();

        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNew();
            LoadData();
            LoadDetail();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
            LoadData();
            LoadDetail();
        }

        private void itemDanhGiaLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DanhGiaLai();
            LoadData();
            LoadDetail();
        }

        private void itemDieuChuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DieuChuyen();
            LoadData();
            LoadDetail();
        }

        private void itemGhiGiam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GhiGiam();
            LoadData();
            LoadDetail();
        }

        private void gcTaiSan_Click(object sender, EventArgs e)
        {

        }

        private void btnGhiTang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNew();
            LoadData();
            LoadDetail();
        }

        private void btnDanhGiaLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DanhGiaLai();
            LoadData();
            LoadDetail();
        }

        private void btnDieuChuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DieuChuyen();
            LoadData();
            LoadDetail();
        }

        private void btnGhiGiam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GhiGiam();
            LoadData();
            LoadDetail();
        }

        private void btnKhauHao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhauHao();
            LoadData();
            LoadDetail();
        }

        private void btnKiemKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KiemKe();
            LoadData();
            LoadDetail();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
            LoadData();
            LoadDetail();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
            LoadData();
            LoadDetail();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
            LoadData();
            LoadDetail();
        }

        private void itemKhauHao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhauHao();
            LoadData();
            LoadDetail();
        }

        private void itemKiemKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KiemKe();
            LoadData();
            LoadDetail();
        }

        private void itemInAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcTaiSan);
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TaiSan.Import.frmImportGhiTang frm = new Import.frmImportGhiTang();
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
            LoadData();
            LoadDetail();
        }

        private void btnThemMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvTaiSan.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn tài sản để thêm mặt bằng liên quan.");
                return;
            }
            int? MaTS = (int?)gvTaiSan.GetFocusedRowCellValue("ID");
            frmTaiSanMB frm = new frmTaiSanMB();
            frm.MaTS = MaTS;
            frm.ShowDialog();
            LoadDetail();
        }

        private void btnXoaMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvMatBangLQ.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Bạn cần chọn mặt bằng để xóa!");
                gvMatBangLQ.Focus();
                return;
            }
            if (DialogBox.Question("Bạn có chắc muốn xóa mặt bằng này không?") == DialogResult.Yes)
            {
                var obj = db.tsTaiSanMatBangs.SingleOrDefault(p=>p.ID == (int?)gvMatBangLQ.GetFocusedRowCellValue("ID"));
                db.tsTaiSanMatBangs.DeleteOnSubmit(obj);
                try
                {
                    db.SubmitChanges();
                    DialogBox.Alert("Xóa thành công!");
                    LoadDetail();
                }
                catch
                {
                    DialogBox.Alert("Dữ liệu không thể xóa. Vui lòng kiểm tra kết nối mạng!");
                }
                
            }
        }


    }
}