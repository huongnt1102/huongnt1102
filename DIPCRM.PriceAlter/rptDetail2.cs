using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;


namespace DIPCRM.PriceAlert
{
    public partial class rptDetail2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDetail2(int? id)
        {
            InitializeComponent();


            #region Databings
            cSTT.DataBindings.Add("Text", null, "STT");
            cMaSP.DataBindings.Add("Text", null, "MaSP");
            cTenSP.DataBindings.Add("Text", null, "TenSP");           
            cTenDVT.DataBindings.Add("Text", null, "TenDVT");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion

            //var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                //Load template for report
                //Library.Controls.PrintControl.LoadLayout(this, 65);

                //var objCom = db.CongTies.Single(p => p.ID == Common.ComID);
                //cTenCT.Text = objCom.TenCT;
                //cDiaChiCT.Text = objCom.DiaChi;

                //var objPhieu = (from bg in db.BaoGias
                //                join lt in db.LoaiTiens on bg.MaLT equals lt.MaLT
                //                join kh in db.tnKhachHangs on bg.MaKH equals kh.MaKH
                //                join nv in db.tnNhanViens on bg.MaNV equals nv.MaNV
                //                where bg.ID == id
                //                select new
                //                {
                //                    bg.SoBG,
                //                    bg.NgayBG,
                //                    TenLTVT = lt.TenVT,
                //                    lt.TenLT,
                //                    TenKH = kh.TenCongTy,
                //                    DiaChiKH = kh.DiaChiCT,
                //                    MaThueKH = kh.MaSoThueCT,
                //                    bg.GhiChu,
                //                    DienThoaiKH = kh.DienThoaiCT,
                //                    FaxKH = kh.FaxCT
                //                }).First();

                //cNgayDH.Text = string.Format("{0:dd/MM/yyyy}", objPhieu.NgayBG);
                //cSoDH.Text = objPhieu.SoBG;
                //cTenLT.Text = objPhieu.TenLTVT;
                //cTenKH.Text = objPhieu.TenKH;
                //cDiaChiKH.Text = objPhieu.DiaChiKH;
                //cMaThueKH.Text = objPhieu.MaThueKH;
                //cDienGiai.Text = objPhieu.GhiChu;
                //cDienThoaiKH.Text = objPhieu.DienThoaiKH;
                //cFaxKH.Text = objPhieu.FaxKH;
                ////
                //var ltSP = (from ct in db.bgSanPhams
                //            join sp in db.SanPhams on ct.MaSP equals sp.ID
                //            join d in db.DonViTinhs on sp.MaDVT equals d.MaDVT into dv
                //            from dvt in dv.DefaultIfEmpty()
                //            where ct.MaBG == id
                //            select new { sp.MaSP, sp.TenSP, dvt.TenDVT, ct.SoLuong, ct.DonGia, ThanhTien = ct.SoLuong * ct.DonGia, ct.ThueGTGT, ct.TienCK })
                //            .AsEnumerable()
                //            .Select((p, index) => new { STT = index + 1, p.MaSP, p.TenSP, p.TenDVT, p.SoLuong, p.DonGia, p.ThanhTien, p.ThueGTGT, p.TienCK })
                //            .ToList();
                //this.DataSource = ltSP;
                ////
                //var tienCK = ltSP.Sum(p => (decimal?)p.TienCK).GetValueOrDefault();
                //cSumTienCK.Text = string.Format("{0:#,0.##}", tienCK);
                //if (tienCK > 0)
                //{
                //    rTienCK.Visible = true;
                //    cSumThanhTienTitle.Text = "Cộng tiền hàng (Đã trừ CK):";
                //}
                //else
                //{
                //    rTienCK.Visible = false;
                //    cSumThanhTienTitle.Text = "Cộng tiền hàng:";
                //}

                //var thanhTien = ltSP.Sum(p => (decimal?)p.ThanhTien).GetValueOrDefault() - tienCK;
                //cSumThanhTien.Text = string.Format("{0:#,0.##}", thanhTien);

                //var tienThue = ltSP.Sum(p => (decimal?)((p.ThanhTien - p.TienCK.GetValueOrDefault()) * p.ThueGTGT)).GetValueOrDefault();
                //cSumThueGTGT.Text = string.Format("{0:#,0.##}", tienThue);

                //var tongTien = thanhTien + tienThue;
                //cSumTongTien.Text = string.Format("{0:#,0.##}", tongTien);
                //cSumTongTienBC.Text = new TienTeCls().DocTienBangChu(tongTien, objPhieu.TenLT);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                //wait.Close();
                //db.Dispose();
            }
        }
    }
}
