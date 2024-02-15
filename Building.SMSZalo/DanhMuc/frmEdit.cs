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

namespace Building.SMSZalo.DanhMuc
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int? ID;
        public byte MaTN;
        public frmEdit()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            //var lt = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN }).ToList();
            var lt = Library.Common.TowerList;
            glkDuAn.Properties.DataSource = lt;
            if (ID == null)
            {
               
            }
            else
            {
                var obj = db.web_Zalos.FirstOrDefault(o => o.Id == ID);
                glkDuAn.EditValue = obj.MaTN;
                txtTrang.Text = obj.ZaloName;
                txtDuongDan.Text = obj.Link;
                txtToKen.Text = obj.LinkToken;
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (glkDuAn.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                glkDuAn.Focus();
                return;
            }

            if (txtTrang.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên zalo page], xin cảm ơn.");
                txtTrang.Focus();
                return;
            }

            if (txtDuongDan.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Đường dẫn], xin cảm ơn.");
                txtDuongDan.Focus();
                return;
            }

            

            if (txtToKen.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Token], xin cảm ơn.");
                txtToKen.Focus();
                return;
            }

            try
            {
                if (ID == null)
                {
                    web_Zalo i = new web_Zalo();
                    i.MaTN = Convert.ToInt32(glkDuAn.EditValue);
                    i.ZaloName = txtTrang.Text;
                    i.Link = txtDuongDan.Text;
                    i.LinkToken = txtToKen.Text;
                    db.web_Zalos.InsertOnSubmit(i);
                }
                else
                {
                    var obj = db.web_Zalos.FirstOrDefault(o => o.Id == ID);
                    obj.MaTN = Convert.ToInt32(glkDuAn.EditValue);
                    obj.ZaloName = txtTrang.Text;
                    obj.Link = txtDuongDan.Text;
                    obj.LinkToken = txtToKen.Text;
                }
                

                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
            }
            catch
            {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }
    }
}