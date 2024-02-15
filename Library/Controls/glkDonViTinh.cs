using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkDonViTinh : Library.Controls.GridLookup
    {
        public void LoadData()
        {
            this.InIt();

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.DonViTinhs.Select( p=> new { p.ID, p.TenDVT } );
            }
        }

        public glkDonViTinh()
        {
            InitializeComponent();

            this.displayMember = "TenDVT";
            this.valueMember = "ID";
            this.cols = new ColItem[]
                {
                    new ColItem() { Caption = "Đơn vị tính", FieldName = "TenDVT"},
                };
        }
    }
}
