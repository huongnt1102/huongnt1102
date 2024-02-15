using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;


namespace Library.Controls
{
    public class ctlNguonKHEdit: LookEdit
    {
        public ctlNguonKHEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlNguonKHEdit_ButtonClick);
                       
            this.Properties.DisplayMember = "TenNguon";
            this.Properties.ValueMember = "MaNguon";
            this.Properties.ShowHeader = false;
        }

        void ctlNguonKHEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAccess(12) != 1)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }

            //        using (var frm = new Customer.frmNguonKH())
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

        public void LoadData()
        {
            this.Properties.Columns.Clear();

            this.Properties.Columns.Add(new LookUpColumnInfo("TenNguon"));

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.ncNguonKHs.OrderBy(p => p.STT).ToList();
            }
        }
    }
}
