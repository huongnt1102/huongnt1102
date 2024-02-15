using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;
using LinqToExcel;

namespace DichVu.MatBang.Import
{
    public partial class frmMatBang : DevExpress.XtraEditors.XtraForm
    {
        public frmMatBang()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool IsSave { get; set; }


        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();

                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<MatBangItem> list = Library.Import.ExcelAuto.ConvertDataTable<MatBangItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvMatBang, itemSheet));

                gcMatBang.DataSource = list;

                //gcMatBang.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new MatBangItem
                //{
                //    TenKN = p["Khối nhà"].ToString().Trim(),
                //    TenTL = p["Tầng lầu"].ToString().Trim(),
                //    TenLMB = p["Loại"].ToString().Trim(),
                //    //TenTT = p["Trạng thái"].ToString().Trim(),
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    //SoNha = p["Số nhà"].ToString().Trim(),
                //    MaSoKH = p["Mã khách hàng"].ToString().Trim(),
                //    MaSoCSH = p["Mã chủ sở hữu"].ToString().Trim(),
                //    DienTich = p["Diện tích"].Cast<decimal>(),
                //    //DienTichThongThuy = p["Diện tích thông thuỷ"].Cast<decimal>(),
                //    //DienTichTimTuong = p["Diện tích tim tường"].Cast<decimal>(),
                //    //TenLG = p["Loại giá thuê"].ToString().Trim(),
                //    //DonGiaThue = p["Đơn giá thuê"].Cast<decimal>(),
                //    TenLT = p["Loại tiền"].ToString().Trim(),
                //    IsCanHo = p["Là căn hộ"].ToString().Trim(),
                //    NgayBG = p["Ngày bàn giao"].Cast<DateTime>(),
                //    DaGiaoChiaKhoa = p["Đã giao chìa khóa"].ToString().Trim(),
                //    DienGiai = p["Diễn giải"].ToString().Trim(),
                //    //KhoangLuiSauCanHo = p["Khoảng lùi sau căn hộ"].Cast<decimal>(),
                //    //KhoangLuiTruocCanHo = p["Khoảng lùi trước căn hộ"].Cast<decimal>(),
                //    //NhaThauXayDung = p["Nhà thầu xây dựng"].ToString().Trim(),
                //    //NhaThauThiCongHoanThienNoiThat = p["Nhà thầu thi công nội thất"].ToString().Trim(),
                //    //NhanVienBanGiaoNha = p["Nhân viên bàn giao"].ToString().Trim(),
                //}).ToList();

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gvMatBang.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcMatBang.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltMatBang = (from mb in db.mbMatBangs where mb.MaTN == this.MaTN orderby mb.MaSoMB select mb.MaSoMB.ToLower()).ToList();

                var ltKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == this.MaTN orderby kh.KyHieu select new { kh.MaKH, KyHieu = kh.KyHieu.ToLower() }).ToList();
                var ltTangLau = (from tl in db.mbTangLaus
                                 join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                 where kn.MaTN == this.MaTN
                                 select new { tl.MaTL,  TenTL = tl.TenTL.ToLower(), TenKN = kn.TenKN.ToLower() })
                                 .ToList();
                var ltLoaiMatBang = (from p in db.mbLoaiMatBangs orderby p.TenLMB  where p.MaTN==this.MaTN select new { p.MaLMB, TenLMB = p.TenLMB.ToLower() }).ToList();
                var ltTrangThai = (from p in db.mbTrangThais orderby p.TenTT select new { p.MaTT, TenTT = p.TenTT.ToLower() }).ToList();
                //var ltLoaiGia = (from p in db.LoaiGiaThues where p.MaTN == this.MaTN orderby p.TenLG select new { p.ID, TenLG = p.TenLG.ToLower() }).ToList();
                var ltLoaiTien = (from p in db.LoaiTiens orderby p.KyHieuLT select new { p.ID, TenLT =  p.KyHieuLT.ToLower() }).ToList();
                var ltSource = (List<MatBangItem>)gcMatBang.DataSource;
                var ltError = new List<MatBangItem>();

                foreach (var mb in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var _MaTL = ltTangLau.Where(p => p.TenKN == mb.TenKN.ToLower() & p.TenTL == mb.TenTL.ToLower()).Select(p => (int?)p.MaTL).FirstOrDefault();
                        if (_MaTL == null)
                        {
                            mb.Error = "Khối nhà hoặc tầng lầu không chính xác";
                            ltError.Add(mb);
                            continue;
                        }

                        var _MaLMB = ltLoaiMatBang.Where(p => p.TenLMB == mb.TenLMB.ToLower()).Select(p => (int?)p.MaLMB).FirstOrDefault();
                        if (_MaLMB == null)
                        {
                            mb.Error = "Loại mặt bằng không tồn tại";
                            ltError.Add(mb);
                            continue;
                        }

                        var _MaTT = ltTrangThai.Where(p => p.TenTT == mb.TenTT.ToLower()).Select(p => (int?)p.MaTT).FirstOrDefault();
                        if (_MaTT == null)
                        {
                            mb.Error = "Trạng thái mặt bằng không tồn tại";
                            ltError.Add(mb);
                            continue;
                        }

                        if (ltMatBang.IndexOf(mb.MaSoMB.ToLower()) >= 0)
                        {
                            mb.Error = "Mặt bằng đã tồn tại";
                            ltError.Add(mb);
                            continue;
                        }

                        int? _MaKH = null;
                        if (!string.IsNullOrEmpty(mb.MaSoKH))
                        {
                            _MaKH = ltKhachHang.Where(p => p.KyHieu == mb.MaSoKH.ToLower()).Select(p => (int?)p.MaKH).FirstOrDefault();
                            if (_MaKH == null)
                            {
                                mb.Error = "Khách hàng không tồn tại";
                                ltError.Add(mb);
                                continue;
                            }
                        }

                        int? _MaCSH = null;
                        if (!string.IsNullOrEmpty(mb.MaSoCSH))
                        {
                            _MaCSH = ltKhachHang.Where(p => p.KyHieu == mb.MaSoKH.ToLower()).Select(p => (int?)p.MaKH).FirstOrDefault();
                            if (_MaCSH == null)
                            {
                                mb.Error = "Chủ sở hữu không tồn tại";
                                ltError.Add(mb);
                                continue;
                            }
                        }

                        int? _MaLG = null;
                        //if (!string.IsNullOrEmpty(mb.TenLG))
                        //{
                        //    _MaLG = ltLoaiGia.Where(p => p.TenLG == mb.TenLG.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                        //    if (_MaLG == null)
                        //    {
                        //        mb.Error = "Loại giá không tồn tại";
                        //        ltError.Add(mb);
                        //        continue;
                        //    }
                        //}

                        int? _MaLT = null;
                        if (!string.IsNullOrEmpty(mb.TenLT))
                        {
                            _MaLT = ltLoaiTien.Where(p => p.TenLT == mb.TenLT.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                            if (_MaLT == null)
                            {
                                mb.Error = "Loại tiền không tồn tại";
                                ltError.Add(mb);
                                continue;
                            }
                        }

                        var objMB = new mbMatBang();
                        objMB.MaSoMB = mb.MaSoMB;
                        objMB.MaTN = this.MaTN;
                    //     KhoangLuiSauCanHo = p["Khoảng lùi sau căn hộ"].Cast<decimal>(),
                    //KhoangLuiTruocCanHo = p["Khoảng lùi trước căn hộ"].Cast<decimal>(),
                    //NhaThauXayDung = p["Nhà thầu xây dựng"].ToString().Trim(),
                    //NhaThauThiCongHoanThienNoiThat = p["Nhà thầu thi công nội thật"].ToString().Trim(),
                    //NhanVienBanGiaoNha = p["Nhân viên bàn giao"].ToString().Trim(),
                        //objMB.KhoangLuiSauCanHo = mb.KhoangLuiSauCanHo;
                        //objMB.KhoangLuiTruocCanHo = mb.KhoangLuiTruocCanHo;
                        //objMB.NhaThauXayDung = mb.NhaThauXayDung;
                        //objMB.NhaThauThiCongHoanThienNoiThat = mb.NhaThauThiCongHoanThienNoiThat;
                        //objMB.NhanVienBanGiaoNha = mb.NhanVienBanGiaoNha;
                        objMB.MaTL = _MaTL;
                        objMB.MaLMB = _MaLMB;
                        objMB.MaTT = _MaTT;
                        objMB.SoNha = mb.SoNha;
                        objMB.MaKH = _MaKH;
                        objMB.MaKHF1 = _MaCSH;
                        objMB.DienTich = mb.DienTich;
                        objMB.DienTichThongThuy = mb.DienTichThongThuy;
                        objMB.DienTichTimTuong = mb.DienTichTimTuong;
                        //objMB.GiaThue = mb.DonGiaThue * mb.DienTich;
                        objMB.MaLT = _MaLT;
                        objMB.IsCanHoCaNhan = mb.IsCanHo == "x";
                        if (mb.NgayBG.Year != 1)
                        {
                            objMB.NgayBanGiao = mb.NgayBG;
                        }
                        objMB.DaGiaoChiaKhoa = mb.DaGiaoChiaKhoa == "x";
                        objMB.DienGiai = mb.DienGiai;
                        objMB.NgayNhap = db.GetSystemDate();
                        objMB.MaNVN = Common.User.MaNV;

                        if (_MaLG != null)
                        {
                            var objGiaThue = new mbGiaThue();
                            objGiaThue.MaLG = 39;
                            objGiaThue.DienTich = mb.DienTich;
                            objGiaThue.DonGia = mb.DonGiaThue;
                            objGiaThue.ThanhTien = objGiaThue.DienTich * objGiaThue.DonGia;
                            objMB.mbGiaThues.Add(objGiaThue);
                        }

                        db.mbMatBangs.InsertOnSubmit(objMB);
                        db.SubmitChanges();

                        ltMatBang.Add(mb.MaSoMB.ToLower());
                    }
                    catch (Exception ex)
                    {
                        mb.Error = ex.Message;
                        ltError.Add(mb);
                    }
                }

                this.IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcMatBang.DataSource = ltError;
                }
                else
                {
                    gcMatBang.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
                db.Dispose();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcMatBang);
        }
    }

    public class MatBangItem
    {
        public string TenKN { get; set; }
        public string TenTL { get; set; }
        public string TenLMB { get; set; }
        public string TenTT { get; set; }
        public string MaSoMB { get; set; }
        public string SoNha { get; set; }
        public string MaSoKH { get; set; }
        public string MaSoCSH { get; set; }
        public decimal DienTich { get; set; }
        public decimal DienTichThongThuy { get; set; }
        public decimal DienTichTimTuong { get; set; }
        public string TenLG { get; set; }
        public decimal? DonGiaThue { get; set; }
        public string TenLT { get; set; }
        public string IsCanHo { get; set; }
        public DateTime NgayBG { get; set; }
        public string DaGiaoChiaKhoa { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
        public decimal KhoangLuiTruocCanHo{ get; set; }
        public decimal KhoangLuiSauCanHo { get; set; }
           public string  NhaThauXayDung { get; set; }
           public string  NhaThauThiCongHoanThienNoiThat { get; set; }
           public string  NhanVienBanGiaoNha { get; set; }
    }
}