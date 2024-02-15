using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq.SqlClient;
using Building.AppVime;

namespace Building.Survey
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        svSurvey objSV;

        public int? idSV { get; set; }

        public byte? tower_id { get; set; }

        public frmEdit()
        {
            InitializeComponent();
            this.Load += frmEdit_Load;
            gvQuestion.FocusedRowChanged += gvQuestion_FocusedRowChanged;
            gvQuestion.FocusedRowLoaded += gvQuestion_FocusedRowLoaded;
            gvQuestion.ValidateRow += gvQuestion_ValidateRow;
            gvQuestion.InvalidRowException += Common.InvalidRowException;
            gvQuestion.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            gvAnswer.ValidateRow += gvAnswer_ValidateRow;
            gvAnswer.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            

        }

        void gvAnswer_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            var id = gvAnswer.GetRowCellDisplayText(e.ListSourceRow, "STT").ToString();
            var idQ = gvQuestion.GetFocusedRowCellDisplayText("STT").ToString();

            if (list.Any(o => o.STT == id && o.idQuestion == idQ))// && o.MaTN == maTN))
            {
                e.Visible = true;
                e.Handled = true;
            }
            else
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void gvAnswer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var obj = e.Row as svAnswer;

            if (obj.answer_name == null)
            {
                e.ErrorText = "Vui lòng nhập câu trả lời";
                e.Valid = false;
            }

            //if (obj.answer_type == null)
            //{
            //    e.ErrorText = "Vui lòng chọn loại câu hỏi";
            //    e.Valid = false;
            //}
        }

        void gvQuestion_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var obj = e.Row as svQuestion;

            if (obj.question_name == null)
            {
                e.ErrorText = "Vui lòng nhập câu hỏi";
                e.Valid = false;
            }

            if (obj.answer_type == null)
            {
                e.ErrorText = "Vui lòng chọn loại câu hỏi";
                e.Valid = false;
            }
        }

        void gvQuestion_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            LoadAnswer();
        }

        void gvQuestion_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadAnswer();
        }

        void frmEdit_Load(object sender, EventArgs e)
        {

            if (idSV == null)
            {
                objSV = new svSurvey();
                objSV.usercreate = Common.User.MaNV;
                objSV.datecreate = db.GetSystemDate();
                objSV.isactive = true;
                objSV.towerID = tower_id;
                db.svSurveys.InsertOnSubmit(objSV);
            }
            else
            {
                objSV = db.svSurveys.Single(o => o.ID == idSV);
                objSV.userchange = Common.User.MaNV;
                objSV.datechange = db.GetSystemDate();

                dateFrom.EditValue = (DateTime)objSV.datefr;
                dateTo.EditValue = (DateTime)objSV.dateto;
            }

            txtMaKS.Text = objSV.surveycode;
            txtTenKS.Text = objSV.surveyname;
            IsActive.Checked = (bool)objSV.isactive;
            glkAnswerType.DataSource = db.svAnswerTypes;



            //LoadData();
            gcQuestion.DataSource = objSV.svQuestions;

        }

        void LoadAnswer()
        {
            //var id = (int?)gvQuestion.GetFocusedRowCellValue("STT");

            svQuestion quest = (svQuestion)gvQuestion.GetFocusedRow();

            if (quest == null)
                gcAnswer.DataSource = null;
            else
            {
                //gcAnswer.DataSource = quest.svAnswers;
                //list = quest.svAnswers.AsEnumerable().Select((o, Index) => new itemAnswer { STT = (Index + 1).ToString(), idQuestion = gvQuestion.GetFocusedRowCellDisplayText("STT") }).ToList();
                //gvAnswer.CustomRowFilter += gvAnswer_CustomRowFilter;
            }
        }

        class itemAnswer
        {
            public string STT { get; set; }
            public string idQuestion { get; set; }
        }

        List<itemAnswer> list;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMaKS.Text == "")
            {
                DialogBox.Error("Vui lòng nhập [Mã khảo sát] !!!");
                txtMaKS.Focus();
                return;
            }

            if (txtTenKS.Text == "")
            {
                DialogBox.Error("Vui lòng nhập [Tên khảo sát] !!!");
                txtMaKS.Focus();
                return;
            }

            if (gcQuestion.DataSource == null)
            {
                DialogBox.Error("Vui lòng nhập [Câu hỏi] !!!");
                return;
            }

            if (gcAnswer.DataSource == null)
            {
                DialogBox.Error("Vui lòng nhập [Câu trả lời] !!!");
                return;
            }

            if ((DateTime?)dateFrom.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập [Từ ngày]");
                return;
            }

            if ((DateTime?)dateTo.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập [Đến ngày]");
                return;
            }

            if (SqlMethods.DateDiffDay(dateTo.DateTime, dateFrom.DateTime) > 0)
            {
                DialogBox.Error("[Từ ngày] - [Đến ngày] không hợp lệ !!!");
                return;
            }

            objSV.surveycode = txtMaKS.Text;
            objSV.surveyname = txtTenKS.Text;
            objSV.datefr = (DateTime?)dateFrom.DateTime;
            objSV.dateto = (DateTime?)dateTo.DateTime;
            db.SubmitChanges();

            if (idSV == null)
            {
                CommonVime.GetConfig();
                var toa_nha = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == this.tower_id);
                string building_code = toa_nha.DisplayName;
                int building_matn = toa_nha.Id;

                AppVime.Class.tbl_building_get_id model_param = new AppVime.Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model_param);

                //var idNew = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME 
                //                                                                                                                                : db.Connection.ConnectionString, param);

                Notify model = new Notify()
                {
                    ID = objSV.ID,
                    Name = objSV.surveyname,
                    ApiKey = CommonVime.ApiKey,
                    SecretKey = CommonVime.SecretKey,
                    IdNew = 8,//idNew.FirstOrDefault(),
                    isPersonal = VimeService.isPersonal
                };
                var ret = VimeService.Post(model, "/Survey/SendNotify");
            } 

            DialogBox.Success("Dữ liệu đã được cập nhật !!!");
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvAnswer_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            LoadAnswer();
        }

        private void glkAnswerType_EditValueChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            
            if ((int)glk.EditValue == 1)
                gvAnswer.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                
            else
                gvAnswer.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }

        private class Notify
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string ApiKey { get; set; }
            public string SecretKey { get; set; }
            public int IdNew { get; set; }
            public bool isPersonal { get; set; }
        }
    }
}