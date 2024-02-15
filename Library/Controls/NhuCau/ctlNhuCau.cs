using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using Library.Utilities;


namespace Library.Controls.NhuCau
{
    public partial class ctlNhuCau : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public byte? MaTN;

        //public List<byte?> MaToaNha;

        public DateTime? tuNgay, denNgay;

        public int? MaKH;

        ncCauHinh objConfig;

        public System.Windows.Forms.Form frm { get; set; }

        public ctlNhuCau()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            var _maTN = this.MaTN;

            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    if(objConfig == null)
                        objConfig = db.ncCauHinhs.First();

                    var timealert = objConfig.LH_NhacTruoc.GetValueOrDefault();

                    gcNhuCau.DataSource = (from p in db.ncNhuCaus
                                           join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                                           
                                           join nvql in db.tnNhanViens on p.MaNVQL equals nvql.MaNV into nvql_p
                                           from nvql in nvql_p.DefaultIfEmpty()
                                           join tt in db.ncTrangThais on p.MaTT equals tt.MaTT into tt_p
                                           from tt in tt_p.DefaultIfEmpty()
                                           join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nkh_p
                                           from nkh in nkh_p.DefaultIfEmpty()
                                           join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV into nvn_p
                                           from nvn in nvn_p.DefaultIfEmpty()
                                           join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into nvs_p
                                           from nvs in nvs_p.DefaultIfEmpty()
                                           join step in db.ncCauHinhTiemNangs on p.StepID equals step.Step into giaidoan
                                           from step in giaidoan.DefaultIfEmpty()
                                           //join nvht in db.ncNhanVienHoTros on p.MaNC equals nvht.MaNC into dsht
                                           //from nvht in dsht.DefaultIfEmpty()
                                           //where p.MaTN == _maTN | p.MaKH == this.MaKH #L
                                           where p.ncNhanVienHoTros.Any(o => o.MaNV == Common.User.MaNV) || (p.MaKH == this.MaKH) || p.MaTN == _maTN
                                           orderby tt.STT 
                                           select new
                                           {
                                               NgayCN = p.NgayNhap,
                                               p.MaNC,
                                               p.SoNC,
                                               p.MaKH,
                                               p.TenCH,
                                               p.NganSach,
                                               NhanVienQuanLy = p.MaNVQL != null ? nvql.HoTenNV : "",
                                               MaTT = p.MaTT ?? 0,//p.MaTT,
                                               TenTT = p.MaTT != null ? tt.TenTT : "",
                                               p.TiemNang,
                                               MaNVN = p.MaNVN != null ? nvn.HoTenNV : "",
                                               MaNVS = p.MaNVS != null ? nvs.HoTenNV : "",
                                               p.NgayNhap,
                                               p.NgaySua,
                                               MaNKH = nkh != null ? nkh.TenNKH : "",
                                               kh.KyHieu,
                                               //TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.MaKH + " " + kh.TenKH : kh.CtyTen, #L
                                               TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                               DienThoaiCT = kh.DienThoai,
                                               kh.DiDong,
                                               kh.Email,
                                               FaxCT = kh.Fax,
                                               NhanVienHoTro = string.Join(", ", p.ncNhanVienHoTros.Select(o => o.tnNhanVien.HoTenNV).ToArray()),
                                               TrangThaiBG = p.BaoGias.Any() ? "Đã gửi" : "",
                                               //TrangThaiHD = p.BaoGias.Any(o => o.ctHopDongs.Any()) ? "Đã lập" : "",#L
                                               //TrangThaiHD = db.ctHopDongs.Any(o=>o.MaNC == p.MaNC) ? "Đã lập" : "",
                                               TrangThaiThamQuan = p.LichHens.Any(o => o.XuLy_MaTT == 4) ? "Đã tham quan" : "",
                                               GiaiDoan = step.Name,
                                               IsTBLichHen = p.LichHens.Any(o => SqlMethods.DateDiffMinute(DateTime.Now, o.NgayBD) >= 0 & SqlMethods.DateDiffMinute(DateTime.Now, o.NgayBD) <= timealert & o.XuLy_MaTT.GetValueOrDefault() == 0) ? 1 : 0,
                                               TGConLai = p.LichHens.Where(o => SqlMethods.DateDiffMinute(DateTime.Now, o.NgayBD) >= 0
                                                                                & SqlMethods.DateDiffMinute(DateTime.Now, o.NgayBD) <= timealert
                                                                                & o.XuLy_MaTT.GetValueOrDefault() == 0)
                                                                    .Select(o => SqlMethods.DateDiffMinute(DateTime.Now, o.NgayBD)).FirstOrDefault(),
                                               ToaNha = String.Join(", ", p.ncSanPhams.Where(o => o.tnToaNha != null).Select(o => o.tnToaNha.TenVT).Distinct().ToArray()),
                                               SoGhe = String.Join(", ", p.ncSanPhams.Where(o => o.tnToaNha != null).Select(o => o.tnToaNha.TenVT + "(" + ((int)o.SoLuong.GetValueOrDefault()).ToString() + " Ghế" + ")").Distinct().ToArray()),
                                               NoiDung = string.Join (";" ,db.ncNhatKies.Where(o=>o.MaNC == p.MaNC).Select(o=> string.Format("{0} - {1}",o.NgayXL, o.DienGiai)).ToArray())
                                           }).OrderByDescending(o => o.IsTBLichHen).AsEnumerable().Select((p, Index) => new
                                           {
                                               STT = Index + 1,
                                               p.NgayCN,
                                               p.MaNC,
                                               p.SoNC,
                                               p.MaKH,
                                               p.TenCH,
                                               p.NganSach,
                                               p.NhanVienQuanLy,
                                               p.MaTT ,
                                               p.TenTT,
                                               p.TiemNang,
                                               p.MaNVN,
                                               p.MaNVS,
                                               p.NgayNhap,
                                               p.NgaySua,
                                               p.MaNKH,
                                               p.KyHieu,
                                               p.TenKH,
                                               p.DienThoaiCT,
                                               p.DiDong,
                                               p.Email,
                                               p.FaxCT,
                                               p.NhanVienHoTro,
                                               p.TrangThaiBG,
                                               //p.TrangThaiHD,
                                               p.TrangThaiThamQuan,
                                               p.GiaiDoan,
                                               p.IsTBLichHen,
                                               p.TGConLai,
                                               p.ToaNha,
                                               p.SoGhe,
                                               p.NoiDung,
                                           }).ToList();

