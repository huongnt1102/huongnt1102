using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;


namespace Library.Controls
{
    public class ctlQuyDanhEdit: LookEdit
    {
        public ctlQuyDanhEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlQuyDanhEdit_ButtonClick);

            this.Properties.DisplayMember = "TenQD";
            this.Properties.ValueMember = "MaQD";
            this.Properties.ShowHeader = false;
        }

        void ctlQuyDanhEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAccess(10) != 1)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }

            //        using (var frm = new Library.Other.frmQuyDanh())
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
            this.Properties.Columns.Add(new LookUpColumnInfo("TenQD"));

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.QuyDanhs.OrderBy(p => p.STT).ToList();
            }
        }
    }
}
