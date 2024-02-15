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
using DevExpress.XtraTreeList.Nodes;

namespace KyThuat.DauMucCongViec
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public long? MaCVBT { get; set; }
        public byte? NguonCV { get; set; }
        public string MaTQ { get; set; }
        public int? btHanNgay { get; set; }
        public int? MaNguonCV { get; set; }
        public List<int> ListTS { get; set; }

        public tnNhanVien objnhanvien;
        btDauMucCongViec objCVBT;
        MasterDataContext db;
        int STT { get; set; }
        public bool IsSave { get; set; }

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            lookTaiSan.EditValueChanged += new EventHandler(lookTaiSan_EditValueChanged);
        }

        public void EnableStatus()
        {
            btnSave.Enabled = false;
        }

        string getNewMaBT()
        {
            string MaBT = "";
            db.btDauMucCongViec_getNewMaBT(ref MaBT);
            return db.DinhDang(32, int.Parse(MaBT));
        }

        void LoadLookTS()
        {
            switch (STT)
            {
                case 1:
                    lookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == (byte?)lookTN.EditValue & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.MaTT });
                    break;
                case 2:
                    lookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.mbTangLau.MaKN == (int?)lookKhoiNha.EditValue & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.MaTT });
                    break;
                case 3:
                    lookTaiSan.DataSource = db.tsTaiSans.Where(p => p.mbMatBang.MaTL == (int?)lookTangLau.EditValue & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.MaTT });
                    break;
                case 4:
                    lookTaiSan.DataSource = db.tsTaiSans.Where(p => p.MaMB == (int?)lookMatBang.EditValue & (p.IsGhiGiam != true | p.IsGhiGiam == null))
                    .Select(p => new { p.MaTS, p.ID, p.TenTS, p.tsLoaiTaiSan.TenLTS, p.MaTT });
                    break;
            }

        }

        void LoadData()
        {
            if (MaCVBT == null)
            {
                objCVBT=new btDauMucCongViec();
                db.btDauMucCongViecs.InsertOnSubmit(objCVBT);
                txtMaSoCV.Text = getNewMaBT();
                //
                switch (NguonCV)
                {
                    case 0:
                        cbeNguonCV.SelectedIndex = 3;
                        break;
                    case 1:
                        var objVH = db.tqThamQuans.SingleOrDefault(p => p.MaTQ == MaTQ);
                        cbeNguonCV.SelectedIndex = 1;
                        dateThoiGianGN.EditValue = DateTime.Now;
                        lookTrangThai.EditValue = 1;
                        lookTN.EditValue = objVH.MaTN;
                        lookKhoiNha.EditValue = objVH.MaKN;
                        lookTangLau.EditValue = objVH.MaTL;
                        lookMatBang.EditValue = objVH.MaMB;
                       // List<tsTaiSan> ListTaiSan = new List<tsTaiSan>();
                        //tsTaiSan obj;
                        for (int i = 0; i < ListTS.Count; i++)
                        {
                            btDauMucCongViec_TaiSan item = new btDauMucCongViec_TaiSan();
                            item.MaTS = ListTS[i];
                            item.DienGiai = db.tqTaiSans.SingleOrDefault(p => p.MaTQ == MaTQ & p.MaTS == ListTS[i]).DienGiai;
                            objCVBT.btDauMucCongViec_TaiSans.Add(item);
                        }
                        break;
                    case 2:
                        var objBT = db.khbtKeHoaches.SingleOrDefault(p => p.MaKH == MaNguonCV);
                        cbeNguonCV.SelectedIndex = 2;
                        dateThoiGianGN.EditValue = DateTime.Now;
                        lookTrangThai.EditValue = 1;
                        lookTN.EditValue = objBT.MaTN;
                        var khTS=db.khbtTaiSans.Where(p=>p.MaKH==MaNguonCV).ToList();
                        //for(int i=0)
                        break;

                }

            }
            else
            {
                objCVBT = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaCVBT);
                txtMaSoCV.Text = objCVBT.MaSoCV;
                txtMoTa.Text = objCVBT.MoTa;
                dateThoiGianGN.EditValue = (DateTime?)objCVBT.ThoiGianGhiNhan;
                dateThoiGianHT.EditValue = (DateTime?)objCVBT.ThoiGianHT;
                dateThoiGianTH.EditValue = (DateTime?)objCVBT.ThoiGianTH;
                dateThoiGianXN.EditValue = (DateTime?)objCVBT.ThoiGianXacNhan;
                lookHinhThucTH.EditValue = objCVBT.MaHT;
                lookTrangThai.EditValue = objCVBT.TrangThaiCV;
                lookTN.EditValue = objCVBT.MaTN;
                lookKhoiNha.EditValue = objCVBT.MaKN;
                lookTangLau.EditValue = objCVBT.MaTL;
                lookMatBang.EditValue = objCVBT.MaMB;
                cbeNguonCV.SelectedIndex = (int)objCVBT.NguonCV;
                spinTienDo.EditValue = objCVBT.TienDoTH;
                chkHoanThanh.Checked = objCVBT.HoanThanh == true ? true : false;
                spinChiPhi.EditValue = (float?)objCVBT.ChiPhi;
                dateDuKienTH.EditValue = (DateTime?)objCVBT.ThoiGianTheoLich;
                dateHetHan.EditValue = (DateTime?)objCVBT.ThoiGianHetHan;
                timeGioDK.EditValue = objCVBT.GioTHDuKien;
                //timeGioTH.EditValue = objCVBT.ThoiGianTH;
                timeGioXN.EditValue = objCVBT.GioXacNhan;

            }
            LoadLookTS();
            gcTaiSan.DataSource = objCVBT.btDauMucCongViec_TaiSans;
            gcThietBi.DataSource = objCVBT.btDauMucCongViec_ThietBis;
            gcNhanVien.DataSource = objCVBT.btDauMucCongViec_NhanViens;
            gcDoiTac.DataSource = objCVBT.btDauMucCongViec_DoiTacs;

        }

        void SaveData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                objCVBT.MaSoCV = txtMaSoCV.Text;
                objCVBT.MoTa = txtMoTa.Text.Trim();
                objCVBT.MaTN = (byte?)lookTN.EditValue;
                objCVBT.MaKN = (int?)lookKhoiNha.EditValue;
                objCVBT.MaTL = (int?)lookTangLau.EditValue;
                objCVBT.MaMB = (int?)lookMatBang.EditValue;
                objCVBT.NguonCV = Convert.ToByte(cbeNguonCV.SelectedIndex);
                objCVBT.MaCVVH = MaTQ;
                objCVBT.TrangThaiCV = (byte?)lookTrangThai.EditValue;
                objCVBT.ThoiGianGhiNhan = (DateTime?)dateThoiGianGN.EditValue;
                objCVBT.ThoiGianHT = (DateTime?)dateThoiGianHT.EditValue;
                objCVBT.ThoiGianTH = (DateTime?)dateThoiGianTH.EditValue;
                objCVBT.ThoiGianXacNhan = (DateTime?)dateThoiGianXN.EditValue;
                objCVBT.TienDoTH = (decimal?)spinTienDo.EditValue;
                objCVBT.HoanThanh = (bool)chkHoanThanh.Checked;
                objCVBT.MaMB = (int?)lookMatBang.EditValue;
                objCVBT.ChiPhi = Convert.ToDouble(spinChiPhi.EditValue);
                objCVBT.ThoiGianTheoLich = (DateTime?)dateDuKienTH.EditValue;
                objCVBT.ThoiGianHetHan = (DateTime?)dateHetHan.EditValue;
                var objLS = new btDauMucCongViec_LichSu();
                objLS.DienGiai = txtMoTa.Text.Trim();
                objLS.MaNVCN = objnhanvien.MaNV;
                objLS.NgayCN = DateTime.Now;
                objLS.TienDo = (decimal?)spinTienDo.EditValue;
                objLS.TrangThaiCV = (byte?)lookTrangThai.EditValue;
                objCVBT.btDauMucCongViec_LichSus.Add(objLS);
                objCVBT.GioXacNhan = (DateTime?)timeGioXN.EditValue;
                objCVBT.GioTHDuKien = (DateTime?)timeGioDK.EditValue;
               // objCVBT.GioTH = (DateTime?)timeGioTH.EditValue;
                //objCVBT.TrangThaiCV = (byte?)lookTrangThai.EditValue;
                db.SubmitChanges();
                IsSave = true;
                DialogBox.Alert("Dữ liệu đã được lưu.");
            }
            catch 
            {
                DialogBox.Alert("Dữ liệu không thể lưu. Vui lòng kiểm tra lại!");
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookTN.Properties.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
           // lookThietBi.DataSource=db.tsLoaiTaiSans.Select(p=>new {p.MaLTS,p.TenLTS});
            colMaTB.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objnhanvien);
            lookTaiSan.DataSource = db.tsTaiSans.Where(q => q.IsGhiGiam != true).Select(p => new { p.MaTS, p.TenTS, p.ID });
            lookNhanVienTH.DataSource=db.tnNhanViens.Select(p=>new {p.MaNV, p.HoTenNV});
            lookHinhThucTH.Properties.DataSource=db.btHinhThucs;
            lookTrangThai.Properties.DataSource = db.btCongViecBT_trangThais.Select(p => new { p.MaTT, p.ID, p.TenTT });
            lookDoiTac.DataSource = db.tnNhaCungCaps.Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
            LoadData();
        }

        void lookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit look = (LookUpEdit)sender;
            if (look.EditValue == null)
                return;
            var obj = db.tsTaiSans.Where(p => p.ID == (int?)look.EditValue)
                .Select(q => new { 
                    q.tsDonViSuDung.TenDV,
                    q.mbMatBang.MaSoMB,
                    q.NgayBDSD
            }).SingleOrDefault();
            gvTaiSan.SetFocusedRowCellValue(colDVSD, obj.TenDV);
            gvTaiSan.SetFocusedRowCellValue(colMatBangSD, obj.MaSoMB);
            gvTaiSan.SetFocusedRowCellValue(colNgayBDSD, obj.NgayBDSD);
        }

        private void lookToaNha_EditValueChanged(object sender, EventArgs e)
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
            STT = 3;
            LoadLookTS();
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            if (lookMatBang.EditValue == null)
                return;
            STT = 4;
            LoadLookTS();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvThietBi_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong" | e.Column.FieldName == "DonGia")
            {
                if (grvThietBi.GetFocusedRowCellValue("SoLuong") != null &&
                    grvThietBi.GetFocusedRowCellValue("DonGia") != null)
                {
                    decimal ThanhTien = (int)grvThietBi.GetFocusedRowCellValue("SoLuong") *
                        (decimal)grvThietBi.GetFocusedRowCellValue("DonGia");
                    grvThietBi.SetFocusedRowCellValue("ThanhTien", ThanhTien);
                }
            }
        }

        private void grvThietBi_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvThietBi.DeleteSelectedRows();
        }

        private void gvNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                gvNhanVien.DeleteSelectedRows();
        }

        private void grvDoiTac_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvDoiTac.DeleteSelectedRows();
        }

        private void gvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                gvTaiSan.DeleteSelectedRows();
        }

        private void grvThietBi_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                sckhThietBi objTB = (sckhThietBi)grvThietBi.GetRow(e.RowHandle);
                objTB.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);                
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}