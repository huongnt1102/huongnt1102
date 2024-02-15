using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Library;
using DevExpress.XtraPrinting;

namespace LandsoftBuildingGeneral.PhanQuyen
{
    public partial class frmPhanQuyenManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        List<TreeListNode> checkedNodes = new List<TreeListNode>();
        public List<int?> ListModuleAlreadyHasInAccess = new List<int?>();
        int SLChecked = 0; bool IsChecked = false;
        public tnNhanVien objnhanvien;
        
        public frmPhanQuyenManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            db.CommandTimeout = 600;
            TranslateLanguage.TranslateControl(this, barManager1);
        }
        private void frmPhanQuyenManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            var moduleMain = db.pqModules.Where(_ => _.IsInMain == true & _.IsHienThi == true).ToList();
            var moduleParent = moduleMain.Select(_ => _.ModuleID).ToList();
            var moduleChild = db.pqModules.Where(_ => (_.IsInMain == null) & moduleParent.Contains((int)_.ModuleParentID)).ToList();
            var module = moduleMain.Union(moduleChild).ToList();
            treeListModule.DataSource = module;
            //treeListModule.DataSource = db.pqModules.Where(_=>_.IsHienThi == true);
            gcGroupUser.DataSource = db.pqNhoms;
        }

        private void btnForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmForm frm = new frmForm())
            {
                frm.ShowDialog();
            }
        }

        private void btnGenForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MasterDataContext db = new MasterDataContext();
            Type[] AllTypeInProject = Assembly.GetExecutingAssembly().GetTypes();
            var wait = DialogBox.WaitingForm();
            List<pqForm> lstform = new List<pqForm>();
            for (int i = 0; i < AllTypeInProject.Length; i++)
            {
                if (AllTypeInProject[i].BaseType == typeof(XtraForm)
                    | AllTypeInProject[i].BaseType == typeof(Form))
                {
                    try
                    {
                        Form f = (Form)Activator.CreateInstance(AllTypeInProject[i]);
                        if (db.pqForms.Single(p => p.FormName == f.GetType().ToString()) == null)
                        {
                            pqForm objform = new pqForm()
                            {
                                FormName = f.GetType().ToString(),
                                DienGiai = f.Text
                            };
                            lstform.Add(objform);
                        }
                    }
                    catch { }
                }
            }
            db.pqForms.InsertAllOnSubmit(lstform);
            try
            {
                db.SubmitChanges();
            }
            catch { }
            finally
            {
                db.Dispose();
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnModule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmModules frm = new frmModules())
            {
                frm.ShowDialog();
            }
        }

        private void btnUserGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmUserGroup frm = new frmUserGroup() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
            }
        }


        private void CountCheckedNode(TreeListNode treenode)
        {
            checkedNodes.Add(treenode);
            foreach (TreeListNode tn in treenode.Nodes)
            {
                if (tn.Checked)
                    SLChecked ++;
                CountCheckedNode(tn);
            }
            checkedNodes.Remove(treenode);
        }

        private void Apply2()
        {
            SLChecked = 0;
            var wait = DialogBox.WaitingForm();
            checkedNodes.Clear();
            foreach (TreeListNode node in treeListModule.Nodes)
            {
                CountCheckedNode(node);
            }
            if (SLChecked >= (db.pqModules.Count() - SLChecked))
                IsChecked = true;
            else IsChecked = false;

            // Thêm các node chưa có
            db.AccessRightInsert((int)grvGroupUser.GetFocusedRowCellValue("GroupID"));

            db.pqResetNode((int)grvGroupUser.GetFocusedRowCellValue("GroupID"), IsChecked);

            foreach (TreeListNode node in treeListModule.Nodes)
            {
                GetAllNode(node);
                // Check node đầu tiên

                if (node.Checked == !IsChecked)
                    db.spUpdatePqAccess((int)grvGroupUser.GetFocusedRowCellValue("GroupID"), (int)node.GetValue("ModuleID"), !IsChecked);
            }

            if (checkedNodes == null)
            {
                wait.Close();
                wait.Dispose();
                return;
            }

            //var groupobj = db.pqNhoms.Single(p => p.GroupID == (int)grvGroupUser.GetFocusedRowCellValue("GroupID"));
            //var ListModuleAlreadyHasInAccess = db.pqAccessRights.Where(p => p.GroupID == groupobj.GroupID).ToList();
            //List<pqAccessRight> lstpqa = new List<pqAccessRight>();
            //if (ListModuleAlreadyHasInAccess.Count() == 0)
            //{
            //    foreach (TreeListNode node in checkedNodes)
            //    {
            //        pqAccessRight pqa = new pqAccessRight()
            //        {
            //            GroupID = groupobj.GroupID,
            //            IsAccessRight = node.Checked,
            //            ModuleID = (int)node.GetValue("ModuleID")
            //        };
            //        lstpqa.Add(pqa);
            //        db.pqAccessRights.InsertAllOnSubmit(lstpqa);
            //    }
            //}
            //else
            //{
            //foreach (var selectedmoduleId in checkedNodes)
            //{
            //    db.spUpdatePqAccess((int)grvGroupUser.GetFocusedRowCellValue("GroupID"), (int)selectedmoduleId.GetValue("ModuleID"), selectedmoduleId.Checked);
            //}
            //}

            try
            {
                db.SubmitChanges();
            }
            catch
            {
            }
            finally
            {
                wait.Close();
                wait.Dispose();
                //grvGroupUser_FocusedRowChanged(null, null);
            }
        }

        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                SetNodeCheckState(node.ParentNode, b ? CheckState.Checked : check);
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private void SetNodeCheckState(TreeListNode node, CheckState state)
        {
            node.CheckState = state;
            if (node.Checked)
                checkedNodes.Add(node);
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                SetNodeCheckState(node.Nodes[i], check);
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private void treeListModule_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            checkedNodes.Clear();
            if (e.Node.Checked)
                checkedNodes.Add(e.Node);
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private List<int> ShowCheckedNodesValues()
        {
            if (checkedNodes.Count == 0) return null;
            checkedNodes.Sort(new Comparison<TreeListNode>(delegate(TreeListNode node1, TreeListNode node2)
            {
                return Comparer<int>.Default.Compare(node1.Id, node2.Id);
            }));
            List<int> lstModuleID = new List<int>();
            foreach (TreeListNode node in checkedNodes)
                lstModuleID.Add((int)node.GetValue("ModuleID"));

            return lstModuleID;
        }

        private void GetAllNode(TreeListNode treenode)
        {
            checkedNodes.Add(treenode);
            foreach (TreeListNode tn in treenode.Nodes)
            {
                if(tn.Checked == !IsChecked)
                    db.spUpdatePqAccess((int)grvGroupUser.GetFocusedRowCellValue("GroupID"), (int)tn.GetValue("ModuleID"), !IsChecked);
                GetAllNode(tn);
            }
        }

        private List<int> GetNodesValues(List<TreeListNode> TreeNode)
        {
            if (TreeNode.Count == 0) return null;
            TreeNode.Sort(new Comparison<TreeListNode>(delegate(TreeListNode node1, TreeListNode node2)
            {
                return Comparer<int>.Default.Compare(node1.Id, node2.Id);
            }));
            List<int> lstModuleID = new List<int>();
            foreach (TreeListNode node in TreeNode)
                lstModuleID.Add((int)node.GetValue("ModuleID"));

            return lstModuleID;
        }

        List<TreeListNode> lstnode = new List<TreeListNode>();
        private void ReturnNodeRecursive(TreeListNode treeNode)
        {
            lstnode.Add(treeNode);
            foreach (TreeListNode tn in treeNode.Nodes)
            {
                if (ListModuleAlreadyHasInAccess.Contains((int)tn.GetValue("ModuleID")))
                    tn.CheckState = CheckState.Checked;
                ReturnNodeRecursive(tn);
            }

        }

        private void grvGroupUser_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            treeListModule.UncheckAll();
            var groupobj = db.pqNhoms.Single(p => p.GroupID == (int)grvGroupUser.GetFocusedRowCellValue("GroupID"));
            ListModuleAlreadyHasInAccess = db.pqAccessRights.Where(p => p.GroupID == (int)grvGroupUser.GetFocusedRowCellValue("GroupID") & p.IsAccessRight.Value).Select(p => p.ModuleID).ToList(); ;

            lstnode = new List<TreeListNode>();
            foreach (TreeListNode item in treeListModule.Nodes)
            {
                if (ListModuleAlreadyHasInAccess.Contains((int)item.GetValue("ModuleID")))
                    item.CheckState = CheckState.Checked;
                ReturnNodeRecursive(item);
                    
            }

            //var newlst = lstnode.Where(p => ListModuleAlreadyHasInAccess.Contains((int)p.GetValue("ModuleID")));
            //foreach (TreeListNode item in newlst)
            //{
            //    item.CheckState = CheckState.Checked;
            //}

            wait.Close();
            wait.Dispose();
        }

        private void btnOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Apply2();
        }

        private void frmPhanQuyenManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogBox.Question("Bạn có muốn lưu dữ liệu lại không?") == System.Windows.Forms.DialogResult.Yes)
            {
                Apply2();
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvGroupUser_FocusedRowChanged(null, null);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dbo = new MasterDataContext();
            var Nhom = dbo.pqNhoms;
            foreach (var i in Nhom)
            {
                var KTPQNhom = (from ac in dbo.pqModules
                    where !(from o in dbo.pqAccessRights
                        where o.GroupID == i.GroupID
                        select o.ModuleID)
                        .Contains(ac.ModuleID)
                    select ac.ModuleID);
                foreach (var j in KTPQNhom)
                {
                    var ins = new pqAccessRight();
                    ins.GroupID = i.GroupID;
                    ins.IsAccessRight = false;
                    ins.ModuleID = j;
                    dbo.pqAccessRights.InsertOnSubmit(ins);
                    dbo.SubmitChanges();

                }
            }
        }

        private void treeListModule_CustomRowFilter(object sender, DevExpress.XtraTreeList.CustomRowFilterEventArgs e)
        {
            //TreeList treeList = sender as TreeList;
            //string discount = treeList.GetRowCellDisplayText(e.Node, treeList.Columns["Discount"]);
            //if (discount == "15%")
            //{
            //    e.Visible = true;
            //    e.Handled = true;
            //}

            //DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            //if (treeList == null) return;
            //if (treeList.GetRowCellValue(e.Node, treeList.Columns["ModuleID"]) == null) return;
            ////var valueNode = treeList.GetRowCellValue(e.Node, treeList.Columns["ModuleID"]);
            //var objNotShow = db.pqModule_FormControls.Where(_ => _.IsHienThi == false & _.IsInMain == true & _.ModuleID == (int)treeList.GetRowCellValue(e.Node, treeList.Columns["ModuleID"])).ToList();
            //if (objNotShow.Count > 0)
            //{
            //    e.Visible = false;
            //}
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new LandsoftBuildingGeneral.Import.FrmImportPhanQuyen())
            {
                frm.ShowDialog();
            }
        }

        private void itemCapNhatTuFormMain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new Library.MasterDataContext();
            var modulesInMain = db.pq_PhanQuyenMain_Modules;
            foreach (var item in modulesInMain)
            {
                db.pqModule_Update(item.Name, item.Caption, item.ParentId, item.IsHienThi);
            }

            Library.DialogBox.Success();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog _save = new SaveFileDialog();
            _save.InitialDirectory = "";
            _save.Title = "Xuất tất cả tính năng phần mềm";
            _save.DefaultExt = "xlsx";
            _save.Filter = "Excel Files|*.xls;*.xlsx";
            _save.FilterIndex = 2;
            _save.RestoreDirectory = true;
            if (_save.ShowDialog() == DialogResult.OK)
            {
                XlsxExportOptions xport = new XlsxExportOptions();
                xport.TextExportMode = TextExportMode.Text;
                treeListModule.ExportToXlsx(_save.FileName, xport);
            }
        }
    }
}