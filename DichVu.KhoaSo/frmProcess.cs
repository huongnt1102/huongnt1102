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

namespace DichVu.KhoaSo
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public long ID = 0;
        public bool IsLock = true;

        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtNote.Text.Trim() == "")
            {
                DialogBox.Alert("Vui nhập [Lý do/Ghi chú], xin cảm ơn.");
                txtNote.Focus();
                return;
            }

            try
            {
                using (var db = new MasterDataContext())
                {
                    var objKS = db.dvKhoaSos.Single(p => p.ID == ID);
                    if (objKS != null)
                    {
                        if (objKS.IsLock.GetValueOrDefault() != IsLock)
                        {
                            objKS.IsLock = IsLock;

                            var objLS = new dvKhoaSoLichSu();
                            objLS.GhiChu = txtNote.Text.Trim();
                            objLS.IsLock = IsLock;
                            objLS.MaNV = objnhanvien.MaNV;
                            objLS.RefID = ID;
                            db.dvKhoaSoLichSus.InsertOnSubmit(objLS);

                            db.SubmitChanges();

                            DialogBox.Alert("Dữ liệu đã được cập nhật.");
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            DialogBox.Alert(string.Format("Mặt bằng này hiện đang [{0}]. Vui lòng kiểm tra lại, xin cảm ơn.", objKS.IsLock.GetValueOrDefault() ? "Khóa" : "Mở  khóa"));
                        }
                    }
                }
            }
            catch (Exception ex){ DialogBox.Alert("Đã xảy ra lỗi. Code: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}