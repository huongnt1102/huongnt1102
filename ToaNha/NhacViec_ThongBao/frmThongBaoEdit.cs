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

namespace ToaNha.NhacViec_ThongBao
{
    public partial class frmThongBaoEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnThongBao objthongbao;
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        public frmThongBaoEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThongBaoEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (objthongbao!=null)
            {
                objthongbao = db.tnThongBaos.Single(p => p.MaThongBao == objthongbao.MaThongBao);
                txtTieuDe.Text = objthongbao.TieuDe;
                txtNoiDung.Text = objthongbao.NoiDung;
                dateTuNgay.EditValue = objthongbao.TuNgay;
                dateDenNgay.EditValue = objthongbao.DenNgay;
                checkHienThi.Checked = objthongbao.IsEnable.Value;
            }
            else
            {
                objthongbao = new tnThongBao();
                dateTuNgay.DateTime = db.GetSystemDate();
                dateDenNgay.DateTime = db.GetSystemDate();
                db.tnThongBaos.InsertOnSubmit(objthongbao);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            objthongbao.IsEnable = checkHienThi.Checked;
            objthongbao.TuNgay = dateTuNgay.DateTime;
            objthongbao.DenNgay = dateDenNgay.DateTime;
            objthongbao.NoiDung = txtNoiDung.Text.Trim();
            objthongbao.TieuDe = txtTieuDe.Text.Trim();
            objthongbao.NgayDang = db.GetSystemDate();
            objthongbao.MaNV = objnhanvien.MaNV;

            try
            {
                db.SubmitChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Lưu không thành công");
            }
            finally
            {
                this.Close();
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}