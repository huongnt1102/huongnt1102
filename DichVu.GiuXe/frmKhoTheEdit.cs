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
using DevExpress.XtraGrid.Views.Base;
using System.Data.Linq.SqlClient;

namespace DichVu.GiuXe
{
    public partial class frmKhoTheEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmKhoTheEdit()
        {
            InitializeComponent();
        }

        public long? ID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db;
        dvgxTheXe objTheXe;

       
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            db = new MasterDataContext();
            
            if (this.ID != null)
            {
                objTheXe = db.dvgxTheXes.Single(p => p.ID == this.ID);
                txtSoThe.Text = objTheXe.SoThe;
                txtLoaiThe.EditValue = (objTheXe.IsTheXe.GetValueOrDefault() && objTheXe.IsThangMay.GetValueOrDefault()) ? "Thẻ tích hợp" : objTheXe.IsTheXe.GetValueOrDefault() ? "Thẻ xe" : "Thẻ thang máy";
                txtTrangThaiThe.Text = (objTheXe.NgungSuDung == null || objTheXe.NgungSuDung == true | (!objTheXe.IsTheXe.GetValueOrDefault() & !objTheXe.IsThangMay.GetValueOrDefault())) ? "Ngưng SD" : "Đang SD";
                txtDienGiai.EditValue = objTheXe.GhiChu;
                txtLyDo.EditValue = objTheXe.LyDo;
                ckbHongThe.EditValue = objTheXe.IsHongThe;
                ckbNgungSuDung.EditValue = objTheXe.NgungSuDung;
                dateNgungSD.EditValue = objTheXe.NgayNgungSD;
                dateNgungSD.Enabled = ckbNgungSuDung.Checked;
                txtLyDo.Enabled = ckbHongThe.Checked;
            }
            else
            {
                objTheXe = new dvgxTheXe();
                txtLyDo.Enabled = ckbHongThe.Checked;

            }
        }

        private void lkLoaiXe_EditValueChanged(object sender, EventArgs e)
        {
           
        }

      
        #region Nut xu ly luu, huy
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            

            if (txtSoThe.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số thẻ");
                txtSoThe.Focus();
                return;
            }
            if (objTheXe.ID == 0)
            {
                var dbo = new MasterDataContext();
                var KiemTra = dbo.dvgxTheXes.SingleOrDefault(p => p.SoThe == txtSoThe.Text.Trim() & p.MaTN == this.MaTN & !p.IsSaoLuu.GetValueOrDefault());
                if (KiemTra != null)
                {
                    DialogBox.Alert("Số thẻ này đã tồn tại trong hệ thống. Vui lòng kiểm tra lại!");
                    return;
                }

            }
           
            #endregion

            try
            {
                if (objTheXe.ID == 0)
                {
                    objTheXe.MaTN = this.MaTN;
                    objTheXe.NgayNhap = db.GetSystemDate();
                    objTheXe.MaNVN = Common.User.MaNV;
                    db.dvgxTheXes.InsertOnSubmit(objTheXe);
                }
                else
                {
                    
                    objTheXe.NgaySua = db.GetSystemDate();
                    objTheXe.MaNVS = Common.User.MaNV;
                }

                objTheXe.SoThe = txtSoThe.Text;
                objTheXe.GhiChu = txtDienGiai.Text;
                objTheXe.IsTheOto = ckbOto.Checked;
                objTheXe.LyDo = txtLyDo.Text;
                objTheXe.IsHongThe = ckbHongThe.Checked;
                objTheXe.NgayNgungSD = (DateTime?)dateNgungSD.EditValue;
                objTheXe.NgungSuDung = ckbNgungSuDung.Checked;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void txtSoThe_EditValueChanged(object sender, EventArgs e)
        {
            if (objTheXe.ID == 0)
            {
                var dbo = new MasterDataContext();
                var KiemTra = dbo.dvgxTheXes.SingleOrDefault(p => p.SoThe == txtSoThe.Text.Trim());
                if (KiemTra != null)
                {
                    DialogBox.Alert("Số thẻ này đã tồn tại trong hệ thống. Vui lòng kiểm tra lại!");
                    return;
                }

            }
        }

        private void ckbNgungSD_CheckedChanged(object sender, EventArgs e)
        {
            dateNgungSD.Enabled = ckbNgungSuDung.Checked;
            if (ckbNgungSuDung.Checked)
                dateNgungSD.EditValue = DateTime.Now;
            else
                dateNgungSD.EditValue = null;
        }

        private void ckbHongThe_CheckedChanged(object sender, EventArgs e)
        {
            txtLyDo.Enabled = ckbHongThe.Checked;
            if (!ckbHongThe.Checked)
                txtLyDo.EditValue = null;
        }
 
    }
}