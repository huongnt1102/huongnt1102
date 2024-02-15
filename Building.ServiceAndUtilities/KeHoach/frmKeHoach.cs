using Library;
using System.Linq;

namespace Building.ServiceAndUtilities.KeHoach
{
    public partial class frmKeHoach : DevExpress.XtraEditors.XtraForm
    {
        public int Loai { get; set; }

        public frmKeHoach()
        {
            InitializeComponent();
        }

        private void frmKeHoach_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);
            lkToaNha.DataSource = Library.Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            var objKbc = new Library.KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[1];

            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new Library.KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var maTn = (byte)itemToaNha.EditValue;
                var tuNgay = (System.DateTime?)itemTuNgay.EditValue;
                var denNgay = (System.DateTime?)itemDenNgay.EditValue;

                var model = new { matn = maTn, tungay = tuNgay, denngay = denNgay, loai = Loai };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gc.DataSource = Library.Class.Connect.QueryConnect.Query<ml_kehoach_loaddata>("ml_kehoach_loaddata", param);

                LoadDetail();
            }
            catch { }
        }

        #region
        public class ml_kehoach_loaddata
        {
            public System.DateTime? DenNgay { get; set; }

            public System.DateTime? NgaySua { get; set; }

            public System.DateTime? NgayTao { get; set; }

            public System.DateTime? TuNgay { get; set; }

            public long Id { get; set; }

            public string Ma { get; set; }

            public string Ten { get; set; }

            public string NguoiTao { get; set; }

            public string NguoiSua { get; set; }

        }


        public class ml_KhachHang_Load
        {
            public System.DateTime? NgayGui { get; set; }

            public System.DateTime? TuNgay { get; set; }

            public System.DateTime? DenNgay { get; set; }

            public bool? IsDaGui { get; set; }

            public bool? IsDongY { get; set; }

            public long Id { get; set; }

            public string HinhThucGui { get; set; }

            public string KeHoach { get; set; }

            public string TenKH { get; set; }

            public string NguoiGui { get; set; }

            public string SDT { get; set; }

            public string Ma { get; set; }

            public string NguoiLienHe { get; set; }

            public string ChucVu { get; set; }
            public string GhiChu { get; set; }

        }

        #endregion

        private void LoadDetail()
        {
            try
            {
                var db = new Library.MasterDataContext();

                var id = (long?)gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabChecklist":
                        var model_kh = new { keHoachId = id };
                        var param_kh = new Dapper.DynamicParameters();
                        param_kh.AddDynamicParams(model_kh);
                        gcKhachHang.DataSource = Library.Class.Connect.QueryConnect.Query<ml_KhachHang_Load>("ml_KhachHang_Load", param_kh);
                        break;
                }
            }
            catch { }
        }

        private void CbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmEdit { MaTN = (byte)itemToaNha.EditValue, Id = 0, Loai = Loai })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void Gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void ItemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn kế hoạch, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmEdit { MaTN = (byte)itemToaNha.EditValue, Id = (long)gv.GetFocusedRowCellValue("Id"), Loai = Loai })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void XtraTabControl1_Click(object sender, System.EventArgs e)
        {
            LoadDetail();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Library.Commoncls.ExportExcel(gc);
        }

        private void itemThemKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn kế hoạch, xin cảm ơn.");
                    return;
                }

                using (var frm = new KhachHang.frmEdit { MaTN = (byte)itemToaNha.EditValue, Id = 0, KeHoachId = (long)gv.GetFocusedRowCellValue("Id"), IsShow = false, IsXacNhan = false })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }

            
        }

        private void itemSuaKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn kế hoạch, xin cảm ơn.");
                    return;
                }

                if (gvKhachHang.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn khách hàng, xin cảm ơn.");
                    return;
                }

                var isDongY = (bool)gvKhachHang.GetFocusedRowCellValue("IsDongY");
                if(isDongY == true)
                {
                    Library.DialogBox.Error("Kế hoạch đã được khách hàng đồng ý, không thể sửa.");
                    return;
                }

                using (var frm = new KhachHang.frmEdit { MaTN = (byte)itemToaNha.EditValue, Id = (long)gvKhachHang.GetFocusedRowCellValue("Id"), KeHoachId = (long)gv.GetFocusedRowCellValue("Id"), IsShow = false, IsXacNhan = false })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemXoaKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs;//= grv.GetSelectedRows();
            indexs = gvKhachHang.GetSelectedRows();
            var db = new Library.MasterDataContext();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            foreach (int i in indexs)
            {
                long id = (long)gvKhachHang.GetRowCellValue(i, "Id");
                ml_KhachHang nlh = db.ml_KhachHangs.Single(_ => _.Id == id);
                db.ml_KhachHangs.DeleteOnSubmit(nlh);
            }
            try
            {
                db.SubmitChanges();
                gvKhachHang.DeleteSelectedRows();
                LoadDetail();
            }
            catch (System.Exception)
            {
                DialogBox.Alert("Không xóa được dữ liệu vì ràng buộc dữ liệu!");
            }
        }

        private void itemGuiEmailKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new EmailAmazon.frmSendMail())
            {
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();

                // update đã gửi
                var model_sendMail = new { id = (long)gv.GetFocusedRowCellValue("Id"), hinhThucGui = "Mail", nguoiGui = Library.Common.User.MaNV };
                var param_sendMail = new Dapper.DynamicParameters();
                param_sendMail.AddDynamicParams(model_sendMail);
                Library.Class.Connect.QueryConnect.Query<bool>("ml_KhachHang_SendMail", param_sendMail);

                LoadDetail();
            }
        }

        private void itemXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn kế hoạch, xin cảm ơn.");
                    return;
                }

                if (gvKhachHang.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn khách hàng, xin cảm ơn.");
                    return;
                }

                var isDongY = (bool)gvKhachHang.GetFocusedRowCellValue("IsDongY");
                if (isDongY == true)
                {
                    Library.DialogBox.Error("Kế hoạch đã được khách hàng đồng ý, không thể sửa.");
                    return;
                }

                using (var frm = new KhachHang.frmEdit { MaTN = (byte)itemToaNha.EditValue, Id = (long)gvKhachHang.GetFocusedRowCellValue("Id"), KeHoachId = (long)gv.GetFocusedRowCellValue("Id"), IsShow = true, IsXacNhan = true })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        private void itemAddAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn kế hoạch, xin cảm ơn.");
                    return;
                }

                using (var frm = new KhachHang.frmAddAll { MaTN = (byte)itemToaNha.EditValue, Id = 0, KeHoachId = (long)gv.GetFocusedRowCellValue("Id"), IsShow = false, IsXacNhan = false })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadDetail();
                }
            }
            catch (System.Exception)
            {
                //throw;
            }
        }
    }
}