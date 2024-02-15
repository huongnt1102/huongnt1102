using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.XuatKho
{
    public partial class frmChonDonViTinhVatTu : XtraForm
    {

        public frmChonDonViTinhVatTu()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmQuanLyHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {

                }

            }
            catch (Exception ex)
            { }
            finally
            {
                wait.Close();
            }
        }

        private void Chon()
        {
            var madvt = (int?) gv.GetFocusedRowCellValue("MaDVT");
            if (madvt == null)
            {
                DialogBox.Alert("Vui lòng chọn [đơn vị tính]");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chon();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            Chon();
        }

        private void itemThemDonViQuyDoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDvtQuyDoi frm = new frmDvtQuyDoi();
            

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                LoadData();
        }
    }
}