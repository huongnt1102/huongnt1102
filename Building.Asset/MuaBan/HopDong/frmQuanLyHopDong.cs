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
using System.Data.Linq.SqlClient;

namespace TaiSan.XuatKho
{
    public partial class frmQuanLyHopDong : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyHopDong()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmQuanLyHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;


            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbxKBC.Items.Add(str);
            }
            beiKBC.EditValue = objKBC.Source[3];
            SetDate(3);

            //LoadData();
            itemToaNha.EditValue = Common.User.MaTN;
            gvHopDong.BestFitColumns();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvHopDong.RowCount > 0 & gvHopDong.FocusedRowHandle >= 0)
            {
                using (var frm = new TaiSan.XuatKho.frmHopDongVatTu())
                {

                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new TaiSan.XuatKho.frmHopDongVatTu())
            {
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch
            { }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void gvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    LoadDetail();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();
            beiTuNgay.EditValue = objKBC.DateFrom;
            beiDenNgay.EditValue = objKBC.DateTo;
        }

        private void LoadData()
        {

        }

        private void LoadDetail()
        {

        }



        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }


        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var frm = new Import.frmImportHopDongBan { })
            { 
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void bbiHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (gvHopDong.GetFocusedRowCellValue("IsCaNhan") == null) return;
            var isCaNhan = Convert.ToBoolean(gvHopDong.GetFocusedRowCellValue("IsCaNhan").ToString());

            if (isCaNhan)
            {

                var rpt = new TaiSan.BanHang.Report.rptHopDongCaNhan();
                rpt.CreateDocument();
                for (int i=0;i<1;i++)
                {
                    var rpt1=new TaiSan.BanHang.Report.rptHopDongCaNhan();
                    rpt1.CreateDocument();
                    rpt.Pages.AddRange(rpt1.Pages);
                }
                rpt.PrintingSystem.ContinuousPageNumbering = true;
                rpt.ShowPreviewDialog();

            }
            else
            {

                var rpt = new TaiSan.BanHang.Report.rptHopDongCongTy();
                rpt.CreateDocument();
                for (int i = 0; i < 1; i++)
                {
                    var rpt1 = new TaiSan.BanHang.Report.rptHopDongCongTy();
                    rpt1.CreateDocument();
                    rpt.Pages.AddRange(rpt1.Pages);
                }
                rpt.PrintingSystem.ContinuousPageNumbering = true;
                rpt.ShowPreviewDialog();
            }
        }

        private void bbiLenhPhaDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {



                // cá nhân
                using (var frm = new Building.PrintControls.PrintForm { Text = "LỆNH PHÁ DỠ" })
                {
                    var rpt = new TaiSan.BanHang.Report.rptLenhPhaDo();
                    rpt.CreateDocument();
                    for (int i = 0; i < 1; i++)
                    {
                        var rpt1 = new TaiSan.BanHang.Report.rptLenhPhaDo();
                        rpt1.CreateDocument();
                        rpt.Pages.AddRange(rpt1.Pages);
                    }
                    rpt.PrintingSystem.ContinuousPageNumbering = true;
                    rpt.ShowPreviewDialog();
                     
                    
                }

            }
            catch
            {
                //
            }
        }

        private void bbiBienBanNghiemThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // cá nhân
                using (var frm = new Building.PrintControls.PrintForm { Text = "LỆNH PHÁ DỠ" })
                {
                    var rpt = new TaiSan.BanHang.Report.rptBienBanNghiemThu();
                    rpt.CreateDocument();
                    for (int i = 0; i < 1; i++)
                    {
                        var rpt1 = new TaiSan.BanHang.Report.rptBienBanNghiemThu();
                        rpt1.CreateDocument();
                        rpt.Pages.AddRange(rpt1.Pages);
                    }
                    rpt.PrintingSystem.ContinuousPageNumbering = true;
                    rpt.ShowPreviewDialog();

                }

            }
            catch
            {
                //
            }
        }

        private void bbiBBQT_TLHD_PD_CN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                // cá nhân
                using (var frm = new Building.PrintControls.PrintForm { Text = "BBQT_TLHD_PD_CN" })
                {
                    var rpt = new TaiSan.BanHang.Report.rptBBQT_TLHD_PD_CN();
                    rpt.CreateDocument();
                    for (int i = 0; i < 1; i++)
                    {
                        var rpt1 = new TaiSan.BanHang.Report.rptBBQT_TLHD_PD_CN();
                        rpt1.CreateDocument();
                        rpt.Pages.AddRange(rpt1.Pages);
                    }
                    rpt.PrintingSystem.ContinuousPageNumbering = true;
                    rpt.ShowPreviewDialog();
                }

            }
            catch
            {
                //
            }
        }

        private void itemBBQT_TLHD_VT_CN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                // cá nhân
                using (var frm = new Building.PrintControls.PrintForm { Text = "BBQT_TLHD_VT_CN" })
                {
                    var rpt = new TaiSan.BanHang.Report.rptBBQT_TLHD_VT_CN();
                    rpt.CreateDocument();
                    for (int i = 0; i < 1; i++)
                    {
                        var rpt1 = new TaiSan.BanHang.Report.rptBBQT_TLHD_VT_CN();
                        rpt1.CreateDocument();
                        rpt.Pages.AddRange(rpt1.Pages);
                    }
                    rpt.PrintingSystem.ContinuousPageNumbering = true;

                    rpt.ShowPreviewDialog();
                }

            }
            catch
            {
                //
            }
        }

        private void btnKhoiLuongTamTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new TaiSan.XuatKho.frmHopDongVatTu_KhoiLuong())
            {

                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
            

        }

        private void itemKhoiLuongThucTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new TaiSan.XuatKho.frmHopDongVatTu_KhoiLuong())
            {

                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
            
        }

        private void itemImportKhoiLuongTamTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImportHopDongVatTu_KhoiLuong() {  })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
            
        }

        private void itemImportKhoiLuongThucTinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmImportHopDongVatTu_KhoiLuong() {  })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                        LoadDetail();
                    }
                }
            }
            catch
            {
                DialogBox.Error("Đã có lỗi xảy ra khi import dữ liệu");
                return;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
    }
}