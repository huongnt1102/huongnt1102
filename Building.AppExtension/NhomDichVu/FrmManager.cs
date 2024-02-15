using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building.AppExtension.NhomDichVu
{
    public partial class FrmManager : DevExpress.XtraEditors.XtraForm
    {
        public FrmManager()
        {
            InitializeComponent();
        }

        private void FrmManager_Load(object sender, EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);
            lkToaNha.DataSource = Library.Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            LoadData();
        }

        #region
        public void LoadData()
        {
            try
            {
                var maTn = (byte)itemToaNha.EditValue;
                var model = new { matn = maTn };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gridControl1.DataSource = Library.Class.Connect.QueryConnect.Query<app_nhomdichvu_get>("app_nhomdichvu_get", param);
            }
            catch { }
        }

        public void LoadDetail()
        {
            try
            {
                var id = (Guid?)gridView1.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabLoaiDichVuChiTiet":
                        var model = new { idnhom = id };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        gridControl2.DataSource = Library.Class.Connect.QueryConnect.Query<app_loaidichvu_get>("app_loaidichvu_get", param);
                        break;
                }
            }
            catch (System.Exception ex) { }
        }

        public class app_nhomdichvu_get
        {
            public Guid Id { get; set; }

            public byte? MaTN { get; set; }

            public System.DateTime? NgayNgungSuDung { get; set; }

            public System.DateTime? NgayTao { get; set; }

            public bool? IsNgungSuDung { get; set; }

            public string Ten { get; set; }

            public string NguoiNgungSuDung { get; set; }

            public string NguoiTao { get; set; }

        }

        public class app_loaidichvu_get
        {
            public Guid Id { get; set; }

            public Guid? IdNhom { get; set; }

            public System.DateTime? NgayNgungSuDung { get; set; }

            public bool? IsNgungSuDung { get; set; }

            public string Ten { get; set; }

            public string NguoiNgungSuDung { get; set; }

            public string NguoiTao { get; set; }

        }
        #endregion

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            { 
                using (var frm = new FrmEdit { MaTN = (byte)itemToaNha.EditValue, Id
                 = null})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn phiếu, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmEdit
                {
                    MaTN = (byte)itemToaNha.EditValue,
                    Id = (Guid?)gridView1.GetFocusedRowCellValue("Id")
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void itemThemLoaiDichVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn nhóm dịch vụ, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmLoaiDichVuEdit
                {
                    MaTN = (byte)itemToaNha.EditValue,
                    IdParent = (Guid?)gridView1.GetFocusedRowCellValue("Id")
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemSuaLoaiDichVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn nhóm dịch vụ, xin cảm ơn.");
                    return;
                }

                if (gridView2.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn loại dịch vụ, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmLoaiDichVuEdit
                {
                    MaTN = (byte)itemToaNha.EditValue,
                    IdParent = (Guid?)gridView1.GetFocusedRowCellValue("Id"),
                    Id = (Guid?)gridView2.GetFocusedRowCellValue("Id")
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemXoaLoaiDichVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                if (gridView2.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn loại dịch vụ, xin cảm ơn.");
                    return;
                }

                if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

                var model = new { id = (Guid?)gridView2.GetFocusedRowCellValue("Id") };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var result = Library.Class.Connect.QueryConnect.Query<bool>("app_loaidichvu_delete", param);

                LoadDetail();
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                if (gridView1.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn loại dịch vụ, xin cảm ơn.");
                    return;
                }

                if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

                var model = new { id = (Guid?)gridView1.GetFocusedRowCellValue("Id") };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var result = Library.Class.Connect.QueryConnect.Query<bool>("app_nhomdichvu_delete", param);

                LoadData();
            }
            catch (System.Exception)
            {
                //throw;
            }
        }
    }
}