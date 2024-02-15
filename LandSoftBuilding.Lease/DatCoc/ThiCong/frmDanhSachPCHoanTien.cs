using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Lease.DatCoc.ThiCong
{
    public partial class frmDanhSachPCHoanTien : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachPCHoanTien()
        {
            InitializeComponent();
        }

        

        private void frmDanhSachPCHoanTien_Load(object sender, EventArgs e)
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
                        join k in db.tnKhachHangs on p.MaNCC equals k.MaKH into khachHang
                        from k in khachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nhanVien
                        from nv in nhanVien.DefaultIfEmpty()
                        join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                        join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                        from nvs in tblNguoiSua.DefaultIfEmpty()
                        where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 & p.OutputTyleId == 3 && pl.MaPL == "THICONG"
                        select new
                        {
                            p.ID,
                            p.SoPC,
                            p.NgayChi,
                            p.SoTien,
                            TenKH = k != null ? (k.IsCaNhan == true ? k.TenKH : k.CtyTen) : p.NguoiNhan,
                            NguoiChi = nv != null ? nv.HoTenNV : "",
                            p.NguoiNhan,
                            p.DiaChiNN,
                            p.LyDo,
                            pl.TenPL,
                            p.ChungTuGoc,
                            NguoiNhap = nvn.HoTenNV,
                            p.NgayNhap,
                            NguoiSua = nvs.HoTenNV,
                            p.NgaySua,
                            p.OutputTyleName,
                            p.HinhThucChiName,
                            p.HinhThucChiId,
                            p.TuMatBangNo,
                            p.PhieuThuId
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
                                 where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 & p.OutputTyleId == 2 & pl.MaPL != "THICONG"
                                 select new
                                 {
                                     p.ID,
                                     p.SoPC,
                                     p.NgayChi,
                                     p.SoTien,
                                     TenKH = k != null ? k.HoTenNV : "",
                                     NguoiChi = nv.HoTenNV,
                                     p.NguoiNhan,
                                     p.DiaChiNN,
                                     p.LyDo,
                                     pl.TenPL,
                                     p.ChungTuGoc,
                                     NguoiNhap = nvn.HoTenNV,
                                     p.NgayNhap,
                                     NguoiSua = nvs.HoTenNV,
                                     p.NgaySua,
                                     p.OutputTyleName,
                                     p.HinhThucChiName,
                                     p.HinhThucChiId,
                                     p.TuMatBangNo,
                                     p.PhieuThuId
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
                                 where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 & p.MaPhanLoai == 0 & p.OutputTyleId == 1 & pl.MaPL != "THICONG"
                                 select new
                                 {
                                     p.ID,
                                     p.SoPC,
                                     p.NgayChi,
                                     p.SoTien,
                                     TenKH = "CÔNG TY TNHH DỊCH VỤ QUẢN LÝ BĐS SÀI GÒN THƯƠNG TÍN",
                                     NguoiChi = nv != null ? nv.HoTenNV : "",
                                     p.NguoiNhan,
                                     p.DiaChiNN,
                                     p.LyDo,
                                     pl.TenPL,
                                     p.ChungTuGoc,
                                     NguoiNhap = nvn.HoTenNV,
                                     p.NgayNhap,
                                     NguoiSua = nvs.HoTenNV,
                                     p.NgaySua,
                                     p.OutputTyleName,
                                     p.HinhThucChiName,
                                     p.HinhThucChiId,
                                     p.TuMatBangNo,
                                     p.PhieuThuId
                                 }).ToList();
            var tam1 = list.Concat(list2).Concat(list3);
            gcPhieuChi.DataSource = tam1.ToList();
        }
        void RefreshData()
        {
            LoadData();
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

            switch (hinhThucChiId)
            {

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

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (var i in indexs)
            {
                if (gvPhieuChi.GetRowCellValue(i, "PhieuThuId") != null)
                {

                    if (DialogBox.Question("Phiếu chi chuyển tiền có đính kèm phiếu thu, bạn đồng ý xóa phiếu thu? ") == DialogResult.No) return;

                    LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu((int)gvPhieuChi.GetRowCellValue(i, "PhieuThuId"));
                }
                LandSoftBuilding.Fund.Class.PhieuChi.DeletePhieuChi((int)gvPhieuChi.GetRowCellValue(i, "ID"));
            }

            this.RefreshData();
        }
        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }


        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuChi);
        }
    }
}