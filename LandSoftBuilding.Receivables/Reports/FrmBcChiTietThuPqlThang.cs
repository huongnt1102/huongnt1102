using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class FrmBcChiTietThuPqlThang : XtraForm
    {
        private List<BcChiTietThuPqlThang> _list = new List<BcChiTietThuPqlThang>(); 
        public FrmBcChiTietThuPqlThang()
        {
            InitializeComponent();
        }

        private void FrmBcChiTietThuPqlThang_Load(object sender, EventArgs e)
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
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime?)itemTuNgay.EditValue;
                    var denNgay = (DateTime?)itemDenNgay.EditValue;
                    var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                    var ltToaNha = strToaNha.Split(',');
                    if (strToaNha == "") return;

                    
                        var listHd = (from hd in db.dvHoaDons
                            where ltToaNha.Contains(hd.MaTN.ToString()) & hd.MaLDV == 13 &
                                  SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 &
                                  SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0 & hd.IsDuyet == true
                            select new { hd.KyTT, PhaiThu = hd.PhaiThu.GetValueOrDefault(), hd.TuNgay, hd.DenNgay, hd.MaMB, hd.MaKH }).ToList();

                        _list =
                            (from hd in listHd
                                join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into matBang
                                from mb in matBang.DefaultIfEmpty()
                                join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH into khachHang
                                from kh in khachHang.DefaultIfEmpty()
                                group hd by new
                                {
                                    mb.MaSoMB, kh.KyHieu, mb.DienTich,
                                    TenKh = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen
                                }
                                into g
                                orderby g.Key.MaSoMB
                                select new BcChiTietThuPqlThang
                                {
                                    TenKhachHang = g.Key.TenKh, CanHo = g.Key.MaSoMB, MaKhachHang = g.Key.KyHieu,
                                    DienTich = g.Key.DienTich,
                                    Pql1Thang = g.Where(_ => _.KyTT == 1).Sum(_ => _.PhaiThu),
                                    Pql3Thang = g.Where(_ => _.KyTT == 3).Sum(_ => _.PhaiThu),
                                    Pql6Thang = g.Where(_ => _.KyTT == 6).Sum(_ => _.PhaiThu),
                                    Pql1Nam = g.Where(_ => _.KyTT == 12).Sum(_ => _.PhaiThu),
                                    NgayBatDau = g.Min(_ => _.TuNgay),
                                    NgayKetThuc = g.Max(_ => _.DenNgay),
                                    TongPhaiThu = g.Sum(_ => _.PhaiThu)
                                }).ToList();
                    
                    gc.DataSource = _list;
                }
            }
            catch{}
        }

        public class BcChiTietThuPqlThang
        {
            public string TenKhachHang { get; set; }
            public string CanHo { get; set; }
            public string MaKhachHang { get; set; }
            public decimal? DienTich { get; set; }
            public decimal? Pql1Thang { get; set; }
            public decimal? Pql3Thang { get; set; }
            public decimal? Pql6Thang { get; set; }
            public decimal? Pql1Nam { get; set; }
            public decimal? TongPhaiThu { get; set; }
            public DateTime? NgayBatDau { get; set; }
            public DateTime? NgayKetThuc { get; set; }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    .Where(_ => ltToaNha.Contains(_.BuildingId.ToString()) & _.IsSendMail == true & _.GroupId == 9)
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
                            objMail.Subject = "Báo cáo chi tiết Phí Quản Lý";
                            objMail.Content = "";
                            var fileAttach = new System.Net.Mail.Attachment(result, "BaoCaoChiTietPhiQuanLyThang",
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
                    objHistoryEmail.Subject = "Báo cáo chi tiết Phí Quản Lý";
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