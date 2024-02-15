using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class frmKyBaoCaoToaNha : Building.PrintControls.PrintFilterForm
    { 
        public DateTime tuNgay, denNgay;
        MasterDataContext db = new MasterDataContext();
        public int CateID
        {
            get;
            set;
        }
        public byte IDTN
        {
            get;
            set;
        }
        public frmKyBaoCaoToaNha()
        {
            InitializeComponent();
            cbbKyBaoCao.EditValueChanged+=new EventHandler(cbbKyBaoCao_EditValueChanged);
            btnOK.Click+=new EventHandler(btnOK_Click);
            btnCancel.Click+=new EventHandler(btnCancel_Click);
        }

        void  btnCancel_Click(object sender, EventArgs e)
        {
 	        this.Close();
        }
        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
        }

        private void frmKyBaoCaoToaNha_Load(object sender, EventArgs e)
        {
            var list2 = db.tnToaNhas.Where(p => p.MaTN == IDTN)
                 .Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.Properties.DataSource = list2;
                if (list2.Count > 0)
                    lookUpToaNha.EditValue = list2[0].MaTN;
            

            //Ky bao cao
                KyBaoCao objKBC = new KyBaoCao();
                foreach (string str in objKBC.Source)
                    cbbKyBaoCao.Properties.Items.Add(str);
                cbbKyBaoCao.EditValue = objKBC.Source[3];
                SetDate(3);
        }
        private void cbbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateTuNgay.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Kỳ báo cáo], xin cám ơn.");
                dateTuNgay.Focus();
                return;
            }

            if (dateDenNgay.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Kỳ báo cáo], xin cám ơn.");
                dateDenNgay.Focus();
                return;
            }

            if (lookUpToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cám ơn.");
                lookUpToaNha.Focus();
                return;
            }

            tuNgay = dateTuNgay.DateTime;
            denNgay = dateDenNgay.DateTime;


            switch (CateID)
            {

                case 56:
                      this.PrintControl.Report = 
                    new rptTongHopTienMatNopVeCongTy(tuNgay, denNgay);
                    
                    break;
                case 63:
                    this.PrintControl.Report =
                  new rptTongHopTienChi(tuNgay, denNgay, short.Parse(lookUpToaNha.EditValue.ToString()));
                    break;
                case 64:
                    this.PrintControl.Report =
                  new rptTongHopTonQuy(tuNgay, denNgay, short.Parse(lookUpToaNha.EditValue.ToString()));
                    break;
                case 73:
                    this.PrintControl.Report =
                  new rptXePhatSinhTrongThang(tuNgay, denNgay, IDTN);
                    break;
                case 74:
                    this.PrintControl.Report =
                  new rptXeNgungSDTrongThang(tuNgay, denNgay, IDTN);
                    break;
                case 80:
                    this.PrintControl.Report =
                  new rptBaoCaoXe(tuNgay, denNgay, IDTN);
                    break;
                case 82:
                    this.PrintControl.Report =
                  new rptChiTietQuyTienMat(tuNgay, denNgay, IDTN);
                    break;
                case 83:
                    this.PrintControl.Report =
                  new rptTongHopTienChi(tuNgay, denNgay, IDTN);
                    break;
            }

        }

    }
}
