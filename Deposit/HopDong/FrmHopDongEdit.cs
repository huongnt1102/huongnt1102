using System.Linq;

namespace Deposit.HopDong
{
    public partial class FrmHopDongEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? HopDongId { get; set; }
        public byte? BuildingId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.Dep_HopDong _hopDong;
        private string _loaiHopDongName;
        private string _trangThaiName;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmHopDongEdit()
        {
            InitializeComponent();
        }

        private void FrmHopDongEdit_Load(object sender, System.EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            Tranlaste();

            GetDanhMuc(Deposit.Class.Enum.Category.LOAI_HOP_DONG);
            GetDanhMuc(Deposit.Class.Enum.Category.TRANG_THAI);
            GetDanhMuc(Deposit.Class.Enum.Category.NHA_THAU);

            _hopDong = GetHopDong();
            KhoiTaoGiaTri(HopDongId != null ? true : false);

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemClearText.ItemClick += ItemClearText_ItemClick;
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void Tranlaste()
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
        }

        private Library.Dep_HopDong GetHopDong()
        {
            return HopDongId != null ? _db.Dep_HopDongs.First(_ => _.Id == HopDongId) : new Library.Dep_HopDong();
        }

        private Library.Dep_HopDong GetHopDongById(int? hopDongId)
        {
            var db = new Library.MasterDataContext();
            return db.Dep_HopDongs.FirstOrDefault(_ => _.Id == hopDongId);
        }

        private void GetDanhMuc(string category)
        {
            var db = new Library.MasterDataContext();
            switch (category)
            {
                case Deposit.Class.Enum.Category.LOAI_HOP_DONG: glkLoaiHopDong.Properties.DataSource = db.Dep_LoaiHopDongs; break;
                case Deposit.Class.Enum.Category.TRANG_THAI: glkTrangThai.Properties.DataSource = db.Dep_TrangThais; break;
                case Deposit.Class.Enum.Category.NHA_THAU: glkNhaThau.Properties.DataSource = db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { _.MaKH, _.KyHieu, TenKH = _.IsCaNhan == true ? _.TenKH : _.CtyTen }); break;
            }
            
