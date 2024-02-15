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
using DevExpress.XtraGrid;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.AppExtension
{
    public partial class frmChiTietLienHe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmChiTietLienHe()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void frmChiTietLienHe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            gv.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            itemToaNha.EditValue = Common.User.MaTN;
            barManager1.SetPopupContextMenu(gc, popupMenu1);

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[1];
            SetDate(1);
            LoadData();
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;
            try
            {
                var data = (from ct in db.app_KHLienHeChiTiets
                            join lh in db.app_KHLienHes on ct.IDKHLienHe equals lh.ID
                            join kh in db.tnKhachHangs on ct.MaKH equals kh.MaKH
                            join mb in db.mbMatBangs on lh.MaMB equals mb.MaMB
                            where
                               mb.MaTN == matn &&
                              SqlMethods.DateDiffDay(ct.NgayLienHe, denNgay) >= 0 &&
                              SqlMethods.DateDiffDay(tuNgay, ct.NgayLienHe) >= 0 
                            select new
                            {
                                mb.MaTN,
                               MatBang = mb.MaSoMB,
                               kh.TenKH,
                               kh.DienThoaiKH,
                               ct.IDKHLienHe,
                               lh.Done,
                               lh.ID,
                               ct.NgayLienHe,
                               ct.NgayTraLoi,
                               ct.NoiDungLienHe,
                               ct.NoiDungTraLoi,
                               NVTraLoi = db.tnNhanViens.SingleOrDefault(p=>p.MaNV == ct.MaNVTraLoi) != null ? db.tnNhanViens.SingleOrDefault(p => p.MaNV == ct.MaNVTraLoi).HoTenNV : null
                            });
                gc.DataSource = data;
                gv.FocusedRowHandle = -1;
                gv.ExpandAllGroups();

                //var grouper = new DataGridViewGrouper(gv);
                //grouper.SetGroupOn("Patient");

            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex);
            }

        }

      

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
        }

        private void btnTraLoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var check = db.tnNhanViens.SingleOrDefault(p => p.MaNV == Common.User.MaNV).MaPB;
            if (check == 171 || check == 76)
            {
                var matn = (byte)itemToaNha.EditValue;
                var ID = (int?)gv.GetFocusedRowCellValue("ID");
                if (ID != null)
                {
                    frmTraLoi frm = new frmTraLoi();
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
            else
            {
                DialogBox.Alert("Bạn chưa được phân quyền thực hiện thao tác này!");
                return;
            }
        }

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var Done = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Done"]);
                if (Done != "Đã đánh dấu")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                }
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var check = db.tnNhanViens.SingleOrDefault(p => p.MaNV == Common.User.MaNV).MaPB;
            if (check == 171 || check == 76)
            {
                var matn = (byte)itemToaNha.EditValue;
                var ID = (int?)gv.GetFocusedRowCellValue("ID");
                if (ID != null)
                {
                    frmTraLoi frm = new frmTraLoi();
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
            else
            {
                DialogBox.Alert("Bạn chưa được phân quyền thực hiện thao tác này!");
                return;
            }
        }
    }
}
