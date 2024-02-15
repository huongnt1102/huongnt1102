using System;

using System.Windows.Forms;
using DevExpress.XtraEditors;

using Library;


namespace TaiSan.Import
{
    public partial class frmImportTaiSan : XtraForm
    {

        public byte? maTN { get; set; }
        
        public frmImportTaiSan()
        {
            InitializeComponent();

        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

        }


        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "(Excel file)|*.xls;*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);


                    
                }
                catch(Exception ex)
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }

                wait.Close();
                wait.Dispose();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void grvMatBang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTaiSan.DeleteSelectedRows();
        }
    }

    
}