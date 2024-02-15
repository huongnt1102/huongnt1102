using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using LandsoftBuilding.Receivables.Reports.Class;

namespace LandsoftBuilding.Receivables.Reports.TienCoc
{
    public partial class frmTienCoc_Form1 : DevExpress.XtraEditors.XtraForm
    {
        public frmTienCoc_Form1()
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
                var ltToaNha = new List<byte?>();
                var arrMaTN = (itemToaNha.EditValue ?? "").ToString();
                //var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                var result = Library.Class.Connect.QueryConnect.QueryData<BaoCaoTienCoc_Form1Model>("bcDatCoc_Form1",
                    new
                    {
                        TowerArr = arrMaTN,
                        DateTo = _DenNgay,
                        Type = 0
                    });

                gc.DataSource = result.ToList();

            }
            catch(System.Exception ex)
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
            Commoncls.ExportExcel(gc);
        }

        private void frmTienCoc_Form1_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();
            //itemHinhThucThu.EditValue = "Tất cả";
            //KyBaoCao objKBC = new KyBaoCao();
            //objKBC.Initialize(cmbKyBaoCao);
            //var index = 3;
            //itemKyBaoCao.EditValue = objKBC.Source[index];
            //SetDate(index);

            itemDenNgay.EditValue = System.DateTime.Now;
            LoadData();

           // pvHoaDon.OptionsView.ShowColumnTotals = false;
        }
        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void pvHoaDon_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
            {
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");
            }
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
    }
}