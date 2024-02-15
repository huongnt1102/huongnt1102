using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmListCongViec_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }
        public int MaNhomTaiSanID { get; set; }

        public frmListCongViec_Import()
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
                    System.Collections.Generic.List<ListCongViecImport> list = Library.Import.ExcelAuto.ConvertDataTable<ListCongViecImport>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

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
            //if (gc.DataSource == null)
            //{
            //    DialogBox.Error("Vui lòng chọn sheet");
            //    return;
            //}

            //var wait = DialogBox.WaitingForm();
            //var db = new MasterDataContext();
            //try
            //{
            //    var obj = (List<ListCongViecImport>) gc.DataSource;
            //    var ltError = new List<ListCongViecImport>();
            //    foreach (var n in obj)
            //    {
            //        try
            //        {

            //            #region Kiểm tra dữ liệu

            //            var objTuyChon =
            //                db.tbl_TuyChons.FirstOrDefault(_ => _.TenTuyChon.ToLower() == n.TenTuyChon.ToLower());
            //            if (objTuyChon == null)
            //            {
            //                n.Error = "Không có tùy chọn " + n.TenTuyChon + " trong hệ thống";
            //                ltError.Add(n);
            //                continue;
            //            }

            //            var objNhomCongViec = db.tbl_NhomCongViecs.FirstOrDefault(_ =>
            //                _.TenNhomCongViec.ToLower() == n.TenNhomCongViec.ToLower() & _.MaNhomTaiSanID==MaNhomTaiSanID);
            //            if (objNhomCongViec == null)
            //            {
            //                n.Error = "Nhóm công việc " + n.TenNhomCongViec + " không có trong hệ thống";
            //                ltError.Add(n);
            //                continue;
            //            }

            //            #endregion

            //            db = new MasterDataContext();
            //            // import không cập nhật
            //            var objCongViec = db.tbl_CongViecs.FirstOrDefault(_ =>
            //                _.MaNhomCongViecID == objNhomCongViec.ID &
            //                _.TenCongViec.ToLower() == n.TenCongViec.ToLower());
            //            if (objCongViec != null)
            //            {
            //                objCongViec.TieuChuan = n.TieuChuan;
            //                objCongViec.NgungSuDung = n.NgungSuDung;
            //                objCongViec.NgaySua = DateTime.Now;
            //                objCongViec.NguoiSua = Common.User.MaNV;

            //                var objCongViecTuyChon = db.tbl_CongViec_TuyChons.FirstOrDefault(_ =>
            //                    _.CongViecID == objCongViec.ID & _.TuyChonID == objTuyChon.ID);
            //                if (objCongViecTuyChon != null)
            //                {
            //                    objCongViecTuyChon.GiaTriChon = n.GiaTriChon;

            //                    var objCongViecTuyChonList =
            //                        db.tbl_CongViec_TuyChon_Lists.FirstOrDefault(_ =>
            //                            _.CongViecTuyChonID == objCongViecTuyChon.ID);
            //                    if (objCongViecTuyChonList != null)
            //                    {
            //                        objCongViecTuyChonList.GiaTri = n.GiaTri;
            //                    }
            //                    else
            //                    {
            //                        var objCvTcList = new tbl_CongViec_TuyChon_List();
            //                        objCvTcList.GiaTri = n.GiaTri;
            //                        objCongViecTuyChon.tbl_CongViec_TuyChon_Lists.Add(objCvTcList);
            //                    }
            //                }
            //                else
            //                {
            //                    var objCvTc = new tbl_CongViec_TuyChon();
            //                    objCvTc.TuyChonID = objTuyChon.ID;
            //                    objCvTc.GiaTriChon = n.GiaTriChon;
            //                    objCongViec.tbl_CongViec_TuyChons.Add(objCvTc);

            //                    var objCongViecTuyChonList = new tbl_CongViec_TuyChon_List();
            //                    objCongViecTuyChonList.GiaTri = n.GiaTri;
            //                    objCvTc.tbl_CongViec_TuyChon_Lists.Add(objCongViecTuyChonList);
            //                }
            //            }
            //            else
            //            {
            //                var objVc = new tbl_CongViec();
            //                objVc.MaNhomCongViecID = objNhomCongViec.ID;
            //                objVc.TenCongViec = n.TenCongViec;
            //                objVc.TieuChuan = n.TieuChuan;
            //                objVc.NgayNhap = DateTime.Now;
            //                objVc.NguoiNhap = Common.User.MaNV;
            //                objVc.NgungSuDung = n.NgungSuDung;
            //                objVc.MaTN = MaTn;
                            
            //                var objCongViecTuyChon = new tbl_CongViec_TuyChon();
            //                objCongViecTuyChon.GiaTriChon = n.GiaTriChon;
            //                objCongViecTuyChon.TuyChonID = objTuyChon.ID;
            //                objVc.tbl_CongViec_TuyChons.Add(objCongViecTuyChon);

            //                var objCongViecTuyChonList = new tbl_CongViec_TuyChon_List();
            //                objCongViecTuyChonList.GiaTri = n.GiaTri;
            //                objCongViecTuyChon.tbl_CongViec_TuyChon_Lists.Add(objCongViecTuyChonList);

            //                db.tbl_CongViecs.InsertOnSubmit(objVc);
            //            }

            //            db.SubmitChanges();
            //        }
            //        catch (Exception ex)
            //        {
            //            n.Error = ex.Message;
            //            ltError.Add(n);
            //        }
            //    }

            //    IsSave = true;
            //    DialogBox.Success();

            //    if (ltError.Count > 0)
            //    {
            //        gc.DataSource = ltError;
            //    }
            //    else
            //    {
            //        gc.DataSource = null;
            //    }
            //}
            //catch
            //{
            //    wait.Close();
            //    DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
            //    Close();
            //}
            //finally
            //{
            //    wait.Dispose();
            //    db.Dispose();
            //}
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

    public class ListCongViecImport
    {
        public string TenNhomCongViec { get; set; }
        public string TenCongViec { get; set; }
        public string TieuChuan { get; set; }
        public string TenTuyChon { get; set; }
        public string GiaTriChon { get; set; }
        public string GiaTri { get; set; }
        public string GhiChu { get; set; }
        public string Error { get; set; }

        public bool? NgungSuDung { get; set; }
    }
}