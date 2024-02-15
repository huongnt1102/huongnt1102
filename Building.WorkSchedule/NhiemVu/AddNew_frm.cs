using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using System.Linq;
using Library;

namespace Building.WorkSchedule.NhiemVu
{
    public partial class AddNew_frm : DevExpress.XtraEditors.XtraForm
    {
        public Library.NhiemVu o;
        public tnNhanVien objNV;
        public bool IsUpdate = false;
        public int MaNVu = 0;
        int MaNV = 0;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        MasterDataContext db = new MasterDataContext();

        public AddNew_frm()
        {
            InitializeComponent();
            
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void LoadDictionary()
        {
            Library.NhiemVu_LoaiCls objLoai = new Library.NhiemVu_LoaiCls();
            LookupLoai.Properties.DataSource = objLoai.Select();
            LookupLoai.ItemIndex = 0;

            Library.NhiemVu_MucDoCls objMucDo = new Library.NhiemVu_MucDoCls();
            lookUpMucDo.Properties.DataSource = objMucDo.Select();
            lookUpMucDo.ItemIndex = 0;

            Library.NhiemVu_TinhTrangCls objStatus = new Library.NhiemVu_TinhTrangCls();
            lookUpStatus.Properties.DataSource = objStatus.Select();
            lookUpStatus.ItemIndex = 0;
        }

        void LoadData()
        {
            //Library.NhiemVuCls o = new Library.NhiemVuCls(MaNVu);
            o = db.NhiemVus.Single(p => p.MaNVu == this.MaNVu);
            lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", Common.User.HoTenNV , DateTime.Now);
            lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", o.tnNhanVien.HoTenNV, o.NgayBD);
            txtDienGiai.Text = o.DienGiai;
            txtTieuDe.Text = o.TieuDe;
            LookupLoai.EditValue = o.MaLNV;
            //lookTienDo.EditValue = o.TienDo.MaTD;
            lookUpMucDo.EditValue = o.MaMD;
            lookUpStatus.EditValue = o.MaTT;
            if (o.NgayHT!=null && o.NgayHT.Value.Year != 1)
                dateNgayHT.DateTime = o.NgayHT.Value;
            dateNgayKT.DateTime = o.NgayHH.Value;
            dateNgayBD.DateTime = o.NgayBD.Value;

            MaNV = (int)o.MaNV;
            spinHoanThanh.EditValue = o.TienDo;
            checkBNhacViec.Checked = o.IsNhac.Value;
            btnRing.Tag = o.Rings;
            if (o.IsNhac==true)
                dateNhacViec.Enabled = true;
            else
                dateNhacViec.Enabled = false;
            if (o.NgayNhac!=null && o.NgayNhac.Value.Year != 1)
                dateNhacViec.DateTime = o.NgayNhac.Value;
            btnKhachHang.Text = o.tnKhachHang.HoKH;            
            btnKhachHang.Tag = o.tnKhachHang.MaKH;
        }

        private void AddNew_frm_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            LoadDictionary();
            dateNgayHT.Enabled = false;
            if (MaNVu != 0)
            {
                LoadData();
            }
            else
            {
                lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", Common.User.HoTenNV, DateTime.Now);
                lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", Common.User.HoTenNV, DateTime.Now);
                dateNhacViec.Enabled = false;
                dateNgayBD.DateTime = dateNgayKT.DateTime = DateTime.Now;
                btnRing.Enabled = false;
                MaNV = Common.User.MaNV;
            }

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemClearText.ItemClick += ItemClearText_ItemClick;
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void btnSaveClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập <Chủ đề>. Xin cảm ơn.");
                txtTieuDe.Focus();
                return;
            }

            if (dateNgayBD.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập <Ngày bắt đầu>. Xin cảm ơn.");
                dateNgayBD.Focus();
                return;
            }

            if (dateNgayKT.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập <Ngày kết thúc>. Xin cảm ơn.");
                dateNgayKT.Focus();
                return;
            }

