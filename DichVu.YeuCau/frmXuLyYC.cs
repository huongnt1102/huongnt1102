using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.YeuCau
{
    public partial class frmXuLyYC : DevExpress.XtraEditors.XtraForm
    {
        public int? MaYC { set; get; }
        public int TrangThai { get; set; }
        public int? MaTN { set; get; }
        MasterDataContext db;
        tnycYeuCau objYC;

        public frmXuLyYC()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            var ltTN = Common.TowerList.Select(p => (byte?)p.MaTN).ToList();
            lookTrangThai.Properties.DataSource = db.tnycTrangThais;

            lkNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == MaTN).ToList();
            var ltnv = db.tnNhanViens.Where(p =>  p.MaTN==MaTN).ToList();
            lkNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN==MaTN).ToList().Union(ltnv);
            lkNhanVien.Properties.IncrementalSearch = true;
            lkNhanVien.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            lkNhanVien.Properties.ShowButtons = false;
            lkNhanVien.EditValue = Common.User.MaNV;
            lkBoPhan.Properties.DataSource = db.tnPhongBans.Where(p=>p.MaTN==MaTN)
                .Select(p => new { p.MaPB, p.TenPB }).ToList();
            lkBoPhan.EditValue = Common.User.MaPB;
            if (TrangThai==1) // nhan vien xu ly
            {
                var dk = new[] { 2, 3, 7, 10,49 };
                lookTrangThai.Properties.DataSource = from p in db.tnycTrangThais
                                                      where dk.Contains(p.MaTT)
                                                      select p;
            }
            if(TrangThai==2) // doi trang thai
            {
                var dk = new[] { 1,2,3,5,6,8,9,49,56 };
                lookTrangThai.Properties.DataSource = from p in db.tnycTrangThais
                                                      where dk.Contains(p.MaTT)
                                                      select p;
                //lookTrangThai.Properties.DataSource = db.tnycTrangThais;
            }
            if (this.MaYC != null)
            {
                if(TrangThai!=3)
                {
                    objYC = db.tnycYeuCaus.Single(p => p.ID == this.MaYC);

                    if(objYC.MaMB!=null)
                    {
                        txtMatBang.EditValue = objYC.mbMatBang.MaSoMB;
                        txtTrangThaiMB.EditValue = objYC.mbMatBang.mbTrangThai.TenTT;
                    }

                    lookTrangThai.EditValue = objYC.MaTT;
                }
            }
            if (TrangThai==3)
            {
                var dk = new[] { 2, 3, 7, 10 };
                lookTrangThai.Properties.DataSource = from p in db.tnycTrangThais
                                                      where dk.Contains(p.MaTT)
                                                      select p;
                //lookTrangThai.Properties.DataSource = db.tnycTrangThais;
                var objls = (from p in db.tnycLichSuCapNhats
                             where p.ID == this.MaYC
                             select new
                             {
                                 p.MaTT,
                                 p.NoiDung
                             }).FirstOrDefault();
                if(objls!=null)
                {
                    lookTrangThai.EditValue = objls.MaTT;
                    txtNoiDung.Text = objls.NoiDung;
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TrangThai != 3)
                {
                    if (lkBoPhan.EditValue == null)
                    {
                        DialogBox.Alert("Vui lòng chọn [Bộ phận]. Xin cám ơn!");
                        lkBoPhan.Focus();
                        return;
                    }

                    if (lookTrangThai.EditValue == null)
                    {
                        DialogBox.Alert("Vui lòng chọn [Trạng thái]. Xin cám ơn!");
                        lookTrangThai.Focus();
                        return;
                    }

                    if (txtNoiDung.Text.Trim().Length == 0)
                    {
                        DialogBox.Error("Vui lòng nhập nội dung");
                        txtNoiDung.Focus();
                        return;
                    }

                    tnycLichSuCapNhat objXL = new tnycLichSuCapNhat();
                    objXL.MaYC = this.MaYC;
                    objXL.MaNV = Common.User.MaNV;
                    objXL.NgayCN = db.GetSystemDate();
                    objXL.MaTT = (int?)lookTrangThai.EditValue;
                    objXL.NoiDung = txtNoiDung.Text;

                    objXL.NguyenNhan = txtNguyenNhan.Text;
                    objXL.BienPhapXuLy = txtBienPhapXuLy.Text;
                    objXL.MaPB = objYC.MaBP = (int?)lkBoPhan.EditValue;
                    objXL.YKienCH = txtYKienCH.Text;

                    objYC.MaTT = objXL.MaTT;
                    if (TrangThai == 5)
                    {
                        var objYc = db.tnycYeuCaus.FirstOrDefault(_ => _.ID == MaYC);
                        if (objYc == null) return;

                        var objEmailSetup = db.email_Setups.FirstOrDefault(_ =>
                            _.BuildingId == MaTN && _.RequestTyleId == MaYC);
                        if (objEmailSetup != null)
                        {
                            if (objEmailSetup.SendCustomer == true)
                            {
                                if (objYc.MaKH != null)
                                {
                                    var objKh = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == objYc.MaKH);
                                    if (objKh == null) return;
                                    var report = (from rp in db.rptReports
                                        join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                        where tn.MaTN == this.MaTN & rp.GroupID == 11
                                        orderby rp.Rank
                                        select new { rp.ID, rp.Name }).ToList();
                                    foreach (var item in report)
                                    {
                                        var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                                        if (objForm != null)
                                        {
                                            SendMail(objForm.Content, objYc.TieuDe, txtNoiDung.Text, objKh.EmailKH, (int)objKh.MaKH);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    

                    try
                    {
                        db.tnycLichSuCapNhats.InsertOnSubmit(objXL);
                        db.SubmitChanges();
                        objXL.tnycYeuCau.NgayCN = db.GetSystemDate();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Error(ex.Message);
                    }
                }
                else
                {
                    var Data = new MasterDataContext();
                    var objls = Data.tnycLichSuCapNhats.Single(p => p.ID == this.MaYC);
                    objls.MaTT = (int?)lookTrangThai.EditValue;
                    objls.NoiDung = txtNoiDung.Text;
                    objls.MaNV = Common.User.MaNV;
                    objls.NgayCN = db.GetSystemDate();
                    var objy = Data.tnycYeuCaus.Single(p => p.ID == objls.MaYC);
                    objy.MaTT = (int?)lookTrangThai.EditValue;
                    try
                    {
                        Data.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch
                    { }
                }
            }
            catch
            { }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}