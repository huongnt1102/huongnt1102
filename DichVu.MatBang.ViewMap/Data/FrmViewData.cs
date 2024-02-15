using System.Linq;

namespace DichVu.MatBang.ViewMap.Data
{
    public partial class FrmViewData : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<DichVu.MatBang.ViewMap.Data.DataItem> DataItems { get; set; }
        public FrmViewData()
        {
            InitializeComponent();
        }

        private void FrmViewData_Load(object sender, System.EventArgs e)
        {
            gridControl1.DataSource = DataItems;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}