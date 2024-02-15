using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmCapNhatBangGiaDichVu : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();

        public frmCapNhatBangGiaDichVu()
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
                    gcCapNhat.DataSource = db.CapNhatGiaDichVus.Where(p => p.MaTN == maTN && p.MaKH==null && p.MaLMB==null && p.MaMB==null);
                else
                    gcCapNhat.DataSource = db.CapNhatGiaDichVus.Where(p => p.MaTN == maTN && (p.MaLMB == _MaLMB || (p.MaKH == _MaKH & p.MaMB == _MaMB)));
            }
            catch
            {
                gcCapNhat.DataSource = null;
            }
        }

        void Delete()
        {
            var collection = gvCapNhat.GetSelectedRows();
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
                    var obj = db.CapNhatGiaDichVus.Single(p => p.ID == (int)gvCapNhat.GetRowCellValue(item, "ID"));
                    db.CapNhatGiaDichVus.DeleteOnSubmit(obj);
                }
                db.SubmitChanges();
                gvCapNhat.DeleteSelectedRows();
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

            gvCapNhat.InvalidRowException += Library.Common.InvalidRowException;
            
            itemToaNha.EditValue = Common.User.MaTN;
            lkLoaiDV.DataSource = db.dvLoaiDichVus;
            lkNV.DataSource = db.tnNhanViens;

        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvCapNhat.AddNewRow();
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
                gvCapNhat.FocusedRowHandle = -1;
                gvCapNhat.RefreshData();

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
            var maTN = gvCapNhat.GetRowCellValue(e.RowHandle, "MaTN");
            if (maTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Tòa Nhà !";
                return;
            }

            var tenDM = (gvCapNhat.GetRowCellValue(e.RowHandle, "MaLDV") ?? "").ToString();
            if (tenDM.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập loại dịch vụ!";
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
            gvCapNhat.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvCapNhat.SetFocusedRowCellValue("MaLMB", itemLoaiMatBang.EditValue);  
            gvCapNhat.SetFocusedRowCellValue("MaKH", itemKhachHang.EditValue); 
            gvCapNhat.SetFocusedRowCellValue("MaMB", itemMatBang.EditValue);
            gvCapNhat.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
            gvCapNhat.SetFocusedRowCellValue("NgayNhap", db.GetSystemDate());
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

        private void gcDinhMucNuoc_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id =(int?) gvCapNhat.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                 if (DialogBox.Question("Bạn có muốn tiếp tục") == System.Windows.Forms.DialogResult.No) return;
                using (var db = new MasterDataContext())
                {
                    var DK = db.CapNhatGiaDichVus.Single(p => p.ID == id);
                    if (DK.MaLMB != null)
                    {
                        db.HamCapNhatGiaDichVu(DK.MaTN, DK.MaLMB,DK.MaLDV,DK.DonGia);//
                    }
                    else
                    {
                        db.HamCapNhatGiaDichVu(DK.MaTN, null, DK.MaLDV, DK.DonGia);
                    }
                    
                    DialogBox.Alert("Cập nhật thành công !");

                }
                #region Lich su cap nhat gia
                using (var db = new MasterDataContext())
                {
                   LichSuThayDoiGia objLS = new LichSuThayDoiGia();
                   objLS.MaCN = (int?)gvCapNhat.GetFocusedRowCellValue("ID");
                   objLS.DonGia = (decimal)gvCapNhat.GetFocusedRowCellValue("DonGia");
                    objLS.NgayXL = db.GetSystemDate();
                    objLS.NguoiNhap = Common.User.MaNV;
                    db.LichSuThayDoiGias.InsertOnSubmit(objLS);
                    db.SubmitChanges();
                }
                #endregion
            }
        }

        private void gvCapNhat_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
           
        }

        private void gvCapNhat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gcLS.DataSource = null;
            var id = (int?)gvCapNhat.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                gcLS.DataSource = (from ls in db.LichSuThayDoiGias
                    join nv in db.tnNhanViens on ls.NguoiNhap equals nv.MaNV
                    where ls.MaCN == id
                    select new {ls.ID, ls.MaCN, ls.DonGia, ls.NgayXL,NguoiNhap=nv.HoTenNV}).ToList();
            }
        }
       
       
    }
}