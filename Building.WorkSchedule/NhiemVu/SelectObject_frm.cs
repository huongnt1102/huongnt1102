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

namespace Building.WorkSchedule.NhiemVu
{
    public partial class SelectObject_frm : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public int MaNVu = 0;
        MasterDataContext db = new MasterDataContext();
        private DataTable dtNVChon = new DataTable();
        public SelectObject_frm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectObject_frm_Load(object sender, EventArgs e)
        {
            dtNVChon.Columns.Add("MaNV", typeof(int));
            dtNVChon.Columns.Add("HoTen", typeof(string));
            dtNVChon.Columns.Add("NguoiGiao", typeof(string));
            gridControl2.DataSource = dtNVChon;
            LoadNhanVien();
        }

        void LoadRecive()
        {
            Library.NhiemVu_NhanVienCls o = new Library.NhiemVu_NhanVienCls();
            o.MaNVu = MaNVu;
            o.MaNV = objNV.MaNV;
            DataTable tblTemp = new DataTable();
            gridControl2.DataSource = tblTemp;
            tblTemp.Dispose();
        }

        void LoadNhanVien()
        {
            try
            {
                Library.NhanVienCls o = new Library.NhanVienCls();
            //switch (Library.Commoncls.GetAccessData(Common.PerID, 15))
            //{
            //    case 1://Tat ca         
                using (var db = new MasterDataContext())
                {
                    gridControl1.DataSource = db.tnNhanViens.Select(p => new
                    {
                        HoTen = p.HoTenNV,
                        MaSo = p.MaSoNV,
                        p.MaNV
                    });
                }
            //        break;
            //    case 2://Theo phong ban
            //        o.PhongBan.MaPB = Common.MaPB;
            //        gridControl1.DataSource = o.SelectObjectByDepartment();
            //        break;
            //    case 3://Theo nhom
            //        o.NKD.MaNKD = Common.MaNKD;
            //        gridControl1.DataSource = o.SelectObjectByGroup();
            //        break;
            //    case 4://Theo nhan vien
            //        o.MaNV = objNV.MaNV;
            //        gridControl1.DataSource = o.SelectObjectBy();
            //        break;
            //    default:
            //        gridControl1.DataSource = null;
            //        break;
            //}

            gridView1.ExpandAllGroups();

            LoadRecive();            
            }
            catch { }
        }

        private void btnRemoveOne_Click(object sender, EventArgs e)
        {            
            if (DialogBox.QuestionDelete() == DialogResult.Yes)
            {
                int[] Rows = gridView2.GetSelectedRows();
                //string NotDelete = "";
                if (Rows.Length > 0)
                {
                    Library.NhiemVu_NhanVienCls o;
                    foreach (int i in Rows)
                    {
                        //if (Common.HoTen == gridView2.GetRowCellValue(i, colNguoiGiao).ToString().Trim())
                        //{
                            o = new Library.NhiemVu_NhanVienCls();
                            o.MaNVu = MaNVu;
                            try
                            {
                                o.MaNV = int.Parse(gridView2.GetRowCellValue(i, colMaNV).ToString());
                                o.Delete();
                                gridView2.DeleteRow(i);
                            }
                            catch
                            {
                                DialogBox.Alert("Xóa không thành công vì: <Người nhận> đã có phát sinh <Lịch hẹn>\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                return;
                            }
                        //}
                        //else
                        //    NotDelete += gridView2.GetRowCellValue(i, colHoTen2).ToString() + "\n";
                    }

                    //if (NotDelete != "")
                    //    DialogBox.Alert(string.Format("Danh sách xóa không thành công:\n{0}", NotDelete));
                }
                else
                    DialogBox.Alert("Vui lòng chọn <Người nhận> muốn xóa. Xin cảm ơn");
            }            
        }

        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            int[] Rows = gridView1.GetSelectedRows();
            foreach (int i in Rows)
            {
                try
                {
                    if (gridView1.GetRowCellValue(i, colMaNV) != null)
                    {
                        gridView2.AddNewRow();
                        gridView2.SetFocusedRowCellValue(colMaNV2, gridView1.GetRowCellValue(i, colMaNV));
                        gridView2.SetFocusedRowCellValue(colHoTen2, gridView1.GetRowCellValue(i, colHoTen));
                        gridView2.SetFocusedRowCellValue(colNguoiGiao, Common.User.HoTenNV);
                    }
                }
                catch { }
            }
            gridView2.FocusedColumn = colMaNV2;
            gridView2.FocusedRowHandle = 0;
        }

        private void gridView2_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (e.Exception.Message.IndexOf("Column 'MaNV' is constrained to be unique.") >= 0)
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount <= 0)
            {
                DialogBox.Alert("Vui lòng chọn người nhận nhiệm vụ, xin cảm ơn.");
                return;
            }

            Library.NhiemVu_NhanVienCls o;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                NhiemVu_tnNhanVien obj = new NhiemVu_tnNhanVien();
                obj.MaNVu = MaNVu;
                obj.NgayGiao = DateTime.Now;
                obj.MaNV = int.Parse(gridView2.GetRowCellValue(i, colMaNV2).ToString());
                obj.NguoiGiao = Common.User.MaNV;
                obj.DaNhac = false;
                db.NhiemVu_tnNhanViens.InsertOnSubmit(obj);
                db.SubmitChanges();
            }

            DialogBox.Alert("Dữ liệu đã được cập nhật.");
            this.Close();
        }
    }
}