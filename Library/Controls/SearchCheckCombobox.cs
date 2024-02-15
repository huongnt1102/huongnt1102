using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Library.Controls
{
    public partial class SearchCheckCombobox : PopupContainerEdit
    {
        GridControl gc = new GridControl();
        GridView gv = new GridView();

        public string Value = "";

        private GridColumn[] Columns;

        private string ValueMember = "";

        private string DisplayMember = "";

        private object DataSource { get; set; }

        public SearchCheckCombobox()
        {
            PopupContainerControl ctn = new PopupContainerControl();
            ctn.Width = 400;
            ctn.Height = 300;
            gc.Dock = System.Windows.Forms.DockStyle.Fill;
            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowAutoFilterRow = true;
            gv.OptionsFind.AlwaysVisible = true;
            gc.MainView = gv;
            ctn.Controls.Add(gc);
            this.Properties.PopupControl = ctn;
            this.Value = "";
        }

        public void Init(string displayMember, string valueMember,object dataSource ,GridColumn[] columns)
        {
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            this.Columns = columns;
            this.DataSource = dataSource;
            this.CreateColumn();
            this.LoadData();
        }

        public void SetValue(string value)
        {
            this.Value = value;
            SetCheck();
            SetDisplayValue();
        }

        private void SetCheck()
        {
            try
            {
                if (this.Value == null || this.Value.Trim().Length == 0)
                    return;

                var filter = (gc.DataSource as DataTable).Select(@"MaNV in" + string.Format("({0})", this.Value));

                string[] displays = new string[filter.Count()];

                int i = 0;

                foreach (var r in filter)
                {
                    r["IsCheck"] = true;
                    displays[i] = Convert.ToString(r[this.DisplayMember]);
                    i++;
                }

                this.EditValue = string.Join(", ", displays);
                this.Text = string.Join(", ", displays);
            }
            catch
            {
            }
        }

        private void LoadData()
        {
            if (this.DataSource == null)
                return;

            DataTable dt = new DataTable();
            dt.Columns.Add("IsCheck", typeof(bool));

            IList collection = (IList)DataSource;

            if (collection.Count > 0)
            {
                var type = collection[0];

                Type myType = type.GetType();

                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                var fieldNames = props.Select(o => o.Name).ToList();

                foreach (PropertyInfo info in props)
                {
                    dt.Columns.Add(info.Name);
                }

                foreach (var item in collection)
                {
                    DataRow r = dt.NewRow();
                    foreach (var fied in fieldNames)
                    {
                        r[fied] = item.GetType().GetProperty(fied).GetValue(item, null);
                    }
                    dt.Rows.Add(r);
                }

                gc.DataSource = dt;
            }
        }

        private void CreateColumn()
        {
            gv.Columns.Clear();
            GridColumn colCheck = new GridColumn();
            colCheck.Caption = "Chọn";
            colCheck.Visible = true;
            colCheck.VisibleIndex = 0;
            colCheck.FieldName = "IsCheck";

            var ckb = new RepositoryItemCheckEdit();
            colCheck.ColumnEdit = ckb;
            ckb.EditValueChanged += ckb_EditValueChanged;

            foreach (var col in this.Columns)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;

            gv.Columns.AddRange(this.Columns);
            gv.Columns.Add(colCheck);
        }

        void ckb_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var ckb = sender as CheckEdit;

                gv.SetFocusedRowCellValue("IsCheck", ckb.Checked);
                gv.UpdateCurrentRow();

                SetDisplayValue();
            }
            catch
            {
            }
        }

        private void SetDisplayValue()
        {
            try
            {
                var filter = (gc.DataSource as DataTable).Select(@"IsCheck = 1");

                string[] displays = new string[filter.Count()];

                string[] values = new string[filter.Count()];

                foreach (var r in filter)
                {
                }

                for (int i = 0; i < filter.Count(); i++)
                {
                    displays[i] = Convert.ToString(filter[i][this.DisplayMember]);
                    values[i] = Convert.ToString(filter[i][this.ValueMember]);
                }

                this.EditValue = string.Join(", ", displays);
                this.Value = string.Join(", ", values);
            }
            catch
            {
            }
        }

    }
}
