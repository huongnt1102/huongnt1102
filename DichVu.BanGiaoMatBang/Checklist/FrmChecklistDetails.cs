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
    public partial class FrmChecklistDetails : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db;
        public FrmChecklistDetails()
        {
            InitializeComponent();
        }

        private void FrmChecklistDetails_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _db = new MasterDataContext();
                gc.DataSource = _db.ho_ChecklistDetails;
            }
            catch{}
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmChecklistDetailsEdit())
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
                    DialogBox.Error("Vui lòng chọn công việc cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmChecklistDetailsEdit { Id = (int?)gv.GetFocusedRowCellValue("Id") })
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
                        var checklistDetail = db.ho_ChecklistDetails.FirstOrDefault(_ =>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (checklistDetail != null)
                        {
                            var checklistDetailNew = new ho_ChecklistDetail
                            {
                                GroupName = checklistDetail.GroupName, Name = checklistDetail.Name, IsNotUse = false, Description = checklistDetail.Description, Stt = checklistDetail.Stt
                            };
                            db.ho_ChecklistDetails.InsertOnSubmit(checklistDetailNew);
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Không copy được.");
                return;
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.DeleteSelectedRows();
            }
            catch (Exception)
            {
                DialogBox.Error("Không xóa được.");
                return;
            }
        }

        private void ItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.FrmChecklistDetails())
                {
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
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
    }
}