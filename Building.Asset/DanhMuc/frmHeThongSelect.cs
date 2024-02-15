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
namespace Building.Asset.DanhMuc
{
    public partial class frmHeThongSelect : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public byte? MaTN { set; get; }
        public List<int> listSel = new List<int>();
        public bool IsSave = false;
        public frmHeThongSelect()
        {
            InitializeComponent();
        }
        public class HeThongCheck
        {
            public int? MaHT { set; get; }
            public string TenVT { set; get; }
            public string TenHT { set; get; }
            public bool Check { set; get; }
        }
        private void frmHeThongSelect_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = db.tbl_NhomTaiSans.Where(_ => _.MaTN == (byte?)MaTN).Select(p => new HeThongCheck { Check = false, MaHT = p.ID, TenVT = p.TenVietTat, TenHT = p.TenNhomTaiSan });
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.PostEditor();
            gridView1.RefreshData();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(gridView1.GetRowCellValue(i, "Check")))
                {
                    listSel.Add(Convert.ToInt32(gridView1.GetRowCellValue(i, "MaHT")));
                }
            }
            if (listSel.Count > 0)
            {
                IsSave = true;
                this.Close();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn hệ thống áp dụng");
            }
        }
    }
}