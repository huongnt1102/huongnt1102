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

namespace EmailAmazon.Sending
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmManager()
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
                this.gcSending.DataSource = (object)MailCommon.cmd.GetChienDich(MailCommon.MaHD, MailCommon.MatKhau, this.itemTuNgay.EditValue != null ? (DateTime)this.itemTuNgay.EditValue : DateTime.Now.AddDays(-90.0), this.itemDenNgay.EditValue != null ? (DateTime)this.itemDenNgay.EditValue : DateTime.Now);
                this.grvSending.FocusedRowHandle = -1;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            GeneratorThangMay();
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void Receive_Detail()
        {
            long? nullable = (long?)this.grvSending.GetFocusedRowCellValue("ID");
            if (!nullable.HasValue)
            {
                switch (this.xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        this.htmlContent.InnerHtml = "";
                        break;
                    case 1:
                        this.gcNotSend.DataSource = (object)null;
                        break;
                    case 2:
                        this.gcSucceed.DataSource = (object)null;
                        break;
                    case 3:
                        this.gcFails.DataSource = (object)null;
                        break;
                    case 4:
                        this.gcReceive.DataSource = (object)null;
                        break;
                }
            }
            else
            {
                switch (this.xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        this.htmlContent.InnerHtml = MailCommon.cmd.DetailChienDich(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value).NoiDung;
                        break;
                    case 1:
                        this.gcNotSend.DataSource = (object)MailCommon.cmd.GetKhachHangTheoChienDich(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value, 1);
                        break;
                    case 2:
                        this.gcSucceed.DataSource = (object)MailCommon.cmd.GetKhachHangTheoChienDich(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value, 2);
                        break;
                    case 3:
                        this.gcFails.DataSource = (object)MailCommon.cmd.GetKhachHangTheoChienDich(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value, 3);
                        break;
                    case 4:
                        this.gcReceive.DataSource = (object)MailCommon.cmd.GetNhomKhachHangTheoChienDich(MailCommon.MaHD, MailCommon.MatKhau, nullable.Value);
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
                long? nullable1 = (long?)this.grvSending.GetFocusedRowCellValue("ID");
                if (!nullable1.HasValue)
                {
                    DialogBox.Error("Vui lòng chọn chiến dịch");
                }
                else
                {
                    int? nullable2 = (int?)this.grvSending.GetFocusedRowCellValue("MaTT");
                    if ((nullable2.GetValueOrDefault() != 2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
                    {
                        DialogBox.Error("Chiến dịch đã được thực hiện, không thể xóa");
                    }
                    else
                    {
                        if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No)
                            return;
                        switch (MailCommon.cmd.DeleteChienDich(MailCommon.MaHD, MailCommon.MatKhau, nullable1.Value))
                        {
                            case Result.DaSuDung:
                                DialogBox.Error("Chiến dịch này đã sử dụng, không thể xóa");
                                break;
                            case Result.ThanhCong:
                                this.grvSending.DeleteRow(this.grvSending.FocusedRowHandle);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }


        private void Sending_Active()
        {
            long? nullable1 = (long?)this.grvSending.GetFocusedRowCellValue("ID");
            if (!nullable1.HasValue)
            {
                DialogBox.Error("Vui lòng chọn chiến dịch");
            }
            else
            {
                int? nullable2 = (int?)this.grvSending.GetFocusedRowCellValue("MaTT");
                int? nullable3 = nullable2;
                int num1 = nullable3.GetValueOrDefault() != 2 ? 0 : (nullable3.HasValue ? 1 : 0);
                nullable3 = nullable2;
                int num2 = nullable3.GetValueOrDefault() != 3 ? 0 : (nullable3.HasValue ? 1 : 0);
                if ((num1 | num2) != 0)
                {
                    DialogBox.Error("Chiến dịch đã được thực hiện, không thể kích hoạt");
                }
                else
                {
                    if (DialogBox.Question("Bạn có chắc chắn không?") == DialogResult.No)
                        return;
                    bool KichHoat = !(bool)this.grvSending.GetFocusedRowCellValue("KichHoat");
                    switch (MailCommon.cmd.EditChienDichKichHoat(MailCommon.MaHD, MailCommon.MatKhau, nullable1.Value, KichHoat))
                    {
                        case Result.DaSuDung:
                            DialogBox.Error("Chiến dịch đã được thực hiện, không thể cập nhật");
                            break;
                        case Result.ThanhCong:
                            LoadData();
                            break;
                    }
                }
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
            long? nullable1 = (long?)this.grvSending.GetFocusedRowCellValue("ID");
            if (!nullable1.HasValue)
            {
                DialogBox.Error("Vui lòng chọn chiến dịch");
            }
            else
            {
                int? nullable2 = (int?)this.grvSending.GetFocusedRowCellValue("MaTT");
                if ((nullable2.GetValueOrDefault() != 2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
                {
                    DialogBox.Error("Chiến dịch đã được thực hiện, không thể sửa");
                }
                else
                {
                    frmEdit frmSending = new frmEdit();
                    frmSending.ID = nullable1.Value;
                    int num = (int)frmSending.ShowDialog();
                    if (frmSending.DialogResult != DialogResult.OK)
                        return;
                    LoadData();
                }
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
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvSending.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
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
            if (grvSending.FocusedRowHandle < 0) return;
            if (((bool?)this.grvSending.GetFocusedRowCellValue("KichHoat")).GetValueOrDefault())
            {
                this.itemAction.ImageIndex = 12;
                this.itemAction.Caption = "Tạm dừng";
            }
            else
            {
                this.itemAction.ImageIndex = 11;
                this.itemAction.Caption = "Kích hoạt";
            }
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
                frm.MaNKH = ((int?)grvSending.GetFocusedRowCellValue("ID"));
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
            }
        }

        private void itemAction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sending_Active();
        }

    }
}