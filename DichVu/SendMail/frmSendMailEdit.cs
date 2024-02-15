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
    public partial class frmSendMailEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public Library.SendMail objsm;
        public tnNhanVien objnhanvien;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmSendMailEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmSendMailEdit_Load(object sender, EventArgs e)
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
                lookMailFrom.Properties.DataSource = db.SendMailAccounts;
                lookNguoiNhan.DataSource = db.tnKhachHangs.Select(p => new
                {
                    p.MaKH,
                    KhachHang = p.IsCaNhan.HasValue ? (p.IsCaNhan.Value ? p.HoKH + " " + p.TenKH : p.CtyTen) : "",
                    p.EmailKH
                });

                if (objsm != null)
                {
                    objsm = db.SendMails.Single(p => p.MailID == objsm.MailID);
                    txtTieuDe.Text = objsm.TieuDe;
                    txtNoiDung.InnerHtml = objsm.NoiDung;
                    dateNgayGui.EditValue = objsm.ThoiGianGui;
                }
                else
                {
                    objsm = new Library.SendMail();
                    db.SendMails.InsertOnSubmit(objsm);
                    dateNgayGui.DateTime = DateTime.Now;
                    var objMail = db.SendMailAccounts.Where(p => p.DiaChi == objnhanvien.Email).FirstOrDefault();
                    if (objMail != null)
                        lookMailFrom.EditValue = objMail.ID;
                }
                gcNguoiNhan.DataSource = objsm.SendMailDetails;
            }
            catch { }

            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private string TinhTrangGui(SendMailDetail objsmdetail)
        {
            string result = string.Empty;
            switch (objsmdetail.TrangThai.Value)
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

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (txtTieuDe.Text.Trim().Length == 0)
            {
                DialogBox.Alert("Vui lòng nhập tiêu đề");  
                return;
            }
            if (dateNgayGui.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn ngày gửi");  
                return;
            }
            objsm.NoiDung = txtNoiDung.InnerHtml;
            objsm.TieuDe = txtTieuDe.Text;
            objsm.MaNV = objnhanvien.MaNV;
            objsm.ThoiGianGui = dateNgayGui.DateTime;
            if (lookMailFrom.EditValue != null)
            {
                objsm.MailFrom = (int)lookMailFrom.EditValue;
            }
            else
            {
                var objfrom = db.SendMailAccounts.FirstOrDefault(p => p.DiaChi == Library.Properties.Settings.Default.YourMail);
                if (objfrom == null)
                {
                    DialogBox.Error("Chưa thiết lập địa chỉ Email gửi mặc định");  
                    return;
                }
                objsm.MailFrom = objfrom.ID;
            }
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");  
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grvNguoiNhan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvNguoiNhan.SetFocusedRowCellValue("TrangThai", 3); // dang cho gui
        }

        private void ckSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSelectAll.Checked)
            {
                var lstAllKhachHang = db.tnKhachHangs;
                foreach (var item in lstAllKhachHang)
                {
                    grvNguoiNhan.AddNewRow();
                    grvNguoiNhan.SetFocusedRowCellValue(colMaKhachHang, item.MaKH);
                    grvNguoiNhan.SetFocusedRowCellValue(colTrangThai, 3); // trang thai dang cho gui
                }
            }
            else
            {
                grvNguoiNhan.SelectAll();
                grvNguoiNhan.DeleteSelectedRows();
                //for (int i = 0; i < grvNguoiNhan.RowCount; i++)
                //{
                //    grvNguoiNhan.DeleteRow(i);
                //}
            }
        }
    }
}