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

namespace DichVu.ChoThue.Setting
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        thueHopDongCaiDat objDM;
        public int? ID;
        public byte? MaTN;
        public string ToaNha;
        public frmEdit()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddDinhMuc_Load(object sender, EventArgs e)
        {
            txtToaNha.Text = ToaNha;
            if (ID == null)
            {
                objDM = new thueHopDongCaiDat();
                db.thueHopDongCaiDats.InsertOnSubmit(objDM); 
                objDM.NgayTao = db.GetSystemDate();
                objDM.MaNV = objNV.MaNV;
            }
            else
            {
                try
                {
                    objDM = db.thueHopDongCaiDats.Single(p => p.ID == ID);
                    spinSoNgay.EditValue = objDM.SoNgay ?? 0;
                    txtDienGiai.Text = objDM.DienGiai;
                }
                catch { }
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {            
            try
            {                
                objDM.SoNgay = Convert.ToInt32( spinSoNgay.EditValue);
                objDM.DienGiai = txtDienGiai.Text;
                objDM.MaTN = MaTN;
                if (ID == null)
                {
                    db.thueHopDongCaiDats.InsertOnSubmit(objDM);
                }
                else
                {
                    objDM.NgayCN = db.GetSystemDate();
                    objDM.MaNVCN = objNV.MaNV;
                }

                if (db.thueHopDongCaiDats.Where(p => p.MaTN == MaTN & (p.ID != ID | ID == null)).Count() > 0)
                {
                    DialogBox.Alert("[Dự án] này đã thiết lập ngày nhắc.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }

                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
            }
            catch
            {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }

        private void lookKhuNha_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void gcMatBang_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}