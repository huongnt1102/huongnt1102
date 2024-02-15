using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq;
using System.Linq;

namespace DichVu.HoBoi.LoaiThe
{
    public partial class frmLoaiThe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmLoaiThe()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            db = new MasterDataContext();

            gcLoaiThe.DataSource = db.dvhbLoaiThes;
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvLoaiThe.DeleteRow(grvLoaiThe.FocusedRowHandle);
            }
            catch
            {
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Đã xảy ra lỗi, vui lòng kiểm tra lại dữ liệu!");
            }

        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void frmLoaiThe_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}