using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;
//using Library.Email;

namespace Library.Controls.LichHen
{
    public partial class ctlLichHen : DevExpress.XtraEditors.XtraUserControl
    {
        DateTime? tuNgay { get; set; }

        DateTime? denNgay { get; set; }

        int? maNC { get; set; }

        int? maNV { get; set; }

        int? maKH { get; set; }

        int? maThoiDiem { get; set; }

        byte? maTN { get; set; }

        public System.Windows.Forms.Form frm { get; set; }

        public ctlLichHen()
        {
            InitializeComponent();
        }

        public void LoadData(DateTime? _tuNgay, DateTime? _denNgay, int? _maNV, int? _maThoiDiem,byte? _maTN)
        {
            this.tuNgay = _tuNgay;
            this.denNgay = _denNgay;
            this.maNV = _maNV;
            this.maThoiDiem = _maThoiDiem;
            this.maTN = _maTN;
            this._Load();
        }

        public void LoadData(int? maNC, int? _maKH)
        {
            this.maNC = maNC;
            this.maKH = _maKH;
            this._Load();
        }

        void _Load()
        {
            //using (var db = new MasterDataContext())
            //{
            MasterDataContext db = new MasterDataContext();
                gcScheduler.DataSource = (from lh in db.LichHens
                                          join cd in db.LichHen_ChuDes on lh.MaCD equals cd.MaCD into chuDe
                                          from cd in chuDe.DefaultIfEmpty()
                                          join td in db.LichHen_ThoiDiems on lh.MaTD equals td.MaTD into thoiDiem
                                          from td in thoiDiem.DefaultIfEmpty()
                                          join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH into khachHang
                                          from kh in khachHang.DefaultIfEmpty()
                                          join nv in db.tnNhanViens on lh.MaNV equals nv.MaNV into nhanVien
                                          from nv in nhanVien.DefaultIfEmpty()
                                          join tt in db.LichHen_TrangThais on lh.XuLy_MaTT equals tt.ID into trangthai
                                          from tt in trangthai.DefaultIfEmpty()

                                          join nc in db.ncNhuCaus on lh.MaNC equals nc.MaNC into nhucau
                                          from nc in nhucau.DefaultIfEmpty()

                                          join nvtiep in db.tnNhanViens on lh.MaNVTiep equals nvtiep.MaNV into nhanvientiep
                                          from nvtiep in nhanvientiep.DefaultIfEmpty()

                                          join nvsup in db.tnNhanViens on lh.MaNVHoTro equals nvsup.MaNV into nhanviensupport
                                          from nvsup in nhanviensupport.DefaultIfEmpty()

                                          //join tn in db.tnToaNhas on lh.DiaDiem equals tn.MaTN 

                                          where (lh.MaNC == this.maNC | lh.MaKH == this.maKH)
                                          |
                                          (SqlMethods.DateDiffDay(tuNgay, lh.NgayBD) >= 0
                                          & SqlMethods.DateDiffDay(lh.NgayBD, denNgay) >= 0
                                          | lh.MaTN == maTN 
                                          & (
                                               maThoiDiem == 0 | lh.MaTD == maThoiDiem
                                            )
                                          & ((
                                               maNV == 0
                                             | lh.MaNV == maNV
                                             | lh.LichHen_tnNhanViens.Any(o => o.MaNV == maNV)
                                             | lh.MaNVHoTro == maNV
                                             | lh.MaNVTiep == maNV
                                             
                                           )| Common.User.IsSuperAdmin == true))
                                          orderby lh.NgayBD descending
                                          select new
                                          {
                                              lh.MaLH,

                                              lh.TieuDe,

                                              lh.DienGiai,

                                              nv.HoTenNV,

                                              lh.NgayBD,

                                              lh.NgayKT,
                                              lh.NgayNhac,
                                              lh.IsNhac,
                                              lh.IsRepeat,
                                              chuDe = String.Join(", ", lh.LichHen_ChiTietChuDes.Select( o=> o.LichHen_ChuDe.TenCD).ToArray()),

                                              StatusID = td.STT,
                                              kh.KyHieu,
                                              HoTenKH = kh.HoKH,
                                              //lh.DiaDiem,
                                              DiaDiem = lh.DiaDiem,
                                              lh.ThoiGianGap,
                                              tt.TenTrangThai,
                                              //lh.XuLy_NoiDung,
                                              XuLy_NoiDung = string.Format("{0} - {1}", lh.XuLy_NgayXuLy == null ? "" : lh.XuLy_NgayXuLy.Value.ToString(),lh.XuLy_NoiDung ),
                                              NguoiLienQuan = String.Join(", ", db.LichHen_tnNhanViens.Where(o => o.MaLH == lh.MaLH).Select(o => o.tnNhanVien.HoTenNV).ToArray()),
                                              lh.MaNC,
                                              nc.SoNC,
                                              NVTiep = nvtiep.HoTenNV,
                                              NVHoTro = nvsup.HoTenNV,
                                          });
            //}
        }

