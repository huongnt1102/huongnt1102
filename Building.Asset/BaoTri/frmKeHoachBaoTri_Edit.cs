using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoTri
{
    public partial class frmKeHoachBaoTri_Edit : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0

        private MasterDataContext _db;
        private tbl_KeHoachVanHanh o;
        private List<tbl_NhomTaiSan> objNts;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;
        public frmKeHoachBaoTri_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            _db = new MasterDataContext();
            glkToaNha.Properties.DataSource = _db.tnToaNhas;
            MaTn = MaTn ?? (byte?) Common.User.MaNV;

            dateNgayLapKeHoach.DateTime = Common.GetDateTimeSystem();
            dateTuNgay.DateTime = Common.GetDateTimeSystem();
            dateDenNgay.DateTime = Common.GetDateTimeSystem();
            glkTanSuat.Properties.DataSource = _db.tbl_TanSuats;

            if (IsSua == null || IsSua == 0)
            {
                glkToaNha.EditValue = Common.User.MaTN;
                o = new tbl_KeHoachVanHanh();
            }
            else
            {
                o = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ => _.ID == Id);
                if (o != null)
                {
                    glkToaNha.EditValue = o.MaTN;
                    MaTn = (byte?)o.MaTN;
                    txtSoKeHoach.EditValue = o.SoKeHoach;
                    dateNgayLapKeHoach.DateTime = o.NgayLapKeHoach ?? DateTime.Now;
                    dateTuNgay.DateTime = o.TuNgay ?? DateTime.Now;
                    dateDenNgay.DateTime = o.DenNgay ?? DateTime.Now;
                    glkNhomTaiSan.EditValue = o.NhomTaiSanID;
                    glkTanSuat.EditValue = o.TanSuatID;

                    chkCaTruc.SetEditValue(o.PhanLoaiCaIDs);
                    spinSoNgayCoTheTre.EditValue = o.SoNgayCoTheTre.GetValueOrDefault();
                    
                }
                else
                    o = new tbl_KeHoachVanHanh();
            }
            gcTaiSan.DataSource = o.tbl_KeHoachVanHanh_ChiTiets;
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
                            kt = false;
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
                
                o.TanSuatID = (int) glkTanSuat.EditValue;
                o.NhomTaiSanID = (int) glkNhomTaiSan.EditValue;

                string items = string.Empty;
                foreach (var item in chkCaTruc.Properties.GetItems().GetCheckedValues())
                        items += string.Format("{0},", item);
                items = items.TrimEnd(",".ToCharArray());
                o.PhanLoaiCaIDs = items;
                o.IsKeHoachBaoTri = true;
                o.PhanLoaiCaKyHieus = chkCaTruc.Properties.GetDisplayText(chkCaTruc.EditValue);
                o.LoaiHeThong = 3;
                o.SoNgayCoTheTre = (decimal) spinSoNgayCoTheTre.Value;
                o.NgayHetHanCuoiCung = ((DateTime) o.DenNgay).AddDays((double) o.SoNgayCoTheTre);

                if (IsSua == 0 & Id == 0)
                {
                    o.NguoiNhap = Common.User.MaNV;
                    o.NgayNhap = DateTime.Now;
                    o.MaTN = (byte?)glkToaNha.EditValue;
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

        private void glkNhomTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item == null) return;

                repTaiSan.DataSource = (from t in _db.tbl_TenTaiSans
                    join lts in _db.tbl_LoaiTaiSans on t.LoaiTaiSanID equals lts.ID
                    join ht in _db.tbl_NhomTaiSans on lts.NhomTaiSanID equals ht.ID
                    join pro in _db.tbl_Profile_GanHeThongs on new {MaTS = "TTS" + t.ID, MaTN = t.MaTN.ToString()}
                        equals new {MaTS = pro.TaiSanID.ToString(), MaTN = pro.MaTN.ToString()} into _pro
                    from pro in _pro.DefaultIfEmpty()
                    where ht.MaTN.ToString() == glkToaNha.EditValue.ToString() && ht.ID == (int?) item.EditValue
                    select new
                    {
                        t.ID,
                        t.TenVietTat,
                        t.TenTaiSan,
                        TenMau = pro.IDProfileBaoTri == null
                            ? ""
                            : _db.tbl_Profiles.FirstOrDefault(p => p.ID == pro.IDProfileBaoTri).TenMau,
                        ProfileID = pro == null ? (int?) null : pro.IDProfileBaoTri
                    }).ToList();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.ToString());
            }
            
        }

        private void glkToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                var item = sender as GridLookUpEdit;
                if (item == null) return;

                txtSoKeHoach.Text = Common.CreateKeHoachVanHanh(byte.Parse(item.EditValue.ToString()), Convert.ToInt32(item.EditValue), dateNgayLapKeHoach.DateTime.Year);
                objNts = db.tbl_NhomTaiSans.Where(_ => _.MaTN.ToString() == item.EditValue.ToString()).ToList();

                glkNhomTaiSan.Properties.DataSource = objNts;

                glkProfile.DataSource = db.tbl_Profiles.Where(p =>
                    p.MaTN.ToString() == item.EditValue.ToString() && p.IsDuyet.GetValueOrDefault() == true && p.LoaiID == 1).Select(_ => new { _.ID, _.NhomTaiSanID, _.TenMau }).ToList();
                if (objNts.Count > 0)
                {
                    glkNhomTaiSan.EditValue = objNts.FirstOrDefault().ID;
                }

                chkCaTruc.Properties.DataSource = db.tbl_PhanCong_PhanLoaiCas
                    .Where(c => c.MaTN.ToString() == item.EditValue.ToString()).Select(p => new { p.ID, p.KyHieu }).ToList();
                chkCaTruc.Properties.ValueMember = "ID";
                chkCaTruc.Properties.SeparatorChar = ',';
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.ToString());
            }

        }

        private void itemHuongDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void itemXoaText_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

        }
    }
}