                    grvNhuCau.FocusedRowHandle = -1;
                }
            }
            catch
            {
            }
            finally
            {
                wait.Close();
            }
        }

        private void grvNhuCau_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                var IsTBLichHen = (bool?)grvNhuCau.GetRowCellValue(e.RowHandle, "IsTBLichHen");

                if (IsTBLichHen.GetValueOrDefault())
                {
                    e.Appearance.BackColor = Color.FromArgb(objConfig.LH_MauSac.GetValueOrDefault());
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
            catch { }
        }

        public void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var _maKH = grvNhuCau.GetFocusedRowCellValue("MaKH");
            //var rows = grvNhuCau.GetSelectedRows();
            //if (rows.Length <= 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
            //    return;
            //}

            //int TongGui = 0;

            //List<Library.Mail.MailClient.EmailCls> Emails = new List<Library.Mail.MailClient.EmailCls>();

            //foreach (var index in rows)
            //{
            //    try
            //    {
            //        string[] ltMail = grvNhuCau.GetRowCellDisplayText(index, "Email").ToString().Replace(" ", "").Split(';');
            //        foreach (var email in ltMail)
            //        {
            //            if (email.Length > 0)
            //            {
            //                Emails.Add(new Library.Mail.MailClient.EmailCls { MaKH = Convert.ToInt32(grvNhuCau.GetRowCellValue(index, "MaKH")), Email = email });
            //                TongGui++;
            //            }
            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        DialogBox.Error(ex.Message);
            //    }
            //}

            //if (TongGui == 0)
            //{
            //    DialogBox.Error("Không có email nào để gửi");
            //    return;
            //}

            //var frm = new Library.Email.frmSend("", 0 ,this.MaTN.Value, Emails);
            //var date = this.tuNgay.Value;
            //frm.Month = date.Month;
            //frm.Year = date.Year;
            //frm.Show();
        }

        public void itemPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhuCau_Edit(true);
        }

        public void itmThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmEdit(false))
            {
                if(MaTN != null)
                    frm.MaTN = (byte?)this.MaTN;
                else
                    frm.MaTN = Common.User.MaTN;

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.LoadData();

            }
        }

        public void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhuCau_Edit(false);
        }

        private void NhuCau_Edit(bool IsView)
        {
            using (var frm = new Library.Controls.NhuCau.frmEdit(IsView))
            {
                frm.ID = (int)grvNhuCau.GetFocusedRowCellValue("MaNC");
                frm.MaTN = this.MaTN;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadData();
                }
            }
        }

        public void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhuCau_Delete();
        }

        private void NhuCau_Delete()
        {
            var indexs = grvNhuCau.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn nhu cầu");
                return;
            }

            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;

            foreach (var i in indexs)
            {
                var objNC = db.ncNhuCaus.Single(p => p.MaNC == (int)grvNhuCau.GetRowCellValue(i, "MaNC"));

                if (objNC.BaoGias.Any())
                    continue;

                db.ncNhuCaus.DeleteOnSubmit(objNC);
            }

            db.SubmitChanges();

            grvNhuCau.DeleteSelectedRows();
        }

        public void itemXuLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
            try
            {
                var id = (int)grvNhuCau.GetFocusedRowCellValue("MaNC");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn nhu cầu");
                    return;
                }

                using (var frm = new Library.Controls.NhuCau.frmDuyet())
                {
                    frm.MaTN = this.MaTN;
                    frm.maNC = id;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.LoadData();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        public void itemNhanVienHoTro_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maNC = (int?)grvNhuCau.GetFocusedRowCellValue("MaNC");
            if (maNC == null)
            {
                DialogBox.Error("Vui lòng chọn <Nhu cầu>, xin cảm ơn.");
                return;
            }

            var frm = new Library.Controls.NhuCau.frmChuyenNhanVien();
            frm.MaNC = maNC;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        public void itemTinhTiemNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maNC = (int?)grvNhuCau.GetFocusedRowCellValue("MaNC");
            if (maNC == null)
            {
                DialogBox.Error("Vui lòng chọn <Nhu cầu>, xin cảm ơn.");
                return;
            }

            NhuCauCls.TinhTiemNang(maNC);
        }

        public void itemHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maNC = (int?)grvNhuCau.GetFocusedRowCellValue("MaNC");
            if (maNC == null)
            {
                DialogBox.Error("Vui lòng chọn <Nhu cầu>, xin cảm ơn.");
                return;
            }
        }


        public void itemGuiMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        public void itemGuiSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        public void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        public int? getID()
        {
            return (int?)grvNhuCau.GetFocusedRowCellValue("MaNC");
        }

        public int? getMaKH()
        {
            return (int?)grvNhuCau.GetFocusedRowCellValue("MaKH");
        }

        private void ctlNhuCau_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
        }
    }
}
