using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DichVu.GiuXe
{
    public partial class frmChonTheChip: XtraForm
    {
        public string SoThe { set; get; }
        public string ID { get; set; }
        public byte MaTN { get; set; }
        public string BlockCode { get; set; }
        public APITheXe.TheChuaTaoVeThang objTicket;

        public frmChonTheChip()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var tn = db.tnToaNhas.Single(o => o.MaTN == this.MaTN);
                gcTaiSan.DataSource = APITheXe.DanhSachTheXeChuaTaoVeThang(this.MaTN, BlockCode);
            }
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
 
        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectRow();
        }

        void SelectRow()
        {
            try
            {
                if (gvTicket.FocusedRowHandle < 0)
                    return;

                objTicket = (APITheXe.TheChuaTaoVeThang)gvTicket.GetFocusedRow();
                DialogResult = DialogResult.OK;

            }
            catch (Exception)
            {
            }
        }

        private void repositoryItemSpinEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //var item = sender as SpinEdit;
            //if (item == null) return;
            //gvTaiSan.SetFocusedRowCellValue("SoLuongDuyet", item.EditValue);
        }

        private void gvTaiSan_DoubleClick(object sender, EventArgs e)
        {
            SelectRow();
        }
    }
}