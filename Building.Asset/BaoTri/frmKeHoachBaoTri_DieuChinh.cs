using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Building.Asset.BaoTri
{
    public partial class frmKeHoachBaoTri_DieuChinh : XtraForm
    {
        public DateTime? TuNgayCu { get; set; }
        public DateTime? DenNgayCu { get; set; }
        public DateTime TuNgayMoi { get; set; }
        public DateTime DenNgayMoi { get; set; }
        //public int OptChon { get; set; }
        public string TanXuat { get; set; }

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmKeHoachBaoTri_DieuChinh()
        {
            InitializeComponent();
        }

        private void frmKeHoachBaoTri_DieuChinh_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            txtTanXuat.Text = TanXuat;

            if (TuNgayCu != null)
            {
                dateTuNgayCu.DateTime = (DateTime) TuNgayCu;
                dateTuNgayMoi.DateTime = (DateTime) TuNgayCu;
            }

            if (DenNgayCu != null)
            {
                dateDenNgayCu.DateTime = (DateTime) DenNgayCu;
                dateDenNgayMoi.DateTime = (DateTime) DenNgayCu;
            }

            //rdoGChon.SelectedIndex = 3;
            dateTuNgayMoi.Focus();
        }

        private void itemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            TuNgayMoi = new DateTime(dateTuNgayMoi.DateTime.Year,dateTuNgayMoi.DateTime.Month,dateTuNgayMoi.DateTime.Day);
            DenNgayMoi = new DateTime(dateDenNgayMoi.DateTime.Year,dateDenNgayMoi.DateTime.Month,dateDenNgayMoi.DateTime.Day);
            //OptChon = rdoGChon.SelectedIndex;

            DialogResult = DialogResult.OK;
        }

        private void itemHuongDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void itemClearText_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtTanXuat.Text = TanXuat;
        }
    }
}