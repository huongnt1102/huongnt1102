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

namespace LandSoftBuilding.Receivables.DuTinh
{
    public partial class frmXe : DevExpress.XtraEditors.XtraForm
    {
        public frmXe()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }
    
        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
  
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            var date = DateTime.Now;
            itemThangHienTai.EditValue = date.Month;
            itemNamHienTai.EditValue = date.Year;
            SetDate(7);
            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var thang = Convert.ToUInt32(itemThangHienTai.EditValue);
            var nam = Convert.ToUInt32(itemNamHienTai.EditValue);
            if (thang <= 0 || thang >= 13)
            {
                DialogBox.Error("Tháng không hợp lệ");
                return;
            }
            if (nam <= 0 )
            {
                DialogBox.Error("Năm không hợp lệ");
                return;
            }
            LoadData();
        }     

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void LoadData()
        {
            var _Thang = Convert.ToUInt32(itemThangHienTai.EditValue);
            var _Nam = Convert.ToUInt32(itemNamHienTai.EditValue);
            var db = new MasterDataContext();

            var objHienTai = (from hd in db.dvHoaDons
                              join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                              //join tx in db.dvgxTheXes on new { TableName = hd.TableName, LinkID = hd.LinkID } equals new { TableName = "dvgxTheXe", LinkID = (int?)tx.ID }
                              where hd.IsDuyet.GetValueOrDefault()
                              & hd.NgayTT.Value.Month == _Thang
                              & hd.NgayTT.Value.Year == _Nam
                              & hd.MaLDV == 6
                              group hd by new { hd.MaTN , tn.TenTN} into gr
                              select new ltdata
                              {
                                  TenTN = gr.Key.TenTN,
                                  SoTien = gr.Sum(o=>o.PhaiThu),
                              }).ToList();

            var objDuTinh = (from hd in db.dvHoaDons
                              join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                              join tx in db.dvgxTheXes on new { TableName = hd.TableName, LinkID = hd.LinkID } equals new { TableName = "dvgxTheXe", LinkID = (int?)tx.ID }
                              where hd.IsDuyet.GetValueOrDefault()
                              & !tx.NgungSuDung.GetValueOrDefault()
                              & hd.NgayTT.Value.Month == _Thang
                              & hd.NgayTT.Value.Year == _Nam
                              & hd.MaLDV == 6
                              group hd by new { hd.MaTN, tn.TenTN } into gr
                              select new ltdata
                              {
                                  TenTN = gr.Key.TenTN,
                                  SoTienDuTinh = gr.Sum(o => o.PhaiThu),
                              }).ToList();
            var data = objHienTai.Concat(objDuTinh).ToList();
            var lt = (from hd in data
                     group hd by new { hd.TenTN } into gr
                     select new ltdata
                     {
                         TenTN = gr.Key.TenTN,
                         SoTien = gr.Sum(o => o.SoTien),
                         SoTienDuTinh = gr.Sum(o => o.SoTienDuTinh),
                     }).ToList();
            gcDuTinh.DataSource = lt;
        }
 
        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDuTinh);
        }

        public class ltdata
        {
            public string TenTN { get; set; }
            public decimal? SoTien { get; set; }
            public decimal? SoTienDuTinh { get; set; }
        }
    }
}