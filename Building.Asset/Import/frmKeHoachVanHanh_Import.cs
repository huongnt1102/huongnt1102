using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmKeHoachVanHanh_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmKeHoachVanHanh_Import()
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
                    System.Collections.Generic.List<KeHoachVanHanhImport> list = Library.Import.ExcelAuto.ConvertDataTable<KeHoachVanHanhImport>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

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
                var obj = (List<KeHoachVanHanhImport>) gc.DataSource;
                var ltError = new List<KeHoachVanHanhImport>();
                foreach (var n in obj)
                {
                    try
                    {

                        #region Kiểm tra dữ liệu

                        var objTanXuat =
                            db.tbl_TanSuats.FirstOrDefault(_ => _.TenTanSuat.ToLower() == n.TenTanSuat.ToLower());
                        if (objTanXuat == null)
                        {
                            n.Error = "Tần xuất " + n.TenTanSuat + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objNhomTaiSan =
                            db.tbl_NhomTaiSans.FirstOrDefault(_ => _.TenVietTat.ToLower() == n.TenNhomTaiSan.ToLower());
                        if (objNhomTaiSan == null)
                        {
                            n.Error = "Hệ thống " + n.TenNhomTaiSan + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objTenTaiSan = db.tbl_TenTaiSans.FirstOrDefault(_ => _.TenVietTat.ToLower() == n.TenTaiSan.ToLower() & _.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID == objNhomTaiSan.ID);
                        if (objTenTaiSan == null)
                        {
                            n.Error = "Tài sản " + n.TenTaiSan + " không tồn tại trong hệ thống "+n.TenNhomTaiSan;
                            ltError.Add(n);
                            continue;
                        }

                        
                        #endregion

                        // import không cập nhật

                        var o = new tbl_KeHoachVanHanh();
                        o.NgayLapKeHoach = n.NgayLapKeHoach;
                        o.TuNgay = n.TuNgay;
                        o.DenNgay = n.DenNgay;
                        o.MaTN = MaTn;
                        o.TanSuatID = objTanXuat.ID;
                        o.NhomTaiSanID = objNhomTaiSan.ID;
                        o.NguoiNhap = Common.User.MaNV;
                        o.NgayNhap = DateTime.Now;

                        var ct = new tbl_KeHoachVanHanh_ChiTiet();
                        ct.MaTenTaiSanID = objTenTaiSan.ID;
                        ct.TenTaiSan = objTenTaiSan.TenTaiSan;
                        o.tbl_KeHoachVanHanh_ChiTiets.Add(ct);

                        db.tbl_KeHoachVanHanhs.InsertOnSubmit(o);

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

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class KeHoachVanHanhImport
    {
        public DateTime? NgayLapKeHoach { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public string TenTanSuat { get; set; }
        public string TenTaiSan { get; set; }
        public string TenNhomTaiSan { get; set; }

        public string Error { get; set; }
    }
}