using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace KyThuat.DauMucCongViec
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
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                gcDauMucCongViec.DataSource = null;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcDauMucCongViec.DataSource = db.btDauMucCongViecs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.ThoiGianGhiNhan.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.ThoiGianGhiNhan.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.ThoiGianGhiNhan)
                        .Select(p => new
                        {
                            //STT = index + 1,
                            p.ID,
                            p.MaSoCV,
                            TaiSan = p.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.TenTS,
                            HeThong = p.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.tsHeThong.TenHT,
                            p.ThoiGianGhiNhan,
                            p.ThoiGianHT,
                            p.ThoiGianTH,
                            p.ThoiGianXacNhan,
                            p.TrangThaiCV,
                            p.tnToaNha.TenTN,
                            p.TienDoTH,
                            MaNCV = p.NguonCV,
                            NguonCV = (p.NguonCV == 0 ? "YCKH" : (p.NguonCV == 1 ? "HTVH" : "LBT-TSCD")),
                            p.MoTa,
                            p.btCongViecBT_trangThai.TenTT,
                            p.btHinhThuc.TenHT,
                            p.ThoiGianTheoLich,
                            GioTH = p.ThoiGianTH,
                            p.GioTHDuKien,
                            p.GioXacNhan,
                            p.SoYeuCau,
                            p.ThoiGianHetHan,
                            MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB
                        }).ToList();
                }
                else
                {
                    gcDauMucCongViec.DataSource = db.btDauMucCongViecs
                     .Where(p => p.MaTN == objnhanvien.MaTN & SqlMethods.DateDiffDay(tuNgay, p.ThoiGianGhiNhan.Value) >= 0 &
                             SqlMethods.DateDiffDay(p.ThoiGianGhiNhan.Value, denNgay) >= 0)
                     .OrderByDescending(p => p.ThoiGianGhiNhan)//.AsEnumerable()
                     .Select(p => new
                     {
                         //STT = index + 1,
                         p.ID,
                         p.MaSoCV,
                         p.ThoiGianGhiNhan,
                         p.ThoiGianHT,
                         TaiSan = p.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.TenTS,
                         HeThong = p.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.tsHeThong.TenHT,
                         p.ThoiGianTH,
                         p.ThoiGianXacNhan,
                         p.TrangThaiCV,
                         p.tnToaNha.TenTN,
                         p.TienDoTH,
                         MaNCV = p.NguonCV,
                         NguonCV = (p.NguonCV == 0 ? "YCKH" : (p.NguonCV == 1 ? "HTVH" : "LBT-TSCD")),
                         p.btCongViecBT_trangThai.TenTT,
                         p.MoTa,
                         p.btHinhThuc.TenHT,
                         p.ThoiGianTheoLich,
                         p.GioXacNhan,
                         p.GioTHDuKien,
                         GioTH=p.ThoiGianTH,
                         p.SoYeuCau,
                         p.ThoiGianHetHan,
                         MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB
                     }).ToList();
                }
            }
            else
            {
                gcDauMucCongViec.DataSource = null;
            }
        }

        void LoadDataByPage()
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                gcThietBi.DataSource = null;
                gcNhanSu.DataSource = null;
                gcTSCT.DataSource = null;
                gcDoiTac.DataSource = null;
                gcLichSu.DataSource = null;
                gcViTri.DataSource = null;
                return;
            }

            var MaBT = (long)gvDauMucCongViec.GetFocusedRowCellValue("ID");

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcLichSu.DataSource = db.btDauMucCongViec_LichSus.Where(p => p.MaCVBT == MaBT).OrderByDescending(t=>t.NgayCN)
                        .Select(q => new { 
                            q.tnNhanVien.HoTenNV,
                            q.NgayCN,
                            q.TienDo,
                            q.btCongViecBT_trangThai.TenTT,
                            q.DienGiai
                        });
                    break;
                case 1:
                    gcTSCT.DataSource = db.btDauMucCongViec_TaiSans.Where(p => p.MaCVBT == MaBT)
                        .Select(q => new
                        {
                            q.tsTaiSan.TenTS,
                            q.tsTaiSan.tnNhaCungCap.TenNCC,
                            q.tsTaiSan.NhaSanXuat,
                            q.tsTaiSan.NuocSX,
                            q.tsTaiSan.NamSX,
                            q.tsTaiSan.NgayBDSD,
                            q.DienGiai,
                            q.tsTaiSan.tsTinhTrang.TenTT,
                            HanSuDung = string.Format("{0:#,0} {1}", q.tsTaiSan.ThoiGianSD,(q.tsTaiSan.DVTTGSD == true ? " Năm" : " Tháng")),
                            TenHT = q.tsTaiSan.MaHT == null ? "" : q.tsTaiSan.tsHeThong.TenHT
                        });
                    break;
                case 2:

                    gcThietBi.DataSource = db.btDauMucCongViec_ThietBis.Where(p => p.MaCVBT == MaBT)
                        .Select(p => new
                        {
                            p.tsLoaiTaiSan.TenLTS,
                            p.SoLuong,
                            p.DonGia,
                            p.ThanhTien,
                            p.DienGiai
                        });
                    break;
                case 3:
                gcNhanSu.DataSource = db.btDauMucCongViec_NhanViens.Where(p => p.MaCVBT == MaBT)
                    .Select(p => new { 
                        p.tnNhanVien.MaSoNV,
                        p.tnNhanVien.HoTenNV,
                        p.NoiDungCV,
                        p.NgayGiaoViec,
                        p.ThoiGianHT,
                        p.ThoiGianHetHan,
                        p.ThoiGianTH,
                        p.TienDo,
                        p.HoanThanh
                    });
                    break;
                case 4:
                    gcDoiTac.DataSource = db.btDauMucCongViec_DoiTacs.Where(p => p.MaDMCV == MaBT).Select(q => new  {q.tnNhaCungCap.TenVT, q.tnNhaCungCap.TenNCC,q.DienGiai });
                    break;
                case 5:
                    gcViTri.DataSource = db.btDauMucCongViecs.Where(p => p.ID == MaBT)
                        .Select(p => new
                        {
                            TenTN = p.MaTN != null ? p.tnToaNha.TenTN : "",
                            TenKN = p.MaKN != null ? p.mbKhoiNha.TenKN : "",
                            TenTL = p.MaTL != null ? p.mbTangLau.TenTL : "",
                            MaSoMB = p.MaMB != null ? p.mbMatBang.MaSoMB : ""
                        });
                    break;
                case 6:
                    viewLichHen.objNV = objnhanvien;
                    viewLichHen.MaDMCV = MaBT;
                    viewLichHen.LoadData();
                    break;
            }
        }

        void LoadProcess()
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                subYCKhachHang.Enabled = false;
                subCVKhac.Enabled = false;
                subXuLyChung.Enabled = false;
            }
            else
            {
                subXuLyChung.Enabled = true;
                if ((byte)gvDauMucCongViec.GetFocusedRowCellValue("MaNCV") == 0)
                {
                    subCVKhac.Enabled = false;
                    subYCKhachHang.Enabled = true;
                }
                else
                {
                    subYCKhachHang.Enabled = false;
                    subCVKhac.Enabled = true;
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

        void EditData()
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }
            frmEdit frm = new frmEdit();
            frm.MaCVBT = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
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
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn đầu mục công việc để xóa. Xin cảm ơn!");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            var macv = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            db.btDauMucCongViecs.DeleteOnSubmit(db.btDauMucCongViecs.SingleOrDefault(p => p.ID == macv));
            db.SubmitChanges();
            gvDauMucCongViec.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
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

        private void grvBaoTri_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (SqlMethods.DateDiffDay(db.GetSystemDate(), (DateTime?)gvDauMucCongViec.GetRowCellValue(e.RowHandle, "ThoiGianHetHan")) <= 0 & (byte?)gvDauMucCongViec.GetRowCellValue(e.RowHandle, "TrangThaiCV") < 7)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;

                   // e.Appearance.Font.FontFamily= D
                }
            }
            catch { }
        }

        private void gvDauMucCongViec_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            try
            {
                if (e.RowHandle < 0) return;

                if (e.Column == cNguonCV)
                {
                    switch ((byte?)gvDauMucCongViec.GetRowCellValue(e.RowHandle, "MaNCV"))
                    {
                        case 0:
                            e.Appearance.BackColor = Color.Orange;
                            break;
                        case 1:
                            e.Appearance.BackColor = Color.Gray;
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.Green;
                            break;
                    }
                }
                if (e.Column == colThoiGianTH && SqlMethods.DateDiffDay(db.GetSystemDate(), (DateTime?)gvDauMucCongViec.GetRowCellValue(e.RowHandle, "ThoiGianTH")) < 3 & (byte?)gvDauMucCongViec.GetRowCellValue(e.RowHandle, "TrangThaiCV") < 6)
                {
                    e.Appearance.BackColor = Color.BlueViolet;
                }

            }
            catch { }
        }

        private void btnXinXacNhanLT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void btnCapNhatTienDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void btCapNhatCongViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
            LoadData();
            LoadDataByPage();
        }

        private void btnHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn đầu mục công việc để xác nhận hoàn thành. Xin cảm ơn!");
                return;
            }
            var objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
            objDMCV.TienDoTH = 100M;
            objDMCV.TrangThaiCV = 7;
            var objls = new btDauMucCongViec_LichSu();
            objls.MaNVCN = objnhanvien.MaNV;
            objls.NgayCN = DateTime.Now;
            objls.TienDo = 100M;
            objls.TrangThaiCV = 7;
            objDMCV.btDauMucCongViec_LichSus.Add(objls);
            tnycYeuCau objYC;
            if (objDMCV.NguonCV == 0)//nếu nguồn từ yêu cau KH thì cập nhật luôn tạng thái hoàn thành cho yêu cầu từ lễ tân
            {
                objYC = db.tnycYeuCaus.SingleOrDefault(p=>p.ID==objDMCV.MaNguonCV);
                objYC.NgayHoanThanh = DateTime.Now;
                objYC.MaTT = 5;
                var objLSYC = new tnycLichSuCapNhat();
                //objLSYC.TrangThaiYC = 5;
                objLSYC.NgayCN = DateTime.Now;
                objLSYC.MaNV = objnhanvien.MaNV;
                objYC.tnycLichSuCapNhats.Add(objLSYC);
            }
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Cập nhật dữ liệu thành công!");
                LoadData();
                LoadDataByPage();
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể lưu!");
            }
        }

        private void gvDauMucCongViec_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataByPage();
            LoadProcess();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void btnKSTXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để xác nhận. Xin cảm ơn!");
                return;
            }
            try
            {
                long a = (long)gvDauMucCongViec.GetFocusedRowCellValue("ID");
                var obj = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
                if (obj.TrangThaiCV != 4)
                {
                    DialogBox.Alert("Kỹ sư trưởng chỉ xác nhận với các đầu mục công việc đang ở trạng thái chờ duyệt.Xin cảm ơn!");
                    return;
                }
                obj.TrangThaiCV = 5;
                var objLS = new btDauMucCongViec_LichSu();
                obj.btDauMucCongViec_LichSus.Add(objLS);
                objLS.TrangThaiCV = 5;
                objLS.MaNVCN = objnhanvien.MaNV;
                objLS.NgayCN = DateTime.Now;
                objLS.TienDo = obj.TienDoTH;
                db.SubmitChanges();
                DialogBox.Alert("Duyệt thành công công đầu mục công việc. Xin cảm ơn!");
                LoadData();
                LoadDataByPage();
            }
            catch
            {
                DialogBox.Alert("Không thể duyệt đầu mục công việc này. Vui lòng kiểm tra lại!");
            }
        }

        private void btnXinXacNhanLT1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (gvDauMucCongViec.FocusedRowHandle < 0 || (byte)gvDauMucCongViec.GetFocusedRowCellValue("MaNCV") != 0)
                //{
                //    DialogBox.Alert("Vui lòng chọn đầu mục công việc từ yêu cầu khách hàng để xin xác nhận từ lễ tân. Xin cảm ơn!");
                //    return;
                //}
                var obj = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
                obj.TrangThaiCV = 2;
                var objls = new btDauMucCongViec_LichSu();
                obj.btDauMucCongViec_LichSus.Add(objls);
                objls.MaNVCN = objnhanvien.MaNV;
                objls.NgayCN = DateTime.Now;
                objls.TienDo = obj.TienDoTH;
                objls.TrangThaiCV = 2;
                var objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == obj.MaNguonCV);
                objYC.MaTT = 3;
                db.SubmitChanges();
                DialogBox.Alert("Đã hoàn thành xin xác nhận của lễ tân.");
                LoadData();
                LoadDataByPage();
            }
            catch
            {
                DialogBox.Alert("Không thể gửi yêu cầu xác nhận của cho lễ tân. Vui lòng kiểm tra lại!");
            }
        }

        private void btnSuaChuaKH1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long? maDMCV = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            var objSC = db.sckhSuaChuas.SingleOrDefault(p => p.MaDMCV == maDMCV);
            if (objSC != null)
            {
                DialogBox.Alert("Đầu mục công việc này đã tạo phiếu sữa chữa!");
                if (DialogBox.Question("Bạn muốn xem và chỉnh sửa phiếu sửa chữa này không") == DialogResult.No)
                    return;
                else
                {
                    var objSCkh = db.sckhSuaChuas.SingleOrDefault(p => p.MaDMCV == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
                    KyThuat.SuaChua.frmEdit frm1 = new SuaChua.frmEdit();
                    frm1.objnhanvien = objnhanvien;
                    frm1.objSC = objSCkh;
                    frm1.ShowDialog();
                    return;
                }
            }
            KyThuat.SuaChua.frmEdit frm = new SuaChua.frmEdit();
            frm.objnhanvien = objnhanvien;
            frm.MaDMCV = maDMCV;
            frm.ShowDialog();
        }

        private void btnTaoPhieu1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gvDauMucCongViec.FocusedRowHandle < 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn đầu mục công việc để tạo giấy xin xác nhận của khách hàng. Xin cảm ơn!");
            //    return;
            //}
            //if ((byte)gvDauMucCongViec.GetFocusedRowCellValue("MaNCV") == 0)
            //{
            //    DialogBox.Alert("Đây là công việc từ hệ thống yêu cầu của khách hàng bạn phải [Xin xác nhận của lễ tân]. Xin cảm ơn!");
            //    return;
            //}
            KyThuat.DauMucCongViec.rptXinXacNhanLT rpt = new rptXinXacNhanLT((long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
            rpt.ShowPreviewDialog();
            var obj = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
            obj.TrangThaiCV = 2;
            var objls = new btDauMucCongViec_LichSu();
            obj.btDauMucCongViec_LichSus.Add(objls);
            objls.MaNVCN = objnhanvien.MaNV;
            objls.NgayCN = DateTime.Now;
            objls.TienDo = obj.TienDoTH;
            objls.TrangThaiCV = 2;
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Đã in phiếu xin xác nhận của lễ tân.");
                LoadData();
                LoadDataByPage();
            }
            catch
            {

            }
        }

        private void btnCapNhatNXN1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            frmConfirmTime frm = new frmConfirmTime();
            frm.objNV = objnhanvien;
            frm.MaDMCV = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            LoadData();
            LoadDataByPage();
        }

        private void btnXinXNKST1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công việc để xin xác nhận từ kỹ sư trưởng. Xin cảm ơn!");
                return;
            }
            try
            {
                long a = (long)gvDauMucCongViec.GetFocusedRowCellValue("ID");
                var obj = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
                obj.TrangThaiCV = 4;
                var objLS = new btDauMucCongViec_LichSu();
                obj.btDauMucCongViec_LichSus.Add(objLS);
                objLS.TrangThaiCV = 4;
                objLS.MaNVCN = objnhanvien.MaNV;
                objLS.NgayCN = DateTime.Now;
                objLS.TienDo = obj.TienDoTH;
                db.SubmitChanges();
                DialogBox.Alert("Đã gửi yêu cầu phê duyệt thành công. Xin cảm ơn!");
                LoadData();
                LoadDataByPage();
            }
            catch
            {
                DialogBox.Alert("Yêu cầu phê duyệt không thể gửi. Vui lòng kiểm tra lại!");
            }
        }

        private void GiaoViec()
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn đầu mục công việc đê giao việc. Xin cảm ơn!");
                return;
            }
            frmGiaoViec frm = new frmGiaoViec();
            frm.objNV = objnhanvien;
            frm.MaDMCV = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
        }

        private void btnGiaoViec1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoViec();
        }

        private void btnTienDo1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn đầu mục công việc để ");
                return;
            }
            frmUpdate frm = new frmUpdate();
            frm.ObjNV = objnhanvien;
            frm.MaDMCV = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            LoadData();
            LoadDataByPage();
        }

        private void btnThongTin1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
            LoadData();
            LoadDataByPage();
        }

        private void btnXNHoanThanh1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDauMucCongViec.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn đầu mục công việc để xác nhận hoàn thành. Xin cảm ơn!");
                return;
            }
            //var objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID"));
            frmComplete frm = new frmComplete();
            frm.objNV = objnhanvien;
            frm.MaDMCV = (long?)gvDauMucCongViec.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            //objDMCV.TienDoTH = 100M;
            //objDMCV.TrangThaiCV = 7;
            //var objls = new btDauMucCongViec_LichSu();
            //objls.MaNVCN = objnhanvien.MaNV;
            //objls.NgayCN = DateTime.Now;
            //objls.TienDo = 100M;
            //objls.TrangThaiCV = 7;
            //objDMCV.btDauMucCongViec_LichSus.Add(objls);
            //tnycYeuCau objYC;
            //if (objDMCV.NguonCV == 0)//nếu nguồn từ yêu cau KH thì cập nhật luôn tạng thái hoàn thành cho yêu cầu từ lễ tân
            //{
            //    objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == objDMCV.MaNguonCV);
            //    objYC.NgayHoanThanh = DateTime.Now;
            //    objYC.MaTT = 5;
            //    var objLSYC = new tnycLichSuCapNhat();
            //    objLSYC.TrangThaiYC = 5;
            //    objLSYC.NgayCN = DateTime.Now;
            //    objLSYC.MaNV = objnhanvien.MaNV;
            //    objYC.tnycLichSuCapNhats.Add(objLSYC);
            //}
            //try
            //{
            //    db.SubmitChanges();
            //    DialogBox.Alert("Cập nhật dữ liệu thành công!");
            //    LoadData();
            //    LoadDataByPage();
            //}
            //catch
            //{
            //    DialogBox.Alert("Dữ liệu không thể lưu!");
            //}
        }

        private void itemLichCV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KyThuat.DauMucCongViec.frmLichCongViec frm = new KyThuat.DauMucCongViec.frmLichCongViec();
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
        }

        private void gvDauMucCongViec_DoubleClick(object sender, EventArgs e)
        {
            GiaoViec();
        }

        private void btnBaoCaoCV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrinCV frm = new frmPrinCV();
            frm.MauSo = 1;
            frm.ShowDialog();
            
        }

        private void btnBaoCaoCV2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrinCV frm = new frmPrinCV();
            frm.MauSo = 2;
            frm.ShowDialog();
        }
    }
}