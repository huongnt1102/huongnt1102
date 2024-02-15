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
using EmailAmazon.API;

namespace EmailAmazon.Receive
{
    public partial class frmReceive : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmReceive()
        {
            InitializeComponent();
            MailCommon.cmd = new API.APISoapClient();
            MailCommon.cmd.Open();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            try
            {
                db = new MasterDataContext();

                    gcReceive.DataSource  = MailCommon.cmd.GetNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau).ToList();
            }
            catch { }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            //itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            //itemTuNgay.EditValue = objKBC.DateFrom;
            //itemDenNgay.EditValue = objKBC.DateTo;
            //itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            GeneratorThangMay();
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            //itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void Receive_Detail()
        {
            int? nullable = (int?)this.grvReceive.GetFocusedRowCellValue("ID");
            if (!nullable.HasValue)
            {
                switch (this.xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        this.gcSending.DataSource = (object)null;
                        break;
                    case 1:
                        this.gcList.DataSource = (object)null;
                        break;
                }
            }
            else
            {
                switch (this.xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        this.gcSending.DataSource = (object)MailCommon.cmd.GetChienDichTheoNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value);
                        break;
                    case 1:
                        this.gcList.DataSource = (object)MailCommon.cmd.GetKhachHang(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value);
                        break;
                }
            }
        }

        private void GeneratorThangMay()
        {
            DateTime now = db.GetSystemDate();
            var getAllDVTM = from tt in db.dvtmTheThangMays
                             select tt;

            foreach (dvtmTheThangMay dv in getAllDVTM)
            {
                var getALLDVTT = from dvtt in db.dvtmThanhToanThangMays
                                 where dvtt.TheThangMayID == dv.ID
                                 select dvtt;

                if (SqlMethods.DateDiffMonth(dv.NgayDK, now) >= 0)
                {
                    if (getALLDVTT.Count() <= 0)
                    {
                        for (int i = 0; i < SqlMethods.DateDiffMonth(dv.NgayDK, now) + 3; i++)
                        {
                            dvtmThanhToanThangMay objdvtt = new dvtmThanhToanThangMay() { TheThangMayID = dv.ID, ThangThanhToan = dv.NgayDK.Value.AddMonths(i), DaTT = false };
                            db.dvtmThanhToanThangMays.InsertOnSubmit(objdvtt);
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        while (SqlMethods.DateDiffMonth(getALLDVTT.Max(p => p.ThangThanhToan), now) > 1 + 3)
                        {
                            dvtmThanhToanThangMay objdvtt = new dvtmThanhToanThangMay() { TheThangMayID = dv.ID, ThangThanhToan = getALLDVTT.Max(p => p.ThangThanhToan).Value.AddMonths(1), DaTT = false };
                            db.dvtmThanhToanThangMays.InsertOnSubmit(objdvtt);
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void DeleteSelected()
        {
            try
            {
                int? nullable = (int?)this.grvReceive.GetFocusedRowCellValue("ID");
                if (!nullable.HasValue)
                {
                    DialogBox.Error("Vui lòng chọn danh sách, xin cảm ơn.");
                }
                else
                {
                    if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No)
                        return;

                    MailCommon.cmd.DeleteNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSelected();
        }

        private void grvTheXe_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Delete)
                DeleteSelected();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? nullable = (int?)this.grvReceive.GetFocusedRowCellValue("ID");
            if (!nullable.HasValue)
            {
                DialogBox.Error("Vui lòng chọn danh sách, xin cảm ơn.");
            }
            else
            {
                frmEdit frm = new frmEdit();
                frm.ID = nullable.Value;
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK)
                    return;

                LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmThanhToan frmtt = new frmThanhToan();
            //frmtt.objdvtm = db.dvtmTheThangMays.Single(p => p.ID == (int)grvTheXe.GetFocusedRowCellValue("ID"));
            //frmtt.ShowDialog();

            //if (frmtt.DialogResult == DialogResult.OK)
            //    LoadData();
        }

        private void grvTheXe_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvReceive.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void btn2Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                //if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                //{
                //    var tuNgay = (DateTime)itemTuNgay.EditValue;
                //    var denNgay = (DateTime)itemDenNgay.EditValue;
                //    DataTable dt = new DataTable();

                //    var ts = db.dvtmThanhToanThangMays
                //                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.ThangThanhToan) >= 0
                //                    & SqlMethods.DateDiffDay(p.ThangThanhToan, denNgay) >= 0)
                //                .OrderBy(p => p.dvtmTheThangMay.mbMatBang.MaSoMB)
                //                .Select(p => new
                //                {
                //                    p.dvtmTheThangMay.mbMatBang.MaSoMB,
                //                    KhachHang = p.dvtmTheThangMay.MaKH.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? p.dvtmTheThangMay.tnKhachHang.HoKH + " " + p.dvtmTheThangMay.tnKhachHang.TenKH : p.dvtmTheThangMay.tnKhachHang.CtyTen) : "") : "",
                //                    p.dvtmTheThangMay.ChuThe,
                //                    p.dvtmTheThangMay.SoThe,
                //                    p.dvtmTheThangMay.PhiLamThe,
                //                    Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                //                    ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                //                    TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                //                });
                //    dt = SqlCommon.LINQToDataTable(ts);
                //    ExportToExcel.exportDataToExcel("Danh sách tài sản", dt);
                //}
            }
        }

        private void grvTheXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvReceive.FocusedRowHandle < 0) return;
            Receive_Detail();
        }

        private void btnGiayBaoTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (ReportMisc.DichVu.ThangMay.Report.frmPrintControl frm = new ReportMisc.DichVu.ThangMay.Report.frmPrintControl(0, "", EnumIn.GiayBaoTong))
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void grvTheXe_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator & e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Receive_Detail();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImport())
            {
                frm.MaNKH = ((int?)grvReceive.GetFocusedRowCellValue("ID"));
                frm.ShowDialog();
            }
        }

        private void itemXoaList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = this.grvList.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
            }
            else
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No)
                    return;
                ArrayOfLong ArrID = new ArrayOfLong();
                foreach (int rowHandle in selectedRows)
                    ArrID.Add((long)this.grvList.GetRowCellValue(rowHandle, "ID"));
                if (MailCommon.cmd.DeleteKhachHang(MailCommon.MaHD, MailCommon.MatKhau, ArrID) != Result.ThanhCong)
                    return;
                this.grvList.DeleteSelectedRows();
            }
        }

    }
}