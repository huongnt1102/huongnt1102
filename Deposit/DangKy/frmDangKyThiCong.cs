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

namespace Deposit.DangKy
{
    public partial class frmDangKyThiCong : DevExpress.XtraEditors.XtraForm
    {
        public frmDangKyThiCong()
        {
            InitializeComponent();
        }

        private void frmDangKyThiCong_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            var param = new Dapper.DynamicParameters();
            gridControl1.DataSource = Library.Class.Connect.QueryConnect.Query<dep_dang_ky_thi_cong_get>("dep_dang_ky_thi_cong_get", param);
        }

        public class dep_dang_ky_thi_cong_get
        {
            public Guid Id { get; set; }

            public byte? MaTN { get; set; }

            public int? MaMB { get; set; }

            public int? MaKH { get; set; }

            public int? MaPB { get; set; }

            public int? MaTT { get; set; }

            public System.DateTime? NgayDangKy { get; set; }

            public System.DateTime? TuNgay { get; set; }

            public System.DateTime? DenNgay { get; set; }

            public string LinkTaiLieu { get; set; }

            public string GhiChu { get; set; }

            public string MaSoMB { get; set; }

            public string TenKH { get; set; }

            public string TenPB { get; set; }

            public string TenVT { get; set; }

            public string TenTT { get; set; }

        }

        /// <summary>
        /// Nạp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gridView1.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            foreach (var i in indexs)
            {
                Guid? id = (Guid?)gridView1.GetRowCellValue(i, "Id");
                var model = new { id = id, matt = 2 };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("dep_dang_ky_thi_cong_duyet", param);
            }
            Library.DialogBox.Success();
            LoadData();
        }

        /// <summary>
        /// Không duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gridView1.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            foreach (var i in indexs)
            {
                Guid? id = (Guid?)gridView1.GetRowCellValue(i, "Id");
                var model = new { id = id, matt = 3 };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("dep_dang_ky_thi_cong_duyet", param);
            }
            Library.DialogBox.Success();
            LoadData();
        }
    }
}