using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkCoHoi : Library.Controls.GridLookup
    {
        public glkCoHoi()
        {
            InitializeComponent();
            this.displayMember = "TenHienThi";
            this.valueMember = "MaNC";
            this.cols = new ColItem[]
                {
                     new ColItem() { Caption = "Số cơ hội", FieldName = "SoNC"},
                    new ColItem() { Caption = "Tên cơ hội",  FieldName = "TenCH", Width = 200},
                    new ColItem() { Caption = "Ngày nhập",  FieldName = "NgayNhap"}
                };
        }

        public void LoadData(int? maKH)
        {
            this.InIt();
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.ncNhuCaus
                                               .Where(p => p.MaKH == maKH)
                                               .Select(p => new
                                                {
                                                    p.MaNC,
                                                    p.SoNC,
                                                    TenHienThi = String.Format("{0} - {1}", p.SoNC ?? "", p.TenCH ?? ""),
                                                    p.TenCH,
                                                    p.NgayNhap,
                                                }).ToList();
            }
        }
    }
}
