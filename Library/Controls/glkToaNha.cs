using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkToaNha : Library.Controls.GridLookup
    {
        public void LoadData()
        {
            this.InIt();
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.tnToaNhas;
            }
        }

        public glkToaNha()
        {
            InitializeComponent();

            this.displayMember = "TenTN";
            this.valueMember = "MaTN";
            this.cols = new ColItem[]
                {
                    new ColItem() { Caption = "Dự án", FieldName = "TenTN"},
                };
        }
    }
}
