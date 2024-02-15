using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace LandSoftBuilding.Lease.TOS
{
    public partial class frmDoanhSo : DevExpress.XtraEditors.XtraForm
    {
        public frmDoanhSo()
        {
            InitializeComponent();
        }

        #region Class


        #endregion

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

        void LoadData()
        {
            gcLTT.DataSource = null;
            //gcLTT.DataSource = linqInstantFeedbackSource1;
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                var model = new { MaTN = _MaTN, TuNgay = _TuNgay, DenNgay = _DenNgay };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gcLTT.DataSource = Library.Class.Connect.QueryConnect.Query<Class.lease_tos_doanhso_load>("lease_tos_doanhso_load", param);
            }
            catch { }
        }

        void RefreshData()
        {
            //linqInstantFeedbackSource1.Refresh();
            LoadData();
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            this.LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemDieuChinhTyGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvLTT.GetFocusedRowCellValue("MaHD");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            frmLSDCGEdit frm = new frmLSDCGEdit();
            frm.MaHD = id;
            frm.ShowDialog();
            if (frm.IsSave)
            {
                this.RefreshData();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if((int?)gvLTT.GetFocusedRowCellValue("ID")==null)return;
            using (var dbo = new MasterDataContext())
            {
                var Tungay = (DateTime) itemTuNgay.EditValue;
                var ID = (int) gvLTT.GetFocusedRowCellValue("ID");
                 var KT = dbo.dvHoaDons.SingleOrDefault(p => p.LinkID == ID & p.TableName == "ctLichThanhToan" 
                    & p.NgayTT.Value.Month == Tungay.Month & p.NgayTT.Value.Year == Tungay.Year
                    & p.MaTN == Convert.ToInt32(itemToaNha.EditValue) 
                    );
                if (KT == null)
                {
                    dbo.dvHoaDon_InsertAllLTT((byte?)itemToaNha.EditValue, Tungay.Month,
                        Tungay.Year, Common.User.MaNV);
                }
                else
                {
                    KT.TienTT = (decimal)gvLTT.GetFocusedRowCellValue("SoTienQD");
                    KT.PhiDV = (decimal)gvLTT.GetFocusedRowCellValue("SoTienQD");
                    KT.PhaiThu = (decimal)gvLTT.GetFocusedRowCellValue("SoTienQD");
                    KT.DaThu = 0;
                    KT.ConNo = (decimal)gvLTT.GetFocusedRowCellValue("SoTienQD");
                    dbo.SubmitChanges();
                }
                try
                {
                    DialogBox.Alert("Dư liệu đã được cập nhật");
                }
                catch (Exception)
                {
                    
                    DialogBox.Error("Đã xảy ra lỗi vui long kiểm tra lại");
                }
               
            }
        }

        private void itemImportSales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            byte? MaTN = (byte?)itemToaNha.EditValue;
            if (MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án]. Xin cám ơn!");
                return;
            }

            var frm = new LandSoftBuilding.Lease.TOS.frmImportSales();
            frm.MaTN = MaTN.Value;
            frm.ShowDialog();
            if (frm.isSave)
                this.LoadData();
        }

        private void itemTaoHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                var model = new { MaTN = _MaTN, TuNgay = _TuNgay, DenNgay = _DenNgay, MaNV = Library.Common.User.MaNV };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("dvHoaDon_InsertLTT_TOS", param);
                DialogBox.Success("Tạo hóa đơn cho các doanh số có ngày thanh toán trong tháng " + _TuNgay.Month.ToString() + " thành công");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = gvLTT.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (int i in indexs)
                {
                    var ct_ds = db.ctChiTiet_DoanhSos.FirstOrDefault(_ => _.ID == (int)gvLTT.GetRowCellValue(i, "ID"));
                    db.ctChiTiet_DoanhSos.DeleteOnSubmit(ct_ds);
                }
                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
                //DialogBox.Alert(
                //    "Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmDoanhSoEdit { MaTN = (byte?)itemToaNha.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (int?)gvLTT.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (var frm = new frmDoanhSoEdit { MaTN = (byte?)itemToaNha.EditValue, MaCT = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {
            }
        }

        private void gcLTT_Click(object sender, EventArgs e)
        {

        }
    }
}