using System.Linq;

namespace LandSoftBuilding.Fund.ChiThuTruoc.ChiThuTruoc
{
    public partial class FrmChiThuTruocEdit : DevExpress.XtraEditors.XtraForm
    {
        #region Tham số

        /// <summary>
        /// Id phiếu chi
        /// </summary>
        public int? PhieuChiId { get; set; }

        /// <summary>
        /// Mã tòa nhà
        /// </summary>
        public byte? BuildingId { get; set; }

        /// <summary>
        /// Hình thức chi: chi trả thu trước hay chuyển khoản thu trước
        /// </summary>
        public int? HinhThucChiId { get; set; }

        /// <summary>
        /// Tên hình thức chi
        /// </summary>
        public string HinhThucChiName { get; set; }

        /// <summary>
        /// data masterdata
        /// </summary>
        private Library.MasterDataContext _db = new Library.MasterDataContext();

        /// <summary>
        /// phiếu chi cần thêm hoặc sửa
        /// </summary>
        private Library.pcPhieuChi _pcPhieuChi;

        /// <summary>
        /// danh sách phiếu chi chi tiết
        /// </summary>
        private System.Collections.Generic.List<Library.pcChiTiet> _pcChiTiets;

        /// <summary>
        /// Phiếu thu
        /// </summary>
        private Library.ptPhieuThu _phieuThu;

        #endregion

        public FrmChiThuTruocEdit()
        {
            InitializeComponent();
        }

        private void FrmChiThuTruocEdit_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this);

            ShowAndHide();

            LoadDanhMuc();
            GiaTriMacDinh();

            #region Phiếu chi

            _pcPhieuChi = GetPhieuChiById(PhieuChiId);
            if (_pcPhieuChi == null)
            {
                _pcPhieuChi = new Library.pcPhieuChi();
                _pcPhieuChi.MaNVN = Library.Common.User.MaNV;
                _pcPhieuChi.NgayNhap = System.DateTime.UtcNow.AddHours(7);
                _db.pcPhieuChis.InsertOnSubmit(_pcPhieuChi);

                txtSoPhieu.Text = Library.Common.CreatePhieuChi(BuildingId.Value, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year);
            }
            else
            {
                _pcPhieuChi.MaNVS = Library.Common.User.MaNV;
                _pcPhieuChi.NgaySua = System.DateTime.UtcNow.AddHours(7);
            }

            if (_pcPhieuChi != null) txtSoPhieu.Text = _pcPhieuChi.SoPC;
            if (_pcPhieuChi.NgayChi != null) dateNgayChi.DateTime = (System.DateTime)_pcPhieuChi.NgayChi;
            if (_pcPhieuChi.SoTien != null) spinTienChi.EditValue = _pcPhieuChi.SoTien;
            glkNguoiChi.EditValue = Library.Common.User.MaNV;
            glkKhachHang.EditValue = _pcPhieuChi.MaNCC;
            txtNguoiNhan.EditValue = _pcPhieuChi.NguoiNhan;
            txtDiaChi.EditValue = _pcPhieuChi.DiaChiNN;
            glkNganHang.EditValue = _pcPhieuChi.MaTKNH;
            txtDienGiai.EditValue = _pcPhieuChi.LyDo;
            glkPhuongThucThanhToan.EditValue = _pcPhieuChi.MaTKNH == null ? 0 : 1;
            txtChungTuGoc.EditValue = _pcPhieuChi.ChungTuGoc;
            if (_pcPhieuChi.TuMatBangId != null) glkTuMatBang.EditValue = _pcPhieuChi.TuMatBangId;

            if (_pcChiTiets != null)
            {
                foreach (var i in _pcChiTiets)
                {
                    var ct = new Library.pcChiTiet();
                    ct.ID = i.ID;
                    ct.DienGiai = i.DienGiai;
                    ct.SoTien = i.SoTien;
                    _pcPhieuChi.pcChiTiets.Add(ct);
                }
                spinTienChi.EditValue = TinhTien();
                if (glkKhachHang.EditValue != null)
                {
                    spinTienThuTruoc.EditValue = TinhThuTruoc((int?)glkKhachHang.EditValue);
                    spinTienConLai.EditValue = TinhThuThua(spinTienThuTruoc.Value, spinTienChi.Value);
                }
            }

