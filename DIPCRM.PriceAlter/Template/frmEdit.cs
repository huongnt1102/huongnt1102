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


namespace DIPCRM.PriceAlert.Template
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }

        MasterDataContext db;
        //bgBieuMau objBM;

        bool isDuplication(string fieldName, int index, object value)
        {
            var newValue = value.ToString();
            for (int i = 0; i < grvSanPham.RowCount - 1; i++)
            {
                if (i == index) continue;
                var oldValue = grvSanPham.GetRowCellValue(i, fieldName).ToString();
                if (oldValue == newValue) return true;
            }
            return false;
        }

        Library.Controls.ctlSanPhamItemEdit ctlSanPhamItemEdit1;

        public frmEdit()
        {
            InitializeComponent();

            //Translate.Language.TranslateControl(this, barManager1);

            this.Load += new EventHandler(frmEdit_Load);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            grvSanPham.KeyUp += new KeyEventHandler(grvSanPham_KeyUp);
            grvSanPham.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(grvSanPham_ValidateRow);
            grvSanPham.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(grvSanPham_InvalidRowException);
            //ctlSanPhamItemEdit1 = new Library.Controls.ctlSanPhamItemEdit();
            //ctlSanPhamItemEdit1.EditValueChanged += new EventHandler(ctlSanPhamItemEdit1_EditValueChanged);
            //colMaSP.ColumnEdit = ctlSanPhamItemEdit1;
        }

        void ctlSanPhamItemEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var sp = (LookUpEdit)sender;
            if (sp.EditValue == null) return;
            if (isDuplication("MaSP", grvSanPham.FocusedRowHandle, sp.EditValue))
            {
                DialogBox.Error("Trùng sản phẩm, vui lòng chọn sản phẩm khác");
                grvSanPham.SetFocusedRowCellValue("MaSP", grvSanPham.GetFocusedRowCellValue("MaSP"));
            }
        }

        void grvSanPham_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        void grvSanPham_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var maSP = (int?)grvSanPham.GetRowCellValue(e.RowHandle, "MaSP");
            if (maSP == null)
            {
                e.ErrorText = "Vui lòng chọn sản phẩm";
                e.Valid = false;
                return;
            }

            //var sl = (decimal?)grvSanPham.GetRowCellValue(e.RowHandle, "SoLuong");
            //if (sl.GetValueOrDefault() <= 0)
            //{
            //    e.ErrorText = "Vui lòng nhập số lượng";
            //    e.Valid = false;
            //}
        }

        void grvSanPham_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc chắn không?") == System.Windows.Forms.DialogResult.No) return;
                grvSanPham.DeleteSelectedRows();
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tiêu đề");
                txtTieuDe.Focus();
                return;
            }

            if (ctlBieuMauEdit1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu");
                return;
            }

            grvSanPham.RefreshData();
            if (grvSanPham.RowCount <= 1)
            {
                DialogBox.Error("Vui lòng nhập sản phẩm");
                return;
            }
            #endregion

            //bao gia
            //objBM.MaDA = (int?)ctlDuAnEdit1.EditValue;
            objBM.MaNV = (int?)ctlNhanVienEdit1.EditValue;
            objBM.DKGH = txtDKGH.Text;
            objBM.DKTT = txtDKTT.Text;
            objBM.ThoiHanBG = txtGiaTriGH.Text;
            objBM.MaLT = (short?)ctlLoaiTienEdit1.EditValue;
            objBM.MaBM = (int?)ctlBieuMauEdit1.EditValue;
            objBM.TieuDe = txtTieuDe.Text;
            objBM.GhiChu = txtGhiChu.Text;
            if (this.ID == null)
            {
                objBM.NgayNhap = DateTime.Now;
                objBM.MaNVN = Common.User.MaNV;
                db.bgBieuMaus.InsertOnSubmit(objBM);
            }
            else
            {
                objBM.NgaySua = DateTime.Now;
                objBM.MaNVS = Common.User.MaNV;
            }
            //
            db.SubmitChanges();
            //
            this.ID = objBM.ID;
            DialogBox.Success();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            ctlLoaiTienEdit1.LoadData();
            //ctlDuAnEdit1.LoadData();
            ctlNhanVienEdit1.LoadData();
            ctlSanPhamItemEdit1.LoadData();
            ctlBieuMauEdit1.LoadData();

            if (this.ID != null)
            {
                objBM = db.bgBieuMaus.Single(p => p.ID == this.ID);
                //ctlDuAnEdit1.EditValue = objBM.MaDA;
                ctlNhanVienEdit1.EditValue = objBM.MaNV;
                txtDKGH.EditValue = objBM.DKGH;
                txtDKTT.EditValue = objBM.DKTT;
                txtGiaTriGH.EditValue = objBM.ThoiHanBG;
                ctlLoaiTienEdit1.EditValue = objBM.MaLT;
                ctlBieuMauEdit1.EditValue = objBM.MaBM;
                txtTieuDe.EditValue = objBM.TieuDe;
                txtGhiChu.EditValue = objBM.GhiChu;
            }
            else
            {
                ctlLoaiTienEdit1.ItemIndex = 0;
                ctlNhanVienEdit1.EditValue = Common.User.MaNV;

                objBM = new bgBieuMau();
            }

            //gcSanPham.DataSource = objBM.bgbmSanPhams;
        }
    }
}