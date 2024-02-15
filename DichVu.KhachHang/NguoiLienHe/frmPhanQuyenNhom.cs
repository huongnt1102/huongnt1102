using System;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmPhanQuyenNhom : XtraForm
    {
        public int? GroupId { get; set; }

        public MasterDataContext db = new MasterDataContext();
        public frmPhanQuyenNhom()
        {
            InitializeComponent();
        }

        private void frmPhanQuyenBaoCao_Load(object sender, EventArgs e)
        {
            Nap();
            if (GroupId == null) GetAllModule();
            else GetModuleByGroupId();
            gvModule.GroupPanelText = "";
        }

        void Nap()
        {
            var data = new MasterDataContext();
            gcNhom.DataSource = data.tnKhachHang_NguoiLienHe_NhomLienHes.Select(p => new { p.ID, p.TenNhomLienHe });

            
        }

        private void GetModuleByGroupId()
        {
            gcModule.DataSource = db.appModules.Where(_=>_.Id == GroupId).Select(p => new BaoCao  { Duyet = false, ID = p.Id, Name = p.Name });
        }

        private void GetAllModule()
        {
            gcModule.DataSource = db.appModules.Select(p => new BaoCao { Duyet = false, ID = p.Id, Name = p.Name });
        }

        private void grvToaNha_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void GetModule(int groupId)
        {
            if (GroupId == null) GetAllModule();
            else GetModuleByGroupId();

            var rp = "";
            var query = db.appModuleAccessRights.Where(p => p.ContactGroupId == groupId);
            foreach (var i in query)
            {
                rp += i.AppModuleId + ",";
                
            }


            rp = rp.TrimEnd(' ').TrimEnd(',');
            if (rp == "")
            {
               
                return;
                
            }

            var arrListStr = rp.Split(',');
            var dem = arrListStr.Length;
            for (var j = 0; j < gvModule.RowCount; j++)
            {
                
               
               
                 
                    for (var k = 0; k < dem; k++)
                    {
                        if ((int)gvModule.GetRowCellValue(j, "ID") == int.Parse(arrListStr[k]))
                        {
                            gvModule.SetRowCellValue(j, "Duyet", true);
                        }

                    }

                
            }
           
        }
        int? groupId { get;set; }
        private void grvToaNha_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            groupId = (int?)gvNhom.GetFocusedRowCellValue("ID");
            if (groupId == null) return;
            gvModule.GroupPanelText = gvNhom.GetFocusedRowCellValue("TenNhomLienHe").ToString();
            GetModule((int)groupId);
        }

        private void grvBaoCao_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            var dbo = new MasterDataContext();
            var bien = (CheckEdit)sender; 
            var query =
                    dbo.appModuleAccessRights.SingleOrDefault(
                        p => p.ContactGroupId == groupId & p.AppModuleId == (int?)gvModule.GetFocusedRowCellValue("ID"));
            if ((bool) bien.EditValue)
            {
                
                if (query == null)
                {
                    var accessRight = new appModuleAccessRight
                    {
                        AppModuleId = (int?) gvModule.GetFocusedRowCellValue("ID"), ContactGroupId = groupId, IsAccessRight = true
                    };
                    dbo.appModuleAccessRights.InsertOnSubmit(accessRight);
                    dbo.SubmitChanges();
                }
            }
            else
            {
                if (query != null)
                {
                   
                    dbo.appModuleAccessRights.DeleteOnSubmit(query);
                    dbo.SubmitChanges();
                }
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nap();
        }
    }

    class BaoCao
    {
        public bool Duyet { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}