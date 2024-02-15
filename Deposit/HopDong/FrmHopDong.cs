using System.Linq;

namespace Deposit.HopDong
{
    public partial class FrmHopDong : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db;
        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();

        public FrmHopDong()
        {
            InitializeComponent();
        }

        private void FrmHopDong_Load(object sender, System.EventArgs e)
        {
            this.Text = Deposit.Class.Enum.FormName.HOP_DONG_DAT_COC;
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            Library.KyBaoCao objKbc = new Library.KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);

            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new Library.KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemDateFrom.EditValue = objKbc.DateFrom;
            itemDateTo.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                _db = new Library.MasterDataContext();

                var dateFrom = (System.DateTime) itemDateFrom.EditValue;
                var dateTo = (System.DateTime) itemDateTo.EditValue;
                var buildingId = (byte) itemBuilding.EditValue;

                //gc.DataSource = (from p in _db.Dep_HopDongs
                //                 join nt in _db.tnKhachHangs on p.MaNhaThau equals nt.MaKH into nhathau from nt in nhathau.DefaultIfEmpty()
                //    where p.BuildingId == buildingId &
                //          System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(dateFrom, p.DateCreate) >= 0 &
                //          System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(p.DateCreate, dateTo) >= 0
                //    select new
                //    {
                //        p.BuildingId, p.DateCreate, p.DateUpdate, p.LoaiHopDongId, p.LoaiHopDongName, p.Name, p.No,
                //        p.TienTra, p.TongTien, p.ThuPhat, p.TrangThaiName, p.UserCreateName, p.UserUpdateName,
                //        p.Dep_TrangThai.Color, p.Id, ConLai = p.TongTien.GetValueOrDefault() - p.TienTra.GetValueOrDefault() - p.ThuPhat.GetValueOrDefault(),
                //        TenKH = nt.IsCaNhan == true ? nt.TenKH : nt.CtyTen
                //    }).ToList();

