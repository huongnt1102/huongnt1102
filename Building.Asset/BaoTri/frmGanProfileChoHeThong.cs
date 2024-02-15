using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using DevExpress.XtraPrinting;
namespace Building.Asset.BaoTri
{
    public partial class frmGanProfileChoHeThong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmGanProfileChoHeThong()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            var obj = (from ht in db.tbl_NhomTaiSans
                       where ht.MaTN == (byte?)itemToaNha.EditValue
                       select new
                       {
                           ID = "NTS" + ht.ID.ToString(),
                           IDName = ht.ID,
                           Name = ht.TenNhomTaiSan,
                           ParentID = "0"
                       }).Union(from ht in db.tbl_NhomTaiSans
                                join lts in db.tbl_LoaiTaiSans on ht.ID equals lts.NhomTaiSanID
                                where ht.MaTN == (byte?)itemToaNha.EditValue
                                select new
                                {
                                    ID = "LTS" + lts.ID.ToString(),
                                    IDName = lts.ID,
                                    Name = lts.TenLoaiTaiSan,
                                    ParentID = "NTS" + lts.NhomTaiSanID.ToString()
                                }).Union(from ht in db.tbl_NhomTaiSans
                                         join lts in db.tbl_LoaiTaiSans on ht.ID equals lts.NhomTaiSanID
                                         join tts in db.tbl_TenTaiSans on lts.ID equals tts.LoaiTaiSanID
                                         where ht.MaTN == (byte?)itemToaNha.EditValue
                                         select new
                                         {
                                             ID = "TTS" + tts.ID.ToString(),
                                             IDName = tts.ID,
                                             Name = tts.TenTaiSan,
                                             ParentID = "LTS" + tts.LoaiTaiSanID.ToString()
                                         }).ToList();
            obj.Add(new { ID = "0", IDName = 0, Name = "ROOT", ParentID = "" });
            treeList1.DataSource = (from t in obj
                                    join pf in db.tbl_Profile_GanHeThongs on t.ID equals pf.TaiSanID into _temp
                                    from pf in _temp.DefaultIfEmpty()
                                    select new GanProfileChoTaiSan
                                    {
                                        ID = t.ID,
                                        IDName = t.IDName,
                                        Name = t.Name,
                                        ParentID = t.ParentID,
                                        ProfileID = pf == null ? (int?)null : pf.ProfileID,
                                        IDProfileBaoTri = pf == null ? (int?)null : pf.IDProfileBaoTri
                                    }).ToList();
            treeList1.ExpandAll();

        }
        private void frmGanProfileChoHeThong_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            rspToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LoadData();

        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
            rspProfileVanHanh.DataSource = db.tbl_Profiles.Where(p => p.MaTN == (byte?)itemToaNha.EditValue & p.IsDuyet.GetValueOrDefault() == true & p.LoaiID == 2);
            rspProfileBaoTri.DataSource = db.tbl_Profiles.Where(p => p.MaTN == (byte?)itemToaNha.EditValue & p.IsDuyet.GetValueOrDefault() == true & p.LoaiID == 1);
        }

        private class GanProfileChoTaiSan
        {
            public string ID { set; get; }
            public string Name { set; get; }
            public int? IDName { set; get; }
            public string ParentID { set; get; }
            public int? ProfileID { set; get; }
            public int? IDProfileBaoTri { get; set; }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.Focus();
            DevExpress.XtraTreeList.Nodes.TreeListNode oMainNode = treeList1.Nodes[0];
            PrintNodesRecursive(oMainNode);
            db.SubmitChanges();
            DialogBox.Alert("Đã lưu dữ liệu thành công!");

        }
        public void PrintNodesRecursive(DevExpress.XtraTreeList.Nodes.TreeListNode oParentNode)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode oSubNode in oParentNode.Nodes)
            {
                var objKT = db.tbl_Profile_GanHeThongs.FirstOrDefault(p => p.MaTN == (byte?)itemToaNha.EditValue && p.TaiSanID.Equals(oSubNode["ID"].ToString()));
                if (objKT == null)
                {
                    objKT = new tbl_Profile_GanHeThong();
                    objKT.IDName = Convert.ToInt32(oSubNode["IDName"]);
                    objKT.MaTN = (byte?)itemToaNha.EditValue;
                    objKT.ProfileID = (int?)oSubNode["ProfileID"];
                    objKT.TaiSanID = oSubNode["ID"].ToString();
                    objKT.IDProfileBaoTri = (int?)oSubNode["IDProfileBaoTri"];
                    db.tbl_Profile_GanHeThongs.InsertOnSubmit(objKT);
                }
                else
                {
                    objKT.IDName = Convert.ToInt32(oSubNode["IDName"]);
                    objKT.MaTN = (byte?)itemToaNha.EditValue;
                    objKT.ProfileID = (int?)oSubNode["ProfileID"];
                    objKT.IDProfileBaoTri = (int?)oSubNode["IDProfileBaoTri"];
                    objKT.TaiSanID = oSubNode["ID"].ToString();
                }
                PrintNodesRecursive(oSubNode);
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void rspProfileVanHanh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                treeList1.FocusedNode.SetValue(colProfileVanHanh, (int?)null);
                treeList1.RefreshCell(treeList1.FocusedNode, colProfileVanHanh);
            }
        }

        private void rspProfileBaoTri_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                treeList1.FocusedNode.SetValue(colProfileBaoTri, (int?)null);
                treeList1.RefreshCell(treeList1.FocusedNode, colProfileBaoTri);
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog _save = new SaveFileDialog();
            _save.InitialDirectory = "";
            _save.Title = "Xuất danh mục tài sản";
            _save.DefaultExt = "xlsx";
            _save.Filter = "Excel Files|*.xls;*.xlsx";
            _save.FilterIndex = 2;
            _save.RestoreDirectory = true;
            if (_save.ShowDialog() == DialogResult.OK)
            {
                XlsxExportOptions xport = new XlsxExportOptions();
                xport.TextExportMode = TextExportMode.Text;
                treeList1.ExportToXlsx(_save.FileName, xport);
            }
        }
    }
}