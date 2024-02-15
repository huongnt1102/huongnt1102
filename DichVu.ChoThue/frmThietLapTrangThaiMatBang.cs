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

namespace DichVu.ChoThue
{
    public partial class frmThietLapTrangThaiMatBang : DevExpress.XtraEditors.XtraForm
    {
        public mbTrangThai objTrangThai { get; set; }

        MasterDataContext db;

        public frmThietLapTrangThaiMatBang()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmThietLapTrangThaiMatBang_Load(object sender, EventArgs e)
        {
            lookTrangThai.Properties.DataSource = db.mbTrangThais;
            
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn trạng thái mặt bằng muốn thiết lập");
                return;
            }
            objTrangThai = db.mbTrangThais.Single(p => p.MaTT == (int)lookTrangThai.EditValue);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}