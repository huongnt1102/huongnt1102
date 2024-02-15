using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Building.Asset.VanHanh
{
    public partial class frmPhieuVanHanh_Edit_StatusLevel : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? IsSua { get; set; } 
        public int? Id { get; set; }

        private MasterDataContext _db;
        private tbl_PhieuVanHanh _o;

        public frmPhieuVanHanh_Edit_StatusLevel()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            glkStatusLevel.Properties.DataSource = _db.tbl_PhieuVanHanh_Status_Levels;
            glkStatusLevel.EditValue = glkStatusLevel.Properties.GetKeyValue(0);

            if (IsSua == null || IsSua == 0)
            {
                return;
            }
            else
            {
                _o = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == Id);
                if (_o != null)
                {
                    glkStatusLevel.EditValue = _o.StatusLevelID ?? glkStatusLevel.Properties.GetKeyValue(0);
                    txtSoPhieu.Text = _o.SoPhieu;
                }
            }
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                #region kiểm tra

                if (glkStatusLevel.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn mức độ.");
                    return;
                }

                #endregion

                _o.StatusLevelID = (int) glkStatusLevel.EditValue;

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
    }
}