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
using System.Net;
using DevExpress.XtraGrid.Views.Grid;
using FTP;
using Building.AppVime;
using System.IO;

namespace DichVu.YeuCau.BieuDo.MotToaNha
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        bool first = true;
        public byte? MaTN;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public int? SoSao;
        public int? DoUuTien;
        public int? MaTinhTrang;
        public int? NguonDen;
        public int? MaNhomCongViec;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }


        void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                db = new MasterDataContext();
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                gcYeuCau.DataSource = (from p in db.tnycYeuCaus
                                       join ncv in db.app_GroupProcesses on p.GroupProcessId equals ncv.Id into ncviec
                                       from ncv in ncviec.DefaultIfEmpty()
                                       join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                                       from nv in nvs.DefaultIfEmpty()
                                       join ntn in db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                                       from ntn in ntns.DefaultIfEmpty()
                                       where SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                       & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                       & p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN
                                       &(SoSao==null || p.Rating==(decimal?)SoSao) & (DoUuTien==null || p.MaDoUuTien==DoUuTien) & (MaTinhTrang == null || p.MaTT == MaTinhTrang) & ( NguonDen == null || p.MaNguonDen == NguonDen) && (MaNhomCongViec == null || p.GroupProcessId == MaNhomCongViec)
                                       orderby p.NgayYC descending
                                       select new
                                       {
                                           p.ID,
                                           p.MaYC,
                                           p.NgayYC,
                                           p.TieuDe,
                                           p.NoiDung,
                                           p.Rating,
                                           p.RatingComment,
                                           p.tnycTrangThai.TenTT,
                                           p.tnPhongBan.TenPB,
                                           TenKH = p.NguoiGui,
                                           HoTenNTN = ntn.HoTenNV,
                                           HoTenNV = nv.HoTenNV,
                                           p.tnycTrangThai.MauNen,
                                           p.tnycDoUuTien.TenDoUuTien,
                                           p.mbMatBang.MaSoMB,
                                           p.tnycNguonDen.TenNguonDen,
                                           TenNhomCongViec = ncv.Name,
                                           ThoiGian =
                                           (from ct in db.tnycLichSuCapNhats

                                            orderby ct.NgayCN descending
                                            where ct.MaYC == p.ID
                                            select new
                                            {

                                                ct.NgayCN,

                                            }).FirstOrDefault().NgayCN == null ? 0 : SqlMethods.DateDiffHour(p.NgayYC, (from ct in db.tnycLichSuCapNhats

                                                                                                                        orderby ct.NgayCN descending
                                                                                                                        where ct.MaYC == p.ID
                                                                                                                        select new
                                                                                                                        {

                                                                                                                            ct.NgayCN,

                                                                                                                        }).FirstOrDefault().NgayCN),
                                           NgayXL = (from ct in db.tnycLichSuCapNhats

                                                     orderby ct.NgayCN descending
                                                     where ct.MaYC == p.ID & ct.MaTT == 3
                                                     select new
                                                     {

                                                         ct.NgayCN,

                                                     }).FirstOrDefault().NgayCN,
                                           NgayDong = (from ct in db.tnycLichSuCapNhats

                                                       orderby ct.NgayCN descending
                                                       where ct.MaYC == p.ID & ct.MaTT == 5
                                                       select new
                                                       {

                                                           ct.NgayCN,

                                                       }).FirstOrDefault().NgayCN
                                       }).ToList();
            }
            else
            {
                gcYeuCau.DataSource = null;
            }

            grvYeuCau.BestFitColumns();
        }
        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = MaTN;
            itemTuNgay.EditValue = TuNgay;
            itemDenNgay.EditValue = DenNgay;
            LoadData();
            first = false;
            grvYeuCau.BestFitColumns();
        }
        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvYeuCau.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu cần xóa!");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            List<tnycYeuCau> ListXoa = new List<tnycYeuCau>();
            foreach (int i in indexs)
            {
                var objyc = db.tnycYeuCaus.Single(p => p.ID == (int)grvYeuCau.GetRowCellValue(i, "ID"));
                if (objyc.MaTT > 1)
                {
                    DialogBox.Alert("Bạn chỉ có thể xóa các yêu cầu trong tình trạng [Yêu cầu mới]. Vui lòng kiểm tra lại!");
                    return;
                }
                ListXoa.Add(objyc);
            }
            db.tnycYeuCaus.DeleteAllOnSubmit(ListXoa);
            db.SubmitChanges();

            grvYeuCau.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu");
                return;
            }

            using (frmEdit frm = new frmEdit())
            {
                frm.ID = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                frm.MaTN = (byte?)itemToaNha.EditValue ?? Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit())
            {
                frm.MaTN = (byte?)itemToaNha.EditValue ?? Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnPhanHoiYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu");
                return;
            }

            using (frmXuLyYC frm = new frmXuLyYC())
            {
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                frm.MaYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                frm.MaTN = maTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemXuLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region
            //if (grvYeuCau.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn yêu cầu");
            //    return;
            //}
            //var frm = new ToaNha.NhacViec_ThongBao.frmNhacViecEdit();
            //frm.objnhanvien = objnhanvien;
            //frm.MaYC = (int)grvYeuCau.GetFocusedRowCellValue("ID");
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //     ClickYC();
            #endregion
        }

       



        string getNewMaBT()
        {
            string MaBT = "";
            db.btDauMucCongViec_getNewMaBT(ref MaBT);
            return db.DinhDang(32, int.Parse(MaBT));
        }

    }
}