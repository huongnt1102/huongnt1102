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
using BuildingDesignTemplate;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Fund.Output
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db=new MasterDataContext();
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
            MasterDataContext db = new MasterDataContext();
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            // chi cho khách hàng
            var list = (from p in db.pcPhieuChis
                        join pl in db.pcPhanLoais on p.MaPhanLoai equals pl.ID into tblPhanLoai
                        from pl in tblPhanLoai.DefaultIfEmpty()
                        join k in db.tnKhachHangs on p.MaNCC equals k.MaKH into khachHang from k in khachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nhanVien from nv in nhanVien.DefaultIfEmpty()
                        join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                        join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                        from nvs in tblNguoiSua.DefaultIfEmpty()
                        where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 & p.OutputTyleId == 3 
                        && pl.MaPL != "THICONG"
                        select new
                        {
                            p.ID,
                            p.SoPC,
                            p.NgayChi,
                            p.SoTien,
                            TenKH = k!= null? ( k.IsCaNhan == true ? k.TenKH : k.CtyTen) : p.NguoiNhan,
                            NguoiChi = nv!= null ? nv.HoTenNV : "",
                            p.NguoiNhan,
                            p.DiaChiNN,
                            p.LyDo,
                            p.IDHopDongTL,
                            pl.TenPL,
                            p.ChungTuGoc,
                            NguoiNhap = nvn.HoTenNV,
                            p.NgayNhap,
                            NguoiSua = nvs.HoTenNV,
                            p.NgaySua, p.OutputTyleName, p.HinhThucChiName, p.HinhThucChiId, p.TuMatBangNo, p.PhieuThuId
                        }).ToList();

            // chi nội bộ
            var list2 =
                                (from p in db.pcPhieuChis
                                 join pl in db.pcPhanLoais on p.MaPhanLoai equals pl.ID into tblPhanLoai
                                 from pl in tblPhanLoai.DefaultIfEmpty()
                                 join k in db.tnNhanViens on p.MaNVNhan equals k.MaNV into nhanVien
                                 from k in nhanVien.DefaultIfEmpty()
                                 join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                 join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                 join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                 from nvs in tblNguoiSua.DefaultIfEmpty()
                                 where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 & p.OutputTyleId == 2 
                                 & pl.MaPL != "THICONG"
                                 select new
                                 {
                                     p.ID,
                                     p.SoPC,
                                     p.NgayChi,
                                     p.SoTien,
                                     TenKH = k!= null ? k.HoTenNV : "",
                                     NguoiChi = nv.HoTenNV,
                                     p.NguoiNhan,
                                     p.DiaChiNN,
                                     p.LyDo,
                                     p.IDHopDongTL,
                                     pl.TenPL,
                                     p.ChungTuGoc,
                                     NguoiNhap = nvn.HoTenNV,
                                     p.NgayNhap,
                                     NguoiSua = nvs.HoTenNV,
                                     p.NgaySua, p.OutputTyleName,
                                     p.HinhThucChiName,
                                     p.HinhThucChiId,
                                     p.TuMatBangNo, p.PhieuThuId
                                 }).ToList();

            // chi cho nhà cung cấp
            var list3 =
                                (from p in db.pcPhieuChis
                                 join pl in db.pcPhanLoais on p.MaPhanLoai equals pl.ID into tblPhanLoai
                                 from pl in tblPhanLoai.DefaultIfEmpty()
                                 join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nhanVien
                                 from nv in nhanVien.DefaultIfEmpty()
                                 join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                 join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                 from nvs in tblNguoiSua.DefaultIfEmpty()
                                 where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 & p.MaPhanLoai==0 & p.OutputTyleId == 1 
                                 & pl.MaPL != "THICONG"
                                 select new
                                 {
                                     p.ID,
                                     p.SoPC,
                                     p.NgayChi,
                                     p.SoTien,
                                     TenKH = "CÔNG TY TNHH DỊCH VỤ QUẢN LÝ BĐS SÀI GÒN THƯƠNG TÍN",
                                     NguoiChi = nv!= null?  nv.HoTenNV : "",
                                     p.NguoiNhan,
                                     p.DiaChiNN,
                                     p.LyDo,
                                     p.IDHopDongTL,
                                     pl.TenPL,
                                     p.ChungTuGoc,
                                     NguoiNhap = nvn.HoTenNV,
                                     p.NgayNhap,
                                     NguoiSua = nvs.HoTenNV,
                                     p.NgaySua, p.OutputTyleName,
                                     p.HinhThucChiName,
                                     p.HinhThucChiId,
                                     p.TuMatBangNo, p.PhieuThuId
                                 }).ToList();
            var tam1 = list.Concat(list2).Concat(list3);
            gcPhieuChi.DataSource = tam1.ToList();
        }

        void RefreshData()
        {
            LoadData();
        }

        void AddRecord()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            var hinhThucChiId = (int?)gvPhieuChi.GetFocusedRowCellValue("HinhThucChiId");
            hinhThucChiId = hinhThucChiId != null ? hinhThucChiId : 0;

            switch(hinhThucChiId)
            {
                case 1:
                    using (var frm = new LandSoftBuilding.Fund.ChiThuTruoc.ChiThuTruoc.FrmChiThuTruocEdit() { BuildingId = (byte)itemToaNha.EditValue, PhieuChiId = id, HinhThucChiId = 1, HinhThucChiName = "Trả tiền thu trước" })
                    {
                        frm.ShowDialog();
                        LoadData();
                    }
                    break;

                case 2:
                    using (var frm = new LandSoftBuilding.Fund.ChiThuTruoc.ChiThuTruoc.FrmChiThuTruocEdit() { BuildingId = (byte)itemToaNha.EditValue, PhieuChiId = id, HinhThucChiId = 2, HinhThucChiName = "Chuyển tiền thu trước" })
                    {
                        frm.ShowDialog();
                        LoadData();
                    }
                    break;

                default:
                    using (var frm = new frmEdit())
                    {
                        frm.ID = id;
                        frm.MaTN = (byte)itemToaNha.EditValue;
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                            RefreshData();
                    }
                    break;
            }
        }

        void DeleteRecord()
        {
            var indexs = gvPhieuChi.GetSelectedRows();
            db = new MasterDataContext();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (var i in indexs)
            {
                if ((int?)gvPhieuChi.GetRowCellValue(i, "IDHopDongTL") > 0)
                {
                    Library.DialogBox.Alert("Đây là phiếu chi bên thanh lý hợp đồng. Vui lòng qua nghiệp vụ khấu trừ để xóa");
                    return;
                }
                if (gvPhieuChi.GetRowCellValue(i, "PhieuThuId") != null) {

                    if (DialogBox.Question("Phiếu chi chuyển tiền có đính kèm phiếu thu, bạn đồng ý xóa phiếu thu? ") == DialogResult.No) return;

                    LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu((int)gvPhieuChi.GetRowCellValue(i, "PhieuThuId")); 
                }
                LandSoftBuilding.Fund.Class.PhieuChi.DeletePhieuChi((int) gvPhieuChi.GetRowCellValue(i, "ID"));
            }

            this.RefreshData();
        }

        void Details()
        {
           
            try
            {
                var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from ct in db.pcChiTiets
                                        join pl in db.LoaiChi_ChiTiets on ct.MaPL equals pl.ID into phan
                                        from pl in phan.DefaultIfEmpty()
                                        where ct.MaPC == id
                                        select new { ct.DienGiai, ct.SoTien,pl.TenLoaiChi })
                                       .ToList();
            }
            catch
            {
            }
            finally
            {
               // db.Dispose();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuChi.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            gvChiTiet.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(7);

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

            

            var list = (from p in db.pcPhieuChis
                                join pl in db.pcPhanLoais on p.MaPhanLoai equals pl.ID into tblPhanLoai
                                from pl in tblPhanLoai.DefaultIfEmpty()
                                join k in db.tnKhachHangs on p.MaNCC equals k.MaKH //into tblKhachHang
                                //from k in tblKhachHang.DefaultIfEmpty()
                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0
                                select new
                                {
                                    p.ID,
                                    p.SoPC,
                                    p.NgayChi,
                                    p.SoTien,
                                    TenKH = k.IsCaNhan == true ? String.Format("{0} {1}", k.HoKH, k.TenKH) : k.CtyTen,
                                    NguoiChi = nv.HoTenNV,
                                    p.NguoiNhan,
                                    p.DiaChiNN,
                                    p.LyDo,
                                    pl.TenPL,
                                    p.ChungTuGoc,
                                    NguoiNhap = nvn.HoTenNV,
                                    p.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    p.NgaySua
                                }).ToList();
            var list2 =
                                (from p in db.pcPhieuChis
                                 join pl in db.pcPhanLoais on p.MaPhanLoai equals pl.ID into tblPhanLoai
                                 from pl in tblPhanLoai.DefaultIfEmpty()
                                 join k in db.tnNhanViens on p.MaNVNhan equals k.MaNV //into tblKhachHang
                                 //from k in tblKhachHang.DefaultIfEmpty()
                                 join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                 join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                 join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                 from nvs in tblNguoiSua.DefaultIfEmpty()
                                 where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0
                                 select new
                                 {
                                     p.ID,
                                     p.SoPC,
                                     p.NgayChi,
                                     p.SoTien,
                                     TenKH = k.HoTenNV,
                                     NguoiChi = nv.HoTenNV,
                                     p.NguoiNhan,
                                     p.DiaChiNN,
                                     p.LyDo,
                                     pl.TenPL,
                                     p.ChungTuGoc,
                                     NguoiNhap = nvn.HoTenNV,
                                     p.NgayNhap,
                                     NguoiSua = nvs.HoTenNV,
                                     p.NgaySua
                                 }).ToList();
            var tam1 = list.Concat(list2);
            e.QueryableSource = tam1.AsQueryable();
            e.Tag = db;
        }
        
        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
           
        }

        private void itemInPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = gvPhieuChi.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu chi] cần in");
                return;
            }

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                foreach (var item in rows)
                { 
                    using (var frm = new Building.PrintControls.PrintForm())
                    {
                        //frm.PrintControl.MaBC = 4;
                        //frm.PrintControl.MaTN = maTN;
                        //frm.PrintControl.IDPhieuthu = (int)gvPhieuChi.GetRowCellValue(item, "ID");
                        //frm.PrintControl.Report = new rptPhieuChiSongNgu((int)gvPhieuChi.GetRowCellValue(item, "ID"), maTN);

                        //frm.ShowDialog();
                    }
                   // var rpt = new rptPhieuChiSongNgu((int)gvPhieuChi.GetRowCellValue(item, "ID"), maTN);
                   // rpt.Print();

                    System.Threading.Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0}", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); }
        }

        private void itemXemPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu chi] cần xem");
                return;
            }

            //var maTN = (byte)itemToaNha.EditValue;
            //var rpt = new rptPhieuChi(id.Value, maTN);
            //rpt.ShowPreviewDialog();

            var db = new MasterDataContext();

            DevExpress.XtraReports.UI.XtraReport rpt = null;
            var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)e.Item.Tag);
            if (objForm != null)
            {
                var rtf = BuildingDesignTemplate.Class.MergeField.PhieuChi(id.Value, objForm.Content);
                var frm = new FrmShow { RtfText = rtf};
                frm.ShowDialog(this);
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuChi);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
          
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == (byte)itemToaNha.EditValue & rp.GroupID == 6
                                orderby rp.Rank
                                select new { rp.ID, rp.Name }).ToList();


                barPrint.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemXemPhieuChi_ItemClick);//barPrint
                    barManager1.Items.Add(itemPrint);
                    barPrint.ItemLinks.Add(itemPrint);
                }
            }
            catch { }
            finally
            {
               
            }
        }

        private void itemLoaiChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte)itemToaNha.EditValue;

            var frm = new LandSoftBuilding.Fund.Output.frmHinhThucThanhToan();
            frm.MaTN = maTN;
            frm.ShowDialog();
        }

        private void itemChiTienThuTruoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using(var frm = new LandSoftBuilding.Fund.ChiThuTruoc.ChiThuTruoc.FrmChiThuTruocEdit() { BuildingId = (byte)itemToaNha.EditValue, HinhThucChiId = 1, HinhThucChiName = "Trả tiền thu trước" })
            {
                frm.ShowDialog();
                LoadData();
            }
        }

        //private void itemChuyenTienThuTruoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    using (var frm = new LandSoftBuilding.Fund.ChiThuTruoc.ChiThuTruoc.FrmChiThuTruocEdit() { BuildingId = (byte)itemToaNha.EditValue, HinhThucChiId = 2, HinhThucChiName = "Chuyển tiền thu trước" })
        //    {
        //        frm.ShowDialog();
        //        LoadData();
        //    }
        //}

    }
}