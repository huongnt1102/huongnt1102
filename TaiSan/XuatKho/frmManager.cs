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

namespace TaiSan.XuatKho
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
                        gcPXK.DataSource = db.xkXuatKhos
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayXK.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayXK.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayXK).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaXK,
                                p.NgayXK,
                                p.MaSoXK,
                                p.NguoiNhan,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV
                            }).ToList();
                    }
                    else
                    {
                        gcPXK.DataSource = db.xkXuatKhos
                            .Where(p => p.MaTN == objnhanvien.MaTN &
                                    SqlMethods.DateDiffDay(tuNgay, p.NgayXK.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayXK.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayXK).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaXK,
                                p.NgayXK,
                                p.MaSoXK,
                                p.NguoiNhan,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV
                            }).ToList();
                    }
                }
                else
                {
                    gcPXK.DataSource = null;
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
            if (grvPXK.FocusedRowHandle >= 0)
            {
                var MaXK = (int)grvPXK.GetFocusedRowCellValue("MaXK");

                gcTaiSan.DataSource = db.xkTaiSans.Where(p => p.MaXK == MaXK).AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.Kho.tsLoaiTaiSan.TenLTS,
                        p.SoLuong,
                        p.DonGia,
                        p.DienGiai,
                        TenMD = p.MaMD == null ? "" : p.xkMucDich.TenMD,
                        p.MucDichKhac,
                        TenNCC = p.MaNCC != null ? p.tnNhaCungCap.TenNCC : ""
                    }).ToList();

                ctlTaiLieu1.FormID = 44;
                ctlTaiLieu1.LinkID = MaXK;
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

            var MaXKs = "|";
            int[] indexs = grvPXK.GetSelectedRows();

            foreach (int i in indexs)
                MaXKs += grvPXK.GetRowCellValue(i, "MaXK") + "|";

            db.xkTaiSans.DeleteAllOnSubmit(db.xkTaiSans.Where(p => SqlMethods.Like(MaXKs, String.Format("%{0}%", p.MaXK))));
            db.xkXuatKhos.DeleteAllOnSubmit(db.xkXuatKhos.Where(p => SqlMethods.Like(MaXKs, String.Format("%{0}%", p.MaXK))));
            db.SubmitChanges();

            grvPXK.DeleteSelectedRows();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPXK.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn mục cần sửa");
                return;
            }
            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objXK = db.xkXuatKhos.Single(p => p.MaXK == (int)grvPXK.GetFocusedRowCellValue("MaXK")) })
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

        private void grvPXK_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void btnInPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPXK.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu nhập kho");
                return;
            }
            using (var frm = new Phieu.frmPrintControl(Phieu.EnumPhieu.PhieuXuatKho, (int)grvPXK.GetFocusedRowCellValue("MaXK")))
            {
                frm.ShowDialog();
            }
        }
    }
}