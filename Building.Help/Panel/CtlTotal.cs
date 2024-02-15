using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Building.Help.Panel
{
    public partial class CtlTotal : DevExpress.XtraEditors.XtraUserControl
    {

        public CtlTotal()
        {
            InitializeComponent();
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Controls.Clear();
            Building.Help.Panel.CtlGroup group = new Building.Help.Panel.CtlGroup() { GroupName = e.Item.Group.Text };
            group.Dock = DockStyle.Fill;
            this.Controls.Add(group);
            group.Show();
        }
    }
}
