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

namespace DichVu.DichVuCongCong
{
    public partial class frmCongNo : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmCongNo()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmCongNo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            gcCongNo.DataSource = db.dvCongCongThanhToans
                .Where(p=>!p.DaTT.Value)
                .Select(p => new
                {
                    p.MaDVTT,
                    p.dvCongCong.TenLoaiDV,
                    p.mbMatBang.MaSoMB,
                    p.PhiDichVu,
                    p.DaTT,
                    ThangThanhToan = p.ThangThanhToan ?? DateTime.Now
                });
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmThemPhiDichVuCongCong frm = new frmThemPhiDichVuCongCong() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công nợ cần thanh toán");  
                return;
            }

            var objcn = db.dvCongCongThanhToans.Single(p => p.MaDVTT == (int)grvCongNo.GetFocusedRowCellValue("MaDVTT"));
            using (frmThanhToan frm = new frmThanhToan() { objdvcc = objcn })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

    }
}