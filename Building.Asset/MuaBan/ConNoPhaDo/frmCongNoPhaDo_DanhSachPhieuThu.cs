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
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.XuatKho
{
    public partial class frmCongNoPhaDo_DanhSachPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        public frmCongNoPhaDo_DanhSachPhieuThu()
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
        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
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

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            
        }
    }
}