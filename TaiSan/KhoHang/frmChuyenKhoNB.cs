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

namespace TaiSan.KhoHang
{
    public partial class frmChuyenKhoNB : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        public int? ID { get; set; }
        Kho obj1, obj2;

        public frmChuyenKhoNB()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmChuyenKhoNB_Load(object sender, EventArgs e)
        {
            obj1 = db.Khos.Single(p=>p.ID == ID);
            lookKhoNhan.Properties.DataSource = lookKhoChuyen.Properties.DataSource = db.KhoHangs;
            slookLoaiTaiSan.Properties.DataSource = db.tsLoaiTaiSans;
            slookLoaiTaiSan.EditValue = obj1.MaTS;
            lookKhoChuyen.EditValue = obj1.MaKhoHang;
            dateNgayChuyen.EditValue = DateTime.Now;
        }

        private void lookKhoChuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (lookKhoChuyen.EditValue == null)
                return;
           // obj1 = db.Khos.SingleOrDefault(p => p.MaTS == MaLTS && p.MaKhoHang == (int?)lookKhoChuyen.EditValue);
            if (obj1 != null)
            {
                spinSL1.EditValue = SpinSLChuyen.EditValue = (decimal?)obj1.SoLuong;
                spinDonGia1.EditValue = spinDonGia2.EditValue = (decimal?)obj1.DonGia;
                spinHSD1.EditValue = spinHSD2.EditValue = (decimal?)obj1.HanSuDung;
            }
        }

        private void lookKhoNhan_EditValueChanged(object sender, EventArgs e)
        {
            if (lookKhoNhan.EditValue == null)
                return;
            var a= (int?)lookKhoNhan.EditValue;
            var obj = db.Khos.Where(p => p.MaTS == obj1.MaTS && p.MaKhoHang == (int?)lookKhoNhan.EditValue).ToList().Count == 0 ? 0 : db.Khos.Where(p => p.MaTS == obj1.MaTS && p.MaKhoHang == (int?)lookKhoNhan.EditValue).GroupBy(p => p.MaKhoHang)
                .Select(p => new
                {
                    SoLuong = p.Sum(s => s.SoLuong)
                }).Single().SoLuong;
            //spinSL2.EditValue = (decimal?)obj.SoLuong;
            //if (obj2 != null)
            //{
            //    spinSL1.EditValue = (decimal?)obj2.SoLuong;
            //    spinDonGia1.EditValue = (decimal?)obj2.DonGia;
            //    spinHSD1.EditValue = (decimal?)obj2.HanSuDung;
            //}
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (slookLoaiTaiSan.EditValue == null)
            {
                DialogBox.Alert("Bạn cần chọn tài sản để tiến hành điều chuyển");
                slookLoaiTaiSan.Focus();
                return;
            }

            if (SpinSLChuyen.Value < 0)
            {
                DialogBox.Alert("Số lượng chuyển cần phải lớn hơn [0]. Vui lòng kiểm tra lại!");
                SpinSLChuyen.Focus();
                return;
            }

            if (obj1 == null)
            {
                DialogBox.Alert("Bạn chưa chọn kho hàng cần chuyển hoặc tài sản trong kho hàng chuyển có số lượng bằng [0]. Vui lòng kiểm tra lại!");
                lookKhoChuyen.Focus();
                return;
            }

            if (lookKhoNhan.EditValue == null)
            {
                DialogBox.Alert("Bạn cần chọn kho hàng nhận!");
                lookKhoNhan.Focus();
                return;
            }


            if (lookKhoNhan.EditValue == lookKhoChuyen.EditValue)
            {
                DialogBox.Alert("Kho nhận phải khác kho chuyển. Xin cảm ơn!");
                lookKhoNhan.Focus();
                return;
            }

            if (SpinSLChuyen.Value > spinSL1.Value)
            {
                DialogBox.Alert("Số lượng chuyển phải nhỏ hơn số lượng trong kho chuyển.Xin cảm ơn!");
                SpinSLChuyen.Focus();
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                obj1.SoLuong -= Convert.ToInt32(SpinSLChuyen.Value);
                if (obj2 == null)
                {
                    obj2 = new Kho();
                    obj2.NgayNhap = (DateTime?)dateNgayChuyen.EditValue;
                    obj2.MaTT = obj1.MaTT;
                    obj2.MaTS = obj1.MaTS;
                    obj2.MaNV = obj1.MaNV;
                    obj2.MaNK = obj1.MaNK;
                    obj2.MaKhoHang = (int?)lookKhoNhan.EditValue;
                    obj2.HanSuDung = (decimal?)spinHSD2.EditValue;
                    obj2.DonGia = (decimal?)spinDonGia2.EditValue;
                    obj2.SoLuong = (int?)SpinSLChuyen.Value;
                    db.Khos.InsertOnSubmit(obj2);
                }
                else
                {
                    obj2.SoLuong += Convert.ToInt32(SpinSLChuyen.Value);
                    obj2.MaKhoHang = (int?)lookKhoNhan.EditValue;
                    obj2.HanSuDung = (decimal?)spinHSD2.EditValue;
                    obj2.DonGia = (decimal?)spinDonGia2.EditValue;
                }
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã cập nhật");
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể cập nhật. Vui lòng kiểm tra kết nối Internet!");
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}