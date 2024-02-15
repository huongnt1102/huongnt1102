using System;
using System.Linq;
using Library;

namespace Deposit.PhieuChi
{
    public partial class FrmWithDrawEdit: DevExpress.XtraEditors.XtraForm
    {
        public int? MaPc { set; get; }
        public int? MaPt { set; get; }
        public int? HopDongDatCocId { get; set; }
        public bool IsSave = false;
        byte _maTn = 0;
        MasterDataContext _db = new MasterDataContext();
        pcPhieuChi_TraLaiKhachHang objPC;
        private ptPhieuThu objPt;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmWithDrawEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_ChiTraKhachHang_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            dtNgayChi.EditValue = DateTime.UtcNow.AddHours(7);
            spinTienChi.EditValue = 0;
            spinTienPhat.EditValue = 0;
            objPt = _db.ptPhieuThus.FirstOrDefault(p => p.ID == MaPt);
            if (objPt == null) return;

            spinTienKyQuy.EditValue = objPt.SoTien - objPt.TotalReceipts.GetValueOrDefault()-objPt.TotalPay.GetValueOrDefault();

            if (objPt.MaTN != null)
            {
                _maTn = (byte) objPt.MaTN;
                //Hiển thị khách hàng ký quỹ
                var objKh = _db.tnKhachHangs.FirstOrDefault(p => p.MaKH == objPt.MaKH);
                if (objKh != null)
                {
                    txtKhacHang.EditValue = objKh.IsCaNhan == false ? objKh.CtyTen : objKh.HoKH + " " + objKh.TenKH;
                }


                if (MaPc == null)
                {
                    objPC = new pcPhieuChi_TraLaiKhachHang();
                    txtSoCT.EditValue = Common.CreatePhieuKyQuy(dtNgayChi.DateTime.Month, dtNgayChi.DateTime.Year,
                        objPt.MaTN.Value);
                }
                else
                {
                    objPC = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(p => p.ID == MaPc);
                    if (objPC == null) return;
                    dtNgayChi.EditValue = objPC.NgayChi;
                    txtSoCT.EditValue = objPC.SoPhieuChi;
                    spinTienChi.EditValue = objPC.SoTienChi;
                    spinTienPhat.EditValue = objPC.SoTienPhat;
                    txtGhiChu.EditValue = objPC.GhiChu;
                    spinTienKyQuy.EditValue = spinTienKyQuy.Value + spinTienChi.Value + spinTienPhat.Value;
                }
            }

            itemHuongDan.Click += ItemHuongDan_Click;
            itemClearText.Click += ItemClearText_Click;
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KiemTra(string loai)
        {
            switch (loai)
            {
                case Deposit.Class.Enum.KiemTra.RANGBUOC:
                    if (HopDongDatCocId == null)
                    {
                        Library.DialogBox.Error("Phiếu chi không thuộc hợp đồng.");
                        return true;
                    }

                    if (txtSoCT.Text.Trim() == "")
                    {
                        DialogBox.Alert("Vui lòng nhập số chứng từ phiếu chi");
                        //txtSoCT.Focus();
                        return true;
                    }
                    if (Convert.ToDecimal(spinTienChi.EditValue) == 0 && Convert.ToDecimal(spinTienPhat.EditValue) == 0)
                    {
                        DialogBox.Alert("Vui lòng nhập số tiền chi cho khách hàng");
                        //spinTienChi.Focus();
                        return true;
                    }
                    if (spinTienKyQuy.Value < spinTienChi.Value + spinTienPhat.Value)
                    {
                        DialogBox.Error("Số tiền chi không thể lớn hơn số tiền ký quỹ");
                        //spinTienChi.Focus();
                        return true;
                    }
                    //Kiểm tra tổng số tiền chi ra so với phiếu thu ký quỹ
                    //var objKyQuy = _db.pcPhieuChi_TraLaiKhachHangs.Where(p => p.MaPT == this.MaPt & (p.ID!= objPC.ID)).ToList();
                    //decimal sSoTienKyQuy = 0;
                    //foreach (var item in objKyQuy)
                    //{
                    //    sSoTienKyQuy = sSoTienKyQuy + item.SoTienPhat.GetValueOrDefault() + item.SoTienChi.GetValueOrDefault();
                    //}
                    //if (sSoTienKyQuy + spinTienChi.Value + spinTienPhat.Value > spinTienKyQuy.Value)
                    //{
                    //    DialogBox.Error("Số tiền chi không thể lớn hơn số tiền ký quỹ");
                    //    spinTienChi.Focus();
                    //    return;
                    //}
                    if (spinTienChi.Value + spinTienPhat.Value > spinTienKyQuy.Value)
                    {
                        DialogBox.Error("Số tiền chi không thể lớn hơn số tiền ký quỹ");
                        //spinTienChi.Focus();
                        return true;
                    }
                    break;

                case Deposit.Class.Enum.KiemTra.MASO:
                    //Check số chứng từ
                    if (MaPc == null)
                    {
                        var objKt = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(p => p.SoPhieuChi.Equals(txtSoCT.Text.Trim()));
                        if (objKt != null)
                        {
                            txtSoCT.EditValue = Common.CreatePhieuKyQuy(dtNgayChi.DateTime.Month, dtNgayChi.DateTime.Year, _maTn);
                        }
                    }
                    else
                    {

                        var objKt = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(p => p.SoPhieuChi.Equals(txtSoCT.Text.Trim()) && p.ID != this.MaPc);
                        if (objKt != null)
                        {
                            txtSoCT.EditValue = Common.CreatePhieuKyQuy(dtNgayChi.DateTime.Month, dtNgayChi.DateTime.Year, _maTn);
                        }
                    }
                    break;
            }
            
            return false;
        }

