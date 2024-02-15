using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Linq;
using Library;
using System.IO;
using LandSoftBuilding.Receivables.GiayBao;

namespace LandSoftBuilding.Receivables.Email
{
    public partial class frmSend : DevExpress.XtraEditors.XtraForm
    {
        public frmSend()
        {
            InitializeComponent();
        }

        public int? SendID { get; set; }

        public byte MaTN { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
        public int MaMB { get; set; }

        public List<int> ListMaKHs { get; set; }

        private List<int> ListMaLDVs;

        public List<InfoCusSendMail> ListKhMbs;
        public List<Library.CongNoCls.DataCongNo> Depts { get; set; }

        public string FormName { get; set; }

        #region Field
        Thread thread;
        delegate void UpdateControls();
        #endregion

        DevExpress.XtraReports.UI.XtraReport GetReport(int _MaKH, List<int> _MaLDVs)
        {
            var _MaTK = (int)lkTaiKhoan.EditValue;
            var _ReportID = (int)lkMauIn.EditValue;
            switch (_ReportID)
            {
                
                case 89:
                    return new rptGiayBaoTTC(this.MaTN, Month, Year, _MaKH, MaMB, _MaLDVs, _MaTK);
                case 100:
                    return new rptGiayBaoTTC_XemLai(this.MaTN, Month, Year, _MaKH, MaMB, _MaLDVs, _MaTK);
                case 90:
                    return new rptThongBaoNhacNo2(this.MaTN, Month, Year, _MaKH, MaMB, _MaLDVs, _MaTK);
                case 91:
                    return new rptThongBaoNhacNo1(this.MaTN, Month, Year, _MaKH, MaMB, _MaLDVs, _MaTK);
                case 92:
                    return new rptGiayBaoImperiaNhacNo(this.MaTN, Month, Year, MaMB, _MaLDVs, _MaTK);
                case 93:
                    return new rptGiayBaoImperiaPQL(this.MaTN, Month, Year, _MaKH, _MaLDVs, _MaTK);
                case 94:
                    return new rptGiayBaoImperiaNuoc(this.MaTN, Month, Year, _MaKH, _MaLDVs, _MaTK);
                case 95:
                    return new rptGiayBaoImperiaXe(this.MaTN, Month, Year, _MaKH, _MaLDVs, _MaTK);
                case 96:
                    return new rptGiayBaoImperia(this.MaTN, Month, Year, _MaKH, _MaLDVs, _MaTK);
                case 98:
                    return new RptThongBaoThuPhiQuanLyVanHanh04(MaTN, _MaKH, Month, Year, _MaTK);
                case 99:
                    return new RptThongBaoThuPhiQuanLyVanHanh05(MaTN, _MaKH, Month, Year, _MaTK);
                     default:
                    return null;
            }
        }

        void updateTongSo(int count)
        {
            lblTongSo.Text = "Tổng số: " + count;
        }

        void updateStatus(int max)
        {
            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.PercentView = true;
            progressBarControl1.Properties.Maximum = max;
            progressBarControl1.Properties.Minimum = 0;
        }

        void updateStatus()
        {
            progressBarControl1.PerformStep();
        }

        void updateDaGui(int count)
        {
            lblDaGui.Text = "Đã gửi: " + count;
        }

        void updateThanhCong(int count)
        {
            lblThanhCong.Text = "Thành công: " + count;
        }

        void updateThatBai(int count)
        {
            lblThatBai.Text = "Thất bại: " + count;
        }

        void enableControl(bool isStop)
        {
            btnStart.Enabled = isStop;
            btnStop.Enabled = !isStop;
            btnClose.Enabled = isStop;
        }

        void showForm()
        {
            try
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            catch { }
        }

        void updateNofiIcon(int thanhcong, int thatbai)
        {
            notifyIcon1.Text = string.Format("Tiến trình gửi mail: thành công ({0}), thất bại ({1})", thanhcong, thatbai);
        }

        List<int> GetMaLDV()
        {
            var ltMaLDV = new List<int>();
            var arrMaLDV = ckbLoaiDichVu.EditValue.ToString().Split(',');
            foreach (var s in arrMaLDV)
            {
                if (s != "")
                {
                    ltMaLDV.Add(int.Parse(s));
                }
            }

            return ltMaLDV;
        }

        void process()
        {
            var db = new MasterDataContext();
            var objMail = new Marketing.Mail.MailClient();
            int thanhCong = 0, thatBai = 0;
            var daGui = 0;
            try
            {
                lblTongSo.BeginInvoke(new UpdateControls(() => updateTongSo(ListKhMbs.Count)));
                progressBarControl1.BeginInvoke(new UpdateControls(() => updateStatus(ListKhMbs.Count)));

                ListMaLDVs = GetMaLDV();

                var TuNgay = new DateTime(Year, Month, 1);
                var DenNgay = Common.GetLastDayOfMonth(Month, Year);

                foreach (var i in Depts)
                {
                    var status = 1;
                    var message = htmlContent.InnerHtml;
                    //string message = "aa";
                    var strCc = "";
                    var objKh = db.tnKhachHangs.SingleOrDefault(p => p.MaKH == i.MaKH);
                    var objFrom = db.mailConfigs.SingleOrDefault(p => p.ID == (int?)lkEmailSend.EditValue);

                    try
                    {
                        if (objKh == null) continue;

                        string email_nhan = "";
                        if (objKh.IsCaNhan == false)
                        {
                            // lấy danh sách nhận email từ cấu hình email
                            var strBoPhan = (chkBoPhanLienHe.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                            var ltBoPhan = strBoPhan.Split(',');
                            foreach (var item in ltBoPhan)
                            {
                                var bophan = int.Parse(item);
                                var obj_email_nhan = db.tnKhachHang_NguoiLienHes.Where(_ => _.MaKH == objKh.MaKH & _.MaBoPhan == bophan);
                                foreach (var ii in obj_email_nhan)
                                    email_nhan += ii.Email + ", ";
                            }
                        }
                        else email_nhan = objKh.EmailKH;
                        email_nhan = email_nhan.Trim().Trim(',');


                        if (email_nhan == "") continue;

                        if (objFrom == null) continue;

                        #region Thay the noi dung

                        string tenKh = "";
                        if(objKh.IsCaNhan.GetValueOrDefault())
                        {
                            tenKh = Convert.ToString(objKh.TenKH);
                        }
                        else
                        {
                            tenKh = Convert.ToString(objKh.CtyTen);
                        }

                        message = message.Replace("[BirthDate]", string.Format("{0:dd/MM/yyyy}", objKh.NgaySinh));
                        message = message.Replace("[Phone]", objKh.DienThoaiKH);
                        message = message.Replace("[Email]", objKh.EmailKH);
                        message = message.Replace("[HomeAddress]", objKh.DCLL);
                        message = message.Replace("[CompanyName]", objKh.CtyTen);
                        message = message.Replace("[Month]", this.Month.ToString());
                        message = message.Replace("[Year]", this.Year.ToString());
                        message = message.Replace("[FullName]", tenKh);
                        //account number

                        var tkTaiKhoan = (from tk in db.nhTaiKhoans
                                              join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                              where tk.ID == (int?)lkTaiKhoan.EditValue
                                              select new {
                                                tk.SoTK,
                                                tk.ChuTK,
                                                nh.TenNH,
                                                nh.DiaChi
                                              }).ToList().FirstOrDefault();
                        message = message.Replace("[SubCode]", objKh.MaPhu.ToString());
                        message = message.Replace("[AccountNumber]", tkTaiKhoan!= null? tkTaiKhoan.SoTK : "");
                        message = message.Replace("[AccountHolder]", tkTaiKhoan!= null? tkTaiKhoan.ChuTK : "");
                        message = message.Replace("[BankName]", tkTaiKhoan!= null? tkTaiKhoan.TenNH : "");
                        message = message.Replace("[BankAddress]", tkTaiKhoan!= null? tkTaiKhoan.DiaChi : "");

                        string departmentCode = "";
                        try
                        {
                            var matBangs = db.mbMatBangs.Where(_ => _.MaKH == objKh.MaKH);
                            foreach (var itemMatBang in matBangs)
                            {
                                departmentCode += itemMatBang.MaSoMB + "; ";
                            }
                            departmentCode = departmentCode.Trim().Trim(';');

                            
                        }
                        catch { }

                        message = message.Replace("[DepartmentCode]", departmentCode);
                        #endregion

                        #region Danh sach CC mail
                        var ltNhanVien = (from p in db.mailStaffs
                                          join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                          where p.MaTN == this.MaTN & nv.Email != ""
                                          select new { nv.Email }).ToList();
                        foreach (var item in ltNhanVien)
                            strCc += item.Email + ", ";
                        strCc = strCc.Trim().Trim(',');
                        #endregion

                        #region File dinh kem
                        
                        //DevExpress.XtraRichEdit.RichEditControl richEdit = e.Item.Tag as RichEditControl;
                        //richEdit.LoadDocument("Documents\\Grimm.docx");
                        ////Set the required export options:
                        //DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
                        //options.DocumentOptions.Author = "Mark Jones";
                        //options.Compressed = false;
                        //options.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.High;
                        ////Export the document to the file:
                        //richEdit.ExportToPdf("resultingDocument.pdf", options);
                        ////Export the document to the file stream:
                        //using (FileStream pdfFileStream = new FileStream("resultingDocumentFromStream.pdf", FileMode.Create))
                        //{
                        //    richEdit.ExportToPdf(pdfFileStream, options);
                        //}

                        //System.Diagnostics.Process.Start("resultingDocument.pdf");
                        //var rpt = new GiayBao.rptGiayBao(this.MaTN, this.Month, this.Year, objKH.MaKH,MaMB, ListMaLDVs, (int)lkTaiKhoan.EditValue);
                        //rpt.ExportToPdf(streamPDT);
                        var reportId = (int)lkMauIn.EditValue;

                        int? templateId = 0;

                        var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)lkMauIn.EditValue);
                        if (objForm != null)
                        {
                            if(objForm.IsUseApartment.GetValueOrDefault())
                            {
                                var ApartmentList = db.mbMatBangs.Where(_ => _.MaKH == i.MaKH.GetValueOrDefault());
                                //var attachments = new List<System.Net.Mail.Attachment>();
                                objMail.Attachs = new List<System.Net.Mail.Attachment>();
                                foreach (var am in ApartmentList)
                                {
                                    var streamPdt = new System.IO.MemoryStream();
                                    var rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Month, Year, am.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, i, objForm.IsUseApartment.GetValueOrDefault(), am.MaMB);

                                    DevExpress.XtraRichEdit.RichEditControl richEdit = new DevExpress.XtraRichEdit.RichEditControl();
                                    richEdit.RtfText = rtfText;

                                    DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
                                    options.DocumentOptions.Author = "Mark Jones";
                                    options.Compressed = false;
                                    options.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.High;

                                    //using (FileStream pdfFileStream = new FileStream("resultingDocumentFromStream.pdf", FileMode.Create))
                                    //{
                                    //    richEdit.ExportToPdf(pdfFileStream, options);
                                    //}

                                    richEdit.ExportToPdf(streamPdt, options);
                                    streamPdt.Seek(0, System.IO.SeekOrigin.Begin);
                                    string name = "thongbaophi." + am.MaSoMB + ".pdf";
                                    var fileAttach = new System.Net.Mail.Attachment(streamPdt, name, "application/pdf");

                                    //attachments.Add(fileAttach);
                                    objMail.Attachs.Add(fileAttach);
                                }
                                //objMail.Attachs = attachments;
                                //ail.Attachments.Add(new Attachment(txtAttachments.Text.ToString()));
                                
                            }
                            else
                            {
                                var streamPdt = new System.IO.MemoryStream();
                                var rtfText = BuildingDesignTemplate.Class.ThongBaoPhi.Merge(MaTN, Month, Year, i.MaKH.GetValueOrDefault(), GetMaLDV(), (int)lkTaiKhoan.EditValue, objForm.Content, 0, 0, i, objForm.IsUseApartment.GetValueOrDefault(), 0);

                                DevExpress.XtraRichEdit.RichEditControl richEdit = new DevExpress.XtraRichEdit.RichEditControl();
                                richEdit.RtfText = rtfText;

                                DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
                                options.DocumentOptions.Author = "Mark Jones";
                                options.Compressed = false;
                                options.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.High;

                                //using (FileStream pdfFileStream = new FileStream("resultingDocumentFromStream.pdf", FileMode.Create))
                                //{
                                //    richEdit.ExportToPdf(pdfFileStream, options);
                                //}

                                richEdit.ExportToPdf(streamPdt, options);
                                streamPdt.Seek(0, System.IO.SeekOrigin.Begin);
                                var fileAttach = new System.Net.Mail.Attachment(streamPdt, "thongbaophi.pdf", "application/pdf");
                                objMail.Attachs = new List<System.Net.Mail.Attachment>();
                                objMail.Attachs.Add(fileAttach);
                            }


                            //richEdit.ExportToPdf(streamPdt);
                            templateId = objForm.Id;
                        }



                        #region Mẫu thông báo kiểu xtrareport
                        //switch (reportId)
                        //{
                        //    // thông báo tiền điện 3 pha
                        //    case 7:
                        //    {
                        //        var rpt = new rptTienDien3Pha(MaTN, objKh.MaKH,Month, Year);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }

                        //        break;
                        //    // TTC
                        //    case 89:
                        //    {
                        //        var rpt = new rptGiayBaoTTC(MaTN, Month, Year,
                        //            objKh.MaKH, i.MaMB, ListMaLDVs, (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    // thông báo nhắc nợ lần 1
                        //    case 90:
                        //    {
                        //        var rpt = new rptThongBaoNhacNo2(MaTN, Month, Year, objKh.MaKH, i.MaMB,
                        //            ListMaLDVs, (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    // thông báo nhắc nợ lần 2
                        //    case 91:
                        //    {
                        //        var rpt = new rptThongBaoNhacNo1(MaTN, Month, Year, objKh.MaKH, i.MaMB,
                        //            ListMaLDVs, (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    case 92:
                        //    {
                        //        var rpt = new rptGiayBaoImperiaNhacNo(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    case 93:
                        //    {
                        //        var rpt = new rptGiayBaoImperiaPQL(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    case 94:
                        //    {
                        //        var rpt = new rptGiayBaoImperiaNuoc(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    case 95:
                        //    {
                        //        var rpt = new rptGiayBaoImperiaXe(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    case 96:
                        //    {
                        //        var rpt = new rptGiayBaoImperia(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    // thông báo thu phí vận hành mẫu 4
                        //    case 98:
                        //    {
                        //        var rpt = new RptThongBaoThuPhiQuanLyVanHanh04(MaTN, objKh.MaKH, Month, Year,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    // thông báo thu phí vận hành mẫu 5
                        //    case 99:
                        //    {
                        //        var rpt = new RptThongBaoThuPhiQuanLyVanHanh05(MaTN, objKh.MaKH, Month, Year,
                        //            (int) lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //    case 100:
                        //    {
                        //        var rpt = new rptGiayBaoTTC_XemLai(this.MaTN, Month, Year, objKh.MaKH, i.MaMB, ListMaLDVs, (int)lkTaiKhoan.EditValue);
                        //        rpt.ExportToPdf(streamPdt);
                        //    }
                        //        break;
                        //}
                        #endregion


                        //var rpt = new GiayBao.rptGiayBao(this.MaTN, this.Month, this.Year, objKH.MaKH, MaMB, ListMaLDVs, (int)lkTaiKhoan.EditValue);
                        //rpt.ExportToPdf(streamPDT);
                        //streamPdt.Seek(0, System.IO.SeekOrigin.Begin);
                        #endregion

                        #region Gui mail
                        objMail.SmtpServer = objFrom.Server;
                        objMail.Port = objFrom.Port.Value;
                        objMail.EnableSsl = objFrom.EnableSsl.Value;
                        objMail.From = objFrom.Email;
                        objMail.Reply = objFrom.Reply;
                        objMail.Display = objFrom.Display;
                        objMail.Pass = it.EncDec.Decrypt(objFrom.Password);
                        objMail.To = email_nhan;
                        if (strCc != "") objMail.Cc = strCc;
                        objMail.Subject = txtTieuDe.Text;
                        objMail.Content = message;
                        //var fileAttach = new System.Net.Mail.Attachment(streamPdt, "thongbaophi.pdf", "application/pdf");
                        //objMail.Attachs = new List<System.Net.Mail.Attachment>();
                        //objMail.Attachs.Add(fileAttach);

                        try
                        {
                            objMail.message = message;
                            objMail.Susscess = thanhCong;
                            objMail.Error = thatBai;
                            objMail.Send();

                            thanhCong = objMail.Susscess.GetValueOrDefault();
                            thatBai = objMail.Error.GetValueOrDefault();
                            status = objMail.Status.GetValueOrDefault();

                        }
                        catch(System.Exception ex) {
                            message = ex.Message;
                        }

                        #region Save to database
                        mailHistory objHisMail = new mailHistory();
                        objHisMail.MailID = objFrom.ID;
                        objHisMail.ToMail = objKh.EmailKH;
                        objHisMail.CcMail = strCc;
                        objHisMail.BccMail = "";
                        objHisMail.Subject = txtTieuDe.Text;
                        objHisMail.Contents = objMail.message;
                        objHisMail.Status = status;
                        objHisMail.DateCreate = db.GetSystemDate();
                        objHisMail.StaffCreate = Common.User.MaNV;
                        objHisMail.CusID = objKh.MaKH;
                        objHisMail.TemplateId = templateId;
                        db.mailHistories.InsertOnSubmit(objHisMail);
                        db.SubmitChanges();
                        #endregion

                        #endregion

                        //thanhCong++;
                        lblThanhCong.BeginInvoke(new UpdateControls(() => updateThanhCong(thanhCong)));
                        //status = 1;
                    }
                    catch
                    {
                        //thatBai++;
                        lblThatBai.BeginInvoke(new UpdateControls(() => updateThatBai(thatBai)));
                        status = 2;
                    }

                    daGui++;
                    lblDaGui.BeginInvoke(new UpdateControls(() => updateDaGui(daGui)));
                    progressBarControl1.BeginInvoke(new UpdateControls(updateStatus));
                    this.BeginInvoke(new UpdateControls(() => updateNofiIcon(thanhCong, thatBai)));

                    //#region Save to database
                    //mailHistory objHisMail = new mailHistory();
                    //objHisMail.MailID = objFrom.ID;
                    //objHisMail.ToMail = objKh.EmailKH;
                    //objHisMail.CcMail = strCc;
                    //objHisMail.BccMail = "";
                    //objHisMail.Subject = txtTieuDe.Text;
                    //objHisMail.Contents = message;
                    //objHisMail.Status = status;
                    //objHisMail.DateCreate = db.GetSystemDate();
                    //objHisMail.StaffCreate = Common.User.MaNV;
                    //objHisMail.CusID = objKh.MaKH;
                    //db.mailHistories.InsertOnSubmit(objHisMail);
                    //db.SubmitChanges();
                    //#endregion

                    this.BeginInvoke(new UpdateControls(() => enableControl(true)));
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                db = null;
                objMail = null;
            }
        }

