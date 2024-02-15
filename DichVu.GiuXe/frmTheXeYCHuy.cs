//using Building.AppVime;
using Building.AppVime;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DichVu.GiuXe
{
    public partial class frmTheXeYCHuy : Form
    {
        public frmTheXeYCHuy()
        {
            InitializeComponent();
        }


        void Load_Data()
        {
            using (var db = new MasterDataContext())
            {
                var maTN = (byte)itemToaNha.EditValue;
                gcTheXeYCHuy.DataSource = from tx in db.dvgxTheXes
                                              //join x in db.dvgxTheXes on tx.Ma
                                          join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                          join mb in db.mbMatBangs on tx.MaMB equals mb.MaMB
                                          join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                                          join nvn in db.tnNhanViens on tx.MaNVN equals nvn.MaNV
                                          where tx.MaTN == maTN && tx.IsYC == true
                                          select new
                                          {
                                              tx.ID,
                                              tx.MaGX,
                                              mb.MaSoMB,
                                              mb.MaMB,
                                              TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                              tx.NgayDK,
                                              tx.SoThe,
                                              tx.ChuThe,
                                              tx.NgayNgung,
                                              lx.TenLX,
                                              lx.MaLX,
                                              tx.BienSo,
                                              tx.MauXe,
                                              tx.DoiXe,
                                              tx.GiaThang,
                                              tx.NgayTT,
                                              tx.KyTT,
                                              tx.TienTT,
                                              tx.DienGiai,
                                              NguoiNhap = mb.tnKhachHang.TenKH,
                                              tx.NgayNhap,
                                              tx.NgungSuDung,
                                              tx.PhiLamThe,
                                              tx.MaTN,
                                              tx.MaKH,
                                              tx.MaNK,
                                              tx.GiaNgay,
                                              tx.MaDM,
                                              //  tx.MaKHN,
                                              tx.MaNVS,
                                              tx.NgaySua,
                                              tx.IsYCDuyet,
                                             NguoiNgung= nvn.HoTenNV,
                                              tx.NgayDuyetNgung
                                              
                                              // NguoiSua = tx.
                                          };
            }

        }

        private void frmTheXeYCHuy_Load(object sender, EventArgs e)
        {
            //TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            grvTheXeYCHuy.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            Load_Data();
        }



        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Load_Data();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var obj = (dvgxTheXeCuDan)grvTheXe.GetFocusedRow();
            var id = (int?)grvTheXeYCHuy.GetFocusedRowCellValue("ID");
            var MaKH = (int?)grvTheXeYCHuy.GetFocusedRowCellValue("MaKH");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Thẻ xe] để duyệt");
                return;
            }
            try
            {
                using (var db = new MasterDataContext()){

                    var objTheXe = db.dvgxTheXes.FirstOrDefault(p=> p.ID == id);
                    objTheXe.NgayDuyetNgung = DateTime.Now;
                    objTheXe.NVDuyetNgung = Common.User.MaNV;
                    objTheXe.IsYCDuyet = true;
                    objTheXe.NgungSuDung = true;
                    objTheXe.NgayNgung = DateTime.Now;
                    db.SubmitChanges();
                    DialogBox.Alert("Đã ngưng thẻ xe thành công");
                    Load_Data();
                }


                using (var db = new MasterDataContext())
                {
                    CommonVime.GetConfig();
                    var toa_nha = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == (byte)itemToaNha.EditValue);
                    string building_code = toa_nha.TenVT;
                    int building_matn = toa_nha.MaTN;

                    Building.AppVime.Class.tbl_building_get_id model_param = new Building.AppVime.Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_param);
                    var idNew = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME
                                                                                                                                               : Library.Properties.Settings.Default.Building_dbConnectionString, param);


                    var param1 = new Dapper.DynamicParameters();
                    //param.AddDynamicParams(model);
                    param1.Add("makh", MaKH, DbType.Int32, ParameterDirection.Input, 255);
                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<string>("dbo.ho_get_token_khach_hang", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME
                                                                                                                                                      : Library.Properties.Settings.Default.Building_dbConnectionString, param1);
                    var tokens = a.ToList();

                    var thexe = db.dvgxTheXes.FirstOrDefault(c => c.ID == id);

                    Notify model = new Notify()
                    {
                        ID = thexe.ID,
                        ActionID = 20,
                        MaKH = (int)thexe.MaKH,
                        apiKey = CommonVime.ApiKey,
                        secretKey = CommonVime.SecretKey,
                        idNew = idNew.FirstOrDefault(),
                        isPersonal = VimeService.isPersonal
                    };
                    var ret = VimeService.Post(model, "/Notification/SendNotify");



                    Load_Data();

                }

            }
            catch
            {

            }
        }
        public class Notify
        {
            public int ID { get; set; }
            public int ActionID { get; set; }
            public byte MaTN { get; set; }
            public int MaMB { get; set; }
            public int MaPB { get; set; }
            public int MaKH { get; set; }

            public int idNew { get; set; }
            public string apiKey { get; set; }
            public string secretKey { get; set; }
            public bool? isPersonal { get; set; }
        }
    }

}
