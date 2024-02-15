using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.YeuCau.Import
{
    public partial class FrmLichSuYeuCau : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public FrmLichSuYeuCau()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());

                    System.Collections.Generic.List<LichSuYeuCauImport> list = Library.Import.ExcelAuto.ConvertDataTable<LichSuYeuCauImport>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new LichSuYeuCauImport
                    //{
                    //    MaYeuCau = _["Mã yêu cầu"].ToString().Trim(),
                    //    NgayCapNhat = _["Ngày cập nhật"].Cast<DateTime>(),
                    //    TrangThai = _["Trạng thái"].ToString().Trim(),
                    //    NoiDung = _["Nội dung"].ToString().Trim(),
                    //    NhanVien = _["Nhân viên"].ToString().Trim(),
                    //    NguyenNhan = _["Nguyên nhân"].ToString().Trim(),
                    //    BienPhapXuLy = _["Biện pháp xử lý"].ToString().Trim(),
                    //    NgayHetHanDauTien = _["Ngày hết hạn đầu tiên"].Cast<DateTime?>(),
                    //    NgayHetHanCuoiCung = _["Ngày hết hạn cuối cùng"].Cast<DateTime?>(),
                    //    ToaNha = _["Tòa nhà"].ToString().Trim()
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objCaTruc = (List<LichSuYeuCauImport>) gc.DataSource;
                var ltError = new List<LichSuYeuCauImport>();
                var toaNha = db.tnToaNhas.ToList();
                var trangThai = db.tnycTrangThais.Select(_ => new {_.MaTT, _.TenTT}).ToList();
                var nhanVien = db.tnNhanViens.Select(_ => new {_.MaNV,_.MaSoNV}).ToList();
                foreach (var n in objCaTruc)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra dữ liệu
                        var objTn = toaNha.FirstOrDefault(_ => _.TenVT.ToLower() == n.ToaNha.ToLower());
                        if (objTn == null)
                        {
                            n.Error = "Tòa nhà " + n.ToaNha + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objYeuCau = db.tnycYeuCaus.FirstOrDefault(_ =>
                            _.MaYC.ToLower() == n.MaYeuCau.ToLower().Trim() & _.MaTN == objTn.MaTN);
                        if (objYeuCau == null)
                        {
                            n.Error = "Yêu cầu " + n.MaYeuCau + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objTrangThai =
                            trangThai.FirstOrDefault(_ => _.TenTT.ToLower() == n.TrangThai.ToLower().Trim());
                        if (objTrangThai == null)
                        {
                            n.Error = "Trạng thái " + n.TrangThai + " không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objNhanVien =
                            nhanVien.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.NhanVien.ToLower().Trim());
                        if (objNhanVien == null)
                        {
                            n.Error = "Nhân viên " + n.NhanVien + " không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        #region insert history
                        var objLichSu = new tnycLichSuCapNhat();
                        objLichSu.MaYC = objYeuCau.ID;
                        objLichSu.MaNV = objNhanVien.MaNV;
                        objLichSu.NgayCN = n.NgayCapNhat;
                        objLichSu.MaTT = objTrangThai.MaTT;
                        objLichSu.NoiDung = n.NoiDung;
                        objLichSu.NguyenNhan = (n.NguyenNhan == "" ? objYeuCau.NoiDung : n.NguyenNhan);
                        objLichSu.BienPhapXuLy = n.BienPhapXuLy;
                        objLichSu.NgayHetHanDauTien = n.NgayHetHanDauTien;
                        objLichSu.NgayHetHanCuoiCung = n.NgayHetHanCuoiCung;
                        objLichSu.GroupProcessId = objYeuCau.GroupProcessId;
                        db.tnycLichSuCapNhats.InsertOnSubmit(objLichSu);
                        #endregion

                        // update objYeuCau
                        objYeuCau.MaTT = objTrangThai.MaTT;

                        #region Gửi thông báo điện thoại và email

                        // nếu không phải là yêu cầu mới thì không cần gửi email
                        var objEmailSetup = db.email_Setups.FirstOrDefault(_ =>
                            _.BuildingId == objTn.MaTN & _.RequestTyleId == objYeuCau.MaLoaiYeuCau);
                        if (objTrangThai.MaTT == 1)
                        {
                            var objThongBao = db.tnycCaiDats
                                .Where(_ => _.MaTN == objTn.MaTN & _.MaPB == objYeuCau.MaBP & _.IsRemind == true)
                                .Select(_ => new { _.MaNV }).Distinct().ToList();
                            foreach (var i in objThongBao)
                            {
                                #region Gửi thông báo điện thoại
                                var objTb = new tnycThongBao();
                                objTb.NgayBD = db.GetSystemDate();
                                objTb.NgayKT = db.GetSystemDate().AddDays(1);
                                objTb.MaNV = i.MaNV;
                                objTb.IsNhac = true;
                                objTb.IsRepeat = true;
                                objTb.TimeID = (byte)1;
                                objTb.TimeID2 = (byte)1;

                                var objTime = db.Times.Single(_ => _.TimeID == (byte?)1);
                                objTb.NgayNhac = objTb.NgayBD.Value.AddMinutes(-objTime.Minutes.GetValueOrDefault());
                                objTb.tnycYeuCau = objYeuCau;
                                db.tnycThongBaos.InsertOnSubmit(objTb);
                                #endregion

                                #region Gửi đến email nhân viên

                                if (objEmailSetup == null) continue;
                                if (objEmailSetup.SendEmployee != true) continue;
                                var emailNv = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == i.MaNV);
                                if (emailNv == null) continue;
                                if (emailNv.Email == "") continue;
                                if (objYeuCau.MaKH == null) continue;
                                var report = (from rp in db.rptReports
                                              join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                              where tn.MaTN == objTn.MaTN & rp.GroupID == 10
                                              orderby rp.Rank
                                              select new { rp.ID, rp.Name }).ToList();
                                foreach (var item in report)
                                {
                                    var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                                    if (objForm != null)
                                    {
                                        SendMail(objForm.Content, objYeuCau.TieuDe, objYeuCau.NoiDung, emailNv.Email,
                                            (int)objYeuCau.MaKH);
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
                                    if (objYeuCau.MaKH != null)
                                    {
                                        var objKh = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == objYeuCau.MaKH);
                                        if (objKh == null) return;
                                        var report = (from rp in db.rptReports
                                                      join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                                      where tn.MaTN == objTn.MaTN & rp.GroupID == 11
                                                      orderby rp.Rank
                                                      select new { rp.ID, rp.Name }).ToList();
                                        foreach (var item in report)
                                        {
                                            var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                                            if (objForm != null)
                                            {
                                                SendMail(objForm.Content, objYeuCau.TieuDe, objYeuCau.NoiDung, objKh.EmailKH, (int)objKh.MaKH);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                        db.SubmitChanges();
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
                db.Dispose();
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

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
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

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class LichSuYeuCauImport
    {
        public DateTime? NgayCapNhat { get; set; }
        public DateTime? NgayHetHanDauTien { get; set; }
        public DateTime? NgayHetHanCuoiCung { get; set; }

        public string TrangThai { get; set; }
        public string NoiDung { get; set; }
        public string NhanVien { get; set; }
        public string NguyenNhan { get; set; }
        public string BienPhapXuLy { get; set; }
        public string ToaNha { get; set; }
        public string MaYeuCau { get; set; }
        public string Error { get; set; }
    }
}