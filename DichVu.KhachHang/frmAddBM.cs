using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace KyThuat.KhachHang
{
    public partial class frmAddBM : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmAddBM()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        public DichVu.KhachHang.frmManager.AddItemDelegate AddItemCallback;

        private void frmAddBM_Load(object sender, EventArgs e)
        {
            gcBieuMau.DataSource = db.BmBieuMaus.Where(p => p.MaLBM != 1 & !p.IsLock.Value)
                .Select(p => new
                {
                    p.MaBM,
                    p.TenBM,
                    p.Description
                });
        }

        private void btnChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(grvBieuMau.FocusedRowHandle <0)
            {
                DialogBox.Alert("Vui lòng chọn biểu mẫu muốn thêm");
                return;
            }
            
            int MaBM = (int)grvBieuMau.GetFocusedRowCellValue(colMaBm);
            AddItemCallback(MaBM);
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void grvBieuMau_DoubleClick(object sender, EventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn biểu mẫu muốn thêm");
                return;
            }
            int MaBM = (int)grvBieuMau.GetFocusedRowCellValue(colMaBm);
            AddItemCallback(MaBM);
        }
    }
}