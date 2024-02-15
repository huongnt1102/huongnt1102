using System;
using System.Windows.Forms;
using Library;
using System.Linq;
//using ReportMisc.DichVu;
using System.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.Office.Utils;
using System.Collections.Generic;

namespace DichVu.ChoThue
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public thueHopDong objhd;
        public tnNhanVien objnhanvien;
        string sSoPhieu;
        public frmThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            LoadData();
            
            if (objhd!=null)
            {
                txtSoPhieu.Text = "PTHD-" + sSoPhieu;
                spinSoTienCanThanhToan.EditValue = objhd.ThanhTien * objhd.ChuKyThanhToan;
                txtsotien.Text = (objhd.ThanhTien * objhd.ChuKyThanhToan).ToString();
                txtSoHD.Text = objhd.SoHD;
                txtMatBang.Text = objhd.mbMatBang.MaSoMB;
                //lookChuKy.EditValue = objhd.ChuKyThanhToan;
                
                txtThoiHan.Text = objhd.ThoiHan.ToString();
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
                lookNhanVien.EditValue = objnhanvien.MaNV;

                //Dien ten mac dinh
                try
                {
                    txtHoVaTen.Text = objhd.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objhd.tnKhachHang.HoKH, objhd.tnKhachHang.TenKH) : objhd.tnKhachHang.CtyTen;
                    txtDiaChi.Text = objhd.tnKhachHang.DCLL;
                    txtSoDienThoai.Text = objhd.tnKhachHang.DienThoaiKH;
                    txtEmail.Text = objhd.tnKhachHang.EmailKH;
                    txtGhiChu.Text = string.Format("Thanh toán tiền cho thuê hợp đồng {0}, mặt bằng {1}", objhd.SoHD, objhd.mbMatBang.MaSoMB);
                }
                catch { }
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<thueCongNo> objcongno;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtHoVaTen.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập họ và tên");
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập địa chỉ");
                return;
            }
            if (checkListChuKy.CheckedItems.Count <= 0)
            {
                DialogBox.Error("vui lòng chọn chu kỳ thanh toán");
                return;
            }
            if (objhd == null) return;
            
            PhieuThu objphieuthu = new PhieuThu() 
            { 
                DiaChi = txtDiaChi.Text.Trim(), 
                NguoiNop = txtHoVaTen.Text.Trim(), 
                //DichVu = (int)EnumLoaiDichVu.HopDongThue, 
                DienGiai = txtGhiChu.Text.Trim(), 
                MaHopDong = objhd.SoHD, 
                MaNV = objhd.MaNV, 
                NgayThu = db.GetSystemDate(),
                DotThanhToan = db.GetSystemDate(), 
                SoTienThanhToan = txtsotien.Value,
                SoPhieu = "PTDV-" + sSoPhieu,
                KeToanDaDuyet = false,
                MaMB = objhd.MaMB
            };

            db.PhieuThus.InsertOnSubmit(objphieuthu);

            #region Công nợ

            if (objcongno != null)
            {
                foreach (var item in objcongno)
                {
                    item.DaThanhToan = item.DaThanhToan + txtsotien.Value;
                    item.ConNo = item.ConNo - txtsotien.Value;
                    item.NgayThanhToan = db.GetSystemDate();
                    item.ChuyenKhoan = ckChuyenKhoan.Checked;
                    item.SoPhieuThu = "PTDV-" + sSoPhieu;
                }
                
            }
            #endregion

            try
            {
                db.SubmitChanges();

                if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                {
                    //4: Cong no
                    List<int> ListPhieu = new List<int>();
                    foreach (var item in objcongno)
                    {
                        ListPhieu.Add(item.MaCN);
                    }
                    //using (ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl(ListPhieu, 4, ""))
                    //{
                    //    frm.ShowDialog();
                    //}
                }

                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //1: Phieu thanh toan
                    List<int> ListPhieuThu = new List<int>();
                    ListPhieuThu.Add(objphieuthu.MaPhieu);
                    //using (ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl(ListPhieuThu, 1, txtSoPhieu.Text))
                    //{
                    //    frm.ShowDialog();
                    //}
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Error("Vui lòng kiểm tra các giá trị nhập vào");
            }

            
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            //4: Cong no
            List<int> ListPhieu = new List<int>();
            foreach (var item in objcongno)
            {
                ListPhieu.Add(item.MaCN);
            }
            //using (ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl(ListPhieu, 4, ""))
            //{
            //    frm.ShowDialog();
            //}
        }

        System.Collections.Generic.List<ChuKyItem> ListChuKy;
        private void LoadData()
        {
            DateTime now = db.GetSystemDate();
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            var chuky = db.thueCongNos.Where(p => p.MaHD == objhd.MaHD & p.ConNo > 0)
                .Select(p => new
            {
                KeyValue = p.MaCN,
                TuNgay = p.ChuKyMin,
                DenNgay = p.ChuKyMax,
                ChuKy = string.Format("{0} - {1}", p.ChuKyMin.Value.ToShortDateString() , p.ChuKyMax.Value.ToShortDateString())
            });

            ListChuKy = new System.Collections.Generic.List<ChuKyItem>();
            if (chuky.Count() <= 0)
            {
                DialogBox.Error("Không có công nợ nào cần thành toán");
                this.Close();
            }
            foreach (var item in chuky)
            {
                ChuKyItem it = new ChuKyItem() { Display = item.ChuKy, Value = item.KeyValue };
                ListChuKy.Add(it);
            }
            //lookChuKy.Properties.DataSource = chuky;
            checkListChuKy.DataSource = ListChuKy;
            
        }
        
        private void checkListChuKy_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> ListMaCN = new List<int>();
            objcongno = new List<thueCongNo>();
            try
            {
                foreach (ChuKyItem item in checkListChuKy.CheckedItems)
                {
                    ListMaCN.Add(item.Value);
                }

                objcongno = db.thueCongNos.Where(p => ListMaCN.Contains(p.MaCN)).ToList();

                txtsotien.EditValue = spinSoTienCanThanhToan.EditValue = objcongno.Sum(p => p.ConNo) ?? 0;

                //txtsotien.EditValue = objcongno.ConNo;
            }
            catch { }
        }

        class ChuKyItem
        {
            public int Value { get; set; }
            public string Display { get; set; }
        }
    }
}