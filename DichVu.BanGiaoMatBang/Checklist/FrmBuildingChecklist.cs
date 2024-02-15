using System;
using System.Linq;
using Library;

namespace DichVu.BanGiaoMatBang.Checklist
{
    public partial class FrmBuildingChecklist : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmBuildingChecklist()
        {
            InitializeComponent();
        }

        private void FrmBuildingChecklist_Load(object sender, EventArgs e)
        {
            _db = new MasterDataContext();

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // hiện tại bỏ đi cái lọc theo loại mặt bằng, do lúc lên kế hoạch, trong từng gridview chọn từng loại khác nhau có vẻ hơi chậm. Nếu sau này bắt buộc thì miễn cưỡng làm lại cũng được.
                var maTn = (byte?) itemToaNha.EditValue;
                //var apartmentTyle = (int?)itemApartmentTyle.EditValue ?? 0; // & _.ApartmentTyleId == apartmentTyle //& _.ApartmentTyleId == apartmentTyle
                gc.DataSource = _db.ho_ListChecklists.Select(p => new BuildingChecklist
                {
                    Id = p.Id,
                    IsUse =
                        _db.ho_BuildingChecklists.FirstOrDefault(_ =>
                            _.ListChecklistId == p.Id & _.BuildingId == maTn) != null
                            ? !_db.ho_BuildingChecklists.First(_ => _.ListChecklistId == p.Id & _.BuildingId == maTn )
                                .IsNotUse
                            : false,
                    Name = p.Name
                }).ToList();
            }
            catch{}
        }

        private void LoadDetail()
        {
            try
            {
                var id = (int?) gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = _db.ho_Checklists.Where(_ => _.ListChecklistId == id & _.IsNotUse != true).Select(_ => new { _.Id,_.Name,_.GroupName,_.Stt,_.Description}).ToList();
            }
            catch{}
        }

        public class BuildingChecklist
        {
            public int? Id { get; set; }
            public bool? IsUse { get; set; }
            public string Name { get; set; }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //var db = new MasterDataContext();
                var maTn = (byte?) itemToaNha.EditValue;
                //var apartmentTyle = (int?) itemApartmentTyle.EditValue;

                //if (apartmentTyle == null)
                //{
                //    DialogBox.Error("Vui lòng chọn loại mặt bằng");
                //    return;
                //}

                for (var i = 0; i < gv.RowCount; i++)
                {
                    if (gv.GetRowCellValue(i, "Id") == null) continue;

                    var listChecklistId = (int?)gv.GetRowCellValue(i, "Id");
                    ho_BuildingChecklist buildingChecklist;

                    buildingChecklist = _db.ho_BuildingChecklists.FirstOrDefault(_ =>
                        _.ListChecklistId == listChecklistId & _.BuildingId == maTn ); //&_.ApartmentTyleId == apartmentTyle
                    if (buildingChecklist == null)
                    {
                        buildingChecklist = new ho_BuildingChecklist();
                        _db.ho_BuildingChecklists.InsertOnSubmit(buildingChecklist);
                    }

                    buildingChecklist.BuildingId = maTn;
                    //buildingChecklist.ApartmentTyleId = apartmentTyle;
                    buildingChecklist.IsNotUse = !(bool?) gv.GetRowCellValue(i, "IsUse");
                    buildingChecklist.ListChecklistId = listChecklistId;
                    buildingChecklist.ListChecklistName = gv.GetRowCellValue(i, "Name").ToString();
                    
                }

                _db.SubmitChanges();

                DialogBox.Success();

            }
            catch{}
        }

        private void ItemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            //lkApartmentTyle.DataSource = _db.mbLoaiMatBangs.Where(_ => _.MaTN == (byte) itemToaNha.EditValue);
        }

        private void Gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }
    }
}