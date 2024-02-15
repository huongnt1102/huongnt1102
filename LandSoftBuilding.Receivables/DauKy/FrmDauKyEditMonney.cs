using System;
using System.Linq;

namespace LandSoftBuilding.Receivables.DauKy
{
    public partial class FrmDauKyEditMonney : DevExpress.XtraEditors.XtraForm
    {
        public long? DauKyId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.dvDauKy _dauKy;

        public FrmDauKyEditMonney()
        {
            InitializeComponent();
        }

        private void FrmDauKyEdit_Load(object sender, EventArgs e)
        {
            _dauKy = GetDauKy();
            if (_dauKy == null) return;

        }

        private Library.dvDauKy GetDauKy()
        {
            return _db.dvDauKies.FirstOrDefault(_ => _.Id == DauKyId);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _dauKy.SoTien = spinSoTien.Value;

                _db.SubmitChanges();
                Library.DialogBox.Success();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch{}
        }
    }
}