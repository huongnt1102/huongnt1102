using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace DichVu.GiuXe
{
    public partial class frmKhoThe : DevExpress.XtraEditors.XtraForm
    {
        public frmKhoThe()
        {
            InitializeComponent();
            grvTheXe.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvTheXe_FocusedRowChanged);
        }

        void DetailGiuXe()
        {
            var db = new MasterDataContext();
            try
            {
                var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");
                if (_ID == null)
                {
                    gcLichSu.DataSource = null;
                    //gcLTT.DataSource = null;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        gcLichSu.DataSource = (from ls in db.dvgxTheXe_LichSuCapNhats
                                               join nv in db.tnNhanViens on ls.NguoiSua equals nv.MaNV
                                               join mb in db.mbMatBangs on ls.MaMB equals mb.MaMB into dsmb from mb in dsmb.DefaultIfEmpty()
                                               join kh in db.tnKhachHangs on ls.MaKH equals kh.MaKH into dskh from kh in dskh.DefaultIfEmpty()
                                               where ls.MaThe == _ID
                                               select new
                                               {
                                                   ls.NgaySua,
                                                   NVN = nv.HoTenNV,
                                                   ls.NoiDung,
                                                   mb.MaSoMB,
                                                   kh.TenKH,
                                                   ls.KhoThe_DSCapThe.SoCT,
                                               }).ToList();

                        break;
                }

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void grvTheXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DetailGiuXe();
        }


        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();
            //itemTuNgay.EditValue = objKBC.DateFrom;
           // itemDenNgay.EditValue = objKBC.DateTo;
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
            using (var frm = new frmKhoTheEdit())
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
                DialogBox.Error("Vui lòng chọn thẻ ");
                return;
            }

            using (var frm = new frmKhoTheEdit())
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
                    db.dvgxTheXes.DeleteOnSubmit(tx);
                }

                db.SubmitChanges();

                this.Refresh_Data();
            }
            catch 
            {
                DialogBox.Error("Thẻ này đã được sử dụng trong danh sách cấp thẻ. Vui lòng kiểm tra lại");
            }
            finally
            {
                db.Dispose();
            }
        }

        void Import_TheXe()
        {
            try
            {
                using (var frm = new frmImportThe())
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

            //KyBaoCao objKBC = new KyBaoCao();
            //foreach (string str in objKBC.Source)
            //{
            //    cbbKyBC.Items.Add(str);
            //}
            ////itemKyBC.EditValue = objKBC.Source[8];
            //SetDate(8);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            var maTN = (byte)itemToaNha.EditValue;
            e.QueryableSource = from tx in db.dvgxTheXes
                                join nvn in db.tnNhanViens on tx.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on tx.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH into dskh from kh in dskh.DefaultIfEmpty()
                                where tx.MaTN == maTN & !tx.IsSaoLuu.GetValueOrDefault()
                                select new
                                {
                                    tx.ID,
                                    tx.SoThe,
                                    tx.GhiChu,
                                    tx.mbMatBang.MaSoMB,
                                    kh.TenKH,
                                    TenLoaiThe =    tx.IsTheOto.GetValueOrDefault() ? "Thẻ ô tô"
                                                   : (tx.IsTheXe.GetValueOrDefault() && tx.IsThangMay.GetValueOrDefault()) ? "Thẻ tích hợp" : tx.IsTheXe.GetValueOrDefault() ? "Thẻ xe" : "Thẻ thang máy",
                                    NguoiNhap = nvn.HoTenNV,
                                    tx.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    tx.NgaySua,
                                    //TrangThai = (tx.NgungSuDung == null || tx.NgungSuDung == true | (!tx.IsTheXe.GetValueOrDefault() & !tx.IsThangMay.GetValueOrDefault()) ) ? "Ngưng SD" : "Đang SD",
                                    TrangThai = tx.NgungSuDung.GetValueOrDefault() | tx.NgungSuDung == null ? "Ngưng SD" : "Đang SD",
                                    tx.IsHongThe,
                                    tx.LyDo,
                                    tx.BienSo,
                                    tx.dvgxLoaiXe.TenLX,
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

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmCapThe())
                {
                    var status = (bool?)grvTheXe.GetFocusedRowCellValue("IsHongThe");
                    if (status == true)
                    {
                        DialogBox.Error("Không thẻ cấp thẻ này. Vui lòng kiểm tra lại");
                        return;
                    }
                    frm.MaTN = (byte) itemToaNha.EditValue;
                    frm.MaThe = (int?)grvTheXe.GetFocusedRowCellValue("ID");
                    frm.ShowDialog();
                    //if (frm.isSave)
                    //    this.Refresh_Data();
                }
            }
            catch
            {
            }
        }
    }
}