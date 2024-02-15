using System;
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
    public partial class frmTanSuat : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db;

        void ExportRecord()
        {
            var db = new MasterDataContext();
            try
            {
             
                var ltData = from n in db.tbl_TanSuats
                             orderby n.ID
                             select new
                             {
                                n.TenTanSuat,
                                n.SoNgay,
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

        public frmTanSuat()
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

            gcTanSuat.DataSource = _db.tbl_TanSuats;
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTanSuat.AddNewRow();
            //grvTanSuat.SetFocusedRowCellValue("TenTanSuat", DateTime.Now);
            //grvTanSuat.SetFocusedRowCellValue("SoNgay", Common.User.MaNV);
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
                    var obj = _db.tbl_TanSuats.FirstOrDefault(_ => _.ID == (int)grvTanSuat.GetFocusedRowCellValue("ID"));
                    if (obj != null)
                    {
                        _db.tbl_TanSuats.DeleteOnSubmit(obj);
                    }

                    _db.SubmitChanges();
                    grvTanSuat.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
                }
            }
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvTanSuat.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {

                    var obj = _db.tbl_TanSuats.Single(_ => _.ID == (int)grvTanSuat.GetFocusedRowCellValue("ID"));
                    if (obj != null)
                    {
                        _db.tbl_TanSuats.DeleteOnSubmit(obj);
                    }
                    _db.SubmitChanges();
                    grvTanSuat.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
            finally
            {
                _db.Dispose();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //var id = (int?)grvTanSuat.GetFocusedRowCellValue("ID");
            //if (id == null | id == 0) return;
            //if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            //{
            //    grvTanSuat.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
            //    grvTanSuat.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            //}
        }

        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmTanSuat_Import())
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

        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < grvTanSuat.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (grvTanSuat.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = grvTanSuat.GetRowCellValue(i, fielName).ToString();
                    if (oldValue == value) return true;
                }
            }
            return false;
        }

        private void grvTanSuat_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var tt = grvTanSuat.GetFocusedRowCellValue("TenTanSuat");
            if (tt == null)
            {
                e.ErrorText = "Vui lòng nhập tên tần suất!";
                e.Valid = false;
                return;
            }

            if (IsDuplication("TenTanSuat", e.RowHandle, tt.ToString()))
            {
                e.ErrorText = "Hệ thống này đã tồn tại tần suất!";
                e.Valid = false;
                grvTanSuat.FocusedRowHandle = e.RowHandle;
                return;
            }
        }
    }
}