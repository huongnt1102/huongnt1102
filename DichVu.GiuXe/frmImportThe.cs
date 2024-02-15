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
    public partial class frmImportThe : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        public bool IsGiuXe { get; set; }
        public bool isSave { get; set; }
        CachTinhCls objCT;

        public frmImportThe()
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
                DialogBox.Error(ex.Message);
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
                    System.Collections.Generic.List<TheXeItem3> list = Library.Import.ExcelAuto.ConvertDataTable<TheXeItem3>(Library.Import.ExcelAuto.GetDataExcel(excel, grvTheXe, itemSheet));

                    gcTheXe.DataSource = list;

                    //gcTheXe.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new TheXeItem3
                    //{
                       
                    //    SoThe = p["Số thẻ"].ToString().Trim(),
                    //    //TrangThaiThe  = p["Trạng thái thẻ"].ToString().Trim(),
                    //    LoaiThe  = p["Loại thẻ"].ToString().Trim(),
                    //    DienGiai = p["Diễn giải"].ToString().Trim(),
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
                var listloaiThe = (from lx in db.Kho_LoaiThes 
                                   select new { lx.ID, lx.TenLoaiThe }).ToList();
                var listTrangThai = (from mb in db.Kho_TrangThaiThes  select new { mb.ID,mb.TenTrangThaiThe }).ToList();
                
                #region "   Rang buoc du lieu"
                if (listloaiThe.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Loại thẻ] cho Dự án này trước.");
                    return;
                }

                if (listTrangThai.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Trạng thái thẻ] cho Dự án này trước.");
                    return;
                }

                #endregion

                
                var ltTheXe = (List<TheXeItem3>)gcTheXe.DataSource;
                var ltError = new List<TheXeItem3>();
                foreach (var n in ltTheXe)
                {
                    db = new MasterDataContext();
                    var KT = db.Kho_Thes.SingleOrDefault(p => p.SoThe == n.SoThe.ToLower());
                    if (KT == null)
                    {
                        try
                        {
                            #region "   Rang buoc du lieu"
                            if (db.dvgxTheXes.Any(o => o.SoThe == n.SoThe & o.MaTN == this.MaTN))
                            {
                                n.Error = "Số thẻ đã tồn tại trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }

                            if (n.LoaiThe.Trim() == "")
                            {
                                n.Error = "Vui lòng nhập loại thẻ";
                                ltError.Add(n);
                                continue;
                            }

                            //var objTrangThai = listTrangThai.FirstOrDefault(lx => lx.TenTrangThaiThe.ToLower() == n.TrangThaiThe.ToLower());
                            //if (objTrangThai == null)
                            //{
                            //    n.Error = "Trạng thái này không tồn tại trong hệ thống";
                            //    ltError.Add(n);
                            //    continue;
                            //}

                            #endregion

                            //The xe
                            var objTheXe = new dvgxTheXe();
                            objTheXe.MaTN = this.MaTN;
                            objTheXe.NgayNhap = db.GetSystemDate();
                            objTheXe.MaNVN = Common.User.MaNV;
                            objTheXe.SoThe = n.SoThe;
                            objTheXe.GhiChu = n.DienGiai;
                            objTheXe.IsTheOto = int.Parse(n.LoaiThe) == 2 ? true : false; 
                            db.dvgxTheXes.InsertOnSubmit(objTheXe);


                            db.SubmitChanges();
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
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
            finally { wait.Dispose(); db.Dispose(); }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            objCT = new CachTinhCls();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcTheXe);
        }
    }

    public class TheXeItem3
    {
       
        public string SoThe { get; set; }
        public string LoaiThe { get; set; }
        //public string TrangThaiThe { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}