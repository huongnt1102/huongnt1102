using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace ToaNha.NhacViec_ThongBao
{
    public partial class frmNhacViecEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public tnNhacViec objnhacviec;
        MasterDataContext db;

        public frmNhacViecEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmNhacViecEdit_Load(object sender, EventArgs e)
        {
            lookNguoiNhan.DataSource = db.tnNhanViens;
            if (objnhacviec != null)
            {
                objnhacviec = db.tnNhacViecs.Single(p => p.MaNhacViec == objnhacviec.MaNhacViec);
                txtNoiDung.Text = objnhacviec.NoiDung;
            }
            else
            {
                objnhacviec = new tnNhacViec();
                objnhacviec.NguoiGui = objnhanvien.MaNV;
                db.tnNhacViecs.InsertOnSubmit(objnhacviec);
            }
            gcNguoiNhan.DataSource = objnhacviec.tnNhacViec_Details;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            objnhacviec.NoiDung = txtNoiDung.Text.Trim();
            objnhacviec.NgayGui = db.GetSystemDate();
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu dữ liệu thành công");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch 
            {
                DialogBox.Alert("Nội dung vượt quá số kí tự cho phép (500 kí tự)");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvNguoiNhan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvNguoiNhan.SetFocusedRowCellValue(colDaDoc,false);
        }

    }
}