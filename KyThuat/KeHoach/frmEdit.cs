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
using System.Data.Linq.SqlClient;

namespace KyThuat.KeHoach
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public khbtKeHoach objKH;
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        bool check = false;

        byte? MaTN { get; set; }
        int? MaKN { get; set; }
        int? MaTL { get; set; }
        int? MaMB { get; set; }
        byte STT { get; set; }
        public bool IsEdit = true;
      //  btDauMucCongViec objCVBT;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        void LoadLookTS()
        {
            var MaHT = lookHeThong.EditValue == null ? 0 : Convert.ToInt32(lookHeThong.EditValue);
            switch (STT)
            {
                case 1:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == MaTN & (p.IsGhiGiam != true | p.IsGhiGiam == null) & (p.MaHT == MaHT | MaHT == 0))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
                case 2:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.mbTangLau.MaKN == MaKN & (p.IsGhiGiam != true | p.IsGhiGiam == null) & (p.MaHT == MaHT | MaHT == 0))
                   .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
                case 3:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.MaTL == MaTL & (p.IsGhiGiam != true | p.IsGhiGiam == null) & (p.MaHT == MaHT | MaHT == 0))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
                case 4:
                    slookTaiSan.DataSource = db.tsTaiSans.Where(p => p.MaMB == MaMB & (p.IsGhiGiam != true | p.IsGhiGiam == null) & (p.MaHT == MaHT | MaHT == 0))
                   .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.tsTinhTrang.TenTT, MaSoMB = p.MaMB == null ? "" : p.mbMatBang.MaSoMB, p.NhaSanXuat, TenDVSD = p.MaDVSD == null ? "" : p.tsDonViSuDung.TenDV });
                    break;
            }
        }

        string getNewMaKH()
        {
            string MaKH = "";
            db.khbtKeHoach_getNewMaKH(ref MaKH);
            return db.DinhDang(20, int.Parse(MaKH));
            
        }

        void LoadData()
        {

            colMaTB.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objnhanvien);
            lookTN.Properties.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            slookTaiSan.DataSource = db.tsTaiSans.Where(q => q.IsGhiGiam != true)
                    .Select(p => new { p.MaTS, p.TenTS, p.ID, p.tsLoaiTaiSan.TenLTS, p.MaTT });
            LoadLookTS();
            lookHeThong.Properties.DataSource = db.tsHeThongs;
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
                lookDoiTac.DataSource = db.tnNhaCungCaps
                    .Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
                lookDoiTac.DataSource = db.tnNhaCungCaps//.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
            }
         //   lookTrangThai.DataSource = db.tsTrangThais;
            lookNhanSu.DataSource = lookNhanVien.Properties.DataSource;
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Monday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Tuesday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Wednesday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Thursday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Friday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Saturday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Sunday);

            for (int i = 1; i <= 12; i++)
            {
                ckMonthOfYear.Properties.Items.Add(i);
            }

            for (int i = 1; i <= 31; i++)
            {
                cNgayTrongThang.Properties.Items.Add(i);
            }

            if (this.objKH != null)
            {
                //objCVBT = db.btDauMucCongViecs.SingleOrDefault(p => p.MaNguonCV == objKH.MaKH);
                check = true;
                objKH = db.khbtKeHoaches.Single(p => p.MaKH == objKH.MaKH);
                txtMaSoKH.Text = objKH.MaSoKH;
                dateNgayKH.DateTime = (DateTime)objKH.NgayKH;
                lookNhanVien.EditValue = objKH.MaNV;
                txtChiPhi.Text = objKH.ChiPhi.ToString();
                txtDienGiai.Text = objKH.DienGiai;
                dateTuNgay.DateTime = objKH.TuNgay ?? DateTime.Now;
                dateDenNgay.DateTime = objKH.DenNgay ?? DateTime.Now;
                lookTN.EditValue = objKH.MaTN;
                lookKhoiNha.EditValue = objKH.MaKN;
                lookTangLau.EditValue = objKH.MaTL;
                lookMatBang.EditValue = objKH.MaMB;
                ckDayOfWeek.SetEditValue(objKH.DayOfWeeks);
                ckMonthOfYear.SetEditValue(objKH.MonthOfYears);
                cNgayTrongThang.SetEditValue(objKH.DayOfMonth);
                lookHeThong.EditValue = (int?)objKH.MaHT;
                chkLapLai.EditValue = objKH.IsLoop.GetValueOrDefault();
                switch (objKH.LoaiLichBT)
                {
                    case 1:
                        rdHangNgay.Checked = true;
                        break;
                    case 2:
                        rdHangTuan.Checked = true;
                        break;
                    case 3:
                        rdHangThang.Checked = true;
                        break;
                    case 4:
                        rdHangNam.Checked = true;
                        break;
                }
            }
            else
            {
                objKH = new khbtKeHoach();
                db.khbtKeHoaches.InsertOnSubmit(objKH);
                txtMaSoKH.Text = getNewMaKH();
                dateNgayKH.DateTime = db.GetSystemDate();
                lookNhanVien.EditValue = objnhanvien.MaNV;
                objKH.MaTT = 1;
            }
            dateNgayKH.DateTime = db.GetSystemDate();
            gcTaiSan.DataSource = objKH.khbtTaiSans;
            gcThietBi.DataSource = objKH.khbtThietBis;
            gcDoiTac.DataSource = objKH.khbtDoiTacs;
            gcNhanSu.DataSource = objKH.khbtNhanViens;
        }

     //   void EditTaiSanHeThong()
        //{
        //    if (rdTaiSan.Checked)
        //    {
        //        lookHeThong.EditValue = null;
        //        lookHeThong.Enabled = false;
        //        lookTN.Enabled = lookKhoiNha.Enabled = lookTangLau.Enabled = lookMatBang.Enabled = true;
                
        //    }
        //    else
        //    {
        //        lookTN.Enabled = lookHeThong.Enabled = true;
        //        lookTN.EditValue = lookKhoiNha.EditValue = lookTangLau.EditValue = lookMatBang.EditValue = null;
        //        lookKhoiNha.Enabled = lookTangLau.Enabled = lookMatBang.Enabled = false;
        //    }
        //    grvTaiSan.SelectAll();
        //    grvTaiSan.DeleteSelectedRows();
        //}

        private void frmEdit_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = IsEdit;
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var TuNgay = (DateTime?)dateTuNgay.EditValue;
            var DenNgay = (DateTime?)dateDenNgay.EditValue;
            if (grvTaiSan.RowCount <2)
            {
                DialogBox.Alert("Bạn cần chọn tài sản cho kế hoạch bảo trì!");
                grvTaiSan.Focus();
                return;
            }

            if (TuNgay== null | DenNgay == null)
            {
                DialogBox.Error("Vui lòng chọn [Ngày bắt đầu], [Ngày kết thúc] hoặc thiết lập kế hoạch định kỳ");
                return;
            }

             if (check == false)
            {
                var ListCheck = db.khbtKeHoaches.Where(p => ((SqlMethods.DateDiffDay(p.TuNgay, TuNgay) >= 0 & SqlMethods.DateDiffDay(TuNgay, p.DenNgay) >= 0) | (SqlMethods.DateDiffDay(p.TuNgay, DenNgay) >= 0
                    & SqlMethods.DateDiffDay(DenNgay, p.DenNgay) >= 0)) && p.khbtTaiSans.FirstOrDefault().MaTS == (int?)grvTaiSan.GetRowCellValue(0, "MaTS")).ToList();
                if (ListCheck.Count > 0)
                {
                    DialogBox.Alert("Dữ liệu không thể lưu vì xảy ra chồng chéo thời gian bảo trì của thiết bị này!");
                    return;
                        
                }
                  
            }


            objKH.MaSoKH = txtMaSoKH.Text;
            objKH.NgayKH = dateNgayKH.DateTime;
            objKH.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objKH.ChiPhi = (decimal)txtChiPhi.EditValue;
           // objKH.IsBTTaiSan = (bool?)rdTaiSan.Checked;
            objKH.DienGiai = txtDienGiai.Text;
            objKH.MaTN = (byte?)lookTN.EditValue;
            objKH.MaKN = (int?)lookKhoiNha.EditValue;
            objKH.MaTL = (int?)lookTangLau.EditValue;
            objKH.MaMB = (int?)lookMatBang.EditValue;
            objKH.MaHT = (int?)lookHeThong.EditValue;
            objKH.TuNgay = dateTuNgay.DateTime;
            objKH.DenNgay = dateDenNgay.DateTime;
            objKH.IsLoop = (bool)chkLapLai.Checked;

            if (rdHangNam.Checked)
            {
                var month = ckMonthOfYear.Properties.GetCheckedItems();
                objKH.LoaiLichBT = 4;
                objKH.MonthOfYears = month.ToString();
                objKH.DayOfWeeks = null;
                objKH.DayOfMonth = null;

            }
            if (rdHangNgay.Checked)
            {
                objKH.LoaiLichBT = 1;
                objKH.MonthOfYears = null;
                objKH.DayOfWeeks = null;
                objKH.DayOfMonth = null;
                
            }
            if (rdHangTuan.Checked)
            {
                var day = ckDayOfWeek.Properties.GetCheckedItems();
                objKH.LoaiLichBT = 2;
                objKH.DayOfWeeks = day.ToString();
                objKH.MonthOfYears = null;
                objKH.DayOfMonth = null;
            }
            if (rdHangThang.Checked)
            {
                var day = cNgayTrongThang.Properties.GetCheckedItems();
                objKH.LoaiLichBT = 3;
                objKH.DayOfMonth = day.ToString();
                objKH.MonthOfYears = null;
                objKH.DayOfWeeks = null;
            }

            try
            {
                db.SubmitChanges();
                List<khbtTaiSan> listTS = new List<khbtTaiSan>();
                if (grvTaiSan.RowCount < 0)
                    return;
                for (int i = 0; i < grvTaiSan.RowCount -1; i++)
                {
                    var ts = new khbtTaiSan();
                    ts.MaKH = objKH.MaKH;
                    ts.MaTS = (int?)grvTaiSan.GetRowCellValue(i, "MaTS");
                    ts.DienGiai = grvTaiSan.GetRowCellValue(i, "DienGiai").ToString();
                    listTS.Add(ts);
                }
                db.khbtTaiSans.DeleteAllOnSubmit(objKH.khbtTaiSans);
                db.khbtTaiSans.InsertAllOnSubmit(listTS);
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
                this.Close();
            }
            catch(Exception ex)
            {
                DialogBox.Alert(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void slookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit l = (LookUpEdit)sender;
            grvTaiSan.SetFocusedRowCellValue("MaTT", l.GetColumnValue("MaTT"));
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                switch (e.Column.FieldName)
                {
                    case "MaTS":
                        //khbtTaiSan objTS = (khbtTaiSan)grvTaiSan.GetRow(e.RowHandle);
                        //objTS.tsTaiSanSuDung = db.tsTaiSanSuDungs.Single(p => p.MaTS == (int)e.Value);
                        break;
                    case "MaTT":
                        khbtTaiSan objTT = (khbtTaiSan)grvTaiSan.GetRow(e.RowHandle);
                        objTT.tsTrangThai = db.tsTrangThais.Single(p => p.MaTT == (int)e.Value);
                        break;
                }
            }
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        private void grvThietBi_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaTS")
            {
                khbtThietBi objTB = (khbtThietBi)grvThietBi.GetRow(e.RowHandle);
                objTB.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvThietBi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvThietBi.DeleteSelectedRows();
        }

        private void grvDoiTac_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaTS")
            {
                khbtDoiTac objDT = (khbtDoiTac)grvDoiTac.GetRow(e.RowHandle);
                objDT.tnNhaCungCap = db.tnNhaCungCaps.Single(p => p.MaNCC == (int)e.Value);
            }
        }

        private void grvDoiTac_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvDoiTac.DeleteSelectedRows();
        }

        private void grvNhanSu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaTS")
            {
                khbtNhanVien objNV = (khbtNhanVien)grvNhanSu.GetRow(e.RowHandle);
                objNV.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)e.Value);
            }
        }

        private void grvNhanSu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvNhanSu.DeleteSelectedRows();
        }

        private void rdHangTuan_CheckedChanged(object sender, EventArgs e)
        {
            ckDayOfWeek.Properties.ReadOnly = !rdHangTuan.Checked;
            ckMonthOfYear.EditValue = null;
            cNgayTrongThang.EditValue = null;

        }

        private void rdHangThang_CheckedChanged(object sender, EventArgs e)
        {
            cNgayTrongThang.Properties.ReadOnly = !rdHangThang.Checked;
           // cNgayTrongThang.EditValue = null;
            ckMonthOfYear.EditValue = null;
            ckDayOfWeek.EditValue = null;
        }

        private void rdHangNam_CheckedChanged(object sender, EventArgs e)
        {
            ckMonthOfYear.Properties.ReadOnly = !rdHangNam.Checked;
            cNgayTrongThang.EditValue = null;
            ckDayOfWeek.EditValue = null;
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
            lookHeThong.Properties.DataSource = db.tsHeThongs.Where(p => p.MaTN == (byte?)lookTN.EditValue);
            MaTN = (byte?)lookTN.EditValue;
            STT = 1;
            LoadLookTS();
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
            lookMatBang.Properties.DataSource = db.mbMatBangs.Where(q => q.MaTL == (int?)lookTangLau.EditValue).Select(p => new { p.MaMB, p.MaSoMB });
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

        private void rdHangNgay_CheckedChanged(object sender, EventArgs e)
        {
            ckDayOfWeek.EditValue = null;
            ckMonthOfYear.EditValue = null;
            cNgayTrongThang.EditValue = null;
        }

        private void rdTaiSan_CheckedChanged(object sender, EventArgs e)
        {
          //  EditTaiSanHeThong();
        }

        private void lookHeThong_EditValueChanged(object sender, EventArgs e)
        {
            #region code thêm ds tài sản tự động quan trọng ko đc xóa
            //slookTaiSan.DataSource = db.tsTaiSans.Where(p => (p.IsGhiGiam != true | p.IsGhiGiam == null))
            //       .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.MaTT });
            //if (lookHeThong.EditValue == null)
            //    return;
            //gcTaiSan.DataSource = null;
            ////for (int i = 0; i < grvTaiSan.RowCount; i++)
            ////{
            ////    grvTaiSan.FocusedRowHandle = i;
            ////    grvTaiSan.DeleteSelectedRows();
            ////    i--;
            ////}
            //var obj = db.tsTaiSans.Where(p => p.MaHT == (int?)lookHeThong.EditValue).Select(p => p.ID).ToList();
            //List<khbtTaiSan> list = new List<khbtTaiSan>();
            //foreach (var p in obj)
            //{
            //    khbtTaiSan item = new khbtTaiSan();
            //    item.MaKH = objKH.MaKH;
            //    item.MaTS = p;
            //    item.DienGiai = "";
            //    list.Add(item);
            //}
            //gcTaiSan.DataSource = list;
            //grvTaiSan.RefreshData();
            #endregion
            LoadLookTS();

        }

        private void chkLapLai_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLapLai.Checked)
                groupNoiDung.Enabled = true;
            else
                groupNoiDung.Enabled = false;
            
        }
    }
}