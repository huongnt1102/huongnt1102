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

namespace LandsoftBuildingGeneral.DynBieuMau
{
    public partial class frmDanhSoTuDong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmDanhSoTuDong()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmDanhSoTuDong_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            gcbieumau.DataSource = db.QuyTacDanhSos;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvbieumau.AddNewRow();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                DialogBox.Error("Lưu không thành công, vui lòng thử lại sau");
            }
        }
    }
}