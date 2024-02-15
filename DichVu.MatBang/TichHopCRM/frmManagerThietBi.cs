using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.MatBang.TichHopCRM
{
    public partial class frmManagerThietBi : XtraForm
    {
        private MasterDataContext _db=new MasterDataContext();

        public frmManagerThietBi()
        {
            InitializeComponent();
        }

        private void frmManagerThietBi_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            repNhanVien.DataSource = _db.tnNhanViens;
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gridControl1.DataSource = _db.mbMatBang_ThietBis;
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmThietBiEdit())
            {
                frm.ID = 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (int?)gridView1.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Thiết Bị], xin cảm ơn.");
                return;
            }

            using (var frm = new frmThietBiEdit())
            {
                frm.ID = _ID;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (int?)gridView1.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Thiết bị], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            try
            {
                _db = new MasterDataContext();
                _db.mbMatBang_ThietBis.DeleteOnSubmit(_db.mbMatBang_ThietBis.First(_ => _.ID == _ID));
                _db.SubmitChanges();

                LoadData();
                DialogBox.Alert("Đã xóa thành công");
            }
            catch
            {
                DialogBox.Error("Xóa không thành công, vui lòng liên hệ kỹ thuật.");
            }
        }
    }
}