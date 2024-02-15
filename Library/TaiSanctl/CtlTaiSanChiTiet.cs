using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Library.TaiSanctl
{
    public partial class CtlTaiSanChiTiet : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;
        public tsLoaiTaiSan objLTS;
        string sKyHieu;

        public CtlTaiSanChiTiet()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void CtlTaiSanChiTiet_Load(object sender, EventArgs e)
        {
            if (objLTS != null) LoadChiTietTS(objLTS);
        }

        private void LoadChiTietTS(tsLoaiTaiSan objLTS)
        {
            repositoryHSX.DataSource = db.tsHangSanXuats;
            repositoryNCC.DataSource = db.tnNhaCungCaps;
            repositoryTrangThai.DataSource = db.tsTrangThais;
            repositoryXuatXu.DataSource = db.tsXuatXus;

            gcChiTietTS.DataSource = db.ChiTietTaiSans
                .Where(p => p.MaTS == objLTS.MaLTS);
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var lstChitiet = db.tsTaiSanChiTiets.Where(p => p.tsTaiSanSuDung.MaLTS == objLTS.MaLTS)
                    .Select(p=> new
                    {
                        p.ChiTietTaiSan.TenChiTiet,
                        p.MaTS,
                        p.NgayNhap
                    }).ToList();
                List<string> tenmoithem = new List<string>();
                for (int i = 0; i < grvChiTietTS.RowCount; i++)
                {
                    string tenct = grvChiTietTS.GetRowCellValue(i, colTenChiTiet).ToString();
                    tenmoithem.Add(tenct);
                }
                var listInsert = tenmoithem.Except(lstChitiet.Select(p=>p.TenChiTiet));
                List<tsTaiSanChiTiet> lst = new List<tsTaiSanChiTiet>();
                foreach (var item in listInsert)
                {
                    tsTaiSanChiTiet ts = new tsTaiSanChiTiet()
                    {
                        MaTS = lstChitiet.Select(p => p.MaTS).First(),
                        MaChiTiet = db.ChiTietTaiSans.Where(p => p.TenChiTiet == item).Select(p => p.MaChiTiet).First(),
                        MaTT = db.ChiTietTaiSans.Where(p => p.TenChiTiet == item).Select(p => p.MaTT).First(),
                        NgayNhap = lstChitiet.Select(p => p.NgayNhap).First()
                    };
                    lst.Add(ts);
                }
                db.tsTaiSanChiTiets.InsertAllOnSubmit(lst);
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");  
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.StackTrace);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.OK)
            {
                grvChiTietTS.DeleteSelectedRows();
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.StackTrace);
                }
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvChiTietTS.AddNewRow();
        }

        private void grvChiTietTS_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            db.btHopDong_getNewMaHD(ref sKyHieu);
            grvChiTietTS.SetFocusedRowCellValue(colMaTS, objLTS.MaLTS);
            grvChiTietTS.SetFocusedRowCellValue(colKyHieu, "TSCT-" + sKyHieu);
        }
    }
}
