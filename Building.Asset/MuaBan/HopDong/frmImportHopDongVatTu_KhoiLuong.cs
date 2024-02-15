using System;

using System.Windows.Forms;

using System.Linq;
using Library;


namespace TaiSan.XuatKho
{
    public partial class frmImportHopDongVatTu_KhoiLuong : DevExpress.XtraEditors.XtraForm
    {

        public frmImportHopDongVatTu_KhoiLuong()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this,barManager1);
        }


        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            { file.Dispose(); }

        }

        

        private void LoadData()
        {

        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvKhoiLuong.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gcKhoiLuong.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(this.Tag.ToString());

                    gcKhoiLuong.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new KhoiLuongItem
                    {
                        MaHopDong = p["Mã hợp đồng"].ToString().Trim(),
                        MaPhaDoCt = p["Tên công việc phá dỡ"].ToString().Trim(),
                        MaNoiDung = p["Nội dung"].ToString().Trim(),
                        Dai = p["Dài"].Cast<decimal>(),
                        Cao = p["Cao"].Cast<decimal>(),
                        Day = p["Dày"].Cast<decimal>(),
                        Cua = p["Cửa"].Cast<decimal>(),
                        NguoiLap = p["Người lập"].ToString().Trim(),
                        Ngaylap = p["Ngày lập"].ToString().Trim()
                    }).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }
    }

    class KhoiLuongItem
    {
        public string MaHopDong { get; set; }
        public string MaPhaDoCt { get; set; }
        public string MaNoiDung { get; set; }
        public string Ngaylap { get; set; }
        public string NguoiLap { get; set; }
        public string Error { get; set; }

        public decimal Dai { get; set; }
        public decimal Cao { get; set; }
        public decimal Day { get; set; }
        public decimal Cua { get; set; }
    }
}