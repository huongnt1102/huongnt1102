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

namespace DichVu.BanGiaoMatBang.Xe
{
    public partial class frmCauHinhLichDC : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }

        MasterDataContext db;

        public frmCauHinhLichDC()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue; 
            gcLX.DataSource = db.ptDCXEs.Where(o => o.MaTN == _MaTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            grlkLoaiXe.DataSource = db.ptLoaiXes;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        }

        private void grvLoaiMB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.ptDCXEs.Single(p => p.id == (int)grvLX.GetFocusedRowCellValue("id"));
                    db.ptDCXEs.DeleteOnSubmit(del);
                    db.SubmitChanges();
                    grvLX.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                    return;
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.ptDCXEs.Single(p => p.id == (int)grvLX.GetFocusedRowCellValue("id"));
                db.ptDCXEs.DeleteOnSubmit(del);
                db.SubmitChanges();
                grvLX.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                return;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLX.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvLoaiMB_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvLX.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void grvTinTuc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var _Xe = grvLX.GetFocusedRowCellValue("Xe");
            if (_Xe == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Xe]");
                return;
            }

            var _DiTu = grvLX.GetFocusedRowCellValue("DiTu");
            if (_DiTu == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Đi từ]");
                return;
            }
            var _DiDen = grvLX.GetFocusedRowCellValue("DiDen");
            if (_DiDen == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Đi đến]");
                return;
            }
           
        }

        private void grlkLoaiTinTuc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn [Loại xe]");
                    return;
                }
                var TenLoai = item.Properties.View.GetFocusedRowCellValue("TenLoaiXe").ToString();
                //var TenLoaiTT = db.ttLoaiTinTucs.Single(O => O.MaLoaiTinTuc == (int)item.EditValue).TenLoaiTinTuc;
                grvLX.SetFocusedRowCellValue("TenLoaiXe", TenLoai);
            }
            catch { }
        }

        private void itemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CreatePhanQuyen();
        }

    }
}