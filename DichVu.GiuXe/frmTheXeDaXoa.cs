using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.GiuXe
{
    public partial class frmTheXeDaXoa : DevExpress.XtraEditors.XtraForm
    {
        public frmTheXeDaXoa()
        {
            InitializeComponent();
        }

        void Load_Data()
        {
            gcTheXe.DataSource = null;
            gcTheXe.DataSource = linqInstantFeedbackSource1;
        }

        void Refresh_Data()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void Add_TheXe()
        {
            using (var frm = new frmTheXeEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.Refresh_Data();
                }
            }
        }

        void Edit_TheXe()
        {
            var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;
            }

            using (var frm = new frmTheXeEdit())
            {
                frm.ID = _ID;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.Refresh_Data();
                }
            }
        }

        void Delete_TheXe()
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in indexs)
                {
                    var id = (int?)grvTheXe.GetRowCellValue(i, "ID");

                    var tx = db.dvgxTheXes.Single(p => p.ID == id);
                    #region Luu lai LS xoa the xe Phu Luc XuanMai
                    if ((byte?)itemToaNha.EditValue == 36)
                    {
                        var thexe = new dvgxTheXeDaXoa();
                        thexe.BienSo = tx.BienSo;
                        thexe.ChuThe = tx.ChuThe;                   
                        thexe.DienGiai = tx.DienGiai;
                        thexe.DoiXe = tx.DoiXe;
                        thexe.KyTT = tx.KyTT;
                        thexe.GiaNgay = tx.GiaNgay;
                        thexe.GiaThang = tx.GiaThang;
                        thexe.MaKH = tx.MaKH;
                        thexe.MaMB = tx.MaMB;
                        thexe.KyTT = tx.KyTT;
                        thexe.MaDM = tx.MaDM;
                        thexe.NgayXoa = DateTime.Now;
                        thexe.MaNVXoa = Common.User.MaNV;
                        thexe.NgayTT = tx.NgayTT;
                        thexe.PhiLamThe = tx.PhiLamThe;
                        thexe.NgayDK = tx.NgayDK;
                        thexe.TienTT = tx.TienTT;
                        thexe.MaTN = tx.MaTN;
                        thexe.SoThe = tx.SoThe;
                        thexe.MaMB = tx.MaMB;
                        thexe.MaKH = tx.MaKH;
                        thexe.MaLX = tx.MaLX;
                        thexe.MaNK = tx.MaNK;
                     
                        db.dvgxTheXeDaXoas.InsertOnSubmit(thexe);


                    }
                    #endregion
                    db.dvgxTheXes.DeleteOnSubmit(tx);
                }

                db.SubmitChanges();

                this.Refresh_Data();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void Import_TheXe()
        {
            try
            {
                using (var frm = new frmImport())
                {
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.isSave)
                        this.Refresh_Data();
                }
            }
            catch { }
        }

        private void frmTheXe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            grvTheXe.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            var maTN = (byte)itemToaNha.EditValue;
            e.QueryableSource = from tx in db.dvgxTheXeDaXoas
                                join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                //join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID into tblGiuXe
                                //from gx in tblGiuXe.DefaultIfEmpty()
                                join mb in db.mbMatBangs on tx.MaMB equals mb.MaMB
                                join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                                join nvn in db.tnNhanViens on tx.MaNVXoa equals nvn.MaNV
                                join nvs in db.tnNhanViens on tx.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where tx.MaTN == maTN
                                select new
                                {
                                    tx.ID,
                                    tx.MaGX,
                                    mb.MaSoMB,
                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                    tx.NgayDK,
                                    tx.SoThe,
                                    tx.ChuThe,
                                    lx.TenLX,
                                    tx.BienSo,
                                    tx.MauXe,
                                    tx.DoiXe,
                                    tx.GiaThang,
                                    tx.NgayTT,
                                    tx.KyTT,
                                    tx.TienTT,
                                    tx.DienGiai,
                                    //gx.SoDK,
                                    NguoiNhap = nvn.HoTenNV,
                                    tx.NgayXoa,
                                  
                                    NguoiSua = nvs.HoTenNV,
                                    
                                    tx.NgungSuDung
                                };
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.Load_Data();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Load_Data();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Add_TheXe();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit_TheXe();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete_TheXe();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Import_TheXe();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcTheXe);
        }
    }
}