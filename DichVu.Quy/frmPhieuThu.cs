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
using DevExpress.XtraReports.UI;
//using ReportMisc.DichVu;

namespace DichVu.Quy
{
    public partial class frmPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;

        public frmPhieuThu()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmPhieuThu_Load(object sender, EventArgs e)
        {
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            tpChuMatBangDN.PageVisible = false;

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            LoadData();

            first = false;
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    var option = itemOption.EditValue.ToString() == "Ngày thu" ? true : false;
                    var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                    if (option)
                    {
                        gcPhieuThu.DataSource = db.PhieuThus
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayThu.Value) >= 0
                                & SqlMethods.DateDiffDay(p.NgayThu.Value, denNgay) >= 0 &
                                p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN)
                            .Select(p => new
                            {
                                p.NgayThu,
                                p.MaPhieu,
                                p.DiaChi,
                                p.DichVu,
                                p.MaHopDong,
                                p.DotThanhToan,
                                p.SoTienThanhToan,
                                p.NguoiNop,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                p.SoPhieu,
                                p.KeToanDaDuyet,
                                p.mbMatBang.MaSoMB,
                                p.mbMatBang.MaKH,
                                p.mbMatBang.tnKhachHang.KyHieu,
                                p.mbMatBang.tnKhachHang.MaPhu,
                                p.MaMB,
                                LoaiDichVu = LoaiDichVu(p.DichVu.Value, p.MaHopDong),
                                p.SoThangThuPhiQuanLy,
                                p.SoTienChietKhauPhiQL,
                                p.NgayCN,
                                HoTenNVCN = p.tnNhanVien1 == null ? "" : p.tnNhanVien1.HoTenNV,
                                ChuyenKhoan = p.ChuyenKhoan.GetValueOrDefault(),
                                p.NgayNhap,
                                p.SoHDVAT
                            })
                            .OrderByDescending(p => p.NgayThu);
                    }
                    else
                    {
                        gcPhieuThu.DataSource = db.PhieuThus
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.DotThanhToan.Value) >= 0
                                & SqlMethods.DateDiffDay(p.DotThanhToan.Value, denNgay) >= 0 &
                                p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN)
                            .Select(p => new
                            {
                                p.NgayThu,
                                p.MaPhieu,
                                p.DiaChi,
                                p.DichVu,
                                p.MaHopDong,
                                p.DotThanhToan,
                                p.SoTienThanhToan,
                                p.NguoiNop,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                p.SoPhieu,
                                p.KeToanDaDuyet,
                                p.mbMatBang.MaSoMB,
                                p.mbMatBang.MaKH,
                                p.mbMatBang.tnKhachHang.KyHieu,
                                p.mbMatBang.tnKhachHang.MaPhu,
                                p.MaMB,
                                LoaiDichVu = LoaiDichVu(p.DichVu.Value, p.MaHopDong),
                                p.SoThangThuPhiQuanLy,
                                p.SoTienChietKhauPhiQL,
                                p.NgayCN,
                                HoTenNVCN = p.tnNhanVien1 == null ? "" : p.tnNhanVien1.HoTenNV,
                                ChuyenKhoan = p.ChuyenKhoan.GetValueOrDefault(),
                                p.SoHDVAT
                            })
                            .OrderByDescending(p => p.DotThanhToan);
                    }
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

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin bên dưới");
                return;
            }
            var phieu = db.PhieuThus.Single(p=>p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            frmPhieuThuDetail frm = new frmPhieuThuDetail() { objphieuthu = phieu };
            frm.ShowDialog();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin bên dưới");
                return;
            }
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                var phieu = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
                db.PhieuThus.DeleteOnSubmit(phieu);
                
                try
                {
                    db.SubmitChanges();
                    grvPhieuThu.DeleteSelectedRows();
                    DialogBox.Alert("Xóa thành công");
                    LoadData();
                }
                catch { }
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
                LoadData();
            }
            catch { }
        }

        private void grvPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            if (itemXem.Enabled == false) return;
            if (grvPhieuThu.FocusedRowHandle < 0) return;

            var phieu = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            frmPhieuThuDetail frm = new frmPhieuThuDetail() { objphieuthu = phieu };
            frm.ShowDialog();
        }

        private void btnKeToanDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin bên dưới");
                return;
            }
            var phieu = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            if (phieu.KeToanDaDuyet == true)
            {
                DialogBox.Alert("Phiếu này đã duyệt rồi!");
                return;
            }
            phieu.KeToanDaDuyet = true;
            db.SubmitChanges();
            DialogBox.Alert("Đã duyệt xong phiếu thu");
        }

        private void grvPhieuThu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        void Clicks()
        {
            var MaMB = (int?)grvPhieuThu.GetFocusedRowCellValue(colMaMB);

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    gcMatBang.DataSource = db.mbMatBangs
                        .Where(p => p.MaMB == MaMB)
                        .Select(p => new
                        {
                            MSMB = p.MaSoMB,
                            TenLoaiMB = p.mbLoaiMatBang.TenLMB,
                            ThuocToaNha = p.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                            //DonGia = p.DonGia,
                            DienTich = p.DienTich,
                            //ThanhTien = p.ThanhTien,
                            TrangThai = p.mbTrangThai.TenTT,
                            DienGiai = p.DienGiai
                        }).ToList();
                    break;
                case 1:
                case 2:
                    LoadKhachHang();
                    break;
                case 3:
                    LoadChiTiet();
                    break;
                case 4:
                    LoadLichSu();
                    break;
            }
        }

        void LoadKhachHang()
        {
            if (grvPhieuThu.GetFocusedRowCellValue("MaKH") == null)
            {
                tpChuMatBangCN.PageVisible = false;
                tpChuMatBangDN.PageVisible = false;
            }
            else
            {
                bool IsCaNhan = (bool)db.tnKhachHangs.Single(kh => kh.MaKH == (int)grvPhieuThu.GetFocusedRowCellValue("MaKH")).IsCaNhan;

                if (IsCaNhan)
                {
                    tpChuMatBangCN.PageVisible = true;
                    tpChuMatBangDN.PageVisible = false;
                    gckhachhang.DataSource = db.tnKhachHangs
                        .Where(p => p.MaKH == (int)grvPhieuThu.GetFocusedRowCellValue("MaKH"))
                        .Select(p => new
                        {
                            MaKhachHang = p.MaKH,
                            TenKhachHang = String.Format("{0} {1}", p.HoKH, p.TenKH),
                            NgaySinhKhachHang = p.NgaySinh,
                            GioiTinhKhachHang = (bool)p.GioiTinh ? "Nam" : "Nữ",
                            DiaChiLienLac = p.DCLL,
                            EmailKhachHang = p.EmailKH,
                            DienThoaiKhachHang = p.DienThoaiKH
                        }).ToList();
                }
                else
                {
                    tpChuMatBangCN.PageVisible = false;
                    tpChuMatBangDN.PageVisible = true;
                    gcDoanhNghiep.DataSource = db.tnKhachHangs
                        .Where(p => p.MaKH == (int)grvPhieuThu.GetFocusedRowCellValue("MaKH"))
                        .Select(p => new
                        {
                            TenCty = p.CtyTen,
                            TenCtyVT = p.CtyTenVT,
                            DiaChiCty = p.DCLL,
                            DienThoaiCty = p.DienThoaiKH,
                            FaxCty = p.CtyFax,
                            MaSoThueCty = p.CtyMaSoThue,
                            SoDKKDCty = p.CtySoDKKD,
                            NgayDKKDCty = p.CtyNgayDKKD,
                            NoiDKKDCty = p.CtyNoiDKKD,
                            NguoiDDCTY = p.CtyNguoiDD,
                            ChucVuNDD = p.CtyChucVuDD,
                            TenNKCty = p.CtyTenNH,
                            SoTKNHCty = p.CtySoTKNH
                        }).ToList();
                }
            }
        }

        void LoadChiTiet()
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            gcDichVu.DataSource = db.PaidMulti_edit((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
        }

        void LoadLichSu()
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            gcLichSu.DataSource = db.ptLichSus.Where(p => p.MaPT == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu")).AsEnumerable()
                .Select((p, index) => new { 
                    STT = index + 1,
                    p.DienGiai,
                    p.NgayTH,
                    NhanVien = p.tnNhanVien.HoTenNV
                });
        }

        private string LoaiDichVu(int LDV, string MaHopDong)
        {
            string LoaiDV;

            switch (LDV)
            {
                //case (int)EnumLoaiDichVu.DichVuDien:
                //    LoaiDV = "Điện";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuNuoc:
                //    LoaiDV = "Nước";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuKhac:
                //    //LoaiDV = db.dvkDichVus.Single(p => String.Compare(p.MaDV.ToString(), MaHopDong, false) == 0).dvkLoai.TenLDV;
                //    LoaiDV = "Nước";
                //    break;
                //case (int)EnumLoaiDichVu.HopDongThue:
                //    LoaiDV = "Hợp đồng thuê";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuGiuXe:
                //    LoaiDV = "Thẻ xe";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuThangMay:
                //    LoaiDV = "Thẻ thang máy";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuHoptac:
                //    LoaiDV = "Dịch vụ hợp tác";
                //    break;
                //case (int)EnumLoaiDichVu.DichVuGas:
                //    LoaiDV = "Gas";
                //    break;
                //case (int)EnumLoaiDichVu.PhiQuanLy:
                //    LoaiDV = "Phí quản lý";
                //    break;
                //case (int)EnumLoaiDichVu.PhiVeSinh:
                //    LoaiDV = "Phí vệ sinh";
                //    break;

                default:
                    LoaiDV = "Tổng hợp";
                    break;
            }

            return LoaiDV;
        }

        private void btnVietPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmVietPhieuThu frm = new frmVietPhieuThu() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void btnInPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //Barcode
            //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now);
            //using (var rpt = new ReportMisc.DichVu.Quy.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), barCodeString))
            //{
            //    if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
            //        SavePrint(barCodeString);
            //}
            //var rpt = new ReportMisc.DichVu.HoaDon.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            //rpt.PrintDialog();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            switch ((int)grvPhieuThu.GetFocusedRowCellValue("DichVu"))
            {
                case 1:
                    EditDien((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 2:
                    EditNuoc((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 5:
                    EditChoThue((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 6:
                    EditTheXe((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 11:
                    EditGas((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 12:
                    EditPQL((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 13:
                    EditPVS((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
                case 100:
                    EditTH((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), (int)grvPhieuThu.GetFocusedRowCellValue("MaMB"));
                    break;
            }
        }

        void EditGas(int maPhieu, int maMB)
        {
            //var f = new Gas.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            //f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    LoadData();
        }

        void EditNuoc(int maPhieu, int maMB)
        {
            //var f = new Nuoc.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            //f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    LoadData();
        }

        void EditDien(int maPhieu, int maMB)
        {
            //var f = new Dien.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            //f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    LoadData();
        }

        void EditTheXe(int maPhieu, int maMB)
        {
            //var f = new GiuXe.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            //f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    LoadData();
        }

        void EditChoThue(int maPhieu, int maMB)
        {
            var f = new ChoThue.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        void EditPQL(int maPhieu, int maMB)
        {
            var f = new DichVu.PhiQuanLy.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        void EditPVS(int maPhieu, int maMB)
        {
            //var f = new PhiVeSinh.frmPaid() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            //f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    LoadData();
        }

        void EditTH(int maPhieu, int maMB)
        {
            //var f = new HoaDon.frmPaidMultiV3() { objnhanvien = objnhanvien, MaMB = maMB, MaPhieu = maPhieu };
            //f.MaSoMB = grvPhieuThu.GetFocusedRowCellValue("MaSoMB").ToString();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    LoadData();
        }

        private void itemOption_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        List<ItemData> listData;
                
        private void itemCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                listData = new List<ItemData>();
                for (int i = 0; i < grvPhieuThu.RowCount; i++)
                {
                    var item = new ItemData();
                    item.KhachHang = grvPhieuThu.GetRowCellValue(i, "NguoiNop").ToString();
                    item.MaSoMB = grvPhieuThu.GetRowCellValue(i, "MaSoMB").ToString();
                    item.MaMB = Convert.ToInt32(grvPhieuThu.GetRowCellValue(i, "MaMB"));
                    item.SoDot = 1;
                    item.SoTien = Convert.ToDecimal(grvPhieuThu.GetRowCellValue(i, "SoTienThanhToan"));

                    AddItem(item);
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }

            var f = new frmCheckList();
            f.listData = listData;
            f.ShowDialog();
        }

        void AddItem(ItemData item)
        {
            if (listData.Where(p=>p.MaMB == item.MaMB).Count() > 0)
            {
                var obj = listData.Single(p => p.MaMB == item.MaMB);
                listData.Remove(obj);
                obj.SoDot += 1;
                obj.SoTien += item.SoTien;
                listData.Add(obj);
            }
            else
                listData.Add(item);
        }

        private void itemPrint2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //Barcode
            string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now);
            //using (ReportMisc.DichVu.Quy.rptManagermentFee rpt = new ReportMisc.DichVu.Quy.rptManagermentFee((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu")))
            //{
            //    if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
            //        SavePrint(barCodeString);
            //}
        }

        private void grvPhieuThu_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (!subConfirm.Enabled) return;

            if (e.Column.FieldName == "ChuyenKhoan")
            {
                popupMenu1.ShowPopup(Cursor.Position);
                //try
                //{
                //    var obj = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
                //    obj.ChuyenKhoan = Convert.ToBoolean(grvPhieuThu.GetFocusedRowCellValue("ChuyenKhoan"));
                //    db.SubmitChanges();

                //    grvPhieuThu.SetFocusedRowCellValue("ChuyenKhoan", obj.ChuyenKhoan);
                //}
                //catch { }
            }
        }

        private void itemConfirmTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvPhieuThu.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            foreach (int i in rows)
            {
                try
                {
                    var obj = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetRowCellValue(i, "MaPhieu"));
                    obj.ChuyenKhoan = true;
                    db.SubmitChanges();

                    db.ptLichSu_add(obj.MaPhieu, objnhanvien.MaNV, "[Duyệt] chuyển khoản");

                    grvPhieuThu.SetRowCellValue(i, colChuyenKhoan, true);
                    grvPhieuThu.RefreshData();
                }
                catch { }
            }
        }

        private void itemTienMat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvPhieuThu.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            foreach (int i in rows)
            {
                try
                {
                    var obj = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetRowCellValue(i, "MaPhieu"));
                    obj.ChuyenKhoan = false;
                    db.SubmitChanges();

                    db.ptLichSu_add(obj.MaPhieu, objnhanvien.MaNV, "[Không duyệt] chuyển khoản");

                    grvPhieuThu.SetRowCellValue(i, colChuyenKhoan, false);
                    grvPhieuThu.RefreshData();
                }
                catch { }
            }
        }

        private void itemChuyenKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvPhieuThu.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            foreach (int i in rows)
            {
                try
                {
                    var obj = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetRowCellValue(i, "MaPhieu"));
                    obj.ChuyenKhoan = true;
                    db.SubmitChanges();

                    db.ptLichSu_add(obj.MaPhieu, objnhanvien.MaNV, "[Duyệt] chuyển khoản");

                    grvPhieuThu.SetRowCellValue(i, colChuyenKhoan, true);
                    grvPhieuThu.RefreshData();
                }
                catch { }
            }
        }

        private void popupMenu1_BeforePopup(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(grvPhieuThu.GetFocusedRowCellValue("ChuyenKhoan")))
                {
                    itemTienMat.Enabled = true;
                    itemChuyenKhoan.Enabled = false;
                }
                else
                {
                    itemTienMat.Enabled = false;
                    itemChuyenKhoan.Enabled = true;
                }

            }
            catch { }
        }

        private void itemNotConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvPhieuThu.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            foreach (int i in rows)
            {
                try
                {
                    var obj = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetRowCellValue(i, "MaPhieu"));
                    obj.ChuyenKhoan = false;
                    db.SubmitChanges();

                    db.ptLichSu_add(obj.MaPhieu, objnhanvien.MaNV, "[Không duyệt] chuyển khoản");

                    grvPhieuThu.SetRowCellValue(i, colChuyenKhoan, false);
                    grvPhieuThu.RefreshData();
                }
                catch { }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new PhieuChi.frmEdit();
            f.objNV = objnhanvien;
            if (grvPhieuThu.FocusedRowHandle >= 0)
            {
                f.MaMB = (int?)grvPhieuThu.GetFocusedRowCellValue("MaMB");
                f.MaTN = itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue);
                f.DiaChi = grvPhieuThu.GetFocusedRowCellValue("DiaChi").ToString();
                f.NguoiNhan = grvPhieuThu.GetFocusedRowCellValue("NguoiNop").ToString();
            }
            f.ShowDialog();
        }

        private void itemPrintTransfer_DownChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemPrint1Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //Barcode
            string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now);
            //using (var rpt = new ReportMisc.DichVu.Quy.Sacomreal.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue), objnhanvien.HoTenNV))
            //{
            //    if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
            //        SavePrint(barCodeString);
            //}
        }

        private void itemPrintTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            if ((bool)grvPhieuThu.GetFocusedRowCellValue("ChuyenKhoan"))
            {
                //Barcode
                string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now);
                //using (var rpt = new ReportMisc.DichVu.Quy.Sacomreal.rptPhieuThuTienChuyenKhoan((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue), objnhanvien.HoTenNV))
                //{
                //    if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                //        SavePrint(barCodeString);
                //}
            }
            else
                DialogBox.Alert("Vui lòng chọn [Phiếu thu] chuyển khoản, xin cảm ơn.");
        }

        private void itemPrintMauTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //Barcode
            string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now);
            //using (var rpt = new ReportMisc.DichVu.Quy.rptGiayNhan((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), objnhanvien, barCodeString))
            //{
            //    if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
            //        SavePrint(barCodeString);
            //}
        }

        void SavePrint(string barCode)
        {
            var obj = new ptLichSu();
            obj.DienGiai = "In phiếu thu. Mã vạch [" + barCode + "]";
            obj.MaNV = objnhanvien.MaNV;
            obj.MaPT = (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu");
            obj.NgayTH = db.GetSystemDate();
            db.ptLichSus.InsertOnSubmit(obj);

            var objPT = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            objPT.Barcode = barCode;
            db.SubmitChanges();
        }

        private void itemPrintMauTongHop2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //Barcode
            string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
            //using (var rpt = new ReportMisc.DichVu.Quy.rptGiayNhan2LienPT((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), objnhanvien, barCodeString))
            //{
            //    if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
            //        SavePrint(barCodeString);
            //}
        }

        private void itemBarcode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.GetFocusedRowCellValue("MaPhieu") != null)
            {
                db = new MasterDataContext();
                var objPT = db.PhieuThus.Single(p => p.MaPhieu == (int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
                if (objPT.Barcode == null || objPT.Barcode == "")
                    DialogBox.Alert("[Phiếu thu] này chưa phát sinh mã vạch. Vui lòng kiểm tra lại, xin cảm ơn.");
                //else
                //{
                //    var frm = new ReportMisc.frmBarCode(objPT.Barcode);
                //    frm.ShowDialog();
                //}
            }
            else
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
        }

        private void itemViewTongHop2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (var rpt = new ReportMisc.DichVu.Quy.rptGiayNhan2LienPT((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), objnhanvien, "no"))
            //{
            //    var frm = new ReportMisc.frmPreview(rpt);
            //    frm.ShowDialog();
            //}
        }

        private void itemView1Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (var rpt = new ReportMisc.DichVu.Quy.Sacomreal.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue), objnhanvien.HoTenNV))
            //{
            //    var frm = new ReportMisc.frmPreview(rpt);
            //    frm.ShowDialog();
            //}
        }

        private void itemView2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), ""))
            //{
               // var frm = new ReportMisc.frmPreview(rpt);
                //rpt.ShowPreviewDialog();
           // }
            //var rpt = new ReportMisc.DichVu.HoaDon.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            //rpt.ShowPreviewDialog();
        }

        private void itemViewPQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (ReportMisc.DichVu.Quy.rptManagermentFee rpt = new ReportMisc.DichVu.Quy.rptManagermentFee((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu")))
            //{
            //    var frm = new ReportMisc.frmPreview(rpt);
            //    frm.ShowDialog();
            //}
        }

        private void itemViewTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            if ((bool)grvPhieuThu.GetFocusedRowCellValue("ChuyenKhoan"))
            {
                //using (var rpt = new ReportMisc.DichVu.Quy.Sacomreal.rptPhieuThuTienChuyenKhoan((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue), objnhanvien.HoTenNV))
                //{
                //    var frm = new ReportMisc.frmPreview(rpt);
                //    frm.ShowDialog();
                //}
            }
            else
                DialogBox.Alert("Vui lòng chọn [Phiếu thu] chuyển khoản, xin cảm ơn.");
        }

        private void itemViewTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (var rpt = new ReportMisc.DichVu.Quy.rptGiayNhan((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"), objnhanvien, "no"))
            //{
            //    var frm = new ReportMisc.frmPreview(rpt);
            //    frm.ShowDialog();
            //}
            //var rpt = new ReportMisc.DichVu.HoaDon.rptPhieuThu((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu"));
            //rpt.ShowPreviewDialog();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }

        private void itemViewMau1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV2((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu")))
            //{
            //    var frm = new ReportMisc.frmPreview(rpt);
            //    frm.ShowDialog();
            //}
        }

        private void itemPrintMau1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV2((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu")))
            //{
            //    rpt.PrintDialog();
            //}
        }

        private void itemPrintMau2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuThu.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
                return;
            }

            //using (var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV3((int)grvPhieuThu.GetFocusedRowCellValue("MaPhieu")))
            //{
            //    rpt.PrintDialog();
            //}
        }
    }

    public class ItemData
    {
        public int MaMB { get; set; }
        public string MaSoMB { get; set; }
        public string KhachHang { get; set; }
        public decimal SoTien { get; set; }
        public int SoDot { get; set; }
    }
}