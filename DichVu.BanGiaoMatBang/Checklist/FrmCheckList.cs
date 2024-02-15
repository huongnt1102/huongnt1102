using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Checklist
{
    public partial class FrmCheckList : DevExpress.XtraEditors.XtraForm

    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmCheckList()
        {
            InitializeComponent();
        }


        private void FrmCheckList_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            try
            {
                gc.DataSource = _db.ho_ListChecklists;

                LoadDetail();
            }
            catch{}
        }

        private void LoadDetail()
        {
            var id = (int?) gv.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                gcChiTiet.DataSource = null;
                return;
            }

            gcChiTiet.DataSource = _db.ho_Checklists.Where(_ => _.ListChecklistId == id & _.IsNotUse !=true).OrderBy(_=>_.Stt).ToList();
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();
                DialogBox.Success();
            }
            catch{}
        }

        private void ItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmCheckListEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn mẫu checklist cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmCheckListEdit { Id = (int?)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những phiếu cần copy");
                        return;
                    }
                    if (DialogBox.Question("Bạn có muốn copy?") == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var listChecklist = db.ho_ListChecklists.FirstOrDefault(_ =>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (listChecklist != null)
                        {
                            var listChecklistNew = new ho_ListChecklist();
                            listChecklistNew.Name = listChecklist.Name + " - Copy";
                            db.ho_ListChecklists.InsertOnSubmit(listChecklistNew);

                            foreach (var item in listChecklist.ho_Checklists)
                            {
                                var objItem = db.ho_Checklists.FirstOrDefault(_ => _.Id == item.Id);
                                if (objItem != null)
                                {
                                    var itemNew = new ho_Checklist();
                                    itemNew.IsNotUse = objItem.IsNotUse;
                                    itemNew.ListChecklistName = listChecklist.Name + " - Copy";
                                    itemNew.Name = objItem.Name;
                                    itemNew.Stt = objItem.Stt;
                                    itemNew.ChecklistDetailsId = objItem.ChecklistDetailsId;
                                    itemNew.GroupName = objItem.GroupName;
                                    itemNew.Description = objItem.Description;

                                    listChecklistNew.ho_Checklists.Add(itemNew);
                                }
                            }
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Không copy được.");
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                        return;
                    }
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var listChecklist = db.ho_ListChecklists.FirstOrDefault(_ =>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (listChecklist != null)
                        {
                            db.ho_Checklists.DeleteAllOnSubmit(listChecklist.ho_Checklists);
                            db.ho_ListChecklists.DeleteOnSubmit(listChecklist);
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Không xóa được.");
            }
        }

        private void ItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }
    }
}