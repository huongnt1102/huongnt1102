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
namespace Building.Asset.DanhMuc
{
    public partial class frmThietLapNgayNghi_Edit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID;
        public bool IsSave = false;
        MasterDataContext db = new MasterDataContext();
        tbl_ThietLapNgayNghi_DanhMuc objNgayNghi;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmThietLapNgayNghi_Edit()
        {
            InitializeComponent();
        }

        private void frmThietLapNgayNghi_Edit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            if (ID == null)
            {
                objNgayNghi = new tbl_ThietLapNgayNghi_DanhMuc();
                spin_Nam.EditValue = DateTime.UtcNow.AddHours(7).Year;
                chkThuBay.Checked = true;
                chkChuNhat.Checked = true;
            }
            else
            {
                objNgayNghi = db.tbl_ThietLapNgayNghi_DanhMucs.FirstOrDefault(p => p.ID == ID);
                spin_Nam.EditValue = objNgayNghi.Nam;
                chkThuBay.Checked = objNgayNghi.IsThuBay.GetValueOrDefault();
                chkChuNhat.Checked = objNgayNghi.IsChuNhat.GetValueOrDefault();
                txtTieuDe.EditValue = objNgayNghi.TieuDe;
            }
            gcChiTiet.DataSource = objNgayNghi.tbl_ThietLapNgayNghi_DanhMuc_ChiTiets;

            itemHuongDan.Click += ItemHuongDan_Click;
            itemLamLai.Click += ItemLamLai_Click;
        }

        private void ItemLamLai_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (spin_Nam.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập năm thiết lập");
                return;
            }
            objNgayNghi.Nam =Convert.ToInt32(spin_Nam.EditValue);
            objNgayNghi.IsThuBay = chkThuBay.Checked;
            objNgayNghi.IsChuNhat = chkChuNhat.Checked;
            objNgayNghi.TieuDe = txtTieuDe.Text;
            if (ID == null)
            {
                objNgayNghi.NgayNhap = Common.GetDateTimeSystem();
                objNgayNghi.NguoiNhap = Common.User.MaNV;
                db.tbl_ThietLapNgayNghi_DanhMucs.InsertOnSubmit(objNgayNghi);
            }
            else
            {
                objNgayNghi.NgaySua = Common.GetDateTimeSystem();
                objNgayNghi.NguoiSua = Common.User.MaNV;
            }
            try
            {
                db.SubmitChanges();
                IsSave = true;
                this.Close();
            }
            catch ( Exception ex)
            {
                DialogBox.Alert(" Có lỗi trong quá trình lưu dữ liệu:" + ex.Message);
            }
        }
    }
}