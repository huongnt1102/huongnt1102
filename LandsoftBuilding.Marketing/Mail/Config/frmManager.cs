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

namespace LandSoftBuilding.Marketing.Mail.Config
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                gcConfig.DataSource = (from p in db.mailConfigs
                                       join nv in db.tnNhanViens on p.StaffID equals nv.MaNV
                                       where p.MaTN == (byte)itemToaNha.EditValue
                                       select new
                                       {
                                           p.ID,
                                           p.Display,
                                           p.Email,
                                           p.EnableSsl,
                                           p.Server,
                                           p.Port,
                                           p.InServer,
                                           p.InSsl,
                                           p.InPort,
                                           p.isSynonym,
                                           p.Reply,
                                           p.SendMax,
                                           p.DateModify,
                                           HoTen = nv.HoTenNV
                                       }).ToList();
            }
            catch
            {
                gcConfig.DataSource = null;
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.MaTN = (byte?)itemToaNha.EditValue ?? Common.User.MaTN;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvConfig.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần sửa. Xin cám ơn!");
                return;
            }

            var frm = new frmEdit();
            frm.MailID = (int?)gvConfig.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvConfig.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    var obj = db.mailConfigs.Single(p => p.ID == (int?)gvConfig.GetRowCellValue(i, "ID"));
                    db.mailConfigs.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                gvConfig.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}