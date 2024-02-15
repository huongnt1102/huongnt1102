using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace Building.Notes
{
    public partial class ctlNoteHistory : DevExpress.XtraEditors.XtraUserControl
    {
        public tnNhanVien objNV;
        public ctlNoteHistory()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;

            Permission();
        }

        public int? LinkID { get; set; }
        public int? FormID { get; set; }
        public int? MaNV { get; set; }

        void Permission()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                   
                }
            }
            catch //(Exception ex)
            {
                //DialogBox.Error(ex.Message);
            }
        }

        public void NoteHistory_Load()
        {
            using (var db = new MasterDataContext())
            {
                
            }
        }

        public void NoteHistory_Remove()
        {
            gcNotes.DataSource = null;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MaNV != objNV.MaNV) return;

            var frm = new frmEdit();
            frm.FormID = this.FormID;
            frm.LinkID = this.LinkID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) NoteHistory_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvNotes.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn ghi chú");
                return;
            }
            var frm = new frmEdit();
            frm.ID = id;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) NoteHistory_Load();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = grvNotes.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn ghi chú");
                    return;
                }
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    foreach (var i in indexs)
                    {
                        var objDoc = db.NoteHistories.Single(p => p.ID == (int)grvNotes.GetRowCellValue(i, "ID"));
                        db.NoteHistories.DeleteOnSubmit(objDoc);
                    }
                    db.SubmitChanges();
                }
                NoteHistory_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}
