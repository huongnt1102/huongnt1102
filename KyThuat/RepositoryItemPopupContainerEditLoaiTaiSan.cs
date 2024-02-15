using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using Library;

namespace Library
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemPopupContainerEditLoaiTaiSan : RepositoryItemTreeListEdit
    {
        public RepositoryItemPopupContainerEditLoaiTaiSan(tnNhanVien objnhanvien)
        {
            MasterDataContext db = new MasterDataContext();
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

            if (objnhanvien.IsSuperAdmin.Value)
                TreeListControl.DataSource = db.tsLoaiTaiSans.ToList();
            else
                TreeListControl.DataSource = db.tsLoaiTaiSans.Where(p => p.MaTN == objnhanvien.MaTN).ToList();

            this.TreeListControl.ForceInitialize();
            this.TreeListControl.ExpandAll();
            this.TreeListControl.Dock = System.Windows.Forms.DockStyle.Fill;
        }

    }
}
