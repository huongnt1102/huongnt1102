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
    public partial class frmCauHinhLX : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmCauHinhLX()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue; 
            gcLX.DataSource = db.ptXEs.Where(o => o.MaTN == _MaTN);
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
                    var del = db.ptXEs.Single(p => p.id == (int)grvLX.GetFocusedRowCellValue("id"));
                    db.ptXEs.DeleteOnSubmit(del);
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
                var del = db.ptXEs.Single(p => p.id == (int)grvLX.GetFocusedRowCellValue("id"));
                db.ptXEs.DeleteOnSubmit(del);
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

            var _Socho = grvLX.GetFocusedRowCellValue("SoCho");
            if (_Socho == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Số chỗ ngồi]");
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

      
    }
}