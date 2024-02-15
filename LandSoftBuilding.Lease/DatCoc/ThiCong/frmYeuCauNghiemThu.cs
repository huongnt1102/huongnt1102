using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Lease.DatCoc.ThiCong
{
    public partial class frmYeuCauNghiemThu : DevExpress.XtraEditors.XtraForm
    {
        public Guid DatCocThiCongId { set; get; }
        public decimal TienDatCoc { set; get; }
        public decimal TienPhat { set; get; }
        public decimal TienHoanTra { set; get; }
        public frmYeuCauNghiemThu()
        {
            InitializeComponent();
        }

        private void frmYeuCauNghiemThu_Load(object sender, EventArgs e)
        {
            spTienCoc.EditValue = TienDatCoc;
            spTienPhat.EditValue = TienPhat;
            spTienHoanTra.EditValue = TienHoanTra;
        }

        private void btnNghiemThu_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dbe = new MasterDataContext())
                {
                    var dkThiCong = dbe.dkThiCongs.FirstOrDefault(x => x.Id == DatCocThiCongId);
                    if (dkThiCong != null)
                    {
                        dkThiCong.MaTT = 8;
                        dkThiCong.TienDatCoc = (decimal)spTienCoc.EditValue;
                        dkThiCong.TienPhat = (decimal)spTienPhat.EditValue;
                        dkThiCong.TienHoanTra = (decimal)spTienHoanTra.EditValue;
                        dkThiCong.GhiChuNghiemThu = (string)txtGhiChu.EditValue;
                    }
                    dbe.SubmitChanges();
                    DialogBox.Alert("Dữ liệu đã được lưu!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        private void btnHuyNghiemThu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}