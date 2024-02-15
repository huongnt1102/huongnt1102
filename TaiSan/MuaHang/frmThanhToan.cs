using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace TaiSan.MuaHang
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {         
        public msLichSuTT objLTT;
        public int? MaMH { get; set; }
        msMuaHang objMH;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        string getNewMaMH()
        {
            string MaMH = "";
            db.msMuaHang_getNewMaMH(ref MaMH);
            return db.DinhDang(19,int.Parse(MaMH));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {

            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
            }

            if (this.objLTT != null)
            {
                objLTT = db.msLichSuTTs.SingleOrDefault(p => p.MaMH == objLTT.MaMH);
                objMH = db.msMuaHangs.Single(p=>p.MaMH == objLTT.MaMH);
                txtDienGiai.Text = objLTT.GhiChu;
                txtMaSoMH.Text = objMH.MaSoMH;
                txtNguoiTT.Text = objLTT.NguoiTT;
                spinSoTien.EditValue = objLTT.SoTien;
                dateNgayTT.EditValue = objLTT.NgayTT;
                lookNhanVien.EditValue = objLTT.MaNV;
            }
            else
            {
                objLTT = new msLichSuTT();
                db.msLichSuTTs.InsertOnSubmit(objLTT);
                objMH = db.msMuaHangs.Single(p => p.MaMH == MaMH);
                txtMaSoMH.Text = objMH.MaSoMH;
                dateNgayTT.EditValue = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MaMH != null)
                    objLTT.MaMH = MaMH;
                objLTT.MaNV = (int?)lookNhanVien.EditValue;
                objLTT.NgayTT = (DateTime?)dateNgayTT.EditValue;
                objLTT.NguoiTT = txtNguoiTT.Text.Trim();
                objLTT.SoTien = (decimal?)spinSoTien.EditValue;
                objLTT.GhiChu = txtDienGiai.Text;
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu!");
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể lưu vui lòng kiểm tra kết nối!");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}