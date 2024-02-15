using System;

namespace BuildingDesignTemplate
{
    public partial class FrmShowHaveBuilding : DevExpress.XtraEditors.XtraForm
    {
        public string RtfText { get; set; }
        public int? FormTemplateId { get; set; }
        public int PaddingTop { get; set; }
        public int PaddingBottom { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }

        public DevExpress.XtraRichEdit.RichEditControl RichControl { get; set; }

        public FrmShowHaveBuilding()
        {
            InitializeComponent();

            itemLuu.ItemClick += itemLuu_ItemClick;
            itemDong.ItemClick += itemDong_ItemClick;
            Load += frmDesign_Load;
        }

        private void frmDesign_Load(object sender, EventArgs e)
        {
            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;
            itemNam.EditValue = System.DateTime.Now.Year;
            itemThang.EditValue = System.DateTime.Now.Month;
            LoadData();
        }

        private void LoadData()
        {
            var nam = int.Parse(itemNam.EditValue.ToString());
            var thang = int.Parse(itemThang.EditValue.ToString());
            if (FormTemplateId == null) { RtfText = ""; }
            
            RtfText = BuildingDesignTemplate.Class.MergeField.GetContentReport(FormTemplateId, (byte)itemBuilding.EditValue, nam, thang);

            txtContent.RtfText = RtfText;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void ItemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}