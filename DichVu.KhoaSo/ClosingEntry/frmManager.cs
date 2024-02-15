using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace DichVu.KhoaSo.ClosingEntry
{
    public partial class frmManager : XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                if (
                        beiToaNha.EditValue != null 
                        && beiTuNgay.EditValue != null 
                        && beiDenNgay.EditValue != null
                    )
                {
                    var db = new MasterDataContext();
                    var dateFrom = (DateTime)beiTuNgay.EditValue;
                    var dateTo = (DateTime)beiDenNgay.EditValue;

                    gc.DataSource = Library.Class.Connect.QueryConnect.QueryData
                        <
                            DichVu.KhoaSo.Class.ClosingEntry.GetData
                        >
                        (
                            "bcClosingGetData",
                            new
                            {
                                DateFrom = dateFrom,
                                DateTo = dateTo
                            }
                        );

                }
            }
            catch
            {
                // ignored
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();

            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }

            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            var db = new MasterDataContext();
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những kỳ cần xóa");
                        return;
                    }
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var objClosing = db.bcBookClosings.FirstOrDefault(_=>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if(objClosing != null)
                        {
                            db.bcBookClosings.DeleteOnSubmit(objClosing);
                        }
                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng thiết bị này nên không xóa được");
                return;
            }
        }

        private void gvDanhSachYeuCau_RowClick(object sender, RowClickEventArgs e)
        {
            
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            
        }

        private void itemDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MaTN = (byte)beiToaNha.EditValue
            using (var frm = new frmAddAll { Id = 0 })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn kỳ dịch vụ, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmAddAll {  Id = (int?)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.FieldName == "PhanLoaiName")
                    e.Appearance.BackColor = Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
            }
            catch { }
        }
    }
}