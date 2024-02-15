using System;
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

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmManager_TienKyQuy : DevExpress.XtraEditors.XtraForm
    {
        public frmManager_TienKyQuy()
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
            gcPhieuThu.DataSource = null;
            gcPhieuThu.DataSource = linqInstantFeedbackSource1;

            
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddRecord()
        {
            using (var frm = new frmEdit_ChiTraKhachHang())
            {
                if (gvPhieuThu.GetFocusedRowCellValue("ID") != null)
                {
                    frm.MaPT = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
                    frm.ShowDialog();
                    if (frm.IsSave)
                        this.RefreshData();
                }
                else
                {
                    DialogBox.Error("Vui lòng chọn phiếu thu để chi trả khách hàng!");
                }
            }
        }

        void EditRecord()
        {
            var id = (int?)gvChiTiet.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEdit_ChiTraKhachHang())
            {
                frm.MaPT = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
                frm.MaPC = id;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void DeleteRecord()
        {
            var id = (int?)gvChiTiet.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            var pt = db.pcPhieuChi_TraLaiKhachHangs.Single(p => p.ID == (int)gvChiTiet.GetFocusedRowCellValue("ID"));
            db.pcPhieuChi_TraLaiKhachHangs.DeleteOnSubmit(pt);
            try
            {
                db.SubmitChanges();
                gvChiTiet.DeleteSelectedRows();
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

                gcChiTiet.DataSource = (from ct in db.pcPhieuChi_TraLaiKhachHangs
                                        join nn in db.tnNhanViens on ct.NguoiNhap equals nn.MaNV
                                        join ns in db.tnNhanViens on ct.NguoiSua equals ns.MaNV into nsua
                                        from ns in nsua.DefaultIfEmpty()
                                        where ct.MaPT == id
                                        select new { 
                                        ct.ID,
                                        ct.SoPhieuChi,
                                        ct.NgayChi,
                                        ct.SoTienChi,
                                        ct.SoTienPhat,
                                        ct.NgayNhap,
                                        ct.NgaySua,
                                        ct.GhiChu,
                                        NguoiNhap=nn.HoTenNV,
                                        NguoiSua=ns.HoTenNV
                                        })
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
                    RefreshData();
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
        
        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            //e.QueryableSource
            e.QueryableSource = from p in db.ptPhieuThus
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
                                join nv in db.tnNhanViens on p.MaNV equals (int?)nv.MaNV
                                join nvn in db.tnNhanViens on p.MaNVN equals (int?)nvn.MaNV
                                join nvs in db.tnNhanViens on p.MaNVS equals (int?)nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join tk in db.nhTaiKhoans on p.MaTKNH equals (int?)tk.ID into tblTaiKhoan
                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                join nh in db.nhNganHangs on tk.MaNH equals (int?)nh.ID into tblNganHang
                                from nh in tblNganHang.DefaultIfEmpty()
                                where ((int?)p.MaTN == (int?)((int)maTN) & SqlMethods.DateDiffDay((DateTime?)tuNgay, p.NgayThu) >= (int?)0 & SqlMethods.DateDiffDay(p.NgayThu, (DateTime?)denNgay) >= (int?)0) & p.IsKhauTru == false & p.MaPL==24
                                
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
                                    TenKN=tl.mbKhoiNha.TenKN,
                                    SoTienThu = p.ptChiTietPhieuThus.Sum(ct => ct.SoTien.GetValueOrDefault()),
                                    DaChi=db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(pc=>pc.MaPT==p.ID)==null?false:true,
                                    TienDaTra = db.pcPhieuChi_TraLaiKhachHangs.Where(dt=>dt.MaPT==p.ID).Sum(o=>o.SoTienChi).GetValueOrDefault(),
                                    TienPhat = db.pcPhieuChi_TraLaiKhachHangs.Where(tp => tp.MaPT == p.ID).Sum(o => o.SoTienPhat).GetValueOrDefault(),
                                } into p
                                select new
                                {
                                    ID = p.ID,
                                    SoPT = p.SoPT,
                                    NgayThu = p.NgayThu, //p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0') + " | " + p.NgayThu.Value.Day.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Month.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Year.ToString(),
                                    GioThu=p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0'), 
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
                                    TenKN=p.TenKN,
                                    TenNH = p.TenNH,
                                    TenNKH = p.TenNKH,
                                    p.DaChi,
                                    p.SoTienThu,
                                    p.TienDaTra,
                                    p.TienPhat
                                };
            e.Tag = db;
        }
        
        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DismissQueryable: " + ex.Message);
            }
        }


        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void itemExport_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }

        private void itemppEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void itemppDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        


    }
}