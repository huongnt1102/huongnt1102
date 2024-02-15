using DevExpress.XtraBars;
using Library;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SAP.Import
{
    public partial class frmMappingDoanhThu : DevExpress.XtraEditors.XtraForm
    {
        public frmMappingDoanhThu()
        {
            InitializeComponent();
        }

        public bool isSave { get; set; }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
        }

        private void itemChoice_ItemClick(object sender, ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();

                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
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

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<ImportItem> list = Library.Import.ExcelAuto.ConvertDataTable<ImportItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gcImport.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltSource = (List<ImportItem>)gcImport.DataSource;
                var ltError = new List<ImportItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        //if (i.KyHieu == null)
                        //{
                        //    i.Error = "Vui lòng nhập mã công ty";
                        //    ltError.Add(i);
                        //    continue;
                        //}
                        #endregion

                        var objToaNha = db.tnToaNhas.FirstOrDefault(_ => _.TenVT.ToLower() == i.MaTN.ToLower());
                        if (objToaNha == null)
                        {
                            i.Error = "Dự án không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objKhoiNha = db.mbKhoiNhas.FirstOrDefault(_ => _.MaVT.ToLower() == i.MaKN.ToLower());
                        if(objKhoiNha == null)
                        {
                            i.Error = "Khối nhà không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objCompany = db.CompanyCodes.FirstOrDefault(_ => _.KyHieu.ToLower() == i.CompanyCode.ToLower());
                        if (objCompany == null)
                        {
                            i.Error = "CompanyCode không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objLoaiDichVu = db.dvLoaiDichVus.FirstOrDefault(_ => _.TenLDV.ToLower() == i.MaLDV.ToLower());
                        if(objLoaiDichVu == null)
                        {
                            i.Error = "Mã loại dịch vụ không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var objCuDanCDT = db.CuDanCDTs.FirstOrDefault(_ => _.Name.ToLower() == i.MaCuDanCDT.ToLower());
                        if(objCuDanCDT == null)
                        {
                            i.Error = "Mã báo cáo doanh thu không tồn tại";
                            ltError.Add(i);
                            continue;
                        }
                        
                        

                        try
                        {
                            //var obj = db.CompanyCodes.FirstOrDefault(_ => _.KyHieu.ToLower() == i.KyHieu.ToLower());
                            //if (obj == null)
                            //{
                            //    obj = new CompanyCode();
                            //    obj.KyHieu = i.KyHieu;
                            //    db.CompanyCodes.InsertOnSubmit(obj);
                            //}
                            var obj = new MappingDoanhThu();
                            obj.MaTN = objToaNha.MaTN;
                            obj.MaKN = objKhoiNha.MaKN;
                            obj.CompanyCode = objCompany.ID;
                            obj.MaLDV = objLoaiDichVu.ID;
                            obj.IsCaNhan = i.IsCaNhan;
                            obj.MaCuDanCDT = objCuDanCDT.ID;
                            if (i.TenLX != null)
                            {
                                var loaiXe = db.dvgxLoaiXes.FirstOrDefault(_ => _.TenLX.ToLower() == i.TenLX.ToLower());
                                if (loaiXe == null)
                                {
                                    i.Error = "Loại xe không tồn tại";
                                    ltError.Add(i);
                                    continue;
                                }
                                obj.MaLX = loaiXe.MaLX;
                            }
                            

                            db.MappingDoanhThus.InsertOnSubmit(obj);
                        }
                        catch { }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcImport.DataSource = ltError;
                }
                else
                {
                    gcImport.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
                db.Dispose();
            }
        }

        private void itemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcImport);
        }

        public class ImportItem
        {
            public string MaTN { get; set; }
            public string MaKN { get; set; }
            public string CompanyCode { get; set; }
            public string MaLDV { get; set; }
            public string MaCuDanCDT { get; set; }
            public bool? IsCaNhan { get; set; }
            public string TenLX { get; set; }
            public string Error { get; set; }
        }
    }
}