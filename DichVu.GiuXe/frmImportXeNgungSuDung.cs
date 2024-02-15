using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;

namespace DichVu.GiuXe
{
    public partial class frmImportXeNgungSuDung : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        public bool IsGiuXe { get; set; }
        public bool isSave { get; set; }
        CachTinhCls objCT;

        public frmImportXeNgungSuDung()
        {
            InitializeComponent();
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog(); 
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                //DialogBox.Error(ex.Message);
            }
            finally
            { file.Dispose(); }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gcTheXe.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(this.Tag.ToString());
                    System.Collections.Generic.List<NgungSuDungXe> list = Library.Import.ExcelAuto.ConvertDataTable<NgungSuDungXe>(Library.Import.ExcelAuto.GetDataExcel(excel, grvTheXe, itemSheet));

                    gcTheXe.DataSource = list;

                    excel = null;
                }
                catch (Exception ex)
                {
                    //DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            grvTheXe.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcTheXe.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltTheXe = (List<NgungSuDungXe>)gcTheXe.DataSource;
                var ltError = new List<NgungSuDungXe>();
                foreach (var n in ltTheXe)
                {
                    var KT = db.Kho_Thes.SingleOrDefault(p => p.SoThe == n.SoThe.ToLower());
                    if (KT == null)
                    {
                        try
                        {
                            #region "   Rang buoc du lieu"
                            var mat_bang = db.mbMatBangs.FirstOrDefault(_=>_.MaSoMB == n.MatBang & _.MaTN == this.MaTN);
                            if(mat_bang == null)
                            {
                                n.Error = "Mặt bằng không tồn tại trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }

                            var the_xe = db.dvgxTheXes.FirstOrDefault(_=>_.SoThe == n.SoThe & _.MaMB == mat_bang.MaMB & _.MaTN == this.MaTN);
                            if(the_xe == null)
                            {
                                n.Error = "Thẻ xe không tồn tại trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }

                            if(n.IsNgungSuDung == true & n.NgayNgungSD == null)
                            {
                                n.Error ="Thiếu ngày ngưng sử dụng";
                                ltError.Add(n);
                                continue;
                            }

                            #endregion

                            // update ngày ngưng sử dụng cho thẻ xe

                            if(the_xe.NgungSuDung != n.IsNgungSuDung)
                            {
                                the_xe.NgungSuDung = n.IsNgungSuDung;
                                if(n.IsNgungSuDung == true)
                                {
                                    the_xe.NgayNgungSD = n.NgayNgungSD;
                                }
                                else
                                {
                                    the_xe.NgayNgungSD = null;
                                }

                                db.SubmitChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            n.Error = ex.Message;
                            ltError.Add(n);
                        }
                    }
                    
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcTheXe.DataSource = ltError;
                }
                else
                {
                    gcTheXe.DataSource = null;
                }
            }
            catch (System.Exception ex)
            {
                this.isSave = true;
                wait.Close();
                //DialogBox.Alert(ex.Message);
                this.Close();
            }
            finally { wait.Dispose();  }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            objCT = new CachTinhCls();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcTheXe);
        }
        public class NgungSuDungXe
        {

            public string SoThe { get; set; }
            public string MatBang { get; set; }
            public bool? IsNgungSuDung { get; set; }
            public DateTime? NgayNgungSD { get; set; }
            public string Error { get; set; }
        }
    }

    
}