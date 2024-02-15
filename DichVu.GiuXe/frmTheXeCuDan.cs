using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
//using Building.AppVime;
using System.Net;
using Building.AppVime;

namespace DichVu.GiuXe
{
    public partial class frmTheXeCuDan : DevExpress.XtraEditors.XtraForm
    {
        public frmTheXeCuDan()
        {
            InitializeComponent();
            itemDuyet.ItemClick += ItemDuyet_ItemClick;
        }

        private void ItemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //var obj = (dvgxTheXeCuDan)grvTheXe.GetFocusedRow();
                var isduyet = (bool)grvTheXe.GetFocusedRowCellValue("IsDuyet");
                if (isduyet == true)
                {
                    DialogBox.Error("Thẻ xe này đã được duyệt!");
                    return;
                }
                var id = (int?)grvTheXe.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [Thẻ xe] để duyệt");
                    return;
                }
                using (var frm = new frmTheXeEdit())
                {
                    //frm.objTheCuDan =;
                    frm.idTheXeCuDan = id;
                    frm.IsTheCuDan = true;
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
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
                                                                                                                                        : Library.Class.Enum.ConnectString.CONNECT_STRING, param);


                            var param1 = new Dapper.DynamicParameters();
                            //param.AddDynamicParams(model);
                            param1.Add("makh", frm.MaKH, DbType.Int32, ParameterDirection.Input, 255);
                            var a = Library.Class.Connect.QueryConnect.QueryAsyncString<string>("dbo.ho_get_token_khach_hang", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME
                                                                                                                                                              : Library.Class.Enum.ConnectString.CONNECT_STRING, param1);
                            var tokens = a.ToList();

                            var thexe = db.dvgxTheXes.FirstOrDefault(c => c.ID == frm.IDTX);

                            if (thexe != null)
                            {
                                Notify model = new Notify()
                                {
                                    ID = thexe.ID,
                                    ActionID = 19,
                                    MaKH = (int)thexe.MaKH,

                                    apiKey = CommonVime.ApiKey,
                                    secretKey = CommonVime.SecretKey,
                                    idNew = idNew.FirstOrDefault(),
                                    isPersonal = VimeService.isPersonal
                                };
                                var ret = VimeService.Post(model, "/Notification/SendNotify");
                            }

                        }


                        Load_Data();

                    }

                }
            }
            catch
            {
                DialogBox.Alert("Không có thẻ xe nào được chọn");
                return;
            }
            
        }

        void Load_Data()
        {
            using (var db = new MasterDataContext())
            {
                var maTN = (byte)itemToaNha.EditValue;
                gcTheXe.DataSource = from tx in db.dvgxTheXeCuDans
                                     join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                     join mb in db.mbMatBangs on tx.MaMB equals mb.MaMB
                                     join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                                     //join nvn in db.tnNhanViens on tx.MaNVN equals nvn.MaNV
                                     where tx.MaTN == maTN
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
                                         NguoiNhap = TenKH(tx.MaKHN),
                                         tx.NgayNhap,
                                         tx.NgungSuDung,
                                         tx.PhiLamThe,
                                         tx.MaTN,
                                         tx.MaKH,
                                         tx.MaNK,
                                         tx.GiaNgay,
                                         tx.MaDM,
                                         tx.MaKHN,
                                         tx.MaNVS,
                                         tx.NgaySua,
                                         tx.IsDuyet,
                                         NguoiSua = TenKH(tx.MaNVS),
                                         tx.LyDo
                                     };
            }
            LoadAnh();

        }



        private void frmTheXe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            grvTheXe.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            Load_Data();
        }


        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.Load_Data();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Load_Data();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcTheXe);
        }

        private string TenKH(int? MaKH)
        {
            string result = "";
            using (var db = new MasterDataContext())
            {
                var kh = db.tnKhachHangs.FirstOrDefault(c => c.MaKH.Equals(MaKH));
                if (kh != null)
                    result = kh.IsCaNhan.GetValueOrDefault() == true ? string.Format("{0} {1}", kh.HoKH, kh.TenKH) : kh.CtyTen;
            }
            return result;
        }


        private void grvTheXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadAnh();
            if (tcChiTietNuoc.SelectedTabPageIndex==0)
            {
                if (gvHinh.GetFocusedRowCellValue("LinkUrl") == null) return;
                LoadImageUpLoad(gvHinh.GetFocusedRowCellValue("LinkUrl").ToString());
            }
        }
        void LoadAnh()
        {
            MasterDataContext db = new MasterDataContext();
            var id = (int?)grvTheXe.GetFocusedRowCellValue("ID");
            gcHinh.DataSource = db.dvgxTheXeCuDanImages.Where(p => p.LinkID == id);
        }
        void LoadImageUpLoad(string url)
        {
            pic1.Image = null;
            try
            {
                var db = new MasterDataContext();
                if (url != null)
                {
                    var ftp = db.tblConfigs.FirstOrDefault();
                    if (!url.Contains("http"))
                    {
                        url = ftp.WebUrl + url;
                    }

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                    var request = WebRequest.Create(url);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            pic1.Image = Bitmap.FromStream(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



        private void gvHinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /*            using (var db = new MasterDataContext())
                        {
                           var id = (int?)grvTheXe.GetFocusedRowCellValue("ID");
                           var URL  = db.dvgxTheXeCuDanImages.Where(p => p.LinkID == id).Select(p=>p.FileUrl).FirstOrDefault();
                        }*/

            
            if (gvHinh.GetFocusedRowCellValue("FileUrl") != null)
            {
                var url = gvHinh.GetFocusedRowCellValue("FileUrl").ToString();
                LoadImageUpLoad(url);
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

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var isduyet = (bool)grvTheXe.GetFocusedRowCellValue("IsDuyet");
                if (isduyet == true)
                {
                    DialogBox.Error("Thẻ xe này đã được duyệt!");
                    return;
                }
                var id = (int?)grvTheXe.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [Thẻ xe] để từ chối");
                    return;
                }
                var db = new MasterDataContext();
                var theXe = db.dvgxTheXeCuDans.FirstOrDefault(_ => _.ID == id);
                if (theXe == null)
                {
                    DialogBox.Error("Dữ liệu thẻ xe không chính xác");
                    return;
                }

                using (var frm = new Accept.frmAccept())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        theXe.LyDo = frm.reason;
                        theXe.IsDuyet = false;
                        db.SubmitChanges();

                        CommonVime.GetConfig();
                        var toa_nha = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == (byte)itemToaNha.EditValue);

                        var idNew = Library.Class.Connect.QueryConnect.QueryData<int>("dbo.tbl_building_get_id",
                            new
                            {
                                Building_Code = toa_nha.TenVT,
                                Building_MaTN = toa_nha.MaTN
                            });

                        var a = Library.Class.Connect.QueryConnect.QueryData<string>("dbo.ho_get_token_khach_hang",
                            new
                            {
                                makh = theXe.MaKH
                            });

                        var tokens = a.ToList();

                        Notify model = new Notify()
                        {
                            ID = theXe.ID,
                            ActionID = 21,
                            MaKH = (int)theXe.MaKH,

                            apiKey = CommonVime.ApiKey,
                            secretKey = CommonVime.SecretKey,
                            idNew = idNew.FirstOrDefault(),
                            isPersonal = VimeService.isPersonal
                        };
                        var ret = VimeService.Post(model, "/Notification/SendNotify");

                        Load_Data();
                    }
                }
            }
            catch
            {
                DialogBox.Alert("Không có thẻ xe nào được chọn");
                return;
            }
            
        }
    }

}