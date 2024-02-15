using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Library;

namespace Library.Controls
{
    public class ctlLoaiTienEdit: LookUpEdit
    {
        public ctlLoaiTienEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlLoaiTienEdit_ButtonClick);

            this.Properties.DisplayMember = "TenLT";
            this.Properties.ValueMember = "ID";
            this.Properties.ShowLines = false;           
        }

        void ctlLoaiTienEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAccess(125) != 1)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }

            //        using (var frm = new Library.Other.frmLoaiTien())
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
            this.Properties.Columns.Add(new LookUpColumnInfo("KyHieuLT", 30, "Viết tắt"));
            this.Properties.Columns.Add(new LookUpColumnInfo("TenLT", 70, "Tên"));
            
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.LoaiTiens.ToList();
            }
        }
    }
}
