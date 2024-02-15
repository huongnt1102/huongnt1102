using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Library;

namespace Library.Controls
{
    public class ctlNhomKHCheckListEdit: CheckLookEdit
    {
        public ctlNhomKHCheckListEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlNhomKHCheckListEdit_ButtonClick);

            this.Properties.DisplayMember = "TenNKH";
            this.Properties.ValueMember = "MaNKH";
        }

        void ctlNhomKHCheckListEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    //using (var frm = new Customer.frmNhomKH())
                    //{
                    //    frm.ShowDialog();
                    //    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    //    {
                    //        this.LoadData();
                    //    }
                    //}
                    break;
            }
        }

        public void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.khLoaiKhachHangs.Select(p => new { MaNKH = p.ID, TenNKH = p.TenLoaiKH }).ToList();
            }
        }
    }
}
