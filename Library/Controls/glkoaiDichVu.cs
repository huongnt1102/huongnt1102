using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkoaiDichVu : GridLookup
    {
        public void LoadData()
        {
            this.InIt();
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.dvLoaiDichVus
                                              .Select(p => new
                                              {
                                                  p.ID,
                                                  p.TenHienThi
                                              }).ToList();
            }
        }

        public glkoaiDichVu()
        {
            InitializeComponent();


            this.displayMember = "TenHienThi";
            this.valueMember = "ID";
            this.cols = new ColItem[]
                {
                    new ColItem() { Caption="Tên loại dv", FieldName= "TenHienThi" },
                };
        }
    }
}
