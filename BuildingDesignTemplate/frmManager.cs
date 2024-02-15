using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace BuildingDesignTemplate
{
    public partial class FrmManager : XtraForm
    {
        public byte? GroupId { get; set; }

        private MasterDataContext _db;
        public FrmManager()
        {
            InitializeComponent();
            _db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmVatTu_Manager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            switch (GroupId != null)
            {
                case true: GetDataByGroupId(); break;
                case false: GetDataAll(); break;
            }

            gv.BestFitColumns();
        }

        private void GetDataByGroupId()
        {
            gc.DataSource = (from p in _db.template_Forms
                join gr in _db.rptGroups on p.GroupId equals gr.ID
                join nv in _db.tnNhanViens on p.UserId equals nv.MaNV into user
                from nv in user.DefaultIfEmpty()
                where gr.ID == GroupId
                orderby p.DateUpdate descending
                select new
                {
                    p.Name,
                    p.Id,
                    p.Description,
                    FormGroupName = gr.Name,
                    UserName = nv.HoTenNV,
                    p.DateUpdate,
                    p.IsUseApartment
                }).ToList();
        }

        private void GetDataAll()
        {
            gc.DataSource = (from p in _db.template_Forms
                join gr in _db.rptGroups on p.GroupId equals gr.ID
                join nv in _db.tnNhanViens on p.UserId equals nv.MaNV into user
                from nv in user.DefaultIfEmpty()

                orderby p.DateUpdate descending
                select new
                {
                    p.Name,
                    p.Id,
                    p.Description,
                    FormGroupName = gr.Name,
                    UserName = nv.HoTenNV,
                    p.DateUpdate,
                    p.IsUseApartment
                }).ToList();
        }

        private void barButtonItemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FrmEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) LoadData();
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var o = _db.template_Forms.FirstOrDefault(p =>p.Id == (int) gv.GetFocusedRowCellValue("Id"));
                if (o != null)
                {
                    _db.rptReports_ToaNhas.DeleteAllOnSubmit(
                        _db.rptReports_ToaNhas.Where(_ => _.ReportID == o.ReportId));
                    _db.rptReports.DeleteAllOnSubmit(_db.rptReports.Where(_ => _.ID == o.ReportId));
                    _db.template_Forms.DeleteOnSubmit(o);
                }
                _db.SubmitChanges();
                gv.DeleteSelectedRows();
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Không xóa được");
            }
        }

        private void barEditItemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void barButtonItemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu cần sửa");
                return;
            }
            var frm = new FrmEdit
            {
                FormId = (int)id
            };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) LoadData();
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //using (var frm = new Import())
                //{
                //    frm.MaTn = (byte)barEditItemToaNha.EditValue;
                //    frm.ShowDialog();
                //    if (frm.IsSave)
                //        LoadData();
                //}
            }
            catch
            {
                //
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void itemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu cần copy");
                return;
            }
            if (DialogBox.Question("Đồng ý Sao chép?") == DialogResult.No) return;
            try
            {
                var o = _db.template_Forms.FirstOrDefault(p => p.Id == (int)gv.GetFocusedRowCellValue("Id"));
                if (o != null)
                {
                    _db.rptReports_ToaNhas.DeleteAllOnSubmit(
                        _db.rptReports_ToaNhas.Where(_ => _.ReportID == o.ReportId));
                    _db.rptReports.DeleteAllOnSubmit(_db.rptReports.Where(_ => _.ID == o.ReportId));
                    _db.template_Forms.DeleteOnSubmit(o);

                    var form = new template_Form
                    {
                        Name = o.Name,
                        Description = o.Description,
                        Content = o.Content,
                        UserId = Common.User.MaNV,
                        DateUpdate = DateTime.Now,
                        IsLock = o.IsLock,
                        IsBarCode = o.IsBarCode,
                        IsPowerSign = o.IsPowerSign,
                        PaddingLeft = o.PaddingLeft,
                        PaddingRight = o.PaddingRight,
                        PaddingBottom = o.PaddingBottom,
                        PaddingTop = o.PaddingTop,
                        LogoUrl = o.LogoUrl,
                        IsDIPLogo = o.IsDIPLogo,
                        ActionId = o.ActionId,
                        GroupId = o.GroupId,
                        IsUseApartment = o.IsUseApartment
                    };


                    _db.template_Forms.InsertOnSubmit(form);
                    // insert report
                    var idReport = _db.rptReports.Max(_ => _.ID);
                    var rpt = new rptReport { ID = idReport + 1, Name = form.Name, GroupID = o.GroupId };

                    _db.rptReports.InsertOnSubmit(rpt);

                    _db.SubmitChanges();

                    form.ReportId = rpt.ID;

                    _db.SubmitChanges();
                }
                _db.SubmitChanges();
                gv.DeleteSelectedRows();
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Không xóa được");
            }
        }
    }
}