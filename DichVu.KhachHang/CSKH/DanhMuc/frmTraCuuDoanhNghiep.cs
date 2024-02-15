using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.KhachHang.CSKH
{
    public partial class frmTraCuuDoanhNghiep : DevExpress.XtraEditors.XtraForm
    {
        public frmTraCuuDoanhNghiep()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
        }

        private void itemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var key = itemkeySearch.EditValue.ToString();

            gc.DataSource = Library.Other.API.GetThongTinDoanhNghiep(key);
        }

        private void frmTraCuuDoanhNghiep_Load(object sender, EventArgs e)
        {
        }

        private void itemChuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var mst = gv.GetFocusedRowCellValue("MaSoThue").ToString();

            var objKH = Library.Other.API.GetChiTietDoanhNghiep(mst);

            using (var frm = new frmEdit())
            {
                frm.IsResearch = true;
                frm.maTN = Common.User.MaTN.Value;
                frm.objKH = objKH;
                frm.ShowDialog();
            }
        }
    }
}