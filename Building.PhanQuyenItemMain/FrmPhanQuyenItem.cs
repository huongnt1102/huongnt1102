using DevExpress.XtraEditors;
using System.Linq;
using System.Windows.Forms;

namespace Building.PhanQuyenItemMain
{
    public partial class FrmPhanQuyenItem : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }
        public int? ParentId { get; set; }

        private Library.MasterDataContext _db;
        private System.Collections.Generic.List<int?> _lModuleShow = new System.Collections.Generic.List<int?>();

        public FrmPhanQuyenItem()
        {
            InitializeComponent();
        }

        private void FrmPhanQuyenItem_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);

            LoadData();

            CreatePhanQuyen();
        }

        private void CreatePhanQuyen()
        {
            if (LControlName == null) return;

            var lModuleControl = new System.Collections.Generic.List<Library.PhanQuyen.ModuleControl>();


            foreach (var item in barManager1.Items)
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    var button = (DevExpress.XtraBars.BarButtonItem)item;
                    lModuleControl.Add(new Library.PhanQuyen.ModuleControl { ModuleName = button.Caption, ModuleDescription = button.Caption, ControlNames = button.Name });
                }

            Library.PhanQuyen.CreatePhanQuyen(GetType().Namespace + "." + Name, Text, ParentId, Library.PhanQuyen.ModuleId.FORM_MAIN_ID, LControlName, lModuleControl);
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext { CommandTimeout = 600 };
            gc.DataSource = _db.pq_PhanQuyenMain_Groups;
            treeList1.DataSource = _db.pqModules.Where(_ => _.IsInMain == true & _.Name != null);
            gv_FocusedRowChanged(null, null);
        }

        private void LoadNhom()
        {
            _db = new Library.MasterDataContext();
            gc.DataSource = _db.pq_PhanQuyenMain_Groups; //.Select(_=>new{_.Id, IsHienThi = _.IsHienThi==true?"Đang chọn":"", _.Name});
        }

        private void TreeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        #region Set checked child nodes
        private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, System.Windows.Forms.CheckState checkState)
        {
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                SetNodeCheckState(node.Nodes[i], checkState);
                SetCheckedChildNodes(node.Nodes[i], checkState);
            }
        }

        private void SetNodeCheckState(DevExpress.XtraTreeList.Nodes.TreeListNode node, System.Windows.Forms.CheckState checkState)
        {
            node.CheckState = checkState;
        }
        #endregion

        #region Set checked parent nodes

        private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, System.Windows.Forms.CheckState checkState)
        {
            if (node.ParentNode == null) return;
            var check = false;
            for (var i = 0; i < node.ParentNode.Nodes.Count; i++)
            {
                if (checkState.Equals(node.ParentNode.Nodes[i].CheckState)) continue;
                check = true;
                break;
            }

            SetNodeCheckState(node.ParentNode, check ? System.Windows.Forms.CheckState.Checked : checkState);
            SetCheckedParentNodes(node.ParentNode, checkState);
        }

        #endregion

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private async void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var nhomId = (int)gv.GetFocusedRowCellValue("Id");

                //var module = _db.pqModules;
                //module.Where(_=>_.IsInMain == true & _.Name!=null).ToList().ForEach(_ => _.IsHienThi = false);

                //var lCaiDat = _db.pq_PhanQuyenMain_CaiDats.Where(_ => _.GroupId == nhomId);
                //lCaiDat.ToList().ForEach(_ => { _.IsShow = false; });

                //// danh sách đã có
                //var lCaiDatDaCo = lCaiDat.Where(_ => _.GroupId == nhomId).Select(_ => _.ModuleId).ToList();
                //// danh sách chưa có
                //var lCaiDatChuaCo = module.ToList().Where(_ => !lCaiDatDaCo.Contains(_.ModuleID)).ToList();
                //foreach (var item in lCaiDatChuaCo)
                //{
                //    var caiDat = new pq_PhanQuyenMain_CaiDat { ModuleId = item.ModuleID, GroupId = nhomId, IsShow = true };
                //    _db.pq_PhanQuyenMain_CaiDats.InsertOnSubmit(caiDat);
                //}

                //ChonHienThi(nhomId);
                var model = new { nhom_id = nhomId };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("dbo.pq_save_tool",  param).ToList();

                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in treeList1.GetAllCheckedNodes())
                {
                    var moduleId = (int)node.GetValue("ModuleID");

                    var model1 = new { module_id = moduleId, node_check = node.Checked, nhom_id = nhomId };
                    var param1 = new Dapper.DynamicParameters();
                    param1.AddDynamicParams(model1);
                    Library.Class.Connect.QueryConnect.Query<bool>("dbo.pq_save_tool_item",  param1).ToList();

                    //await System.Threading.Tasks.Task.Run(() =>
                    //{
                    //    var nodeSys = module.FirstOrDefault(_ => _.ModuleID == moduleId);
                    //    if (nodeSys != null)
                    //        nodeSys.IsHienThi = node.Checked;
                    //});

                    //await System.Threading.Tasks.Task.Run(() =>
                    //{
                    //    var caiDatPhanQuyen = lCaiDat.FirstOrDefault(_ => _.ModuleId == moduleId & _.GroupId == nhomId);
                    //    if (caiDatPhanQuyen == null)
                    //    {
                    //        caiDatPhanQuyen = new pq_PhanQuyenMain_CaiDat
                    //        {
                    //            ModuleId = moduleId,
                    //            GroupId = nhomId
                    //        };
                    //        _db.pq_PhanQuyenMain_CaiDats.InsertOnSubmit(caiDatPhanQuyen);
                    //    }

                    //    caiDatPhanQuyen.IsShow = true;
                    //});
                }

                //_db.SubmitChanges();

                DevExpress.XtraEditors.XtraMessageBox.Show("Lưu thành công", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 5000;
                args.Caption = ex.GetType().FullName;
                args.Text = ex.Message;
                args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                XtraMessageBox.Show(args).ToString();
            }
        }

        private void ChonHienThi(int? nhomId)
        {
            // lưu nhóm
            var deleteAll = _db.pq_PhanQuyenMain_Groups;
            deleteAll.ToList().ForEach(_ => { _.IsHienThi = false; });

            var nhom = _db.pq_PhanQuyenMain_Groups.FirstOrDefault(_ => _.Id == nhomId);
            if (nhom == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Không tìm thấy nhóm trong database", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            nhom.IsHienThi = true;
        }

        private void ReturnNodeRecursive(DevExpress.XtraTreeList.Nodes.TreeListNode treeListNode)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in treeListNode.Nodes)
            {
                if (_lModuleShow.Contains((int) node.GetValue("ModuleID")))
                    node.CheckState = System.Windows.Forms.CheckState.Checked;
                ReturnNodeRecursive(node);
            }
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            treeList1.UncheckAll();

            _lModuleShow = _db.pq_PhanQuyenMain_CaiDats.Where(_=>_.GroupId == (int)gv.GetFocusedRowCellValue("Id") & _.IsShow.Value).Select(_=>_.ModuleId).ToList();

            //_lNode = new System.Collections.Generic.List<DevExpress.XtraTreeList.Nodes.TreeListNode>();
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in treeList1.Nodes)
            {
                if (_lModuleShow.Contains((int) node.GetValue("ModuleID"))) node.CheckState = System.Windows.Forms.CheckState.Checked;
                ReturnNodeRecursive(node);
            }
        }

        private void ItemThemNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Building.PhanQuyenItemMain.Group.FrmGroup())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadNhom();
            }
        }

        private void itemModule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (LandsoftBuildingGeneral.PhanQuyen.frmModules frm = new LandsoftBuildingGeneral.PhanQuyen.frmModules())
            {
                frm.ShowDialog();
            }
        }
    }
}