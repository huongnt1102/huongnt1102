using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.Gas
{
    public partial class frmDongHo : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();

        public frmDongHo()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                gcDongHo.DataSource = db.dvGasDongHos.Where(p => p.MaTN == _MaTN);
            }
            catch
            {
                gcDongHo.DataSource = null;
            }
        }

        void Delete()
        {
            var collection = gvDongHo.GetSelectedRows();
            if (collection.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            gvDongHo.DeleteSelectedRows();
        }

        void ImportRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new frmImportDongHo())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                {
                    this.LoadData();
                }
            }
        }

        private void frmDongHo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            
            gvDongHo.InvalidRowException += Library.Common.InvalidRowException;
            gcDongHo.KeyUp += Common.GridViewKeyUp;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDongHo.AddNewRow();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                glkMatBang.DataSource = (from mb in db.mbMatBangs
                                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                         where mb.MaTN == _MaTN
                                         select new { mb.MaMB, mb.MaSoMB, TenKH = kh.IsCaNhan.GetValueOrDefault() ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen })
                                        .ToList();
                glkDongHo.DataSource = (from dh in db.dvGasDongHos
                                        join mb in db.mbMatBangs on dh.MaMB equals mb.MaMB
                                        where dh.MaTN == _MaTN
                                        select new { dh.ID, dh.SoDH, mb.MaSoMB })
                                        .ToList();
            }
            catch { }

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvDongHo.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void gvDongHo_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvDongHo.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void gvDongHo_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var _SoDH = (gvDongHo.GetRowCellValue(e.RowHandle, "SoDH") ?? "").ToString();
            if (_SoDH.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập số đồng hồ!";
                return;
            }
            else if (Common.Duplication(gvDongHo, e.RowHandle, "SoDH", _SoDH))
            {
                e.Valid = false;
                e.ErrorText = "Số đồng hồ đã tồn tại, vui lòng nhập lại !";
                return;
            }
        }

        private void glkDongHo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gvDongHo.SetFocusedRowCellValue("ParentID", null);
            }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }
    }
}