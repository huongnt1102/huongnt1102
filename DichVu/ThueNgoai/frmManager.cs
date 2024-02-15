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

namespace DichVu.ThueNgoai
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;
        public frmManager()
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
                    int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                    gcHopDong.DataSource = db.hdtnHopDongs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayKy.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayKy.Value, denNgay) >= 0 &
                                p.MaTN == maTN)
                        .OrderByDescending(p => p.NgayKT)//.AsEnumerable()
                        .Select(p => new
                        {
                           // STT = index + 1,
                            p.MaHD,
                            p.TenHD,
                            p.SoHD,
                            p.NgayKy,
                            p.NgayBD,
                            p.NgayKT,
                            TenTT = p.MaTT == null ? "" : p.hdtnTrangThai.TenTT,
                             p.hdtnTrangThai.MauNen,
                            p.GiaTri,
                            p.TienCoc,
                            TenTG = p.MaTG == null ? "" : p.tnTyGia.TenVT,
                            TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.MaNCC,
                            p.MaTT,
                            SoTien = p.hdtnCongNos.Sum(cn => cn.SoTien) ?? 0,
                            DaThu = p.hdtnCongNos.Sum(cn => cn.DaThanhToan),
                            ConLai = (p.GiaTri - p.hdtnCongNos.Sum(cn => cn.DaThanhToan)) ?? 0
                        }).ToList();

                }
                else
                {
                    gcHopDong.DataSource = null;
                }
            }
            catch { gcHopDong.DataSource = null; }
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

        private void LoadLSCN()
        {
            gcLSTT.DataSource = db.hdtnCongNos.Where(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"))
                    .Select(p => new
                    {
                        p.MaCongNo,
                        p.SoHD,
                        p.DaThanhToan,
                        p.ConLai,
                        p.DienGiai,
                        p.NgayThanhToan,
                        p.SoTien
                    })
                    .OrderBy(p => p.NgayThanhToan);
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
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Hợp đồng], xin cảm ơn.");  
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objHD = db.hdtnHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void DeleteSelected()
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Hợp đồng] cần xóa");  
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            try
            {
                var objHD = db.hdtnHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
                db.hdtnHopDongs.DeleteOnSubmit(objHD);
                db.SubmitChanges();

                grvHopDong.DeleteSelectedRows();
            }
            catch { }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSelected();
        }

        private void grvHopDong_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Delete)
                DeleteSelected();
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

        private void grvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        void Clicks()
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                gcNCC.DataSource = null;
                gcLSTT.DataSource = null;
                gcHistory.DataSource = null;
                return;
            };

            db = new MasterDataContext();
            switch (xtraTabControlThueNgoai.SelectedTabPageIndex)
            {
                case 0:
                    gcNCC.DataSource = db.tnNhaCungCaps
                        .Where(p => p.MaNCC == (int)grvHopDong.GetFocusedRowCellValue("MaNCC"))
                        .Select(p => new
                        {
                            p.TenNCC,
                            p.TenVT,
                            p.DienThoai,
                            p.Fax,
                            p.DiaChi,
                            p.Email,
                            p.WebSite
                        }).ToList();
                    break;
                case 1:
                    LoadLSCN();
                    break;
                case 2:
                    gcHistory.DataSource = db.hdtnLichSu_getByMaHD((int)grvHopDong.GetFocusedRowCellValue("MaHD"));
                    break;
            }
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Hợp đồng], xin cảm ơn.");  
                return;
            }

            using (var frm = new frmPaid())
            {
                frm.objnhanvien = objnhanvien;
                frm.MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");  
                frm.MaNCC = (int)grvHopDong.GetFocusedRowCellValue("MaNCC");  
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadLSCN();
        }

        private void btnXoaLSTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var objlstt = db.hdtnCongNos.Single(p => p.MaCongNo == (int)grvLSTT.GetFocusedRowCellValue("MaCongNo"));
            db.hdtnCongNos.DeleteOnSubmit(objlstt);
            try
            {
                db.SubmitChanges();
                grvLSTT.DeleteSelectedRows();
                LoadLSCN();
            }
            catch
            {
            }
        }

        private void btnSuaLSTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmCongNohdtn frm = new frmCongNohdtn())
            {
                frm.objcn = db.hdtnCongNos.Single(p => p.MaCongNo == (int)grvLSTT.GetFocusedRowCellValue("MaCongNo"));
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadLSCN();
                }
            }
        }

        private void btnDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");  
                return;
            }

            if (DialogBox.Question("Bạn có chắn chắn muốn duyệt [Hợp đồng] này không?") == System.Windows.Forms.DialogResult.No) return;
            var wait = DialogBox.WaitingForm();

            try
            {
                int mahd = (int)grvHopDong.GetFocusedRowCellValue("MaHD");  
                int? maTT = (int?)grvHopDong.GetFocusedRowCellValue("MaTT");  
                var objhd = db.hdtnHopDongs.Single(p => p.MaHD == mahd);
                if (objhd.MaTT == maTT & maTT == 1)
                {
                    objhd.hdtnTrangThai = db.hdtnTrangThais.Single(p => p.MaTT == 2);

                    var objLS = new hdtnLichSu();
                    objLS.ContractID = (int)grvHopDong.GetFocusedRowCellValue("MaHD");  
                    objLS.DateCreate = db.GetSystemDate();
                    objLS.Description = "Duyệt hợp đồng";
                    objLS.StaffID = objnhanvien.MaNV;
                    db.hdtnLichSus.InsertOnSubmit(objLS);

                    db.SubmitChanges();

                    LoadData();

                    DialogBox.Alert("Đã duyệt hợp đồng.");  
                }
                else
                {
                    DialogBox.Alert("Chỉ duyệt [Hợp đồng] ở tình trạng [Chờ duyệt].r\nVui lòng kiểm tra lại, xin cảm ơn.");  
                    grvHopDong.SetFocusedRowCellValue("MaTT", objhd.MaTT);
                }
            }
            catch {
                DialogBox.Alert("Đã xảy ra sự cố. Vui lòng kiểm tra lại, xin cảm ơn.");  
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void xtraTabControlThueNgoai_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemTrash_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");  
                return;
            }

            if (DialogBox.Question("Bạn có chắn chắn muốn hủy [Hợp đồng] này không?") == System.Windows.Forms.DialogResult.No) return;
            var wait = DialogBox.WaitingForm();

            try
            {
                int mahd = (int)grvHopDong.GetFocusedRowCellValue("MaHD");  
                var objhd = db.hdtnHopDongs.Single(p => p.MaHD == mahd);
                objhd.hdtnTrangThai = db.hdtnTrangThais.Single(p => p.MaTT == 5);

                var objLS = new hdtnLichSu();
                objLS.ContractID = (int)grvHopDong.GetFocusedRowCellValue("MaHD");  
                objLS.DateCreate = db.GetSystemDate();
                objLS.Description = "Hủy hợp đồng";
                objLS.StaffID = objnhanvien.MaNV;
                db.hdtnLichSus.InsertOnSubmit(objLS);

                db.SubmitChanges();

                LoadData();

                DialogBox.Alert("Đã hủy hợp đồng.");  
            }
            catch
            {
                DialogBox.Alert("Đã xảy ra sự cố. Vui lòng kiểm tra lại, xin cảm ơn.");  
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Hợp đồng], xin cảm ơn.");  
                return;
            }

            using (var frm = new frmProcess() { objnhanvien = objnhanvien, MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD") })
            {
                frm.MaTT = (int?)grvHopDong.GetFocusedRowCellValue("MaTT");  
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemEditChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaCN = (int)grvLSTT.GetFocusedRowCellValue("MaCongNo");  
            var frm = new frmDieuCinhCN() { objNV = objnhanvien, MaCN = MaCN };
            frm.ShowDialog();
        }
    }
}