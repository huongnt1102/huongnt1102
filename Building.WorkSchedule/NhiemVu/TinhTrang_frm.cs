using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.WorkSchedule.NhiemVu
{
    public partial class TinhTrang_frm : DevExpress.XtraEditors.XtraForm
    {
        public TinhTrang_frm()
        {
            InitializeComponent();
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            Library.NhiemVu_TinhTrangCls o = new Library.NhiemVu_TinhTrangCls();
            DataTable tbl = o.Select();
            tbl.Columns["TenTT"].Unique = true;
            tbl.Columns["TenTT"].AllowDBNull = false;
            gridControl1.DataSource = tbl;
            tbl.Dispose();
        }

        private void btnDOng_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có chắc chắn xóa <Tình trạng> này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (gridView1.GetFocusedRowCellValue(colMaTT) != null)
                    {
                        Library.NhiemVu_TinhTrangCls o = new Library.NhiemVu_TinhTrangCls();
                        o.MaTT = int.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Xóa không thành công vì: <Tình trạng> này đã được sử dụng.\nVui lòng kiểm tra lại, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng chọn <Tình trạng> cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable tblTemp = (DataTable)gridControl1.DataSource;
            Library.NhiemVu_TinhTrangCls o;
            foreach (DataRow r in tblTemp.Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    o = new Library.NhiemVu_TinhTrangCls();
                    o.TenTT = r["TenTT"].ToString();
                    o.Insert();
                }
                else
                {
                    if (r.RowState == DataRowState.Modified)
                    {
                        o = new Library.NhiemVu_TinhTrangCls();
                        o.MaTT = int.Parse(r["MaTT"].ToString());
                        o.TenTT = r["TenTT"].ToString();
                        o.Update();
                    }
                }
            }

            XtraMessageBox.Show("Dữ liệu đã được lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tblTemp.Dispose();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colMaTT, -1);
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenTT' is constrained to be unique") >= 0)
                XtraMessageBox.Show("<Tên tình trạng> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (e.ErrorText == "Column 'TenTT' does not allow nulls")
                XtraMessageBox.Show("<Tên tình trạng> không được để trống. Vui lòng nhập <Tên tình trạng>, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Alert("Vui lòng nhập <Tên tình trạng>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}