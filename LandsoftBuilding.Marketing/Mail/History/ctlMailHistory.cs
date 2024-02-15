using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandSoftBuilding.Marketing.Mail.History
{
    public partial class ctlMailHistory : DevExpress.XtraEditors.XtraUserControl
    {
        public int? MaKH { get; set; }

        public void MailHistory_Load()
        {
            if (this.MaKH == null)
            {
                gcMailHistory.DataSource = null;
                return;
            }

            var db = new MasterDataContext();
            try
            {
                gcMailHistory.DataSource = (from m in db.mailHistories
                                            join c in db.mailConfigs on m.MailID equals c.ID
                                            join n in db.tnNhanViens on m.StaffCreate equals n.MaNV
                                            join s in db.tnNhanViens on m.StaffModify equals s.MaNV into nv
                                            from s in nv.DefaultIfEmpty()
                                            orderby m.DateCreate descending
                                            where m.CusID == this.MaKH
                                            select new
                                            {
                                                m.ID,
                                                m.Subject,
                                                FromMail = c.Email,
                                                m.ToMail,
                                                m.CcMail,
                                                m.BccMail,
                                                m.Contents,
                                                Status = m.Status == 2 ? "Thất bại" : "Thành công",
                                                NameCreate = n.HoTenNV,
                                                m.DateCreate,
                                                NameModify = s.HoTenNV,
                                                m.DateModify
                                            }).ToList();
            }
            catch
            {
                gcMailHistory.DataSource = null;
            }
        }

        public ctlMailHistory()
        {
            InitializeComponent();

            this.Load += new EventHandler(ctlMailHistory_Load);
            grvMailHistory.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvMailHistory_CustomDrawRowIndicator);
        }

        void ctlMailHistory_Load(object sender, EventArgs e)
        {
        }

        void grvMailHistory_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        
        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var frm = new frmEdit();
            //frm.FormID = this.FormID;
            //frm.LinkID = this.LinkID;
            //frm.MaKH = this.MaKH;
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK) MailHistory_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvMailHistory.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn mail");
            //    return;
            //}
            //var frm = new frmEdit();
            //frm.ID = id;
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK) MailHistory_Load();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = grvMailHistory.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn mail");
                    return;
                }

                if (DialogBox.Question("Bạn có chắc chắn xóa dữ liệu không?") == DialogResult.No) return;

                var db = new MasterDataContext();
                foreach (var i in indexs)
                {
                    var objMail = db.mailHistories.Single(p => p.ID == (int)grvMailHistory.GetRowCellValue(i, "ID"));
                    db.mailHistories.DeleteOnSubmit(objMail);
                }

                db.SubmitChanges();
                MailHistory_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}