            if (dateNgayBD.DateTime.CompareTo(dateNgayKT.DateTime) > 0)
            {
                XtraMessageBox.Show("<Ngày kết thúc> phải lớn hơn <Ngày bắt đầu>. Vui lòng kiểm tra lại, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateNgayKT.Focus();
                return;
            }

            //if (lookTienDo.Text == "")
            //{
            //    DialogBox.Error("Vui lòng chọn tiến độ");
            //    return;
            //}

            //Library.NhiemVuCls o = new Library.NhiemVuCls();
            if (MaNVu == 0)
            {
                o = new Library.NhiemVu();
                o.NgayTao = DateTime.Now;
                o.MaNV = MaNV;
                db.NhiemVus.InsertOnSubmit(o);
            }
            else
            {
                o.NgayCapNhat = DateTime.Today;
                o.NguoiCapNhat = Common.User.MaNV;
            }
            o.TieuDe = txtTieuDe.Text.Trim();
            if (lookUpStatus.EditValue!=null)
              o.MaTT = int.Parse(lookUpStatus.EditValue.ToString());
            o.DienGiai = txtDienGiai.Text.Trim();
            if (btnKhachHang.Tag != null)
                o.MaKH = int.Parse(btnKhachHang.Tag.ToString());
            //else
            //   o.MaKH = 0;
            if (LookupLoai.EditValue!=null)
            o.MaLNV = int.Parse(LookupLoai.EditValue.ToString());
            //o.MaTD = (short)lookTienDo.EditValue;
            if(lookUpMucDo.EditValue!=null)
              o.MaMD = int.Parse(lookUpMucDo.EditValue.ToString());
            o.NgayBD = dateNgayBD.DateTime;
            o.NgayHH = dateNgayKT.DateTime;
            o.TienDo = int.Parse(spinHoanThanh.EditValue.ToString());
            o.IsNhac = checkBNhacViec.Checked;
            if (dateNhacViec.Text != "")
                o.NgayNhac = dateNhacViec.DateTime;
            if (btnRing.Tag != null)
                o.Rings = btnRing.Tag.ToString();
            else
                o.Rings = "";
            //if (MaNVu == 0)
            //    o.Insert();
            //else
            //{
            //    o.MaNVu = MaNVu;
            //    o.Update();
            //}
            db.SubmitChanges();
            IsUpdate = true;
            MessageBox.Show("Dữ liệu đã cập nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                var frm = new DichVu.KhachHang.frmFind();

                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    btnKhachHang.Tag = frm.MaKH;
                    btnKhachHang.Text = frm.HoTen;
                }
            }
            else
            {
                btnKhachHang.Tag = 0;
                btnKhachHang.Text = "";
            }
        }

        private void btnFinish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MaNVu != 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xác nhận hoàn thành nhiệm vụ này không?") == DialogResult.Yes)
                {
                    Library.NhiemVuCls o = new Library.NhiemVuCls();
                    o.MaNVu = MaNVu;
                    o.UpdateFinish();

                    LoadData();
                }
            }
        }

        private void chkRemine_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
            {
                dateNhacViec.Enabled = true;
                btnRing.Enabled = true;
            }
            else
            {
                dateNhacViec.Enabled = false;
                btnRing.Enabled = false;
            }
        }

        private void txtTieuDe_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            this.Text = _New.Text + " - Nhiệm vụ";
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            Rings_frm frm = new Rings_frm();
            if (btnRing.Tag != null)
                frm.Rings = btnRing.Tag.ToString();
            frm.ShowDialog();
            if (frm.IsUpdate)
                btnRing.Tag = frm.Rings;
        }

        private void LookupLoai_EditValueChanged(object sender, EventArgs e)
        {
            //lookTienDo.Properties.DataSource = db.NhiemVu_TienDos.Where(p => p.MaLNV == (byte)LookupLoai.EditValue);
            //lookTienDo.ItemIndex = 0;
        }
    }
}