using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Linq;
using LandSoft.Library;
using DevExpress.Utils;
using System.Collections;
using System.Data.Linq.SqlClient;

namespace LandSoft.DuAn.BieuMau
{
    public partial class ctlProjectTemplate : UserControl
    {
        MasterDataContext db = new MasterDataContext();
        byte SDBID = 6;
        bool IsFirst = true;

        public ctlProjectTemplate()
        {
            InitializeComponent();

            LoadPermission();

         //   ctlChinhSachChung.ShowMenu = false;

            var listProject = db.DuAn_getList();
            lookDuAn.DataSource = listProject;
            lookUpEditDuAn.DataSource = listProject;

            lookTinhTrang.DataSource = db.pgcTinhTrangs;

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            //objKBC.Initialize(cmbKyBaoCao);
        }

        void LoadPermission()
        {
            try
            {
                var ltAction = db.ActionDatas.Where(p => p.PerID == LandSoft.Library.Common.PerID & p.FormID == 163).Select(p => p.FeatureID).ToList();
                btnThem.Enabled = ltAction.Contains(1);
                btnSua.Enabled = ltAction.Contains(2);
                btnXoa.Enabled = ltAction.Contains(3);

                var ltAccess = db.AccessDatas.Where(p => p.PerID == LandSoft.Library.Common.PerID & p.FormID == 163).Select(p => p.SDBID).ToList();
                if (ltAccess.Count > 0)
                    this.SDBID = ltAccess[0];
            }
            catch
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        void LoadData()
        {
            DialogBox.ShowWaitForm(this.ParentForm);

            if (SDBID == 6)
            {
                gcBieuMau.DataSource = null;
                return;
            }

            try
            {
                int maPB = 0, maNKD = 0, maNV = 0, maCN = 0;
                switch (SDBID)
                {
                    case 2: maPB = LandSoft.Library.Common.DepartmentID;
                        maCN = Library.Common.BranchID;
                        break;
                    case 3: maNKD = LandSoft.Library.Common.GroupID;
                        maCN = Library.Common.BranchID; break;
                    case 4: maNV = Library.Common.StaffID; break;
                    case 5: maCN = Library.Common.BranchID; break;
                }

                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                
                gcBieuMau.DataSource = db.daBieuMaus.Where(p => p.MaDA == maDA
                    & (p.MaNV == maNV | maNV == 0)
                    & (p.NhanVien.MaPB == maPB | maPB == 0)
                    & (p.NhanVien.MaNKD == maNKD | maNKD == 0)
                    & (p.NhanVien.BranchID == maCN | maCN == 0)
                    & SqlMethods.DateDiffDay(tuNgay, p.NgayCN) >= 0 
                    & SqlMethods.DateDiffDay(p.NgayCN, denNgay) >= 0)
                    .OrderBy(p => p.MaLBM).OrderBy(p => p.NgayCN)
                          .Select(p => new { p.MaBM, p.TenBM, p.DienGiai, p.daLoaiBieuMau.TenLBM, p.Khoa, p.NgayCN, p.NhanVien.HoTen, HoTen1 = "", p.MaDA })
                          .ToList();
            }
            catch
            { }
            DialogBox.HideWaitForm();
        }

        void EnableControl()
        {
            //byte? maTT = (byte?)grvGiuCho.GetFocusedRowCellValue(colMaTT);
            //if (maTT == null)
            //{
            //    SetEnableConfirmInfo(false);
            //    SetEnableConfirmBuy(false);
            //}
            //else
            //{
            //    switch (maTT.Value.ToString())
            //    {
            //        case "1":
            //            SetEnableConfirmInfo(true);
            //            SetEnableConfirmBuy(false);
            //            break;
            //        case "2":
            //            SetEnableConfirmInfo(false);
            //            SetEnableConfirmBuy(true);
            //            break;
            //        case "3":
            //            SetEnableConfirmInfo(true);
            //            SetEnableConfirmBuy(false);
            //            break;
            //        case "4":
            //            SetEnableConfirmInfo(false);
            //            SetEnableConfirmBuy(true);
            //            break;
            //        case "5":
            //            SetEnableConfirmInfo(false);
            //            SetEnableConfirmBuy(false);
            //            break;
            //        case "6":
            //            SetEnableConfirmInfo(false);
            //            SetEnableConfirmBuy(false);
            //            break;
            //        case "7":
            //            SetEnableConfirmInfo(false);
            //            SetEnableConfirmBuy(true);
            //            break;
            //        default:
            //            SetEnableConfirmInfo(false);
            //            SetEnableConfirmBuy(false);
            //            break;
            //    }
            //}
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemDenNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsFirst) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsFirst) LoadData();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsFirst) LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            LoadPermission();

            SetDate(0);

            LoadData();

            IsFirst = false;
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
            if (maDA == -1)
            {
                DialogBox.Warning("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            LandSoft.DuAn.BieuMau.frmEdit frm = new frmEdit();
            frm.MaDA = maDA;
            frm.ShowDialog();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Warning("Cần chọn biểu mẫu để chỉnh sữa");
                return;
            }
            int mabm = (int)grvBieuMau.GetFocusedRowCellValue("MaBM");
            LandSoft.DuAn.BieuMau.frmEdit frm = new frmEdit();
            frm.MaBM = mabm;
            frm.MaDA = maDA;
            frm.ShowDialog();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = grvBieuMau.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question() == DialogResult.No) return;

                foreach (var i in indexs)
                {
                    daBieuMau objSP = db.daBieuMaus.Single(p => p.MaBM == (int)grvBieuMau.GetRowCellValue(i, "MaBM"));
                    db.daBieuMaus.DeleteOnSubmit(objSP);
                }

                db.SubmitChanges();

                LoadData();
            }

            catch (Exception ex)
            {
                DialogBox.Infomation(ex.Message);
            }
        }
    }
}
