using LandsoftBuildingGeneral.Import;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DichVu.BanGiaoMatBang
{
    public partial class frmImportKeHoachBanGiaoNoiBo : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        public frmImportKeHoachBanGiaoNoiBo()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());

                    System.Collections.Generic.List<ImportUser> list = Library.Import.ExcelAuto.ConvertDataTable<ImportUser>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    excel = null;
                }
                catch (System.Exception ex)
                {
                    Library.DialogBox.Error(ex.Message);
                }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new System.Windows.Forms.OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                Library.DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = Library.DialogBox.WaitingForm();
            var db = new Library.MasterDataContext();
            try
            {
                var obj = (System.Collections.Generic.List<ImportUser>)gc.DataSource;
                var ltError = new System.Collections.Generic.List<ImportUser>();
                foreach (var n in obj)
                {
                    try
                    {
                        db = new Library.MasterDataContext();
                        #region Kiểm tra
                        
                        #endregion
                        var NV = Common.User.MaNV;
                        var ITEM = db.ho_Plans.FirstOrDefault(_ => _.No.ToLower() == n.MaSo.ToLower() & _.BuildingId == MaTN);
                        if (ITEM == null)
                        {
                            ITEM = new Library.ho_Plan();
                            ITEM.UserCreate = NV;
                            ITEM.UserCreateName = db.tnNhanViens.SingleOrDefault(p => p.MaNV == NV).HoTenNV;
                            ITEM.DateCreate = DateTime.Now;
                            ITEM.No = n.MaSo.ToUpper();
                            ITEM.Name = n.TenKeHoach;
                            ITEM.BuildingId = MaTN;
                            ITEM.DateHandOverFrom = n.TuNgay;
                            ITEM.DateHandOverTo = n.DenNgay;
                            ITEM.ContentAllow = n.GhiChu;
                            ITEM.IsLocal = true;
                            db.ho_Plans.InsertOnSubmit(ITEM);
                            db.SubmitChanges();
                        }
                        else if (ITEM != null)
                        {
                            ITEM.UserUpdate = NV;
                            ITEM.UserUpdateName = db.tnNhanViens.SingleOrDefault(p => p.MaNV == NV).HoTenNV;
                            ITEM.DateUpdate = DateTime.Now;
                            ITEM.No = n.MaSo.ToUpper();
                            ITEM.Name = n.TenKeHoach;
                            ITEM.DateHandOverFrom = n.TuNgay;
                            ITEM.DateHandOverTo = n.DenNgay;
                            ITEM.ContentAllow = n.GhiChu;
                            db.SubmitChanges();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }
                Library.DialogBox.Success();
                gc.DataSource = ltError.Count > 0 ? ltError : null;
            }
            catch
            {
                wait.Close();
                Library.DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
                this.Close();
            }
        }
    }
    public class ImportUser
    {
        public string MaSo { get; set; }
        public string TenKeHoach { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string GhiChu { get; set; }
        public string Error { get; set; }
    }
}
