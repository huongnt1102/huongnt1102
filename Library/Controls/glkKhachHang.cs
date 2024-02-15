using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library.Controls
{
    public partial class glkKhachHang : GridLookup
    {
        public void LoadData(bool? IsChinhThuc)
        {
            this.InIt();
            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = (from kh in db.tnKhachHangs
                                              where   (IsChinhThuc == true & (kh.IsChinhThuc.GetValueOrDefault() | kh.IsRoot.GetValueOrDefault()))
                                                    | (IsChinhThuc == false & kh.IsCSKH.GetValueOrDefault())
                                                    | (IsChinhThuc == null)
                                              orderby kh.KyHieu descending
                                              select new
                                              {
                                                  MaKH = kh.MaKH,
                                                  kh.KyHieu,
                                                  TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                  kh.DiaChi
                                              }).ToList();
            }
        }

        public glkKhachHang()
        {
            InitializeComponent();

            this.displayMember = "TenKH";
            this.valueMember = "MaKH";
            this.cols = new ColItem[]
                {
                    new ColItem() { Caption="Ký hiệu", FieldName= "KyHieu" },
                new ColItem() { Caption="Tên khách hàng", FieldName= "TenKH", Width= 250 },
                new ColItem() { Caption="Địa chỉ", FieldName= "DiaChi", Width=400 }
                };
        }
    }
}
