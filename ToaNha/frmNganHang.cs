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

namespace ToaNha
{
    public partial class frmNganHang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmNganHang()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);
        }

        private void frmNganHang_Load(object sender, EventArgs e)
        {
            gcBank.DataSource = db.nhNganHangs;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                grvBank.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Đã cập nhật dữ liệu thành công!");
                //this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var rows = grvBank.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Ngân hàng]. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            try
            {
                foreach (var i in rows)
                {
                    var obj = db.nhNganHangs.Single(p => p.ID == (int)grvBank.GetRowCellValue(i, "ID"));
                    db.nhNganHangs.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                grvBank.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemImport_Click(object sender, EventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ngân hàng", "Import");
            using (var f = new Import.frmNganHang())
            {
                f.ShowDialog();
                //if (f.isSave)
                //    LoadData();
            }
        }
    }
}