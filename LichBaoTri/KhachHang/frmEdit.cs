using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LichBaoTri.KhachHang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public long? Id { get; set; }
        public byte? MaTN { get; set; }
        public long? KeHoachId { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsXacNhan { get; set; }

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            var db = new Library.MasterDataContext();
            glkKhachHang.Properties.DataSource = db.tnKhachHangs.Where(_=>_.MaTN == MaTN).Select(_=>new {_.MaKH, TenKH = _.IsCaNhan == true ? _.TenKH : _.CtyTen}).ToList();
            if(Id != 0)
            {
                var khachHang = db.ml_KhachHangs.FirstOrDefault(_ => _.Id == Id);
                if (khachHang != null)
                {
                    glkKhachHang.EditValue = khachHang.MaKH;
                    txtNguoiDaiDien.Text = khachHang.NguoiLienHe;
                    txtSoDienThoai.Text = khachHang.SDT;
                    txtChucVu.Text = khachHang.ChucVu;
                    dateTuNgay.EditValue = khachHang.TuNgay;
                    dateDenNgay.EditValue = khachHang.DenNgay;
                    txtGhiChu.Text = khachHang.GhiChu;
                    if(IsXacNhan == false)
                    {
                        IsXacNhan = khachHang.IsDongY;
                    }
                }

            }
            if (IsShow == false)
                Hide();
        }

        public void Hide()
        {
            layoutNguoiDaiDien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutSoDienThoai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutChucVu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var makh = (int?)glkKhachHang.EditValue;
            if(makh == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn khách hàng");
            }

            var model = new { id = Id, makh = makh, keHoachId = KeHoachId, nguoiLienHe = txtNguoiDaiDien.Text, soDienThoai = txtSoDienThoai.Text, chucVu = txtChucVu.Text, tuNgay = dateTuNgay.DateTime, denNgay = dateDenNgay.DateTime, ghiChu = txtGhiChu.Text, isXacNhan = IsXacNhan };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            Library.Class.Connect.QueryConnect.Query<bool>("ml_KhachHang_SaveEdit", param);

            Library.DialogBox.Success("Lưu dữ liệu thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}