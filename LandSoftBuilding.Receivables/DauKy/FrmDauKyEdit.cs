using System;
using System.Linq;

namespace LandSoftBuilding.Receivables.DauKy
{
    public partial class FrmDauKyEdit : DevExpress.XtraEditors.XtraForm
    {
        public long? DauKyId { get; set; }
        public byte? BuildingId { get; set; }
        public int? Nam { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.dvDauKy _dauKy;

        public FrmDauKyEdit()
        {
            InitializeComponent();
        }

        private void FrmDauKyEdit_Load(object sender, EventArgs e)
        {
            _dauKy = GetDauKy();
            LoadDanhMuc();

            SetValue(DauKyId != null);
        }

        private void SetValue(bool isEdit)
        {
            switch (isEdit)
            {
                case true:
                    glkKhachHang.EditValue = _dauKy.MaKH;
                    glkLoaiDichVu.EditValue = _dauKy.MaLDV;
                    glkMatBang.EditValue = _dauKy.MaMB;

                    if (_dauKy.SoTien != null) spinSoTien.Value = (decimal) _dauKy.SoTien;
                    break;

                case false:
                    spinSoTien.Value = 0;
                    _db.dvDauKies.InsertOnSubmit(_dauKy);
                    break;
            }
        }

        private void LoadDanhMuc()
        {
            glkKhachHang.Properties.DataSource = _db.tnKhachHangs.Where(_ => _.MaTN == BuildingId);
            glkLoaiDichVu.Properties.DataSource = _db.dvLoaiDichVus;
            glkMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == BuildingId);
        }

        private Library.dvDauKy GetDauKy()
        {
            return DauKyId != null ? _db.dvDauKies.First(_ => _.Id == DauKyId) : new Library.dvDauKy();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _dauKy.MaKH = (int) glkKhachHang.EditValue;
                _dauKy.MaMB = (int) glkMatBang.EditValue;
                _dauKy.MaLDV = (int) glkLoaiDichVu.EditValue;
                _dauKy.Nam = Nam;
                _dauKy.SoTien = spinSoTien.Value;
                _dauKy.MaTN = BuildingId;

                _db.SubmitChanges();
                Library.DialogBox.Success();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi lưu dữ liệu: " + ex);
            }
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;

                glkMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaKH == (int) item.EditValue);
            }
            catch{}
        }
    }
}