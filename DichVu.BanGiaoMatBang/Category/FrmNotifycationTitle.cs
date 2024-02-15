﻿using System;
using System.Linq;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Category
{
    public partial class FrmNotifycationTitle : XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmNotifycationTitle()
        {
            InitializeComponent();
        }

        private void FrmDuty_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            //lkBuilding.DataSource = Common.TowerList;
            //itemBuilding.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                //var buildingId = (byte) itemBuilding.EditValue;
                gc.DataSource = _db.ho_NotifycationTitles;
            }
            catch
            {
                //
            }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                _db.SubmitChanges();
                DialogBox.Success();
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu");
                return;
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.DeleteSelectedRows();
        }
    }
}