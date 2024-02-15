using System.Linq;

namespace Building.KinhPhiDuKien.Import
{
    public partial class FrmMoneyPurposeItemsImport : DevExpress.XtraEditors.XtraForm
    {
        public bool IsSave { get; set; }
        public int? MpId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmMoneyPurposeItemsImport()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, System.EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());
                    System.Collections.Generic.List<ImportMoneyPurposeItems> list = Library.Import.ExcelAuto.ConvertDataTable<ImportMoneyPurposeItems>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new ImportMoneyPurposeItems
                    //{
                    //    No = _["Mã hạng mục"].ToString().Trim(),
                    //    Name = _["Tên hạng mục"].ToString().Trim(),
                    //    MoneyPurpose=_["Kinh phí dự kiến"].Cast<decimal>(),
                    //}).ToList();

                    excel = null;
                }
                catch (System.Exception ex)
                {
                    Library.DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private Library.mp_MoneyPurpose GetMoneyPurpose(int? id)
        {
            return _db.mp_MoneyPurposes.FirstOrDefault(_ => _.Id == id);
        }

        private Library.mp_MoneyPurposeItem GetMoneyPurposeItem(string no)
        {
            return _db.mp_MoneyPurposeItems.FirstOrDefault(_ => _.No.ToLower() == no.ToLower())??new Library.mp_MoneyPurposeItem();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                Library.DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var mp = GetMoneyPurpose(MpId);

            if (mp == null)
            {
                Library.DialogBox.Error("Vui lòng nhập kinh phí cả năm trước");
                return;
            }

            var wait = Library.DialogBox.WaitingForm();
            try
            {
                var lMpi = (System.Collections.Generic.List<ImportMoneyPurposeItems>) gc.DataSource;
                var ltError = new System.Collections.Generic.List<ImportMoneyPurposeItems>();
                foreach (var n in lMpi)
                {
                    try
                    {
                        Library.mp_MoneyPurposeItem _mpi = GetMoneyPurposeItem(n.No.ToLower());
                        if (_mpi.Id == 0)
                        {
                            _mpi.BuildingId = mp.BuildingId;
                            _mpi.DateCreate = System.DateTime.UtcNow.AddHours(7);
                            _mpi.MoneyUsed = 0;
                            _mpi.UserCreateId = Library.Common.User.MaNV;
                            _mpi.UserCreateName = Library.Common.User.HoTenNV;
                            _db.mp_MoneyPurposeItems.InsertOnSubmit(_mpi);
                        }

                        _mpi.MoneyPurposeId = mp.Id;
                        _mpi.MoneyPurpose = n.MoneyPurpose;
                        _mpi.Name = n.Name;
                        _mpi.MoneyExist = _mpi.MoneyPurpose - _mpi.MoneyUsed;
                        _mpi.Year = mp.Year;
                        _mpi.No = n.No;

                        mp.MoneyUsed = mp.MoneyUsed + _mpi.MoneyPurpose;
                        mp.MoneyExist = mp.MoneyPurpose - mp.MoneyUsed;
                        mp.DateUpdate = System.DateTime.UtcNow.AddHours(7);
                        mp.UserUpdateId = Library.Common.User.MaNV;
                        mp.UserUpdateName = Library.Common.User.HoTenNV;
                        
                        _db.SubmitChanges();
                    }
                    catch (System.Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
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
                _db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new System.Windows.Forms.OpenFileDialog();
            try
            {
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

    }

    public class ImportMoneyPurposeItems
    {
        public string No { get; set; }
        public string Name { get; set; }
        public decimal? MoneyPurpose { get; set; }

        public string Error { get; set; }
    }
}