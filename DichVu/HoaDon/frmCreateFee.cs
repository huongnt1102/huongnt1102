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

namespace DichVu.HoaDon
{
    public partial class frmCreateFee : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        List<ItemData> listMB;
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

            if (CheckCount() == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dịch vụ], xin cảm ơn.");  
                return;
            }

            //Check Khoa So
            DichVu.KhoaSo.ManagerCls.IsFirst = true;
            DichVu.KhoaSo.ManagerCls.Month = dateSyn.DateTime.Month;
            DichVu.KhoaSo.ManagerCls.TowerID = Convert.ToByte(lookUpToaNha.EditValue);
            DichVu.KhoaSo.ManagerCls.TowerName = lookUpToaNha.Text;
            DichVu.KhoaSo.ManagerCls.Year = dateSyn.DateTime.Year;
            if (DichVu.KhoaSo.ManagerCls.CheckEditData())
                return;
            
            var wait = DialogBox.WaitingForm();
            
            try
            {
                //listMB = db.mbMatBangs.Where(p => p.MaKH != null && p.mbTangLau.mbKhoiNha.MaTN == Convert.ToInt32(lookUpToaNha.EditValue.ToString())).Select(p => new ItemData() { ID = p.MaMB, CusID = p.MaKH.Value, Fee = 0, Fee2 = 0, Fee3 = 0, DateFee = 0, DateFeeVS = 0, DateEndFee = 0, IsChuyen = 0 }).ToList();
                for (int i = 0; i < gvService.RowCount; i++)
                {
                    if (((bool?)gvService.GetRowCellValue(i, "IsCheck")).GetValueOrDefault())
                    {
                        wait.SetCaption(string.Format("Syn: {0}", gvService.GetRowCellValue(i, "Name").ToString()));

                        Synchronize(byte.Parse(gvService.GetRowCellValue(i, "ID").ToString()));

                    }
                    Thread.Sleep(1000);
                }
            }
            catch { }
            finally { wait.Close(); }
        }

        void Synchronize(byte cateID)
        {
            switch (cateID)
            {
                case 3://Dich vu khac
                    break;
                case 4://Dich vu thue ngoai
                    break;
                case 5://Hop dong thue
                    HopDongThue();
                    break;
                case 6://Giu xe
                    break;
                case 7://Thang may
                    break;
                case 8://Hop tac
                    break;
                case 9://Thue ngan han
                    break;
                case 10://Nha cung cap
                    break;
                case 12://Phi quan ly
                    PhiQuanLy();
                    break;
                case 13://Phi ve sinh
                    PhiVeSinh();
                    break;
                case 16://Phi ve sinh
                    PhiBaoTri();
                    break;
            }
        }

        void HopDongThue()
        {
            var list = db.thueHopDongs.Where(p => p.MaTN == Convert.ToInt32(lookUpToaNha.EditValue.ToString()) & (p.MaTT == 2 | p.MaTT == 4))
                .Select(p => new { p.MaMB, p.ThanhTien, p.MaKH}).ToList();
            foreach (var item in list)
            {
                db.cnLichSu_addHopDongThue(item.MaMB, item.MaKH, dateSyn.DateTime, item.ThanhTien);
            }
        }

        void PhiQuanLy()
        {
            foreach (var item in listMB)
            {
                if (SqlMethods.DateDiffDay(item.DateFee, dateSyn.DateTime) >= 0)
                {
                    db.cnLichSu_addPQL(item.ID, item.CusID, dateSyn.DateTime, GetFee(item));

                    var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, dateSyn.DateTime) >= 0 & SqlMethods.DateDiffDay(dateSyn.DateTime, p.NgayKT) >= 0 & p.MaMB == item.ID).FirstOrDefault();
                    if (objPDK != null)
                    {
                        if (objPDK.NgayDK.Value.Month != dateSyn.DateTime.Month | objPDK.NgayDK.Value.Year != dateSyn.DateTime.Year)
                            db.cnLichSu_addPQL(item.ID, item.CusID, dateSyn.DateTime, 0);
                    }
                }
                else
                    db.cnLichSu_addPQL(item.ID, item.CusID, dateSyn.DateTime, 0);
            }
        }

        private decimal GetFee(ItemData item)
        {
            decimal Fee = 0;
            try
            {
                //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên
                if (item.DateFee.Value.Month == dateSyn.DateTime.Month & item.DateFee.Value.Year == dateSyn.DateTime.Year)
                {
                    if (item.IsChuyen)
                        Fee = 0;
                    else
                    {
                        //Ngày của tháng
                        int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                        //Ngày ở thực tế
                        int dayReal = day - item.DateFee.Value.Day;

                        Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                    }
                }
                else//Tháng tiếp theo
                {
                    if (item.IsChuyen)
                    {
                        //Chuyen sang thang sau tinh phi thang dau tien
                        if ((item.DateFee.Value.Month + 1) == dateSyn.DateTime.Month & item.DateFee.Value.Year == dateSyn.DateTime.Year)
                        {
                            //Ngày của tháng
                            int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                            //Ngày ở thực tế
                            int dayReal = day - item.DateFee.Value.Day;

                            Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + item.Fee;//+1 tính luôn ngày vào ở
                        }//
                        else
                            Fee = item.Fee;
                    }
                    else
                        Fee = item.Fee;
                }

                var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, dateSyn.DateTime) >= 0 & SqlMethods.DateDiffDay(dateSyn.DateTime, p.NgayKT) >= 0 & p.MaMB == item.ID).FirstOrDefault();
                if (objPDK != null)
                {
                    if (objPDK.NgayDK.Value.Month == dateSyn.DateTime.Month & objPDK.NgayDK.Value.Year == dateSyn.DateTime.Year)
                        Fee = objPDK.PhaiThu ?? 0;
                    else
                        Fee = 0;
                }
            }
            catch { Fee = 0; }

            return Fee;
        }

        void PhiBaoTri()
        {
            foreach (var item in listMB)
            {
                if (SqlMethods.DateDiffDay(item.DateFee, dateSyn.DateTime) >= 0)
                {
                    db.cnLichSu_addPBT(item.ID, item.CusID, dateSyn.DateTime, GetFeePBT(item));
                }
                else
                    db.cnLichSu_addPBT(item.ID, item.CusID, dateSyn.DateTime, 0);
            }
        }

        private decimal GetFeePBT(ItemData item)
        {
            decimal Fee = 0;
            try
            {
                //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên
                if (item.DateFee.Value.Month == dateSyn.DateTime.Month & item.DateFee.Value.Year == dateSyn.DateTime.Year)
                {
                    if (item.IsChuyen)
                        Fee = 0;
                    else
                    {
                        //Ngày của tháng
                        int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                        //Ngày ở thực tế
                        int dayReal = day - item.DateFee.Value.Day;

                        Fee = Math.Round((item.Fee3 / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                    }
                }
                else//Tháng tiếp theo
                {
                    if (item.IsChuyen)
                    {
                        //Chuyen sang thang sau tinh phi thang dau tien
                        if ((item.DateFee.Value.Month + 1) == dateSyn.DateTime.Month & item.DateFee.Value.Year == dateSyn.DateTime.Year)
                        {
                            //Ngày của tháng
                            int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                            //Ngày ở thực tế
                            int dayReal = day - item.DateFee.Value.Day;

                            Fee = Math.Round((item.Fee3 / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + item.Fee;//+1 tính luôn ngày vào ở
                        }//
                        else
                            Fee = item.Fee3;
                    }
                    else
                        Fee = item.Fee3;
                }
            }
            catch { Fee = 0; }

            return Math.Round(Fee, 0, MidpointRounding.AwayFromZero);
        }

        void PhiVeSinh()
        {
            foreach (var item in listMB)
            {
                if (SqlMethods.DateDiffDay(item.DateFeeVS, dateSyn.DateTime) >= 0)
                    db.cnLichSu_addPVS(item.ID, item.CusID, dateSyn.DateTime, item.Fee2);
                else
                    db.cnLichSu_addPVS(item.ID, item.CusID, dateSyn.DateTime, 0);
            }
        }

        void CheckAll()
        {
            if (chkCheckAll.Checked)
            {
                for (int i = 0; i < gvService.RowCount; i++)
                    gvService.SetRowCellValue(i, "IsCheck", true);
            }
            else
            {
                for (int i = 0; i < gvService.RowCount; i++)
                    gvService.SetRowCellValue(i, "IsCheck", false);
            }
        }

        int CheckCount()
        {
            int count = 0;
            for (int i = 0; i < gvService.RowCount; i++)
                if (((bool?)gvService.GetRowCellValue(i, "IsCheck")).GetValueOrDefault())
                    count++;
            return count;
        }

        private void chkCheck_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = (CheckEdit)sender;
            gvService.SetFocusedRowCellValue("IsCheck", ckb.Checked);
            if (CheckCount() == gvService.RowCount)
                chkCheckAll.Checked = true;
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void frmCreateFee_Load(object sender, EventArgs e)
        {
            gcService.DataSource = db.dvLoaiDV_getSyn();

            db = new MasterDataContext();
            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.Properties.DataSource = list;
            if (list.Count > 0)
                lookUpToaNha.EditValue = list[0].MaTN;

            dateSyn.EditValue = DateTime.Now;
        }
    }

    public class ItemData
    {
        public int ID { get; set; }
        public int CusID { get; set; }
        public decimal Fee { get; set; }
        public decimal Fee2 { get; set; }
        public decimal Fee3 { get; set; }
        public DateTime? DateFee { get; set; }
        public DateTime? DateEndFee { get; set; }
        public DateTime? DateFeeVS { get; set; }
        public bool IsChuyen { get; set; }
    }
}