using System;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace LandSoftBuilding.Report
{
    public partial class frmPhanQuyenBaoCao : XtraForm
    {
        public byte? GroupId { get; set; }

        public MasterDataContext Db = new MasterDataContext();
        public frmPhanQuyenBaoCao()
        {
            InitializeComponent();
        }

        private void frmPhanQuyenBaoCao_Load(object sender, EventArgs e)
        {
            gcToaNha.DataSource = Db.tnToaNhas.Select(p=>new {p.TenTN,p.TenVT,p.MaTN,p.DiaChi,p.DienThoai});
            //gcBaoCao.DataSource = Db.rptReports.Select(p=> new BaoCao {Duyet = false,ID = p.ID,Name = p.Name});
            if (GroupId == null) GetBaoCaoAll();
            else GetBaoCaoByGroupId();
            grvBaoCao.GroupPanelText = "";

        }

        private void GetBaoCaoByGroupId()
        {
            gcBaoCao.DataSource = Db.rptReports.Where(_=>_.GroupID == GroupId).Select(p => new BaoCao { Duyet = false, ID = p.ID, Name = p.Name });
        }

        private void GetBaoCaoAll()
        {
            gcBaoCao.DataSource = Db.rptReports.Select(p => new BaoCao { Duyet = false, ID = p.ID, Name = p.Name });
        }

        private void grvToaNha_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void GetBaoCao(int tn)
        {
            //gcBaoCao.DataSource = Db.rptReports.Select(p => new BaoCao { Duyet = false, ID = p.ID, Name = p.Name });
            if (GroupId == null) GetBaoCaoAll();
            else GetBaoCaoByGroupId();

            var rp = "";
            var query = Db.rptReports_ToaNhas.Where(p => p.MaTN == tn);
            foreach (var i in query)
            {
                rp += i.ReportID + ",";
                
            }


            rp = rp.TrimEnd(' ').TrimEnd(',');
            if (rp == "")
            {
               
                return;
                
            }

            var arrListStr = rp.Split(',');
            var dem = arrListStr.Length;
            for (var j = 0; j < grvBaoCao.RowCount; j++)
            {
                
               
               
                 
                    for (var k = 0; k < dem; k++)
                    {
                        if ((int)grvBaoCao.GetRowCellValue(j, "ID") == int.Parse(arrListStr[k]))
                        {
                            grvBaoCao.SetRowCellValue(j, "Duyet", true);
                        }

                    }

                
            }
           
        }
        byte? Idtn { get;set; }
        private void grvToaNha_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Idtn = (byte?)grvToaNha.GetFocusedRowCellValue("MaTN");
            if (Idtn == null) return;
            grvBaoCao.GroupPanelText = grvToaNha.GetFocusedRowCellValue("TenTN").ToString();
            GetBaoCao((int)Idtn);
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
                    dbo.rptReports_ToaNhas.SingleOrDefault(
                        p => p.MaTN == Idtn & p.ReportID == (int?)grvBaoCao.GetFocusedRowCellValue("ID"));
            if ((bool) bien.EditValue)
            {
                
                if (query == null)
                {
                    var baoCaoTn = new rptReports_ToaNha
                    {
                        ReportID = (int?) grvBaoCao.GetFocusedRowCellValue("ID"), MaTN = Idtn
                    };
                    dbo.rptReports_ToaNhas.InsertOnSubmit(baoCaoTn);
                    dbo.SubmitChanges();
                }
            }
            else
            {
                if (query != null)
                {
                   
                    dbo.rptReports_ToaNhas.DeleteOnSubmit(query);
                    dbo.SubmitChanges();
                }
            }
        }
    }

    class BaoCao
    {
        public bool Duyet { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}