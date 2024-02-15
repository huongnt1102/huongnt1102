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

namespace Building.WorkSchedule.NhiemVu
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public int MaNVu { get; set; }

        MasterDataContext db = new MasterDataContext();

        public frmDuyet()
        {
            InitializeComponent();
        }

        void LoadDictionary()
        {
            Library.NhiemVu_TinhTrangCls objStatus = new Library.NhiemVu_TinhTrangCls();
            lookUpStatus.Properties.DataSource = objStatus.Select();
            lookUpStatus.ItemIndex = 0;

            //lookTienDo.Properties.DataSource = db.NhiemVu_TienDos;
            //lookTienDo.ItemIndex = 0;
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            try
            {
                //Library.NhiemVuCls o = new Library.NhiemVuCls();
                // Update NhiemVu
                var o = db.NhiemVus.Single(p => p.MaNVu == MaNVu);
                o.MaTT = Convert.ToInt32(lookUpStatus.EditValue);
                o.TienDo = int.Parse(spinHoanThanh.EditValue.ToString());
                // Luu NhiemVu_XuLy
                NhiemVu_XuLy objxl = new NhiemVu_XuLy();
                objxl.MaNVu = MaNVu;
                objxl.MaTT =  (int?)(lookUpStatus.EditValue);
                objxl.NgayXL = DateTime.Now;
                objxl.MaNV = Common.User.MaNV;
                objxl.TienDo = int.Parse(spinHoanThanh.EditValue.ToString());
                objxl.DienGiai = txtDienGiai.Text;
                db.NhiemVu_XuLies.InsertOnSubmit(objxl);
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch { DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn."); }
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            if (MaNVu != 0)
            {
                //Library.NhiemVuCls o = new Library.NhiemVuCls(MaNVu);
                //try
                //{
                //    lookTienDo.EditValue = o.TienDo.MaTD;
                //}
                //catch { }
                //lookUpStatus.EditValue = o.TinhTrang.MaTT;
                //spinHoanThanh.EditValue = o.PhanTramHT;
            }
        }
    }
}