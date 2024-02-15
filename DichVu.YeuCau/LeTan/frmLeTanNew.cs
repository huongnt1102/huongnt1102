using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.Data.Mask;
using DevExpress.XtraLayout.Converter;
using Library;
using Library.NoticeCtl;

namespace DichVu.YeuCau.LeTan
{
    public partial class frmLeTanNew : DevExpress.XtraEditors.XtraForm
    {
        public long? ID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db;
        ltLeTan objLT;

        public frmLeTanNew()
        {
            InitializeComponent();
            
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            lkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                               join chuho in db.tnKhachHangs on mb.MaKH equals chuho.MaKH
                                               join nguoithue in db.tnKhachHangs on mb.MaKHF1 equals nguoithue.MaKH
                                               join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
                                               where mb.MaTN == this.MaTN
                                               select new
                                               {
                                                   mb.MaMB,
                                                   mb.MaSoMB,
                                                   tt.MaTT,
                                                   tt.TenTT,
                                                   DaO = mb.MaKH == null ? "Chưa về ở" : "Đã ở",
                                                   NgayDen = mb.NgayBanGiao,
                                                   DienThoaiChuHo=chuho.DienThoaiKH,
                                                   DienThoaiNguoiThue=nguoithue.DienThoaiKH,
                                                   NgayDi = "",
                                                   chuho.MaKH,
                                                   chuho.KyHieu,
                                                   TenKH = chuho.IsCaNhan.Value ? chuho.HoKH + " " + chuho.TenKH : chuho.CtyTen,
                                                   SoCMND = chuho.IsCaNhan.Value ? chuho.CMND : chuho.MaSoThue,
                                                   DienThoai = chuho.DienThoaiKH
                                               }).ToList();
        
            lkTinhTrang.Properties.DataSource = db.mbTrangThais;
            
