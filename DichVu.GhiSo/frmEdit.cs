using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Threading;

namespace DichVu.GhiSo
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte MaTN = 0;
        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.Properties.DataSource = list;
            if (list.Count > 0)
                lookUpToaNha.EditValue = list[0].MaTN;

            if (MaTN > 0)
                lookUpToaNha.EditValue = MaTN;

            spinYear.EditValue = db.GetSystemDate().Year;
            SetMonth();
        }

        int GetMonth()
        {
            switch (cmbMonth.EditValue.ToString())
            {
                case "Tháng 1":
                    return 1;
                case "Tháng 2":
                    return 2;
                case "Tháng 3":
                    return 3;
                case "Tháng 4":
                    return 4;
                case "Tháng 5":
                    return 5;
                case "Tháng 6":
                    return 6;
                case "Tháng 7":
                    return 7;
                case "Tháng 8":
                    return 8;
                case "Tháng 9":
                    return 9;
                case "Tháng 10":
                    return 10;
                case "Tháng 11":
                    return 11;
                case "Tháng 12":
                    return 12;
                default:
                    return 0;
            }
        }

        void SetMonth()
        {
            int month = DateTime.Now.Month;

            cmbMonth.EditValue = string.Format("Tháng {0}", month);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lookUpToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                lookUpToaNha.Focus();
                return;
            }

            //Check GhiSo
            ManagerCls.Month = GetMonth();
            ManagerCls.TowerID = Convert.ToByte(lookUpToaNha.EditValue);
            ManagerCls.TowerName = lookUpToaNha.Text;
            ManagerCls.Year = Convert.ToInt32(spinYear.EditValue);
            if (ManagerCls.Check())
                return;
            //var listKS = db.dvKhoaSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == Convert.ToInt32(lookUpToaNha.EditValue)
            //    & p.Nam == Convert.ToInt32(spinYear.EditValue)
            //    & p.Thang == GetMonth() 
            //    & p.IsFirst.GetValueOrDefault() == chkDauKy.Checked).ToList();

            //if (listKS.Count > 0)
            //{
            //    DialogBox.Alert(string.Format("[Dự án] {0} đã khóa sổ tháng {0}/{1}", lookUpToaNha.Text, GetMonth(), Convert.ToInt32(spinYear.EditValue)));
            //    return;
            //}

            var wait = DialogBox.WaitingForm();

            try
            {
                var listMB = db.mbMatBangs.Where(p => p.mbTangLau.mbKhoiNha.MaTN == Convert.ToInt32(lookUpToaNha.EditValue));
                var now = db.GetSystemDate();
                var list = new List<dvKhoaSo>();

                int count = 0;
                foreach (var item in listMB)
                {
                    int loop = 0;
                doo:
                    try
                    {
                        var obj = new dvGhiSo();
                        obj.ID = Guid.NewGuid();
                        obj.MaMB = item.MaMB;
                        obj.MaNV = objnhanvien.MaNV;
                        obj.NgayTao = now;
                        obj.Years = Convert.ToInt32(spinYear.EditValue);
                        obj.Months = GetMonth();
                        obj.MaTN = Convert.ToByte(lookUpToaNha.EditValue);
                        obj.CodeUnique = string.Format("{0}-{1}-{2}", obj.MaMB, obj.Months, obj.Years);

                        //list.Add(obj);
                        db.dvGhiSos.InsertOnSubmit(obj);
                        db.SubmitChanges();

                        count++;
                        wait.SetCaption(string.Format("Đã ghi sổ {0}/{1} căn", count, listMB.Count()));
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "Violation of UNIQUE KEY constraint 'IX_dvGhiSo'. Cannot insert duplicate key in object 'dbo.dvGhiSo'.\r\nThe statement has been terminated.")
                        {
                            loop++;
                            if (loop < 5) goto doo;
                        }
                    }
                }

                //db.dvKhoaSos.InsertAllOnSubmit(list);
                //db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                this.Close();
            }
            catch (Exception ex) { DialogBox.Alert(ex.Message); }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}