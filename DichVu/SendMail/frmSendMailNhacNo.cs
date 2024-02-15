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
using System.IO;

namespace DichVu.SendMail
{
    public partial class frmSendMailNhacNo : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmSendMailNhacNo()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmSendMailNhacNo_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            dateTuNgay.DateTime = dateDenNgay.DateTime = DateTime.Now;
            lookMailFrom.Properties.DataSource = db.SendMailAccounts;

            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            var khTM = db.dvtmThanhToanThangMays
                    .Where(p => p.DaTT == false)
                    .Select(p => p.dvtmTheThangMay.MaKH);
            var khHD = db.thueCongNos
                .Where(p => p.ConNo > 0)
                .Select(p => p.thueHopDong.MaKH);
            var khDVK = 0;
            var khNuoc = db.dvdnNuocs
                .Where(p => p.DaTT == false)
                .Select(p => p.MaKH);
            var khDien = db.dvdnDiens
                .Where(p => p.DaTT == false)
                .Select(p => p.MaKH);

            var lstkhachhang = db.tnKhachHangs.Where(p => khTM.Contains(p.MaKH)
                    | khHD.Contains(p.MaKH)
                    | khNuoc.Contains(p.MaKH)).Distinct();

            List<SendMailNhacNo> lstm = new List<SendMailNhacNo>();
            foreach (var item in lstkhachhang)
            {
                //ReportMisc.TongHop.RptCongNoTheoKhachHang rpt = new ReportMisc.TongHop.RptCongNoTheoKhachHang(item, dateTuNgay.DateTime, dateDenNgay.DateTime);

                MemoryStream stream = new MemoryStream();
                //rpt.ExportToPdf(stream);
                stream.Position = 0;

                BinaryReader reader = new BinaryReader(stream);
                byte[] file = reader.ReadBytes((int)stream.Length);
                SendMailNhacNo mnn = new SendMailNhacNo()
                {
                    FileDinhKem = file,
                    MaKH = item.MaKH,
                    MaNV = objnhanvien.MaNV,
                    NoiDung = txtNoiDung.InnerHtml,
                    ThoiGianGui = db.GetSystemDate(),
                    TieuDe = txtTieuDe.Text.Trim(),
                    TrangThai = 3 //dang cho gui 
                };
                if (lookMailFrom.EditValue != null)
                {
                    mnn.MailFrom = (int)lookMailFrom.EditValue;
                }
                else
                {
                    var objfrom = db.SendMailAccounts.FirstOrDefault(p => p.DiaChi == Library.Properties.Settings.Default.YourMail);
                    if (objfrom == null)
                    {
                        DialogBox.Error("Chưa thiết lập địa chỉ Email gửi mặc định");  
                        return;
                    }
                    mnn.MailFrom = objfrom.ID;
                }
                lstm.Add(mnn);
                reader.Close();
                stream.Close();
            }

            db.SendMailNhacNos.InsertAllOnSubmit(lstm);

            
            try
            {
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
            }
            catch (Exception ex)
            {
                wait.Close();
                wait.Dispose();
                DialogBox.Error(ex.Message);
            }
            finally
            {
                Close();
            }
            
            #region download
            //byte[] FileBytesdl = null;
            //FileStream streamdl;
            //SaveFileDialog open = new SaveFileDialog();
            //open.Filter = "Tất cả|*.*";
            //if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    var wait = DialogBox.WaitingForm();
            //    try
            //    {
            //        FileBytesdl = (byte[])file.ToArray();
            //        streamdl = new FileStream(open.FileName + ".pdf", FileMode.Create);
            //        streamdl.Write(FileBytesdl, 0, FileBytesdl.Length);
            //        streamdl.Close();
            //        wait.Close();
            //        wait.Dispose();
            //        DialogBox.Alert("Tải file thành công");  
            //    }
            //    catch
            //    {
            //        wait.Close();
            //        wait.Dispose();
            //        DialogBox.Alert("Không tải file này về được, vui lòng thử lại sau!");  
            //        this.Close();
            //    }
            //}
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}