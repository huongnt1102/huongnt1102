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

namespace KyThuat.ThamQuan
{
    public partial class frmUpload : DevExpress.XtraEditors.XtraForm
    {
        public tqThamQuan objtq;
        MasterDataContext db;
        public frmUpload()
        {
            InitializeComponent();
            db = new MasterDataContext();
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
            if (lookNCC.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn nhà cung cấp");
                return;
            }
            string filename = txtDuongDan.Text.Substring(Convert.ToInt32(txtDuongDan.Text.LastIndexOf("\\")) + 1, txtDuongDan.Text.Length - (Convert.ToInt32(txtDuongDan.Text.LastIndexOf("\\")) + 1));
            string filetype = txtDuongDan.Text.Substring(Convert.ToInt32(txtDuongDan.Text.LastIndexOf(".")) + 1, txtDuongDan.Text.Length - (Convert.ToInt32(txtDuongDan.Text.LastIndexOf(".")) + 1));
            FileStream fs = new FileStream(txtDuongDan.Text, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            byte[] file = reader.ReadBytes((int)fs.Length);
            reader.Close();
            fs.Close();

            tqBaoGia dkobj = new tqBaoGia()
            {
                DienGiai = txtDienGiai.Text.Trim(),
                FileDinhKem = file,
                TenFile = txtTenTaiLieu.Text.Trim(),
                MaTQ = objtq.MaTQ,
                MaNhaCungCap = (int)lookNCC.EditValue,
                GiaDuaRa = spinGiaDuaRa.Value,
                DaDuyet = false,
                FileName = filename
            };

            db.tqBaoGias.InsertOnSubmit(dkobj);

            var wait = DialogBox.WaitingForm();
            try
            {
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Thêm báo giá thành công");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch
            {
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Thêm báo giá thất bại");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmUpload_Load(object sender, EventArgs e)
        {
            lookNCC.Properties.DataSource = db.tnNhaCungCaps;
        }
    }
}