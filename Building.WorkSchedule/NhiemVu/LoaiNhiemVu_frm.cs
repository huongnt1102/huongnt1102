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
    public partial class LoaiNhiemVu_frm : DevExpress.XtraEditors.XtraForm
    {
        public LoaiNhiemVu_frm()
        {
            InitializeComponent();
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            Library.NhiemVu_LoaiCls o = new Library.NhiemVu_LoaiCls();
            DataTable tbl = o.Select();
            tbl.Columns["TenLNV"].Unique = true;
            tbl.Columns["TenLNV"].AllowDBNull = false;
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
            if (gridView1.GetFocusedRowCellValue(colMaTT) != null)
            {
                if (XtraMessageBox.Show("Bạn có chắc chắn xóa <Loại nhiệm vụ> này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Library.NhiemVu_LoaiCls o = new Library.NhiemVu_LoaiCls();
                        o.MaLNV = byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Xóa không thành công vì: <Loại nhiệm vụ> này đã được sử dụng.\nVui lòng kiểm tra lại, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
                XtraMessageBox.Show("Vui lòng chọn <Loại nhiệm vụ> cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable tblTemp = (DataTable)gridControl1.DataSource;
            Library.NhiemVu_LoaiCls o;
            foreach (DataRow r in tblTemp.Rows)
            {
                if (r.RowState == DataRowState.Added)
                {
                    o = new Library.NhiemVu_LoaiCls();
                    o.TenLNV = r["TenLNV"].ToString();
                    o.STT = 0;
                    o.Insert();
                }
                else
                {
                    if (r.RowState == DataRowState.Modified)
                    {
                        o = new Library.NhiemVu_LoaiCls();
                        o.MaLNV = byte.Parse(r["MaLNV"].ToString());
                        o.TenLNV = r["TenLNV"].ToString();
                        o.STT = 0;
                        o.Update();
                    }
                }
            }

            XtraMessageBox.Show("Dữ liệu đã được lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tblTemp.Dispose();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colMaTT, 0);
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenLNV' is constrained to be unique") >= 0)
                XtraMessageBox.Show("<Tên loại nhiệm vụ> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (e.ErrorText == "Column 'TenLNV' does not allow nulls")
                XtraMessageBox.Show("<Tên loại nhiệm vụ> không được để trống. Vui lòng nhập <Tên loại nhiệm vụ>, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Alert("Vui lòng nhập <Tên loại nhiệm vụ>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}