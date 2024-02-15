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

namespace DichVu.ChoThue
{
    public partial class frmManagerCoc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;
        public frmManagerCoc()
        {
            InitializeComponent();
            db = new MasterDataContext();

            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : -1;
                    gcTienCoc.DataSource = db.thueLTTCocs.Where(p => (p.thueHopDong.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN | maTN == -1) & SqlMethods.DateDiffDay(p.HanTT, tuNgay) <= 0 & SqlMethods.DateDiffDay(p.HanTT, denNgay) >= 0)
                        .Select(p => new
                        {
                            p.GhiChu,
                            p.DaThu,
                            p.HanTT,
                            p.ID,
                            p.MaHD,
                            p.NgayThu,
                            p.SoTien,
                            p.thueHopDong.SoHD,
                            p.thueHopDong.NgayHD,
                            p.thueHopDong.mbMatBang.MaSoMB,
                            KhachHang = p.thueHopDong.tnKhachHang.IsCaNhan == true ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen
                        });

                }
                else
                {
                    gcTienCoc.DataSource = null;
                }
            }
            catch { gcTienCoc.DataSource = null; }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.DataSource = list;
                if (list.Count > 0)
                    itemToaNha.EditValue = list[0].MaTN;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.DataSource = list2;
                if (list2.Count > 0)
                    itemToaNha.EditValue = list2[0].MaTN;
            }

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
            first = false;
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(grvTienCoc.FocusedRowHandle<0)
            {
                DialogBox.Alert("Vui lòng chọn mục cần xóa!");
                return;
            }
            int ID = (int)grvTienCoc.GetFocusedRowCellValue("ID");
            var objDel = db.thueLTTCocs.FirstOrDefault(p => p.ID == ID);
            db.thueLTTCocs.DeleteOnSubmit(objDel);
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã xóa thành công!");
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Lỗi xảy ra: " + ex.Message);
            }
        }

        //private void grvHopDong_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RowHandle < 0) return;
        //        e.Appearance.BackColor = Color.FromArgb(int.Parse(grvHopDong.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
        //    }
        //    catch { }
        //}

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTienCoc.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn lịch thanh toán để tiến hành.");
                return;
            }
            int? MaDotTT = (int?)grvTienCoc.GetFocusedRowCellValue("ID");
            using (var frm = new frmThanhToanCoc() { objnhanvien = objnhanvien, MaDotTT = MaDotTT })
            {
                frm.ShowDialog();
                LoadData();
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcTienCoc);
        }

    }
}