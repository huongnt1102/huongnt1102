using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkHopDongCDT : GridLookup
    {
        public glkHopDongCDT()
        {
            InitializeComponent();
            this.displayMember = "SoHD";
            this.valueMember = "ID";
            this.cols = new ColItem[]
                {
                     new ColItem() { Caption="Số hợp đồng", FieldName= "SoHD" },
                };
        }

        public void LoadData(byte? MaTN)
        {
            this.InIt();

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = (from hd in db.cdtHopDongs
                                              where hd.MaTN == MaTN
                                              orderby hd.SoHD descending
                                              select new
                                              {
                                                  ID = hd.ID,
                                                  SoHD = hd.SoHD
                                              }).ToList();
            }
        }
    }
}
