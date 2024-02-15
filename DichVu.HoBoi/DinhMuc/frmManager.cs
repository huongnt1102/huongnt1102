using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Data;

namespace DichVu.HoBoi.DinhMuc
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();

                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var maTN = (byte?)itemToaNha.EditValue ?? 0;
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcDinhMuc.DataSource = db.dvhbDinhMucs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTao.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayTao.Value, denNgay) >= 0 & p.MaTN == maTN)
                        .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.TenDM,
                            p.TuNgay,
                            p.DenNgay,
                            p.dvhbLoaiThe.TenLT,
                            p.MucPhi,
                            p.tnNhanVien.HoTenNV,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN
                        }).ToList();
                }
                else
                {
                    var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                    if (GetNhomOfNV.Count > 0)
                    {
                        var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                        gcDinhMuc.DataSource = db.dvhbDinhMucs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTao.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayTao.Value, denNgay) >= 0 &
                                p.MaTN == objnhanvien.MaTN &
                                GetListNV.Contains(p.MaNV.Value))
                        .OrderByDescending(p => p.NgayTao).AsEnumerable()
                       .Select((p, index) => new
                       {
                           STT = index + 1,
                           p.ID,
                           p.TenDM,
                           p.TuNgay,
                           p.DenNgay,
                           p.dvhbLoaiThe.TenLT,
                           p.MucPhi,
                           p.tnNhanVien.HoTenNV,
                           p.NgayTao,
                           HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                           p.NgayCN
                       }).ToList();
                    }
                    else
                    {
                        gcDinhMuc.DataSource = db.dvhbDinhMucs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayTao.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayTao.Value, denNgay) >= 0 &
                                p.MaTN == objnhanvien.MaTN &
                                p.MaNV == objnhanvien.MaNV)
                        .OrderByDescending(p => p.NgayTao).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.TenDM,
                            p.TuNgay,
                            p.DenNgay,
                            p.dvhbLoaiThe.TenLT,
                            p.MucPhi,
                            p.tnNhanVien.HoTenNV,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN
                        }).ToList();
                    }
                }

                wait.Close();
                wait.Dispose();
            }
            else
            {
                gcDinhMuc.DataSource = null;
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

        private void frmManager_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            
            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            try
            {
                if (!objnhanvien.IsSuperAdmin.Value)
                {
                    itemToaNha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    itemToaNha.EditValue = objnhanvien.MaTN;
                }
            }
            catch { }
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

        void DeleteSelected()
        {
            int[] indexs = grvDinhMuc.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Định mức], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                List<dvhbDinhMuc> lst = new List<dvhbDinhMuc>();
                foreach (int i in indexs)
                {
                    dvhbDinhMuc objNK = db.dvhbDinhMucs.Single(p => p.ID == (int)grvDinhMuc.GetRowCellValue(i, "ID"));
                    lst.Add(objNK);
                }
                db.dvhbDinhMucs.DeleteAllOnSubmit(lst);
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                LoadData();
            }
            catch
            {
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSelected();
        }

        private void grvDinhMuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelected();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDinhMuc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Địch mức], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { objNV = objnhanvien, KeyID = (int)grvDinhMuc.GetFocusedRowCellValue("ID") })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte?)itemToaNha.EditValue ?? 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Toà nhà], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { objNV = objnhanvien })
            {
                frm.MaTN = maTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDinhMuc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Định mức], xin cảm ơn.");
                return;
            }

            var selectedNK = new List<int>();
            foreach (int row in grvDinhMuc.GetSelectedRows())
            {
                selectedNK.Add((int)grvDinhMuc.GetRowCellValue(row, colID));
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();

                var ts = db.dvhbDinhMucs
                    .Where(p => selectedNK.Contains(p.ID))
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.TenDM,
                            p.TuNgay,
                            p.DenNgay,
                            p.dvhbLoaiThe.TenLT,
                            p.MucPhi,
                            p.tnNhanVien.HoTenNV,
                            p.NgayTao,
                            HoTenNVCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : "",
                            p.NgayCN
                        }).OrderBy(p=>p.NgayTao).ToList();
                dt = SqlCommon.LINQToDataTable(ts);
                ExportToExcel.exportDataToExcel("Danh sách định mức phí thẻ hồ bơi", dt);
            }
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}