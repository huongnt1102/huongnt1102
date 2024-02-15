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
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DichVu
{
    public partial class frmDSVanChuyenDo : XtraForm
    {
        public frmDSVanChuyenDo()
        {
            InitializeComponent();
        }
        MasterDataContext db = new MasterDataContext();
        void LoadData()
        {
            var data = (from dk in db.tnDangKyVanChuyens
                       join nv in db.tnNhanViens on dk.NVQLDuyet equals nv.MaNV into nhanvien
                       from nv in nhanvien.DefaultIfEmpty()
                       join mb in db.mbMatBangs on dk.MaMB equals mb.MaMB
                       join kh in db.tnKhachHangs on dk.MaKH equals kh.MaKH
                       where mb.MaTN == (byte?)itemToaNha.EditValue
                       select new
                       {
                           dk.ID,
                           TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen,
                           mb.MaMB,
                           SoMatBang = mb.MaSoMB,
                           HinhThuc = dk.Vao == true? "Chuyển vào": "Chuyển ra",
                           NoiDung = dk.LyDo,
                           NgayTaoPhieu = dk.NgayTao,
                           Duyet = dk.BQLDuyet == true? "Đã duyệt" : "Chưa duyệt",
                           NguoiDuyet = nv.HoTenNV,
                           NgayDuyet = dk.NgayBQLDuyet,
                           TraLoi = dk.LyDoKhongDuyet,
                           NgayVanChuyen = dk.NgayVanChuyen,
                       });
            gc.DataSource = data;
        }

        void LoadDetail()
        {
            var check = (int?)gv.GetFocusedRowCellValue("ID");
            var detail = (from ct in db.tnDangKyVanChuyen_ChiTiets
                        where ct.IDVanChuyen == check
                        select new
                        {
                           ct.HangHoa,
                           ct.KhoiLuong,
                           ct.SoLuong,
                           ct.GhiChu,
                           STT = ct.Stt
                        });
            gcChiTiet.DataSource = detail;
        }
        private void frmDSVanChuyenDo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.TowerList.First().MaTN;
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var Done = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Duyet"]);
                if (Done != "Đã duyệt")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                }
            }
        }

        private void btnDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var matn = (byte)itemToaNha.EditValue;
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                frmDuyet frm = new frmDuyet();
                frm.IDToaNha = matn;
                frm.ID = ID;
                frm.ShowDialog();
                LoadData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dòng!");
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                DialogResult dialogResult = MessageBox.Show("Xác nhận xóa?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var check = db.tnDangKyVanChuyens.SingleOrDefault(p => p.ID == ID);
                    if (check != null)
                    {
                        var chitiet = db.tnDangKyVanChuyen_ChiTiets.Where(p => p.IDVanChuyen == check.ID);
                        db.tnDangKyVanChuyen_ChiTiets.DeleteAllOnSubmit(chitiet);
                        db.tnDangKyVanChuyens.DeleteOnSubmit(check);
                        db.SubmitChanges();
                        LoadData();
                    }
                }
            }
            else
            {
                DialogBox.Error("Vui lòng chọn dòng");
            }
            
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}
