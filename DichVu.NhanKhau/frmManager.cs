using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraReports.UI;

namespace DichVu.NhanKhau
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmManager()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                db = new MasterDataContext();
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    //var wait = DialogBox.WaitingForm();

                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    var maTN = (byte?)itemToaNha.EditValue ?? 0;

                    //gcNhanKhau.DataSource = (from p in db.tnNhanKhaus
 
                    //                         select p).ToList();

                    gcNhanKhau.DataSource =
                        (
                        from p in db.tnNhanKhaus
                        join qt in db.QuocTiches on p.MaQT equals qt.ID into dsqt
                        from qt in dsqt.DefaultIfEmpty()

                        where
                        p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN
                        select new
                        {
                            p.ID,
                            p.MaKH,
                            p.MaMB,
                            p.NgayDK,
                            p.HoTenNK,
                            GioiTinh = (bool)p.GioiTinh ? "Nam" : "Nữ",
                            p.NgaySinh,
                            p.CMND,
                            p.NgayCap,
                            p.NoiCap,
                            p.DCTT,
                            p.DienThoai,
                            p.DaDKTT,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.mbMatBang.MaSoMB,
                            MauNen = db.tnNhanKhauTrangThais.SingleOrDefault(k => k.MaTT == p.MaTT).MauNen,
                            TenTrangThai = db.tnNhanKhauTrangThais.SingleOrDefault(k => k.MaTT == p.MaTT).TenTrangThai,
                            TenKH = p.tnKhachHang == null ? "" : ((bool)p.tnKhachHang.IsCaNhan ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen),
                            QuocTich = qt.TenQuocTich,
                            p.NgayHetHanDKTT,
                            //NgayConDKTT = SqlMethods.DateDiffDay(now, p.NgayHetHanDKTT ?? now),
                            p.DanToc,
                            p.TonGiao,
                            p.SoThiThucVISA,
                            p.HanThiThucVISA,
                            p.Email,
                            p.Email2,
                            p.DienThoai2,
                            QuanHe = p.tnQuanHe.Name,
                            p.NoiLamViec,
                            p.NgheNghiep,
                            p.mbMatBang.mbLoaiMatBang.TenLMB,
                            p.NgayChuyenDen//,p.CardNumber
                        }).ToList();
                        //.OrderByDescending(p => p.NgayDK).AsEnumerable()
                        //.Select((p, index) => new
                        //{
                        //    STT = index + 1,
                        //    p.ID,
                        //    p.MaKH,
                        //    p.MaMB,
                        //    p.NgayDK,
                        //    p.HoTenNK,
                        //    p.GioiTinh,
                        //    p.NgaySinh,
                        //    p.CMND,
                        //    p.NgayCap,
                        //    p.NoiCap,
                        //    p.DCTT,
                        //    p.DienThoai,
                        //    p.DaDKTT,
                        //    p.DienGiai,
                        //    p.HoTenNV,
                        //    p.MaSoMB,
                        //    //p.MauNen,
                        //    //p.TenTrangThai,
                        //    //p.TenKH,
                        //    p.QuocTich,
                        //    p.NgayHetHanDKTT,
                        //    //p.NgayConDKTT,
                        //    p.DanToc,
                        //    p.TonGiao,
                        //    p.SoThiThucVISA,
                        //    p.HanThiThucVISA,
                        //    p.Email,
                        //    p.Email2,
                        //    p.DienThoai2,
                        //    p.QuanHe,
                        //    p.NoiLamViec,
                        //    p.NgheNghiep,
                        //    p.TenLMB,
                        //    p.NgayChuyenDen,
                        //    // p.CardNumber
                        //}).ToList();


                    //wait.Close();
                    //wait.Dispose();
                }
                else
                {
                    gcNhanKhau.DataSource = null;
                }
            }
            catch(System.Exception ex) { }
            
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
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            db = new MasterDataContext();
            now = db.GetSystemDate();

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
            int[] indexs = grvNhanKhau.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Nhân khẩu], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                List<tnNhanKhau> lst = new List<tnNhanKhau>();
                foreach (int i in indexs)
                {
                    tnNhanKhau objNK = db.tnNhanKhaus.Single(p => p.ID == (int)grvNhanKhau.GetRowCellValue(i, "ID"));
                    lst.Add(objNK);
                }
                db.tnNhanKhaus.DeleteAllOnSubmit(lst);
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
            if (e.KeyCode == Keys.Delete)
                DeleteSelected();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhanKhau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Nhân khẩu], xin cảm ơn.");
                return;
            }
            if ((byte?) itemToaNha.EditValue != 27)
            {
                using (frmEdit frm = new frmEdit())
                {
                    frm.ID = (int?)grvNhanKhau.GetFocusedRowCellValue("ID");
                    frm.MaKH = (int?)grvNhanKhau.GetFocusedRowCellValue("MaKH");
                    frm.MaMB = (int?)grvNhanKhau.GetFocusedRowCellValue("MaMB");
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
            else 
            {
                using (frmNhanKhaiThangLong frm = new frmNhanKhaiThangLong())
                {
                    frm.ID = (int?)grvNhanKhau.GetFocusedRowCellValue("ID");
                    frm.MaKH = (int?)grvNhanKhau.GetFocusedRowCellValue("MaKH");
                    frm.MaMB = (int?)grvNhanKhau.GetFocusedRowCellValue("MaMB");
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
            
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((byte?) itemToaNha.EditValue != 27)
            {
                using (frmEdit frm = new frmEdit())
                {
                    frm.MaTN = (byte?) itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
            else
            {
                using (frmNhanKhaiThangLong frm = new frmNhanKhaiThangLong())
                {
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            }
        }

        private void grvNhanKhau_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                DateTime NgayHH = (DateTime)(grvNhanKhau.GetRowCellValue(e.RowHandle, colNgayHHDKTT) ?? now);
                if (NgayHH < now)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }
            catch { }
        }

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhanKhau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Nhân khẩu], xin cảm ơn.");
                return;
            }
            Commoncls.ExportExcel(gcNhanKhau);
            //var selectedNK = new List<int>();
            //foreach (int row in grvNhanKhau.GetSelectedRows())
            //{
            //    selectedNK.Add((int)grvNhanKhau.GetRowCellValue(row, colID));
            //}
            //using (MasterDataContext db = new MasterDataContext())
            //{
            //    DataTable dt = new DataTable();

            //    var ts = db.tnNhanKhaus
            //        .Where(p => selectedNK.Contains(p.ID))
            //        .Select(p => new
            //        {
            //            //p.mbMatBang.MaSoMB,
            //            //p.HoTenNK,
            //            //p.DCTT,
            //            //p.GioiTinh,
            //            //p.NgaySinh,
            //            //p.CMND,
            //            //p.NgayCap,
            //            //p.NoiCap,
            //            //p.DienThoai,
            //            //p.Email,
            //            //p.DaDKTT,
            //            //p.NgayHetHanDKTT,
            //            //TrangThai = db.tnNhanKhauTrangThais.SingleOrDefault(k => k.MaTT == p.MaTT).TenTrangThai,
            //            //p.QuocTich,
            //            //NgayDangKyNhanKhau = p.NgayDK,
            //            //p.DienGiai

            //            p.mbMatBang.MaSoMB,
            //            p.HoTenNK,
            //            QuanHe = p.tnQuanHe.Name,
            //            p.QuocTich,
            //            GioiTinh = p.GioiTinh == true ? "Nam" : "Nữ",
            //            p.DienThoai,
            //            p.Email,
            //            p.NoiLamViec,
            //            p.NgayDK,
            //            p.NgayHetHanDKTT,
            //            p.NgayChuyenDen,
            //            TrangThai = db.tnNhanKhauTrangThais.SingleOrDefault(k => k.MaTT == p.MaTT).TenTrangThai,
            //        });
            //    dt = SqlCommon.LINQToDataTable(ts);
            //    ExportToExcel.exportDataToExcel("Danh sách nhân khẩu", dt);
            //}


            //grvNhanKhau.Columns["TenLMB"].Visible = false;
            //grvNhanKhau.Columns["NgaySinh"].Visible = false;
            //grvNhanKhau.Columns["DCTT"].Visible = false;
            //grvNhanKhau.Columns["CMND"].Visible = false;
            //grvNhanKhau.Columns["NgayCap"].Visible = false;
            //grvNhanKhau.Columns["NoiCap"].Visible = false;
            //grvNhanKhau.Columns["SoThiThucVISA"].Visible = false;
            //grvNhanKhau.Columns["HanThiThucVISA"].Visible = false;
            //grvNhanKhau.Columns["TonGiao"].Visible = false;
            //grvNhanKhau.Columns["NoiLamViec"].Visible = false;
            //grvNhanKhau.Columns["TenKH"].Visible = false;
            //grvNhanKhau.Columns["NgayChuyenDen"].Visible = false;
            //grvNhanKhau.Columns["NgayDi"].Visible = false;
            //grvNhanKhau.Columns["DanToc"].Visible = false;
            ////Commoncls.ExportExcel(gcNhanKhau);
            //grvNhanKhau.Columns["TenLMB"].Visible = true;
            //grvNhanKhau.Columns["NgaySinh"].Visible = true;
            //grvNhanKhau.Columns["DCTT"].Visible = true;
            //grvNhanKhau.Columns["CMND"].Visible = true;
            //grvNhanKhau.Columns["NgayCap"].Visible = true;
            //grvNhanKhau.Columns["NoiCap"].Visible = true;
            //grvNhanKhau.Columns["SoThiThucVISA"].Visible = true;
            //grvNhanKhau.Columns["HanThiThucVISA"].Visible = true;
            //grvNhanKhau.Columns["TenKH"].Visible = true;
            //grvNhanKhau.Columns["NgayChuyenDen"].Visible = true;
            //grvNhanKhau.Columns["NgayDi"].Visible = true;
            //grvNhanKhau.Columns["NoiLamViec"].Visible = true;
            //grvNhanKhau.Columns["TonGiao"].Visible = true;
            //grvNhanKhau.Columns["DanToc"].Visible = true;

            //grvNhanKhau.Columns["STT"].VisibleIndex = 0;
            //grvNhanKhau.Columns["MaSoMB"].VisibleIndex = 1;
            //grvNhanKhau.Columns["TenLMB"].VisibleIndex = 2;
            //grvNhanKhau.Columns["HoTenNK"].VisibleIndex = 3;
            //grvNhanKhau.Columns["QuanHe"].VisibleIndex = 4;
            //grvNhanKhau.Columns["QuocTich"].VisibleIndex = 5;
            //grvNhanKhau.Columns["GioiTinh"].VisibleIndex = 6;
            //grvNhanKhau.Columns["DienThoai"].VisibleIndex = 7;
            //grvNhanKhau.Columns["Email"].VisibleIndex = 8;
            //grvNhanKhau.Columns["NgheNghiep"].VisibleIndex = 9;
            //grvNhanKhau.Columns["NgaySinh"].VisibleIndex = 10;
            //grvNhanKhau.Columns["DCTT"].VisibleIndex = 11;
            //grvNhanKhau.Columns["CMND"].VisibleIndex = 12;
            //grvNhanKhau.Columns["NgayCap"].VisibleIndex = 13;
            //grvNhanKhau.Columns["NoiCap"].VisibleIndex = 14;
            //grvNhanKhau.Columns["SoThiThucVISA"].VisibleIndex = 15;
            //grvNhanKhau.Columns["HanThiThucVISA"].VisibleIndex = 16;
            //grvNhanKhau.Columns["NgayDK"].VisibleIndex = 17;
            //grvNhanKhau.Columns["NgayHetHanDKTT"].VisibleIndex = 18;
            //grvNhanKhau.Columns["TenTrangThai"].VisibleIndex = 19;
            //grvNhanKhau.Columns["NgayChuyenDen"].VisibleIndex = 20;
            //grvNhanKhau.Columns["NgayDi"].VisibleIndex = 21;
            //grvNhanKhau.Columns["NoiLamViec"].VisibleIndex = 22;
            //grvNhanKhau.Columns["TonGiao"].VisibleIndex = 23;
            //grvNhanKhau.Columns["TenKH"].VisibleIndex = 24;
            //grvNhanKhau.Columns["DanToc"].VisibleIndex = 25;
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte?)itemToaNha.EditValue ?? 0;
           
            using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien })
            {

                frm.MaTN = maTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvNhanKhau_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (grvNhanKhau.FocusedRowHandle < 0)
                {
                    ctlTaiLieu1.TaiLieu_Remove();
                    return;
                }

                var maNK = (int?)grvNhanKhau.GetFocusedRowCellValue("ID");
                ctlTaiLieu1.FormID = 39;
                ctlTaiLieu1.LinkID = maNK;
                ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                ctlTaiLieu1.objNV = objnhanvien;
                ctlTaiLieu1.TaiLieu_Load();
            }
            catch(System.Exception ex) { }
            
        }

        private void itemExportMauUuDai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhanKhau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Nhân khẩu], xin cảm ơn.");
                return;
            }

            var selectedNK = new List<int>();
            foreach (int row in grvNhanKhau.GetSelectedRows())
            {
                selectedNK.Add((int)grvNhanKhau.GetRowCellValue(row, colID));
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();
                var now = db.GetSystemDate();
                var nown = (DateTime?)now;
                nown = null;
                var ts = db.tnNhanKhaus
                    .Where(p => selectedNK.Contains(p.ID))
                    .Select(p => new
                    {
                        MaNK = p.ID,
                        p.HoTenNK,
                        p.mbMatBang.MaSoMB,
                        TuNgay = now,
                        DenNgay = nown,
                        DienGiai = ""
                    });
                dt = SqlCommon.LINQToDataTable(ts);
                ExportToExcel.exportDataToExcel("Danh sách nhân khẩu - Mẫu ưu đãi", dt);
            }
        }

        private void itemAddMuti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte?)itemToaNha.EditValue ?? 0;
            var frm = new frmAddMuti();
            frm.MaTN = maTN;
            frm.ShowDialog();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var maTN = (byte?)itemToaNha.EditValue ?? 0;
                var rpt = new DichVu.NhanKhau.Reports.rptNhanKhau(tuNgay, denNgay, maTN);
                rpt.ShowPreviewDialog();
            }
            catch { }
        }

        private void gcNhanKhau_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
            }
        }
    }
}