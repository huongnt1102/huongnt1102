using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MSDN.Html.Editor;
using System.Linq;
using Library;
using EmailAmazon.API;

namespace EmailAmazon.Sending
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public byte? MaTN { get; set; }
        public long ID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<int> MaLDVs { get; set; }
        public int? MaTKNH { get; set; }

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            this.ccbNhomKH.Properties.DataSource = (object)MailCommon.cmd.GetNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau);
            this.lkThuongHieu.Properties.DataSource = (object)MailCommon.cmd.GetThuongHieu(MailCommon.MaHD, MailCommon.MatKhau);
            if (this.ID != 0L)
            {
                ChienDich chienDich = MailCommon.cmd.DetailChienDich(MailCommon.MaHD, MailCommon.MatKhau, this.ID);
                this.txtTitle.EditValue = (object)chienDich.TieuDe;
                this.lkThuongHieu.EditValue = (object)chienDich.TenTH;
                this.ckbActive.EditValue = (object)chienDich.KichHoat;
                this.dateSend.EditValue = (object)chienDich.NgayGui;
                this.htmlContent.InnerHtml = chienDich.NoiDung;
                ArrayOfInt maNkHbyMaCd = MailCommon.cmd.GetMaNKHbyMaCD(MailCommon.MaHD, MailCommon.MatKhau, chienDich.ID);
                string str = "";
                foreach (int? nullable in (List<int?>)maNkHbyMaCd)
                    str = str + (object)nullable + ",";
                this.ccbNhomKH.SetEditValue((object)str.Trim(','));
            }
            else
            {
                this.ckbActive.Checked = true;
                this.dateSend.DateTime = DateTime.Now;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtTitle.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên việc gửi");
                this.txtTitle.Focus();
            }
            else if (this.lkThuongHieu.Text == "")
            {
                DialogBox.Error("Vui lòng chọn thương hiệu");
                this.ccbNhomKH.Focus();
            }
            else if (this.ccbNhomKH.Text == "")
            {
                DialogBox.Error("Vui lòng chọn danh sách nhận");
                this.ccbNhomKH.Focus();
            }
            else if (this.dateSend.Text == "")
            {
                DialogBox.Error("Vui lòng nhập thời điểm gửi");
                this.dateSend.Focus();
            }
            else if (this.htmlContent.InnerHtml == null || this.htmlContent.InnerHtml.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                this.htmlContent.Focus();
            }
            else
            {
                deChienDich ChienDich = new deChienDich();
                ChienDich.TieuDe = this.txtTitle.Text;
                ChienDich.TenTH = this.lkThuongHieu.Text;
                ChienDich.Email = this.lkThuongHieu.GetColumnValue("Email").ToString();
                ChienDich.KichHoat = new bool?(this.ckbActive.Checked);
                ChienDich.NgayGui = new DateTime?(this.dateSend.DateTime);
                ChienDich.NoiDung = this.htmlContent.InnerHtml;
                ChienDich.NgaySua = new DateTime?(DateTime.Now);
                if (this.ID == 0L)
                {
                    ChienDich.NgayNhap = new DateTime?(DateTime.Now);
                    ChienDich.NguoiNhap = Common.User.HoTenNV;
                }
                else
                {
                    ChienDich.ID = this.ID;
                    ChienDich.NguoiSua = Common.User.HoTenNV;
                }
                ChienDich.decdNhomKHs = new ArrayOfDecdNhomKH();
                string str = this.ccbNhomKH.EditValue.ToString();
                char[] chArray = new char[1]
                {
                  ','
                };
                foreach (string s in str.Split(chArray))
                    ChienDich.decdNhomKHs.Add(new decdNhomKH()
                    {
                        MaNKH = new int?(int.Parse(s))
                    });
                switch (MailCommon.cmd.EditChienDich(MailCommon.MaHD, MailCommon.MatKhau, ChienDich))
                {
                    case Result.DaSuDung:
                        DialogBox.Error("Chiến dịch đã được sử dụng, không thể cập nhật");
                        break;
                    case Result.EmailKhongHhopLe:
                        DialogBox.Error("Email không hợp lệ");
                        break;
                    case Result.ThanhCong:
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = htmlContent;
            frm.Show(this);
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            using (var frm = new Templates.frmManager())
            {
                frm.IsSelect = true;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    htmlContent.InnerHtml = frm.Content;
            }
        }
    }
}