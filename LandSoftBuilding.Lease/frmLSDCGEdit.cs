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
namespace LandSoftBuilding.Lease
{
    public partial class frmLSDCGEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCT { get; set; }
        public int? MaHD { get; set; }
        public bool IsSave { get; set; }

        #region Param trả về
        public DateTime? NgayDieuChinh { get; set; }
        public int? PhanLoai { get; set; }
        public decimal? GiaTriCu { get; set; }
        public decimal? TyLeDieuChinh { get; set; }
        public decimal? GiaTriMoi { get; set; }
        public string DienGiai { get; set; }
        #endregion

        MasterDataContext db = new MasterDataContext();
        
        public frmLSDCGEdit()
        {
            InitializeComponent();
        }

        private void itemSubmit_Click(object sender, EventArgs e)
        {
            if (spGiaTriCu.Value == spGiaTriMoi.Value)
            {
                DialogBox.Error("Vui lòng nhập thông tin điều chỉnh giá");
                return;
            }

            if (DialogBox.Question("Bạn có muốn thực hiện không?") == System.Windows.Forms.DialogResult.No) return;

            var _MaPL = (int)lkPhanLoai.EditValue;

            NgayDieuChinh = dateNgayDC.DateTime;
            PhanLoai = _MaPL;
            //GiaTriCu = spGiaTriCu.Value;
            //TyLeDieuChinh = spTyLeDC.Value;
            GiaTriMoi = spGiaTriMoi.Value;
            DienGiai = txtDienGiai.Text;

            
            IsSave = true;
            this.Close();
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLSDCGEdit_Load(object sender, EventArgs e)
        {
            if (this.MaCT != null)
                lkPhanLoai.Properties.DataSource = (from pl in db.ctLoaiDieuChinhs where pl.ID != 4 select new { pl.ID, pl.TenPL }).ToList();
            else
                lkPhanLoai.Properties.DataSource = (from pl in db.ctLoaiDieuChinhs where pl.ID == 4 select new { pl.ID, pl.TenPL }).ToList();
            dateNgayDC.EditValue = DateTime.Now;
            lkPhanLoai.ItemIndex = 0;
        }

        private void lkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spTyLeDC.EditValue = 0;

                if (this.MaCT != null)
                {
                    decimal? _GiaTriCu;
                    var objCT = db.ctChiTiets.Single(p => p.ID == this.MaCT);
                    this.MaHD = objCT.MaHDCT;
                    switch ((int)lkPhanLoai.EditValue)
                    {
                        case 1:
                            _GiaTriCu = objCT.DonGia;
                            break;
                        case 2:
                            _GiaTriCu = objCT.PhiDichVu;
                            break;
                        default:
                            _GiaTriCu = objCT.DienTich;
                            break;
                    }

                    spGiaTriCu.EditValue = spGiaTriMoi.EditValue = _GiaTriCu;
                }
                else
                {
                    var objHD = db.ctHopDongs.Single(p => p.ID == this.MaHD);

                    spGiaTriCu.EditValue = objHD.TyGia;
                    spGiaTriMoi.EditValue = db.LoaiTiens.Single(p => p.ID == objHD.MaLT).TyGia;
                }
            }
            catch { }
        }

        private void spTyLeDC_EditValueChanged(object sender, EventArgs e)
        {
            spGiaTriMoi.EditValue = spGiaTriCu.Value * (1 + spTyLeDC.Value);
        }
    }
}