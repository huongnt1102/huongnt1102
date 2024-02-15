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
using DevExpress.XtraReports.UI;
//using ReportMisc.DichVu.Quy;

namespace DichVu.Quy
{
    public partial class frmPhieuChi : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;

        public frmPhieuChi()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmPhieuChi_Load(object sender, EventArgs e)
        {
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            LoadData();

            first = false;
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                gcPhieuChi.DataSource = db.PhieuChis
                    .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayChi.Value) >= 0
                        & SqlMethods.DateDiffDay(p.NgayChi.Value, denNgay) >= 0 &
                                p.MaTN == maTN)
                        .OrderByDescending(p => p.NgayChi)
                        .Select(p => new
                        {
                            p.MaPhieu,
                            p.SoPhieu,
                            p.NgayChi,
                            p.DotThanhToan,
                            p.SoTienThanhToan,
                            NhanVien = p.tnNhanVien.HoTenNV,
                            p.DienGiai,
                            p.NgayCN,
                            NhanVienCN = p.tnNhanVien1 == null ? "" : p.tnNhanVien1.HoTenNV,
                            p.mbMatBang.MaSoMB,
                            p.NguoiNhan,
                            p.DiaChi,
                            p.NgayTao
                        });
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

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.OK)
            {
                var phieu = db.PhieuChis.Single(p => p.MaPhieu == (int)grvPhieuChi.GetFocusedRowCellValue(colMaPhieu));
                db.PhieuChis.DeleteOnSubmit(phieu);

                try
                {
                    db.SubmitChanges();
                    grvPhieuChi.DeleteSelectedRows();
                    DialogBox.Alert("Xóa thành công");
                    LoadData();
                }
                catch { }
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
                LoadData();
            }
            catch { }
        }

        private void itemXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuChi.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu chi], xin cảm ơn.");
                return;
            }

            var objphieuchi = db.PhieuChis.Single(p=>p.MaPhieu==(int)grvPhieuChi.GetFocusedRowCellValue("MaPhieu"));
           // rptPhieuChi rpt = new rptPhieuChi(objphieuchi);
            //var rpt = new ReportMisc.DichVu.HoaDon.rptPhieuChi((int)grvPhieuChi.GetFocusedRowCellValue("MaPhieu"));
            //rpt.ShowPreviewDialog();
        }

        private void btnVietPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new PhieuChi.frmEdit() { objNV = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuChi.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu chi], xin cảm ơn.");
                return;
            }

            using (var frm = new PhieuChi.frmEdit() { objNV = objnhanvien })
            {
                frm.MaPC = (int?)grvPhieuChi.GetFocusedRowCellValue("MaPhieu");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuChi.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu chi], xin cảm ơn.");
                return;
            }

            //var rpt = new ReportMisc.DichVu.Quy.Sacomreal.rptPhieuChi((int)grvPhieuChi.GetFocusedRowCellValue("MaPhieu"), objnhanvien.HoTenNV);
            //rpt.ShowPreviewDialog();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }
    }
}