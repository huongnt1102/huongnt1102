using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace TaiSan.DatHang.Report
{
    public partial class rptDonDatHang: DevExpress.XtraReports.UI.XtraReport
    {
        public rptDonDatHang(string MaPhieu)
        {
            InitializeComponent();

            using (MasterDataContext db = new MasterDataContext())
            {
                try
                {
                    DateTime now = db.GetSystemDate();
                    var phieu = db.ddhDatHangs.Single(p => p.MaDH == int.Parse(MaPhieu));
                    lblSoPhieu.Text = phieu.MaSoDH;


                    #region Thông tin côngty
                    var tn = db.tnToaNhas.FirstOrDefault(p => p.MaTN == phieu.MaTN);
                    if (tn != null)
                    {
                        lblTenCtyDH.Text = tn.CongTyQuanLy;
                        lblDiaChiCtyDH.Text = "Địa chỉ: " + tn.DiaChiCongTy;
                        lblDienThoaiCtyDH.Text = string.Format("Tel: {0}/Fax: {1}",tn.DienThoai,tn.Fax);
                        lblEmailCtyDH.Text = string.Format("Email: {0}", tn.Email);

                        lblPhongBan.Text = string.Format("Trước tiên, {0} gửi tới Quý công ty lời chào trân trọng!",tn.CongTyQuanLy);
                        lblDeNghi.Text =
                            string.Format("    {0} đề nghị Quý công ty cấp cho chúng tôi một số chủng loại vật tư sau :", tn.CongTyQuanLy);

                        lblCongTyVietHoaDon.Text = tn.CongTyQuanLy;
                        lblDiaChiVietHoaDon.Text= "Địa chỉ: " + tn.DiaChiCongTy;
                        lblMaSoThueVietHoaDon.Text = string.Format("Mã số thuế : {0}", tn.MaSoThue);
                        cChuKy.Text = string.Format("{0}", tn.CongTyQuanLy);
                    }
                    #endregion

                    lblHanThanhToan.Text =string.Format("                                         Bên mua thanh toán trong vòng {0} ngày kể từ ngày hoàn thành việc giao nhận hàng hóa và nhận được hóa đơn tài chính do Bên bán cung cấp.",phieu.HanThanhToan!=null?phieu.HanThanhToan:0);

                    lblNgayThangNam.Text = string.Format("Ngày {0} tháng {1} năm {2}", phieu.NgayDH.Value.Day, phieu.NgayDH.Value.Month, phieu.NgayDH.Value.Year);
                    var tenNCC = db.tnKhachHangs.SingleOrDefault(p=>p.MaKH == phieu.MaNCC);
                    lblTenNhaCungCap.Text = tenNCC.CtyTen;
                    lblDTNhaCungCap.Text = "Tel: "+ tenNCC.CtyDienThoai;
                    lblFaxNhaCungCap.Text = "Fax: "+ tenNCC.CtyFax;
                    if (phieu.NgayGH != null)
                    {
                        lblNgayGiao.Text = string.Format("{0:dd/MM/yyyy}", phieu.NgayGH);
                    }
                    else lblNgayGiao.Text = "";
                    lblNoiNhanHang.Text = phieu.NoiNhanHang;
                    lblChatLuong.Text = phieu.ChatLuong;
                    var nguoinhan = (from p in db.tnNhanViens
                                    join q in db.tnChucVus on p.MaCV equals q.MaCV into chuc
                                    from q in chuc.DefaultIfEmpty()
                                    where p.MaNV == phieu.NguoiNhanHang
                                    select new {p.HoTenNV, p.DienThoai,q.TenCV}).ToList();
                    lblTenNguoiNhan.Text = "+ " + nguoinhan.FirstOrDefault().HoTenNV;
                    lblChucVu.Text = "Chức vụ : " + nguoinhan.FirstOrDefault().TenCV;
                    lblDTNguoiNhan.Text = "ĐT : " + nguoinhan.FirstOrDefault().DienThoai;
                    var _ttvt = (from p in db.ddhTaiSans
                                 join a in db.tsLoaiTaiSans on p.MaLTS equals a.MaLTS into lts
                                 from a in lts.DefaultIfEmpty()
                                 join b in db.tsLoaiTaiSan_DVTs on a.MaDVT equals b.MaDVT into dvt
                                 from b in dvt.DefaultIfEmpty()
                                 where (p.MaDH == phieu.MaDH)
                                 select new
                                 {
                                     TenLTS = " " + p.tsLoaiTaiSan.TenLTS,
                                     p.NhaSX,
                                     p.SoLuong,
                                     b.TenDVT,
                                     DonGia = p.DonGia,
                                     ThanhTien = (p.SoLuong * p.DonGia)//.Value.ToString("{N0}"),
                                 }).ToList();

                    this.DataSource = _ttvt;

                    lblTenHang.DataBindings.Add(new XRBinding("Text", DataSource,"TenLTS"));
                    lblSoLuong.DataBindings.Add(new XRBinding("Text", DataSource, "SoLuong","{0:N0}"));
                    lblThanhTien.DataBindings.Add(new XRBinding("Text", DataSource, "ThanhTien", "{0:N0}"));
                    lblHangSX.DataBindings.Add(new XRBinding("Text", DataSource, "NhaSX"));
                    lblDonGia.DataBindings.Add(new XRBinding("Text", DataSource, "DonGia", "{0:N0}"));
                    lblDVT.DataBindings.Add(new XRBinding("Text", DataSource, "TenDVT"));
                    decimal sum = 0;
                    foreach (var item in _ttvt)
                    {
                        sum += (item.ThanhTien == null ? 0 : item.ThanhTien.Value);
                    }
                    decimal vat = (sum * (phieu.VAT == null ? 0 : phieu.VAT.Value))/100;
                    decimal sumVAT = sum + vat;
                    lblGTTT.Text = decimal.Round(sum).ToString("N0");
                    lblVAT.Text = decimal.Round(vat).ToString("N0");
                    lblTong.Text = decimal.Round(sumVAT).ToString("N0");
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
        }
        private void lblDonGia_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }
    }
}
