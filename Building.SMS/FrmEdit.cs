using System;
using System.Linq;
using System.Windows.Forms;
using Library;

namespace Building.SMS
{
    public partial class FrmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int FormId;
        private MasterDataContext _db = new MasterDataContext();
        private SmsTemplateDv _template;

        public FrmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            glkTyle.Properties.DataSource = _db.SmsTyles;
            if (FormId != 0)
            {
                try
                {
                    _template = _db.SmsTemplateDvs.SingleOrDefault(p => p.Id == FormId);
                    if (_template == null) return;
                    txtName.EditValue = _template.Name;
                    glkTyle.EditValue = _template.TyleId;
                    _template.UserUpdateId = Common.User.MaNV;
                    _template.UserUpdateName = Common.User.HoTenNV;
                    _template.DateUpdate = DateTime.UtcNow.AddHours(7);
                }
                catch {
                    Close();
                }
            }
            else
            {
                _template = new SmsTemplateDv();
                _template.DateCreate = DateTime.UtcNow.AddHours(7);
                _template.UserCreateId = Common.User.MaNV;
                _template.UserCreateName = Common.User.HoTenNV;
            }
        }

        private void txtTenBM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (var frm = new FrmDesign())
            {
                frm.TextContents = _template.Contents;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    _template.Contents = frm.TextContents;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Tên biểu mẫu], xin cảm ơn.");
                txtName.Focus();
                return;
            }
            else
            {
                var count = _db.SmsTemplateDvs.Count(p => p.Name == txtName.Text.Trim() & p.Id != FormId);
                if (count > 0)
                {
                    DialogBox.Error("Trùng [Tên biểu mẫu], vui lòng nhập lại");
                    txtName.Focus();
                    return;
                }
            }

            if (glkTyle.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại biểu mẫu], xin cảm ơn.");
                glkTyle.Focus();
                return;
            }

            _template.Name = txtName.Text.Trim();
            _template.TyleId = (int)glkTyle.EditValue;
            _template.TyleName = _db.SmsTyles.First(_ => _.Id == (int) glkTyle.EditValue).Name;

            if (FormId == 0)
            {
                _db.SmsTemplateDvs.InsertOnSubmit(_template);
            }

            _db.SubmitChanges();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmPreview {RtfText = _template.Contents};
                frm.ShowDialog(this);
            }
            catch { }
        }
    }
}