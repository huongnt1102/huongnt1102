using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Building.Asset.BaoTri
{
    public partial class frmKeHoachBaoTri_ChinhNgay: XtraForm
    {
        public int? Id { get; set; }

        private MasterDataContext _db;
        private tbl_PhieuVanHanh _o;

        public frmKeHoachBaoTri_ChinhNgay()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            _db = new MasterDataContext();
            _o= _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == Id);
            if (_o != null)
            {
                if (_o.TuNgay != null)
                {
                    dateTuNgayCu.DateTime = (DateTime) _o.TuNgay;
                    dateTuNgayMoi.DateTime = (DateTime) _o.TuNgay;
                }

                if (_o.DenNgay != null)
                {
                    dateDenNgayMoi.DateTime = (DateTime) _o.DenNgay;
                    dateDenNgayCu.DateTime = (DateTime) _o.DenNgay;
                }
            }

            dateTuNgayMoi.Focus();
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

                if (_o != null)
                {
                    _o.TuNgay = dateTuNgayMoi.DateTime;
                    _o.DenNgay = dateDenNgayMoi.DateTime;
                }
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