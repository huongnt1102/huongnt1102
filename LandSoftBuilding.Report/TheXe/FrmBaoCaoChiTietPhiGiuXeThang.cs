using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace LandSoftBuilding.Report.TheXe
{
    public partial class FrmBaoCaoChiTietPhiGiuXeThang : XtraForm
    {
        public FrmBaoCaoChiTietPhiGiuXeThang()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoChiTietPhiGiuXeThang_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            cbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        public void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        public async void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null) return;
                var tuNgay = (DateTime) itemTuNgay.EditValue;
                var denNgay = (DateTime) itemDenNgay.EditValue;
                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                    .Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;

                var listHd = new List<ListHdBcChiTietGiuXeThang>();
                List<BcChiTietGiuXeThang> list1 = new List<BcChiTietGiuXeThang>(),
                    list = new List<BcChiTietGiuXeThang>(), objList = new List<BcChiTietGiuXeThang>();

                await Task.Run(() =>
                {
                    listHd = (from hd in db.dvHoaDons
                        where ltToaNha.Contains(hd.MaTN.ToString()) & hd.IsDuyet.GetValueOrDefault() &
                              hd.MaLDV == 6 &
                              SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 &
                              SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                        select new ListHdBcChiTietGiuXeThang
                        {
                            PhaiThu = hd.PhaiThu.GetValueOrDefault(), LinkId = hd.LinkID, MaMb = hd.MaMB,
                            MaKh = hd.MaKH
                        }).ToList();
                });

                await Task.Run(() =>
                {
                    list1 = (from hd in listHd
                        join tx in db.dvgxTheXes on hd.LinkId equals tx.ID
                        join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                        join mb in db.mbMatBangs on hd.MaMb equals mb.MaMB
                        join kh in db.tnKhachHangs on hd.MaKh equals kh.MaKH
                        orderby mb.MaMB
                        select new BcChiTietGiuXeThang
                        {
                            MaCanHo = mb.MaSoMB,
                            MaPhu = kh.KyHieu,
                            Email = kh.EmailKH, MaKh = hd.MaKh, MaMb = hd.MaMb,
                            TenKhachHang = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                            XeOTo = lx.MaNX == 3 ? hd.PhaiThu : 0,
                            XeMay = lx.MaNX == 2 ? hd.PhaiThu : 0,
                            XeDap = lx.MaNX == 1 ? hd.PhaiThu : 0,
                            TongCong = hd.PhaiThu
                        }).ToList();
                });

                await Task.Run(() =>
                {
                    list = (from hd in list1
                        group new {hd} by
                            new
                            {
                                hd.MaCanHo, hd.MaPhu,
                                hd.TenKhachHang, hd.Email, hd.MaMb,hd.MaKh
                            }
                        into g
                        select new BcChiTietGiuXeThang
                        {
                            MaCanHo = g.Key.MaCanHo, MaPhu = g.Key.MaPhu, TenKhachHang = g.Key.TenKhachHang, Email = g.Key.Email, MaMb = g.Key.MaMb, MaKh = g.Key.MaKh,
                            XeOTo = g.Sum(_ => _.hd.XeOTo),
                            XeMay = g.Sum(_ => _.hd.XeMay),
                            XeDap = g.Sum(_ => _.hd.XeDap),
                            TongCong = g.Sum(_ => _.hd.TongCong)
                        }).ToList();
                });
                await Task.Run(() =>
                {
                    objList = (from hd in list
                        where hd.TongCong != 0
                        select new BcChiTietGiuXeThang
                        {
                            MaCanHo = hd.MaCanHo, MaPhu = hd.MaPhu, TenKhachHang = hd.TenKhachHang, Email=hd.Email, MaKh = hd.MaKh, MaMb = hd.MaMb,
                            XeDap = hd.XeDap, XeOTo = hd.XeOTo, XeMay = hd.XeMay, TongCong = hd.TongCong
                        }).ToList();
                });
                gc.DataSource = objList;
            }
            catch{}
        }

        public class ListHdBcChiTietGiuXeThang
        {
            public decimal? PhaiThu { get; set; }
            public int? LinkId { get; set; }
            public int? MaMb { get; set; }
            public int? MaKh { get; set; }
        }

        public class BcChiTietGiuXeThang
        {
            public int? MaMb { get; set; }
            public int? MaKh { get; set; }

            public string MaCanHo { get; set; }
            public string MaPhu { get; set; }
            public string TenKhachHang { get; set; }
            public string Email { get; set; }

            public decimal? XeOTo { get; set; }
            public decimal? XeMay { get; set; }
            public decimal? XeDap { get; set; }
            public decimal? TongCong { get; set; }
        }

        private void CbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void Gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }
        private bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void itemSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.SelectAll();
                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                    .Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
 
                var db = new MasterDataContext();

                var objManagerEmployee = db.mail_SetupSendMailToEmployeeDebits
                    .Where(_ => ltToaNha.Contains(_.BuildingId.ToString()) & _.IsSendMail == true & _.GroupId == 8)
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
                            objMail.Subject = "Báo cáo chi tiết phí gửi xe tháng";
                            objMail.Content = "";
                            var fileAttach = new System.Net.Mail.Attachment(result, "BaoCaoChiTietPhiGuiXeThang",
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
                    objHistoryEmail.Subject = "Báo cáo chi tiết phí gửi xe tháng";
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
            catch{}
            
        }
    }
}