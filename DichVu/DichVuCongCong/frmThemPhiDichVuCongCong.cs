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

namespace DichVu.DichVuCongCong
{
    public partial class frmThemPhiDichVuCongCong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmThemPhiDichVuCongCong()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThemPhiDichVuCongCong_Load(object sender, EventArgs e)
        {
           lookLoaiDichVu.Properties.DataSource = db.dvCongCongs;
            dateThangThanhToan.DateTime = DateTime.Now;
        }

        decimal SoTien = 0;
        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            var listMatBang = db.mbMatBangs.Where(p => p.MaKH != null).ToList();
            List<dvCongCongThanhToan> listAdd = new List<dvCongCongThanhToan>();

            if (listMatBang.Count > 0)
            {
                SoTien = spinPhiDV.Value / listMatBang.Count;
            }
            else
            {
                DialogBox.Alert("Hiện tại không có mặt bằng nào tính phí các dịch vụ công cộng");  
                return;
            }
            foreach (var item in listMatBang)
            {
                dvCongCongThanhToan obj = new dvCongCongThanhToan();
                obj.ThangThanhToan = dateThangThanhToan.DateTime;
                obj.MaTN = objnhanvien.MaTN;
                obj.MaMB = item.MaMB;
                obj.PhiDichVu = SoTien;
                obj.MaDV = (int)lookLoaiDichVu.EditValue;
                obj.DaTT = false;

                listAdd.Add(obj);
            }

            db.dvCongCongThanhToans.InsertAllOnSubmit(listAdd);
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Đã hình thành xong công nợ dịch vụ");  
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu");  
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}