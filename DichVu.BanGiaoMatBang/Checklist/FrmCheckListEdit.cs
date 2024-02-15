using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Checklist
{
    public partial class FrmCheckListEdit : XtraForm
    {
        public int? Id { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_ListChecklist _l;
        private List<ListChecklist> _lChecklist = new List<ListChecklist>();

        public FrmCheckListEdit()
        {
            InitializeComponent();
        }

        private void FrmCheckListEdit_Load(object sender, EventArgs e)
        {
            if (Id != null)
            {
                _l = _db.ho_ListChecklists.FirstOrDefault(_ => _.Id == Id);
                if (_l != null)
                {
                    txtNameList.Text = _l.Name;
                    IEnumerable<ListChecklist> checklistItem = _db.ho_Checklists.Where(_ => _.ListChecklistId == _l.Id & _.ChecklistDetailsId != null)
                        .Select(_ => new ListChecklist { Id = _.Id, Name = _.Name, GroupName = _.GroupName, Stt = _.Stt, IsChoose = _.IsNotUse.GetValueOrDefault(), ChecklistDetailsId = _.ChecklistDetailsId,Description=_.Description }).AsEnumerable();

                    //IEnumerable<ListChecklist> listChecklists = checklistItem as ListChecklist[] ?? checklistItem.ToArray();

                    // nếu đặt enumerable thành to array hoặc to list, linq báo lỗi khi run: không thể truy xuất biến cục bộ
                    // chuyển enumerable, báo lỗi trả nhiều kết quả, mỗi lần trả where qua 3 lần, dễ dẫn đến tình trạng thắt cổ chai khi dữ liệu quá lớn
                    // khắc phục làm sao?
                    _lChecklist = _db.ho_ChecklistDetails.Where(_ => _.IsNotUse != true).Select(_ => new ListChecklist
                    {
                        Id = checklistItem.FirstOrDefault(item => item.ChecklistDetailsId == _.Id) != null ? checklistItem.First(item => item.ChecklistDetailsId == _.Id).Id : 0,
                        Name = checklistItem.FirstOrDefault(item => item.ChecklistDetailsId == _.Id) != null
                            ? checklistItem.First(item => item.ChecklistDetailsId == _.Id).Name
                            : _.Name,
                        GroupName = checklistItem.FirstOrDefault(item => item.ChecklistDetailsId == _.Id) != null
                            ? checklistItem.First(item => item.ChecklistDetailsId == _.Id).GroupName
                            : _.GroupName,
                        Stt = checklistItem.FirstOrDefault(item => item.ChecklistDetailsId == _.Id) != null
                            ? checklistItem.First(item => item.ChecklistDetailsId == _.Id).Stt
                            : _.Stt,
                        IsChoose = checklistItem.FirstOrDefault(item => item.ChecklistDetailsId == _.Id) != null ? !checklistItem.First(item => item.ChecklistDetailsId == _.Id).IsChoose : false,
                        ChecklistDetailsId = _.Id,
                        Description = checklistItem.FirstOrDefault(i=>i.ChecklistDetailsId == _.Id)!=null?checklistItem.First(i=>i.ChecklistDetailsId==_.Id).Description:_.Description
                    }).ToList();
                }
            }
            else
            {
                _l = new ho_ListChecklist();
                _db.ho_ListChecklists.InsertOnSubmit(_l);
                _lChecklist = _db.ho_ChecklistDetails.Where(_ => _.IsNotUse != true).Select(_ => new ListChecklist
                {
                    Id = 0,
                    Name = _.Name,
                    GroupName = _.GroupName,
                    Stt = _.Stt,
                    IsChoose = false,
                    ChecklistDetailsId = _.Id,
                    Description=_.Description
                }).ToList();
            }

            gc.DataSource = _lChecklist;
        }

        public class ListChecklist
        {
            public int? Id { get; set; }
            public int? ChecklistDetailsId { get; set; }
            public string Name { get; set; }
            public string GroupName { get; set; }
            public string Stt { get; set; }
            public string Description { get; set; }
            public bool? IsChoose { get; set; }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();

                _l.Name = txtNameList.Text;

                for (var i = 0; i < gv.RowCount; i++)
                {
                    if (gv.GetRowCellValue(i, "ChecklistDetailsId") == null) continue;
                    var checklist = new ho_Checklist();
                    var checklistDetailsId = (int?)gv.GetRowCellValue(i, "ChecklistDetailsId");

                    if((int?)gv.GetRowCellValue(i,"Id")!=0)
                    {
                        var id = (int?) gv.GetRowCellValue(i, "Id");
                            
                        checklist = _db.ho_Checklists.FirstOrDefault(_ => _.Id == id) ?? new ho_Checklist();
                    }

                    checklist.Name = gv.GetRowCellValue(i, "Name").ToString();
                    checklist.GroupName = gv.GetRowCellValue(i, "GroupName").ToString();
                    checklist.ChecklistDetailsId = checklistDetailsId;
                    checklist.IsNotUse = !(bool?) gv.GetRowCellValue(i, "IsChoose");
                    checklist.ListChecklistName = txtNameList.Text;
                    checklist.Stt = gv.GetRowCellValue(i, "Stt").ToString();
                    checklist.Description = gv.GetRowCellValue(i, "Description").ToString();
                    _l.ho_Checklists.Add(checklist);
                }

                _db.SubmitChanges();

                DialogBox.Success();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}