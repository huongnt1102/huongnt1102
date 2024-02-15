using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LinqToExcel;
using System.Linq;

namespace Library.Others
{
    public partial class frmImportHuyen : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public bool IsSave { get; set; }
        public int MaTinh { get; set; }
        public frmImportHuyen()
        {
            InitializeComponent();
        }

        private void itemBrowse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file|*.xls;*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                    var item = book.Worksheet(0).Select(p => new
                    {
                        TenHuyen = p[0].ToString().Trim(),
                        TenHienThi = p[1].ToString().Trim(),
                        TenTinh = p[2].ToString().Trim()
                    });

                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var it in item)
                    {
                        ImportItem importitem = new ImportItem()
                        {
                            TenHuyen = it.TenHuyen,
                            TenTinh = it.TenTinh,
                            TenHienThi = it.TenHienThi,

                        };
                        newlist.Add(importitem);
                    }
                    gcHuyen.DataSource = newlist;
                }
                catch (Exception ex)
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại.\r\nCode: " + ex.Message);
                }

                wait.Close();
                wait.Dispose();
            }
        }

     

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvHuyen.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                var ltHuyen = (from h in db.Huyens where h.MaTinh == this.MaTinh select h.TenHuyen).ToList();
                var ltSource = (List<ImportItem>)gcHuyen.DataSource;
                var ltError = new List<ImportItem>();

                foreach (var lt in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var objTinh = db.Tinhs.Where(o => o.TenTinh == lt.TenTinh).FirstOrDefault();

                        if (objTinh == null)
                        {
                            lt.Error = "Tên tỉnh không tồn tại";
                            ltError.Add(lt);
                            continue;
                        }

                        var objHuyen = new Huyen();
                        objHuyen.MaTinh = objTinh.MaTinh;
                        objHuyen.TenHienThi = lt.TenHienThi;
                        objHuyen.TenHuyen = lt.TenHuyen;

                        db.Huyens.InsertOnSubmit(objHuyen);
                        db.SubmitChanges();
                        ltHuyen.Add(lt.TenHuyen.ToLower());

                    }

                    catch (Exception ex)
                    {
                        lt.Error = ex.Message;
                        ltError.Add(lt);
                    }

                    this.IsSave = true;
                    DialogBox.Success();

                }

                    if (ltError.Count > 0)
                    {
                        gcHuyen.DataSource = ltError;
                    }
                    else
                    {
                        gcHuyen.DataSource = null;
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

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvHuyen.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        class ImportItem
        {
            public string TenTinh { get; set; }
            public string TenHuyen { get; set; }
            public string TenHienThi { get; set; }
            public string Error { get; set; }
        }
    }
}