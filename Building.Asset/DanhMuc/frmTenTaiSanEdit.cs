using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class frmTenTaiSanEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db=new MasterDataContext();
        tbl_TenTaiSan CT_TS;
        public byte? MaTn;
        public int? Id;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmTenTaiSanEdit()
        {
            InitializeComponent();
        }

        private void frmTaiSanEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            CT_TS = new tbl_TenTaiSan();
            gridLKHeThongTS.Properties.DataSource = db.tbl_NhomTaiSans.Where(_=>_.MaTN==MaTn & _.IsSuDung==true);
            gridLKNCC.Properties.DataSource = db.tbl_NhaCungCapTaiSans;
            gridLKTinhTrangTS.Properties.DataSource = db.tbl_TinhTrangTaiSans;
            gridLKKhoiNha.Properties.DataSource = db.mbKhoiNhas;
            if (Id != null)
            {
                var objCT_TS = db.tbl_TenTaiSans.FirstOrDefault(o => o.ID == Id);
                objCT_TS.MaTN = MaTn;
                txtMaTenTaiSan.Text = objCT_TS.TenVietTat;
                txtTenTaiSan.Text = objCT_TS.TenTaiSan;
                gridLKTinhTrangTS.EditValue = objCT_TS.TinhTrangTaiSanID;             
                gridLKNCC.EditValue = objCT_TS.NhaCungCapID;
                txtThongSo.EditValue = objCT_TS.ThongSoKyThuat;
                txtMoTa.EditValue = objCT_TS.DienGiai;
                gridLKKhoiNha.EditValue = objCT_TS.BlockID;
                txtNguyenGia.EditValue = objCT_TS.NguyenGia;
                dateNgayMua.EditValue = objCT_TS.NgayMua;
                dateNgaySuDung.EditValue = objCT_TS.NgayDuaVaoSuDung;
                txtvitri.EditValue = objCT_TS.ViTri;
                txTonKho.EditValue = objCT_TS.TonKho;
                txtSoLuong.EditValue = objCT_TS.SoLuong;
                if (objCT_TS.QuanTrong == true)
                    checkeditQuanTrong.Checked = true;
                else
                    checkeditQuanTrong.Checked = false;

                if (objCT_TS.NgungSuDung == true)
                    checkNSD.Checked = true;
                else
                    checkNSD.Checked = false;
                dateNgayHetHanBH.EditValue = objCT_TS.NgayHetHanBaoHanh;
                txtThoigianBH.EditValue = objCT_TS.ThoiGianBaoHanh;

                gridLKLoaiTaiSan.Properties.DataSource = db.tbl_LoaiTaiSans;

                var objLTS = db.tbl_LoaiTaiSans.Where(o => o.ID == objCT_TS.LoaiTaiSanID).Select(o => o.NhomTaiSanID).First();
                gridLKLoaiTaiSan.EditValue = objLTS;

                var objNTS = db.tbl_NhomTaiSans.Where(o => o.ID == objLTS).Select(o => o.ID).First();
                gridLKHeThongTS.EditValue = objNTS;

            }

            itemHuongDan.Click += ItemHuongDan_Click;
            itemClearText.Click += ItemClearText_Click;
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Kiểm tra

                if (gridLKTinhTrangTS.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tình trạng tài sản");
                    gridLKTinhTrangTS.Focus();
                    return;
                }
                if (gridLKHeThongTS.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn hệ thống");
                    gridLKHeThongTS.Focus();
                    return;
                }
                if (gridLKLoaiTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn loại tài sản");
                    gridLKLoaiTaiSan.Focus();
                    return;
                }
                if (gridLKNCC.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhà cung cấp");
                    gridLKNCC.Focus();
                    return;
                }
              
                if (txtMaTenTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui nhập mã tên tài sản");
                    txtMaTenTaiSan.Focus();
                    return;
                }
                #endregion

                if (Id==null)
                {
                    #region Thêm mới chi tiết tài sản
                    CT_TS.MaTN = MaTn;
                    CT_TS.TenTaiSan = txtTenTaiSan.Text.Trim();
                    CT_TS.TenVietTat = txtMaTenTaiSan.Text.Trim();
                    CT_TS.LoaiTaiSanID = (int?)gridLKLoaiTaiSan.EditValue;
                    CT_TS.TinhTrangTaiSanID = (int?)gridLKTinhTrangTS.EditValue;
                    CT_TS.NhaCungCapID = (int?)gridLKNCC.EditValue;
                    CT_TS.ThongSoKyThuat = txtThongSo.EditValue as string;
                    CT_TS.DienGiai = txtMoTa.EditValue as string;
                    CT_TS.BlockID = (int?)gridLKKhoiNha.EditValue;
                    CT_TS.NguyenGia = (decimal?)txtNguyenGia.EditValue;
                    CT_TS.NgayMua = (DateTime?)dateNgayMua.EditValue;
                    CT_TS.NgayDuaVaoSuDung =(DateTime?) dateNgaySuDung.EditValue;
                    CT_TS.ViTri = txtvitri.EditValue as string;
                    CT_TS.TonKho =(double?)txTonKho.Value;
                    CT_TS.SoLuong = (double?)txtSoLuong.Value;
                    if (checkeditQuanTrong.Checked == true)
                        CT_TS.QuanTrong = true;
                    else
                        CT_TS.QuanTrong = false;

                    if (checkNSD.Checked == true)
                        CT_TS.NgungSuDung = true;
                    else
                        CT_TS.NgungSuDung = false;
                    CT_TS.NgayHetHanBaoHanh =(DateTime?) dateNgayHetHanBH.EditValue;
                    CT_TS.ThoiGianBaoHanh = (int?)txtThoigianBH.Value;
                    CT_TS.NgayNhap = DateTime.Now;
                    CT_TS.NguoiNhap = Common.User.MaNV;
                    db.tbl_TenTaiSans.InsertOnSubmit(CT_TS);
                    db.SubmitChanges();
                    #endregion
                }
                else
                {
                    #region Sửa công việc
                    var objCT_TS = db.tbl_TenTaiSans.FirstOrDefault(o => o.ID == Id);
                    objCT_TS.MaTN = MaTn;
                    objCT_TS.TenVietTat = txtMaTenTaiSan.Text.Trim();
                    objCT_TS.TenTaiSan = txtTenTaiSan.Text.Trim();
                    objCT_TS.LoaiTaiSanID =(int?) gridLKLoaiTaiSan.EditValue;
                    objCT_TS.TinhTrangTaiSanID = (int?)gridLKTinhTrangTS.EditValue;
                    objCT_TS.NhaCungCapID = (int?)gridLKNCC.EditValue;
                    objCT_TS.ThongSoKyThuat = txtThongSo.EditValue as string;
                    objCT_TS.DienGiai = txtMoTa.EditValue as string;
                    objCT_TS.BlockID = (int?)gridLKKhoiNha.EditValue;
                    objCT_TS.NguyenGia = (decimal?)txtNguyenGia.EditValue;
                    objCT_TS.NgayMua =(DateTime?) dateNgayMua.EditValue;
                    objCT_TS.NgayDuaVaoSuDung = (DateTime?)dateNgaySuDung.EditValue;
                    objCT_TS.ViTri = txtvitri.EditValue as string;
                    objCT_TS.TonKho = (double?)txTonKho.Value;
                    objCT_TS.SoLuong = (double?)txtSoLuong.Value;
                    if (checkeditQuanTrong.Checked == true)
                        objCT_TS.QuanTrong = true;
                    else
                        objCT_TS.QuanTrong = false;

                    if (checkNSD.Checked == true)
                        objCT_TS.NgungSuDung = true;
                    else
                        objCT_TS.NgungSuDung = false;
                    objCT_TS.NgayHetHanBaoHanh =(DateTime?) dateNgayHetHanBH.EditValue;
                    objCT_TS.ThoiGianBaoHanh = (int?)txtThoigianBH.Value;
                    objCT_TS.NgaySua = DateTime.Now;
                    objCT_TS.NguoiSua = Common.User.MaNV;
                    db.SubmitChanges();
                    #endregion
                }
                db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }

        private void gridLKHeThongTS_EditValueChanged(object sender, EventArgs e)
        {
            gridLKLoaiTaiSan.Properties.DataSource = db.tbl_LoaiTaiSans.Where(_ => _.NhomTaiSanID == (int?)gridLKHeThongTS.EditValue);
            gridLKLoaiTaiSan.EditValue = gridLKLoaiTaiSan.Properties.GetKeyValue(0);

        }

        private void itemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        
    }
}