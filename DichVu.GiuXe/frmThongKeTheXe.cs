using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.GiuXe
{
    public partial class frmThongKeTheXe : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN;
        public DateTime TuNgay;
        public DateTime DenNgay;
        public frmThongKeTheXe()
        {
            InitializeComponent();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var objTK = APITheXe.ThongKeTheXeSuDung(MaTN, TuNgay, DenNgay);
            if (objTK != null)
            {
                List<TKTheXe> lst = new List<TKTheXe>();
                var item = new TKTheXe();
                item.Name = "1. Số thẻ vãng lai thời điểm hiện tại";
                item.Value = objTK.TotalTicketGuestUsingNow;
                lst.Add(item);
                var item1 = new TKTheXe();
                item1.Name = "2. Tổng vé tháng";
                item1.Value = objTK.TotalTicketMonth;
                lst.Add(item1);
                var item2 = new TKTheXe();
                item2.Name = "3. Tổng vé tháng đang sử dụng";
                item2.Value = objTK.TotalTicketMonthUsing;
                lst.Add(item2);
                var item3 = new TKTheXe();
                item3.Name = "4. Tổng vé tháng Ngưng sử dụng";
                item3.Value = objTK.TotalStopUsingTicketMonth;
                lst.Add(item3);
                var item4 = new TKTheXe();
                item4.Name = "5. Tổng Vé Tháng Trong danh sách đen";
                item4.Value = objTK.TotalBlackTicketMonth;
                lst.Add(item4);
                var item5 = new TKTheXe();
                item5.Name = "6.Tổng số lượt tạo mới vé tháng trong khoảng thời gian tìm kiếm";
                item5.Value = objTK.TotalCreateNew;
                lst.Add(item5);
                gridControl1.DataSource = lst;
            }
            else
            {
                gridControl1.DataSource = null;
            }
        }

        private void frmThongKeTheXe_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public class TKTheXe
        {
            public string Name { set; get; }
            public int? Value { set; get; }
        }
    }
}