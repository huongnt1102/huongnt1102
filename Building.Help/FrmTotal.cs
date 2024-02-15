

namespace Building.Help
{
    public partial class FrmTotal : DevExpress.XtraEditors.XtraForm
    {
        public FrmTotal()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void FrmTotal_Load(object sender, System.EventArgs e)
        {
            //Library.TranslateLanguage.TranslateControl(this,null);
            //Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, null);

            this.Controls.Clear();
            Building.Help.Panel.CtlTotal total = new Building.Help.Panel.CtlTotal();
            total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(total);
            total.Show();
        }

        private void tileControl1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //panelControl1.Controls.Clear();
            //var ctl = new Building.Help.Panel.CtlGroup() { GroupName = e.Item.Group.Text };
            //panelControl1.Controls.Add(ctl);
            //ctl.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}