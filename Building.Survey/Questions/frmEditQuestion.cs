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

namespace Building.Survey.Questions
{
    public partial class frmEditQuestion : DevExpress.XtraEditors.XtraForm
    {
        public int? question_id { get; set; }

        MasterDataContext db = new MasterDataContext();

        SV_QUESTION objQuestion;

        public frmEditQuestion()
        {
            InitializeComponent();
        }

        private void frmEditQuestion_Load(object sender, EventArgs e)
        {
            glkQuestionType.Properties.DataSource = db.SV_QUESTION_TYPEs;

            if (question_id != null)
            {
                objQuestion = db.SV_QUESTIONs.Single(o => o.question_id == question_id);
                glkQuestionType.EditValue = objQuestion.question_type;
                memoQuestion.Text = objQuestion.question_name;
            }
            else
            {
                objQuestion = new SV_QUESTION();
                db.SV_QUESTIONs.InsertOnSubmit(objQuestion);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int?)glkQuestionType.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn loại câu hỏi");
                    glkQuestionType.Focus();
                    return;
                }

                if (memoQuestion.Text == "")
                {
                    DialogBox.Error("Vui lòng nhập câu hỏi");
                    memoQuestion.Focus();
                    return;
                }

                objQuestion.question_name = memoQuestion.Text;
                objQuestion.question_type = (int?)glkQuestionType.EditValue;

                db.SubmitChanges();
                DialogBox.Success("Dữ liệu đã được cập nhật!!!");
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