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

namespace KyThuat.ThanhToanDichVu
{
    public partial class frmEdit_Dien : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaTN = 0;
        decimal TongTien = 0;
        decimal SoTienConLai = 0;
        public tnNhanVien objnhanvien { get; set; }
        public LichThanhToan objLichThanhToan { get; set; }

        public frmEdit_Dien()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }
        
        private void frmEdit_Dien_Load(object sender, EventArgs e)
        {
            lookUpNhaCC.Properties.DataSource = db.tnNhaCungCaps.Select(p => new { p.MaNCC, p.TenNCC });
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookUpToaNha.Properties.DataSource = db.tnToaNhas;
            }
            else
            {
                lookUpToaNha.Properties.DataSource = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN);
            }

            if (objLichThanhToan == null)
            {
                objLichThanhToan = new LichThanhToan();
                objLichThanhToan.MaNV = objnhanvien.MaNV;
                objLichThanhToan.NgayTao = db.GetSystemDate();
                db.LichThanhToans.InsertOnSubmit(objLichThanhToan);

                lookUpToaNha.EditValue = MaTN;
            }
            else
            {
                objLichThanhToan = db.LichThanhToans.Single(p => p.LichID == objLichThanhToan.LichID);
                spinPhiDV.Value = objLichThanhToan.TongPhiDichVu ?? 0;
                spinSoLanThanhToan.Value = objLichThanhToan.SoLanThanhToan ?? 1;
                dateThangThanhToan.DateTime = objLichThanhToan.ThangThanhToan ?? DateTime.Now;
                lookUpNhaCC.EditValue = objLichThanhToan.MaNCC;
                txtDienGiai.Text = objLichThanhToan.DienGiai;
                objLichThanhToan.MaNVCN = objnhanvien.MaNV;
                objLichThanhToan.NgayCN = db.GetSystemDate();

                lookUpToaNha.EditValue = (byte?)objLichThanhToan.MaTN;
                lookKhoiNha.EditValue = objLichThanhToan.MaKN;
            }
            gcThanhToan.DataSource = objLichThanhToan.LichThanhToan_ChiTiets;
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (lookUpNhaCC.Text == "[Chọn nhà cung cấp]")
            {
                DialogBox.Alert("Vui lòng chọn [Nhà cung cấp], xin cảm ơn.");
                lookUpNhaCC.Focus();
                return;
            }

            if (lookUpToaNha.Text == "[Chọn Dự án]")
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                lookUpToaNha.Focus();
                return;
            }

            if (dateThangThanhToan.EditValue == null | spinSoLanThanhToan.Value <= 0 | spinPhiDV.Value <= 0)
            {
                DialogBox.Alert("Vui lòng điền thông tin [Tổng phí dịch vụ], [Tháng thanh toán], [Số lần thanh toán]");
                return;
            }
            objLichThanhToan.ThangThanhToan = dateThangThanhToan.DateTime;
            objLichThanhToan.TongPhiDichVu = spinPhiDV.Value;
            objLichThanhToan.SoLanThanhToan = Convert.ToInt32(spinSoLanThanhToan.Value);
            objLichThanhToan.DienGiai = txtDienGiai.Text.Trim();
            objLichThanhToan.MaNCC = Convert.ToInt32(lookUpNhaCC.EditValue);
            objLichThanhToan.MaTN = Convert.ToInt32(lookUpToaNha.EditValue);
            objLichThanhToan.MaKN = (int?)lookKhoiNha.EditValue;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnTaoLich_Click(object sender, EventArgs e)
        {
            if (spinSoLanThanhToan.EditValue == null | spinPhiDV.EditValue == null | dateThangThanhToan.EditValue == null
                | spinSoLanThanhToan.Value <= 0 | spinPhiDV.Value <= 0)
            {
                return;
            }

            grvThanhToan.SelectAll();
            grvThanhToan.DeleteSelectedRows();

            var SoLanThanhToan = spinSoLanThanhToan.Value;
            var TongSoTienThanhToan = spinPhiDV.Value;
            var ThangThanhToan = dateThangThanhToan.DateTime;

            for (int i = 0; i < SoLanThanhToan; i++)
            {
                grvThanhToan.AddNewRow();
                grvThanhToan.SetFocusedRowCellValue(colDaTT, false);
                grvThanhToan.SetFocusedRowCellValue(colLichID, objLichThanhToan.LichID);
                if (i == (SoLanThanhToan - 1))
                {
                    TinhPhiDaSuDung();
                    SoTienConLai = spinPhiDV.Value - TongTien;
                    grvThanhToan.SetFocusedRowCellValue(colSoTien, SoTienConLai);
                }
                else
                {
                    grvThanhToan.SetFocusedRowCellValue(colSoTien, TongSoTienThanhToan / SoLanThanhToan);
                }
                grvThanhToan.UpdateCurrentRow();
            }
            TinhPhiDaSuDung();
            spinSoTienConLai.Value = spinPhiDV.Value - TongTien;
        }

        private void TinhPhiDaSuDung()
        {
            TongTien = 0;
            try
            {
                for (int i = 0; i < grvThanhToan.RowCount; i++)
                {
                    TongTien = TongTien + (decimal)grvThanhToan.GetRowCellValue(i, colSoTien);
                }
            }
            catch { }
        }

        private void grvThanhToan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colSoTien & spinPhiDV.Value > 0)
            {
                TinhPhiDaSuDung();
                spinSoTienConLai.Value = spinPhiDV.Value - TongTien;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvThanhToan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvThanhToan.SetFocusedRowCellValue("DaTT", false);
        }

        private void lookUpToaNha_EditValueChanged(object sender, EventArgs e)
        {
            int matn = int.Parse(lookUpToaNha.EditValue.ToString());
            lookKhoiNha.Properties.DataSource = db.mbKhoiNhas.Where(p => p.MaTN == matn);
            lookKhoiNha.EditValue = null;
        }

        private void lookKhoiNha_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookKhoiNha.EditValue = null;
                lookKhoiNha.ClosePopup();
            }
        }
    }
}