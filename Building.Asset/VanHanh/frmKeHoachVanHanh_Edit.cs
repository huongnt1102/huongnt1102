using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.VanHanh
{
    public partial class frmKeHoachVanHanh_Edit : XtraForm
    {
        /// <summary>
        /// Loại danh sách chi tiết: 1 - chọn danh sách chi tiết là hệ thống, 2 - là loại tài sản, 3 - là tên tài sản...
        /// </summary>

        public byte? MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0

        private MasterDataContext _db;
        private tbl_KeHoachVanHanh o;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmKeHoachVanHanh_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            TranslateLanguage.TranslateControl(this, barManager1);

            _db = new MasterDataContext();

            var l = new List<LoaiDanhSachChiTiet>();
            l.Add(new LoaiDanhSachChiTiet {ID = 1, Name = "Hệ thống"});
            l.Add(new LoaiDanhSachChiTiet {ID = 2, Name = "Loại tài sản"});
            l.Add(new LoaiDanhSachChiTiet {ID = 3, Name = "Tên tài sản"});
            glkLoaiDanhSachChiTiet.Properties.DataSource = l;
            glkLoaiDanhSachChiTiet.EditValue = glkLoaiDanhSachChiTiet.Properties.GetKeyValue(0);

            LoadData();

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemClearText.ItemClick += ItemClearText_ItemClick;
        }

        private void ItemClearText_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void LoadData()
        {
            dateNgayLapKeHoach.DateTime = Common.GetDateTimeSystem();
            dateTuNgay.DateTime = Common.GetDateTimeSystem();
            dateDenNgay.DateTime = Common.GetDateTimeSystem();
            glkTanSuat.Properties.DataSource = _db.tbl_TanSuats;
            spinChiPhiTheoKh.EditValue = 0;
            spinChiPhiThucHien.EditValue = 0;

            var objNts = _db.tbl_NhomTaiSans.Where(_ => _.MaTN == MaTn).ToList();
            glkNhomTaiSan.Properties.DataSource = objNts;
            glkNhomTaiSan.EditValue = glkNhomTaiSan.Properties.GetKeyValue(0);

            glkProfile.DataSource = _db.tbl_Profiles.Where(p => p.MaTN == MaTn && (p.LoaiID==2||p.LoaiID==3) & p.IsDuyet == true);

            // mặc định đầu tiên là hệ thống, thì cái danh sách ở phía dưới toàn là hệ thống với edit value = glkDanhSachChiTiet loại hệ thống
            if (glkNhomTaiSan.EditValue != null)
            {
                repMa.DataSource = (from _ in _db.tbl_NhomTaiSans
                    join pro in _db.tbl_Profile_GanHeThongs on new {MaTS = "NTS" + _.ID, MaTN = (byte?) _.MaTN} equals
                        new {MaTS = pro.TaiSanID, pro.MaTN} into profile
                    from pro in profile.DefaultIfEmpty()
                    where _.ID == (int) glkNhomTaiSan.EditValue
                    select new
                    {
                        _.ID,
                        _.TenVietTat, TenTaiSan = _.TenNhomTaiSan,
                        TenMau = pro.ProfileID == null
                            ? ""
                            : _db.tbl_Profiles.FirstOrDefault(p => p.ID == pro.ProfileID).TenMau,
                        ProfileID = pro == null ? (int?) null : pro.ProfileID
                    }).ToList();
            }

            chkCaTruc.Properties.DataSource = _db.tbl_PhanCong_PhanLoaiCas.Where(k=>k.MaTN==MaTn).Select(p => new {p.ID, p.KyHieu}).ToList();
            chkCaTruc.Properties.ValueMember = "ID";
            chkCaTruc.Properties.SeparatorChar = ',';
                
            if (IsSua == null || IsSua == 0)
            {
                o = new tbl_KeHoachVanHanh();
            }
            else
            {
                o = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ => _.ID == Id);
                if (o != null)
                {
                    txtSoKeHoach.EditValue = o.SoKeHoach;
                    dateNgayLapKeHoach.DateTime = o.NgayLapKeHoach ?? DateTime.Now;
                    dateTuNgay.DateTime = o.TuNgay ?? DateTime.Now;
                    dateDenNgay.DateTime = o.DenNgay ?? DateTime.Now;
                    glkNhomTaiSan.EditValue = o.NhomTaiSanID;
                    glkTanSuat.EditValue = o.TanSuatID;
                    spinChiPhiTheoKh.EditValue = o.ChiPhiTheoKh;
                    spinChiPhiThucHien.EditValue = o.ChiPhiThucHien;
                    glkLoaiDanhSachChiTiet.EditValue = o.LoaiHeThong ?? 3;

                    chkCaTruc.SetEditValue(o.PhanLoaiCaIDs);
                }
                else
                    o = new tbl_KeHoachVanHanh();
            }
            gcTaiSan.DataSource = o.tbl_KeHoachVanHanh_ChiTiets;
        }

        public class LoaiDanhSachChiTiet
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvTaiSan.PostEditor();

                #region kiểm tra

                if (glkTanSuat.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tần suất");
                    return;
                }

                if (glkNhomTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn Nhóm tài sản");
                    return;
                }
                bool kt = true;
                for (int i = 0; i < gvTaiSan.RowCount; i++)
                {
                    if (gvTaiSan.GetRowCellValue(i, "MaTenTaiSanID") != null)
                    {
                        if (gvTaiSan.GetRowCellValue(i, "ProfileID") == null)
                        {
                            kt=false;
                        }
                    }
                }
                if (kt == false)
                {
                    DialogBox.Error("Vui lòng chọn profile cho tài sản");
                    return;
                }
                #endregion

                o.NgayLapKeHoach = dateNgayLapKeHoach.DateTime;
                o.SoKeHoach = txtSoKeHoach.Text;
                o.TuNgay = dateTuNgay.DateTime;
                o.DenNgay = dateDenNgay.DateTime;
                o.IsKeHoachBaoTri = false;
                o.TanSuatID = (int) glkTanSuat.EditValue;
                o.NhomTaiSanID = (int) glkNhomTaiSan.EditValue;
                o.ChiPhiTheoKh = spinChiPhiTheoKh.Value;
                o.ChiPhiThucHien = spinChiPhiThucHien.Value;
                o.LoaiHeThong = (int) glkLoaiDanhSachChiTiet.EditValue;

                var items = string.Empty;
                foreach (var item in chkCaTruc.Properties.GetItems().GetCheckedValues())
                        items += string.Format("{0},", item);
                items = items.TrimEnd(",".ToCharArray());
                o.PhanLoaiCaIDs = items;

                o.PhanLoaiCaKyHieus = chkCaTruc.Properties.GetDisplayText(chkCaTruc.EditValue);

                if (IsSua == 0 & Id == 0)
                {
                    o.NguoiNhap = Common.User.MaNV;
                    o.NgayNhap = DateTime.Now;
                    o.MaTN = MaTn;
                    _db.tbl_KeHoachVanHanhs.InsertOnSubmit(o);
                }
                else
                {
                    o.NgaySua = DateTime.Now;
                    o.NguoiSua = Common.User.MaNV;
                }

                _db.SubmitChanges();

                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");

            }
            catch (Exception)
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }

        private void bbiXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(gvTaiSan.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gvTaiSan.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvTaiSan_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvTaiSan.SetFocusedRowCellValue("ID",0);
            gvTaiSan.SetFocusedRowCellValue("MaCVID", 0);
        }

        private void gvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gvTaiSan.GetSelectedRows().ToString()))
                {
                    gvTaiSan.DeleteSelectedRows();
                }
            }
        }

        private void repTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn Tài Sản");
                    return;
                }

                gvTaiSan.SetFocusedRowCellValue("TenTaiSan", item.Properties.View.GetFocusedRowCellValue("TenTaiSan"));
                gvTaiSan.SetFocusedRowCellValue("ProfileID", item.Properties.View.GetFocusedRowCellValue("ProfileID"));
                gvTaiSan.SetFocusedRowCellValue("MaTenTaiSanID", (int)item.Properties.View.GetFocusedRowCellValue("ID"));
                gvTaiSan.UpdateCurrentRow();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public void EditValue(int loaiDanhSach, int nhomTaiSan)
        {
            switch ((int)glkLoaiDanhSachChiTiet.EditValue)
            {
                case 1: // hệ thống
                    // khi là hệ thống, gridview chỉ cần bằng chính cái id của glkNhomTaiSan
                    repMa.DataSource = (from _ in _db.tbl_NhomTaiSans
                                        join pro in _db.tbl_Profile_GanHeThongs on new { MaTS = "NTS" + _.ID, MaTN = (byte?)_.MaTN } equals
                                            new { MaTS = pro.TaiSanID, pro.MaTN } into profile
                                        from pro in profile.DefaultIfEmpty()
                                        where _.ID == nhomTaiSan
                                        select new
                                        {
                                            _.ID,
                                            _.TenVietTat,
                                            TenTaiSan = _.TenNhomTaiSan,
                                            TenMau = pro.ProfileID == null
                                                ? ""
                                                : _db.tbl_Profiles.FirstOrDefault(p => p.ID == pro.ProfileID).TenMau,
                                            ProfileID = pro == null ? (int?)null : pro.ProfileID
                                        }).ToList();
                    break;
                case 2: // loại tài sản
                    repMa.DataSource = (from _ in _db.tbl_LoaiTaiSans
                                        join pro in _db.tbl_Profile_GanHeThongs on new { MaTS = "LTS" + _.ID, MaTN = (byte?)_.tbl_NhomTaiSan.MaTN } equals
                                            new { MaTS = pro.TaiSanID, pro.MaTN } into profile
                                        from pro in profile.DefaultIfEmpty()
                                        where _.NhomTaiSanID == nhomTaiSan
                                        select new
                                        {
                                            _.ID,
                                            _.TenVietTat,
                                            TenTaiSan = _.TenLoaiTaiSan,
                                            TenMau = pro.ProfileID == null
                                                ? ""
                                                : _db.tbl_Profiles.FirstOrDefault(p => p.ID == pro.ProfileID).TenMau,
                                            ProfileID = pro == null ? (int?)null : pro.ProfileID
                                        }).ToList();
                    break;
                case 3: // tên tài sản
                    repMa.DataSource = (from _ in _db.tbl_TenTaiSans
                                        join pro in _db.tbl_Profile_GanHeThongs on new { MaTS = "TTS" + _.ID, MaTN = (byte?)_.MaTN } equals
                                            new { MaTS = pro.TaiSanID, pro.MaTN } into profile
                                        from pro in profile.DefaultIfEmpty()
                                        where _.tbl_LoaiTaiSan.NhomTaiSanID == nhomTaiSan
                                        select new
                                        {
                                            _.ID,
                                            _.TenVietTat,
                                            TenTaiSan = _.TenTaiSan,
                                            TenMau = pro.ProfileID == null
                                                ? ""
                                                : _db.tbl_Profiles.FirstOrDefault(p => p.ID == pro.ProfileID).TenMau,
                                            ProfileID = pro == null ? (int?)null : pro.ProfileID
                                        }).ToList();
                    break;
            }
        }
        private void glkNhomTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;

            if (MaTn != null)
                txtSoKeHoach.Text = Common.CreateKeHoachVanHanh(MaTn.Value, Convert.ToInt32(item.EditValue),
                    dateNgayLapKeHoach.DateTime.Year);

            EditValue((int) glkLoaiDanhSachChiTiet.EditValue, (int) item.EditValue);
        }

        private void glkLoaiDanhSachChiTiet_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            if (glkNhomTaiSan.EditValue != null && glkNhomTaiSan.EditValue.ToString() != "")
            {
                EditValue((int) item.EditValue, int.Parse(glkNhomTaiSan.EditValue.ToString()));
            }
        }

    }
}