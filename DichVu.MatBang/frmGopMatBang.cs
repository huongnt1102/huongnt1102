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

namespace DichVu.MatBang
{
    public partial class frmGopMatBang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public mbMatBang objmatbang;
        public tnNhanVien objnhanvien;
        public frmGopMatBang()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmGopMatBang_Load(object sender, EventArgs e)
        {
            searchLookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH == null);
            lookLoaiMB.Properties.DataSource = db.mbLoaiMatBangs;

            if (objmatbang != null)
            {
                //txtGiaChoThue.Text = objmatbang.ThanhTien.ToString();
                if (objmatbang.MaLMB != null)
                {
                    txtLoaiMatBang.Text = db.mbLoaiMatBangs.SingleOrDefault(p1 => p1.MaLMB == objmatbang.MaLMB).TenLMB;//objmatbang.mbLoaiMatBang.TenLMB;
                }
                txtKyHieu.Text = objmatbang.MaSoMB;
                //txtDonGia.Text = objmatbang.DonGia.ToString();

                if (objmatbang.MaTL != null)
                {
                    txtToaNha.Text = objmatbang.mbTangLau.mbKhoiNha.tnToaNha.TenTN;
                    txtTangLau.Text = objmatbang.mbTangLau.TenTL;
                    txtKhoiNha.Text = objmatbang.mbTangLau.mbKhoiNha.TenKN;
                }
                txtDienTich.Value = objmatbang.DienTich ?? 0;
            }
        }

        private void searchLookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = db.mbMatBangs.Single(p => p.MaMB == (int)searchLookMatBang.EditValue);
                //spinGiaChoThue.Text = obj.ThanhTien.ToString();
                if (obj.MaLMB != null)
                {
                    lookLoaiMB.EditValue = obj.MaLMB;
                }
                txtKyHieu2.Text = obj.MaSoMB;
                //spinDonGia.Text = obj.DonGia.ToString();

                if (obj.MaTL != null)
                {
                    txtToaNha2.Text = obj.mbTangLau.mbKhoiNha.tnToaNha.TenTN;
                    txtTangLau2.Text = obj.mbTangLau.TenTL;
                    txtKhoiNha2.Text = obj.mbTangLau.mbKhoiNha.TenKN;
                }
                spinDienTich.Value = obj.DienTich ?? 0;
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (searchLookMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng đích");
                return;
            }
            var obj = db.mbMatBangs.Single(p => p.MaMB == (int)searchLookMatBang.EditValue);
            obj.DienTich = obj.DienTich + objmatbang.DienTich;
            //if (obj.TinhPhiTheoDienTich ?? false)
            //{
            //    obj.SoTienPerMet = objmatbang.SoTienPerMet;
            //    obj.DienTichThuPhi = obj.DienTichThuPhi + objmatbang.DienTichThuPhi;
            //    obj.PhiQuanLy = objmatbang.SoTienPerMet * obj.DienTichThuPhi;
            //}

            objmatbang = db.mbMatBangs.Single(p => p.MaMB == objmatbang.MaMB);
            db.mbMatBangs.DeleteOnSubmit(objmatbang);

            try
            {
                db.SubmitChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}