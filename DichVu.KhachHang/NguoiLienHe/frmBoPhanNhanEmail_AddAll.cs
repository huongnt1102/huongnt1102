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
using Library;

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmBoPhanNhanEmail_AddAll : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }

        public frmBoPhanNhanEmail_AddAll()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            var db = new Library.MasterDataContext();
            chkBoPhan.Properties.DataSource = db.tnKhachHang_NguoiLienHe_BoPhans.ToList();
            if (ID != null)
            {
                var obj = db.tnKhachHang_BoPhan_NhanEmails.FirstOrDefault(_ => _.ID == ID);
                if (obj != null)
                {
                    chkBoPhan.SetEditValue(obj.NhomBoPhan);
                    txtFormThucThi.Text = obj.FormThucThi;
                    txtLoaiGui.Text = obj.LoaiGui;
                    
                }
            }
        }


        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var makh = (int?)glkKhachHang.EditValue;
            //if(makh == null)
            //{
            //    Library.DialogBox.Alert("Vui lòng chọn khách hàng");
            //}

            var strBoPhan = (chkBoPhan.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            //if (strMatBang == "") return;
            var ltMatBang = strBoPhan.Split(',');
            //if (strMatBang == "") { Library.DialogBox.Alert("Vui lòng chọn mặt bằng"); return; }
            //foreach(var item in ltMatBang)
            //{
            //    var model = new { id = Id, makh = int.Parse(item), keHoachId = KeHoachId, nguoiLienHe = txtLoaiGui.Text, soDienThoai = txtSoDienThoai.Text, chucVu = txtChucVu.Text, tuNgay = dateTuNgay.DateTime, denNgay = dateDenNgay.DateTime, ghiChu = txtFormThucThi.Text, isXacNhan = IsXacNhan };
            //    var param = new Dapper.DynamicParameters();
            //    param.AddDynamicParams(model);
            //    Library.Class.Connect.QueryConnect.Query<bool>("ml_KhachHang_SaveEdit", param);
            //}

            var db = new MasterDataContext();
            //if (ID != null)
            //{
                var obj = db.tnKhachHang_BoPhan_NhanEmails.FirstOrDefault(_ => _.ID == ID);
                if (obj == null)
                {
                    obj = new tnKhachHang_BoPhan_NhanEmail();
                    db.tnKhachHang_BoPhan_NhanEmails.InsertOnSubmit(obj);
                }

                obj.LoaiGui = txtLoaiGui.Text;
                obj.NhomBoPhan = strBoPhan;
                obj.TenNhomBoPhan = chkBoPhan.Text;
                obj.FormThucThi = txtFormThucThi.Text;

                db.SubmitChanges();

            //}
            

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