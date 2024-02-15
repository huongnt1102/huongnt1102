using System;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Library;

namespace DichVu.BanGiaoMatBang.Asset
{
    public partial class FrmStatusAsset : XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmStatusAsset()
        {
            InitializeComponent();
        }

        private void FrmUnit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            gc.DataSource = _db.tbl_TinhTrangTaiSans;
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();
                DialogBox.Success();
            }
            catch
            {
                DialogBox.Error("Lưu dữ liệu lỗi");
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }
    }
}