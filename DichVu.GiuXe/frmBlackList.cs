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

namespace DichVu.GiuXe
{
    public partial class frmBlackList : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN;
        public frmBlackList()
        {
            InitializeComponent();
        }

        private void frmBlackList_Load(object sender, EventArgs e)
        {
            gcBlackList.DataSource = Library.APITheXe.DanhSachKhoaThe(MaTN);
        }

        private void repositoryItemButtonEdit1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var rowID = gvBlackList.GetFocusedRowCellValue("RowID");
            var id = gvBlackList.GetFocusedRowCellValue("ID");
            var BlockCode = gvBlackList.GetFocusedRowCellValue("BlockCode");
            if(id == null) return;

            if (APITheXe.HuyKhoaThe(id.ToString(), rowID.ToString(), MaTN, BlockCode.ToString()))
            {
                using (var db = new MasterDataContext())
                {
                    var tx = db.dvgxTheXes.FirstOrDefault(o => o.RowID == Convert.ToInt32(rowID));

                    if (tx != null)
                    {
                        tx.MaTheChip = null;
                        tx.RowID =(int?) null;
                        tx.IsKhoaThe = false;
                        tx.NgayKhoaThe =(DateTime?)null;
                        db.SubmitChanges();
                        gvBlackList.DeleteSelectedRows();
                    }
                }
            }
        }
    }
}