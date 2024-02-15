using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace KyThuat.DauMucCongViec
{
    public partial class frmPrinCV : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        public int MauSo { get; set; }
        public byte KindID;
        public frmPrinCV()
        {
            InitializeComponent();
            db = new MasterDataContext();
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

        private void BaoCao_Nap()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            byte MaTN = itemToaNha.EditValue == null ? (byte)0 : Convert.ToByte(itemToaNha.EditValue);
            var NguonCV = (itemNguonCV.EditValue == "YCKH" ? 0 : (itemNguonCV.EditValue == "HTVH" ? 1 : (itemNguonCV.EditValue == "LBT-TSCD" ? 2 : -1)));
            var wait = DialogBox.WaitingForm();
            try
            {
                switch (MauSo)
                {
                    case 1:
                        // var MaHT = itemHeThong.EditValue == null ? 0 : (int)itemHeThong.EditValue;
                      //  var HT2 = itemMultiHeThong.EditValue.ToString();
                        //  var rpt = new rptBaoTriTaiSan(tuNgay, denNgay, MaTN, NguonCV,HT2, chkHeThong.GetDisplayText(itemMultiHeThong.EditValue));
                        var rpt = new rptBaoTriTaiSan(tuNgay, denNgay, MaTN, NguonCV);
                        rpt.CreateDocument();
                        printControl1.PrintingSystem = rpt.PrintingSystem;
                        break;
                    case 2:
                        var rpt2 = new rptHoatDongKyThuat(tuNgay, denNgay, MaTN, NguonCV);
                        rpt2.CreateDocument();
                        printControl1.PrintingSystem = rpt2.PrintingSystem;
                        break;
                }

            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void itemKyBC_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemNguonCV_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void frmPrinCV_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN, p.TenVT});
            lookHeThong.DataSource = db.tsHeThongs;
            chkHeThong.DataSource = db.tsHeThongs;
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
            BaoCao_Nap();
            if (MauSo == 2)
            {
                itemNguonCV.EditValue = "LBT-TSCD";
                cbmNguonCV.ReadOnly = true;
            }
            else
                itemNguonCV.EditValue = "< Tất cả >";
        }
    }
}