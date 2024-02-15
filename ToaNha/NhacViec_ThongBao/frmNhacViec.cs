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

namespace ToaNha.NhacViec_ThongBao
{
    public partial class frmNhacViec : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        public frmNhacViec()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmNhacViec_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadKBC();
        }

        private void LoadKBC()
        {
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                using (MasterDataContext db = new MasterDataContext())
                {
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcNhacViec.DataSource = db.tnNhacViecs
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayGui.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayGui.Value, denNgay) >= 0)
                            .OrderByDescending(p => p.NgayGui)
                            .Select(p => new
                            {
                                p.MaNhacViec,
                                p.NgayGui,
                                NguoiGui = p.tnNhanVien.HoTenNV,
                                p.NoiDung
                            });
                    }
                    else
                    {
                        gcNhacViec.DataSource = db.tnNhacViecs
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayGui.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayGui.Value, denNgay) >= 0 & p.NguoiGui == objnhanvien.MaNV)
                            .OrderByDescending(p => p.NgayGui)
                            .Select(p => new
                            {
                                p.MaNhacViec,
                                p.NgayGui,
                                NguoiGui = p.tnNhanVien.HoTenNV,
                                p.NoiDung
                            });
                    }
                }
            }
            else
            {
                gcNhacViec.DataSource = null;
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

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhacViec.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn thông báo");
                return;
            }
            try
            {
                int[] lsttb = grvNhacViec.GetSelectedRows();
                if (lsttb.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn nhắc việc cần xóa");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;


                using (MasterDataContext db = new MasterDataContext())
                {
                    foreach (int i in lsttb)
                    {
                        var objtb = db.tnNhacViecs.Single(p => p.MaNhacViec == (int)grvNhacViec.GetRowCellValue(i, "MaNhacViec"));
                        db.tnNhacViecs.DeleteOnSubmit(objtb);
                    }
                    db.SubmitChanges();
                }
                DialogBox.Error("Xóa thành công");
                LoadData();
            }
            catch
            {
                DialogBox.Error("Xóa không thành công");
                return;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (NhacViec_ThongBao.frmNhacViecEdit frm = new frmNhacViecEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhacViec.GetFocusedRowCellValue("MaNhacViec") == null)
            {
                DialogBox.Alert("Chọn mục cần sửa");
                return;
            }

            var objnhacviec = db.tnNhacViecs.Single(p=>p.MaNhacViec==(int)grvNhacViec.GetFocusedRowCellValue("MaNhacViec"));
            using (NhacViec_ThongBao.frmNhacViecEdit frm = new frmNhacViecEdit() { objnhacviec = objnhacviec, objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }
    }
}