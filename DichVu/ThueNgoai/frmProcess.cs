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

namespace DichVu.ThueNgoai
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public int MaHD = 0;
        public int? MaTT = 0;
        public frmProcess()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lookTrangThai.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng chọn [Trạng thái], xin cmar ơn.");  
                lookTrangThai.Focus();
                return;
            }

            try
            {
                var objHD = db.hdtnHopDongs.Single(p => p.MaHD == MaHD);
                objHD.MaTT = Convert.ToInt32(lookTrangThai.EditValue);

                var objLS = new hdtnLichSu();
                objLS.ContractID = MaHD;
                objLS.DateCreate = db.GetSystemDate();
                objLS.Description = "Cập nhật hợp đồng";
                objLS.StaffID = objnhanvien.MaNV;
                objLS.StatusID = Convert.ToInt32(lookTrangThai.EditValue);
                db.hdtnLichSus.InsertOnSubmit(objLS);

                db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogBox.Alert("Dữ liệu đã được cập nhật");  
                this.Close();
            }
            catch {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");  
            }
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            lookTrangThai.Properties.DataSource = db.hdtnTrangThais;
            lookTrangThai.EditValue = MaTT;
        }
    }
}