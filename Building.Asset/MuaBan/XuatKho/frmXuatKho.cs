using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace TaiSan.XuatKho
{
    public partial class frmXuatKho : XtraForm
    {


        public frmXuatKho()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);
            TranslateLanguage.TranslateControl(this, barManager1);
        }


        private void frmXuatKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void slookBanHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

            }

        }



        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void lookKho_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void grvBanHang_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                
            }
        }

        private void grvBanHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {

                }
            }
            catch (Exception)
            {
                
            }
        }

        private void grvVatTuBan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }


        private void LoadData()
        {
            
        }


        private void spinSoLuongXuatKho_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (grvVatTu.GetFocusedRowCellValue("ThanhTien") != null)
                {
                    var slXuat = ((SpinEdit)sender).Value;
                    var dg = Convert.ToDecimal(grvVatTu.GetFocusedRowCellValue("DonGia"));
                    grvVatTu.SetFocusedRowCellValue("ThanhTien", slXuat * dg);
                }
            }
            catch { }
        }
    }
}