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

namespace LandSoftBuilding.Lease.TangGia
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? Id{ get; set; }
        public byte? MaTN { get; set; }

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;

            var db = new Library.MasterDataContext();
            var hopDong = db.ctLichThanhToans.Where(_ => _.MaHD == Id).OrderByDescending(_ => _.DenNgay).FirstOrDefault();
            if(hopDong != null)
            {
                if (hopDong.DenNgay != null)
                    dateDenNgay.EditValue = hopDong.DenNgay;

            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var model = new { id = Id, dongia = spinDonGia.Value, tungay = dateTuNgay.DateTime, denngay = dateDenNgay.DateTime };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            Library.Class.Connect.QueryConnect.Query<bool>("ct_tang_gia", param);

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