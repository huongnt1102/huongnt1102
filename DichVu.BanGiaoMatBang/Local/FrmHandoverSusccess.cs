using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmHandoverSusccess : DevExpress.XtraEditors.XtraForm
    {
        public FrmHandoverSusccess()
        {
            InitializeComponent();
        }

        private void FrmHandoverLocal_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[1];

            SetDate(1);
            LoadData();

            // thêm user thực hiện, quy định mỗi user sẽ được bao nhiêu khách hàng hoặc phiếu nội bộ, khi nào ok thì mới tiến hành, bảng sẽ có bao nhiêu nội bộ, bao nhiêu khách hàng
            // khi nhân viên đó được chọn là 1 căn hộ, thì tính nếu nhân viên đó dc chọn, số của nó sẽ tăng lên. Nếu đã chọn full 5 thì sẽ k dc chọn nữa
            // gán nhân viên cho tòa nhà. tích chọn từng tòa nhà. Nếu tích chọn quá 5 cái thì sẽ k dc chọn nữa. nếu tích chọn quá 5 cái, check sẽ bị mờ đi, hoặc cảnh báo rồi k tích nữa
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var maTn = (byte)itemToaNha.EditValue;
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;

                var db = new MasterDataContext();

                gc.DataSource = (from _ in db.ho_ScheduleApartments
                    join cl in db.ho_Status on _.StatusId equals cl.Id into color
                    from cl in color.DefaultIfEmpty()
                    where _.BuildingId == maTn & SqlMethods.DateDiffDay(tuNgay, _.DateHandoverTo) >= 0 &
                          SqlMethods.DateDiffDay(_.DateHandoverTo, denNgay) >= 0 & _.IsChoose == true &
                          (_.StatusId == 4)
                    select new
                    {
                        _.ApartmentName,
                        _.CustomerName,
                        _.BuildingChecklistName,
                        _.DateHandoverFrom,
                        _.DateHandoverTo,
                        _.Id,
                        _.PlanName,
                        _.IsChoose,
                        _.ScheduleName,
                        _.StatusName,
                        _.UserName,
                        _.DateNumberNotification,
                        cl.Color
                    }).ToList();

                LoadDetail();
            }
            catch { }
        }

        private void LoadDetail()
        {
            try
            {
                var db = new MasterDataContext();

                var id = (int?)gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabChecklist":
                        gcScheduleApartmentChecklist.DataSource =
                            db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleApartmentId == id);
                        break;
                    case "tabHistory":
                        gcHistory.DataSource = db.ho_PlanHistories.Where(_=>_.ScheduleApartmentId == id);
                        break;
                }
            }
            catch { }
        }

        private void CbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemUserHandover_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmHandoverUser { BuildingId = (byte)itemToaNha.EditValue })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void ItemHandoverAllow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmHandoverCheckListAllow { Id = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue })
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

        private void Gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void ItemEditChecklist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmHandoverCheckList { Id = (int?)gv.GetFocusedRowCellValue("Id"), BuildingId = (byte)itemToaNha.EditValue })
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

        private void XtraTabControl1_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void ItemViewImg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (long?)gvApartmentChecklist.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Checklist].");
                return;
            }

            using (var frm = new Category.FrmViewImg())
            {
                frm.Id = id;
                //frm.ID = 3651;
                frm.ShowDialog();
            }

        }

        private void gv_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "StatusName")
                {
                    if (gv.GetRowCellValue(e.RowHandle, "Color") == null) return;
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            catch { }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}