using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Threading;

namespace DichVu.PhongTap
{
    public partial class frmCreateFee : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        List<ItemData> listMB;
        List<ItemData> listData;
        public byte MaTN = 0;
        public frmCreateFee()
        {
            InitializeComponent();

            db = new MasterDataContext();
            db.CommandTimeout = 300;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateSyn.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Thời gian], xin cảm ơn.");
                dateSyn.Focus();
                return;
            }

            if (lookUpToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                lookUpToaNha.Focus();
                return;
            }
            
            var wait = DialogBox.WaitingForm();
            
            try
            {
                listMB = db.dvPhongTaps.Select(p => new ItemData() { ID = p.tnNhanKhau.mbMatBang.MaMB, CusID = p.tnNhanKhau.mbMatBang.MaKH.Value, Fee = p.dvPhongTap_Loai.PhiHangThang ?? 0, Fee2 = p.PhiLamThe ?? 0, DateFrom = p.NgayDK, DateTo = p.NgayHetHan, IsFullTerm = p.IsFullTerm.GetValueOrDefault() }).OrderBy(p => p.ID).ToList();

                int count = 1;

                listData = new List<ItemData>();
                foreach (var item in listMB)
                {
                    item.Fee = GetFee(item);
                    listData.Add(item);

                    wait.SetCaption(string.Format("Đã tạo {0:#,0.#}/{1:#,0.#} công nợ", count, listMB.Count));

                    count++;
                    Thread.Sleep(10);
                }

                wait.SetCaption("Cập nhật công nợ.");

                count = 1;
                foreach (var item in listData)
                {
                    db.cnLichSu_addTheXe(item.ID, item.CusID, dateSyn.DateTime, item.Fee);

                    wait.SetCaption(string.Format("Đã cập nhật {0:#,0.#}/{1:#,0.#} công nợ", count, listData.Count));

                    count++;
                    Thread.Sleep(50);
                }
            }
            catch { }
            finally { wait.Close();
                wait.Dispose();
            }
        }

        private decimal GetFee(ItemData item)
        {
            decimal Fee = 0;
            try
            {
                //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên (bao gồm phí làm thẻ)
                if (item.DateFrom.Value.Month == dateSyn.DateTime.Month & item.DateFrom.Value.Year == dateSyn.DateTime.Year)
                {
                    if (item.IsFullTerm)//Tính đủ tháng
                        Fee = item.Fee;
                    else//Tính theo ngày ở thực tế
                    {
                        //Ngày đăng ký = Ngày hết hạn: Tính ngày ở thực tế
                        if (item.DateFrom.Value.Month == item.DateTo.Value.Month & item.DateFrom.Value.Year == item.DateTo.Value.Year)
                        {
                            //4: Tính theo ngày ở thực tế
                            Fee = db.funcGetTheXeFeeV2(item.ID, item.Fee, item.DateFrom.Value, item.DateTo, 4, 1) ?? 0;
                        }
                        else//1: Tính phí bình thường
                            Fee = db.funcGetTheXeFeeV2(item.ID, item.Fee, item.DateFrom.Value, item.DateTo, 1, 1) ?? 0;
                    }
                }
                else
                {
                    if (item.IsFullTerm)//Tính đủ tháng
                        Fee = item.Fee;
                    else//Tính theo ngày ở thực tế
                    {
                        //Ngày tính phí = Ngày hết hạn (MM/yyyy)
                        if (item.DateTo.Value.Month == dateSyn.DateTime.Month & item.DateTo.Value.Year == dateSyn.DateTime.Year)
                        {
                            //Tính phí tháng cuối
                            Fee = db.funcGetTheXeFeeV2(item.ID, item.Fee, item.DateFrom.Value, item.DateTo, 3, 1) ?? 0;
                        }
                        else//Tính phí hàng tháng
                            Fee = item.Fee;
                    }
                }

                //Cộng phí làm thẻ
                Fee += item.Fee2;
            }
            catch { Fee = 0; }

            return Fee;
        }

        private void AddItem(ItemData item)
        {
            if (listData.Contains(item))
            {
                var obj = listData.Single(p => p.ID == item.ID);
                listData.Remove(obj);

                item.Fee += obj.Fee;
            }

            listData.Add(item);
        }

        private void frmCreateFee_Load(object sender, EventArgs e)
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.Properties.DataSource = list;
                if (list.Count > 0)
                    lookUpToaNha.EditValue = list[0].MaTN;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.Properties.DataSource = list2;
                if (list2.Count > 0)
                    lookUpToaNha.EditValue = list2[0].MaTN;
            }

            if (MaTN != 0)
                lookUpToaNha.EditValue = (byte)MaTN;
            dateSyn.EditValue = DateTime.Now;
        }
    }

    public class ItemData
    {
        public int ID { get; set; }
        public int CusID { get; set; }
        public decimal Fee { get; set; }
        public decimal Fee2 { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IsFullTerm { get; set; }

        public void AddItem(ItemData item)
        {
            
        }
    }
}