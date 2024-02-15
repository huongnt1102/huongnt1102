using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkLoaiBaoGia : Library.Controls.GridLookup
    {
        public void LoadData()
        {
            this.InIt();
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.bgLoaiBaoGias;
            }
        }

        public glkLoaiBaoGia()
        {
            InitializeComponent();

            this.displayMember = "Name";
            this.valueMember = "ID";
            this.cols = new ColItem[]
                {
                     new ColItem() { Caption = "Loại báo giá", FieldName = "Name"},
                };
        }
    }
}
