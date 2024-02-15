using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
namespace Building.Asset.DanhMuc
{
    public partial class frmManagerCauHinhDuyet : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext _db = new MasterDataContext();
        public frmManagerCauHinhDuyet()
        {
            InitializeComponent();
        }

        private void frmManagerCauHinhDuyet_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
          gcFormDuyet.DataSource = _db.tbl_FromDuyets.OrderBy(p => p.FormName).ToList();
        }

        private void gvFormDuyet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var db1 = new MasterDataContext())
            {
                if (gvFormDuyet.GetFocusedRowCellValue("ID") != null)
                {
                    int? FormID = (int?)gvFormDuyet.GetFocusedRowCellValue("ID");
                    byte? MaTN = (byte?)itemToaNha.EditValue;
                    var obj = (from m in db1.tbl_FromDuyet_ChucVus
                               join cv in db1.tnChucVus on m.ChucVuID equals cv.MaCV
                               join nv in db1.tnNhanViens on m.NhanVienID equals nv.MaNV into nvien
                               from nv in nvien.DefaultIfEmpty()
                               where m.MaTN == MaTN && m.FormDuyetID == FormID
                               select new
                               {
                                   cv.TenCV,
                                   m.STT,
                                   m.IsDuyet,
                                   HoTen = nv.HoTenNV
                               }).ToList();
                    gcChucVu.DataSource = obj.Select(p => new { p.TenCV, p.HoTen, p.STT, p.IsDuyet }).Distinct();
                    //Load hệ thống
                    var objHT = (from m in db1.tbl_FromDuyet_ChucVus
                                 join ht in db1.tbl_NhomTaiSans on m.HeThongTaiSanID equals ht.ID
                                 where m.MaTN == MaTN && m.FormDuyetID == FormID && ht.MaTN == MaTN
                                 select new
                                 {
                                     MaHT = ht.ID,
                                     TenHT = ht.TenNhomTaiSan
                                 }).ToList();
                    gcHeThong.DataSource = objHT.Select(p => new { p.MaHT, p.TenHT }).Distinct();
                }
            }
        }

        private void itemThietLap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvFormDuyet.GetFocusedRowCellValue("ID") != null)
            {
                frmCauHinhDuyetEdit frm = new frmCauHinhDuyetEdit();
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.FormDuyetID = (int?)gvFormDuyet.GetFocusedRowCellValue("ID");
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    LoadData();
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn chức năng để thiết lập");
            }
        }

        private void itemppThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHeThongSelect frm = new frmHeThongSelect();
            frm.MaTN = (byte?)itemToaNha.EditValue;
            frm.ShowDialog();
            if (frm.IsSave)
            {
                MasterDataContext db = new MasterDataContext();
                var objKT = db.tbl_FromDuyet_ChucVus.Where(p => p.MaTN == (byte?)itemToaNha.EditValue && p.FormDuyetID == (int?)gvFormDuyet.GetFocusedRowCellValue("ID")).ToList();
                var objCV = objKT.Select(p => new {p.ChucVuID,p.NhanVienID,p.STT,p.IsDuyet }).Distinct();
                var objHT = objKT.Select(p => p.HeThongTaiSanID).Distinct();
                foreach (var ht in objHT)
                {
                    if (ht != null)
                    {
                        frm.listSel.Add(ht.Value);
                    }
                }
                try
                {
                    db.Connection.Open();
                    db.CommandTimeout = 60000;
                    System.Data.Common.DbTransaction transaction = db.Connection.BeginTransaction();
                    db.Transaction = transaction;
                    db.tbl_FromDuyet_ChucVus.DeleteAllOnSubmit(db.tbl_FromDuyet_ChucVus.Where(p => p.MaTN == (byte?)itemToaNha.EditValue && p.FormDuyetID == (int?)gvFormDuyet.GetFocusedRowCellValue("ID")));
                    db.SubmitChanges();
                    foreach (var item in frm.listSel)
                    {
                        foreach (var itemcv in objCV)
                        {
                            var objThem = new tbl_FromDuyet_ChucVu();
                            objThem.ChucVuID = itemcv.ChucVuID;
                            objThem.FormDuyetID = (int?)gvFormDuyet.GetFocusedRowCellValue("ID");
                            objThem.HeThongTaiSanID = item;
                            objThem.IsDuyet = itemcv.IsDuyet;
                            objThem.MaTN = (byte?)itemToaNha.EditValue;
                            objThem.NhanVienID = itemcv.NhanVienID;
                            objThem.STT = itemcv.STT;
                            db.tbl_FromDuyet_ChucVus.InsertOnSubmit(objThem);
                        }
                    }
                    db.SubmitChanges();
                    db.Transaction.Commit();
                    LoadData();
                }
                catch (Exception ex)
                {
                    db.Transaction.Rollback();
                    DialogBox.Alert(ex.Message);
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        private void itemppXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvHeThong.GetFocusedRowCellValue("MaHT") != null)
            {
                if (DialogBox.QuestionDelete() == DialogResult.Yes)
                {
                    _db.tbl_FromDuyet_ChucVus.DeleteAllOnSubmit(_db.tbl_FromDuyet_ChucVus.Where(p => p.HeThongTaiSanID == (int?)gvHeThong.GetFocusedRowCellValue("MaHT") && p.MaTN == (byte?)itemToaNha.EditValue && p.FormDuyetID == (int?)gvFormDuyet.GetFocusedRowCellValue("ID")));
                    _db.SubmitChanges();
                    DialogBox.Alert("Xóa thành công");
                    LoadData();
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn hệ thống để xóa");
            }

        }

        private void gvHeThong_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvFormDuyet_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvChucVu_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}