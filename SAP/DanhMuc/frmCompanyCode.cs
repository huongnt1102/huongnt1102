﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace SAP.DanhMuc
{
    public partial class frmCompanyCode : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmCompanyCode()
        {
            InitializeComponent();
            
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Company Code", "Load");
            gc.DataSource = db.CompanyCodes; 
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            glkQuocGia.DataSource = db.QuocTiches;

            glkTinh.DataSource = db.Tinhs;
            glkLoaiTien.DataSource = db.LoaiTiens;

            LoadData();          
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Company Code", "Edit");
                grv.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật thành công!");
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
            }
        }

        private void grvKhoiNha_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    if (DialogBox.QuestionDelete() == DialogResult.No) return;
            //    try
            //    {
            //        var del = db.mbKhoiNhas.Single(p => p.MaKN == (int)grvTaiKhoan.GetFocusedRowCellValue("MaKN"));
            //        db.SubmitChanges();
            //        grvTaiKhoan.DeleteSelectedRows();
            //    }
            //    catch
            //    {
            //        DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            //        this.Close();
            //    }
            //}
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Company Code", "Xóa");
                var del = db.CompanyCodes.Single(p => p.ID == (int)grv.GetFocusedRowCellValue("ID"));
                db.CompanyCodes.DeleteOnSubmit(del);
                db.SubmitChanges();
                grv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grv.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvTaiKhoan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grv.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            //grv.SetFocusedRowCellValue("STT", grv.RowCount);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        void ImportRecord()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Company Code", "Import");
            using (var f = new Import.frmCompanyCode())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }
    }
}