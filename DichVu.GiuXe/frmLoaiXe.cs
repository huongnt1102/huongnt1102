using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Collections.Generic;

namespace DichVu.GiuXe
{
    public partial class frmLoaiXe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        bool IsLoad = false;
        //List<LoaiXeCls> lst = new List<LoaiXeCls>();
        //public class LoaiXeCls
        //{
        //    public string Name { set; get; }
        //}
        public frmLoaiXe()
        {
            InitializeComponent();
            
        }

        void LoadData()
        {
            db = new MasterDataContext();
            try
            {
                gcLoaiXe.DataSource = db.dvgxLoaiXes.Where(p => p.MaTN == (byte)itemToaNha.EditValue);
                //var objLX=APITheXe.DanhSachLoaiXe((byte)itemToaNha.EditValue);
                //foreach(var item in objLX)
                //{
                //    var lx = new LoaiXeCls();
                //    lx.Name = item.Name;
                //    lst.Add(lx);
                //}
                //lkLoaiXe_CPK.DataSource = lst.Select(p => new {p.Name }).Distinct();

            }
            catch { }
        }

        void DeleteRecord()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                grvLoaiXe.DeleteSelectedRows();
            }
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            db = new MasterDataContext();
            lkNhomXe.DataSource = (from nx in db.dvgxNhomXes
                                   orderby nx.STT
                                   select new { nx.ID, nx.TenNX })
                                  .ToList();
            lkToaNha.DataSource = Common.TowerList;
            IsLoad = true;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if(IsLoad)
            LoadData();
           
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsLoad)
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLoaiXe.UpdateCurrentRow();
            try
            {
                db.SubmitChanges();

                DialogBox.Success();
            }
            catch
            {
                
            }

            this.LoadData();
        }

        private void grvLoaiXe_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteRecord();
            }
        }

        private void grvLoaiXe_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvLoaiXe.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var f = new frmLoaiXeImport())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }
    }
}