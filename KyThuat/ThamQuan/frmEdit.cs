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

namespace KyThuat.ThamQuan
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public tqThamQuan objTQ;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        byte? MaTN { get; set; }
        int? MaKN { get; set; }
        int? MaTL { get; set; }
        int? MaMB { get; set; }
        byte STT { get; set; }
        

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
            slookTaiSan.EditValueChanged+=new EventHandler(slookTaiSan_EditValueChanged);
        }

        string getNewMaTQ()
        {
            string maTQ = "";
            db.tqThamQuan_getNewMaTQ(ref maTQ);
            return maTQ;
        }

        void LoadLookTS()
        {
            switch (STT)
            {
                case 1:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == MaTN & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                        .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
                case 2:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.mbTangLau.MaKN == MaKN & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
                case 3:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.MaTL == MaTL & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
                case 4:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.MaMB == MaMB & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                     .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
            }
            
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookTrangThai.DataSource = db.tsTinhTrangs.Select(p => new { p.MaTT, p.TenTT });
            lookTN.Properties.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN});
            lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaPB == 18).Select(q => new { q.MaNV, q.HoTenNV });
            LoadLookTS();
            if (this.objTQ != null)
            {
                objTQ = db.tqThamQuans.Single(p => p.MaTQ == objTQ.MaTQ);
                txtMaSoTQ.Text = objTQ.MaSoTQ;
                dateNgayTQ.DateTime = (DateTime)objTQ.NgayTQ;
                lookNhanVien.EditValue = objTQ.MaNV;
                txtDienGiai.Text = objTQ.DienGiai;
                lookTN.EditValue = objTQ.MaTN;
                lookKhoiNha.EditValue = objTQ.MaKN;
                lookTangLau.EditValue = objTQ.MaTL;
                lookMatBang.EditValue = objTQ.MaMB;
                
            }
            else
            {
                objTQ = new tqThamQuan();
                db.tqThamQuans.InsertOnSubmit(objTQ);
                objTQ.MaTQ = getNewMaTQ();
                txtMaSoTQ.Text = "TQ-" + objTQ.MaTQ;
                dateNgayTQ.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                lookTN.EditValue = objnhanvien.MaTN;
            }
            gcTaiSan.DataSource = objTQ.tqTaiSans;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lookTN.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án trước khi lưu. Xin cảm ơn");
                return;
            }
            objTQ.MaSoTQ = txtMaSoTQ.Text;
            objTQ.NgayTQ = (DateTime?)dateNgayTQ.DateTime;
            objTQ.DienGiai = txtDienGiai.Text;
            objTQ.MaNV = (int?)lookNhanVien.EditValue;
            objTQ.MaTN = (byte?)lookTN.EditValue;
            objTQ.MaKN = (int?)lookKhoiNha.EditValue;
            objTQ.MaTL = (int?)lookTangLau.EditValue;
            objTQ.MaMB = (int?)lookMatBang.EditValue;

            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu!");
            }
            catch
            {
                objTQ.MaTQ = getNewMaTQ();
                for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                    grvTaiSan.SetRowCellValue(i, "MaKH", objTQ.MaTQ);
                DialogBox.Alert("Dữ liệu không thể lưu. Vui lòng kiểm tra lại kết nối!");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void slookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            if (lookTN.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án trước khi thêm tài sản. Xin cảm ơn!");
                return;
            }
          //  LookUpEdit l = (LookUpEdit)sender;

           // grvTaiSan.SetFocusedRowCellValue("MaTT", l.GetColumnValue("MaTT"));
        }

        private void grvTaiSan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTaiSan.SetFocusedRowCellValue("MaTQ", objTQ.MaTQ);
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    switch (e.Column.FieldName)
            //    {
            //        case "MaTS":
            //            tqTaiSan objTS = (tqTaiSan)grvTaiSan.GetRow(e.RowHandle);
            //            objTS.tsTaiSanSuDung = db.tsTaiSanSuDungs.Single(p => p.MaTS == (int)e.Value);
            //            break;
            //        case "MaTT":
            //            tqTaiSan objTT = (tqTaiSan)grvTaiSan.GetRow(e.RowHandle);
            //            objTT.tsTrangThai = db.tsTrangThais.Single(p => p.MaTT == (int)e.Value);
            //            break;
            //    }
            //}
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        private void lookKhoiNha_EditValueChanged(object sender, EventArgs e)
        {
            if (lookKhoiNha.EditValue == null)
            {
                lookTangLau.Properties.DataSource = null;
                return;
            }
            lookTangLau.EditValue = null;
            lookMatBang.EditValue = null;
            lookTangLau.Properties.DataSource = db.mbTangLaus.Where(q => q.MaKN == (int?)lookKhoiNha.EditValue).Select(p => new { p.MaTL, p.TenTL });
            lookMatBang.Properties.DataSource = db.mbMatBangs.Where(q => q.mbTangLau.MaKN == (int?)lookKhoiNha.EditValue).Select(p => new { p.MaMB, p.MaSoMB });
            MaKN = (int?)lookKhoiNha.EditValue;
            STT = 2;
            LoadLookTS();
        }

        private void lookTangLau_EditValueChanged(object sender, EventArgs e)
        {
            if (lookTangLau.EditValue == null)
            {
                lookMatBang.Properties.DataSource = null;
                return;
            }
            lookMatBang.EditValue = null;
            lookMatBang.Properties.DataSource = db.mbMatBangs.Where(q=>q.MaTL==(int?)lookTangLau.EditValue).Select(p => new { p.MaMB, p.MaSoMB });
            MaTL = (int?)lookTangLau.EditValue;
            STT = 3;
            LoadLookTS();
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            if (lookMatBang.EditValue == null)
                return;
            MaMB = (int?)lookMatBang.EditValue;
            STT = 4;
            LoadLookTS();
        }

        private void lookTN_EditValueChanged(object sender, EventArgs e)
        {
            if (lookTN.EditValue == null)
            {
                lookKhoiNha.Properties.DataSource = null;
                return;
            }
            lookKhoiNha.EditValue = null;
            lookTangLau.EditValue = null;
            lookMatBang.EditValue = null;
            lookKhoiNha.Properties.DataSource = db.mbKhoiNhas.Where(q => q.MaTN == (byte?)lookTN.EditValue).Select(p => new { p.MaKN, p.TenKN });
            lookTangLau.Properties.DataSource = db.mbTangLaus.Where(q => q.mbKhoiNha.MaTN == (byte?)lookTN.EditValue).Select(p => new { p.MaTL, p.TenTL });
            lookMatBang.Properties.DataSource = db.mbMatBangs.Where(q => q.mbTangLau.mbKhoiNha.MaTN == (byte?)lookTN.EditValue).Select(p => new { p.MaMB, p.MaSoMB });
            MaTN = (byte?)lookTN.EditValue;
            STT = 1;
            LoadLookTS();
        }
    }
}