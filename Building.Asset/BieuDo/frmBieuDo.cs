using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo : Form
    {
        public frmBieuDo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var menu = new List<ItemMenu>
            {
                new ItemMenu {ID = 1, Name = "THỐNG KÊ TIN TỨC", SubName = "TIN TỨC CÁC BAN"},
                new ItemMenu {ID = 2, Name = "THỐNG KÊ TIN TỨC", SubName = "TIN TỨC TỪNG TÒA NHÀ"},
                new ItemMenu {ID = 3, Name = "THỐNG KÊ TIN TỨC", SubName = "TỔNG PHẢN ÁNH"},
                new ItemMenu {ID = 4, Name = "THỐNG KÊ TIN TỨC", SubName = "TỶ LỆ ĐĂNG TIN"},
                new ItemMenu{ID=5,Name="THỐNG KÊ SỐ LƯỢNG SỬ DỤNG APP",SubName="THỐNG KÊ ĐĂNG KÝ/ SỬ DỤNG TỪNG TÒA NHÀ"},
                new ItemMenu{ID=6,Name="THỐNG KÊ SỐ LƯỢNG SỬ DỤNG APP",SubName="TỶ LỆ ĐĂNG KÝ/ SỬ DỤNG CỦA TỪNG TÒA NHÀ"},
                new ItemMenu{ID=7,Name="THỐNG KÊ SỐ LƯỢNG SỬ DỤNG APP",SubName="THỐNG KÊ ĐĂNG KÝ/ SỬ DỤNG CÁC TÒA NHÀ"},
                new ItemMenu{ID=8,Name="THỐNG KÊ SỐ LƯỢNG SỬ DỤNG APP",SubName="TỶ LỆ ĐĂNG KÝ/ SỬ DỤNG TỔNG"}
            };
            gridControl1.DataSource = menu;
        }

        public class ItemMenu
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string SubName { get; set; }
        }

        private void gridView1_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            switch (e.Level)
            {
                case 0:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    e.LevelAppearance.ForeColor = Color.Blue;
                    break;
                case 1:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    //e.LevelAppearance.BackColor = Color.FromArgb(141, 180, 226);
                    break;
                case 2:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    //e.LevelAppearance.BackColor = Color.FromArgb(197, 217, 241);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        private void ShowFormInPanel2(Form f)
        {
            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(f);
            f.Show();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form p in splitContainer1.Panel2.Controls)
                {
                    p.Close();
                }
                if (gridView1.GetFocusedRowCellValue("ID") == null)
                {
                    return;
                }
                var id = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
                switch (id)
                {
                    case 1: ShowFormInPanel2(new frmBieuDo_TinTucToaNha());
                        break;
                    case 2: ShowFormInPanel2(new frmBieuDo_TinTucTungToaNha());
                        break;
                    case 3: ShowFormInPanel2(new frmBieuDo_TinTucTong());
                        break;
                    case 4: ShowFormInPanel2(new frmBieuDo_TyLeDangTin());
                        break;
                    case 5: ShowFormInPanel2(new frmBieuDo_ThongKeSuDungTungToaNha());
                        break;
                    case 6: ShowFormInPanel2(new frmBieuDo_TyLeSuDungTungToaNha());
                        break;
                    case 7:
                        ShowFormInPanel2(new frmBieuDo_ThongKeSuDungCacToaNha());
                        break;
                    case 8: ShowFormInPanel2(new frmBieuDo_TyLeSuDungTong());
                        break;
                }

                
            }
            catch { }
        }

        private void gridView1_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.RowHandle >= 0)
                e.RowHeight = 25;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName.Contains("SubName"))
            {
                if (e.RowHandle == gridView1.FocusedRowHandle)
                {
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Blue;
                }
            }
        }

        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0)
            {
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Blue;
                }
                else
                {
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Blue;
                    e.Appearance.BackColor2 = Color.LightBlue;
                }
            }
        }
    }
}
