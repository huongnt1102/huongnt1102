using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using Library;
using System.Data.Linq.SqlClient;
using System.IO;

namespace DichVu.YeuCau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        public string[] BuildingIds { get; set; }
        public int? ID { get; set; }
        private bool _isSendSms = false;

        MasterDataContext db;
        tnycYeuCau objYC;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        string getNewMaYC()
        {
            string MaYC = "";
            db.tnycYeuCau_getNewMaYC(ref MaYC);
            return db.DinhDang(4, int.Parse(MaYC));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            lkYeuCau.Properties.DataSource = db.tnycLoaiYeuCaus;
            //lkYeuCau.Properties.DataSource = db.email_Setups.Where(_ => _.BuildingId == MaTN).Select(_=>new{TenLoaiYeuCau=_.RequestTyleName,ID=_.RequestTyleId}).ToList();
            searchLookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => BuildingIds.Contains(p.MaTN.ToString())).Select(p => new
            {
                p.MaMB,
                p.MaSoMB,
                p.mbTangLau.TenTL,
                p.mbTangLau.mbKhoiNha.TenKN,
                p.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                MaKH=p.MaKHF1,
                TenKH = p.tnKhachHang.IsCaNhan.GetValueOrDefault() ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen,
                p.MaTN
            }).ToList();
            enableHoiKy(checkHoiKy.Checked);
            lookBoPhan.Properties.DataSource = db.tnPhongBans.Where(p=> BuildingIds.Contains(p.MaTN.ToString())).Select(p => new { p.MaPB, p.TenPB }).ToList();
            lookTrangThai.Properties.DataSource = db.tnycTrangThais.Select(p => new { p.MaTT, p.TenTT });
            glkNhomCongViec.Properties.DataSource = db.app_GroupProcesses.ToList();
            lookDoUuTien.Properties.DataSource = db.tnycDoUuTiens;
            lookNguonDen.Properties.DataSource = db.tnycNguonDens;
            var ltTN = Common.TowerList.Select(p => (byte?)p.MaTN).ToList();
            lkNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaNV != Common.User.MaNV & ltTN.Contains(p.MaTN) & p.MaCV == 17).ToList();
            var ltnv = db.tnNhanViens.Where(p => p.MaNV != Common.User.MaNV & ltTN.Contains(p.MaTN) & p.MaCV != 17).ToList();
            lkNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaNV != Common.User.MaNV & ltTN.Contains(p.MaTN) & p.MaCV == 17).ToList().Union(ltnv);
            if (this.ID != null)
            {
                objYC = db.tnycYeuCaus.Single(p => p.ID == this.ID);
                searchLookMatBang.EditValue = objYC.MaMB;
                dateNgayYC.EditValue = objYC.NgayYC;
                lookBoPhan.EditValue = objYC.MaBP;
                lookCuDan.EditValue = objYC.MaKH;
                txtTieuDe.Text = objYC.TieuDe;
                txtNoiDung.Text = objYC.NoiDung;
                lookDoUuTien.EditValue = objYC.MaDoUuTien;
                lookNguonDen.EditValue = objYC.MaNguonDen;
                txtMaYC.Text = objYC.MaYC;
                txtMaYC.Properties.ReadOnly = true;
                txtNguoiGui.Text = objYC.NguoiGui;
                lookTrangThai.EditValue = objYC.MaTT;
                lkYeuCau.EditValue = objYC.MaLoaiYeuCau;
                dateTGKhachHen.EditValue = objYC.TGKhachHen;
                txtSDTNguoiGui.EditValue = objYC.SoDienThoai;
                glkNhomCongViec.EditValue = objYC.GroupProcessId;

                if (objYC.NgayHetHanBH != null) dateNgayHetHanBH.DateTime = (System.DateTime)objYC.NgayHetHanBH;
            }
            else
            {
                objYC = new tnycYeuCau();
                objYC.MaNV = Common.User.MaNV;
                //objYC.MaTN = this.MaTN;
                glkNhomCongViec.EditValue = 1;
                lookDoUuTien.ItemIndex = 2;
                txtMaYC.Text = getNewMaYC();
                dateNgayYC.DateTime = Common.GetDateTimeSystem();
                db.tnycYeuCaus.InsertOnSubmit(objYC);
            }
            lkLoaiTinNhan.Properties.DataSource = db.tnycLoaiSms;
        }

        private void ResetColorLayout()
        {
            this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = false;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = false;
            this.layoutControlItem10.AppearanceItemCaption.Options.UseForeColor = false;
            this.layoutControlItem11.AppearanceItemCaption.Options.UseForeColor = false;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = false;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResetColorLayout();

            if (txtMaYC.Text.Trim().Length == 0)
            {
                DialogBox.Error("Mã yêu cầu không được để trống");
                this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
                txtMaYC.Focus();
                return;
            }

            if (dateNgayYC.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày yêu cầu");
                this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
                dateNgayYC.Focus();
                return;
            }

            if (dateNgayHetHanBH.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày hết hạn");
                this.layoutControlItem30.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem30.AppearanceItemCaption.Options.UseForeColor = true;
                dateNgayHetHanBH.Focus();
                return;
            }

            if (txtTieuDe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tiêu đề");
                this.layoutControlItem10.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem10.AppearanceItemCaption.Options.UseForeColor = true;
                txtTieuDe.Focus();
                return;
            }
            if (txtNoiDung.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                this.layoutControlItem11.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem11.AppearanceItemCaption.Options.UseForeColor = true;
                txtNoiDung.Focus();
                return;
            }

            if (lookDoUuTien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn độ ưu tiên");
                this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
                return;
            }

            if (lookBoPhan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn độ bộ phận nhận yêu cầu");
                this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
                return;
            }



            objYC.MaYC = txtMaYC.Text;
            objYC.NgayYC = dateNgayYC.DateTime;
            if (dateNgayBanGiao.Text.Trim().Length != 0)
            {
                objYC.NgayBanGiaoCH = dateNgayBanGiao.DateTime;
            }

            objYC.NgayHetHanDauTien = objYC.NgayHetHanCuoiCung = objYC.NgayHetHanBH = dateNgayHetHanBH.DateTime;

            objYC.MaTT = (int?)lookTrangThai.EditValue;
            objYC.MaBP = (int?)lookBoPhan.EditValue;
            
            var maMB= (int?)searchLookMatBang.EditValue;
            var kh =
                (from p in db.mbMatBangs where p.MaMB == (int?) searchLookMatBang.EditValue select new {p.MaKH, p.MaTN})
                    .FirstOrDefault();
            if (kh != null) objYC.MaKH = kh.MaKH;
            byte? buildingId = kh != null ? kh.MaTN : Library.Common.User.MaTN;

            objYC.MaTN = buildingId;
            objYC.MaMB = (int?)searchLookMatBang.EditValue;
            objYC.TieuDe = txtTieuDe.Text;
            objYC.NoiDung = txtNoiDung.Text;
            objYC.MaDoUuTien = (int?)lookDoUuTien.EditValue;
            objYC.MaNguonDen = (int?)lookNguonDen.EditValue;
            objYC.NguoiGui = txtNguoiGui.Text.Trim();
            objYC.MaLoaiYeuCau = (int?)lkYeuCau.EditValue;
            objYC.TGKhachHen = (DateTime?)dateTGKhachHen.EditValue;
            objYC.SoDienThoai = txtSDTNguoiGui.Text;
            objYC.NgayCN = db.GetSystemDate();
            objYC.GroupProcessId =(int?) glkNhomCongViec.EditValue;
            //save LS
            var objLS = new tnycLichSuCapNhat();
            objYC.tnycLichSuCapNhats.Add(objLS);
            objLS.MaNV = Common.User.MaNV;
            objLS.NgayCN = db.GetSystemDate();
            objLS.MaTT = (int?)lookTrangThai.EditValue;
            if (checkHoiKy.Checked)
            {
                objLS.NoiDung = txtNoiDung.Text;
                objLS.BienPhapXuLy = txtBienPhap.Text;
                objLS.NguyenNhan = txtNguyenNhan.Text;
                objLS.NhanVien = objYC.NVXuLy=  lkNhanVien.EditValue.ToString();
            }
            else
                objLS.NoiDung = objYC.ID != 0 ? "[Cập nhật yêu cầu]" : "[Thêm mới yêu cầu]";
        save:
            try
            {
                var objEmailSetup = db.email_Setups.FirstOrDefault(_ =>
                    _.BuildingId == buildingId && _.RequestTyleId == (int?) lkYeuCau.EditValue);
                if (objYC.ID == 0)
                {
                    var ltTB = db.tnycCaiDats.Where(p => p.MaTN == buildingId & p.MaPB == objYC.MaBP & p.IsRemind == true).Select(p => new { p.MaNV }).Distinct().ToList();
                    foreach (var i in ltTB)
                    {
                        tnycThongBao objTB = new tnycThongBao();
                        objTB.NgayBD = db.GetSystemDate();
                        objTB.NgayKT = db.GetSystemDate().AddDays(1);
                        objTB.MaNV = i.MaNV;
                        objTB.IsNhac = true;
                        objTB.IsRepeat = true;
                        objTB.TimeID = (byte)1;
                        objTB.TimeID2 = (byte)1;                        
                        var obj = db.Times.Single(p => p.TimeID == (byte?)1);
                        objTB.NgayNhac = objTB.NgayBD.Value.AddMinutes(-obj.Minutes.GetValueOrDefault());
                        objTB.tnycYeuCau = objYC;
                        db.tnycThongBaos.InsertOnSubmit(objTB);

                        // send email, send email cho nhân viên khi kế hoạch bắt đầu, và gửi email cho khách hàng khi kế hoạch kết thúc
                        // kiểm tra đây có phải là yêu cầu bắt đầu hay k
                        // kiểm tra loại yêu cầu có được gửi email cho nhân viên hay k
                        // objYc.id ==0, thì nó đã là mới rồi, khỏi kiểm tra trạng thái yêu cầu bắt đầu
                        // kiểm tra loại yêu cầu có được gửi cho nhân viên hay k

                        if (objEmailSetup == null) continue;
                        if (objEmailSetup.SendEmployee != true) continue;
                        var emailNv = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == i.MaNV);
                        if (emailNv == null) continue;
                        if (emailNv.Email == "") continue;
                        if (kh == null) return;
                        var report= (from rp in db.rptReports
                            join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                            where tn.MaTN ==buildingId & rp.GroupID == 10
                            orderby rp.Rank
                            select new { rp.ID, rp.Name }).ToList();
                        foreach (var item in report)
                        {
                            var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                            if (objForm != null)
                            {
                                SendMail(objForm.Content, txtTieuDe.Text, txtNoiDung.Text,emailNv.Email,(int) kh.MaKH);
                            }
                        }
                    }
                }
                // nếu id !=null, kiểm tra đóng yêu cầu, nếu đúng đóng yêu cầu, thì gửi cho khách hàng nếu requesttyle thõa mãn
                // kiểm tra đây có phải yêu cầu óng cuối cùng hay k
                // kiểm tra loại yêu cầu có được gửi emaill cho khách hàng hay k
                // send email
                if ((int?) lookTrangThai.EditValue == 5)
                {
                    if (objEmailSetup != null)
                    {
                        if (objEmailSetup.SendCustomer == true)
                        {
                            if (kh != null)
                            {
                                var objKh = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == kh.MaKH);
                                if (objKh == null) return;
                                var report = (from rp in db.rptReports
                                    join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                    where tn.MaTN == buildingId & rp.GroupID == 11
                                    orderby rp.Rank
                                    select new { rp.ID, rp.Name }).ToList();
                                foreach (var item in report)
                                {
                                    var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == item.ID);
                                    if (objForm != null)
                                    {
                                        SendMail(objForm.Content, txtTieuDe.Text, txtNoiDung.Text,objKh.EmailKH, (int)kh.MaKH);
                                    }
                                }
                            }
                        }
                    }
                }
                db.SubmitChanges();

                // yêu cầu mới thì mới gửi cho nhân viên
                if ((int?)lkLoaiTinNhan.EditValue == 1)
                {
                    int result = 0;
                    if (_isSendSms == true & objYC.MaTT == 2)
                    {
                        result = Building.SMS.Class.DichVuYeuCau.SendSmsStaf(txtTieuDe.Text, txtNoiDung.Text, (byte)MaTN, (int)lookBoPhan.EditValue);
                    }

                    if (objYC.MaTT == 3 & objYC.MaKH != null)
                    {
                        // nếu hoàn thành, gửi cho khách hàng
                        result = Building.SMS.Class.DichVuYeuCau.SendSmsCustomer(txtTieuDe.Text, txtNoiDung.Text, (int)objYC.MaKH, (byte)MaTN);
                    }
                }
                else if((int?)lkLoaiTinNhan.EditValue == 2)
                {
                    var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
                    if (objConfig == null)
                    {
                        DialogBox.Error("Dự án này chưa được cấu hình sms zalo!");
                        return;
                    }
                    if (_isSendSms == true & objYC.MaTT == 2)
                    {
                        var send = Building.SMS.Class.DichVuYeuCau.SendSmsStafZalo(txtTieuDe.Text, txtNoiDung.Text, (byte)MaTN, (int)lookBoPhan.EditValue, objConfig.LinkToken, null , "");
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                txtMaYC.Text = getNewMaYC();
                goto save;
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
                objMail.Subject = txtTieuDe.Text;
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

        void GuiMailList()
        {
            using (var db = new MasterDataContext())
            {
                var objCaiDat = db.tnCaiDatMails.Where(p => SqlMethods.DateDiffDay(objYC.NgayYC, p.TuNgay) >= 0 &
                        SqlMethods.DateDiffDay(p.TuNgay, objYC.NgayYC) >= 0 & p.MaPB == objYC.MaBP).ToList();
                foreach (var cd in objCaiDat)
                {
                    string[] dsmnv = cd.DSMaNhanVien.Split(':');
                    var listNhanvien = db.tnNhanViens.Where(p => dsmnv.Contains(p.MaNV.ToString())).Select(p => new { p.Email }).ToList();
                    foreach (var nv in listNhanvien)
                    {
                        MailProviderCls objMail = new MailProviderCls();
                        objMail.MailFrom = cd.SendMailAccount.DiaChi;
                        var objMailForm = new MailAddress(cd.SendMailAccount.DiaChi, "Indochina IPH");
                        objMail.MailAddressFrom = objMailForm;
                        var objMailTo = new MailAddress(nv.Email);
                        objMail.MailAddressTo = objMailTo;
                        objMail.SmtpServer = cd.SendMailAccount.Server;
                        objMail.EnableSsl = true;
                        objMail.PassWord =Commoncls.DecodeString(cd.SendMailAccount.Password);
                        objMail.Subject = objYC.TieuDe;
                        objMail.Content = objYC.NoiDung;
                        try
                        {
                            objMail.SendMailV2();
                        }
                        catch { }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void searchLookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //var objMB = db.mbMatBangs.Where(p => p.MaMB == (int?)searchLookMatBang.EditValue).Select(p => new { TenKH = p.tnKhachHang.IsCaNhan.GetValueOrDefault() ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen, p.tnKhachHang.DienThoaiKH }).FirstOrDefault();
                var objMB = (from p in db.mbMatBangs
                         join kh in db.tnKhachHangs on p.MaKHF1 equals kh.MaKH into tblKhachHang
                         from kh in tblKhachHang.DefaultIfEmpty()
                         join csh in db.tnKhachHangs on p.MaKHF1 equals csh.MaKH into tblChuSoHuu
                         from csh in tblChuSoHuu.DefaultIfEmpty()
                         join tt in db.mbTrangThais on p.MaTT equals tt.MaTT into trangthai
                         from tt in trangthai.DefaultIfEmpty()
                         where p.MaMB == (int?)searchLookMatBang.EditValue
                         select new
                         {
                             TenKH = p.tnKhachHang.IsCaNhan.GetValueOrDefault() ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen,
                             DienThoaiKH = p.tnKhachHang.DienThoaiKH,
                             TenCSH = csh.IsCaNhan.GetValueOrDefault() ? (csh.HoKH + " " + csh.TenKH) : csh.CtyTen,
                             DienThoaiCSH = csh.DienThoaiKH,
                             p.NgayBanGiao,
                             tt.TenTT,
                         }).FirstOrDefault();
                txtTrangThai.EditValue = objMB.TenTT;
                txtChuMatBang.EditValue = objMB.TenCSH;
                txtDienThoai.EditValue = objMB.DienThoaiCSH;
                txtNguoiThue.EditValue = objMB.TenKH;
                txtSDTNguoiThue.EditValue = objMB.DienThoaiKH;
                dateNgayDen.EditValue = objMB.NgayBanGiao;
                dateNgayBanGiao.EditValue = objMB.NgayBanGiao;
                lookCuDan.Properties.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == (int)searchLookMatBang.EditValue).Select(p => new { p.ID, p.HoTenNK, SoCMND = p.CMND, p.DienThoai }).ToList();
            }
            catch { }
        }

        private void lookCuDan_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiGui.Text = lookCuDan.Text;
        }

        private void searchLookMatBang_SizeChanged(object sender, EventArgs e)
        {
            searchLookMatBang.Properties.PopupFormSize = new Size(searchLookMatBang.Size.Width, 0);
        }

        private void lookCuDan_SizeChanged(object sender, EventArgs e)
        {
            lookCuDan.Properties.PopupFormSize = new Size(lookCuDan.Size.Width, 0);
        }

        void enableHoiKy(bool check)
        {
            txtHienTrang.Enabled = check;
            txtBienPhap.Enabled = check;
            txtNguyenNhan.Enabled = check;
            lkNhanVien.Enabled = check;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            enableHoiKy(checkHoiKy.Checked);
        }

        private void itemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtMaYC.Text = getNewMaYC();
        }

        private void lookBoPhan_EditValueChanged(object sender, EventArgs e)
        {
            // chuẩn bị gửi sms, kiểm tra coi có ai trong bộ phận này được nhận sms hay không
            var item = sender as LookUpEdit;
            if (item == null) return;

            _isSendSms = true;
        }

        public class ltLoaiTin
        {
            public int? MaLoai { get; set; }
            public string TenLoai { get; set; }
        }
    }
}