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

namespace DichVu.SendMail
{
    public partial class frmMailManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmMailManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmMailManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            gcMail.DataSource = db.SendMailDetails.Select(p => new
            {
                p.SendMail.TieuDe,
                p.SendMail.ThoiGianGui,
                p.SendMail.SendMailAccount.DiaChi,
                KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                p.tnKhachHang.EmailKH,
                TrangThai = TinhTrangGui(p.TrangThai.Value),
                p.ID,
                p.SendMail.tnNhanVien.HoTenNV,
                p.MailID
            });
        }

        private string TinhTrangGui(int TrangThai)
        {
            string result = string.Empty;
            switch (TrangThai)
            {
                case (int)Library.HeThongCls.EnumSendMailStatus.DaGuiThanhCong:
                    result = "Đã gửi thành công";
                    break;
                case (int)Library.HeThongCls.EnumSendMailStatus.KhongGuiDuoc:
                    result = "Không gửi được";
                    break;
                case (int)Library.HeThongCls.EnumSendMailStatus.DangChoGui:
                    result = "Đang chờ gửi";
                    break;
                default:
                    break;
            }
            return result;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (DichVu.SendMail.frmSendMailEdit frm = new DichVu.SendMail.frmSendMailEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMail.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn email");  
                return;
            }
            int mailid =(int)grvMail.GetFocusedRowCellValue("MailID");  
            var obj = db.SendMails.Single(p => p.MailID == mailid);
            using (DichVu.SendMail.frmSendMailEdit frm = new DichVu.SendMail.frmSendMailEdit() { objnhanvien = objnhanvien, objsm = obj })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMail.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn email");  
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                var objemail = db.SendMailDetails.Single(p => p.ID == (int)grvMail.GetFocusedRowCellValue("ID"));
                db.SendMailDetails.DeleteOnSubmit(objemail);

                try
                {
                    db.SubmitChanges();
                    grvMail.DeleteSelectedRows();
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
            }
        }
    }
}