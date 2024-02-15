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

namespace ToaNha
{
    public partial class frmLoaiDichVu : DevExpress.XtraEditors.XtraForm
    {
        int _NewID;

        MasterDataContext db = new MasterDataContext();

        void LoadData()
        {
            treeList1.DataSource = db.dvLoaiDichVus;
            treeList1.ExpandAll();

            _NewID = db.dvLoaiDichVus.Max(p => p.ID);
            treeField.DataSource = db.dvDienGiais.Select(o => new { o.FieldName, o.DienGiai }); ;
        }

        void SetNewID()
        {
            _NewID++;
            treeList1.FocusedNode.SetValue("ID", _NewID);
            try
            {
                var idParent = treeList1.GetFocusedRowCellValue(ParentID);
                if (idParent != null && (int)idParent == 12)
                    treeList1.FocusedNode.SetValue("TableName", "dvDichVuKhac");
            }
            catch { }
        }

        public frmLoaiDichVu()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmLoaiSanPham_Load);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemAddChild.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAddChild_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSave_ItemClick);
            itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemClose_ItemClick);
        }

        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Success("Dữ liệu đã được lưu");

                this.LoadData();
            }
            catch { }
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                var _IsXoa = (bool?)treeList1.FocusedNode.GetValue("IsXoa");

                if (_IsXoa == false)
                {
                    DialogBox.Error("Không thể xóa vì đây là dịch vụ cố định");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;



                treeList1.DeleteNode(treeList1.FocusedNode);
            }
        }

        void itemAddChild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.Select();
            if (treeList1.FocusedNode != null)
            {
                treeList1.FocusedNode = treeList1.AppendNode(null, treeList1.FocusedNode);
                this.SetNewID();
            }
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.Select();
            treeList1.FocusedNode = treeList1.AppendNode(null, null);
            this.SetNewID();
        }

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var DienGiai = treeList1.GetFocusedRowCellValue(colCTDG);

            var Table_Name = treeList1.GetFocusedRowCellValue(TableName);

            var id = treeList1.GetFocusedRowCellValue(ID);

            if (DienGiai == null)
                DialogBox.Alert("Loại dịch vụ này chưa cài đặt diễn giải");

            using (var frm = new frmViewDienGiai())
            {
                frm.MaLDV = (int)id;
                frm.DienGiai = DienGiai.ToString();
                frm.TableName = Table_Name.ToString();
                frm.ShowDialog();
            }


        }
    }
}