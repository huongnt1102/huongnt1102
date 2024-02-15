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
using System.Data.Linq.SqlClient;

namespace DichVu.KhachHang
{
    public partial class frmFind : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH = 0;
        public string HoTen = "";
        public frmFind()
        {
            InitializeComponent();
        }

        private void txtKey_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaKH) != null)
            {
                MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                HoTen = gridView1.GetFocusedRowCellValue(colHoKH).ToString();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }

            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnChon_Click(sender, e);
        }

        private void Find_frm_Load(object sender, EventArgs e)
        {
            
        }

        private void Find_frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoadData();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            string queryString = txtKey.Text;
            if (queryString != "")
            {
                using (var db = new MasterDataContext())
                {
                    gridControl1.DataSource = db.tnKhachHangs
                                    .Where(p => SqlMethods.Like(p.TenKH, "%" + queryString + "%") | SqlMethods.Like(p.HoKH, "%" + queryString + "%")
                                            | SqlMethods.Like(p.CtyTen, "%" + queryString + "%"))
                                    .OrderBy(p => p.TenKH).AsEnumerable()
                                    .Select(p => new
                                    {
                                        p.MaKH,
                                        p.KyHieu,
                                        HoTenKH = p.IsCaNhan.GetValueOrDefault() ? (p.HoKH + " " + p.TenKH) : p.CtyTen,
                                        SoCMND = p.IsCaNhan.GetValueOrDefault() ? p.CMND : p.CtyMaSoThue,
                                        DienThoai = p.DienThoaiKH
                                    }).ToList();
                }
            }
            else
                gridControl1.DataSource = null;

            try
            {
                wait.Close();
            }
            catch { }
            finally
            {
                System.GC.Collect();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}