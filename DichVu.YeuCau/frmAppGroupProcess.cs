using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.YeuCau
{
    public partial class FrmAppGroupProcess: XtraForm
    {
        private MasterDataContext _db=new MasterDataContext();

        public FrmAppGroupProcess()
        {
            InitializeComponent();
        }

        private void frmManagerAppGroupProcess_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gridControl1.DataSource = _db.app_GroupProcesses;
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmAppGroupProcessEdit())
            {
                frm.Id = 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gridView1.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Nhóm Công Việc], xin cảm ơn.");
                return;
            }

            using (var frm = new FrmAppGroupProcessEdit())
            {
                frm.Id = id;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gridView1.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Nhóm công việc], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            try
            {
                _db = new MasterDataContext();
                _db.app_GroupProcesses.DeleteOnSubmit(_db.app_GroupProcesses.First(_ => _.Id == id));
                _db.SubmitChanges();

                LoadData();
                DialogBox.Alert("Đã xóa thành công.");
            }
            catch
            {
                DialogBox.Error("Xóa không thành công, vui lòng liên hệ kỹ thuật.");
            }
        }
    }
}