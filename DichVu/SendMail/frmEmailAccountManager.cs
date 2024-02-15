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

namespace DichVu.SendMail
{
    public partial class frmEmailAccountManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmEmailAccountManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmEmailAccountManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            gcEmail.DataSource = db.SendMailAccounts.Select(p => new
            {
                p.DiaChi,
                p.ID,
                p.Server,
                p.Username,
                p.Port
            });
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (DichVu.SendMail.frmEmailAccount frm = new DichVu.SendMail.frmEmailAccount())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvEmail.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn địa chỉ email");  
                return;
            }
            var objemail = db.SendMailAccounts.Single(p => p.ID == (int)grvEmail.GetFocusedRowCellValue("ID"));
            using (DichVu.SendMail.frmEmailAccount frm = new DichVu.SendMail.frmEmailAccount() { objacc = objemail })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvEmail.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn địa chỉ email");  
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                var objemail = db.SendMailAccounts.Single(p => p.ID == (int)grvEmail.GetFocusedRowCellValue("ID"));
                db.SendMailAccounts.DeleteOnSubmit(objemail);

                try
                {
                    db.SubmitChanges();
                    grvEmail.DeleteSelectedRows();
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
            }
        }

        private void btnSetDefault_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvEmail.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn địa chỉ email");  
                return;
            }
            try
            {
                var objemail = db.SendMailAccounts.Single(p => p.ID == (int)grvEmail.GetFocusedRowCellValue("ID"));
                Library.Properties.Settings.Default.MailServer = objemail.Server;
                Library.Properties.Settings.Default.MailPass = objemail.Password;
                Library.Properties.Settings.Default.YourMail = objemail.DiaChi;
                Library.Properties.Settings.Default.Save();
                DialogBox.Alert(String.Format("Thiết lập tải khoản <{0}> thành công", objemail.DiaChi));
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}