﻿using System;
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

namespace LandSoftBuilding.Receivables
{
    public partial class frmManagerKiemTra : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH { get; set; }
        public int LinkID { get; set; }
        public int MaLDV { get; set; }
        public frmManagerKiemTra()
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

        //void LoadData()
        //{
        //    gcHoaDon.DataSource = null;
        //    gcHoaDon.DataSource = linqInstantFeedbackSource1;
        //}

        void RefreshData()
        {
            //linqInstantFeedbackSource1.Refresh();
            LoadData();
        }

        void AddRecord()
        {
            using (var frm = new frmAddAuto())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void AddNew()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            //if (_MaTN == 34 || _MaTN == 39 || _MaTN == 40)
            //{
            //    using (var frm = new frmEditPMT())
            //    {
            //        frm.MaTN = _MaTN;
            //        frm.ShowDialog();
            //        if (frm.DialogResult == DialogResult.OK)
            //            LoadData();
            //    }
            //}
            //else
            //{
                using (var frm = new frmEdit())
                {
                    frm.MaTN = _MaTN;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
                }
            //}
        }

        void AddMulti()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var frm = new frmAddMulti())
            {
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void Edit()
        {
            if (gvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn bản ghi, xin cảm ơn.");
                return;
            }

            if ((bool)gvCongNo.GetFocusedRowCellValue("IsDuyet") == true)
            {
                DialogBox.Error("Hóa đơn đã duyệt, không thể sửa");
                return;
            }
            var maTN = (byte)itemToaNha.EditValue;
            //if (maTN == 34 || maTN == 39 || maTN == 40)
            //{
            //    var f = new frmEditPMT();
            //    f.MaTN = (byte)itemToaNha.EditValue;
            //    f.ID = (long)gvHoaDon.GetFocusedRowCellValue("ID");
            //    f.ShowDialog();
            //    if (f.DialogResult == DialogResult.OK)
            //        LoadData();
            //}
            //else
            //{
                var f = new frmEdit();
                f.MaTN = (byte)itemToaNha.EditValue;
                f.ID = (long)gvCongNo.GetFocusedRowCellValue("ID");
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                    LoadData();
            //}
        }

        void DeleteRecord()
        {
            var indexs = gvCongNo.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var hd = db.dvHoaDons.FirstOrDefault(p => p.ID == (long)gvCongNo.GetRowCellValue(i, "ID") & p.DaThu.GetValueOrDefault() <= 0 & p.IsDuyet.GetValueOrDefault() == false);
                if (hd != null)
                {
                    #region Luu lai LS xoa hoa don Phu Luc XuanMai
                    //if ((byte?)itemToaNha.EditValue == 36)
                    //{
                        var HDDX = new dvHoaDonDaXoa();
                        HDDX.ConNo = hd.ConNo;
                        HDDX.DaThu = hd.DaThu;
                        HDDX.DenNgay = hd.DenNgay;
                        HDDX.DienGiai = hd.DienGiai;
                        HDDX.IsDuyet = hd.IsDuyet;
                        HDDX.KyTT = hd.KyTT;
                        HDDX.LinkID = hd.LinkID;
                        HDDX.MaDVTG = hd.MaDVTG;
                        HDDX.MaKH = hd.MaKH;
                        HDDX.MaMB = hd.MaMB;
                        HDDX.MaLDV = hd.MaLDV;
                        HDDX.MaNVN = Common.User.MaNV;
                        HDDX.NgayNhap = DateTime.Now;
                        HDDX.NgayTT = hd.NgayTT;
                        HDDX.MaTN = hd.MaTN;
                        HDDX.PhaiThu = hd.PhaiThu;
                        HDDX.TableName = hd.TableName;
                        HDDX.PhiDV = hd.PhiDV;
                        HDDX.TienCK = hd.TienCK;
                        HDDX.TienTT = hd.TienTT;
                        HDDX.TuNgay = hd.TuNgay;
                        HDDX.TyLeCK = hd.TyLeCK;
                        db.dvHoaDonDaXoas.InsertOnSubmit(HDDX);


                    //}
                    #endregion
                    db.dvHoaDons.DeleteOnSubmit(hd);
                }
            }

            try
            {
                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void Payment()
        {
            using (var frm = new frmPayment())
            {
                frm.MaKH = (int?)gvCongNo.GetFocusedRowCellValue("MaKH");
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.RefreshData();
                }
            }
        }

        void ImportRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        void Duyet(bool isDuyet)
        {
            var rows = gvCongNo.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    if ((bool?)gvCongNo.GetRowCellValue(i, "IsDuyet") == isDuyet) continue;

                    var objHD = db.dvHoaDons.Single(p => p.ID == (long)gvCongNo.GetRowCellValue(i, "ID"));
                    objHD.IsDuyet = isDuyet;
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void DuyetAll(bool isDuyet)
        {
            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var _MaTN = (byte)itemToaNha.EditValue;

                var ltHoaDon = from hd in db.dvHoaDons
                               where hd.MaTN == _MaTN & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 
                               & hd.DaThu.GetValueOrDefault() == 0 & hd.IsDuyet.GetValueOrDefault() != isDuyet
                               select hd;

                foreach (var hd in ltHoaDon)
                    hd.IsDuyet = isDuyet;

                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void Load_PhieuThu()
        {
            var db = new MasterDataContext();
            try
            { 
                var id = (long?)gvCongNo.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                //gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                        where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                        select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList();
                //;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}
                
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            gvCongNo.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[5];
            SetDate(5);

            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            if ((byte?)itemToaNha.EditValue !=null )
            {
                if ((byte)itemToaNha.EditValue==36)
                gvCongNo.Columns[21].Visible = true;
                else
                {
                    gvCongNo.Columns[21].Visible = false;
                }
            }
            
            
            gcCongNo.DataSource = from hd in db.dvHoaDons
                                join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                from mb in tblMatBang.DefaultIfEmpty()
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                from tl in tblTangLau.DefaultIfEmpty()
                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                from kn in tblKhoiNha.DefaultIfEmpty()
                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                from lmb in tblLoaiMatBang.DefaultIfEmpty()
                                where //hd.MaTN == matn //& SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                               // & 
                                hd.LinkID == this.LinkID & hd.MaKH == this.MaKH & hd.MaLDV == this.MaLDV
                                  orderby kn.TenKN ascending,tl.STT ascending,mb.MaMB ascending
                                select new
                                {
                                    hd.ID,
                                    hd.NgayTT,
                                    hd.MaKH,
                                    kh.KyHieu,
                                    kh.MaPhu,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    TenLDV = l.TenHienThi,
                                    hd.DienGiai,
                                    hd.PhiDV,
                                    hd.KyTT,
                                    hd.TienTT,
                                    hd.TyLeCK,
                                    hd.TienCK,
                                    hd.PhaiThu,
                                    DaThu = (from ct in db.ptChiTietPhieuThus
                                             join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                             join hdct in db.dvHoaDons on ct.LinkID equals hdct.ID into tam
                                             from hdct in tam.DefaultIfEmpty()
                                             where ct.LinkID == hd.ID & ct.TableName == "dvHoaDon"
                                             select ct.SoTien).Sum().GetValueOrDefault(),
                                    ConNo =
                                        hd.PhaiThu
                                        - ((from ct in db.ptChiTietPhieuThus
                                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                            where
                                                ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID //&
                                               //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0
                                            select ct.SoTien).Sum().GetValueOrDefault()),
                                    hd.IsDuyet,
                                    mb.MaSoMB,
                                    tl.TenTL,
                                    kn.TenKN,
                                    lmb.TenLMB,
                                    NgayThu=(from ct in db.ptChiTietPhieuThus
                                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                            where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                             select  pt.NgayThu ).FirstOrDefault(),
                                };
            
        }
        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from hd in db.dvHoaDons
                                join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                from mb in tblMatBang.DefaultIfEmpty()
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                from tl in tblTangLau.DefaultIfEmpty()
                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                from kn in tblKhoiNha.DefaultIfEmpty()
                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                from lmb in tblLoaiMatBang.DefaultIfEmpty()
                                where hd.MaTN == matn & SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                                orderby kn.TenKN,tl.TenTL,mb.MaMB ascending //, hd.NgayTT descending
                                select new
                                {
                                    hd.ID,
                                    hd.NgayTT,
                                    hd.MaKH,
                                    kh.KyHieu,
                                    kh.MaPhu,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    TenLDV = l.TenHienThi,
                                    hd.DienGiai,
                                    hd.PhiDV,
                                    hd.KyTT,
                                    hd.TienTT,
                                    hd.TyLeCK,
                                    hd.TienCK,
                                    hd.PhaiThu,
                                    hd.DaThu,
                                    hd.ConNo,
                                    hd.IsDuyet,
                                    mb.MaSoMB,
                                    tl.TenTL,
                                    kn.TenKN,
                                    lmb.TenLMB
                                };
            e.Tag = db;
        }

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Payment();
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Duyet(true);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Duyet(false);
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddNew();
        }

        private void itemDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DuyetAll(true);
        }

        private void itemKhongDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DuyetAll(false);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcCongNo);
        }

        private void itemAddMulti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddMulti();
        }

        private void gvHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Load_PhieuThu();
        }

        private void gvHoaDon_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Load_PhieuThu();
        }
    }
}