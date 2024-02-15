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
using System.Data.Linq.SqlClient;
using System.Threading;

namespace DichVu.LichSuDieuChinhCN
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        string MM = "";
        bool first = true;
        public frmManager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            db = new MasterDataContext();
            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.DataSource = list;
                if (list.Count > 0)
                    itemToaNha.EditValue = list[0].MaTN;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.DataSource = list2;
                if (list2.Count > 0)
                    itemToaNha.EditValue = list2[0].MaTN;
            }
            itemYear.EditValue = db.GetSystemDate().Year;
            SetMonth();

            LoadData();
            first = false;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                var year = Convert.ToInt32(itemYear.EditValue.ToString());
                var month = GetMonth();
                int maTN = -1;
                maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : -1;
                gcTongHop.DataSource = db.GetLichSuDC(year, month, maTN);
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        int GetMonth()
        {
            if (itemMonth.EditValue == null)
                return 0;
            else
            {
                if (MM != itemMonth.EditValue.ToString())
                    MM = itemMonth.EditValue.ToString();
                switch (MM)
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
        }

        void SetMonth()
        {
            int month = DateTime.Now.Month;

            MM = string.Format("Tháng {0}", month);

            itemMonth.EditValue = MM;
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemYear_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcTongHop);
        }
    }
}