using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Building.Asset.VanHanh
{
    public partial class frmPhieuVanHanh_Edit : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0

        private MasterDataContext _db;
        private tbl_PhieuVanHanh _o;
        private bool? _check;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmPhieuVanHanh_Edit()
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

            try
            {
                _db = new MasterDataContext();

                gridView1.Columns["DonViTinh"].Visible = false;
                gridView1.Columns["ChiSoCu"].Visible = false;
                gridView1.Columns["ChiSoMoi"].Visible = false;
                gridView1.Columns["TieuThu"].Visible = false;

                glkKeHoachVanHanh.Properties.DataSource = _db.tbl_KeHoachVanHanhs.Where(_ => _.MaTN == MaTn);
                glkNhomTaiSan.Properties.DataSource = _db.tbl_NhomTaiSans.Where(_ => _.MaTN == MaTn);
                glkNguoiThucHien.Properties.DataSource = _db.tnNhanViens.Where(_ => _.MaTN == MaTn);

                dateNgayPhieu.DateTime = DateTime.Now;
                glkNguoiThucHien.EditValue = Common.User.MaNV;

                if (IsSua == null || IsSua == 0)
                {
                    _o = new tbl_PhieuVanHanh();
                }
                else
                {
                    _o = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == Id);
                    if (_o != null)
                    {
                        if (_o.NgayPhieu != null) dateNgayPhieu.DateTime = (DateTime)_o.NgayPhieu;
                        txtSoPhieu.Text = _o.SoPhieu;
                        glkKeHoachVanHanh.EditValue = _o.KeHoachVanHanhID;
                        glkNguoiThucHien.EditValue = _o.NguoiThucHien;
                    }
                }

                if (_o != null)
                {
                    gridControl1.DataSource = _o.tbl_PhieuVanHanh_ChiTiets;
                    var item = _o.tbl_PhieuVanHanh_ChiTiets.FirstOrDefault();
                    if (item != null)
                    {
                        if (item.ProfileID != null)
                        {
                            var objProfile = _db.tbl_Profile_ChiTiets.FirstOrDefault(_ => _.ID == item.ProfileID);
                            if (objProfile != null)
                            {
                                var show = objProfile.tbl_Profile.LoaiID == 3;
                                gridView1.Columns["DonViTinh"].Visible = show;
                                gridView1.Columns["ChiSoCu"].Visible = show;
                                gridView1.Columns["ChiSoMoi"].Visible = show;
                                gridView1.Columns["TieuThu"].Visible = show;

                                if (show)
                                {
                                    gridView1.Columns["DonViTinh"].VisibleIndex = 7;
                                    gridView1.Columns["ChiSoCu"].VisibleIndex = 8;
                                    gridView1.Columns["ChiSoMoi"].VisibleIndex = 9;
                                    gridView1.Columns["TieuThu"].VisibleIndex = 10;
                                }
                            }
                        }
                    }
                }

                gridView1.ExpandAllGroups();
            }
            catch
            {

            }
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

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gridView1.PostEditor();

                #region kiểm tra

                if (glkKeHoachVanHanh.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn kế hoạch vận hành");
                    return;
                }

                if (glkNhomTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn hệ thống");
                    return;
                }

                #endregion

                _o.NgayPhieu = dateNgayPhieu.DateTime;
                _o.IsPhieuBaoTri = false;
                _o.KeHoachVanHanhID = (int)glkKeHoachVanHanh.EditValue;
                _o.SoPhieu = txtSoPhieu.Text;
                _o.NguoiThucHien = (int?)glkNguoiThucHien.EditValue;
                if (IsSua == 0 & Id == 0)
                {
                    _o.NhomTaiSanID = (int)glkNhomTaiSan.EditValue;
                    _o.MaTN = MaTn;
                    
                    _db.tbl_PhieuVanHanhs.InsertOnSubmit(_o);
                }

                #region kiểm tra tình trạng bất thường tự động
                var flag = false;

                foreach (var i in _o.tbl_PhieuVanHanh_ChiTiets)
                {
                    if (i.IsChon == null || i.IsChon == false)
                    {
                        flag = true;
                    }
                }

                if (flag == false)
                {
                    _o.StatusLevelID = _db.tbl_PhieuVanHanh_Status_Levels.First(_ => _.Levels == 0).ID;
                    _o.IsBatThuong = false;
                }
                else
                {
                    _o.StatusLevelID = _db.tbl_PhieuVanHanh_Status_Levels.OrderByDescending(_ => _.Levels).First().ID;
                    _o.IsBatThuong = true;
                }
                #endregion
                
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
                if (!string.IsNullOrEmpty(gridView1.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gridView1.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void glkKeHoachVanHanh_EditValueChanged(object sender, EventArgs e)
        {
            var rowKeHoachVanHanh = glkKeHoachVanHanh.Properties.GetRowByKeyValue((int)((GridLookUpEdit) sender).EditValue);
            var type = rowKeHoachVanHanh.GetType();
            var nhomTaiSanId = type.GetProperty("NhomTaiSanID").GetValue(rowKeHoachVanHanh, null);
            if (nhomTaiSanId != null) glkNhomTaiSan.EditValue = (int?)nhomTaiSanId;
        }
    }
}