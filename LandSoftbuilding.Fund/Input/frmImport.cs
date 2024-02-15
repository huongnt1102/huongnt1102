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

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        List<int> GetMaLDVs(string _MaLDVs)
        {
            var ltMaLDV = new List<int>();

            try
            {
                var arrMaLDV = _MaLDVs.Split(',');
                if (arrMaLDV[0].Trim() != "")
                {
                    foreach (var i in arrMaLDV)
                    {
                        ltMaLDV.Add(int.Parse(i));
                    }
                }
            }
            catch { }

            return ltMaLDV;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
        }

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

                System.Collections.Generic.List<PhieuThuItem> list = Library.Import.ExcelAuto.ConvertDataTable<PhieuThuItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                //gcImport.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new PhieuThuItem
                //{
                //    NgayCT = p["Ngày chứng từ"].Cast<DateTime>(),
                //    SoCT = p["Số chứng từ"].ToString().Trim(),
                //    MaSoKH = p["Mã khách hàng"].ToString().Trim(),
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    NguoiNop = p["Người nộp"].ToString().Trim(),
                //    DiaChi = p["Địa chỉ"].ToString().Trim(),
                //    DienGiai = p["Diễn giải"].ToString().Trim(),
                //    SoTien = p["Số tiền"].Cast<decimal>(),
                //    SoTKNH = p["Số TKNH"].ToString().Trim(),
                //    ChungTuGoc = p["Chứng từ gốc"].ToString().Trim(),
                //    TenPL = p["Phân loại"].ToString().Trim(),
                //    MaLDVs = p["MaLDV"].ToString().Trim(),
                //    Thang = p["Tháng"].Cast<int>(),
                //    Nam = p["Năm"].Cast<int>()
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
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcImport.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                #region Nap tu dien 
                var ltKhachHang = (from kh in db.tnKhachHangs
                                   where kh.MaTN == this.MaTN
                                   select new { kh.MaKH, MaSoKH = kh.KyHieu.ToLower() })
                                   .ToList();
                var ltMatBang = (from kh in db.mbMatBangs
                                   where kh.MaTN == this.MaTN
                                   select new { kh.MaMB, MaSoMB = kh.MaSoMB.ToLower() })
                                   .ToList();

                var ltPhanLoai = (from pl in db.ptPhanLoais
                                  select new { pl.ID, TenPL = pl.TenPL.ToLower() })
                                  .ToList();

                var ltSoTKNH = (from tk in db.nhTaiKhoans
                                where tk.MaTN == this.MaTN
                                select new { tk.ID, SoTKNH = tk.SoTK.ToLower() })
                                .ToList();
                #endregion

                var ltSource = (List<PhieuThuItem>)gcImport.DataSource;
                var ltError = new List<PhieuThuItem>();

                foreach(var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.NgayCT.Year == 1)
                        {
                            i.Error = "Vui lòng nhập ngày chứng từ";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.SoTien == 0)
                        {
                            i.Error = "Vui lòng nhập số tiền";
                            ltError.Add(i);
                            continue;
                        }

                        var objKH = ltKhachHang.FirstOrDefault(p => p.MaSoKH == i.MaSoKH.ToLower());
                        if (objKH == null)
                        {
                            i.Error = "Khách hàng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }
                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB == i.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            i.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }
                        var objPL = ltPhanLoai.FirstOrDefault(p => p.TenPL == i.TenPL.ToLower());
                        if (objPL == null)
                        {
                            i.Error = "Phân loại không chính xác";
                            ltError.Add(i);
                            continue;
                        }

                        int? _MaTKNH = null;
                        if (!string.IsNullOrEmpty(i.SoTKNH))
                        {
                            var objTKNH = ltSoTKNH.FirstOrDefault(p => p.SoTKNH == i.SoTKNH.ToLower());
                            if (objTKNH == null)
                            {
                                i.Error = "Tài khoản ngân hàng không chính xác";
                                ltError.Add(i);
                                continue;
                            }
                            else
                            {
                                _MaTKNH = objTKNH.ID;
                            }
                        }
                        #endregion

                        #region Kiểm tra khóa hóa đơn
                        // Cần trả về là có được phép sửa hay return
                        // truyền vào form service, từ ngày đến ngày, tòa nhà

                        var result = DichVu.KhoaSo.Class.ClosingEntry.Closing(this.MaTN, i.NgayCT, DichVu.KhoaSo.Class.Enum.PAY);

                        if (result.Count() > 0)
                        {
                            i.Error = "Kỳ thanh toán đã khóa sổ";
                            ltError.Add(i);
                            continue;
                        }

                        #endregion

                        #region Them phieu thu

                        var objPT = new ptPhieuThu();
                        objPT.MaTN = this.MaTN;
                        objPT.NgayThu = i.NgayCT;
                        if (string.IsNullOrEmpty(i.SoCT))
                        {
                            objPT.SoPT = db.CreateSoChungTu(10, this.MaTN);
                        }
                        else
                        {
                            objPT.SoPT = i.SoCT;
                            db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.SoPhieu == i.SoCT && p.IsPhieuThu == true));
                        }
                        objPT.MaKH = objKH.MaKH;
                        objPT.MaMB = objMB.MaMB;
                        objPT.NguoiNop = i.NguoiNop;
                        objPT.DiaChiNN = i.DiaChi;
                        objPT.LyDo = i.DienGiai;
                        objPT.SoTien = i.SoTien;
                        
                        objPT.MaPL = objPL.ID;
                        objPT.MaNV = Common.User.MaNV;
                        objPT.NgayNhap = db.GetSystemDate();
                        objPT.MaNVN = Common.User.MaNV;

                        if (_MaTKNH != null)
                        {
                            objPT.MaTKNH = _MaTKNH;
                            objPT.MaHTHT = 2;
                            objPT.HinhThucThanhToanId = 2;
                            objPT.HinhThucThanhToanName = "Chuyển khoản";
                        }
                        else
                        {
                            objPT.MaTKNH = null;
                            objPT.MaHTHT = 1;
                            objPT.HinhThucThanhToanId = 1;
                            objPT.HinhThucThanhToanName = "Tiền mặt";
                        }

                        objPT.IsKhauTru = false;

                        db.SubmitChanges();

                        #endregion

                        #region Them chi tiet phieu thu
                        var ltMaLDV = this.GetMaLDVs(i.MaLDVs);
                        if (ltMaLDV.Count > 0)
                        {
                            var ltHoaDon = (from hd in db.dvHoaDons
                                            where hd.MaTN == this.MaTN & hd.MaKH == objKH.MaKH & ltMaLDV.Contains(hd.MaLDV.GetValueOrDefault()) & hd.ConNo > 0
                                                & (hd.NgayTT.Value.Month == i.Thang | i.Thang == 0) & (hd.NgayTT.Value.Year == i.Nam | i.Nam == 0)
                                            orderby hd.NgayTT
                                            select new
                                            {
                                                hd.ID,
                                                hd.NgayTT,
                                                hd.DienGiai,
                                                hd.ConNo
                                            }).ToList();

                            if (ltHoaDon.Count == 0)
                            {
                                i.Error = "Không tìm thấy hóa đơn";
                                ltError.Add(i);
                                continue;
                            }
                            else if (ltHoaDon.Sum(p => p.ConNo).GetValueOrDefault() < i.SoTien)
                            {
                                i.Error = "Số tiền [đã thu] phải nhỏ hơn hoặc bằng số tiền [phải thu]";
                                ltError.Add(i);
                                continue;
                            }

                            foreach (var hd in ltHoaDon)
                            {
                                var ct = new ptChiTietPhieuThu();
                                objPT.ptChiTietPhieuThus.Add(ct);
                                ct.TableName = "dvHoaDon";
                                ct.LinkID = hd.ID;
                                ct.DienGiai = hd.DienGiai;
                                if (i.SoTien > hd.ConNo)
                                {
                                    ct.SoTien = hd.ConNo;
                                    i.SoTien -= hd.ConNo.Value;
                                }
                                else
                                {
                                    ct.SoTien = i.SoTien;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            var ct = new ptChiTietPhieuThu();
                            objPT.ptChiTietPhieuThus.Add(ct);
                            ct.DienGiai = i.DienGiai;
                            ct.SoTien = i.SoTien;
                        }
                        #endregion

                        db.ptPhieuThus.InsertOnSubmit(objPT);
                        db.SubmitChanges();

                        foreach (var hd in objPT.ptChiTietPhieuThus)
                        {
                            int? iPL = objPT.MaPL;
                            var phanloai = db.ptPhanLoais.First(_ => _.ID == iPL);
                            var isapp = phanloai.IsDvApp ?? false;
                            Common.SoQuy_Insert(db, objPT.NgayThu.Value.Month, objPT.NgayThu.Value.Year, this.MaTN, (int)objPT.MaKH, (int?)objPT.MaMB, hd.MaPT, hd.ID, objPT.NgayThu, objPT.SoPT, objPT.HinhThucThanhToanId, (int?)iPL, true, hd.PhaiThu.GetValueOrDefault(), hd.SoTien.GetValueOrDefault(), iPL == 2 ? hd.SoTien.GetValueOrDefault() : hd.ThuThua.GetValueOrDefault(), hd.KhauTru.GetValueOrDefault(), hd.LinkID, hd.TableName, hd.DienGiai, Common.User.MaNV, objPT.IsKhauTru.GetValueOrDefault(), isapp);
                        }

                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.isSave = true;

                if (ltError.Count > 0)
                {
                    DialogBox.Alert(string.Format("Có {0:n0} dòng xảy ra lỗi", ltError.Count));
                     gcImport.DataSource = ltError;
                }
                else
                {
                    DialogBox.Success();
                    gcImport.DataSource = null;
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
            Library.Commoncls.ExportExcel(gcImport);
        }
    }

    public class PhieuThuItem
    {
        public DateTime NgayCT { get; set; }
        public string SoCT { get; set; }
        public string MaSoKH { get; set; }
        public string MaSoMB { get; set; }
        public string NguoiNop { get; set; }
        public string DiaChi { get; set; }
        public string DienGiai { get; set; }
        public decimal SoTien { get; set; }
        public string SoTKNH { get; set; }
        public string ChungTuGoc { get; set; }
        public string TenPL { get; set; }
        public string MaLDVs { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string Error { get; set; }
    }
}