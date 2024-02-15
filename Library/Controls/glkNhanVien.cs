using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkNhanVien : GridLookup
    {
        public void LoadData(byte? MaTN)
        {
            this.InIt();

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.tnNhanViens.Where(p => p.IsLocked == false & (p.MaTN == MaTN | MaTN == null))
                                                           .Select(p => new
                                                           {
                                                               p.MaNV,
                                                               MaSo = p.MaSoNV,
                                                               HoTen = p.HoTenNV,
                                                               TenHienThi = String.Format("{0}-{1}", p.MaSoNV, p.HoTenNV),
                                                           }).OrderBy(p => p.MaSo).ToList();
            }
        }

        public glkNhanVien()
        {
            InitializeComponent();

            this.displayMember = "TenHienThi";
            this.valueMember = "MaNV";
            this.cols = new ColItem[]
                {
                     new ColItem() { Caption = "Mã số", FieldName = "MaSo"},
                    new ColItem() { Caption = "Họ tên",  FieldName = "HoTen", Width = 150},
                };
        }
    }
}
