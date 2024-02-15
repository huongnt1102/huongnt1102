using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;

namespace AnNinh
{
    public partial class frmAdminLogGhiNhanSuViec : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmAdminLogGhiNhanSuViec()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmGhiNhanSuViec_Load(object sender, EventArgs e)
        {
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[0];
            SetDate(0);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
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
        
        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                gcSuViec.DataSource = db.AnNinhSuViecTrongCas.Where(p =>
                            SqlMethods.DateDiffDay(tuNgay, p.ThoiGianGhiNhan.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.ThoiGianGhiNhan.Value, denNgay) >= 0)
                            .Select(p => new
                            {
                                p.SuViec,
                                p.tnNhanVien.HoTenNV,
                                p.MaSuViec,
                                p.ThoiGianGhiNhan
                            });
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            BaoCao.rptBaoCaoSuViecTrongCa rpt = new BaoCao.rptBaoCaoSuViecTrongCa(tuNgay, denNgay, objnhanvien);
            rpt.ShowPreviewDialog();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Đã ghi nhận lại báo cáo");
            }
            catch
            {
                DialogBox.Alert("Không lưu được dữ liệu, vui lòng thử lại sau");
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                grvSuViec.DeleteSelectedRows();
                itemLuu_ItemClick(null, null);
            }
        }

    }
}