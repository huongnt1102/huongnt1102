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
using EmailVerifier.Core;
using EmailAmazon.API;
namespace EmailAmazon
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte? MaTN;
        public bool IsUpdate = false;
        public int? MaNKH { get; set; }

        public frmImport()
        {
            InitializeComponent();
            MailCommon.cmd = new APISoapClient();
            MailCommon.cmd.Open();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
            MailCommon.cmd = new API.APISoapClient();
            MailCommon.cmd.Open();
        }

        bool getSex(string val)
        {
            try
            {
                return Convert.ToBoolean(val.Trim());
            }
            catch { }

            return false;
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                //try
                //{
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                    var item = book.Worksheet(0).Select(p => new
                    {
                        XungHo = p["Xưng hô"].ToString().Trim(),
                        HoTen = p["Họ và tên"].ToString().Trim(),
                        Email = p["Email"].ToString().Trim(),
                        NgaySinh = p["Ngày sinh"].ToString().Trim(),
                        DienThoai = p["Điện thoại"].ToString().Trim(),
                        DiaChi = p["Địa chỉ"].ToString().Trim(),
                    });

                    List<KhachHang> newlist = new List<KhachHang>();
                    foreach (var it in item)
                    {
                        KhachHang importitem = new KhachHang()
                        {
                            XungHo = it.XungHo,
                            TenKH = it.HoTen,
                            Email = it.Email,
                            NgaySinh = it.NgaySinh,
                            DienThoai = it.DienThoai,
                            DiaChi = it.DiaChi,
                        };
                        newlist.Add(importitem);
                    }
                    gcCaNhan.DataSource = newlist;
                //}
                //catch(Exception ex)
                //{
                //    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại.\r\nCode: " + ex.Message);
                //}

                wait.Close();
                wait.Dispose();
            }
        }

        private DateTime? MyConvert(LinqToExcel.Cell value)
        {
            try
            {
                //return value.Cast<DateTime>(); 
                return DateTime.FromOADate(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        private int? SearchKV(string chuoi)
        {
            try
            {
                return db.tnKhuVucs.FirstOrDefault(p => SqlMethods.Like(p.TenKV, "%" + chuoi + "%")).MaKV;
            }
            catch
            {
                return null;
            }
        }

        private void LoadData()
        {
            lkNhomKhachHang.DataSource = (object)MailCommon.cmd.GetNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau);
            itemNhomKhachHang.EditValue = (object)this.MaNKH;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

           Insert();
        }

        void Insert()
        {
           if (gcCaNhan.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var verifier = new EmailVerifier.Core.EmailVerifier(true);
                var ltMail = (List<KhachHang>)gcCaNhan.DataSource;
                var ltHopLe = new List<KhachHang>();
                var ltError = new List<KhachHang>();

                var kh = new API.KhachHang();
                foreach (var item in ltMail)
                {
                    switch (verifier.CheckExists(item.Email))
                    {
                        case 1:
                            ltHopLe.Add(item);
                            break;
                        case 2:
                            item.KQKT = "Email không xác định";
                            ltError.Add(item);
                            break;
                        default:
                            item.KQKT = "Email không tồn tại";
                            ltError.Add(item);
                            break;
                    }
                }

                var KQ = MailCommon.cmd.EditKhachHang(MailCommon.MaHD, MailCommon.MatKhau, (int)itemNhomKhachHang.EditValue, ltHopLe.ToArray(), Common.User.HoTenNV);
                if (KQ == API.Result.ThanhCong)
                {
                };
                gcCaNhan.DataSource = ltError;
                wait.Close();
            }
            catch { }
        }

        private string GetNewMaKH()
        {
            string makh = "";
            db.khKhachHang_getNewMaKH(ref makh);
            return makh;
        }

        private void CheckData()
        {
            var data = db.tnKhachHangs.Where(p => p.IsCaNhan.Value).ToList();
            for (int i = 0; i < grvCaNhan.RowCount; i++)
            {
                var MaKH = grvCaNhan.GetRowCellValue(i, colKyHieu).ToString();
                if (data.Where(p => p.KyHieu == MaKH.Trim()).Count() > 0)
                {
                    //grvCaNhan.setr
                    //e.Appearance.BackColor = Color.Blue;
                }
            }
        }

        private void grvCaNhan_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (db.tnKhachHangs.Where(p => p.KyHieu == grvCaNhan.GetRowCellValue(e.RowHandle, colKyHieu).ToString().Trim()).Count() > 0)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
            catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvCaNhan.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcCaNhan);
        }
    }

    class ImportItem
    {
        public string XungHo { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string NgaySinh { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Loi { get; set; }
    }
}