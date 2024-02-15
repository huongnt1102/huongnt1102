using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building.AppVime.News
{
    public partial class frmStt : DevExpress.XtraEditors.XtraForm
    {
        public Guid Id { get; set; }
        public int? STT { get; set; }
        public frmStt()
        {
            InitializeComponent();
        }

        private void frmStt_Load(object sender, EventArgs e)
        {
            if (STT != null)
                spinStt.EditValue = STT;
        }

        private void frmStt_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(spinStt.EditValue != null)
            {
                int s = int.Parse(spinStt.EditValue.ToString());

                var model = new { Id = Id, STT = s };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var result = Library.Class.Connect.QueryConnect.Query<bool>("dbo.app_news_update_stt", param);

                STT = s;
            }
            this.Close();
        }
    }
}