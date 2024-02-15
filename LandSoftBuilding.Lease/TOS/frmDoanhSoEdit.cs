using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandSoftBuilding.Lease.TOS
{
    public partial class frmDoanhSoEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCT { get; set; }
        public byte? MaTN { get; set; }
        public frmDoanhSoEdit()
        {
            InitializeComponent();
        }

        private void frmDoanhSoEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            try
            {
                var db = new MasterDataContext();

                glkHopDong.Properties.DataSource = (from hd in db.ctHopDongs 
                                                    join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH 
                                                    where hd.MaTN == MaTN & hd.IsHopDongTOS.GetValueOrDefault() == true
                                                    select new { hd.SoHDCT, kh.KyHieu, hd.ID, TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen });

                glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                    where mb.MaTN == MaTN
                                                    select new { mb.MaSoMB, mb.MaMB });
                glkLoaiTien.Properties.DataSource = db.LoaiTiens;
                glkLoaiTien.EditValue = 1;
                spinTyGia.EditValue = 1;

                spinSales.EditValue = 0;
                dateTuNgay.DateTime = dateDenNgay.DateTime = dateNgayThanhToan.DateTime = DateTime.UtcNow.AddHours(7);


                if(MaCT != null)
                {
                    var ds = db.ctChiTiet_DoanhSos.FirstOrDefault(_ => _.ID == MaCT);
                    if (ds != null)
                    {
                        glkHopDong.EditValue = ds.MaHDCT;
                        glkMatBang.EditValue = ds.MaMB;
                        spinSales.EditValue = ds.Sales;
                        dateTuNgay.EditValue = ds.TuNgay;
                        dateDenNgay.EditValue = ds.DenNgay;
                        dateNgayThanhToan.EditValue = ds.NgayTT;
                        txtDienGiai.EditValue = ds.DienGiai;
                        glkLoaiTien.EditValue = ds.MaLoaiTien;
                        spinTyGia.EditValue = ds.TyGia;
                    }
                }
                
            }
            catch { }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (glkHopDong.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn hợp đồng!");
                    glkHopDong.Focus();
                    return;
                }
                if (glkMatBang.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn mặt bằng!");
                    glkMatBang.Focus();
                    return;
                }

                var model = new { MaHD = glkHopDong.EditValue, MaMB = glkMatBang.EditValue, DoanhThuThucTe = spinSales.EditValue, TuNgay = dateTuNgay.DateTime, DenNgay = dateDenNgay.DateTime, NgayTT = dateNgayThanhToan.DateTime, DienGiai = txtDienGiai.Text, LoaiTien = (int)glkLoaiTien.EditValue, TyGia = (decimal)spinTyGia.Value };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("lease_frmImport_ctChiTiet_UpdateDoanhSo", param);

                DialogBox.Alert("Dữ liệu đã được lưu!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex) { DialogBox.Error(ex.Message); }
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void glkHopDong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }
                var db = new Library.MasterDataContext();
                var list = (from mb in db.mbMatBangs
                            join ct in db.ctChiTiets on mb.MaMB equals ct.MaMB
                            where ct.MaHDCT == (int)item.EditValue
                            group new { mb } by new { mb.MaSoMB, mb.MaMB } into g
                            select new { g.Key.MaSoMB, g.Key.MaMB }).ToList();
                glkMatBang.Properties.DataSource = list;
                if (list.Count() > 0)
                {

                    glkMatBang.EditValue = list.First().MaMB;
                }
            }
            catch (System.Exception ex) { }
        }

        private void glkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spinTyGia.EditValue = glkLoaiTien.Properties.View.GetFocusedRowCellValue("TyGia");
            }
            catch { }
        }
    }
}