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

namespace LandSoftBuilding.Marketing.Mail.History
{
    public partial class frmSendMail : DevExpress.XtraEditors.XtraForm
    {
        public frmSendMail()
        {
            InitializeComponent();
        }

        public byte? MaTN { get; set; }
        public int? SendID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<int> MaLDVs { get; set; }
        public int? MaTKNH { get; set; }

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

        private void frmSendMail_Load(object sender, EventArgs e)
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
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lkEmailSend.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Email gửi]. Xin cám ơn!");
                lkEmailSend.Focus();
                return;
            }            

            if (lkTaiKhoan.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Tài khoản ngân hàng]. Xin cám ơn!");
                lkTaiKhoan.Focus();
                return;
            }

            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tiêu đề]. Xin cám ơn!");
                txtTieuDe.Focus();
                return;
            }

            if (txtNoiDung.InnerText.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Nội dung]. Xin cám ơn!");
                txtNoiDung.Focus();
                return;
            }           

            SendID = (int?)lkEmailSend.EditValue;
            Subject = txtTieuDe.Text;
            Content = txtNoiDung.InnerText;
            MaLDVs = GetMaLDV();
            MaTKNH = (int?)lkTaiKhoan.EditValue;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = txtNoiDung;
            frm.Show(this);
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {

        }
    }
}