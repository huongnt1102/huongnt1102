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
    public partial class frmCongNohdtn : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public hdtnHopDong objhd;
        public hdtnCongNo objcn;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;
        public frmCongNohdtn()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmCongNohdtn_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            try
            {
                if (objhd == null)
                {
                    txtMaHopDong.Text = objcn.hdtnHopDong.SoHD;
                    txtTenHopDong.Text = objcn.hdtnHopDong.TenHD;
                    spinDaTT.EditValue = db.hdtnCongNos.Where(p => p.MaHD == objcn.MaHD).Sum(p => p.SoTien) ?? 0;
                    spinConLai.EditValue = (objcn.hdtnHopDong.GiaTri - (decimal)spinDaTT.EditValue) ?? 0;

                }
                else
                {
                    txtMaHopDong.Text = objhd.SoHD;
                    txtTenHopDong.Text = objhd.TenHD;
                    spinDaTT.EditValue = db.hdtnCongNos.Where(p => p.MaHD == objhd.MaHD).Sum(p => p.SoTien) ?? 0;
                    spinConLai.EditValue = (objhd.GiaTri - (decimal)spinDaTT.EditValue) ?? 0;
                }

                if (objcn != null)
                {
                    txtSoHoaDon.Text = objcn.SoHD;
                    txtDienGiai.Text = objcn.DienGiai;
                    spinSoTien.EditValue = objcn.SoTien;
                    dateNgayThanhToan.EditValue = objcn.NgayThanhToan;
                }
                else
                {
                    dateNgayThanhToan.EditValue = db.GetSystemDate();
                    objcn = new hdtnCongNo();
                    db.hdtnCongNos.InsertOnSubmit(objcn);
                }
            }
            catch { }

            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (txtSoHoaDon.Text.Trim().Length==0)
            {
                DialogBox.Error("Vui lòng nhập số hóa đơn");  
                return;
            }
            if ((decimal)spinSoTien.EditValue <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền thanh toán hợp lệ");  
                return;
            }
            try
            {
                decimal datt = db.hdtnCongNos.Where(p => p.MaHD == objhd.MaHD).Sum(p => p.SoTien) ?? 0;

                objcn.SoTien = (decimal)spinSoTien.EditValue;
                objcn.ConLai = objhd.GiaTri - datt - (decimal)spinSoTien.EditValue;
                objcn.DienGiai = txtDienGiai.Text.Trim();
                objcn.NgayThanhToan = dateNgayThanhToan.DateTime;
                objcn.SoHD = txtSoHoaDon.Text;
                objcn.MaHD = objhd.MaHD;
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Không lưu được, có thể đường truyền không ổn định! Vui lòng thử lại sau");  
                this.DialogResult = DialogResult.Cancel;
            }
            finally
            {
                db.Dispose();
                this.Close();
            }
        }
    }
}