using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Linq;
using Library;

namespace DichVu.ChoThue
{
    public partial class frmUpload : DevExpress.XtraEditors.XtraForm
    {
        public thueHopDong objhd;
        public frmUpload()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void txtDuongDan_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDuongDan.Text = open.FileName;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtTenTaiLieu.Text.Trim().Length == 0)
            {
                DialogBox.Alert("Vui lòng nhập tên của tài liệu");
                return;
            }
            if (txtDuongDan.Text.Trim().Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn file tài liệu");
                return;
            }
            string filename = txtDuongDan.Text.Substring(Convert.ToInt32(txtDuongDan.Text.LastIndexOf("\\")) + 1, txtDuongDan.Text.Length - (Convert.ToInt32(txtDuongDan.Text.LastIndexOf("\\")) + 1));
            string filetype = txtDuongDan.Text.Substring(Convert.ToInt32(txtDuongDan.Text.LastIndexOf(".")) + 1, txtDuongDan.Text.Length - (Convert.ToInt32(txtDuongDan.Text.LastIndexOf(".")) + 1));
            FileStream fs = new FileStream(txtDuongDan.Text, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            byte[] file = reader.ReadBytes((int)fs.Length);
            reader.Close();
            fs.Close();

            using (MasterDataContext db = new MasterDataContext())
            {
                thueHopDong_DinhKem dkobj = new thueHopDong_DinhKem()
                {
                    DienGiai = txtDienGiai.Text.Trim(),
                    FileDinhKem = file,
                    TenFile = txtTenTaiLieu.Text.Trim(),
                    MaHD = objhd.MaHD,
                    FileName = filename
                };

                db.thueHopDong_DinhKems.InsertOnSubmit(dkobj);

                var wait = DialogBox.WaitingForm();
                try
                {
                    db.SubmitChanges();
                    wait.Close();
                    wait.Dispose();
                    DialogBox.Alert("Thêm tài liệu thành công");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                catch
                {
                    wait.Close();
                    wait.Dispose();
                    DialogBox.Alert("Thêm tài liệu thất bại");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}