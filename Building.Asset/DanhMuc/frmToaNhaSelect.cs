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
    public partial class frmToaNhaSelect : DevExpress.XtraEditors.XtraForm
    {
        public List<byte> ltToaNhaSelect=new List<byte>();
        public bool IsSave = false;
        MasterDataContext db = new MasterDataContext();
        public frmToaNhaSelect()
        {
            InitializeComponent();
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
                    ltToaNhaSelect.Add(Convert.ToByte(gridView1.GetRowCellValue(i, "MaTN")));
                }
            }
            if (ltToaNhaSelect.Count <= 0)
            {
                DialogBox.Alert("Vui lòng chọn tòa nhà muốn áp dụng");
            }
            else
            {
                IsSave = true;
                this.Close();
            }

        }

        private void frmToaNhaSelect_Load(object sender, EventArgs e)
        {
            var ltToaNha = (from nv in db.tnToaNhaNguoiDungs
                        join tn in db.tnToaNhas on nv.MaTN equals tn.MaTN
                        where nv.MaNV == Common.User.MaNV
                            select new ToaNhaSelect() {Check=false, MaTN = tn.MaTN, TenTN = tn.TenTN })
                           .ToList();
            gridControl1.DataSource=ltToaNha;
        }
        public class ToaNhaSelect
        {
            public  byte MaTN {set;get;}
            public string TenTN { set; get; }
            public bool Check { set; get; }
        }
    }
}