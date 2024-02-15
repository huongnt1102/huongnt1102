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
using DevExpress.XtraGrid;
using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Utils;
using System.Threading.Tasks;
using System.Collections;
using DevExpress.Data;

namespace Building.Survey
{
    public partial class frmSurveyManager : DevExpress.XtraEditors.XtraForm
    {
        public frmSurveyManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            //gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        #region

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        #endregion

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmSurveyEdit())
            {
                frm.tower_id = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
            }
            LoadData();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvSurvey.GetFocusedRowCellValue("survey_id");

            using (var frm = new frmSurveyEdit())
            {
                frm.tower_id = (byte?)itemToaNha.EditValue;
                frm.survey_id = id;
                frm.ShowDialog();
            }
            LoadData();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var obj = gvSurvey.GetSelectedRows();

            if (obj.Count() == 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu khảo sát");
                return;
            }

            using (var db = new MasterDataContext())
            {
                foreach (var i in obj)
                {
                    var sv = db.SV_SURVEYs.Single(o => o.survey_id == (int?)gvSurvey.GetRowCellValue(i, "survey_id"));
                    db.SV_SURVEYs.DeleteOnSubmit(sv);

                }

                try
                {
                    db.SubmitChanges();
                    DialogBox.Success("Dữ liệu đã được cập nhật");

                }
                catch (Exception ex)
                {
                    DialogBox.Error("Lỗi: " + ex.Message);
                }
            }

            LoadData();
        }

        void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var towerID = (byte?)itemToaNha.EditValue;
                var dateFr = (DateTime?)itemTuNgay.EditValue;
                var dateTo = (DateTime?)itemDenNgay.EditValue;

                gcSurvey.DataSource = (from s in db.SV_SURVEYs
                                       join nvn in db.tnNhanViens on s.createby equals nvn.MaNV
                                       join nvs in db.tnNhanViens on s.updateby equals nvs.MaNV into dsnvs
                                       from nvs in dsnvs.DefaultIfEmpty()
                                       where s.TowerId == towerID
                                       & SqlMethods.DateDiffDay(dateFr, s.create_datetime) >= 0
                                       & SqlMethods.DateDiffDay(s.create_datetime, dateTo) >= 0
                                       select new
                                       {
                                           s.survey_id,
                                           s.survey_name,
                                           s.survey_info,
                                           s.survey_isactive,
                                           s.update_datetime,
                                           s.create_datetime,
                                           createby = nvn.HoTenNV,
                                           updateby = nvs.HoTenNV,
                                       }).ToList();
            }
        }

        void LoadDetail()
        {
            var id = (int?)gvSurvey.GetFocusedRowCellValue("survey_id");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu khảo sát");
                return;
            }
            using (var db = new MasterDataContext())
                gcQuestion.DataSource = (from s in db.SV_QUESTION_OF_SURVEYs
                                         join q in db.SV_QUESTIONs on s.question_id equals q.question_id
                                         join t in db.SV_QUESTION_TYPEs on q.question_type equals t.question_type_id
                                         where s.survey_id == id
                                         select new
                                         {
                                             t.question_type_name,
                                             q.question_name
                                         }).ToList();
        }

        private void gvSurvey_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void gvSurvey_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            LoadDetail();
        }
    }
    
}