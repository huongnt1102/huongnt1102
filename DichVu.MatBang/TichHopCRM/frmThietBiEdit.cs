using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.MatBang.TichHopCRM
{
    public partial class frmThietBiEdit : XtraForm
    {
        public int? ID;

        private MasterDataContext _db;
        private mbMatBang_ThietBi _o;

        public frmThietBiEdit()
        {
            InitializeComponent();
        }

        private void frmThietBiEdit_Load(object sender, EventArgs e)
        {
            _db = new MasterDataContext();
            _o = new mbMatBang_ThietBi();

            chkNgungSuDung.Checked = false;

            if (ID != null & ID != 0)
            {
                _o = _db.mbMatBang_ThietBis.FirstOrDefault(_ => _.ID == ID);
                if (_o != null)
                {
                    txtMaThietBi.Text = _o.MaThietBi;
                    txtTenThietBi.Text = _o.TenThietBi;
                    if (_o.NgungSD != null) chkNgungSuDung.Checked = _o.NgungSD.Value;
                }
                else
                    _o = new mbMatBang_ThietBi();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _o.MaThietBi = txtMaThietBi.Text;
            _o.TenThietBi = txtTenThietBi.Text;
            _o.NgungSD = chkNgungSuDung.Checked;

            if (ID != null & ID != 0)
            {
                _o.NguoiSua = Common.User.MaNV;
                _o.NgaySua = DateTime.Now;
            }
            else
            {
                _o.NguoiNhap = Common.User.MaNV;
                _o.NgayNhap = DateTime.Now;
                _db.mbMatBang_ThietBis.InsertOnSubmit(_o);
            }

            try
            {
                _db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }
    }
}