            lkMoiQuanHe.Properties.DataSource = db.tnQuanHes.Select(p=> new {p.ID,p.Name});
            lkGhiChu.Properties.DataSource = db.LeTanGhiChus.Select(p=>new {p.ID,p.LoaiGhiChu});
            lkDVT.Properties.DataSource = db.DonViTinhs;
            lkDVT.EditValue = 6;
            lkTrangThai.Properties.DataSource = db.LeTanTrangThais;
            lkTrangThai.EditValue = 2;
            txtNguoiNhan.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == this.MaTN);
            if (this.ID == null)
            {
                objLT = new ltLeTan();
                db.ltLeTans.InsertOnSubmit(objLT);
                objLT.MaTN = this.MaTN;
                objLT.MaNVN = Common.User.MaNV;
                objLT.NgayNhap = db.GetSystemDate();
            }
            else
            {
                objLT = db.ltLeTans.Single(p => p.ID == this.ID);
                lkMatBang.EditValue = objLT.MaMB;
                txtHoTenNguoiDenTham.Text = objLT.KhachDen;
                spSLNguoiDenTham.EditValue = objLT.SoLuongNguoi;
                spSLDenTham.EditValue = objLT.SoLuongThe;
                dateGioRaTham.EditValue = objLT.GioRa;
                dateGioVaoTham.EditValue = objLT.GioVao;
                txtCMND.Text = objLT.SoCMND;
                txtNoiCap.Text = objLT.NoiCap;
                dateNgayCap.EditValue = objLT.NgayCap;
                txtSDT3.Text = objLT.DienThoai;
                dateNgayHH.EditValue = objLT.NgayHetHan;
                lkMoiQuanHe.EditValue = objLT.MaQH;
                lkGhiChu.EditValue = objLT.GhiChu;
                memoNoiDung.Text = objLT.NoiDung;
                txtSoThe.Text = objLT.SoThe;
                lkDVT.EditValue = objLT.MaDVT;
                dateTGNhan.EditValue = objLT.ThoiGianNhan;
                txtNguoiNhan.EditValue = objLT.NguoiNhan;
                dateTGTra.EditValue = objLT.ThoiGianTra;
                txtNguoiTra.Text = objLT.NguoiTra;
                lkTrangThai.EditValue = objLT.MaTT;
                objLT.NgaySua = db.GetSystemDate();
                objLT.MaNVS = Common.User.MaNV;
            }
        }

        public void Save(bool is_gui_notify)
        {
            if (dateGioVaoTham.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Ngày vào]. Xin cám ơn");
                dateGioVaoTham.Focus();
                return;
            }
            if (lkTrangThai.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Trạng thái]. Xin cám ơn");
                lkTrangThai.Focus();
                return;
            }
            //if (lkGhiChu.EditValue == null)
            //{
            //    DialogBox.Alert("Vui lòng nhập [Ghi chú]. Xin cám ơn");
            //    lkGhiChu.Focus();
            //    return;
            //}
            if (txtHoTenNguoiDenTham.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Khách đến]. Xin cám ơn");
                txtHoTenNguoiDenTham.Focus();
                return;
            }

            try
            {
                var GhiChu = lkGhiChu.EditValue;
                if (GhiChu == "")
                {
                    GhiChu = null;
                }
                objLT.SoThe = txtSoThe.Text;
                objLT.SoLuongNguoi = Convert.ToInt32(spSLNguoiDenTham.EditValue);
                objLT.GioVao = (DateTime?)dateGioVaoTham.EditValue;
                objLT.KhachDen = txtHoTenNguoiDenTham.Text;
                objLT.SoCMND = txtCMND.Text;
                objLT.NgayCap = (DateTime?)dateNgayCap.EditValue;
                objLT.NoiCap = txtNoiCap.Text;

                objLT.MaMB = (int?)lkMatBang.EditValue;
                objLT.NoiDung = memoNoiDung.Text;
                objLT.MaQH = (int?)lkMoiQuanHe.EditValue;
                objLT.SoLuongThe = Convert.ToInt32(spSLDenTham.EditValue);
                objLT.GioRa = (DateTime?)dateGioRaTham.EditValue;
                objLT.GioVao = (DateTime?)dateGioVaoTham.EditValue;
                objLT.NgayHetHan = (DateTime?)dateNgayHH.EditValue;
                objLT.MaDVT = (int?)lkDVT.EditValue;
                objLT.ThoiGianNhan = (DateTime?)dateTGNhan.EditValue;
                objLT.ThoiGianTra = (DateTime?)dateTGTra.EditValue;
                objLT.NguoiNhan = (int?)txtNguoiNhan.EditValue;
                objLT.NguoiTra = txtNguoiTra.Text;
                objLT.MaTN = this.MaTN;
                objLT.MaTT = Convert.ToInt32(lkTrangThai.EditValue);
                objLT.GhiChu = (int?)GhiChu;
                objLT.DienThoai = txtSDT3.Text;
                db.SubmitChanges();

                #region Gửi notify đến chủ hộ
                if (is_gui_notify == true)
                {
                    var model_lt = new { id = (long?)objLT.ID };
                    var param_lt = new Dapper.DynamicParameters();
                    param_lt.AddDynamicParams(model_lt);
                    var result_lt = Library.Class.Connect.QueryConnect.Query<SendChuHo>("ltletan_gui_chu_ho", param_lt);

                    foreach (var item in result_lt)
                    {
                        var model_param = new { Building_Code = item.TowerName, Building_MaTN = item.TowerId };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", Building.AppVime.VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Properties.Settings.Default.Building_dbConnectionString, param);

                        item.IdNew = a.FirstOrDefault();
                        item.isPersonal = Building.AppVime.VimeService.isPersonal;

                        var ret = Building.AppVime.VimeService.PostH(item, "/Notification/SendChuHo");
                        var result = ret.Replace("\"", "");
                    }
                }
                #endregion

                DialogBox.Success();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Save(false);
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookKhachHang_SizeChanged(object sender, EventArgs e)
        {
            //lookKhachHang.Properties.PopupFormSize = new Size(lookKhachHang.Size.Width, 0);
        }

        private void lkMatBang_EditValueChanged(object sender, EventArgs e)
        {

            if ((int?)lkMatBang.EditValue != null)
            {
                var tam  = (from mb in db.mbMatBangs
                                                             join chuho in db.tnKhachHangs on mb.MaKH equals chuho.MaKH
                                                             join nguoithue in db.tnKhachHangs on mb.MaKHF1 equals nguoithue.MaKH
                                                             join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
                            where mb.MaTN == this.MaTN & mb.MaMB == (int?)lkMatBang.EditValue
                                                             select new
                                                             {
                                                                 mb.MaMB,
                                                                 mb.MaSoMB,
                                                                 tt.MaTT,
                                                                 tt.TenTT,
                                                                 DaO = mb.MaKH == null ? "Chưa về ở" : "Đã ở",
                                                                 NgayDen = mb.NgayBanGiao,
                                                                 DienThoaiChuHo = chuho.DienThoaiKH,
                                                                 DienThoaiNguoiThue = nguoithue.DienThoaiKH,
                                                                 NgayDi = "",
                                                                 chuho.MaKH,
                                                                 chuho.KyHieu,
                                                                 TenKH = chuho.IsCaNhan.Value ? chuho.HoKH + " " + chuho.TenKH : chuho.CtyTen,
                                                                 SoCMND = chuho.IsCaNhan.Value ? chuho.CMND : chuho.MaSoThue,
                                                                 
                                                             }).First();
                lkTinhTrang.EditValue = tam.MaTT;
                txtDaO.Text = tam.DaO;
                txtChuMatBang.Text = tam.TenKH;
                txtNguoiThue.Text = tam.TenKH;
                dateNgayDenO.EditValue = tam.NgayDen;
                txtDD1.Text = tam.DienThoaiChuHo;
                txtDD2.Text = tam.DienThoaiNguoiThue;
                dateNgayDiO.EditValue = null;
            }
        }

        /// <summary>
        /// Lưu và gửi notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Save(true);
        }
    }
}