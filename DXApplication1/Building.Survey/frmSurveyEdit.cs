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

namespace Building.Survey
{
    public partial class frmSurveyEdit : DevExpress.XtraEditors.XtraForm
    {

        public int? survey_id { get; set; }
        public byte? tower_id { get; set; }

        MasterDataContext db = new MasterDataContext();

        SV_SURVEY objSurvey;

        List<itemQuestion> ltQuestion;

        public frmSurveyEdit()
        {
            InitializeComponent();
        }

        private void frmSurveyEdit_Load(object sender, EventArgs e)
        {

            if (survey_id != null)
            {
                objSurvey = db.SV_SURVEYs.Single(o => o.survey_id == survey_id);
                objSurvey.TowerId = tower_id;
                objSurvey.survey_isactive = true;
                objSurvey.update_datetime = db.GetSystemDate();
                objSurvey.updateby = Common.User.MaNV;

                ltQuestion =  db.SV_QUESTIONs.Select(o => new itemQuestion { check = false, question_type_id = o.question_type, question_id = o.question_id , question_type_name = o.SV_QUESTION_TYPE.question_type_name, question_name = o.question_name }).ToList();
            }
            else
            {
                objSurvey = new SV_SURVEY();
                objSurvey.create_datetime = db.GetSystemDate();
                objSurvey.createby = Common.User.MaNV;
                db.SV_SURVEYs.InsertOnSubmit(objSurvey);

                ltQuestion = db.SV_QUESTIONs.Where(o=>objSurvey.SV_QUESTION_OF_SURVEYs.Any(p=>p.question_id == o.question_id)).Select(o => new itemQuestion { check = true, question_type_id = o.question_type, question_id = o.question_id, question_type_name = o.SV_QUESTION_TYPE.question_type_name, question_name = o.question_name }).ToList();
            }

            txtSurveyName.Text = objSurvey.survey_name;
            memoSurveyInfo.Text = objSurvey.survey_info;
            gcQuestion.DataSource = ltQuestion;
        }

        public class itemQuestion
        {
            public bool? check { get; set; }
            public int? question_id { get; set; }
            public string question_name { get; set; }
            public int? question_type_id { get; set; }
            public string question_type_name { get; set; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSurveyName.Text == "")
            {
                DialogBox.Error("Vui lòng nhập phiếu khảo sát");
                txtSurveyName.Focus();
                return;
            }

            if (ltQuestion.Count == 0)
            {
                DialogBox.Error("Vui lòng chọn câu hỏi");
                return;
            }

            try
            {
                objSurvey.survey_name = txtSurveyName.Text;
                objSurvey.survey_info = memoSurveyInfo.Text;

                foreach (var i in ltQuestion.Where(o => o.check == true))
                {
                    var obj = new SV_QUESTION_OF_SURVEY();
                    obj.question_id = i.question_id;
                    objSurvey.SV_QUESTION_OF_SURVEYs.Add(obj);
                }

                db.SubmitChanges();
                DialogBox.Success("Dữ liệu đã được cập nhật");
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
                return;
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

    }
}