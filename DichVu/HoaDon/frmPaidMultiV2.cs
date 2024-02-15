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
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;

namespace DichVu.HoaDon
{
    public partial class frmPaidMultiV2 : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaMB = 0, MaPhieu = 0, Month = 0, Year = 0;
        public int? MaKH;
        public string MaSoMB = "";
        string sSoPhieu;
        public tnNhanVien objnhanvien;
        TienTeCls ttcls = new TienTeCls();
        DateTime now;
        decimal PhiQL = 0, TongPQL = 0;

        PhieuThu objPT;
        ptChiTiet objPTCT;
        List<ptChiTiet> listPTCT;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;
        public frmPaidMultiV2()
        {
            InitializeComponent();

            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmPaidMulti_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            if (MaPhieu == 0)
            {
                LoadData();
                var list = db.PaidMultiV2(MaMB, Month, Year).ToList();
                gcDichVu.DataSource = list;

                spinTongTien.EditValue = list.Where(p => p.IsCheck.GetValueOrDefault()).Sum(p => p.PhaiThu);
                
                dateNgayThu.DateTime = now;
                txtSoPhieu.Text = Library.ReceiptMangerCls.CreateKeyword(now);
            }
            else
            {
                objPT = db.PhieuThus.Single(p => p.MaPhieu == MaPhieu);
                txtSoPhieu.Text = objPT.SoPhieu;
                txtDiaChi.Text = objPT.DiaChi;
                txtDienGiai.Text = objPT.DienGiai;
                txtMatBang.Text = MaSoMB;
                txtNguoiNop.Text = objPT.NguoiNop;
                MaKH = objPT.CusID;
                MaMB = objPT.MaMB.Value;
                PhiQL = objPT.PhiQuanLy ?? 0;
                if (objPT.NgayThu != null)
                    dateNgayThu.DateTime = objPT.NgayThu.Value;
                spinTongTien.EditValue = objPT.SoTienThanhToan ?? 0;
                ckChuyenKhoan.Checked = objPT.ChuyenKhoan.GetValueOrDefault();
                gcDichVu.DataSource = db.PaidMulti_edit(MaPhieu);
                chkAuto.Checked = objPT.IsAuto.GetValueOrDefault();

                colIsCheck.Visible = false;
            }

            itemHuongDan.Click += ItemHuongDan_Click;
            itemClearText.Click += ItemClearText_Click;
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void LoadData()
        {
            var objmb = db.mbMatBangs.FirstOrDefault(p => p.MaMB == MaMB);
            if (objmb == null) return;
            txtMatBang.Text = objmb.MaSoMB;
            PhiQL = 0;
            DateTime now = db.GetSystemDate();
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            //Dien ten mac dinh
            try
            {
                txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                txtDienGiai.Text = string.Format("Thanh toán phí dịch vụ tổng hợp cho mặt bằng {0}", objmb.MaSoMB);
            }
            catch { }
        }

        decimal ChietKhau(int soThang)
        {
            decimal ck = 0;
            var obj = db.PhiQuanLy_ChietKhaus.Where(p => p.SoThangThanhToan <= soThang).OrderByDescending(p => p.SoThangThanhToan).FirstOrDefault();
            if (obj == null)
                ck = 0;
            else
            {
                ck = PhiQL * soThang * (obj.TiLeChietKhau ?? 0);
            }

            TongPQL = PhiQL * soThang;
            return ck;
        }

        private void spinTongTien_EditValueChanged(object sender, EventArgs e)
        {
            ResetData();
            gvDichVu.RefreshData();
        }

        void ResetData()
        {
            if (!chkAuto.Checked) return;

            decimal Total = spinTongTien.Value;
            if (Total < TongPQL) return;

            decimal left = Total - TongPQL;

            gvDichVu.FocusedColumn = colMaMB;
            for (int i = 0; i < gvDichVu.RowCount; i++)
            {
                if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                {
                    if (i == 0)
                    {
                        if (Convert.ToInt32(gvDichVu.GetRowCellValue(i, "SoThang")) == 0)
                        {
                            gvDichVu.SetRowCellValue(i, "SoTien", (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu"));
                            gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                            left -= (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                        }
                        else
                            gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                    }
                    else
                    {
                        if (left > (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu"))
                        {
                            gvDichVu.SetRowCellValue(i, "SoTien", (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu"));
                            gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                            left -= (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                        }
                        else
                        {
                            gvDichVu.SetRowCellValue(i, "SoTien", left);
                            gvDichVu.SetRowCellValue(i, "TongCong", (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau") + (decimal)gvDichVu.GetRowCellValue(i, "SoTien"));
                            left = 0;
                        }
                    }
                }
                else
                    gvDichVu.SetRowCellValue(i, "SoTien", (decimal)0);
            }
            gvDichVu.FocusedColumn = colDichVu;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số phiếu thu], xin cảm ơn.");  
                txtSoPhieu.Focus();
                return;
            }

            if (dateNgayThu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Ngày thu], xin cảm ơn.");  
                dateNgayThu.Focus();
                return;
            }

            if (MaPhieu == 0)
            {
                objPT = new PhieuThu();
                objPT.NgayNhap = now;
                #region Detail
                //listPTCT = new List<ptChiTiet>();
                for (int i = 0; i < gvDichVu.RowCount; i++)
                {
                    if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                    {
                        var objPTCT = new ptChiTiet();
                        objPTCT.ChietKhau = (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau");  
                        objPTCT.DienGiai = gvDichVu.GetRowCellValue(i, "DienGiai").ToString();
                        objPTCT.MaLDV = (byte)gvDichVu.GetRowCellValue(i, "MaLDV");  
                        objPTCT.MaMB = MaMB;
                        var date = (DateTime?)gvDichVu.GetRowCellValue(i, "NgayThu");  
                        if (date != null)
                            objPTCT.NgayThu = date;
                        else
                        {
                            if (dateNgayThu.DateTime.Year != 1)
                                objPTCT.NgayThu = dateNgayThu.DateTime;
                        }
                        objPTCT.PhaiThu = (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                        objPTCT.TongCong = (decimal)gvDichVu.GetRowCellValue(i, "TongCong");  
                        objPTCT.IsDebt = (bool)gvDichVu.GetRowCellValue(i, "IsDebt");  
                        objPT.ptChiTiets.Add(objPTCT);
                    }
                }
                #endregion

                objPT.MaNV = objnhanvien.MaNV;
            }
            else
            {
                objPT.MaNVCN = objnhanvien.MaNV;
                objPT.NgayCN = now;

                #region Detail
                //listPTCT = new List<ptChiTiet>();
                for (int i = 0; i < gvDichVu.RowCount; i++)
                {
                    if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                    {
                        var objPTCT = objPT.ptChiTiets.SingleOrDefault(p => p.ID == (int?)gvDichVu.GetRowCellValue(i, "ID"));
                        objPTCT.ChietKhau = (decimal)gvDichVu.GetRowCellValue(i, "ChietKhau");  
                        objPTCT.DienGiai = gvDichVu.GetRowCellValue(i, "DienGiai").ToString();
                        objPTCT.MaLDV = (byte)gvDichVu.GetRowCellValue(i, "MaLDV");  
                        objPTCT.MaMB = MaMB;
                        var date = (DateTime?)gvDichVu.GetRowCellValue(i, "NgayThu");  
                        if (date != null)
                            objPTCT.NgayThu = date;
                        else
                        {
                            if (dateNgayThu.DateTime.Year != 1)
                                objPTCT.NgayThu = dateNgayThu.DateTime;
                        }
                        objPTCT.PhaiThu = (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                        objPTCT.TongCong = (decimal)gvDichVu.GetRowCellValue(i, "TongCong");  
                        objPTCT.IsDebt = (bool)gvDichVu.GetRowCellValue(i, "IsDebt");  
                    }
                }

                db.ptLichSu_add(objPT.MaPhieu, objnhanvien.MaNV, "Cập nhật thông tin");  
                #endregion
            }

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 100;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            objPT.MaHopDong = MaMB.ToString();
            if (dateNgayThu.DateTime.Year != 1)
            {
                objPT.NgayThu = dateNgayThu.DateTime;
                objPT.DotThanhToan = dateNgayThu.DateTime;
            }
            objPT.SoTienThanhToan = spinTongTien.Value;
            try
            {
                objPT.SoThangThuPhiQuanLy = Convert.ToInt32(gvDichVu.GetRowCellValue(0, "SoThang").ToString());
            }
            catch { objPT.SoThangThuPhiQuanLy = 1; }
            objPT.SoTienChietKhauPhiQL = (decimal)gvDichVu.GetRowCellValue(0, "ChietKhau");  
            objPT.PhaiThu = spinTongTien.Value;
            objPT.PhiQuanLy = PhiQL;
            objPT.KeToanDaDuyet = false;
            objPT.MaMB = MaMB;
            objPT.CusID = MaKH;
            objPT.IsAuto = chkAuto.Checked;
            
            objPT.ChuyenKhoan = ckChuyenKhoan.Checked;

            try
            {
                if (MaPhieu == 0)
                    db.PhieuThus.InsertOnSubmit(objPT);

                db.SubmitChanges();

                if (DialogBox.Question("Bạn có muốn in [Phiếu thu] không?") == DialogResult.Yes)
                {
                    //Barcode
                    //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", DateTime.Now); 
                    //var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV2(objPT.MaPhieu);
                    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                    //    SavePrint(barCodeString, objPT.MaPhieu);
                    //var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV2(objPT.MaPhieu);
                    //rpt.ShowPreviewDialog();
                }
                //if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                //{
                //    ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objPT);
                //    rpt.ShowPreviewDialog();
                //}
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu, vui lòng thử lại sau");  
            }
        }

        void SavePrint(string barCode, int maPhieu)
        {
            var obj = new ptLichSu();
            obj.DienGiai = "In phiếu thu. Mã vạch [" + barCode + "]";
            obj.MaNV = objnhanvien.MaNV;
            obj.MaPT = maPhieu;
            obj.NgayTH = db.GetSystemDate();
            db.ptLichSus.InsertOnSubmit(obj);

            var objPT = db.PhieuThus.Single(p => p.MaPhieu == maPhieu);
            objPT.Barcode = barCode;
            db.SubmitChanges();
        }

        private void gvDichVu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void spinSoThang_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _new = (SpinEdit)sender;
            
            if ((byte)gvDichVu.GetFocusedRowCellValue("MaLDV") == 12)
            {
                decimal ck = ChietKhau(Convert.ToInt32(_new.EditValue));
                gvDichVu.SetFocusedRowCellValue("SoThang", Convert.ToInt32(_new.EditValue));
                gvDichVu.SetFocusedRowCellValue("ChietKhau", ck);
                gvDichVu.SetFocusedRowCellValue("SoTien", TongPQL - ck);
                gvDichVu.SetFocusedRowCellValue("TongCong", TongPQL);
            }
            ResetData();
            gvDichVu.RefreshData();
        }

        private void spinSoTien_EditValueChanged(object sender, EventArgs e)
        {
            if ((byte)gvDichVu.GetFocusedRowCellValue("MaLDV") != 12)
            {
                SpinEdit _new = (SpinEdit)sender;
                gvDichVu.SetFocusedRowCellValue("SoTien", Convert.ToDecimal(_new.EditValue));
                gvDichVu.SetFocusedRowCellValue("TongCong", Convert.ToDecimal(_new.EditValue));
                ResetData();
                gvDichVu.RefreshData();
            }
            {
                try
                {
                    if ((int)gvDichVu.GetFocusedRowCellValue("SoThang") == 0)
                    {
                        SpinEdit _new = (SpinEdit)sender;
                        //TongPQL = Convert.ToDecimal(_new.EditValue);
                        gvDichVu.SetFocusedRowCellValue("SoTien", Convert.ToDecimal(_new.EditValue));
                        gvDichVu.SetFocusedRowCellValue("TongCong", Convert.ToDecimal(_new.EditValue));
                    }
                    else
                    {
                        decimal ck = ChietKhau((int)gvDichVu.GetFocusedRowCellValue("SoThang"));
                        gvDichVu.SetFocusedRowCellValue("ChietKhau", ck);
                        gvDichVu.SetFocusedRowCellValue("SoTien", TongPQL - ck);
                        gvDichVu.SetFocusedRowCellValue("TongCong", TongPQL);
                    }
                }
                catch { }
                ResetData();
                gvDichVu.RefreshData();
            }
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            ResetData();
            gvDichVu.RefreshData();
        }

        private void chkCheck_EditValueChanged(object sender, EventArgs e)
        {
            gvDichVu.FocusedColumn = colMaMB;
            spinTongTien.EditValue = TotalPaid();
        }

        decimal? TotalPaid()
        {
            gvDichVu.RefreshData();
            decimal? result = 0;
            for (int i = 0; i < gvDichVu.RowCount; i++)
            {
                if ((bool)gvDichVu.GetRowCellValue(i, "IsCheck"))
                {
                    result += (decimal)gvDichVu.GetRowCellValue(i, "PhaiThu");  
                }
                else
                    gvDichVu.SetRowCellValue(i, "SoTien", (decimal)0);
            }

            return result;
        }
    }
}