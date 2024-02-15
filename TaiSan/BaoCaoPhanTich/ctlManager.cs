using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq;
using System.Linq;

namespace TaiSan.BaoCaoPhanTich
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
         int? LoaiBC { get; set; }
        int? ChiNhanh { get; set; }
         int? LoaiTS { get; set; }
        int? DonViSD { get; set; }
     DateTime? TuNgay { get; set; }
       DateTime? DenNgay { get; set; }
       MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
            dateDenNgay.EditValueChanged += new EventHandler(dateDenNgay_EditValueChanged);
            cbmKyBC.EditValueChanged += new EventHandler(cbmKyBC_EditValueChanged);
            
        }

        void cbmKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateTuNgay.EditValueChanged -= new EventHandler(dateTuNgay_EditValueChanged);
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        private void itemFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Controls.Add(popupControlContainer1);
            popupControlContainer1.Show();
        }

        private void panelControl1_Leave(object sender, EventArgs e)
        {
            popupControlContainer1.Hide();
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            lookToaNha.Properties.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            LookDVSD.Properties.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.ID, p.TenDV });
            lookLTS.Properties.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS });
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cbmKyBC.Properties.Items.Add(str);
            SetDate(7);
        }
    }
}
