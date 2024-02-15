using System.Linq;

namespace DichVu.GiuXe.DinhMuc
{
    public partial class FrmDinhMucEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTn { get; set; }
        public int? Id { get; set; }

        private int? MaMb { get; set; }
        private int? MaLmb { get; set; }
        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.dvgxDinhMuc _dinhMuc;

        public FrmDinhMucEdit()
        {
            InitializeComponent();
        }

        private void FrmDinhMucEdit_Load(object sender, System.EventArgs e)
        {
            _dinhMuc = GetDinhMucById(Id);
            if (_dinhMuc == null)
            {
                _dinhMuc = new Library.dvgxDinhMuc();
                _db.dvgxDinhMucs.InsertOnSubmit(_dinhMuc);
            }
            else
            {
                if (_dinhMuc.MaLX != null) glkLoaiXe.EditValue = (int?)_dinhMuc.MaLX;
                txtTenDinhMuc.Text = _dinhMuc.TenDM;
                if (_dinhMuc.SoLuong != null) spinSoLuong.EditValue = _dinhMuc.SoLuong;
                spinGiaNgay.EditValue = _dinhMuc.GiaNgay;
                spinGiaThang.EditValue = _dinhMuc.GiaThang;
                if (_dinhMuc.MaMB != null) MaMb = _dinhMuc.MaMB;
                if (_dinhMuc.MaLMB != null) MaLmb = _dinhMuc.MaLMB;
            }

            System.Collections.Generic.List<LoaiClass> loaiClasses = new System.Collections.Generic.List<LoaiClass>();
            loaiClasses.Add(new LoaiClass() { Id = 1, Name = "Định mức chung" });
            loaiClasses.Add(new LoaiClass() { Id = 2, Name = "Định mức theo loại mặt bằng" });
            loaiClasses.Add(new LoaiClass() { Id = 3, Name = "Định mức theo mặt bằng" });

            glkLoai.Properties.DataSource = loaiClasses;
            glkLoai.EditValue = GetIdLoai();

            glkLoaiXe.Properties.DataSource = _db.dvgxLoaiXes.Where(_ => _.MaTN == MaTn);

            glkLoaiMatBang.Properties.DataSource = _db.mbLoaiMatBangs.Where(_ => _.MaTN == MaTn);
            if (MaLmb != null) glkLoaiMatBang.EditValue = MaLmb;

            glkMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == MaTn);
            if (MaMb != null) glkMatBang.EditValue = MaMb;
        }

        public class LoaiClass
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }

        private int GetIdLoai()
        {
            if (MaMb == null)
                if (MaLmb == null) return 1;
                else return 2;
            else return 3;
        }

        private Library.dvgxDinhMuc GetDinhMucById(int? id)
        {
            return _db.dvgxDinhMucs.FirstOrDefault(_ => _.ID == id);
        }

        private void glkLoai_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
            if (item == null) return;
            switch((int?)item.EditValue)
            {
                case 1:
                    layoutControlItemLoaiMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItemMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case 2:
                    layoutControlItemLoaiMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItemMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case 3:
                    layoutControlItemLoaiMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItemMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    break;
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(glkLoaiXe.EditValue == null) { Library.DialogBox.Error("Vui lòng chọn loại xe"); return; }

            _dinhMuc.TenDM = txtTenDinhMuc.Text;
            _dinhMuc.SoLuong = (int?) spinSoLuong.Value;
            _dinhMuc.GiaThang = spinGiaThang.Value;
            _dinhMuc.GiaNgay = spinGiaNgay.Value;
            _dinhMuc.MaLX = (int?)glkLoaiXe.EditValue;
            _dinhMuc.MaTN = MaTn;

            switch((int?)glkLoai.EditValue)
            {
                case 1:
                    _dinhMuc.MaLMB = null;
                    _dinhMuc.MaMB = null;
                    break;
                case 2:
                    if(glkLoaiMatBang.EditValue == null) { Library.DialogBox.Error("Vui lòng chọn loại mặt bằng"); return; }
                    _dinhMuc.MaLMB = (int?)glkLoaiMatBang.EditValue;
                    _dinhMuc.MaMB = null;
                    break;
                case 3:
                    if(glkMatBang.EditValue == null) { Library.DialogBox.Error("Vui lòng chọn mặt bằng"); return; }
                    _dinhMuc.MaLMB = null;
                    _dinhMuc.MaMB = (int?)glkMatBang.EditValue;
                    break;
            }

            _db.SubmitChanges();
            Library.DialogBox.Success();
            Close();
        }

        private void glkLoaiXe_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using(var frm = new DichVu.GiuXe.frmLoaiXe()) { frm.ShowDialog(); var db = new Library.MasterDataContext(); glkLoaiXe.Properties.DataSource = db.dvgxLoaiXes.Where(_ => _.MaTN == MaTn); }
        }

        private void glkLoaiMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using(var frm = new )
            
        }
    }
}