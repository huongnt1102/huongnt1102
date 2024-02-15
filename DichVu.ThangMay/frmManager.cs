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
//using ReportMisc.DichVu;

namespace DichVu.ThangMay
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            try
            {
                db = new MasterDataContext();
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null & itemToaNha.EditValue !=null)
                {
                    var maTN = (byte)itemToaNha.EditValue;
                    var _TuNgay = (DateTime)itemTuNgay.EditValue;
                    var _DenNgay = (DateTime)itemDenNgay.EditValue;

                        gcTheXe.DataSource = 
                            (from p in db.dvgxTheXes
                             join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                             //join mb in db.mbMatBangs on p.MaMB equals mb.MaMB
                             join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH into dskh
                             from kh in dskh.DefaultIfEmpty()
                            where p.MaTN == maTN & p.IsThangMay == true
                            & SqlMethods.DateDiffDay(_TuNgay, p.NgayNhap) >= 0
                            & SqlMethods.DateDiffDay(p.NgayNhap, _DenNgay) >= 0 
                            orderby p.NgayDK descending
                            select new
                            {
                                //STT = index + 1,
                                p.ID,
                                p.SoThe,
                                p.NgayDK,
                                p.ChuThe,
                                p.PhiLamThe,
                                p.NgungSuDung,
                                p.NgayNgungSD,
                                p.GiaThang,
                                p.KyTT,
                                p.TienTT,
                                p.NgayTT,
                                p.DienGiai,
                                nvn.HoTenNV,
                                p.mbMatBang.MaSoMB,
                                TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                IsTichHop = (p.IsThangMay.GetValueOrDefault() & p.IsTheXe.GetValueOrDefault())
                            }).ToList();
                }
                else
                {
                    gcTheXe.DataSource = null;
                }
            }
            catch { }
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
            int[] indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                var objTM = db.dvgxTheXes.Single(p => p.ID == (int)grvTheXe.GetRowCellValue(i, "ID"));
                db.dvgxTheXes.DeleteOnSubmit(objTM);
            }

            db.SubmitChanges();

            grvTheXe.DeleteSelectedRows();
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
            if (grvTheXe.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, MaThe = (int?)grvTheXe.GetFocusedRowCellValue("ID")})
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThanhToan frmtt = new frmThanhToan();
            frmtt.objdvtm = db.dvtmTheThangMays.Single(p => p.ID == (int)grvTheXe.GetFocusedRowCellValue("ID"));
            frmtt.ShowDialog();

            if (frmtt.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void grvTheXe_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvTheXe.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void btn2Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    DataTable dt = new DataTable();

                    var ts = db.dvtmThanhToanThangMays
                                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.ThangThanhToan) >= 0
                                    & SqlMethods.DateDiffDay(p.ThangThanhToan, denNgay) >= 0)
                                .OrderBy(p => p.dvtmTheThangMay.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.dvtmTheThangMay.mbMatBang.MaSoMB,
                                    KhachHang = p.dvtmTheThangMay.MaKH.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.HasValue ? (p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? p.dvtmTheThangMay.tnKhachHang.HoKH + " " + p.dvtmTheThangMay.tnKhachHang.TenKH : p.dvtmTheThangMay.tnKhachHang.CtyTen) : "") : "",
                                    p.dvtmTheThangMay.ChuThe,
                                    p.dvtmTheThangMay.SoThe,
                                    p.dvtmTheThangMay.PhiLamThe,
                                    Thang = string.Format("{0} / {1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                                    ThanhToan = p.DaTT.Value ? "Đã thanh toán" : "Chưa thanh toán",
                                    TrangThaiNhacNo = p.dvTrangThaiNhacNo.TenTT
                                });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách tài sản", dt);
                }
            }
        }

        private void grvTheXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvTheXe.FocusedRowHandle < 0) return;
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcTheXe);
        }

    }
}