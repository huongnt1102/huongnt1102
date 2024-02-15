using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using LinqToExcel;

namespace DichVu.Nuoc
{
    public partial class frmUuDai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmUuDai()
        {
            InitializeComponent();
            gvUuDaiNuoc.InvalidRowException += Library.Common.InvalidRowException;
        }

        void LoadData()
        {
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                gcUuDaiNuoc.DataSource = db.dvNuocUuDais.Where(p => p.MaTN == maTN);
            }
            catch 
            {
                gcUuDaiNuoc.DataSource = null;
            }
        }

        void Delete()
        {
            var collection = gvUuDaiNuoc.GetSelectedRows();
            if (collection.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var item in collection)
                {
                    var obj = db.dvNuocUuDais.Single(p => p.ID == (int)gvUuDaiNuoc.GetRowCellValue(item, "ID"));
                    db.dvNuocUuDais.DeleteOnSubmit(obj);
                }
                db.SubmitChanges();
                gvUuDaiNuoc.DeleteSelectedRows();
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

        private void frmUuDaiNuoc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvUuDaiNuoc.AddNewRow();
        }

        private void gcDinhMucNuoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvUuDaiNuoc.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvDinhMucNuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var maTN = gvUuDaiNuoc.GetRowCellValue(e.RowHandle, "MaTN");
            if (maTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            byte? maTN = (byte?)itemToaNha.EditValue;
            if (maTN == null) return;

            glMatBang.DataSource = db.mbMatBangs.Where(p => p.MaTN == maTN)
               .Select(p => new { p.MaMB, p.MaSoMB });

            LoadData();
        }

        private void gvDinhMucNuoc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvUuDaiNuoc.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void gvUuDaiNuoc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoNguoi" || e.Column.FieldName == "SoLuong")
            {
                gvUuDaiNuoc.SetRowCellValue(e.RowHandle, "MucUuDai", 
                    (int?)gvUuDaiNuoc.GetFocusedRowCellValue("SoNguoi") * 
                    (int?)gvUuDaiNuoc.GetFocusedRowCellValue("SoLuong"));

            }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var tblData = excel.Worksheet(0);

                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == (byte?)itemToaNha.EditValue
                                 orderby mb.MaSoMB
                                 select new { mb.MaMB, mb.MaSoMB, mb.MaKH }).ToList();

                foreach (var i in tblData)
                {
                    var _SoNguoi = i[1].Cast<int>();
                    if(_SoNguoi <= 0) continue;

                    var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB.ToLower() == i[0].Cast<string>().ToLower());

                    gvUuDaiNuoc.AddNewRow();

                    gvUuDaiNuoc.SetFocusedRowCellValue("MaMB", objMB.MaMB);
                    gvUuDaiNuoc.SetFocusedRowCellValue("SoNguoi", _SoNguoi);
                    gvUuDaiNuoc.SetFocusedRowCellValue("SoLuong", i[2].Cast<int>());
                    gvUuDaiNuoc.SetFocusedRowCellValue("MucUuDai", i[3].Cast<int>());
                    gvUuDaiNuoc.SetFocusedRowCellValue("DienGiai", i[4].Cast<string>());
                }

                gvUuDaiNuoc.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }
    }
}