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

namespace ToaNha
{
    public partial class frmSelect : DevExpress.XtraEditors.XtraForm
    {
        public List<int> ListNhanVien { get; set; }

        void NhanVienLoad()
        {
            using (var db = new MasterDataContext())
            {
                var ltMaTN = Common.TowerList.Select(p => p.MaTN).ToList();
                gcNhanVien.DataSource = (from nv in db.tnNhanViens
                                         where ltMaTN.Contains(nv.MaTN.GetValueOrDefault())
                                         orderby nv.MaSoNV
                                         select new { nv.MaNV, nv.MaSoNV, nv.HoTenNV, nv.tnPhongBan.TenPB, nv.tnChucVu.TenCV}).ToList();
            }
        }

        public frmSelect()
        {
            InitializeComponent();
        }

        private void frmSelect_Load(object sender, EventArgs e)
        {
            NhanVienLoad();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var indexs = gvNhanVien.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn nhân viên");
                return;
            }

            ListNhanVien = new List<int>();

            foreach (var i in indexs)
            {
                ListNhanVien.Add((int)gvNhanVien.GetRowCellValue(i, "MaNV"));
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}