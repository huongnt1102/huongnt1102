using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkBaoGia : Library.Controls.GridLookup
    {
        public void LoadData(int? maKH)
        {
            this.InIt();
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = db.BaoGias
                                               .Where(p => p.MaKH == maKH)
                                               .Select(p => new
                                                {
                                                    p.ID,
                                                    p.SoBG,
                                                    p.NgayNhap,
                                                }).ToList();
            }
        }

        public glkBaoGia(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            this.valueMember = "ID";
            this.cols = new ColItem[]
                {
                      new ColItem() { Caption = "Số báo giá", FieldName = "SoBG"},
                    new ColItem() { Caption = "Ngày nhập",  FieldName = "NgayNhap"}
                };
        }
    }
}
