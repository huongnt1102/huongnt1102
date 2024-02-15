using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Building.Asset.PhanCong
{
    public partial class frmLichTruc_Edit : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? Id { get; set; }
        public int? IsSua { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? MaNv { get; set; }

        private MasterDataContext _db;

        public frmLichTruc_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            repNhanVien.DataSource = _db.tnNhanViens;
            repLoaiCa.DataSource = _db.tbl_PhanCong_PhanLoaiCas.Where(p => p.MaTN == MaTn); ;

            gc.DataSource = _db.tbl_PhanCong_NhanVienChiTiets.Where(_=>_.MaTN==MaTn & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay((DateTime)TuNgay,_.Ngay)>=0 & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.Ngay,(DateTime)DenNgay)>=0);
            if (IsSua == 0)
            {
                gc.DataSource = _db.tbl_PhanCong_NhanVienChiTiets.Where(_ => _.IDPhanLoaiCa == null & _.MaTN == MaTn & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay((DateTime)TuNgay, _.Ngay) >= 0 & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.Ngay, (DateTime)DenNgay) >= 0);
                if (MaNv != null & MaNv!=0)
                {
                    gc.DataSource = _db.tbl_PhanCong_NhanVienChiTiets.Where(_ => _.IDPhanLoaiCa == null & _.MaTN == MaTn & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay((DateTime)TuNgay, _.Ngay) >= 0 & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.Ngay, (DateTime)DenNgay) >= 0 & _.MaNV == MaNv);
                }
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {//
                gv.PostEditor();
                gv.UpdateCurrentRow();
                _db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");

            }
            catch (Exception)
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }

        private void gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null | id == 0) return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gv.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gv.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

    }
}