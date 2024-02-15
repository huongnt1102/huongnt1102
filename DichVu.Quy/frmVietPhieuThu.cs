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
using DevExpress.XtraReports.UI;

namespace DichVu.Quy
{
    public partial class frmVietPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien { get; set; }
        string sSoPhieu;

        public frmVietPhieuThu()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        string GetNewPhieuThu()
        {
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            sSoPhieu = db.DinhDang(10, int.Parse(sSoPhieu));
            return sSoPhieu;
        }

        private void frmVietPhieuChi_Load(object sender, EventArgs e)
        {
            lookNhanVien.Properties.DataSource = db.tnNhanViens;
            dateNgayLap.DateTime = db.GetSystemDate();
            txtSoPhieu.Text = GetNewPhieuThu();
            if (objnhanvien.IsSuperAdmin.Value)
                dateNgayLap.Properties.ReadOnly = lookNhanVien.Properties.ReadOnly = false;
            else
                dateNgayLap.Properties.ReadOnly = lookNhanVien.Properties.ReadOnly = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (db.PhieuChis.Count(p => p.SoPhieu == txtSoPhieu.Text.Trim()) > 0)
            {
                DialogBox.Error("Số phiếu bị trùng");
                return;
            }
            if (lookNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên chi");
                return;
            }
            if (spinSoTien.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền chi");
                return;
            }
            PhieuThu phieu = new PhieuThu()
            {
                DienGiai = txtDienGiai.Text.Trim(),
                MaNV = (int)lookNhanVien.EditValue,
                SoPhieu = txtSoPhieu.Text.Trim(),
                NgayThu = dateNgayLap.DateTime,
                SoTienThanhToan = spinSoTien.Value,
                DotThanhToan = dateNgayLap.DateTime,
            };
            db.PhieuThus.InsertOnSubmit(phieu);
            try
            {
                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu(phieu.MaPhieu, "no");
                    //rpt.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

    }
}