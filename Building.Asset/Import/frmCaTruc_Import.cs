using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmCaTruc_Import : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmCaTruc_Import()
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

                    System.Collections.Generic.List<CaTruc> list = Library.Import.ExcelAuto.ConvertDataTable<CaTruc>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new CaTruc
                    //{
                    //    TenCaTruc = _["Tên ca trực"].ToString().Trim(),
                    //    TuGio = _["Từ giờ"].ToString().Trim(),
                    //    DenGio=_["Đến giờ"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),
                    //    NgungSuDung = _["Ngưng sử dụng"].Cast<bool>()
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objCaTruc = (List<CaTruc>) gc.DataSource;
                var ltError = new List<CaTruc>();
                foreach (var n in objCaTruc)
                {
                    try
                    {
                        db = new MasterDataContext();
                        var kt = db.tbl_CaTrucs.FirstOrDefault(_ => _.TenCaTruc == n.TenCaTruc.ToLower());
                        if (kt != null)
                        {
                            kt.TenCaTruc = n.TenCaTruc;
                            kt.GhiChu = n.DienGiai;
                            kt.NgungSuDung = n.NgungSuDung;
                            kt.NgaySua = DateTime.Now;
                            kt.NguoiSua = Common.User.MaNV;
                            kt.TuGio = n.TuGio;
                            kt.DenGio = n.DenGio;
                        }
                        else
                        {
                            var objCt = new tbl_CaTruc();
                            objCt.TenCaTruc = n.TenCaTruc;
                            objCt.TuGio = n.TuGio;
                            objCt.DenGio = n.DenGio;
                            objCt.GhiChu = n.DienGiai;
                            objCt.NgungSuDung = n.NgungSuDung;
                            objCt.NguoiNhap = Common.User.MaNV;
                            objCt.NgayNhap = DateTime.Now;
                            db.tbl_CaTrucs.InsertOnSubmit(objCt);
                        }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
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
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
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

    public class CaTruc
    {
        public string TenCaTruc { get; set; }
        public string TuGio { get; set; }
        public string DenGio { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }

        public bool? NgungSuDung { get; set; }
    }
}