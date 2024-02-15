using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.YeuCau
{
    public partial class FrmAppGroupProcessEdit : XtraForm
    {
        public int? Id;

        private MasterDataContext _db;
        private app_GroupProcess _o;

        public FrmAppGroupProcessEdit()
        {
            InitializeComponent();
        }

        private void frmAppGroupProcessEdit_Load(object sender, EventArgs e)
        {
            _db = new MasterDataContext();
            _o = new app_GroupProcess();

            chkIsDefault.Checked = false;

            if (Id != null & Id != 0)
            {
                _o = _db.app_GroupProcesses.FirstOrDefault(_ => _.Id == Id);
                if (_o != null)
                {
                    txtName.Text = _o.Name;
                    if (_o.TimeFinish != null) spinTimeFinish.Value = (short) _o.TimeFinish;
                    if (_o.TimeReceive != null) spinTimeRecive.Value = (byte) _o.TimeReceive;
                    if (_o.TimeReply != null) spinTimeReply.Value = (byte) _o.TimeReply;
                    if (_o.IsDefault != null) chkIsDefault.Checked = _o.IsDefault.Value;
                }
                else
                    _o = new app_GroupProcess();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _o.Name = txtName.Text;
            _o.IsDefault = chkIsDefault.Checked;
            _o.TimeFinish = (short?) spinTimeFinish.Value;
            _o.TimeReceive = (byte?) spinTimeRecive.Value;
            _o.TimeReply = (byte?) spinTimeReply.Value;

            if (Id == null | Id == 0)
            {
                _db.app_GroupProcesses.InsertOnSubmit(_o);
            }

            try
            {
                _db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công.");
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại.");
            }
        }
    }
}