                LoadDetail();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi: " + ex);
            }
        }

        private void LoadDetail()
        {
            try
            {
                _db = new Library.MasterDataContext();
                var id = (int?) gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabPhieuThuDatCoc":
                        gcPhieuThuDatCoc.DataSource = Deposit.Class.PhieuThu.GetPhieuThuDatCocByHopDong(id);
                        break;
                    case "tabPhieuThuTienPhat":
                        gcPhieuThuTienPhat.DataSource = Deposit.Class.PhieuThu.GetPhieuThuTienPhatByHopDong(id);
                        break;
                    case "tabPhieuChiHoanTien":
                        gcPhieuChiHoanTien.DataSource = Deposit.Class.PhieuChi.GetPhieuChiByHopDong(id);
                        break;
                    case "tabDoiTac":
                        gcDoiTac.DataSource = _db.Dep_DoiTacs.Where(_ => _.HopDongId == id);
                        break;
                }

            }
            catch (System.Exception ex)
            {
                _lError.Add("LoadDetail: " + ex.Message);
            }
        }

        private bool KiemTraId(int? id)
        {
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu");
                return true;
            }

            return false;
        }

        private Library.Dep_HopDong GetHopDongById(int? id)
        {
            return _db.Dep_HopDongs.FirstOrDefault(_ => _.Id == id);
        }

        private void CbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemThemDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmHopDongEdit() {BuildingId = (byte) itemBuilding.EditValue})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void ItemThemPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("Id");
            if (KiemTraId(id)) return;

            using (var frm = new Deposit.FrmDepositEdit())
            {
                frm.MaTn = (byte)itemBuilding.EditValue;
                frm.IsDepositFather = true;
                frm.HopDongDatCocId = id;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("Id");
            if (KiemTraId(id)) return;

            using (var frm = new Deposit.HopDong.FrmHopDongEdit() {HopDongId = id, BuildingId = (byte) itemBuilding.EditValue})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gv.GetFocusedRowCellValue("Id");
            if (KiemTraId(id)) return;

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var hopDong = GetHopDongById(id);
            if (hopDong == null) return;
            if (hopDong.TongTien > 0 | hopDong.ThuPhat > 0 | hopDong.TienTra > 0)
            {
                Library.DialogBox.Error("Vui lòng xóa phiếu thu chi trước");
                return;
            }
            
            _db.Dep_HopDongs.DeleteOnSubmit(hopDong);

            _db.SubmitChanges();
            LoadData();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void ItemEditPhieuThuDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gvPhieuThuDatCoc.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            using (var frm = new FrmDepositEdit())
            {
                frm.MaPt = id;
                frm.MaTn = (byte)itemBuilding.EditValue;
                frm.IsDepositFather = true;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void ItemXoaPhieuThuDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvPhieuThuDatCoc.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu thu");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            var db = new Library.MasterDataContext();
            foreach (var i in indexs)
            {
                var pt = db.ptPhieuThus.FirstOrDefault(_ => _.ID == (int)gvPhieuThuDatCoc.GetRowCellValue(i, "ID"));
                if (pt != null)
                {
                    if (pt.MaPCT != null)
                    {
                        Library.DialogBox.Alert("Đây là phiếu thu bên chuyển tiền. Vui lòng qua nghiệp vụ chuyển tiền để xóa");
                        return;
                    }

                    #region Update hợp đồng

                    var hopDong = db.Dep_HopDongs.FirstOrDefault(_ => _.Id == pt.HopDongDatCocId);
                    if (hopDong != null) hopDong = Deposit.Class.HopDong.UpdateHopDongAll(hopDong, -pt.TotalReceipts.GetValueOrDefault(), -pt.TotalPay.GetValueOrDefault(), -pt.SoTien.GetValueOrDefault());
                    #endregion

                    #region Lưu lại phiếu thu đã xóa

                    var ptdx = Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(pt.LyDo, pt.MaKH, pt.MaNV, Library.Common.User.MaNV, pt.MaPL, pt.MaTKNH, pt.MaTN, pt.NguoiNop, System.DateTime.UtcNow.AddHours(7), pt.NgayThu, pt.SoPT, pt.SoTien, pt.ChungTuGoc, pt.DiaChiNN);

                    db.ptPhieuThuDaXoas.InsertOnSubmit(ptdx);

                    var queryChiTietPt = db.ptChiTietPhieuThus.Where(p => p.MaPT == pt.ID).ToList();
                    if (queryChiTietPt.Count > 0)
                    {
                        foreach (var qe in queryChiTietPt)
                        {
                            var ptdxChiTiet = Deposit.Class.PhieuThu.CreateChiTietPhieuThuDaXoa(qe.LinkID, pt.SoPT, qe.SoTien, qe.TableName, qe.DienGiai);
                            db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(ptdxChiTiet);
                        }
                    }

                    #endregion

                    db.ptPhieuThus.DeleteOnSubmit(pt);
                    //Xóa Sổ quỹ thu chi
                    db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == (int?)gvPhieuThuDatCoc.GetRowCellValue(i, "ID") && p.IsPhieuThu == true));
                }
            }
            try
            {
                db.SubmitChanges();
                LoadData();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void ItemSuaPhieuChiTienPhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuThuTienPhat.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            using (var frm = new FrmDepositEdit())
            {
                frm.MaPt = id;
                frm.MaTn = (byte)itemBuilding.EditValue;
                frm.IsDepositFather = (bool?)gvPhieuThuTienPhat.GetFocusedRowCellValue("IsDepositFather");
                frm.DepositFatherId = (int?)gvPhieuThuTienPhat.GetFocusedRowCellValue("DepositFatherId");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void ItemXoaPhieuChiTienPhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvPhieuThuTienPhat.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu thu");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            var db = new Library.MasterDataContext();
            foreach (var i in indexs)
            {
                var pt = db.ptPhieuThus.FirstOrDefault(_ => _.ID == (int)gvPhieuThuTienPhat.GetRowCellValue(i, "ID"));
                if (pt != null)
                {
                    if (pt.MaPCT != null)
                    {
                        Library.DialogBox.Alert("Đây là phiếu thu bên chuyển tiền. Vui lòng qua nghiệp vụ chuyển tiền để xóa");
                        return;
                    }
                    #region Lưu lại phiếu thu đã xóa

                    db.ptPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(pt.LyDo, pt.MaKH, pt.MaNV, Library.Common.User.MaNV, pt.MaPL, pt.MaTKNH, pt.MaTN, pt.NguoiNop, System.DateTime.UtcNow.AddHours(7), pt.NgayThu, pt.SoPT, pt.SoTien, pt.ChungTuGoc, pt.DiaChiNN));

                    var queryChiTietPt = db.ptChiTietPhieuThus.Where(p => p.MaPT == pt.ID).ToList();
                    if (queryChiTietPt.Count > 0)
                    {
                        foreach (var qe in queryChiTietPt)
                        {
                            db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreateChiTietPhieuThuDaXoa(qe.LinkID, pt.SoPT, qe.SoTien, qe.TableName, qe.DienGiai));
                        }

                    }

                    #endregion

                    db.ptPhieuThus.DeleteOnSubmit(pt);
                    //Xóa Sổ quỹ thu chi
                    db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == (int?)gvPhieuThuTienPhat.GetRowCellValue(i, "ID") && p.IsPhieuThu == true));
                }
            }
            try
            {
                db.SubmitChanges();

                LoadData();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void ItemSuaPhieuChiHoanTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuChiHoanTien.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            var hopDongDatCocId = (int?)gvPhieuChiHoanTien.GetFocusedRowCellValue("HopDongDatCocId");
            if (hopDongDatCocId == null)
            {
                Library.DialogBox.Alert("Phiếu không có hợp đồng đặt cọc");
                return;
            }

            using (var frm = new Deposit.PhieuChi.FrmWithDrawEdit())
            {
                frm.MaPc = id;
                frm.MaPt = (int?)gvPhieuChiHoanTien.GetFocusedRowCellValue("MaPT");
                frm.HopDongDatCocId = (int?)gvPhieuChiHoanTien.GetFocusedRowCellValue("HopDongDatCocId");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void ItemXoaPhieuChiHoanTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvPhieuChiHoanTien.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn phiếu thu");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            _db = new Library.MasterDataContext();
            foreach (var i in indexs)
            {
                var pc = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(_ => _.ID == (int)gvPhieuChiHoanTien.GetRowCellValue(i, "ID"));
                if (pc != null)
                {
                    #region Lưu lại phiếu thu đã xóa
                    // xóa hết những phiếu thu tự tạo
                    var ptChild = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == pc.PtPhatId);
                    if (ptChild != null)
                    {
                        _db.ptPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(ptChild.LyDo, ptChild.MaKH, ptChild.MaNV, Library.Common.User.MaNV, ptChild.MaPL, ptChild.MaTKNH, ptChild.MaTN, ptChild.NguoiNop, System.DateTime.UtcNow.AddHours(7), ptChild.NgayThu, ptChild.SoPT, ptChild.SoTien, ptChild.ChungTuGoc, ptChild.DiaChiNN));
                        var queryChiTietPt = _db.ptChiTietPhieuThus.Where(p => p.MaPT == pc.ID).ToList();
                        if (queryChiTietPt.Count > 0)
                        {
                            foreach (var qe in queryChiTietPt)
                            {
                                _db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreateChiTietPhieuThuDaXoa(qe.LinkID, ptChild.SoPT, qe.SoTien, qe.TableName, qe.DienGiai));
                            }
                        }
                        _db.ptChiTietPhieuThus.DeleteAllOnSubmit(ptChild.ptChiTietPhieuThus);
                        _db.ptPhieuThus.DeleteOnSubmit(ptChild);
                        //Xóa Sổ quỹ thu chi
                        _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == ptChild.ID && p.IsPhieuThu == true));
                    }

                    #endregion
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(_ => _.IDPhieu == pc.ID && _.IsKhauTru == false));
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
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
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

        private Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong)
        {
            var listPhieuChi = _db.ptPhieuThus.Where(p => p.HopDongDatCocId == hopDong.Id).ToList();
            hopDong.ThuPhat = listPhieuChi.Sum(_ => _.TotalReceipts).GetValueOrDefault();
            hopDong.TienTra = listPhieuChi.Sum(_ => _.TotalPay).GetValueOrDefault();

            return hopDong;
        }

        private void ItemThemDoiTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gv.GetFocusedRowCellValue("Id");
            if (KiemTraId(id)) return;

            using (var frm = new Deposit.DoiTac.FrmDoiTac() {HopDongDatCocId = id, HopDongDatCocNo = gv.GetFocusedRowCellValue("No").ToString(), BuildingId = (byte)itemBuilding.EditValue})
            {
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
            }
        }

        private void ItemSuaDoiTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gvDoiTac.GetFocusedRowCellValue("Id");
            if (KiemTraId(id)) return;

            using (var frm = new Deposit.DoiTac.FrmDoiTac() { HopDongDatCocId = (int?)gv.GetFocusedRowCellValue("Id"), HopDongDatCocNo = gv.GetFocusedRowCellValue("No").ToString(), BuildingId = (byte)itemBuilding.EditValue, DoiTacId = id })
            {
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
            }
        }

        private void ItemXoaDoiTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvDoiTac.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn đối tác");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            _db = new Library.MasterDataContext();
            foreach (var i in indexs)
            {
                var pc = _db.Dep_DoiTacs.FirstOrDefault(_ => _.Id == (int) gvDoiTac.GetRowCellValue(i, "Id"));
                if (pc != null)
                {
                    _db.Dep_DoiTacs.DeleteOnSubmit(pc);
                }
            }

            _db.SubmitChanges();
            LoadDetail();
        }

        private void itemBuilding_EditValueChanged(object sender, System.EventArgs e)
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            try
            {
                var lReport = (from rp in db.rptReports
                    join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                    where tn.MaTN == (byte) itemBuilding.EditValue &
                          rp.GroupID == Library.BieuMauCls.BieuMauConst.NHOM_DAT_COC_THI_CONG
                    orderby rp.Rank
                    select new {rp.ID, rp.Name}).ToList();

                barInHopDong.ItemLinks.Clear();

                foreach (var report in lReport)
                {
                    DevExpress.XtraBars.BarButtonItem itemInHopDong =
                        new DevExpress.XtraBars.BarButtonItem(barManager1, report.Name) {Tag = report.ID};
                    itemInHopDong.ItemClick += itemInHopDong_ItemClick;
                    barManager1.Items.Add(itemInHopDong);
                    barInHopDong.ItemLinks.Add(itemInHopDong);
                }
            }
            catch (System.Exception ex)
            {
                _lError.Add("itemBuildingEditValueChange: " + ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void itemInHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var hopDongId = (int?) gv.GetFocusedRowCellValue("Id");
            if (hopDongId == null)
            {
                Library.DialogBox.Error("Vui lòng chọn [Hợp đồng] cần in.");
                return;
            }

            var lDoiTac = _db.Dep_DoiTacs.Where(_ => _.HopDongId == hopDongId);
            foreach (var doiTac in lDoiTac)
            {
                var rtfText = Deposit.Class.InHopDong.Print(hopDongId, (int) e.Item.Tag,
                    Library.BieuMauCls.BieuMauConst.NHOM_DAT_COC_THI_CONG, "No", doiTac, doiTac.KhachHangName, doiTac.MatBangName);
                var frm = new BuildingDesignTemplate.FrmShow {RtfText = rtfText};
                frm.ShowDialog(this);
            }
        }
    }
}