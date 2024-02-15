using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using System.Data.Linq.SqlClient;

namespace DichVu.PhiQuanLy
{
    public partial class ctlHistoryPrint : UserControl
    {
        MasterDataContext db = new MasterDataContext();
        public tnNhanVien objnhanvien;
        DateTime now;
        string MM = "";
        bool first = true;
        public ctlHistoryPrint()
        {
            InitializeComponent();
            db = new MasterDataContext();
            //TranslateLanguage.TranslateControl(this, barManager1);
            now = db.GetSystemDate();
        }

        private void ctlHistoryPrint_Load(object sender, EventArgs e)
        {
            //Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
            {
                p.MaTN,
                p.TenTN
            }).ToList();

            cmbToaNha.DataSource = list2;
            if (list2.Count > 0)
                itemToaNha.EditValue = list2[0].MaTN;
            SetMonth();
            Load_History();
            first = false;
        }

        private void Load_History()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = Convert.ToInt32(spinYear.EditValue.ToString());
                var month = GetMonth();
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                gc_HistoryPrint.DataSource = db.PhiQuanLy_selectBySuperAdminV3(year, month, maTN)
                    .AsEnumerable()
                    .Select((p, index) => new 
                    {
                        STT=index+1,
                        p.ID,
                        p.NgayTH,
                        p.NhanVien,
                        p.SoNha,
                        p.Barcode,
                        p.DienGiai,
                        p.MaSoMB
                    });
            }
            catch
            {
                gc_HistoryPrint.DataSource = null;
            }
            finally
            {
                wait.Close();
                wait.Dispose();
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
            Load_History();
        }


        private void spinYear_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) Load_History();
        }

        private void itemMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) Load_History();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) Load_History();
        }

        private void itemBarCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grv_HistoryPrint.GetFocusedRowCellValue("ID") != null)
            {
            db = new MasterDataContext();
                var objPT = db.PhiQuanLy_LichSus.Single(p => p.ID== (int)grv_HistoryPrint.GetFocusedRowCellValue("ID"));
                if (objPT.Barcode == null || objPT.Barcode == "")
                    DialogBox.Alert("[Giấy báo] này chưa phát sinh mã vạch. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    //var frm = new ReportMisc.frmBarCode(objPT.Barcode);
                    //frm.ShowDialog();
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn [Giấy báo], xin cảm ơn.");
        }
    }
}
