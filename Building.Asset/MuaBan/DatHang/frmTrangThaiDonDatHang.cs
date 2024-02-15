﻿using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace TaiSan.DatHang
{
    public partial class frmTrangThaiDonDatHang : DevExpress.XtraEditors.XtraForm
    {

        public frmTrangThaiDonDatHang()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        } 

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            LoadData();
        } 

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvTrangThaiDonDatHang.AddNewRow();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvTrangThaiDonDatHang.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtTenDeXuat_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvTrangThaiDeXuat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grvTrangThaiDeXuat_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;
                    grvTrangThaiDonDatHang.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grvTrangThaiDeXuat_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
           
        }

        
        private void LoadData()
        {

        }

    }
}