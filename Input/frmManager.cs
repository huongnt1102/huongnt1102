﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using BuildingDesignTemplate;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
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


        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            var objData = from p in db.ptPhieuThus
                                join mb in db.mbMatBangs on p.MaMB equals mb.MaMB into mbang
                                from mb in mbang.DefaultIfEmpty()
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tlau
                                from tl in tlau.DefaultIfEmpty()
                                join pl in db.ptPhanLoais on p.MaPL equals (int?)pl.ID into tblPhanLoai
                                from pl in tblPhanLoai.DefaultIfEmpty()
                                join k in db.tnKhachHangs on p.MaKH equals (int?)k.MaKH into tblKhachHang
                                from k in tblKhachHang.DefaultIfEmpty()
                                join nkh in db.khNhomKhachHangs on k.MaNKH equals (int?)nkh.ID into tblNhomKhachHang
                                from nkh in tblNhomKhachHang.DefaultIfEmpty()
                                join nv in db.tnNhanViens on p.MaNV equals (int?)nv.MaNV into nvchinh
                                from nv in nvchinh.DefaultIfEmpty()
                                join nvn in db.tnNhanViens on p.MaNVN equals (int?)nvn.MaNV into nvnhap
                                from nvn in nvnhap.DefaultIfEmpty()
                                join nvs in db.tnNhanViens on p.MaNVS equals (int?)nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join tk in db.nhTaiKhoans on p.MaTKNH equals (int?)tk.ID into tblTaiKhoan
                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                join nh in db.nhNganHangs on tk.MaNH equals (int?)nh.ID into tblNganHang
                                from nh in tblNganHang.DefaultIfEmpty()
                                join nt in db.ptPhieuThu_NguonThanhToans on p.NguonThanhToan equals nt.ID
                                where ((int?)p.MaTN == (int?)((int)maTN) & SqlMethods.DateDiffDay((DateTime?)tuNgay, p.NgayThu) >= (int?)0 & SqlMethods.DateDiffDay(p.NgayThu, (DateTime?)denNgay) >= (int?)0) & p.IsKhauTru == false & p.MaPL!=24
                                //&& (p.MaPL != (int?)2 || (p.MaPL == (int?)2 && p.ptChiTietPhieuThus.FirstOrDefault((ptChiTietPhieuThu g) => g.LinkID == null) != null)) 
                                select new
                                {
                                    ID = p.ID,
                                    SoPT = p.SoPT,
                                    NgayThu = p.NgayThu,
                                    KyHieu = k.KyHieu,
                                    TenKH = ((k.IsCaNhan == (bool?)true) ? (k.HoKH + " " + k.TenKH) : k.CtyTen),
                                    NguoiThu = nv.HoTenNV,
                                    NguoiNop = p.NguoiNop,
                                    DiaChiNN = p.DiaChiNN,
                                    LyDo = p.LyDo,
                                    TenPL = pl.TenPL,
                                    ChungTuGoc = p.ChungTuGoc,
                                    NguoiNhap = nvn.HoTenNV,
                                    NgayNhap = p.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    NgaySua = p.NgaySua,
                                    PhuongThuc = ((p.MaTKNH != null) ? "Chuyển khoản" : "Tiền mặt"),
                                    SoTK = tk.SoTK,
                                    TenNH = nh.TenNH,
                                    TenNKH = nkh.TenNKH,
                                    TenKN = tl.mbKhoiNha.TenKN,
                                    SoTienThu = p.ptChiTietPhieuThus.Sum(ct => ct.SoTien.GetValueOrDefault()),
                                    TienPhaiThu = p.ptChiTietPhieuThus.Sum(ct => ct.PhaiThu.GetValueOrDefault()),
                                    TienKhauTru = p.ptChiTietPhieuThus.Sum(ct => ct.KhauTru.GetValueOrDefault()),
                                    TienThuThua = p.ptChiTietPhieuThus.Sum(ct => ct.ThuThua.GetValueOrDefault()),
                                   NguonThu=nt.Name,
                                } into p
                                select new
                                {
                                    ID = p.ID,
                                    SoPT = p.SoPT,
                                    NgayThu = p.NgayThu, //p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0') + " | " + p.NgayThu.Value.Day.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Month.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Year.ToString(),
                                    GioThu = p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0'),
                                    KyHieu = p.KyHieu,
                                    TenKH = p.TenKH,
                                    NguoiThu = p.NguoiThu,
                                    NguoiNop = p.NguoiNop,
                                    DiaChiNN = p.DiaChiNN,
                                    LyDo = p.LyDo,
                                    TenPL = p.TenPL,
                                    ChungTuGoc = p.ChungTuGoc,
                                    NguoiNhap = p.NguoiNhap,
                                    NgayNhap = p.NgayNhap,
                                    NguoiSua = p.NguoiSua,
                                    NgaySua = p.NgaySua,
                                    PhuongThuc = p.PhuongThuc,
                                    SoTK = p.SoTK,
                                    TenKN = p.TenKN,
                                    TenNH = p.TenNH,
                                    TenNKH = p.TenNKH,
                                    p.TienPhaiThu,
                                    p.TienKhauTru,
                                    p.TienThuThua,
                                    p.SoTienThu,
                                    p.NguonThu

                                };
            gcPhieuThu.DataSource = objData;

            
        }


        void AddRecord()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.MaPT = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        void DeleteRecord()
        {
            var indexs = gvPhieuThu.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (var i in indexs)
            {
                LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu((int) gvPhieuThu.GetRowCellValue(i, "ID"));
            }

            LoadData();
        }

        void Details()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                                        where ct.MaPT == id
                                        select new { ct.DienGiai, ct.SoTien, ct.KhauTru,ct.ThuThua })
                                       .ToList();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
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
                    LoadData();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuThu.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            gvChiTiet.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void gvPhieuThu_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Details();
        }

        private void gvPhieuThu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Details();
        }
        
        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == (byte)itemToaNha.EditValue & rp.GroupID == 5
                                orderby rp.Rank
                                select new { rp.ID, rp.Name }).ToList();

                barPrint.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                    barPrint.ItemLinks.Add(itemPrint);
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                return;
            }

            var maTN = (byte)itemToaNha.EditValue;
            var db = new MasterDataContext();

            DevExpress.XtraReports.UI.XtraReport rpt = null;
            var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int) e.Item.Tag);
            if (objForm != null)
            {
                var rtfText = BuildingDesignTemplate.Class.MergeField.PhieuThu(id.Value, objForm.Content);
                var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText};
                frm.ShowDialog(this);
            }

            //switch ((int)e.Item.Tag)
            //{
            //    case 3:
            //        rpt = new rptPhieuThu(id.Value, maTN,1);
            //        for (int i = 1; i <= 3; i++)
            //        {
            //            var rpt1 = new rptPhieuThu(id.Value, maTN,i);
            //            rpt1.CreateDocument();
            //            rpt.Pages.AddRange(rpt1.Pages);
            //        }
            //        rpt.PrintingSystem.ContinuousPageNumbering = true;
            //        break;
            //    case 19:
            //        rpt = new rptDetail(id.Value, maTN);
            //        break;
            //    case 42:
            //        rpt = new rptPhieuThuMau3(id.Value, maTN);
            //        break;
            //    case 84:
            //        rpt = new LandSoftBuilding.Report.rptPhieuThuLienBacHaMau2(id.Value, maTN);
            //        break;
            //    case 85:
            //        using (var frm = new Building.PrintControls.PrintForm())
            //        {
            //            frm.PrintControl.MaBC = 3;
            //            frm.PrintControl.MaTN = maTN;
            //            frm.PrintControl.IDPhieuthu = id.Value;
            //            frm.PrintControl.Report = new rptPhieuThu257(id.Value, maTN, 3);

            //            frm.ShowDialog();
            //        }
            //        break;
            //    case 88:
            //        rpt = new rptPhieuThuImperia(id.Value, maTN);
            //        break;
            //    case 97:
            //        rpt = new rptPhieuThuImperiaOutside(id.Value, maTN);
            //        break;
             
            //}

            //if (rpt != null)
            //{
            //    rpt.ShowPreviewDialog();
            //}
        }

        private void itemLoaiThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte)itemToaNha.EditValue;

            var frm = new LandSoftBuilding.Fund.Input.frmHinhThucThanhToan();
            frm.MaTN = maTN;
            frm.ShowDialog();
        }
    }
}