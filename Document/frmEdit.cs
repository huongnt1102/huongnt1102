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
using ftp = FTP;

namespace Document
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        public int? MaTL { get; set; }
        public int? LinkID { get; set; }
        public int? FormID { get; set; }

        MasterDataContext db = new MasterDataContext();
        docTaiLieu objDoc;
        public tnNhanVien objNV;

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookLoaiTaiLieu.Properties.DataSource = db.docLoaiTaiLieus.OrderBy(p => p.STT);
            if (this.MaTL != null)
            {
                objDoc = db.docTaiLieus.Single(p => p.MaTL == this.MaTL);
                txtKyHieu.EditValue = objDoc.KyHieu;
                txtTenTL.EditValue = objDoc.TenTL;
                lookLoaiTaiLieu.EditValue = objDoc.MaLTL;
                txtDienGiai.EditValue = objDoc.DienGiai;
            }
            else
            {
                string kyHieu = "";
                db.docTaiLieu_TaoKyHieu(ref kyHieu);
                txtKyHieu.Text = kyHieu;
            }
        }

        private void txtTenTL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new ftp.frmUploadFile();
            if (frm.SelectFile(false))
            {
                txtTenTL.Tag = frm.ClientPath;
                if (txtTenTL.Text.Trim() == "")
                    txtTenTL.Text = frm.FileName;
            }
            frm.Dispose();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtTenTL.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên tài liệu");
                txtTenTL.Focus();
                return;
            }

            if (this.MaTL == null)
            {
                objDoc = new docTaiLieu();
                objDoc.FormID = this.FormID;
                objDoc.LinkID = this.LinkID;
            }

            try
            {
                if (txtTenTL.Tag != null)
                {
                    var frm = new ftp.frmUploadFile();
                    frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
                    frm.ClientPath = txtTenTL.Tag.ToString();
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    objDoc.DuongDan = frm.FileName;
                }

                objDoc.KyHieu = txtKyHieu.Text;
                objDoc.TenTL = txtTenTL.Text;
                objDoc.MaLTL = (short?)lookLoaiTaiLieu.EditValue;
                objDoc.DienGiai = txtDienGiai.Text;
                objDoc.NgayTao = DateTime.Now;
                objDoc.MaNV = objNV.MaNV;

                if (this.MaTL == null)
                    db.docTaiLieus.InsertOnSubmit(objDoc);

                db.SubmitChanges();
            }
            catch
            {
                DialogBox.Error("Đã xảy ra lỗi, vui lòng kiểm tra lại.");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}