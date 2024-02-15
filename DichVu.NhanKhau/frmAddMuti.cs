using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Library;

namespace DichVu.NhanKhau
{
    public partial class frmAddMuti : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        
        MasterDataContext db = new MasterDataContext();
        public frmAddMuti()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void frmAddMuti_Load(object sender, EventArgs e)
        {

            lookQT.DataSource = db.QuocTiches;
            lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == Common.User.MaTN);
            lkMatBang.DataSource = db.mbMatBangs.ToList();
            var ltMatBang = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaTN == MaTN
                             select new
                             {
                                 mb.MaMB,
                                 mb.MaSoMB,
                                 tl.TenTL,
                                 kn.TenKN,
                                 kh.MaKH,
                                 kh.KyHieu,
                                 TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                             }).ToList();
            lookMatBang.Properties.DataSource = ltMatBang;
            lkTT.DataSource = db.tnNhanKhauTrangThais;
            lkQuanHe.DataSource = db.tnQuanHes;
            dateNgayDK.Text = "";
            lookNhanVien.EditValue = Common.User.MaNV;
            List<GioiTinh> ltGT = new List<GioiTinh>();
            ltGT.Add(new GioiTinh { Name = "Nam", Value = true });
            ltGT.Add(new GioiTinh { Name = "Nữ", Value = false });
            lkGioiTinh.DataSource = ltGT.ToList();

        }
       
        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            if (lookMatBang.EditValue == null) return;
            var _MaMB = (int)lookMatBang.EditValue;
            txtLoaiCH.Text = db.mbMatBangs.FirstOrDefault(p => p.MaMB == _MaMB).mbLoaiMatBang.TenLMB;
            //lookChuHo.Properties.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == _MaMB).Select(p => new { p.ID, p.HoTenNK });
            //lookChuHo.ItemIndex = 0;

            gcCuDan.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == (int?)lookMatBang.EditValue);
            try
            {
                var objMB = (from p in db.mbMatBangs
                             where p.MaMB == _MaMB
                             select new
                             {
                                 TenKH = p.tnKhachHang == null ? "" : ((bool)p.tnKhachHang.IsCaNhan ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen),
                             }).FirstOrDefault();
                txtChuHo.Text = objMB.TenKH;
            }
            catch { }
        }


        private void grvCuDan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void txtHoTen_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit) sender;
            if (value.Text != "")
            {
                grvCuDan.SetFocusedRowCellValue("MaMB", lookMatBang.EditValue);
                grvCuDan.SetFocusedRowCellValue("MaKH", lookMatBang.Properties.View.GetFocusedRowCellValue("MaKH"));
                grvCuDan.SetFocusedRowCellValue("NgayCap","");

                grvCuDan.SetFocusedRowCellValue( "NgaySinh","");

            
                grvCuDan.SetFocusedRowCellValue("SoThiThucVISA","");
                grvCuDan.SetFocusedRowCellValue("TonGiao","");
                grvCuDan.SetFocusedRowCellValue("DanToc", "");
                grvCuDan.SetFocusedRowCellValue("NoiLamViec","");
                grvCuDan.SetFocusedRowCellValue("NgheNghiep", "");
                grvCuDan.SetFocusedRowCellValue("NoiCap", "");
                grvCuDan.SetFocusedRowCellValue("Email","");

                grvCuDan.SetFocusedRowCellValue("DienThoai","");




                grvCuDan.SetFocusedRowCellValue("NgayHetHanDKTT", "");
                grvCuDan.SetFocusedRowCellValue("NgaySinh", "");
                grvCuDan.SetFocusedRowCellValue("NgayCap", "");
                grvCuDan.SetFocusedRowCellValue("HanThiThucVISAThangLong", "");


                grvCuDan.SetFocusedRowCellValue("CMND","");
                grvCuDan.SetFocusedRowCellValue("HanThiThucVISA", "");
                grvCuDan.SetFocusedRowCellValue("GioiTinh",true);
                grvCuDan.SetFocusedRowCellValue("QuanHeID",13);
                grvCuDan.SetFocusedRowCellValue("MaQT", 1);
                grvCuDan.SetFocusedRowCellValue("MaTT", 1);

            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

            if (lookMatBang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                lookMatBang.Focus();
                return;
            }

            if (dateNgayDK.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày đăng ký");
                dateNgayDK.Focus();
                return;
            }

            //if (lookTrangThai.Text.Trim().Length == 0)
            //{
            //    DialogBox.Error("Vui lòng trạng thái");
            //    lookMatBang.Focus();
            //    return;
            //}

            grvCuDan.FocusedRowHandle = -1;
            for (int i = 0; i < grvCuDan.RowCount-1; i++)
            {
                if ((int)grvCuDan.GetRowCellValue(i, "ID") == 0 & (int?)grvCuDan.GetRowCellValue(i, "MaMB") != null)
                {
                    var db = new MasterDataContext();
                    var objNK = new tnNhanKhau();
                    objNK.NgayChuyenDi = dateNgayDi.Text;
                    objNK.NgayChuyenDen = dateNgayDen.Text;
                    objNK.MaMB = (int?)grvCuDan.GetRowCellValue(i, "MaMB");
                    objNK.MaKH = (int?)grvCuDan.GetRowCellValue(i, "MaKH");
                    objNK.MaNV = (int?)lookNhanVien.EditValue;
                    objNK.NgayDK = dateNgayDK.Text;
                    objNK.HoTenNK = grvCuDan.GetRowCellValue(i, "HoTenNK").ToString();
                    objNK.NgayCap = grvCuDan.GetRowCellValue(i, "NgayCap").ToString();

                    objNK.NgaySinh = grvCuDan.GetRowCellValue(i, "NgaySinh").ToString();
                    objNK.NgayHetHanDKTT =grvCuDan.GetRowCellValue(i, "NgayHetHanDKTT").ToString();
                    objNK.QuanHeID = (int?)grvCuDan.GetRowCellValue(i, "QuanHeID");
                    objNK.SoThiThucVISA = grvCuDan.GetRowCellValue(i, "SoThiThucVISA").ToString();
                    objNK.TonGiao = grvCuDan.GetRowCellValue(i, "TonGiao").ToString();
                    objNK.MaQT = (int?)grvCuDan.GetRowCellValue(i, "MaQT");
                    objNK.NoiLamViec = grvCuDan.GetRowCellValue(i, "NoiLamViec").ToString();
                    //objNK.ParentID = (int?)lookChuHo.EditValue;
                    objNK.NgheNghiep = grvCuDan.GetRowCellValue(i, "NgheNghiep").ToString();
                    objNK.NoiCap = grvCuDan.GetRowCellValue(i, "NoiCap").ToString();
                    objNK.HanThiThucVISAThangLong = grvCuDan.GetRowCellValue(i, "HanThiThucVISAThangLong").ToString();
                    //objNK.DangKyDinhMuc = ckDinhMuc.Checked;
                    objNK.DaDKTT = ckbDaDKTT.Checked;
                    objNK.MaTT = (int?)grvCuDan.GetRowCellValue(i, "MaTT");
                    objNK.Email = grvCuDan.GetRowCellValue(i, "Email").ToString();
                    objNK.DienGiai = txtDienGiai.Text;
                    objNK.DienThoai = grvCuDan.GetRowCellValue(i, "DienThoai").ToString();
                    objNK.GioiTinh = (bool)grvCuDan.GetRowCellValue(i, "GioiTinh");
                    objNK.CMND = grvCuDan.GetRowCellValue(i, "CMND").ToString();
                    objNK.DanToc = grvCuDan.GetRowCellValue(i, "DanToc").ToString();
                    db.tnNhanKhaus.InsertOnSubmit(objNK);
                    db.SubmitChanges();
                    
                }
               
            }
            DialogBox.Alert("Dữ liệu đã được cập nhật");
            //AddNew();
            this.Close();
        }

        private void chkGioiTinh_EditValueChanged(object sender, EventArgs e)
        {
            var value = (CheckEdit)sender;
            if (value.EditValue != null)
            {
                grvCuDan.SetFocusedRowCellValue("GioiTinh", value.EditValue);
                
            }
        }

        private void dateHanThiThuc_EditValueChanged(object sender, EventArgs e)
        {
            var value = (DateEdit)sender;
            if (value.EditValue != null)
            {
                grvCuDan.SetFocusedRowCellValue("HanThiThucVISA", value.EditValue);

            }
        }

        private void txtNgayCap_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;
            if (value.EditValue == null)
            {
                grvCuDan.SetFocusedRowCellValue("NgayCap", "");
                
            }
                grvCuDan.SetFocusedRowCellValue("NgayCap", value.Text);

        }

        private void txtNgaySinh_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("NgaySinh", value.Text);
        }

        private void txtSoCMND_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("CMND", value.Text);
        }

        private void txtNoiCap_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("NoiCap", value.Text);
        }

        private void txtSoThiThucVISA_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("SoThiThucVISA", value.Text);
        }

        private void txtDienThoai_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("DienThoai", value.Text);
        }

        private void txtEmail_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("Email", value.Text);
        }

        private void txtTonGiao_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("TonGiao", value.Text);
        }

        private void txtNoiLamViec_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("NoiLamViec", value.Text);
        }

        private void txtNgheNghiep_EditValueChanged(object sender, EventArgs e)
        {
            var value = (TextEdit)sender;

            grvCuDan.SetFocusedRowCellValue("NgheNghiep", value.Text);
        }

        private void grvCuDan_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            
        }

        void AddNew()
        {
            var db = new MasterDataContext();
            
            lookNhanVien.Properties.DataSource = null;
            lookMatBang.Properties.DataSource = null;
            lookMatBang.Text=null;
            dateNgayDen.EditValue = null;
            dateNgayDi.EditValue = null;
            lookNhanVien.Properties.DataSource = null;
            lkTT.DataSource = null;
            lkQuanHe.DataSource = null;
            dateNgayDK.Text = "";
            lookNhanVien.EditValue = Common.User.MaNV;
            gcCuDan.DataSource = null;
            
            
            txtDienGiai.Text = "";
            //lookChuHo.Properties.DataSource = null;
            //lookChuHo.Text = null;
            lookQT.DataSource = db.QuocTiches;
            lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == Common.User.MaTN);
            var ltMatBang = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaTN == MaTN
                             select new
                             {
                                 mb.MaMB,
                                 mb.MaSoMB,
                                 tl.TenTL,
                                 kn.TenKN,
                                 kh.MaKH,
                                 kh.KyHieu,
                                 TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                             }).ToList();
            
            lookMatBang.Properties.DataSource = ltMatBang;

            lkTT.DataSource = db.tnNhanKhauTrangThais;
            lkQuanHe.DataSource = db.tnQuanHes;
            dateNgayDK.Text = "";
            lookNhanVien.EditValue = Common.User.MaNV;

            
        }
            

        private void itemExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmAddMuti_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogBox.Question("Bạn có muốn thoát không") == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
        }

        private void txtQuocTich_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmQuocTich();
                frm.ShowDialog();
            }
        }

        private void lookTrangThai_EditValueChanged(object sender, EventArgs e)
        {

        }

        class GioiTinh
        {
            public string Name { get; set; }
            public bool Value { get; set; }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _maMB = (int?)lookMatBang.EditValue;
            if (_maMB == null) return;
            var rpt = new NhanKhau.rptMauDangKy(_maMB);
            rpt.ShowPreviewDialog();
        }
        
    }
}
