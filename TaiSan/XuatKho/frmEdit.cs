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

namespace TaiSan.XuatKho
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public xkXuatKho objXK;
        public tnNhanVien objnhanvien;
        public mbMatBang objmatbang;
        bool IsSave = false;
        MasterDataContext db;
        bool EditMode = false;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
            slookTaiSan.EditValueChanged += new EventHandler(slookTaiSan_EditValueChanged);
        }

        void slookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var LookTS = (SearchLookUpEdit)sender;
            if (LookTS.EditValue != null)
            {
                var obj = db.Khos.SingleOrDefault(p => p.ID == (int?)LookTS.EditValue);
                grvTaiSan.SetFocusedRowCellValue("MaNCC", obj.MaNCC);
                grvTaiSan.SetFocusedRowCellValue("SoLuong", obj.SoLuong);
                grvTaiSan.SetFocusedRowCellValue("DonGia", obj.DonGia);
            }
        }

        string getNewMaXK()
        {
            string MaXK = "";
            db.xkXuatKho_getNewMaXK(ref MaXK);
            return db.DinhDang(23, int.Parse(MaXK));
        }

        void Save()
        {
            if (lookMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng nhận");
                return;
            }

            grvTaiSan.UpdateCurrentRow();
            objXK.MaSoXK = txtMaSoXK.Text;
            objXK.NgayXK = dateNgayXK.DateTime;
            objXK.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            if (lookNguoiNhan.EditValue != null)
                objXK.MaNN = (int)lookNguoiNhan.EditValue;
            objXK.NguoiNhan = txtNguoiNhan.Text;
            objXK.DienGiai = txtDienGiai.Text;
            objXK.MaTN = objnhanvien.MaTN;
            objXK.MaMB = (int)lookMatBang.EditValue;

            List<TaiSanXuatKho> ListTSXK = new List<TaiSanXuatKho>();
            if (!EditMode)
            {

                #region Lay ma kho va so luong tren luoi do vao list [ListTSXK]
                List<Kho> ListOnGrid = new List<Kho>();
                for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                {
                    Kho objk = new Kho()
                    {
                        ID = (int)grvTaiSan.GetRowCellValue(i, "KhoID"),
                        SoLuong = (int)grvTaiSan.GetRowCellValue(i, "SoLuong")
                    };
                    ListOnGrid.Add(objk);
                }

                var ListOnGridGroup = ListOnGrid.GroupBy(p => p.ID)
                    .Select(p => new
                    {
                        KhoID = p.Key,
                        SoLuong = p.Sum(s => s.SoLuong)
                    }).ToList();


                ListTSXK = new List<TaiSanXuatKho>();
                foreach (var item in ListOnGridGroup)
                {
                    TaiSanXuatKho objtsxk = new TaiSanXuatKho()
                    {
                        SanPham = item.KhoID,
                        SoLuong = item.SoLuong ?? 0
                    };

                    ListTSXK.Add(objtsxk);
                }
                #endregion

                #region Kiem tra truoc khi submit
                foreach (var item in ListTSXK)
                {
                    var TaiSanTrongKho = db.Khos.Single(p => p.ID == item.SanPham);
                    if (TaiSanTrongKho.SoLuong < item.SoLuong)
                    {
                        string ThongBaoStr = string.Format("Tài sản: {0} - {1} trong kho hiện không đủ để xuất ra, vui lòng kiểm tra lại danh sách", TaiSanTrongKho.MaTS, TaiSanTrongKho.tsLoaiTaiSan.TenLTS);
                        DialogBox.Error(ThongBaoStr);
                        return;
                    }
                }
                #endregion
            }

        Save:
            try
            {
                db.SubmitChanges();
                if (!EditMode)
                {
                    foreach (var item in ListTSXK)
                    {
                        var TaiSanTrongKho = db.Khos.Single(p => p.ID == item.SanPham);
                        if (TaiSanTrongKho.SoLuong >= item.SoLuong)
                        {
                            TaiSanTrongKho.SoLuong = TaiSanTrongKho.SoLuong - item.SoLuong;
                        }
                        else
                        {
                            DialogBox.Error("Tài sản trong kho không đủ");
                            return;
                        }
                        db.SubmitChanges();
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                IsSave = true;
                this.Close();
            }
            catch
            {
                objXK.MaSoXK = getNewMaXK();
                //for (int i = 0; i < grvTaiSan.RowCount - 1; i++)
                //    grvTaiSan.SetRowCellValue(i, "MaXK", objXK.MaXK);
                goto Save;
            }
        }

        string GetKhachHang(int? MaKH)
        {
            if (MaKH == null) return "";
            else
            {
                var obj = db.tnKhachHangs.Single(p => p.MaKH == MaKH);
                return obj.IsCaNhan.Value ? string.Format("{0} {1}", obj.HoKH, obj.TenKH) : obj.CtyTen;
            }
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            slookNhaCungCap.DataSource = db.tnNhaCungCaps;
            lookMucDich.DataSource = db.xkMucDiches;
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN);
            }

            slookTaiSan.DataSource = db.Khos
            .Where(p => p.SoLuong > 0)
            .Select(p => new
            {
                p.ID,
                p.MaTS,
                p.tsTrangThai.TenTT,
                p.tsLoaiTaiSan.TenLTS,
                p.nkNhapKho.MaSoNK,
                p.SoLuong,
                p.NgayNhap,
                p.DonGia
            }).OrderBy(p => p.TenLTS);
            lookNguoiNhan.Properties.DataSource = lookNhanVien.Properties.DataSource;
            lookMatBang.Properties.DataSource = db.mbMatBangs
                .Select(p => new
                {
                    p.MaMB,
                    p.MaSoMB,
                    p.SoNha,
                    KhachHang = GetKhachHang(p.MaKH),
                    p.NgayBanGiao
                });
            if (this.objXK != null)
            {
                EditMode = true;
                objXK = db.xkXuatKhos.Single(p => p.MaXK == objXK.MaXK);
                txtMaSoXK.Text = objXK.MaSoXK;
                dateNgayXK.EditValue = objXK.NgayXK;
                lookNguoiNhan.EditValue = objXK.MaNN;
                txtNguoiNhan.Text = objXK.NguoiNhan;
                lookNhanVien.EditValue = objXK.MaNV;
                txtDienGiai.Text = objXK.DienGiai;
                lookMatBang.EditValue = objXK.MaMB;
            }
            else
            {
                EditMode = false;
                objXK = new xkXuatKho();
                db.xkXuatKhos.InsertOnSubmit(objXK);

                txtMaSoXK.Text = getNewMaXK();
                dateNgayXK.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                if (objmatbang != null) lookMatBang.EditValue = objmatbang.MaMB;
            }

            gcTaiSan.DataSource = objXK.xkTaiSans;
        }

        private void LayDSSPTrongKho()
        {
            var source = db.Khos
                .GroupBy(p => p.MaTS)
                .Select(p => new
                {
                    SanPham = p.Key,
                    SoLuong = p.Sum(s => s.SoLuong)
                });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grvTaiSan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvTaiSan.SetFocusedRowCellValue("MaXK", objXK.MaXK);
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            //{
            //    xkTaiSan objTS = (xkTaiSan)grvTaiSan.GetRow(e.RowHandle);
            //    objTS.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            //}
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvTaiSan.DeleteSelectedRows();
        }

        private void lookNguoiNhan_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiNhan.Text = lookNguoiNhan.GetColumnValue("HoTenNV").ToString();
        }

        private void lookTaiSanTrongKho_EditValueChanged(object sender, EventArgs e)
        {
            var LookTS = (LookUpEdit)sender;
            if (LookTS.EditValue != null)
            {
                var obj = db.Khos.SingleOrDefault(p => p.ID == (int?)LookTS.EditValue);
                grvTaiSan.SetFocusedRowCellValue("MaNCC", obj.MaNCC);
                grvTaiSan.SetFocusedRowCellValue("SoLuong", obj.SoLuong);
                grvTaiSan.SetFocusedRowCellValue("DonGia", obj.DonGia);
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            Save();
            if (IsSave)
                if (DialogBox.Question("Bạn có muốn in phiếu nhập kho này không?") == DialogResult.Yes)
                {
                    using (var frm = new Phieu.frmPrintControl(Phieu.EnumPhieu.PhieuXuatKho, objXK.MaXK))
                    {
                        frm.ShowDialog();
                    }
                }
        }
    }
    class TaiSanXuatKho
    {
        public int SanPham { get; set; }
        public int SoLuong { get; set; }
    }
}