            gridControl1.DataSource = _pcPhieuChi.pcChiTiets;

            #endregion

            #region Get phiếu thu

            if(HinhThucChiId == 2)
            {
                _phieuThu = GetPhieuThu(_pcPhieuChi.PhieuThuId);
                if(_phieuThu == null)
                {
                    _phieuThu = new Library.ptPhieuThu();
                    _db.ptPhieuThus.InsertOnSubmit(_phieuThu);
                }
                if (_phieuThu.MaMB != null) glkDenMatBang.EditValue = (int?)_phieuThu.MaMB;
            }

            #endregion

            #region Load mẫu in
            LoadMauIn();
            #endregion
        }

        private void LoadMauIn()
        {
            try
            {
                var ltReport = (from rp in _db.rptReports
                                join tn in _db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == (byte)BuildingId & rp.GroupID == 6
                                orderby rp.Rank
                                select new { rp.ID, rp.Name }).ToList();

                barPrint.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += ItemPrint_ItemClick;
                    barManager1.Items.Add(itemPrint);
                    barPrint.ItemLinks.Add(itemPrint);
                }
            }
            catch { }
        }

        private void ItemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)_pcPhieuChi.ID;
            //if (id == null)
            //{
            //    Library.DialogBox.Error("Vui lòng chọn [Phiếu chi] cần xem");
            //    return;
            //}

            //var maTN = (byte)itemToaNha.EditValue;
            //var db = new MasterDataContext();

            //DevExpress.XtraReports.UI.XtraReport rpt = null;
            //var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)e.Item.Tag);
            //if (objForm != null)
            //{
            //    var rtfText = BuildingDesignTemplate.Class.MergeField.PhieuThu(id.Value, objForm.Content);
            //    var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
            //    frm.ShowDialog(this);
            //}

            Save(true, (int)e.Item.Tag);
        }

        #region Danh mục

        /// <summary>
        /// Hiển thị và ẩn đối với phiếu chuyển tiền thu trước
        /// </summary>
        private void ShowAndHide()
        {
            if(HinhThucChiId==2)
            {
                this.Text = "Phiếu chuyển tiền thu trước sang mặt bằng khác";

                layoutControlItemDenMatBang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                layoutControlItemTuMatBang.Text = "2. Từ mặt bằng";
                layoutControlItemDenMatBang.Text = "3. Đến mặt bằng";
                layoutControlItemNguoiNhan.Text = "4. Người nhận";
                layoutControlItemDiaChi.Text = "5. Địa chỉ";
                layoutControlItemNganHang.Text = "6. Ngân hàng";
                layoutControlItemDienGiai.Text = "7. Diễn giải";
                layoutControlItemKemTheo.Text = "8. Kèm theo";
            }
        }

        private Library.ptPhieuThu GetPhieuThu(int? id)
        {
            return _db.ptPhieuThus.FirstOrDefault(_ => _.ID == id);
        }

        /// <summary>
        /// Load các danh mục: thanh toán, khách hàng, tài khoản ngân hàng, nhân viên chi
        /// </summary>
        private void LoadDanhMuc()
        {
            LoadPhuongThucThanhToan();
            LoadKhachHang();
            LoadMatBang();
            LoadDenMatBang();
            LoadTaiKhoanNganHang();
            LoadNhanVienChi();
        }

        /// <summary>
        /// Gán các giá trị mặc định
        /// </summary>
        private void GiaTriMacDinh()
        {
            glkPhuongThucThanhToan.EditValue = 1;
            dateNgayChi.DateTime = System.DateTime.UtcNow.AddHours(7);
        }

        /// <summary>
        /// Load phương thức thanh toán: tiền mặt, chuyển khoản...
        /// </summary>
        private void LoadPhuongThucThanhToan()
        {
            var lPhuongThucThanhToan = new System.Collections.Generic.List<LandSoftBuilding.Fund.ChiThuTruoc.Class.Type>();
            lPhuongThucThanhToan.Add(new Class.Type() { Id = 0, Name = "Tiền mặt" });
            lPhuongThucThanhToan.Add(new Class.Type() { Id = 1, Name = "Chuyển khoản" });
            glkPhuongThucThanhToan.Properties.DataSource = lPhuongThucThanhToan;
            
        }

        /// <summary>
        /// Load danh sách khách hàng
        /// </summary>
        private void LoadKhachHang()
        {
            glkKhachHang.Properties.DataSource = _db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { _.MaKH, Name = _.IsCaNhan == true ? _.HoKH + " " + _.TenKH : _.CtyTen, KyHieu = _.KyHieu, DiaChi = _.DCLL });
        }

        /// <summary>
        /// Load từ mặt bằng
        /// </summary>
        private void LoadMatBang()
        {
            glkTuMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { Id = _.MaMB, Name = _.MaSoMB }).ToList();
        }

        /// <summary>
        /// Load mặt bằng đến
        /// </summary>
        private void LoadDenMatBang()
        {
            glkDenMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { Id = _.MaMB, Name = _.MaSoMB }).ToList();
        }

        /// <summary>
        /// Load danh sách tài khoản ngân hàng
        /// </summary>
        private void LoadTaiKhoanNganHang()
        {
            glkNganHang.Properties.DataSource = (from tk in _db.nhTaiKhoans join nh in _db.nhNganHangs on tk.MaNH equals nh.ID where tk.MaTN == BuildingId select new { Id = tk.ID, Name = tk.SoTK, tk.ChuTK, nh.TenNH, nh.DiaChi }).ToList();
        }

        /// <summary>
        /// Load danh sách nhân viên thực hiện phiếu chi
        /// </summary>
        private void LoadNhanVienChi()
        {
            glkNguoiChi.Properties.DataSource = _db.tnNhanViens.Where(_ => _.MaTN == BuildingId).Select(_ => new { Id = _.MaNV, Name = _.HoTenNV });
        }

        #endregion

        #region Tính tiền

        /// <summary>
        /// Tính tiền chi, spinTienChi
        /// </summary>
        /// <returns></returns>
        private decimal? TinhTien()
        {
            return _pcPhieuChi.pcChiTiets.Sum(_ => _.SoTien).GetValueOrDefault();
        }

        /// <summary>
        /// Tính thu trước theo khách hàng
        /// Khi thu trước khách hàng, không cần theo phiếu thu
        /// </summary>
        /// <param name="khachHangId">Mã khách hàng</param>
        /// <returns></returns>
        private decimal? TinhThuTruoc(int? khachHangId)
        {
            // _.IsPhieuThu == true & 
            return _db.SoQuy_ThuChis.Where(_ =>_.MaKH == khachHangId).Sum(_ => _.ThuThua.GetValueOrDefault() - _.KhauTru.GetValueOrDefault());
        }

        /// <summary>
        /// Tính thu thừa theo thu trước và tiền chi
        /// </summary>
        /// <param name="thuTruoc">Tiền thu trước</param>
        /// <param name="tienChi">Tiền chi</param>
        /// <returns></returns>
        private decimal? TinhThuThua(decimal? thuTruoc, decimal? tienChi)
        {
            return thuTruoc.GetValueOrDefault() - tienChi.GetValueOrDefault();
        }

        #endregion

        /// <summary>
        /// Get phiếu chi theo Id
        /// </summary>
        /// <param name="id">Id phiếu chi</param>
        /// <returns>phiếu chi</returns>
        private Library.pcPhieuChi GetPhieuChiById(int? id)
        {
            return _db.pcPhieuChis.FirstOrDefault(_ => _.ID == id);
        }

        private void glkPhuongThucThanhToan_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
            if (item == null) return;
            glkNganHang.Enabled = (int)item.EditValue == 1;
            txtSoPhieu.Text = Library.Common.CreatePhieuChi(BuildingId.Value, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year);
        }

        private void glkNganHang_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
            txtTenNganHang.EditValue = null;
            if (item.EditValue != null & (int)glkPhuongThucThanhToan.EditValue == 1) txtTenNganHang.EditValue = _db.nhTaiKhoans.First(_ => _.ID == (int)item.EditValue).nhNganHang.TenNH;
            else txtTenNganHang.EditValue = null;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                spinTienChi.EditValue = this.TinhTien();
                spinTienConLai.EditValue = this.TinhThuThua((decimal?)spinTienThuTruoc.Value, (decimal?)spinTienChi.Value);

                var sDienGiai = "";
                foreach(var i in _pcPhieuChi.pcChiTiets)
                {
                    sDienGiai += "; " + i.DienGiai;
                }
                sDienGiai = sDienGiai.Trim().Trim(';');
                txtDienGiai.Text = sDienGiai;
            }
            catch { }
        }

        private void glkKhachHang_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;

                var rowIndex = glkKhachHang.Properties.GetRowByKeyValue(item.EditValue);
                if(rowIndex!=null)
                {
                    var type = rowIndex.GetType();
                    txtNguoiNhan.Text = type.GetProperty("Name").GetValue(rowIndex, null).ToString();
                    txtDiaChi.Text = type.GetProperty("DiaChi").GetValue(rowIndex, null).ToString();

                    var matBang = _db.mbMatBangs.FirstOrDefault(_ => _.MaKH == (int?)item.EditValue & _.MaTN == BuildingId);
                    if(matBang!=null)
                    {
                        glkTuMatBang.EditValue = matBang.MaMB;
                    }
                }

                var tienThuTruoc = TinhThuTruoc((int)item.EditValue);
                spinTienThuTruoc.EditValue = tienThuTruoc;
                if(tienThuTruoc<=0)
                {
                    Library.DialogBox.Alert("Khách hàng này không có thu trước");
                }
            }
            catch { }
        }

        private void dateNgayChi_EditValueChanged(object sender, System.EventArgs e)
        {
            txtSoPhieu.Text = Library.Common.CreatePhieuChi(BuildingId.Value, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save(false, null);
        }

        private void Save(bool print, int? reportId)
        {
            try
            {
                gridView1.UpdateCurrentRow();

                #region Kiểm tra

                if (KiemTraLoi()) return;

                #endregion

                #region Xóa sổ quỹ phiếu thu
                if (HinhThucChiId == 2 & _pcPhieuChi.PhieuThuId != null)
                {
                    XoaSoQuyPhieuThu(_pcPhieuChi.PhieuThuId);
                }
                #endregion

                #region Lưu dữ liệu phiếu chi

                XoaSoQuyPhieuChi();

                LuuThongTinChungPhieuChi(_pcPhieuChi.MaNVN, _pcPhieuChi.NgayNhap, _pcPhieuChi.MaNVS, _pcPhieuChi.NgaySua);
                LuuChiTietPhieuChi();

                if (HinhThucChiId == 2)
                {
                    LuuThongTinChungPhieuThu();
                    LuuChiTietPhieuThu();

                    _pcPhieuChi.PhieuThuId = _phieuThu.ID;
                    _pcPhieuChi.PhieuThuNo = _phieuThu.SoPT;
                    _pcPhieuChi.DenMatBangId = _phieuThu.MaMB;
                    _pcPhieuChi.DenMatBangNo = _phieuThu.DenMatBangNo;
                }

                _db.SubmitChanges();

                if (print == true & reportId !=null) { InPhieuChi(reportId); }
                else
                { 
                    Library.DialogBox.Success();
                }
                Close();
                #endregion
            }
            catch (System.Exception ex) { Library.DialogBox.Error("Khi lưu đã xảy ra lỗi: " + ex.Message + "\nVui lòng báo với kỹ thuật."); }
        }

        private void InPhieuChi(int? reportId)
        {
            var id = (int?)_pcPhieuChi.ID;
            if (id == null)
            {
                Library.DialogBox.Error("Không in được phiếu chi");
                return;
            }


            DevExpress.XtraReports.UI.XtraReport rpt = null;
            var objForm = _db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)reportId);
            if (objForm != null)
            {
                var rtfText = BuildingDesignTemplate.Class.MergeField.PhieuChi(id.Value, objForm.Content);
                var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
                frm.ShowDialog(this);
            }
        }

        #region Kiểm tra lỗi

        /// <summary>
        /// Kiểm tra lỗi trước khi lưu
        /// </summary>
        /// <returns>bool</returns>
        private bool KiemTraLoi()
        {
            if (spinTienConLai.Value < 0)
            {
                Library.DialogBox.Error("Không được chi trả nhiều hơn tiền thu trước");
                spinTienConLai.Focus();
                return true;
            }

            if (glkKhachHang.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn khách hàng");
                glkKhachHang.Focus();
                return true;
            }

            if (glkPhuongThucThanhToan.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn hình thức thanh toán");
                glkPhuongThucThanhToan.Focus();
                return true;
            }

            if ((int)glkPhuongThucThanhToan.EditValue == 1 & glkNganHang.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                glkPhuongThucThanhToan.Focus();
                return true;
            }

            if (txtSoPhieu.Text.Trim() == "")
            {
                Library.DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPhieu.Focus();
                return true;
            }
            else
            {
                var exits = _db.pcPhieuChis.Where(_ => _.MaTN == BuildingId & _.SoPC == txtSoPhieu.Text & _.ID != _pcPhieuChi.ID).Count();
                if (exits > 0)
                {
                    Library.DialogBox.Error("Trùng số phiếu chi, vui lòng kiểm tra lại.");
                    txtSoPhieu.Focus();
                    return true;
                }
            }

            if (dateNgayChi.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng nhập ngày chi");
                dateNgayChi.Focus();
                return true;
            }

            if (glkNguoiChi.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn người chi");
                glkNguoiChi.Focus();
                return true;
            }

            // Kiểm tra chuyển tiền
            if (HinhThucChiId == 2)
            {
                if (glkDenMatBang.EditValue == null)
                {
                    Library.DialogBox.Alert("Vui lòng chọn mặt bằng chuyển tiền đến");
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Lưu phiếu chi

        /// <summary>
        /// Xóa sổ quỹ phiếu chi
        /// </summary>
        private void XoaSoQuyPhieuChi()
        {
            if (PhieuChiId != null)
            {
                _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(_ => _.IDPhieu == PhieuChiId & _.IsPhieuThu == false));
            }
        }

        /// <summary>
        /// Lưu thông tin pcPhieuChi
        /// </summary>
        /// <param name="nguoiNhap">Ngày nhập</param>
        /// <param name="ngayNhap">Ngày sửa</param>
        /// <param name="nguoiSua">Người sửa</param>
        /// <param name="ngaySua">Ngày sửa</param>
        private void LuuThongTinChungPhieuChi(int? nguoiNhap, System.DateTime? ngayNhap, int? nguoiSua, System.DateTime? ngaySua)
        {
            var nganHangId = glkNganHang.EditValue != null ? (int?)glkNganHang.EditValue : null;
            var matBangId = glkTuMatBang.EditValue != null ? (int?)glkTuMatBang.EditValue : null;
            var matBangNo = glkTuMatBang.EditValue != null ? glkTuMatBang.Properties.GetDisplayTextByKeyValue((int?)glkTuMatBang.EditValue) : null;

            _pcPhieuChi = LandSoftBuilding.Fund.ChiThuTruoc.Class.PhieuChi.SetPhieuChi(_pcPhieuChi, BuildingId, txtSoPhieu.Text, (System.DateTime)dateNgayChi.EditValue, (decimal)spinTienChi.EditValue, (int?)glkNguoiChi.EditValue, (int?)glkKhachHang.EditValue, txtNguoiNhan.Text, txtDiaChi.Text, nguoiNhap, (int?)glkNguoiChi.EditValue, nganHangId, txtChungTuGoc.Text, txtDienGiai.Text, 3, "Chi cho Khách hàng", HinhThucChiId, HinhThucChiName, matBangId, matBangNo, null, null, null, null, ngayNhap, nguoiSua, ngaySua);

            _db.SubmitChanges();
        }

        /// <summary>
        /// Lưu phiếu chi chi tiết pcChiTiet
        /// </summary>
        private void LuuChiTietPhieuChi()
        {
            // get mã mặt bằng theo khách hàng

            foreach (var item in _pcPhieuChi.pcChiTiets)
            {
                Library.Common.SoQuy_Insert(_db, dateNgayChi.DateTime.Month, (int)dateNgayChi.DateTime.Year, BuildingId, _pcPhieuChi.MaNCC, (int?)glkTuMatBang.EditValue, _pcPhieuChi.ID, item.ID, dateNgayChi.DateTime, txtSoPhieu.Text, (int?)glkPhuongThucThanhToan.EditValue, 2, false, 0, (decimal?)spinTienChi.Value, -(decimal)spinTienChi.Value, 0, null, "KH", item.DienGiai, Library.Common.User.MaNV, false, false);
            }
            _db.SubmitChanges();
        }

        #endregion

        #region Lưu phiếu thu

        /// <summary>
        /// Xóa sổ quỹ phiếu thu
        /// </summary>
        /// <param name="phieuThuId">Id phiếu thu</param>
        private void XoaSoQuyPhieuThu(int? phieuThuId)
        {
            _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(_ => _.IDPhieu == phieuThuId & _.IsPhieuThu == true));
        }

        /// <summary>
        /// Lưu thông tin chung phiếu thu
        /// </summary>
        private void LuuThongTinChungPhieuThu()
        {
            if (_phieuThu.SoPT == null) _phieuThu.SoPT = LandSoftBuilding.Fund.ChiThuTruoc.Class.PhieuThu.TaoSoPhieuThu((int)glkPhuongThucThanhToan.EditValue, glkDenMatBang.EditValue, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year, (byte)BuildingId, false);

            _phieuThu = LandSoftBuilding.Fund.ChiThuTruoc.Class.PhieuThu.SetptPhieuThu(_phieuThu, BuildingId, _phieuThu.SoPT, dateNgayChi.DateTime, (int?)glkKhachHang.EditValue, (int?)glkNganHang.EditValue, txtNguoiNhan.Text, txtDiaChi.Text, txtDienGiai.Text, txtChungTuGoc.Text, spinTienChi.Value, 2, (int?)glkNguoiChi.EditValue, Library.Common.User.MaNV, null, System.DateTime.UtcNow.AddHours(7), null, false, false, 0, (int?)glkPhuongThucThanhToan.EditValue, glkPhuongThucThanhToan.Properties.GetDisplayTextByKeyValue((int?)glkPhuongThucThanhToan.EditValue), (int?)glkTuMatBang.EditValue, glkTuMatBang.Properties.GetDisplayTextByKeyValue((int?)glkTuMatBang.EditValue), (int?)glkDenMatBang.EditValue, glkDenMatBang.Properties.GetDisplayTextByKeyValue((int?)glkDenMatBang.EditValue), _pcPhieuChi.ID, _pcPhieuChi.SoPC);
            _db.SubmitChanges();
        }

        private void LuuChiTietPhieuThu()
        {
            XoaChiTietPhieuThu(_phieuThu.ID);

            var phieuThuChiTiet = new Library.ptChiTietPhieuThu();
            phieuThuChiTiet = LandSoftBuilding.Fund.ChiThuTruoc.Class.PhieuThu.SetChiTietPhieuThu(phieuThuChiTiet, _phieuThu.ID, txtDienGiai.Text, spinTienChi.Value);
            _phieuThu.ptChiTietPhieuThus.Add(phieuThuChiTiet);

            _db.SubmitChanges();

            Library.Common.SoQuy_Insert(_db, dateNgayChi.DateTime.Month, dateNgayChi.DateTime.Year, BuildingId, (int?)glkKhachHang.EditValue, (int?)glkDenMatBang.EditValue, _phieuThu.ID, phieuThuChiTiet.ID, dateNgayChi.DateTime, _phieuThu.SoPT, (int?)glkPhuongThucThanhToan.EditValue, 2, true, 0, spinTienChi.Value, spinTienChi.Value, 0, null, "KH", txtDienGiai.Text, Library.Common.User.MaNV, false, false);

            _db.SubmitChanges();
        }

        #endregion

        private void XoaChiTietPhieuThu(int? phieuThuId)
        {
            _db.ptChiTietPhieuThus.DeleteAllOnSubmit(_db.ptChiTietPhieuThus.Where(_ => _.MaPT == phieuThuId));
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}