using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.Services
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }

        public frmManager()
        {
            InitializeComponent();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.Id = (int)gvDepartment.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDepartment.FocusedRowHandle >= 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    var objCheck = db.app_TowerSettingServices.FirstOrDefault(p=>p.ServiceId == (int)gvDepartment.GetFocusedRowCellValue("Id"));
                    if (objCheck == null)
                    {
                        var obj = db.app_SettingServices.FirstOrDefault(p => p.Id == (int)gvDepartment.GetFocusedRowCellValue("Id"));
                        try
                        {
                            db.app_SettingServices.DeleteOnSubmit(obj);
                            db.SubmitChanges();

                            gvDepartment.DeleteSelectedRows();
                        }
                        catch { }
                    }else
                    {
                        DialogBox.Warning("[Dịch vụ] này đã được sử dụng.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dữ liệu.");
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                using (var db = new MasterDataContext())
                {
                    var list = (from p in db.app_SettingServices
                                               join dv in db.dvLoaiDichVus on p.Id equals dv.ID into loaiDichVu from dv in loaiDichVu.DefaultIfEmpty()
                                               join gr in db.dvLoaiDichVus on dv.ParentID equals gr.ID into tempParent
                                               from gr in tempParent.DefaultIfEmpty()
                                               join u in db.DonViTinhs on p.UnitId equals u.ID into tempUnit
                                               from u in tempUnit.DefaultIfEmpty()
                                               join nv in db.tnNhanViens on p.EmployeeIdModify equals nv.MaNV into nhanVien from nv in nhanVien.DefaultIfEmpty()
                                               join type in db.app_TypeOfServices on p.TypeId equals type.Id into loai from type in loai.DefaultIfEmpty()
                                               orderby p.TypeId, p.NumberIndex
                                               select new
                                               {
                                                   p.Id,
                                                   Name = dv!=null? dv.TenHienThi:"",
                                                   Group =gr!=null? gr.TenHienThi:"",
                                                   p.Amount,
                                                   p.ContentReminder,
                                                   p.DateModify,
                                                   p.Deposit,
                                                   p.NumberIndex,
                                                   p.PreregistrationTime,
                                                   p.Price,
                                                   Type =type!=null? type.Name:"",
                                                   Unit =u!=null? u.TenDVT:"",
                                                   EmployeeName = nv.HoTenNV,
                                                   ParentId =dv!=null?( dv.ParentID ?? dv.ID):0,
                                                   IsParent =dv!=null?( dv.ParentID ?? 0):0
                                               });

                    gcDepartment.DataSource = list.OrderBy(p => p.Type).ThenBy(p => p.ParentId).ThenBy(p => p.NumberIndex);
                }
            }catch
            {
                //gcDepartment.DataSource = null;
                wait.Close();
                wait.Dispose();
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvDepartment_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (Convert.ToInt32(gvDepartment.GetRowCellValue(e.RowHandle, "IsParent")) == 0)
            {
                if (e.Column.FieldName == "Name")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }
    }
}
