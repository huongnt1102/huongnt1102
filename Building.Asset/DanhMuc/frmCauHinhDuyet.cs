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
    public partial class frmCauHinhDuyet : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmCauHinhDuyet()
        {
            InitializeComponent();
        }

        private void frmCauHinhDuyet_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            gcFormDuyet.DataSource = db.tbl_FromDuyets.OrderBy(p => p.ID);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            //glkChucVu.DataSource = db.tnChucVus.Where(p => p.MaTN == (byte?)itemToaNha.EditValue);
            glkChucVu.DataSource = db.tnChucVus;
            glkHeThong.DataSource = db.tbl_NhomTaiSans.Where(_ => _.MaTN == (byte?) itemToaNha.EditValue);
            itemHeThong.EditValue = glkHeThong.GetKeyValue(0);
        }

        private void gvFormDuyet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Alert("Lưu thành công!");
        }

        private void gvCapDuyet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                gvCapDuyet.DeleteSelectedRows();
            }
        }

        private void glkChucVu_EditValueChanged(object sender, EventArgs e)
        {
            var _value=(GridLookUpEdit)sender;
            gvCapDuyet.SetFocusedRowCellValue("FormDuyetID", Convert.ToInt32(gvFormDuyet.GetFocusedRowCellValue("ID")));
            gvCapDuyet.SetFocusedRowCellValue("MaTN", (byte?)itemToaNha.EditValue);
            gvCapDuyet.SetFocusedRowCellValue("STT", 1);
            gvCapDuyet.SetFocusedRowCellValue("ChucVuID", (int?)_value.EditValue);
            gvCapDuyet.SetFocusedRowCellValue("HeThongTaiSanID", (int) itemHeThong.EditValue);

            spinThuTuDuyet.EditValue = 1;
            IsDuyetCuoi.Checked = false;

            glkNhanVien.Properties.DataSource = db.tnNhanViens.Where(_ => _.MaTN == (byte?)itemToaNha.EditValue & _.MaCV == (int?)_value.EditValue).ToList();
            //glkNhanVien.Properties.DataSource = db.tnNhanViens.Where(_ => _.MaCV == (int?)_value.EditValue).ToList();
        }

        private void gvCapDuyet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvCapDuyet.SetFocusedRowCellValue("HeThongTaiSanID", (int)itemHeThong.EditValue);
        }

        private void spinThuTuDuyet_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as SpinEdit;
            if (item != null) gvCapDuyet.SetFocusedRowCellValue("HeThongTaiSanID", (int) item.Value);
        }

        private void IsDuyetCuoi_CheckedChanged(object sender, EventArgs e)
        {
            gvCapDuyet.SetFocusedRowCellValue("IsDuyet", (bool?)IsDuyetCuoi.Checked);
        }

        private void gvCapDuyet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            db.SubmitChanges();
            if (gvCapDuyet.GetFocusedRowCellValue("ID") != null)
            {
                var r = db.tbl_FromDuyet_ChucVus.FirstOrDefault(_ => _.ID == (int?)gvCapDuyet.GetFocusedRowCellValue("ID"));
                if (r != null)
                {
                    spinThuTuDuyet.EditValue = r.STT;
                    //glkNhanVien.Properties.DataSource = db.tnNhanViens.Where(_ => _.MaTN == (byte?)itemToaNha.EditValue & _.MaCV == r.ChucVuID).ToList();
                    glkNhanVien.Properties.DataSource = db.tnNhanViens.Where(_ => _.MaCV == r.ChucVuID).ToList();
                    if (r.NhanVienID != null) glkNhanVien.EditValue = r.NhanVienID;
                    if (r.IsDuyet != null) IsDuyetCuoi.Checked = (bool) r.IsDuyet;
                }
            }
        }

        private void glkNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item != null) gvCapDuyet.SetFocusedRowCellValue("NhanVienID", (int?)item.EditValue);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            if (gvFormDuyet.GetFocusedRowCellValue("ID") != null)
            {
                gcCapDuyet.DataSource = db.tbl_FromDuyet_ChucVus.Where(p => p.FormDuyetID == (int?)gvFormDuyet.GetFocusedRowCellValue("ID") && p.MaTN == (byte?)itemToaNha.EditValue & p.HeThongTaiSanID == (int)itemHeThong.EditValue);

                if (gvCapDuyet.GetFocusedRowCellValue("ID") != null)
                {
                    var r = db.tbl_FromDuyet_ChucVus.FirstOrDefault(_ => _.ID == (int?)gvCapDuyet.GetFocusedRowCellValue("ID"));
                    if (r != null)
                    {
                        spinThuTuDuyet.EditValue = r.STT;
                        glkNhanVien.Properties.DataSource = db.tnNhanViens.Where(_ => _.MaTN == (byte?)itemToaNha.EditValue & _.MaCV == r.ChucVuID).ToList();
                        //glkNhanVien.Properties.DataSource = db.tnNhanViens.Where(_ => _.MaCV == r.ChucVuID).ToList();
                        if (r.NhanVienID != null) glkNhanVien.EditValue = r.NhanVienID;
                        else
                            glkNhanVien.EditValue = null;
                        if (r.IsDuyet != null) IsDuyetCuoi.Checked = (bool)r.IsDuyet;
                        else
                            IsDuyetCuoi.Checked = false;
                    }
                }
                else
                {
                    spinThuTuDuyet.EditValue = 1;
                    glkNhanVien.Properties.DataSource = null;
                    IsDuyetCuoi.Checked = false;
                }
            }
            else
            {
                gcCapDuyet.DataSource = null;
                spinThuTuDuyet.EditValue = 1;
                glkNhanVien.Properties.DataSource = null;
                IsDuyetCuoi.Checked = false;
            }
        }

        private void itemHeThong_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}