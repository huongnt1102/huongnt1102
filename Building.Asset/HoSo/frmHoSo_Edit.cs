using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Library;
using ftp = FTP;

namespace Building.Asset.HoSo
{
    public partial class frmHoSo_Edit : Form
    {
        private MasterDataContext _db = new MasterDataContext();
        private tbl_HoSo _o;

        public  int? ID { get; set; }
        public byte? MaTN { get; set; }

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmHoSo_Edit()
        {
            InitializeComponent();
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            // ẩn hết 3 group
            groupThongTinLuuTru.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            groupVanBanDen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            groupVanBanDi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            glkPhanLoai.Properties.DataSource = _db.tbl_HoSo_PhanLoaiDiDens;
            glkNoiBanHanh.Properties.DataSource = _db.tbl_HoSo_NoiBanHanhs;
            glkLoaiVanBan.Properties.DataSource = _db.tbl_HoSo_LoaiVanBans;
            glkLoaiVanBan.EditValue = glkLoaiVanBan.Properties.GetKeyValue(0);
            glkDayKe.Properties.DataSource = _db.tbl_HoSo_DayKes;
            glkDayKe.EditValue = glkDayKe.Properties.GetKeyValue(0);
            glkDoKhanCap.Properties.DataSource = _db.tbl_HoSo_DoKhanCaps;
            glkDoKhanCap.EditValue = glkDoKhanCap.Properties.GetKeyValue(0);
            glkKhoGiay.Properties.DataSource = _db.tbl_HoSo_KhoGiays;
            glkKhoGiay.EditValue = glkKhoGiay.Properties.GetKeyValue(0);
            glkMucDoBaoMat.Properties.DataSource = _db.tbl_HoSo_MucDoBaoMats;
            glkNhomHoSo.Properties.DataSource = _db.tbl_HoSo_NhomHoSos;
            glkPhongLuuTru.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == MaTN).Select(_ => new {_.MaMB, _.MaSoMB, _.mbTangLau.TenTL, _.mbTangLau.mbKhoiNha.TenKN}).ToList();

            dateNgayHetHan.DateTime = DateTime.Now;
            dateNgayBhDen.DateTime = System.DateTime.UtcNow.AddHours(7);
            dateNgayDen.DateTime = System.DateTime.UtcNow.AddHours(7);
            dateNgayBanHanhDi.DateTime = System.DateTime.UtcNow.AddHours(7);
           
            if (ID != null)
            {
                _o = _db.tbl_HoSos.FirstOrDefault(_ => _.ID == ID);
                if (_o != null)
                {
                    if (_o.PhanLoaiId != null) glkPhanLoai.EditValue = _o.PhanLoaiId;

                    txtKyHieu.Text = _o.KyHieu;
                    txtDienGiai.Text = _o.DienGiai;
                    glkLoaiVanBan.EditValue = _o.LoaiVanBanID;
                    txtTenHoSo.Text = _o.Ten;
                    _o.NguoiSua = Common.User.MaNV;
                    _o.NgaySua = DateTime.Now;
                    glkDayKe.EditValue = _o.DayKeID;
                    glkDoKhanCap.EditValue = _o.KhanCapID;
                    glkKhoGiay.EditValue = _o.KhoGiayID;
                    glkLoaiVanBan.EditValue = _o.LoaiVanBanID;
                    glkMucDoBaoMat.EditValue = _o.BaoMatID;
                    glkNhomHoSo.EditValue = _o.NhomHoSoID;
                    glkPhongLuuTru.EditValue = _o.MaMB;
                    dateNgayHetHan.DateTime = _o.NgayHetHan != null ? _o.NgayHetHan.Value.Date : DateTime.Now;
                    if (_o.SoTrang != null) spinSoTrang.Value = (decimal) _o.SoTrang;
                    txtNoiDung.Text = _o.NoiDung;

                    LoadVanBanDen();

                    LoadVanBanDi();
                }
                else
                {
                    _o = new tbl_HoSo();
                    txtKyHieu.Text = GetMaHoSo();
                    _o.NgayNhap = DateTime.Now;
                    _o.NguoiNhap = Common.User.MaNV;
                }
            }
            else
            {
                _o = new tbl_HoSo();
                txtKyHieu.Text = GetMaHoSo();
                _o.NgayNhap = DateTime.Now;
                _o.NguoiNhap = Common.User.MaNV;
                txtTenHoSo.Text = "";
            }