            db.Dispose();
        }

        private void KhoiTaoGiaTri(bool isEdit)
        {
            switch (isEdit)
            {
                case false:
                    txtMaSo.Text = CreateNo();

                    spinConLai.Value = spinDaHoanTra.Value = spinDaThuPhat.Value = spinTongDatCoc.Value = 0;

                    _db.Dep_HopDongs.InsertOnSubmit(_hopDong);

                    _loaiHopDongName = _trangThaiName = "";
                    break;

                case true:
                    glkLoaiHopDong.EditValue = _hopDong.LoaiHopDongId;
                    glkTrangThai.EditValue = _hopDong.TrangThaiId;
                    //glkNhaThau.EditValue = _hopDong.MaNhaThau;

                    txtMaSo.Text = _hopDong.No;
                    txtNoiDung.Text = _hopDong.Name;
                    _loaiHopDongName = _hopDong.LoaiHopDongName;
                    _trangThaiName = _hopDong.TrangThaiName;

                    ThongTinTienCoc(_hopDong);

                    break;
            }
        }

        private void ThongTinTienCoc(Library.Dep_HopDong hopDong)
        {
            if (hopDong.TongTien != null) spinTongDatCoc.Value = (decimal)hopDong.TongTien;
            if (hopDong.TienTra != null) spinDaHoanTra.Value = (decimal)hopDong.TienTra;
            if (hopDong.ThuPhat != null) spinDaThuPhat.Value = (decimal)hopDong.ThuPhat;
            spinConLai.Value = spinTongDatCoc.Value - spinDaHoanTra.Value - spinDaThuPhat.Value;
        }

        private void SaveMacDinh(bool isEdit)
        {
            switch (isEdit)
            {
                case false:
                    _hopDong.DateCreate = System.DateTime.UtcNow.AddHours(7);
                    _hopDong.UserCreateId = Library.Common.User.MaNV;
                    _hopDong.UserCreateName = Library.Common.User.HoTenNV;
                    _hopDong.BuildingId = BuildingId;
                    break;

                case true:
                    _hopDong.DateUpdate = System.DateTime.UtcNow.AddHours(7);
                    _hopDong.UserUpdateId = Library.Common.User.MaNV;
                    _hopDong.UserUpdateName = Library.Common.User.HoTenNV;
                    break;
            }
        }

        private string CreateNo()
        {
            var db = new Library.MasterDataContext();

            string temp = "DC.";
            string stt = "";

            var obj = (from _ in db.Dep_HopDongs
                where _.BuildingId == BuildingId
                orderby _.No.Substring(_.No.IndexOf('.') + 3) descending
                select new {Stt = _.No.Substring(_.No.IndexOf('.') + 3)}).FirstOrDefault();
            if (obj == null || (obj != null & obj.Stt == null)) stt = "0001";
            else stt = (int.Parse(obj.Stt) + 1).ToString().PadLeft(4, '0');

            temp = temp + stt;

            db.Dispose();
            return temp;
        }

        private bool KiemTraDuLieu(string thaoTac)
        {
            switch (thaoTac)
            {
                case Deposit.Class.Enum.ThaoTac.SAVE:
                    if (glkLoaiHopDong.EditValue == null)
                    {
                        Library.DialogBox.Error("Vui lòng chọn loại hợp đồng.");
                        return true;
                    }

                    if (glkTrangThai.EditValue == null)
                    {
                        Library.DialogBox.Error("Vui lòng chọn trạng thái.");
                        return true;
                    }
                    break;

                case Deposit.Class.Enum.ThaoTac.VIEW:
                    if (_hopDong.Id == 0)
                    {
                        Library.DialogBox.Error("Vui lòng lưu hợp đồng trước khi tạo phiếu thu");
                        return true;
                    }
                    break;
            }
            
            return false;
        }

        private Library.Dep_HopDong SaveHopDong(Library.Dep_HopDong hopDong)
        {
            hopDong.LoaiHopDongId = (int?) glkLoaiHopDong.EditValue;
            hopDong.LoaiHopDongName = _loaiHopDongName;
            hopDong.Name = txtNoiDung.Text;
            hopDong.No = txtMaSo.Text;
            hopDong.TrangThaiId = (int?) glkTrangThai.EditValue;
            hopDong.TrangThaiName = _trangThaiName;
           // hopDong.MaNhaThau = (int?)glkNhaThau.EditValue;
            
            return hopDong;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (KiemTraDuLieu(Deposit.Class.Enum.ThaoTac.SAVE)) return;

                SaveMacDinh(HopDongId!=null);
                _hopDong = SaveHopDong(_hopDong);

                _db.SubmitChanges();
                Library.DialogBox.Success();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lưu bị lỗi: " + ex);
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void ItemLoaiHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Deposit.Category.FrmLoaiHopDong())
            {
                frm.ShowDialog();
                GetDanhMuc(Deposit.Class.Enum.Category.LOAI_HOP_DONG);
            }
        }

        private void ItemTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Deposit.Category.FrmTrangThai())
            {
                frm.ShowDialog();
                GetDanhMuc(Deposit.Class.Enum.Category.TRANG_THAI);
            }
        }

        private void ItemPhieuThuTienCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTraDuLieu(Deposit.Class.Enum.ThaoTac.VIEW)) return;
            using (var frm = new Deposit.PhieuThuDatCoc.FrmDepositManager() {HopDongDatCocId = _hopDong.Id})
            {
                frm.ShowDialog();
                var hopDong = GetHopDongById(_hopDong.Id);
                if (hopDong.Id != 0) ThongTinTienCoc(hopDong);
            }
        }

        private void ItemPhieuChiTienCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTraDuLieu(Deposit.Class.Enum.ThaoTac.VIEW)) return;
            using (var frm = new Deposit.PhieuChi.FrmWithDraw { HopDongDatCocId = _hopDong.Id, FormName = Deposit.Class.Enum.FormName.PHIEU_CHI_HOAN_TIEN })
            {
                frm.ShowDialog();
                var hopDong = GetHopDongById(_hopDong.Id);
                if (hopDong.Id != 0) ThongTinTienCoc(hopDong);
            }
        }

        private void ItemPhieuThuTienPhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTraDuLieu(Deposit.Class.Enum.ThaoTac.VIEW)) return;
            using (var frm = new Deposit.PhieuThuTienPhat.FrmDeposit { HopDongDatCocId = _hopDong.Id })
            {
                frm.ShowDialog();

                var hopDong = GetHopDongById(_hopDong.Id);
                if (hopDong.Id != 0) ThongTinTienCoc(hopDong);
            }
        }

        private void glkLoaiHopDong_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null || item.EditValue == null)
                {
                    Library.DialogBox.Alert("Vui lòng chọn loại hợp đồng");
                    return;
                }
                if (item.Properties.View.GetFocusedRowCellValue("Name") != null)
                    _loaiHopDongName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch{}
        }

        private void glkTrangThai_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null || item.EditValue == null)
                {
                    Library.DialogBox.Alert("Vui lòng chọn trạng thái.");
                    return;
                }
                if (item.Properties.View.GetFocusedRowCellValue("Name") != null)
                    _trangThaiName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch{}
        }
    }
}