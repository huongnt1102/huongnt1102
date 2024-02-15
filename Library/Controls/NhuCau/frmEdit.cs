using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using DevExpress.XtraEditors;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using Library.Utilities;

namespace Library.Controls.NhuCau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }

        public int? ID { get; set; }

        public int? MaCD { get; set; }

        public int? MaNVU { get; set; }

        public int? MaKH { get; set; }

        public bool IsXuLy { get; set; }

        MasterDataContext db;

        public ncNhuCau objNC;

        byte SDBID;

        void CoHoiLoad()
        {
            db = new MasterDataContext();

            objNC = db.ncNhuCaus.Single(p => p.MaNC == this.ID.Value);

            txtSoCH.EditValue = objNC.SoNC;

            glKhachHang.EditValue = objNC.MaKH; 
      
            lkTrangThai.EditValue = objNC.MaTT;

            spTiemNang.EditValue = objNC.TiemNang;

            txtDienGiai.EditValue = objNC.DienGiai;

            this.MaCD = objNC.MaCD;

            string nv = "";

            foreach (var i in objNC.ncNhanVienHoTros)
                nv += i.MaNV + ", ";
            nv = nv.TrimEnd(' ').TrimEnd(',');
            cmbNhanVien.SetEditValue(nv);
        }


        void CoHoiAddNew()
        {
            db = new MasterDataContext();
            objNC = new ncNhuCau();
            objNC.MaTN = this.MaTN;
            txtSoCH.EditValue = Library.Utilities.NhuCauCls.TaoSoCT(this.MaTN);
            glKhachHang.EditValue = this.MaKH;
            //lkTrangThai.EditValue = 1;#L
            lkTrangThai.EditValue = 2;
            txtDienGiai.EditValue = null;
            cmbNhanVien.SetEditValue(null);
            lkTrangThai.Properties.ReadOnly = true;
        }

        void CoHoiSave()
        {
            if (txtSoCH.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập <Ký hiệu>, xin cảm ơn.");
                txtSoCH.Focus();
                return;
            }

            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn <Khách hàng>, xin cảm ơn.");
                glKhachHang.Focus();
                return;
            }

            if (lkTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn <Trạng thái>, xin cảm ơn.");
                lkTrangThai.Focus();
                return;
            }

            if (objNC.MaNC <= 0 && NhuCauCls.checkHopLe((int?)glKhachHang.EditValue))
            {
                DialogBox.Error("Khách hàng này có cơ hội chưa xử lý xong. Không thể lưu");
                return;
            }

            grvSanPham.RefreshData();

            objNC.MaKH = (int?)glKhachHang.EditValue;
            objNC.SoNC = txtSoCH.Text;
            objNC.MaKH = (int)glKhachHang.EditValue;                    
            objNC.TiemNang = Convert.ToByte(spTiemNang.EditValue);
            objNC.MaTT = (byte)lkTrangThai.EditValue;
            objNC.DienGiai = txtDienGiai.Text;
            objNC.NgaySua = DateTime.Now;

            string[] nv = cmbNhanVien.EditValue != null ? cmbNhanVien.EditValue.ToString().Split(',') : null;
            
            if (objNC.MaNC == 0)
            {
                objNC.MaNVQL = Common.User.MaNV;
                objNC.NgayNhap = DateTime.Now;
                objNC.MaNVN = Common.User.MaNV;
                db.ncNhuCaus.InsertOnSubmit(objNC);

                var objKH = db.tnKhachHangs.Single(o => o.MaKH == objNC.MaKH);
                objKH.ncNhuCau = objNC;
                objKH.XuLy_NgayXuLy = DateTime.UtcNow.AddHours(7);
            }
            else
            {
                objNC.NgaySua = DateTime.Now;

                objNC.MaNVS = Common.User.MaNV;
                if (nv != null)
                {
                    foreach (var i in objNC.ncNhanVienHoTros)
                    {
                        if (nv.Where(p => p == i.MaNV.ToString()).Count() <= 0)
                        {
                            db.ncNhanVienHoTros.DeleteOnSubmit(i);
                        }
                    }
                }
            }

            if (nv[0] != "")
            {
                foreach (var i in nv)
                {
                    if (objNC.ncNhanVienHoTros.Where(p => p.MaNV.ToString() == i).Count() <= 0)
                    {
                        var objNV = new ncNhanVienHoTro();
                        objNV.MaNV = int.Parse(i);
                        objNC.ncNhanVienHoTros.Add(objNV);
                    }
                }
            }

            db.SubmitChanges();

            NhuCauCls.SetTenCoHoi(objNC.MaNC);
            NhuCauCls.TinhTiemNang(objNC.MaNC);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void TinhThanhTien(GridView gv)
        {
            var sl = (decimal?)gv.GetFocusedRowCellValue("SoLuong") ?? 0;
            var dg = (decimal?)gv.GetFocusedRowCellValue("DonGia") ?? 0;
            var thue = (decimal?)gv.GetFocusedRowCellValue("ThueGTGT") ?? 0;
            var tyleCK = (decimal?)gv.GetFocusedRowCellValue("TyLeCK") ?? 0;
            var tienCK = (decimal?)gv.GetFocusedRowCellValue("TienCK") ?? 0;
            var tt = sl * dg;
            if (tyleCK > 0)
            {
                tienCK = tt * tyleCK;
            }
            else if (tienCK > 0)
            {
                tyleCK = tienCK / tt;
            }
            var tienThue = tt * thue;
            var tongtien = tt - tienCK + tienThue;
            gv.SetFocusedRowCellValue("ThanhTien", tt);
            gv.SetFocusedRowCellValue("TyLeCK", tyleCK);
            gv.SetFocusedRowCellValue("TienCK", tienCK);
            gv.SetFocusedRowCellValue("TienGTGT", tienThue);
            gv.SetFocusedRowCellValue("SoTien", tongtien);
        }

        public frmEdit(bool isView)
        {
            InitializeComponent();
            itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSave_ItemClick);
            itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemClose_ItemClick);

            spinDonGia.EditValueChanged += new EventHandler(spinDonGia_EditValueChanged);
            spinSoLuong.EditValueChanged += new EventHandler(spinSoLuong_EditValueChanged);
            spinThue.EditValueChanged += new EventHandler(spinThue_EditValueChanged);
            spinTyLeCK.EditValueChanged += new EventHandler(spinTyLeCK_EditValueChanged);
            spinTienCK.EditValueChanged += new EventHandler(spinTienCK_EditValueChanged);
            grvSanPham.KeyUp += new KeyEventHandler(grvSanPham_KeyUp);

            if (isView)
                itemSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        void ctlSanPhamItemEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch { }
        }

        void grvSanPham_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grvSanPham.DeleteSelectedRows();
            }
        }

        void spinThue_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("ThueGTGT", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void spinSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("SoLuong", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void spinDonGia_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("DonGia", ((SpinEdit)sender).Value);
            TinhThanhTien(grvSanPham);
        }

        void spinTyLeCK_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("TyLeCK", ((SpinEdit)sender).Value);
            grvSanPham.SetFocusedRowCellValue("TienCK", 0);
            TinhThanhTien(grvSanPham);
        }

        void spinTienCK_EditValueChanged(object sender, EventArgs e)
        {
            grvSanPham.SetFocusedRowCellValue("TienCK", ((SpinEdit)sender).Value);
            grvSanPham.SetFocusedRowCellValue("TyLeCK", 0);
            TinhThanhTien(grvSanPham);
        }

        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void frmEdit_Load(object sender, EventArgs e)
        {
            if (this.IsXuLy)
                this.Text = "Xử lý";

            db = new MasterDataContext();

            lkTrangThai.Properties.DataSource = db.ncTrangThais.OrderBy(p => p.STT).Select(p => new { p.MaTT, p.TenTT, p.TiemNang });

            lookDVT.DataSource = db.DonViTinhs.Select(p => new { MaDVT = p.ID, p.TenDVT });

            glkNhuCauThue.DataSource = db.NhuCauThues.OrderBy(p => p.STT).Select(p => new { p.ID, p.TenNhuCau });

            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.IsCSKH.GetValueOrDefault() | !kh.IsRoot.GetValueOrDefault()
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                 });

            glkToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });

            cmbNhanVien.Properties.DataSource = db.tnNhanViens.Select(o => new {o.MaNV, HoTenNV = string.Format("{0} - {1}",o.MaSoNV, o.HoTenNV) }).ToList();

            if (this.ID != null)
            {
                CoHoiLoad();
            }
            else
            {
                CoHoiAddNew();
            }

            gcSanPham.DataSource = objNC.ncSanPhams;
        }
       
        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CoHoiSave();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CoHoiAddNew();
        }

        private void lkTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            spTiemNang.EditValue = lkTrangThai.GetColumnValue("TiemNang");
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var phong = grvSanPham.GetFocusedRowCellValue("MaMB");
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    if(phong != null)
                        gcSanPham.DataSource = objNC.ncSanPhams;
                    else
                        gcSanPham.DataSource = null;
                    break;
            }
        }
    }
}
