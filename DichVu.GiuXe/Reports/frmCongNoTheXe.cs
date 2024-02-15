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
using DevExpress.Data.PivotGrid;

namespace DichVu.GiuXe.Reports
{
    public partial class frmCongNoTheXe : DevExpress.XtraEditors.XtraForm
    {
        public frmCongNoTheXe()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var ltToaNha = this.GetToaNha();

            
                var ltTheXe = from tx in db.dvgxTheXes
                              join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                              join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                              join mb in db.mbMatBangs on tx.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                              join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                              from lmb in tblLoaiMatBang.DefaultIfEmpty()
                              join tn in db.tnToaNhas on tx.MaTN equals tn.MaTN
                              where ltToaNha.Contains(tx.MaTN) //& SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0

                              select new
                              {
                                  tx.SoThe,
                                  tx.NgayDK,
                                  tx.ChuThe,
                                  tx.BienSo,
                                  tx.DoiXe,
                                  tx.MauXe,
                                  SoLuong = 1,
                                  DonGia = tx.GiaThang,
                                  lx.TenLX,
                                  kh.KyHieu,
                                  TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                  mb.MaSoMB,
                                  tl.TenTL,
                                  kn.TenKN,
                                  lmb.TenLMB,
                                  tn.TenTN,
                                  DauKy = (from hd in db.dvHoaDons
                                           where hd.LinkID == tx.ID 
                                           & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 
                                           & hd.IsDuyet == true & hd.MaLDV == 6                                          
                                           select new
                                           {
                                               ConNo = hd.PhaiThu - (from pt in db.ptPhieuThus
                                                                     join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
                                                                     where
                                                                         SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 &
                                                                         ptct.LinkID == hd.ID
                                                                     select ptct.SoTien).Sum().GetValueOrDefault()
                                                     - (from kt in db.ktttKhauTruThuTruocs
                                                        join ktct in db.ktttChiTiets on kt.ID equals ktct.MaCT
                                                        where
                                                            SqlMethods.DateDiffDay(kt.NgayCT, _TuNgay) > 0 &
                                                            ktct.LinkID == hd.ID
                                                        select ktct.SoTien).Sum().GetValueOrDefault()
                                           }).Sum(p => p.ConNo).GetValueOrDefault(),

                                  PhatSinh = (from hd in db.dvHoaDons
                                              where hd.LinkID == tx.ID & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.MaLDV == 6 & hd.IsDuyet == true
                                              select new
                                              {
                                                  ConNo = hd.PhaiThu - (from pt in db.ptPhieuThus
                                                                        join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
                                                                        where
                                                                            SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 &
                                                                            ptct.LinkID == hd.ID
                                                                        select ptct.SoTien).Sum().GetValueOrDefault()
                                                      - (from kt in db.ktttKhauTruThuTruocs
                                                         join ktct in db.ktttChiTiets on kt.ID equals ktct.MaCT
                                                         where
                                                             SqlMethods.DateDiffDay(kt.NgayCT, _TuNgay) > 0 &
                                                             ktct.LinkID == hd.ID
                                                         select ktct.SoTien).Sum().GetValueOrDefault()
                                              }).Sum(p => p.ConNo).GetValueOrDefault(),
                                  DaThu = (from ct in db.ptChiTietPhieuThus
                                           join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                           join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                           where hd.LinkID == tx.ID & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 
                                           & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                            & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                           & hd.MaLDV == 6 & hd.IsDuyet == true
                                           select ct.SoTien).Sum().GetValueOrDefault(),
                                  KhauTru = (from ct in db.ktttChiTiets
                                             join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                             join kt in db.ktttKhauTruThuTruocs on ct.MaCT equals kt.ID
                                             where hd.LinkID == tx.ID & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & SqlMethods.DateDiffDay(_TuNgay, kt.NgayCT) >= 0 & SqlMethods.DateDiffDay(kt.NgayCT, _DenNgay) >= 0 & hd.MaLDV == 6 & hd.IsDuyet == true
                                             select ct.SoTien).Sum().GetValueOrDefault(),
                                  //CuoiKy = (from hd in db.dvHoaDons
                                  //          where hd.LinkID == tx.ID & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.IsDuyet == true 
                                  //          & hd.PhaiThu.GetValueOrDefault()
                                  //          - (from pt in db.ptPhieuThus
                                  //             join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
                                  //             where
                                  //                 SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0 &
                                  //                 ptct.LinkID == hd.ID
                                  //             select ptct.SoTien).Sum().GetValueOrDefault()
                                  //                   - (from kt in db.ktttKhauTruThuTruocs
                                  //                      join ktct in db.ktttChiTiets on kt.ID equals ktct.MaCT
                                  //                      where
                                  //                          SqlMethods.DateDiffDay(kt.NgayCT, _DenNgay) >= 0 &
                                  //                          ktct.LinkID == hd.ID
                                  //                      select ktct.SoTien).Sum().GetValueOrDefault()

                                  //          > 0 & hd.MaLDV == 6
                                  //          select new
                                  //          {
                                  //              ConNo = hd.PhaiThu - (from pt in db.ptPhieuThus
                                  //                                    join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
                                  //                                    where
                                  //                                        SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0 &
                                  //                                        ptct.LinkID == hd.ID
                                  //                                    select ptct.SoTien).Sum().GetValueOrDefault()
                                  //                  - (from kt in db.ktttKhauTruThuTruocs
                                  //                     join ktct in db.ktttChiTiets on kt.ID equals ktct.MaCT
                                  //                     where
                                  //                         SqlMethods.DateDiffDay(kt.NgayCT, _DenNgay) >= 0 &
                                  //                         ktct.LinkID == hd.ID
                                  //                     select ktct.SoTien).Sum().GetValueOrDefault()
                                  //          }).Sum(p => p.ConNo).GetValueOrDefault()
                              };
                pvTheXe.DataSource = ltTheXe.Select(p=> new
                {p.SoThe,
                                  p.NgayDK,
                                  p.ChuThe,
                                  p.BienSo,
                                  p.DoiXe,
                                  p.MauXe,
                                  p.SoLuong,
                                  p.DonGia,
                                  p.TenLX,
                                  p.KyHieu,
                                  p.TenKH,
                                  p.MaSoMB,
                                  p.TenTL,
                                  p.TenKN,
                                  p.TenLMB,
                                  p.TenTN,
                                  p.DauKy,p.PhatSinh,p.KhauTru,p.DaThu,
                 CuoiKy = p.DauKy+ p.PhatSinh - p.DaThu - p.KhauTru
                });//.Where(p => p.PhatSinh != 0);
            }
            catch { }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        void Print()
        {
            var rpt = new rptCongNoTheXe(Common.User.MaTN.Value);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvTheXe.OptionsView.ShowColumnHeaders = false;
            pvTheXe.OptionsView.ShowDataHeaders = false;
            pvTheXe.OptionsView.ShowFilterHeaders = false;
            pvTheXe.SavePivotGridToStream(stream);
            pvTheXe.OptionsView.ShowColumnHeaders = true;
            pvTheXe.OptionsView.ShowDataHeaders = true;
            pvTheXe.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmCongNoTheXe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = DateTime.Now.Month + 8;
            itemKyBaoCao.EditValue = objKBC.Source[index];
            SetDate(index);

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(pvTheXe);
        }
    }
}