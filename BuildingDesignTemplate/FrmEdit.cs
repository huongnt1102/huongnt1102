using System;
using System.Data.Linq;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace BuildingDesignTemplate
{
    public partial class FrmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int FormId;
        private MasterDataContext _db = new MasterDataContext();
        private template_Form _objForm;

        public FrmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            glkFormGroup.Properties.DataSource = _db.rptGroups;
            if (FormId != 0)
            {
                try
                {
                    _objForm = _db.template_Forms.SingleOrDefault(p => p.Id == FormId);
                    if (_objForm == null) return;
                    txtFormName.EditValue = _objForm.Name;
                    glkFormGroup.EditValue = _objForm.GroupId;
                    txtDescription.EditValue = _objForm.Description;
                    ckbIsUseApartment.EditValue = _objForm.IsUseApartment;
                    lookUpAction.EditValue = _objForm.ActionId;
                    //txtTenLM.Text = _objForm.TenLM;

                    chkBarcode.Checked = _objForm.IsBarCode.GetValueOrDefault();
                    chkLogoCty.Checked = _objForm.IsPowerSign.GetValueOrDefault();

                    spinBottom.EditValue = _objForm.PaddingBottom ?? 0;
                    spinLeft.EditValue = _objForm.PaddingLeft ?? 0;
                    spinTop.EditValue = _objForm.PaddingTop ?? 0;
                    spinRight.EditValue = _objForm.PaddingRight ?? 0;

                    chkLogoDIP.Checked = _objForm.IsDIPLogo.GetValueOrDefault();
                    btnImageLogo.Text = chkLogoCty.Checked ? _objForm.LogoUrl : "";


                }
                catch {
                    Close();
                }
            }
            else
            {
                _objForm = new template_Form();
            }
        }

        private void txtTenBM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (var frm = new FrmDesign())
            {
                frm.RtfText = _objForm.Content;
                if (glkFormGroup.EditValue != null) frm.GroupId = (byte) glkFormGroup.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    _objForm.Content = frm.RtfText;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtFormName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Tên biểu mẫu], xin cảm ơn.");
                txtFormName.Focus();
                return;
            }
            else
            {
                var count = _db.template_Forms.Count(p => p.Name == txtFormName.Text.Trim() & p.Id != FormId);
                if (count > 0)
                {
                    DialogBox.Error("Trùng [Tên biểu mẫu], vui lòng nhập lại");
                    txtFormName.Focus();
                    return;
                }
            }

            if (glkFormGroup.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại biểu mẫu], xin cảm ơn.");
                glkFormGroup.Focus();
                return;
            }

            _objForm.Name = txtFormName.Text.Trim();
            _objForm.GroupId = (byte)glkFormGroup.EditValue;
            _objForm.Description = txtDescription.Text;
            _objForm.IsUseApartment = ckbIsUseApartment.Checked;
            _objForm.DateUpdate = DateTime.Now;
            _objForm.UserId = Common.User.MaNV;
            _objForm.ActionId = (int?)lookUpAction.EditValue;
            //_objForm.TenLM = txtTenLM.Text.Trim();

            _objForm.PaddingBottom = Convert.ToInt32(spinBottom.EditValue);
            _objForm.PaddingLeft = Convert.ToInt32(spinLeft.EditValue);
            _objForm.PaddingRight = Convert.ToInt32(spinRight.EditValue);
            _objForm.PaddingTop = Convert.ToInt32(spinTop.EditValue);

            _objForm.IsPowerSign = chkLogoCty.Checked;
            _objForm.IsBarCode = chkBarcode.Checked;
            _objForm.IsDIPLogo = chkLogoDIP.Checked;
            _objForm.LogoUrl = _objForm.IsPowerSign.GetValueOrDefault() ? btnImageLogo.Text : "";

            rptReport rpt;

            if (FormId == 0)
            {
                _db.template_Forms.InsertOnSubmit(_objForm);

                // insert report
                var idReport = _db.rptReports.Max(_ => _.ID);
                rpt = new rptReport {ID = idReport + 1, Name = txtFormName.Text.Trim()};

                _db.rptReports.InsertOnSubmit(rpt);

                _db.SubmitChanges();
            }
            else
            {
                rpt = _db.rptReports.FirstOrDefault(_ => _.ID == _objForm.ReportId);
                if (rpt != null)
                {
                    rpt.Name = txtFormName.Text.Trim();
                }
            }

            _objForm.ReportId = rpt.ID;
            rpt.GroupID = (byte)glkFormGroup.EditValue;
            _db.SubmitChanges();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmPreview {RtfText = _objForm.Content};
                frm.ShowDialog(this);
            }
            catch { }
        }

        private void btnImageLogo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var frm = new FTP.frmUploadFile();
            //if (frm.SelectFile(false))
            //{
            //    frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
            //    frm.ClientPath = frm.ClientPath;
            //    frm.ShowDialog();
            //    if (frm.DialogResult != DialogResult.OK) return;
            //    btnImageLogo.Text = frm.FileName;
            //}
            //frm.Dispose();
        }

        private void GlkFormGroup_EditValueChanged(object sender, EventArgs e)
        {
            lookUpAction.Properties.DataSource = _db.template_Actions.Where(p => p.GroupId == (byte)glkFormGroup.EditValue)
                .Select(p => new { p.Name, p.Id });
        }
    }
}