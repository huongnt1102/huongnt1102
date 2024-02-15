using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Fund.Reports
{
    public partial class FrmBcDaThu : XtraForm
    {
        public FrmBcDaThu()
        {
            InitializeComponent();
        }

        private void FrmBcDaThu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var v in objKbc.Source) cbxKbc.Items.Add(v);
            itemKyBaoCao.EditValue = objKbc.Source[7];
            SetDate(7);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                gc.DataSource = null;
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime) itemTuNgay.EditValue;
                    var denNgay = (DateTime) itemDenNgay.EditValue;
                    var toaNha = (byte) itemToaNha.EditValue;

                    var objHd = (from ct in db.SoQuy_ThuChis
                        join kh in db.tnKhachHangs on ct.MaKH equals kh.MaKH
                        join hd in db.dvHoaDons on new {ct.TableName, ct.LinkID} equals
                            new {TableName = "dvHoaDon", LinkID = (long?) hd.ID}
                        join dv in db.dvLoaiDichVus on hd.MaLDV equals dv.ID
                        join nv in db.tnNhanViens on ct.MaNV equals nv.MaNV into nhanVien from nv in nhanVien.DefaultIfEmpty()
                        where ct.MaTN == toaNha && ct.MaLoaiPhieu != 24 &
                              SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0 &
                              SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0 & hd.IsDuyet == true &
                              ct.IsPhieuThu == true
                        group ct by new
                        {
                            dv.TenLDV, kh.KyHieu, kh.IsCaNhan, kh.HoKH, kh.TenKH, kh.CtyTen, ct.NgayPhieu.Value.Year,
                            ct.NgayPhieu.Value.Month, ct.NgayPhieu.Value.Day, ct.SoPhieu, ct.DienGiai,
                            ct.HinhThucThuChi, ct.IsKhauTru, nv.HoTenNV
                        }
                        into g
                        select new BcDaThu
                        {
                            LoaiDichVu = g.Key.TenLDV,
                            MaKhachHang = g.Key.KyHieu,
                            TenKhachHang = g.Key.IsCaNhan == true ? (g.Key.HoKH + " " + g.Key.TenKH) : g.Key.CtyTen,
                            NgayHachToan = g.Key.Day + "/" + g.Key.Month + "/" + g.Key.Year,
                            SoPhieuThu = g.Key.SoPhieu,
                            SoTien = g.Sum(_ => _.DaThu),
                            DienGiai = g.Key.DienGiai,
                            HinhThuc = g.Key.IsKhauTru == true
                                ? "Khấu trừ"
                                : (g.Key.HinhThucThuChi == 0 ? "Tiền mặt" : "Chuyển khoản"),
                            NguoiThu = g.Key.HoTenNV
                        }).ToList();

                    var objThuKhac = (from ct in db.SoQuy_ThuChis
                        join kh in db.tnKhachHangs on ct.MaKH equals kh.MaKH
                        join nv in db.tnNhanViens on ct.MaNV equals nv.MaNV into nhanVien
                        from nv in nhanVien.DefaultIfEmpty()
                        where ct.MaTN == toaNha && ct.MaLoaiPhieu != 24 &
                              SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0 &
                              SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0 & ct.LinkID == null &
                              ct.IsPhieuThu == true
                        group ct by new
                        {
                            kh.KyHieu, kh.IsCaNhan, kh.HoKH, kh.TenKH, kh.CtyTen, ct.NgayPhieu.Value.Year,
                            ct.NgayPhieu.Value.Month,
                            ct.NgayPhieu.Value.Day,
                            ct.SoPhieu,
                            ct.DienGiai,
                            ct.HinhThucThuChi,
                            ct.IsKhauTru,
                            nv.HoTenNV
                        }
                        into g
                        select new BcDaThu
                        {
                            LoaiDichVu = "Phí khác",
                            MaKhachHang = g.Key.KyHieu,
                            TenKhachHang = g.Key.IsCaNhan == true ? (g.Key.HoKH + " " + g.Key.TenKH) : g.Key.CtyTen,
                            NgayHachToan = g.Key.Day + "/" + g.Key.Month + "/" + g.Key.Year,
                            SoPhieuThu = g.Key.SoPhieu,
                            SoTien = g.Sum(_ => _.DaThu),
                            DienGiai = g.Key.DienGiai,
                            HinhThuc = g.Key.IsKhauTru == true ? "Khấu trừ" : (g.Key.HinhThucThuChi == 0 ? "Tiền mặt" : "Chuyển khoản"),
                            NguoiThu = g.Key.HoTenNV
                        }).ToList();
                    var obj = objHd.Concat(objThuKhac);
                    gc.DataSource = (from p in obj where p.SoTien != 0 select p).ToList();
                }
            }
            catch{}
        }

        public class BcDaThu
        {
            public decimal? SoTien { get; set; }
            public string NgayHachToan { get; set; }
            public string SoPhieuThu { get; set; }
            public string MaKhachHang { get; set; }
            public string TenKhachHang { get; set; }
            public string LoaiDichVu { get; set; }
            public string DienGiai { get; set; }
            public string HinhThuc { get; set; }
            public string NguoiThu { get; set; }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.SelectAll();
                //var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                //    .Replace(" ", "");
                //var ltToaNha = strToaNha.Split(',');
                //if (strToaNha == "") return;

                var db = new MasterDataContext();

                var objManagerEmployee = db.mail_SetupSendMailToEmployeeDebits
                    .Where(_ => _.BuildingId == (byte) itemToaNha.EditValue & _.IsSendMail == true & _.GroupId == 2)
                    .Select(_ => new {_.EmployeeId}).Distinct().ToList();

                foreach (var employee in objManagerEmployee)
                {
                    var objEmployee = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == employee.EmployeeId);
                    if (objEmployee == null) continue;
                    if (objEmployee.Email == "") continue;

                    #region Send mail

                    var objMail = new LandSoftBuilding.Marketing.Mail.MailClient();
                    var objFrom = db.mailConfigs.OrderByDescending(_ => _.ID).FirstOrDefault();
                    int status = 1;
                    if (objFrom != null)
                    {
                        try
                        {
                            // file excel
                            var result = new System.IO.MemoryStream();
                            var options = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gc.ExportToXlsx(result);
                            result.Seek(0, System.IO.SeekOrigin.Begin);

                            // send email
                            objMail.SmtpServer = objFrom.Server;
                            if (objFrom.Port != null) objMail.Port = objFrom.Port.Value;
                            if (objFrom.EnableSsl != null) objMail.EnableSsl = objFrom.EnableSsl.Value;
                            objMail.From = objFrom.Email;
                            objMail.Reply = objFrom.Reply;
                            objMail.Display = objFrom.Display;
                            objMail.Pass = it.EncDec.Decrypt(objFrom.Password);
                            objMail.To = objEmployee.Email;
                            objMail.Subject = "Báo cáo đã thu chi tiết theo khách hàng";
                            objMail.Content = "";
                            var fileAttach = new System.Net.Mail.Attachment(result, "BaoCaoDaThuChiTietTheoKhachHang",
                                "application/vnd.ms-excel");
                            objMail.Attachs = new List<System.Net.Mail.Attachment>();
                            objMail.Attachs.Add(fileAttach);

                            objMail.Send();
                            status = 1;
                        }
                        catch
                        {
                            status = 2;
                        }
                    }

                    #endregion

                    #region Lịch sử gửi email

                    mailHistory objHistoryEmail = new mailHistory();
                    objHistoryEmail.MailID = objFrom.ID;
                    objHistoryEmail.ToMail = objEmployee.Email;
                    objHistoryEmail.CcMail = "";
                    objHistoryEmail.BccMail = "";
                    objHistoryEmail.Subject = "Báo cáo đã thu chi tiết theo khách hàng";
                    objHistoryEmail.Contents = "";
                    objHistoryEmail.Status = status;
                    objHistoryEmail.DateCreate = DateTime.UtcNow.AddHours(7);
                    objHistoryEmail.StaffCreate = Common.User.MaNV;
                    objHistoryEmail.CusID = null;
                    db.mailHistories.InsertOnSubmit(objHistoryEmail);
                    db.SubmitChanges();

                    #endregion
                }

                DialogBox.Success("Đã gửi thư xong.");
                return;
            }
            catch
            {
            }
        }
    }
}