using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
using System.Linq;
using DevExpress.XtraEditors.Registrator;

namespace Library
{
    [UserRepositoryItem("Register")]
    public class RepoTaiSan : RepositoryItem
    {
        public TreeList TreeListControl { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }

        public RepoTaiSan()
        {
            MasterDataContext db = new MasterDataContext();

            this.TreeListControl = new TreeList();
            this.TreeListControl.OptionsBehavior.Editable = false;
            this.TreeListControl.OptionsView.ShowIndicator = false;
            this.TreeListControl.OptionsView.ShowColumns = false;
            this.TreeListControl.OptionsView.ShowHorzLines = false;
            this.TreeListControl.OptionsView.ShowVertLines = false;

            DevExpress.XtraTreeList.Columns.TreeListColumn colTenLTS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            colTenLTS.Caption = "Tên loại tài sản";
            colTenLTS.FieldName = "TenLTS";
            colTenLTS.Visible = true;
            colTenLTS.VisibleIndex = 0;
            colTenLTS.Name = "ColTaiSan";

            this.TreeListControl.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { colTenLTS });
            this.TreeListControl.KeyFieldName = "MaLTS";
            this.TreeListControl.ParentFieldName = "MaLTSCHA";
            this.ValueMember = "MaLTS";
            this.DisplayMember = "TenLTS";


            this.TreeListControl.DataSource = db.tsLoaiTaiSans.ToList();
            this.TreeListControl.ForceInitialize();
            this.TreeListControl.ExpandAll();
        }
    }
}
