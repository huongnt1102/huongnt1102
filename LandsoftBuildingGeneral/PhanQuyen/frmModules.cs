using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandsoftBuildingGeneral.PhanQuyen
{
    public partial class frmModules : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmModules()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmModules_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                treeModules.DataSource = db.pqModules;
                lookForm.DataSource = db.pqForms.OrderBy(p=>p.FormName);
                //treeModules.ExpandAll();
                //ModuleID = treeModules.Nodes.Count >0 ? db.pqModules.Max(p=>p.ModuleID) : (int)0;
            }
            catch { }
            
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeModules.FocusedNode = treeModules.AppendNode(null, null);
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                treeModules.RefreshNode(treeModules.FocusedNode);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu");
                LoadData();
            }
            catch(Exception ex) { XtraMessageBox.Show("Mã lỗi: " + ex.GetType().FullName + "\n" + "Chi tiết: " + ex.Message + "\n" + "Tác nhân gây lỗi: " + "Lưu", ex.GetType().FullName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnThemCap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeModules.FocusedNode != null)
            {
                //treeModules.AppendNode(null, treeModules.FocusedNode);
                treeModules.FocusedNode = treeModules.AppendNode(null, treeModules.FocusedNode);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeModules.FocusedNode != null)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    treeModules.DeleteNode(treeModules.FocusedNode);
                }
                catch {}
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void treeModules_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (gcControl.DataSource != null) gcControl.DataSource = null;
            
            var val = (pqModule)treeModules.GetDataRecordByNode(treeModules.FocusedNode);
            gcControl.DataSource = val.pqModule_FormControls;
        }

        private void grvControl_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvControl.SetFocusedRowCellValue("IsAccess", false);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvControl.DeleteSelectedRows();
        }

        private void itemCapNhatIdToAppYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var val = (pqModule)treeModules.GetDataRecordByNode(treeModules.FocusedNode);
            // cập nhật module id vào table pqmodule_yeucau
            //using(var data = new Library.MasterDataContext())
            //{
            //    var moduleYeuCau = data.pqModule_YeuCaus;
            //    foreach(var item in moduleYeuCau)
            //    {
            //        item.ModuleParentId = val.ModuleID;
            //        item.ModuleParentName = val.Name;
            //    }
            //    data.SubmitChanges();
            //}
            Library.DialogBox.Alert("Module: " + val.ModuleID+" - Module name: "+val.Name);
        }

        private void itemViewId_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var val = (pqModule)treeModules.GetDataRecordByNode(treeModules.FocusedNode);
            Library.DialogBox.Alert("Module: " + val.ModuleID + " - Module name: " + val.Name);
        }

        private void itemCaiDatHanhDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using(var frm = new LandsoftBuildingGeneral.App.FrmHanhDong()) { frm.ShowDialog(); }
        }
    }
}