using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;

namespace DichVu.MatBang.PhanNhom
{
    public partial class frmPhanNhom : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanNhom()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            gcMatBang.DataSource = null;
            gcMatBang.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void ImportMatBang()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            var _MaLMB = (int?)itemLoaiMatBang.EditValue;
            if (_MaLMB == null)
            {
                DialogBox.Alert("Vui lòng chọn loại mặt bằng");
                return;
            }

            var _MaLDV = (int?)itemLoaiDichVu.EditValue;
            if (_MaLDV == null)
            {
                DialogBox.Alert("Vui lòng chọn loại dịch vụ");
                return;
            }

            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.MaLMB = _MaLMB.Value;
                f.MaLDV = _MaLDV.Value;
                f.ShowDialog();
                if (f.isSave)
                    this.RefreshData();
            }
        }

        void DeleteMatBang()
        {
            int[] indexs = grvMatBang.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những mặt bằng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            try
            {
                foreach (int i in indexs)
                {
                    var objMB = db.mbPhanNhoms.Single(p => p.ID == (int)grvMatBang.GetRowCellValue(i, "ID"));
                    db.mbPhanNhoms.DeleteOnSubmit(objMB);
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Alert(ex.Message);
                return;
            }
            finally
            {
                db.Dispose();
            }
        }

        private void frmPhanNhom_Load(object sender, EventArgs e)
        {
            try
            {
                TranslateLanguage.TranslateControl(this, barManager1);
                Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

                //Toa nha
                lkToaNha.DataSource = Common.TowerList;
                itemToaNha.EditValue = Common.User.MaTN;

                //Load loai dich vu 
                using (var db = new MasterDataContext())
                {
                    lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus select new { l.ID, TenLDV = l.TenHienThi }).ToList();
                }
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteMatBang();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                lkLoaiMatBang.DataSource = (from n in db.mbLoaiMatBangs where n.MaTN == _MaTN select new { n.MaLMB, n.TenLMB }).ToList();
                itemLoaiMatBang.EditValue = null;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                var _MaLMB = (int)itemLoaiMatBang.EditValue;
                var _MaLDV = (int)itemLoaiDichVu.EditValue;
                e.QueryableSource = from p in db.mbPhanNhoms
                                    join mb in db.mbMatBangs on p.MaMB equals mb.MaMB
                                    join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                    join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                    join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                    join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
                                    join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH into tblKhachHang
                                    from kh in tblKhachHang.DefaultIfEmpty()
                                    join csh in db.tnKhachHangs on mb.MaKHF1 equals csh.MaKH into tblChuSoHuu
                                    from csh in tblChuSoHuu.DefaultIfEmpty()
                                    where p.MaLMB == _MaLMB & p.MaLDV == _MaLDV
                                    orderby mb.MaSoMB
                                    select new
                                    {
                                        p.ID,
                                        mb.MaSoMB,
                                        mb.SoNha,
                                        tl.TenTL,
                                        kn.TenKN,
                                        lmb.TenLMB,
                                        tt.TenTT,
                                        mb.DienTich,
                                        TenKH = kh.IsCaNhan.GetValueOrDefault() ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                        TenCSH = csh.IsCaNhan.GetValueOrDefault() ? (csh.HoKH + " " + csh.TenKH) : csh.CtyTen,
                                        mb.DienGiai
                                    };
                e.Tag = db;
            }
            catch { }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportMatBang();
        }
    }
}