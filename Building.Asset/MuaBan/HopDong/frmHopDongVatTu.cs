using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;


namespace TaiSan.XuatKho
{
    public partial class frmHopDongVatTu : XtraForm
    {

        public frmHopDongVatTu()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmHopDongVatTu_Load(object sender, EventArgs e)
        {
            

            LoadData();

        }
        private void slookKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button == slookKhachHang.Properties.Buttons[1])
            {
                using (KyThuat.KhachHang.frmEdit frm = new KyThuat.KhachHang.frmEdit() { })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        grvKhachHang.RefreshData();
                        if (radiogroupLoaiHD.SelectedIndex == 0) //cá nhân
                        {
                            NapDuLieuKhachHang(true);
                        }
                        else//công ty
                        {
                            NapDuLieuKhachHang(false);
                        }
                    }
                }
            }
        }

        private void slookKhachHang_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void slookVatTu_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void grvKhachHang_RowClick(object sender, RowClickEventArgs e)
        {
            setValue_ThongTinKhachHang();
        }

        private void grvKhachHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                setValue_ThongTinKhachHang();
            }
        }

        private void grvChonVatTu_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            setValue_ThongTinVatTu(e.RowHandle);
            grvVatTu.PostEditor();
        }

        private void grvVatTu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grvVatTu.PostEditor();
            try
            {

            }
            catch (Exception ex)
            {

            }
        }


        private void LoadData()
        {
            NapduLieuVatTu();

        }


        private void NapDuLieuKhachHang(bool isCaNhan)
        {
            

        }

        private void NapduLieuVatTu()
        {
           
        }

        private void setValue_ThongTinVatTu(int i)
        {
           
        }

        private void setValue_ThongTinKhachHang()
        {
            
        }


        private void radiogroupLoaiHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLayout();
        }

        private void LoadLayout()
        {
            if (radiogroupLoaiHD.SelectedIndex == 0) //cá nhân
            {
                groupThongTinKhachHang.Height = 238; //222
                groupCongTy.Visible = false;
                groupCaNhan.Visible = true;
                groupCaNhan.Location = new Point(12, 53);
                groupCanHo.Location = new Point(12, 125); //117
                groupPhaDo.Location = new Point(12, 283); /// 266
                groupPhaDo.Height = 110;


                labelDonViThiCong.Location = new Point(19, 180);
                glkDonViThiCong.Location = new Point(130, 177); //(110, 177);

                lblTenKhachHang.Text = "2. Tên khách hàng";
                NapDuLieuKhachHang(true);


            }
            else//công ty
            {
                groupThongTinKhachHang.Height = 238;
                groupCaNhan.Visible = false;
                groupCongTy.Visible = true;
                groupCongTy.Location = new Point(12, 53);
                groupCanHo.Location = new Point(12, 165); // 173//160
                groupPhaDo.Location = new Point(12, 283); // 266
                groupPhaDo.Height = 110;

                labelDonViThiCong.Location = new Point(19, 218);
                glkDonViThiCong.Location = new Point(130, 214); //(110, 214);

                lblTenKhachHang.Text = "2. Tên công ty";
                NapDuLieuKhachHang(false);
            }
        }

        private void repCongViec_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new frmCongViecPhaDoVanChuyen();
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {

                    }
                    break;
            }
        }

        private void repDonViTinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new frmdvt();
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {

                    }
                    break;
            }
        }

        private void gvPhaDoVanChuyen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                gvPhaDoVanChuyen.DeleteSelectedRows();
            }
        }




        private void repCongViec_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void repKhoiLuongTamTinh_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void repDonGia_EditValueChanged(object sender, EventArgs e)
        {
            
        }


        private void gridlkCanHo_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {

        }

        private void gvPhaDoVanChuyen_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void repDonViTinhVatTu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new frmdvt();
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {

                    }
                    break;
            }
        }

        private void slookVatTu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new frmLoaiTaiSan();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                    }
                    break;
            }
        }

        private void grvVatTu_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }


        private void repDonGiaVatTu_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvVatTu_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void repKhoiLuongThucTinh_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvVatTu_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            using (var frm = new frmChonDonViTinhVatTu())
            {

                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    

                }
            }



        }

        private void glkNguoiDaiDien_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void gvPhaDoVanChuyen_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName)
                {
                    case "TuongChiTiet":
                        gvPhaDoVanChuyen.Columns["KhoiLuongTamTinh"].OptionsColumn.AllowEdit = Convert.ToBoolean(e.Value) == false;
                        gvPhaDoVanChuyen.Columns["KhoiLuongThucTinh"].OptionsColumn.AllowEdit = Convert.ToBoolean(e.Value) == false;
                        break;
                }

            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void gvPhaDoVanChuyen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                gvPhaDoVanChuyen.Columns["KhoiLuongTamTinh"].OptionsColumn.AllowEdit = (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("TuongChiTiet")).GetValueOrDefault() == false) && (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("isTuongChiTiet")).GetValueOrDefault() == false);
                gvPhaDoVanChuyen.Columns["KhoiLuongThucTinh"].OptionsColumn.AllowEdit = (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("TuongChiTiet")).GetValueOrDefault() == false) && (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("isTuongChiTiet")).GetValueOrDefault() == false);
            }
            catch (Exception)
            {
                gvPhaDoVanChuyen.Columns["KhoiLuongTamTinh"].OptionsColumn.AllowEdit = true;
                gvPhaDoVanChuyen.Columns["KhoiLuongThucTinh"].OptionsColumn.AllowEdit = true;
            }
        }

        private void gvPhaDoVanChuyen_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "KhoiLuongTamTinh" | e.Column.FieldName == "KhoiLuongThucTinh")
            {
                gvPhaDoVanChuyen.Columns["KhoiLuongTamTinh"].OptionsColumn.AllowEdit = (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("TuongChiTiet")).GetValueOrDefault() == false) && (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("isTuongChiTiet")).GetValueOrDefault() == false);
                gvPhaDoVanChuyen.Columns["KhoiLuongThucTinh"].OptionsColumn.AllowEdit = (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("TuongChiTiet")).GetValueOrDefault() == false) && (((bool?)gvPhaDoVanChuyen.GetFocusedRowCellValue("isTuongChiTiet")).GetValueOrDefault() == false);
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(gvPhaDoVanChuyen.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gvPhaDoVanChuyen.DeleteSelectedRows();
                }
                else
                {
                    if (!string.IsNullOrEmpty(grvVatTu.GetFocusedDataSourceRowIndex().ToString()))
                    {
                        grvVatTu.DeleteSelectedRows();
                    }
                }

            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void glkMatBang_EditValueChanged_1(object sender, EventArgs e)
        {
            

        }

        private void repChkTronGoi_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void repChkTuongChiTiet_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if(item!=null)
            {
                gvPhaDoVanChuyen.SetFocusedRowCellValue("TuongChiTiet", (bool)item.EditValue);
            }
        }
    }
}