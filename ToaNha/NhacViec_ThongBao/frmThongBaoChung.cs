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
    public partial class frmThongBaoChung : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        public frmThongBaoChung()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmThongBaoChung_Load(object sender, EventArgs e)
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
                    gcThongBao.DataSource = db.tnThongBaos
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.TuNgay.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.DenNgay.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayDang)
                        .Select(p => new
                        {
                            p.MaThongBao,
                            p.TuNgay,
                            p.DenNgay,
                            p.IsEnable,
                            p.tnNhanVien.HoTenNV,
                            p.tnNhanVien.MaSoNV,
                            p.TieuDe,
                            p.NoiDung
                        });
                }
            }
            else
            {
                gcThongBao.DataSource = null;
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
        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmThongBaoEdit frm = new frmThongBaoEdit() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvThongBao.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn thông báo");
                return;
            }
            int matb = int.Parse(grvThongBao.GetFocusedRowCellValue("MaThongBao").ToString());

            frmThongBaoEdit frm = new frmThongBaoEdit() { objnhanvien = objnhanvien };
            frm.objthongbao = db.tnThongBaos.Single(p => p.MaThongBao == matb);
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvThongBao.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn thông báo");
                return;
            }
            try
            {
                int[] lsttb = grvThongBao.GetSelectedRows();
                if (lsttb.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn thông báo cần xóa");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                
                using (MasterDataContext db = new MasterDataContext())
                {
                    foreach (int i in lsttb)
                    {
                        var objtb = db.tnThongBaos.Single(p => p.MaThongBao == (int)grvThongBao.GetRowCellValue(i, "MaThongBao"));
                        db.tnThongBaos.DeleteOnSubmit(objtb);   
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

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
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
    }
}