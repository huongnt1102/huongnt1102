using System.Linq;

namespace HopDongThueNgoai
{
    public partial class FrmEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTn { get; set; }
        public int? Id { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.hdctnDanhSachHopDongThueNgoai _hd;
        private Library.hdctnLichSu _historyHopDong;
        private Library.hdctnLichSu _historyTrangThai;
        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();
        private bool _isTaoLichThanhToan;

        private string _maNhomCongViec;
        private string _soHopDongCha = "";

        private decimal _tienChuaThue = 0, _tienThue = 0, _tienSauThue = 0;
        public bool _saveCt = false;

        public FrmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this);
            txtNhanVien.EditValue = Library.Common.User.HoTenNV;
            _db = new Library.MasterDataContext();

            glkLoai.Properties.DataSource = HopDongThueNgoai.Class.HopDongFunc.GetListPhanLoai();

            _hd = GetHopDong();
            glkLoai.EditValue = _hd.IsPhuLuc == true ? HopDongThueNgoai.Class.TenHienThi.PHU_LUC : HopDongThueNgoai.Class.TenHienThi.HOP_DONG;

            HienThi((string)glkLoai.EditValue);

            LoadDanhMuc();

            gc.DataSource = _hd.hdctnChiTietHopDongThueNgoais;
            gcLichThanhToan.DataSource = _hd.hdctnLichThanhToans;

            GanGiaTri(Id != null);

