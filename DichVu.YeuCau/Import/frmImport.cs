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
using System.IO;

namespace DichVu.YeuCau
{
    public partial class FrmImport : XtraForm
    {
        private MasterDataContext _db;
        public tnNhanVien Objnhanvien;
        public bool IsSave { get; set; }

        public FrmImport()
        {
            InitializeComponent();
            _db = new MasterDataContext();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            itemNgayNhap.EditValue = _db.GetSystemDate();
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = @"(Excel file)|*.xls;*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);

                    System.Collections.Generic.List<ImportItem> list = Library.Import.ExcelAuto.ConvertDataTable<ImportItem>(Library.Import.ExcelAuto.GetDataExcel(book, gv));

                    gc.DataSource = list;

                    //var newds = book.Worksheet(0).Select(p => new
                    //{

                    //    MaYC = p["Mã yêu cầu"].ToString().Trim(),
                    //    NgayYC = p["Ngày yêu cầu"].Cast<DateTime>(),
                    //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                    //    TrangThai = p["Trạng thái"].ToString().Trim(),
                    //    BoPhan = p["Bộ phận"].ToString().Trim(),
                    //    MucDoUuTien = p["Mức độ ưu tiên"].ToString().Trim(),
                    //    NguonDen = p["Nguồn đến"].ToString().Trim(),
                    //    NguoiGui = p["Người gửi"].ToString().Trim(),
                    //    LoaiYC = p["Loại YC"].ToString().Trim(),
                    //    TGKhachHen = p["TG khách hẹn"].Cast<DateTime>(),
                    //    SDTNguoiGui = p["SDT người gửi"].ToString().Trim(),
                    //    NgayCN = p["Ngày cập nhật"].Cast<DateTime>(),
                    //    NoiDung = p["Nội dung"].ToString().Trim(),
                    //    TieuDe = p["Tiêu đề"].ToString().Trim(),
                    //    BuildingNo = p["Tòa nhà"].ToString().Trim(),
                    //    GroupProcessName = p["Nhóm công việc"].ToString().Trim(),
                    //    NguyenNhan = p["Nguyên nhân"].ToString().Trim(),
                    //    BienPhapXuLy = p["BienPhapXuLy"].ToString().Trim()
                    //}).ToList();

                    //List<ImportItem> newlist = new List<ImportItem>();
                    //foreach (var item in newds)
                    //{
                    //    ImportItem import = new ImportItem
                    //    {
                    //        MaYC = item.MaYC,
                    //        NgayYC = item.NgayYC,
                    //        MaSoMB = item.MaSoMB,

                    //        TrangThai = item.TrangThai,
                    //        BoPhan = item.BoPhan,
                    //        NguonDen = item.NguonDen,
                    //        MucDoUuTien = item.MucDoUuTien,
                    //        NguoiGui = item.NguoiGui,
                    //        LoaiYC = item.LoaiYC,
                    //        TGKhachHen = item.TGKhachHen,
                    //        SDTNguoiGui = item.SDTNguoiGui,
                    //        NgayCN = item.NgayCN,
                    //        NoiDung = item.NoiDung,
                    //        TieuDe = item.TieuDe,
                    //        BuildingNo = item.BuildingNo,
                    //        GroupProcessName = item.GroupProcessName,
                    //        NguyenNhan = item.NguyenNhan,
                    //        BienPhapXuLy = item.BienPhapXuLy
                    //    };
                    //    newlist.Add(import);
                    //}
                    //gc.DataSource = newlist;
                }
                catch(System.Exception ex)
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }

                wait.Close();
                wait.Dispose();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            _db = new MasterDataContext();
            var listMb = (from mb in _db.mbMatBangs
                join kh in _db.tnKhachHangs on mb.MaKH equals kh.MaKH
                select new
                {
                    mb.MaMB,
                    mb.MaSoMB,
                    mb.MaKH,
                    mb.IsCanHoCaNhan,
                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen, mb.MaTN
                }).ToList();

