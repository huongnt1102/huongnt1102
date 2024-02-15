using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;


namespace DIPCRM.PriceAlert.Template
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        byte SDBID = 0;
        public System.Windows.Forms.Form frm { get; set; }
        private void BaoGia_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {

                using (var db = new MasterDataContext())
                {
                    //gcBaoGia.DataSource = (from bg in db.bgBieuMaus
                    //                       join da in db.DuAns on bg.MaDA equals da.ID into duan
                    //                       from da in duan.DefaultIfEmpty()
                    //                       join nv in db.tnNhanViens on bg.MaNV equals nv.MaNV
                    //                       select new
                    //                       {
                    //                           bg.ID,
                    //                           TenCT = da.TenDA,
                    //                           bg.MaNV,
                    //                           bg.DKGH,
                    //                           bg.DKTT,
                    //                           bg.ThoiHanBG,
                    //                           bg.MaLT,
                    //                           bg.TieuDe,
                    //                           bg.GhiChu,
                    //                           bg.MaNVN,
                    //                           bg.NgayNhap,
                    //                           bg.MaNVS,
                    //                           bg.NgaySua
                    //                       }).ToList();

                    if (grvBaoGia.FocusedRowHandle == 0) grvBaoGia.FocusedRowHandle = -1;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void BaoGia_Add()
        {
            if (!itemAdd.Enabled) return;

            using (var frm = new frmEdit())
            {
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                    BaoGia_Load();
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
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                    BaoGia_Load();
            }
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
                    //var bg = db.bgBieuMaus.Single(p => p.ID == (int)grvBaoGia.GetRowCellValue(i, "ID"));
                    //db.bgBieuMaus.DeleteOnSubmit(bg);
                }
                db.SubmitChanges();
            }
            grvBaoGia.DeleteSelectedRows();
        }

        private void BaoGia_Click()
        {
            var id = (int?)grvBaoGia.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0: gcSanPham.DataSource = null; break;
                }
                return;
            }

            using (var db = new MasterDataContext())
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        //gcSanPham.DataSource = (from ct in db.bgbmSanPhams
                        //                        join bm in db.bgBieuMaus on ct.MaBM equals bm.ID
                        //                        join ltbm in db.LoaiTiens on bm.MaLT equals ltbm.MaLT
                        //                        join sp in db.SanPhams on ct.MaSP equals sp.ID
                        //                        join ltsp in db.LoaiTiens on sp.MaLT equals ltsp.MaLT
                        //                        join x in db.XuatXus on sp.MaXX equals x.MaXX into xl
                        //                        from xx in xl.DefaultIfEmpty()
                        //                        join d in db.DonViTinhs on sp.MaDVT equals d.MaDVT into dv
                        //                        from dvt in dv.DefaultIfEmpty()
                        //                        where ct.MaBM == id
                        //                        select new
                        //                        {
                        //                            sp.ID,
                        //                            sp.MaSP,
                        //                            sp.TenSP,
                        //                            sp.DienGiai,
                        //                            xx.TenXX,
                        //                            dvt.TenDVT,
                        //                            ct.SoLuong,
                        //                            DonGia = sp.GiaBan * ltsp.TyGia / ltbm.TyGia,
                        //                            sp.ThueGTGT,
                        //                            ThanhTien = sp.GiaBan * ltsp.TyGia / ltbm.TyGia * ct.SoLuong * (1 + sp.ThueGTGT)
                        //                        }).ToList();
                        break;
                }
            }
        }

        public ctlManager()
        {
            InitializeComponent();
            
            ////Translate.Language.TranslateUserControl(this, barManager1);

            this.Load += new EventHandler(ctlManager_Load);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);

            grvBaoGia.DoubleClick += new EventHandler(grvBaoGia_DoubleClick);
            grvBaoGia.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvBaoGia_FocusedRowChanged);
            grvBaoGia.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvBaoGia_CustomDrawRowIndicator);
            grvSanPham.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvSanPham_CustomDrawRowIndicator);
        }

        void grvBaoGia_DoubleClick(object sender, EventArgs e)
        {
            BaoGia_Edit();
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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

        void ctlManager_Load(object sender, EventArgs e)
        {
            //this.SDBID = Common.Permission(barManager1, 133);
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
            using (var db = new MasterDataContext())
            {
                lookLoaiTien.DataSource = db.LoaiTiens;
                lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, HoTen = p.HoTenNV });
            }

            BaoGia_Load();
        }
    }
}
