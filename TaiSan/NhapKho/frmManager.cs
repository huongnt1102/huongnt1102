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

namespace TaiSan.NhapKho
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcPNK.DataSource = db.nkNhapKhos
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayNK.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayNK.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayNK).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaNK,
                                p.NgayNK,
                                p.MaSoNK,
                                TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                TenKho = p.NhapVaoKho != null ? p.KhoHang.TenKho : ""
                            }).ToList();
                    }
                    else
                    {
                        gcPNK.DataSource = db.nkNhapKhos
                            .Where(p => p.MaTN == objnhanvien.MaTN &
                                    SqlMethods.DateDiffDay(tuNgay, p.NgayNK.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayNK.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayNK).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaNK,
                                p.NgayNK,
                                p.MaSoNK,
                                TenNCC = p.MaNCC == null ? "" : p.tnNhaCungCap.TenNCC,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                TenKho = p.NhapVaoKho != null ? p.KhoHang.TenKho : ""
                            }).ToList();
                    }
                }
                else
                {
                    gcPNK.DataSource = null;
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        void LoadDetail()
        {
            if (grvPNK.FocusedRowHandle >= 0)
            {
                var MaNK = (int)grvPNK.GetFocusedRowCellValue("MaNK");

                gcTaiSan.DataSource = db.nkTaiSans.Where(p => p.MaNK == MaNK).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.tsLoaiTaiSan.TenLTS,
                        p.SoLuong,
                        p.DonGia,
                        p.DienGiai,
                        TenNCC = p.MaNCC != null ? p.tnNhaCungCap.TenNCC : ""
                    }).ToList();

                ctlTaiLieu1.FormID = 102;
                ctlTaiLieu1.LinkID = MaNK;
                ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                ctlTaiLieu1.objNV = objnhanvien;
                ctlTaiLieu1.TaiLieu_Load();
            }
            else
            {
                gcTaiSan.DataSource = null;
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var MaNKs = "|";
            int[] indexs = grvPNK.GetSelectedRows();

            foreach (int i in indexs)
                MaNKs += grvPNK.GetRowCellValue(i, "MaNK") + "|";

            db.nkTaiSans.DeleteAllOnSubmit(db.nkTaiSans.Where(p => SqlMethods.Like(MaNKs, "%" + p.MaNK + "%")));
            db.nkNhapKhos.DeleteAllOnSubmit(db.nkNhapKhos.Where(p => SqlMethods.Like(MaNKs, "%" + p.MaNK + "%")));
            db.Khos.DeleteAllOnSubmit(db.Khos.Where(p => SqlMethods.Like(MaNKs, "%" + p.MaNK + "%")));
            db.SubmitChanges();

            grvPNK.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPNK.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng mục cần sửa");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objNK = db.nkNhapKhos.Single(p => p.MaNK == (int)grvPNK.GetFocusedRowCellValue("MaNK")) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                    LoadDetail();
                }
            }
        }

        private void grvPNK_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void btnInHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPNK.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu nhập kho");
                return;
            }
            using (var frm = new Phieu.frmPrintControl(Phieu.EnumPhieu.PhieuNhapKho,(int)grvPNK.GetFocusedRowCellValue("MaNK")))
            {
                frm.ShowDialog();
            }

        }
    }
}