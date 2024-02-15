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
using LinqToExcel;

namespace Library
{
    public partial class frmImportTinh : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public bool IsUpdate = false;
        public frmImportTinh()
        {
            InitializeComponent();
        }
        private void frmImportTinh_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
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
                    TenTinh = p[0].ToString().Trim(),
                    TenHienThi = p[1].ToString().Trim()
                });

                List<ImportItem> newlist = new List<ImportItem>();
                foreach (var it in item)
                {
                    ImportItem importitem = new ImportItem()
                    {
                        TenTinh = it.TenTinh,
                        TenHienThi = it.TenHienThi,
                       
                    };
                    newlist.Add(importitem);
                }
                gcTinh.DataSource = newlist;
                }
                catch(Exception ex)
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại.\r\nCode: " + ex.Message);
                }

                wait.Close();
                wait.Dispose();
            }
        }
        class ImportItem
        {
            public string TenTinh { get; set; }
            public string TenHienThi { get; set; }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvTinh.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }
            else
                Insert();
        }

        void Insert()
        {
            db = new MasterDataContext();
            List<Tinh> listTinh = new List<Tinh>();
            for (int i = 0; i < gvTinh.RowCount; i++)
            {
                var obj = new Tinh();
                obj.TenTinh = gvTinh.GetRowCellValue(i, colTenTinh).ToString();
                obj.TenHienThi = gvTinh.GetRowCellValue(i, colTenHienThi).ToString();
                listTinh.Add(obj);
            }

            var wait = DialogBox.WaitingForm();
            //try
            //{
                db.Tinhs.InsertAllOnSubmit(listTinh);
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Đã lưu");
            //}
            //catch
            //{
            //    DialogBox.Error("Mã Tỉnh bị trùng. Vui lòng xem lại dữ liệu");
            //    wait.Close();
            //    wait.Dispose();
            //}
            //finally
            //{
            //    wait.Close();
            //    wait.Dispose();
            //}
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                gvTinh.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}