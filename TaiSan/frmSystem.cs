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
using System.Data.Linq;

namespace TaiSan
{
    public partial class frmSystem : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmSystem()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            db = new MasterDataContext();
            //db.tnToaNhas.Single().MaTN
            var MaTN =itemToaNha.EditValue == null ? 0: Convert.ToByte(itemToaNha.EditValue);
            gcHeThong.DataSource = db.tsHeThongs.Where(p => p.MaTN == MaTN || MaTN == 0);
        }

        void LoadDetail()
        {
            var maHT = (int?)gvHeThong.GetFocusedRowCellValue("ID");
            var obj = db.tsTaiSans.Where(p => p.MaHT == maHT)
                .Select(p => new
                {
                    p.TenTS,
                    TenTT = p.MaTT == null ? "" : p.tsTinhTrang.TenTT,
                    TenTN = p.MaMB == null ? "" : p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                    TenKN = p.MaMB == null ? "" : p.mbMatBang.mbTangLau.mbKhoiNha.TenKN,
                    TenTL = p.MaMB == null ? "" : p.mbMatBang.mbTangLau.TenTL,
                    MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB,
                    TenLTS = p.MaLTS == null ? "" : p.tsLoaiTaiSan.TenLTS,
                    p.NgayGT,
                    p.IsNoiBo
                });
            gcTaiSan.DataSource = obj;
        }

        private void frmSystem_Load(object sender, EventArgs e)
        {
            lookTN.DataSource = lookToaNHa.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN});
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvHeThong.UpdateCurrentRow();
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
            }
        }

        private void itemAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvHeThong.AddNewRow();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvHeThong.DeleteSelectedRows();
        }

        private void gvHeThong_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            LoadDetail();
        }

        private void gvHeThong_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void gvHeThong_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (itemToaNha.EditValue != null)
            {
                gvHeThong.SetFocusedRowCellValue("MaTN", Convert.ToByte(itemToaNha.EditValue));
            }
                
        }
    }
}