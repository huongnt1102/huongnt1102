using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;

namespace DichVu.ChoThue.PhuLuc
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();

                db = new MasterDataContext();
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var sysdt = db.GetSystemDate();
                try
                {
                    gcPhuLuc.DataSource = (from pl in db.thuePhuLucs
                                           join hdt in db.thueHopDongs on pl.MaHD equals hdt.MaHD into hptt
                                           from hd in hptt.DefaultIfEmpty()
                                           join kht in db.tnKhachHangs on hd.MaKH equals kht.MaKH into khtt
                                           from kh in khtt.DefaultIfEmpty()
                                           where SqlMethods.DateDiffDay(tuNgay, pl.NgayTao.Value) >= 0 &
                                                    SqlMethods.DateDiffDay(pl.NgayTao.Value, denNgay) >= 0
                                           select new
                                           {
                                               pl.ID,
                                               pl.SoPL,
                                               pl.NgayPL,
                                               hd.MaHD,
                                               hd.SoHD,
                                               MaSoMB = hd.mbMatBang != null ? hd.mbMatBang.MaSoMB : "",
                                               pl.ThoiHan,
                                               TenTT = hd.thueTrangThai != null ? hd.thueTrangThai.TenTT : "",
                                               pl.DienTich,
                                               pl.DonGia,
                                               pl.GiaThue,
                                               pl.DienGiai,
                                               pl.NgayGiao,
                                               TenTG = hd.tnTyGia != null ? hd.tnTyGia.TenVT : "",
                                               KhachHang = hd.MaKH != null ? (bool)kh.IsCaNhan ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen : "",
                                               DienThoaiKH = hd.MaKH != null ? kh.DienThoaiKH :  "",
                                               DiaChiKH = hd.MaKH != null ? kh.DCLL  : "",
                                               pl.tnNhanVien.HoTenNV,
                                               pl.ChuKyThanhToan,
                                               TimeRemain = SqlMethods.DateDiffDay(sysdt, pl.NgayGiao.Value.AddMonths(pl.ThoiHan ?? 0)) < 0 ? 0 : SqlMethods.DateDiffDay(sysdt, pl.NgayGiao.Value.AddMonths(pl.ThoiHan ?? 0)),
                                               DaDuyet = pl.IsConfirm ?? false,
                                               pl.NgayTao,
                                               pl.NgayCN,
                                               HoTenNVCN = pl.MaNVCN != null ? pl.tnNhanVien1.HoTenNV : "",
                                               pl.NgayHH,
                                               pl.SoTienCoc,
                                               pl.tnTyGia.TenVT,
                                               pl.ThanhTienKTT,
                                               pl.DonGiaUSD,
                                               pl.GiaThueUSD,
                                               pl.ChuKyThanhToanUSD,
                                               pl.ThanhTienKTTUSD,
                                               pl.SoTienCocUSD,
                                           }).ToList();
                }
                catch { }
                finally
                {
                    wait.Close();
                    wait.Dispose();
                }
            }
            else
            {
                gcPhuLuc.DataSource = null;
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

        void DeleteSelected()
        {
            int[] indexs = grvPhuLuc.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Phụ lục], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                List<thuePhuLuc> lst = new List<thuePhuLuc>();
                foreach (int i in indexs)
                {
                    thuePhuLuc objNK = db.thuePhuLucs.Single(p => p.ID == (int)grvPhuLuc.GetRowCellValue(i, "ID"));
                    if (objNK.IsConfirm.GetValueOrDefault())
                    {
                        DialogBox.Alert(string.Format("Phụ lục {0} đã được duyệt, không thể xoá!", objNK.SoPL));
                    }
                    else
                        lst.Add(objNK);
                }
                db.thuePhuLucs.DeleteAllOnSubmit(lst);
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
            if (grvPhuLuc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Phụ lục], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { ID = (int?)grvPhuLuc.GetFocusedRowCellValue("ID"), objnhanvien = objnhanvien, objPL = db.thuePhuLucs.Single(p => p.ID == (int)grvPhuLuc.GetFocusedRowCellValue("ID")) })
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

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhuLuc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Phụ lục], xin cảm ơn.");
                return;
            }

            var selectedNK = new List<int>();
            foreach (int row in grvPhuLuc.GetSelectedRows())
            {
                selectedNK.Add((int)grvPhuLuc.GetRowCellValue(row, "ID"));
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();

                var ts = (from pl in db.thuePhuLucs
                          join hdt in db.thueHopDongs on pl.MaHD equals hdt.MaHD into hptt
                          from hd in hptt.DefaultIfEmpty()
                          join kht in db.tnKhachHangs on hd.MaKH equals kht.MaKH into khtt
                          from kh in khtt.DefaultIfEmpty()
                          where selectedNK.Contains(pl.ID)
                          select
                          new
                          {
                              pl.SoPL,
                              pl.NgayPL,
                              hd.MaHD,
                              hd.SoHD,
                              MaSoMB = hd.mbMatBang != null ? hd.mbMatBang.MaSoMB : "",
                              pl.ThoiHan,
                              TenTT = hd.thueTrangThai != null ? hd.thueTrangThai.TenTT : "",
                              pl.DienTich,
                              pl.DonGia,
                              pl.GiaThue,
                              pl.DienGiai,
                              pl.NgayGiao,
                              TenTG = hd.tnTyGia != null ? hd.tnTyGia.TenVT : "",
                              KhachHang = hd.MaKH != null ? (bool)kh.IsCaNhan ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen : "",
                              DienThoaiKH = hd.MaKH != null ? kh.DienThoaiKH: "",
                              DiaChiKH = hd.MaKH != null ? kh.DCLL: "",
                              pl.tnNhanVien.HoTenNV,
                              pl.ChuKyThanhToan,
                              TimeRemain = SqlMethods.DateDiffDay(db.GetSystemDate(), pl.NgayGiao.Value.AddMonths(pl.ThoiHan ?? 0)) < 0 ? 0 : SqlMethods.DateDiffDay(db.GetSystemDate(), pl.NgayGiao.Value.AddMonths(pl.ThoiHan ?? 0)),
                              DaDuyet = pl.IsConfirm ?? false,
                              pl.NgayTao,
                              pl.NgayCN,
                              HoTenNVCN = pl.MaNVCN != null ? pl.tnNhanVien1.HoTenNV : ""
                          }).ToList();
                dt = SqlCommon.LINQToDataTable(ts);
                ExportToExcel.exportDataToExcel("Danh sách phụ lục hợp đồng", dt);
            }
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien })
            //{
            //    frm.ShowDialog();
            //    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //        LoadData();
            //}
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhuLuc.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phụ lục], xin cảm ơn.");
                return;
            }

            using (MasterDataContext db = new MasterDataContext())
            {
                try
                {
                    thuePhuLuc pl = db.thuePhuLucs.Single(p => p.ID == (int)grvPhuLuc.GetFocusedRowCellValue("ID"));
                    thueHopDong hd = db.thueHopDongs.Single(p => p.MaHD == pl.MaHD);

                    pl.IsConfirm = true;
                    hd.ThoiHan = pl.ThoiHan;
                    hd.DienTich = pl.DienTich;
                    hd.DonGia = pl.DonGia;
                    hd.NgayBG = pl.NgayGiao;
                    hd.ThanhTien = pl.GiaThue;
                    hd.ChuKyThanhToan = pl.ChuKyThanhToan;
                    hd.NgayCN = db.GetSystemDate();
                    hd.MaNVCN = objnhanvien.MaNV;

                    var objLS = new thueLichSu();
                    objLS.DienGiai = "Duyệt phụ lục hợp đồng" + pl.SoPL;
                    objLS.MaHD = hd.MaHD;
                    objLS.MaNV = objnhanvien.MaNV;
                    objLS.MaTT = 2;
                    objLS.NgayTao = db.GetSystemDate();
                    db.thueLichSus.InsertOnSubmit(objLS);

                    db.SubmitChanges();
                    LoadData();
                    DialogBox.Alert("Dữ liệu đã được cập nhật.");
                }
                catch { }
            }
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhuLuc.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phụ lục], xin cảm ơn.");
                return;
            }

            using (MasterDataContext db = new MasterDataContext())
            {
                try
                {
                    thuePhuLuc pl = db.thuePhuLucs.Single(p => p.ID == (int)grvPhuLuc.GetFocusedRowCellValue("ID"));
                    if (pl.IsConfirm.GetValueOrDefault())
                    {
                        thueHopDong hd = db.thueHopDongs.Single(p => p.MaHD == pl.MaHD);

                        pl.IsConfirm = false;
                        hd.ThoiHan = pl.ThoiHanOld;
                        hd.DienTich = pl.DienTichOld;
                        hd.DonGia = pl.DonGiaOld;
                        hd.NgayBG = pl.NgayBGOld;
                        hd.ThanhTien = pl.GiaThueOld;
                        hd.ChuKyThanhToan = pl.ChuKyThanhToanOld;
                        hd.NgayCN = db.GetSystemDate();
                        hd.MaNVCN = objnhanvien.MaNV;

                        var objLS = new thueLichSu();
                        objLS.DienGiai = "Không duyệt duyệt phụ lục hợp đồng" + pl.SoPL;
                        objLS.MaHD = hd.MaHD;
                        objLS.MaNV = objnhanvien.MaNV;
                        objLS.MaTT = 2;
                        objLS.NgayTao = db.GetSystemDate();
                        db.thueLichSus.InsertOnSubmit(objLS);

                        db.SubmitChanges();
                        LoadData();
                        DialogBox.Alert("Dữ liệu đã được cập nhật.");
                    }
                }
                catch { }
            }
        }

        private void grvPhuLuc_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                { e.Info.DisplayText = (e.RowHandle + 1).ToString(); }
            }
            catch { }
        }

        private void itemTaoCN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhuLuc.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }

            thuePhuLuc objPL = db.thuePhuLucs.Single(p => p.ID == (int)grvPhuLuc.GetFocusedRowCellValue("ID"));
            if (objPL.IsTaoCN.GetValueOrDefault() == true)
            {
                DialogBox.Alert("Phụ lục này đã phát sinh công nợ!");
                return;
            }
            if (DialogBox.Question("Bạn có muốn hình thành công nợ cho phụ lục này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                //delete công nợ đến thời điểm của phụ lục hiện tại
                decimal? GiaThue = 0;
                int? ChuKyThanhToan = 0;
                var objThueCN = db.thueCongNos.Where(p => p.MaHD == objPL.MaHD && SqlMethods.DateDiffDay(p.ChuKyMin, objPL.NgayGiao) <= 0);
                var objCNLS = db.cnLichSus.Where(p => p.MaMB == objPL.thueHopDong.MaMB && p.MaKH == objPL.thueHopDong.MaKH && p.MaLDV == 5 && (SqlMethods.DateDiffDay(p.NgayNhap, objPL.NgayGiao) <= 0 || SqlMethods.DateDiffDay(p.NgayCN, objPL.NgayGiao) <= 0));
                var objCNCongNo = db.cnCongNos.Where(p => p.MaLDV == 5 && p.MaMB == objPL.thueHopDong.MaMB && p.MaKH == objPL.thueHopDong.MaKH && SqlMethods.DateDiffDay(p.DateCreate, objPL.NgayGiao) <= 0);
                db.thueCongNos.DeleteAllOnSubmit(objThueCN);
                db.cnCongNos.DeleteAllOnSubmit(objCNCongNo);
                db.cnLichSus.DeleteAllOnSubmit(objCNLS);
                db.SubmitChanges();
                //
                #region Add vào công nợ
                var objhopdongthue = new Library.CongNoPL.PhuLucHD();
                var objchuky = new Library.CongNoPL.ChuKyCls();
                List<Library.CongNoPL.ChuKyCls> lstchuky = new List<Library.CongNoPL.ChuKyCls>();
                objchuky = objhopdongthue.LayChuKyTheoThoiDiemHienTai(objPL);
                lstchuky = objhopdongthue.DanhSachChuKyThanhToan(objPL);

                var objLS = db.cnLichSus.Where(p => p.MaLDV == 5 && p.MaMB == objPL.thueHopDong.MaMB);
                GiaThue = objPL.TyGiaPL == 1 ? objPL.GiaThue : objPL.GiaThueUSD;
                ChuKyThanhToan = objPL.TyGiaPL == 1 ? objPL.ChuKyThanhToan : objPL.ChuKyThanhToanUSD;
                decimal? ConNoCuoiKy = db.thueCongNos.Where(p => p.MaHD == objPL.MaHD).OrderByDescending(p => p.ChuKyMin).FirstOrDefault().NoTruoc;
                for (int i = 0; i < lstchuky.Count; i++)
                {
                    var chuky = lstchuky[i];
                    if (i != lstchuky.Count - 1)
                    {
                        cnLichSu objcnls = new cnLichSu()
                        {
                            MaMB = objPL.thueHopDong.MaMB,
                            MaKH = objPL.thueHopDong.MaKH,
                            SoTien = GiaThue * ChuKyThanhToan,
                            MaLDV = 5,
                            DaThu = 0,
                            NoTruoc = (i+1) * GiaThue * ChuKyThanhToan + ConNoCuoiKy,
                            IsPay = false,
                            NgayNhap = chuky.Max
                        };
                        db.cnLichSus.InsertOnSubmit(objcnls);

                        thueCongNo objcongno = new thueCongNo()
                        {
                            MaHD = objPL.MaHD,
                            DaThanhToan = 0,
                            ConNo = GiaThue * ChuKyThanhToan,
                            NoTruoc = (i + 1) * GiaThue * ChuKyThanhToan + ConNoCuoiKy,
                            ChuKyMin = chuky.Min,
                            ChuKyMax = chuky.Max
                        };
                        db.thueCongNos.InsertOnSubmit(objcongno);
                    }
                    else
                    {
                        cnLichSu objcnls = new cnLichSu()
                        {
                            MaMB = objPL.thueHopDong.MaMB,
                            MaKH = objPL.thueHopDong.MaKH,
                            SoTien = GiaThue * ((objPL.ThoiHan % ChuKyThanhToan) == 0 ? ChuKyThanhToan : (objPL.ThoiHan % ChuKyThanhToan)),
                            MaLDV = 5,
                            DaThu = 0,
                            NoTruoc = (i + 1) * GiaThue * ChuKyThanhToan + ConNoCuoiKy,
                            IsPay = false,
                            NgayNhap = chuky.Max
                        };
                        db.cnLichSus.InsertOnSubmit(objcnls);

                        thueCongNo objcongno = new thueCongNo()
                        {
                            MaHD = objPL.MaHD,
                            DaThanhToan = 0,
                            ConNo = GiaThue * ((objPL.ThoiHan % ChuKyThanhToan) == 0 ? ChuKyThanhToan : (objPL.ThoiHan % ChuKyThanhToan)),
                            NoTruoc = (i+1) * GiaThue * ChuKyThanhToan + ConNoCuoiKy,
                            //Sau thue
                            ChuKyMin = chuky.Min,
                            ChuKyMax = chuky.Max
                        };
                        db.thueCongNos.InsertOnSubmit(objcongno);

                    }
                }

                try
                {
                    db.SubmitChanges();

                    //  db.thueCongNo_resetDaThuSingle(objHD.MaMB, objHD.MaHD);
                    DialogBox.Alert("Đã phát sinh xong công nợ.");
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                    this.Close();
                }
                #endregion
            }
        }
    }
}