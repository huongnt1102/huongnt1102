using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data;
using System.Reflection;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoTri
{
    public partial class frmProfile_Edit : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? Id { get; set; }
        public int? IsSua { get; set; }
        public List<int> Ids { get; set; }
        public int? Nts { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public bool? thayDoi = false;

        private Profile old;
        private List<ProfileCt> oldCt;

        private tbl_Profile _o;

        private MasterDataContext _db;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmProfile_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        #region LoadData

        private void LoadData()
        {
            _db = new MasterDataContext();
            glkLoaiProfile.Properties.DataSource = _db.tbl_Profile_Loais;
            glkLoaiProfile.EditValue = glkLoaiProfile.Properties.GetKeyValue(0);
            var objListCV = (from cv in _db.tbl_Profile_ChiTiets
                                 //from cv in _db.tbl_CongViecs
                                 //join ncv in _db.tbl_NhomCongViecs on cv.MaNhomCongViecID equals ncv.ID
                             select new
                             {
                                 cv.ID,
                                 cv.TenCongViec,
                                 cv.TieuChuan,
                                 cv.TenNhomCongViec,
                                 cv.GiaTriChon,
                                 cv.GiaTriNhap
                                 //GiaTriNhap = string.Join("/", cv.tbl_CongViec_GiaTriNhaps.Select(p => string.Format("{0}", p.TenGiaTriNhap)).ToArray()),
                             }).ToList();
            //repCongViec.DataSource = objListCV;
            //gcChiTiet.DataSource = _o.tbl_Profile_ChiTiets;

            if (IsSua == 0)
            {
                //itemLuu.Visibility=BarItemVisibility.Never;
                _o = new tbl_Profile();
            }
            else
            {
                _o = _db.tbl_Profiles.FirstOrDefault(_ => _.ID == Id);

                //gán gia trị cũ để lưu vào lịch sử
                old = new Profile();
                old.TenMau = _o.TenMau;
                old.GhiChu = _o.GhiChu;
                old.GhiChuDuyet = _o.GhiChuDuyet;
                old.NgayDuyet = _o.NgayDuyet;
                old.NguoiDuyet = _o.NguoiDuyet;
                old.Profile_DMID = _o.ProfileDMID;
                old.ProfileParentID = _o.ID;
                old.IsDuyet = _o.IsDuyet;
                old.LoaiID = _o.LoaiID;
                old.NhomTaiSanID = _o.NhomTaiSanID;
                old.MaTN = _o.MaTN;
                //gán giá trị chi tiết cũ

                oldCt = new List<ProfileCt>();
                var ct = _o.tbl_Profile_ChiTiets;
                foreach (var i in ct)
                {
                    var l = new ProfileCt();
                    l.TenCongViec = i.TenCongViec;
                    l.TenNhomCongViec = i.TenNhomCongViec;
                    l.TieuChuan = i.TieuChuan;
                    l.ProfileID = i.ProfileID;
                    l.GiaTriChon = i.GiaTriChon;
                    l.GiaTriNhap = i.GiaTriNhap;
                    l.IsHinhAnh = i.IsHinhAnh;
                    l.DonViTinh = i.DonViTinh;
                    oldCt.Add(l);
                }
                if (_o != null)
                {
                    glkLoaiProfile.EditValue = _o.LoaiID;
                    txtTenMau.Text = _o.TenMau;
                    txtGhiChu.Text = _o.GhiChu;
                }
                else
                {
                    _o = new tbl_Profile();
                }
            }

            gcChiTiet.DataSource = _o.tbl_Profile_ChiTiets;
            // kiểm tra loại có phải là ghi chỉ số hay không
            //if (glkLoaiProfile.EditValue != null)
            //{
            //    var ghiChiSo = int.Parse(glkLoaiProfile.EditValue.ToString());
            //    gvChiTiet.Columns["DonViTinh"].Visible = ghiChiSo == 3 ? true : false;
            //}
        }


        #endregion

        private void frmEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            _db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);

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

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            txtGhiChu.Focus();
            _o.TenMau = txtTenMau.Text;
            _o.GhiChu = txtGhiChu.Text;
            _o.LoaiID = (int)glkLoaiProfile.EditValue;

            if (IsSua == 0)
            {
                _o.MaTN = MaTn;
                _db.tbl_Profiles.InsertOnSubmit(_o);
            }
            else
            {
                if (thayDoi == true)
                {
                    //DialogBox.Error("có thay đổi");
                    //lưu vào lịch sử

                    var objLog = new tbl_Profile_Log();
                    objLog.TenMau = old.TenMau;
                    objLog.GhiChu = old.GhiChu;
                    objLog.LoaiID = old.LoaiID;
                    objLog.ProfileDMID = old.Profile_DMID;
                    objLog.NhomTaiSanID = old.NhomTaiSanID;
                    objLog.MaTN = old.MaTN;
                    objLog.ProfileParentID = Id;
                    objLog.NgayDuyet = old.NgayDuyet;
                    objLog.NguoiDuyet = old.NguoiDuyet;
                    objLog.IsDuyet = old.IsDuyet;
                    objLog.GhiChuDuyet = old.GhiChuDuyet;
                    _db.tbl_Profile_Logs.InsertOnSubmit(objLog);
                    _db.SubmitChanges();

                    var chiTiet = oldCt;
                    foreach (var i in chiTiet)
                    {
                        var objCt = new tbl_Profile_ChiTiet_Log();
                        objCt.ProfileID = objLog.ID;
                        objCt.TenCongViec = i.TenCongViec;
                        objCt.TenNhomCongViec = i.TenNhomCongViec;
                        objCt.TieuChuan = i.TieuChuan;
                        objCt.GiaTriChon = i.GiaTriChon;
                        objCt.GiaTriNhap = i.GiaTriNhap;
                        objCt.IsHinhAnh = i.IsHinhAnh;
                        _db.tbl_Profile_ChiTiet_Logs.InsertOnSubmit(objCt);
                    }
                }

            }

            try
            {
                _db.SubmitChanges();

                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");

            }
            catch (Exception ex)
            {
                //DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
            finally
            {
                _db.Dispose();
            }
        }

        private void bbiXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvChiTiet.DeleteSelectedRows();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvChiTiet_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvChiTiet.AddNewRow();
            thayDoi = true;
        }

        private void gvChiTiet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                gvChiTiet.DeleteSelectedRows();
                thayDoi = true;
            }
        }

        private void gvChiTiet_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            thayDoi = true;
        }
        private bool IsChecked(GridView view, int row)
        {
            GridColumn col = view.Columns["GiaTriChon"];
            bool val = Convert.ToBoolean(view.GetRowCellValue(row, col) == null ? false : view.GetRowCellValue(row, col));
            if (!val)
                gvChiTiet.SetRowCellValue(row, "GiaTriNhap", "");
            return val == false;
        }
        private void chkCheckChon_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.PostEditor();
            IsChecked(gvChiTiet, gvChiTiet.FocusedRowHandle);
        }
        private void gvChiTiet_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if ((view.FocusedColumn.FieldName == "GiaTriNhap") && IsChecked(view, view.FocusedRowHandle))
                e.Cancel = true;
        }
        private class Profile
        {
            //public int? Id { get; set; }
            public int? NhomTaiSanID { get; set; }
            public string TenMau { get; set; }
            public string GhiChu { get; set; }
            public byte? MaTN { get; set; }
            public int? LoaiID { get; set; }
            public bool? IsDuyet { get; set; }
            public int? NguoiDuyet { get; set; }
            public DateTime? NgayDuyet { get; set; }
            public string GhiChuDuyet { get; set; }
            public int? ProfileParentID { get; set; }
            public int? Profile_DMID { get; set; }
        }
        private class ProfileCt
        {
            //public int? Id { get; set; }
            public string TenCongViec { get; set; }
            public string TieuChuan { get; set; }
            //public int? LoaiID { get; set; }
            public bool? GiaTriChon { get; set; }
            public string TenNhomCongViec { get; set; }
            public string GiaTriNhap { get; set; }
            public int? ProfileID { get; set; }
            public bool? IsHinhAnh { get; set; }
            public string DonViTinh { get; set; }
        }

        private void glkLoaiProfile_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            var show = (int) item.EditValue == 3;

            gvChiTiet.Columns["DonViTinh"].Visible = show;
            //gvChiTiet.Columns["ChiSoCu"].Visible = show;
            //gvChiTiet.Columns["ChiSoMoi"].Visible = show;
            //gvChiTiet.Columns["TieuThu"].Visible = show;

            if (show == true)
            {
                gvChiTiet.Columns["DonViTinh"].VisibleIndex = 5;
                //gvChiTiet.Columns["ChiSoCu"].VisibleIndex = 6;
                //gvChiTiet.Columns["ChiSoMoi"].VisibleIndex = 7;
                //gvChiTiet.Columns["TieuThu"].VisibleIndex = 8;
            }
            
        }
    }
}