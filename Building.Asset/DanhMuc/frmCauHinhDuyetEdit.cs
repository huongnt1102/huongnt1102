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
    public partial class frmCauHinhDuyetEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public byte? MaTN { set; get; }
        public int? FormDuyetID { set; get; }
        public bool IsSave = false;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmCauHinhDuyetEdit()
        {
            InitializeComponent();
        }

        private void frmCauHinhDuyetEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            lkFormDuyet.Properties.DataSource = db.tbl_FromDuyets.OrderBy(p => p.FormName).ToList();
            glkChucVu.DataSource = db.tnChucVus.OrderBy(p => p.TenCV).ToList();
            lkFormDuyet.EditValue = FormDuyetID;

            itemHuy.Click += ItemHuy_Click;
            itemLuu.Click += ItemLuu_Click;
            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemLuu_Click(object sender, EventArgs e)
        {
            spinThuTuDuyet.Focus();
            gvCapDuyet.RefreshData();
            if (gvCapDuyet.RowCount <= 1)
            {
                DialogBox.Alert("Vui lòng chọn chức vụ cần duyệt trước khi lưu");
                return;
            }
            for (int i = 0; i < gvCapDuyet.RowCount; i++)
            {
                if (gvCapDuyet.GetRowCellValue(i, "ChucVuID") != null)
                {
                    var objKT = db.tbl_FromDuyet_ChucVus.FirstOrDefault(p => p.MaTN == MaTN && p.FormDuyetID == FormDuyetID && p.ChucVuID == (int?)gvCapDuyet.GetRowCellValue(i, "ChucVuID"));
                    if (objKT == null)
                    {
                        var objDuyet = new tbl_FromDuyet_ChucVu();
                        objDuyet.MaTN = MaTN;
                        objDuyet.FormDuyetID = FormDuyetID;
                        objDuyet.ChucVuID = (int?)gvCapDuyet.GetRowCellValue(i, "ChucVuID");
                        objDuyet.NhanVienID = (int?)gvCapDuyet.GetRowCellValue(i, "NhanVienID");
                        objDuyet.STT = (int?)gvCapDuyet.GetRowCellValue(i, "STT");
                        objDuyet.IsDuyet = (bool?)gvCapDuyet.GetRowCellValue(i, "IsDuyet");
                        db.tbl_FromDuyet_ChucVus.InsertOnSubmit(objDuyet);
                        db.SubmitChanges();
                        IsSave = true;
                    }
                    else
                    {
                        var objKTSua = db.tbl_FromDuyet_ChucVus.Where(p => p.MaTN == MaTN && p.FormDuyetID == FormDuyetID && p.ChucVuID == (int?)gvCapDuyet.GetRowCellValue(i, "ChucVuID")).ToList();
                        foreach (var items in objKTSua)
                        {
                            var objS = db.tbl_FromDuyet_ChucVus.Single(p => p.ID == items.ID);
                            objS.MaTN = MaTN;
                            objS.FormDuyetID = FormDuyetID;
                            objS.ChucVuID = (int?)gvCapDuyet.GetRowCellValue(i, "ChucVuID");
                            objS.NhanVienID = (int?)gvCapDuyet.GetRowCellValue(i, "NhanVienID");
                            objS.STT = (int?)gvCapDuyet.GetRowCellValue(i, "STT");
                            objS.IsDuyet = (bool?)gvCapDuyet.GetRowCellValue(i, "IsDuyet");
                            db.SubmitChanges();
                            IsSave = true;
                        }
                    }

                }
            }
            this.Close();
        }

        private void ItemHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvCapDuyet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCapDuyet.GetFocusedRowCellValue("ChucVuID") != null)
            {
                var objNhanVien = (from nv in db.tnNhanViens
                                   join pq in db.tnToaNhaNguoiDungs on nv.MaNV equals pq.MaNV
                                   where pq.MaTN == (byte?)MaTN && nv.MaCV == (int?)gvCapDuyet.GetFocusedRowCellValue("ChucVuID")
                                   select new
                                   {
                                       nv.MaNV,
                                       nv.MaSoNV,
                                       nv.HoTenNV,
                                   }).ToList();
                glkNhanVien.Properties.DataSource = objNhanVien.Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV }).Distinct().ToList();
                //var objNVDuyet = db.tbl_FromDuyet_ChucVus.Where(p => p.MaTN == (byte?)MaTN && p.FormDuyetID == (int?)lkFormDuyet.EditValue && p.ChucVuID == (int?)gvCapDuyet.GetFocusedRowCellValue("ChucVuID")).ToList();
                //if (objNVDuyet.Count > 0)
                //{
                //    glkNhanVien.EditValue = objNVDuyet.OrderBy(p => p.STT).FirstOrDefault().NhanVienID;
                //    spinThuTuDuyet.EditValue = objNVDuyet.FirstOrDefault().STT;
                //    if (objNVDuyet.FirstOrDefault().IsDuyet != null)
                //        IsDuyetCuoi.Checked = (bool)objNVDuyet.FirstOrDefault().IsDuyet;
                //}
                //else
                //{
                //    glkNhanVien.EditValue = null;
                //}
                glkNhanVien.EditValue = (int?)gvCapDuyet.GetFocusedRowCellValue("NhanVienID");
                spinThuTuDuyet.EditValue = (int?)gvCapDuyet.GetFocusedRowCellValue("STT");
                IsDuyetCuoi.EditValue = gvCapDuyet.GetFocusedRowCellValue("IsDuyet") == null ? false : (bool?)gvCapDuyet.GetFocusedRowCellValue("IsDuyet");
            }
            else
            {
                glkNhanVien.Properties.DataSource = null;
                glkNhanVien.EditValue = null;
            }

        }

        private void lkFormDuyet_EditValueChanged(object sender, EventArgs e)
        {
            var _value = (LookUpEdit)sender;
            if (_value.EditValue != null)
            {
                var objCapDuyet = db.tbl_FromDuyet_ChucVus.Where(p => p.FormDuyetID == FormDuyetID && p.MaTN==MaTN);
                var objTemp = objCapDuyet.Select(p => new { p.ChucVuID,p.STT,p.NhanVienID,p.IsDuyet }).Distinct();
                gcCapDuyet.DataSource = objTemp.Select(p => new ChucVuClass { ChucVuID = p.ChucVuID,NhanVienID=p.NhanVienID,STT=p.STT, IsDuyet=p.IsDuyet });
            }
        }
        public class ChucVuClass
        {
            public int? ChucVuID { set; get; }
            public int? STT { set; get; }
            public int? NhanVienID { set; get; }
            public bool? IsDuyet { set; get; }
        }

        private void gvCapDuyet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //Xóa 
               var objKT = db.tbl_FromDuyet_ChucVus.Where(p => p.MaTN == MaTN && p.FormDuyetID == FormDuyetID && p.ChucVuID == (int?)gvCapDuyet.GetFocusedRowCellValue("ChucVuID"));
               if (objKT.Count() > 0)
               {
                   db.tbl_FromDuyet_ChucVus.DeleteAllOnSubmit(objKT);
                   db.SubmitChanges();
               }
                gvCapDuyet.DeleteSelectedRows();
            }
        }

        private void spinThuTuDuyet_EditValueChanged(object sender, EventArgs e)
        {
            if (gvCapDuyet.GetFocusedRowCellValue("ChucVuID") != null)
            {
                gvCapDuyet.SetFocusedRowCellValue("STT", Convert.ToInt32(spinThuTuDuyet.EditValue));
            }
        }

        private void IsDuyetCuoi_EditValueChanged(object sender, EventArgs e)
        {
            if (gvCapDuyet.GetFocusedRowCellValue("ChucVuID") != null)
            {
                gvCapDuyet.SetFocusedRowCellValue("IsDuyet", IsDuyetCuoi.Checked);
            }
        }

        private void glkNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            var _value = (GridLookUpEdit)sender;
            if (gvCapDuyet.GetFocusedRowCellValue("ChucVuID") != null)
            {
                gvCapDuyet.SetFocusedRowCellValue("NhanVienID", (int?)_value.EditValue);
            }
        }
    }
}