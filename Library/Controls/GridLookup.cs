using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Library.Controls
{
    public partial class GridLookup : GridLookUpEdit
    {
        const int HEIGHT = 300;
        const int COLUMN_WIDTH = 100;
        private bool IsInIt = false;

        public string displayMember = "";
        public string valueMember = "";
        public ColItem[] cols;

        public delegate void dlgLloadData();

        public dlgLloadData loadData_Function;

        public GridLookup()
        {
            InitializeComponent();
            this.Properties.NullText = "";
            this.Properties.View.OptionsView.ShowAutoFilterRow = true;
        }

        public void CreateColumn()
        {
            int width = 0;
            foreach (var item in cols)
            {
                GridColumn col = new GridColumn();
                col.Caption = item.Caption;
                col.FieldName = item.FieldName;
                col.Width = item.Width ?? COLUMN_WIDTH;
                col.Visible = true;
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
                col.VisibleIndex = this.Properties.View.Columns.Count;
                this.Properties.View.Columns.Add(col);
                width += col.Width;
            }

            this.Properties.PopupFormSize = new System.Drawing.Size(width, HEIGHT);
        }

        public void SetProperties()
        {
            this.Properties.DisplayMember = displayMember;
            this.Properties.ValueMember = valueMember;
        }

        public void InIt()
        {
            if (!this.IsInIt)
            {
                this.CreateColumn();
                this.SetProperties();
                this.IsInIt = true;
            }
        }

        public GridLookup(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public class ColItem
        {
            public string Caption { get; set; }
            public string FieldName { get; set; }
            public int? Width { get; set; }
        }
    }
}
