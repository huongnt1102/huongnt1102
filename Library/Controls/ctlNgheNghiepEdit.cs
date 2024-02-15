using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;


namespace Library.Controls
{
    public class ctlNgheNghiepEdit : LookEdit
    {
        public ctlNgheNghiepEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlNgheNghiepEdit_ButtonClick);

            this.Properties.DisplayMember = "TenNN";
            this.Properties.ValueMember = "MaNN";
            this.Properties.ShowHeader = false;
        }

        void ctlNgheNghiepEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAccess(13) != 1)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }

            //        using (var frm = new Customer.frmNgheNghiep())
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
            this.Properties.Columns.Add(new LookUpColumnInfo("TenNN"));

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.NgheNghieps.OrderBy(p => p.STT).ToList();
            }
        }
    }
}