        void start()
        {
            try
            {
                #region Rang buoc
                if (lkEmailSend.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn [Email gửi]. Xin cám ơn!");
                    return;
                }

                if (lkMauIn.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn mẫu in");
                    return;
                }

                if (lkTaiKhoan.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn [Tài khoản ngân hàng]. Xin cám ơn!");
                    return;
                }

                if (txtTieuDe.Text.Trim() == "")
                {
                    DialogBox.Alert("Vui lòng nhập [Tiêu đề]. Xin cám ơn!");
                    return;
                }

                if (htmlContent.InnerHtml == "")
                {
                    DialogBox.Alert("Vui lòng nhập [Nội dung]. Xin cám ơn!");
                    return;
                }
                #endregion

                enableControl(false);

                thread = new Thread(this.process);
                thread.IsBackground = true;
                thread.Start();
            }
            catch { }
        }

        void stop()
        {
            try
            {
                enableControl(true);
                if (thread != null)
                    thread.Abort();
            }
            catch
            {
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc chắn không?") == System.Windows.Forms.DialogResult.No) return;

            this.start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc chắn không?") == System.Windows.Forms.DialogResult.No) return;

            this.stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                lkEmailSend.Properties.DataSource = (from p in db.mailConfigs
                                                     where p.MaTN == this.MaTN
                                                     select new { p.ID, p.Email }).ToList();
                ckbLoaiDichVu.Properties.DataSource = (from l in db.dvLoaiDichVus
                                                       select new { l.ID, TenLDV = l.TenHienThi }).ToList();
                lkTaiKhoan.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                    join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                    where tk.MaTN == this.MaTN
                                                    select new { tk.ID, tk.SoTK, tk.ChuTK, nh.TenNH })
                                                    .ToList();
                lkMauIn.Properties.DataSource = (from rp in db.rptReports
                                                 join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                                 where tn.MaTN == this.MaTN & rp.GroupID == 1
                                                 orderby rp.Rank
                                                 select new { rp.ID, rp.Name }).ToList();
                lkMauIn.ItemIndex = 0;

                //chkMatBang.Properties.DataSource = result.ToList();
                chkBoPhanLienHe.Properties.DataSource = db.tnKhachHang_NguoiLienHe_BoPhans.ToList();
                var item_default = db.tnKhachHang_BoPhan_NhanEmails.FirstOrDefault(_ => _.FormThucThi == FormName);
                if (item_default != null)
                {
                    chkBoPhanLienHe.SetEditValue(item_default.NhomBoPhan);
                }
                //chkBoPhanLienHe.SetEditValue("1,2");
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void frmSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.stop();
                notifyIcon1.Dispose();
            }
            catch { }
        }

        private void frmSend_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.Visible = false;
            }
            catch { }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.showForm();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            var frm = new Marketing.Mail.Templates.frmManager();
            frm.IsCate = false;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                htmlContent.InnerHtml = frm.Content;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            Marketing.Mail.frmFields frm = new Marketing.Mail.frmFields();
            frm.txtContent = htmlContent;
            frm.Show(this);
        }

        private void htmlContent_ImageBrowser(object sender, MSDN.Html.Editor.ImageBrowserEventArgs e)
        {
            var frm = new FTP.frmUploadFile();

            if (frm.SelectFile(true))
            {
                var wait = DialogBox.WaitingForm();
                wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                byte[] img = File.ReadAllBytes(frm.ClientPath);
                e.ImageUrl = frm.ClientPath;

                wait.Close();
                wait.Dispose();
            }
            frm.Dispose();
        }
    }
}