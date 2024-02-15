using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace TaiSan.KhoHang
{
    public partial class frmEditTrangThai : DevExpress.XtraEditors.XtraForm
    {
        public Kho objkho;
        public frmEditTrangThai()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEditTrangThai_Load(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                lookTrangThai.Properties.DataSource = db.tsTrangThais;
            }
            if (objkho !=null)
            {
                txtDonGia.Text = string.Format("{0:#,0.#} VNĐ", objkho.DonGia);
                txtNgayNhap.Text = objkho.NgayNhap.ToString();
                txtSoLuong.Text = objkho.SoLuong.ToString();
                txtTaiSan.Text = objkho.tsLoaiTaiSan.TenLTS;
                lookTrangThai.EditValue = objkho.MaTT;
            }
            
        }

        private void btnCanCel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái hoặc ấn nút Hủy nếu không muốn thay đổi trạng thái");
                return;
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                if (objkho != null)
                {
                    var objkhoNew = db.Khos.Single(p => p.ID == objkho.ID);
                    objkhoNew.MaTT = (int)lookTrangThai.EditValue;
                }
                try
                {
                    db.SubmitChanges();
                    DialogBox.Alert("Lưu thành công");
                }
                catch
                {
                    DialogBox.Error("Có lỗi, có thể đường truyền không ổn định. Vui lòng thử lại");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}