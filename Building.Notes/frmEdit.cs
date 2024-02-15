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

namespace Building.Notes
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public int? FormID { get; set; }
        public int? LinkID { get; set; }

        private MasterDataContext db;
        private NoteHistory objNote;
        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            lookNoteType.Properties.DataSource = db.NoteTypes;

            if (this.ID != null)
            {
                objNote = db.NoteHistories.Single(p => p.ID == this.ID);
                lookNoteType.EditValue = objNote.TypeID;
                txtContents.EditValue = objNote.Contents;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtContents.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                txtContents.Focus();
                return;
            }

            if (this.ID == null)
            {
                objNote = new NoteHistory();
                objNote.FormID = this.FormID;
                objNote.LinkID = this.LinkID;
                objNote.DateCreate = db.GetSystemDate();
                objNote.StaffCreate = objNV.MaNV;
                db.NoteHistories.InsertOnSubmit(objNote);
            }

            objNote.TypeID = (short?)lookNoteType.EditValue;
            objNote.Contents = txtContents.Text.Trim();

            db.SubmitChanges();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}