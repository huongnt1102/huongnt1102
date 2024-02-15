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
using DevExpress.XtraEditors.Controls;

namespace DichVu.ChoThue
{
    public partial class frmChuyenKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public mbMatBang objmb;
        public mbMatBang_ChiaLo objLoCon;
        public tnKhachHang objkhachHangSource;
        public tnKhachHang objkhachHangDestination;

        MasterDataContext db;
        public frmChuyenKhachHang()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmChuyenTaiSan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lookKhachHang.Properties.NullText = "Chọn khách hàng";
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookKhachHang.Properties.DataSource = db.tnKhachHangs
                    .Select(p => new
                    {
                        p.MaKH,
                        KhachHang = p.IsCaNhan.Value ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                        p.GioiTinh,
                        DiaChi =p.DCLL,
                        DienThoai = p.IsCaNhan.Value ? p.DienThoaiKH : String.Format("{0} / {1}", p.DienThoaiKH, p.CtyFax)
                    });
            }
            else
            {
                lookKhachHang.Properties.DataSource = db.tnKhachHangs.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new
                    {
                        p.MaKH,
                        KhachHang = p.IsCaNhan.Value ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                        p.GioiTinh,
                        DiaChi = p.DCLL,
                        DienThoai = p.IsCaNhan.Value ? p.DienThoaiKH : String.Format("{0} / {1}", p.DienThoaiKH, p.CtyFax)
                    });
            }
            lookKhachHang.Properties.ValueMember = "MaKH";
            lookKhachHang.Properties.DisplayMember = "KhachHang";
            lookKhachHang.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookKhachHang.Properties.SearchMode = SearchMode.AutoComplete;
            lookKhachHang.Properties.DropDownRows = 10;
            LookUpColumnInfoCollection col = lookKhachHang.Properties.Columns;
            col.Add(new LookUpColumnInfo("KhachHang", "Họ tên",50));
            col.Add(new LookUpColumnInfo("GioiTinh", "Giới tính", 20));
            col.Add(new LookUpColumnInfo("DienThoai", "Điện thoại", 50));
            col.Add(new LookUpColumnInfo("DiaChi", "Địa chỉ", 120));

            if (objmb != null)
            {
                //txtGiaChoThue.Text = objmb.ThanhTien.ToString();
                if (objmb.MaLMB != null) txtLoaiMatBang.Text = objmb.mbLoaiMatBang.TenLMB;
                txtKyHieu.Text = objmb.MaSoMB;
                ///txtDonGia.Text = objmb.DonGia.ToString();

                if (objmb.MaTL != null)
                {
                    txtToaNha.Text = objmb.mbTangLau.mbKhoiNha.tnToaNha.TenTN;
                    txtTangLau.Text = objmb.mbTangLau.TenTL;
                    txtKhoiNha.Text = objmb.mbTangLau.mbKhoiNha.TenKN;
                }
                txtDienTich.Text = string.Format("{0} m2", objmb.DienTich);
            }
            if (objLoCon != null)
            {
                txtGiaChoThue.Text = objLoCon.GiaThue.ToString();
                if (objLoCon.mbMatBang.MaLMB != null) txtLoaiMatBang.Text = objLoCon.mbMatBang.mbLoaiMatBang.TenLMB;
                txtKyHieu.Text = objLoCon.TenLo;

                if (objLoCon.mbMatBang.MaTL != null)
                {
                    txtToaNha.Text = objLoCon.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.TenTN;
                    txtTangLau.Text = objLoCon.mbMatBang.mbTangLau.TenTL;
                    txtKhoiNha.Text = objLoCon.mbMatBang.mbTangLau.mbKhoiNha.TenKN;
                }
                txtDienTich.Text = string.Format("{0} m2", objLoCon.DienTich);
            }
            if (objkhachHangSource != null)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    vgkhSource.DataSource = db.tnKhachHangs.Where(p => p.MaKH == objkhachHangSource.MaKH)
                        .Select(p => new
                        {
                            KhachHang = p.IsCaNhan.Value ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                            GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                            DiaChi = p.DCLL,
                            DienThoai = p.IsCaNhan.Value ? p.DienThoaiKH : String.Format("{0} / {1}", p.DienThoaiKH, p.CtyFax),
                            p.NgaySinh,
                            p.EmailKH,
                            p.DCTT
                        });
                }
            }
            else
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var makh = db.mbMatBangs.Single(p => p.MaMB == objmb.MaMB).MaKH;
                    if (makh != null)
                    {
                        vgkhSource.DataSource = db.tnKhachHangs.Where(p => p.MaKH == makh)
                            .Select(p => new
                            {
                                KhachHang = p.IsCaNhan.Value ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                                GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                                DiaChi = p.DCLL,
                                DienThoai = p.IsCaNhan.Value ? p.DienThoaiKH : String.Format("{0} / {1}", p.DienThoaiKH, p.CtyFax),
                                p.NgaySinh,
                                p.EmailKH,
                                p.DCTT
                            });
                    }
                }
            }

            if (objkhachHangDestination !=null)
            {
                lookKhachHang.EditValue = objkhachHangDestination.MaKH;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            db.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lookKhachHang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng muốn chuyển quyền sử dụng");
                return;
            }
            var wait = DialogBox.WaitingForm();

            if (objmb != null)
            {
                objmb = db.mbMatBangs.Single(p => p.MaMB == objmb.MaMB);
                objmb.MaKH = (int)lookKhachHang.EditValue;
                //objmb.NgayThayDoiTT = db.GetSystemDate();

                mbLichSuSuDung objtssd = new mbLichSuSuDung()
                {
                    DienGiai = txtDienGiai.Text.Trim(),
                    MaKH = (int)lookKhachHang.EditValue,
                    MaMB = objmb.MaMB,
                    MaTT = objmb.MaTT,
                    NgayBatDau = db.GetSystemDate()
                };

                db.mbLichSuSuDungs.InsertOnSubmit(objtssd);
            }

            if (objLoCon != null)
            {
                objLoCon = db.mbMatBang_ChiaLos.Single(p => p.MaLo == objLoCon.MaLo);
                objLoCon.MaKH = (int)lookKhachHang.EditValue;

                mbLichSuSuDung objtssd = new mbLichSuSuDung()
                {
                    DienGiai = txtDienGiai.Text.Trim(),
                    MaKH = (int)lookKhachHang.EditValue,
                    MaLo = objLoCon.MaLo,
                    MaTT = objLoCon.MaTT,
                    NgayBatDau = db.GetSystemDate()
                };

                db.mbLichSuSuDungs.InsertOnSubmit(objtssd);
            }
            try
            {
                if (objmb != null)
                {
                    var maxls = db.mbLichSuSuDungs.Where(p => p.MaMB == objmb.MaMB).Max(p => p.NgayBatDau);
                    var lssdsource = db.mbLichSuSuDungs.Single(p => p.MaMB == objmb.MaMB
                        & p.NgayBatDau == maxls);

                    lssdsource.NgayKetThuc = db.GetSystemDate();
                }
                if (objLoCon != null)
                {
                    var maxls = db.mbLichSuSuDungs.Where(p => p.MaLo == objLoCon.MaLo).Max(p => p.NgayBatDau);
                    var lssdsource = db.mbLichSuSuDungs.Single(p => p.MaLo == objLoCon.MaLo
                        & p.NgayBatDau == maxls);

                    lssdsource.NgayKetThuc = db.GetSystemDate();
                }
            }
            catch { }

            

            try
            {
                db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Lưu không thành công");
            }
            finally
            {
                this.Close();
            }
        }

        private void lookKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                vgcmbDestination.DataSource = db.tnKhachHangs.Where(p => p.MaKH == (int)lookKhachHang.EditValue)
                    .Select(p => new
                    {
                        KhachHang = p.IsCaNhan.Value ? String.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                        GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                        DiaChi = p.DCLL,
                        DienThoai = p.IsCaNhan.Value ? p.DienThoaiKH : String.Format("{0} / {1}", p.DienThoaiKH, p.CtyFax),
                        p.NgaySinh,
                        p.EmailKH,
                        p.DCTT
                    });
            }
        }
    }
}