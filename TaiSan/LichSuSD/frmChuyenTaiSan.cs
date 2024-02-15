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
using DevExpress.XtraEditors.Controls;

namespace TaiSan.LichSuSD
{
    public partial class frmChuyenTaiSan : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public tsTaiSanSuDung objts;
        public mbMatBang objmbSource;

        MasterDataContext db;
        public frmChuyenTaiSan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmChuyenTaiSan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lookToaNha.Properties.NullText = "Chọn Dự án";
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookToaNha.Properties.DataSource = db.tnToaNhas;
            }
            else
            {
                lookToaNha.Properties.DataSource = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN);
            }
            lookToaNha.Properties.ValueMember = "MaTN";
            lookToaNha.Properties.DisplayMember = "TenVT";
            lookToaNha.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookToaNha.Properties.SearchMode = SearchMode.AutoComplete;
            lookToaNha.Properties.DropDownRows = 10;
            LookUpColumnInfoCollection col = lookToaNha.Properties.Columns;
            col.Add(new LookUpColumnInfo("TenVT", "Tên viết tắt",50));
            col.Add(new LookUpColumnInfo("TenTN", "Tên Dự án", 100));

            if (objts == null)
                return;

            txtHSD.Text = string.Format("{0} tháng", objts.ThoiHan);
            txtHSX.Text = objts.tsHangSanXuat.TenHSX;
            txtKyHieu.Text = objts.KyHieu;
            txtNCC.Text = objts.tnNhaCungCap.TenNCC;
            txtNgaySX.Text = objts.NgaySX.Value.ToShortDateString();
            txtTenTS.Text = objts.tsLoaiTaiSan.TenLTS;
            txtTrangThai.Text = objts.tsTrangThai.TenTT;
            txtXX.Text = objts.tsXuatXu.TenXX;

            if (objmbSource == null) return;

            using (MasterDataContext db = new MasterDataContext())
            {
                vgcmbSource.DataSource = db.mbMatBangs.Where(p => p.MaMB == objmbSource.MaMB)
                    .Select(p => new
                    {
                        p.MaSoMB,
                        p.mbTangLau.TenTL,
                        p.mbTangLau.mbKhoiNha.TenKN,
                        p.mbLoaiMatBang.TenLMB,
                        p.mbTrangThai.TenTT,
                        KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                        p.DienGiai
                    });
            }
        }

        private void lookToaNha_EditValueChanged(object sender, EventArgs e)
        {
            lookKhoiNha.Properties.NullText = "Chọn khối nhà";
            lookKhoiNha.Properties.DataSource = db.mbKhoiNhas.Where(p => p.MaTN == (byte)lookToaNha.EditValue);
            lookKhoiNha.Properties.ValueMember = "MaKN";
            lookKhoiNha.Properties.DisplayMember = "TenKN";
            lookKhoiNha.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookKhoiNha.Properties.SearchMode = SearchMode.AutoComplete;
            lookKhoiNha.Properties.DropDownRows = 10;
        }

        private void lookKhoiNha_EditValueChanged(object sender, EventArgs e)
        {
            lookTangLau.Properties.NullText = "Chọn tầng lầu";
            lookTangLau.Properties.DataSource = db.mbTangLaus.Where(p => p.MaKN == (int)lookKhoiNha.EditValue);
            lookTangLau.Properties.ValueMember = "MaTL";
            lookTangLau.Properties.DisplayMember = "TenTL";
            lookTangLau.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookTangLau.Properties.SearchMode = SearchMode.AutoComplete;
            lookTangLau.Properties.DropDownRows = 10;
        }

        private void lookTangLau_EditValueChanged(object sender, EventArgs e)
        {
            lookMatBang.Properties.NullText = "Chọn mặt bằng";
            lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaTL == (int)lookTangLau.EditValue)
                .Select(p => new
                {
                    p.MaMB,
                    p.MaSoMB,
                    p.mbLoaiMatBang.TenLMB,
                    p.DienGiai
                });
            lookMatBang.Properties.ValueMember = "MaMB";
            lookMatBang.Properties.DisplayMember = "MaSoMB";
            lookMatBang.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookMatBang.Properties.SearchMode = SearchMode.AutoComplete;
            lookMatBang.Properties.DropDownRows = 10;
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                vgcmbDestination.DataSource = db.mbMatBangs.Where(p => p.MaMB == (int)lookMatBang.EditValue)
                    .Select(p => new
                    {
                        p.MaSoMB,
                        p.mbTangLau.TenTL,
                        p.mbTangLau.mbKhoiNha.TenKN,
                        p.mbLoaiMatBang.TenLMB,
                        p.mbTrangThai.TenTT,
                        KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                        p.DienGiai
                    });
            }
            catch 
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            db.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lookMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng đích");
                return;
            }
            var wait = DialogBox.WaitingForm();

            var ts = db.tsTaiSanSuDungs.Single(p => p.MaTS == objts.MaTS);
            ts.MaMB = (int)lookMatBang.EditValue;
            if (ts.NgaySD == null) ts.NgaySD = db.GetSystemDate();
            try
            {
                var maxls = db.tsLichSuSDs.Where(p =>p.MaTS == objts.MaTS).Max(p => p.NgayBatDauSD);
                var lssdsource = db.tsLichSuSDs.Single(p => p.MaTS == objts.MaTS
                    & p.NgayBatDauSD == maxls);

                lssdsource.NgayKetThucSD = db.GetSystemDate();
            }
            catch { }

            tsLichSuSD objtssd = new tsLichSuSD()
            {
                DienGiai = txtDienGiai.Text.Trim(),
                MaMB = (int)lookMatBang.EditValue,
                MaTS = ts.MaTS,
                MaTT = ts.MaTT,
                NgayBatDauSD = db.GetSystemDate()
            };

            db.tsLichSuSDs.InsertOnSubmit(objtssd);
            try
            {
                db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Lưu không thành công");
            }
            finally
            {
                this.Close();
            }
        }
    }
}