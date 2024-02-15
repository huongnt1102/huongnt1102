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
using System.Data.Linq.SqlClient;

namespace DichVu.YeuCau.LeTan
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte? MaTN { get; set; }
        public frmImport()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            itemNgayNhap.EditValue = db.GetSystemDate();
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);

                    var newds = book.Worksheet(0).Select(p => new
                    {

                        MaSoMB = p["Mặt bằng"].ToString().Trim(),
                        MaSo= p["Mã số"].ToString().Trim(),
                        SoCMND = p["Số CMND"].ToString().Trim(),
                        QH=p["Mối QH với chủ hộ"].ToString().Trim(),
                        NgayCap = p["Ngày cấp"].Cast<DateTime>(),
                        NgayHH = p["Ngày hết hạn"].Cast<DateTime>(),
                        TGVao = p["TG vào (nhận)"].Cast<DateTime>(),
                        TGRa = p["TG ra (trả)"].Cast<DateTime>(),
                        SL = p["Số lượng"].Cast<int>(),
                        SLNguoi = p["Số lượng người"].Cast<int>(),

                        MucDo = p["Mức độ"].ToString().Trim(),
                        TrangThai = p["Trạng thái"].ToString().Trim(),
                        NoiDung = p["Nội dung"].ToString().Trim(),
                        HoTenKH = p["Họ tên khách"].ToString().Trim(),
                        NoiCap = p["Nơi cấp"].ToString().Trim(),
                        SDT = p["SĐT"].ToString().Trim(),
                        NguoiTra = p["Người trả"].ToString().Trim(),
                        NguoiNhan = p["Người nhận"].ToString().Trim(),
                        TGNhan = p["Thời gian nhận"].Cast<DateTime>(),
                        TGTra = p["Thời gian trả"].Cast<DateTime>(),
                      
                    }).ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                           
                          
                            MaSoMB = item.MaSoMB,
                            MaSo = item.MaSo,
                            SoCMND = item.SoCMND,
                            SL=item.SL,
                            NguoiTra=item.NguoiTra,
                            NguoiNhan=item.NguoiNhan,
                            QH=item.QH,
                            TGVao=item.TGVao,SLNguoi=item.SLNguoi,
                            TGRa=item.TGRa,
                TrangThai=item.TrangThai,
                NoiCap=item.NoiCap,
                SDT=item.SDT,
                NoiDung=item.NoiDung,
                HoTenKH=item.HoTenKH,
                            NgayHH=item.NgayHH,
                            NgayCap=item.NgayCap,
                            MucDo=item.MucDo,
                        };
                        newlist.Add(import);
                    }
                    gcHopDong.DataSource = newlist;
                }
                catch
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }

                wait.Close();
                wait.Dispose();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }
            var listMB = (from mb in db.mbMatBangs join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                          where mb.MaTN == MaTN 
                          select new
                          {
                              mb.MaMB, mb.MaSoMB, mb.MaKH, mb.IsCanHoCaNhan,TenKH=kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                          }).ToList();

            var listTT = (from mb in db.LeTanTrangThais select new { mb.ID,mb.TrangThai }).ToList();
            var listQH = (from mb in db.tnQuanHes  select new { mb.ID,mb.Name }).ToList();
            var listNV = (from mb in db.tnNhanViens select new { mb.MaNV, mb.HoTenNV }).ToList();
            var listMucDo = (from mb in db.LeTanGhiChus select new { mb.ID, mb.LoaiGhiChu }).ToList();
            var ngaynhap = (DateTime)itemNgayNhap.EditValue;
            var wait = DialogBox.WaitingForm();
            try
            {


                for (int i = 0; i < grvHopDong.RowCount; i++)
                {
                    db = new MasterDataContext();
                    var objMB =
                        listMB.FirstOrDefault(
                            p => p.MaSoMB == grvHopDong.GetRowCellValue(i, "MaSoMB").ToString());
                    if (objMB == null)
                    {

                        grvHopDong.SetRowCellValue(i, "Error", "Mặt bằng này không tồn tại trong hệ thống");



                    }
                    var objQH =
                        listQH.FirstOrDefault(p => p.Name == grvHopDong.GetRowCellValue(i, "QH").ToString().Trim());
                    if (objQH == null)
                    {

                        grvHopDong.SetRowCellValue(i, "Error", "Quan hệ với chủ hộ không tồn tại trong hệ thống");



                    }
                       var objGC =
                        listMucDo.FirstOrDefault(p => p.LoaiGhiChu == grvHopDong.GetRowCellValue(i, "MucDo").ToString());
                       if (objGC == null)
                    {

                        grvHopDong.SetRowCellValue(i, "Error", "Mức độ không tồn tại trong hệ thống");



                    }
                    var objTT =
                     listTT.FirstOrDefault(p => p.TrangThai== grvHopDong.GetRowCellValue(i, "TrangThai").ToString());
                    if (objTT == null)
                    {

                        grvHopDong.SetRowCellValue(i, "Error", "Trạng thái không tồn tại trong hệ thống");



                    }
                    var objNV =
                     listNV.FirstOrDefault(p => p.HoTenNV== grvHopDong.GetRowCellValue(i, "NguoiNhan").ToString());
                    //if (objNV == null)
                    //{

                       
                    if(grvHopDong.GetRowCellValue(i, "MaSo").ToString().Trim() == "")
                    {
                        grvHopDong.SetRowCellValue(i, "Error", "Thẻ không được để trống");
                    }


                    //}
                    var objLT = new ltLeTan();
                    objLT.SoThe = grvHopDong.GetRowCellValue(i, "MaSo").ToString();
                    objLT.SoCMND = grvHopDong.GetRowCellValue(i, "SoCMND").ToString();
                    objLT.MaQH = objQH.ID;
                    objLT.GhiChu = objGC.ID;
                    objLT.MaMB = objMB.MaMB;
                    objLT.NgayCap = (DateTime?) grvHopDong.GetRowCellValue(i, "NgayCap");
                    objLT.NgayHetHan = (DateTime?)grvHopDong.GetRowCellValue(i, "NgayHH");
                    objLT.GioVao = (DateTime?)grvHopDong.GetRowCellValue(i, "TGVao");
                    objLT.GioRa = (DateTime?)grvHopDong.GetRowCellValue(i, "TGRa");
                    objLT.ThoiGianNhan = (DateTime?)grvHopDong.GetRowCellValue(i, "TGNhan");
                    objLT.ThoiGianTra = (DateTime?)grvHopDong.GetRowCellValue(i, "TGTra");
                    objLT.MaTT = objTT.ID;
                    objLT.SoLuongNguoi = (int?)grvHopDong.GetRowCellValue(i, "SLNguoi") ?? 1;
                    objLT.KhachDen = grvHopDong.GetRowCellValue(i, "HoTenKH").ToString();
                    objLT.NoiDung = grvHopDong.GetRowCellValue(i, "NoiDung").ToString();
                    objLT.NoiCap = grvHopDong.GetRowCellValue(i, "NoiCap").ToString(); ;
                    objLT.DienThoai = grvHopDong.GetRowCellValue(i, "SDT").ToString(); ;
                    objLT.NguoiNhan = objNV==null?null:(int?)objNV.MaNV;
                    objLT.NguoiTra = grvHopDong.GetRowCellValue(i, "NguoiTra").ToString();
                    objLT.MaDVT = 6;
                    objLT.MaTN = MaTN;
                    objLT.SoLuongThe = (int?)grvHopDong.GetRowCellValue(i, "SL")??1;
                    objLT.MaNVN = Common.User.MaNV;
                    objLT.NgayNhap = db.GetSystemDate();
                    db.ltLeTans.InsertOnSubmit(objLT);
                    db.SubmitChanges();
                    wait.Close();
                    wait.Dispose();
                    DialogBox.Alert("Đã lưu");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            catch
            {
                DialogBox.Error("Vui lòng xem lại dữ liệu!");
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }

        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvHopDong.DeleteSelectedRows();
        }
    }

    class ImportItem
    {
        public int? STT { get; set; }
        public string TrangThai { get; set; }
        public DateTime? TGVao { get; set; }
        public DateTime? TGRa { get; set; }
        public string SDT { get; set; }
        public string QH { get; set; }
        public string MucDo { get; set; }
        public string NoiDung { get; set; }
        public string HoTenKH { get; set; }
        public string SoCMND { get; set; }
        public DateTime? NgayCap { get; set; }
        public DateTime? NgayHH { get; set; }
        public DateTime? TGNhan { get; set; }
        public DateTime? TGTra { get; set; }
        public int? SL { get; set; }
        public int? SLNguoi { get; set; }
        public string MaSo { get; set; }
        public string NguoiNhan { get; set; }
        public string NguoiTra { get; set; }
        public int? MaMB { get; set; }
        public string MaSoMB { get; set; }
        public string NoiCap { get; set; }
        public string Error { get; set; }
    }
}