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

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class ctlContact : DevExpress.XtraEditors.XtraUserControl
    {
        public int? MaKH { get; set; }
        public byte? MaTN { get; set; }

        public System.Windows.Forms.Form frm { get; set; }


        public void LienHeLoad()
        {
            if (this.MaKH == null)
            {
                gcLienHe.DataSource = null;
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {

                    var idbieuMau = db.NguoiLienHes.Where( o=> o.MaKH == MaKH).Select(o => new
                    {
                        o.ID,
                        loaiBieuMaus = (o.idLoaiBieuMaus ?? "").Replace(" ", "").Split(','),
                        bieuMaus = (o.idBieuMaus ?? "").Replace(" ", "").Split(','),
                    }).ToList();

                    var bieuMau = db.BmBieuMaus.ToList();

                    var loaiBieuMau = db.BmLoaiBieuMaus.ToList();

                    var nguoiLienHes = (from lh in db.NguoiLienHes
                                        join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH
                                        join nv in db.tnNhanViens on lh.MaNVN equals nv.MaNV
                                        where lh.MaKH == MaKH
                                        select new
                                        {
                                            lh.ID,
                                            lh.MaHieu,
                                            kh.KyHieu,
                                            lh.HoTen,
                                            lh.DiDong,
                                            lh.DiDongKhac,
                                            lh.DienThoai,
                                            lh.Email,
                                            lh.TenCV,
                                            lh.DienThoaiCQ,
                                            TenCongTy = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                            lh.GhiChu,
                                            lh.NgayNhap,
                                            lh.MaNVN,
                                            lh.IsMail,
                                            lh.IsLock,
                                        }).ToList();

                    gcLienHe.DataSource = from lh in nguoiLienHes
                                          join bm in idbieuMau on lh.ID equals bm.ID
                                          select new
                                          {
                                              lh.ID,
                                              lh.MaHieu,
                                              lh.HoTen,
                                              lh.DiDong,
                                              lh.DiDongKhac,
                                              lh.DienThoai,
                                              lh.Email,
                                              lh.TenCV,
                                              lh.DienThoaiCQ,
                                              lh.TenCongTy,
                                              lh.GhiChu,
                                              lh.NgayNhap,
                                              lh.MaNVN,
                                              lh.IsMail,
                                              lh.IsLock,
                                              TenLBM = string.Join(", ", bm.loaiBieuMaus.Join(loaiBieuMau, pk => pk, fk => fk.MaLBM.ToString(), (pk, fk) => new { pk, fk }).Select(o => o.fk.TenLBM).ToArray()),
                                              TenBM = string.Join(", ", bm.bieuMaus.Join(bieuMau, pk => pk, fk => fk.MaBM.ToString(), (pk, fk) => new { pk, fk }).Select(o => o.fk.TenBM).ToArray())
                                          };
                }

            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void LienHeAdd()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = this.MaTN;
                frm.MaKH = this.MaKH;
                frm.ShowDialog(this);
                if (frm.IsSave)
                    LienHeLoad();
            }
        }

        void LienHeEdit()
        {
            var id = (int?)gvLienHe.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }
            using (var frm = new frmEdit())
            {
                frm.ID = id;
                frm.ShowDialog(this);
                if (frm.IsSave)
                    LienHeLoad();
            }
        }

        void LienHeDelete()
        {
            var indexs = gvLienHe.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }
            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;
            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    var lh = db.NguoiLienHes.Single(p => p.ID == (int)gvLienHe.GetRowCellValue(i, "ID"));
                    db.NguoiLienHes.DeleteOnSubmit(lh);
                }
                //gvLienHe.DeleteSelectedRows();
                db.SubmitChanges();
            }
            
        }

        public ctlContact()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctlContact_Load);
            itemAddNLH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEditNLH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDeleteNLH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            gvLienHe.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gvLienHe_CustomDrawRowIndicator);
        }

        void gvLienHe_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        
        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeDelete();
            LienHeLoad();
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeEdit();
            LienHeLoad();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeAdd();
            LienHeLoad();
        }

        void ctlContact_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
            using (var db = new MasterDataContext())
            {
                lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, HoTen = p.HoTenNV });
            }
        }
    }
}
