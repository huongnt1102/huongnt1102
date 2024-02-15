using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace LandsoftBuildingGeneral.BieuMau
{
    public partial class frmEditor : DevExpress.XtraEditors.XtraForm
    {
        #region Constructor
        private int _MaBM = -1;
        public int MaBM
        {
            get { return _MaBM; }
            set { _MaBM = value; }
        }

        private bool _IsCreateNew = false;
        public bool IsCreateNew
        {
            get { return _IsCreateNew; }
            set { _IsCreateNew = value; }
        }
        public string TenBieuMau { get; set; }
        public string GhiChu { get; set; }
        #endregion
        public frmEditor()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void save()
        {
            Cursor currentcs = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            Library.MasterDataContext bmdc = new Library.MasterDataContext();

            Library.BmBieuMau bieumau = new Library.BmBieuMau();
            bieumau = bmdc.BmBieuMaus.Single(p => p.MaBM == MaBM);
            bieumau.Template = txteditor.RtfText;
            try
            {
                bmdc.SubmitChanges();
            }
            catch
            {
                Library.DialogBox.Error("Có lỗi xảy ra, không lưu được");
            }
        }

        private void btiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = Library.DialogBox.WaitingForm();
            if (IsCreateNew)
                saveNew();
            else
                save();
            wait.Close();
            wait.Dispose();
            MessageBox.Show("Dữ liệu cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            
        }

        private void btiDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Dữ liệu có thay đổi, bạn có muốn cập nhật lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                btiSave_ItemClick(sender, e);
            else
                this.Close();
        }

        private void frmEditor_Load(object sender, EventArgs e)
        {
            if (!IsCreateNew)
            {
                Library.MasterDataContext bm = new Library.MasterDataContext();
                Library.BmBieuMau bieumau = new Library.BmBieuMau();
                bieumau = bm.BmBieuMaus.Single(p => p.MaBM == this.MaBM);
                txteditor.RtfText = bieumau.Template;
            }
        }

        private void saveNew()
        {
            Cursor currentcs = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            using (Library.MasterDataContext bmdc = new Library.MasterDataContext())
            {
                Library.BmBieuMau bieumau = new Library.BmBieuMau { Template = txteditor.RtfText, TenBM = this.TenBieuMau, Description = this.GhiChu };
                bmdc.BmBieuMaus.InsertOnSubmit(bieumau);
                try
                {
                    bmdc.SubmitChanges();
                }
                catch
                {
                    Library.DialogBox.Error("Có lỗi xảy ra, không lưu được");
                }
            }
        }
        private void saveName()
        {
            Cursor currentcs = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            using (Library.MasterDataContext bmdc = new Library.MasterDataContext())
            {
                Library.BmBieuMau bieumau = new Library.BmBieuMau { TenBM = this.TenBieuMau, Description = this.GhiChu };
                bmdc.BmBieuMaus.InsertOnSubmit(bieumau);
                try
                {
                    bmdc.SubmitChanges();
                }
                catch
                {
                    Library.DialogBox.Error("Có lỗi xảy ra, không lưu được");
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmCreateNew frmnew = new frmCreateNew())
            {
                frmnew.ShowDialog();
            }
            saveName();
        }

        private void fileSaveItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btiSave_ItemClick(null, null);
        }
    }
}