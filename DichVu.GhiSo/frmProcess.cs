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

namespace DichVu.GhiSo
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public Guid ID;
        public int Month, Year;
        public byte TowerID;
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
                    #region Code old

                    //var objKS = db.dvGhiSos.SingleOrDefault(p => p.ID == ID);
                    //if (objKS != null)
                    //{
                    //    if (objKS.IsLock.GetValueOrDefault() != IsLock)
                    //    {
                    //        objKS.IsLock = IsLock;

                    //        var objLS = new dvGhiSoLichSu();
                    //        objLS.GhiChu = txtNote.Text.Trim();
                    //        objLS.IsLock = IsLock;
                    //        objLS.MaNV = objnhanvien.MaNV;
                    //        objLS.RefID = ID;
                    //        db.dvGhiSoLichSus.InsertOnSubmit(objLS);

                    //        db.SubmitChanges();

                    //        DialogBox.Alert("Dữ liệu đã được cập nhật.");
                    //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //        this.Close();
                    //    }
                    //    else
                    //    {
                    //        DialogBox.Alert(string.Format("Mặt bằng này hiện đang [{0}]. Vui lòng kiểm tra lại, xin cảm ơn.", objKS.IsLock.GetValueOrDefault() ? "Khóa" : "Mở  khóa"));
                    //    }
                    //}

                    #endregion

                    #region Code new

                    db.dvGhiSo_Confirm(TowerID, Month, Year, IsLock);

                    var objLS = new dvGhiSoLichSu();
                    objLS.ID = Guid.NewGuid();
                    objLS.GhiChu = "Function (Confirm): " + txtNote.Text.Trim();
                    objLS.IsLock = IsLock;
                    objLS.MaNV = objnhanvien.MaNV;
                    objLS.Months = Month;
                    objLS.Years = Year;
                    objLS.TowerID = TowerID;
                    db.dvGhiSoLichSus.InsertOnSubmit(objLS);

                    db.SubmitChanges();

                    DialogBox.Alert("Dữ liệu đã được cập nhật.");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();

                    #endregion
                }
            }
            catch (Exception ex) { DialogBox.Alert("Đã xảy ra lỗi. Code: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}