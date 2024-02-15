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

namespace SAP.DanhMuc
{
    public partial class frmMappingDoanhThu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmMappingDoanhThu()
        {
            InitializeComponent();
            
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mapping Doanh Thu", "Load");
            var _MaTN = (byte)itemToaNha.EditValue;

            glkCompanyCode.DataSource = db.CompanyCodes;
            glkCuDanCDT.DataSource = db.CuDanCDTs;
            glkKhoiNha.DataSource = (from p in db.mbKhoiNhas
                                     join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                                     where tn.MaTN == _MaTN
                                     select new
                                     {
                                         p.MaKN,
                                         p.TenKN,
                                         p.MaVT,
                                         tn.TenTN,
                                         MaTN = tn.TenVT
                                     }).ToList();
            glkLoaiDichVu.DataSource = db.dvLoaiDichVus;
            glkToaNha.DataSource = db.tnToaNhas.Where(_ => _.MaTN == _MaTN) ;
            glkLoaiXe.DataSource = (from p in db.dvgxLoaiXes
                                    join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
                                    where tn.MaTN == _MaTN
                                    select new
                                    {
                                        p.MaLX,
                                        p.TenLX,
                                        p.MaDichVu,
                                        TenTN = tn.TenTN,
                                        tn.TenVT
                                    }).ToList();


            gc.DataSource = db.MappingDoanhThus.Where(_=>_.MaTN == _MaTN); 
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;

            itemToaNha.EditValue = Common.User.MaTN;


            LoadData();          
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grv.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật thành công!");
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
            }
        }

        private void grvKhoiNha_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    if (DialogBox.QuestionDelete() == DialogResult.No) return;
            //    try
            //    {
            //        var del = db.mbKhoiNhas.Single(p => p.MaKN == (int)grvTaiKhoan.GetFocusedRowCellValue("MaKN"));
            //        db.SubmitChanges();
            //        grvTaiKhoan.DeleteSelectedRows();
            //    }
            //    catch
            //    {
            //        DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            //        this.Close();
            //    }
            //}
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mapping Doanh Thu", "Edit");
                var del = db.MappingDoanhThus.Single(p => p.ID == (int)grv.GetFocusedRowCellValue("ID"));
                db.MappingDoanhThus.DeleteOnSubmit(del);
                db.SubmitChanges();
                grv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grv.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvTaiKhoan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grv.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            //grv.SetFocusedRowCellValue("STT", grv.RowCount);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        void ImportRecord()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mapping Doanh Thu", "Import");
            using (var f = new Import.frmMappingDoanhThu())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }

        private void glkToaNha_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    var _MaTN = (byte?)glkToaNha.edit;
            //    glkKhoiNha.DataSource = (from p in db.mbKhoiNhas
            //                             join tn in db.tnToaNhas on p.MaTN equals tn.MaTN
            //                             where tn.MaTN == _MaTN
            //                             select new
            //                             {
            //                                 p.MaKN,
            //                                 p.TenKN,
            //                                 p.MaVT,
            //                                 tn.TenTN,
            //                                 MaTN = tn.TenVT
            //                             }).ToList();
            //}
            //catch (System.Exception ex) { }
             
        }
    }
}