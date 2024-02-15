using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Library;

namespace Library.Controls
{
    public class ctlQuyMoEdit: LookEdit
    {
        public ctlQuyMoEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(NhomKHEdit_ButtonClick);

            this.Properties.DisplayMember = "TenQM";
            this.Properties.ValueMember = "ID";
            this.Properties.ShowHeader = false;
        }

        void NhomKHEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAccess(165) != 1)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }

            //        using (var frm = new Customer.frmQuyMo())
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
            this.Properties.Columns.Add(new LookUpColumnInfo("TenQM"));

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.QuyMoCongTies.OrderBy(p => p.STT).ToList();
            }
        }
    }
}