            var listLyc = (from mb in _db.tnycLoaiYeuCaus select new { mb.ID, mb.TenLoaiYeuCau, mb.TenVT }).ToList();
            var listPb = (from mb in _db.tnPhongBans select new { mb.MaPB, mb.TenPB }).ToList();
            //var listNV = (from mb in db.tnNhanViens select new { mb.MaNV, mb.HoTenNV }).ToList();
            var listMucDo = (from mb in _db.tnycDoUuTiens select new { mb.MaDoUuTien, mb.TenDoUuTien }).ToList();
            var listTt = (from mb in _db.tnycTrangThais select new { mb.MaTT, mb.TenTT }).ToList();
            var listNguonDen = (from mb in _db.tnycNguonDens select new { mb.ID, mb.TenNguonDen }).ToList();
            var groupProcess = _db.app_GroupProcesses;
            var toaNha = _db.tnToaNhas;
            var ngaynhap = (DateTime)itemNgayNhap.EditValue;
            var wait = DialogBox.WaitingForm();
            try
            {
                var obj = (List<ImportItem>)gc.DataSource;
                var ltError = new List<ImportItem>();
                foreach (var n in obj)
                {
                    _db = new MasterDataContext();
                    try
                    {
                        #region Kiểm tra dữ liệu
                        var objTn = toaNha.FirstOrDefault(_ => _.TenVT.ToLower() == n.BuildingNo.ToLower());
                        if (objTn == null)
                        {
                            n.Error = "Tòa nhà " + n.BuildingNo + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objMb = listMb.Where(_=>_.MaTN == objTn.MaTN).FirstOrDefault(_ => _.MaSoMB == n.MaSoMB);
                        if(objMb==null)
                        {
                            n.Error = "Mặt bằng " + n.MaSoMB + " không có trong tòa nhà "+n.BuildingNo;
                            ltError.Add(n);
                            continue;
                        }

                        var objLyc = listLyc.FirstOrDefault(_ => _.TenVT.ToLower() == n.LoaiYC.ToLower().Trim());
                        if (objLyc == null)
                        {
                            n.Error = "Loại yêu cầu " + n.LoaiYC + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objPb = listPb.FirstOrDefault(_ => _.TenPB.ToLower() == n.BoPhan.ToLower().Trim());
                        if (objPb == null)
                        {
                            n.Error = "Phòng ban " + n.BoPhan + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objMucDo = listMucDo.FirstOrDefault(_ =>
                            _.TenDoUuTien.ToLower() == n.MucDoUuTien.ToLower().Trim());
                        if (objMucDo == null)
                        {
                            n.Error = "Mức độ " + n.MucDoUuTien + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objTrangThai =
                            listTt.FirstOrDefault(_ => _.TenTT.ToLower() == n.TrangThai.ToLower().Trim());
                        if (objTrangThai == null)
                        {
                            n.Error = "Trạng thái " + n.TrangThai + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objNguonDen =
                            listNguonDen.FirstOrDefault(_ => _.TenNguonDen.ToLower() == n.NguonDen.ToLower().Trim());
                        if (objNguonDen == null)
                        {
                            n.Error = "Nguồn đến " + n.NguonDen + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var listGroupProcess = groupProcess.FirstOrDefault(_ =>
                            _.Name.ToLower() == n.GroupProcessName.ToLower().Trim());
                        if (listGroupProcess == null)
                        {
                            n.Error = "Nhóm công việc " + n.GroupProcessName + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        // import không cập nhật
                        var objYc = new tnycYeuCau();
                        objYc.NgayYC = n.NgayYC;
                        objYc.MaYC = n.MaYC;
                        if (n.MaYC == "") objYc.MaYC = GetNewMaYc();
                        objYc.MaTN = objTn.MaTN;
                        objYc.MaTT = objTrangThai.MaTT;
                        objYc.MaBP = objPb.MaPB;

                        objYc.MaKH = objMb.MaKH;
                        objYc.MaMB = objMb.MaMB;
                        objYc.TieuDe = n.TieuDe;
                        objYc.NoiDung = n.NoiDung;
                        objYc.MaDoUuTien = objMucDo.MaDoUuTien;
                        objYc.MaNguonDen = objNguonDen.ID;
                        objYc.NguoiGui = n.NguoiGui;
                        objYc.MaLoaiYeuCau = objLyc.ID;
                        objYc.TGKhachHen = n.TGKhachHen;
                        objYc.SoDienThoai = n.SDTNguoiGui;
                        objYc.NgayCN = _db.GetSystemDate();
                        objYc.GroupProcessId = listGroupProcess.Id;
                        //save LS
                        var objLS = new tnycLichSuCapNhat();
                        objYc.tnycLichSuCapNhats.Add(objLS);
                        objLS.MaNV = Common.User.MaNV;
                        objLS.NgayCN = _db.GetSystemDate();
                        objLS.MaTT = objTrangThai.MaTT;
                        objLS.NoiDung = objYc.ID != 0 ? "[Cập nhật yêu cầu]" : "[Thêm mới yêu cầu]";
                        objLS.GroupProcessId = listGroupProcess.Id;
                         
                        _db.tnycYeuCaus.InsertOnSubmit(objYc);

                        #region Gửi thông báo điện thoại và email
                        
                        // nếu không phải là yêu cầu mới thì không cần gửi email
                        var objEmailSetup = _db.email_Setups.FirstOrDefault(_ =>
                            _.BuildingId == objTn.MaTN & _.RequestTyleId == objLyc.ID);
                        if (objTrangThai.MaTT == 1)
                        {
                            var objThongBao = _db.tnycCaiDats
                                .Where(_ => _.MaTN == objTn.MaTN & _.MaPB == objPb.MaPB & _.IsRemind == true)
                                .Select(_ => new { _.MaNV }).Distinct().ToList();
                            foreach (var i in objThongBao)
                            {
                                #region Gửi thông báo điện thoại
                                var objTb = new tnycThongBao();
                                objTb.NgayBD = _db.GetSystemDate();
                                objTb.NgayKT = _db.GetSystemDate().AddDays(1);
                                objTb.MaNV = i.MaNV;
                                objTb.IsNhac = true;
                                objTb.IsRepeat = true;
                                objTb.TimeID = (byte)1;
                                objTb.TimeID2 = (byte)1;

                                var objTime = _db.Times.Single(_ => _.TimeID == (byte?)1);
                                objTb.NgayNhac = objTb.NgayBD.Value.AddMinutes(-objTime.Minutes.GetValueOrDefault());
                                objTb.tnycYeuCau = objYc;
                                _db.tnycThongBaos.InsertOnSubmit(objTb);
                                #endregion

                                #region Gửi đến email nhân viên

                                if (objEmailSetup == null) continue;
                                if (objEmailSetup.SendEmployee != true) continue;
                                var emailNv = _db.tnNhanViens.FirstOrDefault(_ => _.MaNV == i.MaNV);
                                if (emailNv == null) continue;
                                if (emailNv.Email == "") continue;
                                if (objMb.MaKH == null) continue;
                                var report = (from rp in _db.rptReports
                                              join tn in _db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                              where tn.MaTN == objTn.MaTN & rp.GroupID == 10
                                              orderby rp.Rank
                                              select new { rp.ID, rp.Name }).ToList();
                                foreach (var item in report)
                                {
                                    var objForm = _db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                                    if (objForm != null)
                                    {
                                        SendMail(objForm.Content, objYc.TieuDe, objYc.NoiDung, emailNv.Email,
                                            (int)objMb.MaKH);
                                    }
                                }

                                #endregion
                            }
                        }
                        #region Gửi đến email khách hàng
                        if (objTrangThai.MaTT == 5)
                        {
                            if (objEmailSetup != null)
                            {
                                if (objEmailSetup.SendCustomer == true)
                                {
                                    if (objMb.MaKH != null)
                                    {
                                        var objKh = _db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == objMb.MaKH);
                                        if (objKh == null) return;
                                        var report = (from rp in _db.rptReports
                                            join tn in _db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                            where tn.MaTN == objTn.MaTN & rp.GroupID == 11
                                            orderby rp.Rank
                                            select new { rp.ID, rp.Name }).ToList();
                                        foreach (var item in report)
                                        {
                                            var objForm = _db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                                            if (objForm != null)
                                            {
                                                SendMail(objForm.Content, objYc.TieuDe, objYc.NoiDung, objKh.EmailKH, (int)objKh.MaKH);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        
                        
                        #endregion

                        _db.SubmitChanges();
                        //DialogBox.Alert("Đã lưu");

                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                _db.Dispose();
            }
        }
        private void SendMail(string rtfText, string title, string content, string email, int maKh)
        {
            var db = new MasterDataContext();
            int thanhCong = 0, thatBai = 0, daGui = 0, status = 1;
            var objMail = new LandSoftBuilding.Marketing.Mail.MailClient();
            var objFrom = db.mailConfigs.OrderByDescending(p => p.ID).FirstOrDefault();
            if (objFrom == null) return;
            try
            {
                var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRtf.RtfText = BuildingDesignTemplate.Class.MergeField.Request(rtfText, title, content); // truyền vào 1 template trong form content, sau đó đổ dữ liệu vào
                DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
                options.DocumentOptions.Author = "";
                options.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.High;
                ctlRtf.ExportToPdf("EmailYeuCau.pdf", options);
                var stream = new MemoryStream();
                ctlRtf.ExportToPdf(stream);
                stream.Seek(0, SeekOrigin.Begin);
                #region Gửi email

                objMail.SmtpServer = objFrom.Server;
                objMail.Port = objFrom.Port.Value;
                objMail.EnableSsl = objFrom.EnableSsl.Value;
                objMail.From = objFrom.Email;
                objMail.Reply = objFrom.Reply;
                objMail.Display = objFrom.Display;
                objMail.Pass = it.EncDec.Decrypt(objFrom.Password);
                objMail.To = email;
                //if (strCc != "") objMail.Cc = strCc;
                objMail.Subject = title;
                objMail.Content = content;
                var fileAttach = new System.Net.Mail.Attachment(stream, "YeuCauCanXuLy.pdf", "application/pdf");
                objMail.Attachs = new List<System.Net.Mail.Attachment>();
                objMail.Attachs.Add(fileAttach);

                objMail.Send();

                #endregion

                thanhCong++;
                status = 1;
            }
            catch
            {
                thatBai++;
                status = 2;
            }


            #region Lịch sử gửi email

            mailHistory objHistoryEmail = new mailHistory();
            objHistoryEmail.MailID = objFrom.ID;
            objHistoryEmail.ToMail = email;
            objHistoryEmail.CcMail = "";
            objHistoryEmail.BccMail = "";
            objHistoryEmail.Subject = title;
            objHistoryEmail.Contents = content;
            objHistoryEmail.Status = status;
            objHistoryEmail.DateCreate = DateTime.UtcNow.AddHours(7);
            objHistoryEmail.StaffCreate = Common.User.MaNV;
            objHistoryEmail.CusID = maKh;
            db.mailHistories.InsertOnSubmit(objHistoryEmail);
            db.SubmitChanges();

            #endregion
        }
        private string GetNewMaYc()
        {
            var maYc = "";
            _db.tnycYeuCau_getNewMaYC(ref maYc);
            return _db.DinhDang(4, int.Parse(maYc));
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.DeleteSelectedRows();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }
    }

    class ImportItem
    {
        //public int? STT { get; set; }
        //public int? MaMB { get; set; }

        public DateTime? NgayYC { get; set; }
        //public DateTime? NgayCN { get; set; }
        //public DateTime? TGVao { get; set; }
        //public DateTime? TGRa { get; set; }
        public DateTime? NgayCN { get; set; }
        public DateTime? TGKhachHen { get; set; }

        public string SDTNguoiGui { get; set; }
        //public string HoTenKH { get; set; }
        public string TrangThai { get; set; }
        public string BoPhan { get; set; }
        public string MucDoUuTien { get; set; }
        public string NguonDen { get; set; }
        public string NguoiGui { get; set; }
        public string LoaiYC { get; set; }
        public string NoiDung { get; set; }
        public string TieuDe { get; set; }
        public string MaYC { get; set; }
        public string MaSoMB { get; set; }
        
        //// ký hiệu viết tắt tòa nhà
        public string BuildingNo { get; set; }

        //// nhóm công việc
        public string GroupProcessName { get; set; }

        //// nguyên nhân
        public string NguyenNhan { get; set; }

        //// biện pháp xử lý
        public string BienPhapXuLy { get; set; }

        public string Error { get; set; }
    }
}