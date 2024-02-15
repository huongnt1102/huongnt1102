using FTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Library
{
    public partial class frmQuanLyBanner : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmQuanLyBanner()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEditBanner frm = new frmEditBanner();
            frm.ID = null;
            frm.ShowDialog();
            LoadData();
        }

        private void frmQuanLyBanner_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            var objData = (from bn in db.app_bannerHomes
                           join nvn in db.tnNhanViens on bn.Create_By equals nvn.MaNV into _nvNhap
                           from nvn in _nvNhap.DefaultIfEmpty()
                           join nvs in db.tnNhanViens on bn.Update_By equals nvs.MaNV into _nvSua
                           from nvs in _nvSua.DefaultIfEmpty()
                           select new
                           {
                               bn.ID,
                               bn.Title,
                               bn.Link,
                               bn.IsDisplay,
                               bn.Order_Banner,
                               bn.Header,
                               bn.Footer,
                               bn.Banner,
                               bn.ImageURL,
                               bn.Create_Date,
                               NguoiTao=nvn.HoTenNV,
                               bn.Update_Date,
                               NguoiCapNhat=nvs.HoTenNV
                           }).ToList();
            gc.DataSource = objData;
        }
        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                frmEditBanner frm = new frmEditBanner();
                frm.ID = id;
                frm.ShowDialog();
                LoadData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dòng!");
                return;
            }
           
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult del = MessageBox.Show("Xác nhận xóa?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (del == DialogResult.No) return;
            else
            {
                var id = (int?)gv.GetFocusedRowCellValue("ID");
                var obj = db.app_bannerHomes.SingleOrDefault(p => p.ID == id);
                if (obj != null)
                {
                    List<string> files = new List<string>();
                    var cmd = new FtpClient();
                    files.Add(obj.Link);
                    foreach (var url in files)
                    {
                        cmd.Url = url;
                        try
                        {
                            cmd.DeleteFile();
                        }
                        catch { }
                    }
                    db.app_bannerHome_Towers.DeleteAllOnSubmit(db.app_bannerHome_Towers.Where(p => p.BannerID == id));
                    db.app_bannerHomes.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    LoadData();
                }
            }
           
        }
        void LoadDataToaNha()
        {
            int? ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID != null)
            {
                var objData = db.app_bannerHome_Towers.Where(p => p.BannerID == ID).Select(k => new { MaTN = k.TowerID, TenTN = k.tnToaNha.TenTN, k.ID }).ToList();
                gcToaNha.DataSource = objData;

            }
            else
            {
                gcToaNha.DataSource = null;
            }
        }
        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataToaNha();
        }

        private void grvToaNha_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gv_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? id = (int?)grvToaNha.GetFocusedRowCellValue("ID");
            if(id!=null)
            {
                if(DialogBox.QuestionDelete()==DialogResult.Yes)
                {
                    var obj = db.app_bannerHome_Towers.FirstOrDefault(p=>p.ID==id);
                    db.app_bannerHome_Towers.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    LoadDataToaNha();
                }

            }
            else
            {
                DialogBox.Error("Vui lòng chọn dự án muốn xóa!");
            }
        }
    }
}
