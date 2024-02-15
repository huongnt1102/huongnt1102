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
    public partial class frmKyBaoCaoYeuCauKhieuNaiCuaKH : Building.PrintControls.PrintFilterForm
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
        public frmKyBaoCaoYeuCauKhieuNaiCuaKH()
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

            lkDoUuTien.Properties.DataSource = db.tnycDoUuTiens;
            lkTrangThai.Properties.DataSource = db.tnycTrangThais;
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
            if (string.IsNullOrEmpty(lookUpToaNha.Text))
            {
                DialogBox.Error("Vui lòng chọn loại dịch vụ");
                return;
            }
            //if (lookUpToaNha.EditValue == null)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cám ơn.");
            //    lookUpToaNha.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(lkDoUuTien.Text))
            {
                DialogBox.Error("Vui lòng chọn loại ưu tiên");
                return;
            }
            var _MaLDVs = new List<int>();
            foreach (var s in (lkTrangThai.EditValue ?? "").ToString().Split(','))
            {
                _MaLDVs.Add(int.Parse(s));
            }
            var _MaYCs = new List<int>();
            foreach (var s in (lkDoUuTien.EditValue ?? "").ToString().Split(','))
            {
                _MaYCs.Add(int.Parse(s));
            }
            tuNgay = dateTuNgay.DateTime;
            denNgay = dateDenNgay.DateTime;


            switch (CateID)
            {

             
                case 81:
                    this.PrintControl.Report =
                  new rptBaoCaoyeuCauKhieuNaiKhachHangThue(tuNgay, denNgay, byte.Parse(lookUpToaNha.EditValue.ToString()), _MaLDVs, _MaYCs);

                    break;
            }

        }

    }
}