        private void itemXuLy_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");

			using (var db = new MasterDataContext())
			{
				if(db.LichHens.Any( o=> o.MaLH == id & o.XuLy_MaTT.GetValueOrDefault() > 0))
				{
					DialogBox.Error("Lịch hẹn này đã xử lý. Không thể xử lý lần nữa");
					return;
				}
			}

			using (var frm = new frmXuLy(id))
			{
				if (frm.ShowDialog() == DialogResult.OK)
					this._Load();
			}
        }

        private void itemViewCH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maNC = (int?)gvScheduler.GetFocusedRowCellValue("MaNC");

            if (maNC == null)
            {
                DialogBox.Error("Không tìm thấy nhu cầu. Vui lòng kiểm tra lại");
                return;
            }

            Library.App_Codes.DelegateMeThodCls.CoHoiView(maNC);
        }

		private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			var id = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");

			using (var frm = new AddNew_frm())
			{
				frm.MaLH = id;
				if (frm.ShowDialog() == DialogResult.OK)
					this._Load();
			}
		}

        public void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new AddNew_frm())
            {
                using (var db = new MasterDataContext())
                {
                    var objNC = db.ncNhuCaus.FirstOrDefault(o => o.MaNC == this.maNC);
                    var objKH = db.tnKhachHangs.FirstOrDefault(o => o.MaKH == this.maKH);

                    frm.MaTN = this.maTN;
                    frm.MaKH = this.maKH;
                    frm.MaTN = Common.User.MaTN;

                    if (objNC != null)
                    {
                        frm.MaTN = objNC.MaTN;
                        frm.MaNC = this.maNC;
                    }


                    if (frm.ShowDialog() == DialogResult.OK)
                        this._Load();
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");

            using (var db = new MasterDataContext())
            {
                var objLH = db.LichHens.FirstOrDefault(o => o.MaLH == id);

                if (objLH == null)
                    return;

                db.LichHens.DeleteOnSubmit(objLH);
                db.SubmitChanges();
                Library.Utilities.NhuCauCls.TinhTiemNang(objLH.MaNC);

                this._Load();
            }
        }

        private void itemDoiNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maLH = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");

            if (maLH == null)
            {
                DialogBox.Error("Vui lòng chọn lịch hẹn");
                return;
            }

            using (var frm = new Library.Controls.LichHen.formDoiNhanVien(maLH))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    this._Load();
            }
        }

        private void itemGuiMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var maLH = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");

            //using (var db = new MasterDataContext())
            //{
            //    var objLH = db.LichHens.SingleOrDefault(o => o.MaLH == maLH);

            //    if (objLH == null)
            //    {
            //        DialogBox.Error("Vui lòng chọn lịch hẹn");
            //        return;
            //    }

            //    var CcNhanViens = new List<string>();
            //    CcNhanViens.Add(objLH.MaNVTiep.ToString());
            //    CcNhanViens.Add(objLH.MaNVHoTro.ToString());

            //    var objKH = db.tnKhachHangs.Single(o => o.MaKH == objLH.MaKH);

            //    List<Mail.MailClient.EmailCls> Emails = new List<Mail.MailClient.EmailCls>();

            //    Emails.Add(new Library.Mail.MailClient.EmailCls() { MaKH = objKH.MaKH, Email = objKH.Email });

            //    using (var frm = new Library.Email.frmSend("Building.WorkSchedule.LichHen", 248, objLH.MaTN.Value, Emails))
            //    {
            //        frm.LinkID = objLH.MaLH;
            //        frm.CcNhanViens = CcNhanViens;
            //        frm.ShowDialog();
            //    }
            //}
        }

        private void gcScheduler_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
        }

        public void itemViewLichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");

            using (var frm = new AddNew_frm() {IsView = true })
            {
                frm.MaLH = id;
                if (frm.ShowDialog() == DialogResult.OK)
                    this._Load();
            }
        }
	}
}
