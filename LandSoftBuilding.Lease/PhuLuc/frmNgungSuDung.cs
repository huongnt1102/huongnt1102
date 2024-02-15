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
using DevExpress.XtraGrid.Views.Grid;
namespace LandSoftBuilding.Lease
{
    public partial class frmNgungSuDung : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPL { get; set; }
        public int? MaHD { get; set; }

        ctHopDong objHD;
        ctPhuLuc objPL;

        MasterDataContext db = new MasterDataContext();

        public frmNgungSuDung()
        {
            InitializeComponent();
            ckb.EditValueChanged += ckb_EditValueChanged;
        }

        void ckb_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ckb = sender as CheckEdit;

            gvLTT.SetFocusedRowCellValue("IsCheck", ckb.Checked);
            GetGiaThue();
        }

        void Load_LichThanhToan()
        {
            var ltLTT = db.ctLichThanhToans.Where(o => o.MaHD == MaHD).GroupBy(o => new { o.DotTT, o.TuNgay, o.DenNgay }).Select(o => new { o.Key.DotTT, o.Key.TuNgay, o.Key.DenNgay });
            glkKyTT_From.Properties.DataSource = ltLTT;
            glkKyTT_To.Properties.DataSource = ltLTT;
        }

        private void itemSubmit_Click(object sender, EventArgs e)
        {
            DateTime? tuNgay = (DateTime?)glkKyTT_From.EditValue;
            DateTime? denNgay = (DateTime?)glkKyTT_To.EditValue;


            objPL.SoPL = txtSoPL.Text;
            objPL.NgayPL = dateNgayDC.DateTime;
            objPL.TuNgay = tuNgay;
            objPL.DenNgay = denNgay;

            // Lấy danh sách mặt bằng áp dụng
            for (int i = 0; i < gvLTT.RowCount; i++)
            {
                var item = (ctChiTiet)gvLTT.GetRow(i);

                if (item.NgungSuDung.GetValueOrDefault())
                {


                    // Lưu chi tiết phụ lục
                    var ctpl = new ctPhuLuc_ChiTiet();
                    ctpl.MaMB = item.MaMB;
                    objPL.ctPhuLuc_ChiTiets.Add(ctpl);

                    // Điều chỉnh lịch thanh toán
                    var ltLTT = db.ctLichThanhToans.Where(o => SqlMethods.DateDiffDay(tuNgay, o.TuNgay) >= 0
                                                                    & SqlMethods.DateDiffDay(o.DenNgay, denNgay) >= 0
                                                                    & o.MaMB == item.MaMB
                                                                    & o.MaHD == objHD.ID
                                                                    & !o.IsKhuyenMai.GetValueOrDefault());

                    foreach (var ltt in ltLTT)
                        ltt.NgungSuDung = true;
                }

                db.SubmitChanges();
                this.Close();

            }
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLSDCGEdit_Load(object sender, EventArgs e)
        {
            objHD = db.ctHopDongs.Single(o => o.ID == MaHD);
            objPL = db.ctPhuLucs.SingleOrDefault(o => o.MaPL == MaPL);

            if (objPL == null)
            {
                //txtSoPL.EditValue = LeaseScheduleCls.TaoSoCT_PhuLuc(MaHD);
                dateNgayDC.EditValue = DateTime.Now;

                objPL = new ctPhuLuc();
                objPL.LoaiPL = (int)PhuLuc.PhuLucCls.LoaiPhuLuc.NgungSuDung;
                objPL.MaHD = MaHD;
                objPL.MaNVN = Common.User.MaNV;
                objPL.NgayNhap = DateTime.Now;
                db.ctPhuLucs.InsertOnSubmit(objPL);
            }

            lkMatBang.DataSource = db.mbMatBangs.Where(o => o.MaTN == objHD.MaTN);

            gcLTT.DataSource = (from p in db.ctChiTiets
                                where p.MaHDCT == this.MaHD
                                & !p.NgungSuDung.GetValueOrDefault()
                                select p);

            Load_LichThanhToan();
        }

        void GetGiaThue()
        {
            List<decimal?> ltGiaThue = new List<decimal?>();

            for (int i = 0; i < gvLTT.RowCount; i++)
            {
                if ((bool?)gvLTT.GetRowCellValue(i, "IsCheck") == true)
                    ltGiaThue.Add((decimal?)gvLTT.GetRowCellValue(i, "DonGia"));
            }

            var giaThue = ltGiaThue.Distinct();

        }

        private void lkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void spTyLeDC_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void gvLTT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle >= 0 & (bool?)view.GetRowCellValue(e.RowHandle, "IsCheck") == true)
            {
                 e.Appearance.BackColor = Color.NavajoWhite;
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void dateDenNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void dateTuNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            gvLTT.RefreshData();
        }

        private void cmbMatBang_EditValueChanged(object sender, EventArgs e)
        {
        }
    }
}