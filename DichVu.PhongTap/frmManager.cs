using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Data;
//using ReportMisc.DichVu;

namespace DichVu.PhongTap
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
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcThePhongTap.DataSource = db.dvPhongTaps
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDK.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayDK.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayDK)
                        .Select(p => new
                        {
                            p.ID,
                            p.SoThe,
                            p.NgayDK,
                            p.PhiLamThe,
                            p.PhiHangThang,
                            p.DienGiai,
                            TenKH = p.tnNhanKhau.HoTenNK,
                            p.NgayHetHan,
                            p.IsInUse,
                            p.dvPhongTap_Loai.TenLoai,
                            p.tnNhanKhau.mbMatBang.MaSoMB,
                            ThoiGianConLai = SqlMethods.DateDiffDay(db.GetSystemDate(),p.NgayHetHan),
                            p.TinhTrangSucKhoe,
                            p.LienLacKhanCap
                        });
                }
                else
                {
                    gcThePhongTap.DataSource = db.dvPhongTaps
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDK.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayDK.Value, denNgay) >= 0 &
                                p.tnNhanKhau.mbMatBang.mbTangLau.mbKhoiNha.MaTN == objnhanvien.MaTN)
                        .OrderByDescending(p => p.NgayDK)
                        .Select(p => new
                        {
                            p.ID,
                            p.SoThe,
                            p.NgayDK,
                            p.PhiLamThe,
                            p.PhiHangThang,
                            p.DienGiai,
                            TenKH = p.tnNhanKhau.HoTenNK,
                            p.NgayHetHan,
                            p.IsInUse,
                            p.dvPhongTap_Loai.TenLoai,
                            p.tnNhanKhau.mbMatBang.MaSoMB,
                            ThoiGianConLai = SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayHetHan),
                            p.TinhTrangSucKhoe,
                            p.LienLacKhanCap
                        });
                }
            }
            else
            {
                gcThePhongTap.DataSource = null;
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
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
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
            int[] indexs = grvThePhongTap.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                var objTX = db.dvPhongTaps.Single(p => p.ID == (int)grvThePhongTap.GetRowCellValue(i, "ID"));
                db.dvPhongTaps.DeleteOnSubmit(objTX);
            }

            db.SubmitChanges();

            grvThePhongTap.DeleteSelectedRows();
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
            if (grvThePhongTap.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ");
                return;
            }
            var objpt = db.dvPhongTaps.Single(p => p.ID == (int)grvThePhongTap.GetFocusedRowCellValue("ID"));
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objPT = objpt })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnthanhtoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvThePhongTap.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;
            }
            //using (frmThanhToan frm = new frmThanhToan())
            //{
            //    frm.objthexe = db.dvgxTheXes.Single(p => p.ID == (int)grvThePhongTap.GetFocusedRowCellValue("ID"));
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //        LoadData();
            //}
        }

        private void grvTheXe_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvThePhongTap.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
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

                    var ts = db.dvPhongTaps
                                    .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayDK.Value) >= 0 &
                                        SqlMethods.DateDiffDay(p.NgayDK.Value, denNgay) >= 0 &
                                        p.tnNhanKhau.mbMatBang.mbTangLau.mbKhoiNha.MaTN == objnhanvien.MaTN)
                                    .OrderByDescending(p => p.NgayDK).AsEnumerable()
                                    .Select(p => new
                                    {
                                        p.SoThe,
                                        p.NgayDK,
                                        CuDan = p.tnNhanKhau.HoTenNK,
                                        MatBang = p.tnNhanKhau.mbMatBang.MaSoMB,
                                        p.dvPhongTap_Loai.TenLoai,
                                        p.PhiLamThe,
                                        p.PhiHangThang,
                                        p.NgayHetHan,
                                        p.IsInUse,
                                        ThoiGianConLai = SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayHetHan),
                                        p.TinhTrangSucKhoe,
                                        p.LienLacKhanCap,
                                        p.DienGiai
                                    });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách sử dụng dịch vụ phòng tập", dt);
                }
            }
        }

        private void btnGiayBaoTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (ReportMisc.DichVu.GiuXe.Report.frmPrintControl frm = new ReportMisc.DichVu.GiuXe.Report.frmPrintControl(0, "", EnumIn.GiayBaoTong))
            //{
            //    frm.ShowDialog();
            //}
        }
    }
}