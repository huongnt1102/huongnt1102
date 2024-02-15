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

namespace DichVu.KhachHang.CSKH
{
    public partial class frmCauHinhTiemNang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
		ncCauHinh objConfig;

        public frmCauHinhTiemNang()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
        }

        private void frmCauHinhTiemNang_Load(object sender, EventArgs e)
        {
            gcTiemNang.DataSource = db.ncCauHinhTiemNangs.ToList();
            var obj = db.ncCauHinhs.ToList();
            if(obj.Count() > 0)
            {
                objConfig = db.ncCauHinhs.First();

                spNhacTruoc.EditValue = objConfig.LH_NhacTruoc;
                colorLichHen.EditValue = objConfig.LH_MauSac;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                objConfig.LH_NhacTruoc = (int)spNhacTruoc.Value;
                objConfig.LH_MauSac = colorLichHen.Color.ToArgb();
                db.SubmitChanges();
                DialogBox.Success();
            }
            catch { }
            finally { db.Dispose(); }
        }
    }
}