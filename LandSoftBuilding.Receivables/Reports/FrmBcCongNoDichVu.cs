using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class FrmBcCongNoDichVu : XtraForm
    {
        public FrmBcCongNoDichVu()
        {
            InitializeComponent();
        }

        private void FrmBcCongNoDichVu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[7];
            SetDate(7);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                gc.DataSource = null;
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime) itemTuNgay.EditValue;
                    var denNgay = (DateTime) itemDenNgay.EditValue;
                    List<CongNoHoaDon> objHdNdk = new List<CongNoHoaDon>(), objSqNdk = new List<CongNoHoaDon>(), objPhatSinh = new List<CongNoHoaDon>(), objDaThu = new List<CongNoHoaDon>();
                    List<BcCongNoDichVu> objList = new List<BcCongNoDichVu>(), obj = new List<BcCongNoDichVu>();
                    var objKh = db.tnKhachHangs.Where(_ => _.MaTN == (byte) itemToaNha.EditValue).Select(_ =>
                        new
                        {
                            _.MaKH, MaKhachHang = _.KyHieu,
                            TenKhachHang = _.IsCaNhan == true ? (_.HoKH + " " + _.TenKH) : _.CtyTen
                        }).ToList();
                    
                        objHdNdk = (from hd in db.dvHoaDons
                            where SqlMethods.DateDiffDay(hd.NgayTT, tuNgay) > 0 & hd.IsDuyet == true &
                                  hd.MaTN == (byte) itemToaNha.EditValue
                            group hd by hd.MaKH
                            into ndk
                            select new CongNoHoaDon {MaKH = ndk.Key, PhaiThu = ndk.Sum(_ => _.PhaiThu)}).ToList();
                    
                        objSqNdk = (from sq in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffDay(sq.NgayPhieu, tuNgay) > 0 &&
                                  sq.MaTN == (byte)itemToaNha.EditValue && sq.IsPhieuThu == true && sq.MaLoaiPhieu != 24
                            group sq by sq.MaKH
                            into ndk
                            select new CongNoHoaDon { MaKH = ndk.Key, PhaiThu = ndk.Sum(_ => _.DaThu + _.KhauTru - _.ThuThua) }).ToList();
                    
                        objPhatSinh = (from hd in db.dvHoaDons
                            where SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0 & hd.IsDuyet == true &&
                                  hd.MaTN == (byte)itemToaNha.EditValue
                            group hd by hd.MaKH
                            into ps
                            select new CongNoHoaDon { MaKH = ps.Key, PhaiThu = ps.Sum(_ => _.PhaiThu).GetValueOrDefault() }).ToList();
                    
                        objDaThu = (from ct in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0 && ct.IsPhieuThu == true &&
                                  ct.MaLoaiPhieu != 24 && ct.MaTN == (byte)itemToaNha.EditValue
                            group ct by ct.MaKH
                            into dt
                            select new CongNoHoaDon { MaKH = dt.Key, PhaiThu = dt.Sum(_ => _.DaThu).GetValueOrDefault() }).ToList();
                    
                        objList = (from kh in objKh
                            join ndk in objHdNdk on kh.MaKH equals ndk.MaKH into nodk
                            from ndk in nodk.DefaultIfEmpty()
                            join sqdk in objSqNdk on kh.MaKH equals sqdk.MaKH into soquydk
                            from sqdk in soquydk.DefaultIfEmpty()
                            join ps in objPhatSinh on kh.MaKH equals ps.MaKH into psinh
                            from ps in psinh.DefaultIfEmpty()
                            join dt in objDaThu on kh.MaKH equals dt.MaKH into dthu
                            from dt in dthu.DefaultIfEmpty()
                            select new BcCongNoDichVu
                            {
                                MaKH = kh.MaKH,
                                MaKhachHang = kh.MaKhachHang,
                                TenKhachHang = kh.TenKhachHang,
                                CongNoCu = (ndk == null ? 0 : ndk.PhaiThu.GetValueOrDefault()) -
                                           (sqdk == null ? 0 : sqdk.PhaiThu.GetValueOrDefault()),
                                PhatSinh = ps == null ? 0 : ps.PhaiThu,
                                DaThu = dt == null ? 0 : dt.PhaiThu,
                            }).ToList();
                    
                        obj = (from o in objList
                            where o.CongNoCu != 0 || o.PhatSinh != 0 || o.DaThu != 0
                            select new BcCongNoDichVu
                            {
                                MaKhachHang= o.MaKhachHang,
                                TenKhachHang= o.TenKhachHang,
                                CongNoCu= o.CongNoCu,
                                PhatSinh= o.PhatSinh,
                                DaThu= o.DaThu,
                                CongNoCuoi = o.CongNoCu + o.PhatSinh - o.DaThu
                            }).ToList();
                    

                    gc.DataSource = obj;
                }
            }
            catch{}
        }

        public class BcCongNoDichVu
        {
            public int? MaKH { get; set; }
            public string MaKhachHang { get; set; }
            public string TenKhachHang { get; set; }
            public decimal? CongNoCu { get; set; }
            public decimal? PhatSinh { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? CongNoCuoi { get; set; }
        }

        public class CongNoHoaDon
        {
            public int? MaKH { get; set; }
            public decimal? PhaiThu { get; set; }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.SelectAll();
                //var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                //    .Replace(" ", "");
                //var ltToaNha = strToaNha.Split(',');
                //if (strToaNha == "") return;

                var db = new MasterDataContext();

                var objManagerEmployee = db.mail_SetupSendMailToEmployeeDebits
                    .Where(_ => _.BuildingId == (byte)itemToaNha.EditValue & _.IsSendMail == true & _.GroupId == 1)
                    .Select(_ => new { _.EmployeeId }).Distinct().ToList();

                foreach (var employee in objManagerEmployee)
                {
                    var objEmployee = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == employee.EmployeeId);
                    if (objEmployee == null) continue;
                    if (objEmployee.Email == "") continue;

                    #region Send mail

                    var objMail = new LandSoftBuilding.Marketing.Mail.MailClient();
                    var objFrom = db.mailConfigs.OrderByDescending(_ => _.ID).FirstOrDefault();
                    int status = 1;
                    if (objFrom != null)
                    {
                        try
                        {
                            // file excel
                            var result = new System.IO.MemoryStream();
                            var options = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gc.ExportToXlsx(result);
                            result.Seek(0, System.IO.SeekOrigin.Begin);

                            // send email
                            objMail.SmtpServer = objFrom.Server;
                            if (objFrom.Port != null) objMail.Port = objFrom.Port.Value;
                            if (objFrom.EnableSsl != null) objMail.EnableSsl = objFrom.EnableSsl.Value;
                            objMail.From = objFrom.Email;
                            objMail.Reply = objFrom.Reply;
                            objMail.Display = objFrom.Display;
                            objMail.Pass = it.EncDec.Decrypt(objFrom.Password);
                            objMail.To = objEmployee.Email;
                            objMail.Subject = "Báo cáo tổng hợp Công nợ dịch vụ";
                            objMail.Content = "";
                            var fileAttach = new System.Net.Mail.Attachment(result, "BaoCaoTongHopCongNoDichVu",
                                "application/vnd.ms-excel");
                            objMail.Attachs = new List<System.Net.Mail.Attachment>();
                            objMail.Attachs.Add(fileAttach);

                            objMail.Send();
                            status = 1;
                        }
                        catch
                        {
                            status = 2;
                        }
                    }

                    #endregion

                    #region Lịch sử gửi email

                    mailHistory objHistoryEmail = new mailHistory();
                    objHistoryEmail.MailID = objFrom.ID;
                    objHistoryEmail.ToMail = objEmployee.Email;
                    objHistoryEmail.CcMail = "";
                    objHistoryEmail.BccMail = "";
                    objHistoryEmail.Subject = "Báo cáo tổng hợp Công nợ dịch vụ";
                    objHistoryEmail.Contents = "";
                    objHistoryEmail.Status = status;
                    objHistoryEmail.DateCreate = DateTime.UtcNow.AddHours(7);
                    objHistoryEmail.StaffCreate = Common.User.MaNV;
                    objHistoryEmail.CusID = null;
                    db.mailHistories.InsertOnSubmit(objHistoryEmail);
                    db.SubmitChanges();

                    #endregion
                }

                DialogBox.Success("Đã gửi thư xong.");
                return;
            }
            catch { }
        }
    }
}