using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using Library;


namespace DIPCRM.PriceAlert
{
    public partial class ctlBaoGia : DevExpress.XtraEditors.XtraUserControl
    {
        public int? MaKH { get; set; }
        public int? MaNC { get; set; }

        public System.Windows.Forms.Form frm { get; set; }

        public void BaoGia_Load()
        {
            try
            {
                if (this.MaKH == null && this.MaNC == null)
                {
                    gcBaoGia.DataSource = null;
                    gcSanPham.DataSource = null;
                    return;
                }

                using (var db = new MasterDataContext())
                {
                    //#L
                    //gcBaoGia.DataSource = from bg in db.BaoGias
                    //                      join kh in db.tnKhachHangs on bg.MaKH equals kh.MaKH
                    //                      join nv in db.tnNhanViens on bg.MaNV equals nv.MaNV
                    //                      join n in db.tnNhanViens on bg.MaNVN equals n.MaNV into nvn
                    //                      from n in nvn.DefaultIfEmpty()
                    //                      join s in db.tnNhanViens on bg.MaNVS equals s.MaNV into nvs
                    //                      from s in nvs.DefaultIfEmpty()
                    //                      where (bg.MaNC == this.MaNC | this.MaNC == null)
                    //                      & (bg.MaKH == this.MaKH | this.MaKH == null)
                    //                      select new
                    //                      {
                    //                          bg.ID,
                    //                          bg.SoBG,
                    //                          bg.NgayBG,
                    //                          bg.NgayYC,
                    //                          bg.MaDA,
                    //                          //TenCT = da.TenDA,
                    //                          HoTenNVBG = nv.HoTenNV,
                    //                          bg.MaKH,
                    //                          kh.Email,
                    //                          bg.DKGH,
                    //                          bg.DKTT,
                    //                          bg.ThoiHanBG,
                    //                          SoTien = db.bgSanPhams.Sum(p => p.SoTien),
                    //                          //TenLT = lt.TenVT,
                    //                          bg.TieuDe,
                    //                          bg.GhiChu,
                    //                          HoTenNVN = n.HoTenNV,
                    //                          bg.NgayNhap,
                    //                          HoTenNVS = s.HoTenNV,
                    //                          bg.NgaySua
                    //                      };

                    gcBaoGia.DataSource = (from bg in db.BaoGias
                                           join kh in db.tnKhachHangs on bg.MaKH equals kh.MaKH into dskh
                                           from kh in dskh.DefaultIfEmpty()
                                           //join lh in db.NguoiLienHes on kh.MaNLH equals lh.ID into lhe
                                           //from lh in lhe.DefaultIfEmpty()
                                           join da in db.tnToaNhas on bg.MaDA equals da.MaTN into duan
                                           from da in duan.DefaultIfEmpty()
                                           join nv in db.tnNhanViens on bg.MaNV equals nv.MaNV into dsnv
                                           from nv in dsnv.DefaultIfEmpty()
                                           join nc in db.ncNhuCaus on bg.MaNC equals nc.MaNC into dsnc
                                           from nc in dsnc.DefaultIfEmpty()
                                           orderby bg.NgayBG descending
                                           where (bg.MaNC == this.MaNC | this.MaNC == null)
                                               &&(bg.MaKH == this.MaKH | this.MaKH == null)
                                           select new
                                           {
                                               ID = bg.ID,
                                               bg.SoBG,
                                               bg.NgayBG,
                                               bg.NgayYC,
                                               TenCT = da.TenTN,
                                               bg.MaKH,
                                               TenCongTy = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                               //HoTenNLH = lh.HoTen,
                                               EmailKH = kh.Email,
                                               bg.MaNV,
                                               bg.DKGH,
                                               bg.DKTT,
                                               bg.ThoiHanBG,
                                               SoTien = bg.bgSanPhams.Sum(p => p.SoTien),
                                               bg.MaLT,
                                               bg.TieuDe,
                                               bg.GhiChu,
                                               //bg.MaNVN,
                                               //bg.NgayNhap,
                                               //bg.MaNVS,
                                               //bg.NgaySua,
                                               nc.TenCH,
                                               kh.Email
                                           }).ToList();

                    if (grvBaoGia.FocusedRowHandle == 0) grvBaoGia.FocusedRowHandle = -1;
                }
            }
            catch { }
        }

        private void BaoGia_Add()
        {
            using (var frm = new frmEdit())
            {
                frm.MaKH = this.MaKH;
                frm.MaNC = this.MaNC;
                frm.ShowDialog(this);
            }
        }

        private void BaoGia_Edit()
        {
            var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn báo giá");
                return;
            }
            using (var frm = new frmEdit())
            {
                frm.ID = id;
                frm.MaNC = this.MaNC;
                if (frm.ShowDialog() == DialogResult.OK) 
                    BaoGia_Load();
            }
        }

