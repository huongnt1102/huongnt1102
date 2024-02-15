using LandSoftBuilding.Receivables.GiayBao;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Building.SMSZalo.Gui
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
        public List<smsZalo> ListMaKHs { get; set; }
        private List<int> ListMaLDVs;
        #region Field
        Thread thread;
        delegate void UpdateControls();
        #endregion

        void updateTongSo(int count)
        {
            lblTongSo.Text = "Tổng số: " + count;
        }

        void updateStatus(int max)
        {
            //progressBarControl1.Properties.Step = 1;
            //progressBarControl1.Properties.PercentView = true;
            //progressBarControl1.Properties.Maximum = max;
            //progressBarControl1.Properties.Minimum = 0;

        }

        void updateprogess(int tong, int tc, int tb)
        {
            var ht = tc + tb;
            var i = (int)(((decimal)ht / (decimal)tong) * 100);
            progressBarControl1.Position = i;
        }
        void updateStatus()
        {
            progressBarControl1.PerformStep();
        }

        void updateDaGui(int count)
        {
            //lblDaGui.Text = "Đã gửi: " + count;
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
            //notifyIcon1.Text = string.Format("Tiến trình gửi mail: thành công ({0}), thất bại ({1})", thanhcong, thatbai);
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

            int thanhCong = 0, thatBai = 0;
            var daGui = 0;
            try
            {
                var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
                if (objConfig == null)
                {
                    DialogBox.Error("Dự án này chưa được cấu hình sms zalo!");
                    return;
                }
                var Tong = ListMaKHs.Count();
                lblTongSo.BeginInvoke(new UpdateControls(() => updateTongSo(Tong)));
                foreach (var i in ListMaKHs)
                {
                    var status = 1;
                    var objKh = db.tnKhachHangs.SingleOrDefault(p => p.MaKH == i.MaKH);
                    try
                    {
                        if (objKh == null) continue;
                        string path = "";
                        #region File dinh kem
                        if (lkMauIn.EditValue != null)
                        {
                            path = System.IO.Path.Combine(getHomePath(), "Downloads\\ThongBaoPhi.pdf");
                            ListMaLDVs = GetMaLDV();
                            var streamPdt = new System.IO.MemoryStream();
                            DevExpress.XtraReports.UI.XtraReport rpt = null;
                            var reportId = (int)lkMauIn.EditValue;
                            switch (reportId)
                            {
                                // thông báo tiền điện 3 pha
                                case 7:
                                    {
                                        rpt = new rptTienDien3Pha(MaTN, objKh.MaKH, Month, Year);
                                        rpt.ExportToPdf(path);
                                    }

                                    break;
                                // TTC
                                case 89:
                                    {
                                        rpt = new rptGiayBaoTTC(MaTN, Month, Year,objKh.MaKH, i.MaMB, ListMaLDVs, (int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                // thông báo nhắc nợ lần 1
                                case 90:
                                    {
                                        rpt = new rptThongBaoNhacNo2(MaTN, Month, Year, objKh.MaKH, i.MaMB,ListMaLDVs, (int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                // thông báo nhắc nợ lần 2
                                case 91:
                                    {
                                        rpt = new rptThongBaoNhacNo1(MaTN, Month, Year, objKh.MaKH, i.MaMB, ListMaLDVs, (int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                case 92:
                                    {
                                        rpt = new rptGiayBaoImperiaNhacNo(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,(int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                case 93:
                                    {
                                        rpt = new rptGiayBaoImperiaPQL(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,(int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                case 94:
                                    {
                                        rpt = new rptGiayBaoImperiaNuoc(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,(int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                case 95:
                                    {
                                        rpt = new rptGiayBaoImperiaXe(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,(int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                case 96:
                                    {
                                        rpt = new rptGiayBaoImperia(MaTN, Month, Year, objKh.MaKH, ListMaLDVs,(int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                // thông báo thu phí vận hành mẫu 4
                                case 98:
                                    {
                                        rpt = new RptThongBaoThuPhiQuanLyVanHanh04(MaTN, objKh.MaKH, Month, Year,(int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                // thông báo thu phí vận hành mẫu 5
                                case 99:
                                    {
                                        rpt = new RptThongBaoThuPhiQuanLyVanHanh05(MaTN, objKh.MaKH, Month, Year, (int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                                case 100:
                                    {
                                        rpt = new rptGiayBaoTTC_XemLai(this.MaTN, Month, Year, objKh.MaKH, i.MaMB, ListMaLDVs, (int)lkTaiKhoan.EditValue);
                                        rpt.ExportToPdf(path);
                                    }
                                    break;
                            }
                        }
                        #endregion

                        #region Gui sms
                        var lt = ListMaKHs.FirstOrDefault(o => o.MaKH == objKh.MaKH);
                        string noidung = Building.SMSZalo.ReplaceField.ReplaceCongNo(lt, txtNoiDung.Text);
                        var send = Building.SMSZalo.ZaloSend.smsZALO(MaTN,objConfig.LinkToken, noidung, objKh.MaKH, objKh.smsZalo, path);
                        var tc = send.Item1;
                        var tb = send.Item2;
                        var mes = send.Item3;
                        #endregion

                        thanhCong = thanhCong + tc;
                        thatBai = thatBai + tb;
                        lblThanhCong.BeginInvoke(new UpdateControls(() => updateThanhCong(thanhCong)));
                        lblThatBai.BeginInvoke(new UpdateControls(() => updateThatBai(thatBai)));
                        progressBarControl1.BeginInvoke(new UpdateControls(() => updateprogess(Tong, thanhCong, thatBai)));
                    }
                    catch(Exception ex)
                    {
                        thatBai++;
                        lblThatBai.BeginInvoke(new UpdateControls(() => updateThatBai(thatBai)));
                    }
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
            }
        }

        public static string getHomePath()
        {
            // Not in .NET 2.0
            // System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                return System.Environment.GetEnvironmentVariable("HOME");

            return System.Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        }
        void start()
        {
            try
            {
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
                thread.Abort();
            }
            catch
            {
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            #region Rang buoc
            if (grlMauGuiSms.EditValue == null)
            {
                grlMauGuiSms.Focus();
                DialogBox.Alert("Vui lòng chọn mẫu gửi");
                return;
            }
            if (lkMauIn.EditValue != null && lkTaiKhoan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tài khoản ngân hàng");
                lkTaiKhoan.Focus();
                return;
            }
            #endregion

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
                grlMauGuiSms.Properties.DataSource = db.web_ZaloTemplates.Where(o => o.MaTN == MaTN).ToList();

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
                //lkMauIn.ItemIndex = 0;
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
            //var frm = new Marketing.Mail.Templates.frmManager();
            //frm.IsCate = false;
            //frm.ShowDialog();
            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    htmlContent.InnerHtml = frm.Content;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            //Marketing.Mail.frmFields frm = new Marketing.Mail.frmFields();
            //frm.txtContent = htmlContent;
            //frm.Show(this);
        }

        private void grlMauGuiSms_EditValueChanged(object sender, EventArgs e)
        {
            var MauID = Convert.ToInt32(grlMauGuiSms.EditValue);
            if (MauID == null || MauID == 0)
            {
                DialogBox.Error("Vui lòng chọn mẫu in!");
                grlMauGuiSms.Focus();
                return;
            }
            var db = new MasterDataContext();
            var obj = db.web_ZaloTemplates.FirstOrDefault(o => o.Id == MauID);
            txtNoiDung.Text = "";
            txtNoiDung.Text = obj.Content;
        }
        public class smsZalo
        {
            public int MaKH { get; set; }
            public int MaMB { get; set; }
            public int Thang { get; set; }
            public int Nam { get; set; }
            public string KyHieu { get; set; }
            public string MaSoMB { get; set; }
            public decimal? NoDauKy { get; set; }
            public decimal? PhatSinh { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? KhauTru { get; set; }
            public decimal? ConNo { get; set; }
            public decimal? ThuTruoc { get; set; }
            public decimal? NoCuoi { get; set; }
        }
    }
}