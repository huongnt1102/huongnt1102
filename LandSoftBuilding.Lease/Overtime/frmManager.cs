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

namespace LandSoftBuilding.Lease.Overtime
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
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

        void LoadData()
        {
            gcNgoaiGio.DataSource = null;
            gcNgoaiGio.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddRecord()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvNgoaiGio.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.ID = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void DeleteRecord()
        {
            var indexs = gvNgoaiGio.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var ng = db.ctNgoaiGios.Single(p => p.ID == (int)gvNgoaiGio.GetRowCellValue(i, "ID"));
                db.ctNgoaiGios.DeleteOnSubmit(ng);
            }

            try
            {
                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
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
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from ng in db.ctNgoaiGios
                                join lt in db.LoaiTiens on ng.MaLT equals lt.ID
                                join kh in db.tnKhachHangs on ng.MaKH equals kh.MaKH
                                join mb in db.mbMatBangs on ng.MaMB equals mb.MaMB
                                join nvn in db.tnNhanViens on ng.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on ng.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where ng.MaTN == matn & SqlMethods.DateDiffDay(tuNgay, ng.NgayCT) >= 0 & SqlMethods.DateDiffDay(ng.NgayCT, denNgay) >= 0
                                orderby ng.NgayCT descending
                                select new
                                {
                                    ng.ID,
                                    ng.NgayCT,
                                    kh.KyHieu,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    mb.MaSoMB,
                                    ng.SoGio,
                                    ng.DonGia,
                                    ng.ThanhTien,
                                    lt.KyHieuLT,
                                    ng.ThanhTienQD,
                                    ng.DienGiai,
                                    NguoiNhap = nvn.HoTenNV,
                                    ng.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    ng.NgaySua
                                };
            e.Tag = db;
        }
    }
}