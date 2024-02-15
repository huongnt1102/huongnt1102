

namespace Building.Help
{
    public partial class FrmManager : DevExpress.XtraEditors.XtraForm
    {
        public FrmManager()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void FrmManager_Load(object sender, System.EventArgs e)
        {

        }

        private void tileControl1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            // Sự kiện item click
            // Cần get group item, cái group item này sẽ lấy theo bảng phân quyền.
            using(Building.Help.Group.FrmGroup frmGroup = new Building.Help.Group.FrmGroup() { GroupName = e.Item.Group.Text })
            {
                // Chổ này làm sao cho nó hiển thị ngay trong frmMain?
                // Nếu không hiển thị được frmMain, thì chỉ đành cho nó show dialog lên thôi
                frmGroup.ShowDialog();
            }
        }
    }
}