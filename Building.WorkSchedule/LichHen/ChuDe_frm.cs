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

namespace Building.WorkSchedule.LichHen
{
    public partial class ChuDe_frm : DevExpress.XtraEditors.XtraForm
    {
        public MasterDataContext db = new MasterDataContext();
        public ChuDe_frm()
        {
            InitializeComponent();
        }

        private void TinhTrang_frm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            gridControl1.DataSource = db.LichHen_ChuDes;
            //Library.LichHen_ChuDeCls o = new Library.LichHen_ChuDeCls();
            //DataTable tbl = o.Select();
            //tbl.Columns["TenCD"].Unique = true;
            //tbl.Columns["TenCD"].AllowDBNull = false;
            //gridControl1.DataSource = tbl;
            //tbl.Dispose();
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
            if (gridView1.GetFocusedRowCellValue("MaCD") != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn xóa <Chủ đề> này không!") == DialogResult.Yes)
                {
                    try
                    {
                        //Library.LichHen_ChuDeCls o = new Library.LichHen_ChuDeCls();
                        //o.MaCD = byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString());
                        //o.Delete();
                        //gridView1.DeleteSelectedRows();
                     
                    }
                    catch
                    {
                        DialogBox.Alert("Xóa không thành công vì: <Chủ đề> này đã được dùng.\nVui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn <Chủ đề> cần xóa, xin cảm ơn.");

        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
            //DataTable tblTemp = (DataTable)gridControl1.DataSource;
            //Library.LichHen_ChuDeCls o;
            //foreach (DataRow r in tblTemp.Rows)
            //{
            //    if (r.RowState == DataRowState.Added)
            //    {
            //        o = new Library.LichHen_ChuDeCls();
            //        o.TenCD = r["TenCD"].ToString();
            //        o.STT = 0;
            //        o.Insert();
            //    }
            //    else
            //    {
            //        if (r.RowState == DataRowState.Modified)
            //        {
            //            o = new Library.LichHen_ChuDeCls();
            //            o.MaCD = int.Parse(r["MaCD"].ToString());
            //            o.TenCD = r["TenCD"].ToString();
            //            o.STT = 0;
            //            o.Update();
            //        }
            //    }
            //}

            //XtraMessageBox.Show("Dữ liệu đã được lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //tblTemp.Dispose();
            //this.Close();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.ErrorText.IndexOf("Column 'TenCD' is constrained to be unique") >= 0)
                XtraMessageBox.Show("<Tên chủ đề> này đã bị trùng. Vui lòng nhập lại tên khác, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (e.ErrorText == "Column 'TenCD' does not allow nulls")
                XtraMessageBox.Show("<Tên chủ đề> không được để trống. Vui lòng nhập <Tên chủ đề>, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            try
            {
                if (gridView1.GetRowCellValue(e.RowHandle, colTenTT).ToString() == "")
                {
                    DialogBox.Alert("Vui lòng nhập <Tên chủ đề>, xin cảm ơn.");
                    e.Allow = false;
                    gridView1.FocusedRowHandle = e.RowHandle;
                }
            }
            catch { }
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.LichHen_ChuDes.Single(p => p.MaCD == (int)gridView1.GetFocusedRowCellValue("MaCD"));
                    db.SubmitChanges();
                    gridView1.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                    this.Close();
                }
            }
        }
    }
}