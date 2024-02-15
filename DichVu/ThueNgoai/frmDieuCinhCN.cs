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

namespace DichVu.ThueNgoai
{
    public partial class frmDieuCinhCN : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public int MaCN { get; set; }
        hdtnCongNo objCN;
        MasterDataContext db;
        public frmDieuCinhCN()
        {
            InitializeComponent();
        }

        private void frmDieuCinhCN_Load(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            objCN = db.hdtnCongNos.FirstOrDefault(p => p.MaCongNo == MaCN);
            spinSoTien.EditValue = objCN.SoTien ?? 0;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objCN.SoTien = (decimal?)spinSoTien.EditValue;
            var objLS = new hdtnLichSu();
            objLS.DateCreate = db.GetSystemDate();
            objLS.Description = "Sửa giá hợp đồng thuê ngoài thành: " + spinSoTien.Text;
            objLS.StaffID = objNV.MaNV;
            objLS.StatusID = objCN.hdtnHopDong.MaTT;
            objLS.ContractID = objCN.MaHD;

            try
            {
                db.hdtnLichSus.InsertOnSubmit(objLS);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã lưu thành công");  
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Lỗi phát sinh: " + ex.Message);
            }
        }
    }
}