        private void BaoGia_Delete()
        {
            var indexs = grvBaoGia.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn báo giá");
                return;
            }
            if (DialogBox.Question("Bạn có chắc chắn không?") == DialogResult.No) return;

            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    var bg = db.BaoGias.Single(p => p.ID == (int)grvBaoGia.GetRowCellValue(i, "ID"));
                    db.BaoGias.DeleteOnSubmit(bg);
                    db.SubmitChanges();
                    Library.Utilities.NhuCauCls.TinhTiemNang(bg.MaNC);

                }
                db.SubmitChanges();
            }
          
        }

        private void BaoGia_Print()
        {
            //var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn báo giá");
            //    return;
            //}

            //using (var frm = new Controls.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptDetail2(id);
            //    frm.ShowDialog();
            //}
        }

        private void BaoGia_SendMail()
        {
            //var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");

            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn báo giá");
            //    return;
            //}

            //using(var db = new MasterDataContext())
            //{
            //    var objBG = db.BaoGias.Single( o=> o.ID == id );
            //    var objKH = db.tnKhachHangs.Single(o => o.MaKH == objBG.MaKH);
            //    List<Library.Mail.MailClient.EmailCls> Emails = new List<Library.Mail.MailClient.EmailCls>();
            //    Emails.Add(new Library.Mail.MailClient.EmailCls() { Email = objKH.Email, MaKH = objKH.MaKH });
            //    using (var frm = new Library.Email.frmSend("DIPCRM.PriceAlert.ctlManager", 238, objBG.MaTN.Value, Emails))
            //    {
            //        frm.LinkID = objBG.ID;
            //        frm.ShowDialog();
            //    }
            //}
        }

        private void Permission()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    //var listAction = db.ActionDatas.Where(p => p.FormID == 107 & p.PerID == Common.PerID).Select(p => p.FeatureID).ToList();
                    //itemAdd.Enabled = listAction.Contains(1);
                    //itemEdit.Enabled = listAction.Contains(2);
                    //itemDelete.Enabled = listAction.Contains(3);
                    //itemPrint.Enabled = listAction.Contains(4);
                }
            }
            catch
            {

            }
        }

        public ctlBaoGia()
        {
            InitializeComponent();

            Permission();

            grvBaoGia.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvBaoGia_FocusedRowChanged); 
            grvBaoGia.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvBaoGia_CustomDrawRowIndicator);
            grvSanPham.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvSanPham_CustomDrawRowIndicator);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
            itemSendMail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSendMail_ItemClick);

            using (var db = new MasterDataContext())
            {
                lookLoaiTien.DataSource = db.LoaiTiens;
                lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, HoTen = p.HoTenNV });
            }
        }

        void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_SendMail();
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_Print();
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_Delete();
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_Edit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_Add();
        }

        void grvSanPham_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void grvBaoGia_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void grvBaoGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
                //gcSanPham.DataSource = (from ct in db.bgSanPhams
                //                        join sp in db.SanPhams on ct.MaSP equals sp.ID
                //                        join x in db.XuatXus on sp.MaXX equals x.MaXX into xl
                //                        from xx in xl.DefaultIfEmpty()
                //                        join d in db.DonViTinhs on sp.MaDVT equals d.MaDVT into dv
                //                        from dvt in dv.DefaultIfEmpty()
                //                        where ct.MaBG == id
                //                        select new
                //                        {
                //                            sp.ID,
                //                            sp.MaSP,
                //                            sp.TenSP,
                //                            xx.TenXX,
                //                            dvt.TenDVT,
                //                            ct.SoLuong,
                //                            ct.DonGia,
                //                            ct.ThueGTGT,
                //                            ct.ThanhTien
                //                        }).ToList();

                gcSanPham.DataSource = (from ct in db.bgSanPhams
                                        join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                        join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                        where ct.MaBG == id
                                        select new
                                        {
                                            MaSP = mb.MaMB,
                                            TenSP = mb.MaSoMB,
                                            //LoaiCT = mb.IsGhe.GetValueOrDefault() ? "Ghế" : "Phòng",
                                            LoaiCT = "Căn hộ",
                                            mb.DienGiai,
                                            ct.SoLuong,
                                            ct.DonGia,
                                            ct.ThueGTGT,
                                            ct.ThanhTien,
                                            ct.TienGTGT,
                                            ct.TyLeCK,
                                            ct.TienCK,
                                            ct.TienDaCK,
                                            ct.SoTien
                                        }).ToList();
            }
        }

        private void ctlBaoGia_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
        }
    }
}
