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

namespace AnNinh
{
    public partial class frmMapKeHoach : DevExpress.XtraEditors.XtraForm
    {
        public string TenKeHoach;
        public AnNinhKeHoach objkehoach;
        MasterDataContext db;
        public frmMapKeHoach()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmMapKeHoach_Load(object sender, EventArgs e)
        {
            this.TenKeHoach = string.Format("Gán kế hoạch {0} cho nhóm nhân viên", TenKeHoach.ToUpper());
            lookNhom.DataSource = db.pqNhoms;
            gcMap.DataSource = db.AnNinhMapKeHoaches.Where(p=>p.MaKeHoach == objkehoach.MaKeHoach);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
            }
            catch
            {
                DialogBox.Alert("Kế hoạch này đã được gán cho nhóm quyền này rồi");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvMap_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvMap.SetFocusedRowCellValue(colKeHoach, objkehoach.MaKeHoach);
        }
    }
}