using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.WorkSchedule.LichHen
{
    public partial class ThoiDiem_frm : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db = new Library.MasterDataContext();
        public ThoiDiem_frm()
        {
            InitializeComponent();
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            //Library.LichHen_ThoiDiemCls o = new Library.LichHen_ThoiDiemCls();
            //DataTable tbl = o.Select();
            //tbl.Columns["TenTD"].Unique = true;
            //tbl.Columns["TenTD"].AllowDBNull = false;
            //gridControl1.DataSource = tbl;
            //tbl.Dispose();
            
            gc.DataSource = _db.LichHen_ThoiDiems;
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
            if (gv.GetFocusedRowCellValue(colMaTT) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn xóa <Thời điểm> này không?") == DialogResult.Yes)
                {
                    try
                    {
                        //Library.LichHen_ThoiDiemCls o = new Library.LichHen_ThoiDiemCls();
                        //o.MaTD = byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString());
                        //o.Delete();
                        gv.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Alert("Xóa không thành công vì: <Thời điểm> này đã được dùng.\nVui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                 DialogBox.Alert("Vui lòng chọn <Thời điểm> cần xóa!");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //DataTable tblTemp = (DataTable) gridControl1.DataSource;
                //Library.LichHen_ThoiDiemCls o;
                //foreach (DataRow r in tblTemp.Rows)
                //{
                //    if (r.RowState == DataRowState.Added)
                //    {
                //        o = new Library.LichHen_ThoiDiemCls();
                //        o.TenTD = r["TenTD"].ToString();
                //        o.STT = 0;
                //        o.Insert();
                //    }
                //    else
                //    {
                //        if (r.RowState == DataRowState.Modified)
                //        {
                //            o = new Library.LichHen_ThoiDiemCls();
                //            o.MaTD = int.Parse(r["MaTD"].ToString());
                //            o.TenTD = r["TenTD"].ToString();
                //            o.STT = 0;
                //            o.Update();
                //        }
                //    }
                //}

                //DialogBox.Alert("Dữ liệu đã được lưu.");
                //tblTemp.Dispose();
                gv.PostEditor();
                //var command = _db.Connection.CreateCommand();
                //command.CommandText = "SET IDENTITY_INSERT LichHen_ThoiDiem ON";
                //command.ExecuteNonQuery();
                //_db.ExecuteCommand("SET IDENTITY_INSERT LichHen_ThoiDiem ON");
                _db.SubmitChanges();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi khi lưu dữ liệu: " + ex);
            }

            //this.Close();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.SetFocusedRowCellValue(colMaTT, 0);
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenTD' is constrained to be unique") >= 0)
                XtraMessageBox.Show("<Tên thời điểm> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (e.ErrorText == "Column 'TenTD' does not allow nulls")
                XtraMessageBox.Show("<Tên thời điểm> không được để trống. Vui lòng nhập tên thời điểm, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gv.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Alert("Vui lòng nhập <Tên thời điểm>, xin cảm ơn.");
                    e.Allow = false;
                    gv.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }
    }
}