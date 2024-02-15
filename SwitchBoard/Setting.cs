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

namespace DIP.SwitchBoard
{
    public partial class Setting : DevExpress.XtraEditors.XtraForm
    {
        public Setting()
        {
            InitializeComponent();
        }

        void LoadSetting()
        {
            var db = new MasterDataContext();
            try
            {
                var objPBX = (from p in db.pbxSettings select p).First();
                txtServer.Text = objPBX.Server;
                txtLinkReport.Text = objPBX.LinkReport;
                txtUserName.Text = objPBX.UserName;
                txtPassword.Text = it.EncDec.Decrypt(objPBX.Password);
                txtLinkAudio.Text = objPBX.LinkAudio;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void LoadExtension()
        {
            var db = new MasterDataContext();
            try
            {
                gcExtension.DataSource = (from e in db.pbxExtensions
                                          join n in db.tnNhanViens on e.StaffID equals n.MaNV
                                          select new
                                          {
                                              e.ID,
                                              e.ExtenName,
                                              e.Port,
                                              e.Display,
                                              StaffName = n.HoTenNV
                                          })
                                          .AsEnumerable()
                                          .Select((e, index) => new
                                          {
                                              Number = index + 1,
                                              e.ID,
                                              e.ExtenName,
                                              e.Port,
                                              e.Display,
                                              e.StaffName
                                          })
                                          .ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            this.LoadSetting();
            this.LoadExtension();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region rang buoc
            if (txtServer.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập máy chủ");
                txtServer.Focus();
                return;
            }

            if (txtLinkReport.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập đường dẫn");
                txtLinkReport.Focus();
                return;
            }

            if (txtUserName.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tên đăng nhập");
                txtUserName.Focus();
                return;
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }

            if (txtLinkAudio.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập link audio");
                txtLinkAudio.Focus();
                return;
            }
            #endregion

            var db = new MasterDataContext();
            try
            {
                var objPBX = (from p in db.pbxSettings select p).First();
                 objPBX.Server = txtServer.Text;
                 objPBX.LinkReport = txtLinkReport.Text;
                 objPBX.UserName = txtUserName.Text;
                 objPBX.Password = it.EncDec.Encrypt(txtPassword.Text.Trim());
                 objPBX.LinkAudio = txtLinkAudio.Text;
                 db.SubmitChanges();

                 DialogBox.Alert("Dữ liệu đã được lưu");
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new EditExten())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadExtension();
                }
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvExtension.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn máy nhánh");
                return;
            }

            using (var frm = new EditExten())
            {
                frm.ID = id;   
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadExtension();
                }
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvExtension.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn máy nhánh");
                return;
            }

            if (DialogBox.Question("Bạn thật sự muốn xóa?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                var objExten = db.pbxExtensions.Single(p => p.ID == id);
                db.pbxExtensions.DeleteOnSubmit(objExten);
                db.SubmitChanges();

                this.LoadExtension();
            }
            catch { }
            finally
            {
                db.Dispose();
            }

        }
    }
}