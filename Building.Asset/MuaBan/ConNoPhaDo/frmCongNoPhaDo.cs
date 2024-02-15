using System;
using System.Collections.Generic;

using System.Drawing;

using System.Windows.Forms;

using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.XuatKho
{
    public partial class frmCongNoPhaDo : XtraForm
    {

        public frmCongNoPhaDo()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmQuanLyHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;


            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbxKBC.Items.Add(str);
            }
            beiKBC.EditValue = objKBC.Source[3];
            SetDate(3);
            itemToaNha.EditValue = Common.User.MaTN;
            gv.BestFitColumns();
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex);
        }

        private void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();
            beiTuNgay.EditValue = objKBC.DateFrom;
            beiDenNgay.EditValue = objKBC.DateTo;
        }

        private void LoadData()
        {
           
        }

        private void LoadDetail()
        {

        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maHD = (int?)gvChiTietCongNo.GetFocusedRowCellValue("MaHD");
            if (maHD == null)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }
            
            var frm =new frmCongNoPhaDo_ThanhToan();

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void gvLichSuThuTien_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "Loai")
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MaLoai"]);
                if (category == "0")
                {
                    e.Appearance.BackColor = Color.Green;
                }
                if (category == "1")
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }
        }

        private void gvChiTietCongNo_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "TrangThai")
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MaLoai"]);
                if (category == "0")
                {
                    e.Appearance.BackColor = Color.Green;
                }
                if (category == "1")
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }
        }

        private void ppSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvLichSuThuTien.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn dòng muốn sửa!");
                return;
            }
            var frm = new frmCongNoPhaDo_ThanhToan();

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void ppXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
    }
}