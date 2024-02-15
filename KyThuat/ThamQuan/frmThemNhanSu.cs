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

namespace KyThuat.ThamQuan
{
    public partial class frmThemNhanSu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tqThamQuan objthamquan { get; set; }

        public frmThemNhanSu()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmThemNhanSu_Load(object sender, EventArgs e)
        {
            gcNhanSu.DataSource = db.tqNhaThauNhanSus.Where(p => p.MaTQ.Trim() == objthamquan.MaTQ.Trim());

            var SelectedNCC = db.tqBaoGias.Where(p => p.MaTQ.Trim() == objthamquan.MaTQ.Trim() & p.DaDuyet.Value).Select(p=>p.MaNhaCungCap).ToList();
            lookNhaCungCap.DataSource = db.tnNhaCungCaps.Where(p => SelectedNCC.Contains(p.MaNCC));
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvNhanSu.DeleteSelectedRows();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void grvNhanSu_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvNhanSu.SetFocusedRowCellValue(colMaTQ, objthamquan.MaTQ);
        }
    }
}