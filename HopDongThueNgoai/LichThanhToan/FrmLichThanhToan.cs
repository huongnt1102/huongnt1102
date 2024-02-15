using System.Linq;

namespace HopDongThueNgoai.LichThanhToan
{
    public partial class FrmLichThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }

        private Library.MasterDataContext _db;
        private Library.hdctnCongNoNhaCungCap _congNo;

        public FrmLichThanhToan()
        {
            InitializeComponent();
        }

        private void FrmLichThanhToan_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            Library.KyBaoCao kbc = new Library.KyBaoCao();
            foreach (string item in kbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = kbc.Source[3];

            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var kbc = new Library.KyBaoCao() {Index = index};
            kbc.SetToDate();

            itemDateFrom.EditValue = kbc.DateFrom;
            itemDateTo.EditValue = kbc.DateTo;
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext();
            var tuNgay = (System.DateTime) itemDateFrom.EditValue;
            var denNgay = (System.DateTime) itemDateTo.EditValue;

            gc.DataSource = (from _ in _db.hdctnLichThanhToans
                join dt in _db.tnKhachHangs on _.KhachHangId equals dt.MaKH
                //join mb in _db.mbMatBangs on _.MatBangId equals mb.MaMB into matBang from mb in matBang.DefaultIfEmpty()
                join lt in _db.LoaiTiens on _.MaLoaiTien equals lt.ID into loaiTien from lt in loaiTien.DefaultIfEmpty()
                where _.hdctnDanhSachHopDongThueNgoai.MaToaNha == itemBuilding.EditValue.ToString() &
                      System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, _.TuNgay) >= 0 &
                      System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.TuNgay, denNgay) >= 0
                select new
                {
                    HopDongNo = _.hdctnDanhSachHopDongThueNgoai.SoHopDong,
                    HoTenKh = dt.IsCaNhan == true ? dt.HoKH + " " + dt.TenKH : dt.CtyTen,
                     _.DotThanhToan, _.DienGiai, _.Id, _.HopDongId, _.KhachHangId, _.MaLoaiTien, _.MatBangId, TenLoaiTien = lt.TenLT,
                    _.NgayHetHan, _.SoTien, _.SoTienQuyDoi, _.SoThang, _.TyGia, _.TuNgay, _.DenNgay,_.hdctnDanhSachHopDongThueNgoai.NoiLamViecName,_.DaTra, MaSoMB = _.MatBangNames
                });
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void CbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private bool KiemTra(int? id)
        {
            if (id != null) return false;
            Library.DialogBox.Alert("Vui lòng chọn lịch thanh toán");
            return true;
        }

        private bool KiemTra(int[] indexs)
        {
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch thanh toán");
                return true;
            }
            return false;
        }

        private bool KiemTra(decimal? conLai)
        {
            if (!(conLai <= 0)) return false;
            Library.DialogBox.Alert("Lịch này đã thanh toán");
            return true;
        }

        private bool KiemTra(Library.hdctnLichThanhToan lichThanhToan)
        {
            if (lichThanhToan != null) return false;
            Library.DialogBox.Alert("Không tìm thấy lịch thanh toán");
            return true;
        }

        private bool KiemTra(Library.hdctnDanhSachHopDongThueNgoai hopDong)
        {
            if (hopDong == null)
            {
                Library.DialogBox.Alert("Không tìm thấy hợp đồng");
                return true;
            }

            return false;
        }

        private void ItemThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("Id");
            if(KiemTra(id)) return;

            var lichThanhToan = GetLichThanhToanById(id);
            if (KiemTra(lichThanhToan)) return;

            var conLai = lichThanhToan.SoTienQuyDoi.GetValueOrDefault() - lichThanhToan.DaTra.GetValueOrDefault() ;
            if (KiemTra(conLai)) return;

            var hopDong = GetHopDongById(lichThanhToan.HopDongId);
            if (KiemTra(hopDong)) return;

            _congNo = new Library.hdctnCongNoNhaCungCap { SoTien = 0, BuildingId = (byte?)itemBuilding.EditValue, IsPhieuChi = true, LichThanhToanId = lichThanhToan.Id, HopDongId = hopDong.RowID, DateCreate = System.DateTime.UtcNow.AddHours(7), UserCreate = Library.Common.User.MaNV, UserName = Library.Common.User.HoTenNV };
            _db.hdctnCongNoNhaCungCaps.InsertOnSubmit(_congNo);

            ThanhToan(id, conLai, lichThanhToan, hopDong);

            Library.DialogBox.Success();
            LoadData();
        }

        private void ThanhToan(int? id, decimal? conLai, Library.hdctnLichThanhToan lichThanhToan, Library.hdctnDanhSachHopDongThueNgoai hopDong)
        {
            var phieuChi = new LandSoftBuilding.Fund.Output.ChiTietPhieuChiItem() { LinkID = id, SoTien = conLai, DienGiai = lichThanhToan.DienGiai };

            using (var frm = new LandSoftBuilding.Fund.Output.frmEdit
            {
                MaTN = (byte?)itemBuilding.EditValue,
                MaKH = lichThanhToan.KhachHangId,
                TableName = "hdctnLichThanhToan"
            })
            {
                frm.ChiTiets = new System.Collections.Generic.List<LandSoftBuilding.Fund.Output.ChiTietPhieuChiItem> {phieuChi};
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    _congNo.SoTien = frm.SoTien;
                    _congNo.KhachHangId = frm.MaKH;
                    _congNo.PhieuChiId = frm.ID;
                    lichThanhToan.DaTra = lichThanhToan.DaTra.GetValueOrDefault() + frm.SoTien;
                    hopDong.DaTra = hopDong.DaTra.GetValueOrDefault() + frm.SoTien;
                    _congNo.SoTienThuChi = frm.SoTien;
                    _congNo.DienGiai = frm.DienGiai;

                    _db.SubmitChanges();
                }
            }
        }

        private Library.hdctnLichThanhToan GetLichThanhToanById(int? lichThanhToanId)
        {
            return _db.hdctnLichThanhToans.FirstOrDefault(_ => _.Id == lichThanhToanId);
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetHopDongById(int? hopDongId)
        {
            return _db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == hopDongId);
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (KiemTra(indexs)) return;

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            foreach (var i in indexs)
            {
                DeleteLichThanhToan((int) gv.GetRowCellValue(i, "Id"));
            }

            _db.SubmitChanges();

            LoadData();
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

        private void itemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreatePhanQuyen();
        }

        private void CreatePhanQuyen()
        {
            if (LControlName == null) return;

            var lModuleControl = new System.Collections.Generic.List<Library.PhanQuyen.ModuleControl>();

            foreach (var item in barManager1.Items)
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    var button = (DevExpress.XtraBars.BarButtonItem)item;
                    lModuleControl.Add(new Library.PhanQuyen.ModuleControl { ModuleName = button.Caption, ModuleDescription = button.Caption, ControlNames = button.Name });
                }

            Library.PhanQuyen.CreatePhanQuyen(GetType().Namespace + "." + Name, Text, Library.PhanQuyen.ModuleId.HOP_DONG_THUE_NGOAI_ID, Library.PhanQuyen.ModuleId.FORM_MAIN_ID, LControlName, lModuleControl);
        }
    }
}