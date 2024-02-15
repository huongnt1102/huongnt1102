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

namespace KyThuat.ThamQuan
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
            db = new MasterDataContext();
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcThamQuan.DataSource = db.tqThamQuans
                    .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTQ.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.NgayTQ.Value, denNgay) >= 0)
                    .OrderByDescending(p => p.NgayTQ).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.MaTQ,
                        p.NgayTQ,
                        p.MaSoTQ,
                        p.DienGiai,
                        p.tnNhanVien.HoTenNV,
                        TenTN = p.tnToaNha == null ? null : p.tnToaNha.TenTN,
                        TenKN = p.mbKhoiNha == null ? null : p.mbKhoiNha.TenKN,
                        TenTL = p.mbTangLau == null ? null : p.mbTangLau.TenTL,
                        MaSoMB = p.mbMatBang == null ? null : p.mbMatBang.MaSoMB
                    }).ToList();
                }
                else
                {
                    gcThamQuan.DataSource = db.tqThamQuans
                    .Where(p => p.MaTN == objnhanvien.MaTN &
                            SqlMethods.DateDiffDay(tuNgay, p.NgayTQ.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.NgayTQ.Value, denNgay) >= 0)
                    .OrderByDescending(p => p.NgayTQ).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.MaTQ,
                        p.NgayTQ,
                        p.MaSoTQ,
                        p.DienGiai,
                        p.tnNhanVien.HoTenNV,
                        TenTN = p.tnToaNha == null ? null : p.tnToaNha.TenTN,
                        TenKN = p.mbKhoiNha == null ? null : p.mbKhoiNha.TenKN,
                        TenTL = p.mbTangLau == null ? null : p.mbTangLau.TenTL
                    }).ToList();
                }
            }
            else
            {
                gcThamQuan.DataSource = null;
            }
        }

        void LoadDetail()
        {
            if (grvThamQuan.FocusedRowHandle >= 0)
            {
                var maTQ = grvThamQuan.GetFocusedRowCellValue("MaTQ").ToString();

                gcTaiSan.DataSource = db.tqTaiSans.Where(p => String.Compare(p.MaTQ, maTQ, false) == 0)
                    .Select(p => new
                    {
                        p.MaTS,
                        TenTS = p.MaTS == null ? "" : p.tsTaiSan.TenTS,
                        TenLTS = p.MaTS == null ? "" : p.tsTaiSan.tsLoaiTaiSan.TenLTS,
                        TenTT = p.MaTT == null ? "" : p.tsTrangThai.TenTT,
                        p.DienGiai,
                        XuLy = (p.IsCreateWork == true ? "Đã tạo công việc" : "Chưa xử lý")
                    }).ToList();
            }
            else
            {
                gcTaiSan.DataSource = null;
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var maTQs = "|";
            int[] indexs = grvThamQuan.GetSelectedRows();

            foreach (int i in indexs)
                maTQs += grvThamQuan.GetRowCellValue(i, "MaTQ") + "|";

            db.tqTaiSans.DeleteAllOnSubmit(db.tqTaiSans.Where(p => SqlMethods.Like(maTQs, String.Format("%{0}%", p.MaTQ))));
            db.tqThamQuans.DeleteAllOnSubmit(db.tqThamQuans.Where(p => SqlMethods.Like(maTQs, String.Format("%{0}%", p.MaTQ))));
            db.SubmitChanges();

            grvThamQuan.DeleteSelectedRows();
            LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvThamQuan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objTQ = db.tqThamQuans.Single(p => String.Compare(p.MaTQ, grvThamQuan.GetFocusedRowCellValue("MaTQ").ToString(), false) == 0) })
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

        private void grvThamQuan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void btnTaoCV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> ListTS = new List<int>();
            if (grvThamQuan.FocusedRowHandle < 0)
            {
                btnTaoCV.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                return;
            }

            if (grvTaiSan.FocusedRowHandle < 0)
            {
                btnTaoCV.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                return;
            }
            //int[] List = grvTaiSan.GetSelectedRows();
            foreach (var p in grvTaiSan.GetSelectedRows())
            {
                 var obj = db.tqTaiSans.Where(q => q.MaTQ == grvThamQuan.GetFocusedRowCellValue("MaTQ").ToString() & q.MaTS == (int)grvTaiSan.GetRowCellValue(p, "MaTS")).SingleOrDefault();
                 if (obj.IsCreateWork == true)
                 {
                     DialogBox.Alert("Có tài sản đã tạo công việc trong danh sách chọn. Vui lòng kiểm tra lại!");
                     return;
                 }
                ListTS.Add((int)grvTaiSan.GetRowCellValue(p,"MaTS"));
            }

            KyThuat.DauMucCongViec.frmEdit frm = new DauMucCongViec.frmEdit();
            frm.NguonCV = 1;
            frm.MaTQ = grvThamQuan.GetFocusedRowCellValue("MaTQ").ToString();
            frm.ListTS = ListTS;
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
            if (frm.IsSave)
            {
                foreach (var p in grvTaiSan.GetSelectedRows())
                {
                    var obj = db.tqTaiSans.Where(q => q.MaTQ == grvThamQuan.GetFocusedRowCellValue("MaTQ").ToString() & q.MaTS == (int)grvTaiSan.GetRowCellValue(p, "MaTS")).SingleOrDefault();
                    obj.IsCreateWork = true;
                    db.SubmitChanges();
                }
                LoadDetail();
            }

        }
    }
}