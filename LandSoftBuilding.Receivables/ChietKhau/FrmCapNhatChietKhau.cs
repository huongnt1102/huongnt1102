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

namespace LandSoftBuilding.Receivables.ChietKhau
{
    public partial class FrmCapNhatChietKhau : DevExpress.XtraEditors.XtraForm
    {
        public string IdHoaDons { get; set; }
        public byte? matn { get; set; }
        public FrmCapNhatChietKhau()
        {
            InitializeComponent();
        }

        private void FrmCapNhatChietKhau_Load(object sender, EventArgs e)
        {
            try
            {
                var model_ck = new { matn = matn };
                var param_ck = new Dapper.DynamicParameters();
                param_ck.AddDynamicParams(model_ck);
                var result = Library.Class.Connect.QueryConnect.Query<decimal>("dvhoadon_get_ty_le_chiet_khau", param_ck);
                if (result.Count() > 0)
                {
                    spinTyLeCK.EditValue = result.First();
                }
            }
            catch { }
        }

        private void itemThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var model_ck = new { matn = matn, ck = (decimal)spinTyLeCK.EditValue, ids = IdHoaDons, manv = Library.Common.User.MaNV, tienck = (decimal)spinTienCK.EditValue };
                var param_ck = new Dapper.DynamicParameters();
                param_ck.AddDynamicParams(model_ck);
                Library.Class.Connect.QueryConnect.Query<bool>("dvhoadon_set_ty_le_chiet_khau", param_ck);

                Library.DialogBox.Success();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch { }
        }
    }
}