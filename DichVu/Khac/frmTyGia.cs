using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DichVu.Khac
{
    public partial class frmTyGia : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }

        public frmTyGia()
        {
            InitializeComponent();
        }

        private void frmLaiSuat_Load(object sender, EventArgs e)
        {
            spinTyGia.EditValue = 0;
            var db = new Library.MasterDataContext();
            glkLoaiTien.Properties.DataSource = db.LoaiTiens;
            glkLoaiTien.EditValue = 2;
        }

        /// <summary>
        /// Đồng ý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var model = new { MaTN = MaTN, MaLT = (int?)glkLoaiTien.EditValue, TyGia = spinTyGia.Value };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("dvDichVuKhac_Cap_Nhat_Ty_Gia_Hang_Loat", param);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch { }
        }

        /// <summary>
        /// Hủy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void glkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glkLoaiTien.Properties.GetRowByKeyValue(glkLoaiTien.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    spinTyGia.EditValue = type.GetProperty("TyGia").GetValue(r, null);
                }
            }
            catch { }
        }
    }
}