using System.Linq;

namespace Building.KinhPhiDuKien
{
    public partial class FrmMoneyPurposeEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MpId { get; set; }
        public decimal Year;
        public byte? BuildingId;

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.mp_MoneyPurpose Mp;

        public FrmMoneyPurposeEdit()
        {
            InitializeComponent();
        }

        private void FrmMoneyPurposeEdit_Load(object sender,System.EventArgs e)
        {
            Mp = GetMoneyPurpose();
            spinYear.Value = Year;
            spinMoneyPurpose.Value = Mp.MoneyPurpose != null ? (decimal)Mp.MoneyPurpose : 0;
        }

        private Library.mp_MoneyPurpose GetMoneyPurpose()
        {
            return _db.mp_MoneyPurposes.FirstOrDefault(_ => _.Id == MpId) ?? new Library.mp_MoneyPurpose();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Mp.Id == 0)
                {
                    Mp.Name = "";
                    Mp.BuildingId = BuildingId;
                    Mp.DateCreate = System.DateTime.UtcNow.AddHours(7);
                    Mp.UserCreateId = Library.Common.User.MaNV;
                    Mp.UserCreateName = Library.Common.User.HoTenNV;
                    Mp.MoneyUsed = 0;
                    _db.mp_MoneyPurposes.InsertOnSubmit(Mp);
                }

                Mp.Year = int.Parse(spinYear.EditValue.ToString());
                Mp.DateUpdate = System.DateTime.UtcNow.AddHours(7);
                Mp.UserUpdateId = Library.Common.User.MaNV;
                Mp.UserUpdateName = Library.Common.User.HoTenNV;
                Mp.MoneyPurpose = (decimal)spinMoneyPurpose.EditValue;
                Mp.MoneyExist = Mp.MoneyPurpose - Mp.MoneyUsed;

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

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}