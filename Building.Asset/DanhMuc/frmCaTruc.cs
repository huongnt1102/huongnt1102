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

namespace Building.Asset.DanhMuc
{
    public partial class frmCaTruc : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db;

        void ExportRecord()
        {
            var db = new MasterDataContext();
            try
            {

                var ltData = from n in db.tbl_CaTrucs
                             orderby n.ID
                             select new
                             {
                                 n.TenCaTruc,
                                 n.GhiChu,
                                 n.TuGio,
                                 n.DenGio,
                                 n.NguoiNhap,
                                 n.NgayNhap,
                                 n.NguoiSua,
                                 n.NgaySua,
                                 n.NgungSuDung
                             };

                var tblData = SqlCommon.LINQToDataTable(ltData);
                ExportToExcel.exportDataToExcel(tblData);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
        public frmCaTruc()
        {
            InitializeComponent();
        }

        private void frmCaTruc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            gridControl1.DataSource = _db.tbl_CaTrucs;
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
            gridView1.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
        }

        private void barButtonItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var obj = _db.tbl_CaTrucs.FirstOrDefault(_ => _.ID == (int)gridView1.GetFocusedRowCellValue("ID"));
                    if (obj != null)
                    {
                        _db.tbl_CaTrucs.DeleteOnSubmit(obj);
                    }

                    _db.SubmitChanges();
                    gridView1.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
                }
            }
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.tbl_CaTrucs.FirstOrDefault(_ => _.ID == (int)gridView1.GetFocusedRowCellValue("ID"));
                if (obj != null)
                {
                    _db.tbl_CaTrucs.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gridView1.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?)gridView1.GetFocusedRowCellValue("ID");
            if (id == null | id == 0) return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gridView1.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gridView1.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmCaTruc_Import())
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

        private void barButtonItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExportRecord();
        }
    }
}