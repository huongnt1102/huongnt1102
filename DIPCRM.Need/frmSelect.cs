using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.NhuCau
{
    public partial class frmSelect : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int MaNC = 0;
        byte SDBID = 6;
        public frmSelect()
        {
            InitializeComponent();

            //
        }

        private void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmSelect_Load(object sender, EventArgs e)
        {
            //cmbTrangThai.DataSource = db.ncTrangThai_filter();

            lookTrangThai.DataSource = db.ncTrangThais;

            //lookNguon.DataSource = db.ncNguonKHs;

            lookLoaiKH.DataSource = db.khNhomKhachHangs;

            //this.SDBID = db.AccessDatas.Single(p => p.FormID == 74 & p.PerID == Common.PerID).SDBID;

            KyBaoCao objKBC = new KyBaoCao();

            objKBC.Initialize(cmbKyBaoCao);

            itemTrangThai.EditValue = "2, 4";

            SetDate(0);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                var listTT = (itemTrangThai.EditValue ?? "").ToString();

                //switch (this.SDBID)
                //{
                //    case 1:
                //        gcNhuCau.DataSource = db.ncNhuCau_choice(listTT, tuNgay, denNgay, 0, 0, 0);
                //        break;
                //    case 2:
                //        gcNhuCau.DataSource = db.ncNhuCau_choice(listTT, tuNgay, denNgay, Common.MaPB, 0, 0);
                //        break;
                //    case 3:
                //        gcNhuCau.DataSource = db.ncNhuCau_choice(listTT, tuNgay, denNgay, 0, Common.MaNKD, 0);
                //        break;
                //    case 4:
                //        gcNhuCau.DataSource = db.ncNhuCau_choice(listTT, tuNgay, denNgay, 0, 0, Common.User.MaNV);
                //        break;
                //}
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Choice();
        }

        void Choice()
        {
            //if (grvNhuCau.FocusedRowHandle < 0)
            //{
            //   DialogBox.Error("Vui lòng chọn <Cơ hội>, xin cảm ơn.");
            //    return;
            //}

            MaNC = Convert.ToInt32(grvNhuCau.GetFocusedRowCellValue("MaNC"));
            this.Close();
        }

        private void frmSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Choice();
            }
        }

        private void grvNhuCau_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if ((e.RowHandle != grvNhuCau.FocusedRowHandle) && e.Column.Caption == "Loại KH")
                e.Appearance.ForeColor = Color.FromArgb(int.Parse(grvNhuCau.GetRowCellValue(e.RowHandle, "Color").ToString()));

            if (e.Column.Caption == "STT")
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvNhuCau.GetRowCellValue(e.RowHandle, "Color").ToString()));
        }

        private void grvNhuCau_DoubleClick(object sender, EventArgs e)
        {
            Choice();
        }
    }
}
