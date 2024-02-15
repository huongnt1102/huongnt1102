using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Library;

namespace Library.Controls
{
    public class ctlMailConfigEdit: LookEdit
    {
        public int MaNV { get; set; }

        public ctlMailConfigEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlMailConfigEdit_ButtonClick);
                        
            this.Properties.DisplayMember = "Display";
            this.Properties.ValueMember = "ID";
            this.Properties.ShowHeader = false;
        }

        void ctlMailConfigEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    //if (Library.Common.getAccess(89) != 1)
                    //{
                    //    DialogBox.Error("Bạn không có quyền thêm");
                    //    return;
                    //}

                    //using (var frm = new Marketing.Mail.frmConfig())
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
            this.Properties.Columns.Clear();
            this.Properties.Columns.Add(new LookUpColumnInfo("Display", 200, "Tên hiển thị"));
            this.Properties.Columns.Add(new LookUpColumnInfo("Email", 200, "Địa chỉ email"));

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                //this.Properties.DataSource = db.mailConfigs.Where(x => x.StaffID == MaNV | MaNV == null).OrderBy(p => p.Display).ToList();
                this.Properties.DataSource = db.mailConfigs.OrderBy(p => p.Display).ToList();
            }
        }
    }
}
