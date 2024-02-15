﻿using System;
using System.Linq;
using System.Windows.Forms;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmScheduleGroup : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmScheduleGroup()
        {
            InitializeComponent();
        }

        private void FrmScheduleGroup_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            try
            {

                gc.DataSource = _db.ho_ScheduleGroups;
            }
            catch
            {
            }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void ItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.ho_ScheduleGroups.FirstOrDefault(_ => _.Id == (int)gv.GetFocusedRowCellValue("Id"));
                if (obj != null)
                {
                    _db.ho_ScheduleGroups.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void Gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
        }
    }
}