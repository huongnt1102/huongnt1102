using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Deposit.PhieuChi;
using DevExpress.XtraEditors;
using Library;

namespace Deposit.PhieuThuTienPhat
{
    public partial class FrmPhieuThuTienPhat : DevExpress.XtraEditors.XtraForm
    {
        public int? HopDongDatCocId { get; set; }
        public string FormName { get; set; }

        private Library.MasterDataContext _db = new MasterDataContext();

        public FrmPhieuThuTienPhat()
        {
            InitializeComponent();
        }

        private void FrmWithDraw_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            this.Text = FormName;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);

            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var maTn = (byte)itemToaNha.EditValue;

                switch (HopDongDatCocId)
                {
                    case null:
                        GetDataAll(maTn, tuNgay, denNgay);
                        break;
                    default:
                        GetDataByHopDongDatCoc(maTn, tuNgay, denNgay);
                        break;
                };
                
            }
            catch{}
        }

        private void GetDataByHopDongDatCoc(byte? maTn, System.DateTime tuNgay, System.DateTime denNgay)
        {
            var db = new MasterDataContext();
            var list = (from p in db.pcPhieuChi_TraLaiKhachHangs
                join pt in db.ptPhieuThus on p.MaPT equals pt.ID
                join nv in db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
                from nv in nhanVien.DefaultIfEmpty()
                where pt.HopDongDatCocId == HopDongDatCocId &
                      pt.MaTN == maTn //&

                //SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 &
                //SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0
                select new
                {
                    p.NgayChi,
                    p.SoPhieuChi,
                    KhachHang = pt.NguoiNop,
                    DiaChi = pt.DiaChiNN,
                    PhieuDatCoc = pt.SoPT,
                    p.SoTienChi,
                    p.SoTienPhat,
                    DienGiai = p.GhiChu,
                    nv.HoTenNV,
                    p.ID,
                    p.MaPT,
                    pt.MaKH,
                    pt.MaNV,
                    pt.HopDongDatCocId
                }).ToList();
            switch (FormName)
            {
                case Deposit.Class.Enum.FormName.PHIEU_THU_TIEN_PHAT:
                    gc.DataSource = list.Where(_ => _.SoTienPhat != 0);
                    break;
            }
            db.Dispose();
        }

        private void GetDataAll(byte? maTn, System.DateTime tuNgay, System.DateTime denNgay)
        {
            var db = new MasterDataContext();
            var list = (from p in db.pcPhieuChi_TraLaiKhachHangs
                join pt in db.ptPhieuThus on p.MaPT equals pt.ID
                join nv in db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
                from nv in nhanVien.DefaultIfEmpty()
                where pt.MaTN == maTn & 
                      pt.HopDongDatCocId!=null &
                      SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 &
                      SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0
                select new
                {
                    p.NgayChi,
                    p.SoPhieuChi,
                    KhachHang = pt.NguoiNop,
                    DiaChi = pt.DiaChiNN,
                    PhieuDatCoc = pt.SoPT,
                    p.SoTienChi,
                    p.SoTienPhat,
                    DienGiai = p.GhiChu,
                    nv.HoTenNV,
                    p.ID,
                    p.MaPT,
                    pt.MaKH,
                    pt.MaNV, pt.HopDongDatCocId
                }).ToList();
            switch (FormName)
            {
                case Deposit.Class.Enum.FormName.PHIEU_THU_TIEN_PHAT:
                    gc.DataSource = list.Where(_ => _.SoTienPhat != 0);
                    break;
            }
            db.Dispose();
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            var hopDongDatCocId = (int?) gv.GetFocusedRowCellValue("HopDongDatCocId");
            if (hopDongDatCocId == null)
            {
                Library.DialogBox.Alert("Phiếu không có hợp đồng đặt cọc");
                return;
            }

            using (var frm = new FrmWithDrawEdit())
            {
                frm.MaPc = id;
                frm.MaPt = (int?)gv.GetFocusedRowCellValue("MaPT");
                frm.HopDongDatCocId = (int?) gv.GetFocusedRowCellValue("HopDongDatCocId");
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thu");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            _db = new MasterDataContext();
            foreach (var i in indexs)
            {
                var pc = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(_ => _.ID == (int)gv.GetRowCellValue(i, "ID"));
                if (pc != null)
                {
                    #region Lưu lại phiếu thu đã xóa
                    // xóa hết những phiếu thu tự tạo
                    var ptChild = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == pc.PtPhatId);
                    if (ptChild != null)
                    {
                        var ptdx = Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(ptChild.LyDo, ptChild.MaKH, ptChild.MaNV, Library.Common.User.MaNV, ptChild.MaPL, ptChild.MaTKNH, ptChild.MaTN, ptChild.NguoiNop, System.DateTime.UtcNow.AddHours(7), ptChild.NgayThu, ptChild.SoPT, ptChild.SoTien, ptChild.ChungTuGoc, ptChild.DiaChiNN);
                        // PTDX.MaNVS = pt.MaNVS;

                        //PTDX.NgaySua = pt.NgaySua;
                        _db.ptPhieuThuDaXoas.InsertOnSubmit(ptdx);
                        var queryChiTietPt = _db.ptChiTietPhieuThus.Where(p => p.MaPT == pc.ID).ToList();
                        if (queryChiTietPt.Count > 0)
                        {
                            foreach (var qe in queryChiTietPt)
                            {
                                var ptdxChiTiet = new ptChiTietPhieuThuDaXoa
                                {
                                    LinkID = qe.LinkID,
                                    MaPT = ptChild.SoPT,
                                    SoTien = qe.SoTien,
                                    TableName = qe.TableName,
                                    DienGiai = qe.DienGiai
                                };
                                _db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(ptdxChiTiet);
                            }
                        }
                        _db.ptChiTietPhieuThus.DeleteAllOnSubmit(ptChild.ptChiTietPhieuThus);
                        _db.ptPhieuThus.DeleteOnSubmit(ptChild);
                        //Xóa Sổ quỹ thu chi
                        _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == ptChild.ID && p.IsPhieuThu == true));
                    }

                    #endregion
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(_=>_.IDPhieu == pc.ID && _.IsKhauTru == false));
                    _db.pcPhieuChi_TraLaiKhachHangs.DeleteOnSubmit(pc);
                    _db.SubmitChanges();

                    #region cập nhật tiền phiếu thu tổng
                    var pt = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == pc.MaPT);
                    if (pt != null)
                    {
                        pt = UpdatePhieuThuTong(pt, pc.MaPT);

                        #region Update hợp đồng

                        var hopDong = GetHopDongById(pt.HopDongDatCocId);
                        if (hopDong != null) hopDong = UpdateHopDong(hopDong);
                        #endregion
                    }
                    
                    #endregion

                    

                    _db.SubmitChanges();
                }
            }
            try
            {
                

                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                _db.Dispose();
            }
        }

        private Library.ptPhieuThu UpdatePhieuThuTong(Library.ptPhieuThu phieuThu, int? maPt)
        {
            decimal totalReceil = 0, totalPay = 0;
            var listPhieuChi = _db.pcPhieuChi_TraLaiKhachHangs.Where(p => p.MaPT == maPt).ToList();
            foreach (var phieuChi in listPhieuChi)
            {
                totalReceil = totalReceil + phieuChi.SoTienPhat.GetValueOrDefault();
                totalPay = totalPay + phieuChi.SoTienChi.GetValueOrDefault();
            }

            phieuThu.TotalPay = totalPay;
            phieuThu.TotalReceipts = totalReceil;
            return phieuThu;
        }

        private Library.Dep_HopDong GetHopDongById(int? id)
        {
            return _db.Dep_HopDongs.FirstOrDefault(_ => _.Id == id);
        }

        private Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong)
        {
            var listPhieuChi = _db.ptPhieuThus.Where(p => p.HopDongDatCocId == hopDong.Id).ToList();
            hopDong.ThuPhat = listPhieuChi.Sum(_ => _.TotalReceipts).GetValueOrDefault();
            hopDong.TienTra = listPhieuChi.Sum(_ => _.TotalPay).GetValueOrDefault();

            return hopDong;
        }

        private void CbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}