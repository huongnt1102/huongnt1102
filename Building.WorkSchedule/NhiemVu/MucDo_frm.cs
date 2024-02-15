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
    public partial class MucDo_frm : DevExpress.XtraEditors.XtraForm
    {
        public MucDo_frm()
        {
            InitializeComponent();
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            Library.NhiemVu_MucDoCls o = new Library.NhiemVu_MucDoCls();
            DataTable tbl = o.Select();
            tbl.Columns["TenMD"].Unique = true;
            tbl.Columns["TenMD"].AllowDBNull = false;
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
            if (XtraMessageBox.Show("Bạn có chắc chắn xóa <Mức độ> này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (gridView1.GetFocusedRowCellValue(colMaMD) != null)
                    {
                        Library.NhiemVu_MucDoCls o = new Library.NhiemVu_MucDoCls();
                        o.MaMD = int.Parse(gridView1.GetFocusedRowCellValue(colMaMD).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Xóa không thành công vì! <Mức độ> này đã được sử dụng.\nVui lòng kiểm tra lại, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng chọn <Mức độ> cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.FocusedColumn = colMaMD;
            DataTable tblTemp = (DataTable)gridControl1.DataSource;
            Library.NhiemVu_MucDoCls o;
            foreach (DataRow r in tblTemp.Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    o = new Library.NhiemVu_MucDoCls();
                    o.TenMD = r["TenMD"].ToString();
                    o.STT = 0;
                    o.Insert();
                }
                else
                {
                    if (r.RowState == DataRowState.Modified)
                    {
                        o = new Library.NhiemVu_MucDoCls();
                        o.MaMD = int.Parse(r["MaMD"].ToString());
                        o.TenMD = r["TenMD"].ToString();
                        o.Update();
                    }
                }
            }

            XtraMessageBox.Show("Dữ liệu đã được lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tblTemp.Dispose();
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenMD' is constrained to be unique") >= 0)
                XtraMessageBox.Show("<Tên mức độ> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (e.ErrorText.IndexOf("Column 'MaMD' is constrained to be unique") >= 0)
            //    XtraMessageBox.Show("Màu này đã bị trùng. Vui lòng chọn màu khác, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (e.ErrorText == "Column 'TenMD' does not allow nulls")
                XtraMessageBox.Show("<Tên mức độ> không được để trống. Vui lòng nhập <Tên mức độ>, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colMaMD, -1);
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Alert("Vui lòng nhập <Tên mức độ>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}