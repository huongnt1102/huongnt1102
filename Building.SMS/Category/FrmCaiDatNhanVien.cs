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

namespace Building.SMS.Category
{
    public partial class FrmCaiDatNhanVien : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db;

        public FrmCaiDatNhanVien()
        {
            InitializeComponent();
        }

        private void FrmCaiDatNhanVien_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;
            _db = new MasterDataContext();
            glkPhongBan.DataSource = _db.tnPhongBans;
            itemPhongBan.EditValue = Common.User.MaPB;
            glkNhanVien.DataSource = _db.tnNhanViens;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte?) itemBuilding.EditValue;
                glkNhanVien.DataSource = _db.tnNhanViens.Where(_ => _.MaPB == (int) itemPhongBan.EditValue & _.MaTN == buildingId);
                gc.DataSource = _db.SmsNhanViens.Where(_=>_.BuildingId == buildingId & _.MaPB == (int)itemPhongBan.EditValue);
            }
            catch{}
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                _db.SubmitChanges();
                DialogBox.Success();
                LoadData();
            }
            catch
            {
                DialogBox.Error("Lưu dữ liệu bị lỗi");
            }
        }

        private void itemCapNhatAllPb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // tự động import tất cả nhân viên trong phòng ban của tòa nhà, với trạng thái gửi sms  = true
            // nếu như trong tòa nhà đó đã có rồi thì thôi. ngoài ra bắt trùng nữa
            try
            {
                var db = new MasterDataContext();
                var nhanVien = db.tnNhanViens.Where(_ => _.MaPB == (int)itemPhongBan.EditValue & _.MaTN == (byte)itemBuilding.EditValue);
                foreach (var item in nhanVien)
                {
                    var sms = db.SmsNhanViens.FirstOrDefault(_ => _.BuildingId == (byte)itemBuilding.EditValue & _.MaPB == (int)itemPhongBan.EditValue & _.NhanVienId == item.MaNV);
                    if (sms == null)
                    {
                        var o = new SmsNhanVien();
                        o.IsDichVuYeuCau = true;
                        o.MaPB = item.MaPB;
                        o.NhanVienId = item.MaNV;
                        o.BuildingId = item.MaTN;
                        db.SmsNhanViens.InsertOnSubmit(o);
                    }
                }

                db.SubmitChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.ToString());
            }
            
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("BuildingId", (byte?)itemBuilding.EditValue ?? Common.User.MaTN);
            gv.SetFocusedRowCellValue("MaPB", (int?) itemPhongBan.EditValue);
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < gv.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gv.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = gv.GetRowCellValue(i, fielName).ToString();
                    if (oldValue == value) return true;
                }
            }
            return false;
        }

        private void glkNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            var ts = sender as GridLookUpEdit;

            if (ts != null && ts.EditValue == null) return;

            if (ts != null && !IsDuplication("NhanVienId", gv.FocusedRowHandle, ts.EditValue.ToString()))
            {
                gv.SetFocusedRowCellValue("NhanVienId", (int)ts.EditValue);

                gv.FocusedRowHandle = -1;
                return;
            }
            DialogBox.Error("Trùng nhân viên, vui lòng chọn nhân viên khác.");
            gv.DeleteSelectedRows();
            gv.UpdateCurrentRow();
            gv.FocusedRowHandle = -1;
        }
    }
}