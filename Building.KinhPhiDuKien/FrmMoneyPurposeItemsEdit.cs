using System.Linq;

namespace Building.KinhPhiDuKien
{
    public partial class FrmMoneyPurposeItemsEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MpId { get; set; }
        public int? MpiId { get; set; }

        private Library.mp_MoneyPurpose _mp;
        private Library.mp_MoneyPurposeItem _mpi;
        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmMoneyPurposeItemsEdit()
        {
            InitializeComponent();
        }

        private void FrmMoneyPurposeItemsEdit_Load(object sender,System.EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            _mp = GetMoneyPurpose();
            _mpi = GetMoneyPurposeItems();

            if (_mp.MoneyExist != null) txtMoneyPurpose.Text = string.Format("{0:#,0.##; (0.##);-}", _mp.MoneyExist);
            if (_mpi.Id != 0)
            {
                txtMoneyPurpose.Text = string.Format("{0:#,0.##; (0.##);-}", _mp.MoneyExist + _mpi.MoneyPurpose);
                memoName.Text = _mpi.Name;
                txtNo.Text = _mpi.No;
                if (_mpi.MoneyPurpose != null) spinMoneyPurposeItems.Value = (decimal) _mpi.MoneyPurpose;

                _mpi.DateUpdate = System.DateTime.UtcNow.AddHours(7);
                _mpi.UserUpdateId = Library.Common.User.MaNV;
                _mpi.UserUpdateName = Library.Common.User.HoTenNV;
            }

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemClearText.ItemClick += ItemClearText_ItemClick;
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private Library.mp_MoneyPurpose GetMoneyPurpose()
        {
            return _db.mp_MoneyPurposes.FirstOrDefault(_ => _.Id == MpId) ?? new Library.mp_MoneyPurpose();
        }

        private Library.mp_MoneyPurposeItem GetMoneyPurposeItems()
        {
            return _db.mp_MoneyPurposeItems.FirstOrDefault(_ => _.Id == MpiId) ?? new Library.mp_MoneyPurposeItem();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (GetMoneyPurposeItemsByNo(_mpi.Id) != null)
                {
                    Library.DialogBox.Error("Mã hạng mục đã có, vui lòng chọn mã hạng mục khác");
                    return;
                }

                if (_mpi.Id == 0)
                {
                    _mpi.BuildingId = _mp.BuildingId;
                    _mpi.DateCreate = System.DateTime.UtcNow.AddHours(7);
                    _mpi.MoneyUsed = 0;
                    _mpi.UserCreateId = Library.Common.User.MaNV;
                    _mpi.UserCreateName = Library.Common.User.HoTenNV;
                    _db.mp_MoneyPurposeItems.InsertOnSubmit(_mpi);
                }

                _mpi.MoneyPurposeId = _mp.Id;
                _mpi.MoneyPurpose = spinMoneyPurposeItems.Value;
                _mpi.Name = memoName.Text;
                _mpi.MoneyExist = _mpi.MoneyPurpose - _mpi.MoneyUsed;
                _mpi.Year = _mp.Year;
                _mpi.No = txtNo.Text;

                _mp.MoneyUsed = _mp.MoneyUsed + _mpi.MoneyPurpose;
                _mp.MoneyExist = _mp.MoneyPurpose - _mp.MoneyUsed;
                _mp.DateUpdate = System.DateTime.UtcNow.AddHours(7);
                _mp.UserUpdateId = Library.Common.User.MaNV;
                _mp.UserUpdateName = Library.Common.User.HoTenNV;
                
                _db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Library.DialogBox.Success();
            }
            catch (System.Exception ex)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Library.DialogBox.Error("Không lưu được dữ liệu: " + ex);
            }
        }

        private Library.mp_MoneyPurposeItem GetMoneyPurposeItemsByNo(int id)
        {
            return _db.mp_MoneyPurposeItems.FirstOrDefault(_ => _.No.ToLower() == txtNo.Text.Trim().ToLower() & _.Id != id);
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}