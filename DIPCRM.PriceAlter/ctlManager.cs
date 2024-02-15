using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;
//using Library.Email;


namespace DIPCRM.PriceAlert
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraForm
    {
        byte SDBID = 0;

        private void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
			objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void BaoGia_Load()
        {
            var _maTN = (byte?)itemToaNha.EditValue;
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;

                lkToaNha.DataSource = Common.TowerList;

                if(itemToaNha.EditValue == null)
                    itemToaNha.EditValue = Common.User.MaTN;

                using (var db = new MasterDataContext())
                {
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
                                           where SqlMethods.DateDiffDay(tuNgay, bg.NgayBG) >= 0
                                           & SqlMethods.DateDiffDay(bg.NgayBG, denNgay) >= 0 //&& da.MaTN == _maTN
                                           & bg.MaTN == _maTN
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
                                               bg.MaNVN,
                                               bg.NgayNhap,
                                               bg.MaNVS,
                                               bg.NgaySua,
                                               nc.TenCH,
                                               kh.Email
                                           }).ToList();
                    grvBaoGia.FocusedRowHandle = -1;
                }
            }
            catch { }
            finally
            {
                //wait.Close();
            }
        }

        private void BaoGia_Add()
        {
            if (!itemAdd.Enabled) return;

            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte?)itemToaNha.EditValue;

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                   
                    BaoGia_Load();
                }
            }
        }

        private void BaoGia_Edit()
        {
            if (!itemEdit.Enabled) return;

            var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn báo giá");
                return;
            }
            using (var frm = new frmEdit())
            {
                frm.ID = id;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                   
                    BaoGia_Load();
                }
            }
            var SoBG = grvBaoGia.GetFocusedRowCellValue("SoBG").ToString();
        }

        private void BaoGia_Delete()
        {
            if (!itemDelete.Enabled) return;

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

                    var SoBG = grvBaoGia.GetFocusedRowCellValue("SoBG").ToString();
                    db.SubmitChanges();
                    Library.Utilities.NhuCauCls.TinhTiemNang(bg.MaNC);
                }
            }
            grvBaoGia.DeleteSelectedRows();
        }

        private void BaoGia_Click()
        {
            var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tpChiTietPhongGhe": 
                        gcSanPham.DataSource = null; 
                        break;
                    case "tpEmail":
                        //ctlMailHistory1.FormID = null;
                        //ctlMailHistory1.LinkID = null;
                        //ctlMailHistory1.MaKH = null;
                        //ctlMailHistory1.MailHistory_Load();
                        break;
                }
                return;
            }

            using (var db = new MasterDataContext())
            {
                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tpChiTietPhongGhe": 
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
                        break;
                    case "tpEmail":
                        //ctlMailHistory1.FormID = 238;
                        //ctlMailHistory1.LinkID = id;
                        //ctlMailHistory1.MaKH = (int?)grvBaoGia.GetFocusedRowCellValue("MaKH");
                        //ctlMailHistory1.MailHistory_Load(); 
                        break;
                }
            }
        }

        private void BaoGia_Print()
        {
            var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn báo giá");
                return;
            }

            var rpt = new rptDetail(id);
            rpt.ShowPrintPreview();
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách báo giá", "In");
        }

        private void BaoGia_SendMail()
        {
            //var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");

            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn báo giá");
            //    return;
            //}

            //using (var db = new MasterDataContext())
            //{
            //    var objBG = db.BaoGias.Single(o => o.ID == id);
            //    var objKH = db.tnKhachHangs.Single(o => o.MaKH == objBG.MaKH);
            //    List<Library.Mail.MailClient.EmailCls> Emails = new List<Library.Mail.MailClient.EmailCls>();
            //    Emails.Add(new Library.Mail.MailClient.EmailCls() { Email = objKH.Email, MaKH = objKH.MaKH });
            //    //using (var frm = new Library.Email.frmSendAmazon("DIPCRM.PriceAlert.ctlManager", 238, objBG.MaTN.Value, Emails))
            //    using (var frm = new Library.Email.frmSend("DIPCRM.PriceAlert.ctlManager", 238, objBG.MaTN.Value, Emails))
            //    {
            //        frm.LinkID = objBG.ID;
            //        frm.ShowDialog();
            //    }
            //}
        }

        public ctlManager()
        {
            InitializeComponent();

            ////Translate.Language.TranslateUserControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            this.Load += new EventHandler(ctlManager_Load);

            //ctlMailHistory1.frm = this;

            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
            itemDenNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
            cmbKyBaoCao.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemMail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSendMail_ItemClick);
            //itemCongTy.EditValueChanged += new EventHandler(itemCongTy_EditValueChanged);

            grvBaoGia.DoubleClick += new EventHandler(grvBaoGia_DoubleClick);
            grvBaoGia.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvBaoGia_FocusedRowChanged);
            grvBaoGia.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvBaoGia_CustomDrawRowIndicator);
            grvSanPham.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvSanPham_CustomDrawRowIndicator);
            xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);

            try
            {
                var db = new MasterDataContext();
                //var ltReport = (from rp in db.BmBieuMaus
                //                where rp.FormID == 6
                //                select new { rp.MaBM, rp.TenBM }).ToList();

                DevExpress.XtraBars.BarButtonItem itemPrint;

                //foreach (var i in ltReport)
                //{
                //    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.TenBM);
                //    itemPrint.Tag = i.MaBM;
                //    itemPrintBM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(ItemPrintBM_ItemClick);
                //    barManager1.Items.Add(itemPrint);
                //    itemPrintBM.ItemLinks.Add(itemPrint);
                //}
            }
            catch { }
                  
        }

        private void ItemPrintBM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = grvBaoGia.GetFocusedRowCellValue("SoBG");

            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn [Số Báo giá], xin cảm ơn.");
            //    return;
            //}

            //switch (e.Item.Tag)
            //{
            //    default:
            //        BieuMauHDCls.HopDong_Print(id.Value, (int)e.Item.Tag);
            //        break;

            //}
        }

        private void GvGhe_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void itemCongTy_EditValueChanged(object sender, EventArgs e)
        {
            BaoGia_Load();
        }

        void grvBaoGia_DoubleClick(object sender, EventArgs e)
        {
            BaoGia_Edit();
        }

        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            BaoGia_Click();
        }

        void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_SendMail();
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
            BaoGia_Click();
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

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoGia_Load();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoGia_Load();
        }

        void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoGia_Load();
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
            //this.SDBID = Common.Permission(barManager1, 107);

            using (var db = new MasterDataContext())
            {
                lookLoaiTien.DataSource = db.LoaiTiens;
                lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, HoTen = p.HoTenNV });
                cmbCongTy.DataSource = db.tnToaNhas.Select(p => new { ID = p.MaTN, TenCT = p.TenTN }).ToList();
            }

            //if (Common.getAccess(148) != 1)
            //{
            //    itemCongTy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    itemCongTy.EditValue = Common.ComID;
            //}

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void itemPrintBaoGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}

            //using (var frm = new Controls.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptDetail2(id);
            //    frm.ShowDialog();
            //}
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
