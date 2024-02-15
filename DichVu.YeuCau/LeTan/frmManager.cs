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

namespace DichVu.YeuCau.LeTan
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                byte? maTN = (byte?)itemToaNha.EditValue;
                DateTime? tuNgay = (DateTime?)itemTuNgay.EditValue;
                DateTime? denNgay = (DateTime?)itemDenNgay.EditValue;

                gcLeTan.DataSource = (from p in db.ltLeTans
                                      
                                      join tt in db.LeTanTrangThais on p.MaTT equals tt.ID into trangthai
                                      from tt in trangthai.DefaultIfEmpty()
                                      join mb in db.mbMatBangs on p.MaMB equals mb.MaMB
                                      join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                      join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into sua
                                      from nvs in sua.DefaultIfEmpty()
                                      join nvnhan in db.tnNhanViens on p.NguoiNhan equals nvnhan.MaNV into nguoinhan
                                                                   from nvnhan in nguoinhan.DefaultIfEmpty()                                   
                                      join chuho in db.tnKhachHangs on mb.MaKH equals chuho.MaKH
                                      join nguoithue in db.tnKhachHangs on mb.MaKHF1 equals nguoithue.MaKH
                                      join MucDo in db.LeTanGhiChus on p.GhiChu equals MucDo.ID into ghichu
                                      from MucDo in ghichu.DefaultIfEmpty()
                                      join dvt in db.DonViTinhs on p.MaDVT equals  dvt.ID
                                      join qh in db.tnQuanHes on p.MaQH equals qh.ID into quanhe
                                      from qh in quanhe.DefaultIfEmpty()
                                      orderby p.GioVao descending
                                      where p.MaTN == maTN
                                      & SqlMethods.DateDiffDay(tuNgay, p.GioVao) >= 0
                                      & SqlMethods.DateDiffDay(p.GioVao, denNgay) >= 0
                                      select new
                                      {
                                          mb.MaSoMB,
                                          p.DienThoai,
                                          p.ID,
                                          p.SoThe,
                                          p.GioVao,
                                          p.GioRa,
                                          p.SoLuongNguoi,
                                          p.KhachDen,
                                          p.SoCMND,
                                          p.NgayCap,
                                          p.NoiCap,
                                          dvt.TenDVT,
                                          MaTT=tt.TrangThai,
                                          DienThoaiChuHo = chuho.DienThoaiKH,
                                          DienThoaiNguoiThue = nguoithue.DienThoaiKH,
                                          p.GhiChu,
                                          TenChuHo = chuho.IsCaNhan.GetValueOrDefault() ? chuho.HoKH + " " + chuho.TenKH : chuho.CtyTen,
                                          TenNguoiThue = nguoithue.IsCaNhan.GetValueOrDefault() ? nguoithue.HoKH + " " + nguoithue.TenKH : nguoithue.CtyTen,
                                          p.NoiDung,
                                          QuanHe=qh.Name,
                                          p.NgayHetHan,
                                          p.NguoiTra,
                                          NguoiNhan=nvnhan.HoTenNV,
                                          MucDo.LoaiGhiChu,
                                          NgayDen=mb.NgayBanGiao,
                                          NgayDi="",
                                          HoTenNVN = nvn.HoTenNV,
                                          p.NgayNhap,
                                          HoTenNVS = nvs.HoTenNV,
                                          p.NgaySua
                                      }).ToList();

                grvLeTan.FocusedRowHandle = -1;
            }
            catch
            {
                gcLeTan.DataSource = null;
            }
            finally
            {
                wait.Close();
            }
        }

        private void LoadDetail()
        {
            var id = (long?)grvLeTan.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                gcXuLy.DataSource = null;
                return;
            }

            db = new MasterDataContext();
            try
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        gcXuLy.DataSource = (from ct in db.ltXuLies
                                             join tt in db.LeTanTrangThais on ct.MaTT equals tt.ID
                                             join nv in db.tnNhanViens on ct.MaNV equals nv.MaNV
                                             orderby ct.GioRa descending
                                             where ct.MaLT == id
                                             select new
                                             {
                                                 ct.ID,
                                                 ct.GioRa,
                                                 ct.GhiChu,
                                                 ct.NgayXL,
                                                 MaTT = tt.TrangThai,
                                                 nv.HoTenNV
                                             }).ToList();
                        break;
                    case 1:
                        var model_ls_notify = new { id = id };
                        var param_ls_notify = new Dapper.DynamicParameters();
                        param_ls_notify.AddDynamicParams(model_ls_notify);
                        gridControl1.DataSource = Library.Class.Connect.QueryConnect.Query<GetLichSuNotify>("ltletan_get_list_su_notify", param_ls_notify);
                        break;
                }
            }
            catch
            {
                gcLeTan.DataSource = null;
            }
        }

        public class GetLichSuNotify
        {
            public string Token { get; set; }
            public string TieuDe { get; set; }
            public string NoiDung { get; set; }
            public string Phone { get; set; }
            public DateTime? DateCreate { get; set; }
        }

        private void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValueChanged -= new EventHandler(itemToaNha_EditValueChanged);
            itemToaNha.EditValue = Common.User.MaTN;
            itemToaNha.EditValueChanged += new EventHandler(itemToaNha_EditValueChanged);

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBaoCao.Items.Add(str);
            itemKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte?)itemToaNha.EditValue;
            if (maTN == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án]. Xin cám ơn!");
                return;
            }

            var frm = new frmLeTanNew();
            frm.MaTN = maTN;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (long?)grvLeTan.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần sửa. Xin cám ơn!");
                return;
            } var maTN = (byte?)itemToaNha.EditValue;
            if (maTN == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án]. Xin cám ơn!");
                return;
            }

            var frm = new frmLeTanNew();
            frm.ID = id;
            frm.MaTN = maTN;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvLeTan.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    var obj = db.ltLeTans.Single(p => p.ID == (long?)grvLeTan.GetRowCellValue(i, "ID"));
                    db.ltLeTans.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();

                grvLeTan.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            var rows = grvLeTan.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xử lý. Xin cám ơn!");
                return;
            }
            var id = (long?)grvLeTan.GetFocusedRowCellValue("ID");
            byte? maTN = (byte?)itemToaNha.EditValue;
            var frm = new frmXuLy();
            frm.MaTN = maTN;
            frm.ID = id;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
               
                    LoadData();
            
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcLeTan);
        }

        private void grvLeTan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte?)itemToaNha.EditValue ?? 0;

            using (frmImport frm = new frmImport())
            {

                frm.MaTN = maTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvLeTan.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần gửi. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn chắc chắn muốn gửi thông báo đến chủ hộ?") == System.Windows.Forms.DialogResult.No) return;

            db = new MasterDataContext();
            try
            {
                var send = 0;
                foreach (var i in rows)
                {
                    var obj = db.ltLeTans.Single(p => p.ID == (long?)grvLeTan.GetRowCellValue(i, "ID"));

                    var model_lt = new { id = (long?)grvLeTan.GetRowCellValue(i, "ID") };
                    var param_lt = new Dapper.DynamicParameters();
                    param_lt.AddDynamicParams(model_lt);
                    var result_lt = Library.Class.Connect.QueryConnect.Query<SendChuHo>("ltletan_gui_chu_ho", param_lt);

                    foreach(var item in result_lt)
                    {
                        var model_param = new  { Building_Code = item.TowerName, Building_MaTN = item.TowerId };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", Building.AppVime.VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Properties.Settings.Default.Building_dbConnectionString, param);

                        item.IdNew = a.FirstOrDefault();
                        item.isPersonal = Building.AppVime.VimeService.isPersonal;

                        var ret = Building.AppVime.VimeService.PostH(item, "/Notification/SendChuHo");
                        var result = ret.Replace("\"", "");
                        if (result.Equals("1"))
                        {
                            
                        }
                        else
                        {
                            send = 1;
                        }
                    }
                }
                if(send == 1)
                {
                    DialogBox.Error("Gửi không thành công");
                }
                else
                {
                    DialogBox.Alert("Gửi thành công");
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        
    }
    public class SendChuHo
    {
        /// <summary>
        /// idnew để xác định server sql
        /// </summary>
        public int IdNew { get; set; }
        /// <summary>
        /// ispersonal để xác định link kiểm tra server
        /// </summary>
        public bool? isPersonal { get; set; } //= false;
        /// <summary>
        /// token để xác định gửi notify đến token này
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Phone để xác định đây là ai trong idnew
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Nội dung gửi notify
        /// </summary>
        public string NoiDung { get; set; }
        /// <summary>
        /// Tiêu đề gửi notify
        /// </summary>
        public string TieuDe { get; set; }
        /// <summary>
        /// ID của table quản lý khách hàng ra vào dự án
        /// </summary>
        public string ID { get; set; }

        public long ResidentId { get; set; }
        public byte TowerId { get; set; }
        public string TowerName { get; set; }
    }
}