            CreateHistoryTrangThai();
        }

        private void HienThi(string phanLoai)
        {
            switch (phanLoai)
            {
                case HopDongThueNgoai.Class.TenHienThi.HOP_DONG: HienThiHopDong(); break;

                case HopDongThueNgoai.Class.TenHienThi.PHU_LUC: HienThiPhuLuc(); break;

                default: HienThiHopDong(); break;
            }
        }

        private void HienThiHopDong()
        {
            this.Text = HopDongThueNgoai.Class.TenHienThi.THEM_HOP_DONG_THUE_NGOAI;
            layoutControlSoHopDong.Text = HopDongThueNgoai.Class.TenHienThi.GR_SO_HOP_DONG;
            lciSoHopDong.Text = HopDongThueNgoai.Class.TenHienThi.SO_HOP_DONG;

            layoutControlHopDongCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            layoutControlThongTinHopDong.Text = HopDongThueNgoai.Class.TenHienThi.THONG_TIN_HOP_DONG;
            layoutControlThongTinThanhToan.Text = HopDongThueNgoai.Class.TenHienThi.THANH_TOAN_HOP_DONG;
            layoutControlNhanVien.Text = HopDongThueNgoai.Class.TenHienThi.NHAN_VIEN_HOP_DONG;
            xtraTabControl1.TabPages[0].Text = HopDongThueNgoai.Class.TenHienThi.CONG_VIEC_HOP_DONG;
        }

        private void HienThiPhuLuc()
        {
            this.Text = HopDongThueNgoai.Class.TenHienThi.THEM_PHU_LUC_HOP_DONG_THUE_NGOAI;
            layoutControlSoHopDong.Text = HopDongThueNgoai.Class.TenHienThi.GR_SO_PHU_LUC;
            lciSoHopDong.Text = HopDongThueNgoai.Class.TenHienThi.SO_PHU_LUC;

            layoutControlHopDongCha.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            layoutControlThongTinHopDong.Text = HopDongThueNgoai.Class.TenHienThi.THONG_TIN_PHU_LUC;
            layoutControlThongTinThanhToan.Text = HopDongThueNgoai.Class.TenHienThi.THANH_TOAN_PHU_LUC;
            layoutControlNhanVien.Text = HopDongThueNgoai.Class.TenHienThi.NHAN_VIEN_PHU_LUC;
            xtraTabControl1.TabPages[0].Text = HopDongThueNgoai.Class.TenHienThi.CONG_VIEC_PHU_LUC;
        }

        private string GetTenTn()
        {
            return MaTn != null ? _db.tnToaNhas.First(_ => _.MaTN == MaTn).TenVT : "";
        }

        private void GanGiaTri(bool isEdit)
        {
            switch (isEdit)
            {
                case true:
                    DuocTaoLichThanhToan(false);
                    SuaMaNhomCongViec(_hd.MaCongViec);
                    KhoiTaoNgay(_hd);
                    KhoiTaoTien(_hd.TienChuaThue.GetValueOrDefault());
                    
                    KhoiTaoNguoiSua(Library.Common.User, System.DateTime.UtcNow.AddHours(7));
                    if (_hd.TrangThai != null) KhoiTaoTrangThai(_hd.TrangThai);

                    glkNhaCungCap.EditValue = int.Parse(_hd.NhaCungCap);
                    glkLoaiTien.EditValue = int.Parse(_hd.LoaiTien);
                    spneKyThanhToan.Value = int.Parse(_hd.KyThanhToan.ToString());
                    spneVAT.Value = decimal.Parse(_hd.VAT.ToString());

                    txtSohopDong.EditValue = _hd.SoHopDong;
                    txtDiaChi.Text = _hd.DiaChi;
                    txtSoChungTu.Text = _hd.SoChungTu;
                    txtNoiLamViec.Text = _hd.NoiLamViecName;
                    txtMoiBenGiu.Text = _hd.SoBienBanMoiBenGiu;

                    _soHopDongCha = _hd.SoHopDongCha;
                    if (_hd.HopDongChaId != null) glkHopDong.EditValue = _hd.HopDongChaId;

                    chkCbxMatBang.SetEditValue(_hd.NoiLamViecIds);

                    ThayDoiTac(false);

                    break;

                case false:
                    GetSoHopDong();
                    DuocTaoLichThanhToan(true);
                    KhoiTaoNgay(System.DateTime.UtcNow.AddHours(7));
                    KhoiTaoTien((decimal)0);
                    KhoiTaoNhanVienNhap(Library.Common.User);
                    KhoiTaoTrangThai(glkTrangThai.Properties.GetDisplayValue(0));
                    ThayDoiTac(true);

                    _db.hdctnDanhSachHopDongThueNgoais.InsertOnSubmit(_hd);
                    break;
            }
        }

        private void ThayDoiTac(bool value)
        {
            layoutNhaCungCap.Enabled = value;
        }

        #region Chỉnh sửa phiếu
        
        private void SuaMaNhomCongViec(string maNhomCongViec)
        {
            _maNhomCongViec = maNhomCongViec;
            glkNhomCongViec.EditValue = maNhomCongViec;
        }

        private void KhoiTaoNguoiSua(Library.tnNhanVien user, System.DateTime date)
        {
            _hd.NhanVienSua = user.MaNV.ToString();
            _hd.BoPhan_NVS = user.MaPB.ToString();
            _hd.NgaySua = date;
        }

        private void KhoiTaoNgay(Library.hdctnDanhSachHopDongThueNgoai hd)
        {
            if (hd.NgayKy != null) deNgayKy.DateTime = (System.DateTime)hd.NgayKy;
            if (hd.NgayHieuLuc != null) deNgayHieuLuc.DateTime = (System.DateTime)hd.NgayHieuLuc;
            if (hd.NgayHetHan != null) deNgayHetHan.DateTime = (System.DateTime)hd.NgayHetHan;
        }

        #endregion

        #region Phiếu tạo mới

        private void KhoiTaoNgay(System.DateTime ngay)
        {
            deNgayKy.DateTime = ngay;
            deNgayHieuLuc.DateTime = ngay;
            deNgayHetHan.DateTime = ngay;
            _hd.NgayNhap = ngay;
        }

        private void KhoiTaoTien(decimal tien)
        {
            lblTienChuaThue.Text = System.String.Format("{0:#,0}", tien);
            lblTienSauThue.Text = System.String.Format("{0:#,0}", tien);
            lblTienThue.Text = System.String.Format("{0:#,0}", tien);

            _tienChuaThue = tien;
            _tienSauThue = tien;
            _tienThue = tien;
        }

        private void KhoiTaoNhanVienNhap(Library.tnNhanVien user)
        {
            _hd.NhanVienNhap = user.MaNV.ToString();
            _hd.BoPhan_NVN = user.MaNV.ToString();
        }

        private void KhoiTaoTrangThai(object obj)
        {
            glkTrangThai.EditValue = obj;
        }

        private void DuocTaoLichThanhToan(bool value)
        {
            _isTaoLichThanhToan = value;
        }

        #endregion

        #region Tạo hợp đồng

        #endregion

        private void GetSoHopDong()
        {
            txtSohopDong.EditValue = CreateNo(GetTenTn());
        }

        private string CreateNo(string buildingNo)
        {
            Library.MasterDataContext db = new Library.MasterDataContext();

            string temp = ((string) glkLoai.EditValue == HopDongThueNgoai.Class.TenHienThi.PHU_LUC) ? buildingNo + HopDongThueNgoai.Class.TenHienThi.PHU_LUC_NO_INTER : buildingNo + HopDongThueNgoai.Class.TenHienThi.HOP_DONG_NO_INTER;
            string stt = "";
            int index = (string) glkLoai.EditValue == HopDongThueNgoai.Class.TenHienThi.PHU_LUC ? HopDongThueNgoai.Class.TenHienThi.PHU_LUC_NO_INDEX : HopDongThueNgoai.Class.TenHienThi.HOP_DONG_NO_INDEX;

            var obj = (from _ in db.hdctnDanhSachHopDongThueNgoais
                where _.MaToaNha == MaTn.ToString() &
                      _.IsPhuLuc == ((string) glkLoai.EditValue == HopDongThueNgoai.Class.TenHienThi.PHU_LUC)
                orderby _.SoHopDong.Substring(_.SoHopDong.IndexOf('.') + index) descending
                select new
                {
                    Stt = _.SoHopDong.Substring(_.SoHopDong.IndexOf('.') + index)
                }).FirstOrDefault();
            if (obj == null || (obj.Stt == null))
            {
                stt = "0001";
            }
            else stt = (int.Parse(obj.Stt) + 1).ToString().PadLeft(4, '0');

            temp = temp + stt;
            return temp;
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetHopDong()
        {
            return Id!=null? _db.hdctnDanhSachHopDongThueNgoais.First(_=>_.RowID == Id) : new Library.hdctnDanhSachHopDongThueNgoai();
        }

        private void LoadDanhMuc()
        {
            LoadPhongBan(Library.Common.User.MaPB);
            //txtTaiKhoanCo.EditValue = "3311";

            LoadLoaiTien();

            LoadNhomCongViec();

            LoadHopDong();

            LoadMatBang();

            LoadKhachHang();

            LoadTrangThai();
        }

        #region Load danh mục

        private void LoadTrangThai()
        {
            glkTrangThai.Properties.DataSource = _db.hdctnTrangThais;
        }

        private void LoadMatBang()
        {
            chkCbxMatBang.Properties.DataSource = (_db.mbMatBangs.Where(_ => _.MaTN == MaTn).Select(_ => new { _.MaSoMB, _.MaMB })).ToList(); // _.mbTangLau.TenTL, _.mbTangLau.mbKhoiNha.TenKN,
            chkCbxMatBang.Properties.ValueMember = "MaMB";
            chkCbxMatBang.Properties.SeparatorChar = ',';
        }

        private void LoadHopDong()
        {
            glkHopDong.Properties.DataSource = (from hd in _db.hdctnDanhSachHopDongThueNgoais
                                                join kh in _db.tnKhachHangs on hd.NhaCungCap equals kh.MaKH.ToString()
                                                where hd.IsPhuLuc == null | hd.IsPhuLuc == false & hd.MaToaNha == MaTn.ToString()
                                                select new
                                                {
                                                    hd.RowID,
                                                    hd.SoHopDong,
                                                    kh.KyHieu,
                                                    HoTenKh = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen
                                                }).ToList();
        }

        private void LoadNhomCongViec()
        {
            glkNhomCongViec.Properties.DataSource = (from p in _db.hdctnNhomCongViecs
                                                     select new
                                                     {
                                                         p.TenNhomCongViec,
                                                         p.MaNhomCongViec,
                                                         p.RowID
                                                     }).ToList();
        }

        private void LoadLoaiTien()
        {
            glkLoaiTien.Properties.DataSource = (from p in _db.LoaiTiens
                                                 select new
                                                 {
                                                     p.KyHieuLT,
                                                     p.TenLT,
                                                     p.ID,
                                                     p.TyGia
                                                 }).ToList();
            glkLoaiTien.EditValue = glkLoaiTien.Properties.GetKeyValue(0);
        }

        private void LoadPhongBan(int? maPhongBan)
        {
            var tenPb = (from p in _db.tnPhongBans
                         where p.MaPB == maPhongBan
                         select new
                         {
                             p.TenPB
                         }).FirstOrDefault();
            txtBoPhan.EditValue = tenPb.TenPB.ToString();
        }

        private void LoadKhachHang()
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            glkNhaCungCap.Properties.DataSource = (from p in db.tnKhachHangs
                                                   where p.MaTN == this.MaTn //p.isNCC == true && 
                                                   select new
                                                   {
                                                       TenVT = p.HoKH + " " + p.TenKH,
                                                       TenNCC = p.KyHieu,
                                                       MaNCC = p.MaKH
                                                   }).ToList();
        }

        #endregion

        private void TaoLichThanhToan()
        {
            //_hd.RowID > 0
            if (_hd.RowID > 0 & _isTaoLichThanhToan == false) return;

            if (gvLichThanhToan.RowCount > 0 & _hd.RowID <=0)
            {
                gvLichThanhToan.FocusedRowHandle = -1;
                gvLichThanhToan.SelectAll();
                gvLichThanhToan.DeleteSelectedRows();
            }

            int dotThanhToan = 0;
            var tuNgay = deNgayHieuLuc.DateTime;

            while (tuNgay.CompareTo(deNgayHetHan.DateTime) < 0)
            {
                dotThanhToan++;

                decimal kyThanhToan = (decimal) spneKyThanhToan.Value;
                var denNgay = tuNgay.AddMonths((int) spneKyThanhToan.Value).AddDays(-1);
                if (denNgay.CompareTo(deNgayHetHan.DateTime) > 0)
                {
                    denNgay = deNgayHetHan.DateTime;
                    kyThanhToan = Library.Common.GetTotalMonth(tuNgay, denNgay);
                }

                if (kyThanhToan > 0)
                {
                    gvLichThanhToan.AddNewRow();
                    gvLichThanhToan.SetFocusedRowCellValue("DotThanhToan", dotThanhToan);
                    gvLichThanhToan.SetFocusedRowCellValue("TuNgay", tuNgay);
                    gvLichThanhToan.SetFocusedRowCellValue("DenNgay", denNgay);
                    gvLichThanhToan.SetFocusedRowCellValue("SoThang", kyThanhToan);
                    gvLichThanhToan.SetFocusedRowCellValue("MaLoaiTien", (int?) glkLoaiTien.EditValue);
                    gvLichThanhToan.SetFocusedRowCellValue("DienGiai",string.Format("Tiền thuê từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tuNgay, denNgay));
                    gvLichThanhToan.SetFocusedRowCellValue("TyGia", (decimal) spinTyGia.Value);
                }

                tuNgay = denNgay.AddDays(1);
                if ((int)spneKyThanhToan.Value <= 0) return;
            }

            gvLichThanhToan.RefreshData();
        }

        private void gvChiTiet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.SetFocusedRowCellValue("TaiKhoanNo", "1111");
            gv.SetFocusedRowCellValue("SoCongViec", 0);
            gv.SetFocusedRowCellValue("DonGia", 0);
            gv.SetFocusedRowCellValue("SoTien", 0);
            gv.SetFocusedRowCellValue("MaHopDong", txtSohopDong.Text);
        }

        public void TinhTien(decimal soCongViecs,decimal DonGias)
        {
            try
            {
                decimal soCongViec = 0, donGia = 0;
                if (soCongViecs > 0)
                    soCongViec = soCongViecs;
                else
                    soCongViec = (gv.GetFocusedRowCellValue("SoCongViec") as decimal?) ?? 0;
                if (DonGias > 0)
                {
                    donGia = DonGias;
                }
                else
                {
                    donGia = (gv.GetFocusedRowCellValue("DonGia") as decimal?) ?? 0;
                }
                decimal tg = 0;
                var ptVat = decimal.Parse(spneVAT.EditValue.ToString());
                decimal tongTienChuaThue = 0;
                decimal soTien = 0;
                if (soCongViec >= 0)
                {
                    if (donGia >= 0)
                    {
                        soTien = soCongViec * donGia;
                        gv.SetFocusedRowCellValue("SoTien", soTien);
                    }
                }
                if (gv.RowCount > 0)
                {
                    for (int i = 0; i < gv.RowCount - 1; i++)
                    {
                        tongTienChuaThue += decimal.Parse(gv.GetRowCellValue(i, colSoTien).ToString());
                    }

                }
                if (ptVat > 0)
                {
                    _tienThue = (ptVat / 100) * tongTienChuaThue;
                    lblTienThue.Text =System.String.Format("{0:#,0}", _tienThue);
                }
                _tienChuaThue = tongTienChuaThue;
                _tienSauThue = ((ptVat / 100) * tongTienChuaThue) + tongTienChuaThue;
                lblTienChuaThue.Text = System.String.Format("{0:#,0}", tongTienChuaThue);
                lblTienSauThue.Text = System.String.Format("{0:#,0}", _tienSauThue);
            }
            catch (System.Exception ex)
            {
                //Library.DialogBox.Error("Lỗi tính tiền: " + ex);
            }
        }

        private void spnSoCongViec_EditValueChanged(object sender, System.EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit sp = (DevExpress.XtraEditors.SpinEdit)sender;
            gv.SetFocusedRowCellValue("SoCongViec", sp.Value);
            TinhTien(sp.Value,0);
        }

        private void spnDonGia_EditValueChanged(object sender, System.EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit sp = (DevExpress.XtraEditors.SpinEdit)sender;
            gv.SetFocusedRowCellValue("DonGia", sp.Value);
            TinhTien(0, sp.Value);
        }

        private void spneVAT_EditValueChanged(object sender, System.EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit sp = (DevExpress.XtraEditors.SpinEdit)sender;
            TinhTien(0, 0);
        }

        private void frmEdit_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            try
            {
                _db.Dispose();
                
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi: " + ex.Message);
            }
        }

        private void LuuLichThanhToan()
        {
            _hd.hdctnLichThanhToans.ToList().ForEach(_ =>
            {
                _.KhachHangId = (int) glkNhaCungCap.EditValue;
                if (chkCbxMatBang.EditValue != null) _.MatBangIds = chkCbxMatBang.EditValue.ToString();
                _.MatBangNames = chkCbxMatBang.Properties.GetDisplayText("MaSoMB");
            });
        }

        private void LuuHopDong()
        {
            _hd.TrangThai = 0;
            _hd.SoHopDong = txtSohopDong.Text;
            _hd.MaToaNha = MaTn.ToString();
            _hd.SoChungTu = txtSoChungTu.Text;
            _hd.NhaCungCap = glkNhaCungCap.EditValue.ToString();
            _hd.LoaiTien = glkLoaiTien.EditValue.ToString();
            _hd.TyGia = spinTyGia.Value;
            _hd.TaiKhoanCo = "3311";
            _hd.DiaChi = txtDiaChi.Text;
            _hd.MaCongViec = glkNhomCongViec.EditValue.ToString();
            _hd.TienChuaThue = _tienChuaThue;
            _hd.VAT = spneVAT.Value;
            _hd.NgayKy = deNgayKy.DateTime;
            _hd.NgayHieuLuc = deNgayHieuLuc.DateTime;
            _hd.NgayHetHan = deNgayHetHan.DateTime;
            _hd.KyThanhToan = int.Parse(spneKyThanhToan.Value.ToString());
            
            _hd.TienThue = _tienThue;
            _hd.TienSauThue = _tienSauThue;
            _hd.IsPhuLuc = false;
            _hd.SoHopDongCha = _soHopDongCha;
            _hd.SoBienBanMoiBenGiu = txtMoiBenGiu.Text;
            _hd.NoiLamViecName = txtNoiLamViec.Text;

            if (chkCbxMatBang.EditValue != null) _hd.NoiLamViecIds =  chkCbxMatBang.EditValue.ToString();
            _hd.NoiLamViecNames = chkCbxMatBang.Properties.GetDisplayText("MaSoMB");
            if (glkTrangThai.EditValue != null) _hd.TrangThai = (int) glkTrangThai.EditValue;

            if (glkLoai.EditValue.ToString() == HopDongThueNgoai.Class.TenHienThi.PHU_LUC)
            {
                _hd.IsPhuLuc = true;
                _hd.HopDongChaId = (int) glkHopDong.EditValue;
            }
        }

        private void SaveHistoryHopDong()
        {
            _historyHopDong = new Library.hdctnLichSu();
            _historyHopDong.DateCreate = System.DateTime.UtcNow.AddHours(7);
            _historyHopDong.Description = "Cập nhật hợp đồng";
            _historyHopDong.HopDongNo = _hd.SoHopDong;
            _historyHopDong.LoaiId = 2;
            _historyHopDong.UserId = Library.Common.User.MaNV;
            _historyHopDong.UserName = Library.Common.User.HoTenNV;
            _db.hdctnLichSus.InsertOnSubmit(_historyHopDong);
        }

        private void CreateHistoryTrangThai()
        {
            _historyTrangThai = new Library.hdctnLichSu();
            _historyTrangThai.DateCreate = System.DateTime.UtcNow.AddHours(7);
            _historyTrangThai.LoaiId = 1;
            _historyTrangThai.UserId = Library.Common.User.MaNV;
            _historyTrangThai.UserName = Library.Common.User.HoTenNV;
            _db.hdctnLichSus.InsertOnSubmit(_historyTrangThai);
        }

        private void SaveHistoryTrangThai()
        {
            _historyTrangThai.HopDongNo = _hd.SoHopDong;
        }

        private void GlkTrangThai_EditValueChanged(object sender,System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                _historyTrangThai.Description = "Đổi sang trạng thái: " + item.Properties.View.GetFocusedRowCellValue("TenTrangThai").ToString();
            }
            catch { }
        }

        private void glkLoai_EditValueChanged(object sender,System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                HienThi(item.EditValue.ToString());
                GetSoHopDong();
            }
            catch{}
        }

        private void glkHopDong_EditValueChanged(object sender,System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                _soHopDongCha = item.Properties.View.GetFocusedRowCellValue("SoHopDong").ToString();
            }
            catch{}
        }

        private void deNgayHieuLuc_EditValueChanged(object sender,System.EventArgs e)
        {
            TaoLichThanhToan();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Kiểm tra

            if (glkNhaCungCap.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn Nhà Cung Cấp. Xin cảm ơn!");
                return;
            }

            if (glkNhomCongViec.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn Nhóm Công Việc. Xin cảm ơn!");
                return;
            }

            if (glkLoai.EditValue.ToString() == HopDongThueNgoai.Class.TenHienThi.PHU_LUC)
            {
                if (glkHopDong.EditValue == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn hợp đồng");
                    return;
                }
            }

            #endregion

            try
            {
                LuuHopDong();
                LuuLichThanhToan();
                SaveHistoryHopDong();
                SaveHistoryTrangThai();

                _db.SubmitChanges();

                Library.DialogBox.Success("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Không lưu được hợp đồng, lỗi: " + ex.Message);
            }

            finally
            {
                _db.Dispose();
                this.Close();
            }
        }

        private void BtnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void GlkNhaCungCap_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                Library.tnKhachHang khachHang = GetKhachHangById((int?) item.EditValue);
                if (khachHang == null) return;
                txtDiaChi.Text = khachHang.DCLL;
            }
            catch (System.Exception ex)
            {
                _lError.Add(e.GetType() + ex.Message);
            }
        }

        private Library.tnKhachHang GetKhachHangById(int? khachHangId)
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            return db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == khachHangId);
        }

        private void GlkLoaiTien_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                if (item.Properties.View.GetFocusedRowCellValue("TyGia") == null)
                {
                    Library.LoaiTien loaiTien = GetLoaiTienById((int?) item.EditValue);
                    if (loaiTien == null) return;
                    spinTyGia.EditValue = loaiTien.TyGia;
                }
                else spinTyGia.EditValue = item.Properties.View.GetFocusedRowCellValue("TyGia");
                TaoLichThanhToan();
            }
            catch (System.Exception ex)
            {
                _lError.Add(e.GetType() + ex.Message);
            }
        }

        private Library.LoaiTien GetLoaiTienById(int? loaiTienId)
        {
            return _db.LoaiTiens.FirstOrDefault(_ => _.ID == loaiTienId);
        }

        private void GlkNhomCongViec_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                if (item.Properties.View.GetFocusedRowCellValue("RowID") == null)
                {
                    Library.hdctnNhomCongViec nhomCongViec = GetNhomCongViecByMaSoNhom(item.EditValue.ToString());
                    if (nhomCongViec == null) return;
                    _hd.NhomCongViecId = nhomCongViec.RowID;
                }
                else _hd.NhomCongViecId = (int) item.Properties.View.GetFocusedRowCellValue("RowID");

                var lCongViec = _db.hdctnCongViecs.Where(_ => _.MaNhomCongViec == glkNhomCongViec.EditValue.ToString()).ToList();
                //lkCongViec.DataSource = lCongViec;
                glkCongViec.DataSource = lCongViec;
                if ((string)glkNhomCongViec.EditValue == _maNhomCongViec) return;

                gv.PostEditor();

                _hd.hdctnChiTietHopDongThueNgoais.Clear();
                var rowCount = gv.RowCount;
                for (int i = rowCount; i >= 0; i--) gv.DeleteRow(i);

                foreach (var cv in lCongViec)
                {
                    gv.AddNewRow();
                    gv.SetFocusedRowCellValue("MaCongViec", cv.RowID);
                    gv.SetFocusedRowCellValue("TaiKhoanNo", "1111");
                    gv.SetFocusedRowCellValue("DonGia", 0);
                    gv.SetFocusedRowCellValue("SoCongViec", 0);
                    gv.SetFocusedRowCellValue("SoTien", 0);
                    gv.SetFocusedRowCellValue("MaHopDong", txtSohopDong.Text);
                }

                gv.PostEditor();
            }
            catch (System.Exception ex)
            {
                _lError.Add(e.GetType() + ex.Message);
            }
        }

        private Library.hdctnNhomCongViec GetNhomCongViecByMaSoNhom(string maSoNhomCongViec)
        {
            return _db.hdctnNhomCongViecs.FirstOrDefault(_ => _.MaNhomCongViec.ToLower() == maSoNhomCongViec.ToLower());
        }

        private void ItemLuuDiaChi_Click(object sender, System.EventArgs e)
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            if (glkNhaCungCap.EditValue == null) return;
            Library.tnKhachHang khachHang = db.tnKhachHangs.FirstOrDefault(_=>_.MaKH == (int?)glkNhaCungCap.EditValue);
            if (khachHang == null) return;

            khachHang.DCLL = txtDiaChi.Text;

            try
            {
                db.SubmitChanges();
                Library.DialogBox.Success();
                LoadKhachHang();
                glkNhaCungCap.EditValue = khachHang.MaKH;
            }
            catch (System.Exception ex)
            {
                _lError.Add(e.GetType() + ex.Message);
            }
        }

        private void deNgayHetHan_EditValueChanged(object sender,System.EventArgs e)
        {
            TaoLichThanhToan();
        }

        private void spneKyThanhToan_EditValueChanged(object sender,System.EventArgs e)
        {
            TaoLichThanhToan();
        }

        private void itemXoaLichThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvLichThanhToan.GetSelectedRows();
            if (KiemTra(indexs)) return;

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            foreach (var i in indexs)
            {
                DeleteLichThanhToan((int)gvLichThanhToan.GetRowCellValue(i, "Id"));
            }

            gvLichThanhToan.DeleteSelectedRows();
        }

        #region Xóa lịch thanh toán

        private bool KiemTra(int[] indexs)
        {
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch thanh toán");
                return true;
            }
            return false;
        }

        private void DeleteLichThanhToan(int? lichThanhToanId)
        {
            var congNoNhaCungCaps = _db.hdctnCongNoNhaCungCaps.Where(_ => _.LichThanhToanId == lichThanhToanId);
            foreach (var item in congNoNhaCungCaps)
            {
                if (item.IsPhieuChi == false) LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu(item.PhieuThuId);
                else LandSoftBuilding.Fund.Class.PhieuChi.DeletePhieuChi(item.PhieuChiId);

                var hopDong = GetHopDongById(item.HopDongId);
                if (hopDong == null) continue;
                hopDong.DaTra = item.IsPhieuChi == false ? hopDong.DaTra : hopDong.DaTra.GetValueOrDefault() - item.SoTien;
                hopDong.DaThu = item.IsPhieuChi == false ? hopDong.DaThu.GetValueOrDefault() - item.SoTien : hopDong.DaThu;
            }

            _db.hdctnCongNoNhaCungCaps.DeleteAllOnSubmit(congNoNhaCungCaps);
            _db.hdctnLichThanhToans.DeleteAllOnSubmit(_db.hdctnLichThanhToans.Where(_ => _.Id == lichThanhToanId));
        }

        private void itemThayDoiTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (_hd == null) return;
                using(HopDongThueNgoai.ThayDoiTac.FrmThayDoiTac frm = new HopDongThueNgoai.ThayDoiTac.FrmThayDoiTac { HopDongId = _hd.RowID})
                {
                    frm.ShowDialog();
                    if(frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        glkNhaCungCap.EditValue = frm.KhachHangId;
                        txtDiaChi.Text = frm.DiaChi;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _lError.Add(ex.Message);
            }
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetHopDongById(int? hopDongId)
        {
            return _db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == hopDongId);
        }

        #endregion

        private void itemTaoLichThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuocTaoLichThanhToan(true);
            TaoLichThanhToan();
        }
    }
}