using System;
using Library;

namespace ToaNha.DanhMuc
{
    public partial class FrmDanhMucDuAn : DevExpress.XtraEditors.XtraForm
    {
        public string Loai { get; set; }

        MasterDataContext db = new Library.MasterDataContext();
        public FrmDanhMucDuAn()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void GetText()
        {
            switch (Loai)
            {
                case ToaNha.Class.DanhMucDuAnEnum.KHU_DAT: this.Text = ToaNha.Class.DanhMucDuAnEnum.KHU_DAT_TEXT; break;
                case ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG: this.Text = ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG_TEXT; break;
                case ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA: this.Text = ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA_TEXT; break;
                case ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA: this.Text = ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA_TEXT;break;
                case ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG: this.Text = ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG_TEXT;
                    break;
                case ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY: this.Text = ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY_TEXT;
                    break;
                case ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH: this.Text = ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH_TEXT;
                    break;
            }
        }

        private void frmChucVu_Load(object sender, EventArgs e)
        {
            GetText();
            LoadData();
        }

        private void LoadData()
        {
            switch (Loai)
            {
                case ToaNha.Class.DanhMucDuAnEnum.KHU_DAT: gc.DataSource = db.tnKhuDats; break;
                case ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG: gc.DataSource = db.tnHinhThucXayDungs ;break;
                case ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA: gc.DataSource = db.tnHangToaNhas; break;
                case ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA: gc.DataSource = db.tnNhomNhas;break;
                case ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG: gc.DataSource = db.tnDonViSuDungs;
                    break;
                case ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY: gc.DataSource = db.tnDonViQuanLies;
                    break;
                case ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH: gc.DataSource = db.tnCapCongTrinhs;
                    break;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                DialogBox.Alert("Lưu không thành công. Vui lòng thử lại sau");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}