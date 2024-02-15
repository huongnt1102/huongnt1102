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
using System.Data.Linq.SqlClient;

namespace DichVu.ThueNgoai
{
    public partial class frmCongNoManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

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

        public frmCongNoManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmCongNoManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookToaNha.DataSource = list;
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

                lookToaNha.DataSource = list2;
                if (list2.Count > 0)
                    itemToaNha.EditValue = list2[0].MaTN;
            }
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
            LoadData();
        }

        private void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : -1;

            gcCongNo.DataSource = db.hdtnCongNos
                       .Where(p => p.NgayThanhToan <= (DateTime)itemDenNgay.EditValue
                           & p.ConLai > 0
                           & SqlMethods.DateDiffDay(p.hdtnHopDong.NgayBD, tuNgay) <= 0
                           & SqlMethods.DateDiffDay(p.hdtnHopDong.NgayBD, denNgay) >= 0
                           & p.hdtnHopDong.MaTN == maTN)
                       .Select(p => new
                       {
                           p.MaCongNo,
                           MaHD = p.MaHD,
                           p.hdtnHopDong.MaNCC,
                           SoHopDong = p.hdtnHopDong.SoHD,
                           DaThanhToan = p.DaThanhToan,
                           ConNo = p.ConLai,
                           NgayThanhToan = p.NgayThanhToan,
                           PhaiThanhToan = p.ConLai + p.DaThanhToan,
                           NhaCungCap = p.hdtnHopDong.tnNhaCungCap.TenNCC
                       });
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn công nợ cần thanh toán");  
                return;
            }

            using (var frm = new frmPaid())
            {
                frm.objnhanvien = objnhanvien;
                frm.MaHD = (int)grvCongNo.GetFocusedRowCellValue("MaHD");  
                frm.MaNCC = (int)grvCongNo.GetFocusedRowCellValue("MaNCC");  
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
    }
}