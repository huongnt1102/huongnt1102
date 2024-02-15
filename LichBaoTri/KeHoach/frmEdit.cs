using System;
using System.Linq;
using System.Windows.Forms;

namespace LichBaoTri.KeHoach
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        public long? Id { get; set; }
        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if(Id == 0)
            {
                dateTuNgay.DateTime = System.DateTime.Now;
                dateDenNgay.DateTime = System.DateTime.Now;
            }
            else
            {
                var db = new Library.MasterDataContext();
                var keHoach = db.ml_KeHoaches.FirstOrDefault(_=>_.Id == Id);
                if(keHoach !=null)
                {
                    txtMa.Text = keHoach.Ma;
                    txtTen.Text = keHoach.Ten;
                    dateTuNgay.EditValue = keHoach.TuNgay;
                    dateDenNgay.EditValue = keHoach.DenNgay;
                }
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var model = new { id = Id, ma = txtMa.Text, ten = txtTen.Text, tuNgay = dateTuNgay.DateTime, denNgay = dateDenNgay.DateTime, nhanVien = Library.Common.User.MaNV, maTn = MaTN };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            Library.Class.Connect.QueryConnect.Query<bool>("ml_KeHoach_SaveEdit", param);

            Library.DialogBox.Success("Lưu dữ liệu thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}