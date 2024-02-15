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
    public partial class frmImportXa : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public bool IsSave { get; set; }
        public int MaHuyen { get; set; }
        public frmImportXa()
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
                        TenXa = p[0].ToString().Trim(),
                        TenHienThi = p[1].ToString().Trim(),
                        TenHuyen = p[2].ToString().Trim()
                    });

                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var it in item)
                    {
                        ImportItem importitem = new ImportItem()
                        {
                            TenXa = it.TenXa,
                            TenHienThi = it.TenHienThi,
                            TenHuyen = it.TenHuyen,

                        };
                        newlist.Add(importitem);
                    }
                    gcXa.DataSource = newlist;
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
            if (gvXa.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                var ltXa = (from h in db.Xas where h.MaHuyen == this.MaHuyen select h.TenXa).ToList();
                var ltSource = (List<ImportItem>)gcXa.DataSource;
                var ltError = new List<ImportItem>();

                    foreach (var lt in ltSource)
                    {

                        db = new MasterDataContext();
                        try
                        {
                            var objHuyen = db.Huyens.Where(o => o.TenHuyen == lt.TenHuyen).FirstOrDefault();

                            if (objHuyen == null)
                            {
                                lt.Error = "Tên tỉnh không tồn tại";
                                ltError.Add(lt);
                                continue;
                            }

                            var objXa = new Xa();
                            objXa.MaHuyen = objHuyen.MaHuyen;
                            objXa.TenHienThi = lt.TenHienThi;
                            objXa.TenXa = lt.TenXa;

                            db.Xas.InsertOnSubmit(objXa);
                            db.SubmitChanges();
                            ltXa.Add(lt.TenHuyen.ToLower());

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
                            gcXa.DataSource = ltError;
                        }
                        else
                        {
                            gcXa.DataSource = null;
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
            gvXa.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        class ImportItem
        {
            public string TenXa { get; set; }
            public string TenHuyen { get; set; }
            public string TenHienThi { get; set; }
            public string Error { get; set; }
        }

    }
}