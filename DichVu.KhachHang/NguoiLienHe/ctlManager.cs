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
    public partial class ctlManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        byte SDBID = 0;

        void LienHeLoad()
        {
            gcLienHe.DataSource = null;
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;


                var idbieuMau = db.NguoiLienHes.Select(o => new
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
                                    where kh.MaTN == (byte?)itemToaNha.EditValue
                                    & SqlMethods.DateDiffDay(tuNgay, lh.NgayNhap) >= 0 & SqlMethods.DateDiffDay(lh.NgayNhap, denNgay) >= 0
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
                                          TenLBM = string.Join(", ", bm.loaiBieuMaus.Join(loaiBieuMau, pk => pk, fk => fk.MaLBM.ToString(), (pk, fk) => new { pk, fk}).Select( o=> o.fk.TenLBM).ToArray()),
                                          TenBM = string.Join(", ", bm.bieuMaus.Join(bieuMau, pk => pk, fk => fk.MaBM.ToString(), (pk, fk) => new { pk, fk}).Select( o=> o.fk.TenBM).ToArray())
                                      };
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
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog(this);
                if (frm.IsSave)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người liên hệ", "Thêm");
                    LienHeLoad();
                }
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
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog(this);
                if (frm.IsSave)
                {
                    var mahieu = gvLienHe.GetFocusedRowCellValue("MaHieu").ToString();
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người liên hệ", "Sửa", mahieu);
                    LienHeLoad();                
                }
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

            if (DialogBox.Question("Bạn có muốn xóa không?") == DialogResult.No) return;

            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    var lh = db.NguoiLienHes.Single(p => p.ID == (int?)gvLienHe.GetRowCellValue(i, "ID"));
                    db.NguoiLienHes.DeleteOnSubmit(lh);
                }
                var mahieu = gvLienHe.GetFocusedRowCellValue("MaHieu").ToString();
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người liên hệ", "Xóa", mahieu);
                db.SubmitChanges();
            }
            gvLienHe.DeleteSelectedRows();
        }

        void LienHeDetail()
        {
            try
            {
                var id = (int?)gvLienHe.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    switch (xtraTabControl1.SelectedTabPageIndex)
                    {
                        case 0:
                            break;
                    }
                }

                using (var db = new MasterDataContext())
                {
                    switch (xtraTabControl1.SelectedTabPageIndex)
                    {
                        case 0:
                            #region Chi tiet
                            var objNLH = (from lh in db.NguoiLienHes
                                          join nv in db.tnNhanViens on lh.MaNVN equals nv.MaNV
                                          join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH into kHang
                                          from kh in kHang.DefaultIfEmpty()
                                          where lh.ID == id
                                          select new
                                          {
                                              lh.ID,
                                              lh.HoTen,
                                              lh.GhiChu,
                                              lh.FaxCQ,
                                              lh.Email,
                                              lh.DienThoaiCQ,
                                              lh.DienThoai,
                                              lh.DiDongKhac,
                                              lh.DiDong,
                                              lh.DiaChi,
                                              lh.MaHieu,
                                              lh.TenCV,
                                              lh.TenPB,
                                              TenCongTy = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                              NhanVien = nv.HoTenNV
                                          }).First();
                            lblMaHieu.Text = string.Format("Mã hiệu: <b>{0}</b>", objNLH.MaHieu);
                            lblHoTen.Text = string.Format("Họ tên: <b>{0}</b>", objNLH.HoTen);
                            lblDiDong.Text = string.Format("Di động: <b>{0}</b>", objNLH.DiDong);
                            lblDiDongKhac.Text = string.Format("Di động khác: <b>{0}</b>", objNLH.DiDongKhac);
                            lblDienThoaiNR.Text = string.Format("Điện thoại NR: <b>{0}</b>", objNLH.DienThoai);
                            lblEmail.Text = string.Format("Email: <b>{0}</b>", objNLH.Email);
                            lblDoiTuong.Text = string.Format("Đối tượng: <b>{0}</b>", objNLH.TenCongTy);
                            lblChucDanh.Text = string.Format("Chức danh: <b>{0}</b>", objNLH.TenCV);
                            lblPhongBan.Text = string.Format("Phòng ban: <b>{0}</b>", objNLH.TenPB);
                            lblDienThoaiCQ.Text = string.Format("Điện thoại CQ: <b>{0}</b>", objNLH.DienThoaiCQ);
                            lblFaxCQ.Text = string.Format("Email CQ: <b>{0}</b>", objNLH.FaxCQ);
                            lblDiaChi.Text = string.Format("Địa chỉ: <b>{0}</b>", objNLH.DiaChi);
                            lblGhiChu.Text = string.Format("Ghi chú: <b>{0}</b>", objNLH.GhiChu);
                            lblNhanVien.Text = string.Format("Nhân viên: <b>{0}</b>", objNLH.NhanVien);
                            #endregion
                            break;
                    }
                }
            }
            catch { }
        }

        public ctlManager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            this.Load += new EventHandler(ctlManager_Load);
            cbbKyBC.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            gvLienHe.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvLienHe_FocusedRowChanged);
            gvLienHe.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gvLienHe_CustomDrawRowIndicator);
            btnCallPhone.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnCallPhone_ButtonClick);
        }

        void btnCallPhone_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var phone = (sender as ButtonEdit).Text ?? "";
                if (phone.Trim() == "") return;

                DIP.SwitchBoard.SwitchBoard.SoftPhone.Call(phone);
            }
            catch { }
        }
        
        void gvLienHe_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void gvLienHe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LienHeDetail();
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeDelete();
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeEdit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeAdd();
        }

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeLoad();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
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

        void ctlManager_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, HoTen = p.HoTenNV });
            }

            lkToaNha.DataSource = Common.TowerList;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();

            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }

            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
        }
    }
}
