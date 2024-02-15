using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;
using Library;

namespace DIPCRM.PriceAlert
{
    public class rptDetail : DevExpress.XtraRichEdit.RichEditControl
    {
        public rptDetail(int? id)
        {
            //var wait = DialogBox.WaitingForm();
            MasterDataContext db = new MasterDataContext();
            try
            {
                var objBG = (from bg in db.BaoGias
                             join kh in db.tnKhachHangs on bg.MaKH equals kh.MaKH
                             //join lh in db.NguoiLienHes on kh.MaNLH equals lh.ID into lhe
                             //from lh in lhe.DefaultIfEmpty()
                             join nv in db.tnNhanViens on bg.MaNV equals nv.MaNV
                             //join lt in db.LoaiTiens on bg.MaLT equals lt.ID
                             //join mau in db.bgBieuMaus on bg.MaBM equals mau.ID
                             //join bm in db.bmBieuMaus on mau.MaBM equals bm.ID
                             //join da in db.DuAns on bg.MaDA equals da.ID into duan
                             //from da in duan.DefaultIfEmpty()
                             where bg.ID == id
                             select new
                             {
                                 bg.ID,
                                 bg.SoBG,
                                 bg.NgayBG,
                                 bg.NgayYC,
                                 //TenCT = da.TenDAVT,
                                 TenCongTy = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                 DiaChiCT = kh.DiaChi,
                                 DienThoaiCT = kh.DienThoai,
                                 //kh.FaxCT,
                                 kh.Email,
                                 MaSoThueCT = kh.MaSoThue,
                                 //HoTenNLH = lh.HoTen,
                                 //DiDongNLH = lh.DiDong,
                                 //EmailNLH = lh.Email,
                                 //lh.TenCV,
                                 //HoTenNBG = nv.HoTen,
                                 DiDongNBG = nv.DienThoai,
                                 bg.DKGH,
                                 bg.DKTT,
                                 bg.ThoiHanBG,
                                 //TenLT = lt.TenVT,
                                 //lt.TyGia,
                                 bg.TieuDe,
                                 bg.GhiChu,
                                 //bm.NoiDung
                             }).First();

                //Thong tin chung
                //this.RtfText = objBG.NoiDung;
                //Document.ReplaceAll("[TieuDeBG]", objBG.TieuDe ?? "", SearchOptions.None);
                //Document.ReplaceAll("[GhiChuBG]", objBG.GhiChu ?? "", SearchOptions.None);
                //Document.ReplaceAll("[SoBG]", objBG.SoBG ?? "", SearchOptions.None);
                //Document.ReplaceAll("[NgayBG]", string.Format("{0:dd/MM/yyyy}", objBG.NgayBG), SearchOptions.None);
                //Document.ReplaceAll("[NgayYCBG]", string.Format("{0:dd/MM/yyyy}", objBG.NgayYC), SearchOptions.None);
                //Document.ReplaceAll("[C.TrinhBG]", objBG.TenCT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[DKGHBG]", objBG.DKGH ?? "", SearchOptions.None);
                //Document.ReplaceAll("[DKTTBG]", objBG.DKTT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[G.TriBG]", objBG.ThoiHanBG ?? "", SearchOptions.None);
                //Document.ReplaceAll("[NguoiBG]", objBG.HoTenNBG ?? "", SearchOptions.None);
                //Document.ReplaceAll("[DiDongNBG]", objBG.DiDongNBG ?? "", SearchOptions.None);
                //Document.ReplaceAll("[ChucVuNBG]", "", SearchOptions.None);
                //Document.ReplaceAll("[TenLT]", objBG.TenLT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[TyGiaLT]", string.Format("{0:#,0.##}", objBG.TyGia), SearchOptions.None);
                ////Thong tin khach hang
                //Document.ReplaceAll("[TenCT]", objBG.TenCongTy ?? "", SearchOptions.None);
                //Document.ReplaceAll("[DiaChiCT]", objBG.DiaChiCT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[DienThoaiCT]", objBG.DienThoaiCT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[FaxCT]", objBG.FaxCT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[EmailCT]", objBG.Email ?? "", SearchOptions.None);
                //Document.ReplaceAll("[MSTCT]", objBG.MaSoThueCT ?? "", SearchOptions.None);
                //Document.ReplaceAll("[HoTenLH]", objBG.HoTenNLH ?? "", SearchOptions.None);
                //Document.ReplaceAll("[TenCVLH]", objBG.TenCV ?? "", SearchOptions.None);
                //Document.ReplaceAll("[EmailLH]", objBG.EmailNLH ?? "", SearchOptions.None);
                //Document.ReplaceAll("[DiDongLH]", objBG.DiDongNLH ?? "", SearchOptions.None);
                
                //Danh sach san pham
                var poits = Document.FindAll("[SanPham]", SearchOptions.None);
                if (poits.Length > 0)
                {
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        var rpt = new rptProducts(id.Value);
                        rpt.ExportToImage(stream);
                        var imgSP = System.Drawing.Image.FromStream(stream);
                        foreach (var poit in poits)
                            Document.InsertImage(poit.Start, imgSP);
                    }
                    Document.ReplaceAll("[SanPham]", "", SearchOptions.None);
                }

                //var ltSanPham = (from ct in db.bgSanPhams
                //                 join sp in db.SanPhams on ct.MaSP equals sp.ID
                //                 join x in db.XuatXus on sp.MaXX equals x.MaXX into xl
                //                 from xx in xl.DefaultIfEmpty()
                //                 join d in db.DonViTinhs on sp.MaDVT equals d.MaDVT into dv
                //                 from dvt in dv.DefaultIfEmpty()
                //                 join lsp in db.LoaiSanPhams on sp.MaLSP equals lsp.MaLSP
                //                 where ct.MaBG == id
                //                 orderby lsp.STT
                //                 select new
                //                 {
                //                     sp.ID,
                //                     sp.MaSP,
                //                     sp.DienGiai,
                //                     xx.TenXX,
                //                     dvt.TenDVT,
                //                     ct.SoLuong,
                //                     ct.DonGia,
                //                     ct.ThueGTGT,
                //                     ct.ThanhTien
                //                 }).ToList();
                //int i = 1;
                //foreach (var sp in ltSanPham)
                //{
                //    Document.ReplaceAll("[MaHieu" + i + "]", sp.MaSP ?? "", SearchOptions.None);
                //    Document.ReplaceAll("[MoTa" + i + "]", sp.DienGiai ?? "", SearchOptions.None);
                //    Document.ReplaceAll("[XuatXu" + i + "]", sp.TenXX ?? "", SearchOptions.None);
                //    Document.ReplaceAll("[DVT" + i + "]", sp.TenDVT ?? "", SearchOptions.None);
                //    Document.ReplaceAll("[SL" + i + "]", string.Format("{0:#,0.##}", sp.SoLuong), SearchOptions.None);
                //    Document.ReplaceAll("[D.Gia" + i + "]", string.Format("{0:#,0.##}", sp.DonGia), SearchOptions.None);
                //    Document.ReplaceAll("[ThueGTGT" + i + "]", string.Format("{0:p0}", sp.ThueGTGT), SearchOptions.None);
                //    Document.ReplaceAll("[ThanhTien" + i + "]", string.Format("{0:#,0.##}", sp.ThanhTien), SearchOptions.None);
                //    i++;
                //}
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                //wait.Close();
            }
        }
    }
}
