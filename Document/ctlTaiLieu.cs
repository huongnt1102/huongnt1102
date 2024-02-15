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
using ftp = FTP;

namespace Document
{
    public partial class ctlTaiLieu : DevExpress.XtraEditors.XtraUserControl
    {
        public System.Windows.Forms.Form frm { get; set; }

        public tnNhanVien objNV;
        public ctlTaiLieu()
        {
            InitializeComponent();
            TranslateLanguage.TranslateUserControl(this);
        }

        public int? LinkID { get; set; }
        public int? FormID { get; set; }
        public int? MaNV { get; set; }
        byte SDBID = 6;

        public void TaiLieu_Load()
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);

            using (var db = new MasterDataContext())
            {

                gcTaiLieu.DataSource = db.docTaiLieu_SelectByLink(FormID, LinkID, 0);

            }
        }

        public void TaiLieu_Remove()
        {
            gcTaiLieu.DataSource = null;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.MaNV != null)
            {
                if (this.MaNV == objNV.MaNV)
                {
                    var frm = new frmEdit();
                    frm.objNV = objNV;
                    frm.FormID = this.FormID;
                    frm.LinkID = this.LinkID;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
                }
                else
                {
                    DialogBox.Alert("[Hợp đồng] này không do bạn quản lý.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                }
            }

        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MaNV == objNV.MaNV)
            {
                var maTL = (int?)grvTaiLieu.GetFocusedRowCellValue("MaTL");
                if (maTL == null)
                {
                    DialogBox.Error("Vui lòng chọn [Tài liệu], xin cảm ơn.");
                    return;
                }
                var frm = new frmEdit();
                frm.objNV = objNV;
                frm.MaTL = maTL;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
            }
            else
            {
                DialogBox.Alert("[Hợp đồng] này không do bạn quản lý.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MaNV == objNV.MaNV)
            {
                try
                {
                    var indexs = grvTaiLieu.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn [Tài liệu], xin cảm ơn.");
                        return;
                    }
                    if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;
                    List<string> files = new List<string>();
                    using (var db = new MasterDataContext())
                    {
                        foreach (var i in indexs)
                        {
                            var objDoc = db.docTaiLieus.Single(p => p.MaTL == (int)grvTaiLieu.GetRowCellValue(i, "MaTL"));
                            if (objDoc.DuongDan != null)
                                files.Add(objDoc.DuongDan);
                            db.docTaiLieus.DeleteOnSubmit(objDoc);
                        }
                        db.SubmitChanges();
                    }
                    var cmd = new ftp.FtpClient();
                    foreach (var url in files)
                    {
                        cmd.Url = url;
                        try
                        {
                            cmd.DeleteFile();
                        }
                        catch { }
                    }

                    TaiLieu_Load();
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
            }
            else
            {
                DialogBox.Alert("[Hợp đồng] này không do bạn quản lý.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            }
        }

        private void itemXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new ftp.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            frm.ShowDialog();
        }

        private void itemTaiVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new ftp.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            if (frm.SaveAs())
                frm.ShowDialog();
        }

        private void ctlTaiLieu_Load(object sender, EventArgs e)
        {
            if (LinkID == 100)
            {
                if (Common.User.MaNV != 6)
                {
                    itemXoa.Enabled = false;
                }
                else
                {
                    itemXoa.Enabled = true;
                }
            }
        }
    }
}
