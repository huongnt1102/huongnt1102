using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace LandsoftBuildingGeneral.DynBieuMau
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        Library.MasterDataContext db;
        public Library.tnNhanVien objnhanvien;
        bool first = true;
        public frmManager()
        {
            InitializeComponent();
            db = new Library.MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            lookLoaiBieuMau.DataSource = db.BmLoaiBieuMaus;

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            LoadData();

            first = false;
            grvBieuMau.CellValueChanging += grvBieuMau_CellValueChanging;
        }

        void grvBieuMau_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MaLBM")
            {
                var t = (BmBieuMau)grvBieuMau.GetFocusedRow();
                //t.BmLoaiBieuMau = bieumaudb.BmLoaiBieuMaus.SingleOrDefault(p => p.MaLBM == (int)e.Value);
                t.Clear();
            }
        }

        private void grvbieumau_DoubleClick(object sender, EventArgs e)
        {
            if (btnedit.Enabled == false) return;
            btnedit_ItemClick(null, null);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                gcBieuMau.DataSource = db.BmBieuMau_getAll(maTN);
            }
            catch
            {
                gcBieuMau.DataSource = null;
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void DeleteRow(int[] index)
        {
            if (index.Length <= 0)
            {
                Library.DialogBox.Error("Vui lòng chọn mẫu tin cần xóa");
                return;
            }
            if (Library.DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in index)
            {
                Library.BmBieuMau objBM = db.BmBieuMaus.Single(p => p.MaBM == (int)grvBieuMau.GetRowCellValue(i, "MaBM"));
                db.BmBieuMaus.DeleteOnSubmit(objBM);
            }

            try
            {
                db.SubmitChanges();
                grvBieuMau.DeleteSelectedRows();
            }
            catch
            {
                Library.DialogBox.Error("Có lỗi xảy ra, không xóa được");
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteRow(grvBieuMau.GetSelectedRows());
            LoadData();
        }

        private void btnedit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmDesign frm = new frmDesign())
            {
                if (grvBieuMau.GetFocusedRowCellValue("MaBM") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                    return;
                }
                else
                {
                    int MaBM = (int)grvBieuMau.GetFocusedRowCellValue("MaBM");
                    var objBM = db.BmBieuMaus.Single(p => p.MaBM == MaBM);
                    frm.RtfText = objBM.Template;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        objBM.Template = frm.RtfText;
                        db.SubmitChanges();
                    }
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //grvBieuMau.AddNewRow();
            var f = new frmEdit();
            f.MaTN = (byte)itemToaNha.EditValue;
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void gcbieumau_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                Library.DialogBox.Alert("Đã lưu dữ liệu");
            }
            catch
            {
                Library.DialogBox.Error("Có lỗi xảy ra, không xóa được");
            }
        }

        private void grvbieumau_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvBieuMau.SetFocusedRowCellValue("MaNV", objnhanvien.MaNV);
            grvBieuMau.SetFocusedRowCellValue("MaTN", objnhanvien.MaTN);
            grvBieuMau.SetFocusedRowCellValue("NgayThem", DateTime.Now);
        }

        private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.GetFocusedRowCellValue("MaBM") == null)
            {
                Library.DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                return;
            }
            else
            {
                int MaBM = (int)grvBieuMau.GetFocusedRowCellValue("MaBM");
                var objBM = db.BmBieuMaus.Single(p => p.MaBM == MaBM);
                using (frmPreview frm = new frmPreview())
                {
                    frm.RtfText = objBM.Template;
                    frm.ShowDialog();
                }
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.GetFocusedRowCellValue("MaBM") == null)
            {
                Library.DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                return;
            }

            var f = new frmEdit();
            f.MaBM = Convert.ToInt32(grvBieuMau.GetFocusedRowCellValue("MaBM"));
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }
    }
}