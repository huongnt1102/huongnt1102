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
    public partial class frmProfile_DM_Edit : XtraForm
    {
        public int? Id { get; set; }
        public int? IsSua { get; set; }
        public List<int> Ids { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public bool? thayDoi = false;

        private tbl_Profile_DM _o;
        private profileDM old;
        private List<profileDMCT> oldCt;
        private MasterDataContext _db;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmProfile_DM_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        #region LoadData

        private class profileDM
        {
            //public int? Id { get; set; }
            public string TenMau { get; set; }
            public string GhiChu { get; set; }
            public int? LoaiID { get; set; }
            public bool? IsDuyet { get; set; }
            public int? NguoiDuyet { get; set; }
            public DateTime? NgayDuyet { get; set; }
            public string GhiChuDuyet { get; set; }
            public int? ProfileNhomID { get; set; }
            public int? Profile_DMID { get; set; }
        }
        private class profileDMCT
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
        private void LoadData()
        {
            _db = new MasterDataContext();

            glkLoaiProfile.Properties.DataSource = _db.tbl_Profile_Loais;
            glkLoaiProfile.EditValue = glkLoaiProfile.Properties.GetKeyValue(0);
            glkNhomProfile.Properties.DataSource = _db.tbl_Profile_Nhoms.OrderBy(p => p.TenNhomProfile);
            glkNhomProfile.EditValue = glkNhomProfile.Properties.GetKeyValue(0);



            if (IsSua == 0)
            {
                _o = new tbl_Profile_DM();
            }
            else
            {
                _o = _db.tbl_Profile_DMs.FirstOrDefault(_ => _.ID == Id);
                //gán gia trị cũ để lưu vào lịch sử
                old = new profileDM();
                old.TenMau = _o.TenMau;
                old.GhiChu = _o.GhiChu;
                old.GhiChuDuyet = _o.GhiChuDuyet;
                old.NgayDuyet = _o.NgayDuyet;
                old.NguoiDuyet = _o.NguoiDuyet;
                old.ProfileNhomID = _o.ProfileNhomID;
                old.Profile_DMID = _o.ID;
                old.IsDuyet = _o.IsDuyet;
                old.LoaiID = _o.LoaiID;

                //gán giá trị chi tiết cũ

                oldCt = new List<profileDMCT>();
                var ct = _o.tbl_Profile_DM_ChiTiets;
                foreach(var i in ct)
                {
                    var l = new profileDMCT();
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
                    glkNhomProfile.EditValue = _o.ProfileNhomID;
                    txtTenMau.Text = _o.TenMau;
                    txtGhiChu.Text = _o.GhiChu;
                }
                else
                {
                    _o = new tbl_Profile_DM();
                }
            }

            gcChiTiet.DataSource = _o.tbl_Profile_DM_ChiTiets;
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
            _o.LoaiID = (int?)glkLoaiProfile.EditValue;
            _o.ProfileNhomID = (int?)glkNhomProfile.EditValue;
            if (IsSua == 0)
            {
                _db.tbl_Profile_DMs.InsertOnSubmit(_o);
            }
            else
            {
                if (thayDoi == true)
                {
                    //DialogBox.Error("có thay đổi");
                    //lưu vào lịch sử

                    var objLog = new tbl_Profile_DM_Log();
                    objLog.TenMau = old.TenMau;
                    objLog.GhiChu = old.GhiChu;
                    objLog.LoaiID = old.LoaiID;
                    objLog.ProfileNhomID = old.ProfileNhomID;
                    objLog.Profile_DMID = Id;
                    objLog.NgayDuyet = old.NgayDuyet;
                    objLog.NguoiDuyet = old.NguoiDuyet;
                    objLog.IsDuyet = old.IsDuyet;
                    objLog.GhiChuDuyet = old.GhiChuDuyet;
                    _db.tbl_Profile_DM_Logs.InsertOnSubmit(objLog);
                    _db.SubmitChanges();

                    var chiTiet = oldCt;
                    foreach (var i in chiTiet)
                    {
                        var objCt = new tbl_Profile_DM_ChiTiet_Log();
                        objCt.ProfileID = objLog.ID;
                        objCt.TenCongViec = i.TenCongViec;
                        objCt.TenNhomCongViec = i.TenNhomCongViec;
                        objCt.TieuChuan = i.TieuChuan;
                        objCt.GiaTriChon = i.GiaTriChon;
                        objCt.GiaTriNhap = i.GiaTriNhap;
                        objCt.IsHinhAnh = i.IsHinhAnh;
                        _db.tbl_Profile_DM_ChiTiet_Logs.InsertOnSubmit(objCt);
                    }

                }
                else
                {
                    //DialogBox.Error("không thay đổi");
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

        private void gvChiTiet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
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

        private void glkLoaiProfile_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            var show = (int) item.EditValue==3;
            gvChiTiet.Columns["DonViTinh"].Visible = show;
        }
    }
}