            string kyHieu = txtKyHieu.Text;

            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtKyHieu.Text = kyHieu;

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemLamLai.ItemClick += ItemLamLai_ItemClick;
        }

        private void ItemLamLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string kyHieu = txtKyHieu.Text;
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtKyHieu.Text = kyHieu;
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void LoadVanBanDen()
        {
            // văn bản đến
            if (_o.NgayDen != null) dateNgayBhDen.DateTime = (System.DateTime)_o.NgayDen;
            if (_o.NoiBanHanhId != null) glkNoiBanHanh.EditValue = _o.NoiBanHanhId;
            if (_o.NgayDen != null) dateNgayDen.DateTime = (System.DateTime)_o.NgayDen;
            txtNguoiNhanDen.Text = _o.NguoiNhan;
            txtNguoiKyDen.Text = _o.NguoiKy;
            txtNguoiDuyetDen.Text = _o.NguoiDuyet;
        }

        private void LoadVanBanDi()
        {
            // văn bản đi
            if (_o.NgayBanHanh != null) dateNgayBanHanhDi.DateTime = (System.DateTime)_o.NgayBanHanh;
            txtNoiNhan.Text = _o.NoiNhan;
            if (_o.SoLuongBan != null) spinSoLuongBan.Value = (decimal)_o.SoLuongBan;
            txtNguoiGui.Text = _o.NguoiGui;
            txtNguoiKyDi.Text = _o.NguoiKy;
            txtNguoiDuyetDi.Text = _o.NguoiDuyet;
        }

        private string GetMaHoSo()
        {
            _db = new MasterDataContext();
            int stt = 0;
            string maSo = "";
            do
            {
                stt++;
                var maSoCuoi = _db.tbl_HoSos.Where(_ => _.MaTN == MaTN).OrderByDescending(_ => _.KyHieu)
                    .Select(_ => _.KyHieu).FirstOrDefault();
                if (maSoCuoi != null)
                {
                    Regex regexObj = new Regex(@"(\d+)", RegexOptions.IgnorePatternWhitespace);
                    string match = regexObj.Matches(maSoCuoi)[3].Groups[0].Value;
                    maSo = "HS/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" +
                           _db.DinhDang(39, int.Parse(match) + stt);
                }
                else
                {
                    maSo = "HS/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" +
                           _db.DinhDang(39, (_db.tbl_HoSos.Where(_ => _.MaTN == MaTN).Max(p => (int?)p.ID) ?? 0) + stt);
                }
            }
            while (CheckTrung(maSo));
            return maSo;
        }
        private bool CheckTrung(string maSo)
        {
            using (var db = new MasterDataContext())
            {
                var ts = db.tbl_HoSos.Where(_ => _.MaTN == MaTN).FirstOrDefault(p => p.KyHieu == maSo);
                if (ts != null) return true;
                return false;
            }
        }

