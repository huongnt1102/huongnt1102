using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Library.Controls
{
    public class LookEdit : LookUpEdit
    {
        public LookEdit()
        {
            this.Width = 100;
            this.Height = 20;

            this.Properties.NullText = "";
            this.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Combo });

            this.KeyUp += new System.Windows.Forms.KeyEventHandler(LookEdit_KeyUp);
        }

        void LookEdit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.EditValue = null;
            }
        }
    }
}
