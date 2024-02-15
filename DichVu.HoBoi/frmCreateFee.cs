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
using System.Data.Linq.SqlClient;

namespace DichVu.HoBoi
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
            var date = dateSyn.DateTime;
            try
            {
                listMB = db.dvhbHoBois.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == Convert.ToInt32(lookUpToaNha.EditValue.ToString())
                    & p.IsSuDung.GetValueOrDefault())
                    .Select(p => new ItemData()
                    {
                        ID = p.ID,
                        MaMB = p.MaMB.Value,
                        CusID = p.mbMatBang.MaKH.Value,
                        Fee = p.MucPhi ?? 0,
                        DateTo = p.NgayDangKy,
                        DateFrom = p.NgayHetHan,
                        IsSuDung = p.IsSuDung.GetValueOrDefault(),
                        IsTinhDuThang = p.IsTinhDuThang.GetValueOrDefault()
                    }).OrderBy(p => p.ID).ToList();

                int count = 1;

                listData = new List<ItemData>();
                var time = (DateTime)dateSyn.EditValue;
                foreach (var item in listMB)
                {
                    item.Fee = GetFee(item, date);
                    AddItem(item);

                    db.SubmitChanges();
                    wait.SetCaption(string.Format("Đã tạo {0:#,0.#}/{1:#,0.#} công nợ", count, listMB.Count));

                    count++;
                    Thread.Sleep(10);
                }

                wait.SetCaption("Cập nhật công nợ.");

                count = 1;
                foreach (var item in listData)
                {
                    db.cnLichSu_addHoBoi(item.MaMB, item.CusID, dateSyn.DateTime, item.Fee);

                    wait.SetCaption(string.Format("Đã cập nhật {0:#,0.#}/{1:#,0.#} công nợ", count, listData.Count));

                    count++;
                    Thread.Sleep(50);
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private decimal GetFee(ItemData item, DateTime date)
        {
            decimal Fee = 0;
            try
            {
                if (item.IsTinhDuThang)
                {
                    Fee = item.Fee;
                }
                else
                {
                    //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên
                    if (item.DateFrom.Value.Month == date.Month & item.DateFrom.Value.Year == date.Year)
                    {
                        //Ngày của tháng
                        int day = DateTime.DaysInMonth(item.DateFrom.Value.Year, item.DateFrom.Value.Month);
                        //Ngày ở thực tế
                        int dayReal = day - item.DateFrom.Value.Day;

                        Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                    }
                    else//Tháng tiếp theo
                    {
                        if ((item.DateTo.Value.Month) == date.Month & item.DateTo.Value.Year == date.Year)
                        {
                            //Ngày của tháng
                            int day = DateTime.DaysInMonth(item.DateTo.Value.Year, item.DateTo.Value.Month);
                            //Ngày ở thực tế
                            int dayReal = day - item.DateTo.Value.Day;

                            Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + item.Fee;//+1 tính luôn ngày vào ở
                        }//
                        else
                            Fee = item.Fee;
                    }
                }
            }
            catch { Fee = 0; }

            return Fee;
        }

        private void AddItem(ItemData item)
        {
            if (listData.Where(p => p.MaMB == item.MaMB).Count() > 0)
            {
                var obj = listData.Single(p => p.MaMB == item.MaMB);
                listData.Remove(obj);

                item.Fee += obj.Fee;
            }

            listData.Add(item);
        }

        private void frmCreateFee_Load(object sender, EventArgs e)
        {
            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.Properties.DataSource = list;
            if (list.Count > 0)
                lookUpToaNha.EditValue = list[0].MaTN;

            if (MaTN != 0)
                lookUpToaNha.EditValue = (byte)MaTN;
            dateSyn.EditValue = DateTime.Now;
        }
    }

    public class ItemData
    {
        public int ID { get; set; }
        public int CusID { get; set; }
        public int MaMB { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal Fee { get; set; }
        public bool IsTinhDuThang { get; set; }
        public bool IsSuDung { get; set; }
    }
}