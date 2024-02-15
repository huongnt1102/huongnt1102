using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.Gas
{
    public partial class frmDinhMuc : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();

        public frmDinhMuc()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                var _MaLMB = (int?)itemLoaiMatBang.EditValue;
                var _MaMB = (int?)itemMatBang.EditValue;
                var _MaKH = (int?)itemKhachHang.EditValue;
                if (_MaMB == null && _MaLMB == null && _MaKH == null)
                    gcDinhMucNuoc.DataSource = db.dvGasDinhMucs.Where(p => p.MaTN == maTN && p.MaKH==null && p.MaLMB==null && p.MaMB==null);
                else
                    gcDinhMucNuoc.DataSource = db.dvGasDinhMucs.Where(p => p.MaTN == maTN && (p.MaLMB == _MaLMB || (p.MaKH == _MaKH & p.MaMB == _MaMB)));
            }
            catch
            {
                gcDinhMucNuoc.DataSource = null;
            }
        }

        void Delete()
        {
            var collection = gvDinhMucNuoc.GetSelectedRows();
            if (collection.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var item in collection)
                {
                    var obj = db.dvGasDinhMucs.Single(p => p.ID == (int)gvDinhMucNuoc.GetRowCellValue(item, "ID"));
                    db.dvGasDinhMucs.DeleteOnSubmit(obj);
                }
                db.SubmitChanges();
                gvDinhMucNuoc.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void frmDinhMucNuoc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvDinhMucNuoc.InvalidRowException += Library.Common.InvalidRowException;
            
            itemToaNha.EditValue = Common.User.MaTN;
           


        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDinhMucNuoc.AddNewRow();
        }

        private void gcDinhMucNuoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvDinhMucNuoc.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvDinhMucNuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var maTN = gvDinhMucNuoc.GetRowCellValue(e.RowHandle, "MaTN");
            if (maTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

            var tenDM = (gvDinhMucNuoc.GetRowCellValue(e.RowHandle, "TenDM") ?? "").ToString();
            if (tenDM.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập Tên Định Mức !";
                return;
            }
            else if (Common.Duplication(gvDinhMucNuoc, e.RowHandle, "TenDM", tenDM))
            {
                e.Valid = false;
                e.ErrorText = "Tên Định Mức trùng, vui lòng nhập lại !";
                return;
            }

        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

            try
            { 
                var _MaTN = (byte)itemToaNha.EditValue;


                lookLoaiMatBang.DataSource = (from l in db.mbLoaiMatBangs
                                            where l.MaTN == _MaTN
                                            select new { l.MaLMB, l.TenLMB })
                                            .ToList();
                itemLoaiMatBang.EditValue = null; 

                glkMatBang.DataSource = (from mb in db.mbMatBangs
                                         join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                         join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                         where mb.MaTN == _MaTN
                                         select new { mb.MaMB, mb.MaSoMB, tl.TenTL,kn.TenKN })
                                         .ToList();
                itemMatBang.EditValue = null;

                glkKhachHang.DataSource = (from kh in db.tnKhachHangs
                                           where kh.MaTN == _MaTN
                                           select new
                                           {
                                               kh.KyHieu,
                                               kh.MaKH,
                                               TenKH = kh.IsCaNhan.GetValueOrDefault() ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen
                                           }
                                          ).ToList();
                itemKhachHang.EditValue = null; 
            }
            catch { }

            LoadData();
        }

        private void gvDinhMucNuoc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvDinhMucNuoc.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvDinhMucNuoc.SetFocusedRowCellValue("MaLMB", itemLoaiMatBang.EditValue);  
            gvDinhMucNuoc.SetFocusedRowCellValue("MaKH", itemKhachHang.EditValue); 
            gvDinhMucNuoc.SetFocusedRowCellValue("MaMB", itemMatBang.EditValue);
        }

        private void itemLoaiMatBang_EditValueChanged(object sender, EventArgs e)
        {

            itemKhachHang.EditValueChanged -=new EventHandler(itemKhachHang_EditValueChanged);
            itemMatBang.EditValueChanged -= new EventHandler(itemMatBang_EditValueChanged);

            itemMatBang.EditValue = null;
            itemKhachHang.EditValue = null; 
            LoadData();

            itemKhachHang.EditValueChanged += new EventHandler(itemKhachHang_EditValueChanged);
            itemMatBang.EditValueChanged += new EventHandler(itemMatBang_EditValueChanged);
            
        }

        private void itemMatBang_EditValueChanged(object sender, EventArgs e)
        {
            itemLoaiMatBang.EditValueChanged -=new EventHandler(itemLoaiMatBang_EditValueChanged);
            itemLoaiMatBang.EditValue = null;
            LoadData();
            itemLoaiMatBang.EditValueChanged += new EventHandler(itemLoaiMatBang_EditValueChanged);

        }

        private void itemKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            itemLoaiMatBang.EditValueChanged -= new EventHandler(itemLoaiMatBang_EditValueChanged);
            itemLoaiMatBang.EditValue = null;
            LoadData();
            itemLoaiMatBang.EditValueChanged += new EventHandler(itemLoaiMatBang_EditValueChanged);
        }

       
    }
}