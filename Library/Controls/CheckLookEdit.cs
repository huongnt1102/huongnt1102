using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Library.Controls
{
    public class CheckLookEdit : CheckedComboBoxEdit
    {
        public CheckLookEdit()
        {
            this.Width = 100;
            this.Height = 20;
            this.Properties.NullText = "";
            this.Properties.SelectAllItemCaption = "Tất cả";
            this.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Combo });
            //this.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = DIPCRM.Controls.Properties.Resources.Edit });
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(CheckLookEdit_KeyUp);
        }

        void CheckLookEdit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.SetEditValue(null);
            }
        }
    }
}
