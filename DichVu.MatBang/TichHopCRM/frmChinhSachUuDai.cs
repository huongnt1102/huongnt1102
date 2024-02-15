using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.MatBang.TichHopCRM
{
    public partial class frmChinhSachUuDai : XtraForm
    {
        public int? ID;
        public int? MaMB;
        public byte? MaTN;

        private MasterDataContext _db;
        private mbMatBang_ChinhSachUuDai _o;

        public frmChinhSachUuDai()
        {
            InitializeComponent();
        }

        private void frmChinhSachUuDai_Load(object sender, EventArgs e)
        {
            _db = new MasterDataContext();
            _o = new mbMatBang_ChinhSachUuDai();

            dtNgayApDung.DateTime = DateTime.Now;
            dtThoiGianApDung.DateTime = DateTime.Now;
            dtThoiGianKetThuc.DateTime = DateTime.Now;

            if (ID != null & ID != 0)
            {
                _o = _db.mbMatBang_ChinhSachUuDais.FirstOrDefault(_ => _.ID == ID);
                if (_o != null)
                {
                    txtChinhSacUuDai.Text = _o.TenChinhSachUuDai;
                    txtThongTinUuDai.Text = _o.ThongTinUuDai;
                    txtGhiChu.Text = _o.GhiChu;
                    if (_o.NgayApDung != null) dtNgayApDung.DateTime = (DateTime) _o.NgayApDung;
                    if (_o.ThoiGianApDung != null) dtThoiGianApDung.DateTime = (DateTime) _o.ThoiGianApDung;
                    if (_o.ThoiGianKetThuc != null) dtThoiGianKetThuc.DateTime = (DateTime) _o.ThoiGianKetThuc;
                }
                else
                    _o = new mbMatBang_ChinhSachUuDai();

            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _o.TenChinhSachUuDai = txtChinhSacUuDai.Text;
            _o.ThongTinUuDai = txtThongTinUuDai.Text;
            _o.GhiChu = txtGhiChu.Text;
            _o.NgayApDung = dtNgayApDung.DateTime;
            _o.ThoiGianApDung = dtThoiGianApDung.DateTime;
            _o.ThoiGianKetThuc = dtThoiGianKetThuc.DateTime;
            _o.MaMB = MaMB;
            _o.MaTN = MaTN;

            if (ID == null | ID == 0)
            {
                _db.mbMatBang_ChinhSachUuDais.InsertOnSubmit(_o);
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