        private void txtTenHoSo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new ftp.frmUploadFile();
            if (frm.SelectFile(false))
            {
                txtTenHoSo.Tag = frm.ClientPath;
                if (txtTenHoSo.Text.Trim() == "")
                    txtTenHoSo.Text = frm.FileName;
            }
            frm.Dispose();
        }


        private void ItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTenHoSo.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên hồ sơ");
                txtTenHoSo.Focus();
                return;
            }

            if (glkPhanLoai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phân loại hồ sơ.");
                return;
            }

            try
            {
                LuuUploadFile();

                LuuPhanLoai();

                _o.SoTrang = (int)spinSoTrang.Value;
                _o.FormID = 40;
                _o.KyHieu = txtKyHieu.Text;
                _o.Ten = txtTenHoSo.Text;

                LuuLoaiVanBan();

                _o.DienGiai = txtDienGiai.Text;
                _o.NhomHoSoID = (int?)glkNhomHoSo.EditValue;
                _o.NhanVien = Common.User.MaNV;
                _o.NgayHetHan = (DateTime)dateNgayHetHan.DateTime;
                _o.NoiDung = txtNoiDung.Text;
                _o.MaTN = MaTN;

                LuuThongTinluuTru();

                LuuVanBanDen();

                LuuVanBanDi();

                LuuLichSu();

                _db.SubmitChanges();
            }
            catch (System.Exception ex)
            {
                DialogBox.Error("Đã xảy ra lỗi: "+ex.Message);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void LuuUploadFile()
        {
            if (txtTenHoSo.Tag != null)
            {
                var frm = new ftp.frmUploadFile();
                frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
                frm.ClientPath = txtTenHoSo.Tag.ToString();
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;
                _o.DuongDan = frm.FileName;
            }
        }

        private void LuuPhanLoai()
        {
            tbl_HoSo_PhanLoaiDiDen phanLoai = _db.tbl_HoSo_PhanLoaiDiDens.FirstOrDefault(_ => _.Id == (int)glkPhanLoai.EditValue);
            if (phanLoai != null)
            {
                _o.PhanLoaiId = phanLoai.Id;
                _o.PhanLoaiName = phanLoai.Name;
                _o.PhanLoaiColor = phanLoai.Color;
            }
        }

        private void LuuLoaiVanBan()
        {
            if (glkLoaiVanBan.EditValue != null)
            {
                tbl_HoSo_LoaiVanBan loaiVanBan = _db.tbl_HoSo_LoaiVanBans.FirstOrDefault(_ => _.ID == (int)glkLoaiVanBan.EditValue);
                if (loaiVanBan != null)
                {
                    _o.LoaiVanBanID = loaiVanBan.ID;
                    _o.TenLoaiVanBan = loaiVanBan.Ten;
                }
            }
        }

        private void LuuThongTinluuTru()
        {
            if (groupThongTinLuuTru.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                _o.BaoMatID = (int?)glkMucDoBaoMat.EditValue;
                _o.DayKeID = (int?)glkDayKe.EditValue;
                _o.KhanCapID = (int?)glkDoKhanCap.EditValue;
                _o.KhoGiayID = (int?)glkKhoGiay.EditValue;
                _o.MaMB = (int)glkPhongLuuTru.EditValue;
            }
        }

        private void LuuVanBanDen()
        {
            // văn bản đến
            if (groupVanBanDen.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                _o.NgayBanHanh = dateNgayBhDen.DateTime;
                _o.NgayDen = dateNgayDen.DateTime;

                _o.NguoiNhan = txtNguoiNhanDen.Text;
                _o.NguoiKy = txtNguoiKyDen.Text;
                _o.NguoiDuyet = txtNguoiDuyetDen.Text;

                tbl_HoSo_NoiBanHanh noiBanHanh = null;
                if (glkNoiBanHanh.EditValue != null)
                    noiBanHanh = _db.tbl_HoSo_NoiBanHanhs.FirstOrDefault(_ => _.Id == (int)glkNoiBanHanh.EditValue);
                if (noiBanHanh != null)
                {
                    _o.NoiBanHanhId = noiBanHanh.Id;
                    _o.NoiBanHanhName = noiBanHanh.Name;
                }
            }
        }

        private void LuuVanBanDi()
        {
            if (groupVanBanDi.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                _o.SoLuongBan = (int)spinSoLuongBan.Value;
                _o.NgayBanHanh = dateNgayBanHanhDi.DateTime;
                _o.NoiNhan = txtNoiNhan.Text;
                _o.NguoiGui = txtNguoiGui.Text;
                _o.NguoiKy = txtNguoiKyDi.Text;
                _o.NguoiDuyet = txtNguoiDuyetDi.Text;
            }
        }

        private void LuuLichSu()
        {
            if (ID == null)
            {
                _db.tbl_HoSos.InsertOnSubmit(_o);

                var ls = new tbl_HoSo_LichSu();
                ls.DienGiai = "Thêm hồ sơ " + _o.KyHieu;
                ls.NgayNhap = DateTime.Now;
                ls.NguoiNhap = Common.User.MaNV;
                _o.tbl_HoSo_LichSus.Add(ls);
            }
            else
            {
                var ls = new tbl_HoSo_LichSu();
                ls.DienGiai = "Sửa hồ sơ " + _o.KyHieu;
                ls.NgayNhap = DateTime.Now;
                ls.NguoiNhap = Common.User.MaNV;
                _o.tbl_HoSo_LichSus.Add(ls);
            }
        }

        private void glkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                switch ((int)item.EditValue)
                {
                    case 3: groupThongTinLuuTru.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        groupVanBanDen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        groupVanBanDi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        break;
                    case 2:
                        groupThongTinLuuTru.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        groupVanBanDen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        groupVanBanDi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 1:
                        groupThongTinLuuTru.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        groupVanBanDen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        groupVanBanDi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        break;
                }
            }
            catch{}
        }
    }
}
