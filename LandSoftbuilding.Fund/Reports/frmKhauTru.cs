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
using DevExpress.Data.PivotGrid;

namespace LandSoftBuilding.Fund.Reports
{
    public partial class frmKhauTru : DevExpress.XtraEditors.XtraForm
    {
        public frmKhauTru()
        {
            InitializeComponent();
            
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltToaNha = this.GetToaNha();
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                //Nap vào pivot
                pvData.DataSource = (from pt in db.ktttKhauTruThuTruocs
                                     join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                                     join tn in db.tnToaNhas on pt.MaTN equals tn.MaTN
                                     join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV
                                     where ltToaNha.Contains(pt.MaTN) & SqlMethods.DateDiffDay(_TuNgay, pt.NgayCT) >= 0 & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                     orderby pt.NgayCT descending
                                     select new
                                     {
                                         tn.TenTN,
                                         kh.KyHieu,
                                         TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                         pt.SoCT,
                                         pt.NgayCT,
                                         pt.SoTien,
                                         nv.HoTenNV
                                     }).ToList();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        void Print()
        {
            var rpt = new rptKhauTru();
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvData.OptionsView.ShowColumnHeaders = false;
            pvData.OptionsView.ShowDataHeaders = false;
            pvData.OptionsView.ShowFilterHeaders = false;
            pvData.SavePivotGridToStream(stream);
            pvData.OptionsView.ShowColumnHeaders = true;
            pvData.OptionsView.ShowDataHeaders = true;
            pvData.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmKhauTru_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            itemKyBaoCao.EditValue = objKBC.Source[0];
            SetDate(0);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void pvData_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = "Tổng cộng";
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
    }
}