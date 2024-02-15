using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Checklist
{
    public partial class FrmChecklistDetailsEdit : XtraForm
    {
        public int? Id { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_ChecklistDetail _checklistDetails;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmChecklistDetailsEdit()
        {
            InitializeComponent();
        }

        private void FrmChecklistDetailsEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            itemClearText.ItemClick += ItemClearText_ItemClick;
            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;

            glkBlockItem.Properties.DataSource = _db.ho_BlockItems;
            glkGroupItem.Properties.DataSource = _db.ho_GroupItems;

            _checklistDetails = new ho_ChecklistDetail();
            chkIsNotUse.Checked = false;

            if (Id == null) return;

            _checklistDetails = _db.ho_ChecklistDetails.FirstOrDefault(_ => _.Id == Id);
            if (_checklistDetails == null) return;

            memoGroup.Text = _checklistDetails.GroupName;
            memoName.Text = _checklistDetails.Name;
            memoDescription.Text = _checklistDetails.Description;
            memoFloor.Text = _checklistDetails.Stt;
            chkIsNotUse.Checked = _checklistDetails.IsNotUse ?? false;
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Id == null) _db.ho_ChecklistDetails.InsertOnSubmit(_checklistDetails);
                _checklistDetails.GroupName = memoGroup.Text;
                _checklistDetails.Name = memoName.Text;
                _checklistDetails.IsNotUse = chkIsNotUse.Checked;
                _checklistDetails.Description = memoDescription.Text;
                _checklistDetails.Stt = memoFloor.Text;
                _db.SubmitChanges();
                DialogBox.Success();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                DialogBox.Error("Lưu bị lỗi");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GlkGroupItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;

                memoGroup.Text = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch{}
        }

        private void GlkBlockItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;

                memoFloor.Text = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch { }
        }
    }
}