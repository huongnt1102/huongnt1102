using System;
using System.Windows.Forms;
using Library;
using System.Linq;


namespace DichVu.YeuCau
{
    public partial class frmNguonDen : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        private void LoadData()
        {
            gcControl.DataSource = db.tnycNguonDens.OrderBy(p => p.STT);
        }

        public frmNguonDen()
        {
            InitializeComponent();
        }

        private void frmNguonDen_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemRefesh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvView.AddNewRow();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvView.RefreshData();

                db.SubmitChanges();

                DialogBox.Success();

                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvView.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nguồn tiếp nhận]. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            try
            {
                foreach (var i in rows)
                {
                    var obj = db.tnycNguonDens.Single(p => p.ID == (int?)grvView.GetRowCellValue(i, "ID"));
                    db.tnycNguonDens.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();

                LoadData();

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void grvView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvView.SetFocusedRowCellValue("STT", grvView.RowCount);
        }
    }
}
