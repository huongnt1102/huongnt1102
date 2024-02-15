using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Registrator;
using Library;

namespace Library
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemTreeListEdit: RepositoryItemPopupContainerEdit
    {
        private bool IsSelected;
        private PopupContainerControl popupContainerControl;
        
        public TreeList TreeListControl { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }

        public RepositoryItemTreeListEdit()
        {
            this.TreeListControl = new TreeList();
            this.TreeListControl.OptionsBehavior.Editable = false;
            this.TreeListControl.OptionsView.ShowIndicator = false;
            this.TreeListControl.OptionsView.ShowColumns = false;
            this.TreeListControl.OptionsView.ShowHorzLines = false;
            this.TreeListControl.OptionsView.ShowVertLines = false;
            
            this.popupContainerControl = new PopupContainerControl();
            this.popupContainerControl.Controls.Add(this.TreeListControl);
            this.PopupControl = this.popupContainerControl;

            this.QueryResultValue += new QueryResultValueEventHandler(RepositoryItemTreeListEdit_QueryResultValue);
            this.QueryDisplayText += new QueryDisplayTextEventHandler(RepositoryItemTreeListEdit_QueryDisplayText);
            this.QueryPopUp += new System.ComponentModel.CancelEventHandler(RepositoryItemTreeListEdit_QueryPopUp);
            this.TreeListControl.DoubleClick += new EventHandler(TreeListControl_DoubleClick);
        }

        void TreeListControl_DoubleClick(object sender, EventArgs e)
        {
            if (this.popupContainerControl.OwnerEdit != null)
            {
                this.IsSelected = true;
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }

        void RepositoryItemTreeListEdit_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PopupContainerEdit p = (PopupContainerEdit)sender;
            this.TreeListControl.FocusedNode = this.TreeListControl.FindNodeByFieldValue(this.ValueMember, p.EditValue);
        }

        void RepositoryItemTreeListEdit_QueryDisplayText(object sender, QueryDisplayTextEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                try
                {
                    if (e.EditValue != null)
                    {
                        var dp = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.EditValue);
                        e.DisplayText = dp.TenLTS;

                        //TreeListNode node = TreeListControl.FindNodeByFieldValue(this.ValueMember, e.EditValue);
                        //if (node != null)
                        //{
                        //    e.DisplayText = node.GetDisplayText(TreeListControl.Columns[0]);
                        //}
                    }
                }
                catch { }
            }
        }

        void RepositoryItemTreeListEdit_QueryResultValue(object sender, QueryResultValueEventArgs e)
        {
            if (this.IsSelected)
            {
                this.IsSelected = false;
                e.Value = this.TreeListControl.FocusedNode.GetValue(this.ValueMember);
            }
        }

    }
}
