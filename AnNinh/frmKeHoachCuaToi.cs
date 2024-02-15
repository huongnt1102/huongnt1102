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

namespace AnNinh
{
    public partial class frmKeHoachCuaToi : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public bool ViewLog = false;
        MasterDataContext db;

        public frmKeHoachCuaToi()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmShowKeHoach_Load(object sender, EventArgs e)
        {
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[0];
            SetDate(0);
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
        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value & ViewLog)
                {
                    gcNV.DataSource = db.AnNinhNhiemVuNhanViens
                                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.LastUpdated.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.LastUpdated.Value, denNgay) >= 0)
                                .Select(p => new
                                {
                                    p.MaNhiemVu,
                                    p.AnNinhKeHoachNoiDung.DienGiai,
                                    p.DaThucHien,
                                    DienGiaiRG = p.AnNinhKeHoachNoiDung.DienGiai.Length < 50 ? p.AnNinhKeHoachNoiDung.DienGiai.Substring(0, 20) : p.AnNinhKeHoachNoiDung.DienGiai.Substring(0, 50),
                                    GhiChu = p.DienGiai
                                });
                }
                else
                {
                    gcNV.DataSource = db.AnNinhNhiemVuNhanViens
                                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.LastUpdated.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.LastUpdated.Value, denNgay) >= 0 &
                                    p.MaNV == objnhanvien.MaNV)
                                .Select(p => new
                                {
                                    p.MaNhiemVu,
                                    p.AnNinhKeHoachNoiDung.DienGiai,
                                    p.DaThucHien,
                                    DienGiaiRG = p.AnNinhKeHoachNoiDung.DienGiai.Length < 50 ? p.AnNinhKeHoachNoiDung.DienGiai.Substring(0, 20) : p.AnNinhKeHoachNoiDung.DienGiai.Substring(0, 50),
                                    GhiChu = p.DienGiai
                                });
                }
            }
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDaHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNV.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn nhiệm vụ");
                return;
            }

            try
            {
                int MaNV = (int)grvNV.GetFocusedRowCellValue(colMaNV);

                AnNinhNhiemVuNhanVien obj = db.AnNinhNhiemVuNhanViens.Single(p => p.MaNhiemVu == MaNV);
                frmGhiChu frmgc = new frmGhiChu() { NoiDung = obj.DienGiai };
                frmgc.ShowDialog();

                obj.DaThucHien = true;
                obj.DienGiai = frmgc.NoiDung;
                db.SubmitChanges();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, vui lòng thử lại sau!");
            }
        }

        private void itemChuaHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNV.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn nhiệm vụ");
                return;
            }

            try
            {
                int MaNV = (int)grvNV.GetFocusedRowCellValue(colMaNV);

                AnNinhNhiemVuNhanVien obj = db.AnNinhNhiemVuNhanViens.Single(p => p.MaNhiemVu == MaNV);
                frmGhiChu frmgc = new frmGhiChu() { NoiDung = obj.DienGiai };
                frmgc.ShowDialog();

                obj.DaThucHien = false;
                obj.DienGiai = frmgc.NoiDung;
                db.SubmitChanges();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, vui lòng thử lại sau!");
            }
        }
    }
}