        private Library.pcPhieuChi_TraLaiKhachHang UpdatePhieuChi(Library.pcPhieuChi_TraLaiKhachHang objPc)
        {
            objPc.GhiChu = txtGhiChu.Text;
            objPc.MaPT = MaPt;
            objPc.NgayChi = (DateTime?)dtNgayChi.EditValue;
            objPc.SoPhieuChi = txtSoCT.Text;
            objPc.SoTienChi = spinTienChi.Value;
            objPc.SoTienPhat = spinTienPhat.Value;
            if (MaPc == null)
            {
                objPc.NgayNhap = DateTime.UtcNow.AddHours(7);
                objPc.NguoiNhap = Common.User.MaNV;
                _db.pcPhieuChi_TraLaiKhachHangs.InsertOnSubmit(objPc);
            }
            else
            {
                objPc.NgaySua = DateTime.UtcNow.AddHours(7);
                objPc.NguoiSua = Common.User.MaNV;
            }
            return objPc;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Kiểm tra

            if (KiemTra(Deposit.Class.Enum.KiemTra.RANGBUOC)) return;
            KiemTra(Deposit.Class.Enum.KiemTra.MASO);
            
            #endregion

            #region Tạo phiếu chi

            objPC = UpdatePhieuChi(objPC);
            #endregion

            #region tạo 1 phiếu thu với số tiền phạt

            ptPhieuThu pt = null;
            if (spinTienPhat.Value <= 0)
            {
                if (objPC.PtPhatId != null)
                {
                    // delete phiếu thu
                    #region delete
                     pt = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == objPC.PtPhatId);
                    if (pt != null)
                    {
                        #region Lưu lại phiếu thu đã xóa

                        var ptdx = Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(pt.LyDo, pt.MaKH, pt.MaNV, Library.Common.User.MaNV, pt.MaPL, pt.MaTKNH,pt.MaTN, pt.NguoiNop, System.DateTime.UtcNow.AddHours(7), pt.NgayThu,pt.SoPT, pt.SoTien, pt.ChungTuGoc, pt.DiaChiNN);

                        _db.ptPhieuThuDaXoas.InsertOnSubmit(ptdx);
                        var queryChiTietPt = _db.ptChiTietPhieuThus.Where(p => p.MaPT == pt.ID).ToList();
                        if (queryChiTietPt.Count > 0)
                        {
                            foreach (var qe in queryChiTietPt)
                            {
                                var ptdxChiTiet = Deposit.Class.PhieuThu.CreateChiTietPhieuThuDaXoa(qe.LinkID, pt.SoPT, qe.SoTien, qe.TableName, qe.DienGiai);
                                _db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(ptdxChiTiet);
                            }

                        }

                        #endregion

                        _db.ptPhieuThus.DeleteOnSubmit(pt);
                        //Xóa Sổ quỹ thu chi
                        _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == objPC.PtPhatId && p.IsPhieuThu == true));
                    }
                    #endregion

