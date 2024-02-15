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

namespace KyThuat.ThanhToanDichVu
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien { get; set; }
        MasterDataContext db;
        string sSoPhieu;
        bool first = true;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var sysdt = db.GetSystemDate();
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                    gcLich.DataSource = db.LichThanhToan_getAll(tuNgay, denNgay, maTN, 0);
                }
                catch { }
                finally
                {
                    wait.Close();
                    wait.Dispose();
                }
            }
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
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
            {
                cmbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
            GetPhieu();

            LoadData();
            first = false;
        }

        private void grvLich_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (KyThuat.ThanhToanDichVu.frmEdit_Dien frm = new KyThuat.ThanhToanDichVu.frmEdit_Dien())
            {
                frm.objnhanvien = objnhanvien;
                frm.ShowDialog();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvLich.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn lịch thanh toán muốn sửa");
                return;
            }
            int MaLich = (int)grvLich.GetFocusedRowCellValue("LichID");
            var objlich = db.LichThanhToans.Single(p=>p.LichID == MaLich);
            using (KyThuat.ThanhToanDichVu.frmEdit_Dien frm = new KyThuat.ThanhToanDichVu.frmEdit_Dien() { objLichThanhToan = objlich })
            {
                frm.objnhanvien = objnhanvien;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvLich.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn lịch thanh toán muốn xóa");
                return;
            }
            int MaLich = (int)grvLich.GetFocusedRowCellValue("LichID");
            var objlich = db.LichThanhToans.Single(p => p.LichID == MaLich);

            db.LichThanhToans.DeleteOnSubmit(objlich);
            try
            {
                db.SubmitChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void GetPhieu()
        {
            DateTime now = db.GetSystemDate();
            db.btPhieuChi_getNewMaPhieuChi(ref sSoPhieu);
            sSoPhieu = db.DinhDang(9, int.Parse(sSoPhieu));
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvLich.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhà cung cấp], xin cảm ơn.");
                return;
            }

            using (var frm = new frmPaid())
            {
                frm.objnhanvien = objnhanvien;
                frm.MaHD = (int)grvLich.GetFocusedRowCellValue("LichID");
                frm.MaNCC = (int)grvLich.GetFocusedRowCellValue("MaNCC");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }

            //int MaLich = (int)grvChiTiet.GetFocusedRowCellValue("ID");
            //var objlich = db.LichThanhToan_ChiTiets.Single(p => p.ID == MaLich);
            //if (objlich.DaTT.Value)
            //{
            //    DialogBox.Alert("Lần thanh toán " + objlich.ThoiGian.Value.ToShortDateString() + " đã thanh toán rồi");
            //    return;
            //}
            //if (DialogBox.Question("Bạn có chắc muốn thanh toán tháng " + objlich.LichThanhToan.ThangThanhToan.Value.ToString("MM/yyyy") + " lần thanh toán ngày " + objlich.ThoiGian.Value.ToShortDateString()) != System.Windows.Forms.DialogResult.Yes)
            //{
            //    return;
            //}

            //objlich.DaTT = true;

            //#region Phieu chi
            //PhieuChi objphieuchi = new PhieuChi()
            //{
            //    DichVu = 10,
            //    DienGiai = "Thanh toán tiền điện với nhà cung cấp tháng " + objlich.LichThanhToan.ThangThanhToan.Value.ToShortDateString() + " đợt " + objlich.ThoiGian.Value.ToShortDateString(),
            //    DotThanhToan = objlich.LichThanhToan.ThangThanhToan.Value.ToShortDateString(),
            //    MaHopDong = objlich.LichThanhToan.LichID.ToString(),
            //    MaNV = objnhanvien.MaNV,
            //    NgayChi = db.GetSystemDate(),
            //    SoTienThanhToan = objlich.SoTien,
            //    SoPhieu = sSoPhieu
            //};

            //db.PhieuChis.InsertOnSubmit(objphieuchi);
            //#endregion

            //try
            //{
            //    db.SubmitChanges();

            //    #region In phieu chi
            //    if (DialogBox.Question("Bạn có muốn in phiếu chi không?") == DialogResult.Yes)
            //    {
            //        using (ReportMisc.DichVu.Quy.rptPhieuChi rpt = new ReportMisc.DichVu.Quy.rptPhieuChi(objphieuchi))
            //        {
            //            rpt.ShowPreviewDialog();
            //        }
            //    }
            //    #endregion

            //    grvLich_FocusedColumnChanged(null, null);
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void grvLich_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvLich.FocusedRowHandle >= 0)
            {
                int MaLich = (int)grvLich.GetFocusedRowCellValue(colLichID);
                gcChiTiet.DataSource = db.LichThanhToan_ChiTiets.Where(p => p.LichID == MaLich)
                    .Select(p => new
                    {
                        p.DaTT,
                        p.DienGiai,
                        p.ID,
                        p.LichID,
                        p.SoTien,
                        p.ThoiGian,
                        DaTra = p.DaTra ?? 0,
                        ConLai = p.SoTien - (p.DaTra ?? 0)
                    });
            }
            else
                gcChiTiet.DataSource = null;
        }
    }
}