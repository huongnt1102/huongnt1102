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
    public partial class frmDieuChinhTienCoc : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPL { get; set; }
        public int? MaHD { get; set; }

        ctHopDong objHD;
        ctPhuLuc objPL;

        MasterDataContext db = new MasterDataContext();

        public frmDieuChinhTienCoc()
        {
            InitializeComponent();
        }

        void ckb_EditValueChanged(object sender, EventArgs e)
        {
        }

        void Load_LichThanhToan()
        {
            var ltLTT = db.ctLichThanhToans.Where(o => o.MaHD == MaHD).GroupBy(o => new { o.DotTT, o.TuNgay, o.DenNgay }).Select(o => new { o.Key.DotTT, o.Key.TuNgay, o.Key.DenNgay });
            glkKyTT_From.Properties.DataSource = ltLTT;
            glkKyTT_To.Properties.DataSource = ltLTT;
        }

        private void itemSubmit_Click(object sender, EventArgs e)
        {
            objPL.SoPL = txtSoPL.Text;
            objPL.NgayPL = dateNgayDC.DateTime;

            for (int i = 0; i < gvTienCoc.RowCount; i++)
            {
                gvTienCoc.SetRowCellValue(i, "MaHD", this.MaHD);
                gvTienCoc.SetRowCellValue(i, "MaLDV", 4);
            }

            objPL.TienCoc_new = objHD.ctLichThanhToans.Where(o => o.MaLDV == 4).Sum(o => o.SoTien).GetValueOrDefault();

            gvTienCoc.FocusedRowHandle = -1;

           db.SubmitChanges();
            this.Close();
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
                objPL.LoaiPL = (int?)PhuLuc.PhuLucCls.LoaiPhuLuc.DieuChinhCoc;
                objPL.MaHD = MaHD;
                objPL.MaNVN = Common.User.MaNV;
                objPL.NgayNhap = DateTime.Now;
                objPL.TienCoc = objHD.ctLichThanhToans.Where(o => o.MaHD == this.MaHD & o.MaLDV == 4).Sum(o => o.SoTien);
                db.ctPhuLucs.InsertOnSubmit(objPL);
            }

            gcTienCoc.DataSource = (from p in db.ctLichThanhToans
                                where p.MaHD == this.MaHD
                                & p.MaLDV == 4
                                select p);

            Load_LichThanhToan();
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
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void dateDenNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        }

        private void dateTuNgay_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        }

        private void cmbMatBang_EditValueChanged(object sender, EventArgs e)
        {
        }
    }
}