                    objPC.PtPhatId = null;
                    objPC.SoPhieuThuPhat = null;
                }
            }
            else
            {
                var objMb = (from p in _db.ptPhieuThus
                    join mb in _db.mbMatBangs on p.MaMB equals mb.MaMB
                    where p.ID == MaPt
                    select new { MaKn = mb.mbTangLau.MaKN.Value }).FirstOrDefault();
                int maKn = objMb != null ? objMb.MaKn : 0;
                if (objPC.PtPhatId != null)
                {
                    // edit
                    pt = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == objPC.PtPhatId);
                    if (pt == null)
                    {
                        pt = new ptPhieuThu();
                        pt.SoPT = Common.CreatePhieuThu(0, DateTime.Now.Month,
                            DateTime.Now.Year, maKn, _maTn, false);
                        pt.NgayThu = DateTime.UtcNow.AddHours(7);
                        _db.ptPhieuThus.InsertOnSubmit(pt);
                    }
                    else
                    {
                        _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(_ => _.IDPhieu == pt.ID));
                    }
                }
                else
                {
                    //create
                    pt = new ptPhieuThu();
                    pt.SoPT = Common.CreatePhieuThu(0, DateTime.Now.Month,
                        DateTime.Now.Year, maKn, _maTn, false);
                    pt.NgayThu = DateTime.UtcNow.AddHours(7);
                    pt.MaTN = _maTn;
                    _db.ptPhieuThus.InsertOnSubmit(pt);
                }
                pt.SoTien = (decimal)spinTienPhat.Value;
                pt.MaNV = Common.User.MaNV;
                pt.MaPL = 24;
                //thong tin chung
                pt.MaMB = objPt.MaMB;
                pt.NguoiNop = objPt.NguoiNop;
                pt.DiaChiNN = objPt.DiaChiNN;
                pt.MaTKNH = null;
                pt.ChungTuGoc = "";
                pt.LyDo = txtGhiChu.Text;
                pt.NgayNhap = DateTime.UtcNow.AddHours(7);
                pt.IsKhauTru = false;
                pt.DepositTyleId = objPt.DepositTyleId;
                pt.DepositTyleName = objPt.DepositTyleName;
                pt.IsDepositFather = false; // nếu phiếu này là phiếu tổng thì  = true
                pt.DepositFatherId = objPt.ID;
                pt.MaKH = objPt.MaKH;
                pt.HopDongDatCocId = HopDongDatCocId;

                _db.ptChiTietPhieuThus.DeleteAllOnSubmit(pt.ptChiTietPhieuThus);
                var ptCt = new ptChiTietPhieuThu {DienGiai = txtGhiChu.Text, SoTien = 0,KhauTru=(decimal)spinTienPhat.Value};
                pt.ptChiTietPhieuThus.Add(ptCt);
            }
            #endregion

            _db.SubmitChanges();

            #region Cập nhật sổ quỹ và id phiếu thu child cho phiếu chi
            Common.SoQuy_Insert(_db, DateTime.Now.Month, DateTime.Now.Year, _maTn, objPt.MaKH, (int?)objPt.MaMB, objPC.ID, null, DateTime.UtcNow.AddHours(7), objPC.SoPhieuChi, 0, 24, false, 0, objPC.SoTienChi, 0, 0, null, "pcPhieuChi_TraLaiKhachHang", txtSoCT.Text, Common.User.MaNV, false,false);

            if (pt != null)
            {
                foreach (var item in pt.ptChiTietPhieuThus)
                {
                    Common.SoQuy_Insert(_db, DateTime.Now.Month, DateTime.Now.Year, _maTn,
                        pt.MaKH, (int?)pt.MaMB, pt.ID, item.ID, DateTime.UtcNow.AddHours(7), pt.SoPT,
                        0, 24, true, (decimal?)spinTienPhat.EditValue,
                        0,
                        0,
                        (decimal?)spinTienPhat.EditValue, null, "ptChiTietPhieuThu", item.DienGiai, Common.User.MaNV,
                        false,false);
                }
                objPC.PtPhatId = pt.ID;
                objPC.SoPhieuThuPhat = pt.SoPT;
            }
            #endregion

            #region cập nhật tiền phiếu thu tổng

            objPt = UpdatePhieuThuTong(objPt);
            _db.SubmitChanges();
            #endregion

            #region Update hợp đồng

            var hopDong = GetHopDongById(objPt.HopDongDatCocId);
            if (hopDong != null) hopDong = UpdateHopDong(hopDong);
            #endregion

            _db.SubmitChanges();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            DialogBox.Success("Đã tạo phiếu thành công");
            IsSave = true;
            this.Close();

        }

        private Library.Dep_HopDong GetHopDongById(int? id)
        {
            return _db.Dep_HopDongs.FirstOrDefault(_ => _.Id == id);
        }

        private Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong)
        {
            var listPhieuChi = _db.ptPhieuThus.Where(p => p.HopDongDatCocId == hopDong.Id & p.IsDepositFather == true).ToList();
            hopDong.ThuPhat = listPhieuChi.Sum(_ => _.TotalReceipts).GetValueOrDefault();
            hopDong.TienTra = listPhieuChi.Sum(_ => _.TotalPay).GetValueOrDefault();
            
            return hopDong;
        }

        private Library.ptPhieuThu UpdatePhieuThuTong(Library.ptPhieuThu phieuThu)
        {
            decimal totalReceil = 0, totalPay = 0;
            var listPhieuChi = _db.pcPhieuChi_TraLaiKhachHangs.Where(p => p.MaPT == MaPt).ToList();
            foreach (var phieuChi in listPhieuChi)
            {
                totalReceil = totalReceil + phieuChi.SoTienPhat.GetValueOrDefault();
                totalPay = totalPay + phieuChi.SoTienChi.GetValueOrDefault();
            }

            phieuThu.TotalPay = totalPay;
            phieuThu.TotalReceipts = totalReceil;
            return phieuThu;
        }

        
    }
}