using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Library;

namespace Library.Controls
{
    public class ctlNhomKHEdit: LookEdit
    {
        public ctlNhomKHEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(NhomKHEdit_ButtonClick);

            this.Properties.DisplayMember = "TenNKH";
            this.Properties.ValueMember = "MaNKH";
            this.Properties.ShowHeader = false;
        }

        void NhomKHEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAccess(12) != 1)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }

            //        using (var frm = new Customer.frmNhomKH())
            //        {
            //            frm.ShowDialog();
            //            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //            {
            //                this.LoadData();
            //            }
            //        }
            //        break;
            //}
        }

        public void CreateColumns()
        {
            this.Properties.Columns.Clear();
            this.Properties.Columns.Add(new LookUpColumnInfo("TenNKH"));
        }

        public void LoadData()
        {
            CreateColumns();

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
            }
        }
    }
}
