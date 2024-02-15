﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DichVu
{
    public partial class frmDSYeuCauSC : XtraForm
    {
        public frmDSYeuCauSC()
        {
            InitializeComponent();
        }
        MasterDataContext db = new MasterDataContext();
        void LoadData()
        {
            var data = (from dk in db.tnSuaChuaVatTus
                        join nv in db.tnNhanViens on dk.NVQLTraLoi equals nv.MaNV into nhanvien
                        from nv in nhanvien.DefaultIfEmpty()
                        join qld in db.tnNhanViens on dk.NVBQL equals qld.MaNV into bql
                        from qld in bql.DefaultIfEmpty()
                        join kt in db.tnNhanViens on dk.NVKT equals kt.MaNV into kythuat
                        from kt in kythuat.DefaultIfEmpty()
                        join mb in db.mbMatBangs on dk.MaMB equals mb.MaMB
                        join kh in db.tnKhachHangs on dk.MaKH equals kh.MaKH
                        where mb.MaTN == (byte?)itemToaNha.EditValue
                        select new
                        {
                            dk.ID,
                            TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen,
                            MaSoMB = mb.MaSoMB,
                            KHYeuCau = dk.KhGhiChu,
                            NgayHen = dk.NgayKHYeuCau,
                            KHDuyet = dk.KHDuyet == true ? "Đã duyệt" : "Chưa duyệt",
                            NgayTao = dk.NgayTao,
                            KHTraLoi = dk.KHTraLoi,
                            BQLTraLoi = dk.BQLTraLoi,
                            NVTraLoi = nv.HoTenNV,
                            NgayBQLTraLoi = dk.NgayBQLTraLoi,
                            BQLDuyet = dk.BQLDuyet == true ? "Đã duyệt" : "Chưa duyệt",
                            NgayBQLDuyet = dk.NgayBQLDuyet,
                            NgayKHDuyet = dk.KHNgayDuyet,
                            PhaiTra = dk.PhaiTra > 0 ? dk.PhaiTra : 0,
                            NVDuyet = qld.HoTenNV,
                            KTDuyet = dk.KTDuyet == true ? "Hoàn thành" : " ",
                            NgayKTDuyet = dk.NgayKTDuyet,
                            KTTraLoi = dk.KTTraLoi,
                            NVKT = kt.HoTenNV,
                            XacNhanHT = dk.KHXacNhanHoanThanh == true ? "Hoàn thành" : " ",
                        });
            gc.DataSource = data;
        }

        void LoadDetail()
        {
            var check = (int?)gv.GetFocusedRowCellValue("ID");
            var detail = (from ct in db.tnSuaChuaVatTu_ChiTiets
                          where ct.IDSuaChua == check
                          select new
                          {
                              ct.STT,
                              ct.VatPham,
                              ct.SoLuong,
                              SoLuongThucTe = ct.SoLuongThucTe == null ? ct.SoLuong : ct.SoLuongThucTe,
                              ct.DonGia,
                              ct.ThanhTien,
                              ct.TyLeCK,
                              ct.TyleVAT,
                              ct.TienVAT,
                              ct.TienCK,
                              ct.TongTien,
                              ct.KHGhiChu,
                              ct.KTGhiChu,
                              ct.BQLGhiChu

                          });
            gridControl1.DataSource = detail;
        }
        private void frmDSYeuCauSC_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.TowerList.First().MaTN;
            LoadData();
            barManager1.SetPopupContextMenu(gc, popupMenu1);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                DialogResult dialogResult = MessageBox.Show("Xác nhận xóa?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var check = db.tnSuaChuaVatTus.SingleOrDefault(p => p.ID == ID);
                    if (check != null)
                    {
                        var chitiet = db.tnSuaChuaVatTu_ChiTiets.Where(p => p.IDSuaChua == check.ID);
                        db.tnSuaChuaVatTu_ChiTiets.DeleteAllOnSubmit(chitiet);
                        db.tnSuaChuaVatTus.DeleteOnSubmit(check);
                        db.SubmitChanges();
                        LoadData();
                    }
                }
            }
            else
            {
                DialogBox.Error("Vui lòng chọn dòng");
            }
        }

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var Done1 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["XacNhanHT"]);
                var Done2 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["KTDuyet"]);
                var Done3 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["BQLDuyet"]);
                if (Done1 != "Hoàn thành" && Done2 == "Hoàn thành")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Green);
                }
                else if (Done2 != "Hoàn thành" && Done3 == "Đã duyệt")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Yellow);
                }
                else if (Done3 != "Đã duyệt")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                }
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void btnBaoGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var matn = (byte)itemToaNha.EditValue;
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                frmBaoGiaSC frm = new frmBaoGiaSC();
                frm.IDToaNha = matn;
                frm.ID = ID;
                frm.ShowDialog();
                LoadData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dòng!");
            }
        }

        private void btnBQLDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var matn = (byte)itemToaNha.EditValue;
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                var check = db.tnSuaChuaVatTus.SingleOrDefault(p => p.ID == ID && p.KHDuyet == true);
                if (check != null)
                {
                    frmBQLDuyet frm = new frmBQLDuyet();
                    frm.IDToaNha = matn;
                    frm.ID = ID;
                    frm.ShowDialog();
                    LoadData();
                }
                else
                {
                    DialogBox.Alert("Không thể duyệt vì khách hàng chưa duyệt báo giá!");
                }

            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dòng!");
            }
        }

        private void btnKyThuatDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var matn = (byte)itemToaNha.EditValue;
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                var check = db.tnSuaChuaVatTus.SingleOrDefault(p => p.ID == ID && p.BQLDuyet == true);
                if (check != null)
                {
                    frmKyThuatXacNhan frm = new frmKyThuatXacNhan();
                    frm.IDToaNha = matn;
                    frm.ID = ID;
                    frm.ShowDialog();
                    LoadData();
                }
                else
                {
                    DialogBox.Alert("Không thể duyệt vì BQL chưa xác nhận!");
                }

            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dòng!");
            }
        }
    }
}