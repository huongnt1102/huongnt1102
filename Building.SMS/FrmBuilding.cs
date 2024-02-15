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

namespace Building.SMS
{
    public partial class FrmBuilding : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db;
        public FrmBuilding()
        {
            InitializeComponent();
        }

        private void FrmBuilding_Load(object sender, EventArgs e)
        {
            _db = new MasterDataContext();

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;
            LoadData();
        }

        public class SmsBuildingClass
        {
            public int TemplateId { get; set; }
            public int? TyleId { get; set; }
            public bool? IsUse { get; set; }
            public string Name { get; set; }
            public string TyleName { get; set; }
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte?) itemBuilding.EditValue;
                gc.DataSource = _db.SmsTemplateDvs.Select(_ => new SmsBuildingClass
                {
                    TemplateId = _.Id,
                    IsUse = _db.SmsBuildings.FirstOrDefault(s=>s.TemplateId==_.Id & s.BuildingId == buildingId)!=null? _db.SmsBuildings.FirstOrDefault(s=>s.TemplateId == _.Id & s.BuildingId == buildingId).IsUse :false,
                    Name = _.Name,
                    TyleName = _.TyleName,
                    TyleId = _.TyleId
                }).ToList();
            }
            catch { }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                var buildingId = (byte?) itemBuilding.EditValue;

                for (var i = 0; i < gv.RowCount; i++)
                {
                    if (gv.GetRowCellValue(i, "TemplateId") == null) continue;

                    var templateId = (int?) gv.GetRowCellValue(i, "TemplateId");

                    var smsBuilding = _db.SmsBuildings.FirstOrDefault(_=>_.TemplateId == templateId & _.BuildingId == buildingId);
                    if (smsBuilding == null)
                    {
                        smsBuilding = new SmsBuilding();
                        _db.SmsBuildings.InsertOnSubmit(smsBuilding);
                    }

                    smsBuilding.BuildingId = buildingId;
                    smsBuilding.IsUse = (bool) gv.GetRowCellValue(i, "IsUse");
                    smsBuilding.TemplateId = templateId;
                    smsBuilding.Name = gv.GetRowCellValue(i, "Name").ToString();
                    smsBuilding.TyleId = (int?) gv.GetRowCellValue(i, "TyleId");
                    smsBuilding.TyleName = gv.GetRowCellValue(i, "TyleName").ToString();
                }

                _db.SubmitChanges();

                DialogBox.Success();
            }
            catch{}
        }
    }
}